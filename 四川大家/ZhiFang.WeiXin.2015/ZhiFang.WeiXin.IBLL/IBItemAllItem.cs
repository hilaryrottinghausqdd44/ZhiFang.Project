

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Request;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.IBLL
{
    /// <summary>
    ///
    /// </summary>
    public interface IBItemAllItem : ZhiFang.IBLL.Base.IBGenericManager<TestItem>
    {
        EntityList<Entity.ViewObject.Response.GroupItemVO> SearchGroupItemSubItemByPItemNo(string pitemNo, int page, int limit, string sort);
        bool UpdateTestItemByFieldVO(string[] tempArray, TestItemVO entity);
        bool AddByTestItemVO(TestItemVO entity);
        BaseResultDataValue TestItemCopy(List<string> labCodeList, List<string> itemNoList, int OverRideType);
        BaseResultDataValue TestItemCopyAll(List<string> labCodeList, int OverRideType);
    }
}