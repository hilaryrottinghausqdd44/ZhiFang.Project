

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
    public interface IBBLabTestItem : ZhiFang.IBLL.Base.IBGenericManager<BLabTestItem>
    {
        EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> SearchOSBLabTestItemByLabID(string LabID, int page, int limit, string sort, string where);
        EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> SearchOSBLabTestItemByAreaID(string AreaID, int page, int limit, string sort, string where);
        EntityList<BLabTestItem> SearchBLabGroupItemByPItemNoAndAreaID(string pItemNo, string areaID, int page, int limit, string sort);
        EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> SearchBLabTestItemVOList(int page, int limit, string sort, string where);
        EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> SearchAllBLabTestItemByAreaID(string areaID, int page, int limit, string sort, string where);
        EntityList<BLabTestItem> SearchBLabGroupItemSubItemByPItemNoAndAreaID(string pItemNo, string areaID, int page, int limit, string sort);
        EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> SearchAllBLabTestItemByLabCode(string labCode, int page, int limit, string sort, string where);

        EntityList<BLabTestItem> SearchBLabGroupItemSubItemByPItemNoAndLabCode(string pItemNo, string LabCode, int page, int limit, string sort);
        EntityList<Entity.ViewObject.Response.BLabGroupItemVO> SearchBLabGroupItemSubItemVOByPItemNoAndLabCode(string pitemNo, string LabCode, int page, int limit, string sort);
        BaseResultDataValue AddByBLabTestItemVO(ZhiFang.WeiXin.Entity.ViewObject.Request.BLabTestItemVO entity);
        BaseResultBool UpdateBLabTestItemByFieldVO(string[] tempArray, Entity.ViewObject.Request.BLabTestItemVO entity);
        BaseResultDataValue SearchBLabTestItemByLabCodeAndType(string type, string labCode, string where, string sort);
        BaseResultDataValue TestItemCopyAll(string sourceLabCode, List<string> labCodeList, int overRideType);
        BaseResultDataValue TestItemCopy(string sourceLabCode, List<string> labCodeList, List<string> itemNoList, int overRideType);
        List<ApplyInputItemEntity> ItemEntityDataTableToList(List<BLabTestItem> BLabTestItemlist, List<TestItem> TestItemlist);

        BaseResultDataValue GetTestItem(string supergroupno, string itemkey, int rows, int page, string labcode);
    }
}