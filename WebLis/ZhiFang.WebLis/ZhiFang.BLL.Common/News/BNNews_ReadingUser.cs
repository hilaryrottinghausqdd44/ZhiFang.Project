using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;
using ZhiFang.Model;

namespace ZhiFang.BLL.Common.News
{
    //文档阅读对象表
    public class BNNews_ReadingUser
    {

        private readonly ZhiFang.IDAL.IDNNews_ReadingUser dal = DalFactory<IDNNews_ReadingUser>.GetDalByClassName("DNNews_ReadingUser");
        public BNNews_ReadingUser()
        { }

        #region  Method		

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_News_ReadingUser model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.N_News_ReadingUser model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long FileReadingUserID)
        {

            return dal.Delete(FileReadingUserID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.N_News_ReadingUser GetModel(long FileReadingUserID)
        {

            return dal.GetModel(FileReadingUserID);
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
        public List<Model.N_News_ReadingUser> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.N_News_ReadingUser> DataTableToList(DataTable dt)
        {
            List<Model.N_News_ReadingUser> modelList = new List<Model.N_News_ReadingUser>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.N_News_ReadingUser model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.N_News_ReadingUser();
                    if (dt.Rows[n]["LabID"].ToString() != "")
                    {
                        model.LabID = long.Parse(dt.Rows[n]["LabID"].ToString());
                    }
                    if (dt.Rows[n]["FileReadingUserID"].ToString() != "")
                    {
                        model.FileReadingUserID = long.Parse(dt.Rows[n]["FileReadingUserID"].ToString());
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

        public EntityListEasyUI<N_News_ReadingUser> GetModelList(string where, string sort, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public EntityListEasyUI<N_News_ReadingUser> GetModelList(string where, int page, int limit)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}