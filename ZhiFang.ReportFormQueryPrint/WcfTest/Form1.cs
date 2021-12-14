using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WcfTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportFormImageService.ReportFormImageServiceSoapClient rfiss = new ReportFormImageService.ReportFormImageServiceSoapClient();
            WcfTest.ReportFormImageService.ArrayOfString images;
            WcfTest.ReportFormImageService.ArrayOfBase64Binary a = rfiss.DownloadReportFormImageFile(this.textBox1.Text.Trim(), out images);
            for (int i = 0; i < a.Count; i++)
            {
                var tmpimage = a[i];
                FilesHelper.CreatDirFile(System.AppDomain.CurrentDomain.BaseDirectory + "tmp\\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "\\", i.ToString() + ".jpg", tmpimage);
                var aaa=base.Controls.Find("pictureBox"+(i+1).ToString(), true);
                ((PictureBox)aaa[0]).Load(System.AppDomain.CurrentDomain.BaseDirectory + "tmp\\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "\\0.jpg");
            }
            
             
        }
    }
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
        public static bool CreatDirFile(string FilePath, string FileName, Byte[] Filesteam)
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
                    return CreatDirFile(FilePath, FileName, context);
                }
                else
                {
                    try
                    {
                        StreamWriter fs = new StreamWriter(FilePath + "\\" + FileName, false, Encoding.GetEncoding("UTF-8"));
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
        /// <summary>
        /// 读取文件内容并转换为字符串
        /// </summary>
        /// <param name="FileName">文件名（包含其路径）</param>
        /// <returns>文件内容字符串</returns>
        public static string ReadFileContent(string FileName)
        {
            string tempFileContent = "";
            StringBuilder tempStringBuilder = new StringBuilder();
            try
            {
                if (File.Exists(FileName))
                {
                    StreamReader tempStreamReader = new StreamReader(FileName);
                    string tempLineStr = tempStreamReader.ReadLine();
                    while (tempLineStr != null)
                    {
                        if (tempLineStr.Trim().Length > 0)
                            tempStringBuilder.Append(tempLineStr + "\n");
                        tempLineStr = tempStreamReader.ReadLine();
                    }
                    if (tempStringBuilder.Length > 0)
                        tempFileContent = tempStringBuilder.ToString().TrimEnd('\n');
                }
            }
            catch (Exception ex)
            {
                tempFileContent = "";
                throw new Exception(ex.Message);
            }
            return tempFileContent;
        }
        /// <summary>
        /// 读取文件内容并转换为字符串
        /// </summary>
        /// <param name="FileName">文件流</param>
        /// <returns>文件内容字符串</returns>
        public static string ReadFileContent(Stream FileStream)
        {
            string tempFileContent = "";
            StringBuilder tempStringBuilder = new StringBuilder();
            try
            {
                if (FileStream.Length > 0)
                {
                    StreamReader tempStreamReader = new StreamReader(FileStream);
                    string tempLineStr = tempStreamReader.ReadLine();
                    while (tempLineStr != null)
                    {
                        if (tempLineStr.Trim().Length > 0)
                            tempStringBuilder.Append(tempLineStr + "\n");
                        tempLineStr = tempStreamReader.ReadLine();
                    }
                    if (tempStringBuilder.Length > 0)
                        tempFileContent = tempStringBuilder.ToString().TrimEnd('\n');
                }
            }
            catch (Exception ex)
            {
                tempFileContent = "";
                throw new Exception(ex.Message);
            }
            return tempFileContent;
        }

        /// <summary>
        /// 从本地文件读取所有的内容,如果出现异常，返回null
        /// </summary>
        /// <param name="file"></param>
        /// <returns>文件的内容byte[]</returns>
        public static byte[] readFromLocalFileGB2312(string file)
        {
            string text = "";
            Encoding utf1 = Encoding.GetEncoding("GB2312");
            try
            {
                if (!System.IO.File.Exists(file))
                    return null;

                System.IO.StreamReader fileReader = new System.IO.StreamReader(file, Encoding.Default);
                //取文件内容
                text = fileReader.ReadToEnd();
                fileReader.Close();
            }
            catch (System.Exception ex)
            {
                throw new Exception("读文件：" + file + "出错！\n" + ex.Message);
            }
            return utf1.GetBytes(text);
        }
        /// <summary>
        /// 从本地文件读取所有的内容,如果出现异常，返回null
        /// </summary>
        /// <param name="file"></param>
        /// <returns>文件的内容byte[]</returns>
        public static byte[] readFromLocalFileUTF8(string file)
        {
            string text = "";
            Encoding utf1 = Encoding.GetEncoding("UTF-8");
            try
            {
                if (!System.IO.File.Exists(file))
                    return null;

                System.IO.StreamReader fileReader = new System.IO.StreamReader(file, Encoding.UTF8);
                //取文件内容
                text = fileReader.ReadToEnd();
                fileReader.Close();
            }
            catch (System.Exception ex)
            {
                throw new Exception("读文件：" + file + "出错！\n" + ex.Message);
            }
            return utf1.GetBytes(text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] GetFileByte(string filePath)
        {
            byte[] resultBytes = null;
            if (File.Exists(filePath))
            {
                try
                {
                    FileStream tempFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    byte[] tempBytes = new byte[(int)tempFileStream.Length];
                    tempFileStream.Read(tempBytes, 0, tempBytes.Length);
                    resultBytes = tempBytes;
                    tempFileStream.Close();
                    tempFileStream.Dispose();
                }
                catch (System.Exception ex)
                {
                    throw new Exception("读文件：" + filePath + "出错！\n" + ex.Message);
                }
            }
            return resultBytes;
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