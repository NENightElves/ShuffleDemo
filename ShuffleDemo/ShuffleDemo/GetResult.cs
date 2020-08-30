using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ShuffleDemo
{
    class GetResult
    {
        public static String getResultString(String name, ref long start, ref long end)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(name);
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            start = DateTime.Now.Ticks;
            process.Start();
            String result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            end = DateTime.Now.Ticks;
            return result;
        }
        public static String getResultString2(String name, int n, ref long start, ref long end)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(name, "" + n);
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            start = DateTime.Now.Ticks;
            process.Start();
            String result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            end = DateTime.Now.Ticks;
            return result;
        }

        public static String compileMain(String src, String dst)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo("gcc", $"{src} -o {dst}");
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            String result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            return result;
        }
    }

    class ResultInfo
    {
        public int[,] Result { get; private set; }
        public long StartTime { get; private set; }
        public long EndTime { get; private set; }

        public ResultInfo(String result, long start, long end)
        {
            while (result.EndsWith("\n") || result.EndsWith("\r")) result = result.Remove(result.Length - 1);
            String[] resultLines = result.Split("\n");
            String[] resultLineItems;
            Result = new int[resultLines.Length, 54];
            for (int i = 0; i < resultLines.Length; i++)
            {
                resultLineItems = resultLines[i].Split(" ");
                for (int j = 0; j < 54; j++)
                {
                    Result[i, j] = Convert.ToInt32(resultLineItems[j]);
                }
            }
            StartTime = start;
            EndTime = end;
        }
    }
}
