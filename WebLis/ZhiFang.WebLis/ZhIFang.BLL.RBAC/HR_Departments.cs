using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ZhiFang.Model;
using System.Linq;

namespace ZhiFang.BLL.RBAC
{
    //HR_Departments
    public class HR_Departments
    {

        private readonly ZhiFang.DAL.RBAC.HR_Departments dal = new ZhiFang.DAL.RBAC.HR_Departments();
        public HR_Departments()
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
        public BaseResultDataValue Add(Model.RBAC.Entity.HR_Departments model)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            DataSet ds = new DataSet();
            try
            {
                var pdept = dal.GetModel(model.PID);
                model.PSN = (pdept != null) ? pdept.SN : "";

                if (model.PSN != null && model.PSN.Trim() != "")
                {
                    ds = GetList(1, " SN like '" + model.PSN + "%' and LEN(SN)=" + (model.PSN.Length + 2).ToString(), " SN DESC ");
                }
                else
                {
                    ds = GetList(1, " SN like '" + model.PSN + "%' and LEN(SN)=2", " SN DESC ");
                }

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string tmpsn = ds.Tables[0].Rows[0]["SN"].ToString().Trim();
                    int tmpint = Convert.ToInt32(tmpsn.Substring(model.PSN.Length)) + 1;
                    model.SN = (tmpint >= 10) ? model.PSN + tmpint.ToString() : model.PSN + "0" + tmpint.ToString();
                }
                else
                {
                    model.SN = (model.PSN != null && model.PSN.Trim() != "") ? model.PSN + "01" : "01";
                }
                brdv.success = dal.Add(model) > 0;
                return brdv;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public BaseResultDataValue Update(Model.RBAC.Entity.HR_Departments model)
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
            var dept = dal.GetModel(ID);
            if (dept == null)
            {
                ZhiFang.Common.Log.Log.Error("未能找到此部门，无法删除！部门ID：" + ID);
                throw new Exception("未能找到此部门，无法删除！");
            }
            if (dal.GetRecordCount(" SN like '" + dept.SN + "%' and SN<>'" + dept.SN + "' ") > 0)
            {
                throw new Exception("此部门被引用无法删除！");
            }
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
        public Model.RBAC.Entity.HR_Departments GetModel(int ID)
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
        public DataSet GetList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.RBAC.Entity.HR_Departments> GetListByPage(string strWhere, string orderby, int page, int rows)
        {
            List<Model.RBAC.Entity.HR_Departments> deptlist = new List<Model.RBAC.Entity.HR_Departments>();
            DataSet ds = dal.GetListByPage(strWhere, orderby, page, rows);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                deptlist = DataTableToList(ds.Tables[0]);
            }
            return deptlist;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.RBAC.Entity.HR_Departments> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.RBAC.Entity.HR_Departments> DataTableToList(DataTable dt)
        {
            List<Model.RBAC.Entity.HR_Departments> modelList = new List<Model.RBAC.Entity.HR_Departments>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                
                for (int n = 0; n < rowsCount; n++)
                {
                    Model.RBAC.Entity.HR_Departments model = new Model.RBAC.Entity.HR_Departments();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    model.SN = dt.Rows[n]["SN"].ToString();
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.EName = dt.Rows[n]["EName"].ToString();
                    model.SName = dt.Rows[n]["SName"].ToString();
                    model.Descr = dt.Rows[n]["Descr"].ToString();
                    model.Tel = dt.Rows[n]["Tel"].ToString();
                    model.Fax = dt.Rows[n]["Fax"].ToString();
                    model.Zip = dt.Rows[n]["Zip"].ToString();
                    model.Address = dt.Rows[n]["Address"].ToString();
                    model.Contact = dt.Rows[n]["Contact"].ToString();
                    if (dt.Rows[n]["DeptDesktopID"].ToString() != "")
                    {
                        model.DeptDesktopID = int.Parse(dt.Rows[n]["DeptDesktopID"].ToString());
                    }
                    model.DeptDesktopName = dt.Rows[n]["DeptDesktopName"].ToString();
                    model.ParentOrg = dt.Rows[n]["ParentOrg"].ToString();
                    model.orgType = dt.Rows[n]["orgType"].ToString();
                    model.OrgCode = dt.Rows[n]["OrgCode"].ToString();
                    model.RelationName = dt.Rows[n]["RelationName"].ToString();


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

        public int GetRecordCount(string wherestr)
        {
            return dal.GetRecordCount(wherestr);
        }

        public List<tree> RBAC_GetDeptTree()
        {
            List<tree> tmptreelist = new List<tree>();
            DataSet ds = GetAllList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<Model.RBAC.Entity.HR_Departments> deptlist = new List<Model.RBAC.Entity.HR_Departments>();
                deptlist = DataTableToList(ds.Tables[0]);
                foreach (var dept in deptlist.Where(a => a.SN.Length == 2))
                {
                    tree tmptree = new tree();
                    tmptree.text = dept.CName;
                    tmptree.tid = dept.ID.ToString();
                    tmptree.Target = dept.SN;
                    tmptree.state = "open";
                    string ImageUrl = " ../images/icons/zhi1.gif";
                    tmptree.attributes = "NavigateUrl:'',ImageUrl:'" + ImageUrl + "',Para:'',SN:'" + dept.SN + "'";
                    tmptree.children = RBAC_GetDeptTreeByDeptList(deptlist, tmptree);
                    tmptreelist.Add(tmptree);
                }
            }
            return tmptreelist;
        }

        public List<tree> RBAC_GetDeptTreeByDeptList(List<Model.RBAC.Entity.HR_Departments> deptlist, tree PTree)
        {
            List<tree> tmptreelist = new List<tree>();
            if (deptlist != null && deptlist.Count > 0)
            {
                var tmpdeptlist = deptlist.Where(a => a.SN.IndexOf(PTree.Target) == 0 && a.SN.Trim() != PTree.Target.Trim() && a.SN.Trim().Length - 2 == PTree.Target.Trim().Length);
                foreach (var dept in tmpdeptlist)
                {
                    tree tmptree = new tree();
                    tmptree.text = dept.CName;
                    tmptree.tid = dept.ID.ToString();
                    tmptree.Target = dept.SN;
                    string ImageUrl = " ../images/icons/zhi1.gif";
                    tmptree.attributes = "NavigateUrl:'',ImageUrl:'" + ImageUrl + "',Para:'',SN:'" + dept.SN + "'";
                    tmptree.children = RBAC_GetDeptTreeByDeptList(deptlist, tmptree);
                    tmptreelist.Add(tmptree);
                }
            }
            return tmptreelist;
        }
        #endregion

    }
}