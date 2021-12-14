using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IDAO.LabStar;
using ZhiFang.LabStar.Common;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBSamplingItem : BaseBLL<LBSamplingItem>, ZhiFang.IBLL.LabStar.IBLBSamplingItem
    {
        IDLBItemDao IDLBItemDao { get; set; }
        public EntityList<LBSamplingItem> QueryLBSamplingItemByFetch(string strHqlWhere, string Order, int start, int count)
        {
            return (this.DBDao as IDLBSamplingItemDao).QueryLBSamplingItemByFetchDao(strHqlWhere, Order, start, count);
        }

        public EntityList<LBSamplingGroup> QuerySamplingGroupIsMultiItem(string strHqlWhere, bool isMulti)
        {
            EntityList<LBSamplingGroup> entityList = new EntityList<LBSamplingGroup>();
            IList<LBSamplingGroup> list = (this.DBDao as IDLBSamplingItemDao).QuerySamplingGroupIsMultiItemDao(strHqlWhere, isMulti);
            if (list != null && list.Count > 0)
            {
                entityList.count = list.Count;
                entityList.list = list;
            }
            return entityList;
        }

        public EntityList<LBItem> QueryItemIsMultiSamplingGroup(string strHqlWhere, string strSectionID, bool isMulti)
        {
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            IList<LBItem> list = (this.DBDao as IDLBSamplingItemDao).QueryItemIsMultiSamplingGroupDao(strHqlWhere, strSectionID, isMulti);
            if (list != null && list.Count > 0)
            {
                entityList.count = list.Count;
                entityList.list = list;
            }
            return entityList;
        }

        public EntityList<LBItem> QueryItemNoSamplingGroup(string strHqlWhere, string strSectionID)
        {
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            IList<LBItem> list = (this.DBDao as IDLBSamplingItemDao).QueryItemNoSamplingGroupDao(strHqlWhere, strSectionID);
            if (list != null && list.Count > 0)
            {
                entityList.count = list.Count;
                entityList.list = list;
            }
            return entityList;
        }

        public EntityList<LBSamplingItemVO> SearchLBSamplingItemBandItemName(string where, string sort, int page, int limit)
        {
            EntityList<LBSamplingItemVO> lsivoentitylist = new EntityList<LBSamplingItemVO>();
            EntityList<LBSamplingItem>  lbsientityList = DBDao.GetListByHQL(where,sort,page,limit);
            if (lbsientityList.count > 0)
            {
                List<long> ids = new List<long>();
                foreach (var item in lbsientityList.list)
                {
                    if (item.MustItemID != 0 && item.MustItemID != null)
                    {
                        ids.Add(long.Parse(item.MustItemID.ToString()));
                    }
                }
                ids.Distinct();
                if (ids.Count > 0)
                {
                    string itemwhere = "Id in (" + string.Join(",", ids) + ")";
                    IList<LBItem> lBItems = IDLBItemDao.GetListByHQL(itemwhere);
                    List<LBSamplingItemVO> lBSamplingItemVOs = new List<LBSamplingItemVO>();
                    for (int i = 0; i < lbsientityList.list.Count; i++)
                    {
                        LBSamplingItemVO lBSamplingItemVO = new LBSamplingItemVO();
                        lBSamplingItemVO = ClassMapperHelp.GetMapper<LBSamplingItemVO, LBSamplingItem>(lbsientityList.list[i]);
                        if (lbsientityList.list[i].MustItemID != 0)
                        {
                            var itemnames = lBItems.Where(a => a.Id == lbsientityList.list[i].MustItemID);
                            if (itemnames.Count() > 0)
                            {
                                lBSamplingItemVO.ItemCName = itemnames.First().CName;
                            }
                        }
                        lBSamplingItemVOs.Add(lBSamplingItemVO);

                    }
                    lsivoentitylist.count = lbsientityList.count;
                    lsivoentitylist.list = lBSamplingItemVOs;
                }
                else
                {
                    List<LBSamplingItemVO> lBSamplingItemVOs = new List<LBSamplingItemVO>();
                    for (int i = 0; i < lbsientityList.list.Count; i++)
                    {
                        if (lbsientityList.list[i].MustItemID != 0)
                        {
                            LBSamplingItemVO lBSamplingItemVO = new LBSamplingItemVO();
                            lBSamplingItemVO = ClassMapperHelp.GetMapper<LBSamplingItemVO, LBSamplingItem>(lbsientityList.list[i]);
                            lBSamplingItemVOs.Add(lBSamplingItemVO);
                        }
                    }
                    lsivoentitylist.count = lbsientityList.count;
                    lsivoentitylist.list = lBSamplingItemVOs;
                }

            }
            return lsivoentitylist;
        }

        public BaseResultDataValue LS_UDTO_UpdateSamplingItemIsDefault(long? Id, long? ItemId,bool IsDefault)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (Id == null || Id == 0) {
                baseResultDataValue.ErrorInfo = "参数错误；Id不能为空！";
                baseResultDataValue.success = false;
                return baseResultDataValue;
            }
            if (ItemId == null || ItemId == 0) {
                baseResultDataValue.ErrorInfo = "参数错误；ItemId不能为空！";
                baseResultDataValue.success = false;
                return baseResultDataValue;
            }
            int isok = 0;
            if (IsDefault)
            {
                string uphql = "update LBSamplingItem set IsDefault= 0 where  ItemID = " + ItemId;
                DBDao.UpdateByHql(uphql);
                string uphqlid = "update LBSamplingItem set IsDefault= 1 where  Id = " + Id;
                isok = DBDao.UpdateByHql(uphqlid);
            }
            else {
                string uphqlid = "update LBSamplingItem set IsDefault= 0 where  Id = " + Id;
                isok = DBDao.UpdateByHql(uphqlid);
            }
            if (isok > 0)
            {
                baseResultDataValue.success = true;
            }
            else {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "修改失败！";
            }
            return baseResultDataValue;
        }
    }
}