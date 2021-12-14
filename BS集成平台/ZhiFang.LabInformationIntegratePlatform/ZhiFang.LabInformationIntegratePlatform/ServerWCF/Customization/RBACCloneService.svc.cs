using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization
{
    [ServiceContract(Namespace = "http://ZhiFang.LabInformationIntegratePlatform/")]
    /// <summary>
    /// LISMessageWebService 的摘要说明
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RBACCloneService
    {
        
        [ServiceContractDescription(Name = "从检验之星6中抓取人员信息", Desc = "从检验之星6中抓取人员信息", Url = "RBACCloneService.svc/CatchRBACByLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACByLabStarV6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACByLabStarV6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            return brdv;
        }
        [ServiceContractDescription(Name = "从检验之星6中抓取部门信息", Desc = "从检验之星6中抓取部门信息", Url = "RBACCloneService.svc/CatchDeptByLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchDeptByLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchDeptByLabStar6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBDeptClone RBACClone = (IBLL.LIIP.RBACClone.IBBDeptClone)context.GetObject("BBDeptClone");
                brdv = RBACClone.DeptClone("LabStar6",empid, empname,null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchDeptByLabStar6.异常："+e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从检验之星6中抓取医生信息", Desc = "从检验之星6中抓取医生信息", Url = "RBACCloneService.svc/CatchDoctorByLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchDoctorByLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchDoctorByLabStar6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.EmpClone_Doctor("LabStar6", empid, empname,null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchDoctorByLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从检验之星6中抓取PUser信息", Desc = "从检验之星6中抓取PUser信息", Url = "RBACCloneService.svc/CatchPUserByLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchPUserByLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchPUserByLabStar6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.EmpClone_PUser("LabStar6", empid, empname,null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchDoctorByLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从检验之星6中抓取NPUser信息", Desc = "从检验之星6中抓取NPUser信息", Url = "RBACCloneService.svc/CatchNPUserByLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchNPUserByLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchNPUserByLabStar6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.EmpClone_NPUser("LabStar6", empid, empname,null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchDoctorByLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }


        [ServiceContractDescription(Name = "查询克隆同步记录", Desc = "查询克隆同步记录", Url = "RBACCloneService.svc/ST_UDTO_SearchSLIIPSystemRBACCloneLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSLIIPSystemRBACCloneLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue ST_UDTO_SearchSLIIPSystemRBACCloneLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<SLIIPSystemRBACCloneLog> entityList = new EntityList<SLIIPSystemRBACCloneLog>();
            IApplicationContext context = ContextRegistry.GetContext();
            IBLL.LIIP.RBACClone.IBSLIIPSystemRBACCloneLog IBSLIIPSystemRBACCloneLog = (IBLL.LIIP.RBACClone.IBSLIIPSystemRBACCloneLog)context.GetObject("BSLIIPSystemRBACCloneLog");
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBSLIIPSystemRBACCloneLog.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBSLIIPSystemRBACCloneLog.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SLIIPSystemRBACCloneLog>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchBHospitalById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBSLIIPSystemRBACCloneLog IBSLIIPSystemRBACCloneLog = (IBLL.LIIP.RBACClone.IBSLIIPSystemRBACCloneLog)context.GetObject("BSLIIPSystemRBACCloneLog");
                var entity = IBSLIIPSystemRBACCloneLog.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<SLIIPSystemRBACCloneLog>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }


        [ServiceContractDescription(Name = "", Desc = "", Url = "RBACCloneService.svc/TransLIIPPwd?pwd={pwd}", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/TransLIIPPwd?pwd={pwd}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public string TransLIIPPwd(string pwd)
        {
            return ZhiFang.Common.Public.SecurityHelp.MD5Encrypt(ZhiFang.LIIP.Common.PUserPWDHelp.UnCovertPassWord(pwd), Common.Public.SecurityHelp.PWDMD5Key);
        }

        [ServiceContractDescription(Name = "", Desc = "", Url = "RBACCloneService.svc/TransLabStarPwd?pwd={pwd}", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/TransLabStarPwd?pwd={pwd}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public string TransLabStarPwd(string pwd)
        {
            return ZhiFang.LIIP.Common.PUserPWDHelp.UnCovertPassWord(pwd);
        }

        [ServiceContractDescription(Name = "从检验之星6中抓取部门数据列表", Desc = "从检验之星6中抓取部门数据列表", Url = "RBACCloneService.svc/CatchDeptDataListByLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchDeptDataListByLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchDeptDataListByLabStar6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBDeptClone RBACClone = (IBLL.LIIP.RBACClone.IBBDeptClone)context.GetObject("BBDeptClone");
                brdv = RBACClone.CatchDeptDataList("LabStar6");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchDeptDataListByLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从检验之星6中抓取医生数据列表", Desc = "从检验之星6中抓取医生数据列表", Url = "RBACCloneService.svc/CatchDoctorDataListByLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchDoctorDataListByLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchDoctorDataListByLabStar6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.CatchDoctorDataList();
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchDoctorDataListByLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从检验之星6中抓取Npuser数据列表", Desc = "从检验之星6中抓取Npuser数据列表", Url = "RBACCloneService.svc/CatchNPuserDataListByLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchNPuserDataListByLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchNPuserDataListByLabStar6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.CatchNPuserDataList();
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchNPuserDataListByLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从检验之星6中抓取Puser数据列表", Desc = "从检验之星6中抓取Puser数据列表", Url = "RBACCloneService.svc/CatchPuserDataListByLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchPuserDataListByLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchPuserDataListByLabStar6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.CatchPuserDataList();
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchPuserDataListByLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "同步抓取的检验之星6部门数据列表", Desc = "同步抓取的检验之星6部门数据列表", Url = "RBACCloneService.svc/SYNCDeptByLabStar6", Get = "", Post = "List<Entity.RBAC.HRDept>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCDeptByLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCDeptByLabStar6(List<Entity.RBAC.HRDept> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBDeptClone RBACClone = (IBLL.LIIP.RBACClone.IBBDeptClone)context.GetObject("BBDeptClone");
                brdv = RBACClone.DeptClone("LabStar6", empid, empname,entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCDeptByLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "同步抓取的检验之星6医生数据列表", Desc = "同步抓取的检验之星6医生数据列表", Url = "RBACCloneService.svc/SYNCDoctorByLabStar6", Get = "", Post = "List<Entity.RBAC.HREmployee>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCDoctorByLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCDoctorByLabStar6(List<ZhiFang.Entity.RBAC.HREmployee> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.EmpClone_Doctor("LabStar6", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCDoctorByLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "同步抓取的检验之星6Puser数据列表", Desc = "同步抓取的检验之星6Puser数据列表", Url = "RBACCloneService.svc/SYNCPUserByLabStar6", Get = "", Post = "List<Entity.RBAC.HREmployee>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCPUserByLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCPUserByLabStar6(List<ZhiFang.Entity.RBAC.HREmployee> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.EmpClone_PUser("LabStar6", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCPUserByLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "同步抓取的检验之星6NPuser数据列表", Desc = "同步抓取的检验之星6NPuser数据列表", Url = "RBACCloneService.svc/SYNCNPUserByLabStar6", Get = "", Post = "List<Entity.RBAC.HREmployee>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCNPUserByLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCNPUserByLabStar6(List<ZhiFang.Entity.RBAC.HREmployee> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.EmpClone_NPUser("LabStar6", empid, empname,entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCNPUserByLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取部门信息", Desc = "从QMS中抓取部门信息", Url = "RBACCloneService.svc/CatchHRDeptByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchHRDeptByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchHRDeptByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.HRDeptClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchHRDeptByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取部门数据列表", Desc = "从QMS中抓取部门数据列表", Url = "RBACCloneService.svc/CatchHRDeptDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchHRDeptDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchHRDeptDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchHRDeptDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchHRDeptDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS部门数据列表", Desc = "同步抓取的QMS部门数据列表", Url = "RBACCloneService.svc/SYNCHRDeptByGeneQMS", Get = "", Post = "List<Entity.RBAC.HRDept>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCHRDeptByGeneQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCHRDeptByGeneQMS(List<Entity.RBAC.HRDept> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.HRDeptClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCHRDeptByGeneQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取部门员工关系信息", Desc = "从QMS中抓取部门员工关系信息", Url = "RBACCloneService.svc/CatchHRDeptEmpByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchHRDeptEmpByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchHRDeptEmpByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.HRDeptEmpClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchHRDeptEmpByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取部门员工关系数据列表", Desc = "从QMS中抓取部门员工关系数据列表", Url = "RBACCloneService.svc/CatchHRDeptEmpDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchHRDeptEmpDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchHRDeptEmpDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchHRDeptEmpDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchHRDeptEmpDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS部门员工关系数据列表", Desc = "同步抓取的QMS部门员工关系数据列表", Url = "RBACCloneService.svc/SYNCHRDeptEmpByQMS", Get = "", Post = "List<Entity.RBAC.HRDeptEmp>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCHRDeptEmpByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCHRDeptEmpByQMS(List<Entity.RBAC.HRDeptEmp> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.HRDeptEmpClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCHRDeptByGeneQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取部门身份信息", Desc = "从QMS中抓取部门身份信息", Url = "RBACCloneService.svc/CatchHRDeptIdentityByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchHRDeptIdentityByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchHRDeptIdentityByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.HRDeptIdentityClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchHRDeptIdentityByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取部门身份数据列表", Desc = "从QMS中抓取部门身份数据列表", Url = "RBACCloneService.svc/CatchHRDeptIdentityDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchHRDeptIdentityDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchHRDeptIdentityDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchHRDeptIdentityDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchHRDeptIdentityDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS部门身份数据列表", Desc = "同步抓取的QMS部门身份数据列表", Url = "RBACCloneService.svc/SYNCHRDeptIdentityByQMS", Get = "", Post = "List<Entity.RBAC.HRDeptIdentity>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCHRDeptIdentityByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCHRDeptIdentityByQMS(List<Entity.RBAC.HRDeptIdentity> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.HRDeptIdentityClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCHRDeptIdentityByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取员工身份信息", Desc = "从QMS中抓取员工身份信息", Url = "RBACCloneService.svc/CatchHREmpIdentityByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchHREmpIdentityByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchHREmpIdentityByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.HREmpIdentityClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchHREmpIdentityByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取员工身份数据列表", Desc = "从QMS中抓取员工身份数据列表", Url = "RBACCloneService.svc/CatchHREmpIdentityDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchHREmpIdentityDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchHREmpIdentityDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchHREmpIdentityDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchHREmpIdentityDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS员工身份数据列表", Desc = "同步抓取的QMS员工身份数据列表", Url = "RBACCloneService.svc/SYNCHREmpIdentityByQMS", Get = "", Post = "List<Entity.RBAC.HREmpIdentity>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCHREmpIdentityByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCHREmpIdentityByQMS(List<Entity.RBAC.HREmpIdentity> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.HREmpIdentityClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCHREmpIdentityByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取员工信息", Desc = "从QMS中抓取员工信息", Url = "RBACCloneService.svc/CatchHREmployeeByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchHREmployeeByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchHREmployeeByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.HREmployeeClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchHREmployeeByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取员工数据列表", Desc = "从QMS中抓取员工数据列表", Url = "RBACCloneService.svc/CatchHREmployeeDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchHREmployeeDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchHREmployeeDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchHREmployeeDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchHREmployeeDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS员工数据列表", Desc = "同步抓取的QMS员工数据列表", Url = "RBACCloneService.svc/SYNCHREmployeeByQMS", Get = "", Post = "List<Entity.RBAC.HREmployee>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCHREmployeeByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCHREmployeeByQMS(List<Entity.RBAC.HREmployee> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.HREmployeeClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCHREmployeeByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取职位信息", Desc = "从QMS中抓取职位信息", Url = "RBACCloneService.svc/CatchHRPositionByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchHRPositionByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchHRPositionByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.HRPositionClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchHRPositionByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取职位数据列表", Desc = "从QMS中抓取职位数据列表", Url = "RBACCloneService.svc/CatchHRPositionDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchHRPositionDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchHRPositionDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchHRPositionDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchHRPositionDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS职位数据列表", Desc = "同步抓取的QMS职位数据列表", Url = "RBACCloneService.svc/SYNCHRPositionByQMS", Get = "", Post = "List<Entity.RBAC.HRPosition>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCHRPositionByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCHRPositionByQMS(List<Entity.RBAC.HRPosition> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.HRPositionClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCHRPositionByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取员工设置信息", Desc = "从QMS中抓取员工设置信息", Url = "RBACCloneService.svc/CatchRBACEmpOptionsByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACEmpOptionsByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACEmpOptionsByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACEmpOptionsClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACEmpOptionsByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取员工设置数据列表", Desc = "从QMS中抓取员工设置数据列表", Url = "RBACCloneService.svc/CatchRBACEmpOptionsDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACEmpOptionsDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACEmpOptionsDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchRBACEmpOptionsDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACEmpOptionsDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS员工设置数据列表", Desc = "同步抓取的QMS员工设置数据列表", Url = "RBACCloneService.svc/SYNCRBACEmpOptionsByQMS", Get = "", Post = "List<Entity.RBAC.RBACEmpOptions>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCRBACEmpOptionsByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCRBACEmpOptionsByQMS(List<Entity.RBAC.RBACEmpOptions> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACEmpOptionsClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCRBACEmpOptionsByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取员工角色信息", Desc = "从QMS中抓取员工角色信息", Url = "RBACCloneService.svc/CatchRBACEmpRolesByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACEmpRolesByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACEmpRolesByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACEmpRolesClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACEmpRolesByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取员工角色数据列表", Desc = "从QMS中抓取员工角色数据列表", Url = "RBACCloneService.svc/CatchRBACEmpRolesDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACEmpRolesDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACEmpRolesDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchRBACEmpRolesDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACEmpRolesDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS员工角色数据列表", Desc = "同步抓取的QMS员工角色数据列表", Url = "RBACCloneService.svc/SYNCRBACEmpRolesByQMS", Get = "", Post = "List<Entity.RBAC.RBACEmpRoles>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCRBACEmpRolesByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCRBACEmpRolesByQMS(List<Entity.RBAC.RBACEmpRoles> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACEmpRolesClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCRBACEmpRolesByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取模块信息", Desc = "从QMS中抓取模块信息", Url = "RBACCloneService.svc/CatchRBACModuleByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACModuleByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACModuleByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACModuleClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACModuleByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取模块数据列表", Desc = "从QMS中抓取模块数据列表", Url = "RBACCloneService.svc/CatchRBACModuleDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACModuleDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACModuleDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchRBACModuleDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACModuleDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS模块数据列表", Desc = "同步抓取的QMS模块数据列表", Url = "RBACCloneService.svc/SYNCRBACModuleByQMS", Get = "", Post = "List<Entity.RBAC.RBACModule>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCRBACModuleByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCRBACModuleByQMS(List<Entity.RBAC.RBACModule> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACModuleClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCRBACModuleByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取模块操作信息", Desc = "从QMS中抓取模块操作信息", Url = "RBACCloneService.svc/CatchRBACModuleOperByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACModuleOperByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACModuleOperByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACModuleOperClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACModuleOperByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取模块操作数据列表", Desc = "从QMS中抓取模块操作数据列表", Url = "RBACCloneService.svc/CatchRBACModuleOperDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACModuleOperDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACModuleOperDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchRBACModuleOperDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACModuleOperDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS模块操作数据列表", Desc = "同步抓取的QMS模块操作数据列表", Url = "RBACCloneService.svc/SYNCRBACModuleOperByQMS", Get = "", Post = "List<Entity.RBAC.RBACModuleOper>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCRBACModuleOperByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCRBACModuleOperByQMS(List<Entity.RBAC.RBACModuleOper> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACModuleOperClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCRBACModuleOperByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取角色信息", Desc = "从QMS中抓取角色信息", Url = "RBACCloneService.svc/CatchRBACRoleByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACRoleByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACRoleByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACRoleClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACRoleByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取角色数据列表", Desc = "从QMS中抓取角色数据列表", Url = "RBACCloneService.svc/CatchRBACRoleDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACRoleDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACRoleDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchRBACRoleDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACRoleDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS角色数据列表", Desc = "同步抓取的QMS角色数据列表", Url = "RBACCloneService.svc/SYNCRBACRoleByQMS", Get = "", Post = "List<Entity.RBAC.RBACRole>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCRBACRoleByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCRBACRoleByQMS(List<Entity.RBAC.RBACRole> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACRoleClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCRBACRoleByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取角色模块访问权限信息", Desc = "从QMS中抓取角色模块访问权限信息", Url = "RBACCloneService.svc/CatchRBACRoleModuleByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACRoleModuleByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACRoleModuleByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACRoleModuleClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACRoleModuleByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取角色模块访问权限数据列表", Desc = "从QMS中抓取角色模块访问权限数据列表", Url = "RBACCloneService.svc/CatchRBACRoleModuleDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACRoleModuleDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACRoleModuleDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchRBACRoleModuleDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACRoleModuleDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS角色模块访问权限数据列表", Desc = "同步抓取的QMS角色模块访问权限数据列表", Url = "RBACCloneService.svc/SYNCRBACRoleModuleByQMS", Get = "", Post = "List<Entity.RBAC.RBACRoleModule>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCRBACRoleModuleByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCRBACRoleModuleByQMS(List<Entity.RBAC.RBACRoleModule> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACRoleModuleClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCRBACRoleModuleByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取角色权限信息", Desc = "从QMS中抓取角色权限信息", Url = "RBACCloneService.svc/CatchRBACRoleRightByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACRoleRightByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACRoleRightByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACRoleRightClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACRoleRightByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取角色权限数据列表", Desc = "从QMS中抓取角色权限数据列表", Url = "RBACCloneService.svc/CatchRBACRoleRightDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACRoleRightDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACRoleRightDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchRBACRoleRightDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACRoleRightDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS角色权限数据列表", Desc = "同步抓取的QMS角色权限数据列表", Url = "RBACCloneService.svc/SYNCRBACRoleRightByQMS", Get = "", Post = "List<Entity.RBAC.RBACRoleRight>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCRBACRoleRightByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCRBACRoleRightByQMS(List<Entity.RBAC.RBACRoleRight> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACRoleRightClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCRBACRoleRightByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取行过滤信息", Desc = "从QMS中抓取行过滤信息", Url = "RBACCloneService.svc/CatchRBACRowFilterByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACRowFilterByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACRowFilterByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACRowFilterClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACRowFilterByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取行过滤数据列表", Desc = "从QMS中抓取行过滤数据列表", Url = "RBACCloneService.svc/CatchRBACRowFilterDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACRowFilterDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACRowFilterDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchRBACRowFilterDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACRowFilterDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS行过滤数据列表", Desc = "同步抓取的QMS行过滤数据列表", Url = "RBACCloneService.svc/SYNCRBACRowFilterByQMS", Get = "", Post = "List<Entity.RBAC.RBACRowFilter>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCRBACRowFilterByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCRBACRowFilterByQMS(List<Entity.RBAC.RBACRowFilter> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACRowFilterClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCRBACRowFilterByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从QMS中抓取账户信息", Desc = "从QMS中抓取账户信息", Url = "RBACCloneService.svc/CatchRBACUserByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACUserByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACUserByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACUserClone("QMS", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACUserByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "从QMS中抓取账户数据列表", Desc = "从QMS中抓取账户数据列表", Url = "RBACCloneService.svc/CatchRBACUserDataListByQMS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchRBACUserDataListByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchRBACUserDataListByQMS()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.CatchRBACUserDataList("QMS");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchRBACUserDataListByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的QMS账户数据列表", Desc = "同步抓取的QMS账户数据列表", Url = "RBACCloneService.svc/SYNCRBACUserByQMS", Get = "", Post = "List<Entity.RBAC.RBACUser>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCRBACUserByQMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCRBACUserByQMS(List<Entity.RBAC.RBACUser> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBQMSEmpClone RBACClone = (IBLL.LIIP.RBACClone.IBBQMSEmpClone)context.GetObject("BBQMSEmpClone");
                brdv = RBACClone.RBACUserClone("QMS", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCRBACUserByQMS.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从平台同步科室数据到检验之星6中", Desc = "从平台同步科室数据到检验之星6中", Url = "RBACCloneService.svc/CatchDeptByLIIPGoToLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchDeptByLIIPGoToLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchDeptByLIIPGoToLabStar6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBDeptClone RBACClone = (IBLL.LIIP.RBACClone.IBBDeptClone)context.GetObject("BBDeptClone");
                brdv = RBACClone.HRDeptClone("LabStar6", empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchDeptByLIIPGoToLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "同步抓取的集成平台部门数据列表到检验之星6", Desc = "同步抓取的集成平台部门数据列表到检验之星6", Url = "RBACCloneService.svc/SYNCDeptByLIIPGoToLabStar6", Get = "", Post = "List<Entity.RBAC.HRDept>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCDeptByLIIPGoToLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCDeptByLIIPGoToLabStar6(List<Entity.RBAC.HRDept> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBDeptClone RBACClone = (IBLL.LIIP.RBACClone.IBBDeptClone)context.GetObject("BBDeptClone");
                brdv = RBACClone.HRDeptClone("LabStar6", empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCDeptByLIIPGoToLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从平台同步医生数据到检验之星6中", Desc = "从平台同步医生数据到检验之星6中", Url = "RBACCloneService.svc/CatchDoctorByLIIPGoToLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchDoctorByLIIPGoToLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchDoctorByLIIPGoToLabStar6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                List<string> TableType = new List<string>();
                TableType.Add("DOCTOR");
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.EmpClone_HREmployeeGoToLabStar6Table("LabStar6", TableType, empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchDoctorByLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从平台同步PUser数据到检验之星6中", Desc = "从平台同步PUser数据到检验之星6中", Url = "RBACCloneService.svc/CatchPUserByLIIPGoToLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchPUserByLIIPGoToLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchPUserByLIIPGoToLabStar6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                List<string> TableType = new List<string>();
                TableType.Add("PUSER");
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.EmpClone_HREmployeeGoToLabStar6Table("LabStar6", TableType, empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchPUserByLIIPGoToLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "从平台同步NPUser数据到检验之星6中", Desc = "从平台同步NPUser数据到检验之星6中", Url = "RBACCloneService.svc/CatchNPUserByLIIPGoToLabStar6", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CatchNPUserByLIIPGoToLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue CatchNPUserByLIIPGoToLabStar6()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                List<string> TableType = new List<string>();
                TableType.Add("NPUSER");
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.EmpClone_HREmployeeGoToLabStar6Table("LabStar6", TableType,empid, empname, null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.CatchNPUserByLIIPGoToLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }

            return brdv;
        }

        [ServiceContractDescription(Name = "同步抓取的集成平台员工数据列表到检验之星6医生表", Desc = "同步抓取的集成平台员工数据列表到检验之星6医生表", Url = "RBACCloneService.svc/SYNCDoctortByLIIPGoToLabStar6", Get = "", Post = "List<Entity.RBAC.HREmployee>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCDoctortByLIIPGoToLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCDoctortByLIIPGoToLabStar6(List<Entity.RBAC.HREmployee> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                List<string> TableType = new List<string>();
                TableType.Add("DOCTOR");
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.EmpClone_HREmployeeGoToLabStar6TableByEntity("LabStar6", TableType, empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCDoctortByLIIPGoToLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }
            return brdv;
        }
        [ServiceContractDescription(Name = "同步抓取的集成平台员工数据列表到检验之星6PUser表", Desc = "同步抓取的集成平台员工数据列表到检验之星6PUser表", Url = "RBACCloneService.svc/SYNCPUserByLIIPGoToLabStar6", Get = "", Post = "List<Entity.RBAC.HREmployee>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCPUserByLIIPGoToLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCPUserByLIIPGoToLabStar6(List<Entity.RBAC.HREmployee> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                List<string> TableType = new List<string>();
                TableType.Add("PUSER");
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.EmpClone_HREmployeeGoToLabStar6TableByEntity("LabStar6", TableType, empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCPUserByLIIPGoToLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }
            return brdv;
        }

        [ServiceContractDescription(Name = "同步抓取的集成平台员工数据列表到检验之星6NPUser表", Desc = "同步抓取的集成平台员工数据列表到检验之星6NPUser表", Url = "RBACCloneService.svc/SYNCNPUserByLIIPGoToLabStar6", Get = "", Post = "List<Entity.RBAC.HREmployee>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SYNCNPUserByLIIPGoToLabStar6", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue SYNCNPUserByLIIPGoToLabStar6(List<Entity.RBAC.HREmployee> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                List<string> TableType = new List<string>();
                TableType.Add("NPUSER");
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid is null || empid.Trim().Length <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能获取身份信息！请登录后重试！";
                    return brdv;
                }
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.RBACClone.IBBEmpClone IBBEmpClone = (IBLL.LIIP.RBACClone.IBBEmpClone)context.GetObject("BBEmpClone");
                brdv = IBBEmpClone.EmpClone_HREmployeeGoToLabStar6TableByEntity("LabStar6", TableType, empid, empname, entity);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.RBACCloneService.SYNCNPUserByLIIPGoToLabStar6.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "抓取错误！";
            }
            return brdv;
        }

    }
}
