using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ZhiFang.Common.Public;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Text;
using System.Threading;
using ZhiFang.WebLis.Class;

namespace ZhiFang.WebLis.ExportExcel
{
    public partial class ExportIntoReport : BasePage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.ExportExcel.ExportIntoReport));
        }
        protected void butReadXml_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("xmlPath") + "/" + "未导入");
                DirectoryInfo[] dirs = dir.GetDirectories();
                DataTable dt = new DataTable();
                FileInfo[] files = null;
                string folder_Names = "";
                List<string> xmlList = new List<string>();
                if (dirs != null)
                {
                    DataColumn dc = new DataColumn();
                    dt.Columns.Add("fileName", typeof(string));
                    DataColumn dc1 = new DataColumn();
                    dt.Columns.Add("IsImage", typeof(string));
                    DataColumn dc2 = new DataColumn();
                    dt.Columns.Add("ImageCount", typeof(string));
                    DataColumn dc3 = new DataColumn();
                    dt.Columns.Add("IsExport", typeof(string));
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        folder_Names = subdir.FullName;
                        DirectoryInfo dirXml = new DirectoryInfo(folder_Names);
                        FileInfo[] filesInDir = dirXml.GetFiles();
                        if (filesInDir != null)
                        {
                            foreach (FileInfo fileIn in filesInDir)
                            {
                                xmlList.Add(fileIn.Name);
                            }
                            if (xmlList != null)
                            {
                             
                                for (var i = 0; i < xmlList.Count; i++)
                                {
                                    DataRow row = dt.NewRow();
                                    row["fileName"] = xmlList[i];
                                    string formNo = xmlList[i].Split('.')[0].ToString();
                                    if (System.IO.Directory.Exists(folder_Names + "/" + formNo) == true)
                                    {
                                        row["IsImage"] = "是";
                                        DirectoryInfo dirImage1 = new DirectoryInfo(folder_Names + "/" + formNo);
                                        files = dirImage1.GetFiles();
                                        row["ImageCount"] = files.Length.ToString();
                                    }
                                    else
                                    {
                                        row["IsImage"] = "否";
                                        row["ImageCount"] = "0";
                                    }
                                    row["IsExport"] = "未导入";
                                    dt.Rows.Add(row);

                                }
                                
                            }
                        }
                      
                    }
                }
                DataList1.DataSource = dt;
                DataList1.DataBind();
                if (xmlList.Count == 0)
                {
                    //Response.Write("<script>alert('没有需要导入的文件！');</script>");
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error(ex.ToString());
            }
        }
        //private void beginProgress()
        //{
        //    //根据ProgressBar.htm显示进度条界面
        //    string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
        //    StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("GB2312"));
        //    string html = reader.ReadToEnd();
        //    reader.Close();
        //    Response.Write(html);
        //    Response.Flush();
        //}
        //private void setProgress(int percent)
        //{
        //    string jsBlock = "<script>SetPorgressBar('" + percent.ToString() + "'); </script>";
        //    Response.Write(jsBlock);
        //    Response.Flush();
         
        //}
        //private void finishProgress()
        //{
        //    string jsBlock = "<script>SetCompleted();</script>";
        //    Response.Write(jsBlock);
        //    Response.Flush();
        //    Response.Close();
        //}
        protected void butInXml_Click(object sender, EventArgs e)
        {
            try
            {
                //Page.RegisterStartupScript("aaa", " <script>showFloat();</script>");
                DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("xmlPath") + "/" + "未导入");
                DirectoryInfo[] dirs = dir.GetDirectories();
                DataTable dt = new DataTable();
                string folder_Names = "";
                int h = -1;
                List<string> xmlList = new List<string>();
                List<string> xmlList1 = new List<string>();
                FileInfo[] filesInDir = null;
                List<byte[]> bb = new List<byte[]>();
                List<string> aa = new List<string>();
                string[] a = null;
                byte[][] b = null;
                XmlDocument xd = new XmlDocument();
                if (dirs != null)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        folder_Names = subdir.FullName;
                        DirectoryInfo dirXml = new DirectoryInfo(folder_Names);
                        filesInDir = dirXml.GetFiles();
                        if (filesInDir != null)
                        {
                            foreach (FileInfo fileIn in filesInDir)
                            {
                                xmlList1.Add(fileIn.FullName);
                                xmlList.Add(fileIn.Name);
                            }
                        }
                      
                      
                    }

                    if (xmlList != null)
                    {
                        byte[] bytes = null;
                        //this.myframe.Attributes["src"] = "ProgerssBar.aspx?Count=" + xmlList.Count;
                        for (var i = 0; i < xmlList.Count; i++)
                        {
                            string xmlpath = xmlList1[i];
                            xd.Load(@xmlpath);
                            bytes = Encoding.UTF8.GetBytes(xd.InnerXml);
                            string formNo = xmlList[i].Split('.')[0].ToString();
                            if (System.IO.Directory.Exists(folder_Names + "/" + formNo) == true)
                            {
                                DirectoryInfo dirImage1 = new DirectoryInfo(folder_Names + "/" + formNo);
                                FileInfo[] files = dirImage1.GetFiles();
                                foreach (FileInfo fileIn in files)
                                {
                                    string filepath = fileIn.FullName;
                                    FileStream fileStr = new FileStream(filepath, FileMode.Open,FileAccess.Read);
                                    byte[] imgByte = new byte[fileStr.Length];
                                    BinaryReader br = new BinaryReader(fileStr);
                                    imgByte = br.ReadBytes(Convert.ToInt32(fileStr.Length));//
                                    string fileName = fileIn.Name.Replace('&', ':').ToString();
                                    //fileStr.Read(imgByte, 0, imgByte.Length);
                                    fileStr.Close();
                                    bb.Add(imgByte);
                                    aa.Add(fileName);
                                }
                                a = new string[aa.Count];
                                b = new byte[bb.Count][];
                                for (int j = 0; j < a.Length; j++)
                                {
                                    a[j] = aa[j];
                                    b[j] = bb[j];
                                }
                            }
                            ReportUploadService.WebLisReport weblisReport = new ReportUploadService.WebLisReport();
                            string outError = "";

                            //setProgress(i);
                            //此处用线程休眠代替实际的操作，如加载数据等
                            // System.Threading.Thread.Sleep(2000);
                            h = weblisReport.UpLoadReportFromBytes_ImageList(null, bytes, null, null, null, a, b, null, out outError);
                            if (h == 0)
                            {
                                string sourceFile = xmlList1[i];
                                FileInfo fi = new FileInfo(sourceFile);
                                if (FilesHelper.CheckAndCreatDir(xmlList1[i].Replace("未", "已").Replace(xmlList[i],"")))
                                {
                                    string objectFile = xmlList1[i].Replace("未", "已");
                                    if (System.IO.File.Exists(sourceFile))
                                    {
                                        fi.CopyTo(objectFile, true);
                                        FilesHelper.DelDirFile(xmlList1[i]. Replace(xmlList[i], ""), xmlList[i]);
                                    }
                                }
                                for (var m = 0; m < DataList1.Items.Count; m++)
                                {
                                    if (xmlList[i] == ((Label)DataList1.Items[m].FindControl("fileName")).Text)
                                    {
                                        ((Label)DataList1.Items[m].FindControl("IsExport")).Text = "导入成功";
                                    }
                                }
                            }
                            else
                            {
                                Response.Write("<script type='text/javascript'>alert('导入失败！');</script>");
                                for (var m = 0; m < DataList1.Items.Count; m++)
                                {
                                    if (xmlList[i] == ((Label)DataList1.Items[m].FindControl("fileName")).Text)
                                    {
                                        ((Label)DataList1.Items[m].FindControl("IsExport")).Text = "导入失败";
                                    }
                                }
                            }
                        }
                        //finishProgress();

                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error(ex.ToString());
            }
        }
        //[AjaxPro.AjaxMethod()]
        public int InXml() 
        {
            try
            {
                //beginProgress();
                DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("xmlPath") + "/" + "未导入");
                DirectoryInfo[] dirs = dir.GetDirectories();
                DataTable dt = new DataTable();
                string folder_Names = "";
                int h = -1;
                List<string> xmlList = new List<string>();
                if (dirs != null)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        folder_Names = subdir.FullName;
                        DirectoryInfo dirXml = new DirectoryInfo(folder_Names);
                        FileInfo[] filesInDir = dirXml.GetFiles();
                        List<byte[]> bb = new List<byte[]>();
                        List<string> aa = new List<string>();
                        string[] a = null;
                        byte[][] b = null;
                        if (filesInDir != null)
                        {
                            foreach (FileInfo fileIn in filesInDir)
                            {
                                xmlList.Add(fileIn.Name);
                            }
                            if (xmlList != null)
                            {
                                byte[] bytes = null;
                                for (var i = 0; i < xmlList.Count; i++)
                                {
                                    XmlDocument xd = new XmlDocument();
                                    string xmlpath = folder_Names + "/" + xmlList[i];
                                    xd.Load(@xmlpath);
                                    bytes = Encoding.UTF8.GetBytes(xd.InnerXml);
                                    string formNo = xmlList[i].Split('.')[0].ToString();
                                    if (System.IO.Directory.Exists(folder_Names + "/" + formNo) == true)
                                    {
                                        DirectoryInfo dirImage1 = new DirectoryInfo(folder_Names + "/" + formNo);
                                        FileInfo[] files = dirImage1.GetFiles();
                                        foreach (FileInfo fileIn in files)
                                        {
                                            string filepath = fileIn.FullName;
                                            FileStream fileStr = new FileStream(filepath, FileMode.Open);
                                            string fileName = fileIn.Name.Replace('&', ':').ToString();
                                            byte[] imgByte = new byte[files.Length];
                                            fileStr.Read(imgByte, 0, imgByte.Length);
                                            fileStr.Close();
                                            bb.Add(imgByte);
                                            aa.Add(fileName);
                                        }
                                        a = new string[aa.Count];
                                        b = new byte[bb.Count][];
                                        for (int j = 0; j < a.Length; j++)
                                        {
                                            a[j] = aa[j];
                                            b[j] = bb[j];
                                        }
                                    }

                                    ReportUploadService.WebLisReport weblisReport = new ReportUploadService.WebLisReport();
                                    string outError = "";
                                    //setProgress(i);
                                    //此处用线程休眠代替实际的操作，如加载数据等
                                    //System.Threading.Thread.Sleep(2000);
                                    h = weblisReport.UpLoadReportFromBytes_ImageList(null, bytes, null, null, null, a, b, null, out outError);
                                    if (h == 0)
                                    {
                                        string sourceFile = folder_Names + "/" + xmlList[i];
                                        FileInfo fi = new FileInfo(sourceFile);
                                        if (FilesHelper.CheckAndCreatDir(folder_Names.Replace("未", "已")))
                                        {
                                            string objectFile = folder_Names.Replace("未", "已") + "/" + xmlList[i];
                                            if (System.IO.File.Exists(sourceFile))
                                            {
                                                fi.CopyTo(objectFile, true);
                                                FilesHelper.DelDirFile(folder_Names, xmlList[i]);
                                            }
                                        }
                                        for (var m = 0; m < DataList1.Items.Count; m++)
                                        {
                                            if (xmlList[i] == ((Label)DataList1.Items[m].FindControl("fileName")).Text)
                                            {
                                                ((Label)DataList1.Items[m].FindControl("IsExport")).Text = "导入成功";
                                            }
                                        }
                                        //((Label)DataList1.Items[0].FindControl("zhhao")).Text 
                                        //string strw = DataList1.Items;
                                    }
                                    else
                                    {
                                        Response.Write("<script type='text/javascript'>alert('导入失败！');</script>");
                                        for (var m = 0; m < DataList1.Items.Count; m++)
                                        {
                                            if (xmlList[i] == ((Label)DataList1.Items[m].FindControl("fileName")).Text)
                                            {
                                                ((Label)DataList1.Items[m].FindControl("IsExport")).Text = "导入失败";
                                            }
                                        }
                                    }
                                }
                               // finishProgress();
                               
                            }
                        }
                    }
                }
                return h;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error(ex.ToString());
                return -1;
            }
        }
    }
} 