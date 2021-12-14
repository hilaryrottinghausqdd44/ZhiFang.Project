using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DBUtility;
using System.Data;

namespace ZhiFang.DAL.Oracle.weblis
{
    /// <summary>
    /// 数据访问类Doctor。
    /// </summary>
    public class Doctor : BaseDALLisDB, IDDoctor, IDBatchCopy
    {
        public Doctor(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public Doctor()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("DoctorNo", "Doctor");
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DoctorNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Doctor");
            strSql.Append(" where DoctorNo=" + DoctorNo + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.DoctorNo != null)
            {
                strSql1.Append("DoctorNo,");
                strSql2.Append("" + model.DoctorNo + ",");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.ShortCode != null)
            {
                strSql1.Append("ShortCode,");
                strSql2.Append("'" + model.ShortCode + "',");
            }
            if (model.HisOrderCode != null)
            {
                strSql1.Append("HisOrderCode,");
                strSql2.Append("'" + model.HisOrderCode + "',");
            }
            if (model.Visible != null)
            {
                strSql1.Append("Visible,");
                strSql2.Append("" + model.Visible + ",");
            }
            strSql.Append("insert into Doctor(");
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
        public int Update(ZhiFang.Model.Doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Doctor set ");
            if (model.ShortCode != null)
            {
                strSql.Append("ShortCode='" + model.ShortCode + "',");
            }
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append("HisOrderCode='" + model.HisOrderCode + "',");
            }
            if (model.Visible != null)
            {
                strSql.Append("Visible=" + model.Visible + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where DoctorNo=" + model.DoctorNo + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int DoctorNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Doctor ");
            strSql.Append(" where DoctorNo=" + DoctorNo + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Doctor GetModel(int DoctorNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  ");
            strSql.Append(" DoctorNo,CName,ShortCode,HisOrderCode,Visible ");
            strSql.Append(" from Doctor ");
            strSql.Append(" where ROWNUM <='1' and DoctorNo=" + DoctorNo + " ");
            Model.Doctor model = new Model.Doctor();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DoctorNo"].ToString() != "")
                {
                    model.DoctorNo = int.Parse(ds.Tables[0].Rows[0]["DoctorNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                model.HisOrderCode = ds.Tables[0].Rows[0]["HisOrderCode"].ToString();
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }
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
            strSql.Append("select DoctorNo,CName,ShortCode,HisOrderCode,Visible ");
            strSql.Append(" FROM Doctor ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(ZhiFang.Model.Doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DoctorNo,CName,ShortCode,HisOrderCode,Visible ");
            strSql.Append(" FROM Doctor where 1=1");

            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "'");
            }
            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + "");
            }
            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "'");
            }
            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "'");
            }
            if (model.DoctorNo != null)
            {
                strSql.Append("and DoctorNo='" + model.DoctorNo + "'");
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

            strSql.Append(" DoctorNo,CName,ShortCode,HisOrderCode,Visible ");
            strSql.Append(" FROM Doctor ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                strSql.Append(" and ROWNUM <= '" + Top + "'");
            }
            else
            {
                strSql.Append(" where ROWNUM <= '" + Top + "'");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /*
        */

        #endregion  成员方法

        #region IDataBase<Doctor> 成员


        public DataSet GetList(int Top, Model.Doctor model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");

            strSql.Append(" DoctorNo,CName,ShortCode,HisOrderCode,Visible ");
            strSql.Append(" FROM Doctor where 1=1");
            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "'");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "'");
            }
            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + "");
            }
            if (model.ShortCode != null)
            {
                strSql.Append(" and DoctorNo='" + model.DoctorNo + "'");
            }
            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "'");
            }

            strSql.Append(" and ROWNUM <= '" + Top + "'");

            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Doctor");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM Doctor");
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

        public int GetTotalCount(Model.Doctor model)
        {



            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM Doctor where 1=1 ");
            string strLike = "";
            if (model != null && model.SearchLikeKey != null)
            {
                strLike = " and (DoctorNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
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

        #region IDDoctor 成员


        public int DeleteList(string DoctorIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Doctor ");
            strSql.Append(" where ID in (" + DoctorIDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        #endregion

        #region IDataPage<Doctor> 成员

        /// <summary>
        /// 利用主键分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.Doctor model, int nowPageNum, int nowPageSize)
        {
            string strLike = "";
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                if (model.SearchLikeKey != null)
                {
                    strLike = " and (Doctor.DoctorNo like '%" + model.SearchLikeKey + "%' or Doctor.CName like '%" + model.SearchLikeKey + "%' or Doctor.ShortCode like '%" + model.SearchLikeKey + "%') ";
                }
                string strOrderBy = "";
                if (model.OrderField == "DoctorID")
                {
                    strOrderBy = "Doctor.DoctorNo";
                }
                else if (model.OrderField.ToLower().IndexOf("control") >= 0)
                {
                    strOrderBy = "B_DoctorControl." + model.OrderField;
                }
                else
                {
                    strOrderBy = "Doctor." + model.OrderField;
                }
                strSql.Append(" select  * from Doctor left join B_DoctorControl on Doctor.DoctorNo=B_DoctorControl.DoctorNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_DoctorControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where  ROWNUM <= '" + nowPageSize + "' and Doctor.DoctorNo not in ( ");
                strSql.Append("select Doctor.DoctorNo from  Doctor left join B_DoctorControl on Doctor.DoctorNo=B_DoctorControl.DoctorNo");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_DoctorControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + strLike + "  ) " + strLike + " order by " + strOrderBy + " ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }

            else
            {
                if (model.SearchLikeKey != null)
                {
                    strLike = " and (DoctorNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
                }
                strSql.Append("select * from Doctor where  ROWNUM <= '" + nowPageSize + "' and DoctorNo not in  ");
                strSql.Append("(select DoctorNo from Doctor where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + strLike + " ) " + strLike + " order by " + model.OrderField + "  ");
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

                    strSql.Append("insert into B_Lab_Doctor ( LabCode,");
                    strSql.Append(" LabDoctorNo , CName , ShortCode , HisOrderCode , Visible ");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("DoctorNo,CName,ShortCode,HisOrderCode,Visible");
                    strSql.Append(" from Doctor ");
                    if (str.Trim() != "")
                        strSql.Append(" where DoctorNo not in (" + str + ")");

                    strSqlControl.Append("insert into B_DoctorControl ( ");
                    strSqlControl.Append(" DoctorControlNo,DoctorNo,ControlLabNo,ControlDoctorNo,UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  concat(concat(concat(concat('" + lst[i].Trim() + "','_'),DoctorNo),'_'),DoctorNo) as DoctorControlNo,DoctorNo,'" + lst[i].Trim() + "' as ControlLabNo,DoctorNo,1 ");
                    strSqlControl.Append(" from Doctor ");
                    if (str.Trim() != "")
                        strSqlControl.Append(" where DoctorNo not in (" + str + ")");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();

                    //d_log.OperateLog("Doctor", "", "", DateTime.Now, 1);
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
            strSql.Append("select DoctorNo from B_DoctorControl where ControlLabNo=" + strLabCode);
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (str == "")
                        str = "'" + dr["DoctorNo"].ToString().Trim() + "'";
                    else
                        str += ",'" + dr["DoctorNo"].ToString().Trim() + "'";
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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["DoctorNo"].ToString().Trim())))
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
                strSql.Append("insert into Doctor (");
                strSql.Append("DoctorNo,CName,ShortCode,HisOrderCode,Visible");
                strSql.Append(") values (");
                if (dr.Table.Columns["DoctorNo"] != null && dr.Table.Columns["DoctorNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DoctorNo"].ToString().Trim() + "', ");
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
                if (dr.Table.Columns["ShortCode"] != null && dr.Table.Columns["ShortCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ShortCode"].ToString().Trim() + "', ");
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
                if (dr.Table.Columns["Visible"] != null && dr.Table.Columns["Visible"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Visible"].ToString().Trim() + "' ");
                }
                else
                {
                    strSql.Append(" null ");
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
                strSql.Append("update Doctor set ");


                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ShortCode"] != null && dr.Table.Columns["ShortCode"].ToString().Trim() != "")
                {
                    strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["HisOrderCode"] != null && dr.Table.Columns["HisOrderCode"].ToString().Trim() != "")
                {
                    strSql.Append(" HisOrderCode = '" + dr["HisOrderCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["Visible"] != null && dr.Table.Columns["Visible"].ToString().Trim() != "")
                {
                    strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "'  ");
                }

                strSql.Append(" where DoctorNo='" + dr["DoctorNo"].ToString().Trim() + "' ");

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
                if (dr.Table.Columns["DoctorNo"] != null && dr.Table.Columns["DoctorNo"].ToString().Trim() != "")
                {
                    strSql.Append("delete from Doctor ");
                    strSql.Append(" where DoctorNo='" + dr["DoctorNo"].ToString().Trim() + "' ");
                    return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.Doctor .DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }

        #endregion


        public bool IsExist(string labCodeNo)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select COUNT(1) from B_Lab_Doctor ");
            strSql.Append(" where LabCode='" + labCodeNo + "' ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" select COUNT(1) from B_DoctorControl ");
            strSql2.Append(" where ControlLabNo='" + labCodeNo + "' ");

            if (DbHelperSQL.Exists(strSql.ToString()))
            {
                result = true;
            }
            return result;
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            bool result = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" delete from B_Lab_Doctor ");
            strSql.Append(" where LabCode='" + LabCodeNo + "' ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" delete from B_DoctorControl ");
            strSql2.Append(" where ControlLabNo='" + LabCodeNo + "' ");


            int i = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            int j = DbHelperSQL.ExecuteNonQuery(strSql2.ToString());
            if (i > 0 || j > 0)
                result = true;
            return result;
        }


        public int Add(List<Model.Doctor> modelList)
        {
            throw new NotImplementedException();
        }


        public int Update(List<Model.Doctor> modelList)
        {
            throw new NotImplementedException();
        }
    }
}
