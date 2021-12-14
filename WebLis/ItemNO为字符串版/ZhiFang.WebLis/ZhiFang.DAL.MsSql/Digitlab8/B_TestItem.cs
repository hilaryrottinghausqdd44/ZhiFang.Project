using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
using ZhiFang.Common.Dictionary;
namespace ZhiFang.DAL.MsSql.Digitlab8
{
    //B_TestItem
    public partial class B_TestItem : IDTestItem, IDBatchCopy,IDGetListByTimeStampe
    {
        DBUtility.IDBConnection idb;
        public B_TestItem(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_TestItem()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_TestItem(");
            strSql.Append("Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,ItemNo,IsDoctorItem,IschargeItem,HisDispOrder,SuperGroupNo,Price,SpecialType,SpecialSection,LowPrice,SpecTypeNo,IsCombiItem,CName,IsHistoryItem,IsNurseItem,DefaultSType,SpecName,zdy1,zdy2,code_1,code_2,code_3,EName,StandCode,ZFStandCode,UseFlag,ShortName,ShortCode,DiagMethod,Unit,IsCalc");
            strSql.Append(") values (");
            strSql.Append("@Visible,@DispOrder,@Prec,@IsProfile,@OrderNo,@StandardCode,@ItemDesc,@FWorkLoad,@Secretgrade,@Cuegrade,@ItemNo,@IsDoctorItem,@IschargeItem,@HisDispOrder,@SuperGroupNo,@Price,@SpecialType,@SpecialSection,@LowPrice,@SpecTypeNo,@IsCombiItem,@CName,@IsHistoryItem,@IsNurseItem,@DefaultSType,@SpecName,@zdy1,@zdy2,@code_1,@code_2,@code_3,@EName,@StandCode,@ZFStandCode,@UseFlag,@ShortName,@ShortCode,@DiagMethod,@Unit,@IsCalc");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@Prec", SqlDbType.Int,4) ,            
                        new SqlParameter("@IsProfile", SqlDbType.Int,4) ,            
                        new SqlParameter("@OrderNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@StandardCode", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@ItemDesc", SqlDbType.VarChar,1024) ,            
                        new SqlParameter("@FWorkLoad", SqlDbType.Float,8) ,            
                        new SqlParameter("@Secretgrade", SqlDbType.Int,4) ,            
                        new SqlParameter("@Cuegrade", SqlDbType.Int,4) ,            
                        new SqlParameter("@ItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsDoctorItem", SqlDbType.Int,4) ,            
                        new SqlParameter("@IschargeItem", SqlDbType.Int,4) ,            
                        new SqlParameter("@HisDispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@Price", SqlDbType.Float,8) ,            
                        new SqlParameter("@SpecialType", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SpecialSection", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LowPrice", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SpecTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@IsCombiItem", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsHistoryItem", SqlDbType.Int,4) ,            
                        new SqlParameter("@IsNurseItem", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@DefaultSType", SqlDbType.Int,4) ,            
                        new SqlParameter("@SpecName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy1", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy2", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@code_1", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@code_2", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@code_3", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@EName", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@ShortName", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@DiagMethod", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Unit", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@IsCalc", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.Visible;
            parameters[1].Value = model.DispOrder;
            parameters[2].Value = model.Prec;
            parameters[3].Value = model.IsProfile;
            parameters[4].Value = model.OrderNo;
            parameters[5].Value = model.StandardCode;
            parameters[6].Value = model.ItemDesc;
            parameters[7].Value = model.FWorkLoad;
            parameters[8].Value = model.Secretgrade;
            parameters[9].Value = model.Cuegrade;
            parameters[10].Value = model.ItemNo;
            parameters[11].Value = model.IsDoctorItem;
            parameters[12].Value = model.IschargeItem;
            parameters[13].Value = model.HisDispOrder;
            parameters[14].Value = model.SuperGroupNo;
            parameters[15].Value = model.Price;
            parameters[16].Value = model.SpecialType;
            parameters[17].Value = model.SpecialSection;
            parameters[18].Value = model.LowPrice;
            parameters[19].Value = model.SpecTypeNo;
            parameters[20].Value = model.IsCombiItem;
            parameters[21].Value = model.CName;
            parameters[22].Value = model.IsHistoryItem;
            parameters[23].Value = model.IsNurseItem;
            parameters[24].Value = model.DefaultSType;
            parameters[25].Value = model.SpecName;
            parameters[26].Value = model.zdy1;
            parameters[27].Value = model.zdy2;
            parameters[28].Value = model.code_1;
            parameters[29].Value = model.code_2;
            parameters[30].Value = model.code_3;
            parameters[31].Value = model.EName;
            parameters[32].Value = model.StandCode;
            parameters[33].Value = model.ZFStandCode;
            parameters[34].Value = model.UseFlag;
            parameters[35].Value = model.ShortName;
            parameters[36].Value = model.ShortCode;
            parameters[37].Value = model.DiagMethod;
            parameters[38].Value = model.Unit;
            parameters[39].Value = model.IsCalc;
            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_TestItem set ");

            strSql.Append(" Visible = @Visible , ");
            strSql.Append(" DispOrder = @DispOrder , ");
            strSql.Append(" Prec = @Prec , ");
            strSql.Append(" IsProfile = @IsProfile , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" StandardCode = @StandardCode , ");
            strSql.Append(" ItemDesc = @ItemDesc , ");
            strSql.Append(" FWorkLoad = @FWorkLoad , ");
            strSql.Append(" Secretgrade = @Secretgrade , ");
            strSql.Append(" Cuegrade = @Cuegrade , ");
            strSql.Append(" ItemNo = @ItemNo , ");
            strSql.Append(" IsDoctorItem = @IsDoctorItem , ");
            strSql.Append(" IschargeItem = @IschargeItem , ");
            strSql.Append(" HisDispOrder = @HisDispOrder , ");
            strSql.Append(" SuperGroupNo = @SuperGroupNo , ");
            strSql.Append(" Price = @Price , ");
            strSql.Append(" SpecialType = @SpecialType , ");
            strSql.Append(" SpecialSection = @SpecialSection , ");
            strSql.Append(" LowPrice = @LowPrice , ");
            strSql.Append(" SpecTypeNo = @SpecTypeNo , ");
            strSql.Append(" IsCombiItem = @IsCombiItem , ");
            strSql.Append(" CName = @CName , ");
            strSql.Append(" IsHistoryItem = @IsHistoryItem , ");
            strSql.Append(" IsNurseItem = @IsNurseItem , ");
            strSql.Append(" DefaultSType = @DefaultSType , ");
            strSql.Append(" SpecName = @SpecName , ");
            strSql.Append(" zdy1 = @zdy1 , ");
            strSql.Append(" zdy2 = @zdy2 , ");
            strSql.Append(" code_1 = @code_1 , ");
            strSql.Append(" code_2 = @code_2 , ");
            strSql.Append(" code_3 = @code_3 , ");
            strSql.Append(" EName = @EName , ");
            strSql.Append(" StandCode = @StandCode , ");
            strSql.Append(" ZFStandCode = @ZFStandCode , ");
            strSql.Append(" UseFlag = @UseFlag , ");
            strSql.Append(" ShortName = @ShortName , ");
            strSql.Append(" ShortCode = @ShortCode , ");
            strSql.Append(" DiagMethod = @DiagMethod , ");
            strSql.Append(" Unit = @Unit , ");
            strSql.Append(" IsCalc = @IsCalc  ");
            strSql.Append(" where ItemNo=@ItemNo  ");

            SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Prec", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@IsProfile", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@OrderNo", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@StandardCode", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@ItemDesc", SqlDbType.VarChar,1024) ,            	
                           
            new SqlParameter("@FWorkLoad", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@Secretgrade", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Cuegrade", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ItemNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@IsDoctorItem", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@IschargeItem", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@HisDispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Price", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@SpecialType", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@SpecialSection", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LowPrice", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@SpecTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@IsCombiItem", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@IsHistoryItem", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@IsNurseItem", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@DefaultSType", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SpecName", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@zdy1", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@zdy2", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@code_1", SqlDbType.NVarChar,50) ,            	
                           
            new SqlParameter("@code_2", SqlDbType.NVarChar,50) ,            	
                           
            new SqlParameter("@code_3", SqlDbType.NVarChar,50) ,            	
                        	
                           
            new SqlParameter("@EName", SqlDbType.VarChar,40) ,            	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ShortName", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@DiagMethod", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@Unit", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@IsCalc", SqlDbType.Int,4)             	
              
            };




            if (model.Visible != null)
            {
                parameters[0].Value = model.Visible;
            }



            if (model.DispOrder != null)
            {
                parameters[1].Value = model.DispOrder;
            }



            if (model.Prec != null)
            {
                parameters[2].Value = model.Prec;
            }



            if (model.IsProfile != null)
            {
                parameters[3].Value = model.IsProfile;
            }



            if (model.OrderNo != null)
            {
                parameters[4].Value = model.OrderNo;
            }



            if (model.StandardCode != null)
            {
                parameters[5].Value = model.StandardCode;
            }



            if (model.ItemDesc != null)
            {
                parameters[6].Value = model.ItemDesc;
            }



            if (model.FWorkLoad != null)
            {
                parameters[7].Value = model.FWorkLoad;
            }



            if (model.Secretgrade != null)
            {
                parameters[8].Value = model.Secretgrade;
            }



            if (model.Cuegrade != null)
            {
                parameters[9].Value = model.Cuegrade;
            }



            if (model.ItemNo != null)
            {
                parameters[10].Value = model.ItemNo;
            }



            if (model.IsDoctorItem != null)
            {
                parameters[11].Value = model.IsDoctorItem;
            }



            if (model.IschargeItem != null)
            {
                parameters[12].Value = model.IschargeItem;
            }



            if (model.HisDispOrder != null)
            {
                parameters[13].Value = model.HisDispOrder;
            }



            if (model.SuperGroupNo != null)
            {
                parameters[14].Value = model.SuperGroupNo;
            }



            if (model.Price != null)
            {
                parameters[15].Value = model.Price;
            }



            if (model.SpecialType != null)
            {
                parameters[16].Value = model.SpecialType;
            }



            if (model.SpecialSection != null)
            {
                parameters[17].Value = model.SpecialSection;
            }



            if (model.LowPrice != null)
            {
                parameters[18].Value = model.LowPrice;
            }



            if (model.SpecTypeNo != null)
            {
                parameters[19].Value = model.SpecTypeNo;
            }



            if (model.IsCombiItem != null)
            {
                parameters[20].Value = model.IsCombiItem;
            }



            if (model.CName != null)
            {
                parameters[21].Value = model.CName;
            }



            if (model.IsHistoryItem != null)
            {
                parameters[22].Value = model.IsHistoryItem;
            }



            if (model.IsNurseItem != null)
            {
                parameters[23].Value = model.IsNurseItem;
            }



            if (model.DefaultSType != null)
            {
                parameters[24].Value = model.DefaultSType;
            }



            if (model.SpecName != null)
            {
                parameters[25].Value = model.SpecName;
            }



            if (model.zdy1 != null)
            {
                parameters[26].Value = model.zdy1;
            }



            if (model.zdy2 != null)
            {
                parameters[27].Value = model.zdy2;
            }



            if (model.code_1 != null)
            {
                parameters[28].Value = model.code_1;
            }



            if (model.code_2 != null)
            {
                parameters[29].Value = model.code_2;
            }



            if (model.code_3 != null)
            {
                parameters[30].Value = model.code_3;
            }





            if (model.EName != null)
            {
                parameters[31].Value = model.EName;
            }





            if (model.StandCode != null)
            {
                parameters[32].Value = model.StandCode;
            }



            if (model.ZFStandCode != null)
            {
                parameters[33].Value = model.ZFStandCode;
            }



            if (model.UseFlag != null)
            {
                parameters[34].Value = model.UseFlag;
            }



            if (model.ShortName != null)
            {
                parameters[35].Value = model.ShortName;
            }



            if (model.ShortCode != null)
            {
                parameters[36].Value = model.ShortCode;
            }



            if (model.DiagMethod != null)
            {
                parameters[37].Value = model.DiagMethod;
            }



            if (model.Unit != null)
            {
                parameters[38].Value = model.Unit;
            }



            if (model.IsCalc != null)
            {
                parameters[39].Value = model.IsCalc;
            }


            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string ItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_TestItem ");
            strSql.Append(" where ItemNo=@ItemNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemNo", SqlDbType.VarChar,50)};
            parameters[0].Value = ItemNo;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);
       
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.TestItem GetModel(string ItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemID, Visible, DispOrder, Prec, IsProfile, OrderNo, StandardCode, ItemDesc, FWorkLoad, Secretgrade, Cuegrade, ItemNo, IsDoctorItem, IschargeItem, HisDispOrder, SuperGroupNo, Price, SpecialType, SpecialSection, LowPrice, SpecTypeNo, IsCombiItem, CName, IsHistoryItem, IsNurseItem, DefaultSType, SpecName, zdy1, zdy2, code_1, code_2, code_3, DTimeStampe, EName, AddTime, StandCode, ZFStandCode, UseFlag, ShortName, ShortCode, DiagMethod, Unit, IsCalc  ");
            strSql.Append("  from B_TestItem ");
            strSql.Append(" where ItemNo=@ItemNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemNo", SqlDbType.VarChar,50)};
            parameters[0].Value = ItemNo;


            ZhiFang.Model.TestItem model = new ZhiFang.Model.TestItem();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ItemID"].ToString() != "")
                {
                    model.ItemID = int.Parse(ds.Tables[0].Rows[0]["ItemID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Prec"].ToString() != "")
                {
                    model.Prec = int.Parse(ds.Tables[0].Rows[0]["Prec"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsProfile"].ToString() != "")
                {
                    model.IsProfile = int.Parse(ds.Tables[0].Rows[0]["IsProfile"].ToString());
                }
                model.OrderNo = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                model.StandardCode = ds.Tables[0].Rows[0]["StandardCode"].ToString();
                model.ItemDesc = ds.Tables[0].Rows[0]["ItemDesc"].ToString();
                if (ds.Tables[0].Rows[0]["FWorkLoad"].ToString() != "")
                {
                    model.FWorkLoad = decimal.Parse(ds.Tables[0].Rows[0]["FWorkLoad"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Secretgrade"].ToString() != "")
                {
                    model.Secretgrade = int.Parse(ds.Tables[0].Rows[0]["Secretgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Cuegrade"].ToString() != "")
                {
                    model.Cuegrade = int.Parse(ds.Tables[0].Rows[0]["Cuegrade"].ToString());
                }
                model.ItemNo = ds.Tables[0].Rows[0]["ItemNo"].ToString();
                if (ds.Tables[0].Rows[0]["IsDoctorItem"].ToString() != "")
                {
                    model.IsDoctorItem = int.Parse(ds.Tables[0].Rows[0]["IsDoctorItem"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IschargeItem"].ToString() != "")
                {
                    model.IschargeItem = int.Parse(ds.Tables[0].Rows[0]["IschargeItem"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HisDispOrder"].ToString() != "")
                {
                    model.HisDispOrder = int.Parse(ds.Tables[0].Rows[0]["HisDispOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SuperGroupNo"].ToString() != "")
                {
                    model.SuperGroupNo = int.Parse(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }
                model.SpecialType = ds.Tables[0].Rows[0]["SpecialType"].ToString();
                model.SpecialSection = ds.Tables[0].Rows[0]["SpecialSection"].ToString();
                model.LowPrice = ds.Tables[0].Rows[0]["LowPrice"].ToString();
                if (ds.Tables[0].Rows[0]["SpecTypeNo"].ToString() != "")
                {
                    model.SpecTypeNo = int.Parse(ds.Tables[0].Rows[0]["SpecTypeNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsCombiItem"].ToString() != "")
                {
                    model.IsCombiItem = int.Parse(ds.Tables[0].Rows[0]["IsCombiItem"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                if (ds.Tables[0].Rows[0]["IsHistoryItem"].ToString() != "")
                {
                    model.IsHistoryItem = int.Parse(ds.Tables[0].Rows[0]["IsHistoryItem"].ToString());
                }
                model.IsNurseItem = ds.Tables[0].Rows[0]["IsNurseItem"].ToString();
                if (ds.Tables[0].Rows[0]["DefaultSType"].ToString() != "")
                {
                    model.DefaultSType = int.Parse(ds.Tables[0].Rows[0]["DefaultSType"].ToString());
                }
                model.SpecName = ds.Tables[0].Rows[0]["SpecName"].ToString();
                model.zdy1 = ds.Tables[0].Rows[0]["zdy1"].ToString();
                model.zdy2 = ds.Tables[0].Rows[0]["zdy2"].ToString();
                model.code_1 = ds.Tables[0].Rows[0]["code_1"].ToString();
                model.code_2 = ds.Tables[0].Rows[0]["code_2"].ToString();
                model.code_3 = ds.Tables[0].Rows[0]["code_3"].ToString();
                model.EName = ds.Tables[0].Rows[0]["EName"].ToString();
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                model.StandCode = ds.Tables[0].Rows[0]["StandCode"].ToString();
                model.ZFStandCode = ds.Tables[0].Rows[0]["ZFStandCode"].ToString();
                if (ds.Tables[0].Rows[0]["UseFlag"].ToString() != "")
                {
                    model.UseFlag = int.Parse(ds.Tables[0].Rows[0]["UseFlag"].ToString());
                }
                model.ShortName = ds.Tables[0].Rows[0]["ShortName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                model.DiagMethod = ds.Tables[0].Rows[0]["DiagMethod"].ToString();
                model.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                if (ds.Tables[0].Rows[0]["IsCalc"].ToString() != "")
                {
                    model.IsCalc = int.Parse(ds.Tables[0].Rows[0]["IsCalc"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select '('+ItemNo+')'+CName as ItemNoName,* ");
            strSql.Append(" FROM B_TestItem ");
            if (model.IfUseLike == "1")
            {
                if (model.SuperGroupNo != null)
                {
                    strSql.Append(" where SuperGroupNo=" + model.SuperGroupNo + " and (1=1 ");
                }
                else
                {
                    strSql.Append(" where (1=2 ");
                }
                if (model.ItemNo != null)
                {
                    strSql.Append(" or ItemNo like '%" + model.ItemNo + "%' ");
                }
                if (model.CName != null)
                {
                    strSql.Append(" or CName like '%" + model.CName + "%' ");
                }
                if (model.ShortName != null)
                {
                    strSql.Append(" or ShortName like '%" + model.ShortName + "%' ");
                }
                if (model.ShortCode != null)
                {
                    strSql.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
                }

                strSql.Append(" ) ");

            }
            else
            {
                strSql.Append(" where 1=1 ");
                if (model.DispOrder != null)
                {
                    strSql.Append(" and DispOrder=" + model.DispOrder + " ");
                }

                if (model.Prec != null)
                {
                    strSql.Append(" and Prec=" + model.Prec + " ");
                }

                if (model.IsProfile != -2)
                {
                    strSql.Append(" and IsProfile=" + model.IsProfile + " ");
                }

                if (model.OrderNo != null)
                {
                    strSql.Append(" and OrderNo='" + model.OrderNo + "' ");
                }

                if (model.StandardCode != null)
                {
                    strSql.Append(" and StandardCode='" + model.StandardCode + "' ");
                }

                if (model.ItemDesc != null)
                {
                    strSql.Append(" and ItemDesc='" + model.ItemDesc + "' ");
                }

                if (model.FWorkLoad != null)
                {
                    strSql.Append(" and FWorkLoad=" + model.FWorkLoad + " ");
                }

                if (model.Secretgrade != null)
                {
                    strSql.Append(" and Secretgrade=" + model.Secretgrade + " ");
                }

                if (model.Cuegrade != null)
                {
                    strSql.Append(" and Cuegrade=" + model.Cuegrade + " ");
                }

                if (model.ItemNo != null)
                {
                    strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
                }

                if (model.IsDoctorItem != null)
                {
                    strSql.Append(" and IsDoctorItem=" + model.IsDoctorItem + " ");
                }

                if (model.IschargeItem != null)
                {
                    strSql.Append(" and IschargeItem=" + model.IschargeItem + " ");
                }

                if (model.HisDispOrder != null)
                {
                    strSql.Append(" and HisDispOrder=" + model.HisDispOrder + " ");
                }

                if (model.SuperGroupNo != null)
                {
                    strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + " ");
                }

                if (model.Price != null)
                {
                    strSql.Append(" and Price=" + model.Price + " ");
                }

                if (model.SpecialType != null)
                {
                    strSql.Append(" and SpecialType='" + model.SpecialType + "' ");
                }

                if (model.SpecialSection != null)
                {
                    strSql.Append(" and SpecialSection='" + model.SpecialSection + "' ");
                }

                if (model.LowPrice != null)
                {
                    strSql.Append(" and LowPrice='" + model.LowPrice + "' ");
                }

                if (model.SpecTypeNo != null)
                {
                    strSql.Append(" and SpecTypeNo=" + model.SpecTypeNo + " ");
                }

                if (model.IsCombiItem != null)
                {
                    strSql.Append(" and IsCombiItem=" + model.IsCombiItem + " ");
                }

                if (model.CName != null)
                {
                    strSql.Append(" and CName='" + model.CName + "' ");
                }

                if (model.IsHistoryItem != null)
                {
                    strSql.Append(" and IsHistoryItem=" + model.IsHistoryItem + " ");
                }

                if (model.IsNurseItem != null)
                {
                    strSql.Append(" and IsNurseItem='" + model.IsNurseItem + "' ");
                }

                if (model.DefaultSType != null)
                {
                    strSql.Append(" and DefaultSType=" + model.DefaultSType + " ");
                }

                if (model.SpecName != null)
                {
                    strSql.Append(" and SpecName='" + model.SpecName + "' ");
                }

                if (model.zdy1 != null)
                {
                    strSql.Append(" and zdy1='" + model.zdy1 + "' ");
                }

                if (model.zdy2 != null)
                {
                    strSql.Append(" and zdy2='" + model.zdy2 + "' ");
                }

                if (model.code_1 != null)
                {
                    strSql.Append(" and code_1='" + model.code_1 + "' ");
                }

                if (model.code_2 != null)
                {
                    strSql.Append(" and code_2='" + model.code_2 + "' ");
                }

                if (model.code_3 != null)
                {
                    strSql.Append(" and code_3='" + model.code_3 + "' ");
                }

                if (model.DTimeStampe != null)
                {
                    strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
                }

                if (model.EName != null)
                {
                    strSql.Append(" and EName='" + model.EName + "' ");
                }

                if (model.StandCode != null)
                {
                    strSql.Append(" and StandCode='" + model.StandCode + "' ");
                }

                if (model.ZFStandCode != null)
                {
                    strSql.Append(" and ZFStandCode='" + model.ZFStandCode + "' ");
                }

                if (model.ShortName != null)
                {
                    strSql.Append(" and ShortName='" + model.ShortName + "' ");
                }

                if (model.ShortCode != null)
                {
                    strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
                }

                if (model.DiagMethod != null)
                {
                    strSql.Append(" and DiagMethod='" + model.DiagMethod + "' ");
                }

                if (model.Unit != null)
                {
                    strSql.Append(" and Unit='" + model.Unit + "' ");
                }

                if (model.IsCalc != null)
                {
                    strSql.Append(" and IsCalc=" + model.IsCalc + " ");
                }
            }
            //ZhiFang.Common.Log.Log.Debug("Digitlab8.B_TestItem.GetList(ZhiFang.Model.B_TestItem model).strSql=" + strSql.ToString());
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(ZhiFang.Model.TestItem model, string flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select '('+ItemNo+')'+CName as ItemNoName,* ");
            strSql.Append(" FROM B_TestItem where 1=1 ");
            if (flag == "0")
            {//组套外项目
                strSql.Append(" and (isCombiItem=1 or IsDoctorItem=1 or IschargeItem=1) ");
                if (model.ItemNo.Trim() != "")
                {
                    strSql.Append(" and ItemNo<>'" + model.ItemNo + "' and ItemNo not in (select ItemNo from B_GroupItem where PItemNo='" + model.ItemNo + "') ");
                }
                if (model.SearchKey.Trim() != "")
                {
                    strSql.Append(" and (itemno like '%" + model.SearchKey + "%' or cname like '%" + model.SearchKey + "%' or ShortCode like '%" + model.SearchKey + "%' or ShortName like '%" + model.SearchKey + "%' ) ");
                }
                if (model.PItemNos.Trim() != "")
                {
                    strSql.Append(" and ItemNo not in (" + model.PItemNos + ") ");
                }
            }
            else if (flag == "1")
            { //组套内项目
                strSql.Append(" and ItemNo in (select ItemNo from B_GroupItem where PItemNo='" + model.ItemNo + "') ");
            }
            else
            { }
            return idb.ExecuteDataSet(strSql.ToString());
        }       

        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心项目字典表分页；model!=null时为中心--字典项目对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.TestItem model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.ItemNo != null)
                {
                    strWhere.Append(" and ( B_TestItem.ItemNo like '%" + model.ItemNo + "%'  ");
                }
                if (model.CName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_TestItem.CName like '%" + model.CName + "%'  ");
                    else
                        strWhere.Append(" and ( B_TestItem.CName like '%" + model.CName + "%'  ");
                }
                if (model.ShortName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_TestItem.ShortName like '%" + model.ShortName + "%'  ");
                    else
                        strWhere.Append(" and ( B_TestItem.ShortName like '%" + model.ShortName + "%'  ");
                }
                if (model.ShortCode != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_TestItem.ShortCode like '%" + model.ShortCode + "%'  ");
                    else
                        strWhere.Append(" and ( B_TestItem.ShortCode like '%" + model.ShortCode + "%'  ");
                }
                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from B_TestItem left join B_TestItemControl on B_TestItem.ItemNo=B_TestItemControl.ItemNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_TestItemControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where ItemID not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " ItemID from  B_TestItem left join B_TestItemControl on B_TestItem.ItemNo=B_TestItemControl.ItemNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_TestItemControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" " + strWhere.ToString() + " order by B_TestItem." + model.OrderField + " ) " + strWhere.ToString() + " order by B_TestItem." + model.OrderField + " ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  '('+ItemNo+')'+CName as ItemNoName,* from B_TestItem where ItemID not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " ItemID from B_TestItem where 1=1 " + strWhere.ToString() + " order by " + model.OrderField + ") " + strWhere.ToString() + " order by " + model.OrderField + " ");
                //ZhiFang.Common.Log.Log.Info("B_TestItem------>"+strSql.ToString());
                return idb.ExecuteDataSet(strSql.ToString());
            }
        }
        
        /// <summary>
        /// 获取总记录的数量
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_TestItem");
            string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }

        public int GetTotalCount(ZhiFang.Model.TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM B_TestItem where 1=1  ");
            if (model != null)
            {
                if (model.ItemNo != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or ItemNo like '%" + model.ItemNo + "%' ");
                    else
                        strWhere.Append(" and ( ItemNo like '%" + model.ItemNo + "%' ");
                }

                if (model.IsDoctorItem != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or IsDoctorItem like '%" + model.IsDoctorItem + "' ");
                    else
                        strWhere.Append(" and ( IsDoctorItem like '%" + model.IsDoctorItem + "' ");
                }

                if (model.IschargeItem != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or IschargeItem like '%" + model.IschargeItem + "' ");
                    else
                        strWhere.Append(" and ( IschargeItem like '%" + model.IschargeItem + "' ");
                }

                if (model.IsNurseItem != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or IsNurseItem like '%" + model.IsNurseItem + "' ");
                    else
                        strWhere.Append(" and ( IsNurseItem like '%" + model.IsNurseItem + "' ");
                }

                if (model.IsCombiItem != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or IsCombiItem like '%" + model.IsCombiItem + "' ");
                    else
                        strWhere.Append(" and ( IsCombiItem like '%" + model.IsCombiItem + "' ");
                }

                if (model.IsTestItem != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or IsTestItem like '%" + model.IsTestItem + "' ");
                    else
                        strWhere.Append(" and ( IsTestItem like '%" + model.IsTestItem + "' ");
                }

                if (model.CName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or CName like '%" + model.CName + "%' ");
                    else
                        strWhere.Append(" and ( CName like '%" + model.CName + "%' ");
                }

                if (model.EName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or EName like '%" + model.EName + "%' ");
                    else
                        strWhere.Append(" and ( EName like '%" + model.EName + "%' ");
                }

                if (model.ShortName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or ShortName like '%" + model.ShortName + "%' ");
                    else
                        strWhere.Append(" and ( ShortName like '%" + model.ShortName + "%' ");
                }

                if (model.ShortCode != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
                    else
                        strWhere.Append(" and ( ShortCode like '%" + model.ShortCode + "%' ");
                }
                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            strSql.Append(strWhere.ToString());
            //ZhiFang.Common.Log.Log.Info(strSql.ToString());
            string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }

        #region IDBatchCopy 成员

        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            string LabTableName = "B_TestItem";
            LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey = "ItemNo";
            string TableKeySub = TableKey;
            if (TableKey.ToLower().Contains("no"))
            {
                TableKeySub = TableKey.Substring(0, TableKey.ToLower().IndexOf("no"));
            }
            try
            {
                if (lst.Count > 0)
                {
                    if (lst[0].Trim() == "CopyToLab_LabFirstSelect")
                    {
                        //项目选择性批量复制到客户端
                        if (lst.Count == 1)
                            return true;

                        for (int i = 1; i < lst.Count; i++)
                        {
                            strSql.Append("insert into " + LabTableName + "( LabCode,");
                            strSql.Append(" Visible , DispOrder , Prec , IsProfile , OrderNo , StandardCode , ItemDesc , FWorkLoad , Secretgrade , Cuegrade , LabItemNo , IsDoctorItem , IschargeItem , HisDispOrder , SuperGroupNo , Price , SpecialType , SpecialSection , LowPrice , SpecTypeNo , IsCombiItem , CName , IsHistoryItem , IsNurseItem , DefaultSType , SpecName , zdy1 , zdy2 , code_1 , code_2 , code_3 , EName , StandCode , ZFStandCode , UseFlag , ShortName , ShortCode , DiagMethod , Unit , IsCalc ");
                            strSql.Append(") select '" + lst[i].Trim().Split('|')[0].Trim() + "' as LabCode, ");
                            strSql.Append("Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,ItemNo,IsDoctorItem,IschargeItem,HisDispOrder,SuperGroupNo,Price,SpecialType,SpecialSection,LowPrice,SpecTypeNo,IsCombiItem,CName,IsHistoryItem,IsNurseItem,DefaultSType,SpecName,zdy1,zdy2,code_1,code_2,code_3,EName,StandCode,ZFStandCode,UseFlag,ShortName,ShortCode,DiagMethod,Unit,IsCalc");
                            strSql.Append(" from B_TestItem where ItemNo in " + lst[i].Trim().Split('|')[1].Trim() + " ");

                            strSqlControl.Append("insert into B_TestItemControl ( ");
                            strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
                            strSqlControl.Append(")  select ");
                            strSqlControl.Append("  '" + lst[i].Trim().Split('|')[0].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim().Split('|')[0].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
                            strSqlControl.Append(" from B_TestItem where ItemNo in " + lst[i].Trim().Split('|')[1].Trim() + " ");

                            arrySql.Add(strSql.ToString());
                            arrySql.Add(strSqlControl.ToString());

                            strSql = new StringBuilder();
                            strSqlControl = new StringBuilder();

                        }
                    }
                    else
                    {
                        //把整个表批量复制到客户端
                        for (int i = 0; i < lst.Count; i++)
                        {
                            strSql.Append("insert into " + LabTableName + "( LabCode,");
                            strSql.Append(" Visible , DispOrder , Prec , IsProfile , OrderNo , StandardCode , ItemDesc , FWorkLoad , Secretgrade , Cuegrade , LabItemNo , IsDoctorItem , IschargeItem , HisDispOrder , SuperGroupNo , Price , SpecialType , SpecialSection , LowPrice , SpecTypeNo , IsCombiItem , CName , IsHistoryItem , IsNurseItem , DefaultSType , SpecName , zdy1 , zdy2 , code_1 , code_2 , code_3 , EName , StandCode , ZFStandCode , UseFlag , ShortName , ShortCode , DiagMethod , Unit , IsCalc ");
                            strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                            strSql.Append("Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,ItemNo,IsDoctorItem,IschargeItem,HisDispOrder,SuperGroupNo,Price,SpecialType,SpecialSection,LowPrice,SpecTypeNo,IsCombiItem,CName,IsHistoryItem,IsNurseItem,DefaultSType,SpecName,zdy1,zdy2,code_1,code_2,code_3,EName,StandCode,ZFStandCode,UseFlag,ShortName,ShortCode,DiagMethod,Unit,IsCalc");
                            strSql.Append(" from B_TestItem ");

                            strSqlControl.Append("insert into B_TestItemControl ( ");
                            strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
                            strSqlControl.Append(")  select ");
                            strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
                            strSqlControl.Append(" from B_TestItem ");

                            arrySql.Add(strSql.ToString());
                            arrySql.Add(strSqlControl.ToString());

                            strSql = new StringBuilder();
                            strSqlControl = new StringBuilder();

                        }
                    }
                }
                

                idb.BatchUpdateWithTransaction(arrySql);

                d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);
                return true;
            }
            catch
            {
               
                return false;
            }

        }

        #endregion

        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'" + LabCode + "' as LabCode,ItemNo as LabItemNo from B_TestItem where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabItemNo as ItemNo from B_Lab_TestItem where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql2.Append(" and LabCode= '" + LabCode + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql2.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtLab = idb.ExecuteDataSet(strSql2.ToString()).Tables[0];
            dtLab.TableName = "LabDatas";

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select * from B_TestItemControl where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql3.Append(" and ControlLabNo= '" + LabCode + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql3.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtControl = idb.ExecuteDataSet(strSql3.ToString()).Tables[0];
            dtControl.TableName = "ControlDatas";

            dsAll.Tables.Add(dtServer.Copy());
            dsAll.Tables.Add(dtLab.Copy());
            dsAll.Tables.Add(dtControl.Copy());
            return dsAll;
        }

        #endregion

        #region IDTestItem 成员

        public bool Exists(string ItemNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_TestItem");
            strSql.Append(" where ItemNo='" + ItemNo + "' ");
            string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "" && strCount.Trim() != "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Exists(System.Collections.Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_TestItem where 1=1 ");
            if (ht.Count > 0)
            {
                foreach (System.Collections.DictionaryEntry item in ht)
                {
                    strSql.Append(" and " + item.Key.ToString().Trim() + "='" + item.Value + "' ");
                }
                string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "" && strCount.Trim() != "0")
            {
                return true;
            }
            else
            {
                return false;
            }
            }
            else
            {
                return false;
            }
        }

        public DataSet GetListLike(Model.TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_TestItem where (TestItem.ItemNo is not null and TestItem.ItemNo<>'' and TestItem.CName is not null and TestItem.CName <> '' and (IsDoctorItem=1 or isCombiItem=1 or IschargeItem=1)) and (1=2  ");
            if (model.CName != null)
            {
                strSql.Append(" or CName like '%" + model.CName + "%'");
            }
            if (model.EName != null)
            {
                strSql.Append(" or EName like '%" + model.EName + "%'");
            }
            if (model.ShortName != null)
            {
                strSql.Append(" or ShortName like '%" + model.ShortName + "%'");
            }
            if (model.ShortCode != null)
            {
                strSql.Append(" or ShortCode like '%" + model.ShortCode + "%'");
            }
            strSql.Append(" ) order by ShortCode ");
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public int GetListCount(Model.TestItem TestItem)
        {
            int count = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*)");
            strSql.Append(" FROM B_TestItem INNER JOIN    dbo.SuperGroup ON dbo.B_TestItem.SuperGroupNo = dbo.SuperGroup.SuperGroupNo");
            if (TestItem.TestItemSuperGroupClass != null)
            {                
                switch (TestItem.TestItemSuperGroupClass)
                {
                    case TestItemSuperGroupClass.ALL:
                        if (TestItem.TestItemLikeKey != null && TestItem.TestItemLikeKey.Trim() != "")
                        {
                            strSql.Append(" where  ( B_TestItem.ItemNo LIKE '%" + TestItem.TestItemLikeKey + "%' or testitem.CName like '%" + TestItem.TestItemLikeKey + "%' or testitem.EName like '%" + TestItem.TestItemLikeKey + "%' or testitem.ShortName like '%" + TestItem.TestItemLikeKey + "%' or testitem.shortCode like '%" + TestItem.TestItemLikeKey + "%' ) and ( B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' ) and ( IsDoctorItem=1 or isCombiItem=1 or IschargeItem=1 )  ");
                        }
                        else
                        {
                            strSql.Append(" where  1=1 and B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' and (IsDoctorItem=1 or isCombiItem=1 or IschargeItem=1)");
                        }
                        break;
                    case TestItemSuperGroupClass.DOCTORCOMBICHARGE:
                        if (TestItem.TestItemLikeKey != null && TestItem.TestItemLikeKey.Trim() != "")
                        {
                            strSql.Append(" where  ( B_TestItem.ItemNo LIKE '%" + TestItem.TestItemLikeKey + "%' or testitem.CName like '%" + TestItem.TestItemLikeKey + "%' or testitem.EName like '%" + TestItem.TestItemLikeKey + "%' or testitem.ShortName like '%" + TestItem.TestItemLikeKey + "%' or testitem.shortCode like '%" + TestItem.TestItemLikeKey + "%' ) and ( B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' ) and ( IsDoctorItem=1 or isCombiItem=1 or IschargeItem=1 )  ");
                        }
                        else
                        {
                            strSql.Append(" where  1=1 and B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' and (IsDoctorItem=1 or isCombiItem=1 or IschargeItem=1)");
                        } break;
                    case TestItemSuperGroupClass.OFTEN:
                        strSql.Append(" where ( B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' )  ");
                        break;
                    case TestItemSuperGroupClass.COMBI:
                        if (TestItem.TestItemLikeKey != null && TestItem.TestItemLikeKey.Trim() != "")
                        {
                            strSql.Append(" where  ( B_TestItem.ItemNo LIKE '%" + TestItem.TestItemLikeKey + "%' or testitem.CName like '%" + TestItem.TestItemLikeKey + "%' or testitem.EName like '%" + TestItem.TestItemLikeKey + "%' or testitem.ShortName like '%" + TestItem.TestItemLikeKey + "%' or testitem.shortCode like '%" + TestItem.TestItemLikeKey + "%' ) and ( B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' ) and (isCombiItem=1)  ");
                        }
                        else
                        {
                            strSql.Append(" where  1=1 and B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' and (isCombiItem=1)");
                        }
                        break;
                    case TestItemSuperGroupClass.CHARGE:
                        if (TestItem.TestItemLikeKey != null && TestItem.TestItemLikeKey.Trim() != "")
                        {
                            strSql.Append(" where  ( B_TestItem.ItemNo LIKE '%" + TestItem.TestItemLikeKey + "%' or testitem.CName like '%" + TestItem.TestItemLikeKey + "%' or testitem.EName like '%" + TestItem.TestItemLikeKey + "%' or testitem.ShortName like '%" + TestItem.TestItemLikeKey + "%' or testitem.shortCode like '%" + TestItem.TestItemLikeKey + "%' ) and ( B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' ) and ( IschargeItem=1) and (isCombiItem=0)  ");
                        }
                        else
                        {
                            strSql.Append(" where  1=1 and B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' and ( IschargeItem=1) and (isCombiItem=0) ");
                        }
                        break;
                    case TestItemSuperGroupClass.SUPERGROUP:
                        if (TestItem.TestItemLikeKey != null && TestItem.TestItemLikeKey.Trim() != "")
                        {
                            strSql.Append(" where   SuperGroup.SuperGroupNo =" + TestItem.SuperGroupNo + " and (B_TestItem.ItemNo LIKE '%" + TestItem.TestItemLikeKey + "%' or testitem.CName like '%" + TestItem.TestItemLikeKey + "%' or testitem.EName like '%" + TestItem.TestItemLikeKey + "%' or testitem.ShortName like '%" + TestItem.TestItemLikeKey + "%' or testitem.shortCode like '%" + TestItem.TestItemLikeKey + "%' )  and B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> ''  ");
                        }
                        else
                        {
                            strSql.Append(" where   SuperGroup.SuperGroupNo =" + TestItem.SuperGroupNo + " and 1=1 and B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' ");
                        }
                        break;
                    default:
                        if (TestItem.TestItemLikeKey != null && TestItem.TestItemLikeKey.Trim() != "")
                        {
                            strSql.Append(" where  ( B_TestItem.ItemNo LIKE '%" + TestItem.TestItemLikeKey + "%' or testitem.CName like '%" + TestItem.TestItemLikeKey + "%' or testitem.EName like '%" + TestItem.TestItemLikeKey + "%' or testitem.ShortName like '%" + TestItem.TestItemLikeKey + "%' or testitem.shortCode like '%" + TestItem.TestItemLikeKey + "%' ) and ( B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' )   ");
                        }
                        else
                        {
                            strSql.Append(" where  1=1 and B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' ");
                        }
                        break;
                }
            }
            else
            {
                if (TestItem.TestItemLikeKey != null && TestItem.TestItemLikeKey.Trim() != "")
                {
                    strSql.Append(" where  ( B_TestItem.ItemNo LIKE '%" + TestItem.TestItemLikeKey + "%' or testitem.CName like '%" + TestItem.TestItemLikeKey + "%' or testitem.EName like '%" + TestItem.TestItemLikeKey + "%' or testitem.ShortName like '%" + TestItem.TestItemLikeKey + "%' or testitem.shortCode like '%" + TestItem.TestItemLikeKey + "%' ) and ( B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' )   ");
                }
                else
                {
                    strSql.Append(" where  1=1 and B_TestItem.ItemNo is not null and B_TestItem.ItemNo<>'' and B_TestItem.CName is not null and B_TestItem.CName <> '' ");
                }
            }
            return (int)idb.GetSingle(strSql.ToString());
        }

        #endregion

        #region IDataBase<TestItem> 成员

        public int GetMaxId()
        {
            return idb.GetMaxID("ItemNo", "B_TestItem");
        }

        public DataSet GetList(int Top, Model.TestItem model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM B_TestItem ");


            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + " ");
            }

            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + " ");
            }

            if (model.Prec != null)
            {
                strSql.Append(" and Prec=" + model.Prec + " ");
            }

            if (model.IsProfile != null)
            {
                strSql.Append(" and IsProfile=" + model.IsProfile + " ");
            }

            if (model.OrderNo != null)
            {

                strSql.Append(" and OrderNo='" + model.OrderNo + "' ");
            }

            if (model.StandardCode != null)
            {

                strSql.Append(" and StandardCode='" + model.StandardCode + "' ");
            }

            if (model.ItemDesc != null)
            {

                strSql.Append(" and ItemDesc='" + model.ItemDesc + "' ");
            }

            if (model.FWorkLoad != null)
            {
                strSql.Append(" and FWorkLoad=" + model.FWorkLoad + " ");
            }

            if (model.Secretgrade != null)
            {
                strSql.Append(" and Secretgrade=" + model.Secretgrade + " ");
            }

            if (model.Cuegrade != null)
            {
                strSql.Append(" and Cuegrade=" + model.Cuegrade + " ");
            }

            if (model.ItemNo != null)
            {

                strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
            }

            if (model.IsDoctorItem != null)
            {
                strSql.Append(" and IsDoctorItem=" + model.IsDoctorItem + " ");
            }

            if (model.IschargeItem != null)
            {
                strSql.Append(" and IschargeItem=" + model.IschargeItem + " ");
            }

            if (model.HisDispOrder != null)
            {
                strSql.Append(" and HisDispOrder=" + model.HisDispOrder + " ");
            }

            if (model.IsNurseItem != null)
            {
                strSql.Append(" and IsNurseItem=" + model.IsNurseItem + " ");
            }

            if (model.IsCombiItem != null)
            {
                strSql.Append(" and IsCombiItem=" + model.IsCombiItem + " ");
            }

            if (model.IsTestItem != null)
            {
                strSql.Append(" and IsTestItem=" + model.IsTestItem + " ");
            }

            if (model.IsHistoryItem != null)
            {
                strSql.Append(" and IsHistoryItem=" + model.IsHistoryItem + " ");
            }

            if (model.DTimeStampe != null)
            {

                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {

                strSql.Append(" and AddTime='" + model.AddTime + "' ");
            }

            if (model.StandCode != null)
            {

                strSql.Append(" and StandCode='" + model.StandCode + "' ");
            }

            if (model.CName != null)
            {

                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.ZFStandCode != null)
            {

                strSql.Append(" and ZFStandCode='" + model.ZFStandCode + "' ");
            }

            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + " ");
            }

            if (model.EName != null)
            {

                strSql.Append(" and EName='" + model.EName + "' ");
            }

            if (model.ShortName != null)
            {

                strSql.Append(" and ShortName='" + model.ShortName + "' ");
            }

            if (model.ShortCode != null)
            {

                strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
            }

            if (model.DiagMethod != null)
            {

                strSql.Append(" and DiagMethod='" + model.DiagMethod + "' ");
            }

            if (model.Unit != null)
            {

                strSql.Append(" and Unit='" + model.Unit + "' ");
            }

            if (model.IsCalc != null)
            {
                strSql.Append(" and IsCalc=" + model.IsCalc + " ");
            }

            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    int count = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        if (this.Exists(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim()))
                        {
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                            count += this.AddByDataRow(dr);
                    }
                    if (count == ds.Tables[0].Rows.Count)
                        return 1;
                    else
                        return 0;
                }
                catch
                {
                    return 0;
                }
            }
            else
                return 1;
        }
        public int AddByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into B_TestItem (");
                strSql.Append("Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,ItemNo,IsDoctorItem,IschargeItem,HisDispOrder,SuperGroupNo,Price,SpecialType,SpecialSection,LowPrice,SpecTypeNo,IsCombiItem,CName,IsHistoryItem,IsNurseItem,DefaultSType,SpecName,zdy1,zdy2,code_1,code_2,code_3,EName,StandCode,ZFStandCode,UseFlag,ShortName,ShortCode,DiagMethod,Unit,IsCalc");
                strSql.Append(") values (");
                if (dr.Table.Columns["Visible"] != null && dr.Table.Columns["Visible"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Visible"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["DispOrder"] != null && dr.Table.Columns["DispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DispOrder"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["Prec"] != null && dr.Table.Columns["Prec"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Prec"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["IsProfile"] != null && dr.Table.Columns["IsProfile"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsProfile"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["OrderNo"] != null && dr.Table.Columns["OrderNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["OrderNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["StandardCode"] != null && dr.Table.Columns["StandardCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["StandardCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["ItemDesc"] != null && dr.Table.Columns["ItemDesc"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ItemDesc"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["FWorkLoad"] != null && dr.Table.Columns["FWorkLoad"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["FWorkLoad"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["Secretgrade"] != null && dr.Table.Columns["Secretgrade"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Secretgrade"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["Cuegrade"] != null && dr.Table.Columns["Cuegrade"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Cuegrade"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["ItemNo"] != null && dr.Table.Columns["ItemNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ItemNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["IsDoctorItem"] != null && dr.Table.Columns["IsDoctorItem"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsDoctorItem"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["IschargeItem"] != null && dr.Table.Columns["IschargeItem"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IschargeItem"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["HisDispOrder"] != null && dr.Table.Columns["HisDispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["HisDispOrder"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["SuperGroupNo"] != null && dr.Table.Columns["SuperGroupNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SuperGroupNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["Price"] != null && dr.Table.Columns["Price"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Price"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["SpecialType"] != null && dr.Table.Columns["SpecialType"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SpecialType"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["SpecialSection"] != null && dr.Table.Columns["SpecialSection"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SpecialSection"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["LowPrice"] != null && dr.Table.Columns["LowPrice"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LowPrice"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["SpecTypeNo"] != null && dr.Table.Columns["SpecTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SpecTypeNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["IsCombiItem"] != null && dr.Table.Columns["IsCombiItem"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsCombiItem"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["IsHistoryItem"] != null && dr.Table.Columns["IsHistoryItem"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsHistoryItem"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["IsNurseItem"] != null && dr.Table.Columns["IsNurseItem"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsNurseItem"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["DefaultSType"] != null && dr.Table.Columns["DefaultSType"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DefaultSType"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["SpecName"] != null && dr.Table.Columns["SpecName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SpecName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["zdy1"] != null && dr.Table.Columns["zdy1"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["zdy1"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["zdy2"] != null && dr.Table.Columns["zdy2"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["zdy2"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["code_1"] != null && dr.Table.Columns["code_1"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["code_1"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["code_2"] != null && dr.Table.Columns["code_2"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["code_2"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["code_3"] != null && dr.Table.Columns["code_3"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["code_3"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["EName"] != null && dr.Table.Columns["EName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["EName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["StandCode"] != null && dr.Table.Columns["StandCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["StandCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["ZFStandCode"] != null && dr.Table.Columns["ZFStandCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ZFStandCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["UseFlag"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["ShortName"] != null && dr.Table.Columns["ShortName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ShortName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["ShortCode"] != null && dr.Table.Columns["ShortCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ShortCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["DiagMethod"] != null && dr.Table.Columns["DiagMethod"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DiagMethod"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["Unit"] != null && dr.Table.Columns["Unit"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Unit"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["IsCalc"] != null && dr.Table.Columns["IsCalc"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsCalc"].ToString().Trim() + "' ");
                }
                else
                {
                    strSql.Append(" null ");
                }
                strSql.Append(") ");
                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_TestItem.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update B_TestItem set ");

                if (dr.Table.Columns["Visible"] != null && dr.Table.Columns["Visible"].ToString().Trim() != "")
                {
                    strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["DispOrder"] != null && dr.Table.Columns["DispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" DispOrder = '" + dr["DispOrder"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["Prec"] != null && dr.Table.Columns["Prec"].ToString().Trim() != "")
                {
                    strSql.Append(" Prec = '" + dr["Prec"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["IsProfile"] != null && dr.Table.Columns["IsProfile"].ToString().Trim() != "")
                {
                    strSql.Append(" IsProfile = '" + dr["IsProfile"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["OrderNo"] != null && dr.Table.Columns["OrderNo"].ToString().Trim() != "")
                {
                    strSql.Append(" OrderNo = '" + dr["OrderNo"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["StandardCode"] != null && dr.Table.Columns["StandardCode"].ToString().Trim() != "")
                {
                    strSql.Append(" StandardCode = '" + dr["StandardCode"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["ItemDesc"] != null && dr.Table.Columns["ItemDesc"].ToString().Trim() != "")
                {
                    strSql.Append(" ItemDesc = '" + dr["ItemDesc"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["FWorkLoad"] != null && dr.Table.Columns["FWorkLoad"].ToString().Trim() != "")
                {
                    strSql.Append(" FWorkLoad = '" + dr["FWorkLoad"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["Secretgrade"] != null && dr.Table.Columns["Secretgrade"].ToString().Trim() != "")
                {
                    strSql.Append(" Secretgrade = '" + dr["Secretgrade"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["Cuegrade"] != null && dr.Table.Columns["Cuegrade"].ToString().Trim() != "")
                {
                    strSql.Append(" Cuegrade = '" + dr["Cuegrade"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["IsDoctorItem"] != null && dr.Table.Columns["IsDoctorItem"].ToString().Trim() != "")
                {
                    strSql.Append(" IsDoctorItem = '" + dr["IsDoctorItem"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["IschargeItem"] != null && dr.Table.Columns["IschargeItem"].ToString().Trim() != "")
                {
                    strSql.Append(" IschargeItem = '" + dr["IschargeItem"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["HisDispOrder"] != null && dr.Table.Columns["HisDispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" HisDispOrder = '" + dr["HisDispOrder"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["SuperGroupNo"] != null && dr.Table.Columns["SuperGroupNo"].ToString().Trim() != "")
                {
                    strSql.Append(" SuperGroupNo = '" + dr["SuperGroupNo"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["Price"] != null && dr.Table.Columns["Price"].ToString().Trim() != "")
                {
                    strSql.Append(" Price = '" + dr["Price"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["SpecialType"] != null && dr.Table.Columns["SpecialType"].ToString().Trim() != "")
                {
                    strSql.Append(" SpecialType = '" + dr["SpecialType"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["SpecialSection"] != null && dr.Table.Columns["SpecialSection"].ToString().Trim() != "")
                {
                    strSql.Append(" SpecialSection = '" + dr["SpecialSection"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["LowPrice"] != null && dr.Table.Columns["LowPrice"].ToString().Trim() != "")
                {
                    strSql.Append(" LowPrice = '" + dr["LowPrice"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["SpecTypeNo"] != null && dr.Table.Columns["SpecTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" SpecTypeNo = '" + dr["SpecTypeNo"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["IsCombiItem"] != null && dr.Table.Columns["IsCombiItem"].ToString().Trim() != "")
                {
                    strSql.Append(" IsCombiItem = '" + dr["IsCombiItem"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["IsHistoryItem"] != null && dr.Table.Columns["IsHistoryItem"].ToString().Trim() != "")
                {
                    strSql.Append(" IsHistoryItem = '" + dr["IsHistoryItem"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["IsNurseItem"] != null && dr.Table.Columns["IsNurseItem"].ToString().Trim() != "")
                {
                    strSql.Append(" IsNurseItem = '" + dr["IsNurseItem"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["DefaultSType"] != null && dr.Table.Columns["DefaultSType"].ToString().Trim() != "")
                {
                    strSql.Append(" DefaultSType = '" + dr["DefaultSType"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["SpecName"] != null && dr.Table.Columns["SpecName"].ToString().Trim() != "")
                {
                    strSql.Append(" SpecName = '" + dr["SpecName"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["zdy1"] != null && dr.Table.Columns["zdy1"].ToString().Trim() != "")
                {
                    strSql.Append(" zdy1 = '" + dr["zdy1"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["zdy2"] != null && dr.Table.Columns["zdy2"].ToString().Trim() != "")
                {
                    strSql.Append(" zdy2 = '" + dr["zdy2"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["code_1"] != null && dr.Table.Columns["code_1"].ToString().Trim() != "")
                {
                    strSql.Append(" code_1 = '" + dr["code_1"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["code_2"] != null && dr.Table.Columns["code_2"].ToString().Trim() != "")
                {
                    strSql.Append(" code_2 = '" + dr["code_2"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["code_3"] != null && dr.Table.Columns["code_3"].ToString().Trim() != "")
                {
                    strSql.Append(" code_3 = '" + dr["code_3"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["EName"] != null && dr.Table.Columns["EName"].ToString().Trim() != "")
                {
                    strSql.Append(" EName = '" + dr["EName"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["StandCode"] != null && dr.Table.Columns["StandCode"].ToString().Trim() != "")
                {
                    strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["ZFStandCode"] != null && dr.Table.Columns["ZFStandCode"].ToString().Trim() != "")
                {
                    strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["ShortName"] != null && dr.Table.Columns["ShortName"].ToString().Trim() != "")
                {
                    strSql.Append(" ShortName = '" + dr["ShortName"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["ShortCode"] != null && dr.Table.Columns["ShortCode"].ToString().Trim() != "")
                {
                    strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["DiagMethod"] != null && dr.Table.Columns["DiagMethod"].ToString().Trim() != "")
                {
                    strSql.Append(" DiagMethod = '" + dr["DiagMethod"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["Unit"] != null && dr.Table.Columns["Unit"].ToString().Trim() != "")
                {
                    strSql.Append(" Unit = '" + dr["Unit"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["IsCalc"] != null && dr.Table.Columns["IsCalc"].ToString().Trim() != "")
                {
                    strSql.Append(" IsCalc = '" + dr["IsCalc"].ToString().Trim() + "'  ");
                }

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where ItemNo='" + dr["ItemNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_TestItem .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        public int DeleteByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (dr.Table.Columns["ItemNo"] != null && dr.Table.Columns["ItemNo"].ToString().Trim() != "")
                {
                    strSql.Append("delete from B_TestItem ");
                    strSql.Append(" where ItemNo='" + dr["ItemNo"].ToString().Trim() + "' ");
                    return idb.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.TestItem .DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }        
        #endregion

        #region IDTestItem 成员


        public DataSet GetList(string strWhere)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        #endregion


        public DataSet getItemCName(string ItemNo)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SuperGroup"></param>
        /// <returns></returns>
        public DataSet getTestItemByCombiItem(string SuperGroup)
        {
            throw new NotImplementedException();

        }


        public bool IsExist(string labCodeNo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            throw new NotImplementedException();
        }


        public int Add(List<Model.TestItem> modelList)
        {
            throw new NotImplementedException();
        }


        public int Update(List<Model.TestItem> modelList)
        {
            throw new NotImplementedException();
        }
    }
}

