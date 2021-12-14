using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBItemCalcDao : BaseDaoNHB<LBItemCalc, long>, IDLBItemCalcDao
    {
        public EntityList<LBItemCalc> QueryLBItemCalcDao(string strHqlWhere, string Order, int start, int count)
        {
            //EntityList<LBItemCalc> entityList = new EntityList<LBItemCalc>();   
            string strHQL = " select lbitemcalc from LBItemCalc lbitemcalc " +
                " left join fetch lbitemcalc.LBCalcItem lbcalcitem " +
                " left join fetch lbitemcalc.LBItem lbitem ";
            return this.GetListByHQL(strHqlWhere, Order, start, count, strHQL);
            //return entityList;
        }
    }
}