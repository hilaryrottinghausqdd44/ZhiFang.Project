using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.IO;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.Common.Public;
using ZhiFang.ProjectProgressMonitorManage.Common;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
	/// <summary>
	///
	/// </summary>
	public  class BEAttachment : BaseBLL<EAttachment>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBEAttachment
    {

        public BaseResultDataValue AddEAttachment(EAttachment entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath");
            this.Entity = entity;
            baseResultDataValue.success = this.Add();
            return baseResultDataValue;
        }

        public BaseResultDataValue PreviewTempletAttachment(EAttachment attachment)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(attachment.FilePath))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "附件文件路径为空";
                ZhiFang.Common.Log.Log.Info("附件文件路径为空,附件ID：" + attachment.Id.ToString());
                return baseResultDataValue;
            }
            string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath");
            string filaName = parentPath + "\\" + attachment.FilePath;
            if (!File.Exists(filaName))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "附件文件不存在";
                ZhiFang.Common.Log.Log.Info("附件文件不存在,附件ID：" + attachment.Id.ToString());
                return baseResultDataValue;
            }

            baseResultDataValue.ResultDataValue = filaName;
            return baseResultDataValue;
        }

    }
}