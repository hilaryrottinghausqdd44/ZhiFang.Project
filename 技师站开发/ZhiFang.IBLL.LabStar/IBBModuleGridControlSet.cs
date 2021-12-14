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
    public interface IBBModuleGridControlSet : IBGenericManager<BModuleGridControlSet>
    {
        List<BModuleGridControlList> SearchBModuleGridControlListByGridCode(string GridCode, string sort);
        bool AddBModuleGridControlSets(List<BModuleGridControlSet> bModuleGridControlSets);
        bool EditBModuleGridControlSets(List<BModuleGridControlSetVO> bModuleGridControlSetVOs);
    }
}