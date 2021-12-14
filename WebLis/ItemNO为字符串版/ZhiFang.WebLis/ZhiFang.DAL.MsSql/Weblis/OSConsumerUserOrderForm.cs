using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.Model;
using ZhiFang.Model.UiModel;

namespace ZhiFang.DAL.MsSql.Weblis
{
    public class OSConsumerUserOrderForm: BaseDALLisDB,IDOSConsumerUserOrderForm
    {
        public OSConsumerUserOrderForm(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public OSConsumerUserOrderForm()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        public DataTable GetOSUserOrderFormByPayCode(string PayCode)
        {
            DataSet ds= DbHelperSQL.ExecuteDataSet("select * from OS_UserOrderForm where PayCode='" + PayCode + "'");
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows!=null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public DataTable GetBDoctorAccountByDoctorAccountID(long DoctorAccountID)
        {
            DataSet ds = DbHelperSQL.ExecuteDataSet("select * from B_DoctorAccount where DoctorAccountID="+ DoctorAccountID);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public DataTable GetBWeiXinAccountByWeiXinUserID(long WeiXinUserID)
        {
            DataSet ds = DbHelperSQL.ExecuteDataSet("select * from B_WeiXinAccount where WeiXinUserID=" + WeiXinUserID);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public DataTable GetOSDoctorOrderFormByDOFID(long DOFID)
        {
            DataSet ds = DbHelperSQL.ExecuteDataSet("select * from OS_DoctorOrderForm where DOFID=" + DOFID);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public DataTable GetOSUserOrderItemByUOFID(long UOFID)
        {
            DataSet ds = DbHelperSQL.ExecuteDataSet("select * from OS_UserOrderItem where UOFID=" + UOFID);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public bool SaveOSUserConsumerForm(long nRequestFormNo, DataRow DrOSUserOrderForm, NrequestCombiItemBarCodeEntity jsonentity)
        {
            try
            {
                OSUserConsumerForm osucf = new OSUserConsumerForm();
                DateTime dtnow = DateTime.Now;
                OSUserOrderForm osuof = this.DataRowToModelOSUserOrderForm(DrOSUserOrderForm);
                long osucfid = ZhiFang.Tools.GUIDHelp.GetGUIDLong();
                string OSUserConsumerFormCode = dtnow.ToString("yyyyMMdd") + Tools.GUIDHelp.GetGUIDString().Substring(0, 5) + dtnow.ToString("fff") + dtnow.ToString("HHmmss");
                osucf.AreaID = osuof.AreaID;
                osucf.HospitalID = osuof.HospitalID;
                osucf.OS_UserConsumerFormID = osucfid;
                osucf.OS_UserConsumerFormCode = OSUserConsumerFormCode;
                osucf.NRQFID = nRequestFormNo;
                osucf.DOFID = osuof.DOFID;
                osucf.DoctorAccountID = osuof.DoctorAccountID;
                osucf.WeiXinUserID = osuof.WeiXinUserID;
                osucf.DoctorOpenID = osuof.DoctorOpenID;
                osucf.DoctorName = osuof.DoctorName;
                osucf.MarketPrice = osuof.MarketPrice;
                osucf.GreatMasterPrice = osuof.GreatMasterPrice;
                osucf.DiscountPrice = osuof.DiscountPrice;
                osucf.Discount = osuof.Discount;
                osucf.Price = osuof.Price;
                osucf.AdvicePrice = osuof.AdvicePrice;
                osucf.UserAccountID = osuof.UserAccountID;
                osucf.UserWeiXinUserID = osuof.UserWeiXinUserID;
                osucf.UserName = osuof.UserName;
                osucf.UserOpenID = osuof.UserOpenID;
                osucf.Status = 1;//Status：1消费成功、2消费失败、3已结算。
                osucf.PayCode = osuof.PayCode;
                osucf.Memo = osuof.Memo;
                osucf.IsUse = true;
                osucf.OrgID = long.Parse(jsonentity.NrequestForm.WebLisSourceOrgID);
                osucf.WeblisOrgID = jsonentity.NrequestForm.WebLisSourceOrgID;
                osucf.WeblisOrgName = jsonentity.NrequestForm.WebLisSourceOrgName;
                osucf.EmpID = (jsonentity.NrequestForm.CollecterID != null && jsonentity.NrequestForm.CollecterID.Trim() != "") ? long.Parse(jsonentity.NrequestForm.CollecterID) : 0;
                osucf.EmpName = jsonentity.NrequestForm.CollecterName;
                osucf.DataAddTime = dtnow;
                this.AddOSUserConsumerForm(osucf);

                DataSet dsosuoi = DbHelperSQL.ExecuteDataSet("select * from OS_UserOrderItem where UOFID=" + osuof.UOFID);

                if (dsosuoi != null && dsosuoi.Tables != null && dsosuoi.Tables.Count > 0 && dsosuoi.Tables[0].Rows != null && dsosuoi.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsosuoi.Tables[0].Rows.Count; i++)
                    {
                        OSUserOrderItem osuoi = DataRowToModelOSUserOrderItem(dsosuoi.Tables[0].Rows[i]);
                        OSUserConsumerItem osuci = new OSUserConsumerItem();
                        osuci.AreaID = osuoi.AreaID;
                        osuci.Discount = osuoi.Discount;
                        osuci.DiscountPrice = osuoi.DiscountPrice;
                        osuci.GreatMasterPrice = osuoi.GreatMasterPrice;
                        osuci.HospitalID = osuoi.HospitalID;
                        osuci.IsUse = osuoi.IsUse;
                        osuci.ItemID = osuoi.ItemID;
                        osuci.ItemNo = osuoi.ItemNo;
                        osuci.ItemCName = osuoi.ItemCName;
                        osuci.MarketPrice = osuoi.MarketPrice;
                        osuci.Memo = osuoi.Memo;
                        osuci.OS_UserConsumerFormID = osucfid;
                        osuci.OS_UserConsumerItemID = Tools.GUIDHelp.GetGUIDLong();
                        osuci.RecommendationItemProductID = osuoi.RecommendationItemProductID;
                        osuci.UOIID = osuoi.UOIID;
                        osuci.DataAddTime = dtnow;
                        AddOSUserConsumerItem(osuci);
                    }
                }
                DbHelperSQL.ExecuteNonQuery(" update  OS_UserOrderForm set status=4 , ConsumerFinishedTime='" + dtnow.ToString("yyyy-MM-dd HH:mm:ss") + "' ,OS_UserConsumerFormID=" + osucfid + ",OS_UserConsumerFormCode='" + OSUserConsumerFormCode + "' where UOFID=" + osuof.UOFID);
                return true;
            }
            catch(Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("SaveOSUserConsumerForm.异常：" + e.ToString());
                return false;
            }
            
        }
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public bool AddOSUserConsumerForm(Model.OSUserConsumerForm model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.AreaID != null)
            {
                strSql1.Append("AreaID,");
                strSql2.Append("" + model.AreaID + ",");
            }
            if (model.HospitalID != null)
            {
                strSql1.Append("HospitalID,");
                strSql2.Append("" + model.HospitalID + ",");
            }
            if (model.OS_UserConsumerFormID != null)
            {
                strSql1.Append("OS_UserConsumerFormID,");
                strSql2.Append("" + model.OS_UserConsumerFormID + ",");
            }
            if (model.OS_UserConsumerFormCode != null)
            {
                strSql1.Append("OS_UserConsumerFormCode,");
                strSql2.Append("'" + model.OS_UserConsumerFormCode + "',");
            }
            if (model.NRQFID != null)
            {
                strSql1.Append("NRQFID,");
                strSql2.Append("" + model.NRQFID + ",");
            }
            if (model.DOFID != null)
            {
                strSql1.Append("DOFID,");
                strSql2.Append("" + model.DOFID + ",");
            }
            if (model.DoctorAccountID != null)
            {
                strSql1.Append("DoctorAccountID,");
                strSql2.Append("" + model.DoctorAccountID + ",");
            }
            if (model.WeiXinUserID != null)
            {
                strSql1.Append("WeiXinUserID,");
                strSql2.Append("" + model.WeiXinUserID + ",");
            }
            if (model.DoctorOpenID != null)
            {
                strSql1.Append("DoctorOpenID,");
                strSql2.Append("'" + model.DoctorOpenID + "',");
            }
            if (model.DoctorName != null)
            {
                strSql1.Append("DoctorName,");
                strSql2.Append("'" + model.DoctorName + "',");
            }
            if (model.DataTimeStamp != null)
            {
                strSql1.Append("DataTimeStamp,");
                strSql2.Append("" + model.DataTimeStamp + ",");
            }
            if (model.MarketPrice != null)
            {
                strSql1.Append("MarketPrice,");
                strSql2.Append("" + model.MarketPrice + ",");
            }
            if (model.GreatMasterPrice != null)
            {
                strSql1.Append("GreatMasterPrice,");
                strSql2.Append("" + model.GreatMasterPrice + ",");
            }
            if (model.DiscountPrice != null)
            {
                strSql1.Append("DiscountPrice,");
                strSql2.Append("" + model.DiscountPrice + ",");
            }
            if (model.Discount != null)
            {
                strSql1.Append("Discount,");
                strSql2.Append("" + model.Discount + ",");
            }
            if (model.Price != null)
            {
                strSql1.Append("Price,");
                strSql2.Append("" + model.Price + ",");
            }
            if (model.AdvicePrice != null)
            {
                strSql1.Append("AdvicePrice,");
                strSql2.Append("" + model.AdvicePrice + ",");
            }
            if (model.UserAccountID != null)
            {
                strSql1.Append("UserAccountID,");
                strSql2.Append("" + model.UserAccountID + ",");
            }
            if (model.UserWeiXinUserID != null)
            {
                strSql1.Append("UserWeiXinUserID,");
                strSql2.Append("" + model.UserWeiXinUserID + ",");
            }
            if (model.OS_DoctorBonusID != null)
            {
                strSql1.Append("OS_DoctorBonusID,");
                strSql2.Append("" + model.OS_DoctorBonusID + ",");
            }
            if (model.UserName != null)
            {
                strSql1.Append("UserName,");
                strSql2.Append("'" + model.UserName + "',");
            }
            if (model.UserOpenID != null)
            {
                strSql1.Append("UserOpenID,");
                strSql2.Append("'" + model.UserOpenID + "',");
            }
            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("" + model.Status + ",");
            }
            if (model.PayCode != null)
            {
                strSql1.Append("PayCode,");
                strSql2.Append("'" + model.PayCode + "',");
            }
            if (model.OrgID != null)
            {
                strSql1.Append("OrgID,");
                strSql2.Append("" + model.OrgID + ",");
            }
            if (model.WeblisOrgID != null)
            {
                strSql1.Append("WeblisOrgID,");
                strSql2.Append("'" + model.WeblisOrgID + "',");
            }
            if (model.WeblisOrgName != null)
            {
                strSql1.Append("WeblisOrgName,");
                strSql2.Append("'" + model.WeblisOrgName + "',");
            }
            if (model.EmpID != null)
            {
                strSql1.Append("EmpID,");
                strSql2.Append("" + model.EmpID + ",");
            }
            if (model.EmpName != null)
            {
                strSql1.Append("EmpName,");
                strSql2.Append("'" + model.EmpName + "',");
            }
            if (model.Memo != null)
            {
                strSql1.Append("Memo,");
                strSql2.Append("'" + model.Memo + "',");
            }
            if (model.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("" + model.DispOrder + ",");
            }
            if (model.IsUse != null)
            {
                strSql1.Append("IsUse,");
                strSql2.Append("" + (model.IsUse ? 1 : 0) + ",");
            }
            if (model.DataAddTime != null)
            {
                strSql1.Append("DataAddTime,");
                strSql2.Append("'" + model.DataAddTime + "',");
            }
            if (model.DataUpdateTime != null)
            {
                strSql1.Append("DataUpdateTime,");
                strSql2.Append("'" + model.DataUpdateTime + "',");
            }
            strSql.Append("insert into OS_UserConsumerForm(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateOSUserConsumerForm(Model.OSUserConsumerForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OS_UserConsumerForm set ");
            if (model.AreaID != null)
            {
                strSql.Append("AreaID=" + model.AreaID + ",");
            }
            if (model.HospitalID != null)
            {
                strSql.Append("HospitalID=" + model.HospitalID + ",");
            }
            if (model.OS_UserConsumerFormCode != null)
            {
                strSql.Append("OS_UserConsumerFormCode='" + model.OS_UserConsumerFormCode + "',");
            }
            else
            {
                strSql.Append("OS_UserConsumerFormCode= null ,");
            }
            if (model.NRQFID != null)
            {
                strSql.Append("NRQFID=" + model.NRQFID + ",");
            }
            else
            {
                strSql.Append("NRQFID= null ,");
            }
            if (model.DOFID != null)
            {
                strSql.Append("DOFID=" + model.DOFID + ",");
            }
            else
            {
                strSql.Append("DOFID= null ,");
            }
            if (model.DoctorAccountID != null)
            {
                strSql.Append("DoctorAccountID=" + model.DoctorAccountID + ",");
            }
            else
            {
                strSql.Append("DoctorAccountID= null ,");
            }
            if (model.WeiXinUserID != null)
            {
                strSql.Append("WeiXinUserID=" + model.WeiXinUserID + ",");
            }
            else
            {
                strSql.Append("WeiXinUserID= null ,");
            }
            if (model.DoctorOpenID != null)
            {
                strSql.Append("DoctorOpenID='" + model.DoctorOpenID + "',");
            }
            else
            {
                strSql.Append("DoctorOpenID= null ,");
            }
            if (model.DoctorName != null)
            {
                strSql.Append("DoctorName='" + model.DoctorName + "',");
            }
            else
            {
                strSql.Append("DoctorName= null ,");
            }
            if (model.MarketPrice != null)
            {
                strSql.Append("MarketPrice=" + model.MarketPrice + ",");
            }
            else
            {
                strSql.Append("MarketPrice= null ,");
            }
            if (model.GreatMasterPrice != null)
            {
                strSql.Append("GreatMasterPrice=" + model.GreatMasterPrice + ",");
            }
            else
            {
                strSql.Append("GreatMasterPrice= null ,");
            }
            if (model.DiscountPrice != null)
            {
                strSql.Append("DiscountPrice=" + model.DiscountPrice + ",");
            }
            else
            {
                strSql.Append("DiscountPrice= null ,");
            }
            if (model.Discount != null)
            {
                strSql.Append("Discount=" + model.Discount + ",");
            }
            else
            {
                strSql.Append("Discount= null ,");
            }
            if (model.Price != null)
            {
                strSql.Append("Price=" + model.Price + ",");
            }
            else
            {
                strSql.Append("Price= null ,");
            }
            if (model.AdvicePrice != null)
            {
                strSql.Append("AdvicePrice=" + model.AdvicePrice + ",");
            }
            else
            {
                strSql.Append("AdvicePrice= null ,");
            }
            if (model.UserAccountID != null)
            {
                strSql.Append("UserAccountID=" + model.UserAccountID + ",");
            }
            else
            {
                strSql.Append("UserAccountID= null ,");
            }
            if (model.UserWeiXinUserID != null)
            {
                strSql.Append("UserWeiXinUserID=" + model.UserWeiXinUserID + ",");
            }
            else
            {
                strSql.Append("UserWeiXinUserID= null ,");
            }
            if (model.OS_DoctorBonusID != null)
            {
                strSql.Append("OS_DoctorBonusID=" + model.OS_DoctorBonusID + ",");
            }
            else
            {
                strSql.Append("OS_DoctorBonusID= null ,");
            }
            if (model.UserName != null)
            {
                strSql.Append("UserName='" + model.UserName + "',");
            }
            else
            {
                strSql.Append("UserName= null ,");
            }
            if (model.UserOpenID != null)
            {
                strSql.Append("UserOpenID='" + model.UserOpenID + "',");
            }
            else
            {
                strSql.Append("UserOpenID= null ,");
            }
            if (model.Status != null)
            {
                strSql.Append("Status=" + model.Status + ",");
            }
            else
            {
                strSql.Append("Status= null ,");
            }
            if (model.PayCode != null)
            {
                strSql.Append("PayCode='" + model.PayCode + "',");
            }
            else
            {
                strSql.Append("PayCode= null ,");
            }
            if (model.OrgID != null)
            {
                strSql.Append("OrgID=" + model.OrgID + ",");
            }
            else
            {
                strSql.Append("OrgID= null ,");
            }
            if (model.WeblisOrgID != null)
            {
                strSql.Append("WeblisOrgID='" + model.WeblisOrgID + "',");
            }
            else
            {
                strSql.Append("WeblisOrgID= null ,");
            }
            if (model.WeblisOrgName != null)
            {
                strSql.Append("WeblisOrgName='" + model.WeblisOrgName + "',");
            }
            else
            {
                strSql.Append("WeblisOrgName= null ,");
            }
            if (model.EmpID != null)
            {
                strSql.Append("EmpID=" + model.EmpID + ",");
            }
            else
            {
                strSql.Append("EmpID= null ,");
            }
            if (model.EmpName != null)
            {
                strSql.Append("EmpName='" + model.EmpName + "',");
            }
            else
            {
                strSql.Append("EmpName= null ,");
            }
            if (model.Memo != null)
            {
                strSql.Append("Memo='" + model.Memo + "',");
            }
            else
            {
                strSql.Append("Memo= null ,");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            else
            {
                strSql.Append("DispOrder= null ,");
            }
            if (model.IsUse != null)
            {
                strSql.Append("IsUse=" + (model.IsUse ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("IsUse= null ,");
            }
            if (model.DataAddTime != null)
            {
                strSql.Append("DataAddTime='" + model.DataAddTime + "',");
            }
            else
            {
                strSql.Append("DataAddTime= null ,");
            }
            if (model.DataUpdateTime != null)
            {
                strSql.Append("DataUpdateTime='" + model.DataUpdateTime + "',");
            }
            else
            {
                strSql.Append("DataUpdateTime= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where OS_UserConsumerFormID=" + model.OS_UserConsumerFormID + " ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long OSUserConsumerFormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OS_UserConsumerForm ");
            strSql.Append(" where OS_UserConsumerFormID=" + OSUserConsumerFormID + " ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Model.OSUserOrderForm DataRowToModelOSUserOrderForm(DataRow row)
        {
            Model.OSUserOrderForm model = new Model.OSUserOrderForm();
            if (row != null)
            {
                if (row["AreaID"] != null && row["AreaID"].ToString() != "")
                {
                    model.AreaID = long.Parse(row["AreaID"].ToString());
                }
                if (row["HospitalID"] != null && row["HospitalID"].ToString() != "")
                {
                    model.HospitalID = long.Parse(row["HospitalID"].ToString());
                }
                if (row["UOFID"] != null && row["UOFID"].ToString() != "")
                {
                    model.UOFID = long.Parse(row["UOFID"].ToString());
                }
                if (row["UOFCode"] != null)
                {
                    model.UOFCode = row["UOFCode"].ToString();
                }
                if (row["DOFID"] != null && row["DOFID"].ToString() != "")
                {
                    model.DOFID = long.Parse(row["DOFID"].ToString());
                }
                if (row["DoctorAccountID"] != null && row["DoctorAccountID"].ToString() != "")
                {
                    model.DoctorAccountID = long.Parse(row["DoctorAccountID"].ToString());
                }
                if (row["OS_UserConsumerFormID"] != null && row["OS_UserConsumerFormID"].ToString() != "")
                {
                    model.OS_UserConsumerFormID = long.Parse(row["OS_UserConsumerFormID"].ToString());
                }
                if (row["OS_UserConsumerFormCode"] != null)
                {
                    model.OS_UserConsumerFormCode = row["OS_UserConsumerFormCode"].ToString();
                }
                if (row["WeiXinUserID"] != null && row["WeiXinUserID"].ToString() != "")
                {
                    model.WeiXinUserID = long.Parse(row["WeiXinUserID"].ToString());
                }
                if (row["DoctorOpenID"] != null)
                {
                    model.DoctorOpenID = row["DoctorOpenID"].ToString();
                }
                if (row["DoctorName"] != null)
                {
                    model.DoctorName = row["DoctorName"].ToString();
                }
                if (row["UserAccountID"] != null && row["UserAccountID"].ToString() != "")
                {
                    model.UserAccountID = long.Parse(row["UserAccountID"].ToString());
                }
                if (row["UserWeiXinUserID"] != null && row["UserWeiXinUserID"].ToString() != "")
                {
                    model.UserWeiXinUserID = long.Parse(row["UserWeiXinUserID"].ToString());
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["UserOpenID"] != null)
                {
                    model.UserOpenID = row["UserOpenID"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = long.Parse(row["Status"].ToString());
                }
                if (row["PayCode"] != null)
                {
                    model.PayCode = row["PayCode"].ToString();
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row["IsUse"] != null && row["IsUse"].ToString() != "")
                {
                    if ((row["IsUse"].ToString() == "1") || (row["IsUse"].ToString().ToLower() == "true"))
                    {
                        model.IsUse = true;
                    }
                    else
                    {
                        model.IsUse = false;
                    }
                }
                if (row["DataAddTime"] != null && row["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(row["DataAddTime"].ToString());
                }
                if (row["DataUpdateTime"] != null && row["DataUpdateTime"].ToString() != "")
                {
                    model.DataUpdateTime = DateTime.Parse(row["DataUpdateTime"].ToString());
                }
                if (row["MarketPrice"] != null && row["MarketPrice"].ToString() != "")
                {
                    model.MarketPrice = decimal.Parse(row["MarketPrice"].ToString());
                }
                if (row["GreatMasterPrice"] != null && row["GreatMasterPrice"].ToString() != "")
                {
                    model.GreatMasterPrice = decimal.Parse(row["GreatMasterPrice"].ToString());
                }
                if (row["DiscountPrice"] != null && row["DiscountPrice"].ToString() != "")
                {
                    model.DiscountPrice = decimal.Parse(row["DiscountPrice"].ToString());
                }
                if (row["Discount"] != null && row["Discount"].ToString() != "")
                {
                    model.Discount = decimal.Parse(row["Discount"].ToString());
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(row["Price"].ToString());
                }
                if (row["AdvicePrice"] != null && row["AdvicePrice"].ToString() != "")
                {
                    model.AdvicePrice = decimal.Parse(row["AdvicePrice"].ToString());
                }
                if (row["RefundPrice"] != null && row["RefundPrice"].ToString() != "")
                {
                    model.RefundPrice = decimal.Parse(row["RefundPrice"].ToString());
                }
                if (row["PayTime"] != null && row["PayTime"].ToString() != "")
                {
                    model.PayTime = DateTime.Parse(row["PayTime"].ToString());
                }
                if (row["CancelApplyTime"] != null && row["CancelApplyTime"].ToString() != "")
                {
                    model.CancelApplyTime = DateTime.Parse(row["CancelApplyTime"].ToString());
                }
                if (row["CancelFinishedTime"] != null && row["CancelFinishedTime"].ToString() != "")
                {
                    model.CancelFinishedTime = DateTime.Parse(row["CancelFinishedTime"].ToString());
                }
                if (row["ConsumerStartTime"] != null && row["ConsumerStartTime"].ToString() != "")
                {
                    model.ConsumerStartTime = DateTime.Parse(row["ConsumerStartTime"].ToString());
                }
                if (row["ConsumerFinishedTime"] != null && row["ConsumerFinishedTime"].ToString() != "")
                {
                    model.ConsumerFinishedTime = DateTime.Parse(row["ConsumerFinishedTime"].ToString());
                }
                if (row["RefundApplyTime"] != null && row["RefundApplyTime"].ToString() != "")
                {
                    model.RefundApplyTime = DateTime.Parse(row["RefundApplyTime"].ToString());
                }
                if (row["RefundOneReviewManName"] != null)
                {
                    model.RefundOneReviewManName = row["RefundOneReviewManName"].ToString();
                }
                if (row["RefundOneReviewManID"] != null && row["RefundOneReviewManID"].ToString() != "")
                {
                    model.RefundOneReviewManID = long.Parse(row["RefundOneReviewManID"].ToString());
                }
                if (row["RefundOneReviewStartTime"] != null && row["RefundOneReviewStartTime"].ToString() != "")
                {
                    model.RefundOneReviewStartTime = DateTime.Parse(row["RefundOneReviewStartTime"].ToString());
                }
                if (row["RefundOneReviewFinishTime"] != null && row["RefundOneReviewFinishTime"].ToString() != "")
                {
                    model.RefundOneReviewFinishTime = DateTime.Parse(row["RefundOneReviewFinishTime"].ToString());
                }
                if (row["RefundTwoReviewManName"] != null)
                {
                    model.RefundTwoReviewManName = row["RefundTwoReviewManName"].ToString();
                }
                if (row["RefundTwoReviewManID"] != null && row["RefundTwoReviewManID"].ToString() != "")
                {
                    model.RefundTwoReviewManID = long.Parse(row["RefundTwoReviewManID"].ToString());
                }
                if (row["RefundTwoReviewStartTime"] != null && row["RefundTwoReviewStartTime"].ToString() != "")
                {
                    model.RefundTwoReviewStartTime = DateTime.Parse(row["RefundTwoReviewStartTime"].ToString());
                }
                if (row["RefundTwoReviewFinishTime"] != null && row["RefundTwoReviewFinishTime"].ToString() != "")
                {
                    model.RefundTwoReviewFinishTime = DateTime.Parse(row["RefundTwoReviewFinishTime"].ToString());
                }
                if (row["RefundThreeReviewManName"] != null)
                {
                    model.RefundThreeReviewManName = row["RefundThreeReviewManName"].ToString();
                }
                if (row["RefundThreeReviewManID"] != null && row["RefundThreeReviewManID"].ToString() != "")
                {
                    model.RefundThreeReviewManID = long.Parse(row["RefundThreeReviewManID"].ToString());
                }
                if (row["RefundThreeReviewStartTime"] != null && row["RefundThreeReviewStartTime"].ToString() != "")
                {
                    model.RefundThreeReviewStartTime = DateTime.Parse(row["RefundThreeReviewStartTime"].ToString());
                }
                if (row["RefundThreeReviewFinishTime"] != null && row["RefundThreeReviewFinishTime"].ToString() != "")
                {
                    model.RefundThreeReviewFinishTime = DateTime.Parse(row["RefundThreeReviewFinishTime"].ToString());
                }
                if (row["RefundReason"] != null)
                {
                    model.RefundReason = row["RefundReason"].ToString();
                }
                if (row["RefundOneReviewReason"] != null)
                {
                    model.RefundOneReviewReason = row["RefundOneReviewReason"].ToString();
                }
                if (row["RefundTwoReviewReason"] != null)
                {
                    model.RefundTwoReviewReason = row["RefundTwoReviewReason"].ToString();
                }
                if (row["RefundThreeReviewReason"] != null)
                {
                    model.RefundThreeReviewReason = row["RefundThreeReviewReason"].ToString();
                }
                if (row["IsPrePay"] != null && row["IsPrePay"].ToString() != "")
                {
                    if ((row["IsPrePay"].ToString() == "1") || (row["IsPrePay"].ToString().ToLower() == "true"))
                    {
                        model.IsPrePay = true;
                    }
                    else
                    {
                        model.IsPrePay = false;
                    }
                }
                if (row["PrePayId"] != null)
                {
                    model.PrePayId = row["PrePayId"].ToString();
                }
                if (row["PrePayTime"] != null && row["PrePayTime"].ToString() != "")
                {
                    model.PrePayTime = DateTime.Parse(row["PrePayTime"].ToString());
                }
                if (row["PrePayReturnCode"] != null)
                {
                    model.PrePayReturnCode = row["PrePayReturnCode"].ToString();
                }
                if (row["PrePayReturnMsg"] != null)
                {
                    model.PrePayReturnMsg = row["PrePayReturnMsg"].ToString();
                }
                if (row["PrePayErrCode"] != null)
                {
                    model.PrePayErrCode = row["PrePayErrCode"].ToString();
                }
                if (row["PrePayErrName"] != null)
                {
                    model.PrePayErrName = row["PrePayErrName"].ToString();
                }
                if (row["TransactionId"] != null)
                {
                    model.TransactionId = row["TransactionId"].ToString();
                }
                if (row["Column_59"] != null)
                {
                    model.Column_59 = row["Column_59"].ToString();
                }
                if (row["Column_60"] != null)
                {
                    model.Column_60 = row["Column_60"].ToString();
                }
                if (row["Column_61"] != null)
                {
                    model.Column_61 = row["Column_61"].ToString();
                }
                if (row["Column_62"] != null)
                {
                    model.Column_62 = row["Column_62"].ToString();
                }
            }
            return model;
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.OSUserOrderItem DataRowToModelOSUserOrderItem(DataRow row)
        {
            Model.OSUserOrderItem model = new Model.OSUserOrderItem();
            if (row != null)
            {
                if (row["AreaID"] != null && row["AreaID"].ToString() != "")
                {
                    model.AreaID = long.Parse(row["AreaID"].ToString());
                }
                if (row["HospitalID"] != null && row["HospitalID"].ToString() != "")
                {
                    model.HospitalID = long.Parse(row["HospitalID"].ToString());
                }
                if (row["UOIID"] != null && row["UOIID"].ToString() != "")
                {
                    model.UOIID = long.Parse(row["UOIID"].ToString());
                }
                if (row["UOFID"] != null && row["UOFID"].ToString() != "")
                {
                    model.UOFID = long.Parse(row["UOFID"].ToString());
                }
                if (row["OS_UserConsumerFormID"] != null && row["OS_UserConsumerFormID"].ToString() != "")
                {
                    model.OS_UserConsumerFormID = long.Parse(row["OS_UserConsumerFormID"].ToString());
                }
                if (row["ItemID"] != null && row["ItemID"].ToString() != "")
                {
                    model.ItemID = long.Parse(row["ItemID"].ToString());
                }
                if (row["RecommendationItemProductID"] != null && row["RecommendationItemProductID"].ToString() != "")
                {
                    model.RecommendationItemProductID = long.Parse(row["RecommendationItemProductID"].ToString());
                }
                if (row["DOIID"] != null && row["DOIID"].ToString() != "")
                {
                    model.DOIID = long.Parse(row["DOIID"].ToString());
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row["IsUse"] != null && row["IsUse"].ToString() != "")
                {
                    if ((row["IsUse"].ToString() == "1") || (row["IsUse"].ToString().ToLower() == "true"))
                    {
                        model.IsUse = true;
                    }
                    else
                    {
                        model.IsUse = false;
                    }
                }
                if (row["DataAddTime"] != null && row["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(row["DataAddTime"].ToString());
                }
                if (row["DataUpdateTime"] != null && row["DataUpdateTime"].ToString() != "")
                {
                    model.DataUpdateTime = DateTime.Parse(row["DataUpdateTime"].ToString());
                }
                if (row["MarketPrice"] != null && row["MarketPrice"].ToString() != "")
                {
                    model.MarketPrice = decimal.Parse(row["MarketPrice"].ToString());
                }
                if (row["GreatMasterPrice"] != null && row["GreatMasterPrice"].ToString() != "")
                {
                    model.GreatMasterPrice = decimal.Parse(row["GreatMasterPrice"].ToString());
                }
                if (row["DiscountPrice"] != null && row["DiscountPrice"].ToString() != "")
                {
                    model.DiscountPrice = decimal.Parse(row["DiscountPrice"].ToString());
                }
                if (row["Discount"] != null && row["Discount"].ToString() != "")
                {
                    model.Discount = decimal.Parse(row["Discount"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = long.Parse(row["Status"].ToString());
                }
                if (row["PayCode"] != null)
                {
                    model.PayCode = row["PayCode"].ToString();
                }
                if (row["CancelApplyTime"] != null && row["CancelApplyTime"].ToString() != "")
                {
                    model.CancelApplyTime = DateTime.Parse(row["CancelApplyTime"].ToString());
                }
                if (row["CancelFinishedTime"] != null && row["CancelFinishedTime"].ToString() != "")
                {
                    model.CancelFinishedTime = DateTime.Parse(row["CancelFinishedTime"].ToString());
                }
                if (row["ConsumerStartTime"] != null && row["ConsumerStartTime"].ToString() != "")
                {
                    model.ConsumerStartTime = DateTime.Parse(row["ConsumerStartTime"].ToString());
                }
                if (row["ConsumerFinishedTime"] != null && row["ConsumerFinishedTime"].ToString() != "")
                {
                    model.ConsumerFinishedTime = DateTime.Parse(row["ConsumerFinishedTime"].ToString());
                }
                if (row["ItemNo"] != null)
                {
                    model.ItemNo = row["ItemNo"].ToString();
                }
                if (row["ItemCName"] != null)
                {
                    model.ItemCName = row["ItemCName"].ToString();
                }
            }
            return model;
        }

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public bool AddOSUserConsumerItem(Model.OSUserConsumerItem model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.AreaID != null)
            {
                strSql1.Append("AreaID,");
                strSql2.Append("" + model.AreaID + ",");
            }
            if (model.HospitalID != null)
            {
                strSql1.Append("HospitalID,");
                strSql2.Append("" + model.HospitalID + ",");
            }
            if (model.OS_UserConsumerItemID != null)
            {
                strSql1.Append("OS_UserConsumerItemID,");
                strSql2.Append("" + model.OS_UserConsumerItemID + ",");
            }
            if (model.OS_UserConsumerFormID != null)
            {
                strSql1.Append("OS_UserConsumerFormID,");
                strSql2.Append("" + model.OS_UserConsumerFormID + ",");
            }
            if (model.RecommendationItemProductID != null)
            {
                strSql1.Append("RecommendationItemProductID,");
                strSql2.Append("" + model.RecommendationItemProductID + ",");
            }
            if (model.UOIID != null)
            {
                strSql1.Append("UOIID,");
                strSql2.Append("" + model.UOIID + ",");
            }
            if (model.ItemID != null)
            {
                strSql1.Append("ItemID,");
                strSql2.Append("" + model.ItemID + ",");
            }
            if (model.Memo != null)
            {
                strSql1.Append("Memo,");
                strSql2.Append("'" + model.Memo + "',");
            }
            if (model.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("" + model.DispOrder + ",");
            }
            if (model.IsUse != null)
            {
                strSql1.Append("IsUse,");
                strSql2.Append("" + (model.IsUse ? 1 : 0) + ",");
            }
            if (model.DataAddTime != null)
            {
                strSql1.Append("DataAddTime,");
                strSql2.Append("'" + model.DataAddTime + "',");
            }
            if (model.DataUpdateTime != null)
            {
                strSql1.Append("DataUpdateTime,");
                strSql2.Append("'" + model.DataUpdateTime + "',");
            }
            if (model.DataTimeStamp != null)
            {
                strSql1.Append("DataTimeStamp,");
                strSql2.Append("" + model.DataTimeStamp + ",");
            }
            if (model.MarketPrice != null)
            {
                strSql1.Append("MarketPrice,");
                strSql2.Append("" + model.MarketPrice + ",");
            }
            if (model.GreatMasterPrice != null)
            {
                strSql1.Append("GreatMasterPrice,");
                strSql2.Append("" + model.GreatMasterPrice + ",");
            }
            if (model.DiscountPrice != null)
            {
                strSql1.Append("DiscountPrice,");
                strSql2.Append("" + model.DiscountPrice + ",");
            }
            if (model.Discount != null)
            {
                strSql1.Append("Discount,");
                strSql2.Append("" + model.Discount + ",");
            }
            if (model.ItemNo != null)
            {
                strSql1.Append("ItemNo,");
                strSql2.Append("'" + model.ItemNo + "',");
            }
            strSql.Append("insert into OS_UserConsumerItem(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
		public bool DelOSUserConsumerItemBy(long OSUserConsumerFormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OS_UserConsumerItem ");
            strSql.Append(" where OS_UserConsumerFormID=" + OSUserConsumerFormID + " ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
