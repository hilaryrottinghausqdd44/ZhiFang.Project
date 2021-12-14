using ZhiFang.IDAO.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.BloodTransfusion
{
	public interface IDBloodBOutFormDao : IDBaseDao<BloodBOutForm, string>
	{
        /// <summary>
        /// 获取已发血但是未交接完成的病人清单
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBReqFormVO> SearchBloodBReqFormVOOfHandoverByHQL(string strHqlWhere, string sort, int page, int limit);
        /// <summary>
        /// 联合查询
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBOutForm> SearchBloodBOutFormOfLeftJoinByHQL(string strHqlWhere, string sort, int page, int limit);

    } 
}