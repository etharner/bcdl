using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bcdlgui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
    
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            ProcessStartInfo startInfo = new ProcessStartInfo("python.exe", " bcdl.py " + numericUpDown1.Value);
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = startInfo;
            p.EnableRaisingEvents = true;
            p.OutputDataReceived += new DataReceivedEventHandler(OnDataReceived);
            p.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceived);
            p.Exited += new EventHandler(OnExit);
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBox1.AppendText(text + "\n");
            }
        }

        public void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                SetText(e.Data);
            }
        }

        void OnExit(object sender, System.EventArgs e)
        {
            button1.Enabled = true;
        }
    }
}
