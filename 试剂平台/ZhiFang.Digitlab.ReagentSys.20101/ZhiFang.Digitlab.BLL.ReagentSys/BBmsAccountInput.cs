using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;
using System.Web;
using System.IO;
using ZhiFang.Digitlab.IDAO.ReagentSys;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BBmsAccountInput : BaseBLL<BmsAccountInput>, ZhiFang.Digitlab.IBLL.ReagentSys.IBBmsAccountInput
    {
        IBLL.ReagentSys.IBBmsAccountSaleDoc IBBmsAccountSaleDoc { get; set; }
        IBLL.ReagentSys.IBBmsCenSaleDoc IBBmsCenSaleDoc { get; set; }
        public BaseResultDataValue AddBmsAccountInputAndDtList(string saleDocIDStr)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            #region 验证
            if (this.Entity == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "entity信息为空!";
                return tempBaseResultDataValue;
            }
            if (this.Entity.Lab == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "机构信息为空!";
                return tempBaseResultDataValue;
            }
            if (String.IsNullOrEmpty(saleDocIDStr))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "入帐供货单信息为空!";
                return tempBaseResultDataValue;
            }
            #endregion

            //待入账供货单信息
            IList<BmsCenSaleDoc> bmsCenSaleDocList = new List<BmsCenSaleDoc>();
            bmsCenSaleDocList = IBBmsCenSaleDoc.SearchListByHQL("Id in (" + saleDocIDStr + ")");

            #region 判断待入帐的供货单是否存在已入账的
            IList<BmsAccountSaleDoc> bmsAccountSaleDoccList = IBBmsAccountSaleDoc.SearchListByHQL("bmsaccountsaledoc.BmsCenSaleDoc.Id in (" + saleDocIDStr + ")");
            if (bmsAccountSaleDoccList != null && bmsAccountSaleDoccList.Count > 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "存在已入账的供货单信息!";
                return tempBaseResultDataValue;
            }

            if (bmsCenSaleDocList.Count > 0)
            {
                var tempList = bmsCenSaleDocList.Where(p => p.IsAccountInput != null && p.IsAccountInput.Value == 1).ToList();
                if (tempList != null && tempList.Count > 0)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "存在已入账的供货单信息!";
                    return tempBaseResultDataValue;
                }
            }
            #endregion

            try
            {
                if (this.Add())
                {
                    //入帐明细保存
                    tempBaseResultDataValue = this.AddBatchDt(this.Entity, bmsCenSaleDocList);
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "新增入账保存失败!";
                }
                if (tempBaseResultDataValue.success)
                {
                    tempBaseResultDataValue.success = IBBmsCenSaleDoc.UpdateBatchIsAccountInput(saleDocIDStr, 1);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo + "错误信息:" + ex.Message);
            }
            finally
            {
                //删除已经保存的入帐明细和入帐总单信息
                if (tempBaseResultDataValue.success == false)
                {
                    IBBmsAccountSaleDoc.DeleteBmsAccountSaleDocListByAccountID(this.Entity.Id);
                    this.Remove();
                }
            }

            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 新增入帐明细
        /// </summary>
        /// <param name="bmsAccountInput">入帐总单</param>
        /// <param name="bmsCenSaleDocList">待入账供货单信息</param>
        /// <returns></returns>
        public BaseResultDataValue AddBatchDt(BmsAccountInput bmsAccountInput, IList<BmsCenSaleDoc> bmsCenSaleDocList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            if (bmsAccountInput.DataTimeStamp == null)
            {
                bmsAccountInput.DataTimeStamp = dataTimeStamp;
            }
            foreach (var bmsCenSaleDoc in bmsCenSaleDocList)
            {
                BmsAccountSaleDoc entity = new BmsAccountSaleDoc();
                entity.LabID = bmsAccountInput.LabID;
                entity.LabName = bmsAccountInput.LabName;
                entity.UserID = bmsAccountInput.UserID;
                entity.UserName = bmsAccountInput.UserName;
                entity.BmsAccountInput = bmsAccountInput;
                entity.BmsCenSaleDoc = bmsCenSaleDoc;
                entity.Lab = bmsAccountInput.Lab;
                IBBmsAccountSaleDoc.Entity = entity;
                tempBaseResultDataValue.success = IBBmsAccountSaleDoc.Add();
                if (tempBaseResultDataValue.success == false)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "新增供货单入账保存失败!" + "供货单ID为:" + bmsCenSaleDoc.Id + ",供货单号:" + bmsCenSaleDoc.SaleDocNo;
                    ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                    break;
                }
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool DeleteBmsAccountInputAndDtList(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            //待删除的入账明细信息
            IList<BmsAccountSaleDoc> bmsAccountSaleDocList = new List<BmsAccountSaleDoc>();
            bmsAccountSaleDocList = IBBmsAccountSaleDoc.SearchListByHQL("bmsaccountsaledoc.BmsAccountInput.Id=" + id);
            StringBuilder saleDocIDStr = new StringBuilder();
            foreach (var bmsAccountSaleDoc in bmsAccountSaleDocList)
            {
                saleDocIDStr.Append(bmsAccountSaleDoc.BmsCenSaleDoc.Id);
                saleDocIDStr.Append(",");
            }
            try
            {
                //using (TransactionScope ts = new TransactionScope())
                //{
                tempBaseResultBool.success = IBBmsAccountSaleDoc.DeleteBmsAccountSaleDocListByAccountID(id);
                if (tempBaseResultBool.success == false)
                {
                    tempBaseResultBool.ErrorInfo = "删除入帐总单明细失败!";
                }
                if (!string.IsNullOrEmpty(saleDocIDStr.ToString()))
                {
                    //更新待入账供货单信息的是否入账标志为待入帐
                    tempBaseResultBool.success = IBBmsCenSaleDoc.UpdateBatchIsAccountInput(saleDocIDStr.ToString().TrimEnd(','), 0);
                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "更新供货单的是否入账标志为待入帐失败!";
                    }
                }
                if (tempBaseResultBool.success == true)
                {
                    string hql = " from BmsAccountInput bmsaccountinput where bmsaccountinput.Id =" + id;
                    int counts = this.DeleteByHql(hql);
                    if (counts > 0)
                    {
                        tempBaseResultBool.success = true;
                    }
                    else
                    {
                        tempBaseResultBool.success = false;
                    }
                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "删除入帐总单失败!";
                    }
                    //}
                    //ts.Complete();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = tempBaseResultBool.ErrorInfo + "错误信息:" + ex.Message;
                ZhiFang.Common.Log.Log.Error(tempBaseResultBool.ErrorInfo);
                throw ex;
            }
            return tempBaseResultBool;
        }
    }
}