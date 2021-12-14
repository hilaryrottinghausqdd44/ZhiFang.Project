using NHibernate.Engine;
using Spring.Data.NHibernate.Generic.Support;
using System;
using System.Collections.Generic;
using System.Data;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.LabStar;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.LabStar.Common;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LisCommonDao : HibernateDaoSupport, IDLisCommonDao
    {

        public int GetMaxNoByFieldNameDao(string entityName, string fieldName)
        {
            int maxNo = 0;
            string labIDHQL = "";
            string[] arrayLabInfo = SysCookieValueHelper.GetLabIDInfoBySysCookie();
            if (arrayLabInfo[0] != null && arrayLabInfo[0].Trim() == "1")
            {
                if (arrayLabInfo[1] != null && arrayLabInfo[1].Trim() != "")
                {
                    labIDHQL = " and a.LabID=" + arrayLabInfo[1] + " ";
                }
            }
            IList<int?> listNo = this.Session.CreateQuery("select Max(a." + fieldName + ") as MaxNo from " + entityName + " a where 1=1 " + labIDHQL).List<int?>();
            if (listNo != null && listNo.Count > 0 && listNo[0] != null)
                maxNo = listNo[0].Value;
            return maxNo;
        }

        public BaseResultDataValue ExecSQLDao(string strSQL)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            using (ISessionFactoryImplementor factory = (ISessionFactoryImplementor)this.SessionFactory)
            {
                using (IDbConnection connection = factory.ConnectionProvider.GetConnection())
                {
                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = strSQL;
                            ZhiFang.LabStar.Common.LogHelp.Info("执行SQL语句：" + strSQL);
                            int recordCount = cmd.ExecuteNonQuery();
                            brdv.ResultDataValue = recordCount.ToString();
                        }
                        catch (Exception ex)
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "SQL语句执行失败：" + strSQL + "  Error：" + ex.Message;
                            ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                        }
                    }
                }
            }
            return brdv;
        }

        public DataSet QuerySQLDao(string strSQL)
        {
            DataSet ds = new DataSet();
            using (ISessionFactoryImplementor factory = (ISessionFactoryImplementor)this.SessionFactory)
            {
                using (IDbConnection connection = factory.ConnectionProvider.GetConnection())
                {
                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = strSQL;
                            ZhiFang.LabStar.Common.LogHelp.Info("执行SQL语句：" + strSQL);
                            IDataReader dataReader = cmd.ExecuteReader();
                            DataTable dt = new DataTable();
                            ds.Tables.Add(dt);
                            dt.Load(dataReader);
                        }
                        catch (Exception ex)
                        {
                            ZhiFang.LabStar.Common.LogHelp.Info("SQL语句执行失败：" + strSQL + "  Error：" + ex.Message);
                        }
                    }
                }
            }
            return ds;
        }
    }
}