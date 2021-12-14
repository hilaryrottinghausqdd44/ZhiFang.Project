/*************************************
Author: douss
Project: 请求集成平台服务获取列表
Date: 2021-09-23
Modify: 
*************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.Entity.RBAC
{
    public class RequestLIIPService
    {
        /// <summary>
        /// 请求集成平台科室服务
        /// </summary>
        public static EntityList<HRDept> SearchHRDeptByHQL(int page, int limit, string fields, string where, string sort)
        {
            EntityList<HRDept> tempEntityList = new EntityList<HRDept>();
            try
            {
                if (where.IndexOf("like") > 0)
                {
                    //where中包含like时，比如 like '%10021%',接收时%10变成乱码，导致查询不到，所以单独对where进行一下编码
                    where = HttpContext.Current.Server.UrlEncode(where);
                }
                if (string.IsNullOrWhiteSpace(fields))
                {
                    fields = "HRDept_Id,HRDept_ParentID,HRDept_LevelNum,HRDept_UseCode,HRDept_StandCode,HRDept_DeveCode,HRDept_CName,HRDept_EName,HRDept_SName,HRDept_Shortcode,HRDept_PinYinZiTou,HRDept_Comment,HRDept_IsUse,HRDept_DispOrder,HRDept_DataUpdateTime,HRDept_Tel,HRDept_Fax,HRDept_ZipCode,HRDept_Address,HRDept_Contact,HRDept_ParentOrg,HRDept_OrgType,HRDept_ManagerID,HRDept_ManagerName,HRDept_OrgCode,HRDept_MatchCode,HRDept_DataAddTime";
                }
                if (string.IsNullOrWhiteSpace(sort))
                {
                    sort = "[{property:\"HRDept_Id\",direction:\"ASC\"}]";
                }
                string baseUrl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIP_URL");
                string url = baseUrl + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL";
                string para = string.Format("page={0}&limit={1}&fields={2}&where={3}&sort={4}&isPlanish={5}", page, limit, fields, where, sort, false);
                BaseResultDataValueLIIP resultBHospital = WebRequestHelp.WebHttpGet(url, para);
                if (resultBHospital.success && !string.IsNullOrWhiteSpace(resultBHospital.ResultDataValue))
                {
                    JObject jobj = (JObject)JsonConvert.DeserializeObject(resultBHospital.ResultDataValue);
                    JArray arr = JArray.Parse(jobj["list"].ToString());
                    string count = jobj["count"].ToString();

                    tempEntityList.list = arr.ToObject<List<HRDept>>();
                    tempEntityList.count = Convert.ToInt32(count);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("请求平台服务获取部门异常:" + ex.Message);
            }
            return tempEntityList;
        }
    }
}
