using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBPhraseDao : BaseDaoNHB<LBPhrase, long>, IDLBPhraseDao
    {
        public EntityList<LBPhraseVO> QueryLBPhraseVODao(string phraseType, string typeName, string typeCode, long? objectID, string otherWhere)
        {
            EntityList<LBPhraseVO> entityList = new EntityList<LBPhraseVO>();
            string strHQL = " select new LBPhraseVO(lbphrase.PhraseType, lbphrase.TypeName, lbphrase.TypeCode, lbphrase.CName, lbphrase.Shortcode, lbphrase.PinYinZiTou) from LBPhrase lbphrase  where 1=1 ";
            if (phraseType != null && phraseType.Length > 0)
            {
                strHQL += " and lbphrase.PhraseType=\'" + phraseType + "\'";
            }
            if (typeName != null && typeName.Length > 0)
            {
                strHQL += " and lbphrase.TypeName=\'" + typeName + "\'";
            }
            if (typeCode != null && typeCode.Length > 0)
            {
                strHQL += " and lbphrase.TypeCode=\'" + typeCode + "\'";
            }
            if (objectID != null && objectID > 0)
            {
                strHQL += " and lbphrase.ObjectID=" + objectID.Value;
            }
            if (otherWhere != null && otherWhere.Length > 0)
            {
                strHQL += " and " + otherWhere;
            }
            strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<LBPhrase>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            strHQL += " group by lbphrase.PhraseType, lbphrase.TypeName, lbphrase.TypeCode, lbphrase.CName, lbphrase.Shortcode, lbphrase.PinYinZiTou ";
            IList<LBPhraseVO> list = this.Session.CreateQuery(strHQL).List<LBPhraseVO>();
            if (list != null && list.Count > 0)
            {
                entityList.count = list.Count;
                entityList.list = list;
            }
            return entityList;
        }

    }
}