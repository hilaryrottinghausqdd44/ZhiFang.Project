using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
    //BUSINESSMAN

    public partial class BUSINESSMAN : BaseDALLisDB, IDBUSINESSMAN, IDBatchCopy, IDGetListByTimeStampe
    {
        public BUSINESSMAN(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public BUSINESSMAN()
        {
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.BUSINESSMAN model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.CNAME != null)
            {
                strSql1.Append("CNAME,");
                strSql2.Append("'" + model.CNAME + "',");
            }
            if (model.BMANNO != null)
            {
                strSql1.Append("BMANNO,");
                strSql2.Append("" + model.BMANNO + ",");
            }
            if (model.SHORTCODE != null)
            {
                strSql1.Append("SHORTCODE,");
                strSql2.Append("'" + model.SHORTCODE + "',");
            }
            if (model.ISUSE != null)
            {
                strSql1.Append("ISUSE,");
                strSql2.Append("" + model.ISUSE + ",");
            }
            if (model.IDCODE != null)
            {
                strSql1.Append("IDCODE,");
                strSql2.Append("'" + model.IDCODE + "',");
            }
            if (model.ADDRESS != null)
            {
                strSql1.Append("ADDRESS,");
                strSql2.Append("'" + model.ADDRESS + "',");
            }
            if (model.PHONENUM != null)
            {
                strSql1.Append("PHONENUM,");
                strSql2.Append("'" + model.PHONENUM + "',");
            }
            if (model.ROMARK != null)
            {
                strSql1.Append("ROMARK,");
                strSql2.Append("'" + model.ROMARK + "',");
            }
            if (model.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("" + model.DispOrder + ",");
            }
            strSql.Append("insert into BUSINESSMAN(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());


            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("insert into BUSINESSMAN(");			
            //strSql.Append("CNAME,BMANNO,SHORTCODE,ISUSE,IDCODE,ADDRESS,PHONENUM,ROMARK,DispOrder");
            //strSql.Append(") values (");
            //strSql.Append("@CNAME,@BMANNO,@SHORTCODE,@ISUSE,@IDCODE,@ADDRESS,@PHONENUM,@ROMARK,@DispOrder");            
            //strSql.Append(") ");            

            //SqlParameter[] parameters = {
            //            new SqlParameter("@CNAME", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@BMANNO", SqlDbType.Int,4) ,            
            //            new SqlParameter("@SHORTCODE", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@ISUSE", SqlDbType.Int,4) ,            
            //            new SqlParameter("@IDCODE", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@ADDRESS", SqlDbType.VarChar,80) ,            
            //            new SqlParameter("@PHONENUM", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@ROMARK", SqlDbType.VarChar,200) ,            
            //            new SqlParameter("@DispOrder", SqlDbType.Int,4)             

            //};

            //parameters[0].Value = model.CNAME;                        
            //parameters[1].Value = model.BMANNO;                        
            //parameters[2].Value = model.SHORTCODE;                        
            //parameters[3].Value = model.ISUSE;                        
            //parameters[4].Value = model.IDCODE;                        
            //parameters[5].Value = model.ADDRESS;                        
            //parameters[6].Value = model.PHONENUM;                        
            //parameters[7].Value = model.ROMARK;                        
            //parameters[8].Value = model.DispOrder;                  		
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            //{
            //    return d_log.OperateLog("BUSINESSMAN", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.BUSINESSMAN model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BUSINESSMAN set ");
            if (model.CNAME != null)
            {
                strSql.Append("CNAME='" + model.CNAME + "',");
            }
            else
            {
                strSql.Append("CNAME= null ,");
            }
            if (model.SHORTCODE != null)
            {
                strSql.Append("SHORTCODE='" + model.SHORTCODE + "',");
            }
            else
            {
                strSql.Append("SHORTCODE= null ,");
            }
            if (model.ISUSE != null)
            {
                strSql.Append("ISUSE=" + model.ISUSE + ",");
            }
            else
            {
                strSql.Append("ISUSE= null ,");
            }
            if (model.IDCODE != null)
            {
                strSql.Append("IDCODE='" + model.IDCODE + "',");
            }
            else
            {
                strSql.Append("IDCODE= null ,");
            }
            if (model.ADDRESS != null)
            {
                strSql.Append("ADDRESS='" + model.ADDRESS + "',");
            }
            else
            {
                strSql.Append("ADDRESS= null ,");
            }
            if (model.PHONENUM != null)
            {
                strSql.Append("PHONENUM='" + model.PHONENUM + "',");
            }
            else
            {
                strSql.Append("PHONENUM= null ,");
            }
            if (model.ROMARK != null)
            {
                strSql.Append("ROMARK='" + model.ROMARK + "',");
            }
            else
            {
                strSql.Append("ROMARK= null ,");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            else
            {
                strSql.Append("DispOrder= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where BMANNO=" + model.BMANNO + " ");

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update BUSINESSMAN set ");

            //strSql.Append(" CNAME = @CNAME , ");
            //strSql.Append(" BMANNO = @BMANNO , ");
            //strSql.Append(" SHORTCODE = @SHORTCODE , ");
            //strSql.Append(" ISUSE = @ISUSE , ");
            //strSql.Append(" IDCODE = @IDCODE , ");
            //strSql.Append(" ADDRESS = @ADDRESS , ");
            //strSql.Append(" PHONENUM = @PHONENUM , ");
            //strSql.Append(" ROMARK = @ROMARK , ");
            //strSql.Append(" DispOrder = @DispOrder  ");
            //strSql.Append(" where BMANNO=@BMANNO  ");

            //SqlParameter[] parameters = {
			               
            //new SqlParameter("@CNAME", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@BMANNO", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@SHORTCODE", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@ISUSE", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@IDCODE", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@ADDRESS", SqlDbType.VarChar,80) ,            	
                           
            //new SqlParameter("@PHONENUM", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@ROMARK", SqlDbType.VarChar,200) ,            	
                           
            //new SqlParameter("@DispOrder", SqlDbType.Int,4)             	
              
            //};


            //if (model.CNAME != null)
            //{
            //    parameters[0].Value = model.CNAME;
            //}



            //if (model.BMANNO != null)
            //{
            //    parameters[1].Value = model.BMANNO;
            //}



            //if (model.SHORTCODE != null)
            //{
            //    parameters[2].Value = model.SHORTCODE;
            //}



            //if (model.ISUSE != null)
            //{
            //    parameters[3].Value = model.ISUSE;
            //}



            //if (model.IDCODE != null)
            //{
            //    parameters[4].Value = model.IDCODE;
            //}



            //if (model.ADDRESS != null)
            //{
            //    parameters[5].Value = model.ADDRESS;
            //}



            //if (model.PHONENUM != null)
            //{
            //    parameters[6].Value = model.PHONENUM;
            //}



            //if (model.ROMARK != null)
            //{
            //    parameters[7].Value = model.ROMARK;
            //}



            //if (model.DispOrder != null)
            //{
            //    parameters[8].Value = model.DispOrder;
            //}


            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return d_log.OperateLog("BUSINESSMAN", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int BMANNO)
        {

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("delete from BUSINESSMAN ");
            //strSql.Append(" where BMANNO=@BMANNO ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@BMANNO", SqlDbType.Int,4)};
            //parameters[0].Value = BMANNO;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BUSINESSMAN ");
            strSql.Append(" where BMANNO=" + BMANNO + " ");

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BUSINESSMAN ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.BUSINESSMAN GetModel(int BMANNO)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CNAME, BMANNO, SHORTCODE, ISUSE, IDCODE, ADDRESS, PHONENUM, ROMARK, DispOrder  ");
            strSql.Append("  from BUSINESSMAN ");
            //strSql.Append(" where BMANNO=@BMANNO ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@BMANNO", SqlDbType.Int,4)};
            //parameters[0].Value = BMANNO;
            strSql.Append(" where BMANNO=" + BMANNO + " ");

            ZhiFang.Model.BUSINESSMAN model = new ZhiFang.Model.BUSINESSMAN();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {

                model.CNAME = ds.Tables[0].Rows[0]["CNAME"].ToString();

                if (ds.Tables[0].Rows[0]["BMANNO"].ToString() != "")
                {
                    model.BMANNO = int.Parse(ds.Tables[0].Rows[0]["BMANNO"].ToString());
                }

                model.SHORTCODE = ds.Tables[0].Rows[0]["SHORTCODE"].ToString();

                if (ds.Tables[0].Rows[0]["ISUSE"].ToString() != "")
                {
                    model.ISUSE = int.Parse(ds.Tables[0].Rows[0]["ISUSE"].ToString());
                }

                model.IDCODE = ds.Tables[0].Rows[0]["IDCODE"].ToString();

                model.ADDRESS = ds.Tables[0].Rows[0]["ADDRESS"].ToString();

                model.PHONENUM = ds.Tables[0].Rows[0]["PHONENUM"].ToString();

                model.ROMARK = ds.Tables[0].Rows[0]["ROMARK"].ToString();

                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
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
            strSql.Append("select * ");
            strSql.Append(" FROM BUSINESSMAN ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.BUSINESSMAN model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM BUSINESSMAN where 1=1 ");

            if (model.CNAME != null)
            {
                strSql.Append(" and CNAME='" + model.CNAME + "' ");
            }


            if (model.BMANNO != 0)
            {
                strSql.Append(" and BMANNO=" + model.BMANNO + " ");
            }

            if (model.SHORTCODE != null)
            {
                strSql.Append(" and SHORTCODE='" + model.SHORTCODE + "' ");
            }

            if (model.ISUSE != null)
            {
                strSql.Append(" and ISUSE=" + model.ISUSE + " ");
            }

            if (model.IDCODE != null)
            {
                strSql.Append(" and IDCODE='" + model.IDCODE + "' ");
            }

            if (model.ADDRESS != null)
            {
                strSql.Append(" and ADDRESS='" + model.ADDRESS + "' ");
            }

            if (model.PHONENUM != null)
            {
                strSql.Append(" and PHONENUM='" + model.PHONENUM + "' ");
            }

            if (model.ROMARK != null)
            {
                strSql.Append(" and ROMARK='" + model.ROMARK + "' ");
            }

            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + " ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListLike(ZhiFang.Model.BUSINESSMAN model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BUSINESSMAN.*,BMANNO as LabNo,concat(concat(ClIENTNO,'_'),CNAME) as LabNo_Value,concat(concat(concat(CName,'('),BMANNO),')') as LabNoAndName_Text ");
            strSql.Append(" FROM BUSINESSMAN where 1=1 ");
            if (model.CNAME != null)
            {
                strSql.Append(" where 1=2 ");
                strSql.Append(" or CName like '%" + model.CNAME + "%' ");
            }

            if (model.BMANNO != 0)
            {
                strSql.Append(" or BMANNO like '%" + model.BMANNO + "%' ");
            }

            if (model.SHORTCODE != null)
            {
                strSql.Append(" or ShortCode like '%" + model.SHORTCODE + "%' ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM BUSINESSMAN ");
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
        public int GetTotalCount(ZhiFang.Model.BUSINESSMAN model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM BUSINESSMAN where 1=1 ");


            if (model.CNAME != null)
            {
                strSql.Append(" and CNAME='" + model.CNAME + "' ");
            }


            if (model.BMANNO != 0)
            {
                strSql.Append(" and BMANNO='" + model.BMANNO + "' ");
            }


            if (model.SHORTCODE != null)
            {
                strSql.Append(" and SHORTCODE='" + model.SHORTCODE + "' ");
            }


            if (model.ISUSE != null)
            {
                strSql.Append(" and ISUSE='" + model.ISUSE + "' ");
            }


            if (model.IDCODE != null)
            {
                strSql.Append(" and IDCODE='" + model.IDCODE + "' ");
            }


            if (model.ADDRESS != null)
            {
                strSql.Append(" and ADDRESS='" + model.ADDRESS + "' ");
            }


            if (model.PHONENUM != null)
            {
                strSql.Append(" and PHONENUM='" + model.PHONENUM + "' ");
            }


            if (model.ROMARK != null)
            {
                strSql.Append(" and ROMARK='" + model.ROMARK + "' ");
            }


            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder='" + model.DispOrder + "' ");
            }

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
        public DataSet GetListByPage(ZhiFang.Model.BUSINESSMAN model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select   * from BUSINESSMAN left join BUSINESSMANControl on BUSINESSMAN.BMANNO=BUSINESSMANControl.BMANNO ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and BUSINESSMANControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where  ROWNUM <= '" + nowPageSize + "' and BMANNO not in ( ");
                strSql.Append("select  BMANNO from  BUSINESSMAN left join BUSINESSMANControl on BUSINESSMAN.BMANNO=BUSINESSMANControl.BMANNO  where 1=1 ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and BUSINESSMANControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" and ROWNUM <= '" + (nowPageSize * nowPageNum) + "'  ) order by BUSINESSMAN.BMANNO ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select  * from BUSINESSMAN where  ROWNUM <= '" + nowPageSize + "' and BMANNO not in  ");
                strSql.Append("(select  BMANNO from BUSINESSMAN where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "') order by BMANNO  ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }

        public bool Exists(int BMANNO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BUSINESSMAN ");
            strSql.Append(" where BMANNO ='" + BMANNO + "'");
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
            strSql.Append("select count(1) from BUSINESSMAN where 1=1 ");
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
            string LabTableName = "BUSINESSMAN";
            LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey = "BMANNO";
            string TableKeySub = TableKey;
            if (TableKey.ToLower().Contains("no"))
            {
                TableKeySub = TableKey.Substring(0, TableKey.ToLower().IndexOf("no"));
            }
            try
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    strSql.Append("insert into " + LabTableName + "( LabCode,");
                    strSql.Append(" CNAME , LabBMANNO , SHORTCODE , ISUSE , IDCODE , ADDRESS , PHONENUM , ROMARK , DispOrder ");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("CNAME,BMANNO,SHORTCODE,ISUSE,IDCODE,ADDRESS,PHONENUM,ROMARK,DispOrder");
                    strSql.Append(" from BUSINESSMAN ");

                    strSqlControl.Append("insert into BUSINESSMANControl ( ");
                    strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  concat(concat(concat(concat(" + lst[i].Trim() + ",'_')," + TableKey + "),'_')," + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
                    strSqlControl.Append(" from BUSINESSMAN ");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();

                }

                DbHelperSQL.BatchUpdateWithTransaction(arrySql);
                d_log.OperateLog("BUSINESSMAN", "", "", DateTime.Now, 1);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("BMANNO", "BUSINESSMAN");
        }

        public DataSet GetList(int Top, ZhiFang.Model.BUSINESSMAN model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" * ");
            strSql.Append(" FROM BUSINESSMAN ");

            strSql.Append(" where 1=1 ");

            if (model.CNAME != null)
            {
                strSql.Append(" and CNAME='" + model.CNAME + "' ");
            }


            if (model.BMANNO != 0)
            {
                strSql.Append(" and BMANNO='" + model.BMANNO + "' ");
            }


            if (model.SHORTCODE != null)
            {
                strSql.Append(" and SHORTCODE='" + model.SHORTCODE + "' ");
            }


            if (model.ISUSE != null)
            {
                strSql.Append(" and ISUSE='" + model.ISUSE + "' ");
            }


            if (model.IDCODE != null)
            {
                strSql.Append(" and IDCODE='" + model.IDCODE + "' ");
            }


            if (model.ADDRESS != null)
            {
                strSql.Append(" and ADDRESS='" + model.ADDRESS + "' ");
            }


            if (model.PHONENUM != null)
            {
                strSql.Append(" and PHONENUM='" + model.PHONENUM + "' ");
            }


            if (model.ROMARK != null)
            {
                strSql.Append(" and ROMARK='" + model.ROMARK + "' ");
            }


            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder='" + model.DispOrder + "' ");
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

                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["BMANNO"].ToString().Trim())))
                        {
                            System.Threading.Thread.Sleep(ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ThreadDicSynchInterval"));
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ThreadDicSynchInterval"));
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
                strSql.Append("insert into BUSINESSMAN (");
                strSql.Append("CNAME,BMANNO,SHORTCODE,ISUSE,IDCODE,ADDRESS,PHONENUM,ROMARK,DispOrder");
                strSql.Append(") values (");

                if (dr.Table.Columns["CNAME"] != null && dr.Table.Columns["CNAME"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CNAME"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["BMANNO"] != null && dr.Table.Columns["BMANNO"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["BMANNO"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["SHORTCODE"] != null && dr.Table.Columns["SHORTCODE"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SHORTCODE"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ISUSE"] != null && dr.Table.Columns["ISUSE"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ISUSE"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["IDCODE"] != null && dr.Table.Columns["IDCODE"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IDCODE"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ADDRESS"] != null && dr.Table.Columns["ADDRESS"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ADDRESS"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["PHONENUM"] != null && dr.Table.Columns["PHONENUM"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["PHONENUM"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ROMARK"] != null && dr.Table.Columns["ROMARK"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ROMARK"].ToString().Trim() + "', ");
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

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.BUSINESSMAN.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update BUSINESSMAN set ");


                if (dr.Table.Columns["CNAME"] != null && dr.Table.Columns["CNAME"].ToString().Trim() != "")
                {
                    strSql.Append(" CNAME = '" + dr["CNAME"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["SHORTCODE"] != null && dr.Table.Columns["SHORTCODE"].ToString().Trim() != "")
                {
                    strSql.Append(" SHORTCODE = '" + dr["SHORTCODE"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ISUSE"] != null && dr.Table.Columns["ISUSE"].ToString().Trim() != "")
                {
                    strSql.Append(" ISUSE = '" + dr["ISUSE"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["IDCODE"] != null && dr.Table.Columns["IDCODE"].ToString().Trim() != "")
                {
                    strSql.Append(" IDCODE = '" + dr["IDCODE"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ADDRESS"] != null && dr.Table.Columns["ADDRESS"].ToString().Trim() != "")
                {
                    strSql.Append(" ADDRESS = '" + dr["ADDRESS"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["PHONENUM"] != null && dr.Table.Columns["PHONENUM"].ToString().Trim() != "")
                {
                    strSql.Append(" PHONENUM = '" + dr["PHONENUM"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ROMARK"] != null && dr.Table.Columns["ROMARK"].ToString().Trim() != "")
                {
                    strSql.Append(" ROMARK = '" + dr["ROMARK"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["DispOrder"] != null && dr.Table.Columns["DispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" DispOrder = '" + dr["DispOrder"].ToString().Trim() + "' , ");
                }


                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where BMANNO='" + dr["BMANNO"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.BUSINESSMAN .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        public int DeleteByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (dr.Table.Columns["BMANNO"] != null && dr.Table.Columns["BMANNO"].ToString().Trim() != "")
                {
                    strSql.Append("delete from BUSINESSMAN ");
                    strSql.Append(" where BMANNO='" + dr["BMANNO"].ToString().Trim() + "' ");
                    return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.BUSINESSMAN.DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }

        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BUSINESSMAN.*,'" + LabCode + "' as LabCode,BMANNO as LabBMANNO from BUSINESSMAN where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and TO_CHAR(DTimeStampe,'YYYY-MM-DD:HH24:MI:SS') > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = DbHelperSQL.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select B_Lab_BUSINESSMAN.*,LabBMANNO as BMANNO from B_Lab_BUSINESSMAN where 1=1 ");
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
            strSql3.Append("select * from B_BUSINESSMANControl where 1=1 ");
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




        public bool IsExist(string labCodeNo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            throw new NotImplementedException();
        }
    }
}

