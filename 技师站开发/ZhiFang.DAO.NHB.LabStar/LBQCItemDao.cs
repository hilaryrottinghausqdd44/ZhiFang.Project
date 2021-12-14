using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBQCItemDao : BaseDaoNHB<LBQCItem, long>, IDLBQCItemDao
    {
        public EntityList<LBQCItem> QueryLBQCItemDao(string strHqlWhere, string Order, int start, int count)
        {
            string strHQL = " select lbqcitem from LBQCItem lbqcitem " +
                            " left join fetch lbqcitem.LBQCMaterial lbqcmaterial " +
                            " left join fetch lbqcitem.LBEquip lbequip " +
                            " left join fetch lbqcitem.LBItem lbitem ";
            string strHQLCount = " select count(*) from LBQCItem lbqcitem " +
                                 " left join lbqcitem.LBQCMaterial lbqcmaterial " +
                                 " left join lbqcitem.LBEquip lbequip " +
                                 " left join lbqcitem.LBItem lbitem ";
            return this.GetListByHQL(strHqlWhere, Order, start, count, strHQL, strHQLCount);
        }
    }
}