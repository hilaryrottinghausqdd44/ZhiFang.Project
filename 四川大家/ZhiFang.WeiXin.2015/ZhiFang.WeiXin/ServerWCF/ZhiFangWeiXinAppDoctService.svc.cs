using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.WeiXin.ServerContract;
using ZhiFang.WeiXin.Entity;
using ZhiFang.Entity.Base;
using ZhiFang.Common.Public;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.WeiXin.Entity.ViewObject.Request;
using ZhiFang.WeiXin.BusinessObject;

namespace ZhiFang.WeiXin.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ZhiFangWeiXinAppDoctService : IZhiFangWeiXinAppDoctService
    {
        IBLL.IBBLabTestItem IBBLabTestItem { get; set; }
        IBLL.IBOSDoctorOrderForm IBOSDoctorOrderForm { get; set; }
        IBLL.IBOSUserConsumerForm IBOSUserConsumerForm { get; set; }
        IBLL.IBOSDoctorBonus IBOSDoctorBonus { get; set; }
        IBLL.IBBDoctorAccount IBBDoctorAccount { get; set; }
        IBLL.IBBWeiXinAccount IBBWeiXinAccount { get; set; }
        IBLL.IBOSRecommendationItemProduct IBOSRecommendationItemProduct { get; set; }
        //ZhiFang.IBLL.RBAC.IBHREmployee IBHREmployee { get; set; }
        IBLL.IBOSItemProductClassTree IBOSItemProductClassTree { get; set; }
        IBLL.IBOSItemProductClassTreeLink IBOSItemProductClassTreeLink { get; set; }
        public BaseResultDataValue DS_UDTO_SearchOSTestItemByAreaID(int page, int limit, string where, string sort)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID).Trim() == "")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取区域！";
                ZhiFang.Common.Log.Log.Error("DS_UDTO_SearchOSTestItemByAreaID.无法获取区域!AreaID为空！");
                return baseResultDataValue;
            }
            EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> ELBLTIVO = IBBLabTestItem.SearchOSBLabTestItemByAreaID(Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID), page, limit, sort, where);
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
                    ZhiFang.Common.Log.Log.Error("DS_UDTO_SearchOSTestItemByAreaID.程序错误:" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取组套列表！";
                ZhiFang.Common.Log.Log.Error("DS_UDTO_SearchOSTestItemByAreaID.无法获取组套列表!");

            }
            return baseResultDataValue;
        }

        public BaseResultDataValue DS_UDTO_SearchOSRecommendationItemByAreaID(int page, int limit, string where, string sort)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID).Trim() == "")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取区域！";
                ZhiFang.Common.Log.Log.Error("DS_UDTO_SearchOSRecommendationItemByAreaID.无法获取区域!AreaID为空！");
                return baseResultDataValue;
            }
            EntityList<OSRecommendationItemProductVO> ELBLTIVO = IBOSRecommendationItemProduct.SearchOSBLabTestItemByAreaID(Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID), page, limit, sort, where);
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
                    ZhiFang.Common.Log.Log.Error("DS_UDTO_SearchOSRecommendationItemByAreaID.程序错误:" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取组套列表！";
                ZhiFang.Common.Log.Log.Error("DS_UDTO_SearchOSRecommendationItemByAreaID.无法获取组套列表!");

            }
            return baseResultDataValue;
        }

        public BaseResultDataValue DS_UDTO_SaveOSDoctorOrderForm(Entity.ViewObject.Request.OSDoctorOrderFormVO entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID).Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取区域！";
                    ZhiFang.Common.Log.Log.Error("DS_UDTO_SaveOSDoctorOrderForm.无法获取区域!AreaID为空！");
                    return baseResultDataValue;
                }
                entity.AreaID = long.Parse(Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID));
                if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorId) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorId).Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取医生！";
                    ZhiFang.Common.Log.Log.Error("DS_UDTO_SaveOSDoctorOrderForm.无法获取医生!DoctorId为空！");
                    return baseResultDataValue;
                }
                //if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorOpenID) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorOpenID).Trim() == "")
                //{
                //    baseResultDataValue.success = false;
                //    baseResultDataValue.ErrorInfo = "无法获取医生！";
                //    ZhiFang.Common.Log.Log.Error("DS_UDTO_SaveOSDoctorOrderForm.无法获取医生!DoctorOpenID为空！");
                //    return baseResultDataValue;
                //}
                //if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorName) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorName).Trim() == "")
                //{
                //    baseResultDataValue.success = false;
                //    baseResultDataValue.ErrorInfo = "无法获取医生！";
                //    ZhiFang.Common.Log.Log.Error("DS_UDTO_SaveOSDoctorOrderForm.无法获取医生!DoctorName为空！");
                //    return baseResultDataValue;
                //}
                //if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorHospital) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorHospital).Trim() == "")
                //{
                //    baseResultDataValue.success = false;
                //    baseResultDataValue.ErrorInfo = "无法获取医生！";
                //    ZhiFang.Common.Log.Log.Error("DS_UDTO_SaveOSDoctorOrderForm.无法获取医生!DoctorHospital为空！");
                //    return baseResultDataValue;
                //}

                //if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorWeiXinAccount) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorWeiXinAccount).Trim() == "")
                //{
                //    baseResultDataValue.success = false;
                //    baseResultDataValue.ErrorInfo = "无法获取医生！";
                //    ZhiFang.Common.Log.Log.Error("DS_UDTO_SaveOSDoctorOrderForm.无法获取医生!DoctorWeiXinAccount为空！");
                //    return baseResultDataValue;
                //}
                baseResultDataValue = IBOSDoctorOrderForm.AddOSDoctorOrderFormVO((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorId), entity);


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

        public BaseResultDataValue DS_UDTO_SearchOSDoctorOrderForm(long id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            OSDoctorOrderForm osDOF = IBOSDoctorOrderForm.Get(id);
            if (osDOF != null)
            {
                string fields = "";
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSDoctorOrderForm>(osDOF);
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
                baseResultDataValue.ErrorInfo = "无法根据ID获取到医嘱单信息！ID：" + id.ToString();

            }
            return baseResultDataValue;
        }

        public BaseResultDataValue DS_UDTO_SearchOSDoctorOrderFormList(string beginDate, string endDate, string patName, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            return baseResultDataValue;
        }

        /// <summary>
        /// 医生强制登陆服务
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>BaseResultDataValue</returns>
        public BaseResultDataValue WXADS_BA_Login(string password)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = false;
            string UserWeiXinAccountID = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
            try
            {
                //id = "4755749206623199738";
                if (!string.IsNullOrEmpty(UserWeiXinAccountID))
                {
                    BWeiXinAccount bweiXinAccount = IBBWeiXinAccount.Get(long.Parse(UserWeiXinAccountID));
                    if (bweiXinAccount != null)
                    {
                        IList<BDoctorAccount> listBDoctorAccount = IBBDoctorAccount.SearchListByHQL(" WeiXinUserID=" + bweiXinAccount.Id.ToString());
                        if (listBDoctorAccount != null && listBDoctorAccount.Count > 0)
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
                            baseResultDataValue.ErrorInfo = "此帐号类型不正确！";
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
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("WXADS_BA_Login.异常：" + e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "程序异常！";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 医生帐号绑定
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>BaseResultDataValue</returns>
        public BaseResultDataValue WXADS_DoctorAccountBind(string AccountCode, string password)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = false;
            try
            {

                if (Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID) == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未能读取帐号信息！";
                    return baseResultDataValue;
                }

                if (AccountCode == null || AccountCode.Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未能读取手机号！";
                    return baseResultDataValue;
                }

                if (password == null || password.Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未能读取绑定密码！";
                    return baseResultDataValue;
                }

                string id = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
                //id = "4755749206623199738";
                if (!string.IsNullOrEmpty(id))
                {
                    baseResultDataValue = IBBWeiXinAccount.WeiXinAccountBind(id, AccountCode, password);

                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法从Cookie获取到账号信息！";
                    ZhiFang.Common.Log.Log.Debug("WXADS_DoctorAccountBind:无法从Cookie获取到账号信息！");
                }
                return baseResultDataValue;
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "程序错误异常！";
                ZhiFang.Common.Log.Log.Debug("WXADS_DoctorAccountBind:异常：" + e.ToString());
                return baseResultDataValue;
            }
        }

        /// <summary>
        /// 获取医生费用信息服务
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue WXADS_BA_GetDoctorChargeInfo()
        {
            //ZhiFang.Common.Log.Log.Error("WXADS_BA_GetDoctorChargeInfo");
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string id = Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorId);
            //id = "5508978039851366619";
            if (!string.IsNullOrEmpty(id))
            {
                OSDoctorChargeInfoVO chargeVO = new OSDoctorChargeInfoVO();
                IBBDoctorAccount.GetDoctorBankInfo(chargeVO, long.Parse(id));
                IBOSUserConsumerForm.GetDoctorUserConsumerInfo(chargeVO, long.Parse(id));
                IBOSDoctorBonus.GetDoctorBonusInfo(chargeVO, long.Parse(id));
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish<OSDoctorChargeInfoVO>(chargeVO);
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
                baseResultDataValue.ErrorInfo = "无法从Cookie获取到医生ID信息！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXADS_BA_GetDoctorChargeInfoDay(int page, int limit)
        {
            //ZhiFang.Common.Log.Log.Error("WXADS_BA_GetDoctorChargeInfoDay" );
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string id = Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorId);
            //id = "5508978039851366619";
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    OSDoctorChargeInfoVO chargeVO = new OSDoctorChargeInfoVO();
                    IBBDoctorAccount.GetDoctorBankInfo(chargeVO, long.Parse(id));
                    IBOSUserConsumerForm.GetDoctorUserConsumerInfoByDay(chargeVO, long.Parse(id), page, limit);
                    IBOSDoctorBonus.GetDoctorBonusInfo(chargeVO, long.Parse(id));
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish<OSDoctorChargeInfoVO>(chargeVO);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "程序错误！";
                    ZhiFang.Common.Log.Log.Error("WXADS_BA_GetDoctorChargeInfoDay.异常：" + ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "WXADS_BA_GetDoctorChargeInfoDay.无法从Cookie获取到医生ID信息！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXADS_BA_GetOSUserConsumerForm(string StartDay, string EndDay, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string id = Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.DoctorId);
            //id = "5508978039851366619";
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    EntityList<OSUserConsumerFormVO> osucfvolist = IBOSUserConsumerForm.GetGetOSUserConsumerForm(StartDay, EndDay, id, page, limit);

                    baseResultDataValue.ResultDataValue = JsonSerializer.JsonDotNetSerializer(osucfvolist);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "程序错误！";
                    ZhiFang.Common.Log.Log.Error("WXADS_BA_GetOSUserConsumerForm.异常：" + ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "WXADS_BA_GetOSUserConsumerForm.无法从Cookie获取到医生ID信息！";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 获取医生账号信息服务
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue WXADS_BA_GetDoctorAccountInfo()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            //string id = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
            string id = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.DoctorId);

            //id = "5330139284538302530";
            if (!string.IsNullOrEmpty(id))
            {
                ZhiFang.Common.Log.Log.Debug("WXADS_BA_GetDoctorAccountInfo.DoctorId:" + id.ToString());
                BDoctorAccount bdoctorAccount = IBBDoctorAccount.Get(long.Parse(id));
                if (bdoctorAccount != null)
                {
                    //string fields = "Name,UserName,HeadImgUrl,WeiXinAccount,LastLoginTime,LoginInputPasswordFlag";
                    string fields = "AreaID,HospitalID,HospitalName,HospitalDeptID,HospitalDeptName,HWorkNumberID,Name,BankAccount,BankAddress,BonusPercent";
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BDoctorAccount>(bdoctorAccount, false);
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
                    baseResultDataValue.ErrorInfo = "医生信息不存在！";
                    //ZhiFang.Common.Log.Log.Debug("医生信息不存在！");
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("WXADS_BA_GetDoctorAccountInfo.无法从Cookie获取到医生ID信息!");
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法从Cookie获取到医生ID信息！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WXADS_BA_SearchDoctorOrderForm(int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string weiXinUserID = Cookie.CookieHelper.Read(Entity.SysDicCookieSession.UserWeiXinAccountID);
            //weiXinUserID = "4755749206623199738";
            if (!string.IsNullOrEmpty(weiXinUserID))
            {
                EntityList<OSDoctorOrderForm> entityList = IBOSDoctorOrderForm.SearchListByHQL(" osdoctororderform.DoctorWeiXinUserID=" + weiXinUserID, page, limit);
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
                baseResultDataValue.ErrorInfo = "无法从Cookie获取到医生微信账号ID信息！";
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue DS_UDTO_SearchOSItemProductClassTreeByIdAndHQL(string id, string maxlevel, string where, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BaseResultTree<OSItemProductClassTree> baseResultTree = new BaseResultTree<OSItemProductClassTree>();
            if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID).Trim() == "")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取区域！";
                ZhiFang.Common.Log.Log.Error("DS_UDTO_SearchOSItemProductClassTreeByIdAndHQL.无法获取区域!AreaID为空！");
                return baseResultDataValue;
            }
            try
            {
                string areaID = Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID);
                baseResultTree = IBOSItemProductClassTree.SearchOSItemProductClassTreeByIdAndHQL(id, where, areaID, maxlevel);
                //ZhiFang.Common.Log.Log.Debug("baseResultTree:" + baseResultTree.ToString());
                bool flag = baseResultTree.Tree != null && baseResultTree.Tree.Count > 0;
                if (flag)
                {
                    ParseObjectProperty parseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        baseResultDataValue.ResultDataValue = parseObjectProperty.GetObjectPropertyNoPlanish<BaseResultTree<OSItemProductClassTree>>(baseResultTree, fields);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        ZhiFang.Common.Log.Log.Error("DS_UDTO_SearchOSItemProductClassTreeByIdAndHQL.序列化错误异常：" + ex.ToString());
                    }
                }
            }
            catch (Exception ex2)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex2.Message;
                ZhiFang.Common.Log.Log.Error("DS_UDTO_SearchOSItemProductClassTreeByIdAndHQL.异常：" + ex2.ToString());
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue DS_UDTO_SearchBLabTestItemVOByTreeId(int limit, int page, bool isPlanish, bool isSearchChild, string treeId, string where, string fields, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (String.IsNullOrEmpty(treeId))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "产品分类ID(treeId)不能为空！";
                ZhiFang.Common.Log.Log.Error("DS_UDTO_SearchBLabTestItemVOByTreeId错误:" + tempBaseResultDataValue.ErrorInfo);
                return tempBaseResultDataValue;
            }
            //if (Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID) == null || Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID).Trim() == "")
            //{
            //    tempBaseResultDataValue.success = false;
            //    tempBaseResultDataValue.ErrorInfo = "无法获取区域！";
            //    ZhiFang.Common.Log.Log.Error("DS_UDTO_SearchBLabTestItemVOByTreeId.无法获取区域!AreaID为空！");
            //    return tempBaseResultDataValue;
            //}
            if (String.IsNullOrEmpty(fields))
            {
                fields = "BLabTestItemVO_Id,BLabTestItemVO_LabCode,BLabTestItemVO_ItemNo,BLabTestItemVO_CName,BLabTestItemVO_EName,BLabTestItemVO_ShortName,BLabTestItemVO_ShortCode,BLabTestItemVO_IsDoctorItem,BLabTestItemVO_IschargeItem,BLabTestItemVO_Price,BLabTestItemVO_MarketPrice,BLabTestItemVO_GreatMasterPrice,BLabTestItemVO_IsCombiItem,BLabTestItemVO_BonusPercent";
            }
            EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> tempEntityList = new EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO>();
            try
            {
                string areaID = Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.AreaID);
                tempEntityList = IBOSItemProductClassTreeLink.SearchBLabTestItemVOByTreeId(page, limit, where, CommonServiceMethod.GetSortHQL(sort), areaID, treeId, isSearchChild);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO>(tempEntityList);
                }
                else
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("DS_UDTO_SearchBLabTestItemVOByTreeId.序列化错误异常：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }
    }
        
    }

