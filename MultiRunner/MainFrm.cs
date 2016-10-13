using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
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
        List<Process> processlist;
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
            processlist[i] = p;
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
            processlist = new List<Process>();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            foreach (string filename in dlg.FileNames)
            {
                filelist.Add(filename);
                lstFiles.Items.Add(Path.GetFileName(filename));
            }

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
            processlist.Clear();
            lstCommands.Items.Clear();
            foreach (int k in lstCPUs.CheckedIndices)
            {
                cpulist.Add(k);
                queuelist.Add(new Queue<string>());
                lstCommands.Items.Add("");
                busylist.Add(false);
                processlist.Add(null);
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

            for (int i = 0; i < cpulist.Count; i++)
            {
                if (busylist[i]) continue;
                if (queuelist[i].Count != 0) continue;
                for (int j = 0; j < cpulist.Count; j++)
                {
                    if (busylist[j]) continue;
                    if (queuelist[j].Count > 1)
                    {
                        queuelist[i].Enqueue(queuelist[j].Dequeue());
                        break;
                    }
                }
            }

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

        //ref: http://stackoverflow.com/questions/30249873/process-kill-doesnt-seem-to-kill-the-process
        private static void KillProcessAndChildrens(int pid)
        {
            ManagementObjectSearcher processSearcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection processCollection = processSearcher.Get();

            try
            {
                Process proc = Process.GetProcessById(pid);
                if (!proc.HasExited) proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }

            if (processCollection != null)
            {
                foreach (ManagementObject mo in processCollection)
                {
                    KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"])); 
                    //kill child processes(also kills childrens of childrens etc.)
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want to stop?","MultiRunner", MessageBoxButtons.YesNo) 
                == DialogResult.Yes)
            {
                tWait.Enabled = false;
                for (int i = 0; i < processlist.Count; i++)
                    KillProcessAndChildrens(processlist[i].Id);
            }
        }
    }
}
