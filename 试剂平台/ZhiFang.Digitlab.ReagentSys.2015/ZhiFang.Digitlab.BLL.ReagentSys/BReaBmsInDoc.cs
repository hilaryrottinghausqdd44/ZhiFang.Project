using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaStoreIn;
using ZhiFang.Digitlab.IBLL.ReagentSys;
using System.Text;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsInDoc : BaseBLL<ReaBmsInDoc>, ZhiFang.Digitlab.IBLL.ReagentSys.IBReaBmsInDoc
    {
        IBReaBmsInDtl IBReaBmsInDtl { get; set; }
        IBBmsCenSaleDocConfirm IBBmsCenSaleDocConfirm { get; set; }
        IBBmsCenSaleDtlConfirm IBBmsCenSaleDtlConfirm { get; set; }
        public BaseResultDataValue AddReaBmsInDocAndDtl(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtAddList, long docConfirmID, string dtlDocConfirmIDStr, string codeScanningMode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (docConfirmID <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "本次入库的验收主单ID为空，不能入库！";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(dtlDocConfirmIDStr))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "本次入库的验收明细IDStr为空，不能入库！";
                return baseResultDataValue;
            }
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool = IBReaBmsInDtl.AddReaBmsInDtlValid(dtAddList, codeScanningMode);
            if (tempBaseResultBool.success == false)
            {
                baseResultDataValue.success = tempBaseResultBool.success;
                baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                return baseResultDataValue;
            }

            try
            {
                //入库的验收主单信息
                BmsCenSaleDocConfirm docConfirm = IBBmsCenSaleDocConfirm.Get(docConfirmID);
                if (docConfirm.Status != int.Parse(BmsCenSaleDocConfirmStatus.已验收.Key) && docConfirm.Status != int.Parse(BmsCenSaleDocConfirmStatus.部分入库.Key))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = string.Format("本次入库的验收主单状态为{0}，不能入库！", BmsCenSaleDocConfirmStatus.GetStatusDic()[docConfirm.Status.ToString()].Name);
                    return baseResultDataValue;
                }

                //验收明细待更新的集合
                IList<BmsCenSaleDtlConfirm> listEditDtlConfirm = new List<BmsCenSaleDtlConfirm>();

                IList<BmsCenSaleDtlConfirm> listDtlConfirm = null;
                if (docConfirm.BmsCenSaleDtlConfirmList != null && docConfirm.BmsCenSaleDtlConfirmList.Count > 0)
                    listDtlConfirm = docConfirm.BmsCenSaleDtlConfirmList;
                else
                    listDtlConfirm = IBBmsCenSaleDtlConfirm.SearchListByHQL(string.Format("bmscensaledtlconfirm.BmsCenSaleDocConfirm.Id={0}", docConfirmID));
                //验收明细的状态为已验收及部分入库集合
                var tempListDtlConfirm = listDtlConfirm.Where(p => (p.Status.Value == int.Parse(BmsCenSaleDtlConfirmStatus.已验收.Key) || p.Status.Value == int.Parse(BmsCenSaleDtlConfirmStatus.部分入库.Key))).ToList();
                if (tempListDtlConfirm == null || tempListDtlConfirm.Count <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = string.Format("当前的入库信息不符合入库条件,入库的验收明细的状态必须为{0}或{1}!", BmsCenSaleDtlConfirmStatus.GetStatusDic()[BmsCenSaleDtlConfirmStatus.已验收.Key].Name, BmsCenSaleDtlConfirmStatus.GetStatusDic()[BmsCenSaleDtlConfirmStatus.部分入库.Key].Name);
                }
                entity.Visible = true;
                entity.InDocNo = this.GetInDocNo();
                entity.CreaterID = empID;
                entity.CreaterName = empName;
                entity.OperDate = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.UserID = empID;
                this.Entity = entity;
                if (this.Add())
                {
                    BaseResultBool baseResultBool = IBReaBmsInDtl.AddReaBmsInDtl(entity, dtAddList, empID, empName);
                    if (baseResultBool.success == true)
                        baseResultBool = EditDocConfirmAndDtl(dtAddList, docConfirm, listEditDtlConfirm, listDtlConfirm, tempListDtlConfirm);
                    baseResultDataValue.success = baseResultBool.success;
                    baseResultDataValue.ErrorInfo = baseResultBool.ErrorInfo;
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "保存入库主单信息失败!";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = ex.Message;
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 验收明细的已入库数,状态及验收主单的状态更新
        /// </summary>
        /// <param name="dtAddList"></param>
        /// <param name="docConfirm">验收单</param>
        /// <param name="listEditDtlConfirm">验收明细待更新的集合</param>
        /// <param name="listDtlConfirm">某一验收单的全部验收明细</param>
        /// <param name="tempListDtlConfirm">验收明细的状态为已验收及部分入库集合</param>
        /// <returns></returns>
        private BaseResultBool EditDocConfirmAndDtl(IList<ReaBmsInDtlVO> dtAddList, BmsCenSaleDocConfirm docConfirm, IList<BmsCenSaleDtlConfirm> listEditDtlConfirm, IList<BmsCenSaleDtlConfirm> listDtlConfirm, List<BmsCenSaleDtlConfirm> tempListDtlConfirm)
        {

            BaseResultBool baseResultBool = new BaseResultBool();
            foreach (var vo in dtAddList)
            {
                var model = vo.ReaBmsInDtl;
                BmsCenSaleDtlConfirm dtlConfirm = listDtlConfirm.Where(p => p.Id == model.SaleDtlConfirmID.Value).ElementAt(0);
                double acceptCount = dtlConfirm.AcceptCount;

                IList<ReaBmsInDtl> listInDtl = IBReaBmsInDtl.SearchListByHQL(string.Format("reabmsindtl.SaleDtlConfirmID={0}", model.SaleDtlConfirmID.Value));
                double inCount = 0;
                //验收明细的已入库总数
                if (listInDtl.Count > 0) inCount = listInDtl.Sum(p => p.GoodsQty);
                //验收明细的入库总数
                inCount = inCount + dtAddList.Where(p => p.ReaBmsInDtl.SaleDtlConfirmID.Value == model.SaleDtlConfirmID.Value).Sum(p => p.ReaBmsInDtl.GoodsQty);

                ZhiFang.Common.Log.Log.Debug(string.Format("当前验收明细ID为{0},的入库总数为{1}", dtlConfirm.Id, inCount));
                if (inCount >= acceptCount)
                {
                    //当前验收明细从部分入库集合移除
                    if (tempListDtlConfirm != null && tempListDtlConfirm.Contains(dtlConfirm))
                        tempListDtlConfirm.Remove(dtlConfirm);

                    dtlConfirm.Status = int.Parse(BmsCenSaleDtlConfirmStatus.全部入库.Key);
                    dtlConfirm.StatusName = BmsCenSaleDtlConfirmStatus.GetStatusDic()[dtlConfirm.Status.ToString()].Name;
                }
                else if (inCount < acceptCount)
                {
                    dtlConfirm.Status = int.Parse(BmsCenSaleDtlConfirmStatus.部分入库.Key);
                    dtlConfirm.StatusName = BmsCenSaleDtlConfirmStatus.GetStatusDic()[dtlConfirm.Status.ToString()].Name;
                }
                dtlConfirm.InCount = (int)inCount;
                listEditDtlConfirm.Add(dtlConfirm);
            }

            foreach (BmsCenSaleDtlConfirm dtEdit in listEditDtlConfirm)
            {
                IBBmsCenSaleDtlConfirm.Entity = dtEdit;
                baseResultBool.success = IBBmsCenSaleDtlConfirm.Edit();
                if (baseResultBool.success == false)
                {
                    baseResultBool.ErrorInfo = "入库操作更新验收明细失败!";
                    break;
                }
            }
            if (baseResultBool.success == false) return baseResultBool;

            int docOldStatus = docConfirm.Status;
            //验收主单更新处理
            if (tempListDtlConfirm != null && tempListDtlConfirm.Count > 0)
            {
                docConfirm.Status = int.Parse(BmsCenSaleDocConfirmStatus.部分入库.Key);
                docConfirm.StatusName = BmsCenSaleDocConfirmStatus.GetStatusDic()[docConfirm.Status.ToString()].Name;
            }
            else
            {
                docConfirm.Status = int.Parse(BmsCenSaleDocConfirmStatus.全部入库.Key);
                docConfirm.StatusName = BmsCenSaleDocConfirmStatus.GetStatusDic()[docConfirm.Status.ToString()].Name;
            }
            IBBmsCenSaleDocConfirm.Entity = docConfirm;
            baseResultBool.success = IBBmsCenSaleDocConfirm.Edit();
            return baseResultBool;
        }

        /// <summary>
        /// 入库总单号
        /// </summary>
        /// <returns></returns>
        private string GetInDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }

    }
}