

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.Base;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.IBLL
{
	/// <summary>
	///
	/// </summary>
	public  interface IBOSManagerRefundFormAttachment : IBGenericManager<OSManagerRefundFormAttachment>
	{

        BaseResultDataValue AddSCAttachment(string fkObjectId, string fkObjectName, HttpPostedFile file, string parentPath, string tempPath, string fileExt, OSManagerRefundFormAttachment entity);
        OSManagerRefundFormAttachment GetAttachmentFilePathAndFileName(long id, ref string filePath);

    }
}
