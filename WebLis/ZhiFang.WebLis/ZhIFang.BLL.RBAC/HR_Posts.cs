using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ZhiFang.Model.RBAC.Entity;
using ZhiFang.Model;

namespace ZhiFang.BLL.RBAC
{
    //HR_Posts
    public class HR_Posts
    {

        private readonly ZhiFang.DAL.RBAC.HR_Posts dal = new ZhiFang.DAL.RBAC.HR_Posts();
        public HR_Posts()
        { }

        #region  Method

        #region 注释

        ///// <summary>
        ///// 是否存在该记录
        ///// </summary>
        //public bool Exists(int ID)
        //{
        //    return dal.Exists(ID);
        //}
        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public int Add(Maticsoft.Model.HR_Posts model)
        //{
        //    return dal.Add(model);

        //}

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(Maticsoft.Model.HR_Posts model)
        //{
        //    return dal.Update(model);
        //}

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public bool Delete(int ID)
        //{

        //    return dal.Delete(ID);
        //}
        ///// <summary>
        ///// 批量删除一批数据
        ///// </summary>
        //public bool DeleteList(string IDlist)
        //{
        //    return dal.DeleteList(IDlist);
        //}

        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public Maticsoft.Model.HR_Posts GetModel(int ID)
        //{

        //    return dal.GetModel(ID);
        //}

        ///// <summary>
        ///// 得到一个对象实体，从缓存中
        ///// </summary>
        //public Maticsoft.Model.HR_Posts GetModelByCache(int ID)
        //{

        //    string CacheKey = "HR_PostsModel-" + ID;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(ID);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch { }
        //    }
        //    return (Maticsoft.Model.HR_Posts)objModel;
        //}
        #endregion
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
        public List<ZhiFang.Model.RBAC.Entity.HR_Posts> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.RBAC.Entity.HR_Posts> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.RBAC.Entity.HR_Posts> modelList = new List<ZhiFang.Model.RBAC.Entity.HR_Posts>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.RBAC.Entity.HR_Posts model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.RBAC.Entity.HR_Posts();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    model.SN = dt.Rows[n]["SN"].ToString();
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.EName = dt.Rows[n]["EName"].ToString();
                    model.SName = dt.Rows[n]["SName"].ToString();
                    model.Descr = dt.Rows[n]["Descr"].ToString();
                    model.GroupName = dt.Rows[n]["GroupName"].ToString();
                    if (dt.Rows[n]["GroupOrder"].ToString() != "")
                    {
                        model.GroupOrder = int.Parse(dt.Rows[n]["GroupOrder"].ToString());
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
        #endregion

    }
}