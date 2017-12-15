using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace PaiPai
{
    public partial class PaiPai : Form
    {
        #region MyRegion

        private PositionParam _mPositionParam;

        private bool _mbWebTimeClock;
        private readonly Thread _mWebClockThread;

        private bool _mbLocalTimeClock;
        private readonly Thread _mLocalClockThread;

        private bool _mbStartWork;
        private Thread _mStartWork;
        private DateTime _mNowTime;
        private readonly LogHelper _mLogHelper;

        public PaiPai()
        {
            _mPositionParam = new PositionParam();
            _mLogHelper = new LogHelper();
            ReadParam();
            SetLogInfor();

            Password newWin = new Password();
            newWin.StrPassword = _mPositionParam.Password;
            newWin.ShowDialog();
            if (!newWin.BEnable) return;

            InitializeComponent();

            TxtOptInfor.Text += "获取网络时间...\r\n请先设置参数\r\n";
            StartPosition = FormStartPosition.CenterScreen;
            GetLanguage();
            _mNowTime = new DateTime();

            ////启动计时器
            _mbWebTimeClock = true;
            _mWebClockThread = new Thread(WebTimeClock);
            _mWebClockThread.Start();

            _mbLocalTimeClock = true;
            _mLocalClockThread = new Thread(LocalTimeClock);
            _mLocalClockThread.Start();

            _mbStartWork = false;

            TxtWebTimeCal.Text = "0";
            TxtLocalTimeCal.Text = "0";

            RadButLocal.Checked = true;
        }

        #endregion


        private void GetLanguage()
        {
            RadButWeb.Text = "网络时间：";
            RadButLocal.Text = "本地时间：";
            ButSetPosition.Text = "配置参数";
            ButAddPrice.Text = "手动加价";
            ButStart.Text = "启动自动拍牌";
            ButStop.Text = "停止自动拍牌";
            groupBox1.Text = "消息框";
        }

        protected override void WndProc(ref Message msg)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            if (msg.Msg == WM_SYSCOMMAND && ((int)msg.WParam == SC_CLOSE))
            {
                Process[] allProcess = Process.GetProcesses();
                foreach (Process p in allProcess)
                {
                    if (p.ProcessName.ToLower() == "PaiPai".ToLower())
                    {
                        for (int i = 0; i < p.Threads.Count; i++)
                            p.Threads[i].Dispose();
                        p.Kill();
                        break;
                    }
                }
                return;
            }
            base.WndProc(ref msg);
        }

        private void SetLogInfor()
        {
            try
            {
                _mLogHelper.StrRootPath = AppDomain.CurrentDomain.BaseDirectory + "log";
                _mLogHelper.StrComponentsName = "PaiPai";
                _mLogHelper.OnStart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _mLogHelper.WriteLog(LogType.Exception, "SetLogInfor: " + ex.Message);
            }
        }

        #region 鼠标事件接口

        //结构体布局 本机位置
        [StructLayout(LayoutKind.Sequential)]
        struct NativeRect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        //将枚举作为位域处理
        [Flags]
        enum MouseEventFlag : uint //设置鼠标动作的键值
        {
            Move = 0x0001,               //发生移动
            LeftDown = 0x0002,           //鼠标按下左键
            LeftUp = 0x0004,             //鼠标松开左键
            RightDown = 0x0008,          //鼠标按下右键
            RightUp = 0x0010,            //鼠标松开右键
            MiddleDown = 0x0020,         //鼠标按下中键
            MiddleUp = 0x0040,           //鼠标松开中键
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,              //鼠标轮被移动
            VirtualDesk = 0x4000,        //虚拟桌面
            Absolute = 0x8000
        }

        //设置鼠标位置
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        //设置鼠标按键和动作
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy,
            uint data, UIntPtr extraInfo); //UIntPtr指针多句柄类型

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string strClass, string strWindow);

        //该函数获取一个窗口句柄,该窗口雷鸣和窗口名与给定字符串匹配 hwnParent=Null从桌面窗口查找
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
            string strClass, string strWindow);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(HandleRef hwnd, out NativeRect rect);

        //找到句柄后向窗口发送消息，SendMessage方法有很多的重载
        [DllImport("user32.dll ", EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        //定义变量
        const int AnimationCount = 80;
        private Point _mEndPosition;
        private int _mCount;

        #endregion

        private void ButSetPosition_Click(object sender, EventArgs e)
        {
            SetPosition newWin = new SetPosition();
            newWin.MPositionParam = _mPositionParam;
            newWin.SetParam();
            newWin.ShowDialog();
            _mPositionParam = newWin.MPositionParam;
            TxtOptInfor.Text += "Config Param...\r\n";
            TxtOptInfor.Text += new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(_mPositionParam)+ "\r\n"; 
        }

        private void ButAddPrice_Click(object sender, EventArgs e)
        {
            Addprice();
            TxtOptInfor.Text += "手动加价： "+ _mPositionParam.Price+"\r\n";
        }

        private void ButStart_Click(object sender, EventArgs e)
        {
            if (!_mbStartWork)
            {
                _mbStartWork = true;
                _mStartWork = new Thread(Start);
                _mStartWork.Start();
                TxtOptInfor.Text += "启动自动拍牌，自动加价金额为 ： " + _mPositionParam.Price + "\r\n";
            }
        }

        private void ButStop_Click(object sender, EventArgs e)
        {
            if (_mbStartWork)
            {
                _mbStartWork = false;
                _mStartWork.Join();
            }
            TxtOptInfor.Text += "停止自动拍牌....\r\n";
        }

        #region 时钟控件

        private void WebTimeClock()
        {

            //获取网络时间
            string dt = GetNetDateTime();
            DateTime nowTime = Convert.ToDateTime(dt);
            int i = 0;
            while (_mbWebTimeClock)
            {
                try
                {
                    nowTime = nowTime.AddMilliseconds(50);
                    var showTime = nowTime.AddMilliseconds(Convert.ToInt64(TxtWebTimeCal.Text));
                    if (RadButWeb.Checked)
                    {
                        _mNowTime = showTime;
                    }
                    SetWebText(showTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    if (i == 20)
                    {
                        nowTime = Convert.ToDateTime(dt);
                        i = 0;
                    }
                    Thread.Sleep(50);
                }
                catch
                {
                    // ignored
                }
                i++;
            }

        }

        delegate void SetTextCallBack(string text);

        private void SetWebText(string text)
        {
            if (WebNowTime.InvokeRequired)
            {
                SetTextCallBack stcb = SetWebText;
                Invoke(stcb, text);
            }
            else
            {
                WebNowTime.Text = text;
            }
        }

        private void SetLoaclText(string text)
        {
            if (WebNowTime.InvokeRequired)
            {
                SetTextCallBack stcb = SetLoaclText;
                Invoke(stcb, text);
            }
            else
            {
                LocalNowTime.Text = text;
            }
        }

        private static string GetNetDateTime()
        {
            WebRequest request = null;
            WebResponse response = null;
            WebHeaderCollection headerCollection = null;
            string datetime = string.Empty;
            try
            {
                request = WebRequest.Create("https://www.baidu.com");
                request.Timeout = 3000;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = (WebResponse)request.GetResponse();
                headerCollection = response.Headers;
                foreach (var h in headerCollection.AllKeys)
                { if (h == "Date") { datetime = headerCollection[h]; } }
                return datetime;
            }
            catch (Exception) { return datetime; }
            finally
            {
                if (request != null)
                { request.Abort(); }
                if (response != null)
                { response.Close(); }
                if (headerCollection != null)
                { headerCollection.Clear(); }
            }
        }

        private void LocalTimeClock()
        {
            DateTime nowTime = DateTime.Now;
            int i = 0;
            while (_mbWebTimeClock)
            {
                try
                {
                    nowTime = nowTime.AddMilliseconds(50);
                    var showTime = nowTime.AddMilliseconds(Convert.ToInt64(TxtLocalTimeCal.Text));
                    if(RadButLocal.Checked)
                        _mNowTime = showTime;
                    SetLoaclText(showTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    if (i == 20)
                    {
                        nowTime = DateTime.Now;
                        i = 0;
                    }
                    Thread.Sleep(50);
                }
                catch
                {
                    // ignored
                }
                i++;
            }
        }

        #endregion

        #region 启动线程

        private void Start()
        {
            while (_mbStartWork)
            {
                DateTime temp1 = Convert.ToDateTime(_mPositionParam.AddPriceTime);
                if (_mNowTime.Hour == temp1.Hour &&
                   _mNowTime.Minute == temp1.Minute &&
                    _mNowTime.Second == temp1.Second)
                {
                    Addprice();
                }

                DateTime temp2 = Convert.ToDateTime(_mPositionParam.BidTime);
                if (_mNowTime.Hour == temp2.Hour &&
                    _mNowTime.Minute == temp2.Minute &&
                    _mNowTime.Second == temp2.Second)
                {
                    SetBid();
                }

                DateTime temp3 = Convert.ToDateTime(_mPositionParam.RighTime);

                if (_mNowTime.Hour == temp3.Hour &&
                    _mNowTime.Minute == temp3.Minute &&
                    _mNowTime.Second == temp3.Second &&
                    _mNowTime.Millisecond >= 700)
                {
                    SetRight();
                }

                Thread.Sleep(50);
            }
        }

        #endregion

        #region Common

        private void Addprice()
        {
            NativeRect rect;

            int newPositionX = MousePosition.X;
            int newPositionY = MousePosition.Y;

            //获取获取IE窗口句柄
            IntPtr ptrTaskbar = FindWindow("IEFrame", null);
            if (ptrTaskbar == IntPtr.Zero)
            {
                MessageBox.Show("No windows found!");
                return;
            }
            GetWindowRect(new HandleRef(this, ptrTaskbar), out rect);
            _mEndPosition.X = _mPositionParam.Add100.X;
            _mEndPosition.Y = _mPositionParam.Add100.Y;

            switch (_mPositionParam.Price)
            {
                case 100:
                    SetCursorPos(_mPositionParam.Add100.X, _mPositionParam.Add100.Y);
                    break;
                case 200:
                    SetCursorPos(_mPositionParam.Add200.X, _mPositionParam.Add200.Y);
                    break;
                case 300:
                    SetCursorPos(_mPositionParam.Add300.X, _mPositionParam.Add300.Y);
                    break;
                default:
                    SetCursorPos(_mPositionParam.AddOrder.X, _mPositionParam.AddOrder.Y);
                    break;
            }

            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            SetCursorPos(newPositionX, newPositionY);

            _mLogHelper.WriteLog(LogType.Exception, "Addprice: " + newPositionX + ", " + newPositionY);
        }

        private void SetBid()
        {
            NativeRect rect;

            int newPositionX = MousePosition.X;
            int newPositionY = MousePosition.Y;

            //获取获取IE窗口句柄
            IntPtr ptrTaskbar = FindWindow("IEFrame", null);
            if (ptrTaskbar == IntPtr.Zero)
            {
                MessageBox.Show("No windows found!");
                return;
            }

            GetWindowRect(new HandleRef(this, ptrTaskbar), out rect);
            SetCursorPos(_mPositionParam.Bid.X, _mPositionParam.Bid.Y);
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);

            //还原鼠标位置
            SetCursorPos(newPositionX, newPositionY);
            _mLogHelper.WriteLog(LogType.Exception, "SetBid: " + newPositionX + ", " + newPositionY);
        }

        private void SetRight()
        {
            NativeRect rect;

            int newPositionX = MousePosition.X;
            int newPositionY = MousePosition.Y;

            //获取获取IE窗口句柄
            IntPtr ptrTaskbar = FindWindow("IEFrame", null);
            if (ptrTaskbar == IntPtr.Zero)
            {
                MessageBox.Show("No windows found!");
                return;
            }

            GetWindowRect(new HandleRef(this, ptrTaskbar), out rect);
            SetCursorPos(_mPositionParam.BidRight.X, _mPositionParam.BidRight.Y);
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);

            //还原鼠标位置
            SetCursorPos(newPositionX, newPositionY);
            _mLogHelper.WriteLog(LogType.Exception, "SetRight: " + newPositionX + ", " + newPositionY);
        }

        private void ReadParam()
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "config.js";
            if (File.Exists(strPath))
            {
                string json = string.Empty;
                using (FileStream fs = new FileStream(strPath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        json = sr.ReadToEnd();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var temp = js.Deserialize<PositionParam>(json);
                        _mPositionParam = temp;
                        sr.Close();
                        fs.Close();
                    }
                }
            }
        }
       
        #endregion

        //Tick:定时器,每当经过多少时间发生函数
        private void AddPriceTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                int stepx = (_mPositionParam.Add100.X - MousePosition.X) / _mCount;
                int stepy = (_mPositionParam.Add100.Y - MousePosition.Y) / _mCount;
                _mCount--;
                if (_mCount == 0)
                {
                    AddPriceTimer.Stop();
                    mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
                    mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
                }
                mouse_event(MouseEventFlag.Move, stepx, stepy, 0, UIntPtr.Zero);
            }
            catch (Exception exception)
            {
                // ignored
            }
        }

        private void AddPriceTextTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                int stepx = (_mPositionParam.Add100.X - MousePosition.X) / _mCount;
                int stepy = (_mPositionParam.Add100.Y - MousePosition.Y) / _mCount;
                _mCount--;
                if (_mCount == 0)
                {
                    AddPriceTextTimer.Stop();
                    mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
                    mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
                }

                mouse_event(MouseEventFlag.Move, stepx, stepy, 0, UIntPtr.Zero);
               
            }
            catch (Exception exception)
            {
                // ignored
            }
        }

        private void Close_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_mbWebTimeClock)
            {
                _mbWebTimeClock = false;
                _mWebClockThread.Join();
            }
            if (_mbLocalTimeClock)
            {
                _mbLocalTimeClock = false;
                _mLocalClockThread.Join();
            }
        }

        private void OlayNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键  
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数  
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar);
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符  
                }
            }
        }
    }
}
