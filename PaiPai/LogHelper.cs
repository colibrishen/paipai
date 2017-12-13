using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

//=================================================================
//     注：简单的日志写入类
//=================================================================

namespace PaiPai
{
    public enum LogType
    {
        Success = 0,
        Error,
        Exception,
        Test,
        Show,
        Null
    }

    public struct LogMesssge
    {
        public int LogType { get; set; }
        public string StrMessage { get; set; }
    }

    public class LogHelper
    {
        public LogHelper()
        {
            LogStatus = false;
            _mDeleteStart = false;
            _mMgsQueue = new Queue<LogMesssge>();
            _mLogThreadStart = false;
            _mMutex = new Mutex();
            _mToday = DateTime.Now;
            _mStrRootPath = string.Empty;
            _mStrComponentsName = string.Empty;
            _mStrLogFileName = string.Empty;
            _mStrProgramPath = string.Empty;
            _mIntProcessId = 0;
            _mOutputMessage = new Queue<LogMesssge>();
            _mNextMessage = new LogMesssge();
            _mLogDays = 0;
        }

        #region 参数

        /// <summary>
        /// 日志线程
        /// </summary>
        private Thread _mLogThread;
        private bool _mLogThreadStart;

        /// <summary>
        /// 删除日志线程
        /// </summary>
        private Thread _mDeleteThread;
        private bool _mDeleteStart;

        /// <summary>
        /// 互斥锁
        /// </summary>
        private readonly Mutex _mMutex;

        /// <summary>
        /// 今天的日期
        /// </summary>
        private DateTime _mToday;

        /// <summary>
        /// 输出日志根路径
        /// </summary>
        private string _mStrRootPath;

        private LogMesssge _mNextMessage;

        /// <summary>
        /// 程序文件夹
        /// </summary>
        private static string _mStrComponentsName;

        public string StrComponentsName
        {
            get { return _mStrComponentsName; }
            set { _mStrComponentsName = value; }
        }

        /// <summary>
        /// 日志文件名
        /// </summary>
        private string _mStrLogFileName;

        private string _mStrProgramPath;

        private int _mIntProcessId;

        /// <summary>
        /// 日志队列
        /// </summary>
        private readonly Queue<LogMesssge> _mMgsQueue;

        private readonly Queue<LogMesssge> _mOutputMessage;

        /// <summary>
        /// 日志状态
        /// </summary>
        public bool LogStatus { get; set; }

        /// <summary>
        /// 日志保存天数
        /// </summary>
        private int _mLogDays;

        /// <summary>
        /// 是否返回日志消息
        /// </summary>
        public bool IsOutputMessage { get; set; }

        public string StrRootPath
        {
            get => _mStrRootPath;
            set => _mStrRootPath = value;
        }

        /// <summary>
        /// 日志保存天数
        /// </summary>
        public int LogDays
        {
            get { return _mLogDays; }
            set { _mLogDays = value; }
        }

        #endregion

        /// <summary>
        /// 启动线程
        /// </summary>
        public void OnStart()
        {
            if (_mLogThreadStart) return;
            _mLogThreadStart = true;
            _mLogThread = new Thread(LogWork);
            _mLogThread.Start();
        }

