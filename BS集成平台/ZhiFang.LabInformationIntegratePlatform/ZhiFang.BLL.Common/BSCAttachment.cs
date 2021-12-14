using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ZhiFang.BLL.Base;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.Common;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Common;

namespace ZhiFang.BLL.Common
{
    /// <summary>
    ///
    /// </summary>
    public class BSCAttachment : BaseBLL<SCAttachment>, IBSCAttachment
    {
        public IBBParameter IBBParameter { get; set; }
        //public BaseResultDataValue AddSCAttachment(string fkObjectId, string fkObjectName, HttpPostedFile file, string parentPath, string tempPath, string fileExt, SCAttachment entity)
        //{
        //    BaseResultDataValue brdv = new BaseResultDataValue();
        //    string nullValue = "{id:'',fileSize:''}";
        //    string fileName = entity.FileName.Substring(0, entity.FileName.LastIndexOf(".")) + "_" + entity.Id + fileExt + "." + FileExt.zf.ToString();
        //    string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
        //    if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
        //    {
        //        HREmployee creator = new HREmployee();
        //        creator.Id = long.Parse(employeeID);
        //        byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
        //        creator.DataTimeStamp = arrDataTimeStamp;
        //        //entity.Creator = creator;
        //    }
        //    entity.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
        //    entity.FilePath = entity.FilePath + fileName;
        //    entity.BobjectID = long.Parse(fkObjectId);
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
        //        brdv.ResultDataValue = nullValue;
        //        brdv.ErrorInfo = "错误信息：" + ex.Message;
        //        ZhiFang.Common.Log.Log.Error("公共附件上传错误信息：" + ex.Message);
        //    }
        //    finally
        //    {
        //        if (brdv.success)
        //        {
        //            this.Entity = entity;
        //            brdv.success = this.Add();
        //            if (brdv.success)
        //            {
        //                this.Get(this.Entity.Id);
        //                brdv.ResultDataValue = "{id:" + "\"" + entity.Id.ToString() + "\"" + ",fileSize:" + "\"" + entity.FileSize + "\"" + "}";

        //            }
        //        }
        //    }
        //    return brdv;
        //}

        public SCAttachment GetAttachmentFilePathAndFileName(long attachmentID, ref string filePath)
        {
            filePath = "";
            SCAttachment attachment = null;
            if (attachmentID > 0)
            {
                string basePath = "";
                //basePath = (string)IBBParameter.GetCache(BParameterCache.BParameterParaNo.UploadFilesPath.ToString());
                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("UploadFilesPath").Trim();
                }
                if (!String.IsNullOrEmpty(basePath))
                {
                    attachment = this.Get(attachmentID);
                    if (attachment != null && (!string.IsNullOrEmpty(attachment.FilePath)))
                    {
                        filePath = attachment.FilePath;// + fileName;
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

    }
}