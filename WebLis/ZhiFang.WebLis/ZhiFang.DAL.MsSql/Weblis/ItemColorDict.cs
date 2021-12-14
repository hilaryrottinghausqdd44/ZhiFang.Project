using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data.SqlClient;
using System.Data;
using ZhiFang.Model;

namespace ZhiFang.DAL.MsSql.Weblis
{
    public class ItemColorDict : BaseDALLisDB, IDItemColorDict
    {
        public ItemColorDict(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public ItemColorDict()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        public System.Data.DataSet GetList(Model.ItemColorDict model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ColorID, ColorName,ColorValue");
            strSql.Append(" FROM ItemColorDict where 1=1");
            if (model != null)
            {
                if (model.ColorID != null)
                    strSql.Append(" and ColorID=" + model.ColorID + "");
                if (model.ColorName != null)
                    strSql.Append(" and ColorName=" + model.ColorName + "");
                if (model.ColorValue != null)
                    strSql.Append(" and ColorValue=" + model.ColorValue + "");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Add(Model.ItemColorDict model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ItemColorDict(");
            strSql.Append("ColorName,ColorValue)");
            strSql.Append(" values (");
            strSql.Append("@ColorName,@ColorValue)");
            //strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ColorName", SqlDbType.VarChar,15),
					new SqlParameter("@ColorValue", SqlDbType.VarChar,15)};
            parameters[0].Value = model.ColorName;
            parameters[1].Value = model.ColorValue;

            object obj = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);//.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Update(Model.ItemColorDict model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ItemColorDict set ");
            //if (model.ColorID != null)
            //{
            //    strSql.Append("ColorID='" + model.ColorID + "',");
            //}
            if (model.ColorName != null)
            {
                strSql.Append("ColorName='" + model.ColorName + "',");
            }
            if (model.ColorValue != null)
            {
                strSql.Append("ColorValue='" + model.ColorValue + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ColorID=" + model.ColorID + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ColorID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ItemColorDict");
            strSql.Append(" where ColorID=" + ColorID + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ColorID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ItemColorDict ");
            strSql.Append(" where ColorID=" + ColorID + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        public System.Data.DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ColorID, ColorName,ColorValue");
            strSql.Append(" FROM ItemColorDict where 1=1");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            //throw new NotImplementedException();
        }

        public Model.ItemColorDict GetModel(int colorId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" ColorID, ColorName,ColorValue ");
            strSql.Append(" from ItemColorDict ");
            strSql.Append(" where ColorID=" + colorId + " ");
            Model.ItemColorDict model = new Model.ItemColorDict();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ColorID"].ToString() != "")
                {
                    model.ColorID = int.Parse(ds.Tables[0].Rows[0]["ColorID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ColorName"].ToString() != "")
                {
                    model.ColorName = ds.Tables[0].Rows[0]["ColorName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ColorID"].ToString() != "")
                {
                    model.ColorValue = ds.Tables[0].Rows[0]["ColorValue"].ToString();
                }
                return model;

            }
            else
                return null;
        }
        public int AddUpdateByDataSet(System.Data.DataSet ds)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.ItemColorDict t)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetListByPage(Model.ItemColorDict t, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        public Model.ItemColorDict GetModelByColorName(string colorName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ColorID,ColorName,ColorValue from itemcolordict ");
            //strSql.Append(" where ColorName =@ColorName ");
            //TestItem表中Color存的是颜色编号，B_lab_TestItem 表中Color存的是颜色名称。所以和itemcolordict关联要加多个条件or ganwh edit 2015-12-21
            string strWhere = "";
            int result = 0;
            if (int.TryParse(colorName, out result))
            {
                strWhere = " where ColorID=@ColorName";
            }
            else {
                strWhere = " where ColorName =@ColorName";
            }
            strSql.Append(strWhere);
            SqlParameter[] parameters = {
					new SqlParameter("@ColorName", SqlDbType.VarChar,15)
					};
            parameters[0].Value = colorName;

            ZhiFang.Model.ItemColorDict model = new ZhiFang.Model.ItemColorDict();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ColorID"].ToString() != "")
                {
                    model.ColorID = int.Parse(ds.Tables[0].Rows[0]["ColorID"].ToString());
                }
                model.ColorName = ds.Tables[0].Rows[0]["ColorName"].ToString();
                model.ColorValue = ds.Tables[0].Rows[0]["ColorValue"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM ItemColorDict ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
                strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(int Top, Model.ItemColorDict t, string filedOrder)
        {
            throw new NotImplementedException();
        }
    }
}
