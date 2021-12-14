using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBHospitalEmpLink : IBGenericManager<BHospitalEmpLink>
    {
        EntityList<BHospitalEmpLink> SearchSelectHospitalListByEmpId(string empId, string Sort, int page, int limit);
        EntityList<BHospital> SearchUnSelectHospitalListByEmpId(string empId, string Sort, int page, int limit);
        BaseResultDataValue SaveByList(List<BHospitalEmpLink> entitylist);
        bool BatchAddBHospitalEmpLink(List<BHospitalEmpLink> entitylist);
        bool BHospitalEmpLinkSetLinkType(long id, long typeid);
        long GetLabIdByEmpId(long id);
        BHospitalEmpLink GetBHospitalEmpLinkByEmpId(long id);
    }
}