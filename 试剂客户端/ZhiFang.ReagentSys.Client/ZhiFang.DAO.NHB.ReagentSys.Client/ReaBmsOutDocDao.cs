using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{	
	public class ReaBmsOutDocDao : BaseDaoNHB<ReaBmsOutDoc, long>, IDReaBmsOutDocDao
	{
        /// <summary>
        /// 智方试剂平台使用
        /// 查询 状态=出库单上传平台 且 订货方类型=调拨 的出库单
        /// </summary>
        public EntityList<ReaBmsOutDoc> GetPlatformOutDocListByDBClient(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<ReaBmsOutDoc> entityList = new EntityList<ReaBmsOutDoc>();

            string labId = ZhiFang.ReagentSys.Client.Common.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
            if (string.IsNullOrEmpty(labId))
            {
                throw new System.Exception("未能获取到登录人的LabID信息，请重新登陆后重试！");
            }

            string hql = "select new ReaBmsOutDoc(reabmsoutdoc,reacenorg) from ReaBmsOutDoc reabmsoutdoc,ReaCenOrg reacenorg where reabmsoutdoc.ReaServerLabcCode=reacenorg.PlatformOrgNo";
            hql += " and reacenorg.OrgType=1 and reacenorg.NextBillType=3 and reacenorg.LabID=" + labId;
            if (!string.IsNullOrEmpty(strHqlWhere))
            {
                hql += " and " + strHqlWhere;
            }

            int? start1 = null;
            int? count1 = null;
            if (page > 0)
            {
                start1 = page;
            }
            if (limit > 0)
            {
                count1 = limit;
            }
            GetDataRowRoleHQLString(false);
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsOutDoc>, ReaBmsOutDoc> action = new DaoNHBSearchByHqlAction<List<ReaBmsOutDoc>, ReaBmsOutDoc>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaBmsOutDoc>>(action);
            if (list != null)
            {
                entityList.list = list;
            }

            string strHQL = "select count(DISTINCT reabmsoutdoc.Id) from ReaBmsOutDoc reabmsoutdoc,ReaCenOrg reacenorg where reabmsoutdoc.ReaServerLabcCode=reacenorg.PlatformOrgNo";
            strHQL += " and reacenorg.OrgType=1 and reacenorg.NextBillType=3 and reacenorg.LabID=" + labId;
            if (!string.IsNullOrEmpty(strHqlWhere))
            {
                strHQL += " and " + strHqlWhere;
            }
            strHQL += " and " + DataRowRoleHQLString;

            DaoNHBGetCountByHqlAction<ReaBmsOutDoc> actionCount = new DaoNHBGetCountByHqlAction<ReaBmsOutDoc>(strHQL);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }
    } 
}