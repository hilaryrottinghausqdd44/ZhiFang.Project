using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ZhiFang.Model.RBAC.Entity;
using ZhiFang.Model;

namespace ZhiFang.BLL.RBAC
{
    //HR_Employees
    public class HR_Employees
    {

        private readonly ZhiFang.DAL.RBAC.HR_Employees dal = new ZhiFang.DAL.RBAC.HR_Employees();
        private readonly ZhiFang.DAL.RBAC.RBAC_EmplRoles dalemprole = new ZhiFang.DAL.RBAC.RBAC_EmplRoles();
        private readonly ZhiFang.DAL.RBAC.RBAC_Users dalusers = new ZhiFang.DAL.RBAC.RBAC_Users();
        public HR_Employees()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public BaseResultDataValue Add(Model.RBAC.Entity.HR_Employees model)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (GetRecordCount($" HR_Employees.SN='{model.SN}'") > 0)
                {
                    throw new Exception("人员编码重复！编码："+model.SN);
                }
                var tmpempid = dal.Add(model) ;
                if (tmpempid > 0)
                {
                    #region 建立部门与人员关系
                    Model.RBAC.Entity.RBAC_EmplRoles emprole = new Model.RBAC.Entity.RBAC_EmplRoles();
                    emprole.DeptID = model.DeptId;
                    emprole.EmplID = tmpempid;
                    if (dalemprole.Add(emprole) <= 0)
                    {
                        ZhiFang.Common.Log.Log.Error("建立部门与人员关系失败！tmpempid：" + tmpempid);
                        dal.Delete(tmpempid);
                        throw new Exception("建立部门与人员关系失败！编码：" + model.SN);
                    }
                    #endregion

                    #region 建立帐号与人员关系
                    Model.RBAC.Entity.RBAC_Users users = new Model.RBAC.Entity.RBAC_Users();
                    users.Account = model.Account;
                    users.EmpID = tmpempid;
                    users.Password = ZhiFang.Tools.MD5Helper.StringToMD5Hash(model.PWD.Trim());
                    users.AccCreateTime = DateTime.Now;
                    if (dalusers.Add(users) <= 0)
                    {
                        ZhiFang.Common.Log.Log.Error("建立帐号与人员关系！tmpempid：" + tmpempid);
                        dalemprole.DeleteByEmpID(tmpempid,0);
                        dal.Delete(tmpempid);
                        throw new Exception("建立帐号与人员关系！编码：" + model.SN);
                    }
                    #endregion
                }
                brdv.success = dal.Add(model) > 0;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("HR_Employees.Add.异常：" + e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public BaseResultDataValue Update(Model.RBAC.Entity.HR_Employees model)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //if(model)
            brdv.success = dal.Update(model) > 0;
            return brdv;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            dalemprole.DeleteByEmpID(ID,0);
            dalemprole.DeleteByEmpID(ID, 1);
            dalusers.DeleteListByEmpId(ID.ToString());
            return dal.Delete(ID);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.RBAC.Entity.HR_Employees GetModel(int ID)
        {

            return dal.GetModel(ID);
        }
        
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.RBAC.Entity.HR_Employees> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.RBAC.Entity.HR_Employees> DataTableToList(DataTable dt)
        {
            List<Model.RBAC.Entity.HR_Employees> modelList = new List<Model.RBAC.Entity.HR_Employees>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {                
                for (int n = 0; n < rowsCount; n++)
                {
                    Model.RBAC.Entity.HR_Employees model = new Model.RBAC.Entity.HR_Employees();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    model.SN = dt.Rows[n]["SN"].ToString();
                    model.NameL = dt.Rows[n]["NameL"].ToString();
                    model.NameF = dt.Rows[n]["NameF"].ToString();
                    model.Sex = dt.Rows[n]["Sex"].ToString();
                    if (dt.Rows[n]["Birth"].ToString() != "")
                    {
                        model.Birth = DateTime.Parse(dt.Rows[n]["Birth"].ToString());
                    }
                    model.Email = dt.Rows[n]["Email"].ToString();
                    model.Tel1 = dt.Rows[n]["Tel1"].ToString();
                    model.Tel2 = dt.Rows[n]["Tel2"].ToString();
                    model.Mobile = dt.Rows[n]["Mobile"].ToString();
                    model.Address = dt.Rows[n]["Address"].ToString();
                    model.City = dt.Rows[n]["City"].ToString();
                    model.Province = dt.Rows[n]["Province"].ToString();
                    model.Country = dt.Rows[n]["Country"].ToString();
                    model.Zip = dt.Rows[n]["Zip"].ToString();
                    model.MaritalStatus = dt.Rows[n]["MaritalStatus"].ToString();
                    model.EducationLevel = dt.Rows[n]["EducationLevel"].ToString();
                    if (dt.Rows[n]["Enabled"].ToString() != "")
                    {
                        model.Enabled = int.Parse(dt.Rows[n]["Enabled"].ToString());
                    }
                    if (dt.Rows[n]["Pic"].ToString() != "")
                    {
                        model.Pic = (byte[])dt.Rows[n]["Pic"];
                    }
                    if (dt.Rows[n]["JoinDate"].ToString() != "")
                    {
                        model.JoinDate = DateTime.Parse(dt.Rows[n]["JoinDate"].ToString());
                    }
                    model.IDCardNO = dt.Rows[n]["IDCardNO"].ToString();
                    if (dt.Rows[n]["DesktopTheme"].ToString() != "")
                    {
                        model.DesktopTheme = int.Parse(dt.Rows[n]["DesktopTheme"].ToString());
                    }
                    model.FirstPamId = dt.Rows[n]["FirstPamId"].ToString();
                    model.Degree = dt.Rows[n]["Degree"].ToString();
                    model.GraduateSchool = dt.Rows[n]["GraduateSchool"].ToString();
                    model.ProfessionalCompetence = dt.Rows[n]["ProfessionalCompetence"].ToString();
                    model.EducationBackground = dt.Rows[n]["EducationBackground"].ToString();
                    model.HealthStatusRecord = dt.Rows[n]["HealthStatusRecord"].ToString();
                    model.PoliticalLandscape = dt.Rows[n]["PoliticalLandscape"].ToString();
                    model.Department = dt.Rows[n]["Department"].ToString();
                    model.Training = dt.Rows[n]["Training"].ToString();
                    model.PersonalHomePage = dt.Rows[n]["PersonalHomePage"].ToString();
                    model.JobResume = dt.Rows[n]["JobResume"].ToString();
                    if (dt.Rows[n]["OfficePhone"].ToString() != "")
                    {
                        model.OfficePhone = decimal.Parse(dt.Rows[n]["OfficePhone"].ToString());
                    }
                    model.Family = dt.Rows[n]["Family"].ToString();
                    if (dt.Rows[n]["EntryTime"].ToString() != "")
                    {
                        model.EntryTime = DateTime.Parse(dt.Rows[n]["EntryTime"].ToString());
                    }
                    model.ContinuingEducation = dt.Rows[n]["ContinuingEducation"].ToString();
                    model.Position = dt.Rows[n]["Position"].ToString();
                    model.Remarks = dt.Rows[n]["Remarks"].ToString();
                    model.WageChange = dt.Rows[n]["WageChange"].ToString();
                    model.HomeAddress = dt.Rows[n]["HomeAddress"].ToString();
                    model.LaborContract = dt.Rows[n]["LaborContract"].ToString();
                    if (dt.Rows[n]["Ext"].ToString() != "")
                    {
                        model.Ext = decimal.Parse(dt.Rows[n]["Ext"].ToString());
                    }
                    model.Nationality = dt.Rows[n]["Nationality"].ToString();
                    model.ProfessionalQualifications = dt.Rows[n]["ProfessionalQualifications"].ToString();
                    model.AwardandCertificates = dt.Rows[n]["AwardandCertificates"].ToString();
                    if (dt.Rows[n]["HomeTel"].ToString() != "")
                    {
                        model.HomeTel = decimal.Parse(dt.Rows[n]["HomeTel"].ToString());
                    }
                    model.StuffPhoto = dt.Rows[n]["StuffPhoto"].ToString();
                    model.JobDuty = dt.Rows[n]["JobDuty"].ToString();

                    if (dt.Columns.Contains("DeptCName")&&dt.Rows[n]["DeptCName"].ToString() != "")
                    {
                        model.DeptCName = dt.Rows[n]["DeptCName"].ToString();
                    }
                    if (dt.Columns.Contains("DeptId") && dt.Rows[n]["DeptId"].ToString() != "")
                    {
                        model.DeptId = long.Parse(dt.Rows[n]["DeptId"].ToString());
                    }
                    if (dt.Columns.Contains("CName") && !string.IsNullOrEmpty(dt.Rows[n]["CName"].ToString()))
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        public List<Model.RBAC.Entity.HR_Employees> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            List<Model.RBAC.Entity.HR_Employees> emplist = new List<Model.RBAC.Entity.HR_Employees>();
            DataSet ds = dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                emplist = DataTableToList(ds.Tables[0]);
            }
            return emplist;
        }
        public int GetRecordCount(string wherestr)
        {
            return dal.GetRecordCount(wherestr);
        }
        #endregion

    }
}