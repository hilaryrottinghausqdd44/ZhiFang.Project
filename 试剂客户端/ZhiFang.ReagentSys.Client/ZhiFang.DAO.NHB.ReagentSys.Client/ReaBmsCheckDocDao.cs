using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsCheckDocDao : BaseDaoNHB<ReaBmsCheckDoc, long>, IDReaBmsCheckDocDao
    {
        IDReaBmsCheckDtlDao IDReaBmsCheckDtlDao { get; set; }
        public BaseResultBool EditValidIsLock(long? reaCompanyID, string companyName, long? storageID, string storageName, long? placeID, string placeName, long? goodId)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //当前盘库条件说明
            StringBuilder checkMemo = new StringBuilder();
            ReaBmsCheckDoc checkDoc = new ReaBmsCheckDoc();
            checkDoc.ReaCompanyID = reaCompanyID;
            checkDoc.CompanyName = companyName;
            checkDoc.StorageID = storageID;
            checkDoc.StorageName = storageName;
            checkDoc.PlaceID = placeID;
            checkDoc.PlaceName = placeName;

            string checkHql = GetIsLockCheckHql(checkDoc, int.Parse(ReaBmsCheckDocLock.已锁定.Key), ref checkMemo);
            string checkDtlHql = "";
            if (goodId.HasValue)
                checkDtlHql = " reabmscheckdtl.GoodsID=" + goodId;
            if (checkDtlHql.Length > 0)
            {
                IList<ReaBmsCheckDtl> checkDtlList = IDReaBmsCheckDtlDao.SearchReaBmsCheckDtlListByJoinHQL(checkHql, checkDtlHql, "", "", -1, -1);
                if (checkDtlList.Count > 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("{0}已被盘库锁定,请完成盘库后再操作!", checkMemo.ToString());
                }
            }
            else
            {
                IList<ReaBmsCheckDoc> checkDocList = this.GetListByHQL(checkHql);
                if (checkDocList.Count > 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("{0}已被盘库锁定,请完成盘库后再操作!", checkMemo.ToString());
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditValidIsLock(ReaBmsCheckDoc checkDoc, string reaGoodHql)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //当前盘库条件说明
            StringBuilder checkMemo = new StringBuilder();
            string checkHql = GetIsLockCheckHql(checkDoc, int.Parse(ReaBmsCheckDocLock.已锁定.Key), ref checkMemo);
            //机构货品盘库条件
            string reaGoodsHql2 = "";
            StringBuilder reaGoodsHql = new StringBuilder();
            if (!string.IsNullOrEmpty(checkDoc.GoodsClass))
            {
                reaGoodsHql.Append(" reagoods.GoodsClass='" + checkDoc.GoodsClass + "' or ");
                checkMemo.Append("机构货品一级分类为:" + checkDoc.GoodsClass + ",");
            }
            if (!string.IsNullOrEmpty(checkDoc.GoodsClassType))
            {
                reaGoodsHql.Append(" reagoods.GoodsClassType='" + checkDoc.GoodsClassType + "' or ");
                checkMemo.Append("机构货品二级分类为:" + checkDoc.GoodsClassType + ",");
            }
            if (reaGoodsHql.Length > 0)
            {
                char[] trimChars = new char[] { ' ', 'o', 'r' };
                reaGoodsHql2 = "(" + reaGoodsHql.ToString().TrimEnd(trimChars) + ")";
            }
            if (!string.IsNullOrEmpty(reaGoodHql))
            {
                checkMemo.Append("传入的机构货品条件:" + reaGoodHql + ",");
                if (!string.IsNullOrEmpty(reaGoodsHql2))
                    reaGoodsHql2 = reaGoodsHql2 + " and " + reaGoodHql;
                else
                    reaGoodsHql2 = reaGoodHql;
            }
            if (reaGoodsHql2.Length > 0)
            {
                IList<ReaBmsCheckDtl> checkDtlList = IDReaBmsCheckDtlDao.SearchReaBmsCheckDtlListByJoinHQL(checkHql, "", reaGoodsHql2, "", -1, -1);
                if (checkDtlList.Count > 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("{0}已被盘库锁定,请完成盘库后再操作!", checkMemo.ToString());
                }
            }
            else
            {
                IList<ReaBmsCheckDoc> checkDtlList = this.GetListByHQL(checkHql);
                if (checkDtlList.Count > 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("{0}已被盘库锁定,请完成盘库后再操作!", checkMemo.ToString());
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditValidIsLock(ReaBmsCheckDoc checkDoc, IList<ReaBmsCheckDtl> dtAddList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtAddList.Count < 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("盘库货品明细信息为空,不能盘库!");
                return tempBaseResultBool;
            }
            //当前盘库条件说明
            StringBuilder checkMemo = new StringBuilder();
            //盘库条件1
            string checkHql = GetIsLockCheckHql(checkDoc, int.Parse(ReaBmsCheckDocLock.已锁定.Key), ref checkMemo);
            //机构货品盘库条件
            string reaGoodsHql2 = "";
            StringBuilder reaGoodsHql = new StringBuilder();
            if (!string.IsNullOrEmpty(checkDoc.GoodsClass))
            {
                reaGoodsHql.Append(" reagoods.GoodsClass='" + checkDoc.GoodsClass + "' or ");
                checkMemo.Append("机构货品一级分类为:" + checkDoc.GoodsClass + ",");
            }
            if (!string.IsNullOrEmpty(checkDoc.GoodsClassType))
            {
                reaGoodsHql.Append(" reagoods.GoodsClassType='" + checkDoc.GoodsClassType + "' or ");
                checkMemo.Append("机构货品二级分类为:" + checkDoc.GoodsClassType + ",");
            }

            if (reaGoodsHql.Length > 0)
            {
                char[] trimChars = new char[] { ' ', 'o', 'r' };
                reaGoodsHql2 = "(" + reaGoodsHql.ToString().TrimEnd(trimChars) + ")";
            }
            //盘库货品盘库条件
            string checkDtlHql2 = "";
            StringBuilder checkDtlHql = new StringBuilder();
            foreach (var checkDtl in dtAddList)
            {
                if (checkDtl.GoodsID.HasValue)
                    checkDtlHql.Append(" reabmscheckdtl.GoodsID=" + checkDtl.GoodsID.Value + " and ");
            }
            if (checkDtlHql.Length > 0)
            {
                char[] trimChars = new char[] { ' ', 'a', 'n', 'd' };
                checkDtlHql2 = "(" + checkDtlHql.ToString().TrimEnd(trimChars) + ")";
            }

            IList<ReaBmsCheckDtl> dtlList = IDReaBmsCheckDtlDao.SearchReaBmsCheckDtlListByJoinHQL(checkHql, checkDtlHql2, reaGoodsHql2, "", -1, -1);
            if (dtlList.Count < 0)
            {
                tempBaseResultBool.success = false;
                checkMemo.Clear();
                foreach (var checkDtl in dtlList)
                {
                    checkMemo.Append("货品名称为:");
                    checkMemo.Append(checkDtl.GoodsName + ";");
                    checkMemo.Append(System.Environment.NewLine);
                }
                tempBaseResultBool.ErrorInfo = string.Format("存在被盘库锁定的货品信息:" + checkMemo.ToString());
                return tempBaseResultBool;
            }
            return tempBaseResultBool;
        }
        public string GetIsLockCheckHql(ReaBmsCheckDoc checkDoc, int isLock, ref StringBuilder checkMemo)
        {
            //当前盘库条件
            StringBuilder checkHql = new StringBuilder();
            //已锁定/已解锁的盘库条件
            string hql1 = "";

            #region 2019-01-22
            //if (isLock == int.Parse(ReaBmsCheckDocLock.已锁定.Key) || isLock == int.Parse(ReaBmsCheckDocLock.已解锁.Key))
            //    hql1 = "reabmscheckdoc.Visible=1 and reabmscheckdoc.IsLock=" + isLock + "";
            //else
            //    hql1 = " reabmscheckdoc.Visible=1";
            ////1.(供应商,库房,货架)全部已锁定
            //checkHql.Append("(" + hql1 + " and reabmscheckdoc.ReaCompanyID is null and reabmscheckdoc.StorageID is null and reabmscheckdoc.PlaceID is null)");

            ////2.或者某一供应商已锁定
            //if (checkDoc.ReaCompanyID.HasValue)
            //{
            //    checkHql.Append(" or (" + hql1 + " and reabmscheckdoc.ReaCompanyID=" + checkDoc.ReaCompanyID.Value + ")");
            //    checkMemo.Append("供应商为:" + checkDoc.CompanyName + ",");
            //}
            ////3.或者某一库房已锁定
            //if (checkDoc.StorageID.HasValue)
            //{
            //    checkHql.Append(" or (" + hql1 + " and reabmscheckdoc.StorageID=" + checkDoc.StorageID.Value + ")");
            //    checkMemo.Append("库房为:" + checkDoc.StorageName + ",");
            //}
            ////4.或者某一货架已锁定
            //if (checkDoc.PlaceID.HasValue)
            //{
            //    checkHql.Append(" or (" + hql1 + " and reabmscheckdoc.PlaceID=" + checkDoc.PlaceID.Value + ")");
            //    checkMemo.Append("货架为:" + checkDoc.PlaceName + ",");
            //}
            ////5.全部锁定
            //if (checkMemo.ToString().Length <= 0)
            //    checkMemo.Append("按全部,");
            #endregion

            if (isLock == int.Parse(ReaBmsCheckDocLock.已锁定.Key) || isLock == int.Parse(ReaBmsCheckDocLock.已解锁.Key))
                hql1 = "reabmscheckdoc.Visible=1 and reabmscheckdoc.IsLock=" + isLock + "";
            else
                hql1 = " reabmscheckdoc.Visible=1";

            //1.供应商,库房,货架,一级分类,二级分类组合的盘库已锁定
            string checkHql2 = "";
            if (checkDoc.ReaCompanyID.HasValue)
            {
                checkHql.Append(" reabmscheckdoc.ReaCompanyID=" + checkDoc.ReaCompanyID.Value + " and ");
                checkMemo.Append("供应商为:" + checkDoc.CompanyName + ",");
            }
            if (checkDoc.StorageID.HasValue)
            {
                checkHql.Append(" reabmscheckdoc.StorageID=" + checkDoc.StorageID.Value + " and ");
                checkMemo.Append("库房为:" + checkDoc.StorageName + ",");
            }
            if (checkDoc.PlaceID.HasValue)
            {
                checkHql.Append(" reabmscheckdoc.PlaceID=" + checkDoc.PlaceID.Value + " and ");
                checkMemo.Append("货架为:" + checkDoc.PlaceName + ",");
            }
            if (!string.IsNullOrEmpty(checkDoc.GoodsClass))
            {
                checkHql.Append(" reabmscheckdoc.GoodsClass='" + checkDoc.GoodsClass + "' and ");
                checkMemo.Append("机构货品一级分类为:" + checkDoc.GoodsClass + ",");
            }
            if (!string.IsNullOrEmpty(checkDoc.GoodsClassType))
            {
                checkHql.Append(" reabmscheckdoc.GoodsClassType='" + checkDoc.GoodsClassType + "' and ");
                checkMemo.Append("机构货品二级分类为:" + checkDoc.GoodsClassType + ",");
            }
            if (checkHql.Length > 0)
            {
                checkHql.Append(hql1 + " and ");
                char[] trimChars = new char[] { ' ', 'a', 'n', 'd' };
                checkHql2 = "(" + checkHql.ToString().TrimEnd(trimChars) + ")";
            }

            //(供应商,库房,货架,一级分类,二级分类)全部已锁定
            string checkHql3 = "(" + hql1 + " and reabmscheckdoc.ReaCompanyID is null and reabmscheckdoc.StorageID is null and reabmscheckdoc.PlaceID is null and reabmscheckdoc.GoodsClass is null and reabmscheckdoc.GoodsClassType is null)";
            //5.全部锁定
            if (checkMemo.ToString().Length <= 0)
                checkMemo.Append("按全部,");
            string checkHql4 = checkHql3;
            if (!string.IsNullOrEmpty(checkHql2))
                checkHql4 = string.Format("({0}) or ({1})", checkHql2, checkHql4);//checkHql2 + " or " + checkHql4;

            return "(" + checkHql4 + ")";
        }
    }
}