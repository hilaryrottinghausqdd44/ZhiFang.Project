using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
    //Modules		
    public partial class S_Modules : IDModules
    {
        DBUtility.IDBConnection idb;
        public S_Modules(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public S_Modules()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Modules model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.SN != null)
            {
                strSql1.Append("SN,");
                strSql2.Append("'" + model.SN + "',");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.EName != null)
            {
                strSql1.Append("EName,");
                strSql2.Append("'" + model.EName + "',");
            }
            if (model.SName != null)
            {
                strSql1.Append("SName,");
                strSql2.Append("'" + model.SName + "',");
            }
            if (model.Type != null)
            {
                strSql1.Append("Type,");
                strSql2.Append("" + model.Type + ",");
            }
            if (model.Image != null)
            {
                strSql1.Append("Image,");
                strSql2.Append("'" + model.Image + "',");
            }
            if (model.URL != null)
            {
                strSql1.Append("URL,");
                strSql2.Append("'" + model.URL + "',");
            }
            if (model.Para != null)
            {
                strSql1.Append("Para,");
                strSql2.Append("'" + model.Para + "',");
            }
            if (model.Descr != null)
            {
                strSql1.Append("Descr,");
                strSql2.Append("'" + model.Descr + "',");
            }
            if (model.ButtonsTheme != null)
            {
                strSql1.Append("ButtonsTheme,");
                strSql2.Append("'" + model.ButtonsTheme + "',");
            }
            if (model.Owner != null)
            {
                strSql1.Append("Owner,");
                strSql2.Append("" + model.Owner + ",");
            }
            if (model.CreateDate != null)
            {
                strSql1.Append("CreateDate,");
                strSql2.Append("'" + model.CreateDate + "',");
            }
            if (model.ModuleCode != null)
            {
                strSql1.Append("ModuleCode,");
                strSql2.Append("'" + model.ModuleCode + "',");
            }
            strSql.Append("insert into S_Modules(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            DataSet ds=idb.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString().Trim());
            }
            else
                return -1;

            //int subcount = 0;
            //if (model.PSN == null || model.PID == 0)
            //{
            //    subcount=int.Parse(idb.ExecuteScalar(" select count(*) from S_Modules where LEN(SN)=2 "));
            //}
            //else
            //{
            //    subcount = int.Parse(idb.ExecuteScalar(" select count(*) from S_Modules where  SN LIKE '"+ model.PSN+"%' and Len(SN)=" + (model.Rank*2).ToString() ));
            //}
            //if (subcount > 9)
            //{
            //    model.SN = model.PSN + (subcount + 1).ToString();
            //}
            //else
            //{
            //    model.SN = model.PSN + "0" + (subcount + 1).ToString();
            //}
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into S_Modules(");
            //strSql.Append("Descr,ButtonsTheme,Owner,CreateDate,ModuleCode,SN,CName,EName,SName,Type,Image,URL,Para");
            //strSql.Append(") values (");
            //strSql.Append("@Descr,@ButtonsTheme,@Owner,@CreateDate,@ModuleCode,@SN,@CName,@EName,@SName,@Type,@Image,@URL,@Para");
            //strSql.Append(") ;update S_Modules set ButtonsTheme='~'+Convert(varchar,IDENT_CURRENT ('S_Modules')) where ID=IDENT_CURRENT ('S_Modules') ;select IDENT_CURRENT ('S_Modules') ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@Descr", SqlDbType.NVarChar,50) ,            
            //            new SqlParameter("@ButtonsTheme", SqlDbType.NVarChar,40) ,            
            //            new SqlParameter("@Owner", SqlDbType.Int,4) ,            
            //            new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
            //            new SqlParameter("@ModuleCode", SqlDbType.VarChar,30) ,            
            //            new SqlParameter("@SN", SqlDbType.NVarChar,50) ,            
            //            new SqlParameter("@CName", SqlDbType.NVarChar,50) ,            
            //            new SqlParameter("@EName", SqlDbType.NVarChar,50) ,            
            //            new SqlParameter("@SName", SqlDbType.NVarChar,50) ,            
            //            new SqlParameter("@Type", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Image", SqlDbType.NVarChar,250) ,            
            //            new SqlParameter("@URL", SqlDbType.NVarChar,150) ,            
            //            new SqlParameter("@Para", SqlDbType.NVarChar,500)             
              
            //};

            //parameters[0].Value = model.Descr;
            //parameters[1].Value = model.ButtonsTheme;
            //parameters[2].Value = model.Owner;
            //parameters[3].Value = model.CreateDate;
            //parameters[4].Value = model.ModuleCode;
            //parameters[5].Value = model.SN;
            //parameters[6].Value = model.CName;
            //parameters[7].Value = model.EName;
            //parameters[8].Value = model.SName;
            //parameters[9].Value = model.Type;
            //parameters[10].Value = model.Image;
            //parameters[11].Value = model.URL;
            //parameters[12].Value = model.Para;
            //DataSet ds=idb.ExecuteDataSet(strSql.ToString(), parameters);
            //if ( ds !=null && ds.Tables.Count> 0 && ds.Tables[0].Rows.Count>0)
            //{
            //    return int.Parse(ds.Tables[0].Rows[0][0].ToString().Trim());
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Modules model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update S_Modules set ");
            if (model.SN != null)
            {
                strSql.Append("SN='" + model.SN + "',");
            }
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.EName != null)
            {
                strSql.Append("EName='" + model.EName + "',");
            }
            else
            {
                strSql.Append("EName= null ,");
            }
            if (model.SName != null)
            {
                strSql.Append("SName='" + model.SName + "',");
            }
            else
            {
                strSql.Append("SName= null ,");
            }
            if (model.Type != null)
            {
                strSql.Append("Type=" + model.Type + ",");
            }
            else
            {
                strSql.Append("Type= null ,");
            }
            if (model.Image != null)
            {
                strSql.Append("Image='" + model.Image + "',");
            }
            else
            {
                strSql.Append("Image= null ,");
            }
            if (model.URL != null)
            {
                strSql.Append("URL='" + model.URL + "',");
            }
            else
            {
                strSql.Append("URL= null ,");
            }
            if (model.Para != null)
            {
                strSql.Append("Para='" + model.Para + "',");
            }
            else
            {
                strSql.Append("Para= null ,");
            }
            if (model.Descr != null)
            {
                strSql.Append("Descr='" + model.Descr + "',");
            }
            else
            {
                strSql.Append("Descr= null ,");
            }
            if (model.ButtonsTheme != null)
            {
                strSql.Append("ButtonsTheme='" + model.ButtonsTheme + "',");
            }
            else
            {
                strSql.Append("ButtonsTheme= null ,");
            }
            if (model.Owner != null)
            {
                strSql.Append("Owner=" + model.Owner + ",");
            }
            if (model.CreateDate != null)
            {
                strSql.Append("CreateDate='" + model.CreateDate + "',");
            }
            else
            {
                strSql.Append("CreateDate= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ID=" + model.ID + "");

            if (idb.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return 1;
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update S_Modules set ");

            //strSql.Append(" Descr = @Descr , ");
            //strSql.Append(" ButtonsTheme = @ButtonsTheme , ");
            //strSql.Append(" Owner = @Owner , ");
            //strSql.Append(" CreateDate = @CreateDate , ");
            //strSql.Append(" ModuleCode = @ModuleCode , ");
            //strSql.Append(" SN = @SN , ");
            //strSql.Append(" CName = @CName , ");
            //strSql.Append(" EName = @EName , ");
            //strSql.Append(" SName = @SName , ");
            //strSql.Append(" Type = @Type , ");
            //strSql.Append(" Image = @Image , ");
            //strSql.Append(" URL = @URL , ");
            //strSql.Append(" Para = @Para  ");
            //strSql.Append(" where ID=@ID ");

            //SqlParameter[] parameters = {
			            	
                           
            //new SqlParameter("@Descr", SqlDbType.NVarChar,50) ,            	
                           
            //new SqlParameter("@ButtonsTheme", SqlDbType.NVarChar,40) ,            	
                           
            //new SqlParameter("@Owner", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            	
                           
            //new SqlParameter("@ModuleCode", SqlDbType.VarChar,30) ,            	
                           
            //new SqlParameter("@SN", SqlDbType.NVarChar,50) ,            	
                           
            //new SqlParameter("@CName", SqlDbType.NVarChar,50) ,            	
                           
            //new SqlParameter("@EName", SqlDbType.NVarChar,50) ,            	
                           
            //new SqlParameter("@SName", SqlDbType.NVarChar,50) ,            	
                           
            //new SqlParameter("@Type", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@Image", SqlDbType.NVarChar,250) ,            	
                           
            //new SqlParameter("@URL", SqlDbType.NVarChar,150) ,            	
                           
            //new SqlParameter("@Para", SqlDbType.NVarChar,500)             	
              
            //};




            //if (model.Descr != null)
            //{
            //    parameters[0].Value = model.Descr;
            //}



            //if (model.ButtonsTheme != null)
            //{
            //    parameters[1].Value = model.ButtonsTheme;
            //}



            //if (model.Owner != null)
            //{
            //    parameters[2].Value = model.Owner;
            //}



            //if (model.CreateDate != null)
            //{
            //    parameters[3].Value = model.CreateDate;
            //}



            //if (model.ModuleCode != null)
            //{
            //    parameters[4].Value = model.ModuleCode;
            //}



            //if (model.SN != null)
            //{
            //    parameters[5].Value = model.SN;
            //}



            //if (model.CName != null)
            //{
            //    parameters[6].Value = model.CName;
            //}



            //if (model.EName != null)
            //{
            //    parameters[7].Value = model.EName;
            //}



            //if (model.SName != null)
            //{
            //    parameters[8].Value = model.SName;
            //}



            //if (model.Type != null)
            //{
            //    parameters[9].Value = model.Type;
            //}



            //if (model.Image != null)
            //{
            //    parameters[10].Value = model.Image;
            //}



            //if (model.URL != null)
            //{
            //    parameters[11].Value = model.URL;
            //}



            //if (model.Para != null)
            //{
            //    parameters[12].Value = model.Para;
            //}


            //if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            //{
            //    return 1;
            //}
            //else
            //    return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from S_Modules ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


            return  idb.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from S_Modules ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = idb.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Modules GetModel(int ID)
        {

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select ID, Descr, ButtonsTheme, Owner, CreateDate, ModuleCode, SN, CName, EName, SName, Type, Image, URL, Para  ");
            //strSql.Append("  from S_Modules ");
            //strSql.Append(" where ID=@ID");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@ID", SqlDbType.Int,4)
            //};
            //parameters[0].Value = ID;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" ID, Descr, ButtonsTheme, Owner, CreateDate, ModuleCode, SN, CName, EName, SName, Type, Image, URL, Para  ");
            strSql.Append(" from S_Modules ");
            strSql.Append(" where 1=1 and ROWNUM <= '1' ID=" + ID + "");

            ZhiFang.Model.Modules model = new ZhiFang.Model.Modules();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Descr = ds.Tables[0].Rows[0]["Descr"].ToString();
                model.ButtonsTheme = ds.Tables[0].Rows[0]["ButtonsTheme"].ToString();
                if (ds.Tables[0].Rows[0]["Owner"].ToString() != "")
                {
                    model.Owner = int.Parse(ds.Tables[0].Rows[0]["Owner"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                model.ModuleCode = ds.Tables[0].Rows[0]["ModuleCode"].ToString();
                model.SN = ds.Tables[0].Rows[0]["SN"].ToString();
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.EName = ds.Tables[0].Rows[0]["EName"].ToString();
                model.SName = ds.Tables[0].Rows[0]["SName"].ToString();
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                model.Image = ds.Tables[0].Rows[0]["Image"].ToString();
                model.URL = ds.Tables[0].Rows[0]["URL"].ToString();
                model.Para = ds.Tables[0].Rows[0]["Para"].ToString();

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
            strSql.Append(" FROM S_Modules ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
           
            strSql.Append(" * ");
            strSql.Append(" FROM S_Modules ");
            
            strSql.Append(" and ROWNUM <= '" + Top + "'");
           
            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.Modules model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM S_Modules where 1=1 ");

            if (model.Descr != null)
            {
                strSql.Append(" and Descr='" + model.Descr + "' ");
            }

            if (model.ButtonsTheme != null)
            {
                strSql.Append(" and ButtonsTheme='" + model.ButtonsTheme + "' ");
            }

            if (model.Owner != null)
            {
                strSql.Append(" and Owner=" + model.Owner + " ");
            }

            if (model.CreateDate != null)
            {
                strSql.Append(" and CreateDate='" + model.CreateDate + "' ");
            }

            if (model.ModuleCode != null)
            {
                strSql.Append(" and ModuleCode='" + model.ModuleCode + "' ");
            }

            if (model.SN != null)
            {
                strSql.Append(" and SN='" + model.SN + "' ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.EName != null)
            {
                strSql.Append(" and EName='" + model.EName + "' ");
            }

            if (model.SName != null)
            {
                strSql.Append(" and SName='" + model.SName + "' ");
            }

            if (model.Type != null)
            {
                strSql.Append(" and Type=" + model.Type + " ");
            }

            if (model.Image != null)
            {
                strSql.Append(" and Image='" + model.Image + "' ");
            }

            if (model.URL != null)
            {
                strSql.Append(" and URL='" + model.URL + "' ");
            }

            if (model.Para != null)
            {
                strSql.Append(" and Para='" + model.Para + "' ");
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM S_Modules ");
            string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }
        public int GetTotalCount(ZhiFang.Model.Modules model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM S_Modules where 1=1 ");

            if (model.Descr != null)
            {
                strSql.Append(" and Descr='" + model.Descr + "' ");
            }

            if (model.ButtonsTheme != null)
            {
                strSql.Append(" and ButtonsTheme='" + model.ButtonsTheme + "' ");
            }

            if (model.Owner != null)
            {
                strSql.Append(" and Owner=" + model.Owner + " ");
            }

            if (model.CreateDate != null)
            {
                strSql.Append(" and CreateDate='" + model.CreateDate + "' ");
            }

            if (model.ModuleCode != null)
            {
                strSql.Append(" and ModuleCode='" + model.ModuleCode + "' ");
            }

            if (model.SN != null)
            {
                strSql.Append(" and SN='" + model.SN + "' ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.EName != null)
            {
                strSql.Append(" and EName='" + model.EName + "' ");
            }

            if (model.SName != null)
            {
                strSql.Append(" and SName='" + model.SName + "' ");
            }

            if (model.Type != null)
            {
                strSql.Append(" and Type=" + model.Type + " ");
            }

            if (model.Image != null)
            {
                strSql.Append(" and Image='" + model.Image + "' ");
            }

            if (model.URL != null)
            {
                strSql.Append(" and URL='" + model.URL + "' ");
            }

            if (model.Para != null)
            {
                strSql.Append(" and Para='" + model.Para + "' ");
            }
            string strCount = idb.ExecuteScalar(strSql.ToString());
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
        public DataSet GetListByPage(ZhiFang.Model.Modules model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select * from S_Modules left join S_ModulesControl on S_Modules.ID=S_ModulesControl.ID ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and S_ModulesControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where  ROWNUM <= '" + nowPageSize + "' and ID not in ( ");
                strSql.Append("select  ID from  S_Modules left join S_ModulesControl on S_Modules.ID=S_ModulesControl.ID where 1=1 ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and S_ModulesControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' ) order by S_Modules.ID ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select * from S_Modules where  ROWNUM <= '" + nowPageSize + "' and ID not in  ");
                strSql.Append("(select ID from S_Modules where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "') order by ID  ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
        }

        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from S_Modules ");
            strSql.Append(" where ID ='" + ID + "'");
            string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "" && strCount.Trim() != "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

        public int GetMaxId()
        {
            return idb.GetMaxID("ID", "S_Modules");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Modules model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
         
            strSql.Append(" * ");
            strSql.Append(" FROM S_Modules ");


            if (model.Descr != null)
            {

                strSql.Append(" and Descr='" + model.Descr + "' ");
            }

            if (model.ButtonsTheme != null)
            {

                strSql.Append(" and ButtonsTheme='" + model.ButtonsTheme + "' ");
            }

            if (model.Owner != null)
            {
                strSql.Append(" and Owner=" + model.Owner + " ");
            }

            if (model.CreateDate != null)
            {

                strSql.Append(" and CreateDate='" + model.CreateDate + "' ");
            }

            if (model.ModuleCode != null)
            {

                strSql.Append(" and ModuleCode='" + model.ModuleCode + "' ");
            }

            if (model.SN != null)
            {

                strSql.Append(" and SN='" + model.SN + "' ");
            }

            if (model.CName != null)
            {

                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.EName != null)
            {

                strSql.Append(" and EName='" + model.EName + "' ");
            }

            if (model.SName != null)
            {

                strSql.Append(" and SName='" + model.SName + "' ");
            }

            if (model.Type != null)
            {
                strSql.Append(" and Type=" + model.Type + " ");
            }

            if (model.Image != null)
            {

                strSql.Append(" and Image='" + model.Image + "' ");
            }

            if (model.URL != null)
            {

                strSql.Append(" and URL='" + model.URL + "' ");
            }

            if (model.Para != null)
            {

                strSql.Append(" and Para='" + model.Para + "' ");
            }
           
            strSql.Append(" and ROWNUM <= '" + Top + "'");
           
            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }



        #region IDModules 成员


        public DataSet GetListByRBACModulesList(List<string> rbac_moduleslist)
        {
            StringBuilder strSql = new StringBuilder();
            foreach (var a in rbac_moduleslist)
            {
                strSql.Append(" or  ModuleCode ='" + a + "' ");
            }
            strSql.Insert(0, " select * from S_Modules where 1=2 ");
            strSql.Append(" order by SN ");
            return idb.ExecuteDataSet(strSql.ToString());
        }

        #endregion

        #region IDataBase<Modules> 成员


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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["ID"].ToString().Trim())))
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
                strSql.Append("insert into S_Modules (");
                strSql.Append("SN,CName,EName,SName,Type,Image,URL,Para,Descr,ButtonsTheme,Owner,CreateDate,ModuleCode");
                strSql.Append(") values (");
                strSql.Append("'" + dr["SN"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["EName"].ToString().Trim() + "','" + dr["SName"].ToString().Trim() + "','" + dr["Type"].ToString().Trim() + "','" + dr["Image"].ToString().Trim() + "','" + dr["URL"].ToString().Trim() + "','" + dr["Para"].ToString().Trim() + "','" + dr["Descr"].ToString().Trim() + "','" + dr["ButtonsTheme"].ToString().Trim() + "','" + dr["Owner"].ToString().Trim() + "','" + dr["CreateDate"].ToString().Trim() + "','" + dr["ModuleCode"].ToString().Trim() + "'");
                strSql.Append(") ");
                return idb.ExecuteNonQuery(strSql.ToString());
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
                strSql.Append("update S_Modules set ");

                strSql.Append(" SN = '" + dr["SN"].ToString().Trim() + "' , ");
                strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                strSql.Append(" EName = '" + dr["EName"].ToString().Trim() + "' , ");
                strSql.Append(" SName = '" + dr["SName"].ToString().Trim() + "' , ");
                strSql.Append(" Type = '" + dr["Type"].ToString().Trim() + "' , ");
                strSql.Append(" Image = '" + dr["Image"].ToString().Trim() + "' , ");
                strSql.Append(" URL = '" + dr["URL"].ToString().Trim() + "' , ");
                strSql.Append(" Para = '" + dr["Para"].ToString().Trim() + "' , ");
                strSql.Append(" Descr = '" + dr["Descr"].ToString().Trim() + "' , ");
                strSql.Append(" ButtonsTheme = '" + dr["ButtonsTheme"].ToString().Trim() + "' , ");
                strSql.Append(" Owner = '" + dr["Owner"].ToString().Trim() + "' , ");
                strSql.Append(" CreateDate = '" + dr["CreateDate"].ToString().Trim() + "' , ");
                strSql.Append(" ModuleCode = '" + dr["ModuleCode"].ToString().Trim() + "'  ");
                strSql.Append(" where ID='" + dr["ID"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        

        #endregion
    }
}

