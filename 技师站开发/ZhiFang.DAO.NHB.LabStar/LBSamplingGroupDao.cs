using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBSamplingGroupDao : BaseDaoNHB<LBSamplingGroup, long>, IDLBSamplingGroupDao
    {
        public EntityList<LBSamplingGroup> QueryLBSamplingGroupByFetchDao(string strHqlWhere, string Order, int start, int count)
        {
            string strHQL = " select lbsamplinggroup from LBSamplingGroup lbsamplinggroup " +
                            " left join fetch lbsamplinggroup.LBTcuvete lbtcuvete ";
            return this.GetListByHQL(strHqlWhere, Order, start, count, strHQL);
        }
    }
}