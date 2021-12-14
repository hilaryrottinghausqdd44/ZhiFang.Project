using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBSamplingItemDao : BaseDaoNHB<LBSamplingItem, long>, IDLBSamplingItemDao
    {
        public EntityList<LBSamplingItem> QueryLBSamplingItemByFetchDao(string strHqlWhere, string Order, int start, int count)
        {
            string strHQL = " select lbsamplingitem from LBSamplingItem lbsamplingitem " +
                " left join fetch lbsamplingitem.LBItem lbitem " +
                " left join fetch lbsamplingitem.LBSamplingGroup lbsamplinggroup " +
                " left join fetch lbsamplinggroup.LBTcuvete lbtcuvete ";
            string strHQLCount = " select count(*) from LBSamplingItem lbsamplingitem " +
                " left join lbsamplingitem.LBItem lbitem " +
                " left join lbsamplingitem.LBSamplingGroup lbsamplinggroup " +
                " left join lbsamplinggroup.LBTcuvete lbtcuvete ";
            return this.GetListByHQL(strHqlWhere, Order, start, count, strHQL, strHQLCount);
        }

        //public IList<LBSamplingGroup> QuerySamplingGroupMultiItemDao(string strHqlWhere)
        //{
        //    if (strHqlWhere != null && strHqlWhere.Length > 0)
        //        strHqlWhere = " and " + strHqlWhere;
        //    strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
        //    strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
        //    string listField = "lbsamplinggroup.Id, lbsamplinggroup.CName" +
        //        ",lbsamplinggroup.SampleTypeID,lbsamplinggroup.SuperGroupID,lbsamplinggroup.SName" +
        //        ",lbsamplinggroup.SCode,lbsamplinggroup.Destination" +
        //        ",lbsamplinggroup.Synopsis,lbsamplinggroup.PrintCount" +
        //        ",lbsamplinggroup.AffixTubeFlag,lbsamplinggroup.PrepInfo" +
        //        ",lbsamplinggroup.VirtualNo,lbsamplinggroup.IsUse" +
        //        ",lbsamplinggroup.DispOrder,lbsamplinggroup.LabID" +
        //        ",lbsamplinggroup.DataAddTime,lbsamplinggroup.DataUpdateTime ";
        //    string strHQL = " select " + listField + " from LBSamplingItem lbsamplingitem " +
        //        " left join lbsamplingitem.LBSamplingGroup lbsamplinggroup " +
        //        " left join lbsamplingitem.LBItem lbitem where 1=1 " + strHqlWhere +
        //        " group by " + listField +
        //        " having count(lbitem.Id) > 1";
        //    var listSamplingGroup = this.Session.CreateQuery(strHQL).List();
        //    return null;
        //}

        public IList<LBSamplingGroup> QuerySamplingGroupIsMultiItemDao(string strHqlWhere, bool isMulti)
        {
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<LBSamplingGroup>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strBig = "=";
            if (isMulti)
                strBig = ">";
            string strHQL = "select lbsamplinggroup from LBSamplingGroup lbsamplinggroup " +
                " left join fetch lbsamplinggroup.LBTcuvete lbtcuvete " +
                " where lbsamplinggroup.Id in (select lbsamplingitem.LBSamplingGroup.Id " +
                " from LBSamplingItem lbsamplingitem where 1=1 " + strHqlWhere +
                " group by lbsamplingitem.LBSamplingGroup.Id " +
                " having count(lbsamplingitem.LBItem.Id)" + strBig + "1)";
            IList<LBSamplingGroup> listSamplingGroup = this.Session.CreateQuery(strHQL).List<LBSamplingGroup>();
            return listSamplingGroup;
        }

        public IList<LBItem> QueryItemIsMultiSamplingGroupDao(string strHqlWhere, string strSectionID, bool isMulti)
        {
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<LBItem>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strBig = "=";
            if (isMulti)
                strBig = ">";
            string strHqlSection = "";
            if (strSectionID != null && strSectionID.Length > 0)
            {
                var listSectionID = strSectionID.Split(',');
                foreach (var sectionID in listSectionID)
                {
                    if (strHqlSection == "")
                        strHqlSection = " lbsectionitem.LBSection.Id=" + sectionID;
                    else
                        strHqlSection += " or lbsectionitem.LBSection.Id=" + sectionID;
                }
                strHqlSection = " where (" + strHqlSection + ")";
                strHqlSection += " and " + BaseDataFilter.GetDataRowRoleHQLString<LBItem>();
                strHqlSection = BaseDataFilter.FilterMacroCommand(strHqlSection);
                strHqlSection = " and lbitem.Id in (select lbsectionitem.LBItem.Id from LBSectionItem lbsectionitem " + strHqlSection + ")";
            }
            string strHQL = "select lbitem from LBItem lbitem " +
                " where lbitem.Id in (select lbsamplingitem.LBItem.Id " +
                " from LBSamplingItem lbsamplingitem where 1=1 " + strHqlWhere +
                " group by lbsamplingitem.LBItem.Id " +
                " having count(lbsamplingitem.LBSamplingGroup.Id)" + strBig + "1)" + strHqlSection;
            IList<LBItem> listItem = this.Session.CreateQuery(strHQL).List<LBItem>();
            return listItem;
        }

        public IList<LBItem> QuerySamplingItemIsMultiSamplingGroupDao(string strHqlWhere, string strSectionID, bool isMulti)
        {
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<LBItem>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strBig = "=";
            if (isMulti)
                strBig = ">";
            string strHqlSection = "";
            if (strSectionID != null && strSectionID.Length > 0)
            {
                var listSectionID = strSectionID.Split(',');
                foreach (var sectionID in listSectionID)
                {
                    if (strHqlSection == "")
                        strHqlSection = " lbsectionitem.LBSection.Id=" + sectionID;
                    else
                        strHqlSection += " or lbsectionitem.LBSection.Id=" + sectionID;
                }
                strHqlSection = " where (" + strHqlSection + ")";
                strHqlSection += " and " + BaseDataFilter.GetDataRowRoleHQLString<LBItem>();
                strHqlSection = BaseDataFilter.FilterMacroCommand(strHqlSection);
                strHqlSection = " and lbitem.Id in (select lbsectionitem.LBItem.Id from LBSectionItem lbsectionitem " + strHqlSection + ")";
            }
            string strHQL = "select lbsamplingitem from LBSamplingItem lbsamplingitem " +
                " left join fetch lbsamplingitem.LBSamplingGroup lbsamplinggroup " +
                " left join fetch lbsamplingitem.LBItem lbitem " +
                " where lbitem.Id in (select lbsamplingitem.LBItem.Id " +
                " from LBSamplingItem lbsamplingitem where 1=1 " + strHqlWhere +
                " group by lbsamplingitem.LBItem.Id " +
                " having count(lbsamplingitem.LBSamplingGroup.Id)" + strBig + "1)" + strHqlSection;
            IList<LBItem> listItem = this.Session.CreateQuery(strHQL).List<LBItem>();
            return listItem;
        }

        public IList<LBItem> QueryItemNoSamplingGroupDao(string strHqlWhere, string strSectionID)
        {
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<LBItem>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);

            string strHqlSection = "";
            if (strSectionID != null && strSectionID.Length > 0)
            {
                var listSectionID = strSectionID.Split(',');
                foreach (var sectionID in listSectionID)
                {
                    if (strHqlSection == "")
                        strHqlSection = " lbsectionitem.LBSection.Id=" + sectionID;
                    else
                        strHqlSection += " or lbsectionitem.LBSection.Id=" + sectionID;
                }
                strHqlSection = " where (" + strHqlSection + ")";
                strHqlSection += " and " + BaseDataFilter.GetDataRowRoleHQLString<LBItem>();
                strHqlSection = BaseDataFilter.FilterMacroCommand(strHqlSection);
                strHqlSection = " and lbitem.Id in (select lbsectionitem.LBItem.Id from LBSectionItem lbsectionitem " + strHqlSection + ")";
            }

            string strHQL = " select lbitem from LBItem lbitem " +
                            " where lbitem.Id not in (select lbsamplingitem.LBItem.Id from LBSamplingItem lbsamplingitem) " + strHqlSection + strHqlWhere;
            IList<LBItem> listItem = this.Session.CreateQuery(strHQL).List<LBItem>();
            return listItem;
        }


    }
}