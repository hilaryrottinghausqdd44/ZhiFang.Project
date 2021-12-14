using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.BLL.Base;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using System.Web;
using System.IO;

namespace ZhiFang.WeiXin.BLL
{
	/// <summary>
	///
	/// </summary>
	public  class BOSDoctorBonusAttachment : BaseBLL<OSDoctorBonusAttachment>, ZhiFang.WeiXin.IBLL.IBOSDoctorBonusAttachment
	{
        public IBBParameter IBBParameter { get; set; }
        public BaseResultDataValue AddSCAttachment(string fkObjectId, string fkObjectName, HttpPostedFile file, string parentPath, string tempPath, string fileExt, OSDoctorBonusAttachment entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string nullValue = "{id:'',fileSize:''}";
            string fileName = entity.FileName.Substring(0, entity.FileName.LastIndexOf(".")) + "_" + entity.Id + fileExt + "." + FileExt.zf.ToString();
            string employeeID = Common.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
            {
                HREmployee creator = new HREmployee();
                creator.Id = long.Parse(employeeID);
                byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                creator.DataTimeStamp = arrDataTimeStamp;
                //entity.Creator = creator;
            }
            entity.CreatorName = Common.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

            entity.FilePath = entity.FilePath + fileName;
            entity.BobjectID = long.Parse(fkObjectId);
            try
            {
                string filepath = Path.Combine(parentPath, fileName);
                this.Entity = entity;
                brdv.success = this.Add();
                if (brdv.success)
                {
                    this.Get(this.Entity.Id);
                    //brdv.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(this.Entity);
                    brdv.ResultDataValue = "{id:" + "\"" + entity.Id.ToString() + "\"" + ",fileSize:" + "\"" + entity.FileSize + "\"" + "}";
                    if (file != null)
                    {
                        file.SaveAs(filepath);
                    }
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ResultDataValue = nullValue;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息：" + ex.Message);
            }
            return brdv;
        }

        public OSDoctorBonusAttachment GetAttachmentFilePathAndFileName(long attachmentID, ref string filePath)
        {
            filePath = "";
            OSDoctorBonusAttachment attachment = null;
            if (attachmentID > 0)
            {
                string basePath = "";
                basePath = (string)IBBParameter.GetCache(BParameterParaNoClass.UploadFilesPath.Key.ToString());
                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = Common.ConfigHelper.GetConfigString("UploadFilesPath").Trim();
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