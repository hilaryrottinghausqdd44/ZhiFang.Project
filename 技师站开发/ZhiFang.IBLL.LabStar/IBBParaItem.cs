using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBParaItem : IBGenericManager<BParaItem>
    {

        BaseResultDataValue AddAndEditParaItem(string objectInfo, IList<BParaItem> listParaItem, string operaterID, string operater);

        BaseResultDataValue AddAndEditParaItem(IList<BParaItem> listParaItem, string objectID, string objectName, string operaterID, string operater);

        BaseResultDataValue AddCopyParaItemByObjectID(string fromObjectID, string toObjectID, string toObjectName, string operaterID, string operater);

        BaseResultDataValue EditParaItemDefaultValueByObjectID(string objectID, string paraTypeCode, string operaterID, string operater);

        BaseResultDataValue DeleteParaItemByObjectID(string objectInfo);

        IList<object> QueryParaSystemTypeInfo(string systemTypeCode, string paraTypeCode);

        IList<BParaItem> QuerySystemParaItem(string where, string systemTypeCode, string paraTypeCode);

        BPara QueryParaValueByParaNo(string paraNo, string objectID);

        IList<BPara> QueryParaValueByParaTypeCode(string paraTypeCode, string objectID, string operaterID, string operater);

        IList<BPara> QueryParaValueByParaTypeCode(string where, string paraTypeCode, string objectID, string operaterID, string operater);

        ItemHistoryComparePara GetItemHistoryComparePara(string objectID, string operaterID, string operater);

        BaseResultDataValue PreEditParSystemParaItem(string ObjectID, List<BParaItem> entityList, string operaterID, string operater);

        List<BPara> SelPreBParas(long nodetype, string paranos, string typecode, string userid, string username);
        BaseResultDataValue DeleteParaItemByObjectIDAndHostTypeUser(string objectInfo);

    }
}