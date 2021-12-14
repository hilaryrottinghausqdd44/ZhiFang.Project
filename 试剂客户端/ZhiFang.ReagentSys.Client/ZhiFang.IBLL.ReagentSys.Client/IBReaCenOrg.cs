using System.Data;
using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaCenOrg : IBGenericManager<ReaCenOrg>
    {
        /// <summary>
        /// 新增或编辑保存前验证
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResultBool EditVerification(ReaCenOrg entity);
        /// <summary>
        /// 根据机构ID查询机构信息单列树
        /// </summary>
        /// <param name="id">部门ID</param>
        /// 机构ID等于0时,查询所有机构信息
        /// <returns>BaseResultTree</returns>
        BaseResultTree SearchReaCenOrgTreeByOrgID(long id, int orgType);
        /// <summary>
        /// 根据机构ID查询机构信息列表树
        /// </summary>
        /// <param name="id">机构ID</param>
        /// 机构ID等于0时 查询所有机构信息
        /// <returns>BaseResultTree</returns>
        BaseResultTree<ReaCenOrg> SearchReaCenOrgListTreeByOrgID(long id, int orgType);
        /// <summary>
        /// (可获取机构子孙节点)机构信息列表数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isSearchChild"></param>
        /// <returns></returns>
        EntityList<ReaCenOrg> SearchReaCenOrgAndChildListByHQL(string where, string sort, int page, int limit, bool isSearchChild);

        string SearchOrgIDStrListByOrgID(long orgId, int orgType);

        BaseResultData AddReaCenOrgSyncByInterface(ReaBmsCenSaleDoc saleDoc, ref ReaCenOrg reaOrg, ref int orgNo);

        BaseResultData AddReaCenOrgSyncByInterface(ReaBmsCenSaleDtl saleDtl, ref ReaCenOrg reaOrg, ref int orgNo);

        BaseResultData AddReaCenOrgSyncByInterface(string syncField, string syncFieldValue, Dictionary<string, object> dicFieldAndValue);
        /// <summary>
        /// 保存同步物资接口的供货商信息
        /// </summary>
        /// <param name="editReaCenOrgList"></param>
        /// <returns></returns>
        BaseResultData SaveReaCenOrgByMatchInterface(IList<ReaCenOrg> editReaCenOrgList);
    }
}