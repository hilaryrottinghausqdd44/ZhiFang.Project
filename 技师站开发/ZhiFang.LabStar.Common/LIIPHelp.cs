using System.Collections.Generic;
using System.Net;
using ZhiFang.Entity.Base;

namespace ZhiFang.LabStar.Common
{
    public class LIIPBaseResultDataValue : BaseResultDataValue
    {

    }
    public static class LIIPBaseResultDataValueExtend
    {
        /// <summary>
        /// 转换为IList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lIIPBaseResultDataValue"></param>
        /// <returns></returns>
        public static IList<T> toList<T>(this LIIPBaseResultDataValue lIIPBaseResultDataValue) where T : class
        {
            EntityList<T> elist = toEntityList<T>(lIIPBaseResultDataValue);
            if (elist != null)
            {
                if (elist.list.Count > 0)
                {
                    return elist.list;
                }
            }
            return null;
        }
        /// <summary>
        /// 转换为EntityList类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lIIPBaseResultDataValue"></param>
        /// <returns></returns>
        public static EntityList<T> toEntityList<T>(this LIIPBaseResultDataValue lIIPBaseResultDataValue) where T : class
        {

            string resultValue = lIIPBaseResultDataValue.ResultDataValue;
            if (resultValue != null && resultValue != "")
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<EntityList<T>>(resultValue.Replace(typeof(T).Name + "_", ""));
            }
            return null;
        }
        /// <summary>
        /// 平台返回类型转为本地BaseResultDataValue类型
        /// </summary>
        /// <param name="lIIPBaseResultDataValue"></param>
        /// <returns></returns>
        public static BaseResultDataValue toBaseResultDataValue(this LIIPBaseResultDataValue lIIPBaseResultDataValue)
        {
            return new BaseResultDataValue()
            {
                success = lIIPBaseResultDataValue.success,
                ResultDataValue = lIIPBaseResultDataValue.ResultDataValue,
                ErrorInfo = lIIPBaseResultDataValue.ErrorInfo,
                ResultDataFormatType = lIIPBaseResultDataValue.ResultDataFormatType
            };
        }
    }
    public class LIIPHelp
    {
        /// <summary>
        /// 调用平台--查询医院人员关系表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue SearchBHospitalEmpLinkByHQL(string where, bool isPlanish = true, string fields = null, string sort = null, int page = 0, int limit = 0)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            #region 组织url
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang_LIIP_Url") + "/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalEmpLinkByHQL";//ST_UDTO_SearchBHospitalEmpLinkByHQL
            url += "?isPlanish=" + isPlanish + "&page=" + page;
            if (limit > 0)
            {
                url += "&limit=" + limit;
            }
            if (where != null)
            {
                where += " and IsUse=1";
                where = where.Replace("LinkType", "LinkTypeID");
                where = where.Replace("empId", "EmpID");
                where = where.Replace("EmpId", "EmpID");
                where = where.Replace("LabId", "HospitalID");
                where = where.Replace("order by LabNo asc", "order by HospitalCode asc");
                url += "&where=" + where;
            }
            else
            {
                url += "&where=IsUse=1";
            }
            //if (fields!=null)
            //{
            //    fields = fields.Replace("BLaboratory", "BHospital");

            //    url += "&fields=" + fields;
            //}
            if (sort != null)
            {
                sort = sort.Replace("BEmpLaboratoryLink_LabNo", "BHospitalEmpLink_HospitalCode");
                url += "&sort=" + sort;
            }
            #endregion
            string result = Common.HTTPRequest.Get(url);

            //ZhiFang.Common.Log.Log.Debug("LIIPHelp.SearchBHospitalEmpLinkByHQL.result1:" + result);
            #region 解析返回的数据
            result = result.Replace("BHospitalEmpLink_HospitalName", "BEmpLaboratoryLink_LabName");
            result = result.Replace("BHospitalEmpLink_HospitalCode", "BEmpLaboratoryLink_LabNo");
            result = result.Replace("BHospitalEmpLink_HospitalID", "BEmpLaboratoryLink_LabId");
            result = result.Replace("BHospitalEmpLink_LinkTypeID", "BEmpLaboratoryLink_LinkType");
            result = result.Replace("BHospitalEmpLink_LinkTypeName", "BEmpLaboratoryLink_LinkTypeName");
            result = result.Replace("BHospitalEmpLink_EmpID", "BEmpLaboratoryLink_EmpId");
            result = result.Replace("BHospitalEmpLink_EmpName", "BEmpLaboratoryLink_EmpName");
            result = result.Replace("BHospitalEmpLink_Id", "BEmpLaboratoryLink_Id");

            //为空的属性赋值
            result = result.Replace("\\\"\\\"", "null");
            //ZhiFang.Common.Log.Log.Debug("LIIPHelp.SearchBHospitalEmpLinkByHQL.result2:" + result);
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);

            return lIIPBaseResultDataValue;
        }
        /// <summary>
        /// 调用平台--查询医院区域信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue SearchBHospitalAreaByHQL(string where, bool isPlanish = true, string fields = null, string sort = null, int page = 0, int limit = 0)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            #region 组织url
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang_LIIP_Url") + "/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaByHQL";
            url += "?isPlanish=" + isPlanish + "&page=" + page;
            if (limit > 0)
            {
                url += "&limit=" + limit;
            }
            if (where != null)
            {
                where += " and IsUse=1";
                url += "&where=" + where;
            }
            else
            {
                url += "&where=IsUse=1";
            }
            if (sort != null)
            {
                url += "&sort=" + sort;
            }
            #endregion
            //ZhiFang.Common.Log.Log.Debug("LIIPHelp.SearchBHospitalAreaByHQL.url:" + url);
            string result = Common.HTTPRequest.Get(url);
            //ZhiFang.Common.Log.Log.Debug("LIIPHelp.SearchBHospitalAreaByHQL.result1:" + result);
            #region 解析返回的数据
            result = result.Replace("BHospitalArea_CenterHospitalID", "BLaboratoryArea_CenterHospitalID");
            result = result.Replace("BHospitalArea_Name", "BLaboratoryArea_AreaCName");
            result = result.Replace("BHospitalArea_Code", "BLaboratoryArea_AreaShortName");
            result = result.Replace("BHospitalArea_DataAddTime", "BLaboratoryArea_DataAddTime");
            result = result.Replace("BHospitalArea_DispOrder", "BLaboratoryArea_DispOrder");
            result = result.Replace("BHospitalArea_Id", "BLaboratoryArea_Id");
            result = result.Replace("BHospitalArea_LabID", "BLaboratoryArea_LabID");
            //为空的属性赋值
            //ZhiFang.Common.Log.Log.Debug("LIIPHelp.SearchBHospitalAreaByHQL.result2:" + result);
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);

            return lIIPBaseResultDataValue;
        }

        /// <summary>
        /// 调用平台--查询医院信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue SearchBHospitalByHQL(string where, bool isPlanish = true, string fields = null, string sort = null, int page = 0, int limit = 0)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            #region 组织url
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang_LIIP_Url") + "/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalByHQL";//ST_UDTO_SearchBHospitalEmpLinkByHQL
            url += "?isPlanish=" + isPlanish + "&page=" + page;
            if (limit > 0)
            {
                url += "&limit=" + limit;
            }
            if (where != null)
            {
                where += " and IsUse=1";
                where = where.Replace("CNAME", "Name");
                where = where.Replace("empId", "EmpID");
                url += "&where=" + where;
            }
            else
            {
                url += "&where=IsUse=1";
            }
            //if (fields!=null)
            //{
            //    fields = fields.Replace("BLaboratory", "BHospital");

            //    url += "&fields=" + fields;
            //}
            if (sort != null)
            {
                sort = sort.Replace("BLaboratory_Id", "BHospital_Id");
                url += "&sort=" + sort;
            }
            #endregion

            string result = Common.HTTPRequest.Get(url);
            //ZhiFang.Common.Log.Log.Debug("LIIPHelp.SearchBHospitalByHQL.result1:"+ result);
            #region 解析返回的数据
            result = result.Replace("BHospital_Name", "BLaboratory_CNAME");
            result = result.Replace("BHospital_Id", "BLaboratory_Id");
            result = result.Replace("BHospital_HospitalCode", "BLaboratory_LabNo");
            result = result.Replace("BHospital_AreaID", "BLaboratory_AreaID");
            result = result.Replace("BHospital_EName", "BLaboratory_ENAME");
            result = result.Replace("BHospital_Shortcode", "BLaboratory_SHORTCODE");
            result = result.Replace("BHospital_IsUse", "BLaboratory_IsUse");
            result = result.Replace("BHospital_Comment", "BLaboratory_Comment");
            result = result.Replace("BHospital_Address", "BLaboratory_ADDRESS");
            //为空的属性赋值
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            //ZhiFang.Common.Log.Log.Debug("LIIPHelp.SearchBHospitalByHQL.result2:" + result);
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }

        /// <summary>
        /// 调用平台--根据医院Id查询医院信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue SearchBHospitalById(long id, string fields = null, bool isPlanish = true)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            #region 组织url
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang_LIIP_Url") + "/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalById";
            url += "?id=" + id;
            url += "&isPlanish=" + isPlanish;
            #endregion
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            result = result.Replace("BHospital_Name", "BLaboratory_CNAME");
            result = result.Replace("BHospital_Id", "BLaboratory_Id");
            result = result.Replace("BHospital_HospitalCode", "BLaboratory_LabNo");
            result = result.Replace("BHospital_AreaID", "BLaboratory_AreaID");
            result = result.Replace("BHospital_EName", "BLaboratory_ENAME");
            result = result.Replace("BHospital_Shortcode", "BLaboratory_SHORTCODE");
            result = result.Replace("BHospital_IsUse", "BLaboratory_IsUse");
            result = result.Replace("BHospital_Comment", "BLaboratory_Comment");
            result = result.Replace("BHospital_HospitalCode", "BLaboratory_LabCode");
            result = result.Replace("BHospital_LinkMan", "BLaboratory_LINKMAN");
            result = result.Replace("BHospital_PhoneNum1", "BLaboratory_PHONENUM1");
            result = result.Replace("BHospital_Address", "BLaboratory_ADDRESS");
            result = result.Replace("BHospital_MAILNO", "BLaboratory_MAILNO");
            result = result.Replace("BHospital_EMAIL", "BLaboratory_EMAIL");
            result = result.Replace("BHospital_PhoneNum2", "BLaboratory_PHONENUM2");
            result = result.Replace("BHospital_FaxNo", "BLaboratory_FaxNo");
            result = result.Replace("BHospital_AutoFax", "BLaboratory_AutoFax");
            //为空的属性赋值
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }

        /// <summary>
        /// 调用平台--根据人员查询人员所属医院
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue SearchHospitalByEmpId(long EmpId)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            /* string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang_LIIP_Url") + "/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalEmpLinkByHQL";//ST_UDTO_SearchBHospitalEmpLinkByHQL
             url += "?isPlanish=true&where=EmpID=" + EmpId + " and LinkTypeID =" + EmpLaboratoryLinkType.所属.Key + " or LinkTypeID = " + EmpLaboratoryLinkType.管理.Key;
             string result = Common.HTTPRequest.Get(url);
             #region 解析返回的数据
             result = result.Replace("BHospitalEmpLink_Id", "BEmpLaboratoryLink_Id");
             result = result.Replace("BHospitalEmpLink_HospitalID", "BEmpLaboratoryLink_LabID");
             result = result.Replace("BHospitalEmpLink_HospitalCode", "BEmpLaboratoryLink_LabNo");
             result = result.Replace("BHospitalEmpLink_HospitalName", "BEmpLaboratoryLink_LabName");
             result = result.Replace("BHospitalEmpLink_EmpID", "BEmpLaboratoryLink_EmpID");
             result = result.Replace("BHospitalEmpLink_EmpName", "BEmpLaboratoryLink_EmpName");

             //为空的属性赋值
             result = result.Replace("\\\"\\\"", "null");
             #endregion
             lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);*/

            return lIIPBaseResultDataValue;
        }

        /// <summary>
        /// 根据ID查询模块信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue SearchRBACModuleById(long Id)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang_LIIP_Url") + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACModuleById";//RBAC_UDTO_SearchBTDAppComponentsByModuleID
            url += "?isPlanish=true&id=" + Id;
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            // result = result.Replace("BHospitalEmpLink_Id", "BEmpLaboratoryLink_Id");
            //为空的属性赋值
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }

        /// <summary>
        /// 根据人员ID查询模块
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue SearchModuleByHREmpID(long Id)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang_LIIP_Url") + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchModuleByHREmpID";//RBAC_UDTO_SearchBTDAppComponentsByModuleID
            url += "?id=" + Id;
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            // result = result.Replace("BHospitalEmpLink_Id", "BEmpLaboratoryLink_Id");
            //为空的属性赋值
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }

        /// <summary>
        /// 查询模块
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue SearchRBACModuleByHQL(string where)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang_LIIP_Url") + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACModuleByHQL";//RBAC_UDTO_SearchBTDAppComponentsByModuleID
            url += "?isPlanish=true&where=" + where;
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            // result = result.Replace("BHospitalEmpLink_Id", "BEmpLaboratoryLink_Id");
            //为空的属性赋值
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }

        /// <summary>
        /// 模拟登陆
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static bool Login(string account, string password, out CookieCollection cookies)
        {

            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang_LIIP_Url") + "/ServerWCF/RBACService.svc/RBAC_BA_Login";//RBAC_UDTO_SearchBTDAppComponentsByModuleID
            url += "?strUserAccount=" + account + "&strPassWord=" + password;
            //ZhiFang.Common.Log.Log.Debug("Login.url:" + url);
            string result = Common.HTTPRequest.Get(url, out cookies);
            #region 解析返回的数据
            // result = result.Replace("BHospitalEmpLink_Id", "BEmpLaboratoryLink_Id");
            //为空的属性赋值
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            ZhiFang.Common.Log.Log.Debug("Login.result:" + result);
            return result.ToUpper().Trim() == "TRUE";
        }

        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue SearchRBACRoleByHQL(string where)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("MsgPlatformServiceUrl") + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL";//RBAC_UDTO_SearchBTDAppComponentsByModuleID
            url += "?isPlanish=true&fields=RBACRole_Id&where=" + where;
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            // result = result.Replace("BHospitalEmpLink_Id", "BEmpLaboratoryLink_Id");
            //为空的属性赋值
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }
        /// <summary>
        /// 根据查询角色人员关系
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue SearchRBACEmpRolesByHQL(string where)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("MsgPlatformServiceUrl") + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL";//RBAC_UDTO_SearchBTDAppComponentsByModuleID
            url += "?isPlanish=true&fields=RBACEmpRoles_RBACRole_Id&where=" + where;
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            result = result.Replace("RBACEmpRoles_RBACRole_Id", "Id");
            //为空的属性赋值
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }

        /// <summary>
        /// 根据医院编码查询医院所属区域的中心医院编码
        /// </summary>
        /// <param name="LabCode"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue GetBHospitalAreaCenterHospitalLabCodeByLabCode(string LabCode)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang_LIIP_Url") + "/ServerWCF/LIIPCommonService.svc/ST_UDTO_GetBHospitalAreaCenterHospitalLabCodeByLabCode?LabCode=" + LabCode;
            ZhiFang.Common.Log.Log.Debug("GetBHospitalAreaCenterHospitalLabCodeByLabCode.url:" + url);
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            //为空的属性赋值
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }

        /// <summary>
        /// 根据账户查询账户所属员工的ID
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue GetEmpIDNameByAccount(string Account)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang_LIIP_Url") + "/ServerWCF/RBACService.svc/GetEmpIDNameByAccount?Account=" + Account;
            ZhiFang.Common.Log.Log.Debug("GetEmpIDNameByAccount.url:" + url);
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            //为空的属性赋值
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }
        /// <summary>
        /// 获取员工身份
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue RBAC_UDTO_SearchHREmpIdentityByHQL(string where)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("MsgPlatformServiceUrl") + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?isPlanish=true";
            url += "&fields=HREmpIdentity_TSysName,HREmpIdentity_SystemName,HREmpIdentity_HREmployee_Id,HREmpIdentity_LabID,HREmpIdentity_HREmployee_CName,HREmpIdentity_HREmployee_StandCode,HREmpIdentity_HREmployee_DeveCode";
            url += "&where=" + where;//hrempidentity.TSysCode='1001001' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = 0 
            ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_SearchHREmpIdentityByHQL.url:" + url);
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            //为空的属性赋值
            result = result.Replace("HREmpIdentity_HREmployee_Id", "Id");
            result = result.Replace("HREmpIdentity_LabID", "LabID");
            result = result.Replace("HREmpIdentity_HREmployee_CName", "CName");
            result = result.Replace("HREmpIdentity_HREmployee_StandCode", "StandCode");
            result = result.Replace("HREmpIdentity_HREmployee_DeveCode", "DeveCode");
            result = result.Replace("HREmpIdentity_SystemName", "SystemName");
            result = result.Replace("HREmpIdentity_TSysName", "TSysName");
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }
        /// <summary>
        /// 获取部门身份
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue RBAC_UDTO_SearchHRDeptIdentityByHQL(string where)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("MsgPlatformServiceUrl") + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL?isPlanish=true";
            url += "&fields=HRDeptIdentity_HRDept_Id,HRDeptIdentity_LabID,HRDeptIdentity_HRDept_CName,HRDeptIdentity_HRDept_StandCode,HRDeptIdentity_HRDept_DeveCode";
            url += "&where=" + where;//hrdeptidentity.TSysCode='1001101' and hrdeptidentity.SystemCode='ZF_LAB_START' and hrdeptidentity.LabID = 0 
            ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_SearchHRDeptIdentityByHQL.url:" + url);
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            //为空的属性赋值
            result = result.Replace("HRDeptIdentity_HRDept_Id", "Id");
            result = result.Replace("HRDeptIdentity_LabID", "LabID");
            result = result.Replace("HRDeptIdentity_HRDept_CName", "CName");
            result = result.Replace("HRDeptIdentity_HRDept_StandCode", "StandCode");
            result = result.Replace("HRDeptIdentity_HRDept_DeveCode", "DeveCode");
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue RBAC_UDTO_SearchHREmployeeByHQL(string where)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("MsgPlatformServiceUrl") + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true";
            url += "&fields=HREmployee_Id,HREmployee_LabID,HREmployee_CName,HREmployee_StandCode,HREmployee_DeveCode";
            url += "&where=" + where;
            ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_SearchHREmployeeByHQL.url:" + url);
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            //为空的属性赋值
            result = result.Replace("HREmployee_Id", "Id");
            result = result.Replace("HREmployee_LabID", "LabID");
            result = result.Replace("HREmployee_CName", "CName");
            result = result.Replace("HREmployee_StandCode", "StandCode");
            result = result.Replace("HREmployee_DeveCode", "DeveCode");
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public static LIIPBaseResultDataValue RBAC_UDTO_SearchHRDeptByHQL(string where)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("MsgPlatformServiceUrl") + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL?isPlanish=true";
            url += "&fields=HRDept_Id,HRDept_LabID,HRDept_CName,HRDept_StandCode,HRDept_DeveCode";
            url += "&where=" + where;
            ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_SearchHRDeptByHQL.url:" + url);
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            //为空的属性赋值
            result = result.Replace("HRDept_Id", "Id");
            result = result.Replace("HRDept_LabID", "LabID");
            result = result.Replace("HRDept_CName", "CName");
            result = result.Replace("HRDept_StandCode", "StandCode");
            result = result.Replace("HRDept_DeveCode", "DeveCode");
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }

        /// <summary>
        /// 根据查询模块ID和角色ID查询模块功能权限
        /// </summary>
        /// <returns></returns>
        public static LIIPBaseResultDataValue SearchRBACRoleRightByHQL(string where)
        {
            LIIPBaseResultDataValue lIIPBaseResultDataValue = new LIIPBaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("MsgPlatformServiceUrl") + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleRightByHQL";//RBAC_UDTO_SearchBTDAppComponentsByModuleID
            url += "?isPlanish=true&fields=RBACRoleRight_RBACModuleOper_StandCode&where=" + where;
            string result = Common.HTTPRequest.Get(url);
            #region 解析返回的数据
            result = result.Replace("RBACRoleRight_RBACModuleOper_StandCode", "StandCode");
            //为空的属性赋值
            result = result.Replace("\\\"\\\"", "null");
            #endregion
            lIIPBaseResultDataValue = Newtonsoft.Json.JsonConvert.DeserializeObject<LIIPBaseResultDataValue>(result);
            return lIIPBaseResultDataValue;
        }
    }
}
