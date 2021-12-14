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
    public interface IBReaGoodsRegister : IBGenericManager<ReaGoodsRegister>
    {
        BaseResultDataValue AddReaGoodsRegisterAndUploadRegisterFile(HttpPostedFile file);
        BaseResultBool UpdateReaGoodsRegisterAndUploadRegisterFileByField(string[] tempArray, HttpPostedFile file);
        EntityList<ReaGoodsRegister> SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL(string strHqlWhere, int page, int count);
        EntityList<ReaGoodsRegister> SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL(string strHqlWhere, string Order, int page, int count);
        string GetFileParentPath();
        FileStream GetReaGoodsRegisterFileStream(long id, ref string fileName);
    }
}