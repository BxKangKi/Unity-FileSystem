using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;

namespace FileSystem {
    public class FileIO {
        public static void WriteJsonData<T>(string path, T data, bool create = true) {
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
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
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            if (File.Exists(path)) {
                string json = JsonUtility.ToJson(data, true);
                byte[] bytes = System.Text.Encoding.Default.GetBytes(json);
                File.WriteAllBytes(path, bytes);
            }
        }

        public static T ReadJsonData<T>(string path, bool create = true) {
            string result = "";
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
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
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            if (File.Exists(path)) {
                byte[] bytes = File.ReadAllBytes(path);
                result = System.Text.Encoding.UTF8.GetString(bytes);
            }
            return JsonUtility.FromJson<T>(result);
        }


        public static async void WriteLineTextFile(string path, string data) {
            if (File.Exists(path)) {
                var fi = new FileInfo(path);
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
            DirectoryInfo info = new DirectoryInfo(path);
            if (!info.Exists && create) {
                CreateDirectory(path);
            }
            return info.Exists;
        }

        public static byte[] ReadAllBytes(string path) {
            return File.ReadAllBytes(path);
        }


        public static void CreateDirectory(string path) {
            DirectoryInfo info = new DirectoryInfo(path);
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
            FileInfo info = new FileInfo(path);
            if (!info.Exists && create) {
                CreateFile(path);
            }
            return info.Exists;
        }


        public static void CopyFile(string from, string to) {
            FileInfo info = new FileInfo(from);
            if (info.Exists) {
                info.CopyTo(to);
            }
        }


        public static void ReplaceFile(string from, string to) {
            FileInfo info1 = new FileInfo(from);
            FileInfo info2 = new FileInfo(to);
            if (!info1.Exists) {
                return;
            }
            if (info2.Exists) {
                info1.Replace(from, to);
            } else {
                CopyFile(from, to);
            }
        }



        public static void CreateFile(string path) {
            FileInfo info = new FileInfo(path);
            if (info.Exists) {
                return;
            }
            FileStream fs = info.Create();
            fs.Close();
        }


        public static void RemoveFile(string path) {
            FileInfo info = new FileInfo(path);
            if (info.Exists) {
                info.Delete();
            }
        }


        public static void RemoveDirectory(string path) {
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists) {
                info.Delete(true);
            }
        }


        public static string[] GetFileFullNames(string path) {
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles();
            string[] names = new string[files.Length];
            for (int i = 0; i < files.Length; i++) {
                names[i] = files[i].Name;
            }
            return names;
        }

        public static int GetFileCounts(string path) {
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles();
            return files.Length;
        }


        public static System.DateTime GetFileCreationTime(string path) {
            FileInfo info = new FileInfo(path);
            if (info.Exists) {
                return info.CreationTime;
            }
            return System.DateTime.Now;
        }



        public static System.DateTime GetFileLastWriteTime(string path) {
            FileInfo info = new FileInfo(path);
            if (info.Exists) {
                return info.LastWriteTime;
            }
            return System.DateTime.Now;
        }



        public static string[] GetFileNames(string path, string extenstion) {
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles();
            string[] names = new string[files.Length];
            for (int i = 0; i < files.Length; i++) {
                string name = files[i].Name;
                names[i] = name.Replace(extenstion, "");
            }
            return names;
        }


        public static string[] GetDirectoryNames(string path) {
            DirectoryInfo info = new DirectoryInfo(path);
            DirectoryInfo[] directories = info.GetDirectories();
            string[] names = new string[directories.Length];
            for (int i = 0; i < directories.Length; i++) {
                names[i] = directories[i].Name;
            }
            return names;            
        }
    }
}