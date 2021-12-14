using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBPhraseDao : IDBaseDao<LBPhrase, long>
    {
        EntityList<LBPhraseVO> QueryLBPhraseVODao(string phraseType, string typeName, string typeCode, long? objectID, string otherWhere);

    }
}