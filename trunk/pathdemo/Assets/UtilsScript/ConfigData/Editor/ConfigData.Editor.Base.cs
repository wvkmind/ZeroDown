using System;
using System.Diagnostics;

namespace ConfigData.Editor
{
    public enum DataType
    {
        NORMAL_TABLE,
        LIST,
        KEY_VALUE
    }

    public abstract class Base
    {
        public abstract string ModelDesc();
        public abstract DataType ModelType();
        public void GreateData(string file_path, string output_path)
        {
            try
            {
                if (file_path == "空" || output_path == "空")
                {
                    throw new Exception("请检查参数");
                }
                else
                {
                    using (Process myProcess = new Process())
                    {
                        myProcess.StartInfo.UseShellExecute = false;
                        myProcess.StartInfo.WorkingDirectory = System.Environment.CurrentDirectory + "/../../tools/ruby/bin/";
                        myProcess.StartInfo.FileName = System.Environment.CurrentDirectory + "/../../tools/ruby/bin/ruby.exe";
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.StartInfo.Arguments = System.Environment.CurrentDirectory + "/../../tools/ruby/code/ParseExcel.rb " + " " + file_path + " " + output_path + " " + ModelType();
                        myProcess.StartInfo.RedirectStandardOutput = true;
                        myProcess.StartInfo.RedirectStandardError = true;
                        #if UNITY_EDITOR_OSX
                            UnityEngine.Debug.Log("ruby " + myProcess.StartInfo.Arguments);
                        #else
                            myProcess.Start();
                            UnityEngine.Debug.Log(myProcess.StandardOutput.ReadToEnd());
                            string err = myProcess.StandardError.ReadToEnd();
                            if (err.Length != 0) UnityEngine.Debug.LogError(err); 
                        #endif
                    }
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e.Message);
            }
        }
    }
}