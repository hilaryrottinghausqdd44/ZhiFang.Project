using System;
using System.ServiceModel.Activation;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.WebAssist.ServerContract;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.WebAssist;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;

namespace ZhiFang.WebAssist.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class StatisticalAnalysisService : IStatisticalAnalysisService
    {
        IBAgeUnit IBAgeUnit { get; set; }
        IBDepartment IBDepartment { get; set; }
        IBDistrict IBDistrict { get; set; }
        IBDoctor IBDoctor { get; set; }
        IBEmployee IBEmployee { get; set; }
        IBEquipItem IBEquipItem { get; set; }
        IBEquipment IBEquipment { get; set; }
        IBFolkType IBFolkType { get; set; }
        IBGenderType IBGenderType { get; set; }
        IBGroupItem IBGroupItem { get; set; }
        IBItemRange IBItemRange { get; set; }
        IBItemType IBItemType { get; set; }
        IBPGroup IBPGroup { get; set; }
        IBPUser IBPUser { get; set; }
        IBSampleType IBSampleType { get; set; }
        IBSickType IBSickType { get; set; }
        IBSuperGroup IBSuperGroup { get; set; }
        IBTestEquip IBTestEquip { get; set; }
        IBTestItem IBTestItem { get; set; }
        IBTestType IBTestType { get; set; }
        IBWardType IBWardType { get; set; }
        IBNRequestForm IBNRequestForm { get; set; }

        public BaseResultDataValue Test()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            return baseResultDataValue;
        }

    }
}
