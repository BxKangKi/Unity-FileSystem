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
                // string.Format("{0}{1}_{2}{3}", PATH, NAME, 0, EXTENSION)
                FileIO.WriteLineTextFile(Path + "log_latest.txt", data);
            }
        }


        public static void Init() {
            CreateLogFile();
            initalized = true;
            Text("Logging is Initalized");
        }

        private static void CreateLogFile() {
            FileIO.CreateDirectory(Path);
            string p1 = Path + "log_latest.txt";
            string createDate = FileIO.GetFileLastWriteTime(p1).ToString("d");
            string currentDate = System.DateTime.Now.Date.ToString("d");
#if UNITY_EDITOR
            Debug.Log(createDate);
            Debug.Log(currentDate);
#endif
            if (createDate != currentDate) {
                string p2 = string.Format(Path + "log_{0}.txt", createDate);
                if (FileIO.CheckFile(p2)) {
                    FileIO.ReplaceFile(p1, p2);
                    FileIO.RemoveFile(p1);
                }
            }
            FileIO.CreateFile(p1);
        }
    }
}