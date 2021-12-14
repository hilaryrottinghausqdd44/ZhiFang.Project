using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.DAL.RBAC
{
    //HR_Employees
    public class HR_Employees : BaseDALLisDB
    {

        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from HR_Employees");
            strSql.Append(" where ID=" + ID + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.RBAC.Entity.HR_Employees model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.SN != null)
            {
                strSql1.Append("SN,");
                strSql2.Append("'" + model.SN + "',");
            }
            if (model.NameL != null)
            {
                strSql1.Append("NameL,");
                strSql2.Append("'" + model.NameL + "',");
            }
            if (model.NameF != null)
            {
                strSql1.Append("NameF,");
                strSql2.Append("'" + model.NameF + "',");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.Sex != null)
            {
                strSql1.Append("Sex,");
                strSql2.Append("'" + model.Sex + "',");
            }
            if (model.Birth.HasValue)
            {
                strSql1.Append("Birth,");
                strSql2.Append("'" + model.Birth + "',");
            }
            if (model.Email != null)
            {
                strSql1.Append("Email,");
                strSql2.Append("'" + model.Email + "',");
            }
            if (model.Tel1 != null)
            {
                strSql1.Append("Tel1,");
                strSql2.Append("'" + model.Tel1 + "',");
            }
            if (model.Tel2 != null)
            {
                strSql1.Append("Tel2,");
                strSql2.Append("'" + model.Tel2 + "',");
            }
            if (model.Mobile != null)
            {
                strSql1.Append("Mobile,");
                strSql2.Append("'" + model.Mobile + "',");
            }
            if (model.Address != null)
            {
                strSql1.Append("Address,");
                strSql2.Append("'" + model.Address + "',");
            }
            if (model.City != null)
            {
                strSql1.Append("City,");
                strSql2.Append("'" + model.City + "',");
            }
            if (model.Province != null)
            {
                strSql1.Append("Province,");
                strSql2.Append("'" + model.Province + "',");
            }
            if (model.Country != null)
            {
                strSql1.Append("Country,");
                strSql2.Append("'" + model.Country + "',");
            }
            if (model.Zip != null)
            {
                strSql1.Append("Zip,");
                strSql2.Append("'" + model.Zip + "',");
            }
            if (model.MaritalStatus != null)
            {
                strSql1.Append("MaritalStatus,");
                strSql2.Append("'" + model.MaritalStatus + "',");
            }
            if (model.EducationLevel != null)
            {
                strSql1.Append("EducationLevel,");
                strSql2.Append("'" + model.EducationLevel + "',");
            }
            if (model.Enabled != null)
            {
                strSql1.Append("Enabled,");
                strSql2.Append("" + model.Enabled + ",");
            }
            if (model.Pic != null)
            {
                strSql1.Append("Pic,");
                strSql2.Append("" + model.Pic + ",");
            }
            if (model.JoinDate != null)
            {
                strSql1.Append("JoinDate,");
                strSql2.Append("'" + model.JoinDate + "',");
            }
            if (model.IDCardNO != null)
            {
                strSql1.Append("IDCardNO,");
                strSql2.Append("'" + model.IDCardNO + "',");
            }
            if (model.DesktopTheme != null)
            {
                strSql1.Append("DesktopTheme,");
                strSql2.Append("" + model.DesktopTheme + ",");
            }
            if (model.FirstPamId != null)
            {
                strSql1.Append("FirstPamId,");
                strSql2.Append("'" + model.FirstPamId + "',");
            }
            if (model.Degree != null)
            {
                strSql1.Append("Degree,");
                strSql2.Append("'" + model.Degree + "',");
            }
            if (model.GraduateSchool != null)
            {
                strSql1.Append("GraduateSchool,");
                strSql2.Append("'" + model.GraduateSchool + "',");
            }
            if (model.ProfessionalCompetence != null)
            {
                strSql1.Append("ProfessionalCompetence,");
                strSql2.Append("'" + model.ProfessionalCompetence + "',");
            }
            if (model.EducationBackground != null)
            {
                strSql1.Append("EducationBackground,");
                strSql2.Append("'" + model.EducationBackground + "',");
            }
            if (model.HealthStatusRecord != null)
            {
                strSql1.Append("HealthStatusRecord,");
                strSql2.Append("'" + model.HealthStatusRecord + "',");
            }
            if (model.PoliticalLandscape != null)
            {
                strSql1.Append("PoliticalLandscape,");
                strSql2.Append("'" + model.PoliticalLandscape + "',");
            }
            if (model.Department != null)
            {
                strSql1.Append("Department,");
                strSql2.Append("'" + model.Department + "',");
            }
            if (model.Training != null)
            {
                strSql1.Append("Training,");
                strSql2.Append("'" + model.Training + "',");
            }
            if (model.PersonalHomePage != null)
            {
                strSql1.Append("PersonalHomePage,");
                strSql2.Append("'" + model.PersonalHomePage + "',");
            }
            if (model.JobResume != null)
            {
                strSql1.Append("JobResume,");
                strSql2.Append("'" + model.JobResume + "',");
            }
            if (model.OfficePhone != null)
            {
                strSql1.Append("OfficePhone,");
                strSql2.Append("" + model.OfficePhone + ",");
            }
            if (model.Family != null)
            {
                strSql1.Append("Family,");
                strSql2.Append("'" + model.Family + "',");
            }
            if (model.EntryTime != null)
            {
                strSql1.Append("EntryTime,");
                strSql2.Append("'" + model.EntryTime + "',");
            }
            if (model.ContinuingEducation != null)
            {
                strSql1.Append("ContinuingEducation,");
                strSql2.Append("'" + model.ContinuingEducation + "',");
            }
            if (model.Position != null)
            {
                strSql1.Append("Position,");
                strSql2.Append("'" + model.Position + "',");
            }
            if (model.Remarks != null)
            {
                strSql1.Append("Remarks,");
                strSql2.Append("'" + model.Remarks + "',");
            }
            if (model.WageChange != null)
            {
                strSql1.Append("WageChange,");
                strSql2.Append("'" + model.WageChange + "',");
            }
            if (model.HomeAddress != null)
            {
                strSql1.Append("HomeAddress,");
                strSql2.Append("'" + model.HomeAddress + "',");
            }
            if (model.LaborContract != null)
            {
                strSql1.Append("LaborContract,");
                strSql2.Append("'" + model.LaborContract + "',");
            }
            if (model.Ext != null)
            {
                strSql1.Append("Ext,");
                strSql2.Append("" + model.Ext + ",");
            }
            if (model.Nationality != null)
            {
                strSql1.Append("Nationality,");
                strSql2.Append("'" + model.Nationality + "',");
            }
            if (model.ProfessionalQualifications != null)
            {
                strSql1.Append("ProfessionalQualifications,");
                strSql2.Append("'" + model.ProfessionalQualifications + "',");
            }
            if (model.AwardandCertificates != null)
            {
                strSql1.Append("AwardandCertificates,");
                strSql2.Append("'" + model.AwardandCertificates + "',");
            }
            if (model.HomeTel != null)
            {
                strSql1.Append("HomeTel,");
                strSql2.Append("" + model.HomeTel + ",");
            }
            if (model.StuffPhoto != null)
            {
                strSql1.Append("StuffPhoto,");
                strSql2.Append("'" + model.StuffPhoto + "',");
            }
            if (model.JobDuty != null)
            {
                strSql1.Append("JobDuty,");
                strSql2.Append("'" + model.JobDuty + "',");
            }
            strSql.Append("insert into HR_Employees(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.RBAC.Entity.HR_Employees model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HR_Employees set ");
            if (model.SN != null)
            {
                strSql.Append("SN='" + model.SN + "',");
            }
            else
            {
                strSql.Append("SN= null ,");
            }
            if (model.NameL != null)
            {
                strSql.Append("NameL='" + model.NameL + "',");
            }
            if (model.NameF != null)
            {
                strSql.Append("NameF='" + model.NameF + "',");
            }
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.Sex != null)
            {
                strSql.Append("Sex='" + model.Sex + "',");
            }
            else
            {
                strSql.Append("Sex= null ,");
            }
            if (model.Birth.HasValue)
            {
                strSql.Append("Birth='" + model.Birth + "',");
            }
            else
            {
                strSql.Append("Birth= null ,");
            }
            if (model.Email != null)
            {
                strSql.Append("Email='" + model.Email + "',");
            }
            else
            {
                strSql.Append("Email= null ,");
            }
            if (model.Tel1 != null)
            {
                strSql.Append("Tel1='" + model.Tel1 + "',");
            }
            else
            {
                strSql.Append("Tel1= null ,");
            }
            if (model.Tel2 != null)
            {
                strSql.Append("Tel2='" + model.Tel2 + "',");
            }
            else
            {
                strSql.Append("Tel2= null ,");
            }
            if (model.Mobile != null)
            {
                strSql.Append("Mobile='" + model.Mobile + "',");
            }
            else
            {
                strSql.Append("Mobile= null ,");
            }
            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            else
            {
                strSql.Append("Address= null ,");
            }
            if (model.City != null)
            {
                strSql.Append("City='" + model.City + "',");
            }
            else
            {
                strSql.Append("City= null ,");
            }
            if (model.Province != null)
            {
                strSql.Append("Province='" + model.Province + "',");
            }
            else
            {
                strSql.Append("Province= null ,");
            }
            if (model.Country != null)
            {
                strSql.Append("Country='" + model.Country + "',");
            }
            else
            {
                strSql.Append("Country= null ,");
            }
            if (model.Zip != null)
            {
                strSql.Append("Zip='" + model.Zip + "',");
            }
            else
            {
                strSql.Append("Zip= null ,");
            }
            if (model.MaritalStatus != null)
            {
                strSql.Append("MaritalStatus='" + model.MaritalStatus + "',");
            }
            else
            {
                strSql.Append("MaritalStatus= null ,");
            }
            if (model.EducationLevel != null)
            {
                strSql.Append("EducationLevel='" + model.EducationLevel + "',");
            }
            else
            {
                strSql.Append("EducationLevel= null ,");
            }
            if (model.Enabled != null)
            {
                strSql.Append("Enabled=" + model.Enabled + ",");
            }
            if (model.Pic != null)
            {
                strSql.Append("Pic=" + model.Pic + ",");
            }
            else
            {
                strSql.Append("Pic= null ,");
            }
            if (model.JoinDate != null)
            {
                strSql.Append("JoinDate='" + model.JoinDate + "',");
            }
            else
            {
                strSql.Append("JoinDate= null ,");
            }
            if (model.IDCardNO != null)
            {
                strSql.Append("IDCardNO='" + model.IDCardNO + "',");
            }
            else
            {
                strSql.Append("IDCardNO= null ,");
            }
            if (model.DesktopTheme != null)
            {
                strSql.Append("DesktopTheme=" + model.DesktopTheme + ",");
            }
            else
            {
                strSql.Append("DesktopTheme= null ,");
            }
            if (model.FirstPamId != null)
            {
                strSql.Append("FirstPamId='" + model.FirstPamId + "',");
            }
            else
            {
                strSql.Append("FirstPamId= null ,");
            }
            if (model.Degree != null)
            {
                strSql.Append("Degree='" + model.Degree + "',");
            }
            else
            {
                strSql.Append("Degree= null ,");
            }
            if (model.GraduateSchool != null)
            {
                strSql.Append("GraduateSchool='" + model.GraduateSchool + "',");
            }
            else
            {
                strSql.Append("GraduateSchool= null ,");
            }
            if (model.ProfessionalCompetence != null)
            {
                strSql.Append("ProfessionalCompetence='" + model.ProfessionalCompetence + "',");
            }
            else
            {
                strSql.Append("ProfessionalCompetence= null ,");
            }
            if (model.EducationBackground != null)
            {
                strSql.Append("EducationBackground='" + model.EducationBackground + "',");
            }
            else
            {
                strSql.Append("EducationBackground= null ,");
            }
            if (model.HealthStatusRecord != null)
            {
                strSql.Append("HealthStatusRecord='" + model.HealthStatusRecord + "',");
            }
            else
            {
                strSql.Append("HealthStatusRecord= null ,");
            }
            if (model.PoliticalLandscape != null)
            {
                strSql.Append("PoliticalLandscape='" + model.PoliticalLandscape + "',");
            }
            else
            {
                strSql.Append("PoliticalLandscape= null ,");
            }
            if (model.Department != null)
            {
                strSql.Append("Department='" + model.Department + "',");
            }
            else
            {
                strSql.Append("Department= null ,");
            }
            if (model.Training != null)
            {
                strSql.Append("Training='" + model.Training + "',");
            }
            else
            {
                strSql.Append("Training= null ,");
            }
            if (model.PersonalHomePage != null)
            {
                strSql.Append("PersonalHomePage='" + model.PersonalHomePage + "',");
            }
            else
            {
                strSql.Append("PersonalHomePage= null ,");
            }
            if (model.JobResume != null)
            {
                strSql.Append("JobResume='" + model.JobResume + "',");
            }
            else
            {
                strSql.Append("JobResume= null ,");
            }
            if (model.OfficePhone != null)
            {
                strSql.Append("OfficePhone=" + model.OfficePhone + ",");
            }
            else
            {
                strSql.Append("OfficePhone= null ,");
            }
            if (model.Family != null)
            {
                strSql.Append("Family='" + model.Family + "',");
            }
            else
            {
                strSql.Append("Family= null ,");
            }
            if (model.EntryTime != null)
            {
                strSql.Append("EntryTime='" + model.EntryTime + "',");
            }
            else
            {
                strSql.Append("EntryTime= null ,");
            }
            if (model.ContinuingEducation != null)
            {
                strSql.Append("ContinuingEducation='" + model.ContinuingEducation + "',");
            }
            else
            {
                strSql.Append("ContinuingEducation= null ,");
            }
            if (model.Position != null)
            {
                strSql.Append("Position='" + model.Position + "',");
            }
            else
            {
                strSql.Append("Position= null ,");
            }
            if (model.Remarks != null)
            {
                strSql.Append("Remarks='" + model.Remarks + "',");
            }
            else
            {
                strSql.Append("Remarks= null ,");
            }
            if (model.WageChange != null)
            {
                strSql.Append("WageChange='" + model.WageChange + "',");
            }
            else
            {
                strSql.Append("WageChange= null ,");
            }
            if (model.HomeAddress != null)
            {
                strSql.Append("HomeAddress='" + model.HomeAddress + "',");
            }
            else
            {
                strSql.Append("HomeAddress= null ,");
            }
            if (model.LaborContract != null)
            {
                strSql.Append("LaborContract='" + model.LaborContract + "',");
            }
            else
            {
                strSql.Append("LaborContract= null ,");
            }
            if (model.Ext != null)
            {
                strSql.Append("Ext=" + model.Ext + ",");
            }
            else
            {
                strSql.Append("Ext= null ,");
            }
            if (model.Nationality != null)
            {
                strSql.Append("Nationality='" + model.Nationality + "',");
            }
            else
            {
                strSql.Append("Nationality= null ,");
            }
            if (model.ProfessionalQualifications != null)
            {
                strSql.Append("ProfessionalQualifications='" + model.ProfessionalQualifications + "',");
            }
            else
            {
                strSql.Append("ProfessionalQualifications= null ,");
            }
            if (model.AwardandCertificates != null)
            {
                strSql.Append("AwardandCertificates='" + model.AwardandCertificates + "',");
            }
            else
            {
                strSql.Append("AwardandCertificates= null ,");
            }
            if (model.HomeTel != null)
            {
                strSql.Append("HomeTel=" + model.HomeTel + ",");
            }
            else
            {
                strSql.Append("HomeTel= null ,");
            }
            if (model.StuffPhoto != null)
            {
                strSql.Append("StuffPhoto='" + model.StuffPhoto + "',");
            }
            else
            {
                strSql.Append("StuffPhoto= null ,");
            }
            if (model.JobDuty != null)
            {
                strSql.Append("JobDuty='" + model.JobDuty + "',");
            }
            else
            {
                strSql.Append("JobDuty= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ID=" + model.ID + "");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from HR_Employees ");
            strSql.Append(" where ID=" + ID + "");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from HR_Employees ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
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
        public Model.RBAC.Entity.HR_Employees GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" * ");
            strSql.Append(" from HR_Employees ");
            strSql.Append(" where ID=" + ID + "");
            ZhiFang.Model.RBAC.Entity.HR_Employees model = new Model.RBAC.Entity.HR_Employees();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
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
            strSql.Append(" FROM HR_Employees ");
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
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM HR_Employees ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }       

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM dbo.HR_Employees INNER JOIN dbo.RBAC_EmplRoles ON dbo.HR_Employees.ID = dbo.RBAC_EmplRoles.EmplID ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby))
            {
                strSql.Append("order by dbo.HR_Employees." + orderby);
            }
            else
            {
                strSql.Append("order by dbo.HR_Employees.ID desc");
            }
            strSql.Append(")AS Row, dbo.HR_Employees.*, dbo.RBAC_EmplRoles.DeptID,(select top 1 CName from HR_Departments where DeptID=HR_Departments.ID) as DeptCName  from dbo.HR_Employees INNER JOIN dbo.RBAC_EmplRoles ON dbo.HR_Employees.ID = dbo.RBAC_EmplRoles.EmplID ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            ZhiFang.Common.Log.Log.Debug("HR_Employees.GetListByPage.SQL:"+ strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.RBAC.Entity.HR_Employees DataRowToModel(DataRow row)
        {
            Model.RBAC.Entity.HR_Employees model = new Model.RBAC.Entity.HR_Employees();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["SN"] != null)
                {
                    model.SN = row["SN"].ToString();
                }
                if (row["NameL"] != null)
                {
                    model.NameL = row["NameL"].ToString();
                }
                if (row["NameF"] != null)
                {
                    model.NameF = row["NameF"].ToString();
                }
                if (row["Sex"] != null)
                {
                    model.Sex = row["Sex"].ToString();
                }
                if (row["Birth"] != null && row["Birth"].ToString() != "")
                {
                    model.Birth = DateTime.Parse(row["Birth"].ToString());
                }
                if (row["Email"] != null)
                {
                    model.Email = row["Email"].ToString();
                }
                if (row["Tel1"] != null)
                {
                    model.Tel1 = row["Tel1"].ToString();
                }
                if (row["Tel2"] != null)
                {
                    model.Tel2 = row["Tel2"].ToString();
                }
                if (row["Mobile"] != null)
                {
                    model.Mobile = row["Mobile"].ToString();
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["City"] != null)
                {
                    model.City = row["City"].ToString();
                }
                if (row["Province"] != null)
                {
                    model.Province = row["Province"].ToString();
                }
                if (row["Country"] != null)
                {
                    model.Country = row["Country"].ToString();
                }
                if (row["Zip"] != null)
                {
                    model.Zip = row["Zip"].ToString();
                }
                if (row["MaritalStatus"] != null)
                {
                    model.MaritalStatus = row["MaritalStatus"].ToString();
                }
                if (row["EducationLevel"] != null)
                {
                    model.EducationLevel = row["EducationLevel"].ToString();
                }
                if (row["Enabled"] != null && row["Enabled"].ToString() != "")
                {
                    model.Enabled = int.Parse(row["Enabled"].ToString());
                }
                if (row["Pic"] != null && row["Pic"].ToString() != "")
                {
                    model.Pic = (byte[])row["Pic"];
                }
                if (row["JoinDate"] != null && row["JoinDate"].ToString() != "")
                {
                    model.JoinDate = DateTime.Parse(row["JoinDate"].ToString());
                }
                if (row["IDCardNO"] != null)
                {
                    model.IDCardNO = row["IDCardNO"].ToString();
                }
                if (row["DesktopTheme"] != null && row["DesktopTheme"].ToString() != "")
                {
                    model.DesktopTheme = int.Parse(row["DesktopTheme"].ToString());
                }
                if (row["FirstPamId"] != null)
                {
                    model.FirstPamId = row["FirstPamId"].ToString();
                }
                if (row["Degree"] != null)
                {
                    model.Degree = row["Degree"].ToString();
                }
                if (row["GraduateSchool"] != null)
                {
                    model.GraduateSchool = row["GraduateSchool"].ToString();
                }
                if (row["ProfessionalCompetence"] != null)
                {
                    model.ProfessionalCompetence = row["ProfessionalCompetence"].ToString();
                }
                if (row["EducationBackground"] != null)
                {
                    model.EducationBackground = row["EducationBackground"].ToString();
                }
                if (row["HealthStatusRecord"] != null)
                {
                    model.HealthStatusRecord = row["HealthStatusRecord"].ToString();
                }
                if (row["PoliticalLandscape"] != null)
                {
                    model.PoliticalLandscape = row["PoliticalLandscape"].ToString();
                }
                if (row["Department"] != null)
                {
                    model.Department = row["Department"].ToString();
                }
                if (row["Training"] != null)
                {
                    model.Training = row["Training"].ToString();
                }
                if (row["PersonalHomePage"] != null)
                {
                    model.PersonalHomePage = row["PersonalHomePage"].ToString();
                }
                if (row["JobResume"] != null)
                {
                    model.JobResume = row["JobResume"].ToString();
                }
                if (row["OfficePhone"] != null && row["OfficePhone"].ToString() != "")
                {
                    model.OfficePhone = decimal.Parse(row["OfficePhone"].ToString());
                }
                if (row["Family"] != null)
                {
                    model.Family = row["Family"].ToString();
                }
                if (row["EntryTime"] != null && row["EntryTime"].ToString() != "")
                {
                    model.EntryTime = DateTime.Parse(row["EntryTime"].ToString());
                }
                if (row["ContinuingEducation"] != null)
                {
                    model.ContinuingEducation = row["ContinuingEducation"].ToString();
                }
                if (row["Position"] != null)
                {
                    model.Position = row["Position"].ToString();
                }
                if (row["Remarks"] != null)
                {
                    model.Remarks = row["Remarks"].ToString();
                }
                if (row["WageChange"] != null)
                {
                    model.WageChange = row["WageChange"].ToString();
                }
                if (row["HomeAddress"] != null)
                {
                    model.HomeAddress = row["HomeAddress"].ToString();
                }
                if (row["LaborContract"] != null)
                {
                    model.LaborContract = row["LaborContract"].ToString();
                }
                if (row["Ext"] != null && row["Ext"].ToString() != "")
                {
                    model.Ext = decimal.Parse(row["Ext"].ToString());
                }
                if (row["Nationality"] != null)
                {
                    model.Nationality = row["Nationality"].ToString();
                }
                if (row["ProfessionalQualifications"] != null)
                {
                    model.ProfessionalQualifications = row["ProfessionalQualifications"].ToString();
                }
                if (row["AwardandCertificates"] != null)
                {
                    model.AwardandCertificates = row["AwardandCertificates"].ToString();
                }
                if (row["HomeTel"] != null && row["HomeTel"].ToString() != "")
                {
                    model.HomeTel = decimal.Parse(row["HomeTel"].ToString());
                }
                if (row["StuffPhoto"] != null)
                {
                    model.StuffPhoto = row["StuffPhoto"].ToString();
                }
                if (row["JobDuty"] != null)
                {
                    model.JobDuty = row["JobDuty"].ToString();
                }

                if (row.Table.Columns.Contains("DeptCName") && row["DeptCName"].ToString() != "")
                {
                    model.DeptCName = row["DeptCName"].ToString();
                }
                if (row.Table.Columns.Contains("DeptId") && row["DeptId"].ToString() != "")
                {
                    model.DeptId = long.Parse(row["DeptId"].ToString());
                }
                if (row.Table.Columns.Contains("CName") && !string.IsNullOrEmpty(row["CName"].ToString()))
                {
                    model.CName = row["CName"].ToString();
                }
            }
            return model;
        }
    }
}

