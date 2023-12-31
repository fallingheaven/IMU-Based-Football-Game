using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Debug = UnityEngine.Debug;

public class UnityPythonCommunication : MonoBehaviour
{
    #region 参变量

        public SettingDataSO settingData;
        private Process _pythonProcess;
        private TcpClient _client;
        private NetworkStream _stream;
        private readonly byte[] _receiveBuffer = new byte[1024];
        private static readonly int[] DataLength = new int[] { 8, 3, 4 };
        
        // 用于计算加速度瞬时差值来判断踢球
        private Vector3 _previousAcc;
        public float[] _quat = new float[4];
        private float[] _acc = new float[4];
    
        [Header("事件")]
        public VoidEventSO imuKickEventSO;
        public VoidEventSO onConnectedEventSO;

        // [Header("事件监听")] 
        // public VoidEventSO pauseGameEventSO;
        // public VoidEventSO resumeGameEventSO;

        // private bool _pause = false;
        private bool _isReceiving = false;
        private bool _stopReceiving = false;
        private readonly ManualResetEvent _receiveCompleteEvent = new ManualResetEvent(false);
    
    #endregion

    // 启动python程序，接收IMU数据
    public void StartPythonScript()
    {
        if (_client is { Connected: true })
        {
            Debug.Log("已占用连接");
            return;
        }
        
        UnityMainThreadDispatcher.Initialize();
        
        // Debug.Log("Start");
        // 在后台线程中执行耗时操作
        var backgroundThread = new Thread(OnStartPythonScript);
        backgroundThread.Start();
        
        // StartCoroutine(OnStartPythonScript());
    }

    private void OnStartPythonScript()
    {
        // Debug.Log("开始连接");
        // 指定 Python 脚本的路径，替换为你的 Python 脚本的实际路径
        var pythonPath = Path.Combine("Assets", "Scripts", "DataProcessing", "main.py");

        // 创建一个 ProcessStartInfo 对象，用于配置进程启动参数
        var startInfo = new ProcessStartInfo
        {
            FileName = "python", // 使用 Python 解释器执行脚本
            Arguments = pythonPath, // 传递 Python 脚本路径作为参数
            RedirectStandardOutput = true, // 指定是否重定向标准输出
            UseShellExecute = false, // 指定是否使用外壳执行程序
            CreateNoWindow = true // 不显示外部窗口
        };

        // 创建一个 Process 对象并配置其 StartInfo 属性
        _pythonProcess = new Process();
        _pythonProcess.StartInfo = startInfo;

        // 添加一个事件处理程序，用于捕获 Python 脚本的输出
        // pythonProcess.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);

        // 启动 Python 进程
        _pythonProcess.Start();
        _pythonProcess.BeginOutputReadLine(); // 启动异步读取标准输出
        // 等待 Python 进程完成启动
        // _pythonProcess.WaitForInputIdle();

        // 等待一段时间以确保 Python 服务器已启动
        System.Threading.Thread.Sleep(1000);

        // 连接到 Python 服务器
        ConnectToPythonServer("127.0.0.1", 1234); // 服务器地址和端口要与 Python 脚本中的一致
    }
    
    // 连接到python的socket服务器
    private void ConnectToPythonServer(string ipAddress, int port)
    {
        _isReceiving = true;
        _client = new TcpClient(ipAddress, port);
        _stream = _client.GetStream();

        while (true)
        {
            // Debug.Log("未连接");
            // if (!_client.Connected) continue;
            var bytesRead = _stream.Read(_receiveBuffer, 0, _receiveBuffer.Length);
            if (bytesRead <= 0) continue;
            
            UnityMainThreadDispatcher.Enqueue(() =>
            {
                // Debug.Log("已连接");
                onConnectedEventSO.RaiseEvent();
            });
            
            break;
        }
        // Debug.Log("其实连接上了");
        // 启动数据接收
        ReceiveDataCoroutine();
        // StartCoroutine(ReceiveDataCoroutine());
    }
    
    // 处理数据
    private void ReceiveDataCoroutine()
    {
        _previousAcc = Vector3.zero;
        while (!_stopReceiving)
        {
            // if (_pause) continue;

            // Debug.Log(_stream.CanRead);
            // 数据是否可读
            if (!_stream.CanRead)
            {
                // Debug.Log("错误数据");
                continue;
            }
            
            // 接收数据
            var bytesRead = _stream.Read(_receiveBuffer, 0, _receiveBuffer.Length);
            if (bytesRead <= 0)
            {
                // Debug.Log("连接已关闭");
                _client.Close();
                break;
            }
            
            // 处理数据
            var receivedData = Encoding.UTF8.GetString(_receiveBuffer, 0, bytesRead);
            var splitData = receivedData.Split(", ");
            if (splitData.Length >= DataLength[0])
            {
                var startPos = 0; // 读入数据的处理起点，这里以S为标识
                while (startPos < splitData.Length && splitData[startPos] != "S")
                {
                    startPos++;
                }
            
                // 读入数据格式不正确
                if (startPos >= splitData.Length)
                {
                    continue;
                }
                
                for (var i = startPos + 1; i < splitData.Length; i += DataLength[0])
                {
                    if (i + DataLength[1] > splitData.Length) break;
                    if (!ConvertStringToFloat(splitData, _acc, i, DataLength[1]))
                    {
                        continue;
                    }
                
                    if (i + DataLength[1] + DataLength[2] > splitData.Length) break;
                    if (!ConvertStringToFloat(splitData, _quat, i + DataLength[1], DataLength[2]))
                    {
                        continue;
                    }
                    
                    // 处理加速度
                    var currentAcc = new Vector3(_acc[0], _acc[1], _acc[2]);
                    if (_previousAcc != Vector3.zero &&
                        _previousAcc.sqrMagnitude - currentAcc.sqrMagnitude > settingData.imuSensitivity)
                    {
                        UnityMainThreadDispatcher.Enqueue(() =>
                        {
                            imuKickEventSO.RaiseEvent();
                        });
                    }
                    _previousAcc = currentAcc;
                }
                
            }
            
            // yield return null;
        }

        _receiveCompleteEvent.Set();
    }

    // 退出应用
    private void OnApplicationQuit()
    {
        Time.timeScale = 1f;
        // 标志停止接受数据
        _stopReceiving = true;
        if (!_isReceiving) _receiveCompleteEvent.Set();
        _isReceiving = false;
        
        // 等待数据接收循环完成
        _receiveCompleteEvent.WaitOne();
        
        // Debug.Log("程序关闭");
        _client?.Close();
        _stream?.Close();
        
        // 关闭 Python 进程和与 Python 服务器的连接
        if (_pythonProcess is { HasExited: false })
        {
            _pythonProcess.Kill();
        }
    }

    // 转化读入数据的
    private bool ConvertStringToFloat(IReadOnlyList<string> s, float[] arr, int beginPos, int len)
    {
        for (var i = 0; i < len; i++)
        {
            if (!float.TryParse(s[beginPos + i], out arr[i]))
            {
                return false;
            }
        }

        return true;
    }

    // 重置IMU初始姿态
    public void Resetimu()
    {
        var quaternion = new Quaternion(_quat[0], _quat[1], _quat[2], _quat[3]);
        SetIMUInitialQuaternion.InitImuQuaternion(quaternion);
    }
    //
    // private void PauseReceive()
    // {
    //     _pause = true;
    // }
    //
    // private void ResumeReceive()
    // {
    //     _pause = false;
    // }
}
