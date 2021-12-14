using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Common.Public;

namespace ZhiFang.DAO.NHB.RBAC
{
    public class RBACEmpRolesDao : Base.BaseDaoNHB<RBACEmpRoles, long>, IDRBACEmpRolesDao
    {
        public IList<RBACEmpRoles> GetListByRoleIdList(List<long> roleidlist)
        {
            List<RBACEmpRoles> emplist = new List<RBACEmpRoles>();
            if (roleidlist != null && roleidlist.Count > 0)
            {
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsPlatform") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIPUrl") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsPlatform") == "0")//判断是否是集成平台
                {
                    string platformrbacurl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIPUrl") + "/ServerWCF/RBACService.svc";
                    string param = "/RBAC_UDTO_GetRBACEmpRolesListByRoleIdList?idlist=" + string.Join(",", roleidlist.ToArray());
                    string tmpjson = ProjectProgressMonitorManage.Common.RestfullHelper.InvkerRestServiceByGet(platformrbacurl, param);
                    if (tmpjson != null && tmpjson.Trim().Length > 0)
                    {
                        Entity.Base.BaseResultDataValue resultvalue = JsonSerializer.JsonDotNetDeserializeObject<ZhiFang.Entity.Base.BaseResultDataValue>(tmpjson);
                        if (resultvalue.success)
                        {
                            if (resultvalue.ResultDataValue != null && resultvalue.ResultDataValue.Trim() != "")
                            {
                                emplist = JsonSerializer.JsonDotNetDeserializeObject<List<RBACEmpRoles>>(resultvalue.ResultDataValue);

                                return emplist;
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetListByRoleIdList;resultvalue.ResultDataValue返回为空！platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                                return null;
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetListByRoleIdList;resultvalue.Success=" + resultvalue.success + ";resultvalue.ErrorInfo=" + resultvalue.ErrorInfo + ";platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                            return null;
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetListByRoleIdList;tmpjson返回为空！platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                        return null;
                    }
                }
                else
                {
                    return this.GetListByHQL(" RBACRole.Id in (" + string.Join(",", roleidlist.ToArray()) + ")").ToList();
                }
            }
            else
            {
                return emplist;
            }
        }
    }
}