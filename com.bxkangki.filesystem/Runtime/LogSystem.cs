using System.Globalization;
using UnityEngine;
using System.IO;

namespace FileSystem {
    public static class LogSystem {
        public static string Path = "logs/";
        private static bool initalized = false;
        public static void Text(string data) {
#if UNITY_EDITOR
            Debug.Log(data);
#endif
            var localDate = System.DateTime.Now;
            var culture = new CultureInfo("en-US");
            data = string.Format("{0} >> {1}", localDate.ToString(culture), data);
            if (initalized) {
                FileIO.WriteLineTextFile(string.Format("{0}/{1}/log_latest.txt", Application.persistentDataPath, Path), data);
            }
        }


        public static void Init() {
            CreateLogFile();
            initalized = true;
            Text("Logging is Initalized");
        }

        private static void CreateLogFile() {
            FileIO.CreateDirectory(string.Format("{0}/{1}/", Application.persistentDataPath, Path));
            string p1 = string.Format("{0}/{1}/log_latest.txt", Application.persistentDataPath, Path);
            string createDate = FileIO.GetFileLastWriteTime(p1).ToString("d");
            string currentDate = System.DateTime.Now.Date.ToString("d");
#if UNITY_EDITOR
            Debug.Log(createDate);
            Debug.Log(currentDate);
#endif
            if (createDate != currentDate) {
                string p2 = string.Format("{0}/{1}/log_{2}.txt", Application.persistentDataPath, Path, createDate);
                if (FileIO.CheckFile(p2)) {
                    FileIO.ReplaceFile(p1, p2);
                    FileIO.RemoveFile(p1);
                }
            }
            FileIO.CreateFile(p1);
        }
    }
}