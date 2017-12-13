using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PaiPai
{
    public class Position
    {
        private Point _mEndPosition;

        private IntPtr _mIePtrTaskbar = new IntPtr();

        public Position()
        {
            _mIePtrTaskbar = new IntPtr();
        }

        public IntPtr MIePtrTaskbar
        {
            get { return _mIePtrTaskbar; }
            set { _mIePtrTaskbar = value; }
        }

        public Point MEndPosition
        {
            get { return _mEndPosition; }
            set { _mEndPosition = value; }
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

        //结构体布局 本机位置
        [StructLayout(LayoutKind.Sequential)]
        struct NativeRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(HandleRef hwnd, out NativeRECT rect);

        /// <summary>  
        /// 获取指定窗体的标题  
        /// </summary>  
        /// <param name="winHandle">窗体句柄</param>  
        /// <param name="title">缓冲区取用于存储标题</param>  
        /// <param name="size">缓冲区大小</param>  
        /// <returns></returns>  
        [DllImport("User32.dll")]
        public static extern int GetWindowText(IntPtr winHandle, StringBuilder title, int size);


        public string IeInfor()
        {
            NativeRECT rect;
            //获取获取IE窗口句柄
            _mIePtrTaskbar = FindWindow("IEFrame", null);
            if (_mIePtrTaskbar == IntPtr.Zero)
            {
                MessageBox.Show("No windows found!");
                return null;
            }
            StringBuilder title = new StringBuilder(255);
            GetWindowText(_mIePtrTaskbar, title, 255);

            //获取窗体大小
            GetWindowRect(new HandleRef(this, _mIePtrTaskbar), out rect);
            _mEndPosition.X = (rect.left + rect.right) / 2;
            _mEndPosition.Y = (rect.top + rect.bottom) / 2;
            
            Thread.Sleep(1000);
            SetCursorPos(_mEndPosition.X, _mEndPosition.Y);
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            return string.Format("( {0}, {1} )",Control.MousePosition.X, Control.MousePosition.Y);
        }
    }
}
