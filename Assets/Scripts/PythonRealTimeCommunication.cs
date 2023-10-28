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
    private Vector3 _previousAcc;
    
    public UnityEvent<float[]> angleChange;
    public UnityEvent<float[]> velocityChange;
    public UnityEvent checkShoot;
    private static readonly int[] DataLength = new int[3] { 8, 3, 4 };

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
        StartCoroutine(ReceiveDataCoroutine());
    }
    
    IEnumerator ReceiveDataCoroutine()
    {
        _previousAcc = Vector3.zero;
        while (true)
        {
            if (!_stream.CanRead) continue;
            
            var bytesRead = _stream.Read(_receiveBuffer, 0, _receiveBuffer.Length);
            if (bytesRead <= 0)
            {
                Debug.Log("连接已关闭");
                _client.Close();
            }
            
            var receivedData = Encoding.UTF8.GetString(_receiveBuffer, 0, bytesRead);
            var splitData = receivedData.Split(", ");
            // Debug.Log(receivedData);
            // Debug.Log($"{splitData[0]}, {splitData[1]}, {splitData[2]}");
            if (splitData.Length >= DataLength[0])
            {
                var quat = new float[4];
                var acc = new float[3];
                var startPos = 0;
                while (startPos < splitData.Length && splitData[startPos] != "S")
                {
                    startPos++;
                }
            
                if (startPos >= splitData.Length)
                {
                    yield return null;
                }
                
                for (var i = startPos + 1; i < splitData.Length; i += DataLength[0])
                {
                    if (i + DataLength[1] > splitData.Length) break;
                    if (!ConvertStringToFloat(splitData, acc, i, DataLength[1]))
                    {
                        continue;
                    }
                
                    if (i + DataLength[1] + DataLength[2] > splitData.Length) break;
                    if (!ConvertStringToFloat(splitData, quat, i + DataLength[1], DataLength[2]))
                    {
                        continue;
                    }
                    
                    // Debug.Log($"{quat[0]}, {quat[1]}, {quat[2]}, {quat[3]}");
                    // Debug.Log($"{acc[0]}, {acc[1]}, {acc[2]}");
                    // velocityChange?.Invoke(acc);
                    
                    var currentAcc = new Vector3(acc[0], acc[1], acc[2]);
                    // Debug.Log($"{_previousAcc}, {currentAcc}, {_previousAcc.sqrMagnitude}, {currentAcc.sqrMagnitude}");
                    if (_previousAcc != Vector3.zero && _previousAcc.sqrMagnitude - currentAcc.sqrMagnitude > 3000)
                    {
                        checkShoot?.Invoke();
                    }
                    _previousAcc = currentAcc;
                    
                    angleChange?.Invoke(quat);
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

    bool ConvertStringToFloat(string[] s, float[] arr, int beginPos, int len)
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
}
