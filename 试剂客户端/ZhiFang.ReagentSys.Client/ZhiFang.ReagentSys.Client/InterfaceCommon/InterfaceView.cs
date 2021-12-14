using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.ReagentSys.Client.Service_WangHai_ShiJiTan;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client;

namespace ZhiFang.ReagentSys.Client
{
    public class InterfaceView
    {
        public DataSet GetDBViewInfo(string strSQL)
        {
            DataSet ds = new DataSet();
            string dbType = "";
            string dbConnectStr = "";
            InterfaceCommon.GetThirdSaleDocDBInfo(ref dbType, ref dbConnectStr);
            if (!string.IsNullOrEmpty(dbConnectStr))
            {
                if ((!string.IsNullOrEmpty(dbType)) && dbType.ToLower() == "oracle")
                {
                    ds = ZhiFang.DBUpdate.PM.OracleHelper.QuerySql(strSQL, dbConnectStr);
                }
                else
                {
                    ds = ZhiFang.DBUpdate.PM.SqlServerHelper.QuerySql(strSQL, dbConnectStr);
                }
            }
            else
                ZhiFang.Common.Log.Log.Error("无法获取数据库连接字符串！");
            return ds;
        }

        public BaseResultData GetMateType(string mateTypeCode)
        {
            BaseResultData brd = new BaseResultData();

            return brd;
        }

