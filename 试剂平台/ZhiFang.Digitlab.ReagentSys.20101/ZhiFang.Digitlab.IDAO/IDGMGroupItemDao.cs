using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public interface IDGMGroupItemDao : IDBaseDao<GMGroupItem, long>
    {
        IList<Entity.GMGroupItem> JudgeGMGroupItemByItemID(long gmGroupID);

        IList<Entity.GMGroupItem> SearchListByGroupId(long[] gmGroupID);

        IList<Entity.GMGroupItem> SearchListByGroupId(string gmGroupID);
        IList<GMGroupItem> SearchGMGroupItemByHQL(string strHqlWhere, int page, int count);
        /// <summary>
        /// 根传入的小组类型Url获取小组项目(过滤相同检验项目)
        /// </summary>
        /// <param name="gmgroupTypeUrl"></param>
        /// <returns></returns>
        IList<Entity.GMGroupItem> SearchGMGroupItemByGMGroupTypeUrl(string gmgroupTypeUrl);
    }
}