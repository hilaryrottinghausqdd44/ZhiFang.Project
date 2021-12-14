using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBEquipItemDao : BaseDaoNHB<LBEquipItem, long>, IDLBEquipItemDao
    {
        public EntityList<LBEquipItem> QueryLBEquipItemDao(string strHqlWhere, string Order, int start, int count)
        {
            string strHQL = " select lbequipitem from LBEquipItem lbequipitem " +
                            " left join fetch lbequipitem.LBEquip lbequip " +
                            " left join fetch lbequipitem.LBItem lbitem ";
            string strHQLCount = " select count(*) from LBEquipItem lbequipitem " +
                                 " left join lbequipitem.LBEquip lbequip " +
                                 " left join lbequipitem.LBItem lbitem ";
            return this.GetListByHQL(strHqlWhere, Order, start, count, strHQL, strHQLCount);
        }

        public EntityList<LBEquipItemVO> QueryLBEquipItemVODao(string strHqlWhere, string Order, int start, int count)
        {
            EntityList<LBEquipItemVO> entityVOList = new EntityList<LBEquipItemVO>();
            entityVOList.list = new List<LBEquipItemVO>();
            string strHQL = " select lbequipitem from LBEquipItem lbequipitem " +
                            " left join fetch lbequipitem.LBEquip lbequip " +
                            " left join fetch lbequipitem.LBItem lbitem ";
            string strHQLCount = " select count(*) from LBEquipItem lbequipitem " +
                                 " left join lbequipitem.LBEquip lbequip " +
                                 " left join lbequipitem.LBItem lbitem ";
            EntityList<LBEquipItem> entityList = this.GetListByHQL(strHqlWhere, Order, start, count, strHQL, strHQLCount);
            if (entityList.count > 0 && entityList.list != null)
            {
                foreach (LBEquipItem equipItem in entityList.list)
                {
                    LBEquipItemVO tempVO = new LBEquipItemVO();
                    tempVO.LBEquipItem = equipItem;
                    tempVO.LBEquip = equipItem.LBEquip;
                    tempVO.LBItem = equipItem.LBItem;
                    entityVOList.list.Add(tempVO);
                }
                entityVOList.count = entityList.count;
            }
            return entityVOList;
        }

        /// <summary>
        /// 查找小组项目是否已经设置了仪器项目
        /// </summary>
        /// <param name="sectionID">小组ID</param>
        /// <param name="itemID">项目ID</param>
        /// <returns></returns
        public IList<LBEquipItem> QueryIsExistSectionItemDao(long sectionID, long itemID)
        {
            //EntityList<LBEquipItem> entityList = new EntityList<LBEquipItem>();  
            string strHQL = " select lbequipitem from LBEquipItem lbequipitem " +
                            " left join fetch lbequipitem.LBItem lbitem " +
                            " left join lbequipitem.LBEquip.LBSection lbsection " +
                            //" where lbsection.Id="+ sectionID + " and lbitem.Id=" + itemID;
                            " where lbequipitem.LBEquip.LBSection.Id=" + sectionID + " and lbequipitem.LBItem.Id=" + itemID;
            IList<LBEquipItem> entityList = this.GetListByHQL("", strHQL);
            return entityList;
        }
    }

    public class LBEquipItemVODao : BaseDaoNHB<LBEquipItemVO, long>, IDLBEquipItemVODao
    {
        public EntityList<LBEquipItemVO> QueryLBEquipItemVODao(string strHqlWhere, string Order, int start, int count)
        {
            EntityList<LBEquipItemVO> entityList = new EntityList<LBEquipItemVO>();
            string strHQL = " select {0} from LBEquipItem lbequipitem " +
                " left join lbequipitem.LBEquip lbequip " +
                " left join lbequipitem.LBItem lbitem ";
            entityList = this.GetListByHQL(strHqlWhere, Order, start, count, string.Format(strHQL, "new LBEquipItemVO(lbequipitem,lbequip,lbitem)"), string.Format(strHQL, "count(*)"));
            return entityList;
        }
    }
}