using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBPProjectAttachment : IBGenericManager<PProjectAttachment>
    {
        BaseResultDataValue AddAttachment(long id, PProjectAttachment fattachment, HttpPostedFile file, HREmployee hremployee);

        BaseResultDataValue EditFileContent(string filePath, string fileContent);

        string GetAttachmentFilePath(long AttachmentID);
        PProjectAttachment GetAttachmentFilePathAndFileName(long attachmentID, ref string filePath);
    }
}