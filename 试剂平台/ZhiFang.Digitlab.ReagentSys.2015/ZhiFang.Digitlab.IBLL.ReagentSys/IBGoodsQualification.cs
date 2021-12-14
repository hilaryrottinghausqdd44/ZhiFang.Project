using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using System.Web;
using System.IO;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBGoodsQualification : IBGenericManager<GoodsQualification>
    {
        BaseResultDataValue AddGoodsQualificationAndUploadRegisterFile(HttpPostedFile file, string hrdeptID, string hrdeptCode);
        BaseResultBool UpdateGoodsQualificationAndUploadRegisterFileByField(string[] tempArray, HttpPostedFile file);
        string GetFileParentPath();
        FileStream GetGoodsQualificationFileStream(long id, ref GoodsQualification entity);
    }
}