using System;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DBUtility;
using System.Data;
using System.Collections.Generic;

namespace ZhiFang.DAL.MsSql.Weblis
{
    public class Department : BaseDALLisDB, IDDepartment, IDBatchCopy
    {
        /// <summary>
        /// 数据访问类Department。
        /// </summary>
        public Department(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public Department()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }



        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DeptNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Department");
            strSql.Append(" where DeptNo=" + DeptNo + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Department model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.DeptNo != null)
            {
                strSql1.Append("DeptNo,");
                strSql2.Append("" + model.DeptNo + ",");
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
            strSql.Append("insert into Department(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        public int Add(List<ZhiFang.Model.Department> modelList)
        {
            string strsqls = "";
            foreach (var model in modelList)
            {
                StringBuilder strSql = new StringBuilder();
                StringBuilder strSql1 = new StringBuilder();
                StringBuilder strSql2 = new StringBuilder();
                if (model.DeptNo != null)
                {
                    strSql1.Append("DeptNo,");
                    strSql2.Append("" + model.DeptNo + ",");
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
                strSql.Append("insert into Department(");
                strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
                strSql.Append(")");
                strSql.Append(" values (");
                strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
                strSql.Append(")");
                if (strsqls == "")
                {
                    strsqls = strSql.ToString();
                }
                else
                {
                    strsqls += ";" + strSql.ToString();
                }
            }

            return DbHelperSQL.ExecuteNonQuery(strsqls);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Department model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Department set ");
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
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append("HisOrderCode='" + model.HisOrderCode + "',");
            }
            if (model.Visible != null)
            {
                strSql.Append("Visible='" + model.Visible + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where DeptNo=" + model.DeptNo + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        public int Update(List<ZhiFang.Model.Department> modelList)
        {
            string strsqls = "";
            foreach (var model in modelList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Department set ");
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
                if (model.DispOrder != null)
                {
                    strSql.Append("DispOrder=" + model.DispOrder + ",");
                }
                if (model.HisOrderCode != null)
                {
                    strSql.Append("HisOrderCode='" + model.HisOrderCode + "',");
                }
                if (model.Visible != null)
                {
                    strSql.Append("Visible='" + model.Visible + "',");
                }
                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where DeptNo=" + model.DeptNo + " ");
                if (strsqls == "")
                {
                    strsqls = strSql.ToString();
                }
                else
                {
                    strsqls += ";" + strSql.ToString();
                }
            }
            return DbHelperSQL.ExecuteNonQuery(strsqls);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int DeptNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Department ");
            strSql.Append(" where DeptNo=" + DeptNo + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Department GetModel(int DeptNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" DeptNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode  ");
            strSql.Append(" from Department ");
            strSql.Append(" where DeptNo=" + DeptNo + " ");
            Model.Department model = new Model.Department();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DeptNo"].ToString() != "")
                {
                    model.DeptNo = int.Parse(ds.Tables[0].Rows[0]["DeptNo"].ToString());
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
            strSql.Append("select DeptNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode");
            strSql.Append(" FROM Department ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(ZhiFang.Model.Department model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DeptNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode");
            strSql.Append(" FROM Department where 1=1");
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
            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + "");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "'");
            }
            if (model.DeptNo != null && model.DeptNo != 0)
            {
                strSql.Append(" and DeptNo=" + model.DeptNo + "");
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
            strSql.Append(" DeptNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode  ");
            strSql.Append(" FROM Department ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        #region IDDepartment 成员


        public int DeleteList(string DeptIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Department ");
            strSql.Append(" where ID in (" + DeptIDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>


        public DataSet GetListLike(Model.Department model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,DeptNo as LabNo,CName+'('+convert(varchar(100),DeptNo)+')' as LabNoAndName ");
            strSql.Append(" FROM Department where 1=1 ");

            if (model.OutSideDeptNo != null)
            {
                strSql.Append(" and DeptNo <> " + model.OutSideDeptNo + " ");
            }

            StringBuilder sbWhere = new StringBuilder();
            if (model.CName != null)
            {
                sbWhere.Append(" and ( CName like '%" + model.CName + "%' ");
            }
            if (model.DeptNo != 0)
            {
                if (sbWhere.Length > 0)
                    sbWhere.Append(" or DeptNo like '%" + model.DeptNo + "%' ");
                else
                    sbWhere.Append(" and ( DeptNo like '%" + model.DeptNo + "%' ");
            }
            if (model.ShortCode != null)
            {
                if (sbWhere.Length > 0)
                    sbWhere.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
                else
                    sbWhere.Append(" and ( ShortCode like '%" + model.ShortCode + "%' ");
            }
            if (model.ShortName != null)
            {
                if (sbWhere.Length > 0)
                    strSql.Append(" or ShortName like '%" + model.ShortName + "%' ");
                else
                    strSql.Append(" and ( ShortName like '%" + model.ShortName + "%' ");
            }
            if (sbWhere.Length > 0)
                sbWhere.Append(" ) ");

            strSql.Append(sbWhere.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["DeptNo"].ToString().Trim())))
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
                strSql.Append("insert into Department (");
                strSql.Append("DeptNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode,");
                strSql.Append(") values (");
                if (dr.Table.Columns["DeptNo"] != null && dr.Table.Columns["DeptNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DeptNo"].ToString().Trim() + "', ");
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
                strSql.Append("update Department set ");


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

                strSql.Append(" where DeptNo='" + dr["DeptNo"].ToString().Trim() + "' ");

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
                if (dr.Table.Columns["DeptNo"] != null && dr.Table.Columns["DeptNo"].ToString().Trim() != "")
                {
                    strSql.Append("delete from Department ");
                    strSql.Append(" where DeptNo='" + dr["DeptNo"].ToString().Trim() + "' ");
                    return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.weblis.Department .DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("DeptNo", "Department");
        }

        public DataSet GetList(int Top, Model.Department model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" DeptNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode");
            strSql.Append(" FROM Department where 1=1");
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
            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + "");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "'");
            }
            if (model.DeptNo != null)
            {
                strSql.Append(" and DeptNo=" + model.DeptNo + "");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Department");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM Department");
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public int GetTotalCount(Model.Department model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM Department where 1=1 ");
            string strLike = "";
            if (model != null && model.SearchLikeKey != null)
            {
                strLike = " and (DeptNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
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
            //return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        /// <summary>
        /// 利用主键分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.Department model, int nowPageNum, int nowPageSize)
        {
            string likesql = "";
            StringBuilder strSql = new StringBuilder();

            if (model != null && model.LabCode != null)
            {
                if (model.SearchLikeKey != null)
                {
                    likesql = " and (Department.DeptNo like '%" + model.SearchLikeKey + "%' or Department.CName like '%" + model.SearchLikeKey + "%' or Department.ShortCode like '%" + model.SearchLikeKey + "%') ";
                }

                string strOrderBy = "";
                if (model.OrderField == "DeptID")
                {
                    strOrderBy = "Department.DeptNo";
                }
                else if (model.OrderField.ToLower().IndexOf("control") >= 0)
                {
                    strOrderBy = "B_DepartmentControl." + model.OrderField;
                }
                else
                {
                    strOrderBy = "Department." + model.OrderField;
                }

                strSql.Append(" select top " + nowPageSize + "  * from Department left join B_DepartmentControl on Department.DeptNo=B_DepartmentControl.DeptNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_DepartmentControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where Department.DeptNo not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " Department.DeptNo from  Department left join B_DepartmentControl on Department.DeptNo=B_DepartmentControl.DeptNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_DepartmentControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" where 1=1 " + likesql + " order by " + strOrderBy + " desc ) " + likesql + " order by " + strOrderBy + " desc ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {

                if (model.SearchLikeKey != null)
                {
                    likesql = " and  ( DeptNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%'  or ShortName like '%" + model.SearchLikeKey + "%'  or ShortCode like '%" + model.SearchLikeKey + "%') ";
                }
                strSql.Append("select top " + nowPageSize + "  * from Department where DeptNo not in  ");
                // strSql.Append("(select top " + (nowPageSize * nowPageNum ) + " DeptNo from Department  where 1=1 " + likesql + "  order by DeptNo)");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " DeptNo from Department where 1=1 " + likesql + " order by " + model.OrderField + " desc ) " + likesql + " order by " + model.OrderField + " desc ");
                //strSql.Append(likesql);
                //strSql.Append(" order by DeptNo    ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }    

        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            try
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    string str = this.GetControlItems(lst[i].Trim());
                    strSql.Append("insert into B_Lab_Department ( LabCode,");
                    strSql.Append(" LabDeptNo , CName , ShortName , ShortCode , Visible , DispOrder , HisOrderCode ");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("DeptNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode");
                    strSql.Append(" from Department ");
                    if (str.Trim() != "")
                        strSql.Append(" where DeptNo not in (" + str + ")");

                    strSqlControl.Append("insert into B_DepartmentControl ( ");
                    strSqlControl.Append(" DepartmentControlNo,DeptNo,ControlLabNo,ControlDeptNo,UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50),DeptNo)+'_'+convert(varchar(50), DeptNo) as DepartmentControlNo,DeptNo,'" + lst[i].Trim() + "' as ControlLabNo,DeptNo,1 ");
                    strSqlControl.Append(" from Department ");


                    if (str.Trim() != "")
                        strSqlControl.Append(" where DeptNo not in (" + str + ")");
                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();

                    //d_log.OperateLog("Department", "", "", DateTime.Now, 1);
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
            strSql.Append("select DeptNo from B_DepartmentControl where ControlLabNo=" + strLabCode);
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (str == "")
                        str = "'" + dr["DeptNo"].ToString().Trim() + "'";
                    else
                        str += ",'" + dr["DeptNo"].ToString().Trim() + "'";
                }
            }
            return str;
        }

        #endregion


        public bool IsExist(string labCodeNo)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select COUNT(1) from B_Lab_Department ");
            strSql.Append(" where LabCode=" + labCodeNo + " ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" select COUNT(1) from B_DepartmentControl ");
            strSql2.Append(" where ControlLabNo=" + labCodeNo + " ");

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
            strSql.Append(" delete from B_Lab_Department ");
            strSql.Append(" where LabCode=" + LabCodeNo + " ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" delete from B_DepartmentControl ");
            strSql2.Append(" where ControlLabNo=" + LabCodeNo + " ");


            int i = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            int j = DbHelperSQL.ExecuteNonQuery(strSql2.ToString());
            if (i > 0 || j > 0)
                result = true;
            return result;
        }
    }
}
