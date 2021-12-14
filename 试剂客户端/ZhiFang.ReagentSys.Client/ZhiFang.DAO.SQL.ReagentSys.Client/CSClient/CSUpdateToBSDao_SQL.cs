using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.SQL.ReagentSys.Client
{
    /// <summary>
    /// 因为CS机构货品单位可能存在大小包装单位的机构货品信息,在导入CS机构货品时,BS同时保存CS的GoodsID和GoodsUnitID;在从CS导入ReaEquipReagentLink,ReaGoodsOrgLink,ReaDeptGoods,ReaBmsQtyDtl时,BS各实体的GoodsID暂时取CS的GoodsUnitID,在保存到BS时,再按GoodsUnitID从BS的机构货品里找到对应的GoodsID
    /// </summary>
    public class CSUpdateToBSDao_SQL : IDCSUpdateToBSDao_SQL
    {
        DBUtility.SqlServerHelperP DbHelperSQL = new CSClient.DbHelperSQLP(CSClient.DbHelperSQLP.GetConnectionString());
        /// <summary>
        /// 获得CS的PDept数据列表
        /// </summary>
        public IList<HRDept> GetHRDeptList(string strWhere)
        {
            IList<HRDept> entityList = new List<HRDept>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM PDept as HRDept ");

            string strWhere1 = "DeptID>0";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    HRDept model = new HRDept();
                    if (row["DeptID"] != null && row["DeptID"].ToString() != "")
                    {
                        model.Id = long.Parse(row["DeptID"].ToString());
                    }
                    if (row["DeptID"] != null)
                    {
                        model.StandCode = row["DeptID"].ToString();
                    }
                    if (row["CName"] != null)
                    {
                        model.CName = row["CName"].ToString();
                    }
                    if (row["EName"] != null)
                    {
                        model.EName = row["EName"].ToString();
                    }
                    if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(row["DispOrder"].ToString());
                    }
                    if (row["OtherCode"] != null)
                    {
                        model.SName = row["OtherCode"].ToString();
                    }
                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
        /// <summary>
        /// 获得CS的人员帐号数据列表
        /// </summary>
        public IList<RBACUser> GetRBACUserAndHREmployeeList(string strWhere)
        {
            IList<RBACUser> entityList = new List<RBACUser>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM Puser as RBACUser ");
            string strWhere1 = "UserNo>0";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    RBACUser model = new RBACUser();
                    model.AccLock = false;
                    model.AccBeginTime = DateTime.Now;
                    if (row["Shortcode"] != null)
                    {
                        model.Account = row["Shortcode"].ToString();
                    }
                    model.PWD = Common.Public.SecurityHelp.MD5Encrypt("123456", Common.Public.SecurityHelp.PWDMD5Key);
                    if (row["UserNo"] != null && row["UserNo"].ToString() != "")
                    {
                        model.Id = long.Parse(row["UserNo"].ToString());
                    }
                    if (row["UserNo"] != null)
                    {
                        model.UseCode = row["UserNo"].ToString();
                    }

                    if (row["Cname"] != null)
                    {
                        model.CName = row["Cname"].ToString();
                    }
                    if (row["Shortcode"] != null)
                    {
                        model.Shortcode = row["Shortcode"].ToString();
                    }
                    if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(row["DispOrder"].ToString());
                    }

                    HREmployee employee = new HREmployee();
                    if (row["UserNo"] != null && row["UserNo"].ToString() != "")
                    {
                        employee.Id = long.Parse(row["UserNo"].ToString());
                    }
                    if (row["UserNo"] != null)
                    {
                        employee.UseCode = row["UserNo"].ToString();
                    }
                    employee.NameL = "";
                    employee.NameF = "";
                    employee.IsUse = true;
                    employee.IsEnabled = 1;
                    if (row["Cname"] != null)
                    {
                        employee.CName = row["Cname"].ToString();
                    }
                    if (row["DeptNo"] != null && row["DeptNo"].ToString() != "")
                    {
                        HRDept dept = new HRDept();
                        dept.Id = long.Parse(row["DeptNo"].ToString());
                        employee.HRDept = dept;
                        if (employee.HRDept.DataTimeStamp == null)
                            employee.HRDept.DataTimeStamp = dataTimeStamp;
                    }
                    else
                    {
                        employee.HRDept = null;
                    }
                    if (row["UserNo"] != null && row["UserNo"].ToString() != "")
                    {
                        employee.Id = long.Parse(row["UserNo"].ToString());
                    }

                    model.HREmployee = employee;
                    model.IsUse = true;
                    if (model.HREmployee.DataTimeStamp == null)
                        model.HREmployee.DataTimeStamp = dataTimeStamp;
                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
        /// <summary>
        /// 获得CS的RBACRole数据列表
        /// </summary>
        public IList<RBACRole> GetRBACRoleList(string strWhere)
        {
            IList<RBACRole> entityList = new List<RBACRole>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM PubOffice as RBACRole ");
            string strWhere1 = "OfficeID>0";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    RBACRole model = new RBACRole();
                    if (row["OfficeID"] != null && row["OfficeID"].ToString() != "")
                    {
                        model.Id = long.Parse(row["OfficeID"].ToString());
                    }
                    if (row["OfficeID"] != null)
                    {
                        model.UseCode = row["OfficeID"].ToString();
                    }
                    if (row["CName"] != null)
                    {
                        model.CName = row["CName"].ToString();
                    }
                    //if (row["EName"] != null)
                    //{
                    //    model.EName = row["EName"].ToString();
                    //}
                    if (row["Shortcode"] != null)
                    {
                        model.Shortcode = row["Shortcode"].ToString();
                    }
                    if (row["OfficeDesc"] != null)
                    {
                        model.Comment = row["OfficeDesc"].ToString();
                    }
                    if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(row["DispOrder"].ToString());
                    }
                    model.IsUse = true;
                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
        /// <summary>
        /// 获得CS的ReaCenOrg数据列表
        /// </summary>
        public IList<ReaCenOrg> GetReaCenOrgList(string strWhere)
        {
            IList<ReaCenOrg> entityList = new List<ReaCenOrg>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM PCompany as ReaCenOrg ");
            string strWhere1 = "CompanyID>0";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ReaCenOrg model = new ReaCenOrg();
                    if (row["CompanyID"] != null && row["CompanyID"].ToString() != "")
                    {
                        model.Id = long.Parse(row["CompanyID"].ToString());
                    }
                    if (row["CName"] != null)
                    {
                        model.CName = row["CName"].ToString();
                    }
                    if (row["EName"] != null)
                    {
                        model.EName = row["EName"].ToString();
                    }
                    if (row["ShortCode"] != null)
                    {
                        model.ShortCode = row["ShortCode"].ToString();
                    }
                    if (row["DataCenterIP"] != null)
                    {
                        model.ServerIP = row["DataCenterIP"].ToString();
                    }
                    if (row["ZX1"] != null)
                    {
                        model.ZX1 = row["ZX1"].ToString();
                    }
                    if (row["ZX2"] != null)
                    {
                        model.ZX2 = row["ZX2"].ToString();
                    }
                    if (row["ZX3"] != null)
                    {
                        model.ZX3 = row["ZX3"].ToString();
                    }
                    if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(row["DispOrder"].ToString());
                    }
                    if (row["Visible"] != null && row["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(row["Visible"].ToString());
                    }
                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
        /// <summary>
        /// 获得CS的ReaStorage数据列表
        /// </summary>
        public IList<ReaStorage> GetReaStorageList(string strWhere)
        {
            IList<ReaStorage> entityList = new List<ReaStorage>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM PStorage as ReaStorage ");
            string strWhere1 = "StorageID>0";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ReaStorage model = new ReaStorage();
                    if (row["StorageID"] != null && row["StorageID"].ToString() != "")
                    {
                        model.Id = long.Parse(row["StorageID"].ToString());
                    }
                    if (row["CName"] != null)
                    {
                        model.CName = row["CName"].ToString();
                    }
                    if (row["ShortCode"] != null)
                    {
                        model.ShortCode = row["ShortCode"].ToString();
                    }
                    if (row["ZX1"] != null)
                    {
                        model.ZX1 = row["ZX1"].ToString();
                    }
                    if (row["ZX2"] != null)
                    {
                        model.ZX2 = row["ZX2"].ToString();
                    }
                    if (row["ZX3"] != null)
                    {
                        model.ZX3 = row["ZX3"].ToString();
                    }
                    if (row["Memo"] != null)
                    {
                        model.Memo = row["Memo"].ToString();
                    }
                    if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(row["DispOrder"].ToString());
                    }
                    if (row["Visible"] != null && row["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(row["Visible"].ToString()) == 1 ? true : false;
                    }
                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
        /// <summary>
        /// 获得CS的ReaPlace数据列表
        /// </summary>
        public IList<ReaPlace> GetReaPlaceList(string strWhere)
        {
            IList<ReaPlace> entityList = new List<ReaPlace>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM PPlace as ReaPlace ");
            string strWhere1 = "PlaceID>0";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ReaPlace model = new ReaPlace();
                    if (row["PlaceID"] != null && row["PlaceID"].ToString() != "")
                    {
                        model.Id = long.Parse(row["PlaceID"].ToString());
                    }
                    if (row["StorageID"] != null && row["StorageID"].ToString() != "")
                    {
                        ReaStorage reaStorage = new ReaStorage();
                        reaStorage.Id = long.Parse(row["StorageID"].ToString());
                        model.ReaStorage = reaStorage;// long.Parse(row["StorageID"].ToString());
                        if (model.ReaStorage.DataTimeStamp == null)
                            model.ReaStorage.DataTimeStamp = dataTimeStamp;
                    }
                    else
                    {
                        model.ReaStorage = null;
                    }
                    if (row["CName"] != null)
                    {
                        model.CName = row["CName"].ToString();
                    }
                    if (row["ShortCode"] != null)
                    {
                        model.ShortCode = row["ShortCode"].ToString();
                    }
                    if (row["ZX1"] != null)
                    {
                        model.ZX1 = row["ZX1"].ToString();
                    }
                    if (row["ZX2"] != null)
                    {
                        model.ZX2 = row["ZX2"].ToString();
                    }
                    if (row["ZX3"] != null)
                    {
                        model.ZX3 = row["ZX3"].ToString();
                    }
                    if (row["Memo"] != null)
                    {
                        model.Memo = row["Memo"].ToString();
                    }
                    if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(row["DispOrder"].ToString());
                    }
                    if (row["Visible"] != null && row["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(row["Visible"].ToString()) == 1 ? true : false;
                    }
                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
        /// <summary>
        /// 获得CS的ReaTestEquipLab数据列表
        /// </summary>
        public IList<ReaTestEquipLab> GetReaTestEquipLabList(string strWhere)
        {
            IList<ReaTestEquipLab> entityList = new List<ReaTestEquipLab>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM TestEquip as ReaTestEquipLab ");
            string strWhere1 = "TestEquipID>0";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ReaTestEquipLab model = new ReaTestEquipLab();
                    if (row["TestEquipID"] != null && row["TestEquipID"].ToString() != "")
                    {
                        model.Id = long.Parse(row["TestEquipID"].ToString());
                    }
                    if (row["DeptID"] != null && row["DeptID"].ToString() != "")
                    {
                        model.DeptID = long.Parse(row["DeptID"].ToString());
                    }
                    if (row["CName"] != null)
                    {
                        model.CName = row["CName"].ToString();
                    }
                    if (row["EName"] != null)
                    {
                        model.EName = row["EName"].ToString();
                    }
                    if (row["ShortCode"] != null)
                    {
                        model.ShortCode = row["ShortCode"].ToString();
                    }
                    if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(row["DispOrder"].ToString());
                    }
                    if (row["Visible"] != null && row["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(row["Visible"].ToString());
                    }
                    if (row["LisCode"] != null)
                    {
                        model.LisCode = row["LisCode"].ToString();
                    }
                    if (row["Memo"] != null)
                    {
                        model.Memo = row["Memo"].ToString();
                    }
                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
        /// <summary>
        /// 获得CS的ReaGoods数据列表
        /// </summary>
        public IList<ReaGoods> GetReaGoodsList(string strWhere)
        {
            IList<ReaGoods> entityList = new List<ReaGoods>();
            StringBuilder strSql = new StringBuilder();
            //CS的机构货品以CS的PGoodsUnit为基础,
            strSql.Append(" SELECT PGoodsUnit.GoodsUnitID,PLabGoodsDtlItem.LowQty as StoreLower,PLabGoodsDtlItem.HeightQty as StoreUpper,PLabGoodsDtlItem.EffectiveDates as BeforeWarningDay, PGoodsClass.Cname as GoodsClass, PGoodsClassType.Cname as GoodsClassType,PGoodsUnit.GoodsUnit, PGoodsUnit.Memo as UnitMemo, ReaGoods.* FROM PGoods as ReaGoods left join PGoodsUnit on PGoodsUnit.GoodsID=ReaGoods.GoodsID left join PGoodsClass on PGoodsClass.GoodsClassID=ReaGoods.GoodsClassID left join PGoodsClassType on PGoodsClassType.GoodsClassTypeID=ReaGoods.GoodsClassTypeID left join PLabGoodsDtlItem on PLabGoodsDtlItem.GoodsID=ReaGoods.GoodsID and PLabGoodsDtlItem.GoodsUnitID=PGoodsUnit.GoodsUnitID ");
            string strWhere1 = "ReaGoods.GoodsID>0 and PGoodsUnit.GoodsUnitID>0";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by ReaGoods.GoodsID,ReaGoods.DefaultUnitID,PGoodsUnit.GoodsUnitID,PGoodsUnit.GoodsUnit ");

            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            IList<long> goodsIdList = new List<long>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ReaGoods model = new ReaGoods();
                    long goodsID = long.Parse(row["GoodsID"].ToString());
                    long goodsUnitID = long.Parse(row["GoodsUnitID"].ToString());
                    //先取CS的机构货品GoodsID,如果GoodsID已经作为BS机构货品的主键,再取CS的包装单位ID
                    if (!goodsIdList.Contains(goodsID))
                    {
                        model.Id = goodsID;
                        goodsIdList.Add(goodsID);
                    }
                    else
                    {
                        model.Id = goodsUnitID;
                    }
                    model.GoodsUnitID = goodsUnitID;
                    //model.ZX3 = goodsUnitID.ToString();
                    //以CS原GoodsID作为ReaGoodsNo
                    model.ReaGoodsNo = row["GoodsID"].ToString();
                    if (row["ProdGoodsNo"] != null)
                    {
                        model.ProdGoodsNo = row["ProdGoodsNo"].ToString();
                        model.MatchCode = row["ProdGoodsNo"].ToString();
                    }
                    if (row["GoodsNo"] != null)
                    {
                        model.GoodsNo = row["GoodsNo"].ToString();
                    }
                    if (row["CName"] != null)
                    {
                        model.CName = row["CName"].ToString();
                    }
                    if (row["EName"] != null)
                    {
                        model.EName = row["EName"].ToString();
                    }
                    if (row["ShortCode"] != null)
                    {
                        model.ShortCode = row["ShortCode"].ToString();
                    }
                    if (row["GoodsClass"] != null)
                    {
                        model.GoodsClass = row["GoodsClass"].ToString();
                    }
                    if (row["GoodsClassType"] != null)
                    {
                        model.GoodsClassType = row["GoodsClassType"].ToString();
                    }

                    if (row["ProdCompany"] != null)
                    {
                        model.ProdOrgName = row["ProdCompany"].ToString();
                    }
                    if (row["ProdEara"] != null)
                    {
                        model.ProdEara = row["ProdEara"].ToString();
                    }
                    if (row["GoodsUnit"] != null)
                    {
                        model.UnitName = row["GoodsUnit"].ToString();
                    }
                    if (row["UnitMemo"] != null)
                    {
                        model.UnitMemo = row["UnitMemo"].ToString();
                    }
                    if (row["Standard"] != null)
                    {
                        model.Standard = row["Standard"].ToString();
                    }
                    if (row["StoreLower"] != null && !string.IsNullOrEmpty(row["StoreLower"].ToString()))
                    {
                        model.StoreLower = double.Parse(row["StoreLower"].ToString());
                    }
                    if (row["StoreUpper"] != null && !string.IsNullOrEmpty(row["StoreUpper"].ToString()))
                    {
                        model.StoreUpper = double.Parse(row["StoreUpper"].ToString());
                    }
                    if (row["BeforeWarningDay"] != null && !string.IsNullOrEmpty(row["BeforeWarningDay"].ToString()))
                    {
                        model.BeforeWarningDay = double.Parse(row["BeforeWarningDay"].ToString());
                    }
                    if (row["GoodsDesc"] != null)
                    {
                        model.GoodsDesc = row["GoodsDesc"].ToString();
                    }
                    if (row["ApproveDocNo"] != null)
                    {
                        model.ApproveDocNo = row["ApproveDocNo"].ToString();
                    }
                    if (row["Standard"] != null)
                    {
                        model.Standard = row["Standard"].ToString();
                    }
                    if (row["RegistNo"] != null)
                    {
                        model.RegistNo = row["RegistNo"].ToString();
                    }
                    if (row["RegistDate"] != null && row["RegistDate"].ToString() != "")
                    {
                        model.RegistDate = DateTime.Parse(row["RegistDate"].ToString());
                    }
                    if (row["RegistNoInvalidDate"] != null && row["RegistNoInvalidDate"].ToString() != "")
                    {
                        model.RegistNoInvalidDate = DateTime.Parse(row["RegistNoInvalidDate"].ToString());
                    }
                    if (row["Purpose"] != null)
                    {
                        model.Purpose = row["Purpose"].ToString();
                    }
                    if (row["Constitute"] != null)
                    {
                        model.Constitute = row["Constitute"].ToString();
                    }
                    if (row["ZX1"] != null)
                    {
                        model.ZX1 = row["ZX1"].ToString();
                    }
                    if (row["ZX2"] != null)
                    {
                        model.ZX2 = row["ZX2"].ToString();
                    }
                    //ZX3存CS的货品单位ID
                    if (row["ZX3"] != null)
                    {
                        model.ZX3 = row["ZX3"].ToString();
                    }
                    if (row["BarCodeMgr"] != null && row["BarCodeMgr"].ToString() != "")
                    {
                        model.BarCodeMgr = int.Parse(row["BarCodeMgr"].ToString());
                    }
                    if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(row["DispOrder"].ToString());
                    }
                    if (row["Visible"] != null && row["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(row["Visible"].ToString());
                    }
                    if (row["SuitableType"] != null)
                    {
                        model.SuitableType = row["SuitableType"].ToString();
                    }
                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
        /// <summary>
        /// 获得CS的ReaEquipReagentLink数据列表
        /// </summary>
        public DataSet GetReaEquipReagentLinkDataSet(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PGoodsUnit.GoodsUnitID,ReaEquipReagentLink.* FROM V_TestEquipItemDtl as ReaEquipReagentLink left join PGoodsUnit on PGoodsUnit.GoodsID=ReaEquipReagentLink.GoodsID ");
            string strWhere1 = "TestEquipItemID>0 and TestEquipID>0 and TestItemID>0 and ReaEquipReagentLink.GoodsUnitID>0 and ReaEquipReagentLink.GoodsID>0";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            return ds;
        }
        public IList<ReaEquipReagentLink> GetReaEquipReagentLinkList(string strWhere)
        {
            IList<ReaEquipReagentLink> entityList = new List<ReaEquipReagentLink>();
            DataSet ds = GetReaEquipReagentLinkDataSet(strWhere);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ReaEquipReagentLink model = GetReaEquipReagentLink(row);
                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
        public ReaEquipReagentLink GetReaEquipReagentLink(DataRow row)
        {
            ReaEquipReagentLink model = new ReaEquipReagentLink();
            if (row["TestEquipItemID"] != null && row["TestEquipItemID"].ToString() != "")
            {
                model.Memo = row["TestEquipItemID"].ToString();
            }
            if (row["TestEquipID"] != null && row["TestEquipID"].ToString() != "")
            {
                model.TestEquipID = long.Parse(row["TestEquipID"].ToString());
            }
            if (row["GoodsUnitID"] != null && row["GoodsUnitID"].ToString() != "")
            {
                model.GoodsID = long.Parse(row["GoodsUnitID"].ToString());
                //model.GoodsUnitID = long.Parse(row["GoodsUnitID"].ToString());
            }
            model.Visible = 1;
            return model;
        }
        /// <summary>
        /// 获得CS的ReaGoodsOrgLink数据列表
        /// </summary>
        public DataSet GetReaGoodsOrgLinkDataSet(string strWhere)
        {
            IList<ReaGoodsOrgLink> entityList = new List<ReaGoodsOrgLink>();
            StringBuilder strSql = new StringBuilder();
            //供应商货品关系(无大小包装单位关系)
            strSql.Append("select DISTINCT PGoodsUnit.GoodsUnitID,PGoods.BarCodeMgr as BarCodeType, PGoods.ProdGoodsNo, ReaGoodsOrgLink.* FROM PCompanyLabGoods as ReaGoodsOrgLink left join PGoods on ReaGoodsOrgLink.GoodsID=PGoods.GoodsID left join PGoodsUnit on PGoodsUnit.GoodsID=ReaGoodsOrgLink.GoodsID ");
            string strWhere1 = "CompanyLabGoodsID>0 and ReaGoodsOrgLink.GoodsID>0 and CompanyID>0 and GoodsUnitID>0";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CompanyLabGoodsID,PGoodsUnit.GoodsUnitID ");

            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            return ds;
        }
        public IList<ReaGoodsOrgLink> GetReaGoodsOrgLinkList(string strWhere)
        {
            IList<ReaGoodsOrgLink> entityList = new List<ReaGoodsOrgLink>();
            DataSet ds = GetReaGoodsOrgLinkDataSet(strWhere);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ReaGoodsOrgLink model = GetReaGoodsOrgLink(row);
                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
        public ReaGoodsOrgLink GetReaGoodsOrgLink(DataRow row)
        {
            ReaGoodsOrgLink model = new ReaGoodsOrgLink();
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            if (row["CompanyGoodsCode"] != null)
            {
                model.CenOrgGoodsNo = row["CompanyGoodsCode"].ToString();
            }
            //CS以货品的单位ID作为BS的GoodsID
            if (row["GoodsUnitID"] != null && row["GoodsUnitID"].ToString() != "")
            {
                ReaGoods reaGoods = new ReaGoods();
                reaGoods.Id = long.Parse(row["GoodsUnitID"].ToString());
                model.ReaGoods = reaGoods;
                if (model.ReaGoods.DataTimeStamp == null)
                    model.ReaGoods.DataTimeStamp = dataTimeStamp;
            }
            else
            {
                model.ReaGoods = null;
            }
            if (row["CompanyID"] != null && row["CompanyID"].ToString() != "")
            {
                ReaCenOrg cenOrg = new ReaCenOrg();
                cenOrg.Id = long.Parse(row["CompanyID"].ToString());
                model.CenOrg = cenOrg;
                if (model.CenOrg.DataTimeStamp == null)
                    model.CenOrg.DataTimeStamp = dataTimeStamp;
            }
            else
            {
                model.CenOrg = null;
            }
            if (row["Price"] != null && row["Price"].ToString() != "")
            {
                model.Price = double.Parse(row["Price"].ToString());
            }
            else
            {
                model.Price = 0;
            }
            if (row["BarCodeType"] != null && row["BarCodeType"].ToString() != "")
            {
                model.BarCodeType = int.Parse(row["BarCodeType"].ToString());
            }
            if (row["ProdGoodsNo"] != null)
            {
                model.ProdGoodsNo = row["ProdGoodsNo"].ToString();
            }
            if (row["CompanyGoodsCode"] != null)
            {
                model.CenOrgGoodsNo = row["CompanyGoodsCode"].ToString();
            }
            if (row["BiddingNo"] != null)
            {
                model.BiddingNo = row["BiddingNo"].ToString();
            }
            if (row["ZX1"] != null)
            {
                model.ZX1 = row["ZX1"].ToString();
            }
            if (row["ZX2"] != null)
            {
                model.ZX2 = row["ZX2"].ToString();
            }
            if (row["ZX3"] != null)
            {
                model.ZX3 = row["ZX3"].ToString();
            }
            if (row["Memo"] != null)
            {
                model.Memo = row["Memo"].ToString();
            }
            return model;
        }
        /// <summary>
        /// 获得CS的ReaDeptGoods数据列表
        /// </summary>
        public DataSet GetReaDeptGoodsDataSet(string strWhere)
        {
            IList<ReaDeptGoods> entityList = new List<ReaDeptGoods>();
            StringBuilder strSql = new StringBuilder();
            //供应商货品关系(无大小包装单位关系)
            strSql.Append("select DISTINCT ReaDeptGoods.DeptID,ReaDeptGoods.DeptName,PGoodsUnit.GoodsUnitID,ReaDeptGoods.GoodsID,GoodsName,ReaDeptGoods.Visible from V_Dept_TestEquipItemDtl as ReaDeptGoods left join PGoodsUnit on PGoodsUnit.GoodsID=ReaDeptGoods.GoodsID ");
            string strWhere1 = "ReaDeptGoods.DeptID>0 and PGoodsUnit.GoodsUnitID>0 and ReaDeptGoods.GoodsID>0";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by ReaDeptGoods.DeptID,ReaDeptGoods.GoodsID ");

            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            return ds;
        }
        /// <summary>
        /// 获得CS的ReaDeptGoods数据列表
        /// </summary>
        public IList<ReaDeptGoods> GetReaDeptGoodsList(string strWhere)
        {
            IList<ReaDeptGoods> entityList = new List<ReaDeptGoods>();
            DataSet ds = GetReaDeptGoodsDataSet(strWhere);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ReaDeptGoods model = GetReaDeptGoods(row);

                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
        public ReaDeptGoods GetReaDeptGoods(DataRow row)
        {
            ReaDeptGoods model = new ReaDeptGoods();
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            if (row["DeptID"] != null && row["DeptID"].ToString() != "")
            {
                HRDept dept = new HRDept();
                dept.Id = long.Parse(row["DeptID"].ToString());
                dept.CName = row["DeptName"].ToString();
                model.DeptID = dept.Id;
                model.DeptName = dept.CName;

                //model.HRDept = dept;
                //if (model.HRDept.DataTimeStamp == null)
                //    model.HRDept.DataTimeStamp = dataTimeStamp;
            }
            else
            {
                //model.HRDept = null;
                model.DeptID = null;
                model.DeptName = "";
            }
            //CS以货品的单位ID作为BS的GoodsID
            if (row["GoodsUnitID"] != null && row["GoodsUnitID"].ToString() != "")
            {
                ReaGoods reaGoods = new ReaGoods();
                reaGoods.Id = long.Parse(row["GoodsUnitID"].ToString());
                reaGoods.CName = row["GoodsName"].ToString();
                model.ReaGoods = reaGoods;
                if (model.ReaGoods.DataTimeStamp == null)
                    model.ReaGoods.DataTimeStamp = dataTimeStamp;
            }
            else
            {
                model.ReaGoods = null;
            }
            if (row["Visible"] != null && row["Visible"].ToString() != "")
            {
                model.Visible = int.Parse(row["Visible"].ToString()) == 1 ? true : false;
            }
            return model;
        }
        /// <summary>
        /// 获得CS的ReaBmsQtyDtl数据列表
        /// </summary>
        public DataSet GetReaBmsQtyDtlDataSet(string strWhere)
        {
            IList<ReaBmsQtyDtl> entityList = new List<ReaBmsQtyDtl>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ReaBmsQtyDtl.* from V_BmsQtyDtl as ReaBmsQtyDtl ");
            string strWhere1 = " SumFlag = 1 and isnull(Outflag, 0)!=1  and isnull(GoodsQty, 0) > 0 and GoodsUnitID>0";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CompanyID,GoodsID,LotNo,StorageID,PlaceID,GoodsUnitID ");
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            return ds;
        }
        /// <summary>
        /// 获得CS的ReaBmsQtyDtl数据列表
        /// </summary>
        public IList<ReaBmsQtyDtl> GetReaBmsQtyDtlList(string strWhere)
        {
            IList<ReaBmsQtyDtl> entityList = new List<ReaBmsQtyDtl>();
            DataSet ds = GetReaBmsQtyDtlDataSet(strWhere);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ReaBmsQtyDtl model = GetReaBmsQtyDtl(row);

                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
        public ReaBmsQtyDtl GetReaBmsQtyDtl(DataRow row)
        {
            ReaBmsQtyDtl model = new ReaBmsQtyDtl();
            model.PQtyDtlID = model.Id;
            if (row["InDtlNo"] != null)
            {
                model.CSInDtlNo = row["InDtlNo"].ToString();
            }
            if (row["QtyDtlNo"] != null)
            {
                model.CSQtyDtlNo = row["QtyDtlNo"].ToString();
            }
            if (row["CompanyID"] != null && row["CompanyID"].ToString() != "")
            {
                model.ReaCompanyID = long.Parse(row["CompanyID"].ToString());
            }
            if (row["CompanyName"] != null)
            {
                model.CompanyName = row["CompanyName"].ToString();
            }
            //CS以货品的单位ID作为BS的GoodsID
            if (row["GoodsUnitID"] != null && row["GoodsUnitID"].ToString() != "")
            {
                model.GoodsID = long.Parse(row["GoodsID"].ToString());
                model.GoodsUnitID = long.Parse(row["GoodsUnitID"].ToString());
            }
            if (row["GoodsName"] != null)
            {
                model.GoodsName = row["GoodsName"].ToString();
            }
            if (row["StorageID"] != null && row["StorageID"].ToString() != "")
            {
                model.StorageID = long.Parse(row["StorageID"].ToString());
            }
            if (row["StorageName"] != null)
            {
                model.StorageName = row["StorageName"].ToString();
            }
            if (row["PlaceID"] != null && row["PlaceID"].ToString() != "")
            {
                model.PlaceID = long.Parse(row["PlaceID"].ToString());
            }
            if (row["PlaceName"] != null)
            {
                model.PlaceName = row["PlaceName"].ToString();
            }
            if (row["GoodsUnit"] != null)
            {
                model.GoodsUnit = row["GoodsUnit"].ToString();
            }
            if (row["UnitMemo"] != null)
            {
                model.UnitMemo = row["UnitMemo"].ToString();
            }
            if (row["ProdDate"] != null && row["ProdDate"].ToString() != "")
            {
                model.ProdDate = DateTime.Parse(row["ProdDate"].ToString());
                if (model.ProdDate.HasValue && model.ProdDate.Value.ToString("yyyy-MM-dd") == "1900-01-01")
                    model.ProdDate = null;
            }
            else
            {
                model.ProdDate = null;
            }
            if (row["InvalidDate"] != null && row["InvalidDate"].ToString() != "")
            {
                DateTime invalidDate = DateTime.Parse(row["InvalidDate"].ToString());//  9999-09-09 00:00:00.000
                if (invalidDate.Year > 1911 && invalidDate.Year < 2450)
                    model.InvalidDate = invalidDate;
            }
            if (row["ProdGoodsNo"] != null)
            {
                model.ProdGoodsNo = row["ProdGoodsNo"].ToString();
            }
            if (row["BarCodeMgr"] != null && row["BarCodeMgr"].ToString() != "")
            {
                model.BarCodeType = int.Parse(row["BarCodeMgr"].ToString());
            }
            if (row["LotSerial"] != null)
            {
                model.LotSerial = row["LotSerial"].ToString();
            }
            //临时存CS条码值
            if (row["SerialNo"] != null)
            {
                model.Memo = row["SerialNo"].ToString();
                model.GoodsSerial = row["SerialNo"].ToString();
            }
            if (row["LotNo"] != null)
            {
                model.LotNo = row["LotNo"].ToString();
            }
            if (row["GoodsQty"] != null && row["GoodsQty"].ToString() != "")
            {
                model.GoodsQty = double.Parse(row["GoodsQty"].ToString());
            }
            if (row["Price"] != null && row["Price"].ToString() != "")
            {
                model.Price = double.Parse(row["Price"].ToString());
            }
            if (row["SumTotal"] != null && row["SumTotal"].ToString() != "")
            {
                model.SumTotal = double.Parse(row["SumTotal"].ToString());
            }
            if (!model.SumTotal.HasValue || model.SumTotal <= 0)
            {
                model.SumTotal = model.GoodsQty * model.Price;
            }
            if (row["TaxRate"] != null && row["TaxRate"].ToString() != "")
            {
                model.TaxRate = double.Parse(row["TaxRate"].ToString());
            }
            if (row["OutFlag"] != null && row["OutFlag"].ToString() != "")
            {
                model.OutFlag = int.Parse(row["OutFlag"].ToString());
            }
            if (row["SumFlag"] != null && row["SumFlag"].ToString() != "")
            {
                model.SumFlag = int.Parse(row["SumFlag"].ToString());
            }
            if (row["IOFlag"] != null && row["IOFlag"].ToString() != "")
            {
                model.IOFlag = int.Parse(row["IOFlag"].ToString());
            }
            if (row["ZX1"] != null)
            {
                model.ZX1 = row["ZX1"].ToString();
            }
            if (row["ZX2"] != null)
            {
                model.ZX2 = row["ZX2"].ToString();
            }
            if (row["ZX3"] != null)
            {
                model.ZX3 = row["ZX3"].ToString();
            }
            if (row["LotSerial"] != null)
            {
                model.LotSerial = row["LotSerial"].ToString();
            }
            return model;
        }
        /// <summary>
        /// 获得CS的ReaBmsSerial数据列表
        /// </summary>
        public IList<ReaBmsSerial> GetReaBmsSerialList(string strWhere)
        {
            IList<ReaBmsSerial> entityList = new List<ReaBmsSerial>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  * FROM BmsSerial");
            string strWhere1 = "BMsType='BmsInDtl'";
            if (string.IsNullOrEmpty(strWhere))
                strWhere = strWhere1;
            else
                strWhere = " and " + strWhere1;
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ReaBmsSerial model = new ReaBmsSerial();
                    if (row["BmsType"] != null && row["BmsType"].ToString() != "")
                    {
                        model.BmsType = row["BmsType"].ToString();
                    }
                    if (row["MaxID"] != null && row["MaxID"].ToString() != "")
                    {
                        model.CurBarCode = long.Parse(row["MaxID"].ToString());
                    }
                    if (row["BmsDate"] != null && row["BmsDate"].ToString() != "")
                    {
                        model.DataUpdateTime = DateTime.Parse(row["BmsDate"].ToString());
                    }
                    entityList.Add(model);
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }
    }
}
