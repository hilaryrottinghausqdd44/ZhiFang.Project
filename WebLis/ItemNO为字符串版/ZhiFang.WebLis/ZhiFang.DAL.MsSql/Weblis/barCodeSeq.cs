using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data;
using System.Data.SqlClient;
namespace ZhiFang.DAL.MsSql.Weblis
{
    public class barCodeSeq : BaseDALLisDB, IDbarCodeSeq
    {
        public barCodeSeq(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public barCodeSeq()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="LabCode"></param>
        /// <param name="Operdate"></param>
        /// <returns></returns>
        public bool Exists(string LabCode, string Operdate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from barCodeSeq");
            strSql.Append(" where LabCode=" + LabCode + " ");
            strSql.Append(" and Date=" + Operdate + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        public DataSet GetList(Model.barCodeSeq model)
        {
            throw new NotImplementedException();
        }

        public int UpdateByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        public int AddByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Add(Model.barCodeSeq t)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.barCodeSeq t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, Model.barCodeSeq t, string filedOrder)
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

        public int GetTotalCount(Model.barCodeSeq t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListByPage(Model.barCodeSeq t, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="LabCode"></param>
        /// <param name="Operdate"></param>
        /// <returns></returns>
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

        public bool Exists(string LabCode, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
