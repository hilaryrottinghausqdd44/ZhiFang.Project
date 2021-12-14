using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
	/// <summary>
	/// ���ݷ�����Department��
	/// </summary>
	public class Department:IDDepartment
	{
		public Department()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("DeptNo", "Department"); 
		}


		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int DeptNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Department");
			strSql.Append(" where DeptNo="+DeptNo+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// ����һ������
		/// </summary>
        public int Add(Model.Department model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.DeptNo != null)
			{
				strSql1.Append("DeptNo,");
				strSql2.Append(""+model.DeptNo+",");
			}
			if (model.CName != null)
			{
				strSql1.Append("CName,");
				strSql2.Append("'"+model.CName+"',");
			}
			if (model.ShortName != null)
			{
				strSql1.Append("ShortName,");
				strSql2.Append("'"+model.ShortName+"',");
			}
			if (model.ShortCode != null)
			{
				strSql1.Append("ShortCode,");
				strSql2.Append("'"+model.ShortCode+"',");
			}
			if (model.Visible != null)
			{
				strSql1.Append("Visible,");
				strSql2.Append(""+model.Visible+",");
			}
			if (model.DispOrder != null)
			{
				strSql1.Append("DispOrder,");
				strSql2.Append(""+model.DispOrder+",");
			}
			if (model.HisOrderCode != null)
			{
				strSql1.Append("HisOrderCode,");
				strSql2.Append("'"+model.HisOrderCode+"',");
			}
			strSql.Append("insert into Department(");
			strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
			strSql.Append(")");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// ����һ������
		/// </summary>
        public int Update(Model.Department model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Department set ");
			if (model.CName != null)
			{
				strSql.Append("CName='"+model.CName+"',");
			}
			if (model.ShortName != null)
			{
				strSql.Append("ShortName='"+model.ShortName+"',");
			}
			if (model.ShortCode != null)
			{
				strSql.Append("ShortCode='"+model.ShortCode+"',");
			}
			if (model.DispOrder != null)
			{
				strSql.Append("DispOrder="+model.DispOrder+",");
			}
			if (model.HisOrderCode != null)
			{
				strSql.Append("HisOrderCode='"+model.HisOrderCode+"',");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where DeptNo="+ model.DeptNo+" and Visible="+ model.Visible+" ");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
        public int Delete(int DeptNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Department ");
			strSql.Append(" where DeptNo="+DeptNo+" " );
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public Model.Department GetModel(int DeptNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" DeptNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode ");
			strSql.Append(" from Department ");
			strSql.Append(" where DeptNo="+DeptNo+" " );
			Model.Department model=new Model.Department();
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["DeptNo"].ToString()!="")
				{
					model.DeptNo=int.Parse(ds.Tables[0].Rows[0]["DeptNo"].ToString());
				}
				model.CName=ds.Tables[0].Rows[0]["CName"].ToString();
				model.ShortName=ds.Tables[0].Rows[0]["ShortName"].ToString();
				model.ShortCode=ds.Tables[0].Rows[0]["ShortCode"].ToString();
				if(ds.Tables[0].Rows[0]["Visible"].ToString()!="")
				{
					model.Visible=int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DispOrder"].ToString()!="")
				{
					model.DispOrder=int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
				}
				model.HisOrderCode=ds.Tables[0].Rows[0]["HisOrderCode"].ToString();
				return model;
			}
			else
			{
				return null;
			}
		}

        /// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public Model.Department GetModel(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" DeptNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode ");
            strSql.Append(" from Department ");
            strSql.Append(" where "+where);
            Model.Department model = new Model.Department();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select DeptNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode,code_1 ");
			strSql.Append(" FROM Department ");
			if(strWhere != null && strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
            
            return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" DeptNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode ");
			strSql.Append(" FROM Department ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
		/// ���ǰ��������
		/// </summary>
        public DataSet GetList(string Where, int page, int limit)
        {
            StringBuilder strSql = new StringBuilder();
            if (page < 1)
            {
                page = 1;
            }
            if (limit < 1)
            {
                limit = 1;
            }
            if (Where !=null && Where.Trim() != "")
            {
                Where = " where 1=1 and " + Where;
            }
            strSql.Append("SELECT * FROM(  SELECT TOP " + limit + " * FROM  (    SELECT TOP " + page * limit + " * FROM Department " + Where + "  ORDER BY DeptNo    ) as a  ORDER BY DeptNo desc) as b ORDER BY DeptNo ASC ");
            return DbHelperSQL.Query(strSql.ToString());
        }
		/*
		*/

        public DataSet GetList(Model.Department model)
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
            if (model.DeptNo != null)
            {
                strSql.Append(" and DeptNo=" + model.DeptNo + "");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion
    }
}

