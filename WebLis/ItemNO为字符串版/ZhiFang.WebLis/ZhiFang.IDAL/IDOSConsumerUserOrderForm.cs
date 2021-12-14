using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
    }
}