        public BaseResultData GetReaCenOrgInfo(string compCode, IBReaCenOrg IBReaCenOrg)
        {
            BaseResultData brd = new BaseResultData();
            string strSQL = " select * from V_ReaComp";
            if (!string.IsNullOrEmpty(compCode))
                strSQL += " where MatchCode=\'" + compCode + "\'";
            DataSet ds = GetDBViewInfo(strSQL);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                if (IBReaCenOrg == null)
                {
                    IApplicationContext context = ContextRegistry.GetContext();
                    IBReaCenOrg = (IBReaCenOrg)context.GetObject("BReaCenOrg");
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["ReaCompCode"] == null || dr["ReaCompCode"].ToString() == "")
                        continue;
                    brd = IBReaCenOrg.AddReaCenOrgSyncByInterface("MatchCode", dr["ReaCompCode"].ToString(), new Dictionary<string, object> {
                        { "CName", dr["CName"] },
                        { "EName", dr["EName"] },
                        { "Address", dr["Address"] },
                        { "Tel", dr["Tel"] },
                        { "Email", dr["Email"] },
                        { "Memo", dr["Memo"] },
                        { "MatchCode", dr["ReaCompCode"] }
                    });
                }
            }
            return brd;
        }

        public BaseResultData GetReaGoodsInfo(string goodCode, IBReaGoods IBReaGoods, ref IList<ReaCenOrg> listReaCenOrg, ref IList<ReaGoods> listReaGoods)
        {
            BaseResultData brd = new BaseResultData();
            string strSQL = " select * from V_ReaGoods";
            if (!string.IsNullOrEmpty(goodCode))
                strSQL += " where MatchCode=\'" + goodCode + "\'";
            DataSet ds = GetDBViewInfo(strSQL);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                Dictionary<string, ReaCenOrg> dicCompCode = new Dictionary<string, ReaCenOrg>();
                if (IBReaGoods == null)
                {
                    IApplicationContext context = ContextRegistry.GetContext();
                    IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                }
                Dictionary<string, object> dicFieldAndValue = new Dictionary<string, object>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["MatchCode"] == null || dr["MatchCode"].ToString() == "")
                        continue;
                    ReaGoods reaGoods = new ReaGoods();
                    ReaCenOrg reaCenOrg = new ReaCenOrg();
                    dicFieldAndValue.Clear();
                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                       dicFieldAndValue.Add(dc.ColumnName, dr[dc.ColumnName]);
                    }
                    if (!dicFieldAndValue.Keys.Contains("ReaGoodsNo"))
                        dicFieldAndValue.Add("ReaGoodsNo", dicFieldAndValue["MatchCode"].ToString());
                    else
                        dicFieldAndValue["ReaGoodsNo"] = dicFieldAndValue["MatchCode"].ToString();
                    brd = IBReaGoods.AddReaGoodsSyncByInterface("MatchCode", dicFieldAndValue["MatchCode"].ToString(), dicFieldAndValue, ref reaCenOrg, ref reaGoods);
                    listReaGoods.Add(reaGoods);
                    if ((!string.IsNullOrEmpty(reaCenOrg.MatchCode)) && (!dicCompCode.ContainsKey(reaCenOrg.MatchCode)))
                        dicCompCode.Add(reaCenOrg.MatchCode, reaCenOrg);
                }
                foreach (KeyValuePair<string, ReaCenOrg> kv in dicCompCode)
                    listReaCenOrg.Add(kv.Value);
            }
            return brd;
        }

        public BaseResultData GetUserInfo(string userCode, HRDept dept, IBHREmployee IBHREmployee)
        {
            BaseResultData brd = new BaseResultData();
            string strSQL = " select * from V_UerInfo";
            if (!string.IsNullOrEmpty(userCode))
                strSQL += " where MatchCode=\'" + userCode + "\'";
            DataSet ds = GetDBViewInfo(strSQL);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                if (IBHREmployee == null)
                {
                    IApplicationContext context = ContextRegistry.GetContext();
                    IBHREmployee = (IBHREmployee)context.GetObject("BHREmployee");
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["EmpCode"] == null || dr["EmpCode"].ToString() == "")
                        continue;
                    brd = IBHREmployee.AddHREmployeeSyncByInterface("MatchCode", dr["EmpCode"].ToString(), dept, new Dictionary<string, object> {
                        { "CName", dr["CName"] },
                        { "EName", dr["EName"] },
                        { "Birthday", dr["Birthday"] },
                        { "Tel", dr["Tel"] },
                        { "Email", dr["Email"] },
                        { "Comment", dr["Memo"] },
                        { "MatchCode", dr["EmpCode"] },
                        { "UseCode", dr["EmpCode"] }
                    }, false);
                }
            }
            return brd;
        }

        public BaseResultData GetDeptInfo(string deptCode, HRDept dept, IBHRDept IBHRDept)
        {
            BaseResultData brd = new BaseResultData();
            string strSQL = " select * from V_DeptInfo ";
            if (!string.IsNullOrEmpty(deptCode))
                strSQL += " where MatchCode=\'" + deptCode + "\'";
            DataSet ds = GetDBViewInfo(strSQL);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                if (IBHRDept == null)
                {
                    IApplicationContext context = ContextRegistry.GetContext();
                    IBHRDept = (IBHRDept)context.GetObject("BHRDept");
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["DeptCode"] == null || dr["DeptCode"].ToString() == "")
                        continue;
                    brd = IBHRDept.AddHRDeptSyncByInterface("MatchCode", dr["DeptCode"].ToString(), new Dictionary<string, object> {
                            { "CName", dr["CName"] },
                            { "MatchCode", dr["DeptCode"] },
                            { "StandCode", dr["DeptCode"]},
                            { "DeveCode", dr["DeptCode"] },
                            { "EName", dr["EName"] },
                            { "SName", dr["SName"] },
                            { "Comment", dr["Comment"] },
                    });
                }
            }
            return brd;
        }

        public BaseResultData GetStoreInfo(string storeCode, IBReaStorage IBReaStorage)
        {
            BaseResultData brd = new BaseResultData();
            string strSQL = " select * from V_Storage";
            if (!string.IsNullOrEmpty(storeCode))
                strSQL += " where MatchCode=\'" + storeCode + "\'";
            DataSet ds = GetDBViewInfo(strSQL);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                if (IBReaStorage == null)
                {
                    IApplicationContext context = ContextRegistry.GetContext();
                    IBReaStorage = (IBReaStorage)context.GetObject("BReaStorage");
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["StorageCode"] == null || dr["StorageCode"].ToString() == "")
                        continue;
                    brd = IBReaStorage.AddReaStorageSyncByInterface("MatchCode", dr["StorageCode"].ToString(), new Dictionary<string, object> {
                        { "CName", dr["CName"] },
                        { "ShortCode", dr["StorageCode"] },
                        { "ZX1", dr["ZX1"] },
                        { "ZX2", dr["ZX2"] },
                        { "MatchCode", dr["StorageCode"] } });
                }
            }
            return brd;
        }

        public BaseResultData GetReaSaleDocInfo(string saleDocNo, IBReaBmsCenSaleDoc IBReaBmsCenSaleDoc, ref ReaBmsCenSaleDoc saleDoc, ref IList<ReaBmsCenSaleDtl> saleDtlList, ref long storageID)
        {
            BaseResultData brd = new BaseResultData();
            string strSQL = " select * from V_ReaSaleDoc where SaleDocNo=\'" + saleDocNo + "\'";
            DataSet dsSaleDoc = GetDBViewInfo(strSQL);
            if (dsSaleDoc != null && dsSaleDoc.Tables != null && dsSaleDoc.Tables.Count > 0 && dsSaleDoc.Tables[0].Rows.Count > 0)
            {
                strSQL = " select * from V_ReaSaleDtl where SaleDocNo=\'" + saleDocNo + "\'";
                DataSet dsSaleDtl = GetDBViewInfo(strSQL);
                if (dsSaleDtl != null && dsSaleDtl.Tables != null && dsSaleDtl.Tables.Count > 0 && dsSaleDtl.Tables[0].Rows.Count > 0)
                {
                    brd = IBReaBmsCenSaleDoc.AddReaBmsCenSaleDocByInterface(dsSaleDoc, dsSaleDtl, ref saleDoc, ref saleDtlList);

                }//dsSaleDtl
                else
                {
                    brd.success = false;
                    brd.message = "无编号为【" + saleDocNo + "】供货单明细信息";
                    ZhiFang.Common.Log.Log.Error(brd.message);
                }

            }//dsSaleDoc
            else
            {
                brd.success = false;
                brd.message = "无编号为【"+ saleDocNo + "】供货单信息";
                ZhiFang.Common.Log.Log.Error(brd.message);
            } 

            return brd;
        }

    }
}
