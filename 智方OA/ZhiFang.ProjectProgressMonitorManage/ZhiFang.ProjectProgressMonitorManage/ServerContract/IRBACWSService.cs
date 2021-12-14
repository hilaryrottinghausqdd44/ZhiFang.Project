using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ZhiFang.Entity.RBAC;
using System.ServiceModel.Web;
using System.ComponentModel;
using ZhiFang.Entity.Base;

namespace ZhiFang.ProjectProgressMonitorManage.ServerContract
{
    [ServiceContract]
    public interface IRBACWSService
    {
        [OperationContract(Name="RBAC_UDTO_AddRBACUser_WS")]
        BaseResultBool RBAC_UDTO_AddRBACUser(RBACUser entity);

        [OperationContract(Name="RBAC_UDTO_UpdateRBACUser_WS")]
        BaseResultBool RBAC_UDTO_UpdateRBACUser(RBACUser entity);

        [OperationContract(Name="RBAC_UDTO_DelRBACUser_WS")]
        BaseResultBool RBAC_UDTO_DelRBACUser(long id);

        [OperationContract(Name="RBAC_UDTO_SearchRBACUser_WS")]
        BaseResultDataValue RBAC_UDTO_SearchRBACUser(RBACUser entity);

        [OperationContract(Name="RBAC_UDTO_SearchRBACUserByHQL_WS")]
        BaseResultDataValue RBAC_UDTO_SearchRBACUserByHQL(int page, int limit, string fields, string where, bool isPlanish);

        [OperationContract(Name="RBAC_UDTO_SearchRBACUserById_WS")]
        BaseResultDataValue RBAC_UDTO_SearchRBACUserById(long id, string fields, bool isPlanish);
    }
}