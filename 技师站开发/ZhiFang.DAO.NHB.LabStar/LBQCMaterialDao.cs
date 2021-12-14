using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBQCMaterialDao : BaseDaoNHB<LBQCMaterial, long>, IDLBQCMaterialDao
    {
        public EntityList<LBQCMaterial> QueryLBQCMaterialDao(string strHqlWhere, string Order, int start, int count)
        {
            string strHQL = " select lbqcmaterial from LBQCMaterial lbqcmaterial " +
                            " left join fetch lbqcmaterial.LBQCPMat lbqcpmat " +
                            " left join fetch lbqcmaterial.LBEquip lbequip ";
            string strHQLCount = " select count(*) from  LBQCMaterial lbqcmaterial " +
                                 " left join lbqcmaterial.LBQCPMat lbqcpmat " +
                                 " left join lbqcmaterial.LBEquip lbequip ";
            return this.GetListByHQL(strHqlWhere, Order, start, count, strHQL, strHQLCount);
        }
    }
}