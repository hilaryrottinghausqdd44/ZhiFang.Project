using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.RBAC;
using ZhiFang.LabInformationIntegratePlatform.BusinessObject.Utils;
using ZhiFang.LabInformationIntegratePlatform.ServerContract;
using ZhiFang.LIIP.WeiXin.Mini;
using ZhiFang.ServerContract;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WXMiniService : RBACService, IWXMiniService
    {
        public IBLL.LIIP.IBWXWeiXinAccount IBWXWeiXinAccount { get; set; }

        public IBLL.LIIP.IBSAccountRegister IBSAccountRegister { get; set; }

        public IBLL.LIIP.IBBHospital IBBHospital { get; set; }


        //public override bool RBAC_BA_Login(string strUserAccount, string strPassWord, bool isValidate)
        //{
        //    try
        //    {
        //        if (strUserAccount.Trim() == Entity.RBAC.DicCookieSession.SuperUser && strPassWord == Entity.RBAC.DicCookieSession.SuperUserPwd)
        //        {
        //            ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ".RBAC_BA_Login:非法用户登录！IP:" + IPHelper.GetClientIP());
        //            return false;
        //        }
        //        return base.RBAC_BA_Login(strUserAccount, strPassWord, isValidate);
        //    }
        //    catch (Exception e)
        //    {
        //        ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ".RBAC_BA_Login:异常：" + e.ToString() + ".IP:" + IPHelper.GetClientIP());
        //        return false;
        //    }
        //}

        public BaseResultDataValue RBAC_BA_LoginAndBindingByCode(string strUserAccount, string strPassWord, bool isValidate, string Code)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $".RBAC_BA_LoginAndBindingByCode:strUserAccount:{strUserAccount},strPassWord:{strPassWord},isValidate:{isValidate},Code:{Code},！IP:" + IPHelper.GetClientIP());
                if (strUserAccount.Trim() == Entity.RBAC.DicCookieSession.SuperUser && strPassWord == Entity.RBAC.DicCookieSession.SuperUserPwd)
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ".RBAC_BA_LoginAndBindingByCode:非法用户登录！IP:" + IPHelper.GetClientIP());
                    return new BaseResultDataValue() { success = false, ErrorInfo = "非法用户登录!" };
                }
                if (string.IsNullOrEmpty(Code))
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ".RBAC_BA_LoginAndBindingByCode:Code参数错误！IP:" + IPHelper.GetClientIP());
                    return new BaseResultDataValue() { success = false, ErrorInfo = "Code参数错误!" };
                }
                Auth_code2SessionResult Auth_code2SessionResult = WeiXinMiniBasePage.Auth_code2Session(Code);

                IApplicationContext context = ContextRegistry.GetContext();
                object drrh = context.GetObject("ZhiFang.RBACService");
                IRBACService rbacservice = (IRBACService)drrh;

                if (rbacservice.RBAC_BA_Login(strUserAccount, strPassWord, isValidate))
                {
                    ZhiFang.Common.Log.Log.Debug("1");
                    this.CheckCookieAuth(new List<string> { Entity.RBAC.DicCookieSession.EmployeeID });
                    ZhiFang.Common.Public.Cookie.CookieHelper.Write(ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID, Auth_code2SessionResult.openid);
                    if (IBWXWeiXinAccount.AddEmpLink(Cookie.CookieHelper.Read(Entity.RBAC.DicCookieSession.EmployeeID), Auth_code2SessionResult.openid))
                    {
                        ZhiFang.Common.Log.Log.Debug("2");
                        IBSAccountRegister.SetOpenIdByEmpId(Entity.LIIP.DicCookieSession.WeiXinMiniOpenID, Entity.RBAC.DicCookieSession.EmployeeID);
                        return new BaseResultDataValue() { success = true, ErrorInfo = "绑定成功!" };
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("3");
                        return new BaseResultDataValue() { success = false, ErrorInfo = "绑定失败!" };
                    }
                    ZhiFang.Common.Log.Log.Debug("4");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("5");
                    return new BaseResultDataValue() { success = false, ErrorInfo = "用户名密码错误!" };
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ".RBAC_BA_LoginAndBindingByCode:异常：" + e.ToString() + ".IP:" + IPHelper.GetClientIP());
                return new BaseResultDataValue() { success = false, ErrorInfo = "程序异常!" }; ;
            }
        }

        public BaseResultDataValue WX_Mini_GetOpenIdByCode(string Code)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(Code))
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ".WX_Mini_GetOpenIdByCode:Code参数错误！IP:" + IPHelper.GetClientIP());
                brdv.success = false;
                brdv.ErrorInfo = "Code参数错误!";
                return brdv;
            }
            ZhiFang.Common.Log.Log.Debug($"{ this.GetType().FullName}.WX_Mini_GetOpenIdByCode:{Code}.IP:{IPHelper.GetClientIP()}");

            try
            {
                Auth_code2SessionResult Auth_code2SessionResult = WeiXinMiniBasePage.Auth_code2Session(Code);
                //brdv.ResultDataValue = Auth_code2SessionResult.openid;
                int flag = IBWXWeiXinAccount.CheckOpenIdAndSourceTypeID(Auth_code2SessionResult.openid, WXSourceType.小程序.Key);
                ZhiFang.Common.Public.Cookie.CookieHelper.Write(ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID, Auth_code2SessionResult.openid);
                brdv.ResultDataValue = flag.ToString();
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error($"{this.GetType().FullName}. WX_Mini_GetOpenIdByCode.异常：{e.ToString()}.IP:{IPHelper.GetClientIP()}");
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }

        public BaseResultDataValue WX_Mini_RegeditWeiXinAccount(WeiXinMiniClientUserInfo entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                this.CheckCookieAuth(new List<string> { ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID });
                if (entity == null)
                {
                    ZhiFang.Common.Log.Log.Error($"{this.GetType().FullName}. WX_Mini_RegeditWeiXinAccount.参数错误！.IP:{IPHelper.GetClientIP()}");
                    brdv.success = false;
                    brdv.ErrorInfo = "参数错误！";
                    return brdv;
                }
                //ZhiFang.Common.Log.Log.Error($"{this.GetType().FullName}. WX_Mini_RegeditWeiXinAccount.参数{ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entity)}.IP:{IPHelper.GetClientIP()}");
                WXWeiXinAccount WXWeiXinAccountentity = new WXWeiXinAccount();
                WXWeiXinAccountentity.OpenID = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID);
                WXWeiXinAccountentity.SourceTypeID = long.Parse(WXSourceType.小程序.Key);
                WXWeiXinAccountentity.UserName = entity.userInfo.nickName;
                WXWeiXinAccountentity.SexID = entity.userInfo.gender;
                WXWeiXinAccountentity.Language = entity.userInfo.language;

                WXWeiXinAccountentity.CityName = entity.userInfo.city;
                WXWeiXinAccountentity.ProvinceName = entity.userInfo.province;
                WXWeiXinAccountentity.CountryName = entity.userInfo.country;
                WXWeiXinAccountentity.HeadImgUrl = entity.userInfo.avatarUrl;

                WXWeiXinAccountentity.AddTime = DateTime.Now;
                brdv.success = IBWXWeiXinAccount.AddEntity(WXWeiXinAccountentity);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". WX_Mini_RegeditWeiXinAccount.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }

        public BaseResultDataValue WX_Mini_CheckWeiXinAccountByWeiXinMiniOpenID()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                this.CheckCookieAuth(new List<string> { ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID });
                brdv.success = true;
                brdv.ResultDataValue=IBWXWeiXinAccount.CheckWeiXinAccountByWeiXinMiniOpenID(Entity.LIIP.DicCookieSession.WeiXinMiniOpenID, WXSourceType.小程序.Key).ToString();
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". WX_Mini_CheckWeiXinAccountByWeiXinMiniOpenID.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }

        public BaseResultDataValue WX_Mini_GetBSAccountRegisterStatusByAuth()
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                this.CheckCookieAuth(new List<string> { ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID });

                var list = IBSAccountRegister.SearchListByHQL($" IdInfoNo='{Cookie.CookieHelper.Read(Entity.LIIP.DicCookieSession.WeiXinMiniOpenID)}' and ApplySourceTypeID={AccountApplySourceType.小程序.Key} ");
                if (list != null && list.Count > 0)
                {
                    if (list[0].ApprovalDateTime.HasValue)
                        brdv.ResultDataValue = "{\"ApprovalInfo\":\"" + list[0].ApprovalInfo + "\",\"ApprovalDateTime\":\"" + list[0].ApprovalDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"StatusName\":\"" + list[0].StatusName + "\"  }";
                    else
                        brdv.ResultDataValue = "{\"ApprovalInfo\":\"\",\"ApprovalDateTime\":\"\",\"StatusName\":\"" + list[0].StatusName + "\"  }";
                    brdv.success = true;
                    return brdv;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". WX_Mini_GetBSAccountRegisterStatusByAuth.未能查找到相关申请信息,WeiXinMiniOpenID：" + Cookie.CookieHelper.Read(Entity.LIIP.DicCookieSession.WeiXinMiniOpenID) + ",ApplySourceTypeID:" + AccountApplySourceType.小程序.Key);
                    brdv.success = false;
                    brdv.ResultCode = "500";
                    brdv.ErrorInfo = "未能查找到相关申请信息！";
                    return brdv;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". WX_Mini_GetBSAccountRegisterStatusByAuth.异常：" + e.ToString());
                brdv.success = false;
                brdv.ResultCode = "501";
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }

        public BaseResultDataValue ST_UDTO_AddSAccountRegister(SAccountRegister entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                this.CheckCookieAuth(new List<string> { ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID });
                if (entity != null)
                {
                    entity.IdInfoNo = Cookie.CookieHelper.Read(Entity.LIIP.DicCookieSession.WeiXinMiniOpenID);
                    entity.IdInfoTypeId = 1;
                    entity.DataAddTime = DateTime.Now;
                    entity.DataUpdateTime = DateTime.Now;

                    brdv = IBSAccountRegister.AddEntity(entity);
                    if (brdv.success)
                    {
                        brdv.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }

                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：实体参数不能为空！";
                }
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". ST_UDTO_AddSAccountRegister.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }
        }

        public BaseResultDataValue RBAC_UDTO_SearchModuleTreeTwoStageByModuleID(long ModuleID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            object tempBaseResultTree = null;
            try
            {
                this.CheckCookieAuth(new List<string> {
                    ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID,
                    Entity.RBAC.DicCookieSession.EmployeeID,
                    Entity.RBAC.DicCookieSession.UserAccount});
                string tempEmployeeID = ZhiFang.LIIP.Common.CookieHelper.Read(Entity.RBAC.DicCookieSession.EmployeeID); //EmployeeID 员工ID
                string tempUserAccount = ZhiFang.LIIP.Common.CookieHelper.Read(Entity.RBAC.DicCookieSession.UserAccount);

                if ((tempEmployeeID != null) && (tempEmployeeID.Length > 0))
                    tempBaseResultTree = IBRBACModule.SearchModuleTreeTwoStageByHREmpID(Int64.Parse(tempEmployeeID), ModuleID);
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：Cookie过期！";
                    return brdv;
                }

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");

                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error($"{this.GetType().FullName}. RBAC_UDTO_SearchModuleTreeTwoStageByModuleID.异常：{e.ToString()}.IP:{IPHelper.GetClientIP()}");
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }

        public BaseResultDataValue RBAC_UDTO_SearchModuleTreeTwoStageByModuleCode(string ModuleCode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            object tempBaseResultTree = null;
            try
            {
                this.CheckCookieAuth(new List<string> {
                    ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID,
                    Entity.RBAC.DicCookieSession.EmployeeID,
                    Entity.RBAC.DicCookieSession.UserAccount});
                string tempEmployeeID = ZhiFang.LIIP.Common.CookieHelper.Read(Entity.RBAC.DicCookieSession.EmployeeID); //EmployeeID 员工ID
                string tempUserAccount = ZhiFang.LIIP.Common.CookieHelper.Read(Entity.RBAC.DicCookieSession.UserAccount);

                if ((tempEmployeeID != null) && (tempEmployeeID.Length > 0))
                    tempBaseResultTree = IBRBACModule.SearchModuleTreeTwoStageByHREmpID(Int64.Parse(tempEmployeeID), ModuleCode);
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：Cookie过期！";
                    return brdv;
                }

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree);
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". RBAC_UDTO_SearchModuleTreeTwoStageByModuleCode.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue WX_Mini_LoginByOpenid()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            string openid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID);
            ZhiFang.Common.Log.Log.Debug($"{ this.GetType().FullName}.WX_Mini_LoginByOpenid.WeiXinMiniOpenID:{openid}.IP:{IPHelper.GetClientIP()}");

            try
            {
                this.CheckCookieAuth(new List<string> { ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID });
                RBACUser rbacuser = IBWXWeiXinAccount.GetRbacUserByOpenid(openid);
                base.SetUserSession(rbacuser);
                ZhiFang.Common.Public.Cookie.CookieHelper.Write(ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniUserID, rbacuser.Id.ToString());
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error($"{this.GetType().FullName}. WX_Mini_LoginByOpenid.异常：{e.ToString()}.IP:{IPHelper.GetClientIP()}");
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }

        public BaseResultDataValue WX_Mini_GetEmpInfoById(string fields, bool isPlanish)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                this.CheckCookieAuth(new List<string> {
                    ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID,
                    Entity.RBAC.DicCookieSession.EmployeeID,
                    Entity.RBAC.DicCookieSession.UserAccount});
                string tempEmployeeID = ZhiFang.LIIP.Common.CookieHelper.Read(Entity.RBAC.DicCookieSession.EmployeeID); //EmployeeID 员工ID
                var tempEntity = IBHREmployee.Get(long.Parse(tempEmployeeID));
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                 if (isPlanish)
                    {
                        brdv.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<HREmployee>(tempEntity);
                    }
                    else
                    {
                        brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
              
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error($"{this.GetType().FullName}. WX_Mini_GetEmpInfoById.异常：{e.ToString()}.IP:{IPHelper.GetClientIP()}");
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue WX_Mini_GetHosptialInfoById(string fields, bool isPlanish)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                this.CheckCookieAuth(new List<string> {
                    ZhiFang.Entity.LIIP.DicCookieSession.WeiXinMiniOpenID,
                    Entity.RBAC.DicCookieSession.EmployeeID,
                    Entity.RBAC.DicCookieSession.UserAccount, SysPublicSet.SysDicCookieSession.LabID});
                string LabID = ZhiFang.LIIP.Common.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID); 
                var Hospital = IBBHospital.Get(long.Parse(LabID));
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
               
                    if (isPlanish)
                    {
                        brdv.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BHospital>(Hospital);
                    }
                    else
                    {
                        brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(Hospital, fields);
                    }
              
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error($"{this.GetType().FullName}. WX_Mini_GetHosptialInfoById.异常：{e.ToString()}.IP:{IPHelper.GetClientIP()}");
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }
            return brdv;
        }

        private bool CheckCookieAuth(List<string> keylist)
        {
            keylist.ForEach(a =>
            {
                if (string.IsNullOrEmpty(ZhiFang.Common.Public.Cookie.CookieHelper.Read(a)))
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". CheckCookieAuth.用户身份未确定！" + a);
                    throw new Exception("用户身份未确定！请重新登陆！");
                }
            });
            return true;
        }


    }
}
