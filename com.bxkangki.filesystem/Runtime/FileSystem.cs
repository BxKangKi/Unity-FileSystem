using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;

namespace FileSystem {
    public class FileIO {
        private static string gamePath = "";
        private static bool initalized = false;

        private const string InitializeError = "File System is not initialized! You should initialize this system at least once, and set game path correctly.";

        public static void Init(string path) {
            gamePath = path;
            initalized = true;
        }

        //private static string KEY = "Why Did You Want To See This Content? Well, I Guess You Want To Edit Your Save File, Right? It's No Matter To Me, But All Occured Problems Via Edit This Is Your Responsiblity.";

        public static void WriteJsonData<T>(string path, T data, bool create = true) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return;
            }
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            path = gamePath + path;
            if (File.Exists(path)) {
                using(StreamWriter writer = new StreamWriter(path)) {
                    try {
                        writer.Write(JsonUtility.ToJson(data, true));
                    } catch(IOException e) {
                        LogSystem.Text(e.ToString());
                    }
                    writer.Close();
                }
            }
        }

        public static void WriteJsonDataToBytes<T>(string path, T data, bool create = true) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return;
            }
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            path = gamePath + path;
            if (File.Exists(path)) {
                string json = JsonUtility.ToJson(data, true);
                byte[] bytes = System.Text.Encoding.Default.GetBytes(json);
                File.WriteAllBytes(path, bytes);
            }
        }

        public static T ReadJsonData<T>(string path, bool create = true) {
            string result = "";
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return JsonUtility.FromJson<T>(result);
            }
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            path = gamePath + path;
            if (File.Exists(path)) {
                using(StreamReader reader = new StreamReader(path)) {
                    try {
                        result = reader.ReadToEnd();
                    } catch(IOException e) {
                        LogSystem.Text(e.ToString());
                    }
                    reader.Close();
                }
            }
            return JsonUtility.FromJson<T>(result);
        }


        public static T ReadJsonDataFromBytes<T>(string path, bool create = true) {
            string result = "";
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return JsonUtility.FromJson<T>(result);
            }
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            path = gamePath + path;
            if (File.Exists(path)) {
                byte[] bytes = File.ReadAllBytes(path);
                result = System.Text.Encoding.UTF8.GetString(bytes);
            }
            return JsonUtility.FromJson<T>(result);
        }


        public static async void WriteLineTextFile(string filePath, string data) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return;
            }
            filePath = gamePath + filePath;
            if (File.Exists(filePath)) {
                var fi = new FileInfo(filePath);
                using (StreamWriter sw = fi.AppendText()) {
                    try {
                        await sw.WriteLineAsync(data);
                    } catch(IOException e) {
                        LogSystem.Text(e.ToString());
                    }
                    sw.Close();
                }
            }
        }


        public static bool CheckDirectory(string path, bool create = false) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return false;
            }
            string p = gamePath + path;
            DirectoryInfo info = new DirectoryInfo(p);
            if (!info.Exists && create) {
                CreateDirectory(path);
            }
            return info.Exists;
        }

        public static byte[] ReadAllBytes(string path) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return new byte[0];
            }
            path = gamePath + path;
            return File.ReadAllBytes(path);
        }


        public static void CreateDirectory(string path) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return;
            }
            string p = gamePath + path;
            DirectoryInfo info = new DirectoryInfo(p);
            if (info.Exists) {
                return;
            }
            try {
                info.Create();
            } catch(IOException e) {
                LogSystem.Text(e.ToString());
            }
        }


        public static bool CheckFile(string path, bool create = false) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return false;
            }
            string p = gamePath + path;
            FileInfo info = new FileInfo(p);
            if (!info.Exists && create) {
                CreateFile(path);
            }
            return info.Exists;
        }


        public static void CopyFile(string from, string to) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return;
            }
            string p1 = gamePath + from;
            string p2 = gamePath + to;
            FileInfo info = new FileInfo(p1);
            if (info.Exists) {
                info.CopyTo(p2);
            }
        }


        public static void ReplaceFile(string from, string to) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return;
            }
            string p1 = gamePath + from;
            string p2 = gamePath + to;
            FileInfo info1 = new FileInfo(p1);
            FileInfo info2 = new FileInfo(p2);
            if (!info1.Exists) {
                return;
            }
            if (info2.Exists) {
                info1.Replace(p1, p2);
            } else {
                CopyFile(from, to);
            }
        }



        public static void CreateFile(string path) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return;
            }
            string p = gamePath + path;
            FileInfo info = new FileInfo(p);
            if (info.Exists) {
                return;
            }
            FileStream fs = info.Create();
            fs.Close();
        }


        public static void RemoveFile(string path) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return;
            }
            string p = gamePath + path;
            FileInfo info = new FileInfo(p);
            if (info.Exists) {
                info.Delete();
            }
        }


        public static void RemoveDirectory(string path) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return;
            }
            string p = gamePath + path;
            DirectoryInfo info = new DirectoryInfo(p);
            if (info.Exists) {
                info.Delete(true);
            }
        }


        public static string[] GetFileFullNames(string path) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return new string[0];
            }
            string p = gamePath + path;
            DirectoryInfo info = new DirectoryInfo(p);
            FileInfo[] files = info.GetFiles();
            string[] names = new string[files.Length];
            for (int i = 0; i < files.Length; i++) {
                names[i] = files[i].Name;
            }
            return names;
        }

        public static int GetFileCounts(string path) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return 1;
            }
            string p = gamePath + path;
            DirectoryInfo info = new DirectoryInfo(p);
            FileInfo[] files = info.GetFiles();
            return files.Length;
        }


        public static System.DateTime GetFileCreationTime(string path) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return System.DateTime.Now;
            }
            string p = gamePath + path;
            FileInfo info = new FileInfo(p);
            if (info.Exists) {
                return info.CreationTime;
            }
            return System.DateTime.Now;
        }



        public static System.DateTime GetFileLastWriteTime(string path) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return System.DateTime.Now;
            }
            string p = gamePath + path;
            FileInfo info = new FileInfo(p);
            if (info.Exists) {
                return info.LastWriteTime;
            }
            return System.DateTime.Now;
        }



        public static string[] GetFileNames(string path) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return new string[0];
            }
            string p = gamePath + path;
            DirectoryInfo info = new DirectoryInfo(p);
            FileInfo[] files = info.GetFiles();
            string[] names = new string[files.Length];
            for (int i = 0; i < files.Length; i++) {
                string name = files[i].Name;
                names[i] = name.Replace(".lwd", "");
            }
            return names;
        }


        public static string[] GetDirectoryNames(string path) {
            if (!initalized){
#if UNITY_EDITOR
                Debug.Log(InitializeError);
#endif
                return new string[0];
            }
            string p = gamePath + path;
            DirectoryInfo info = new DirectoryInfo(p);
            DirectoryInfo[] directories = info.GetDirectories();
            string[] names = new string[directories.Length];
            for (int i = 0; i < directories.Length; i++) {
                names[i] = directories[i].Name;
            }
            return names;            
        }
    }
}