using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
	/// <summary>
	/// 数据访问类TestItem。
	/// </summary>
	public class TestItem:IDTestItem
	{
		public TestItem()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ItemNo", "TestItem"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ItemNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TestItem");
			strSql.Append(" where ItemNo="+ItemNo+"");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(Model.TestItem model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.ItemNo != null)
			{
				strSql1.Append("ItemNo,");
				strSql2.Append(""+model.ItemNo+",");
			}
			if (model.CName != null)
			{
				strSql1.Append("CName,");
				strSql2.Append("'"+model.CName+"',");
			}
			if (model.EName != null)
			{
				strSql1.Append("EName,");
				strSql2.Append("'"+model.EName+"',");
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
			if (model.DiagMethod != null)
			{
				strSql1.Append("DiagMethod,");
				strSql2.Append("'"+model.DiagMethod+"',");
			}
			if (model.Unit != null)
			{
				strSql1.Append("Unit,");
				strSql2.Append("'"+model.Unit+"',");
			}
			if (model.IsCalc != null)
			{
				strSql1.Append("IsCalc,");
				strSql2.Append(""+model.IsCalc+",");
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
			if (model.Prec != null)
			{
				strSql1.Append("Prec,");
				strSql2.Append(""+model.Prec+",");
			}
			if (model.IsProfile != null)
			{
				strSql1.Append("IsProfile,");
				strSql2.Append(""+model.IsProfile+",");
			}
			if (model.OrderNo != null)
			{
				strSql1.Append("OrderNo,");
				strSql2.Append("'"+model.OrderNo+"',");
			}
			if (model.StandardCode != null)
			{
				strSql1.Append("StandardCode,");
				strSql2.Append("'"+model.StandardCode+"',");
			}
			if (model.ItemDesc != null)
			{
				strSql1.Append("ItemDesc,");
				strSql2.Append("'"+model.ItemDesc+"',");
			}
			if (model.FWorkLoad != null)
			{
				strSql1.Append("FWorkLoad,");
				strSql2.Append(""+model.FWorkLoad+",");
			}
			if (model.Secretgrade != null)
			{
				strSql1.Append("Secretgrade,");
				strSql2.Append(""+model.Secretgrade+",");
			}
			if (model.Cuegrade != null)
			{
				strSql1.Append("Cuegrade,");
				strSql2.Append(""+model.Cuegrade+",");
			}
			if (model.IsDoctorItem != null)
			{
				strSql1.Append("IsDoctorItem,");
				strSql2.Append(""+model.IsDoctorItem+",");
			}
			if (model.IschargeItem != null)
			{
				strSql1.Append("IschargeItem,");
				strSql2.Append(""+model.IschargeItem+",");
			}
			if (model.HisDispOrder != null)
			{
				strSql1.Append("HisDispOrder,");
				strSql2.Append(""+model.HisDispOrder+",");
			}
			if (model.IsNurseItem != null)
			{
				strSql1.Append("IsNurseItem,");
				strSql2.Append(""+model.IsNurseItem+",");
			}
			strSql.Append("insert into TestItem(");
			strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
			strSql.Append(")");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(Model.TestItem model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TestItem set ");
			if (model.CName != null)
			{
				strSql.Append("CName='"+model.CName+"',");
			}
			if (model.EName != null)
			{
				strSql.Append("EName='"+model.EName+"',");
			}
			if (model.ShortName != null)
			{
				strSql.Append("ShortName='"+model.ShortName+"',");
			}
			if (model.ShortCode != null)
			{
				strSql.Append("ShortCode='"+model.ShortCode+"',");
			}
			if (model.DiagMethod != null)
			{
				strSql.Append("DiagMethod='"+model.DiagMethod+"',");
			}
			if (model.Unit != null)
			{
				strSql.Append("Unit='"+model.Unit+"',");
			}
			if (model.IsCalc != null)
			{
				strSql.Append("IsCalc="+model.IsCalc+",");
			}
			if (model.Visible != null)
			{
				strSql.Append("Visible="+model.Visible+",");
			}
			if (model.DispOrder != null)
			{
				strSql.Append("DispOrder="+model.DispOrder+",");
			}
			if (model.Prec != null)
			{
				strSql.Append("Prec="+model.Prec+",");
			}
			if (model.OrderNo != null)
			{
				strSql.Append("OrderNo='"+model.OrderNo+"',");
			}
			if (model.StandardCode != null)
			{
				strSql.Append("StandardCode='"+model.StandardCode+"',");
			}
			if (model.ItemDesc != null)
			{
				strSql.Append("ItemDesc='"+model.ItemDesc+"',");
			}
			if (model.FWorkLoad != null)
			{
				strSql.Append("FWorkLoad="+model.FWorkLoad+",");
			}
			if (model.Secretgrade != null)
			{
				strSql.Append("Secretgrade="+model.Secretgrade+",");
			}
			if (model.Cuegrade != null)
			{
				strSql.Append("Cuegrade="+model.Cuegrade+",");
			}
			if (model.IsDoctorItem != null)
			{
				strSql.Append("IsDoctorItem="+model.IsDoctorItem+",");
			}
			if (model.IschargeItem != null)
			{
				strSql.Append("IschargeItem="+model.IschargeItem+",");
			}
			if (model.HisDispOrder != null)
			{
				strSql.Append("HisDispOrder="+model.HisDispOrder+",");
			}
			if (model.IsNurseItem != null)
			{
				strSql.Append("IsNurseItem="+model.IsNurseItem+",");
			}
            if (model.IsProfile != null)
            {
                strSql.Append("IsProfile=" + model.IsProfile + ",");
            }
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where ItemNo="+ model.ItemNo+" ");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(int ItemNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TestItem ");
			strSql.Append(" where ItemNo="+ItemNo+" " );
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.TestItem GetModel(int ItemNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" ItemNo,CName,EName,ShortName,ShortCode,DiagMethod,Unit,IsCalc,Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,IsDoctorItem,IschargeItem,HisDispOrder,IsNurseItem ");
			strSql.Append(" from TestItem ");
			strSql.Append(" where ItemNo="+ItemNo+" " );
			Model.TestItem model=new Model.TestItem();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ItemNo"].ToString()!="")
				{
					model.ItemNo=int.Parse(ds.Tables[0].Rows[0]["ItemNo"].ToString());
				}
				model.CName=ds.Tables[0].Rows[0]["CName"].ToString();
				model.EName=ds.Tables[0].Rows[0]["EName"].ToString();
				model.ShortName=ds.Tables[0].Rows[0]["ShortName"].ToString();
				model.ShortCode=ds.Tables[0].Rows[0]["ShortCode"].ToString();
				model.DiagMethod=ds.Tables[0].Rows[0]["DiagMethod"].ToString();
				model.Unit=ds.Tables[0].Rows[0]["Unit"].ToString();
				if(ds.Tables[0].Rows[0]["IsCalc"].ToString()!="")
				{
					model.IsCalc=int.Parse(ds.Tables[0].Rows[0]["IsCalc"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Visible"].ToString()!="")
				{
					model.Visible=int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DispOrder"].ToString()!="")
				{
					model.DispOrder=int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Prec"].ToString()!="")
				{
					model.Prec=int.Parse(ds.Tables[0].Rows[0]["Prec"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsProfile"].ToString()!="")
				{
					model.IsProfile=int.Parse(ds.Tables[0].Rows[0]["IsProfile"].ToString());
				}
				model.OrderNo=ds.Tables[0].Rows[0]["OrderNo"].ToString();
				model.StandardCode=ds.Tables[0].Rows[0]["StandardCode"].ToString();
				model.ItemDesc=ds.Tables[0].Rows[0]["ItemDesc"].ToString();
				if(ds.Tables[0].Rows[0]["FWorkLoad"].ToString()!="")
				{
					model.FWorkLoad=decimal.Parse(ds.Tables[0].Rows[0]["FWorkLoad"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Secretgrade"].ToString()!="")
				{
					model.Secretgrade=int.Parse(ds.Tables[0].Rows[0]["Secretgrade"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Cuegrade"].ToString()!="")
				{
					model.Cuegrade=int.Parse(ds.Tables[0].Rows[0]["Cuegrade"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDoctorItem"].ToString()!="")
				{
					model.IsDoctorItem=int.Parse(ds.Tables[0].Rows[0]["IsDoctorItem"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IschargeItem"].ToString()!="")
				{
					model.IschargeItem=int.Parse(ds.Tables[0].Rows[0]["IschargeItem"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HisDispOrder"].ToString()!="")
				{
					model.HisDispOrder=int.Parse(ds.Tables[0].Rows[0]["HisDispOrder"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsNurseItem"].ToString()!="")
				{
					model.IsNurseItem=int.Parse(ds.Tables[0].Rows[0]["IsNurseItem"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ItemNo,CName,EName,ShortName,ShortCode,DiagMethod,Unit,IsCalc,Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,IsDoctorItem,IschargeItem,HisDispOrder,IsNurseItem ");
			strSql.Append(" FROM TestItem ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ItemNo,CName,EName,ShortName,ShortCode,DiagMethod,Unit,IsCalc,Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,IsDoctorItem,IschargeItem,HisDispOrder,IsNurseItem ");
			strSql.Append(" FROM TestItem ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		*/

        public DataSet GetListLike(Model.TestItem model)
        {
             StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM TestItem where 1=2  ");
            if (model.CName != null)
            {
                strSql.Append(" or CName like '%" + model.CName + "%'");
            }
            if (model.EName != null)
            {
                strSql.Append(" or EName like '%" + model.EName + "%'");
            }
            if (model.ShortName != null)
            {
                strSql.Append(" or ShortName like '%" + model.ShortName + "%'");
            }
            if (model.ShortCode != null)
            {
                strSql.Append(" or ShortCode like '%" + model.ShortCode + "%'");
            }
            strSql.Append(" order by ShortCode ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(Model.TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemNo,CName,EName,ShortName,ShortCode,DiagMethod,Unit,IsCalc,Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,IsDoctorItem,IschargeItem,HisDispOrder,IsNurseItem ");
            strSql.Append(" FROM TestItem where 1=1 ");
            if (model.ItemNo != null)
            {
                strSql.Append(" and  ItemNo=" + model.ItemNo + "");
            }
            if (model.CName != null)
            {
                strSql.Append(" and  CName='" + model.CName + "'");
            }
            if (model.EName != null)
            {
                strSql.Append(" and EName='" + model.EName + "'");
            }
            if (model.ShortName != null)
            {
                strSql.Append(" and ShortName='" + model.ShortName + "'");
            }
            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "'");
            }
            if (model.DiagMethod != null)
            {
                strSql.Append(" and DiagMethod='" + model.DiagMethod + "'");
            }
            if (model.Unit != null)
            {
                strSql.Append(" and Unit='" + model.Unit + "'");
            }
            if (model.IsCalc != null)
            {
                strSql.Append(" and IsCalc=" + model.IsCalc + "");
            }
            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + "");
            }
            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + "");
            }
            if (model.Prec != null)
            {
                strSql.Append(" and Prec=" + model.Prec + "");
            }
            if (model.OrderNo != null)
            {
                strSql.Append(" and OrderNo='" + model.OrderNo + "'");
            }
            if (model.StandardCode != null)
            {
                strSql.Append(" and StandardCode='" + model.StandardCode + "'");
            }
            if (model.ItemDesc != null)
            {
                strSql.Append(" and ItemDesc='" + model.ItemDesc + "'");
            }
            if (model.FWorkLoad != null)
            {
                strSql.Append(" and FWorkLoad=" + model.FWorkLoad + "");
            }
            if (model.Secretgrade != null)
            {
                strSql.Append(" and Secretgrade=" + model.Secretgrade + "");
            }
            if (model.Cuegrade != null)
            {
                strSql.Append(" and Cuegrade=" + model.Cuegrade + "");
            }
            if (model.IsDoctorItem != null)
            {
                strSql.Append(" and IsDoctorItem=" + model.IsDoctorItem + "");
            }
            if (model.IschargeItem != null)
            {
                strSql.Append(" and IschargeItem=" + model.IschargeItem + "");
            }
            if (model.HisDispOrder != null)
            {
                strSql.Append(" and HisDispOrder=" + model.HisDispOrder + "");
            }
            if (model.IsNurseItem != null)
            {
                strSql.Append(" and IsNurseItem=" + model.IsNurseItem + "");
            }
            if (model.IsProfile != null)
            {
                strSql.Append(" and IsProfile=" + model.IsProfile + "");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(string strWhere, string fields)
        {
            StringBuilder strSql = new StringBuilder();
            if (fields == "" || fields == null) {
                fields = " ItemNo,CName ";
            }
            strSql.Append("select ");
            strSql.Append(fields);
            strSql.Append(" FROM TestItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion
    }
}

