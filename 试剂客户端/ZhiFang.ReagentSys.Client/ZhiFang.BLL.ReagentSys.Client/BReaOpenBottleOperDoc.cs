
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
    public class BReaOpenBottleOperDoc : BaseBLL<ReaOpenBottleOperDoc>, ZhiFang.IBLL.ReagentSys.Client.IBReaOpenBottleOperDoc
    {
        IDReaBmsOutDtlDao IDReaBmsOutDtlDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaBmsQtyDtlDao IDReaBmsQtyDtlDao { get; set; }

        public ReaOpenBottleOperDoc AddReaOpenBottleOperDoc(ReaBmsOutDtl outDtl, long outDtlId, long usrId, string usrCName)
        {
            ReaOpenBottleOperDoc oBottleOperDoc = null;
            if (outDtl == null)
                outDtl = IDReaBmsOutDtlDao.Get(outDtlId);
            if (outDtl == null) return oBottleOperDoc;

            ReaGoods reaGoods = IDReaGoodsDao.Get(outDtl.GoodsID.Value);
            oBottleOperDoc = new ReaOpenBottleOperDoc();
            oBottleOperDoc.CreaterID = usrId;
            oBottleOperDoc.CreaterName = usrCName;
            oBottleOperDoc.BOpenDate = outDtl.DataAddTime;
            oBottleOperDoc.GoodsID = outDtl.GoodsID;
            oBottleOperDoc.OutDocID = outDtl.OutDocID;
            oBottleOperDoc.OutDtlID = outDtl.Id;
            oBottleOperDoc.Visible = true;
            oBottleOperDoc.IsObsolete = false;
            long? qtyDtlID = null;
            if (!string.IsNullOrEmpty(outDtl.QtyDtlID) && outDtl.QtyDtlID.IndexOf(',') > -1)
            {
                string[] idArr = outDtl.QtyDtlID.Split(',');
                if (idArr.Length > 0) qtyDtlID = long.Parse(idArr[0]);
            }
            else if (!string.IsNullOrEmpty(outDtl.QtyDtlID))
            {
                qtyDtlID = long.Parse(outDtl.QtyDtlID);
            }
            oBottleOperDoc.QtyDtlID = qtyDtlID;// outDtl.QtyDtlID;
            oBottleOperDoc.IsUseCompleteFlag = false;
            oBottleOperDoc.InvalidBOpenDate = GetInvalidBOpenDate(outDtl, oBottleOperDoc, reaGoods); ;
            this.Entity = oBottleOperDoc;
            this.Add();

            return oBottleOperDoc;
        }
        public ReaOpenBottleOperDoc GetOBottleOperDocByOutDtlId(long outDtlId, long usrId, string usrCName)
        {
            ReaOpenBottleOperDoc oBottleOperDoc = null;

            IList<ReaOpenBottleOperDoc> oBottleOperDocList = this.SearchListByHQL("reaopenbottleoperdoc.OutDtlID=" + outDtlId);
            if (oBottleOperDocList != null && oBottleOperDocList.Count > 1)
            {
                oBottleOperDoc = oBottleOperDocList[0];
            }
            else
            {
                AddReaOpenBottleOperDoc(null, outDtlId, usrId, usrCName);

            }

            return oBottleOperDoc;

        }
        private DateTime? GetInvalidBOpenDate(ReaBmsOutDtl outDtl, ReaOpenBottleOperDoc oBottleOperDoc, ReaGoods reaGoods)
        {
            DateTime? invalidBOpenDate = null;
            //开瓶后有效天数
            if (reaGoods.BOpenDays.HasValue && reaGoods.BOpenDays.Value > 0)
            {
                invalidBOpenDate = outDtl.DataAddTime.Value.AddDays(reaGoods.BOpenDays.Value);
            }
            else
            {
                ReaBmsQtyDtl reaBmsQtyDtl = IDReaBmsQtyDtlDao.Get(oBottleOperDoc.QtyDtlID.Value);
                invalidBOpenDate = reaBmsQtyDtl.InvalidDate;
            }

            return invalidBOpenDate;
        }

    }
}