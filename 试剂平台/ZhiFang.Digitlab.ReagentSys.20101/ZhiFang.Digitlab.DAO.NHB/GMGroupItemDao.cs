using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class GMGroupItemDao : BaseDaoNHB<GMGroupItem, long>, IDGMGroupItemDao
    {
        #region IDGMGroupItemDao 成员
        public new GMGroupItem Get(long id)
        {
            string strHQL = "select gmgroupitem,gmgroupitem.ItemAllItem from GMGroupItem  gmgroupitem where gmgroupitem.GMGroup.Id=" + id;
            IList<GMGroupItem> ilist = this.HibernateTemplate.Find<GMGroupItem>(strHQL);
            Entity.GMGroupItem tmpgmgroupitem = new Entity.GMGroupItem();
            if (ilist != null && ilist.Count > 0)
            {
                tmpgmgroupitem = ilist[0];
                tmpgmgroupitem.ItemAllItem = ilist[0].ItemAllItem;
            }
            else
            {
                tmpgmgroupitem = null;
            }
            return tmpgmgroupitem;
        }
        public IList<GMGroupItem> SearchGMGroupItemByHQL(string strHqlWhere, int page, int count)
        {
            IList<GMGroupItem> lists = new List<GMGroupItem>();
            string strHQL = "select gmgroupitem,gmgroupitem.ItemAllItem from GMGroupItem  gmgroupitem ";
            if (!String.IsNullOrEmpty(strHqlWhere))
            {
                strHQL += " where " + strHqlWhere;
            }
            else
            {
                return lists;
            }
            var tmpobjectarray = this.HibernateTemplate.Find<object>(strHQL);

            foreach (var a in tmpobjectarray)
            {
                object[] tmpobject = (object[])a;
                Entity.GMGroupItem tmpgmgroupitem = (Entity.GMGroupItem)tmpobject[0];
                tmpgmgroupitem.ItemAllItem = (Entity.ItemAllItem)tmpobject[1];
                lists.Add(tmpgmgroupitem);
            }
            return lists;
        }
        public IList<GMGroupItem> JudgeGMGroupItemByItemID(long gmGroupID)
        {
            string strHQL = "select gmgroupitem,gmgroupitem.ItemAllItem from GMGroupItem  gmgroupitem where gmgroupitem.GMGroup.Id=" + gmGroupID;
            var tmpobjectarray = this.HibernateTemplate.Find<object>(strHQL);

            List<Entity.GMGroupItem> listgmgroupitem = new List<GMGroupItem>();
            foreach (var a in tmpobjectarray)
            {
                object[] tmpobject = (object[])a;
                Entity.GMGroupItem tmpgmgroupitem = (Entity.GMGroupItem)tmpobject[0];
                tmpgmgroupitem.ItemAllItem = (Entity.ItemAllItem)tmpobject[1];
                listgmgroupitem.Add(tmpgmgroupitem);
            }
            return listgmgroupitem;
        }

        public IList<Entity.GMGroupItem> SearchListByGroupId(long[] gmGroupID)
        {
            string longstringarray = "";
            if (gmGroupID.Length <= 0)
            {
                return null;
            }
            else
            {
                foreach (long gid in gmGroupID)
                {
                    longstringarray += gid.ToString().Trim() + ",";
                }
            }
            string strHQL = "select gmgroupitem,gmgroupitem.ItemAllItem from GMGroupItem  gmgroupitem  where gmgroupitem.GMGroup.Id in (" + longstringarray.Substring(longstringarray.Length - 1) + ") ";
            var tmpobjectarray = this.HibernateTemplate.Find<object>(strHQL);

            List<Entity.GMGroupItem> listgmgroupitem = new List<GMGroupItem>();
            foreach (var a in tmpobjectarray)
            {
                object[] tmpobject = (object[])a;
                Entity.GMGroupItem tmpgmgroupitem = (Entity.GMGroupItem)tmpobject[0];
                tmpgmgroupitem.ItemAllItem = (Entity.ItemAllItem)tmpobject[1];
                listgmgroupitem.Add(tmpgmgroupitem);
            }
            return listgmgroupitem;
        }

        public IList<Entity.GMGroupItem> SearchListByGroupId(string gmGroupID)
        {
            if (gmGroupID.Length <= 0)
            {
                return null;
            }
            else
            {
                string strHQL = "select gmgroupitem,gmgroupitem.ItemAllItem from GMGroupItem  gmgroupitem  where gmgroupitem.GMGroup.Id in (" + gmGroupID + ") ";
                var tmpobjectarray = this.HibernateTemplate.Find<object>(strHQL);

                List<Entity.GMGroupItem> listgmgroupitem = new List<GMGroupItem>();
                foreach (var a in tmpobjectarray)
                {
                    object[] tmpobject = (object[])a;
                    Entity.GMGroupItem tmpgmgroupitem = (Entity.GMGroupItem)tmpobject[0];
                    tmpgmgroupitem.ItemAllItem = (Entity.ItemAllItem)tmpobject[1];
                    listgmgroupitem.Add(tmpgmgroupitem);
                }
                return listgmgroupitem;
            }
        }

        public IList<RBACModule> SearchModuleByHREmpID(long longHREmpID)
        {
            return null;
            //var pl = this.HibernateTemplate.Find<GMGroup>("  select gmgroupitemlist.Id ,gmgroupitemlist.DefaultReportValue ,gmgroupitemlist.ItemAllItem from GMGroup  gmgroup join gmgroup.GMGroupItemList  gmgroupitemlist join gmgroupitemlist.ItemAllItem  iai   where gmgroup.Id = " + gmGroupID + "");
            //return pl[0].GMGroupItemList;
        }

        #endregion
        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public override bool Delete(GMGroupItem entity)
        {
            bool result = true;
            ZhiFang.Common.Log.Log.Warn("执行删除小组项目开始:检验项目名称为:" + entity.ItemAllItem.CName + ",操作人为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName) + ",操作人ID为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            try
            {
                this.HibernateTemplate.Delete(entity);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>bool</returns>
        public override bool Delete(long id)
        {
            GMGroupItem entity = this.Get(id);
            ZhiFang.Common.Log.Log.Warn("执行删除小组项目开始:检验项目名称为:" + entity.ItemAllItem.CName + ",操作人为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName) + ",操作人ID为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            int result = this.HibernateTemplate.Delete(" From GMGroupItem megroupItem where megroupItem.Id=" + id);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根传入的小组类型Url获取小组项目(过滤相同检验项目)
        /// </summary>
        /// <param name="gmgroupTypeUrl"></param>
        /// <returns></returns>
        public IList<GMGroupItem> SearchGMGroupItemByGMGroupTypeUrl(string gmgroupTypeUrl)
        {
            IList<GMGroupItem> lists = new List<GMGroupItem>();
            IList<ItemAllItem> listItemAllItems = new List<ItemAllItem>();
            string strHQL = "from GMGroupItem  gmgroupitem left join  gmgroupitem.GMGroup left join gmgroupitem.ItemAllItem";
            if (!String.IsNullOrEmpty(gmgroupTypeUrl))
            {
                gmgroupTypeUrl = gmgroupTypeUrl.Replace("\"", "");
                gmgroupTypeUrl = gmgroupTypeUrl.Replace("'", "");
                StringBuilder strbWhere = new StringBuilder();
                strbWhere.Append(" where (1>2");
                string[] arrUrl = gmgroupTypeUrl.Trim().Split(',');
                foreach (string url in arrUrl)
                {
                    strbWhere.Append(" or gmgroupitem.GMGroup.GMGroupType.Url='" + url + "'");
                }
                strbWhere.Append(") ");
                strHQL += strbWhere.ToString();
                var tmpobjectarray = this.HibernateTemplate.Find<object>(strHQL);
                foreach (var a in tmpobjectarray)
                {
                    object[] tmpobject = (object[])a;
                    Entity.GMGroupItem tmpgmgroupitem = (Entity.GMGroupItem)tmpobject[0];
                    tmpgmgroupitem.GMGroup = (Entity.GMGroup)tmpobject[1];
                    tmpgmgroupitem.ItemAllItem = (Entity.ItemAllItem)tmpobject[2];
                    if (!listItemAllItems.Contains(tmpgmgroupitem.ItemAllItem))
                    {
                        listItemAllItems.Add(tmpgmgroupitem.ItemAllItem);
                        lists.Add(tmpgmgroupitem);
                    }

                }
                return lists;
            }
            else
            {
                return lists;
            }

        }
    }
}