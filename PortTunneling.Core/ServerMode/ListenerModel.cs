using PortTunneling.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling.ServerMode
{
    internal sealed class ListenerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int ID { get; private set; }
        public int SrcPort { get; private set; }

        private DnsEndPoint _DstEndPoint;
        public DnsEndPoint DstEndPoint
        {
            get => _DstEndPoint;
            private set
            {
                if(_DstEndPoint != value)
                {
                    _DstEndPoint = value;
                    OnPropertyChanged();
                }
            }
        }
        //public string Description { get; private set; }

        private PhysicalAddress _Mac;
        public PhysicalAddress Mac
        {
            get => _Mac;
            private set
            {
                if(_Mac != value)
                {
                    _Mac = value;
                    OnPropertyChanged();
                }
            }
        }

        public ListenerModel(TunnelsHolder.Listener listener)
        {
            ID = listener.ID;
            SrcPort = listener.SrcPort;
            DstEndPoint = listener.DstEndPoint;
            //Description = listener.Description;
            Mac = listener.Mac;

            listener.DstEndPointChanged += Listener_DstEndPointChanged;
            listener.MacChanged += Listener_MacChanged;
        }

        private void Listener_MacChanged(object sender, PhysicalAddress mac)
        {
            Mac = mac;
        }

        private void Listener_DstEndPointChanged(object sender, DnsEndPoint e)
        {
            DstEndPoint = e;
        }

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
