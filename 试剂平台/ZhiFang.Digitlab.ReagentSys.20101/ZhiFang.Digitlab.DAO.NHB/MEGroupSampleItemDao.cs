using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class MEGroupSampleItemDao : BaseDaoNHB<MEGroupSampleItem, long>, IDMEGroupSampleItemDao
    {
        public IList<MEGroupSampleItem> SearchMEGroupSampleItemByHQL(string strHqlWhere, int page, int count)
        {
            IList<MEGroupSampleItem> lists = new List<MEGroupSampleItem>();
            string strHQL = "from MEGroupSampleItem  megroupsampleitem left join megroupsampleitem.MEGroupSampleForm left join megroupsampleitem.ItemAllItem";//,megroupsampleitem.MEImmuneResults 
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
                Entity.MEGroupSampleItem tmpMEGroupSampleItem = (Entity.MEGroupSampleItem)tmpobject[0];

                tmpMEGroupSampleItem.MEGroupSampleForm = (Entity.MEGroupSampleForm)tmpobject[1];
                tmpMEGroupSampleItem.ItemAllItem = (Entity.ItemAllItem)tmpobject[2];
                //tmpMEGroupSampleItem.PItemAllItem = (Entity.ItemAllItem)tmpobject[3];
                //tmpMEGroupSampleItem.MEPTSampleItem = (Entity.MEPTSampleItem)tmpobject[4];
                //tmpMEGroupSampleItem.MEImmuneResults = (Entity.MEImmuneResults)tmpobject[5];
                lists.Add(tmpMEGroupSampleItem);
            }
            return lists;
        }
    }
}