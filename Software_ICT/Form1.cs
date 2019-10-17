using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Net;
using NONATEST;
using MT_ICT;
using System.Text.RegularExpressions;

namespace Software_ICT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public struct CFG_info
        {
            public string swName;
            public string swVersion;
            public string mode;
            public string password;
            public long inputCount;
            public long passCount;
            public string logPath;
            public string stationID;
            public string mesPATH;
            public int snLength;
        }
        public struct TEST_info
        {
            public DateTime _startT;
            public string snString;
            public bool result;
            public string startTime;
            public string endTime;
            public string errorCode;
            public string testerID;
            public string resultStr;
            public string csvData;
        }

        private CFG_info cfg_info;
        private TEST_info test_info;
        LoadTestPlan ltp = new LoadTestPlan();
        
        private CIni ini;
        string cfg_file = Application.StartupPath + @"\cfg.CFG";
        BackgroundWorker backgroundWorker1;
        private BindingList<string> listData = new BindingList<string>();
        private bool _update_logView = false;
        private object objLock = new object();

        //Dut serial
        myDeviceUC myDUT = new myDeviceUC();
        //Control board serial
        myDeviceUC myControlBoard = new myDeviceUC();
        //DMM instrument
        myInstrumentUC myDMM = new myInstrumentUC();

        //password view
        Password pw_form;

        List<bool> deviceStatusList = new List<bool>();
        bool all_devices_is_ready = false;

        #region Form Event
        private void Form1_Load(object sender, EventArgs e)
        {
            listData = new BindingList<string>();
            listBox1.DataSource = listData;
             
            Thread.Sleep(500);
            Splash.Status = "状态:初始化UI...";
            Splash.ProgressValue = 10;
            swLabel.BackColor = Color.Transparent;
            verLabel.BackColor = Color.Transparent;
            swPB.Controls.Add(swLabel);
            swPB.Controls.Add(verLabel);
            pw_form = new Password();
            pw_form.sendResult2MT += new SendPassWord2MTEventHandler(pw_sendResult2MT);

            Thread.Sleep(500);
            Splash.Status = "状态:载入Testplan...";
            Splash.ProgressValue = 20;
            #region load testplan CSV/CFG

            ltp.csvFile = Application.StartupPath + @"\\TestPlan.csv";
            if (!ltp.LoadCSVTestPlan())
            {
                MessageBox.Show("CSV testplan load fial!");
                Application.Exit();
                return;
            }
            loadTestItems();
            if (!readCFGInfo())
            {
                MessageBox.Show("cfg.CFG file load fail!");
                Application.Exit();
            }
            #endregion
            Thread.Sleep(500);
            Splash.Status = "状态:载入后台测试线程...";
            Splash.ProgressValue = 30;
            backgroundWorker1 = new BackgroundWorker();                      //新建BackgroundWorker
            backgroundWorker1.WorkerReportsProgress = true;                  //允许报告进度
            backgroundWorker1.WorkerSupportsCancellation = false;             //允许取消线程
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;                       //设置主要工作逻辑
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;     //进度变化的相关处理
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;  //线程完成时的处理
            Thread.Sleep(500);
            Splash.Status = "状态:刷新测试panel...";
            Splash.ProgressValue = 50;
            updatePannel();
            Splash.Status = "状态:注册触发事件...";
            Splash.ProgressValue = 70;
            //绑定输入SN后的回车事件
            inputSN_TB.KeyUp += new KeyEventHandler(tb_scanSN_KeyUp);

            Splash.Status = "状态:载入设备...";
            Splash.ProgressValue = 80;
            Activate();

            //AUTO OPEN DEVICES
            //DUT
            myDUT.saveConfigEvent += new myDeviceUC.SaveConfigEventHandler(devUC_saveCfgEvent);
            myDUT.Location = new Point(15, 150);
            tabControl1.TabPages[2].Controls.Add(myDUT);
            //Controls.Add(instrumentDev);
            myDUT.initDeviceUC();
            if (myDUT.AutoOpenSerialPort())
            {
                printLog("serial port:" + myDUT.Name + " Opened OK");
                deviceStatusList.Add(true);
            }
            else
            {
                printLog("serial port:" + myDUT.Name + " Opened NG");
                deviceStatusList.Add(false);
            }
            //CONTROLBOARD
            myControlBoard.saveConfigEvent += new myDeviceUC.SaveConfigEventHandler(devUC_saveCfgEvent);
            myControlBoard.Location = new Point(15, 150+80);
            tabControl1.TabPages[2].Controls.Add(myControlBoard);
            //Controls.Add(instrumentDev);
            myControlBoard.initDeviceUC();
            if (myControlBoard.AutoOpenSerialPort())
            {
                printLog("serial port:" + myControlBoard.Name + " Opened OK");
                deviceStatusList.Add(true);
            }
            else
            {
                printLog("serial port:" + myControlBoard.Name + " Opened NG");
                deviceStatusList.Add(false);
            }
            //DMM
            myDMM.saveConfigEvent += new myInstrumentUC.SaveConfigEventHandler(devUC_saveCfgEvent);
            myDMM.Location = new Point(15, 150 + 80 + 80);
            tabControl1.TabPages[2].Controls.Add(myDMM);
            //Controls.Add(instrumentDev);
            myDMM.initInstrumentUC();
            if (myDMM.AutoOpenInstrument())
            {
                printLog("serial port:" + myDMM.Name + " Opened OK");
                deviceStatusList.Add(true);
            }
            else
            {
                printLog("serial port:" + myDMM.Name + " Opened NG");
                deviceStatusList.Add(false);
            }

            Splash.Status = "状态:载入窗体...";
            Splash.ProgressValue = 100;
            Thread.Sleep(1000);
            Splash.Close();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            Console.WriteLine("From1 shown...");
            Activate();
            inputSN_TB.Focus();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("From1 closing...");
            closeAllDev();
            printLog("close all devices OK");

        }
        private bool readCFGInfo()
        {
            if (!File.Exists(cfg_file))
                return false;

            ini = new CIni(cfg_file);
            cfg_info.swName = ini.getKeyValue("STATION", "SWName");
            cfg_info.swVersion = ini.getKeyValue("STATION", "SWVersion");
            cfg_info.password = ini.getKeyValue("STATION", "PassWord");
            cfg_info.inputCount = long.Parse(ini.getKeyValue("STATION", "Input"));
            cfg_info.passCount = long.Parse(ini.getKeyValue("STATION", "Pass"));
            cfg_info.logPath = ini.getKeyValue("STATION", "LOGPath");
            
            cfg_info.mesPATH = ini.getKeyValue("STATION", "MESPath");

            swLabel.Text = cfg_info.swName;
            verLabel.Text = cfg_info.swVersion;
            cfg_info.snLength = ini.getKeyIntValue("STATION", "SNLength");

            //DUT serial port
            myDUT.dev_id = ini.getKeyIntValue("DUT", "ID");
            myDUT.slot_name = ini.getKeyValue("DUT", "NAME");
            myDUT.groupName = ini.getKeyValue("DUT", "DESCRIPTION");
            myDUT.serialPortName = ini.getKeyValue("DUT", "COM");
            myDUT.serialPortBaud= ini.getKeyIntValue("DUT", "BAUD");
            //control board port
            myControlBoard.dev_id = ini.getKeyIntValue("CONTROLBOARD", "ID");
            myControlBoard.slot_name = ini.getKeyValue("CONTROLBOARD", "NAME");
            myControlBoard.groupName = ini.getKeyValue("CONTROLBOARD", "DESCRIPTION");
            myControlBoard.serialPortName = ini.getKeyValue("CONTROLBOARD", "COM");
            myControlBoard.serialPortBaud = ini.getKeyIntValue("CONTROLBOARD", "BAUD");
            //instrument
            myDMM.instr_id = ini.getKeyIntValue("DMM", "ID");
            myDMM.slot_name = ini.getKeyValue("DMM", "NAME");
            myDMM.groupName = ini.getKeyValue("DMM", "DESCRIPTION");
            myDMM.instrAddr = ini.getKeyValue("DMM", "COM");
            myDMM.instrBaudRate = ini.getKeyIntValue("DMM", "BAUD");

            return true;
        }
        #endregion

        #region ListView
        //public void printMsg2LV(int)
        private void loadTestItems()
        {
            this.listView1.Items.Clear();
            Console.WriteLine("Info:begin load items...");
            this.listView1.BeginUpdate();
            for (int i = 0; i < ltp.itemNameList.Count; i++)
            {
                //column #1: number
                ListViewItem i_item = listView1.Items.Add((listView1.Items.Count + 1) + "");
                //column #2:items
                i_item.SubItems.Add(ltp.itemNameList[i]);
                //column #3:status
                i_item.SubItems.Add("");
                //column #4:value
                i_item.SubItems.Add("");
                //column #5:low
                i_item.SubItems.Add(ltp.lowerLimitList[i]);
                //column #6:typ value
                i_item.SubItems.Add(ltp.typValueList[i]);
                //column #7:up
                i_item.SubItems.Add(ltp.upperLimitList[i]);
                //column #8:unit
                i_item.SubItems.Add(ltp.measUnitList[i]);
                //column #9:duration
                i_item.SubItems.Add("");
            }

            //设置行背景颜色
            //  listView1.Items[2].BackColor = Color.LightGreen;

            this.listView1.EndUpdate();
            //滚动至第一行
            this.listView1.EnsureVisible(0);
            Console.WriteLine("Info:load successful");
        }
        //listView 第row行 第columns列 显示字符串str
        private void printStr2ListView(int row, int columns, string str)
        {
            //row = row - 1;
            //columns = columns - 1;
            Invoke((EventHandler)(delegate {
            listView1.Items[row].SubItems[columns].Text = str;
            //this.listView1.Items[row].UseItemStyleForSubItems = false;
            if (listView1.Items[row].SubItems[2].Text == "PASS")
            {
                listView1.Items[row].BackColor = Color.FromArgb(0, 210, 0);
            }
            else if (listView1.Items[row].SubItems[2].Text == "FAIL")
            {
                listView1.Items[row].BackColor = Color.Red;
            }
            else if (listView1.Items[row].SubItems[2].Text == "TESTING")
            {
                listView1.Items[row].BackColor = Color.Yellow;
            }
            else if (listView1.Items[row].SubItems[2].Text == "SKIPED")
            {
                listView1.Items[row].BackColor = Color.Gray;
            }
            else
            {
                listView1.Items[row].BackColor = Color.LightGray;
            }
            //滚动至row行
            this.listView1.EnsureVisible(row);
            //  Application.DoEvents();
            }));

        }
        //listView 第row行 显示字符串2-status 3-value 7-duration
        public void printMsg2ListView(int row, string status, string value, string duration)
        {
            //row = row - 1;
            //columns = columns - 1;
            Invoke((EventHandler)(delegate {
            listView1.Items[row].SubItems[2].Text = status;
            listView1.Items[row].SubItems[3].Text = value;
            listView1.Items[row].SubItems[8].Text = duration;
            //this.listView1.Items[row].UseItemStyleForSubItems = false;
            if (listView1.Items[row].SubItems[2].Text == "PASS")
            {
                listView1.Items[row].BackColor = Color.FromArgb(0, 210, 0);
            }
            else if (listView1.Items[row].SubItems[2].Text == "FAIL")
            {
                listView1.Items[row].BackColor = Color.Red;
            }
            else if (listView1.Items[row].SubItems[2].Text == "TESTING")
            {
                listView1.Items[row].BackColor = Color.Yellow;
            }
            else if (listView1.Items[row].SubItems[2].Text == "SKIPED")
            {
                listView1.Items[row].BackColor = Color.Gray;
            }
            else
            {
                listView1.Items[row].BackColor = Color.LightGray;
            }
            //滚动至row行
            this.listView1.EnsureVisible(row);
            // Application.DoEvents();
            }));

        }
        #endregion

        #region Background Worker1
        //Main function for testing
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            test_info._startT = DateTime.Now;
            DoOnUIThread(delegate () {
                testTimer.Enabled = true;
                StatusLB.Text = "TEST...";
                StatusLB.BackColor = Color.Yellow;
                test_info.result = true;
                //disable control
                StartBtn.Enabled = false;
                inputSN_TB.Enabled = false;
                CleanBtn.Enabled = false;
                testModeBtn.Enabled = false;
                debugBtn.Enabled = false;
            });
            //******Just for debug****//
            //testDetialView();
            //***********************//
            test_info.startTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
            test_info.errorCode = "";
            test_info.csvData = "";

            #region Main function
            //************Main Function*************//
            //testDetialView();
            string FeedBackStr = "";
            for (int i = 0; i < ltp.itemNameList.Count; i++)
            {
                DateTime startT = DateTime.Now;
                string thisDescription = ltp.itemNameList[i];
                string thisGroup = ltp.groupList[i];
                string thisFunc = ltp.functionList[i];
                string thisSkipTag = ltp.skipList[i];
                string thisCommand = ltp.commandList[i];
                double thisTimeOut = double.Parse(ltp.timeOutList[i]);
                string thisJudgeStyle = ltp.valueStyleList[i];
                string thisTypValue = ltp.typValueList[i];
                string thisValueSave = ltp.valueSaveList[i];
                string thisLow = ltp.lowerLimitList[i];
                string thisUp = ltp.upperLimitList[i];
                int thisDelay = (int)(1000 * double.Parse(ltp.delayList[i]));
                string thisExitEnable = ltp.exitEnableList[i];

                //receive data time out ?
                bool recTimeOut = false;
                string thisValue = "";
                string thisStatus = "PASS";
                string thisDuration = "";

                printLog("==========================================");
                printLog(string.Format("item:{0}", thisDescription));
                //refresh detial view =>"TESTING"
                printStr2ListView(i, 2, "TESTING");

                //check skip item
                if (thisSkipTag.Equals("1"))
                {
                    thisValue = "skip";
                    thisStatus = "SKIPED";

                }

                else if (thisFunc.Equals("Serial"))
                {
                    if (!myDUT.SendCmdWithTimeOut(thisCommand, thisTimeOut, out thisValue, out recTimeOut))
                    {
                        thisValue = "ERROR";
                        thisStatus = "FAIL";
                        break;
                    }
                    if (recTimeOut)
                    {
                        thisValue = "TimeOut";
                        thisStatus = "FAIL";
                    }
                    else
                    {
                        if (thisJudgeStyle == "value") thisValue = RegexValue(thisValue);
                        if (!JudgeValueWithLimit(thisJudgeStyle, thisValue, thisTypValue, thisLow, thisUp))
                        {
                            thisStatus = "FAIL";
                        }
                    }
                    if (thisValueSave == "1") FeedBackStr = thisValue;
                }
                else if (thisFunc.Equals("OpenShort"))
                {
                    if (!myDUT.SendCmdWithTimeOut(thisCommand, thisTimeOut, out thisValue, out recTimeOut))
                    {
                        thisValue = "ERROR";
                        thisStatus = "FAIL";
                        break;
                    }
                    if (recTimeOut)
                    {
                        thisValue = "TimeOut";
                        thisStatus = "FAIL";
                    }
                    else
                    {
                        thisStatus = "PASS";
                    }
                    if (thisValueSave == "1") FeedBackStr = thisValue;
                }
                else if (thisFunc.Equals("CheckFB"))
                {
                    string[] tempArr = FeedBackStr.Split(',');
                    int index = int.Parse(thisCommand);
                    if (index < tempArr.Count())
                    {
                        thisValue = tempArr[index];
                        if (!JudgeValueWithLimit(thisJudgeStyle, thisValue, thisTypValue, thisLow, thisUp))
                        {
                            thisStatus = "FAIL";
                        }
                    }
                    else
                    {
                        thisValue = "data err";
                        thisStatus = "FAIL";
                    }
                }
                else if (thisFunc.Equals("Delay"))
                {
                    int delayT = int.Parse(thisCommand);
                    Thread.Sleep(delayT);
                    thisValue = "OK";
                    thisStatus = "PASS";
                }
                else if (thisFunc.Equals("Dialog"))
                {
                    MessageBox.Show(thisCommand);
                    thisValue = "OK";
                    thisStatus = "PASS";
                }
                else if (thisFunc.Equals("Async"))
                {
                    thisValue = "OK";
                    thisStatus = "PASS";

                }
                //execute sync task
                else if (thisFunc.Equals("Sync"))
                {
                    thisValue = "OK";
                    thisStatus = "PASS";
                }
                //other invalid item/testtag
                else
                {
                    thisValue = "TestTag error";
                    thisStatus = "FAIL";
                }

                thisValue = thisValue.Replace("\r", "");
                thisValue = thisValue.Replace("\n", "");
                thisValue = thisValue.Replace(",", "@");
                thisValue = thisValue.Replace("\r\n", "@");
                thisValue = thisValue.Replace("#", "@");
                thisValue = thisValue.Trim();

                printLog(string.Format("rec:{0}", thisValue));

                //test item duration time
                TimeSpan span = DateTime.Now - startT;
                thisDuration = string.Format("{0}s", span.TotalSeconds.ToString());
                printLog(string.Format("duration:{0}", thisDuration));
                printLog(string.Format("status:{0}", thisStatus));

                //refresh detial view =>status value duration
                printMsg2ListView(i, thisStatus, thisValue, thisDuration);
                
                if (thisStatus == "FAIL" && test_info.errorCode == "")
                {
                    test_info.result = false;
                    test_info.errorCode = string.Format("EC_{0}", (i + 1).ToString());
                }

                test_info.csvData += thisValue + ",";
                if(thisStatus == "FAIL" && thisExitEnable == "1")
                {
                    break;
                }
                Thread.Sleep(thisDelay);
            }
            #endregion

        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;

        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            test_info.endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
            cfg_info.inputCount += 1;
            string str_result = "FAIL";
            if (test_info.result)
            {
                cfg_info.passCount += 1;
                str_result = "PASS";
                StatusLB.Text = "PASS";
                StatusLB.BackColor = Color.FromArgb(124, 252, 0);
            }
            else
            {
                StatusLB.Text = "FAIL";
                StatusLB.BackColor = Color.FromArgb(255, 20, 0);
            }
            //update Pannel
            updatePannel();

            //save csv data
            test_info.resultStr = str_result;
            test_info.csvData = test_info.snString + "," +
                                test_info.resultStr + "," +
                                test_info.errorCode + "," +
                                test_info.testerID + "," +
                                test_info.startTime + "," +
                                test_info.endTime + "," +
                                test_info.csvData + "\r\n";

            printLog(test_info.csvData);
            //TEST MODE=>save csv for MES Client
            if (testModeBtn.Checked)
            {
                //
                Save2MESCSV();
            }
            SaveAllOneCSV(cfg_info.logPath, cfg_info.swName, test_info.csvData);
            printLog("Finish work...");

            //save local log
            SavePrintLog(cfg_info.logPath, cfg_info.swName, listData);
            //stop timer
            testTimer.Enabled = false;
            //enable control
            StartBtn.Enabled = true;
            inputSN_TB.Enabled = true;
            CleanBtn.Enabled = true;
            testModeBtn.Enabled = true;
            debugBtn.Enabled = true;
            inputSN_TB.Text = "";
            inputSN_TB.Focus();
        }
        #endregion

        #region DevicesUC delegate event
        private void devUC_saveCfgEvent(int id, Dictionary<string, string> cfg)
        {
            string addr = cfg["Port"];
            printLog("devUC id:" + id.ToString());
            //DUT
            if (id == 1000)
            {
                ini.setKeyValue("DUT", "COM", addr);
                deviceStatusList[0] = true;
            }
            //CONTROLBOARD
            else if (id == 2000)
            {
                ini.setKeyValue("CONTROLBOARD", "COM", addr);
                deviceStatusList[1] = true;
            }
            //DMM
            else if (id == 3000)
            {
                ini.setKeyValue("DMM", "COM", addr);
                deviceStatusList[2] = true;
            }
            MessageBox.Show(string.Format("Save id:{0} port:{1} OK!",id.ToString(),addr));
        }
        private bool updateDevStatus()
        {
            all_devices_is_ready = true;
            foreach (bool dev_status in deviceStatusList)
            {
                if (!dev_status)
                {
                    all_devices_is_ready = false;
                    break;
                }
            }
            return all_devices_is_ready;
        }
        private void closeAllDev()
        {
            myDUT.serialPort1.Close();
            myControlBoard.serialPort1.Close();
            myDMM.closeInstr();
        }
        #endregion

        #region Judge Response
        private string RegexValue(string value)
        {
            //string str = "优惠6.0万"; 
            /**  \\d+\\.?\\d*
            * \d 表示数字
            * + 表示前面的数字有一个或多个（至少出现一次）
            * \. 此处需要注意，. 表示任何原子，此处进行转义，表示单纯的 小数点
            * ? 表示0个或1个
            * * 表示0次或者多次
            */
            Regex r = new Regex("\\d+\\.?\\d*");
            bool ismatch = r.IsMatch(value);
            if (!ismatch) return "NA";
            MatchCollection mc = r.Matches(value);
            string result = string.Empty;
            for (int i = 0; i < mc.Count; i++)
            {
                result += mc[i];
                //匹配结果是完整的数字，此处可以不做拼接的 
            }
            printLog("regex result:" + result);
            return result;
        }
        private bool JudgeValueWithLimit(string judgeStyle, string value, string typValue,string low, string up)
        {
            //strCheck:OK
            if (judgeStyle.Equals("string"))
            {
                //string matchStr = judgeStyle.Split(':')[1];
                return value.Contains(typValue);
            }
            //judgeStyle:"valueCheck:C" value:34.67C
            else if (judgeStyle.Equals("value"))
            {
                return judgeValue(value, up, low);
            }
            //judgeStyle:"rangeValue"
            else if (judgeStyle.StartsWith("rangeValue"))
            {
                return judgeValue(value, up, low);
            }
            else
            {
                MessageBox.Show("Error judgeStyle:" + judgeStyle);
                return false;
            }

        }
        //匹配数值范围
        private bool judgeValue(string strValue, string upperLimit, string lowerLimit)
        {
            printLog(string.Format("value:{0} low:{1} up:{2}", strValue, lowerLimit, upperLimit));

            if (strValue == "") return false;

            //double receiveValue = System.Convert.ToDouble(strValue);
            double receiveValue = 0.00;
            bool convert_flag = double.TryParse(strValue, out receiveValue);
            if (!convert_flag)
            {
                printLog(string.Format("{0} convert error!", strValue));
                return false;
            }

            if (upperLimit == "" && lowerLimit != "")
            {
                double lower = System.Convert.ToDouble(lowerLimit);

                if (receiveValue >= lower) return true;
                else return false;
            }
            else if (upperLimit != "" && lowerLimit == "")
            {
                double upper = System.Convert.ToDouble(upperLimit);
                if (receiveValue <= upper) return true;
                else return false;
            }
            else if (upperLimit != "" && lowerLimit != "")
            {
                double upper = System.Convert.ToDouble(upperLimit);
                double lower = System.Convert.ToDouble(lowerLimit);

                if (receiveValue >= lower && receiveValue <= upper) return true;
                else return false;
            }
            else
            {
                return true;
            }


        }
        #endregion

        #region Start Btn
        private void StartBtn_Click(object sender, EventArgs e)
        {
           
            loadTestItems();

            listData = new BindingList<string>();
            listBox1.DataSource = listData;
            
            test_info.snString = inputSN_TB.Text;
            if(test_info.snString.Length < 1)
            {
                test_info.snString = "NA";
            }
            if(testModeBtn.Checked)
            {
                if (!SNisOK(test_info.snString))
                {
                    MessageBox.Show("sn:" + test_info.snString + " is wrong!");
                    inputSN_TB.Text = "";
                    return;
                }
            }
            
            if (!updateDevStatus())
            {
                printLog("Devices not ready!");
                MessageBox.Show("Devices not ready!");
                inputSN_TB.Text = "";
                return;
            }

            backgroundWorker1.RunWorkerAsync();
        }
        #endregion

        #region Check SN is OK
        //响应输入SN后的回车事件
        private void tb_scanSN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (inputSN_TB.Text.Length == 0) return;
                StartBtn.PerformClick();
            }
        }
        private bool SNisOK(string sn)
        {
            if (sn.Length != cfg_info.snLength)
            {
                MessageBox.Show("SN length error!");
                return false;
            }
            return true;
        }

        //check SN form MES
        private bool checkRouterWithSn(string sn,string mes_ip,string station_id)
        {
            bool bResult = false;
            string URL = string.Format("http://{0}/bobcat/sfc_response.aspx", mes_ip);
            string Body = string.Format("c=QUERY_RECORD&sn={0}&tsid={1}&p=UNIT_PROCESS_CHECK", sn, station_id);
            string feedback = Post(URL, Body);

            string subStr = "unit_process_check=OK";
            if (feedback.Contains(subStr))
            {
                bResult = true;
            }
            else
            {
                MessageBox.Show(string.Format("MES error:\n{0}", feedback));
            }

            return bResult;
        }
        private string Post(string url, string content)
        {
            string result = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";

                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(content);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            //调试输出
            Console.WriteLine("Info:POST result:" + result);
            return result;
        }
        #endregion

        #region Test ListView(just for DEBUG)
        private void testDetialView()
        {

            // detialView.listData.Clear();
            // detialView.initDetialView();
            // Console.WriteLine("initDetialView...");
            for (int i = 0; i <ltp.itemNameList.Count; i++)
            {
                Thread.Sleep(50);
                printStr2ListView(i, 2, "TESTING");

                //
                Thread.Sleep(100);
                printMsg2ListView(i, "PASS", "123456", "0.123456s");
                
                //
                for (int k = 0; k < 5; k++)
                {
                    Thread.Sleep(10);
                    string log = "item" + i.ToString();
                    printLog(log);
                }

                //Console.WriteLine("testing...");
            }
        }
        #endregion

        #region Print Log
        private void DoOnUIThread(MethodInvoker d)
        {
            if (this.InvokeRequired) { this.Invoke(d); } else { d(); }
        }
        private void printLog(string str)
        {
            lock (objLock)
            {
                string time = DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss:fff]");
                string log = time + str;
                Console.WriteLine(log);

                DoOnUIThread(delegate () {
                    listData.Add(log);
                    if (_update_logView && listBox1.Items.Count > 20)
                    {
                        listBox1.TopIndex = listBox1.Items.Count - 1;
                    }
                });

            }
            
        }
        #endregion

        #region Test Timer
        private void testTimer_Tick(object sender, EventArgs e)
        {
            DateTime currentT = DateTime.Now;
            TimeSpan span = currentT - test_info._startT;
            int timerMi = span.Minutes;
            int timerS = span.Seconds;
            double timerM = span.Milliseconds; // / 1000.0;
            string timerM_str = timerM.ToString();
            int timerSec = timerMi * 60 + timerS;

            TimerLB.Text = timerSec.ToString() + "." + timerM_str[0] + " s";
        }
        #endregion

        #region Panel
        private void CleanBtn_Click(object sender, EventArgs e)
        {
            cfg_info.inputCount = 0;
            cfg_info.passCount = 0;
            updatePannel();
            MessageBox.Show("Clean panel OK!", "Information");
        }
        private void updatePannel()
        {
            ini.setKeyValue("STATION", "Input", cfg_info.inputCount.ToString());
            ini.setKeyValue("STATION", "Pass", cfg_info.passCount.ToString());
            long failL = cfg_info.inputCount - cfg_info.passCount;
            float fPassRate = 0;
            if (cfg_info.inputCount == 0)
            {
                fPassRate = 0;
            }
            else
            {
                fPassRate = (float)cfg_info.passCount / cfg_info.inputCount * 100f;
                printLog("fPassRate:" + fPassRate.ToString());
            }
            Invoke((EventHandler)(delegate
            {
                inputLB.Text = "Input:" + cfg_info.inputCount.ToString();
                passLB.Text = "Pass:" + cfg_info.passCount.ToString();
                failLB.Text = "Fail:" + failL.ToString();
                yieldLB.Text = "Yield:" + fPassRate.ToString("0.00") + "%";
            }));
        }
        #endregion

        #region CSV&Log
        private void Save2MESCSV()
        {
            //1.获取message
            string msgString = "";//getResultMsg();
            //2.创建临时文件夹
            string strPreDirectory = "D:\\csvTemp";
            if (Directory.Exists(strPreDirectory))
            {
                printLog(strPreDirectory + " 此文件夹已经存在，无需创建！");
            }
            else
            {
                Directory.CreateDirectory(strPreDirectory);
                printLog(strPreDirectory + " 创建成功!");
            }

            string targetPath = cfg_info.mesPATH;//"D:\\MESlog\\测试文件路径";//"D:\\UpdateMEScsv";

            //3.生成CSV文件
            string csvName = strPreDirectory + "\\" + test_info.snString + DateTime.Now.ToString("_yyyy-MM-dd_HH_mm_ss_fff") + ".csv";


            try
            {
                string stationID = cfg_info.stationID;
                using (StreamWriter sw = File.CreateText(csvName))
                {
                    //ITKS_A02-MICFPCTEST01 666666 PASS 00 NA
                    sw.WriteLine("StationID,sn,Result,ErrorCode,Message");
                    sw.WriteLine(stationID + "," +
                        test_info.snString + "," +
                        test_info.resultStr + "," +
                        test_info.errorCode + ","
                        + msgString);
                    sw.Close();
                }
                //4.移动CSV文件至MES LOG文件夹
                FileMove(csvName, targetPath);
            }
            catch (Exception ex)
            {
                printLog("Save MES CSV ERROR:" + ex.ToString());
                MessageBox.Show("Save MES CSV ERROR:" + ex.ToString());
            }
            //Delay(200);
        }
        private void FileMove(string sourceFile, string targetPath)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            FileInfo file = new FileInfo(sourceFile);
            file.MoveTo(targetPath + @"\" + file.Name);
        }
        private void SavePrintLog(string Path, string SWName,BindingList<string> listData)
        {
            //1.创建文件夹
            string dateTime = DateTime.Now.ToString("yyyy-MM");
            string dateTime2 = DateTime.Now.ToString("yyyy-MM-dd");
            Path = Path + "\\" + SWName + "\\" + dateTime +"\\" 
                +string.Format("{0}_log",dateTime2);
            if (Directory.Exists(Path))
            {
                printLog(Path + " 此文件夹已经存在，无需创建！");
            }
            else
            {
                Directory.CreateDirectory(Path);
                printLog(Path + " 创建成功!");
            }
            //2.创建log文件
            string modeStr = "TEST";
            if (debugBtn.Checked) modeStr = "DEBUG";
            dateTime = DateTime.Now.ToString("_HH_mm_ss_fff_");
            string fileName = Path + "\\" + test_info.snString+dateTime + modeStr + ".log";
            FileStream fsTemp = new FileStream(fileName, FileMode.Append);
            StreamWriter swTemp = new StreamWriter(fsTemp);
            try
            {
                foreach(string item in listData)
                {
                    swTemp.WriteLine(item);
                }
                
            }
            catch (Exception ex)
            {
                printLog("Save local log ERROR:" + ex.ToString());
                MessageBox.Show("Save local log ERROR:" + ex.ToString());
            }
            swTemp.Close();
            fsTemp.Close();

        }
        private void SaveAllOneCSV(string Path, string SWName, string data)
        {
            //1.创建文件夹
            string dateTime = DateTime.Now.ToString("yyyy-MM");
            Path = Path + "\\" + SWName + "\\" + dateTime;
            if (Directory.Exists(Path))
            {
                printLog(Path + " 此文件夹已经存在，无需创建！");
            }
            else
            {
                Directory.CreateDirectory(Path);
                printLog(Path + " 创建成功!");
            }
            //2.创建CSV文件
            string modeStr = "TEST";
            if (debugBtn.Checked) modeStr = "DEBUG";
            dateTime = DateTime.Now.ToString("yyyy-MM-dd");
            string fileName = Path + "\\" + dateTime + modeStr + ".csv";

            if (!File.Exists(fileName))
            {
                string firstLine = "SerialNumber,Result,ErrorCode,TesterID,Test Start Time,Test End Time,";
                string secondLine = "Upper Limits---->,,,,,,";
                string thirdLine = "Lower Limits---->,,,,,,";
                string fourthLine = "MeasurementUnit---->,,,,,,";
                for (int i = 0; i < ltp.itemNameList.Count; i++)
                {
                    firstLine += ltp.itemNameList[i] + ",";
                    secondLine += ltp.upperLimitList[i] + ",";
                    thirdLine += ltp.lowerLimitList[i] + ",";
                    fourthLine += ltp.measUnitList[i] + ",";
                }
                try
                {
                    FileStream fsTemp = new FileStream(fileName, FileMode.Append);
                    StreamWriter swTemp = new StreamWriter(fsTemp);
                    //swTemp.Write()
                    swTemp.WriteLine(firstLine);
                    swTemp.WriteLine(secondLine);
                    swTemp.WriteLine(thirdLine);
                    swTemp.WriteLine(fourthLine);
                    swTemp.Close();
                    fsTemp.Close();
                }
                catch (Exception ex)
                {
                    printLog("Save All CSV ERROR:" + ex.ToString());
                    MessageBox.Show("Save ALL CSV ERROR:" + ex.ToString());
                }
            }
            //3.写入测试数据           
            FileStream fs = new FileStream(fileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(data);
            sw.Close();
            fs.Close();
        }
        #endregion

        #region Message From Password
        //receive Message from password form
        private void pw_sendResult2MT(bool result)
        {
            if (result)
            {
                tabControl1.SelectedIndex = 2;
                myDUT.showDeviceUC();
                myControlBoard.showDeviceUC();
                myDMM.showInstrumentUC();
            }
            else
            {
                tabControl1.SelectedIndex = 0;
            }
        }
        #endregion

        #region Button Event
        private void TabControl1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("tabcontrol click...");
            if (tabControl1.SelectedIndex == 2)
            {
                tabControl1.SelectedIndex = 0;
                pw_form.str_password = cfg_info.password;
                pw_form.ShowDialog();
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                _update_logView = true;
                printLog("show log view...");
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                //tabControl1.SelectedIndex = 0;
                //pw_form.str_password = "luxshare";
                //pw_form.ShowDialog();
            }
            else
            {
                _update_logView = false;
            }
        }
        private void debugBtn_CheckedChanged(object sender, EventArgs e)
        {
            debugLB.Visible = true;
        }

        private void testModeBtn_CheckedChanged(object sender, EventArgs e)
        {
            debugLB.Visible = false;
        }
        #endregion

        
    }
}
