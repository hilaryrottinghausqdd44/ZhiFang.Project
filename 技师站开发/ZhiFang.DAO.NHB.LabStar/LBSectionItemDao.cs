using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBSectionItemDao : BaseDaoNHB<LBSectionItem, long>, IDLBSectionItemDao
    {
        public IList<LBSectionItem> QueryLBSectionItemDao(string strHqlWhere)
        {
            string strHQL = " select lbsectionitem from LBSectionItem lbsectionitem " +
                            " left join fetch lbsectionitem.LBSection lbsection " +
                            " left join fetch lbsectionitem.LBItem lbitem ";
            return this.GetListByHQL(strHqlWhere, strHQL);
        }

        public EntityList<LBSectionItem> QueryLBSectionItemDao(string strHqlWhere, string Order, int start, int count)
        {
            //EntityList<LBSectionItem> entityList = new EntityList<LBSectionItem>();  
            string strHQL = " select lbsectionitem from LBSectionItem lbsectionitem " +
                            " left join fetch lbsectionitem.LBSection lbsection " +
                            " left join fetch lbsectionitem.LBItem lbitem ";
            return this.GetListByHQL(strHqlWhere, Order, start, count, strHQL);
            //return entityList;
        }

        public EntityList<LBSectionItemVO> QueryLBSectionItemVODao(string strHqlWhere, string Order, int start, int count)
        {
            EntityList<LBSectionItemVO> entityVOList = new EntityList<LBSectionItemVO>();
            entityVOList.list = new List<LBSectionItemVO>();
            string strHQL = " select lbsectionitem from LBSectionItem lbsectionitem " +
                " left join fetch lbsectionitem.LBSection lbsection " +
                " left join fetch lbsectionitem.LBItem lbitem ";
            string strHQLCount = "select count(*) from LBSectionItem lbsectionitem " +
                " left join lbsectionitem.LBSection lbsection " +
                " left join lbsectionitem.LBItem lbitem ";
            EntityList<LBSectionItem> entityList = this.GetListByHQL(strHqlWhere, Order, start, count, strHQL, strHQLCount);
            if (entityList.count > 0 && entityList.list != null)
            {
                foreach (LBSectionItem sectionItem in entityList.list)
                {
                    LBSectionItemVO tempVO = new LBSectionItemVO();
                    tempVO.LBSectionItem = sectionItem;
                    tempVO.LBSection = sectionItem.LBSection;
                    tempVO.LBItem = sectionItem.LBItem;
                    entityVOList.list.Add(tempVO);
                }
                entityVOList.count = entityList.count;
            }
            return entityVOList;
        }
    }

    public class LBSectionItemVODao : BaseDaoNHB<LBSectionItemVO, long>, IDLBSectionItemVODao
    {
        public EntityList<LBSectionItemVO> QueryLBSectionItemVODao(string strHqlWhere, string Order, int start, int count)
        {
            //EntityList<LBSectionItemVO> entityList = new EntityList<LBSectionItemVO>();   
            string strHQL = " select {0} from LBSectionItem lbsectionitem " +
                " left join lbsectionitem.LBSection lbsection " +
                " left join lbsectionitem.LBItem lbitem ";
            return this.GetListByHQL(strHqlWhere, Order, start, count, string.Format(strHQL, "new LBSectionItemVO(lbsectionitem,lbsection,lbitem)"), string.Format(strHQL, "count(*)"));
            //return entityList;
        }

        public IList<LBSectionItemVO> QueryLBSectionItemVODao(string strHqlWhere)
        {
            //IList<LBSectionItemVO> list = new IList<LBSectionItemVO>();
            string strHQL = " select new LBSectionItemVO(lbsectionitem,lbsection,lbitem) from LBSectionItem lbsectionitem " +
                " left join lbsectionitem.LBSection lbsection " +
                " left join lbsectionitem.LBItem lbitem where 1=1 ";
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL += " and " + strHqlWhere;
            }
            strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<LBSectionItem>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            return this.Session.CreateQuery(strHQL).List<LBSectionItemVO>();
            //return list;
        }
    }
}