using System;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace PaiPai
{
    public partial class SetPosition : Form
    {
        public PositionParam MPositionParam;
        public SetPosition()
        {
            InitializeComponent();
            MPositionParam = new PositionParam();

//             TxtAdd300.Text = "678,440";
//             TxtAdd200.Text = "753,440";
//             TxtAdd100.Text = "830,440";
//             TxtLess300.Text = "678,404";
//             TxtLess200.Text = "753,404";
//             TxtLess100.Text = "830,404";
//             TxtBid.Text = "830,475";
//             TxtAddOrder.Text = "830,365";
//             TxtPrice.Text = "300";
//             BidTime.Format = DateTimePickerFormat.Time;
//             AddPriceTime.Format = DateTimePickerFormat.Time;
//             BidTime.Value = Convert.ToDateTime("11:29:50");
//             AddPriceTime.Value = Convert.ToDateTime("11:29:46");
//             TxtUrl.Text = "http://moni.51hupai.org/";

            GetLanguage();
        }

        public void SetParam()
        {
            try
            {
                TxtAdd300.Text = string.Format("{0},{1}", MPositionParam.Add300.X, MPositionParam.Add300.Y);
                TxtAdd200.Text = string.Format("{0},{1}", MPositionParam.Add200.X, MPositionParam.Add200.Y);
                TxtAdd100.Text = string.Format("{0},{1}", MPositionParam.Add100.X, MPositionParam.Add100.Y);

                TxtLess300.Text = string.Format("{0},{1}", MPositionParam.Less300.X, MPositionParam.Less300.Y);
                TxtLess200.Text = string.Format("{0},{1}", MPositionParam.Less200.X, MPositionParam.Less200.Y);
                TxtLess100.Text = string.Format("{0},{1}", MPositionParam.Less100.X, MPositionParam.Less100.Y);
                TxtBid.Text = string.Format("{0},{1}", MPositionParam.Bid.X, MPositionParam.Bid.Y);
                TxtRight.Text = string.Format("{0},{1}", MPositionParam.BidRight.X, MPositionParam.BidRight.Y);
                
                TxtAddOrder.Text = string.Format("{0},{1}", MPositionParam.AddOrder.X, MPositionParam.AddOrder.Y);
                TxtPrice.Text = MPositionParam.Price.ToString();
                BidTime.Format = DateTimePickerFormat.Time;
                AddPriceTime.Format = DateTimePickerFormat.Time;
                RightTime.Format = DateTimePickerFormat.Time;

                BidTime.Value = Convert.ToDateTime(MPositionParam.BidTime);
                AddPriceTime.Value = Convert.ToDateTime(MPositionParam.AddPriceTime);
                RightTime.Value = Convert.ToDateTime(MPositionParam.RighTime);
                TxtUrl.Text = MPositionParam.Url;
            }
            catch
            {
                
            }
            
        }

        private void GetLanguage()
        {
            ButPrice100.Text = "+100";
            ButPrice200.Text = "+200";
            ButPrice300.Text = "+300";
            ButPriceOrder.Text = "自定义价格";

            ButLess100.Text = "-100";
            ButLess200.Text = "-200";
            ButLess300.Text = "-300";
            ButBid.Text = "出价";

            label1.Text = "坐标";
            label2.Text = "坐标";
            label3.Text = "坐标";
            label4.Text = "坐标";
            label5.Text = "坐标";
            label6.Text = "坐标";
            label7.Text = "坐标";
            label8.Text = "自定义加价金额：";
            label9.Text = "坐标";

            label10.Text = "最终出价时间：";
            label11.Text = "最终加价时间：";

            GpSetParam.Text = "设置竞拍参数";

            GbSetProsition.Text = "设置坐标";

            groupBox1.Text = "网页设置";
            label12.Text = "添加网址：";

            ButYes.Text = "确定";
            ButNo.Text = "取消";
        }

        private void ButAddPrice_Click(object sender, EventArgs e)
        {

        }

        private void ButYes_Click(object sender, EventArgs e)
        {
            string[] szTemp = TxtAdd100.Text.Split(',');
            if (!string.IsNullOrEmpty(szTemp[0]))
            {
                MPositionParam.Add100.X = Convert.ToInt32(szTemp[0]);
                if (!string.IsNullOrEmpty(szTemp[1]))
                    MPositionParam.Add100.Y = Convert.ToInt32(szTemp[1]);
            }

            szTemp = TxtAdd200.Text.Split(',');
            if (!string.IsNullOrEmpty(szTemp[0]))
            {
                MPositionParam.Add200.X = Convert.ToInt32(szTemp[0]);
                if (!string.IsNullOrEmpty(szTemp[1]))
                    MPositionParam.Add200.Y = Convert.ToInt32(szTemp[1]);
            }

            szTemp = TxtAdd300.Text.Split(',');
            if (!string.IsNullOrEmpty(szTemp[0]))
            {
                MPositionParam.Add300.X = Convert.ToInt32(szTemp[0]);
                if (!string.IsNullOrEmpty(szTemp[1]))
                    MPositionParam.Add300.Y = Convert.ToInt32(szTemp[1]);
            }

            szTemp = TxtLess100.Text.Split(',');
            if (!string.IsNullOrEmpty(szTemp[0]))
            {
                MPositionParam.Less100.X = Convert.ToInt32(szTemp[0]);
                if (!string.IsNullOrEmpty(szTemp[1]))
                    MPositionParam.Less100.Y = Convert.ToInt32(szTemp[1]);
            }

            szTemp = TxtLess200.Text.Split(',');
            if (!string.IsNullOrEmpty(szTemp[0]))
            {
                MPositionParam.Less200.X = Convert.ToInt32(szTemp[0]);
                if (!string.IsNullOrEmpty(szTemp[1]))
                    MPositionParam.Less200.Y = Convert.ToInt32(szTemp[1]);
            }

            szTemp = TxtLess300.Text.Split(',');
            if (!string.IsNullOrEmpty(szTemp[0]))
            {
                MPositionParam.Less300.X = Convert.ToInt32(szTemp[0]);
                if (!string.IsNullOrEmpty(szTemp[1]))
                    MPositionParam.Less300.Y = Convert.ToInt32(szTemp[1]);
            }

            szTemp = TxtRight.Text.Split(',');
            if (!string.IsNullOrEmpty(szTemp[0]))
            {
                MPositionParam.BidRight.X = Convert.ToInt32(szTemp[0]);
                if (!string.IsNullOrEmpty(szTemp[1]))
                    MPositionParam.BidRight.Y = Convert.ToInt32(szTemp[1]);
            }
            
            szTemp = TxtBid.Text.Split(',');
            if (!string.IsNullOrEmpty(szTemp[0]))
            {
                MPositionParam.Bid.X = Convert.ToInt32(szTemp[0]);
                if (!string.IsNullOrEmpty(szTemp[1]))
                    MPositionParam.Bid.Y = Convert.ToInt32(szTemp[1]);
            }

            szTemp = TxtAddOrder.Text.Split(',');
            if (!string.IsNullOrEmpty(szTemp[0]))
            {
                MPositionParam.AddOrder.X = Convert.ToInt32(szTemp[0]);
                if (!string.IsNullOrEmpty(szTemp[1]))
                    MPositionParam.AddOrder.Y = Convert.ToInt32(szTemp[1]);
            }

            if (!string.IsNullOrEmpty(TxtPrice.Text))
            {
                MPositionParam.Price = Convert.ToInt32(TxtPrice.Text);
            }

            if (!string.IsNullOrEmpty(TxtUrl.Text))
            {
                MPositionParam.Url = TxtUrl.Text;
            }

            if (!string.IsNullOrEmpty(TxtPassword.Text))
            {
                MPositionParam.Password = Encryption(TxtPassword.Text);
            }

            MPositionParam.BidTime = Convert.ToDateTime(BidTime.Value.ToString("HH:mm:ss")).ToString();
            MPositionParam.AddPriceTime = Convert.ToDateTime(AddPriceTime.Value.ToString("HH:mm:ss")).ToString();
            MPositionParam.RighTime = Convert.ToDateTime(RightTime.Value.ToString("HH:mm:ss")).ToString();
            WriteParam();
            this.Close();
        }

        private void ButNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string Encryption(string str)
        {
            byte[] data = Encoding.Unicode.GetBytes(str+"HelloWord");
            int i = 0;
            string strTemp = "";
            foreach (int b in data)
            {
                int k = Convert.ToInt32(b);
                int t = k - 1;
                if (t<=0)
                {
                    t = 127 + t;
                }
                strTemp += Chr(t);
            }
            
            return strTemp;
        }

        public string Chr(int asciiCode)
        {
            return string.Format("{0:X}", asciiCode);
        }

        private void WriteParam()
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "config.js";
            using (FileStream fs = new FileStream(strPath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string json = js.Serialize(MPositionParam);
                    sw.Write(json);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
            }
        }
    }
}
