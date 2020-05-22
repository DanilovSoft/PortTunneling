using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling
{
	public class Config : AppConfiguration, INotifyPropertyChanged
	{
		private ushort _clientPort = 1000;
		private ushort _serverPort = 1000;
        private string _clientServiceName = "Port Tuneling Client";
        private string _serverServiceName = "Port Tuneling Server";
        private string _clientHost;
		public event PropertyChangedEventHandler PropertyChanged;

        public ushort ClientPort 
		{
			get => _clientPort;
			set
            {
                if (_clientPort != value)
                {
                    _clientPort = value;
                    OnPropertyChanged();
                }
            }
		}

		public ushort ServerPort 
		{
			get => _serverPort;
			set
            {
                if (_serverPort != value)
                {
                    _serverPort = value;
                    OnPropertyChanged();
                }
            }
		}

		public string ClientServiceName 
		{
			get => _clientServiceName;
			set
            {
                if (_clientServiceName != value)
                {
                    _clientServiceName = value;
                    OnPropertyChanged();
                }
            }
		}

		public string ServerServiceName 
		{
			get => _serverServiceName;
			set
            {
                if (_serverServiceName != value)
                {
                    _serverServiceName = value;
                    OnPropertyChanged();
                }
            }
		}

		public string ClientHost 
		{
			get => _clientHost;
			set
            {
                if (_clientHost != value)
                {
                    _clientHost = value;
                    OnPropertyChanged();
                }
            }
		}

		private void OnPropertyChanged([CallerMemberName] string name = null)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
	}
}
