using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class BPhraseDao : BaseDaoNHB<BPhrase, long>, IDBPhraseDao
    {
        /// <summary>
        /// 根传入的小组类型Url获取小组结果短语及小组项目结果短语(过滤相同的)
        /// </summary>
        /// <param name="gmgroupTypeUrl"></param>
        /// <returns></returns>
        public IList<BPhrase> SearchListByGMGroupTypeUrl(string gmgroupTypeUrl)
        {
            IList<BPhrase> lists = new List<BPhrase>();
            IList<string> tempList = new List<string>();
            string strHQL = "from BPhrase  bphrase left join  bphrase.BPhraseType";
            if (!String.IsNullOrEmpty(gmgroupTypeUrl))
            {
                gmgroupTypeUrl = gmgroupTypeUrl.Replace("\"", "");
                gmgroupTypeUrl = gmgroupTypeUrl.Replace("'", "");
                StringBuilder strbWhere = new StringBuilder();
                string strItemAllItemId = "", strGMGroupId = "";
                SearchListByGMGroupTypeUrl(gmgroupTypeUrl, ref strItemAllItemId, ref strGMGroupId);

                strbWhere.Append(" where (bphrase.BPhraseType.Shortcode='ItemsPhrase' or bphrase.BPhraseType.Shortcode='GroupResultPhrase')");
                if (!String.IsNullOrEmpty(strGMGroupId) && !String.IsNullOrEmpty(strItemAllItemId))
                {
                    strbWhere.Append(" and (bphrase.SpecificObjectID in(" + strGMGroupId + ")) or (bphrase.SpecificObjectID in(" + strItemAllItemId + "))");
                }
                else if (!String.IsNullOrEmpty(strGMGroupId) && String.IsNullOrEmpty(strItemAllItemId))
                {
                    strbWhere.Append(" and (bphrase.SpecificObjectID in(" + strGMGroupId + "))");
                }
                else if (String.IsNullOrEmpty(strGMGroupId) && !String.IsNullOrEmpty(strItemAllItemId))
                {
                    strbWhere.Append(" and (bphrase.SpecificObjectID in(" + strItemAllItemId + "))");
                }
                var tmpobjectarray = this.HibernateTemplate.Find<object>(strHQL);
                foreach (var a in tmpobjectarray)
                {
                    object[] tmpobject = (object[])a;
                    Entity.BPhrase tempEntity = (Entity.BPhrase)tmpobject[0];
                    tempEntity.BPhraseType = (Entity.BPhraseType)tmpobject[1];
                    if (!tempList.Contains(tempEntity.Content.Trim()))
                    {
                        tempList.Add(tempEntity.Content.Trim());
                        lists.Add(tempEntity);
                    }
                }
            }
            return lists;
        }
        /// <summary>
        /// 根传入的小组类型Url获取小组Id及小组项目Id(过滤相同检验项目Id及相同的检验小组)
        /// </summary>
        /// <param name="gmgroupTypeUrl"></param>
        /// <returns></returns>
        public void SearchListByGMGroupTypeUrl(string gmgroupTypeUrl, ref string strItemAllItemId, ref string strGMGroupId)
        {
            IList<GMGroupItem> lists = new List<GMGroupItem>();
            IList<ItemAllItem> listItemAllItems = new List<ItemAllItem>();
            StringBuilder itemAllItemId = new StringBuilder();
            StringBuilder gmgroupId = new StringBuilder();
            string strHQL = "select gmgroupitem.GMGroup,gmgroupitem.ItemAllItem from GMGroupItem  gmgroupitem left join  gmgroupitem.GMGroup left join gmgroupitem.ItemAllItem";
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
                    Entity.GMGroup gmgroup = (Entity.GMGroup)tmpobject[0];
                    Entity.ItemAllItem itemAllItem = (Entity.ItemAllItem)tmpobject[1];
                    if (!gmgroupId.ToString().Split(',').Contains(gmgroup.Id.ToString()))
                    {
                        gmgroupId.Append(gmgroup.Id.ToString());
                    }
                    if (!itemAllItemId.ToString().Split(',').Contains(itemAllItem.Id.ToString()))
                    {
                        itemAllItemId.Append(itemAllItem.Id.ToString());
                    }

                }
                strItemAllItemId = itemAllItemId.ToString();
                strGMGroupId = gmgroupId.ToString();
            }
            else
            {
                strItemAllItemId = "";
                strGMGroupId = "";
            }

        }
    }
}