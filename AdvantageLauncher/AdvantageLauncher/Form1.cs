using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace AdvantageLauncher
{


    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            // Starts TabletInputService
            startServ();

        }


        private void showButton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.BackColor = Color.LimeGreen;
                checkBox1.Text = "Start Timekeeper";
                Process.Start("cmd", "/C Taskkill /IM tsentry.exe /F").WaitForExit();
                startServ();
                
            }
            else
            {
                checkBox1.BackColor = Color.Red;
                checkBox1.Text = "Stop Timekeeper";
                stopServ();
                Process.Start(@"C:\Program Files (x86)\Deltek\Advantage\9.1\tsentry.exe");

            }
           
        }
        private void showButton2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox2.BackColor = Color.LimeGreen;
                checkBox2.Text = "Start Expensekeeper";
                Process.Start("cmd", "/C Taskkill /IM expense.exe /F").WaitForExit();
                startServ();

            }
            else
            {
                checkBox2.BackColor = Color.Red;
                checkBox2.Text = "Stop Expensekeeper";
                stopServ();
                Process.Start(@"C:\Program Files (x86)\Deltek\Advantage\9.1\expense.exe");

            }

        }
        private void showButton3_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox3.BackColor = Color.LimeGreen;
                checkBox3.Text = "Start Advantage";
                Process.Start("cmd", "/C Taskkill /IM DeltekAdvantage.exe /F").WaitForExit();
                startServ();

            }
            else
            {
                checkBox3.BackColor = Color.Red;
                checkBox3.Text = "Stop Advantage";
                stopServ();
                Process.Start(@"C:\Program Files (x86)\Deltek\Advantage\9.1\DeltekAdvantage.exe");

            }

        }

        private void showButton4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore","https://sp.thermaltech.com/Office%20Resources/Advantage%20Errors.aspx");
        }

        private void showButton5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please send details of the error and a screenshot to redmine.thermaltech@gmail.com");
            System.Diagnostics.Process.Start("mailto:redmine.thermaltech@gmail.com");
        }

        private void startServ()
        {
            var serviceName = "TabletInputService";
            string objPath = string.Format("Win32_Service.Name='{0}'", serviceName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                service.InvokeMethod("ChangeStartMode", new object[] { "Automatic" });
            }
            Process.Start("net", "start TabletInputService").WaitForExit();
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                service.InvokeMethod("ChangeStartMode", new object[] { "Automatic" });
            }

        }
        private void stopServ()
        {
            Process.Start("net", "stop TabletInputService").WaitForExit();
            Process.Start("sc", "config TabletInputService start= disabled").WaitForExit();
        }
    }



}

