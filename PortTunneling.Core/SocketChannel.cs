using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PortTunneling
{
    internal sealed class SocketChannel : IDisposable
    {
        private const int BufferSize = 32768; // 32 кБ.
        private readonly byte[] _buffer;
        private readonly Socket _socketSrc;
        private readonly Socket _socketDst;
        private readonly SocketAsyncEventArgs _receiveArgs;
        private readonly SocketAsyncEventArgs _sendArgs;
        private readonly Action _onErrorCallback;
        /// <summary>
        /// Показывает сколько сокетов еще используется.
        /// </summary>
        private readonly CountdownEvent _ce = new CountdownEvent(2);
        private int _framesToSend = 0;
        private int _lastReceiveOffset;
        private volatile int _lastReceiveCount;
        private int _shutdown;
        /// <summary>
        /// 0 — Idle
        /// 1 — Pending
        /// 2 — Aborted
        /// </summary>
        private int _rcvStat = 0;
        /// <summary>
        /// 0 — Idle
        /// 1 — Pending
        /// 2 — Aborted
        /// </summary>
        private int _sendStat = 0;
        /// <summary>
        /// Сколько байт из блока нужно отправить.
        /// </summary>
        private int _countToSend;

        public SocketChannel(Socket socketSrc, Socket socketDst, Action onErrorCallback)
        {
            _socketSrc = socketSrc;
            _socketDst = socketDst;
            _onErrorCallback = onErrorCallback;

            _buffer = new byte[BufferSize * 2];

            _receiveArgs = new SocketAsyncEventArgs();
            _receiveArgs.Completed += OnReceived;
            _receiveArgs.SetBuffer(_buffer, 0, BufferSize);

            _sendArgs = new SocketAsyncEventArgs();
            _sendArgs.Completed += OnSended;
            _sendArgs.SetBuffer(_buffer, 0, 0);
        }

        /// <summary>
        /// Начинает читать из Src и отправлять в Dst.
        /// </summary>
        public void StartExchange()
        {
            _socketSrc.SetKeepAlive(TimeSpan.FromMinutes(10.0), TimeSpan.FromSeconds(1.0));

            // Disable the Nagle Algorithm for this tcp socket.
            //_socketSrc.NoDelay = true;

            // что-бы первый блок был принят со смещением 0.
            _lastReceiveOffset = BufferSize;

            TryReceive();
        }

        private bool TryReceive()
        {
            if (Interlocked.CompareExchange(ref _rcvStat, 1, 0) == 0)
            {
                // Настроить приём в соседний блок.
                int offset = (_lastReceiveOffset == 0 ? BufferSize : 0);
                _lastReceiveOffset = offset;

                _receiveArgs.SetBuffer(offset, BufferSize);

                if (!_socketSrc.ReceiveAsync(_receiveArgs))
                    OnReceived(null, _receiveArgs);

                return true;
            }
            else
            // Был выполнен атомарный Shutdown для приёма.
            {
                return false;
            }
        }

        private bool TrySend(int offset, int count)
        {
            // Windows использует WSA_FLAG_OVERLAPPED — значит событие сработает когда все байты будут отправлены.

            if (Interlocked.CompareExchange(ref _sendStat, 1, 0) == 0)
            {
                _countToSend = count;

                // Отправить текущий блок.
                _sendArgs.SetBuffer(offset, count);

                if (!_socketDst.SendAsync(_sendArgs))
                    OnSended(null, _sendArgs);

                return true;
            }
            else
            // Был выполнен атомарный Shutdown для отправки.
            {
                return false;
            }
        }

        private void OnReceived(object sender, SocketAsyncEventArgs e)
        {
            if (Interlocked.CompareExchange(ref _rcvStat, 0, 1) == 1) // Может быть два состояния — 1 или 2.
            {
                // Если был выполнен Socket.Shutdown() то BytesTransferred будет 0, а SocketError == Success.
                if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
                {
                    // Если считан второй блок, а первый еще не отправлен то атомарно
                    // передать обязанность на отправку другому потоку.

                    // volatile переменная что-бы другой поток прочитал актуальное значение.
                    _lastReceiveCount = e.BytesTransferred;

                    if (Interlocked.Increment(ref _framesToSend) <= 1)
                    // Отправить блок должны мы.
                    {
                        // Отправить текущий блок.
                        if (TrySend(e.Offset, e.BytesTransferred))
                        {
                            // Начать приём.
                            TryReceive();
                        }
                    }
                }
                else
                // Обрыв соединения.
                {
                    AtomicShutdown();
                }
            }
            else
            // Был выполнен атомарный Shutdown для приёма.
            {
                // Можно безопасно освободить ресурсы.
                _receiveArgs.Dispose();
                Debug.WriteLine("_receiveArgs disposed");
                _ce.Signal();
            }
        }

        private void OnSended(object sender, SocketAsyncEventArgs e)
        {
            if (Interlocked.CompareExchange(ref _sendStat, 0, 1) == 1) // Может быть два состояния — 1 или 2.
            {
                if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
                {
                    if (e.BytesTransferred == _countToSend)
                    // Все байты из блока успешно отправлены.
                    {
                        if (Interlocked.Decrement(ref _framesToSend) > 0)
                        // Соседний блок уже готов к отправке. Его отправить теперь обязаны мы.
                        {
                            // Координаты соседнего блока.
                            int offset = (e.Offset == 0 ? BufferSize : 0);

                            // Начать отправку.
                            if (TrySend(offset, _lastReceiveCount))
                            {
                                // Начать чтение тоже должны мы.
                                TryReceive();
                            }
                        }
                    }
                    else
                    // На Linux операция может завершиться отправив не все данные.
                    {
                        // Повторно отправить оставшуюся часть.
                        TrySend(e.Offset + e.BytesTransferred, _countToSend - e.BytesTransferred);
                    }
                }
                else
                // Обрыв соединения.
                {
                    AtomicShutdown();
                }
            }
            else
            // Был выполнен атомарный Shutdown для отправки.
            {
                // Можно безопасно освободить ресурсы.
                _sendArgs.Dispose();
                Debug.WriteLine("_sendArgs disposed");
                _ce.Signal();
            }
        }

        private void AtomicShutdown()
        {
            if (Interlocked.CompareExchange(ref _shutdown, 1, 0) == 0)
            {
                Debug.WriteLine("SocketChannel.AtomicShutdown()");

                // Атомарно прекратить чтение.
                if (Interlocked.Exchange(ref _rcvStat, 2) == 0)
                // Операция чтения в этот момент не выполнялась.
                {
                    // Можно безопасно освободить ресурсы.
                    _receiveArgs.Dispose();
                    Debug.WriteLine("_receiveArgs disposed");
                    _ce.Signal();
                }

                if (Interlocked.Exchange(ref _sendStat, 2) == 0)
                // Операция отправки в этот момент не выполнялась.
                {
                    // Можно безопасно освободить ресурсы.
                    _sendArgs.Dispose();
                    Debug.WriteLine("_sendArgs disposed");
                    _ce.Signal();
                }
                
                try
                {
                    if (_socketSrc.Connected)
                    {
                        _socketSrc.Shutdown(SocketShutdown.Both);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                try
                {
                    if (_socketDst.Connected)
                    {
                        _socketDst.Shutdown(SocketShutdown.Both);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                _onErrorCallback.Invoke();
            }
        }

        /// <summary>
        /// Потокобезопасно останавливает обмен между сокетами.
        /// </summary>
        public void Shutdown()
        {
            AtomicShutdown();
            _ce.Wait();
        }

        public void Dispose()
        {
            _ce.Dispose();
        }
    }
}
