using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServicesPainelController
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Visible = false;
            GetwindowServices();


        }

        private void GetwindowServices()
        {
            //throw new NotImplementedException();
            ServiceController[] service;
            service = ServiceController.GetServices();
            comboBox1.Items.Clear();
            for (int i = 0; i < service.Length; i++)
            {
                comboBox1.Items.Add(service[i].ServiceName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var singleService = new ServiceController((string)e.Argument);
            if (singleService.Status.Equals(ServiceControllerStatus.Stopped) || (singleService.Status.Equals(ServiceControllerStatus.StopPending)))
            {

                singleService.Start();
            }
            else
            {
                singleService.Stop();
            }
            

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.button1.Enabled = false;
            this.progressBar1.Visible = false;
            if (e.Error!=null)
            {
                MessageBox.Show(e.Error.ToString(),"Could not start the service");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            this.button2.Enabled = true;
            this.progressBar1.Visible = true;
            this.backgroundWorker1.RunWorkerAsync(comboBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            this.button2.Enabled = true;
            this.progressBar1.Visible = true;
            this.backgroundWorker1.RunWorkerAsync(comboBox1.Text);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ServiceController singleService = new ServiceController(comboBox1.Text);
            if (singleService.Status.Equals(ServiceControllerStatus.Stopped) || (singleService.Status.Equals(ServiceControllerStatus.StopPending)))
            {
                button2.Enabled = false;
                button1.Enabled = true;
            }
            else
            {
                button2.Enabled = true;
                button1.Enabled = false;
            }

        }
    }
}
