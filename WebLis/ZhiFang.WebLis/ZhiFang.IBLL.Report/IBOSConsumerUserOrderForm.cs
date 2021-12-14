using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using ZhiFang.Model.UiModel;

namespace ZhiFang.IBLL.Report
{
    public interface IBOSConsumerUserOrderForm
    {
        BaseResultDataValue SaveOSUserConsumerForm(long NRequestFormNo, NrequestCombiItemBarCodeEntity jsonentity);
        BaseResultDataValue ConsumerUserOrderForm(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisOrgID, string WeblisOrgName,string ConsumerAreaID);
        BaseResultDataValue UnLockUserOrderForm(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisOrgID, string WeblisOrgName, string ConsumerAreaID);
        BaseResultDataValue CheckPayCodeIsUseing(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisOrgID, string WeblisOrgName);
        BaseResultDataValue SearchUnConsumerUserOrderFormList(string paycode, string EmpAccount, string ZhiFangUserID, string weblisOrgID, string weblisOrgName);
    }
}
