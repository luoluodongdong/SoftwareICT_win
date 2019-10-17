using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Software_ICT
{
    class XmlOperat
    {
        bool debug = true;

        //第二个节点属性 testItemsList
        public List<string> itemNameList { get; set; }
        public List<string> descriptionsList { get; set; }
        public List<string> testTagList { get; set; }
        public List<string> commandList { get; set; }
        public List<string> responseList { get; set; }
        public List<string> judgeStyleList { get; set; }
        public List<string> upperLimitList { get; set; }
        public List<string> lowerLimitList { get; set; }
        public List<string> measurementUnitList { get; set; }
        public List<string> waitTimeList { get; set; }
        public List<string> errorCodeList { get; set; }

        public string xmlfile = @"";
        //载入xml方法
        private XmlNode xn;
        private XmlNodeList xncfgInfo;
        private XmlNodeList xntestItems;
        public bool loadXmlFile()
        {
            if (xmlfile == @"") return false;
            //string xmlfile = @"D:\WeidongCao_work\CsharpCode\WinTestToolST\WindowsFormsApplication1\autoTestList.xml";
            XmlDocument xmlDoc = new XmlDocument();

            //忽略文档里面的注释
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            //读取xml文件
            XmlReader reader = XmlReader.Create(xmlfile, settings);
            xmlDoc.Load(reader);
            reader.Close();
            //确定根节点
            xn = xmlDoc.SelectSingleNode("testXmlList");
            //xncfgInfo = xn.SelectNodes("cfgInfo");
            xntestItems = xn.SelectNodes("testItems");
            if (debug) System.Diagnostics.Debug.WriteLine("Info:xmlOP->xmItemCount: " + xntestItems.Count.ToString());
           // readcfgInfo();
           // if (debug) System.Diagnostics.Debug.WriteLine("Info:xmlOP->readcfgInfo: DONE");
            readtestItems();
            if (xn != null) return true;
            else return false;
        }
       
        //读取testItemsList
        private void readtestItems()
        {
            itemNameList = new List<string>();
            descriptionsList = new List<string>();
            testTagList = new List<string>();
            commandList = new List<string>();
            responseList = new List<string>();
            judgeStyleList = new List<string>();
            upperLimitList = new List<string>();
            lowerLimitList = new List<string>();
            measurementUnitList = new List<string>();
            waitTimeList = new List<string>();
            errorCodeList = new List<string>();
            //遍历testItems
            foreach (XmlNode xnl in xntestItems)
            {
                XmlElement xe = (XmlElement)xnl;
                itemNameList.Add(xe.GetAttribute("itemName").ToString());
                descriptionsList.Add(xe.GetAttribute("descriptions").ToString());
                testTagList.Add(xe.GetAttribute("testTag").ToString());
                commandList.Add(xe.GetAttribute("command").ToString());
                responseList.Add(xe.GetAttribute("response").ToString());
                judgeStyleList.Add(xe.GetAttribute("judgeStyle").ToString());
                upperLimitList.Add(xe.GetAttribute("upperLimit").ToString());
                lowerLimitList.Add(xe.GetAttribute("lowerLimit").ToString());
                measurementUnitList.Add(xe.GetAttribute("measurementUnit").ToString());
                waitTimeList.Add(xe.GetAttribute("waitTime").ToString());
                errorCodeList.Add(xe.GetAttribute("errorCode").ToString());
            }
      
        }

    }
}