        /// <summary>
        /// 停止线程
        /// </summary>
        public void OnStop()
        {
            if (_mDeleteStart)
            {
                _mDeleteStart = false;
                _mDeleteThread.Join();
            }

            if (!_mLogThreadStart) return;
            _mLogThreadStart = false;
            _mLogThread.Join();
            Clear();
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        public void OnDelete()
        {
            if (_mDeleteStart) return;
            _mDeleteStart = true;
            _mDeleteThread = new Thread(DeleteWork);
            _mDeleteThread.Start();
        }

        /// <summary>
        /// 日志状态
        /// </summary>
        /// <returns></returns>
        public bool IsThreadStatus()
        {
            return _mLogThreadStart;
        }

        /// <summary>
        /// 输出日志内容
        /// </summary>
        /// <returns></returns>
        public LogMesssge OutputLogMessage()
        {
            var temp = new LogMesssge { LogType = (int)LogType.Null };
            if (!IsOutputMessage) return temp;
            lock (_mOutputMessage)
            {
                if (_mOutputMessage.Count <= 0) return temp;
                return _mOutputMessage.Dequeue();
            }
        }

        /// <summary>
        /// 日志主线程
        /// </summary>
        private void LogWork()
        {
            LogStatus = true;
            if (!SetLogPath())
            {
                Console.WriteLine("The log file path didn't set !");
                OnStop();
                LogStatus = false;
                return;
            }

            CreateLogFile();
            WriteLog();
            while (_mLogThreadStart)
            {
                if (_mMgsQueue.Count > 0)
                {
                    lock (_mMgsQueue)
                    {
                        try
                        {
                            var temp = _mMgsQueue.Dequeue();
                            if (_mNextMessage.StrMessage != temp.StrMessage)
                            {
                                WriteLog(temp);
                            }
                            _mNextMessage = temp;
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }
                Thread.Sleep(50);
            }
        }

        private void DeleteWork()
        {
            while (_mDeleteStart)
            {
                if (_mLogDays >0)
                {
                    
                }

                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        private void Clear()
        {
            while (_mMgsQueue.Count > 0)
            {
                lock (_mMgsQueue)
                {
                    WriteLog(_mMgsQueue.Dequeue());
                }
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// 设置日志文件的路径
        /// </summary>
        /// <returns></returns>
        private bool SetLogPath()
        {
            try
            {
                if (!Directory.Exists(string.Format("{0}\\{1}", _mStrRootPath, _mStrComponentsName)))
                    Directory.CreateDirectory(string.Format("{0}\\{1}", _mStrRootPath, _mStrComponentsName));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建日志文件
        /// </summary>
        private void CreateLogFile()
        {
            _mToday = DateTime.Now;
            _mStrLogFileName = _mStrRootPath + "\\" + _mStrComponentsName + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_" +
                               DateTime.Now.ToString("hhmm") + ".log";
            FileInfo fileInfo = new FileInfo(_mStrLogFileName);
            if (!fileInfo.Exists)
                fileInfo.Create().Close();
        }

        /// <summary>
        /// 写入日志信息
        /// </summary>
        private void WriteLog()
        {
            _mMutex.WaitOne();
            CreateLogNewFile();
            StreamWriter writer = File.AppendText(_mStrLogFileName);
            string strMsg = "-= " + _mStrComponentsName + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " =-";
            try
            {
                writer.WriteLine("\n");
                writer.WriteLine(strMsg);
                writer.WriteLine("===============================================================================");
                GetProgramInfo();
                writer.WriteLine("-- Process Info ------------------------------");
                writer.WriteLine("Program Path  :   {0}", _mStrProgramPath);
                writer.WriteLine("Process ID    :   {0}", _mIntProcessId);
                writer.WriteLine("----------------------------------------------");
            }
            catch (Exception e)
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       |    Log Error :" + e.Message);
            }
            writer.Flush();
            writer.Close();
            _mMutex.ReleaseMutex();
        }

        /// <summary>
        /// 把日志写入到队列中
        /// </summary>
        /// <param name="pType">日志类型</param>
        /// <param name="strText">日志内容</param>
        public void WriteLog(LogType pType, string strText)
        {
            try
            {
                lock (_mMgsQueue)
                {
                    LogMesssge logMesssge = new LogMesssge();
                    logMesssge.LogType = (int)pType;
                    logMesssge.StrMessage = strText;
                    _mMgsQueue.Enqueue(logMesssge);
                    if (IsOutputMessage)
                    {
                        _mOutputMessage.Enqueue(logMesssge);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 写入日志信息
        /// </summary>
        /// <param name="logMesssge"></param>
        private void WriteLog(LogMesssge logMesssge)
        {
            _mMutex.WaitOne();
            CreateLogNewFile();
            StreamWriter writer = File.AppendText(_mStrLogFileName);
            try
            {
                switch ((LogType)logMesssge.LogType)
                {
                    case LogType.Success:
                        Console.ForegroundColor = ConsoleColor.Green;
                        writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       |    " + logMesssge.StrMessage);
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       |    " + logMesssge.StrMessage);
                        break;
                    case LogType.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       *>    Log Error : " + logMesssge.StrMessage);
                        writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       *>    Log Error : " + logMesssge.StrMessage);
                        break;
                    case LogType.Exception:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       *>    Log Exception : " + logMesssge.StrMessage);
                        writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       *>    Log Exception : " + logMesssge.StrMessage);
                        break;
                    case LogType.Test:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       *>    Log Test : " + logMesssge.StrMessage);
                        writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       *>    Log Test : " + logMesssge.StrMessage);
                        break;
                    case LogType.Show:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       *>    Log Test : " + logMesssge.StrMessage);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       *>    Log Error :" + e.Message + " :" +
                                  logMesssge.StrMessage);
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       *>    Log Error :" + e.Message + " :" +
                                 logMesssge.StrMessage);
            }
            writer.Flush();
            writer.Close();
            _mMutex.ReleaseMutex();
        }

        /// <summary>
        /// 用来获取程序进程ID
        /// </summary>
        private void GetProgramInfo()
        {
            _mStrProgramPath = Process.GetCurrentProcess().MainModule.FileName;
            _mIntProcessId = Process.GetCurrentProcess().Id;
        }

        /// <summary>
        /// 创建新的日志文件
        /// </summary>
        private void CreateLogNewFile()
        {
            DateTime nowTime = DateTime.Now;
            var tempTime = nowTime - _mToday;
            if (tempTime.Days != 0)
            {
                CreateLogFile();
                WriteLog();
            }
        }

        

    }
}
