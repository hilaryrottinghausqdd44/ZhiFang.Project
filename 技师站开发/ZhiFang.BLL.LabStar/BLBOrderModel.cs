using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBOrderModel : BaseBLL<LBOrderModel>, ZhiFang.IBLL.LabStar.IBLBOrderModel
    {
        IDLBOrderModelItemDao IDLBOrderModelItemDao { get; set; }

        IBLBItem IBLBItem { get; set; }

        public List<tree> GetOrderModelTree(string OrderModelTypeID, string ItemWhere)
        {
            //1.根据条件获取医嘱模板OrderModel，再通过模板ID去LB_OrderModelItem医嘱模板明细表找对应的OrderModelItem，
            //2.再通过每个OrderModelItem的ItemID去LB_Item 检验项目表查项目的具体信息
            //3.如果item的组合类型GroupType是组套类型（0=单项  1=组合 2=组套），则需要通过ItemID去LB_ItemGroup组合项目关系表找出组套项目的关联项目，否则取item的相关信息
            //4.LB_ItemGroup表查出数据的GroupItemID作为第二步的ItemID重复向下查询，直到项目不是组套项目
            List<tree> trees = new List<tree>();
            if (string.IsNullOrEmpty(OrderModelTypeID))
            {
                return trees;
            }
            //获取医嘱模板
            EntityList<LBOrderModel> lbOrderModels = DBDao.GetListByHQL("OrderModelTypeID = "+ OrderModelTypeID, "DispOrder",0,0);
            if (lbOrderModels.list.Count>0)
            {
                foreach (var item in lbOrderModels.list)
                {
                    tree OrderModelTree = new tree();
                    OrderModelTree.text = item.CName;
                    OrderModelTree.expanded = true;
                    OrderModelTree.leaf = false;
                    OrderModelTree.url = "";
                    OrderModelTree.icon = "";
                    OrderModelTree.tid = item.Id.ToString();
                  
                    //获取医嘱模板明细
                    IList<LBOrderModelItem> lbOrderModelItems = IDLBOrderModelItemDao.GetListByHQL("OrderModelID=" + item.Id);
                    if (lbOrderModelItems.Count > 0)
                    {
                        //从item表获取具体的item信息
                        List<string> lbItemIDList = new List<string>();
                        foreach (var orderModelItem in lbOrderModelItems)
                        {
                            lbItemIDList.Add(orderModelItem.ItemID.ToString());
                        }
                        string srtitemwhere = "Id in (" + string.Join(",", lbItemIDList) + ")";
                        if (!string.IsNullOrEmpty(ItemWhere))
                        {
                            srtitemwhere += " and " + ItemWhere;
                        }
                        IList<LBItem> lbItems = IBLBItem.SearchListByHQL(srtitemwhere);
                        //循环lbItems，找到item的树
                        if (lbItems.Count > 0)
                        {
                            OrderModelTree.Tree = IBLBItem.GetModelItemsTree(OrderModelTree.tid, lbItems.ToList());
                        }
                    }
                    if (OrderModelTree.Tree !=null && OrderModelTree.Tree.Count > 0)
                    {
                        trees.Add(OrderModelTree);
                    }
                }
            }
            

            return trees;
        }

        
        
    }
}