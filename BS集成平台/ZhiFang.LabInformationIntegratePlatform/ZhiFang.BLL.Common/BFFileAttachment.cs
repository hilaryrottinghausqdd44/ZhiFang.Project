using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ZhiFang.BLL.Base;
using ZhiFang.IDAO.Common;
using ZhiFang.Entity.Common;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.Common;
using System.Web;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BLL.Common
{
    /// <summary>
    ///
    /// </summary>
    public class BFFileAttachment : BaseBLL<FFileAttachment>, ZhiFang.IBLL.Common.IBFFileAttachment
    {
        public IDFFileDao IDFFileDao { get; set; }
        IBBParameter IBBParameter { get; set; }
        public string GetAttachmentFilePath(long attachmentID)
        {
            string filePath = "";
            if (attachmentID > 0)
            {
                string basePath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("AttachmentPath").Trim();
                FFileAttachment attachment = this.Get(attachmentID);
                if (attachment != null && (!string.IsNullOrEmpty(attachment.FilePath)))
                    filePath = Path.Combine(basePath, attachment.FilePath);
            }
            return filePath;
        }
        public FFileAttachment GetAttachmentFilePathAndFileName(long attachmentID, ref string filePath)
        {
            filePath = "";
            FFileAttachment attachment = null;

            if (attachmentID > 0)
            {
                string basePath = "";
                basePath = (string)IBBParameter.GetCache(FFileParameter.UploadFilesPath.ToString());

                attachment = this.Get(attachmentID);
                if (attachment != null && (!string.IsNullOrEmpty(attachment.FilePath)))
                {
                    filePath = attachment.FilePath;// + fileName;
                    if (attachment != null && (!string.IsNullOrEmpty(basePath)) && (!string.IsNullOrEmpty(filePath)))
                        filePath = basePath + filePath;
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
        /// <summary>
        /// QMS的文档附件信息保存
        /// </summary>
        /// <param name="fkObjectId"></param>
        /// <param name="fkObjectName"></param>
        /// <param name="file"></param>
        /// <param name="parentPath"></param>
        /// <param name="tempPath"></param>
        /// <param name="fileExt"></param>
        /// <param name="entity"></param>
        /// <param name="oldObjectId">原附件的Id</param>
        /// <returns></returns>
        //public BaseResultDataValue AddFFileAttachment(string fkObjectId, string fkObjectName, HttpPostedFile file, string parentPath, string tempPath, string fileExt, FFileAttachment entity, string oldObjectId)
        //{
        //    BaseResultDataValue brdv = new BaseResultDataValue();
        //    string fileName = entity.FileName.Substring(0, entity.FileName.LastIndexOf(".")) + "_" + entity.Id + fileExt + "." + FileExt.zf.ToString();
        //    string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
        //    if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
        //    {
        //        HREmployee creator = new HREmployee();
        //        creator.Id = long.Parse(employeeID);
        //        byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
        //        creator.DataTimeStamp = arrDataTimeStamp;
        //        entity.Creator = creator;
        //    }
        //    entity.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
        //    switch (fkObjectName)
        //    {
        //        case "FFile":
        //            FFile ffile = new FFile();
        //            ffile.Id = long.Parse(fkObjectId);
        //            ffile = IDFFileDao.Get(long.Parse(fkObjectId));
        //            //新增修订文档的附件信息,原文档附件信息
        //            if (!String.IsNullOrEmpty(oldObjectId) && file == null)
        //            {
        //                FFileAttachment ffileAttachment = this.Get(long.Parse(oldObjectId));
        //                entity.FilePath = ffileAttachment.FilePath;
        //                entity.FileType = ffileAttachment.FileType;
        //                entity.FileSize = ffileAttachment.FileSize;
        //                entity.FileExt = ffileAttachment.FileExt;
        //            }
        //            else
        //            {
        //                entity.FilePath = entity.FilePath + fileName;
        //            }
        //            entity.FFile = ffile;
        //            entity.LabID = ffile.LabID;
        //            break;
        //        default:
        //            break;
        //    }
        //    try
        //    {
        //        string filepath = Path.Combine(parentPath, fileName);
        //        if (file != null)
        //        {
        //            file.SaveAs(filepath);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        brdv.success = false;
        //        brdv.ResultDataValue = "{id:''}";
        //        brdv.ErrorInfo = "错误信息：" + ex.Message;
        //        ZhiFang.Common.Log.Log.Error("错误信息：" + ex.Message);
        //    }
        //    finally
        //    {
        //        if (brdv.success)
        //        {
        //            //ZhiFang.Common.Log.Log.Debug("文档附件信息数据库保存操作");
        //            this.Entity = entity;
        //            brdv.success = this.Add();
        //            //ZhiFang.Common.Log.Log.Debug("文档附件新增保存：" + brdv.success);
        //            if (brdv.success)
        //            {
        //                this.Get(this.Entity.Id);
        //                brdv.ResultDataValue = "{id:" + "\"" + Entity.Id.ToString() + "\"" + "}"; 
        //            }
        //        }
        //    }
        //    return brdv;
        //}
    }
}