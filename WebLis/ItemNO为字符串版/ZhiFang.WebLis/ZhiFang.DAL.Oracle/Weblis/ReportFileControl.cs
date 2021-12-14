using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.Oracle.weblis
{
    //ReportFileInfo
    public partial class ReportFileInfo : BaseDALLisDB, IReportFileInfo
    {
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.ReportFileInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ReportFileInfo(");
            strSql.Append("OperaTion,PAT_ID,Card_ID,ClinicType,Name,Age,Sex,MobilePhone,Report_Time,Medical_Institution_Code,File_Url,ChangeStatus,G_ID,UniqueID,AddDataTime,File_Name,Medical_Institution_ID,ProjectCode,PageNo");
            strSql.Append(") values (");
            strSql.Append("@OperaTion,@PAT_ID,@Card_ID,@ClinicType,@Name,@Age,@Sex,@MobilePhone,@Report_Time,@Medical_Institution_Code,@File_Url,@ChangeStatus,@G_ID,@UniqueID,@AddDataTime,@File_Name,@Medical_Institution_ID,@ProjectCode,@PageNo");
            strSql.Append(") ");
            SqlParameter[] parameters = {            
                        new SqlParameter("@OperaTion", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@PAT_ID", SqlDbType.VarChar,150) ,   
                        new SqlParameter("@Card_ID", SqlDbType.VarChar,80) , 
                        new SqlParameter("@ClinicType", SqlDbType.VarChar,80) , 
                        new SqlParameter("@Name", SqlDbType.VarChar,20) ,      
                        new SqlParameter("@Age", SqlDbType.VarChar,20) ,   
                        new SqlParameter("@Sex", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@MobilePhone", SqlDbType.VarChar,32) ,            
                        new SqlParameter("@Report_Time", SqlDbType.VarChar,50) ,  
                        new SqlParameter("@Medical_Institution_Code", SqlDbType.VarChar,32) ,            
                        new SqlParameter("@File_Url", SqlDbType.VarChar,300),
                        new SqlParameter("@ChangeStatus", SqlDbType.Int,8),    
                        new SqlParameter("@G_ID", SqlDbType.BigInt,8),
                        new SqlParameter("@UniqueID", SqlDbType.BigInt,8),
                        new SqlParameter("@AddDataTime", SqlDbType.DateTime),
                        new SqlParameter("@File_Name",SqlDbType.VarChar,150),
                        new SqlParameter("@Medical_Institution_ID", SqlDbType.VarChar,90),
                        new SqlParameter("@ProjectCode", SqlDbType.VarChar,30),
                        new SqlParameter("@PageNo", SqlDbType.VarChar,20)
            };

            parameters[0].Value = model.OperaTion;
            parameters[1].Value = model.PAT_ID;
            parameters[2].Value = model.Card_ID;
            parameters[3].Value = model.ClinicType;
            parameters[4].Value = model.Name;
            parameters[5].Value = model.Age;
            parameters[6].Value = model.Sex;
            parameters[7].Value = model.MobilePhone;
            parameters[8].Value = model.Report_Time;
            parameters[9].Value = model.Medical_Institution_Code;
            parameters[10].Value = model.File_Url;
            parameters[11].Value = model.ChangeStatus;
            parameters[12].Value = model.G_ID;
            parameters[13].Value = model.UniqueID;
            parameters[14].Value = model.AddDataTime;
            parameters[15].Value = model.File_Name;
            parameters[16].Value = model.Medical_Institution_ID;
            parameters[17].Value = model.ProjectCode;
            parameters[18].Value = model.PageNo;
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                if (DbHelperSQL.ConnectionState == ConnectionState.Open)
                {
                    DbHelperSQL.Dispose();
                }
                return d_log.OperateLog("ReportFileInfo", "", "", DateTime.Now, 1);
            }
            else
                if (DbHelperSQL.ConnectionState == ConnectionState.Open)
                {
                    DbHelperSQL.Dispose();
                }
                return -1;

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.ReportFileInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ReportFileInfo set ");
            strSql.Append(" OperaTion = @OperaTion , ");
            strSql.Append(" PAT_ID = @PAT_ID , ");
            strSql.Append(" Card_ID = @Card_ID , ");
            strSql.Append(" ClinicType = @ClinicType , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" Age = @Age , ");
            strSql.Append(" Sex = @Sex , ");
            strSql.Append(" MobilePhone = @MobilePhone , ");
            strSql.Append(" Report_Time = @Report_Time , ");
            strSql.Append(" Medical_Institution_Code = @Medical_Institution_Code , ");
            strSql.Append(" ChangeStatus = @ChangeStatus,  ");
            strSql.Append(" G_ID = @G_ID,  ");
            strSql.Append(" File_Url = @File_Url,  ");
            strSql.Append(" AddDataTime = @AddDataTime, ");
            strSql.Append(" File_Name = @File_Name,  ");
            strSql.Append(" Medical_Institution_ID = @Medical_Institution_ID, ");
            strSql.Append(" ProjectCode = @ProjectCode, ");
            strSql.Append(" PageNo = @PageNo "); 
            strSql.Append(" where UniqueID=@UniqueID ");
            SqlParameter[] parameters = {           
                        new SqlParameter("@OperaTion", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@PAT_ID", SqlDbType.VarChar,150) ,   
                        new SqlParameter("@Card_ID", SqlDbType.VarChar,50) , 
                        new SqlParameter("@ClinicType", SqlDbType.VarChar,50) , 
                        new SqlParameter("@Name", SqlDbType.VarChar,20) , 
                        new SqlParameter("@Age", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Sex", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@MobilePhone", SqlDbType.VarChar,32) ,            
                        new SqlParameter("@Report_Time", SqlDbType.VarChar,50) , 
                        new SqlParameter("@Medical_Institution_Code", SqlDbType.VarChar,32) ,      
                        new SqlParameter("@ChangeStatus", SqlDbType.Int,8),
                        new SqlParameter("@G_ID", SqlDbType.BigInt,8),
                        new SqlParameter("@File_Url", SqlDbType.VarChar,150),
                        new SqlParameter("@UniqueID", SqlDbType.VarChar,100),
                        new SqlParameter("@AddDataTime", SqlDbType.DateTime,100),
                        new SqlParameter("@File_Name", SqlDbType.VarChar,200),
                        new SqlParameter("@Medical_Institution_ID", SqlDbType.VarChar,50),
                        new SqlParameter("@ProjectCode", SqlDbType.VarChar,30),
                        new SqlParameter("@PageNo", SqlDbType.VarChar,20)
            };

            parameters[0].Value = model.OperaTion;
            parameters[1].Value = model.PAT_ID;
            parameters[2].Value = model.Card_ID;
            parameters[3].Value = model.ClinicType;
            parameters[4].Value = model.Name;
            parameters[5].Value = model.Age;
            parameters[6].Value = model.Sex;
            parameters[7].Value = model.MobilePhone;
            parameters[8].Value = model.Report_Time;
            parameters[9].Value = model.Medical_Institution_Code;
            parameters[10].Value = model.ChangeStatus;
            parameters[11].Value = model.G_ID;
            parameters[12].Value = model.File_Url;
            parameters[13].Value = model.UniqueID;
            parameters[14].Value = model.AddDataTime;
            parameters[15].Value = model.File_Name;
            parameters[16].Value = model.Medical_Institution_ID;
            parameters[17].Value = model.ProjectCode;
            parameters[18].Value = model.PageNo;
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                if (DbHelperSQL.ConnectionState == ConnectionState.Open)
                {
                    DbHelperSQL.Dispose();
                }
                return d_log.OperateLog("ReportFileInfo", "", "", DateTime.Now, 1);
            }
            else
                if (DbHelperSQL.ConnectionState == ConnectionState.Open)
                {
                    DbHelperSQL.Dispose();
                }
                return -1;
            
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string UniqueID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ReportFileInfo ");
            strSql.Append(" where UniqueID=@UniqueID");
            SqlParameter[] parameters = {
					new SqlParameter("@UniqueID", SqlDbType.VarChar,100)
			};
            parameters[0].Value = UniqueID;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ReportFileInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select G_ID, OperaTion, PAT_ID,Card_ID,ClinicType, Name,Age, Sex, MobilePhone, Report_Time, Medical_Institution_ID,Medical_Institution_Code, File_Url,ChangeStatus,AddDataTime,File_Name,ProjectCode,PageNo;  ");
            strSql.Append("  from ReportFileInfo ");
            strSql.Append(" where UniqueID=@UniqueID");
            SqlParameter[] parameters = {
					new SqlParameter("@UniqueID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


            Model.ReportFileInfo model = new Model.ReportFileInfo();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["G_ID"].ToString() != "")
                {
                    model.G_ID = int.Parse(ds.Tables[0].Rows[0]["G_ID"].ToString());
                }
                model.OperaTion = ds.Tables[0].Rows[0]["OperaTion"].ToString();
                model.PAT_ID = ds.Tables[0].Rows[0]["PAT_ID"].ToString();
                model.Card_ID = ds.Tables[0].Rows[0]["Card_ID"].ToString();
                model.ClinicType = ds.Tables[0].Rows[0]["ClinicType"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.Age = ds.Tables[0].Rows[0]["Age"].ToString();
                model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                model.MobilePhone = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                model.Report_Time = ds.Tables[0].Rows[0]["Report_Time"].ToString();
                model.Medical_Institution_ID = ds.Tables[0].Rows[0]["Medical_Institution_ID"].ToString(); 
                model.Medical_Institution_Code = ds.Tables[0].Rows[0]["Medical_Institution_Code"].ToString();
                model.ProjectCode = ds.Tables[0].Rows[0]["ProjectCode"].ToString();
                model.PageNo = ds.Tables[0].Rows[0]["PageNo"].ToString();
                model.File_Name = ds.Tables[0].Rows[0]["File_Name"].ToString();
                model.File_Url = ds.Tables[0].Rows[0]["File_Url"].ToString();
                model.ChangeStatus = (int)ds.Tables[0].Rows[0]["ChangeStatus"];
                model.ChangeStatus = (int)ds.Tables[0].Rows[0]["UniqueID"];
                model.AddDataTime = (DateTime)ds.Tables[0].Rows[0]["AddDataTime"];
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
            strSql.Append(" FROM ReportFileInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            
            strSql.Append(" * ");
            strSql.Append(" FROM ReportFileInfo ");
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



        #region IDataBase<WhoNet_AntiData> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Add(Model.WhoNet_AntiData t)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.WhoNet_AntiData t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.WhoNet_AntiData t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, Model.WhoNet_AntiData t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.WhoNet_AntiData t)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}