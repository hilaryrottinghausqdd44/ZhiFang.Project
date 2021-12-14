using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data;
using System.Data.SqlClient;
namespace ZhiFang.DAL.MsSql.Weblis
{
    public partial class SendOrder : BaseDALLisDB, IDSendOrder
    {
        public SendOrder(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public SendOrder()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        public bool Exists(string OrderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SendOrder");
            strSql.Append(" where OrderNo='" + OrderNo + "'");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        public int Delete(string OrderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SendOrder ");
            strSql.Append(" where OrderNo=@OrderNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,30)			};
            parameters[0].Value = OrderNo;

            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
            return rows;
        }

        public Model.SendOrder GetModel(string OrderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 OrderNo,CreateMan,CreateDate,SampleNum,Status,Note,LabCode,IsConfirm from SendOrder ");
            strSql.Append(" where OrderNo=@OrderNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,30)			};
            parameters[0].Value = OrderNo;

            Model.SendOrder model = new Model.SendOrder();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public Model.SendOrder GetModelByOrderPrint(string OrderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select dbo.CLIENTELE.CNAME, SendOrder.* from SendOrder inner join dbo.CLIENTELE on SendOrder.LabCode= dbo.CLIENTELE.ClIENTNO ");
            strSql.Append(" where OrderNo=@OrderNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,30)			};
            parameters[0].Value = OrderNo;

            Model.SendOrder model = new Model.SendOrder();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModelByOrderPrint(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public Model.SendOrder DataRowToModelByOrderPrint(DataRow row)
        {
            Model.SendOrder model = new Model.SendOrder();
            if (row != null)
            {
                if (row["OrderNo"] != null)
                {
                    model.OrderNo = row["OrderNo"].ToString();
                }
                if (row["CreateMan"] != null)
                {
                    model.CreateMan = row["CreateMan"].ToString();
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = row["CreateDate"].ToString();
                }
                if (row["SampleNum"] != null && row["SampleNum"].ToString() != "")
                {
                    model.SampleNum = int.Parse(row["SampleNum"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Note"] != null)
                {
                    model.Note = row["Note"].ToString();
                }
                if (row["LabCode"] != null)
                {
                    model.LabCode = row["LabCode"].ToString();
                }
                if (row["CNAME"] != null)
                    model.SendLabCodeName = row["CNAME"].ToString();
                if (row["isConfirm"] != null)
                    model.IsConfirm = int.Parse(row["isConfirm"].ToString());
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.SendOrder DataRowToModel(DataRow row)
        {
            Model.SendOrder model = new Model.SendOrder();
            if (row != null)
            {
                if (row["OrderNo"] != null)
                {
                    model.OrderNo = row["OrderNo"].ToString();
                }
                if (row["CreateMan"] != null)
                {
                    model.CreateMan = row["CreateMan"].ToString();
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = row["CreateDate"].ToString();
                }
                if (row["SampleNum"] != null && row["SampleNum"].ToString() != "")
                {
                    model.SampleNum = int.Parse(row["SampleNum"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Note"] != null)
                {
                    model.Note = row["Note"].ToString();
                }
                if (row["LabCode"] != null)
                {
                    model.LabCode = row["LabCode"].ToString();
                }
                if (row["isConfirm"] != null)
                    model.IsConfirm = int.Parse(row["isConfirm"].ToString());
            }
            return model;
        }

        public DataSet GetList(Model.SendOrder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SendOrder ");
            strSql.Append(" where 1=1 ");
            if (model != null)
            {
                if (model.OrderNo != null && model.OrderNo != "")
                {
                    strSql.Append(" and OrderNo='" + model.OrderNo + "' ");
                }

                if (model.CreateMan != null && model.CreateMan != "")
                    strSql.Append(" and CreateMan='" + model.CreateMan + "' ");
                //if (model.CreateDate != null)
                //    strSql.Append(" and CreateDate='" + model.CreateDate + "' ");
                if (model.OrderStartDate != null && model.OrderStartDate != "")
                    strSql.Append(" and CreateDate >='" + model.OrderStartDate + "' ");
                if (model.OrderEndDate != null && model.OrderEndDate != "")
                    strSql.Append(" and CreateDate <='" + model.OrderEndDate + " 23:59:59" + "' ");
                if (model.SampleNum != null)
                    strSql.Append(" and SampleNum='" + model.SampleNum + "' ");
                if (model.Status != null)
                    strSql.Append(" and Status='" + model.Status + "' ");
                if (model.Note != null && model.Note != "")
                    strSql.Append(" and Note='" + model.Note + "' ");
                if (model.LabCode != null && model.LabCode != "")
                    strSql.Append(" and LabCode='" + model.LabCode + "' ");
                //if (model.IsConfirm != null)
                //    strSql.Append(" and IsConfirm='" + model.IsConfirm + "' ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public string ExecStoredProcedure(string LabCode, string Operdate)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@LabCode", SqlDbType.VarChar,50),
                    new SqlParameter("@OperDate", SqlDbType.DateTime),
					new SqlParameter("@SN", SqlDbType.VarChar,10)};

            parameters[0].Value = LabCode;
            parameters[1].Value = Operdate;
            parameters[2].Direction = ParameterDirection.Output;

            DbHelperSQL.ExecStoredProcedure("P_GetMaxBarCodeSeq", parameters);
            return parameters[2].Value.ToString();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Add(Model.SendOrder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SendOrder(");
            strSql.Append("OrderNo,CreateMan,CreateDate,SampleNum,Status,Note,LabCode)");
            strSql.Append(" values (");
            strSql.Append("@OrderNo,@CreateMan,@CreateDate,@SampleNum,@Status,@Note,@LabCode)");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,30),
					new SqlParameter("@CreateMan", SqlDbType.VarChar,20),
					new SqlParameter("@CreateDate", SqlDbType.Date),
					new SqlParameter("@SampleNum", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Note", SqlDbType.VarChar,50),
					new SqlParameter("@LabCode", SqlDbType.VarChar,25)};
            parameters[0].Value = model.OrderNo;
            parameters[1].Value = model.CreateMan;
            parameters[2].Value = model.CreateDate;
            parameters[3].Value = model.SampleNum;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.Note;
            parameters[6].Value = model.LabCode;

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        public int Update(Model.SendOrder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SendOrder set ");
            strSql.Append("CreateMan=@CreateMan,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("SampleNum=@SampleNum,");
            strSql.Append("Status=@Status,");
            strSql.Append("Note=@Note,");
            strSql.Append("LabCode=@LabCode");
            strSql.Append(" where OrderNo=@OrderNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@CreateMan", SqlDbType.VarChar,20),
					new SqlParameter("@CreateDate", SqlDbType.Date),
					new SqlParameter("@SampleNum", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Note", SqlDbType.VarChar,50),
					new SqlParameter("@LabCode", SqlDbType.VarChar,25),
					new SqlParameter("@OrderNo", SqlDbType.VarChar,30)};
            parameters[0].Value = model.CreateMan;
            parameters[1].Value = model.CreateDate;
            parameters[2].Value = model.SampleNum;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.Note;
            parameters[5].Value = model.LabCode;
            parameters[6].Value = model.OrderNo;

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        public int UpdateNoteByOrderNo(string OrderNo, string Note)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update  SendOrder ");
            strSql.Append(" set Note=@Note ");
            strSql.Append(" where OrderNo=@OrderNo ");
            SqlParameter[] parameters = {					
			new SqlParameter("@Note", SqlDbType.VarChar,50),
            new SqlParameter("@OrderNo", SqlDbType.VarChar,30)
                                        };
            parameters[0].Value = Note;
            parameters[1].Value = OrderNo;

            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
            return rows;
        }
        public DataSet GetList(int Top, Model.SendOrder t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.SendOrder t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListByPage(Model.SendOrder t, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public int OrderConFrim(string OrderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update  SendOrder  set IsConfirm='1'");
            strSql.Append(" where OrderNo='" + OrderNo + "'");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 确认打印
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public int ConFrimPrint(string OrderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update  SendOrder  set Status='2'");
            strSql.Append(" where OrderNo='" + OrderNo + "'");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }
    }
}
