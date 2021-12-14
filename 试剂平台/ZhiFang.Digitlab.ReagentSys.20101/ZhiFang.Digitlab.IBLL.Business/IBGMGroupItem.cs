

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
    /// <summary>
    ///
    /// </summary>
    public interface IBGMGroupItem : IBGenericManager<GMGroupItem>
    {
        bool JudgeGMGroupItemByItemID(long gmGroupID, long itemID);
        /// <summary>
        /// 删除小组项目时，同时删除仪器项目
        /// </summary>
        /// <param name="groupItemID">小组项目ID</param>
        /// <returns>bool</returns>
        bool DeleteGMGroupItemAndEquipItem(long groupItemID);
        IList<GMGroupItem> JudgeGMGroupItemByItemID(long gmGroupID);

        IList<GMGroupItem> SearchListByGroupId(string GroupId, int page, int limit);

        EntityList<GMGroupItem> SearchListByGroupId(long GroupId, string sort, int page, int limit);
        IList<GMGroupItem> SearchGMGroupItemByHQL(string strHqlWhere, int page, int count);
        /// <summary>
        /// 根传入的小组类型Url获取小组项目(过滤相同检验项目)
        /// </summary>
        /// <param name="gmgroupTypeUrl"></param>
        /// <returns></returns>
        IList<Entity.GMGroupItem> SearchGMGroupItemByGMGroupTypeUrl(string gmgroupTypeUrl);
    }
}