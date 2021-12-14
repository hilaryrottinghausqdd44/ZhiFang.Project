using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
    /// <summary>
    ///
    /// </summary>
    public interface IBSCAttachment : IBGenericManager<SCAttachment>
    {
        BaseResultDataValue AddSCAttachment(string fkObjectId, string fkObjectName, HttpPostedFile file, string parentPath, string tempPath, string fileExt, SCAttachment entity);
        SCAttachment GetAttachmentFilePathAndFileName(long id, ref string filePath);
    }
}