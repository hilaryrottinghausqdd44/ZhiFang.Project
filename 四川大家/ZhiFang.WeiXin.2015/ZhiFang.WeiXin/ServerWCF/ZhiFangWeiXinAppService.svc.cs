using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.BusinessObject;
using ZhiFang.WeiXin.BusinessObject.LabObject;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.ServerContract;

namespace ZhiFang.WeiXin.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ZhiFangWeiXinAppService : IZhiFangWeiXinAppService
    {
        IBLL.IBOSUserOrderForm IBOSUserOrderForm { get; set; }
        IBLL.IBBWeiXinAccount IBBWeiXinAccount { get; set; }
        IBLL.IBOSDoctorOrderForm IBOSDoctorOrderForm { get; set; }
        IBLL.IBBSearchAccountReportForm IBBSearchAccountReportForm { get; set; }
        IBLL.IBRFReportFormIndexInfo IBRFReportFormIndexInfo { get; set; }
        IBLL.IBOSManagerRefundForm IBOSManagerRefundForm { get; set; }
        IBLL.IBBLabTestItem IBBLabTestItem { get; set; }

        public BaseResultBool ST_UDTO_OSUserOrderFormRefundU(string OrderFormCode, string MessageStr)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                //if (Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserName) == null || Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserName).Trim() == "")
                //{
                //    tempBaseResultBool.success = false;
                //    tempBaseResultBool.ErrorInfo = "用户名读取错误！";
                //    ZhiFang.Common.Log.Log.Error("ST_UDTO_OSUserOrderFormRefundU,用户名读取错误！");
                //    return tempBaseResultBool;
                //}
                string UserName = "";
                if (Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserOpenID) == null || Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserOpenID).Trim() == "")
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "用户OID读取错误！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_OSUserOrderFormRefundU,UserOpenID读取错误！");
                    return tempBaseResultBool;
                }
                //tempBaseResultBool = IBOSUserOrderForm.OSUserOrderFormRefundApplyByUser(OrderFormCode, MessageStr, Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserName), Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserOpenID));
                tempBaseResultBool = IBOSUserOrderForm.OSUserOrderFormRefundApplyByUser(OrderFormCode, MessageStr, UserName, Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserOpenID));
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "服务错误！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_OSUserOrderFormRefundU,服务错误！" + ex.ToString());
            }
            return tempBaseResultBool;
        }

        private bool CheckUserInfo()
        {
            if (Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserOpenID) == null || Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserOpenID).Trim() == "")
            {
                return false;
            }
            return true;
        }

        public BaseResultDataValue WXAS_BA_Login(string password)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = false;
            string id = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
            //id = "4755749206623199738";
            if (!string.IsNullOrEmpty(id))
            {
                BWeiXinAccount bweiXinAccount = IBBWeiXinAccount.Get(long.Parse(id));
                if (bweiXinAccount != null)
                {
                    if (string.IsNullOrEmpty(bweiXinAccount.PassWord) && string.IsNullOrEmpty(password))
                        baseResultDataValue.success = true;
                    else
                    {
                        if (!string.IsNullOrEmpty(password))
                            password = SecurityHelp.MD5Encrypt(password, SecurityHelp.PWDMD5Key);
                        baseResultDataValue.success = (bweiXinAccount.PassWord == password);
                    }
                    if (baseResultDataValue.success)
                    {
                        //SetUserSession(bweiXinAccount);
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "帐号密码错误！";
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "帐号信息不存在！";
                    //ZhiFang.Common.Log.Log.Debug("帐号信息不存在！");
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法从Cookie获取到账号信息！";
                //ZhiFang.Common.Log.Log.Debug("无法从Cookie获取到账号信息！");
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_ChangePassword(string oldPassword, string newPassword)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrEmpty(newPassword) && (newPassword.Trim().Length > 0))
            {
                string id = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
                //id = "4755749206623199738";
                if (!string.IsNullOrEmpty(id))
                {
                    BWeiXinAccount bweiXinAccount = IBBWeiXinAccount.Get(long.Parse(id));
                    if (bweiXinAccount != null)
                    {
                        if (string.IsNullOrEmpty(bweiXinAccount.PassWord) && string.IsNullOrEmpty(oldPassword))
                            baseResultDataValue.success = true;
                        else
                        {
                            if (!string.IsNullOrEmpty(oldPassword))
                                oldPassword = SecurityHelp.MD5Encrypt(oldPassword, SecurityHelp.PWDMD5Key);
                            baseResultDataValue.success = (bweiXinAccount.PassWord == oldPassword);
                        }
                        if (baseResultDataValue.success)
                        {
                            IBBWeiXinAccount.Entity = bweiXinAccount;
                            IBBWeiXinAccount.Entity.PassWord = SecurityHelp.MD5Encrypt(newPassword, SecurityHelp.PWDMD5Key);
                            baseResultDataValue.success = IBBWeiXinAccount.Edit();
                        }
                        else
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "帐号密码错误！";
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "帐号信息不存在！";
                        //ZhiFang.Common.Log.Log.Debug("帐号信息不存在！");
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法从Cookie获取到账号信息！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新密码不能为空或为空格！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_ChangePasswordByVerificationCode(string VerificationCode, string newPassword)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (!string.IsNullOrEmpty(newPassword) && (newPassword.Trim().Length > 0))
            {
                if (VerificationCode == null || VerificationCode.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "验证码为空！";
                    return brdv;
                }

                if (SessionHelper.GetSessionObjectValue(Entity.SysDicCookieSession.VerificationCode) == null || SessionHelper.GetSessionObjectValue(Entity.SysDicCookieSession.VerificationCode).ToString().Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "验证码为空！请重新获取！";
                    SessionHelper.SetSessionValue(Entity.SysDicCookieSession.VerificationCode, "");
                    SessionHelper.SetSessionValue(Entity.SysDicCookieSession.VerificationCodeDateTime, null);
                    return brdv;
                }

                if (SessionHelper.GetSessionObjectValue(Entity.SysDicCookieSession.VerificationCode).ToString().Trim() != VerificationCode.Trim())
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "验证码不正确！请重新获取！";
                    SessionHelper.SetSessionValue(Entity.SysDicCookieSession.VerificationCode, "");
                    SessionHelper.SetSessionValue(Entity.SysDicCookieSession.VerificationCodeDateTime, null);
                    return brdv;
                }

                if (SessionHelper.GetSessionObjectValue(Entity.SysDicCookieSession.VerificationCodeDateTime)==null|| DateTime.Now>((DateTime)SessionHelper.GetSessionObjectValue(Entity.SysDicCookieSession.VerificationCodeDateTime)))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "验证码过期！请重新获取！";
                    SessionHelper.SetSessionValue(Entity.SysDicCookieSession.VerificationCode, "");
                    SessionHelper.SetSessionValue(Entity.SysDicCookieSession.VerificationCodeDateTime, null);
                    return brdv;
                }
                string id = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
                //id = "4755749206623199738";
                if (!string.IsNullOrEmpty(id))
                {
                    BWeiXinAccount bweiXinAccount = IBBWeiXinAccount.Get(long.Parse(id));
                    if (bweiXinAccount != null)
                    {
                        IBBWeiXinAccount.Entity = bweiXinAccount;
                        IBBWeiXinAccount.Entity.PassWord = SecurityHelp.MD5Encrypt(newPassword, SecurityHelp.PWDMD5Key);
                        brdv.success = IBBWeiXinAccount.Edit();
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "帐号信息不存在！";
                        //ZhiFang.Common.Log.Log.Debug("帐号信息不存在！");
                    }
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "无法从Cookie获取到账号信息！";
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "新密码不能为空或为空格！";
            }
            return brdv;
        }

        public BaseResultDataValue GetVerificationCode(string MobileCode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string id = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
                //id = "4755749206623199738";
                if (!string.IsNullOrEmpty(id))
                {
                    BWeiXinAccount bweiXinAccount = IBBWeiXinAccount.Get(long.Parse(id));
                    if (bweiXinAccount != null)
                    {
                        if (string.IsNullOrEmpty(bweiXinAccount.MobileCode) || string.IsNullOrEmpty(MobileCode))
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "当前账户的手机号未填写！";
                            return brdv;
                        }
                        if (bweiXinAccount.MobileCode.Trim() != MobileCode.Trim())
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "输入的手机号同当前账户的手机号不匹配！";
                            return brdv;

                        }
                        string verificationcode = Common.GUIDHelp.RandomInt(6);
                        int verificationcodedatetime = 10;
                        SessionHelper.SetSessionValue(Entity.SysDicCookieSession.VerificationCode, verificationcode);
                        SessionHelper.SetSessionValue(Entity.SysDicCookieSession.VerificationCodeDateTime, DateTime.Now.AddSeconds(verificationcodedatetime * 60));
                        int sdkappid = 1400056809;
                        string appkey = "18d466f4362df7f391b77ca4e0240680";
                        int tmplId = 68357;
                        ZhiFang.WeiXin.BusinessObject.QCloud.SmsSender singleSender = new ZhiFang.WeiXin.BusinessObject.QCloud.SmsSender(sdkappid, appkey);

                        List<string> templParams = new List<string>();
                        templParams.Add(verificationcode);
                        templParams.Add(verificationcodedatetime.ToString());
                        // 指定模板单发
                        // 假设短信模板内容为：测试短信，{1}，{2}，{3}，上学。
                        BusinessObject.QCloud.SmsSingleSenderResult singleResult = singleSender.SendWithParam("86", MobileCode, tmplId, templParams, "", "", "");
                        ZhiFang.Common.Log.Log.Debug("MobileSendTest.singleResult:" + singleResult.ToString() + ",tmplId:" + tmplId);
                        if (singleResult.result == 0)
                        {
                            brdv.success = true;
                            return brdv;
                        }
                        else
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "发送验证码异常！";
                            ZhiFang.Common.Log.Log.Debug("GetVerificationCode.异常：singleResult.errmsg："+ singleResult.errmsg );
                        }
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "帐号信息不存在！";
                        //ZhiFang.Common.Log.Log.Debug("帐号信息不存在！");
                    }
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能检测到微信用户身份！";
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取验证码异常！";
                ZhiFang.Common.Log.Log.Debug("GetVerificationCode.异常：" + e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue WXAS_BA_GetPatientInformation()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string id = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
            //id = "4755749206623199738";
            if (!string.IsNullOrEmpty(id))
            {
                BWeiXinAccount bweiXinAccount = IBBWeiXinAccount.Get(long.Parse(id));
                if (bweiXinAccount != null)
                {
                    string fields = "BWeiXinAccount_Name,BWeiXinAccount_UserName,BWeiXinAccount_HeadImgUrl,BWeiXinAccount_WeiXinAccount,BWeiXinAccount_LastLoginTime,BWeiXinAccount_LoginInputPasswordFlag,BWeiXinAccount_WeiXinUserID,BWeiXinAccount_SexID,BWeiXinAccount_Birthday,BWeiXinAccount_MobileCode,BWeiXinAccount_IDNumber,BWeiXinAccount_Id";
                    //string fields = "Name,UserName,HeadImgUrl,WeiXinAccount,LastLoginTime,LoginInputPasswordFlag";
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BWeiXinAccount>(bweiXinAccount, true);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "患者信息不存在！";
                    //ZhiFang.Common.Log.Log.Debug("患者信息不存在！");
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法从Cookie获取到患者微信账号ID信息！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_AccountInfoByWinXinAccount(string fields)
        {
            return null;
        }

        public BaseResultDataValue WXAS_BA_IsPasswordLogin(bool isPassword)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string id = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
            //id = "4755749206623199738";
            if (!string.IsNullOrEmpty(id))
            {
                BWeiXinAccount bweiXinAccount = IBBWeiXinAccount.Get(long.Parse(id));
                if (bweiXinAccount != null)
                {
                    IBBWeiXinAccount.Entity = bweiXinAccount;
                    IBBWeiXinAccount.Entity.LoginInputPasswordFlag = isPassword;
                    baseResultDataValue.success = IBBWeiXinAccount.Edit();
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "患者信息不存在！";
                    //ZhiFang.Common.Log.Log.Debug("患者信息不存在！");
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法从Cookie获取到患者ID信息！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_GetWinXinAccountInfo(string account)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrEmpty(account))
            {
                IList<BWeiXinAccount> listWeiXinAccount = IBBWeiXinAccount.SearchListByHQL(" bweixinaccount.WeiXinAccount=\'" + account + "\'");
                if (listWeiXinAccount != null && listWeiXinAccount.Count == 1)
                {
                    string fields = "Id,Name,UserName,SexID,HeadImgUrl,Birthday,MobileCode,WeiXinUserID,WeiXinAccount";
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BWeiXinAccount>(listWeiXinAccount[0], false);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    if (listWeiXinAccount == null || listWeiXinAccount.Count == 0)
                        baseResultDataValue.ErrorInfo = "微信账号信息不存在！";
                    else if (listWeiXinAccount.Count > 1)
                        baseResultDataValue.ErrorInfo = "微信账号信息不唯一！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "微信账号参数为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_GetWinXinAccountInfoByFields(string account, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrEmpty(account))
            {
                IList<BWeiXinAccount> listWeiXinAccount = IBBWeiXinAccount.SearchListByHQL(" bweixinaccount.WeiXinAccount=\'" + account + "\'");
                if (listWeiXinAccount != null && listWeiXinAccount.Count == 1)
                {
                    //fields = "Name,UserName,SexID,HeadImgUrl,Birthday,MobileCode,WeiXinUserID,WeiXinAccount";
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BWeiXinAccount>(listWeiXinAccount[0], false);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    if (listWeiXinAccount == null || listWeiXinAccount.Count == 0)
                        baseResultDataValue.ErrorInfo = "微信账号信息不存在！";
                    else if (listWeiXinAccount.Count > 1)
                        baseResultDataValue.ErrorInfo = "微信账号信息不唯一！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "微信账号参数为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_GetOSUserOrderFormByID(long id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            //id = 3;
            OSUserOrderForm osUserOrderForm = IBOSUserOrderForm.Get(id);
            if (osUserOrderForm != null)
            {
                Entity.ViewObject.Response.OSUserOrderFormVO voEntity = IBOSUserOrderForm.VO_OSUserOrderForm(osUserOrderForm);
                //用户订单编号、医生、患者、消费单编号、订单状态、消费码、备注、市场价格、大家价格、折扣价格、折扣率、实际金额、咨询费、缴费时间
                string fields = "UFC,DN,UN,OCC,SS,PC,MM,MP,GMP,DP,DT,PE,AP,PT";
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<Entity.ViewObject.Response.OSUserOrderFormVO>(voEntity, false);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法根据订单ID获取订单信息";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_SearchOSUserOrderForm(int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string weiXinUserID = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
            //weiXinUserID = "4755749206623199738";
            if (!string.IsNullOrEmpty(weiXinUserID))
            {
                EntityList<OSUserOrderForm> entityList = IBOSUserOrderForm.SearchListByHQL(" osuserorderform.UserWeiXinUserID=" + weiXinUserID, page, limit);
                if (entityList != null && entityList.count > 0)
                {
                    EntityList<Entity.ViewObject.Response.OSUserOrderFormVO> voList = IBOSUserOrderForm.VO_OSUserOrderFormList(entityList.list);
                    //Id、用户订单编号、医生、患者、消费单编号、订单状态、消费码、备注、市场价格、大家价格、折扣价格、折扣率、实际金额、咨询费、缴费时间
                    string fields = "Id,UFC,DN,UN,OCC,SS,PC,MM,MP,GMP,DP,DT,PE,AP,PT";
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Entity.ViewObject.Response.OSUserOrderFormVO>(voList, false);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法从Cookie获取到患者微信账号ID信息！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_SearchOSUserOrderFormByStatusStr(int page, int limit, string statusStr)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (String.IsNullOrEmpty(statusStr))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "用户订单状态值为空";
                return baseResultDataValue;
            }
            string weiXinUserID = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
            if (string.IsNullOrEmpty(weiXinUserID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法从Cookie获取到患者微信账号ID信息！";
                return baseResultDataValue;
            }
            string hqlWhere = " osuserorderform.UserWeiXinUserID=" + weiXinUserID;
            string orderstr = " DataAddTime desc ";
            if (statusStr.Contains(","))
            {
                hqlWhere = hqlWhere + " and osuserorderform.Status in (" + statusStr + ")";
            }
            else
            {
                hqlWhere = hqlWhere + " and osuserorderform.Status=" + statusStr;
                if (statusStr == UserOrderFormStatus.已交费.ToString())
                {
                    orderstr = " PayTime desc ";
                }
                if (statusStr == UserOrderFormStatus.完全使用.ToString())
                {
                    orderstr = " ConsumerFinishedTime desc ";
                }
            }
            EntityList<OSUserOrderForm> entityList = IBOSUserOrderForm.SearchListByHQL(hqlWhere, orderstr, page, limit);
            if (entityList != null && entityList.count > 0)
            {
                EntityList<Entity.ViewObject.Response.OSUserOrderFormVO> voList = IBOSUserOrderForm.VO_OSUserOrderFormList(entityList.list);
                //Id、用户订单编号、医生、患者、消费单编号、订单状态、消费码、备注、市场价格、大家价格、折扣价格、折扣率、实际金额、咨询费、缴费时间
                string fields = "Id,UFC,DN,UN,OCC,SS,PC,MM,MP,GMP,DP,DT,PE,AP,PT,CFT,DataAddTime";
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Entity.ViewObject.Response.OSUserOrderFormVO>(voList, false);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }

            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_SearchDoctorOrderForm(int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string weiXinUserID = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
            //weiXinUserID = "4755749206623199738";
            if (!string.IsNullOrEmpty(weiXinUserID))
            {
                EntityList<OSDoctorOrderForm> entityList = IBOSDoctorOrderForm.SearchListByHQL(" osdoctororderform.UserWeiXinUserID=" + weiXinUserID, CommonServiceMethod.GetSortHQL("[{\"property\":\"OSDoctorOrderForm_DataAddTime\",\"direction\":\"DESC\"}]"), page, limit);
                if (entityList != null && entityList.count > 0)
                {
                    EntityList<Entity.ViewObject.Response.OSDoctorOrderFormVO> voList = IBOSDoctorOrderForm.VO_OSDoctorOrderFormList(entityList.list);
                    //ID、医生、患者、科室、性别、年龄、年龄单位、病历号、备注、医嘱单状态、医院
                    string fields = "Id,DN,UN,DPN,SD,Age,AUN,PN,MM,SS,HN,DataAddTime";
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Entity.ViewObject.Response.OSDoctorOrderFormVO>(voList, false);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法从Cookie获取到患者微信账号ID信息！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue UserOrderFormConfirm(long Id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.UserOpenID) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.UserOpenID).Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取用户！";
                    ZhiFang.Common.Log.Log.Error("UserOrderFormConfirm.无法获取用户!UserOpenID为空！");
                    return baseResultDataValue;
                }
                if (Id <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "确认医嘱单失败！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_UserOrderFormConfirm.确认医嘱单失败！Id:" + Id);
                    return baseResultDataValue;
                }
                baseResultDataValue = IBOSUserOrderForm.AddUserOrderFormConfirmByOrderFormId(Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.UserOpenID), Id);
                if (baseResultDataValue.success)
                {
                    return baseResultDataValue;
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "确认医嘱单失败！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_UserOrderFormConfirm.确认医嘱单失败！Id:" + Id);
                }
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "确认医嘱单失败！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_UserOrderFormConfirm.确认医嘱单失败！Id:" + Id + "@异常：" + e.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_SearchReportFormByPC(string PayCode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                //if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.UserOpenID) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.UserOpenID).Trim() == "")
                //{
                //    baseResultDataValue.success = false;
                //    baseResultDataValue.ErrorInfo = "无法获取用户！";
                //    ZhiFang.Common.Log.Log.Error("WXAS_BA_SearchReportFormByPC.无法获取用户!UserOpenID为空！");
                //    return baseResultDataValue;
                //}
                if (PayCode == null || PayCode.Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取参数！";
                    ZhiFang.Common.Log.Log.Error("WXAS_BA_SearchReportFormByPC.无法获取参数PayCode！");
                    return baseResultDataValue;
                }
                List<BSearchAccountReportForm> tempList = new List<BSearchAccountReportForm>();
                List<AppRFObject> LARFO = new List<AppRFObject>();

                string hqlwhere = " TakeNo='" + PayCode + "' ";
                ZhiFang.Common.Log.Log.Debug("WXAS_BA_SearchReportFormByPC.hqlwhere:" + hqlwhere);
                tempList = IBBSearchAccountReportForm.SearchListByHQL(hqlwhere, 1, 1000).list.ToList();
                foreach (var bsarf in tempList)
                {
                    AppRFUserInfo info = new AppRFUserInfo() { CheckListNumber = bsarf.VisNo, HospitalName = bsarf.HospitalName, PatientName = bsarf.Name, ReportId = bsarf.ReportFormIndexID.ToString(), ReportType = bsarf.ReportFormType };
                    if (bsarf.ReportFormTime.HasValue)
                    {
                        info.ReportTime = bsarf.ReportFormTime.Value.ToString("yyyy-MM-dd hh:mm:ss");
                    }
                    if (bsarf.COLLECTDATE.HasValue)
                    {
                        info.VisitTime = bsarf.COLLECTDATE.Value.ToString("yyyy-MM-dd");
                    }
                    if (bsarf.PatNo != null && bsarf.PatNo.Trim() != "")
                    {
                        info.PatNumber = bsarf.PatNo;
                    }
                    AppRFObject ARFO = new AppRFObject() { info = info, list = new List<AppRIResult>() };
                    LARFO.Add(ARFO);
                }
                baseResultDataValue.success = true;
                baseResultDataValue.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(LARFO);
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "报告单查询失败！";
                ZhiFang.Common.Log.Log.Error("WXAS_BA_SearchReportFormByPC.报告单查询失败！PayCode:" + PayCode + "@异常：" + e.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue Get_ReportFormPDFURLById(string ReportFormId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string UploadDate = null;
                //string TestDate = null;
                #region 查找pdf
                string pdfPath = ConfigHelper.GetConfigString("ReportPDFConfigPathAll");
                string pdfServicePath = ConfigHelper.GetConfigString("ReportFormPDFServiceUrl") + ConfigHelper.GetConfigString("ReportFormFilesDir");
                if (string.IsNullOrEmpty(pdfPath))
                {
                    ZhiFang.Common.Log.Log.Error("未配置报告路径！ReportPDFConfigPathAll为空！");
                }
                if (string.IsNullOrEmpty(pdfServicePath))
                {
                    ZhiFang.Common.Log.Log.Error("未配置报告服务路径！ReportFormPDFServiceUrl为空！");
                }
                var RFList = IBRFReportFormIndexInfo.SearchListByHQL(" ReportFormId='" + ReportFormId + "' ");
                if (RFList != null && RFList.Count > 0)
                {
                    if (RFList[0].UploadDate.HasValue)
                    {
                        ZhiFang.Common.Log.Log.Info("Get_ReportFormPDFURLById.UploadDate:" + RFList[0].UploadDate.Value.ToString("yyyy/MM/dd") + ",根据UploadDate年月日查找PDF文件所在目录");
                        //TestDate = UploadDate.Split(' ')[0];

                        int Year = RFList[0].UploadDate.Value.Year;
                        int Month = RFList[0].UploadDate.Value.Month;
                        int Day = RFList[0].UploadDate.Value.Day;
                        pdfPath = pdfPath + "\\" + Year + "\\" + Month + "\\" + Day + "\\";
                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.报告:" + ReportFormId + " 所在目录:" + pdfPath);
                        foreach (string pdfFile in Directory.GetFiles(pdfPath, "*.pdf"))
                        {
                            string FileName = Path.GetFileName(pdfFile);
                            ReportFormId = ReportFormId.Replace(':', '：');//替换成中文的冒号,因为英文格式的冒号在文件名里面是非法的
                            //if (FileName.IndexOf("T" + ReportFormId) > -1)//实验室抬头
                            //{
                            //    pdfPath += FileName;
                            //    ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.成功找到PDF文件路径:" + pdfPath);
                            //    baseResultDataValue.ResultDataValue = pdfServicePath + "Report/" + Year + "/" + Month + "/" + Day + "/" + FileName;
                            //    break;
                            //}
                            if (FileName.IndexOf(ReportFormId) > -1&& FileName.IndexOf("T" + ReportFormId) <0)//中心实验室抬头
                            {
                                pdfPath += FileName;
                                ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.成功找到PDF文件路径:" + pdfPath);
                                baseResultDataValue.ResultDataValue = pdfServicePath + "Report/" + Year + "/" + Month + "/" + Day + "/" + FileName;
                                break;
                            }
                        }
                        if (baseResultDataValue.ResultDataValue != null && baseResultDataValue.ResultDataValue.Trim() != "")
                        {
                            baseResultDataValue.success = true;
                        }
                        else
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "报告单查询失败！未找到报告文件！";
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "报告单查询失败！未找到报告上传时间！";
                        ZhiFang.Common.Log.Log.Error("Get_ReportFormPDFURLById. 报告单查询失败！未找到报告上传时间！ReportFormId：" + ReportFormId);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "报告单查询失败！未找到报告单数据！";
                    ZhiFang.Common.Log.Log.Error("Get_ReportFormPDFURLById.报告单查询失败！未找到报告单数据！ReportFormId：" + ReportFormId);
                }

                #endregion

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "报告单查询失败！";
                ZhiFang.Common.Log.Log.Error("Get_ReportFormPDFURLById.报告单查询失败！异常:" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue Get_ReportFormPDFURLByIndexId(string ReportFormIndexId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string UploadDate = null;
                //string TestDate = null;
                #region 查找pdf
                string pdfPath = System.Configuration.ConfigurationManager.AppSettings["ReportPDFConfigPathAll"];
                string pdfServicePath = System.Configuration.ConfigurationManager.AppSettings["ReportFormPDFServiceUrl"];
                if (string.IsNullOrEmpty(pdfPath))
                {
                    ZhiFang.Common.Log.Log.Error("未配置报告路径！ReportPDFConfigPathAll为空！");
                }
                if (string.IsNullOrEmpty(pdfServicePath))
                {
                    ZhiFang.Common.Log.Log.Error("未配置报告服务路径！ReportFormPDFServiceUrl为空！");
                }
                var RFList = IBRFReportFormIndexInfo.SearchListByHQL(" Id='" + ReportFormIndexId + "' ");
                if (RFList != null && RFList.Count > 0)
                {
                    return Get_ReportFormPDFURLById(RFList[0].ReportFormID);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "报告单查询失败！未找到报告单数据！";
                    ZhiFang.Common.Log.Log.Error("Get_ReportFormPDFURLByIndexId.报告单查询失败！未找到报告单数据！ReportFormIndexId：" + ReportFormIndexId);
                }

                #endregion

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "报告单查询失败！";
                ZhiFang.Common.Log.Log.Error("Get_ReportFormPDFURLByIndexId.报告单查询失败！异常:" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_SearchReportFormByRbac(int UserSearchReportDateRoundType, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                EntityList<BSearchAccountReportForm> tempList = new EntityList<BSearchAccountReportForm>();

                EntityList<AppRFObject> elarfo = new EntityList<AppRFObject>();
                elarfo.list = new List<AppRFObject>();
                //List<AppRFObject> LARFO = new List<AppRFObject>();
                if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.UserOpenID) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.UserOpenID).Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取用户信息！";
                    ZhiFang.Common.Log.Log.Error("WXAS_BA_SearchReportFormByRbac.无法获取用户信息!UserOpenID为空！");
                    return baseResultDataValue;
                }

                if (UserSearchReportDateRoundType > 3 || UserSearchReportDateRoundType <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "参数错误！";
                    ZhiFang.Common.Log.Log.Error("WXAS_BA_SearchReportFormByRbac.参数错误!UserSearchReportDateRoundType错误！");
                    return baseResultDataValue;
                }

                tempList = IBBSearchAccountReportForm.SearchListByUserPayCodeAndDateRoundType(Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.UserOpenID), (UserSearchReportDataRoundType)UserSearchReportDateRoundType, page, limit);
                elarfo.count = tempList.count;
                if (tempList != null && tempList.list != null && tempList.list.Count > 0)
                {
                    foreach (var bsarf in tempList.list)
                    {
                        AppRFUserInfo info = new AppRFUserInfo() { CheckListNumber = bsarf.VisNo, HospitalName = bsarf.HospitalName, PatientName = bsarf.Name, ReportId = bsarf.ReportFormIndexID.ToString(), ReportType = bsarf.ReportFormType };
                        if (bsarf.ReportFormTime.HasValue)
                        {
                            info.ReportTime = bsarf.ReportFormTime.Value.ToString("yyyy-MM-dd hh:mm:ss");
                        }
                        if (bsarf.COLLECTDATE.HasValue)
                        {
                            info.VisitTime = bsarf.COLLECTDATE.Value.ToString("yyyy-MM-dd");
                        }
                        if (bsarf.PatNo != null && bsarf.PatNo.Trim() != "")
                        {
                            info.PatNumber = bsarf.PatNo;
                        }
                        AppRFObject ARFO = new AppRFObject() { info = info, list = new List<AppRIResult>() };
                        elarfo.list.Add(ARFO);
                    }
                    baseResultDataValue.success = true;
                    baseResultDataValue.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(elarfo);
                }
                else
                {
                    baseResultDataValue.success = true;
                    baseResultDataValue.ResultDataValue = "";
                    //ZhiFang.Common.Log.Log.Error("WXAS_BA_SearchReportFormByRbac.无报告！UserSearchReportDateRoundType:" + UserSearchReportDateRoundType );
                }
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "报告单查询失败！";
                ZhiFang.Common.Log.Log.Error("WXAS_BA_SearchReportFormByRbac.报告单查询失败！UserSearchReportDateRoundType:" + UserSearchReportDateRoundType + "@异常：" + e.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_SearchRefundFormInfoByUOFCode(string UOFCode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string weiXinUserID = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
            //weiXinUserID = "4755749206623199738";
            if (string.IsNullOrEmpty(weiXinUserID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法从获取到患者微信账号信息！";
                ZhiFang.Common.Log.Log.Error("WXAS_BA_SearchRefundFormInfoByUOFCode,无法从Cookie获取到患者微信账号ID信息！");
                return baseResultDataValue;
            }

            List<Entity.ViewObject.Response.RFVO> entityList = IBOSManagerRefundForm.SearchRefundFormInfoByUOFCode(UOFCode, weiXinUserID);
            if (entityList != null && entityList.Count > 0)
            { 
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("WXAS_BA_SearchRefundFormInfoByUOFCode,序列化错误:" + ex.ToString());
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_BA_UserReadAgreement()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string weiXinUserID = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
            if (string.IsNullOrEmpty(weiXinUserID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法从获取到患者微信账号信息！";
                ZhiFang.Common.Log.Log.Error("WXAS_BA_UserReadAgreement,无法从Cookie获取到患者微信账号ID信息！");
                return baseResultDataValue;
            }


            try
            {
                if (IBBWeiXinAccount.UserReadAgreement(weiXinUserID))
                {
                    ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.ReadAgreement, "1");
                    baseResultDataValue.ResultDataValue = "True";
                }
                else
                {
                    ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.ReadAgreement, "0");
                    baseResultDataValue.ResultDataValue = "False";
                }

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("WXAS_BA_UserReadAgreement,序列化错误:" + ex.ToString());
            }

            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_UDTO_SearchOSTestItemByAreaID(int page, int limit, string where, string sort)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string AreaID = "";
            if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID).Trim() == "")
            {
                if (string.IsNullOrEmpty(ZhiFang.Common.Public.ConfigHelper.GetConfigString("DefaultAreaID")))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取区域或者默认区域！";
                    ZhiFang.Common.Log.Log.Error("WXAS_UDTO_SearchOSTestItemByAreaID.无法获取区域或者默认区域!AreaID为空！");
                    return baseResultDataValue;
                }
                else
                {
                    AreaID = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DefaultAreaID").Trim();
                }
            }
            else
            {
                AreaID = Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID);
            }

            EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> ELBLTIVO = IBBLabTestItem.SearchOSBLabTestItemByAreaID(AreaID, page, limit, sort, where);
            if (ELBLTIVO != null)
            {
                try
                {
                    baseResultDataValue.ResultDataValue = Common.JsonSerializer.JsonDotNetSerializer(ELBLTIVO);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "程序错误！";
                    ZhiFang.Common.Log.Log.Error("WXAS_UDTO_SearchOSTestItemByAreaID.程序错误:" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取组套列表！";
                ZhiFang.Common.Log.Log.Error("WXAS_UDTO_SearchOSTestItemByAreaID.无法获取组套列表!");

            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXAS_UDTO_SaveOSDoctorOrderForm(Entity.ViewObject.Request.OSDoctorOrderFormVO entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string AreaID = "";
                if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID).Trim() == "")
                {
                    if (string.IsNullOrEmpty(ZhiFang.Common.Public.ConfigHelper.GetConfigString("DefaultAreaID")))
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "无法获取区域或者默认区域！";
                        ZhiFang.Common.Log.Log.Error("WXAS_UDTO_SearchOSTestItemByAreaID.无法获取区域或者默认区域!AreaID为空！");
                        return baseResultDataValue;
                    }
                    else
                    {
                        AreaID = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DefaultAreaID").Trim();
                    }
                }
                else
                {
                    AreaID = Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID);
                }
                entity.AreaID= long.Parse(AreaID);
                baseResultDataValue = IBOSDoctorOrderForm.AddOSDoctorOrderFormVOByUser((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity);


                if (baseResultDataValue.success)
                {
                    baseResultDataValue.ResultDataValue = "开单成功！";//CommonServiceMethod.GetAddMethodResultStr(IBBAccountType.Entity);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "程序错误！";
                ZhiFang.Common.Log.Log.Error("DS_UDTO_SaveOSDoctorOrderForm.程序错误:" + ex.ToString());
                //throw new Exception(ex.Message);
            }

            return baseResultDataValue;
        }
    }
}
