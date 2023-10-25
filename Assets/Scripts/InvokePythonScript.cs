using UnityEngine;
using System.Diagnostics;

public class CallPythonScript : MonoBehaviour
{
    void Start()
    {
        string pythonPath = "E:\\python\\test3\\Unity和Python通信测试.py"; // 替换为你的Python脚本路径

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "python"; // 使用Python解释器
        startInfo.Arguments = pythonPath;
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;

        Process process = new Process();
        process.StartInfo = startInfo;
        process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);

        process.Start();
        process.BeginOutputReadLine();

        process.WaitForExit();
        process.Close();
    }

    void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
    {
        if (!string.IsNullOrEmpty(outLine.Data))
        {
            UnityEngine.Debug.Log("Python Output: " + outLine.Data);
        }
    }
}