using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Common
{
    public class FilesHelper
    {
        public static bool CheckAndCreatDir(string Path)
        {
            try
            {
                if (!Directory.Exists(Path))
                {
                    try
                    {
                        Directory.CreateDirectory(Path);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool CreatDirFile(string FilePath, string FileName,　 Byte[] Filesteam)
        {
            try
            {
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                    return CreatDirFile(FilePath, FileName, Filesteam);
                }
                else
                {
                    try
                    {
                        FileStream fs = new FileStream(FilePath + "\\" + FileName, FileMode.OpenOrCreate);
                        fs.Write(Filesteam, 0, Filesteam.Count());
                        fs.Close();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// by spf 2010-08-24
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="FileName"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool CreatDirFile(string FilePath, string FileName, string context)
        {
            try
            {
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                    return CreatDirFile(FilePath, FileName, context );
                }
                else
                {
                    try
                    {
                        StreamWriter fs = new StreamWriter(FilePath + "\\" + FileName, false ,Encoding .GetEncoding ("UTF-8") );
                        fs.Write(context);
                       
                        fs.Close();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool CheckDirFile(string FilePath, string FileName)
        {
            try
            {
                return File.Exists(FilePath + "\\" + FileName);
            }
            catch
            {
                return false;
            }
        }
        public static bool CheckDirectory(string FilePath)
        {
            try
            {
                return Directory.Exists(FilePath);
            }
            catch
            {
                return false;
            }
        }
        public static bool DelDir(string Path)
        {
            try
            {
                if (!Directory.Exists(Path))
                {
                    return true;                    
                }
                else
                {
                    try
                    {
                        Directory.Delete(Path);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool DelDirFile(string FilePath, string FileName)
        {
            try
            {
                if (!File.Exists(FilePath + "\\" + FileName))
                {
                    return true;
                }
                else
                {
                    try
                    {
                        File.Delete(FilePath + "\\" + FileName);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public static void WriteContext(string Context, string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.Write(Context);
            sw.Close();
            sw.Dispose();
        }
        public static string ReadContext(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string context = sr.ReadToEnd();
            fs.Close();
            sr.Close();
            sr.Dispose();
            fs.Dispose();
            return context;
        }
    }
}
