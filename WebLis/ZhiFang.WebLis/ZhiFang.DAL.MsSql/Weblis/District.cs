using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DBUtility;
using System.Data;

namespace ZhiFang.DAL.MsSql.Weblis
{
    /// <summary>
    /// 数据访问类District。
    /// </summary>
    public class District : BaseDALLisDB, IDDistrict, IDBatchCopy
    {
        public District(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public District()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("DistrictNo", "District");
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DistrictNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from District");
            strSql.Append(" where DistrictNo=" + DistrictNo + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.District model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.DistrictNo != null)
            {
                strSql1.Append("DistrictNo,");
                strSql2.Append("" + model.DistrictNo + ",");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.ShortName != null)
            {
                strSql1.Append("ShortName,");
                strSql2.Append("'" + model.ShortName + "',");
            }
            if (model.ShortCode != null)
            {
                strSql1.Append("ShortCode,");
                strSql2.Append("'" + model.ShortCode + "',");
            }
            if (model.Visible != null)
            {
                strSql1.Append("Visible,");
                strSql2.Append("" + model.Visible + ",");
            }
            if (model.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("" + model.DispOrder + ",");
            }
            if (model.HisOrderCode != null)
            {
                strSql1.Append("HisOrderCode,");
                strSql2.Append("'" + model.HisOrderCode + "',");
            }
            strSql.Append("insert into District(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.District model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update District set ");
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.ShortName != null)
            {
                strSql.Append("ShortName='" + model.ShortName + "',");
            }
            if (model.ShortCode != null)
            {
                strSql.Append("ShortCode='" + model.ShortCode + "',");
            }
            if (model.Visible != null)
            {
                strSql.Append("Visible=" + model.Visible + ",");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append("HisOrderCode='" + model.HisOrderCode + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where DistrictNo=" + model.DistrictNo + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int DistrictNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from District ");
            strSql.Append(" where DistrictNo=" + DistrictNo + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.District GetModel(int DistrictNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" DistrictNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode  ");
            strSql.Append(" from District ");
            strSql.Append(" where DistrictNo=" + DistrictNo + " ");
            Model.District model = new Model.District();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DistrictNo"].ToString() != "")
                {
                    model.DistrictNo = int.Parse(ds.Tables[0].Rows[0]["DistrictNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.ShortName = ds.Tables[0].Rows[0]["ShortName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }
                model.HisOrderCode = ds.Tables[0].Rows[0]["HisOrderCode"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DistrictNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode  ");
            strSql.Append(" FROM District ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(ZhiFang.Model.District model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DistrictNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode  ");
            strSql.Append(" FROM District where 1=1");
            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "'");
            }
            if (model.ShortName != null)
            {
                strSql.Append(" and ShortName='" + model.ShortName + "'");
            }
            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "'");
            }
            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + "");
            }
            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + "");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "'");
            }
            if (model.DistrictNo != null)
            {
                strSql.Append(" and DistrictNo='" + model.DistrictNo + "'");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
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
            strSql.Append(" DistrictNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode ");
            strSql.Append(" FROM District ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /*
        */

        #endregion  成员方法

        #region IDataBase<District> 成员


        public DataSet GetList(int Top, Model.District model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" DistrictNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode  ");
            strSql.Append(" FROM District where 1=1");
            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "'");
            }
            if (model.ShortName != null)
            {
                strSql.Append(" and ShortName='" + model.ShortName + "'");
            }
            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "'");
            }
            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + "");
            }
            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + "");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "'");
            }
            if (model.DistrictNo != null)
            {
                strSql.Append(" and DistrictNo='" + model.DistrictNo + "'");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select * from District");
            //return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            return GetList("");
        }

        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM District");
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
            //return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }

        public int GetTotalCount(Model.District model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM District where 1=1 ");
            string strLike = "";
            if (model != null && model.SearchLikeKey != null)
            {
                strLike = " and (DistrictNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
            }
            strSql.Append(strLike);
            strSql.Append(strWhere.ToString());
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
           
        }

        #endregion

        #region IDDistrict 成员


        public int DeleteList(string DistrictIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from District ");
            strSql.Append(" where ID in (" + DistrictIDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        #endregion

        #region IDataPage<District> 成员

        /// <summary>
        /// 利用主键分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.District model, int nowPageNum, int nowPageSize)
        {
            string strLike = "";
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                if (model.SearchLikeKey != null)
                {
                    strLike = " and (District.DistrictNo like '%" + model.SearchLikeKey + "%' or District.CName like '%" + model.SearchLikeKey + "%' or District.ShortCode like '%" + model.SearchLikeKey + "%') ";
                }
                string strOrderBy = "";
                if (model.OrderField == "DistrictID")
                {
                    strOrderBy = "District.DistrictNo";
                }
                else if (model.OrderField.ToLower().IndexOf("control") >= 0)
                {
                    strOrderBy = "B_DistrictControl." + model.OrderField;
                }
                else
                {
                    strOrderBy = "District." + model.OrderField;
                }
                strSql.Append(" select top " + nowPageSize + "  * from District left join B_DistrictControl on District.DistrictNo=B_DistrictControl.DistrictNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_DistrictControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where District.DistrictNo not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + "District.DistrictNo from  District left join B_DistrictControl on District.DistrictNo=B_DistrictControl.DistrictNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_DistrictControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" where 1=1 " + strLike + " order by " + strOrderBy + " desc ) " + strLike + " order by " + strOrderBy + " desc ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
     
            else
            {
                if (model.SearchLikeKey != null)
                {
                    strLike = " and (DistrictNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%' or ShortName like '%"+model.SearchLikeKey+"%') ";
                }
                strSql.Append("select top " + nowPageSize + "  * from District where DistrictNo not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " DistrictNo from District where 1=1 " + strLike + " order by " + model.OrderField + " desc ) " + strLike + " order by " + model.OrderField + " desc ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
          

        }

        #endregion

        #region IDBatchCopy 成员

        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            try
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    string str = GetControlItems(lst[i].Trim());
                    strSql.Append("insert into B_Lab_District ( LabCode,");
                    strSql.Append(" LabDistrictNo , CName , ShortName , ShortCode , Visible , DispOrder , HisOrderCode ");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("DistrictNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode");
                    strSql.Append(" from District ");
                    if (str.Trim() != "")
                        strSql.Append(" where DistrictNo not in (" + str + ")");

                    strSqlControl.Append("insert into B_DistrictControl ( ");
                    strSqlControl.Append(" DistrictControlNo,DistrictNo,ControlLabNo,ControlDistrictNo,UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50),DistrictNo)+'_'+convert(varchar(50), DistrictNo) as DistrictControlNo,DistrictNo,'" + lst[i].Trim() + "' as ControlLabNo,DistrictNo,1 ");
                    strSqlControl.Append(" from District ");
                    if (str.Trim() != "")
                        strSqlControl.Append(" where DistrictNo not in (" + str + ")");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();

                    //d_log.OperateLog("District", "", "", DateTime.Now, 1);
                }

                DbHelperSQL.BatchUpdateWithTransaction(arrySql);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public string GetControlItems(string strLabCode)
        {
            string str = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DistrictNo from B_DistrictControl where ControlLabNo=" + strLabCode);
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (str == "")
                        str = "'" + dr["DistrictNo"].ToString().Trim() + "'";
                    else
                        str += ",'" + dr["DistrictNo"].ToString().Trim() + "'";
                }
            }
            return str;
        }

        #endregion

        #region IDBatchCopy 成员

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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["DistrictNo"].ToString().Trim())))
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
                strSql.Append("insert into District (");
                strSql.Append("DistrictNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode,");
                strSql.Append(") values (");
                if (dr.Table.Columns["DistrictNo"] != null && dr.Table.Columns["DistrictNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DistrictNo"].ToString().Trim() + "', ");
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
                if (dr.Table.Columns["HisOrderCode"] != null && dr.Table.Columns["HisOrderCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["HisOrderCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update District set ");


                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ShortName"] != null && dr.Table.Columns["ShortName"].ToString().Trim() != "")
                {
                    strSql.Append(" ShortName = '" + dr["ShortName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ShortCode"] != null && dr.Table.Columns["ShortCode"].ToString().Trim() != "")
                {
                    strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["Visible"] != null && dr.Table.Columns["Visible"].ToString().Trim() != "")
                {
                    strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["DispOrder"] != null && dr.Table.Columns["DispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" DispOrder = '" + dr["DispOrder"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["HisOrderCode"] != null && dr.Table.Columns["HisOrderCode"].ToString().Trim() != "")
                {
                    strSql.Append(" HisOrderCode = '" + dr["HisOrderCode"].ToString().Trim() + "' , ");
                }
                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where DistrictNo='" + dr["DistrictNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public int DeleteByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (dr.Table.Columns["DistrictNo"] != null && dr.Table.Columns["DistrictNo"].ToString().Trim() != "")
                {
                    strSql.Append("delete from District ");
                    strSql.Append(" where DistrictNo='" + dr["DistrictNo"].ToString().Trim() + "' ");
                    return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.weblis.District .DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }

        #endregion


        public bool IsExist(string labCodeNo)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select COUNT(1) from B_Lab_District ");
            strSql.Append(" where LabCode=" + labCodeNo + " ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" select COUNT(1) from B_DistrictControl ");
            strSql2.Append(" where ControlLabNo=" + labCodeNo + " ");

            if (DbHelperSQL.Exists(strSql.ToString()) )
            {
                result = true;
            }
            return result;
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            bool result = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" delete from B_Lab_District ");
            strSql.Append(" where LabCode=" + LabCodeNo + " ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" delete from B_DistrictControl ");
            strSql2.Append(" where ControlLabNo=" + LabCodeNo + " ");


            int i = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            int j = DbHelperSQL.ExecuteNonQuery(strSql2.ToString());
            if (i > 0 || j > 0)
                result = true;
            return result;
        }
    }
}
