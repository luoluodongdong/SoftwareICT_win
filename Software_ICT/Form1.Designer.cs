namespace Software_ICT
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.testPage = new System.Windows.Forms.TabPage();
            this.debugLB = new System.Windows.Forms.Label();
            this.logPage = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.setPage = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.debugBtn = new System.Windows.Forms.RadioButton();
            this.testModeBtn = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.swPB = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TimerLB = new System.Windows.Forms.Label();
            this.StartBtn = new System.Windows.Forms.Button();
            this.inputSN_TB = new System.Windows.Forms.TextBox();
            this.StatusLB = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CleanBtn = new System.Windows.Forms.Button();
            this.yieldLB = new System.Windows.Forms.Label();
            this.failLB = new System.Windows.Forms.Label();
            this.passLB = new System.Windows.Forms.Label();
            this.inputLB = new System.Windows.Forms.Label();
            this.swLabel = new System.Windows.Forms.Label();
            this.verLabel = new System.Windows.Forms.Label();
            this.testTimer = new System.Windows.Forms.Timer(this.components);
            this.listView1 = new Software_ICT.ListViewNF();
            this.NumHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ItemsHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StatusHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LowHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UpHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UnitHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DurationHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.testPage.SuspendLayout();
            this.logPage.SuspendLayout();
            this.setPage.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.swPB)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.testPage);
            this.tabControl1.Controls.Add(this.logPage);
            this.tabControl1.Controls.Add(this.setPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 131);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(708, 565);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Click += new System.EventHandler(this.TabControl1_Click);
            // 
            // testPage
            // 
            this.testPage.Controls.Add(this.listView1);
            this.testPage.Location = new System.Drawing.Point(4, 29);
            this.testPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.testPage.Name = "testPage";
            this.testPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.testPage.Size = new System.Drawing.Size(700, 532);
            this.testPage.TabIndex = 0;
            this.testPage.Text = "Test";
            this.testPage.UseVisualStyleBackColor = true;
            // 
            // debugLB
            // 
            this.debugLB.AutoSize = true;
            this.debugLB.Font = new System.Drawing.Font("微软雅黑", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.debugLB.ForeColor = System.Drawing.Color.Red;
            this.debugLB.Location = new System.Drawing.Point(781, 466);
            this.debugLB.Name = "debugLB";
            this.debugLB.Size = new System.Drawing.Size(159, 50);
            this.debugLB.TabIndex = 1;
            this.debugLB.Text = "DEBUG";
            this.debugLB.Visible = false;
            // 
            // logPage
            // 
            this.logPage.Controls.Add(this.listBox1);
            this.logPage.Location = new System.Drawing.Point(4, 29);
            this.logPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.logPage.Name = "logPage";
            this.logPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.logPage.Size = new System.Drawing.Size(700, 532);
            this.logPage.TabIndex = 1;
            this.logPage.Text = "Log";
            this.logPage.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(8, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(686, 504);
            this.listBox1.TabIndex = 0;
            // 
            // setPage
            // 
            this.setPage.Controls.Add(this.groupBox4);
            this.setPage.Location = new System.Drawing.Point(4, 29);
            this.setPage.Name = "setPage";
            this.setPage.Size = new System.Drawing.Size(700, 532);
            this.setPage.TabIndex = 2;
            this.setPage.Text = "Set";
            this.setPage.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.debugBtn);
            this.groupBox4.Controls.Add(this.testModeBtn);
            this.groupBox4.Location = new System.Drawing.Point(18, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(466, 100);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Mode";
            // 
            // debugBtn
            // 
            this.debugBtn.AutoSize = true;
            this.debugBtn.Location = new System.Drawing.Point(233, 57);
            this.debugBtn.Name = "debugBtn";
            this.debugBtn.Size = new System.Drawing.Size(80, 24);
            this.debugBtn.TabIndex = 1;
            this.debugBtn.Text = "DEBUG";
            this.debugBtn.UseVisualStyleBackColor = true;
            this.debugBtn.CheckedChanged += new System.EventHandler(this.debugBtn_CheckedChanged);
            // 
            // testModeBtn
            // 
            this.testModeBtn.AutoSize = true;
            this.testModeBtn.Checked = true;
            this.testModeBtn.Location = new System.Drawing.Point(35, 53);
            this.testModeBtn.Name = "testModeBtn";
            this.testModeBtn.Size = new System.Drawing.Size(65, 24);
            this.testModeBtn.TabIndex = 0;
            this.testModeBtn.TabStop = true;
            this.testModeBtn.Text = "TEST";
            this.testModeBtn.UseVisualStyleBackColor = true;
            this.testModeBtn.CheckedChanged += new System.EventHandler(this.testModeBtn_CheckedChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Software_ICT.Properties.Resources.Luxshare;
            this.pictureBox2.Location = new System.Drawing.Point(710, 618);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(284, 78);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // swPB
            // 
            this.swPB.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.swPB.Image = global::Software_ICT.Properties.Resources._2019_03_12_140505;
            this.swPB.Location = new System.Drawing.Point(0, 0);
            this.swPB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swPB.Name = "swPB";
            this.swPB.Size = new System.Drawing.Size(1243, 132);
            this.swPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.swPB.TabIndex = 0;
            this.swPB.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TimerLB);
            this.groupBox1.Controls.Add(this.StartBtn);
            this.groupBox1.Controls.Add(this.inputSN_TB);
            this.groupBox1.Location = new System.Drawing.Point(715, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 122);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SN";
            // 
            // TimerLB
            // 
            this.TimerLB.AutoSize = true;
            this.TimerLB.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TimerLB.Location = new System.Drawing.Point(6, 82);
            this.TimerLB.Name = "TimerLB";
            this.TimerLB.Size = new System.Drawing.Size(33, 27);
            this.TimerLB.TabIndex = 2;
            this.TimerLB.Text = "0s";
            // 
            // StartBtn
            // 
            this.StartBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartBtn.Location = new System.Drawing.Point(147, 74);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(115, 35);
            this.StartBtn.TabIndex = 1;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // inputSN_TB
            // 
            this.inputSN_TB.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.inputSN_TB.Location = new System.Drawing.Point(6, 26);
            this.inputSN_TB.Name = "inputSN_TB";
            this.inputSN_TB.Size = new System.Drawing.Size(267, 34);
            this.inputSN_TB.TabIndex = 0;
            this.inputSN_TB.Text = "123456";
            this.inputSN_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StatusLB
            // 
            this.StatusLB.BackColor = System.Drawing.Color.DarkGray;
            this.StatusLB.Font = new System.Drawing.Font("微软雅黑", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StatusLB.Location = new System.Drawing.Point(714, 463);
            this.StatusLB.Name = "StatusLB";
            this.StatusLB.Size = new System.Drawing.Size(280, 145);
            this.StatusLB.TabIndex = 3;
            this.StatusLB.Text = "Ready";
            this.StatusLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CleanBtn);
            this.groupBox2.Controls.Add(this.yieldLB);
            this.groupBox2.Controls.Add(this.failLB);
            this.groupBox2.Controls.Add(this.passLB);
            this.groupBox2.Controls.Add(this.inputLB);
            this.groupBox2.Location = new System.Drawing.Point(714, 269);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 191);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Panel";
            // 
            // CleanBtn
            // 
            this.CleanBtn.Location = new System.Drawing.Point(188, 137);
            this.CleanBtn.Name = "CleanBtn";
            this.CleanBtn.Size = new System.Drawing.Size(75, 33);
            this.CleanBtn.TabIndex = 4;
            this.CleanBtn.Text = "Clean";
            this.CleanBtn.UseVisualStyleBackColor = true;
            this.CleanBtn.Click += new System.EventHandler(this.CleanBtn_Click);
            // 
            // yieldLB
            // 
            this.yieldLB.AutoSize = true;
            this.yieldLB.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.yieldLB.Location = new System.Drawing.Point(21, 141);
            this.yieldLB.Name = "yieldLB";
            this.yieldLB.Size = new System.Drawing.Size(122, 27);
            this.yieldLB.TabIndex = 3;
            this.yieldLB.Text = "Yield:0.00%";
            // 
            // failLB
            // 
            this.failLB.AutoSize = true;
            this.failLB.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.failLB.Location = new System.Drawing.Point(21, 107);
            this.failLB.Name = "failLB";
            this.failLB.Size = new System.Drawing.Size(169, 27);
            this.failLB.TabIndex = 2;
            this.failLB.Text = "Fail:0123456789";
            // 
            // passLB
            // 
            this.passLB.AutoSize = true;
            this.passLB.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passLB.Location = new System.Drawing.Point(21, 69);
            this.passLB.Name = "passLB";
            this.passLB.Size = new System.Drawing.Size(178, 27);
            this.passLB.TabIndex = 1;
            this.passLB.Text = "Pass:0123456789";
            // 
            // inputLB
            // 
            this.inputLB.AutoSize = true;
            this.inputLB.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.inputLB.Location = new System.Drawing.Point(21, 32);
            this.inputLB.Name = "inputLB";
            this.inputLB.Size = new System.Drawing.Size(187, 27);
            this.inputLB.TabIndex = 0;
            this.inputLB.Text = "Input:0123456789";
            // 
            // swLabel
            // 
            this.swLabel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.swLabel.Font = new System.Drawing.Font("微软雅黑", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.swLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.swLabel.Location = new System.Drawing.Point(74, 21);
            this.swLabel.Name = "swLabel";
            this.swLabel.Size = new System.Drawing.Size(848, 43);
            this.swLabel.TabIndex = 2;
            this.swLabel.Text = "Software Name";
            this.swLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // verLabel
            // 
            this.verLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.verLabel.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.verLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.verLabel.Location = new System.Drawing.Point(794, 91);
            this.verLabel.Name = "verLabel";
            this.verLabel.Size = new System.Drawing.Size(156, 32);
            this.verLabel.TabIndex = 3;
            this.verLabel.Text = "v1.0.0";
            // 
            // testTimer
            // 
            this.testTimer.Interval = 200;
            this.testTimer.Tick += new System.EventHandler(this.testTimer_Tick);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NumHeader,
            this.ItemsHeader,
            this.StatusHeader,
            this.ValueHeader,
            this.LowHeader,
            this.TypValueHeader,
            this.UpHeader1,
            this.UnitHeader1,
            this.DurationHeader1});
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(-4, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(698, 518);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // NumHeader
            // 
            this.NumHeader.Text = "Num";
            // 
            // ItemsHeader
            // 
            this.ItemsHeader.Text = "Items";
            this.ItemsHeader.Width = 226;
            // 
            // StatusHeader
            // 
            this.StatusHeader.Text = "Status";
            this.StatusHeader.Width = 59;
            // 
            // ValueHeader
            // 
            this.ValueHeader.Text = "Value";
            this.ValueHeader.Width = 169;
            // 
            // LowHeader
            // 
            this.LowHeader.Text = "Low";
            this.LowHeader.Width = 64;
            // 
            // TypValueHeader
            // 
            this.TypValueHeader.Text = "TypeValue";
            // 
            // UpHeader1
            // 
            this.UpHeader1.Text = "Up";
            // 
            // UnitHeader1
            // 
            this.UnitHeader1.Text = "Unit";
            this.UnitHeader1.Width = 53;
            // 
            // DurationHeader1
            // 
            this.DurationHeader1.Text = "Duration";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 695);
            this.Controls.Add(this.debugLB);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.StatusLB);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.verLabel);
            this.Controls.Add(this.swLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.swPB);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Software_ICT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.tabControl1.ResumeLayout(false);
            this.testPage.ResumeLayout(false);
            this.logPage.ResumeLayout(false);
            this.setPage.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.swPB)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox swPB;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage testPage;
        private System.Windows.Forms.TabPage logPage;
        private System.Windows.Forms.PictureBox pictureBox2;
        private ListViewNF listView1;
        private System.Windows.Forms.ColumnHeader NumHeader;
        private System.Windows.Forms.ColumnHeader ItemsHeader;
        private System.Windows.Forms.ColumnHeader StatusHeader;
        private System.Windows.Forms.ColumnHeader ValueHeader;
        private System.Windows.Forms.TabPage setPage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label StatusLB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label TimerLB;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.TextBox inputSN_TB;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label yieldLB;
        private System.Windows.Forms.Label failLB;
        private System.Windows.Forms.Label passLB;
        private System.Windows.Forms.Label inputLB;
        private System.Windows.Forms.Label swLabel;
        private System.Windows.Forms.Label verLabel;
        private System.Windows.Forms.ColumnHeader LowHeader;
        private System.Windows.Forms.Timer testTimer;
        private System.Windows.Forms.Button CleanBtn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton debugBtn;
        private System.Windows.Forms.RadioButton testModeBtn;
        private System.Windows.Forms.Label debugLB;
        private System.Windows.Forms.ColumnHeader TypValueHeader;
        private System.Windows.Forms.ColumnHeader UpHeader1;
        private System.Windows.Forms.ColumnHeader UnitHeader1;
        private System.Windows.Forms.ColumnHeader DurationHeader1;
    }
}

