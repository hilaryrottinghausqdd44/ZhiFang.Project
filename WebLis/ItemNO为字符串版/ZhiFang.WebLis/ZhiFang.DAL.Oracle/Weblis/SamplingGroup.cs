using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data.SqlClient;
using System.Data;

namespace ZhiFang.DAL.Oracle.weblis
{
    public partial class SamplingGroup : BaseDALLisDB, IDSamplingGroup
    {
        public SamplingGroup(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public SamplingGroup()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        public bool Exists(string SamplingGroupNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SamplingGroup");
            strSql.Append(" where SampleTypeNo ='" + SamplingGroupNo + "'");
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
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
            strSql.Append("select count(1) from SamplingGroup where 1=1 ");
            if (ht.Count > 0)
            {
                foreach (System.Collections.DictionaryEntry item in ht)
                {
                    strSql.Append(" and " + item.Key.ToString().Trim() + "='" + item.Value + "' ");
                }
                string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
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

        public int AddByDataRow(System.Data.DataRow dr)
        {
            throw new NotImplementedException();
        }

        public int UpdateByDataRow(System.Data.DataRow dr)
        {
            throw new NotImplementedException();
        }

        public int Delete(string SamplingGroupNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SamplingGroup ");
            strSql.Append(" where SamplingGroupNo=@SamplingGroupNo");
            SqlParameter[] parameters = {
					new SqlParameter("@SamplingGroupNo", SqlDbType.Int,4)
			};
            parameters[0].Value = SamplingGroupNo;


            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return rows;
            }
            else
            {
                return 0;
            }
        }

        public int DeleteList(string Idlist)
        {
            throw new NotImplementedException();
        }

        public Model.SamplingGroup GetModel(string SamplingGroupNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SamplingGroupNo, capability, Unit, AppCap, synopsis, disporder, mincapability, PrinterName, ShortCode2, PrintCount, SamplingGroupName, SampleTypeNo, CubeType, CubeColor, SpecialtyType, ShortName, ShortCode, destination  ");
            strSql.Append("  from SamplingGroup ");
            strSql.Append(" where SamplingGroupNo=@SamplingGroupNo");
            SqlParameter[] parameters = {
					new SqlParameter("@SamplingGroupNo", SqlDbType.Int,4)
			};
            parameters[0].Value = SamplingGroupNo;


            Model.SamplingGroup model = new Model.SamplingGroup();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SamplingGroupNo"].ToString() != "")
                {
                    model.SamplingGroupNo = int.Parse(ds.Tables[0].Rows[0]["SamplingGroupNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["capability"].ToString() != "")
                {
                    model.capability = decimal.Parse(ds.Tables[0].Rows[0]["capability"].ToString());
                }
                model.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                if (ds.Tables[0].Rows[0]["AppCap"].ToString() != "")
                {
                    model.AppCap = decimal.Parse(ds.Tables[0].Rows[0]["AppCap"].ToString());
                }
                model.synopsis = ds.Tables[0].Rows[0]["synopsis"].ToString();
                if (ds.Tables[0].Rows[0]["disporder"].ToString() != "")
                {
                    model.disporder = int.Parse(ds.Tables[0].Rows[0]["disporder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["mincapability"].ToString() != "")
                {
                    model.mincapability = decimal.Parse(ds.Tables[0].Rows[0]["mincapability"].ToString());
                }
                model.PrinterName = ds.Tables[0].Rows[0]["PrinterName"].ToString();
                model.ShortCode2 = ds.Tables[0].Rows[0]["ShortCode2"].ToString();
                if (ds.Tables[0].Rows[0]["PrintCount"].ToString() != "")
                {
                    model.PrintCount = int.Parse(ds.Tables[0].Rows[0]["PrintCount"].ToString());
                }
                model.SamplingGroupName = ds.Tables[0].Rows[0]["SamplingGroupName"].ToString();
                if (ds.Tables[0].Rows[0]["SampleTypeNo"].ToString() != "")
                {
                    model.SampleTypeNo = int.Parse(ds.Tables[0].Rows[0]["SampleTypeNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CubeType"].ToString() != "")
                {
                    model.CubeType = int.Parse(ds.Tables[0].Rows[0]["CubeType"].ToString());
                }
                model.CubeColor = ds.Tables[0].Rows[0]["CubeColor"].ToString();
                if (ds.Tables[0].Rows[0]["SpecialtyType"].ToString() != "")
                {
                    model.SpecialtyType = int.Parse(ds.Tables[0].Rows[0]["SpecialtyType"].ToString());
                }
                model.ShortName = ds.Tables[0].Rows[0]["ShortName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                model.destination = ds.Tables[0].Rows[0]["destination"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Add(Model.SamplingGroup t)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.SamplingGroup t)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetList(Model.SamplingGroup SamplingGroup)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM SamplingGroup where 1=1 ");
                if (SamplingGroup.SampleTypeNo != null)
                {
                    strSql.Append(" and SampleTypeNo=" + SamplingGroup.SampleTypeNo + " ");
                }
                if (SamplingGroup.SamplingGroupName != null)
                {
                    strSql.Append(" and SamplingGroupName='" + SamplingGroup.SamplingGroupName + "' ");
                }
                if (SamplingGroup.ShortCode != null)
                {
                    strSql.Append(" and ShortCode='" + SamplingGroup.ShortCode + "' ");
                }
                if (SamplingGroup.CubeType != null)
                {
                    strSql.Append(" and CubeType=" + SamplingGroup.CubeType + " ");
                }
                if (SamplingGroup.CubeColor != null)
                {
                    strSql.Append(" and CubeColor='" + SamplingGroup.CubeColor + "' ");
                }
                if (SamplingGroup.capability != null)
                {
                    strSql.Append(" and capability='" + SamplingGroup.capability + "' ");
                }
                if (SamplingGroup.AppCap != null)
                {
                    strSql.Append(" and AppCap='" + SamplingGroup.AppCap + "' ");
                }
                if (SamplingGroup.ShortName != null)
                {
                    strSql.Append(" and ShortName='" + SamplingGroup.ShortName + "' ");
                }
                Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception ex)
            {
                Common.Log.Log.Error(ex.ToString());
                return null;
            }
        }

        public System.Data.DataSet GetList(int Top, Model.SamplingGroup t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        public int AddUpdateByDataSet(System.Data.DataSet ds)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.SamplingGroup t)
        {
            throw new NotImplementedException();
        }
    }
}
