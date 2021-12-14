using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using System.Web;
using ZhiFang.Entity.RBAC;
using System.IO;
using ZhiFang.IBLL.ProjectProgressMonitorManage;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPProjectAttachment : BaseBLL<PProjectAttachment>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPProjectAttachment
    {
        public IBBParameter IBBParameter { get; set; }
        public BaseResultDataValue AddAttachment(long id, ZhiFang.Entity.ProjectProgressMonitorManage.PProjectAttachment fattachment, HttpPostedFile file, HREmployee hremployee)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (id > 0)
            {
                PProjectAttachment tempFAttachment = this.Get(id);
                tempFAttachment.IsUse = false;
                this.Entity = tempFAttachment;
                this.Edit();
            }
            this.Entity = fattachment;
            if (file != null)
            {
                int len = file.ContentLength;
                if (len > 0)
                {
                    fattachment.FileName = this.Entity.Id.ToString() + "_" + file.FileName;
                    string basePath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("AttachmentPath").Trim();
                    string relativePath = _getDatePath() + fattachment.FileName;//相对路径
                    string directoryPath = Path.Combine(basePath, _getDatePath());
                    string filepath = Path.Combine(basePath, relativePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    file.SaveAs(filepath);
                    fattachment.FileSize = len;
                    fattachment.FilePath = relativePath;
                    fattachment.FileExt = Path.GetExtension(filepath);
                    fattachment.CreatorID = hremployee.Id;
                    fattachment.CreatorName = hremployee.CName;
                    if (this.Add())
                        baseResultDataValue.ResultDataValue = this.Entity.Id.ToString();
                }
                else
                {
                    baseResultDataValue.ErrorInfo = "文件大小为0或为空！";
                    baseResultDataValue.ResultDataValue = "";
                    baseResultDataValue.success = false;
                }

            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditFileContent(string filePath, string fileContent)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            FileStream fs = null;
            try
            {
                if (File.Exists(filePath))
                    fs = new FileStream(filePath, FileMode.Truncate, FileAccess.ReadWrite);
                else
                    fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);

                byte[] data = new UTF8Encoding().GetBytes(fileContent);//获得字节数组

                fs.Write(data, 0, data.Length);//开始写入

                fs.Flush(); //清空缓冲区、关闭流
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "打开文件失败！Error：" + ex.Message;
            }
            finally
            {
                fs.Close();
            }
            return baseResultDataValue;
        }

        public string GetAttachmentFilePath(long attachmentID)
        {
            string filePath = "";
            if (attachmentID > 0)
            {
                string basePath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("AttachmentPath").Trim();
                PProjectAttachment attachment = this.Get(attachmentID);
                if (attachment != null && (!string.IsNullOrEmpty(attachment.FilePath)))
                    filePath = Path.Combine(basePath, attachment.FilePath);
            }
            return filePath;
        }
        public PProjectAttachment GetAttachmentFilePathAndFileName(long attachmentID, ref string filePath)
        {
            filePath = "";
            PProjectAttachment attachment = null;

            if (attachmentID > 0)
            {
                string basePath = "";
                basePath = (string)IBBParameter.GetCache(BParameterParaNo.UploadFilesPath.ToString());
                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("UploadFilesPath").Trim();
                }
                if (!String.IsNullOrEmpty(basePath))
                {
                    attachment = this.Get(attachmentID);
                    if (attachment != null && (!string.IsNullOrEmpty(attachment.FilePath)))
                    {
                        filePath = attachment.FilePath;
                        if (attachment != null && (!string.IsNullOrEmpty(basePath)) && (!string.IsNullOrEmpty(filePath)))
                            filePath = basePath + filePath;
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Warn("附件上传保存路径为空!(请到系统参数设置维护-UploadFilesPath)");
                }
            }
            return attachment;
        }
        private string _getDatePath()
        {
            string datePath = DateTime.Now.ToString("yyyy") + "\\" +
                DateTime.Now.ToString("MM") + "\\" +
                DateTime.Now.ToString("dd") + "\\";
            return datePath;

        }
    }
}