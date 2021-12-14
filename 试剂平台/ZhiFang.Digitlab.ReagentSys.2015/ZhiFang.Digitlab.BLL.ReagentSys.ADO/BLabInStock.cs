using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Common.Public;
using ZhiFang.Digitlab.DAO.ADO;

namespace ZhiFang.Digitlab.BLL.ReagentSys.ADO
{
    /// <summary>
    ///
    /// </summary>
    public class BLabInStock : ZhiFang.Digitlab.IBLL.ReagentSys.ADO.IBLabInStock
    {
        ZhiFang.Digitlab.IBLL.ReagentSys.IBCenOrg IBCenOrg { get; set; }

        public BaseResultDataValue CheckDataBaseLink(string orgNo, string orgName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BaseResultDataValue dbl = DataBaseLink.GetDataBaseLinkByOrgNo(orgNo, orgName);
            if (dbl.success)
            {
                brdv.success = SqlServerHelper.TestDataBaseConnection(dbl.ResultDataValue);
                if (!brdv.success)
                {
                    brdv.ErrorInfo = "错误信息：机构【" + orgName + "】数据库链接失败！";
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                }
            }
            else
                brdv = dbl;
            return brdv;

        }
        public BaseResultDataValue GetLabInStockCount(string orgNo, string orgName, string goodsID, string goodsNo, string prodGoodsNo, string goodsLotNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BaseResultDataValue dbl = DataBaseLink.GetDataBaseLinkByOrgNo(orgNo, orgName);
            if (dbl.success)
            {
                string connectStr = dbl.ResultDataValue;
                string strWhere = "";
                if (!string.IsNullOrEmpty(goodsID))
                    strWhere += " and BQD.GoodsID=" + goodsID;
                if (!string.IsNullOrEmpty(prodGoodsNo))
                    strWhere += " and PG.ProdGoodsNo=\'" + prodGoodsNo + "\'";
                if (!string.IsNullOrEmpty(goodsNo))
                    strWhere += " and PG.GoodsNo=\'" + goodsNo + "\'";
                if (!string.IsNullOrEmpty(goodsLotNo))
                    strWhere += " and BQD.LotNo=\'" + goodsLotNo + "\'";
                string sql = " Select LabID,GoodsID,GoodsName,LotNo,GoodsUnitID,GoodsUnit," +
                             " GoodsType,ProdCompany,BarCodeMgr,Price, " +
                             " Sum(GoodsQty) as GoodsQty,Sum(GoodsQty) * Price as SumTotal" +
                             " from (SELECT BQD.LabID,QtyDtlNo,BQD.GoodsID "+
                             " ,LotNo, GoodsUnitID, GoodsUnit, GoodsQty, Price, TaxRate " +
                             " ,PG.BarCodeMgr, PG.CName as GoodsName, PG.GoodsType, PG.ProdCompany  " +
                             " FROM BmsQtyDtl BQD inner join PGoods PG on BQD.GoodsID=PG.GoodsID " +
                             " where 1=1 " + strWhere+") MM "+
                             " Group By LabID,GoodsID,GoodsName,LotNo,GoodsUnitID,GoodsUnit,Price,BarCodeMgr,GoodsType,ProdCompany";
                DataSet ds = SqlServerHelper.QuerySql(sql, connectStr);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    brdv.ResultDataValue = JsonHelp.DataTableToJson(ds.Tables[0], "BmsQtyDtl", true);
                }
            }
            else
                brdv = dbl;
            return brdv;
        }

        public BaseResultDataValue GetTestConsumeCountResult(string orgNo, string orgName, string beginDate, string endDate, string goodsID, string goodsNo, string prodGoodsNo, string goodsLotNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BaseResultDataValue dbl = DataBaseLink.GetDataBaseLinkByOrgNo(orgNo, orgName);
            if (dbl.success)
            {
                string connectStr = dbl.ResultDataValue;
                string strWhere = "";
                if ((!string.IsNullOrEmpty(beginDate)) && (!string.IsNullOrEmpty(endDate)))
                {
                    string dateBegin = DateTime.Parse(beginDate).ToString("yyyyMM");
                    string dateEnd = DateTime.Parse(endDate).ToString("yyyyMM");
                    strWhere += " and ((CAST(TCCR.CYear AS varchar(10))+RIGHT(REPLICATE('0',2)+CAST(TCCR.CMonth AS varchar(5)),2)))>=" + dateBegin;
                    strWhere += " and ((CAST(TCCR.CYear AS varchar(10))+RIGHT(REPLICATE('0',2)+CAST(TCCR.CMonth AS varchar(5)),2)))<=" + dateEnd;
                }
                if (!string.IsNullOrEmpty(goodsID))
                    strWhere += " and TCCR.GoodsID=" + goodsID;
                if (!string.IsNullOrEmpty(prodGoodsNo))
                    strWhere += " and PG.ProdGoodsNo=\'" + prodGoodsNo + "\'";
                if (!string.IsNullOrEmpty(goodsNo))
                    strWhere += " and PG.GoodsNo=\'" + goodsNo + "\'";
                //if (!string.IsNullOrEmpty(goodsLotNo))
                //    strWhere += " and TCCR.LotNo=\'" + goodsLotNo + "\'";
                string sql = " SELECT TCCR.LabID,CYear,CMonth,DeptID " +
                             " ,DeptName, TestEquipID, TestEquipName, TestEquipTypeCode " +
                             " ,TestEquipTypeName, TestType, TestItemID, TestItemName " +
                             " ,TCCR.GoodsID, GoodsUnitID, GoodsName, UnitMemo, GoodsUnit " +
                             " ,GoodsQty, ItemPrice, OutPrice, RealTestCount, UnitTestCount " +
                             " ,ItemIncomeTotal, ShareIncome, TheoryConsumeQty, TheorySumTotal " +
                             " ,TestEquipCode, LisTestEquipName, TestItemCode, LisTestItemName " +
                             " FROM TestConsumeCountResult TCCR inner join PGoods PG on TCCR.GoodsID=PG.GoodsID " +
                             " where 1=1 " + strWhere;
                DataSet ds = SqlServerHelper.QuerySql(sql, connectStr);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    brdv.ResultDataValue = JsonHelp.DataTableToJson(ds.Tables[0], "TestConsumeCountResult", true);
                }
            }
            else
                brdv = dbl;
            return brdv;
        }
    }
}