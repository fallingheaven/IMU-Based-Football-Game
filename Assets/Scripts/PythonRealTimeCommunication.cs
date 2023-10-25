using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Unity.VisualScripting;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

public class UnityPythonCommunication : MonoBehaviour
{
    private Process _pythonProcess;
    private TcpClient _client;
    private NetworkStream _stream;
    private byte[] _receiveBuffer = new byte[1024];
    
    public UnityEvent<float[]> angleChange;
    private static readonly int[] DataLength = new int[3] { 7, 3, 4 };

    void Start()
    {
        StartPythonScript();
    }

    void StartPythonScript()
    {
        // 指定 Python 脚本的路径，替换为你的 Python 脚本的实际路径
        string pythonPath = Path.Combine("Assets", "Scripts", "main.py");

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
        // pythonProcess.WaitForInputIdle();

        // 等待一段时间以确保 Python 服务器已启动
        System.Threading.Thread.Sleep(2000);

        // 连接到 Python 服务器
        ConnectToPythonServer("127.0.0.1", 8080); // 服务器地址和端口要与 Python 脚本中的一致
    }

    void ConnectToPythonServer(string ipAddress, int port)
    {
        _client = new TcpClient(ipAddress, port);
        _stream = _client.GetStream();

        // 启动数据接收
        // StartReceiving();
        StartCoroutine(ReceiveDataCoroutine());
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator ReceiveDataCoroutine()
    {
        while (true)
        {
            if (_stream.CanRead)
            {
                var bytesRead = _stream.Read(_receiveBuffer, 0, _receiveBuffer.Length);
                if (bytesRead > 0)
                {
                    var receivedData = Encoding.UTF8.GetString(_receiveBuffer, 0, bytesRead);
                    var splitData = receivedData.Split(", ");
                    Debug.Log(receivedData);
                    // Debug.Log($"{splitData[0]}, {splitData[1]}, {splitData[2]}, {splitData[3]}, {splitData[4]}, " +
                    //           $"{splitData[5]}, {splitData[6]}");
                    if (splitData.Length >= DataLength[0])
                    {
                        var quat = new float[4];
                        for (var i = 0; i < splitData.Length; i += DataLength[0])
                        {
                            for (var j = 0; j < DataLength[2]; j++)
                            {
                                quat[j] = float.Parse(splitData[DataLength[1] + j]);
                            }
                        }
                        
                        angleChange?.Invoke(quat);
                        // Debug.Log($"{quat[0]}, {quat[1]}, {quat[2]}, {quat[3]}");
                    }
                }
                else
                {
                    Debug.Log("连接已关闭");
                    _client.Close();
                }
            }

            yield return null;
            // yield return new WaitForSeconds(0.002f); // 等待一段时间再继续接收数据
        }
    }
    

    // 用于处理 Python 脚本的标准输出的事件处理程序
    void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
    {
        if (!string.IsNullOrEmpty(outLine.Data))
        {
            Debug.Log("Python Output: " + outLine.Data); // 在 Unity 控制台中显示 Python 输出
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("0");
        // 关闭 Python 进程和与 Python 服务器的连接
        if (_pythonProcess != null && !_pythonProcess.HasExited)
        {
            _pythonProcess.Kill();
        }

        if (_client != null)
        {
            _client.Close();
        }
    }
}
