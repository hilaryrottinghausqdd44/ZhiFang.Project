using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZhiFang.ReportFormQueryPrint.Common;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class ReportFormImageService : System.Web.Services.WebService
    {
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BRFGraphData brfgd = new BLL.BRFGraphData();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BPUser bpu = new BLL.BPUser();
        ///
        /// 上传文件
        ///
        /// 文件的byte[]
        /// 上传文件的路径
        /// 上传文件名字
        ///
        //[WebMethod]
        //public bool UploadFile(byte[] fs, string path, string fileName)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        //获取上传案例图片路径
        //        path = Server.MapPath(path);
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        //定义并实例化一个内存流，以存放提交上来的字节数组。
        //        MemoryStream m = new MemoryStream(fs);
        //        //定义实际文件对象，保存上载的文件。
        //        FileStream f = new FileStream(path + "\\" + fileName, FileMode.Create);
        //        //把内内存里的数据写入物理文件
        //        m.WriteTo(f);
        //        m.Close();
        //        f.Close();
        //        f = null;
        //        m = null;
        //        flag = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        flag = false;
        //    }
        //    return flag;
        //}

        [WebMethod(Description = "批量下载报告内结果图片")]
        public List<byte[]> DownloadReportFormImageFile(string ReportFormId, out List<string> Images)
        {
            Images = null;
            DataSet dsimages = brfgd.GetListByReportFormId(ReportFormId);
            if (dsimages != null && dsimages.Tables.Count > 0 && dsimages.Tables[0].Rows.Count > 0)
            {
                List<byte[]> a = new List<byte[]>();
                Images = new List<string>();
                for (int i = 0; i < dsimages.Tables[0].Rows.Count; i++)
                {
                    if (dsimages.Tables[0].Rows[i]["FilePath"] != null && dsimages.Tables[0].Rows[i]["FilePath"].ToString().Trim() != "")
                    {
                        FileStream fs = null;
                        string path = "";
                        string file = "";
                        string CurrentUploadFilePath = dsimages.Tables[0].Rows[i]["FilePath"].ToString().Trim();
                        if (File.Exists(CurrentUploadFilePath))
                        {
                            try
                            {
                                ///打开现有文件以进行读取。
                                fs = File.OpenRead(CurrentUploadFilePath);
                                int b1;
                                System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
                                while ((b1 = fs.ReadByte()) != -1)
                                {
                                    tempStream.WriteByte(((byte)b1));
                                }
                                a.Add(tempStream.ToArray());
                                Images.Add(dsimages.Tables[0].Rows[i]["GraphNo"].ToString().Trim() + ";" + dsimages.Tables[0].Rows[i]["GraphName"].ToString().Trim());
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Error("DownloadReportFormImageFile:" + ex.ToString());
                                a.Add(null);
                                Images.Add(dsimages.Tables[0].Rows[i]["GraphNo"].ToString().Trim() + ";" + dsimages.Tables[0].Rows[i]["GraphName"].ToString().Trim());
                            }
                            finally
                            {
                                fs.Close();
                            }
                        }
                        else
                        {
                            a[i] = null;
                            Images.Add(dsimages.Tables[0].Rows[i]["GraphNo"].ToString().Trim() + ";" + dsimages.Tables[0].Rows[i]["GraphName"].ToString().Trim());
                            ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile:未找到图片文件（" + CurrentUploadFilePath + ")");
                        }
                    }
                }
                return a;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile:未找到报告图片列表ReportFormId：" + ReportFormId);
                return null;
            }
        }

        [WebMethod(Description = "单个下载报告内结果图片")]
        public byte[] DownloadReportFormImageFileSingle(string ReportFormId, string Imagename)
        {
            DataSet dsimages = brfgd.GetListByReportFormId(ReportFormId);
            if (dsimages != null && dsimages.Tables.Count > 0 && dsimages.Tables[0].Rows.Count > 0)
            {
                if (dsimages.Tables[0].Rows[0]["FilePath"] != null && dsimages.Tables[0].Rows[0]["FilePath"].ToString().Trim() != "")
                {
                    FileStream fs = null;
                    string path = "";
                    string file = "";
                    string CurrentUploadFilePath = dsimages.Tables[0].Rows[0]["FilePath"].ToString().Trim();
                    if (File.Exists(CurrentUploadFilePath))
                    {
                        try
                        {
                            ///打开现有文件以进行读取。
                            fs = File.OpenRead(CurrentUploadFilePath);
                            int b1;
                            System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
                            while ((b1 = fs.ReadByte()) != -1)
                            {
                                tempStream.WriteByte(((byte)b1));
                            }
                            return tempStream.ToArray();
                        }
                        catch (Exception e)
                        {
                            ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFileSingle:异常（" + e.ToString() + ")");
                            return null;
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFileSingle:未找到图片文件（" + CurrentUploadFilePath + ")");
                        return null;

                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFileSingle:未找到图片文件字段");
                    return null;
                }

            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFileSingle:未找到报告图片列表ReportFormId：" + ReportFormId);
                return null;
            }
        }

        [WebMethod(Description = "批量下载报告内结果图片Base64字符串方式")]
        public List<string> DownloadReportFormImageFile_Base64String(string ReportFormId, out List<string> Images)
        {
            ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile_Base64String:调用开始！ReportFormId：" + ReportFormId);
            Images = null;
            // DataSet dsimages = brfgd.GetListByReportFormId(ReportFormId);
            DataSet dsimages = brfgd.GetListByReportPublicationID(ReportFormId);
            if (dsimages != null && dsimages.Tables.Count > 0 && dsimages.Tables[0].Rows.Count > 0)
            {
                ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile_Base64String:调用开始！ReportFormId：" + ReportFormId);
                List<string> a = new List<string>();
                Images = new List<string>();
                for (int i = 0; i < dsimages.Tables[0].Rows.Count; i++)
                {
                    if (dsimages.Tables[0].Rows[i]["FilePath"] != null && dsimages.Tables[0].Rows[i]["FilePath"].ToString().Trim() != "")
                    {
                        string CurrentUploadFilePath = dsimages.Tables[0].Rows[i]["FilePath"].ToString().Trim();
                        if (File.Exists(CurrentUploadFilePath))
                        {
                            try
                            {
                                ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile_Base64String:FilePath:" + dsimages.Tables[0].Rows[i]["FilePath"].ToString().Trim() + ";GraphNo:" + dsimages.Tables[0].Rows[i]["GraphNo"].ToString().Trim() + ";GraphName:" + dsimages.Tables[0].Rows[i]["GraphName"].ToString().Trim());

                                a.Add(Base64Help.EncodingFileToString(dsimages.Tables[0].Rows[i]["FilePath"].ToString().Trim()));
                                Images.Add(dsimages.Tables[0].Rows[i]["GraphNo"].ToString().Trim() + ";" + dsimages.Tables[0].Rows[i]["GraphName"].ToString().Trim());
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Error("DownloadReportFormImageFile_Base64String:" + ex.ToString());
                                a.Add(null);
                                Images.Add(dsimages.Tables[0].Rows[i]["GraphNo"].ToString().Trim() + ";" + dsimages.Tables[0].Rows[i]["GraphName"].ToString().Trim());
                            }
                        }
                        else
                        {
                            a.Add(null);
                            Images.Add(dsimages.Tables[0].Rows[i]["GraphNo"].ToString().Trim() + ";" + dsimages.Tables[0].Rows[i]["GraphName"].ToString().Trim());
                            ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile_Base64String:未找到图片文件（" + CurrentUploadFilePath + ")");
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile_Base64String:调用开始！ReportFormId：" + ReportFormId+ ",PUser.UserNo=" + dsimages.Tables[0].Rows[i]["UserNo"].ToString() + ",FilePath为空！");
                    }
                }
                return a;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile_Base64String:未找到报告图片列表ReportFormId：" + ReportFormId);
                return null;
            }
        }

        [WebMethod(Description = "批量下载电子签名图片Base64字符串方式")]
        public List<string> DownloadPUserImage_Base64String(List<string> puseridlist, out List<string> pusernoindex)
        {
            ZhiFang.Common.Log.Log.Debug("DownloadPUserImage_Base64String:调用开始！puseridlist：" + string.Join(",", puseridlist.ToArray()));
            pusernoindex = null;
            if (puseridlist != null && puseridlist.Count > 0)
            {
                DataSet dspuserimages = bpu.GetListByPUserIdList(puseridlist.ToArray());
                pusernoindex = new List<string>();
                if (dspuserimages != null && dspuserimages.Tables.Count > 0 && dspuserimages.Tables[0].Rows.Count > 0)
                {
                    List<string> a = new List<string>();
                    for (int i = 0; i < dspuserimages.Tables[0].Rows.Count; i++)
                    {
                        if (dspuserimages.Tables[0].Rows[i]["FilePath"] != null && dspuserimages.Tables[0].Rows[i]["FilePath"].ToString().Trim() != "")
                        {
                            string CurrentUploadFilePath = dspuserimages.Tables[0].Rows[i]["FilePath"].ToString().Trim();
                            if (File.Exists(CurrentUploadFilePath))
                            {
                                try
                                {
                                    a.Add(Base64Help.EncodingFileToString(dspuserimages.Tables[0].Rows[i]["FilePath"].ToString().Trim()));
                                    pusernoindex.Add(dspuserimages.Tables[0].Rows[i]["UserNo"].ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    ZhiFang.Common.Log.Log.Error("DownloadReportFormImageFile:" + ex.ToString());
                                    a.Add(null);
                                    pusernoindex.Add(dspuserimages.Tables[0].Rows[i]["UserNo"].ToString().Trim());
                                }
                            }
                            else
                            {
                                a[i] = null;
                                pusernoindex.Add(dspuserimages.Tables[0].Rows[i]["UserNo"].ToString().Trim());
                                ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile:未找到图片文件（" + CurrentUploadFilePath + ")");
                            }
                        }
                    }
                    return a;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile:未找到用户");
                    return null;
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile:参数为空或参数长度为0");
                return null;
            }
        }

        [WebMethod(Description = "批量下载电子签名图片Base64字符串方式")]
        public List<string> DownloadPUserImage_Base64String_StringPara(string puseridlist, out List<string> pusernoindex)
        {

            pusernoindex = null;
            try
            {
                if (puseridlist != null && puseridlist.Trim().Length > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("DownloadPUserImage_Base64String_StringPara,puseridlist:" + string.Join(",", puseridlist));
                    //DataSet dspuserimages = bpu.GetListByPUserIdList(puseridlist);
                    DataSet dspuserimages = bpu.GetListByPUserIdList(puseridlist);
                    pusernoindex = new List<string>();
                    if (dspuserimages != null && dspuserimages.Tables.Count > 0 && dspuserimages.Tables[0].Rows.Count > 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("DownloadPUserImage_Base64String_StringPara,1.");
                        List<string> a = new List<string>();
                        for (int i = 0; i < dspuserimages.Tables[0].Rows.Count; i++)
                        {
                            if (dspuserimages.Tables[0].Rows[i]["FilePath"] != null && dspuserimages.Tables[0].Rows[i]["FilePath"].ToString().Trim() != "")
                            {
                                ZhiFang.Common.Log.Log.Debug("DownloadPUserImage_Base64String_StringPara,2.");
                                string CurrentUploadFilePath = dspuserimages.Tables[0].Rows[i]["FilePath"].ToString().Trim();
                                if (File.Exists(CurrentUploadFilePath))
                                {
                                    ZhiFang.Common.Log.Log.Debug("DownloadPUserImage_Base64String_StringPara,3.");
                                    try
                                    {
                                        a.Add(Base64Help.EncodingFileToString(dspuserimages.Tables[0].Rows[i]["FilePath"].ToString().Trim()));
                                        pusernoindex.Add(dspuserimages.Tables[0].Rows[i]["UserNo"].ToString().Trim());
                                        ZhiFang.Common.Log.Log.Debug("DownloadPUserImage_Base64String_StringPara,4.");
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("DownloadPUserImage_Base64String_StringPara,5.");
                                        ZhiFang.Common.Log.Log.Error("DownloadReportFormImageFile.Add:" + ex.ToString());
                                        a.Add(null);
                                        pusernoindex.Add(dspuserimages.Tables[0].Rows[i]["UserNo"].ToString().Trim());
                                    }
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug("DownloadPUserImage_Base64String_StringPara,6.");
                                    a[i] = null;
                                    pusernoindex.Add(dspuserimages.Tables[0].Rows[i]["UserNo"].ToString().Trim());
                                    ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile:未找到图片文件（" + CurrentUploadFilePath + ")");
                                }
                            }
                            ZhiFang.Common.Log.Log.Debug("DownloadPUserImage_Base64String_StringPara,7.");
                        }
                        return a;
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("DownloadPUserImage_Base64String_StringPara:未找到用户");
                        return null;
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("DownloadPUserImage_Base64String_StringPara:参数为空或参数长度为0");
                    return null;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("DownloadPUserImage_Base64String_StringPara:" + e.ToString());
                return null;
            }
        }

        [WebMethod(Description = "文件接收服务")]
        public bool UploadFile(string filename, string path, byte[] filestearm, out string error)
        {
            error = "";
            if (filename == null || filename.Trim().Length <= 0)
            {
                error = "filename文件名为空！";
                return false;
            }
            if (path == null || path.Trim().Length < 0)
            {
                error = "path文件路径为空！";
                return false;
            }
            if (filestearm == null || filestearm.Length <= 0)
            {
                error = "filestearm文件流为空！";
                return false;
            }
            ZhiFang.Common.Log.Log.Debug("UploadFile:filename=" + filename + ";path=" + path + ";filestearm.Length=" + filestearm.Length + ".");
            try
            {
                ZhiFang.ReportFormQueryPrint.Common.FilesHelper.CreatDirFile(Server.MapPath("~/") + "\\" + path, filename, filestearm);
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("UploadFile异常:" + e.ToString());
                error = "UploadFile异常:" + e.ToString();
                return false;
            }
        }

        [WebMethod(Description = "文件接收服务_绝对路径")]
        public bool UploadFile_Absolute(string filename, string path, byte[] filestearm, out string error)
        {
            error = "";
            if (filename == null || filename.Trim().Length <= 0)
            {
                error = "filename文件名为空！";
                return false;
            }
            if (path == null || path.Trim().Length < 0)
            {
                error = "path文件路径为空！";
                return false;
            }
            if (filestearm == null || filestearm.Length <= 0)
            {
                error = "filestearm文件流为空！";
                return false;
            }
            ZhiFang.Common.Log.Log.Debug("UploadFile:filename=" + filename + ";path=" + path + ";filestearm.Length=" + filestearm.Length + ".");
            try
            {
                ZhiFang.ReportFormQueryPrint.Common.FilesHelper.CreatDirFile(path, filename, filestearm);
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("UploadFile异常:" + e.ToString());
                error = "UploadFile异常:" + e.ToString();
                return false;
            }
        }
    }
}
