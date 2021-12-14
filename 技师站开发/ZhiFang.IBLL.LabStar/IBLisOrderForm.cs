using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLisOrderForm : IBGenericManager<LisOrderForm>
    {
        SysCookieValue SysCookieValue { get; set; }
        #region 医嘱开单
        BaseResultDataValue Edit_AddOrder(LisPatient lisPatient, LisOrderForm lisOrderForm, IList<LisOrderItem> lisOrderItems,string username,string userid);
        bool EditOrder(string[] lisPatient, string[] lisOrderForm, IList<LisOrderItem> lisOrderItems, LisOrderForm lisOrderFormentity,string userid,string username);

        BaseResultDataValue GetOrderList(string hisDeptNo, string patno, string sickTypeNo, string strWhere);
        BaseResultDataValue UpdateOrder(string orderFormNo,long userid,string username);
        BaseResultDataValue CancelOrder(string orderFormNo, long userid, string username);
        BaseResultDataValue DeleteOrder(string orderFormNo, long userid, string username);
        #endregion
        #region 样本条码
        BaseResultDataValue HISGetSampleAndGrouping(long nodetype, string receiveType, string value, int days,long userid,string username,string labid, int nextindex,out List<BPara> paralist,out List<List<GroupingOrderItemVo>> goivlist,out List<List<LisOrderFormItemVo>> lisoftvolist, out List<LBSamplingItem> lbslilist, out List<LisBarCodeFormVo> barCodeFormVos);
        BaseResultDataValue LISGetSampleAndGrouping(long nodetype, string receiveType, string value, int days,string userid,string username,string labid, int nextindex, out List<BPara> paralist, out List<List<GroupingOrderItemVo>> goivlist, out List<List<LisOrderFormItemVo>> lisoftvolist, out List<LBSamplingItem> lbslilist, out List<LisBarCodeFormVo> barCodeFormVos);
        BaseResultDataValue CheckCard(long nodetype, string receiveType, string card);
        BaseResultDataValue GetHISOrderInfo(long nodetype, string barcode);
        BaseResultDataValue GetSampleAndANewGrouping(string barcode,long nodetype, string receiveType, string value, int days,string userid,string username, string fields,string labid, bool isPlanish, int nextindex);
        BaseResultDataValue GetHaveToPrintBarCodeForm(string barcode, string where, bool? printStatus, string fields, bool isPlanish);
        List<LisBarCodeFormVo> AddBarCodeFormAndEditOrderForm(List<BPara> bParas, List<List<GroupingOrderItemVo>> originalSampleGroups, List<List<LisOrderFormItemVo>> originalItemData, List<LBSamplingItem> originalGroupData,long userid,string username, int nextindex);
        void Edit_OrderFormExecFlag(List<List<LisOrderFormItemVo>> originalItemData);
        BaseResultDataValue GetHISCheckData(long nodetype, string receiveType, string value, int days, long userid, string username, string labid, int nextindex);
        #endregion
    }
}