using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;
using System.Web;
using System.IO;
using ZhiFang.Entity.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
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
        FileStream GetReaGoodsRegisterFileStream(long id, ref string fileName);
    }
}