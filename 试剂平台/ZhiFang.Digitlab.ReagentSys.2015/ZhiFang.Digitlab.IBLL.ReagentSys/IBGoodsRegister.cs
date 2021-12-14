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
    public interface IBGoodsRegister : IBGenericManager<GoodsRegister>
    {
        BaseResultDataValue AddGoodsRegisterAndUploadRegisterFile(HttpPostedFile file,string hrdeptID, string hrdeptCode);
        BaseResultBool UpdateGoodsRegisterAndUploadRegisterFileByField(string[] tempArray, HttpPostedFile file);
        EntityList<GoodsRegister> SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL(string strHqlWhere,int page, int count);
        EntityList<GoodsRegister> SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL(string strHqlWhere, string Order, int page, int count);
        string GetFileParentPath();
        FileStream GetGoodsRegisterFileStream(long id, ref string fileName);
    }
}