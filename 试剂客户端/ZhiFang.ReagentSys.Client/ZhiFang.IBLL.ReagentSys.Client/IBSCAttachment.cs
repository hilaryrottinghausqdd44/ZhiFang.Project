using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
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