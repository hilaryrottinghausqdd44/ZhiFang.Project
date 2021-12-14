using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
	public interface IDReaBmsOutDocDao : IDBaseDao<ReaBmsOutDoc, long>
	{
        /// <summary>
        /// 智方试剂平台使用
        /// 查询 状态=出库单上传平台 且 订货方类型=调拨 的出库单
        /// </summary>
        EntityList<ReaBmsOutDoc> GetPlatformOutDocListByDBClient(string strHqlWhere, string sort, int page, int limit);

    } 
}