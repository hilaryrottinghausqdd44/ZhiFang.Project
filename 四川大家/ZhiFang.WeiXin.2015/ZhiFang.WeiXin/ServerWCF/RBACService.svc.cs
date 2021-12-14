using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Web;
using ZhiFang.Entity.RBAC;
using ZhiFang.Common.Public;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RBACService : RBACServiceCommon, ZhiFang.WeiXin.ServerContract.IRBACService
    {
        public BaseResultDataValue RBAC_UDTO_GetHREmployeeByManagerEmpId(string EmpId, string where, int limit, int page, bool isPlanish, string fields, string sort, string type)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HREmployee> tempEntityList = new EntityList<HREmployee>();
            try
            {
                ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetHREmployeeByManagerEmpId参数：EmpId：" + EmpId + ", where：" + where + ", limit：" + limit + ", page：" + page + ", isPlanish：" + EmpId + ", fields：" + fields + ", sort：" + sort + ",type：" + type + "");
                if (type == null || type.Trim() == "")
                {
                    tempBaseResultDataValue.ErrorInfo = "RBAC_UDTO_GetHREmployeeByManagerEmpId.type为空！";
                    return tempBaseResultDataValue;
                }
                if (EmpId == null || EmpId.Trim() == "")
                {
                    EmpId = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                }
                if (type.ToLower().Trim() == "dept")
                {
                    tempEntityList = IBHREmployee.SearchHREmployeeByManagerEmpId(long.Parse(EmpId), where, page, limit, CommonServiceMethod.GetSortHQL(sort));
                }
                if (type.ToLower().Trim() == "all")
                {
                    if ((sort != null) && (sort.Length > 0))
                    {
                        tempEntityList = IBHREmployee.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                    }
                    else
                    {
                        tempEntityList = IBHREmployee.SearchListByHQL(where, page, limit);
                    }
                }

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HREmployee>(tempEntityList.list);
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
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
    }
}
