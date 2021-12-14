using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.RBAC;
using System.IO;
using ZhiFang.IBLL.ProjectProgressMonitorManage;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPCustomerServiceAttachment : BaseBLL<PCustomerServiceAttachment>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPCustomerServiceAttachment
    {
        public IBBParameter IBBParameter { get; set; }
        public BaseResultDataValue AddPCustomerServiceAttachment(string fkObjectId, string fkObjectName, HttpPostedFile file, string parentPath, string tempPath, string fileExt, PCustomerServiceAttachment entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string fileName = entity.FileName.Substring(0, entity.FileName.LastIndexOf(".")) + "_" + entity.Id + fileExt + "." + FileExt.zf.ToString();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
            {
                HREmployee creator = new HREmployee();
                creator.Id = long.Parse(employeeID);
                byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                creator.DataTimeStamp = arrDataTimeStamp;
                //entity.Creator = creator;
            }
            entity.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

            entity.FilePath = entity.FilePath + fileName;
            entity.CustomerServiceID = long.Parse(fkObjectId);
            try
            {
                string filepath = Path.Combine(parentPath, fileName);

                this.Entity = entity;
                brdv.success = this.Add();
                if (brdv.success)
                {
                    this.Get(this.Entity.Id);
                    //brdv.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(this.Entity);
                    brdv.ResultDataValue = "{id:" + "\"" + entity.Id.ToString() + "\"" + ",fileSize:" + "\"" + this.Entity.FileSize + "\"" + "}";
                    if (file != null)
                    {
                        file.SaveAs(filepath);
                    }
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ResultDataValue = "{id:'',fileSize:''}";
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息：" + ex.Message);
            }
            return brdv;
        }

        public PCustomerServiceAttachment GetAttachmentFilePathAndFileName(long id, ref string filePath)
        {
            filePath = "";
            PCustomerServiceAttachment attachment = null;
            if (id > 0)
            {
                string basePath = "";
                basePath = (string)IBBParameter.GetCache(BParameterParaNo.UploadFilesPath.ToString());
                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("UploadFilesPath").Trim();
                }
                if (!String.IsNullOrEmpty(basePath))
                {
                    attachment = this.Get(id);
                    if (attachment != null && (!string.IsNullOrEmpty(attachment.FilePath)))
                    {
                        filePath = attachment.FilePath;// + fileName;

                        if (attachment != null && (!string.IsNullOrEmpty(basePath)) && (!string.IsNullOrEmpty(filePath)))
                            filePath = basePath + filePath;
                    }
                }
                else {
                    ZhiFang.Common.Log.Log.Warn("附件上传保存路径为空!(请到系统参数设置维护-UploadFilesPath)");
                }
            }
            return attachment;
        }
    }
}