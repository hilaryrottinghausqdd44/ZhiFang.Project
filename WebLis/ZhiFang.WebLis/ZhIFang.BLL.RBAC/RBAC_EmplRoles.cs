using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ZhiFang.Model;

namespace ZhiFang.BLL.RBAC
{
    //RBAC_EmplRoles
    public class RBAC_EmplRoles
    {

        private readonly ZhiFang.DAL.RBAC.RBAC_EmplRoles dal = new DAL.RBAC.RBAC_EmplRoles();
        public RBAC_EmplRoles()
        { }

        #region  Method
        

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.RBAC.Entity.RBAC_EmplRoles model)
        {
            return dal.Add(model);

        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }            

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.RBAC.Entity.RBAC_EmplRoles> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.RBAC.Entity.RBAC_EmplRoles> DataTableToList(DataTable dt)
        {
            List<Model.RBAC.Entity.RBAC_EmplRoles> modelList = new List<Model.RBAC.Entity.RBAC_EmplRoles>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.RBAC.Entity.RBAC_EmplRoles model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.RBAC.Entity.RBAC_EmplRoles();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["SN"].ToString() != "")
                    {
                        model.SN = int.Parse(dt.Rows[n]["SN"].ToString());
                    }
                    if (dt.Rows[n]["EmplID"].ToString() != "")
                    {
                        model.EmplID = int.Parse(dt.Rows[n]["EmplID"].ToString());
                    }
                    if (dt.Rows[n]["DeptID"].ToString() != "")
                    {
                        model.DeptID = int.Parse(dt.Rows[n]["DeptID"].ToString());
                    }
                    if (dt.Rows[n]["PositionID"].ToString() != "")
                    {
                        model.PositionID = int.Parse(dt.Rows[n]["PositionID"].ToString());
                    }
                    if (dt.Rows[n]["PostID"].ToString() != "")
                    {
                        model.PostID = int.Parse(dt.Rows[n]["PostID"].ToString());
                    }
                    if (dt.Rows[n]["Sort"].ToString() != "")
                    {
                        model.Sort = int.Parse(dt.Rows[n]["Sort"].ToString());
                    }
                    model.Validity = dt.Rows[n]["Validity"].ToString();


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

        public BaseResultDataValue SetEmpRoles(string EmpId, List<string> Roles)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                List<Model.RBAC.Entity.RBAC_EmplRoles> tmpemproles = GetModelList("  emplid=" + EmpId + " and postID is not null  ");

                tmpemproles.ForEach(a => {
                    Delete(a.ID);
                });
                
                foreach (var role in Roles)
                {
                    Model.RBAC.Entity.RBAC_EmplRoles model = new Model.RBAC.Entity.RBAC_EmplRoles();
                    model.EmplID = int.Parse(EmpId);
                    model.PostID = int.Parse(role);
                    if (dal.Add(model) <= 0)
                    {
                        ZhiFang.Common.Log.Log.Error($"RBAC_EmplRoles.AddEmpRoles.新增失败：EmpId：{EmpId},role:{role}");
                    }
                }
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("RBAC_EmplRoles.AddEmpRoles.异常：" + e.ToString());
                throw e;
            }
        }
        #endregion

    }
}