using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLisBarCodeFormDao : IDBaseDao<LisBarCodeForm, long>
    {
        List<LisBarCodeForm> QueryLisBarCodeFormByHqlDao(string strHqlWhere, string Order, int start, int count, string strHQL);
        System.Collections.IList GetBarCodeFormList(string fields, string where);

    }
}