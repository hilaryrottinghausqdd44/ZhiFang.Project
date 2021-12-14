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
using ZhiFang.Model.WeiXinDic;

namespace ZhiFang.BLL.Report
{
    public class OSConsumerUserOrderForm: IBOSConsumerUserOrderForm
    {
        private readonly IDAL.IDOSConsumerUserOrderForm dal = DalFactory<IDOSConsumerUserOrderForm>.GetDalByClassName("OSConsumerUserOrderForm");
        public OSConsumerUserOrderForm()
        {

        }
        public BaseResultDataValue ConsumerUserOrderForm(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisOrgID, string WeblisOrgName, string ConsumerAreaID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            UserOrderFormVO uof = new UserOrderFormVO();
            DataTable dtuof = null;
            DataTable dtuoi = null;
            DataTable dtUserAccount = null;
            DataTable dtDoctAccount = null;
            DataTable dtdof = null;
            brdv = LockUserOrderForm(PayCode, EmpAccount, ZhiFangUserID, WeblisOrgID, WeblisOrgName, ConsumerAreaID,  out dtUserAccount, out dtDoctAccount, out dtdof, out dtuof, out dtuoi);
            if (!brdv.success)
            {
                return brdv;
            }
            uof.Name = dtdof.Rows[0]["UserName"].ToString();    
            
            uof.Age = int.Parse(dtdof.Rows[0]["Age"].ToString());
            uof.DoctorMemo = dtdof.Rows[0]["Memo"].ToString();
            if (dtDoctAccount != null)
            {
                uof.DeptName = dtDoctAccount.Rows[0]["HospitalDeptName"] != DBNull.Value ? dtDoctAccount.Rows[0]["HospitalDeptName"].ToString() : "";
                uof.DoctorName = dtDoctAccount.Rows[0]["Name"].ToString();
            }
            if (dtdof.Rows[0]["TypeID"].ToString().Trim() == "2")
            {
                uof.DeptName = "";
                uof.DoctorName = "";
            }
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
            if (status != UserOrderFormStatus.已交费.Key&& status != UserOrderFormStatus.使用中.Key)
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
            if (dtuof.Rows[0]["DoctorAccountID"] != DBNull.Value)
            {
                dtDoctAccount = dal.GetBDoctorAccountByDoctorAccountID(long.Parse(dtuof.Rows[0]["DoctorAccountID"].ToString()));
                if (dtDoctAccount == null || dtDoctAccount.Rows.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到相关医生账户！DoctorAccountID:" + dtuof.Rows[0]["DoctorAccountID"].ToString());
                    brdv.ErrorInfo = "未找到相关医生账户！";
                    brdv.success = false;
                    return brdv;
                }
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

        public BaseResultDataValue LockUserOrderForm(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisOrgID, string WeblisOrgName,string ConsumerAreaID, out DataTable dtUserAccount, out DataTable dtDoctAccount, out DataTable dtdof, out DataTable dtuof, out DataTable dtuoi)
        {
            dtuof = null;
            dtuoi = null;
            dtUserAccount = null;
            dtDoctAccount = null;
            dtdof = null;

            BaseResultDataValue brdv = new BaseResultDataValue();
            if (dal.LockOSUserOrderFormByPayCode(PayCode, EmpAccount,ZhiFangUserID,WeblisOrgID,WeblisOrgName, ConsumerAreaID) <= 0)//锁定（部分消费3）
            {
                #region 检查状态
                dtuof = dal.GetOSUserOrderFormByPayCode(PayCode);
                if (dtuof == null || dtuof.Rows.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.LockUserOrderForm.错误的消费码！PayCode:" + PayCode);
                    brdv.ErrorInfo = "错误的消费码！";
                    brdv.success = false;
                    return brdv;
                }
                if (dtuof.Rows[0]["AreaID"].ToString().Trim()!= ConsumerAreaID)
                {
                    ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.LockUserOrderForm.该消费码不能在当前区域内消费！PayCode:" + PayCode);
                    brdv.ErrorInfo = "该消费码不能在当前区域内消费！";
                    brdv.success = false;
                    return brdv;
                }
                string status = dtuof.Rows[0]["Status"].ToString();
                if (status != UserOrderFormStatus.已交费.Key)
                {
                    ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.LockUserOrderForm.状态错误！PayCode:" + PayCode + ",status:" + UserOrderFormStatus.GetStatusDic()[status].Name);
                    if (status == UserOrderFormStatus.待缴费.Key)
                    {
                        brdv.ErrorInfo = "错误的消费码:订单未缴费！";
                    }

                    if (status == UserOrderFormStatus.使用中.Key)
                    {
                        brdv.ErrorInfo = "错误的消费码:订单正在使用中！";
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
                #endregion
            }
            else
            {
                dtuof = dal.GetOSUserOrderFormByPayCode(PayCode);
                
                dtdof = dal.GetOSDoctorOrderFormByDOFID(long.Parse(dtuof.Rows[0]["DOFID"].ToString()));
                if (dtdof == null || dtdof.Rows.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.LockUserOrderForm.未找到相关医嘱单！DOFID:" + dtuof.Rows[0]["DOFID"].ToString());
                    brdv.ErrorInfo = "未找到相关医嘱单！";
                    brdv.success = false;
                    return brdv;
                }
                if (dtuof.Rows[0]["DoctorAccountID"] != DBNull.Value)
                {
                    dtDoctAccount = dal.GetBDoctorAccountByDoctorAccountID(long.Parse(dtuof.Rows[0]["DoctorAccountID"].ToString()));
                    if (dtDoctAccount == null || dtDoctAccount.Rows.Count <= 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.LockUserOrderForm.未找到相关医生账户！DoctorAccountID:" + dtuof.Rows[0]["DoctorAccountID"].ToString());
                        brdv.ErrorInfo = "未找到相关医生账户！";
                        brdv.success = false;
                        return brdv;
                    }
                }
                dtUserAccount = dal.GetBWeiXinAccountByWeiXinUserID(long.Parse(dtuof.Rows[0]["UserAccountID"].ToString()));
                if (dtUserAccount == null || dtUserAccount.Rows.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.LockUserOrderForm.未找到相关用户账户！UserAccountID:" + dtuof.Rows[0]["UserAccountID"].ToString());
                    brdv.ErrorInfo = "未找到相关用户账户！";
                    brdv.success = false;
                    return brdv;
                }
                dtuoi = dal.GetOSUserOrderItemByUOFID(long.Parse(dtuof.Rows[0]["UOFID"].ToString()));
                if (dtuoi == null || dtuoi.Rows.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.LockUserOrderForm.未找到订单项目！UOFID:" + dtuof.Rows[0]["UOFID"].ToString());
                    brdv.ErrorInfo = "未找到订单项目！";
                    brdv.success = false;
                    return brdv;
                }
            }
            brdv.success = true;
            return brdv;
        }

        public BaseResultDataValue UnLockUserOrderForm(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisOrgID, string WeblisOrgName, string ConsumerAreaID)
        {
            DataTable dtuof = new DataTable();
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (dal.UnLockOSUserOrderFormByPayCode(PayCode, EmpAccount, ZhiFangUserID, WeblisOrgID, WeblisOrgName,null) <= 0)
            {
                brdv.success = false;
                dtuof = dal.GetOSUserOrderFormByPayCode(PayCode);
                if (dtuof == null || dtuof.Rows.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("UnLockUserOrderForm.UnLockUserOrderForm.错误的消费码！PayCode:" + PayCode);
                    brdv.ErrorInfo = "错误的消费码！";
                    brdv.success = false;
                    return brdv;
                }
                string status = dtuof.Rows[0]["Status"].ToString();
                if (status != UserOrderFormStatus.使用中.Key)
                {
                    ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.UnLockUserOrderForm.状态错误！PayCode:" + PayCode + ",status:" + UserOrderFormStatus.GetStatusDic()[status].Name);
                    if (status == UserOrderFormStatus.待缴费.Key)
                    {
                        brdv.ErrorInfo = "错误的消费码:订单未缴费！";
                    }
                    if (status == UserOrderFormStatus.已交费.Key)
                    {
                        brdv.ErrorInfo = "错误的消费码:订单未开始使用！";
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
            }
            brdv.success = true;
            return brdv;
        }

        public BaseResultDataValue CheckPayCodeIsUseing(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisOrgID, string WeblisOrgName)
        {
            DataTable dtuof = new DataTable();
            BaseResultDataValue brdv = new BaseResultDataValue();
            dtuof = dal.GetOSUserOrderFormByPayCode(PayCode);
            if (dtuof == null || dtuof.Rows.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("UnLockUserOrderForm.UnLockUserOrderForm.错误的消费码！PayCode:" + PayCode);
                brdv.ErrorInfo = "错误的消费码！";
                brdv.success = false;
                return brdv;
            }
            string status = dtuof.Rows[0]["Status"].ToString();
            if (status != UserOrderFormStatus.使用中.Key)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.UnLockUserOrderForm.状态错误！PayCode:" + PayCode + ",status:" + UserOrderFormStatus.GetStatusDic()[status].Name);
                if (status == UserOrderFormStatus.待缴费.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单未缴费！";
                }
                if (status == UserOrderFormStatus.已交费.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单未被锁定！";
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
            else
            {
                if (EmpAccount != dtuof.Rows[0]["EmpAccount"].ToString()|| ZhiFangUserID != dtuof.Rows[0]["EmpID"].ToString())
                {
                    brdv.ErrorInfo = "订单已被其它采样人员使用！";
                    brdv.success = false;
                    return brdv;
                }
               
                if (WeblisOrgID != dtuof.Rows[0]["WeblisSourceOrgID"].ToString()|| WeblisOrgName != dtuof.Rows[0]["WeblisSourceOrgName"].ToString())
                {
                    brdv.ErrorInfo = "订单已被其它采血单位使用！";
                    brdv.success = false;
                    return brdv;
                }
            }
            brdv.success = true;
            return brdv;
        }

        public BaseResultDataValue SearchUnConsumerUserOrderFormList(string paycode, string EmpAccount, string ZhiFangUserID, string weblisOrgID, string weblisOrgName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            List<OSUserOrderForm> list = new List<OSUserOrderForm>();
            list= dal.SearchUnConsumerUserOrderFormList(paycode, EmpAccount, ZhiFangUserID, weblisOrgID, weblisOrgName);
            if (list != null && list.Count>0)
            {
                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.Tools.JsonHelp.JsonDotNetSerializer(list);
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "未查找到相关订单！";
            }
            return brdv;
        }
    }

}
