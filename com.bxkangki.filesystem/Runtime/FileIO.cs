using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    public class FileIO
    {

        public static void WriteJsonData<T>(string path, T data, bool create = true)
        {
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            try
            {
                if (File.Exists(path))
                {
                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        writer.Write(JsonUtility.ToJson(data, true));
                        writer.Close();
                    }
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
        }

        public static void WriteJsonDataToBase64<T>(string path, T data, bool create = true)
        {
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            try
            {
                if (File.Exists(path))
                {
                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        string json = JsonUtility.ToJson(data, true);
                        byte[] bytes = Encoding.UTF8.GetBytes(json);
                        writer.Write(System.Convert.ToBase64String(bytes));
                        writer.Close();
                    }
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
        }


        public static T ReadJsonData<T>(string path, bool create = true)
        {
            string result = "";
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        result = reader.ReadToEnd();
                        reader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
            return JsonUtility.FromJson<T>(result);
        }


        public static T ReadJsonDataFromBase64<T>(string path, bool create = true)
        {
            string result = "";
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        result = reader.ReadToEnd();
                        byte[] bytes = System.Convert.FromBase64String(result);
                        result = Encoding.UTF8.GetString(bytes);
                        reader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
            return JsonUtility.FromJson<T>(result);
        }

        public static async void WriteJsonDataAsync<T>(string path, T data, bool create = true)
        {
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            try
            {
                if (File.Exists(path))
                {
                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        await writer.WriteAsync(JsonUtility.ToJson(data, true));
                        writer.Close();
                    }
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
        }

        public static async void WriteJsonDataToBase64Async<T>(string path, T data, bool create = true)
        {
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            try
            {
                if (File.Exists(path))
                {
                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        string json = JsonUtility.ToJson(data, true);
                        byte[] bytes = Encoding.UTF8.GetBytes(json);
                        await writer.WriteAsync(System.Convert.ToBase64String(bytes));
                        writer.Close();
                    }
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
        }


        public static async Task<T> ReadJsonDataAsync<T>(string path, bool create = true)
        {
            string result = null;
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        result = await reader.ReadToEndAsync();
                        reader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
            return JsonUtility.FromJson<T>(result);
        }


        public static async Task<T> ReadJsonDataFromBase64Async<T>(string path, bool create = true)
        {
            string result = "";
            // chekc file already include command to merge path and gamePath itself,
            // so we should run check file first then merge file path and path below.
            CheckFile(path, create);
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        result = await reader.ReadToEndAsync();
                        byte[] bytes = System.Convert.FromBase64String(result);
                        result = Encoding.UTF8.GetString(bytes);
                        reader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
            return JsonUtility.FromJson<T>(result);
        }



        public static async void WriteLineTextFile(string path, string data)
        {
            try
            {
                if (File.Exists(path))
                {
                    var fi = new FileInfo(path);
                    using (StreamWriter sw = fi.AppendText())
                    {
                        await sw.WriteLineAsync(data);
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
        }


        public static bool CheckDirectory(string path, bool create = false)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(path);
                if (!info.Exists && create)
                {
                    CreateDirectory(path);
                }
                return info.Exists;
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
                return false;
            }
        }

        public static byte[] ReadAllBytes(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
                return null;
            }
        }


        public static void CreateDirectory(string path)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(path);
                if (info.Exists)
                {
                    return;
                }
                info.Create();
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
        }


        public static bool CheckFile(string path, bool create = false)
        {
            try
            {
                FileInfo info = new FileInfo(path);
                if (!info.Exists && create)
                {
                    CreateFile(path);
                }
                return info.Exists;
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
                return false;
            }
        }


        public static void CopyFile(string from, string to)
        {
            try
            {
                FileInfo info = new FileInfo(from);
                if (info.Exists)
                {
                    info.CopyTo(to);
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
        }


        public static void ReplaceFile(string from, string to)
        {
            try
            {
                FileInfo info1 = new FileInfo(from);
                FileInfo info2 = new FileInfo(to);
                if (!info1.Exists)
                {
                    return;
                }
                if (info2.Exists)
                {
                    info1.Replace(from, to);
                }
                else
                {
                    CopyFile(from, to);
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
        }



        public static void CreateFile(string path)
        {
            try
            {
                FileInfo info = new FileInfo(path);
                if (info.Exists)
                {
                    return;
                }
                using (FileStream fs = info.Create())
                {
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
        }


        public static void RemoveFile(string path)
        {
            try
            {
                FileInfo info = new FileInfo(path);
                if (info.Exists)
                {
                    info.Delete();
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
        }


        public static void RemoveDirectory(string path)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(path);
                if (info.Exists)
                {
                    info.Delete(true);
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
        }


        public static string[] GetFileFullNames(string path)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(path);
                FileInfo[] files = info.GetFiles();
                string[] names = new string[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    names[i] = files[i].Name;
                }
                return names;
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
                return null;
            }
        }

        public static int GetFileCounts(string path)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(path);
                FileInfo[] files = info.GetFiles();
                return files.Length;
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
                return 0;
            }
        }


        public static System.DateTime GetFileCreationTime(string path)
        {
            try
            {
                FileInfo info = new FileInfo(path);
                if (info.Exists)
                {
                    return info.CreationTime;
                }

            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
            return System.DateTime.Now;
        }



        public static System.DateTime GetFileLastWriteTime(string path)
        {
            try
            {
                FileInfo info = new FileInfo(path);
                if (info.Exists)
                {
                    return info.LastWriteTime;
                }
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
            }
            return System.DateTime.Now;
        }



        public static string[] GetFileNames(string path, string extenstion)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(path);
                FileInfo[] files = info.GetFiles();
                string[] names = new string[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    string name = files[i].Name;
                    names[i] = name.Replace(extenstion, "");
                }
                return names;
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
                return null;
            }
        }


        public static string[] GetDirectoryNames(string path)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(path);
                DirectoryInfo[] directories = info.GetDirectories();
                string[] names = new string[directories.Length];
                for (int i = 0; i < directories.Length; i++)
                {
                    names[i] = directories[i].Name;
                }
                return names;
            }
            catch (Exception e)
            {
                LogSystem.Text(e.ToString());
                return null;
            }
        }
    }
}