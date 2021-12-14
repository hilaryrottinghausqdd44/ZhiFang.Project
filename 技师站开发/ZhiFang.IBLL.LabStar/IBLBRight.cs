using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBRight : IBGenericManager<LBRight>
    {
        IList<string> QueryEmpIDBySectionID(long sectionID);

        EntityList<LBSection> QueryCommoSectionByEmpID(string empIDList);

        BaseResultDataValue AddEmpSectionDataRight(string empIDList, string sectionIDList, string roleIDList, string operateID, string operater);

        BaseResultDataValue DelelteEmpSectionDataRight(string empIDList, string sectionIDList, string roleIDList, string empID, string empName);
        
    }
}