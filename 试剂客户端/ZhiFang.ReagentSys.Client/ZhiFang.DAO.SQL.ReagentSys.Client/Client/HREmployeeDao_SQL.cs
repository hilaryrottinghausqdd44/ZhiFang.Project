using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;
using ZhiFang.DAO.SQL.ReagentSys.Client.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.Base;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.SQL.ReagentSys.Client
{
    public class HREmployeeDao_SQL : IDBaseDao<HREmployee, long>, IDHREmployeeDao_SQL
    {
        //查询字段
        private string FieldStr = "LabID,EmpID,PositionID,DeptID,UseCode,StandCode,DeveCode,NameL,NameF,CName,EName,SName,Shortcode,PinYinZiTou,Comment,IsUse,DispOrder,SexID,Birthday,Email,MobileTel,OfficeTel,ExtTel,HomeTel,Tel,Address,CityID,ProvinceID,CountryID,ZipCode,MaritalStatusID,EducationLevelID,IsEnabled,PicFile,IdNumber,DegreeID,GraduateSchool,ProfessionalAbilityID,EduBackground,HealthStatusID,PoliticsStatusID,Training,PersonalHomePage,JobResume,Family,EntryTime,ContinuingEducation,WageChange,LaborContract,NationalityID,ProfessionalQualifications,AwardandCertificates,JobDuty,DataAddTime,DataUpdateTime,DataTimeStamp,SignatureImage,ManagerID,ManagerName";
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM HR_Employee ");
            if (strWhere.Trim() != "")
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
		/// 获取记录总数
		/// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM HR_Employee ");
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
        public int GetListCountByHQL(string strSqlWhere)
        {
            return GetRecordCount(strSqlWhere);
        }

        public object GetTotalByHQL(string strSqlWhere, string field)
        {
            return GetRecordCount(strSqlWhere);
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" FROM HR_Employee ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HREmployee GetModel(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from HR_Employee ");
            strSql.Append(" where EmpID=" + id + " ");
            HREmployee model = new HREmployee();
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
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
        /// 得到一个对象实体
        /// </summary>
        public HREmployee DataRowToModel(DataRow row)
        {
            HREmployee model = new HREmployee();
            if (row != null)
            {
                if (row["LabID"] != null && row["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(row["LabID"].ToString());
                }
                if (row["EmpID"] != null && row["EmpID"].ToString() != "")
                {
                    model.Id = long.Parse(row["EmpID"].ToString());
                }
                //if (row["PositionID"] != null && row["PositionID"].ToString() != "")
                //{
                //    model.PositionID = long.Parse(row["PositionID"].ToString());
                //}
                if (row["DeptID"] != null && row["DeptID"].ToString() != "")
                {
                    model.HRDept = DataAccess_SQL.CreateHRDeptDao_SQL().Get(long.Parse(row["DeptID"].ToString()));
                }
                if (row["UseCode"] != null)
                {
                    model.UseCode = row["UseCode"].ToString();
                }
                if (row["StandCode"] != null)
                {
                    model.StandCode = row["StandCode"].ToString();
                }
                if (row["DeveCode"] != null)
                {
                    model.DeveCode = row["DeveCode"].ToString();
                }
                if (row["NameL"] != null)
                {
                    model.NameL = row["NameL"].ToString();
                }
                if (row["NameF"] != null)
                {
                    model.NameF = row["NameF"].ToString();
                }
                if (row["CName"] != null)
                {
                    model.CName = row["CName"].ToString();
                }
                if (row["EName"] != null)
                {
                    model.EName = row["EName"].ToString();
                }
                if (row["SName"] != null)
                {
                    model.SName = row["SName"].ToString();
                }
                if (row["Shortcode"] != null)
                {
                    model.Shortcode = row["Shortcode"].ToString();
                }
                if (row["PinYinZiTou"] != null)
                {
                    model.PinYinZiTou = row["PinYinZiTou"].ToString();
                }
                if (row["Comment"] != null)
                {
                    model.Comment = row["Comment"].ToString();
                }
                if (row["IsUse"] != null && row["IsUse"].ToString() != "")
                {
                    if ((row["IsUse"].ToString() == "1") || (row["IsUse"].ToString().ToLower() == "true"))
                    {
                        model.IsUse = true;
                    }
                    else
                    {
                        model.IsUse = false;
                    }
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                //if (row["SexID"] != null && row["SexID"].ToString() != "")
                //{
                //    model.SexID = long.Parse(row["SexID"].ToString());
                //}
                if (row["Birthday"] != null && row["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(row["Birthday"].ToString());
                }
                if (row["Email"] != null)
                {
                    model.Email = row["Email"].ToString();
                }
                if (row["MobileTel"] != null)
                {
                    model.MobileTel = row["MobileTel"].ToString();
                }
                if (row["OfficeTel"] != null && row["OfficeTel"].ToString() != "")
                {
                    model.OfficeTel = double.Parse(row["OfficeTel"].ToString());
                }
                if (row["ExtTel"] != null && row["ExtTel"].ToString() != "")
                {
                    model.ExtTel = double.Parse(row["ExtTel"].ToString());
                }
                if (row["HomeTel"] != null && row["HomeTel"].ToString() != "")
                {
                    model.HomeTel = double.Parse(row["HomeTel"].ToString());
                }
                if (row["Tel"] != null)
                {
                    model.Tel = row["Tel"].ToString();
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                //if (row["CityID"] != null && row["CityID"].ToString() != "")
                //{
                //    model.CityID = long.Parse(row["CityID"].ToString());
                //}
                //if (row["ProvinceID"] != null && row["ProvinceID"].ToString() != "")
                //{
                //    model.ProvinceID = long.Parse(row["ProvinceID"].ToString());
                //}
                //if (row["CountryID"] != null && row["CountryID"].ToString() != "")
                //{
                //    model.CountryID = long.Parse(row["CountryID"].ToString());
                //}
                if (row["ZipCode"] != null)
                {
                    model.ZipCode = row["ZipCode"].ToString();
                }
                //if (row["MaritalStatusID"] != null && row["MaritalStatusID"].ToString() != "")
                //{
                //    model.MaritalStatusID = long.Parse(row["MaritalStatusID"].ToString());
                //}
                //if (row["EducationLevelID"] != null && row["EducationLevelID"].ToString() != "")
                //{
                //    model.EducationLevelID = long.Parse(row["EducationLevelID"].ToString());
                //}
                if (row["IsEnabled"] != null && row["IsEnabled"].ToString() != "")
                {
                    model.IsEnabled = int.Parse(row["IsEnabled"].ToString());
                }
                if (row["PicFile"] != null)
                {
                    model.PicFile = row["PicFile"].ToString();
                }
                if (row["IdNumber"] != null)
                {
                    model.IdNumber = row["IdNumber"].ToString();
                }
                //if (row["DegreeID"] != null && row["DegreeID"].ToString() != "")
                //{
                //    model.DegreeID = long.Parse(row["DegreeID"].ToString());
                //}
                if (row["GraduateSchool"] != null)
                {
                    model.GraduateSchool = row["GraduateSchool"].ToString();
                }
                //if (row["ProfessionalAbilityID"] != null && row["ProfessionalAbilityID"].ToString() != "")
                //{
                //    model.ProfessionalAbilityID = long.Parse(row["ProfessionalAbilityID"].ToString());
                //}
                if (row["EduBackground"] != null)
                {
                    model.EduBackground = row["EduBackground"].ToString();
                }
                //if (row["HealthStatusID"] != null && row["HealthStatusID"].ToString() != "")
                //{
                //    model.HealthStatusID = long.Parse(row["HealthStatusID"].ToString());
                //}
                //if (row["PoliticsStatusID"] != null && row["PoliticsStatusID"].ToString() != "")
                //{
                //    model.PoliticsStatusID = long.Parse(row["PoliticsStatusID"].ToString());
                //}
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
                if (row["WageChange"] != null)
                {
                    model.WageChange = row["WageChange"].ToString();
                }
                if (row["LaborContract"] != null)
                {
                    model.LaborContract = row["LaborContract"].ToString();
                }
                //if (row["NationalityID"] != null && row["NationalityID"].ToString() != "")
                //{
                //    model.NationalityID = long.Parse(row["NationalityID"].ToString());
                //}
                if (row["ProfessionalQualifications"] != null)
                {
                    model.ProfessionalQualifications = row["ProfessionalQualifications"].ToString();
                }
                if (row["AwardandCertificates"] != null)
                {
                    model.AwardandCertificates = row["AwardandCertificates"].ToString();
                }
                if (row["JobDuty"] != null)
                {
                    model.JobDuty = row["JobDuty"].ToString();
                }
                if (row["DataAddTime"] != null && row["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(row["DataAddTime"].ToString());
                }
                if (row["DataUpdateTime"] != null && row["DataUpdateTime"].ToString() != "")
                {
                    model.DataUpdateTime = DateTime.Parse(row["DataUpdateTime"].ToString());
                }
                if (row["DataTimeStamp"] != null && row["DataTimeStamp"].ToString() != "")
                {
                    model.DataTimeStamp = row["DataTimeStamp"] as byte[];
                }
                if (row["SignatureImage"] != null)
                {
                    model.SignatureImage = row["SignatureImage"].ToString();
                }
                if (row["ManagerID"] != null && row["ManagerID"].ToString() != "")
                {
                    model.ManagerID = long.Parse(row["ManagerID"].ToString());
                }
                if (row["ManagerName"] != null)
                {
                    model.ManagerName = row["ManagerName"].ToString();
                }
            }
            return model;
        }
        public IList<HREmployee> GetListByHQL(string strSqlWhere)
        {
            IList<HREmployee> tempList = new List<HREmployee>();
            DataSet ds = GetList(strSqlWhere);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tempList.Add(DataRowToModel(row));
                }
            }
            else
            {
                return tempList;
            }
            return tempList;
        }
        public HREmployee Get(long id)
        {
            return GetModel(id);
        }
        /// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.EmpID desc");
            }
            strSql.Append(")AS Row, T.*  from HR_Employee T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(strSql.ToString());
        }

        public EntityList<HREmployee> GetListByHQL(string strSqlWhere, int start, int count)
        {
            EntityList<HREmployee> entityList = new EntityList<HREmployee>();
            entityList.count = GetRecordCount(strSqlWhere);
            if (entityList.count <= 0) return entityList;

            DataSet ds = GetListByPage(strSqlWhere, "", start, count);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    entityList.list.Add(DataRowToModel(row));
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }

        public EntityList<HREmployee> GetListByHQL(string strSqlWhere, string Order, int start, int count)
        {
            EntityList<HREmployee> entityList = new EntityList<HREmployee>();
            entityList.count = GetRecordCount(strSqlWhere);
            if (entityList.count <= 0) return entityList;

            DataSet ds = GetListByPage(strSqlWhere, Order, start, count);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    entityList.list.Add(DataRowToModel(row));
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }

        public IList<HREmployee> LoadAll()
        {
            IList<HREmployee> tempList = new List<HREmployee>();

            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tempList.Add(DataRowToModel(row));
                }
            }
            else
            {
                return tempList;
            }
            return tempList;
        }

        public IList<HREmployee> GetObjects(HREmployee entity)
        {
            throw new NotImplementedException();
        }
        public bool BatchSaveVO(HREmployee voList)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(HREmployee entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteByHql(string hql)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByHQL(long id)
        {
            throw new NotImplementedException();
        }

        public void Evict(HREmployee entity)
        {
            throw new NotImplementedException();
        }

        public IList<T> Find<T>(string hql)
        {
            throw new NotImplementedException();
        }
        public void Flush()
        {
            throw new NotImplementedException();
        }
        public bool Save(HREmployee entity)
        {
            throw new NotImplementedException();
        }

        public object SaveByEntity(HREmployee entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(HREmployee entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string[] strParas)
        {
            throw new NotImplementedException();
        }

        public bool Update(HREmployee entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateByHql(string hql)
        {
            throw new NotImplementedException();
        }
    }
}
