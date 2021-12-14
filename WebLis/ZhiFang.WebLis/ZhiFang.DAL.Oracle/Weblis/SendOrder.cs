using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
namespace ZhiFang.DAL.Oracle.weblis
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
            strSql.Append(" where OrderNo ='" + OrderNo + "' ");


            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rows;
        }

        public Model.SendOrder GetModel(string OrderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  OrderNo,CreateMan,CreateDate,SampleNum,Status,Note,LabCode from SendOrder ");
            strSql.Append(" where OrderNo= '" + OrderNo + "' and rownum=1 ");


            Model.SendOrder model = new Model.SendOrder();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
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
                    strSql.Append(" and CreateDate >=to_date('" + model.OrderStartDate + "','YYYY-MM-DD HH24:MI:SS') ");
                if (model.OrderEndDate != null && model.OrderEndDate != "")
                    strSql.Append(" and CreateDate <=to_date('" + model.OrderEndDate + " 23:59:59" + "','YYYY-MM-DD HH24:MI:SS') ");
                if (model.SampleNum != null)
                    strSql.Append(" and SampleNum='" + model.SampleNum + "' ");
                if (model.Status != null)
                    strSql.Append(" and Status='" + model.Status + "' ");
                if (model.Note != null && model.Note != "")
                    strSql.Append(" and Note='" + model.Note + "' ");
                if (model.LabCode != null && model.LabCode != "")
                    strSql.Append(" and LabCode='" + model.LabCode + "' ");
                if (model.IsConfirm != null)
                    strSql.Append(" and IsConfirm='" + model.IsConfirm + "' ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public string ExecStoredProcedure(string LabCode, string Operdate)
        {
            OracleParameter[] parameters = {
                    new OracleParameter("LabCode",  OracleType.VarChar,50),
                    new OracleParameter("OperDate", OracleType.VarChar,10),
					new OracleParameter("SN", OracleType.VarChar,100)};

            parameters[0].Value = LabCode;
            parameters[1].Value = Convert.ToDateTime(Operdate).ToString("yyyy-MM-dd");
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
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.OrderNo != null)
            {
                strSql1.Append("OrderNo,");
                strSql2.Append("'" + model.OrderNo + "',");
            }
            if (model.CreateMan != null)
            {
                strSql1.Append("CreateMan,");
                strSql2.Append("'" + model.CreateMan + "',");
            }
            if (model.CreateDate != null)
            {
                strSql1.Append("CreateDate,");
                strSql2.Append("to_date('" + model.CreateDate.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.SampleNum != null)
            {
                strSql1.Append("SampleNum,");
                strSql2.Append("" + model.SampleNum + ",");
            }
            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("" + model.Status + ",");
            }
            if (model.Note != null)
            {
                strSql1.Append("Note,");
                strSql2.Append("'" + model.Note + "',");
            }
            if (model.LabCode != null)
            {
                strSql1.Append("LabCode,");
                strSql2.Append("'" + model.LabCode + "',");
            }
            if (model.IsConfirm != null)
            {
                strSql1.Append("ISCONFIRM,");
                strSql2.Append("" + model.IsConfirm + ",");
            }
            strSql.Append("insert into SENDORDER(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        public int Update(Model.SendOrder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SendOrder set ");
            strSql.Append("CreateMan='" + model.CreateMan + "',");
            strSql.Append("CreateDate=to_date('" + model.CreateDate + "','YYYY-MM-DD HH24:MI:SS'),");
            strSql.Append("SampleNum=" + model.SampleNum + ",");
            strSql.Append("Status=" + model.Status + ",");
            strSql.Append("Note='" + model.Note + "',");
            strSql.Append("LabCode='" + model.LabCode + "'");
            strSql.Append(" where OrderNo='" + model.OrderNo + "' ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        public int UpdateNoteByOrderNo(string OrderNo, string Note)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update  SendOrder ");
            strSql.Append(" set Note='" + Note + "' ");
            strSql.Append(" where OrderNo='" + OrderNo + "' ");

            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
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


        public Model.SendOrder GetModelByOrderPrint(string OrderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select CLIENTELE.CNAME, SendOrder.* from SendOrder inner join CLIENTELE on SendOrder.LabCode= CLIENTELE.ClIENTNO ");
            strSql.Append(" where OrderNo='" + OrderNo + "' ");
          

            Model.SendOrder model = new Model.SendOrder();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
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
    }
}
