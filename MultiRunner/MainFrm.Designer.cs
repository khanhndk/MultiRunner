namespace MultiRunner
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnImport = new System.Windows.Forms.Button();
            this.lstCommands = new System.Windows.Forms.ListBox();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.lstCPUs = new System.Windows.Forms.CheckedListBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stSum = new System.Windows.Forms.ToolStripStatusLabel();
            this.tWait = new System.Windows.Forms.Timer(this.components);
            this.nWait = new System.Windows.Forms.NumericUpDown();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nWait)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(12, 133);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lstCommands
            // 
            this.lstCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCommands.FormattingEnabled = true;
            this.lstCommands.Location = new System.Drawing.Point(12, 199);
            this.lstCommands.Name = "lstCommands";
            this.lstCommands.Size = new System.Drawing.Size(444, 186);
            this.lstCommands.TabIndex = 1;
            // 
            // lstFiles
            // 
            this.lstFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.Location = new System.Drawing.Point(93, 12);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(363, 173);
            this.lstFiles.TabIndex = 2;
            // 
            // lstCPUs
            // 
            this.lstCPUs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCPUs.FormattingEnabled = true;
            this.lstCPUs.Location = new System.Drawing.Point(462, 12);
            this.lstCPUs.Name = "lstCPUs";
            this.lstCPUs.Size = new System.Drawing.Size(120, 349);
            this.lstCPUs.TabIndex = 3;
            this.lstCPUs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstCPUs_ItemCheck);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(12, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(12, 41);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 5;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(12, 162);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stSum});
            this.statusStrip1.Location = new System.Drawing.Point(0, 389);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(594, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stSum
            // 
            this.stSum.Name = "stSum";
            this.stSum.Size = new System.Drawing.Size(39, 17);
            this.stSum.Text = "Ready";
            // 
            // tWait
            // 
            this.tWait.Enabled = true;
            this.tWait.Interval = 10000;
            this.tWait.Tick += new System.EventHandler(this.tWait_Tick);
            // 
            // nWait
            // 
            this.nWait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nWait.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nWait.Location = new System.Drawing.Point(462, 366);
            this.nWait.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nWait.Name = "nWait";
            this.nWait.Size = new System.Drawing.Size(120, 20);
            this.nWait.TabIndex = 9;
            this.nWait.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nWait.ThousandsSeparator = true;
            this.nWait.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nWait.ValueChanged += new System.EventHandler(this.nWait_ValueChanged);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(12, 99);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(12, 70);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(75, 23);
            this.btnResume.TabIndex = 11;
            this.btnResume.Text = "Resume";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 411);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.nWait);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.lstCPUs);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.lstCommands);
            this.Controls.Add(this.btnImport);
            this.Name = "MainFrm";
            this.Text = "MultiRunner";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nWait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ListBox lstCommands;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.CheckedListBox lstCPUs;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stSum;
        private System.Windows.Forms.Timer tWait;
        private System.Windows.Forms.NumericUpDown nWait;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnResume;
    }
}

