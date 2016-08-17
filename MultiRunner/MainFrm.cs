using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MultiRunner
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }
        List<string> filelist;
        List<int> cpulist;
        string[] current_cmdlist;
        List<Queue<string>> queuelist;
        List<bool> busylist;
        int currentfile;

        void FillListCPUs()
        {
            int count = Environment.ProcessorCount;
            for (int i = 0; i < count; i++)
            {
                lstCPUs.Items.Add("CPU " + i.ToString());
                if(i < count - 3)
                    lstCPUs.SetItemChecked(i, true);
            }
        }

        void Import2Queue()
        {
            lstFiles.SelectedIndex = currentfile;
            string filename = filelist[currentfile];
            List<string> cmdlist = new List<string>();
            StreamReader reader = new StreamReader(filename);
            while (!reader.EndOfStream)
                cmdlist.Add(reader.ReadLine());
            int d = (int)Math.Ceiling(1.0 * cmdlist.Count / cpulist.Count);
            
            for (int i = 0; i < cpulist.Count; i++)
            {
                for (int j = 0; j < d; j++)
                    if(i * d + j < cmdlist.Count)
                        queuelist[i].Enqueue(cmdlist[i * d + j]);
            }
        }

        void RunQueue()
        {
            for (int i = 0; i < cpulist.Count; i++)
            {
                RunCmd(i);
            }
        }

        void RunCmd(int i)
        {
            if (queuelist[i].Count == 0)
                return;
            busylist[i] = true;
            string cpuid = Convert.ToString(1<<cpulist[i], 16);
            string cmd = queuelist[i].Dequeue().Replace("@","@"+i);
            current_cmdlist[i] = cmd;
            string arg = "/c start /WAIT /AFFINITY " + cpuid + " /min cmd /c \"" + cmd + "\"";
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("cmd.exe", arg);
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.EnableRaisingEvents = true;
            p.Exited += P_Exited;
            p.Start();
        }

        private void P_Exited(object sender, EventArgs e)
        {
            Process p = (Process)sender;
            string cpuid = p.StartInfo.Arguments.Split(new char[] { ' ' }, 6)[4];
            int cpu = (int)(Math.Log(Convert.ToInt32(cpuid, 16))/Math.Log(2));
            busylist[(cpulist.IndexOf(cpu))] = false;

            //did not work
            //if (!tWait.Enabled)
            //    tWait.Start();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            FillListCPUs();
            filelist = new List<string>();
            cpulist = new List<int>();
            queuelist = new List<Queue<string>>();
            busylist = new List<bool>();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            filelist.Add(dlg.FileName);
            lstFiles.Items.Add(Path.GetFileName(dlg.FileName));
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstFiles.SelectedIndex < 0)
                return;
            int k = lstFiles.SelectedIndex;
            filelist.RemoveAt(k);
            lstFiles.Items.RemoveAt(k);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (lstFiles.Items.Count == 0)
                return;
            if (lstCPUs.CheckedItems.Count == 0)
                return;

            cpulist.Clear();
            queuelist.Clear();
            busylist.Clear();
            lstCommands.Items.Clear();
            foreach (int k in lstCPUs.CheckedIndices)
            {
                cpulist.Add(k);
                queuelist.Add(new Queue<string>());
                lstCommands.Items.Add("");
                busylist.Add(false);
            }
            current_cmdlist = new string[cpulist.Count];
            

            currentfile = 0;
            Import2Queue();
            RunQueue();
        }

        bool tick_busy = false;
        private void tWait_Tick(object sender, EventArgs e)
        {
            if (tick_busy) return;
            tick_busy = true;
            //tWait.Enabled = false;

            stSum.Text = "";
            for (int i = 0; i < cpulist.Count; i++)
            {
                if (busylist[i]) continue;
                stSum.Text += i.ToString() + ", ";
                RunCmd(i);
            }

            bool finished = true;
            for (int i = 0; i < cpulist.Count; i++)
            {
                if (queuelist[i].Count != 0) finished = false;
                string status = i.ToString("00") + ": (";
                status += queuelist[i].Count.ToString("000") + ") : ";
                status += current_cmdlist[i];
                lstCommands.Items[i] = status;
            }
            if (finished)
            {
                currentfile++;
                if (currentfile < filelist.Count)
                {
                    Import2Queue();
                    RunQueue();
                }
            }

            tick_busy = false;
        }

        private void nWait_ValueChanged(object sender, EventArgs e)
        {
            tWait.Interval = (int)nWait.Value;
        }
    }
}
