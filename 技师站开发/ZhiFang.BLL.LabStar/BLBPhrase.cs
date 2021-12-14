using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBPhrase : BaseBLL<LBPhrase>, ZhiFang.IBLL.LabStar.IBLBPhrase
    {
        public EntityList<LBPhraseVO> QueryLBPhraseVO(string phraseType, string typeName, string typeCode, long? objectID, string otherWhere)
        {
            return (this.DBDao as IDLBPhraseDao).QueryLBPhraseVODao(phraseType, typeName, typeCode, objectID, otherWhere);
        }
    }
}