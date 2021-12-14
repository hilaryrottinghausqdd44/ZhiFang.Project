using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.DALFactory;
using ZhiFang.IBLL.Report;
using ZhiFang.IDAL;
using ZhiFang.Model;
using ZhiFang.Model.UiModel;

namespace ZhiFang.BLL.Report
{
    public class OSConsumerUserOrderForm: IBOSConsumerUserOrderForm
    {
        private readonly IDAL.IDOSConsumerUserOrderForm dal = DalFactory<IDOSConsumerUserOrderForm>.GetDalByClassName("OSConsumerUserOrderForm");
        public OSConsumerUserOrderForm()
        {

        }
        public BaseResultDataValue ConsumerUserOrderForm(string PayCode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            UserOrderFormVO uof = new UserOrderFormVO();
            DataTable dtuof = null;
            DataTable dtuoi = null;
            DataTable dtUserAccount = null;
            DataTable dtDoctAccount = null;
            DataTable dtdof = null;
            brdv = CheckUserOrderForm(PayCode, out dtUserAccount, out dtDoctAccount, out dtdof, out dtuof, out dtuoi);
            if (!brdv.success)
            {
                return brdv;
            }
            uof.Name = dtdof.Rows[0]["UserName"].ToString();    
            uof.DeptName = dtDoctAccount.Rows[0]["HospitalDeptName"].ToString();
            uof.Age = int.Parse(dtdof.Rows[0]["Age"].ToString());
            uof.DoctorMemo = dtdof.Rows[0]["Memo"].ToString();
            uof.DoctorName = dtDoctAccount.Rows[0]["Name"].ToString();
            uof.PatNo= (dtdof.Rows[0]["PatNo"]!=null)?dtdof.Rows[0]["PatNo"].ToString():"";
            uof.Price = Double.Parse(dtuof.Rows[0]["Price"].ToString());
            uof.SexName = dtdof.Rows[0]["SexName"].ToString();
            uof.AreaID= dtdof.Rows[0]["AreaID"].ToString();
            uof.UserOrderItem = new List<UserOrderItemVO>();
            for (int i = 0; i < dtuoi.Rows.Count; i++)
            {
                UserOrderItemVO uoi= new UserOrderItemVO();
                uoi.ItemID = long.Parse(dtuoi.Rows[i]["ItemID"].ToString());
                uoi.ItemNo = dtuoi.Rows[i]["ItemNo"].ToString();
                uoi.Name = dtuoi.Rows[i]["ItemCName"].ToString();
                uof.UserOrderItem.Add(uoi);
            }
            brdv.ResultDataValue = ZhiFang.Tools.JsonSerializer.JsonDotNetSerializer(uof);
            brdv.success = true;
            return brdv;
        }
        public BaseResultDataValue SaveOSUserConsumerForm(long NRequestFormNo, NrequestCombiItemBarCodeEntity jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            DataTable dtuof = null;
            DataTable dtuoi = null;
            DataTable dtUserAccount = null;
            DataTable dtDoctAccount = null;
            DataTable dtdof = null;
            brdv = CheckUserOrderForm(jsonentity.PayCode, out dtUserAccount, out dtDoctAccount, out dtdof, out dtuof, out dtuoi);
            if (!brdv.success)
            {
                return brdv;
            }
            brdv.success = dal.SaveOSUserConsumerForm(NRequestFormNo, dtuof.Rows[0], jsonentity);
            brdv.ResultDataValue = brdv.success.ToString();
            return brdv;
        }
        public BaseResultDataValue CheckUserOrderForm(string PayCode, out DataTable dtUserAccount, out DataTable dtDoctAccount, out DataTable dtdof, out DataTable dtuof, out DataTable dtuoi)
        {
            dtuof = null;
            dtuoi = null;
            dtUserAccount = null;
            dtDoctAccount = null;
            dtdof = null;

            BaseResultDataValue brdv = new BaseResultDataValue();
            #region 检查状态
            dtuof = dal.GetOSUserOrderFormByPayCode(PayCode);
            if (dtuof == null || dtuof.Rows.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.错误的消费码！PayCode:" + PayCode);
                brdv.ErrorInfo = "错误的消费码！";
                brdv.success = false;
                return brdv;
            }
            string status = dtuof.Rows[0]["Status"].ToString();
            if (status != UserOrderFormStatus.已交费.Key)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.状态错误！PayCode:" + PayCode + ",status:" + UserOrderFormStatus.GetStatusDic()[status].Name);
                if (status == UserOrderFormStatus.待缴费.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单未缴费！";
                }
                if (status == UserOrderFormStatus.部分使用.Key || status == UserOrderFormStatus.完全使用.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单已使用！";
                }
                if (status == UserOrderFormStatus.取消处理中.Key || status == UserOrderFormStatus.取消成功.Key || status == UserOrderFormStatus.取消订单.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单取消中！";
                }
                if (status == UserOrderFormStatus.退款中.Key || status == UserOrderFormStatus.退款完成.Key || status == UserOrderFormStatus.退款申请.Key || status == UserOrderFormStatus.退款申请处理中.Key || status == UserOrderFormStatus.退款申请.Key || status == UserOrderFormStatus.退款申请被打回.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单退款中！";
                }
                brdv.success = false;
                return brdv;
            }

