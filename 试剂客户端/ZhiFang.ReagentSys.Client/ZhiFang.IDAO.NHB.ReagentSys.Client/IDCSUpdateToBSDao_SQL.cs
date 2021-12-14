using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDCSUpdateToBSDao_SQL
    {
        /// <summary>
        /// 获得CS的PDept数据列表
        /// </summary>
        IList<HRDept> GetHRDeptList(string strWhere);
        /// <summary>
        /// 获得CS的人员帐号数据列表
        /// </summary>
        IList<RBACUser> GetRBACUserAndHREmployeeList(string strWhere);
        /// <summary>
        /// 获得CS的RBACRole数据列表
        /// </summary>
        IList<RBACRole> GetRBACRoleList(string strWhere);
        /// <summary>
        /// 获得CS的ReaCenOrg数据列表
        /// </summary>
        IList<ReaCenOrg> GetReaCenOrgList(string strWhere);
        /// <summary>
        /// 获得CS的ReaStorage数据列表
        /// </summary>
        IList<ReaStorage> GetReaStorageList(string strWhere);
        /// <summary>
        /// 获得CS的ReaPlace数据列表
        /// </summary>
        IList<ReaPlace> GetReaPlaceList(string strWhere);
        /// <summary>
        /// 获得CS的ReaTestEquipLab数据列表
        /// </summary>
        IList<ReaTestEquipLab> GetReaTestEquipLabList(string strWhere);
        /// <summary>
        /// 获得CS的ReaGoods数据列表
        /// </summary>
        IList<ReaGoods> GetReaGoodsList(string strWhere);
        /// <summary>
        /// 获得CS的ReaEquipReagentLink数据列表
        /// </summary>
        IList<ReaEquipReagentLink> GetReaEquipReagentLinkList(string strWhere);
        /// <summary>
        /// 获得CS的ReaGoodsOrgLink数据列表
        /// </summary>
        IList<ReaGoodsOrgLink> GetReaGoodsOrgLinkList(string strWhere);
        /// <summary>
        /// 获得CS的ReaDeptGoods数据列表
        /// </summary>
        IList<ReaDeptGoods> GetReaDeptGoodsList(string strWhere);
        /// <summary>
        /// 获得CS的ReaBmsQtyDtl数据列表
        /// </summary>
        IList<ReaBmsQtyDtl> GetReaBmsQtyDtlList(string strWhere);
        /// <summary>
        /// 获得CS的ReaBmsSerial数据列表
        /// </summary>
        IList<ReaBmsSerial> GetReaBmsSerialList(string strWhere);
    }
}
