using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.Common;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.Common
{
    /// <summary>
    ///
    /// </summary>
    public  interface IBFFileAttachment : IBGenericManager<FFileAttachment>
	{
        string GetAttachmentFilePath(long AttachmentID);
        FFileAttachment GetAttachmentFilePathAndFileName(long attachmentID, ref string filePath);
        BaseResultDataValue AddFFileAttachment(string fkObjectId, string fkObjectName, HttpPostedFile file, string parentPath, string tempPath, string fileExt, FFileAttachment entity, string oldObjectId);
    }
}