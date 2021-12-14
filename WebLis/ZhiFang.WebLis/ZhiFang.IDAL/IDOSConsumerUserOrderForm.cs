using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using ZhiFang.Model.UiModel;

namespace ZhiFang.IDAL
{
    public interface IDOSConsumerUserOrderForm
    {
        DataTable GetOSUserOrderFormByPayCode(string PayCode);
        DataTable GetBDoctorAccountByDoctorAccountID(long DoctorAccountID);
        DataTable GetBWeiXinAccountByWeiXinUserID(long WeiXinUserID);
        DataTable GetOSDoctorOrderFormByDOFID(long DOFID);
        DataTable GetOSUserOrderItemByUOFID(long UOFID);
        bool SaveOSUserConsumerForm(long nRequestFormNo, DataRow DrOSUserOrderForm, NrequestCombiItemBarCodeEntity jsonentity);
        int LockOSUserOrderFormByPayCode(string payCode, string EmpAccount, string ZhiFangUserID, string WeblisOrgID, string WeblisOrgName, string ConsumerAreaID);
        int UnLockOSUserOrderFormByPayCode(string payCode, string EmpAccount, string ZhiFangUserID, string WeblisOrgID, string WeblisOrgName, string ConsumerAreaID);
        List<OSUserOrderForm> SearchUnConsumerUserOrderFormList(string paycode, string empAccount, string zhiFangUserID, string weblisOrgID, string weblisOrgName);
    }
}
