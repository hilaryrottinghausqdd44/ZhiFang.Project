using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
	//GenderType
	public partial class GenderType : BaseDALLisDB, IDGenderType, IDBatchCopy, IDGetListByTimeStampe
	{
		public GenderType(string dbsourceconn)
		{
			DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
		}
		public GenderType()
		{
		}
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.GenderType model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GenderType(");
			strSql.Append("GenderNo,CName,ShortCode,Visible,DispOrder,HisOrderCode");
			strSql.Append(") values (");
			strSql.Append("@GenderNo,@CName,@ShortCode,@Visible,@DispOrder,@HisOrderCode");
			strSql.Append(") ");

			SqlParameter[] parameters = {
			            new SqlParameter("@GenderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20)             
              
            };

			parameters[0].Value = model.GenderNo;
			parameters[1].Value = model.CName;
			parameters[2].Value = model.ShortCode;
			parameters[3].Value = model.Visible;
			parameters[4].Value = model.DispOrder;
			parameters[5].Value = model.HisOrderCode;
			if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
			{
				return d_log.OperateLog("GenderType", "", "", DateTime.Now, 1);
			}
			else
				return -1;
		}


		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.GenderType model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GenderType set ");

			strSql.Append(" GenderNo = @GenderNo , ");
			strSql.Append(" CName = @CName , ");
			strSql.Append(" ShortCode = @ShortCode , ");
			strSql.Append(" Visible = @Visible , ");
			strSql.Append(" DispOrder = @DispOrder , ");
			strSql.Append(" HisOrderCode = @HisOrderCode  ");
			strSql.Append(" where GenderNo=@GenderNo  ");

			SqlParameter[] parameters = {
			               
            new SqlParameter("@GenderNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20)             	
              
            };


			if (model.GenderNo != null)
			{
				parameters[0].Value = model.GenderNo;
			}



			if (model.CName != null)
			{
				parameters[1].Value = model.CName;
			}



			if (model.ShortCode != null)
			{
				parameters[2].Value = model.ShortCode;
			}



			if (model.Visible != null)
			{
				parameters[3].Value = model.Visible;
			}



			if (model.DispOrder != null)
			{
				parameters[4].Value = model.DispOrder;
			}



			if (model.HisOrderCode != null)
			{
				parameters[5].Value = model.HisOrderCode;
			}


			if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
			{
				return d_log.OperateLog("GenderType", "", "", DateTime.Now, 1);
			}
			else
				return -1;
		}


		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int GenderNo)
		{

			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GenderType ");
			strSql.Append(" where GenderNo=@GenderNo ");
			SqlParameter[] parameters = {
					new SqlParameter("@GenderNo", SqlDbType.Int,4)};
			parameters[0].Value = GenderNo;


			return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string IDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GenderType ");
			strSql.Append(" where ID in (" + IDlist + ")  ");
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.GenderType GetModel(int GenderNo)
		{

			StringBuilder strSql = new StringBuilder();
			strSql.Append("select GenderNo, CName, ShortCode, Visible, DispOrder, HisOrderCode  ");
			strSql.Append("  from GenderType ");
			strSql.Append(" where GenderNo=@GenderNo ");
			SqlParameter[] parameters = {
					new SqlParameter("@GenderNo", SqlDbType.Int,4)};
			parameters[0].Value = GenderNo;


			ZhiFang.Model.GenderType model = new ZhiFang.Model.GenderType();
			DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

			if (ds.Tables[0].Rows.Count > 0)
			{

				if (ds.Tables[0].Rows[0]["GenderNo"].ToString() != "")
				{
					model.GenderNo = int.Parse(ds.Tables[0].Rows[0]["GenderNo"].ToString());
				}

				model.CName = ds.Tables[0].Rows[0]["CName"].ToString();

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
			strSql.Append("select * ");
			strSql.Append(" FROM GenderType ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}


		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
		public DataSet GetList(ZhiFang.Model.GenderType model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM GenderType where 1=1 ");


			if (model.GenderNo != 0)
			{
				strSql.Append(" and GenderNo=" + model.GenderNo + " ");
			}

			if (model.CName != null)
			{
				strSql.Append(" and CName='" + model.CName + "' ");
			}

			if (model.ShortCode != null)
			{
				strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
			}

			if (model.DispOrder != null)
			{
				strSql.Append(" and DispOrder=" + model.DispOrder + " ");
			}

			if (model.HisOrderCode != null)
			{
				strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "' ");
			}
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		public DataSet GetListLike(ZhiFang.Model.GenderType model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select *,GenderNo as LabNo,convert(varchar(100),GenderNo)+'_'+CName as LabNo_Value,CName+'('+convert(varchar(100),GenderNo)+')' as LabNoAndName_Text ");
			strSql.Append(" FROM GenderType  ");
			if (model.CName != null)
			{
				strSql.Append(" where 1=2 ");
				strSql.Append(" or CName like '%" + model.CName + "%' ");
			}

			if (model.GenderNo != 0)
			{
				if (strSql.ToString().IndexOf("where 1=2") < 0)
				{
					strSql.Append(" where 1=2 ");
				}
				strSql.Append(" or GenderNo like '%" + model.GenderNo + "%' ");
			}

			if (model.ShortCode != null)
			{
				if (strSql.ToString().IndexOf("where 1=2") < 0)
				{
					strSql.Append(" where 1=2 ");
				}
				strSql.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
			}
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}

		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) FROM GenderType ");
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
		public int GetTotalCount(ZhiFang.Model.GenderType model)
		{
			StringBuilder strSql = new StringBuilder();
			StringBuilder strWhere = new StringBuilder();
			strSql.Append("select count(*) FROM GenderType where 1=1 ");
			string strLike = "";
			if (model != null && model.SearchLikeKey != null)
			{
				strLike = " and (GenderNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
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

		/// <summary>
		/// 利用标识列分页
		/// </summary>
		/// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
		/// <param name="nowPageNum">第几页</param>
		/// <param name="nowPageSize">每页多少行</param>
		/// <returns>DataSet</returns>
		public DataSet GetListByPage(ZhiFang.Model.GenderType model, int nowPageNum, int nowPageSize)
		{
			string strLike = "";

			StringBuilder strSql = new StringBuilder();
			if (model != null && model.LabCode != null)
			{
				if (model != null && model.SearchLikeKey != null)
				{
					strLike = " and (GenderType.GenderNo like '%" + model.SearchLikeKey + "%' or GenderType.CName like '%" + model.SearchLikeKey + "%' or GenderType.ShortCode like '%" + model.SearchLikeKey + "%') ";
				}
				string strOrderBy = "";
				if (model.OrderField == "GenderNoID")
				{
					strOrderBy = "GenderType.GenderNo";
				}
				else if (model.OrderField.ToLower().IndexOf("control") >= 0)
				{
					strOrderBy = "B_GenderTypeControl." + model.OrderField;
				}
				else
				{
					strOrderBy = "GenderType." + model.OrderField;
				}
				strSql.Append(" select top " + nowPageSize + "  * from GenderType left join B_GenderTypeControl on GenderType.GenderNo=B_GenderTypeControl.GenderNo ");
				if (model.LabCode != null)
				{
					strSql.Append(" and B_GenderTypeControl.ControlLabNo='" + model.LabCode + "' ");
				}
				strSql.Append("where GenderType.GenderNo not in ( ");
				strSql.Append("select top " + (nowPageSize * nowPageNum) + " GenderType.GenderNo from  GenderType left join B_GenderTypeControl on GenderType.GenderNo=B_GenderTypeControl.GenderNo ");
				if (model.LabCode != null)
				{
					strSql.Append(" and B_GenderTypeControl.ControlLabNo='" + model.LabCode + "' ");
				}
                strSql.Append(" where 1=1 " + strLike + " order by " + strOrderBy + " desc ) " + strLike + " order by " + strOrderBy + " desc ");
				return DbHelperSQL.ExecuteDataSet(strSql.ToString());
			}
			else
			{
				if (model != null && model.SearchLikeKey != null)
				{
					strLike = " and (GenderNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
				}
				strSql.Append("select top " + nowPageSize + "  * from GenderType where GenderNo not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " GenderNo from GenderType where 1=1 " + strLike + " order by " + model.OrderField + " desc ) " + strLike + " order by " + model.OrderField + " desc ");
				return DbHelperSQL.ExecuteDataSet(strSql.ToString());
			}
		}

		public bool Exists(int GenderNo)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GenderType ");
			strSql.Append(" where GenderNo ='" + GenderNo + "'");
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
			strSql.Append("select count(1) from GenderType where 1=1 ");
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

		public bool CopyToLab(List<string> lst)
		{
			System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
			string LabTableName = "GenderType";
			LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
			StringBuilder strSql = new StringBuilder();
			StringBuilder strSqlControl = new StringBuilder();
			string TableKey = "GenderNo";
			string TableKeySub = TableKey;
			if (TableKey.ToLower().Contains("no"))
			{
				TableKeySub = TableKey.Substring(0, TableKey.ToLower().IndexOf("no"));
			}
			try
			{
				for (int i = 0; i < lst.Count; i++)
				{
					string str = GetControlItems(lst[i].Trim());
					strSql.Append("insert into " + LabTableName + "( LabCode,");
					strSql.Append(" LabGenderNo , CName , ShortCode , Visible , DispOrder , HisOrderCode ");
					strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
					strSql.Append("GenderNo,CName,ShortCode,Visible,DispOrder,HisOrderCode");
					strSql.Append(" from GenderType ");
					if (str.Trim() != "")
						strSql.Append(" where GenderNo not in (" + str + ")");

					strSqlControl.Append("insert into B_GenderTypeControl ( ");
					strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
					strSqlControl.Append(")  select ");
					strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",'1' ");
					strSqlControl.Append(" from GenderType ");
					if (str.Trim() != "")
						strSqlControl.Append(" where GenderNo not in (" + str + ")");

					arrySql.Add(strSql.ToString());
					arrySql.Add(strSqlControl.ToString());

					strSql = new StringBuilder();
					strSqlControl = new StringBuilder();
				}

				DbHelperSQL.BatchUpdateWithTransaction(arrySql);
				//d_log.OperateLog("GenderType", "", "", DateTime.Now, 1);
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
			strSql.Append("select GenderNo from B_GenderTypeControl where ControlLabNo='" + strLabCode+"'");
			DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
			if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					if (str == "")
						str = "'" + dr["GenderNo"].ToString().Trim() + "'";
					else
						str += ",'" + dr["GenderNo"].ToString().Trim() + "'";
				}
			}
			return str;
		}

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("GenderNo", "GenderType");
		}

		public DataSet GetList(int Top, ZhiFang.Model.GenderType model, string filedOrder)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			if (Top > 0)
			{
				strSql.Append(" top " + Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM GenderType ");



			if (model.GenderNo != 0)
			{
				strSql.Append(" and GenderNo='" + model.GenderNo + "' ");
			}


			if (model.CName != null)
			{
				strSql.Append(" and CName='" + model.CName + "' ");
			}


			if (model.ShortCode != null)
			{
				strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
			}


			if (model.DispOrder != null)
			{
				strSql.Append(" and DispOrder='" + model.DispOrder + "' ");
			}


			if (model.HisOrderCode != null)
			{
				strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "' ");
			}

			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}

		public DataSet GetAllList()
		{
			return GetList("");
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

						if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["GenderNo"].ToString().Trim())))
						{
							count += this.UpdateByDataRow(dr);
						}
						else
						{
							count += this.AddByDataRow(dr);
						}
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
				strSql.Append("insert into GenderType (");
				strSql.Append("GenderNo,CName,ShortCode,Visible,DispOrder,HisOrderCode");
				strSql.Append(") values (");

				if (dr.Table.Columns["GenderNo"] != null && dr.Table.Columns["GenderNo"].ToString().Trim() != "")
				{
					strSql.Append(" '" + dr["GenderNo"].ToString().Trim() + "', ");
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

				int n = strSql.ToString().LastIndexOf(",");
				strSql.Remove(n, 1);
				strSql.Append(") ");
				return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			}
			catch (Exception ex)
			{
				ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.GenderType.AddByDataRow 同步数据时异常：", ex);
				return 0;
			}
		}
		public int UpdateByDataRow(DataRow dr)
		{
			try
			{
				StringBuilder strSql = new StringBuilder();
				strSql.Append("update GenderType set ");


				if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
				{
					strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
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
				strSql.Append(" where GenderNo='" + dr["GenderNo"].ToString().Trim() + "' ");

				return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			}
			catch (Exception ex)
			{
				ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.GenderType .UpdateByDataRow同步数据时异常：", ex);
				return 0;
			}
		}
		public int DeleteByDataRow(DataRow dr)
		{
			try
			{
				StringBuilder strSql = new StringBuilder();
				if (dr.Table.Columns["GenderNo"] != null && dr.Table.Columns["GenderNo"].ToString().Trim() != "")
				{
					strSql.Append("delete from GenderType ");
					strSql.Append(" where GenderNo='" + dr["GenderNo"].ToString().Trim() + "' ");
					return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
				}
				else
					return 0;
			}
			catch (Exception ex)
			{
				ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.weblis.GenderType .DeleteByDataRow同步数据时异常：", ex);
				return 0;
			}
		}

		#region IDGetListByTimeStampe 成员

		public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
		{
			DataSet dsAll = new DataSet();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select *,'" + LabCode + "' as LabCode,GenderNo as LabGenderNo from GenderType where 1=1 ");
			if (dTimeStampe != -999999)
			{
				strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
			}
			DataTable dtServer = DbHelperSQL.ExecuteDataSet(strSql.ToString()).Tables[0];
			dtServer.TableName = "ServerDatas";

			StringBuilder strSql2 = new StringBuilder();
			strSql2.Append("select *,LabGenderNo as GenderNo from S_Dic_GenderType where 1=1 ");
			if (LabCode.Trim() != "")
			{
				strSql2.Append(" and LabCode= '" + LabCode.Trim() + "' ");
			}
			if (dTimeStampe != -999999)
			{
				strSql2.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
			}
			DataTable dtLab = DbHelperSQL.ExecuteDataSet(strSql2.ToString()).Tables[0];
			dtLab.TableName = "LabDatas";

			StringBuilder strSql3 = new StringBuilder();
			strSql3.Append("select * from B_GenderTypeControl where 1=1 ");
			if (LabCode.Trim() != "")
			{
				strSql3.Append(" and ControlLabNo= '" + LabCode.Trim() + "' ");
			}
			if (dTimeStampe != -999999)
			{
				strSql3.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
			}
			DataTable dtControl = DbHelperSQL.ExecuteDataSet(strSql3.ToString()).Tables[0];
			dtControl.TableName = "ControlDatas";

			dsAll.Tables.Add(dtServer.Copy());
			dsAll.Tables.Add(dtLab.Copy());
			dsAll.Tables.Add(dtControl.Copy());
			return dsAll;
		}

		#endregion




        public bool IsExist(string labCodeNo)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select COUNT(1) from B_lab_GenderType ");
            strSql.Append(" where LabCode='" + labCodeNo + "' ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" select COUNT(1) from b_GenderTypecontrol ");
            strSql2.Append(" where ControlLabNo='" + labCodeNo + "' ");

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
            strSql.Append(" delete from B_lab_GenderType ");
            strSql.Append(" where LabCode='" + LabCodeNo + "' ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" delete from b_GenderTypecontrol ");
            strSql2.Append(" where ControlLabNo='" + LabCodeNo + "' ");


            int i = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            int j = DbHelperSQL.ExecuteNonQuery(strSql2.ToString());
            if (i > 0 || j > 0)
                result = true;
            return result;
        }
    }
}

