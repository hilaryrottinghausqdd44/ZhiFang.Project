using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.BloodTransfusion.Common
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
                return true;
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
                return File.Exists(FilePath + @"\" + FileName);
            }
            catch
            {
                return false;
            }
        }

        public static bool CreatDirFile(string FilePath, string FileName, string context)
        {
            bool flag;
            try
            {
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                    flag = CreatDirFile(FilePath, FileName, context);
                }
                else
                {
                    try
                    {
                        StreamWriter writer = new StreamWriter(FilePath + @"\" + FileName, false, Encoding.GetEncoding("UTF-8"));
                        writer.Write(context);
                        writer.Close();
                        flag = true;
                    }
                    catch
                    {
                        flag = false;
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static bool DelDir(string Path)
        {
            bool flag;
            try
            {
                if (!Directory.Exists(Path))
                {
                    flag = true;
                }
                else
                {
                    try
                    {
                        Directory.Delete(Path);
                        flag = true;
                    }
                    catch
                    {
                        flag = false;
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static bool DelDirFile(string FilePath, string FileName)
        {
            bool flag;
            try
            {
                if (!File.Exists(FilePath + @"\" + FileName))
                {
                    flag = true;
                }
                else
                {
                    try
                    {
                        File.Delete(FilePath + @"\" + FileName);
                        flag = true;
                    }
                    catch
                    {
                        flag = false;
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
    }
}