            if (dtuof.Rows[0]["DOFID"] == null || dtuof.Rows[0]["DOFID"].ToString() == "")
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.订单的医嘱单ID为空！PayCode:" + PayCode + ",UOFID:" + dtuof.Rows[0]["UOFID"]);
                brdv.ErrorInfo = "错误的消费码！";
                brdv.success = false;
                return brdv;
            }
            dtdof = dal.GetOSDoctorOrderFormByDOFID(long.Parse(dtuof.Rows[0]["DOFID"].ToString()));
            if (dtdof == null || dtdof.Rows.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到相关医嘱单！DOFID:" + dtuof.Rows[0]["DOFID"].ToString());
                brdv.ErrorInfo = "未找到相关医嘱单！";
                brdv.success = false;
                return brdv;
            }
            dtDoctAccount = dal.GetBDoctorAccountByDoctorAccountID(long.Parse(dtuof.Rows[0]["DoctorAccountID"].ToString()));
            if (dtDoctAccount == null || dtDoctAccount.Rows.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到相关医生账户！DoctorAccountID:" + dtuof.Rows[0]["DoctorAccountID"].ToString());
                brdv.ErrorInfo = "未找到相关医生账户！";
                brdv.success = false;
                return brdv;
            }
            dtUserAccount = dal.GetBWeiXinAccountByWeiXinUserID(long.Parse(dtuof.Rows[0]["UserAccountID"].ToString()));
            if (dtUserAccount == null || dtUserAccount.Rows.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到相关用户账户！UserAccountID:" + dtuof.Rows[0]["UserAccountID"].ToString());
                brdv.ErrorInfo = "未找到相关用户账户！";
                brdv.success = false;
                return brdv;
            }
            dtuoi = dal.GetOSUserOrderItemByUOFID(long.Parse(dtuof.Rows[0]["UOFID"].ToString()));
            if (dtuoi == null || dtuoi.Rows.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到订单项目！UOFID:" + dtuof.Rows[0]["UOFID"].ToString());
                brdv.ErrorInfo = "未找到订单项目！";
                brdv.success = false;
                return brdv;
            }
            #endregion
            brdv.success = true;
            return brdv;
        }
    }
    /// <summary>
    /// 用户订单状态
    /// </summary>
    public static class UserOrderFormStatus
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 待缴费 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "待缴费", Code = "UnPay", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 已交费 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已交费", Code = "Payed", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 部分使用 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "部分使用", Code = "PartialUse", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 完全使用 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "完全使用", Code = "Useed", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 取消订单 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "取消订单", Code = "CancelApply", FontColor = "#ffffff", BGColor = "#12abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 取消处理中 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "取消处理中", Code = "Canceling", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 取消成功 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "取消成功", Code = "Canceled", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款申请 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "退款申请", Code = "RefundApply", FontColor = "#ffffff", BGColor = "#1195db" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款申请处理中 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "退款申请处理中", Code = "RefundApplying", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款申请被打回 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "退款申请被打回", Code = "RefundApplyBack", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款中 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "退款中", Code = "Refunding", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款完成 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "退款完成", Code = "Refunded", FontColor = "#ffffff", BGColor = "#e97f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(UserOrderFormStatus.待缴费.Key, UserOrderFormStatus.待缴费.Value);
            dic.Add(UserOrderFormStatus.已交费.Key, UserOrderFormStatus.已交费.Value);
            dic.Add(UserOrderFormStatus.部分使用.Key, UserOrderFormStatus.部分使用.Value);
            dic.Add(UserOrderFormStatus.完全使用.Key, UserOrderFormStatus.完全使用.Value);
            dic.Add(UserOrderFormStatus.取消订单.Key, UserOrderFormStatus.取消订单.Value);
            dic.Add(UserOrderFormStatus.取消处理中.Key, UserOrderFormStatus.取消处理中.Value);
            dic.Add(UserOrderFormStatus.取消成功.Key, UserOrderFormStatus.取消成功.Value);
            dic.Add(UserOrderFormStatus.退款申请.Key, UserOrderFormStatus.退款申请.Value);
            dic.Add(UserOrderFormStatus.退款申请处理中.Key, UserOrderFormStatus.退款申请处理中.Value);
            dic.Add(UserOrderFormStatus.退款中.Key, UserOrderFormStatus.退款中.Value);
            dic.Add(UserOrderFormStatus.退款完成.Key, UserOrderFormStatus.退款完成.Value);
            return dic;
        }
    }
    public class BaseClassDicEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string FontColor { get; set; }
        public string BGColor { get; set; }
    }
}
