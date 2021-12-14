using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLisTestGraph : IBGenericManager<LisTestGraph>
    {
        BaseResultDataValue AppendLisTestGraphToDataBase(long? graphDataID, long labID, string graphBase64, string graphThumb);

    }
}