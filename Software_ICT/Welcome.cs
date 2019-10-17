﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Software_ICT
{
    public delegate void MsgFromWelcomPageEvent(string msg);
    public partial class Welcome : Form
    {
        public string _userName = "123";
        public string _password = "123";
        public bool autoLoginFlag = false;
        public string _StatusInfo = "";
        public int _ProgressValue = 0;

        public MsgFromWelcomPageEvent sendMsgToTT;
        public Welcome()
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            labStatus.Text = "";
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            labStatus.BackColor = Color.Transparent;
            this.Controls.Add(labStatus);

            okBtn.FlatStyle = FlatStyle.Flat;//样式
            okBtn.ForeColor = Color.Transparent;//前景
            okBtn.BackColor = Color.Transparent;//去背景
            okBtn.FlatAppearance.BorderSize = 0;//去边线
            okBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;//鼠标经过
            okBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;//鼠标按下
            //自动登陆
            if (autoLoginFlag)
            {
                loginPanel.Visible = false;
                progressBar1.Visible = true;
                ControlBox = false;
                sendMsgToTT("login");
            }
            Activate();
        }
        public string StatusInfo
        {
            set
            {
                _StatusInfo = value;
                ChangeStatusText();
            }
            get
            {
                return _StatusInfo;
            }
        }

        public void ChangeStatusText()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(this.ChangeStatusText));
                    return;
                }

                labStatus.Text = _StatusInfo;
            }
            catch (Exception e)
            {
                //    异常处理
            }
        }
        public int ProgressValue
        {
            set
            {
                _ProgressValue = value;
                ChangeProgressBar();
            }
            get
            {
                return _ProgressValue;
            }
        }

        public void ChangeProgressBar()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(this.ChangeProgressBar));
                    return;
                }

                progressBar1.Value=_ProgressValue;
            }
            catch (Exception e)
            {
                //    异常处理
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Welcome_FormClosing(object sender, FormClosingEventArgs e)
        {
            sendMsgToTT("close");
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
      
            loginPanel.Visible = false;
            sendMsgToTT("login");
            progressBar1.Visible = true;
            progressBar1.Value = 5;
        }
        private void willLogin()
        {
            string userName = userTB.Text;
            string password = passwordTB.Text;
            if (userName == _userName && password == _password)
            {
                //隐藏标题栏按钮
                this.ControlBox = false;
                loginPanel.Visible = false;
                sendMsgToTT("login");
                progressBar1.Visible = true;
                progressBar1.Value = 5;
            }
            else
            {
                MessageBox.Show("user name or password error!");
                userTB.Focus();
                
            }
        }

        private void userTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string userName = userTB.Text;
                if (userName.Length == 0)
                {
                    MessageBox.Show("User name is empty!");
                    userTB.Focus();
                    return;
                }
                if (userName == _userName)
                {
                    passwordTB.Focus();
                }
                else
                {
                    MessageBox.Show("User name error!");
                    userTB.Text = "";
                    userTB.Focus();
                }

            }
        }

        private void PasswordTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string password = passwordTB.Text;
                if (password.Length == 0) return;
                willLogin();

            }
        }

        private void Welcome_Shown(object sender, EventArgs e)
        {
            this.Activate();
            userTB.Focus();
        }
    }

    public class Splash
    {

        static Welcome MySplashForm = null;
        static Thread MySplashThread = null;
        //登陆是否OK
        static public bool loginIsOk = false;
        //点击关闭窗体按钮
        static public bool clickClose = false;
        static void ShowThread()
        {
            MySplashForm = new Welcome();
            string cfgFile = Application.StartupPath + @"\cfg.CFG";
            CIni ini = new CIni(cfgFile);
            if (System.IO.File.Exists(cfgFile))
            {
                MySplashForm._userName = ini.getKeyValue("USER", "user");
                MySplashForm._password = ini.getKeyValue("USER", "password");
                int autoLoginValue = ini.getKeyIntValue("STATION", "AutoLogin");
                MySplashForm.autoLoginFlag = autoLoginValue == 1 ? true : false;
            }
            else
            {
                MessageBox.Show("File:{0} not exist!", cfgFile);
            }

            MySplashForm.sendMsgToTT += new MsgFromWelcomPageEvent(msgFromWelcomePage);
            Application.Run(MySplashForm);
        }
        private static void msgFromWelcomePage(string msg)
        {
            if(msg == "login")
            {
                loginIsOk = true;
            }
            else if(msg == "close")
            {
                clickClose = true;
            }
        }
        static public void Show()
        {
            if (MySplashThread != null)
                return;

            MySplashThread = new Thread(new ThreadStart(Splash.ShowThread));
            MySplashThread.IsBackground = true;
            MySplashThread.SetApartmentState(ApartmentState.STA);
            MySplashThread.Start();
        }

        static public void Close()
        {
            if (MySplashThread == null) return;
            if (MySplashForm == null) return;

            try
            {
                MySplashForm.Invoke(new MethodInvoker(MySplashForm.Close));
            }
            catch (Exception)
            {
            }
            MySplashThread = null;
            MySplashForm = null;
        }

        static public string Status
        {
            set
            {
                if (MySplashForm == null)
                {
                    return;
                }

                MySplashForm.StatusInfo = value;
            }
            get
            {
                if (MySplashForm == null)
                {
                    throw new InvalidOperationException("Splash Form not on screen");
                }
                return MySplashForm.StatusInfo;
            }
        }
        static public int ProgressValue
        {
            set
            {
                if (MySplashForm == null)
                {
                    return;
                }

                MySplashForm.ProgressValue = value;
            }
            get
            {
                if (MySplashForm == null)
                {
                    throw new InvalidOperationException("Splash Form not on screen");
                }
                return MySplashForm.ProgressValue;
            }
        }
    }
}
