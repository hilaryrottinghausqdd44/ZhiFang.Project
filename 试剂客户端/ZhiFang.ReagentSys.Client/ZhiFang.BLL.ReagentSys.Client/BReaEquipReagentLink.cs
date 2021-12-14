
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaEquipReagentLink : BaseBLL<ReaEquipReagentLink>, ZhiFang.IBLL.ReagentSys.Client.IBReaEquipReagentLink
    {
        IDReaTestEquipLabDao IDReaTestEquipLabDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        public IList<ReaEquipReagentLink> SearchNewListByHQL(string where, string sort, int page, int limit)
        {
            IList<ReaEquipReagentLink> entityList = new List<ReaEquipReagentLink>();
            entityList = ((IDReaEquipReagentLinkDao)base.DBDao).SearchNewListByHQL(where, sort, page, limit);
            return entityList;
        }
        public EntityList<ReaEquipReagentLink> SearchNewEntityListByHQL(string where, string sort, int page, int limit)
        {
            EntityList<ReaEquipReagentLink> entityList = new EntityList<ReaEquipReagentLink>();
            entityList = ((IDReaEquipReagentLinkDao)base.DBDao).SearchNewEntityListByHQL(where, sort, page, limit);
            return entityList;
        }
        public IList<ReaEquipReagentLinkVO> SearchReaEquipReagentLinkVOList(IList<ReaTestEquipLab> testEquipLabList, string deptName)
        {
            IList<ReaEquipReagentLinkVO> tempList = new List<ReaEquipReagentLinkVO>();
            if (testEquipLabList == null)
                testEquipLabList = new List<ReaTestEquipLab>();

            IList<ReaEquipReagentLink> tempLinkList = this.SearchListByHQL("reaequipreagentlink.Visible=1");
            if (tempLinkList != null && tempLinkList.Count > 0)
            {
                var groupByList = tempLinkList.GroupBy(p => p.GoodsID);
                IList<ReaTestEquipLab> tempEquipLabList = new List<ReaTestEquipLab>();
                IList<string> testEquipIDList = new List<string>();
                foreach (var groupBy in groupByList)
                {
                    ReaGoods goods = IDReaGoodsDao.Get(groupBy.Key.Value);
                    ReaEquipReagentLinkVO vo = new ReaEquipReagentLinkVO();
                    vo.GoodsID = groupBy.Key.Value;
                    vo.GoodsCName = goods.CName;
                    vo.ReaTestEquipVOList = new List<ReaTestEquipVO>();
                    foreach (ReaEquipReagentLink equipReagentLink in groupBy)
                    {
                        string tempIdStr = groupBy.Key + "|" + equipReagentLink.TestEquipID;
                        if (testEquipIDList.Contains(tempIdStr))
                            continue;

                        ReaTestEquipLab equip = null;
                        var TestEquipList = tempEquipLabList.Where(p => p.Id == equipReagentLink.TestEquipID.Value).ToList();
                        if (TestEquipList.Count > 0)
                        {
                            equip = TestEquipList[0];
                        }
                        else
                        {
                            equip = IDReaTestEquipLabDao.Get(equipReagentLink.TestEquipID.Value);
                        }
                        if (equip == null)
                        {
                            ZhiFang.Common.Log.Log.Info("仪器ID:" + equipReagentLink.TestEquipID.Value + ",不存在仪器信息表里,请重新维护仪器试剂关系!");
                            continue;
                        }
                        //先判断该仪器是否属于当前登录帐号的所属部门
                        var tempEquipList = testEquipLabList.Where(p => p.Id == equipReagentLink.TestEquipID.Value);
                        if (tempEquipList == null || tempEquipList.Count() <= 0)
                        {
                            ZhiFang.Common.Log.Log.Info("仪器名称:" + equip.CName + ",不属于当前登录帐号所属部门(及其下属部门):" + deptName + "所绑定的仪器,请在仪器维护里维护好仪器的所属部门!");
                            continue;
                        }

                        ReaTestEquipVO equipVO = new ReaTestEquipVO();
                        equipVO.TestEquipID = equipReagentLink.TestEquipID.Value;
                        equipVO.DispOrder = equip.DispOrder;
                        if (!tempEquipLabList.Contains(equip))
                            tempEquipLabList.Add(equip);
                        equipVO.TestEquipName = equip.CName;
                        if (!vo.ReaTestEquipVOList.Contains(equipVO))
                            vo.ReaTestEquipVOList.Add(equipVO);
                        testEquipIDList.Add(tempIdStr);
                    }
                    if (vo.ReaTestEquipVOList.Count > 0)
                        vo.ReaTestEquipVOList = vo.ReaTestEquipVOList.OrderBy(p => p.DispOrder).ToList();
                    tempList.Add(vo);
                }
            }
            return tempList;
        }
    }
}