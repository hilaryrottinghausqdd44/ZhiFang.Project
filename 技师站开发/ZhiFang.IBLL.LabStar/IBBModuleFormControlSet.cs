using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Request;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBModuleFormControlSet : IBGenericManager<BModuleFormControlSet>
    {
        List<BModuleFormControlList> SearchBModuleFormControlSetListByFormCode(string FormCode, string sort);
        bool AddBModuleFormControlSets(List<BModuleFormControlSet> bModuleFormControlSets);
        bool EditBModuleFormControlSets(List<BModuleFormControlSetVO> bModuleFormControlSetVOs);
    }
}