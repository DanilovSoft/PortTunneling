using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Reflection;
using System.Configuration.Install;

namespace PortTunneling
{
    public partial class ServiceControl : UserControl
    {
        public string ServiceName
        {
            get
            {
                return textBox_ServiceName.Text;
            }
            set
            {
                textBox_ServiceName.Text = value;
            }
        }

        public string ServiceSatatus
        {
            get
            {
                return textBox_ServiceStatus.Text;
            }
            set
            {
                textBox_ServiceStatus.Text = value;
            }
        }

        public string HeaderName
        {
            get
            {
                return groupBox3.Text;
            }
            set
            {
                groupBox3.Text = value;
            }
        }

        public string Args { get; set; }

        public ServiceControl()
        {
            InitializeComponent();
        }

        private ServiceController GetMyService(string name)
        {
            return ((IEnumerable<ServiceController>)ServiceController.GetServices())
                .FirstOrDefault(s => s.ServiceName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        private void Button_Create_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ServiceName))
                    throw new InvalidOperationException("Service name is missing.");

                var stringList = new List<string>()
                {
                  "/servicename=\"" + ServiceName + "\""
                };

                if (Args != null)
                    stringList.Add("/args=\"" + Args + "\"");

                stringList.Add(Assembly.GetExecutingAssembly().Location);
                ManagedInstallerClass.InstallHelper(stringList.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.GetBaseException().Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {
            string serviceName = ServiceName;
            if (GetMyService(serviceName) == null)
            {
                MessageBox.Show(this, "Service not exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    ManagedInstallerClass.InstallHelper(new string[3]
                    {
                        "/u",
                        "/servicename=\"" + serviceName + "\"",
                        Assembly.GetExecutingAssembly().Location
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.GetBaseException().Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            ServiceController myService = GetMyService(ServiceName);
            if (myService == null)
            {
                MessageBox.Show(this, "Service not exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (myService.Status == ServiceControllerStatus.Stopped)
                        return;

                    myService.Stop();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.GetBaseException().Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void Button_Start_Click(object sender, EventArgs e)
        {
            ServiceController myService = GetMyService(ServiceName);
            if (myService == null)
            {
                MessageBox.Show(this, "Service not exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (myService.Status == ServiceControllerStatus.Running)
                        return;
                    myService.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.GetBaseException().Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }
    }
}
