using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;
using ZhiFang.IBLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.IBLL
{
	/// <summary>
	///
	/// </summary>
	public  interface IBOSUserOrderForm : IBGenericManager<OSUserOrderForm>
	{
        //BaseResultBool OSUserOrderFormStatusUpdate(string[] tempArray, long empID, string empName);
        BaseResultBool OSUserOrderFormRefundApplyByUser(string UOFCode, string RefundReaso, string UserName, string UserOpenID);
        BaseResultBool OSUserOrderFormRefundApplyByEmp(long UserOrderFormID, long EmpID, string EmpName, string RefundReason, Double RefundPrice);

        EntityList<OSUserOrderFormVO> VO_OSUserOrderFormList(IList<OSUserOrderForm> listOSUserOrderForm);

        OSUserOrderFormVO VO_OSUserOrderForm(OSUserOrderForm osUserOrderForm);

        BaseResultDataValue AddUserOrderFormConfirmByOrderFormId(string UserOpenID, long id);
        void UpdateUnifiedOrder(long UOFID, string PrePayId, string PrePayReturnCode, string PrePayReturnMsg);
        void UpdateUnifiedOrderError(long UOFID, string PrePayReturnCode, string PrePayReturnMsg, string PrePayErrCode, string PrePayErrName);
        void UpdatePayStatus(long UOFID, string TransactionId, string PayTime);
        BaseResultBool UpdateOSUserOrderFormStatusOfCancelOrder(long id);
        BaseResultDataValue ST_UDTO_UnLockOSUserOrderFormById(long oSUserOrderFormId, long v1, string v2);
        int UpdateOSUserOrderFormStatusOfUseing(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisSourceOrgID, string WeblisSourceOrgName, string ConsumerAreaID);
        int UnLockOSUserOrderFormByPayCode(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisSourceOrgID, string WeblisSourceOrgName, string ConsumerAreaID);

        BaseResultDataValue OSConsumerUserOrderForm(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisSourceOrgID, string WeblisSourceOrgName, string ConsumerAreaID);
        BaseResultDataValue CheckPayCodeIsUseing(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisOrgID, string WeblisOrgName);
        BaseResultDataValue CheckUserOrderForm(string PayCode, out EntityList<BWeiXinAccount> dtUserAccount, out EntityList<BDoctorAccount> dtDoctAccount, out EntityList<OSDoctorOrderForm> dtdof, out EntityList<OSUserOrderForm> dtuof, out EntityList<OSUserOrderItem> dtuoi);
    }
}