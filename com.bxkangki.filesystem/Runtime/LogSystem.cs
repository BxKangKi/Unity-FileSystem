using System;
using System.Globalization;
using UnityEngine;

namespace FileSystem
{
    public static class LogSystem
    {
        public static string Path = "logs/";
        private static bool initalized = false;

        public static void Text(string data)
        {
#if UNITY_EDITOR
            Debug.Log(data);
#endif
            var localDate = DateTime.Now;
            var culture = new CultureInfo("en-US");
            data = string.Format("{0} >> {1}", localDate.ToString(culture), data);
            if (initalized)
            {
                FileIO.WriteLineTextFile(string.Format("{0}/{1}/log_latest.txt", Application.persistentDataPath, Path), data);
            }
        }


        public static void Init()
        {
            CreateLogFile();
            initalized = true;
            Text("Logging is Initalized");
        }


        private static void CreateLogFile()
        {
            FileIO.CreateDirectory(string.Format("{0}/{1}/", Application.persistentDataPath, Path));
            string p1 = string.Format("{0}/{1}/log_latest.txt", Application.persistentDataPath, Path);
            DateTime modifiedDate = FileIO.GetFileLastWriteTime(p1);
            DateTime currentDate = DateTime.Now.Date;

#if UNITY_EDITOR
            Debug.Log(modifiedDate.ToString("d"));
            Debug.Log(currentDate.ToString("d"));
#endif
            if (modifiedDate.ToString("d") != currentDate.ToString("d"))
            {
                string p2 = string.Format("{0}/{1}/log_{2}{3}{4}.txt", Application.persistentDataPath, Path, modifiedDate.Year, modifiedDate.Month, modifiedDate.Day);
                FileIO.ReplaceFile(p1, p2);
                FileIO.RemoveFile(p1);
            }
            FileIO.CreateFile(p1);
        }
    }
}