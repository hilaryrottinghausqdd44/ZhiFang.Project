using System.Collections.Generic;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLisOrderFormDao : IDBaseDao<LisOrderForm, long>
    {
        List<LisOrderFormVo> GetOrderList(string hisDeptNo, string patno, string sickTypeNo, string strWhere);
        System.Collections.IList GetOrderFormList(string fields, string where, out string sqlwhere);
    }
}