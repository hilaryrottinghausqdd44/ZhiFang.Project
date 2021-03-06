using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBPhrase : IBGenericManager<LBPhrase>
    {
        EntityList<LBPhraseVO> QueryLBPhraseVO(string phraseType, string typeName, string typeCode, long? objectID, string otherWhere);

    }
}