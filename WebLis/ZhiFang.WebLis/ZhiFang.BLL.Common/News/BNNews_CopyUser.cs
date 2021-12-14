using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;
using ZhiFang.Model;

namespace ZhiFang.BLL.Common.News
{
    //文档抄送对象表
    public class BNNews_CopyUser
    {

        private readonly ZhiFang.IDAL.IDNNews_CopyUser dal = DalFactory<IDNNews_CopyUser>.GetDalByClassName("DNNews_CopyUser");
        public BNNews_CopyUser()
        { }

        #region  Method
       
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_News_CopyUser model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.N_News_CopyUser model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long FileCopyUserID)
        {

            return dal.Delete(FileCopyUserID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.N_News_CopyUser GetModel(long FileCopyUserID)
        {

            return dal.GetModel(FileCopyUserID);
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
        public List<Model.N_News_CopyUser> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.N_News_CopyUser> DataTableToList(DataTable dt)
        {
            List<Model.N_News_CopyUser> modelList = new List<Model.N_News_CopyUser>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.N_News_CopyUser model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.N_News_CopyUser();
                    if (dt.Rows[n]["LabID"].ToString() != "")
                    {
                        model.LabID = long.Parse(dt.Rows[n]["LabID"].ToString());
                    }
                    if (dt.Rows[n]["FileCopyUserID"].ToString() != "")
                    {
                        model.FileCopyUserID = long.Parse(dt.Rows[n]["FileCopyUserID"].ToString());
                    }
                    if (dt.Rows[n]["FileID"].ToString() != "")
                    {
                        model.FileID = long.Parse(dt.Rows[n]["FileID"].ToString());
                    }
                    if (dt.Rows[n]["Type"].ToString() != "")
                    {
                        model.Type = long.Parse(dt.Rows[n]["Type"].ToString());
                    }
                    if (dt.Rows[n]["DeptID"].ToString() != "")
                    {
                        model.DeptID = long.Parse(dt.Rows[n]["DeptID"].ToString());
                    }
                    if (dt.Rows[n]["RoleID"].ToString() != "")
                    {
                        model.RoleID = long.Parse(dt.Rows[n]["RoleID"].ToString());
                    }
                    if (dt.Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = long.Parse(dt.Rows[n]["UserID"].ToString());
                    }
                    model.Memo = dt.Rows[n]["Memo"].ToString();
                    if (dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    if (dt.Rows[n]["IsUse"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsUse"].ToString() == "1") || (dt.Rows[n]["IsUse"].ToString().ToLower() == "true"))
                        {
                            model.IsUse = true;
                        }
                        else
                        {
                            model.IsUse = false;
                        }
                    }
                    if (dt.Rows[n]["CreatorID"].ToString() != "")
                    {
                        model.CreatorID = long.Parse(dt.Rows[n]["CreatorID"].ToString());
                    }
                    model.CreatorName = dt.Rows[n]["CreatorName"].ToString();
                    if (dt.Rows[n]["DataAddTime"].ToString() != "")
                    {
                        model.DataAddTime = DateTime.Parse(dt.Rows[n]["DataAddTime"].ToString());
                    }
                    if (dt.Rows[n]["DataUpdateTime"].ToString() != "")
                    {
                        model.DataUpdateTime = DateTime.Parse(dt.Rows[n]["DataUpdateTime"].ToString());
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

        public EntityListEasyUI<N_News_CopyUser> GetModelList(string where, string sort, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public EntityListEasyUI<N_News_CopyUser> GetModelList(string where, int page, int limit)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}