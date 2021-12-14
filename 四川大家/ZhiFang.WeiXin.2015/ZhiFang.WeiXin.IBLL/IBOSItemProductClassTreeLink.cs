

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.IBLL
{
    /// <summary>
    ///
    /// </summary>
    public interface IBOSItemProductClassTreeLink : ZhiFang.IBLL.Base.IBGenericManager<OSItemProductClassTreeLink>
    {
        EntityList<BLabTestItemVO> SearchBLabTestItemVOByTreeId(int page, int limit, string where, string sort, string areaID, string treeId, bool isSearchChild);
        EntityList<OSItemProductClassTreeLinkVO> SearchOSItemProductClassTreeLinkByTreeId(int page, int limit, string where, string sort, string areaId, string treeId, bool isSearchChild);
    }
}