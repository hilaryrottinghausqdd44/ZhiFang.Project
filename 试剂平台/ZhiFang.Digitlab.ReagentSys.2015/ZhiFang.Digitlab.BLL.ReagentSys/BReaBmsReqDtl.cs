
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.BLL;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsReqDtl : BaseBLL<ReaBmsReqDtl>, ZhiFang.Digitlab.IBLL.ReagentSys.IBReaBmsReqDtl
    {
        public BaseResultBool AddDtList(IList<ReaBmsReqDtl> dtAddList, ReaBmsReqDoc reaBmsReqDoc, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtAddList != null && dtAddList.Count > 0)
            {
                if (reaBmsReqDoc.DataTimeStamp == null)
                {
                    byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                    reaBmsReqDoc.DataTimeStamp = dataTimeStamp;
                }
                foreach (var dt in dtAddList)
                {
                    dt.ReaBmsReqDoc = reaBmsReqDoc;
                    dt.ReqDocNo = reaBmsReqDoc.ReqDocNo;
                    dt.CreaterID = empID;
                    dt.CreaterName = empName;
                    dt.Visible = true;
                    this.Entity = dt;
                    tempBaseResultBool.success = this.Add();
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditDtList(IList<ReaBmsReqDtl> dtEditList, ReaBmsReqDoc reaBmsReqDoc)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtEditList != null && dtEditList.Count > 0)
            {
                List<string> tmpa = new List<string>();
                foreach (var item in dtEditList)
                {
                    //dt.ReaBmsReqDoc = reaBmsReqDoc;
                    tmpa.Clear();

                    tmpa.Add("Id=" + item.Id + " ");
                    tmpa.Add("GoodsQty=" + item.GoodsQty + "");
                    if (item.OrderGoodsID.HasValue) tmpa.Add("OrderGoodsID=" + item.OrderGoodsID);
                    if (item.GoodsUnitID.HasValue) tmpa.Add("GoodsUnitID=" + item.GoodsUnitID);
                    if (!string.IsNullOrEmpty(item.GoodsUnit)) tmpa.Add("GoodsUnit='" + item.GoodsUnit+"'");
                    tmpa.Add("Memo='" + item.Memo + "'");
                    if (item.ReaCenOrg != null)
                    {
                        tmpa.Add("ReaCenOrg.Id=" + item.ReaCenOrg.Id + "");
                        tmpa.Add("OrgName='" + item.OrgName + "'");
                    }
                    else
                    {
                        tmpa.Add("ReaCenOrg.Id=null");
                        tmpa.Add("OrgName=null");
                    }
                    //this.Entity = item;
                    tempBaseResultBool.success = this.Update(tmpa.ToArray());
                }
            }
            return tempBaseResultBool;
        }
    }
}