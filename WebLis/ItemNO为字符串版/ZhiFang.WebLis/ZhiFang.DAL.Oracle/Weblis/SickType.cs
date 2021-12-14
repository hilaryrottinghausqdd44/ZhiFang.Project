using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
	//SickType

	public partial class SickType : BaseDALLisDB, IDSickType, IDBatchCopy, IDGetListByTimeStampe
	{
		public SickType(string dbsourceconn)
		{
			DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
		}
		public SickType()
		{
		}
		//D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.SickType model)
		{
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.SickTypeNo != null)
            {
                strSql1.Append("SickTypeNo,");
                strSql2.Append("" + model.SickTypeNo + ",");
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
                strSql1.Append("tstamp,");
                strSql2.Append("Systimestamp,");
           
            strSql.Append("insert into SickType(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into SickType(");
            //strSql.Append("SickTypeNo,CName,ShortCode,DispOrder,HisOrderCode");
            //strSql.Append(") values (");
            //strSql.Append("@SickTypeNo,@CName,@ShortCode,@DispOrder,@HisOrderCode");
            //strSql.Append(") ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@SickTypeNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@CName", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
            //            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,21)             
              
            //};

            //parameters[0].Value = model.SickTypeNo;
            //parameters[1].Value = model.CName;
            //parameters[2].Value = model.ShortCode;
            //parameters[3].Value = model.DispOrder;
            //parameters[4].Value = model.HisOrderCode;
            //return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.SickType model)
		{

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SickType set ");
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            else
            {
                strSql.Append("CName= null ,");
            }
            if (model.ShortCode != null)
            {
                strSql.Append("ShortCode='" + model.ShortCode + "',");
            }
            else
            {
                strSql.Append("ShortCode= null ,");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            else
            {
                strSql.Append("DispOrder= null ,");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append("HisOrderCode='" + model.HisOrderCode + "',");
            }
            else
            {
                strSql.Append("HisOrderCode= null ,");
            }
            strSql.Append("tstamp= Systimestamp,");

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where SickTypeNo=" + model.SickTypeNo + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update SickType set ");

            //strSql.Append(" SickTypeNo = @SickTypeNo , ");
            //strSql.Append(" CName = @CName , ");
            //strSql.Append(" ShortCode = @ShortCode , ");
            //strSql.Append(" DispOrder = @DispOrder , ");
            //strSql.Append(" HisOrderCode = @HisOrderCode  ");
            //strSql.Append(" where SickTypeNo=@SickTypeNo  ");

            //SqlParameter[] parameters = {
			               
            //new SqlParameter("@SickTypeNo", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@CName", SqlDbType.VarChar,20) ,            	
                           
            //new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            //new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@HisOrderCode", SqlDbType.VarChar,21)             	
              
            //};


            //if (model.SickTypeNo != null)
            //{
            //    parameters[0].Value = model.SickTypeNo;
            //}



            //if (model.CName != null)
            //{
            //    parameters[1].Value = model.CName;
            //}



            //if (model.ShortCode != null)
            //{
            //    parameters[2].Value = model.ShortCode;
            //}



            //if (model.DispOrder != null)
            //{
            //    parameters[3].Value = model.DispOrder;
            //}



            //if (model.HisOrderCode != null)
            //{
            //    parameters[4].Value = model.HisOrderCode;
            //}


            //return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int SickTypeNo)
		{

			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SickType ");
			strSql.Append(" where SickTypeNo="+SickTypeNo);


			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string IDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SickType ");
			strSql.Append(" where ID in (" + IDlist + ")  ");
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.SickType GetModel(int SickTypeNo)
		{

			StringBuilder strSql = new StringBuilder();
			strSql.Append("select SickTypeNo, CName, ShortCode, DispOrder, HisOrderCode  ");
			strSql.Append("  from SickType ");
			strSql.Append(" where SickTypeNo=@SickTypeNo ");
			SqlParameter[] parameters = {
					new SqlParameter("@SickTypeNo", SqlDbType.Int,4)};
			parameters[0].Value = SickTypeNo;


			ZhiFang.Model.SickType model = new ZhiFang.Model.SickType();
			DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

			if (ds.Tables[0].Rows.Count > 0)
			{

				if (ds.Tables[0].Rows[0]["SickTypeNo"].ToString() != "")
				{
					model.SickTypeNo = int.Parse(ds.Tables[0].Rows[0]["SickTypeNo"].ToString());
				}

				model.CName = ds.Tables[0].Rows[0]["CName"].ToString();

				model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();

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
			strSql.Append(" FROM SickType ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}


		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
		public DataSet GetList(ZhiFang.Model.SickType model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM SickType where 1=1 ");


			if (model.SickTypeNo != 0)
			{
				strSql.Append(" and SickTypeNo=" + model.SickTypeNo + " ");
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
		public DataSet GetListLike(ZhiFang.Model.SickType model)
		{
			StringBuilder strSql = new StringBuilder();
            strSql.Append("select SickType.*,SickTypeNo as LabNo,concat(concat(SickTypeNo,'_'),CNAME) as LabNo_Value,concat(concat(concat(CName,'('),SickTypeNo),')') as LabNoAndName_Text ");
			strSql.Append(" FROM SickType  ");
			if (model.CName != null)
			{
				strSql.Append(" where 1=2 ");
				strSql.Append(" or CName like '%" + model.CName + "%' ");
			}

			if (model.SickTypeNo != 0)
			{
				if (strSql.ToString().IndexOf("where 1=2") < 0)
				{
					strSql.Append(" where 1=2 ");
				}
				strSql.Append(" or SickTypeNo like '%" + model.SickTypeNo + "%' ");
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
			strSql.Append("select count(*) FROM SickType ");
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
		public int GetTotalCount(ZhiFang.Model.SickType model)
		{
			StringBuilder strSql = new StringBuilder();
			StringBuilder strWhere = new StringBuilder();
			strSql.Append("select count(*) FROM SickType where 1=1 ");
			string strLike = "";
			if (model != null && model.SearchLikeKey != null)
			{
				strLike = " and (SickTypeNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
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
		public DataSet GetListByPage(ZhiFang.Model.SickType model, int nowPageNum, int nowPageSize)
		{
			string strLike = "";

			StringBuilder strSql = new StringBuilder();
			if (model != null && model.LabCode != null)
			{
				if (model.SearchLikeKey != null)
				{
					strLike = " and (SickType.SickTypeNo like '%" + model.SearchLikeKey + "%' or SickType.CName like '%" + model.SearchLikeKey + "%' or SickType.ShortCode like '%" + model.SearchLikeKey + "%') ";
				}
				string strOrderBy = "";
				if (model.OrderField == "SickTypeID")
				{
					strOrderBy = "SickType.SickTypeNo";
				}
				else if (model.OrderField.ToLower().IndexOf("control") >= 0)
				{
					strOrderBy = "B_SickTypeControl." + model.OrderField;
				}
				else
				{
					strOrderBy = "SickType." + model.OrderField;
				}
				strSql.Append(" select  * from SickType left join B_SickTypeControl on SickType.SickTypeNo=B_SickTypeControl.SickTypeNo ");
				if (model.LabCode != null)
				{
					strSql.Append(" and B_SickTypeControl.ControlLabNo='" + model.LabCode + "' ");
				}
                strSql.Append("where  ROWNUM <= '" + nowPageSize + "' and SickType.SickTypeNo not in ( ");
				strSql.Append("select SickType.SickTypeNo from  SickType left join B_SickTypeControl on SickType.SickTypeNo=B_SickTypeControl.SickTypeNo ");
				if (model.LabCode != null)
				{
					strSql.Append(" and B_SickTypeControl.ControlLabNo='" + model.LabCode + "' ");
				}
                strSql.Append(" where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + strLike + "  ) " + strLike + " order by " + strOrderBy + " ");
				return DbHelperSQL.ExecuteDataSet(strSql.ToString());
			}
			else
			{
				if (model.SearchLikeKey != null)
				{
					strLike = " and (SickTypeNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
				}
                strSql.Append("select  * from SickType where  ROWNUM <= '" + nowPageSize + "' and SickTypeNo not in  ");
                strSql.Append("(select SickTypeNo from SickType where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + strLike + " ) " + strLike + " order by " + model.OrderField + "  ");
				return DbHelperSQL.ExecuteDataSet(strSql.ToString());
			}
		}

		public bool Exists(int SickTypeNo)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SickType ");
			strSql.Append(" where SickTypeNo ='" + SickTypeNo + "'");
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
			strSql.Append("select count(1) from SickType where 1=1 ");
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
			string LabTableName = "SickType";
			LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
			StringBuilder strSql = new StringBuilder();
			StringBuilder strSqlControl = new StringBuilder();
			string TableKey = "SickTypeNo";
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
					strSql.Append(" LabSickTypeNo , CName , ShortCode , DispOrder , HisOrderCode ");
					strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
					strSql.Append("SickTypeNo,CName,ShortCode,DispOrder,HisOrderCode");
					strSql.Append(" from SickType ");
					if (str.Trim() != "")
						strSql.Append(" where SickTypeNo not in (" + str + ")");

					strSqlControl.Append("insert into B_SickTypeControl ( ");
					strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
					strSqlControl.Append(")  select ");
                    strSqlControl.Append("  concat(concat(concat(concat(" + lst[i].Trim() + ",'_')," + TableKey + "),'_')," + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",'1' ");
					strSqlControl.Append(" from SickType ");
					if (str.Trim() != "")
						strSqlControl.Append(" where SickTypeNo not in (" + str + ")");

					arrySql.Add(strSql.ToString());
					arrySql.Add(strSqlControl.ToString());

					strSql = new StringBuilder();
					strSqlControl = new StringBuilder();
				}

				DbHelperSQL.BatchUpdateWithTransaction(arrySql);
				//d_log.OperateLog("SickType", "", "", DateTime.Now, 1);
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
			strSql.Append("select SickTypeNo from B_SickTypeControl where ControlLabNo=" + strLabCode);
			DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
			if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					if (str == "")
						str = "'" + dr["SickTypeNo"].ToString().Trim() + "'";
					else
						str += ",'" + dr["SickTypeNo"].ToString().Trim() + "'";
				}
			}
			return str;
		}

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("SickTypeNo", "SickType");
		}

		public DataSet GetList(int Top, ZhiFang.Model.SickType model, string filedOrder)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			
			strSql.Append(" * ");
			strSql.Append(" FROM SickType ");



			if (model.SickTypeNo != 0)
			{
				strSql.Append(" and SickTypeNo='" + model.SickTypeNo + "' ");
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
           
            strSql.Append(" and ROWNUM <= '" + Top + "'");
           
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

						if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["SickTypeNo"].ToString().Trim())))
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
				strSql.Append("insert into SickType (");
				strSql.Append("SickTypeNo,CName,ShortCode,DispOrder,HisOrderCode");
				strSql.Append(") values (");

				if (dr.Table.Columns["SickTypeNo"] != null && dr.Table.Columns["SickTypeNo"].ToString().Trim() != "")
				{
					strSql.Append(" '" + dr["SickTypeNo"].ToString().Trim() + "', ");
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
				ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.SickType.AddByDataRow 同步数据时异常：", ex);
				return 0;
			}
		}
		public int UpdateByDataRow(DataRow dr)
		{
			try
			{
				StringBuilder strSql = new StringBuilder();
				strSql.Append("update SickType set ");


				if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
				{
					strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
				}


				if (dr.Table.Columns["ShortCode"] != null && dr.Table.Columns["ShortCode"].ToString().Trim() != "")
				{
					strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
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
				strSql.Append(" where SickTypeNo='" + dr["SickTypeNo"].ToString().Trim() + "' ");

				return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			}
			catch (Exception ex)
			{
				ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.SickType .UpdateByDataRow同步数据时异常：", ex);
				return 0;
			}
		}

		#region IDGetListByTimeStampe 成员

		public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
		{
			DataSet dsAll = new DataSet();
			StringBuilder strSql = new StringBuilder();
            strSql.Append("select SickType.*,'" + LabCode + "' as LabCode,SickTypeNo as LabSickTypeNo from SickType where 1=1 ");
			if (dTimeStampe != -999999)
			{
				strSql.Append(" and TO_CHAR(DTimeStampe,'YYYY-MM-DD:HH24:MI:SS') > '" + dTimeStampe + "' ");
			}
			DataTable dtServer = DbHelperSQL.ExecuteDataSet(strSql.ToString()).Tables[0];
			dtServer.TableName = "ServerDatas";

			StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select B_Lab_SickType.*,LabSickTypeNo as SickTypeNo from B_Lab_SickType where 1=1 ");
			if (LabCode.Trim() != "")
			{
				strSql2.Append(" and LabCode= '" + LabCode.Trim() + "' ");
			}
			if (dTimeStampe != -999999)
			{
				strSql2.Append(" and TO_CHAR(DTimeStampe,'YYYY-MM-DD:HH24:MI:SS') > '" + dTimeStampe + "' ");
			}
			DataTable dtLab = DbHelperSQL.ExecuteDataSet(strSql2.ToString()).Tables[0];
			dtLab.TableName = "LabDatas";

			StringBuilder strSql3 = new StringBuilder();
			strSql3.Append("select * from B_SickTypeControl where 1=1 ");
			if (LabCode.Trim() != "")
			{
				strSql3.Append(" and ControlLabNo= '" + LabCode.Trim() + "' ");
			}
			if (dTimeStampe != -999999)
			{
				strSql3.Append(" and TO_CHAR(DTimeStampe,'YYYY-MM-DD:HH24:MI:SS') > '" + dTimeStampe + "' ");
			}
			DataTable dtControl = DbHelperSQL.ExecuteDataSet(strSql3.ToString()).Tables[0];
			dtControl.TableName = "ControlDatas";

			dsAll.Tables.Add(dtServer.Copy());
			dsAll.Tables.Add(dtLab.Copy());
			dsAll.Tables.Add(dtControl.Copy());
			return dsAll;
		}

		#endregion



		#region IDBatchCopy 成员


		public int DeleteByDataRow(DataRow dr)
		{
			throw new NotImplementedException();
		}

		#endregion


        public bool IsExist(string labCodeNo)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select COUNT(1) from B_Lab_SickType ");
            strSql.Append(" where LabCode='" + labCodeNo + "' ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" select COUNT(1) from B_SickTypeControl ");
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
            strSql.Append(" delete from B_Lab_SickType ");
            strSql.Append(" where LabCode='" + LabCodeNo + "' ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" delete from B_SickTypeControl ");
            strSql2.Append(" where ControlLabNo='" + LabCodeNo + "' ");


            int i = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            int j = DbHelperSQL.ExecuteNonQuery(strSql2.ToString());
            if (i > 0 || j > 0)
                result = true;
            return result;
        }


        public int Add(List<Model.SickType> modelList)
        {
            throw new NotImplementedException();
        }


        public int Update(List<Model.SickType> modelList)
        {
            throw new NotImplementedException();
        }
    }
}

