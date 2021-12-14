using System.Collections.Generic;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDBParaItemDao : IDBaseDao<BParaItem, long>
    {
        IList<object> QueryParaSystemTypeInfoDao(string systemTypeCode, string paraTypeCode);

        IList<BParaItem> QuerySystemParaItemDao(string where);

        BPara QueryParaValueByParaNo(string paraNo, string objectID);
        IList<object> QueryParaSystemTypeInfoByParaTypeCodesDao(string systemTypeCode, string paraTypeCodes);
        
    }
}