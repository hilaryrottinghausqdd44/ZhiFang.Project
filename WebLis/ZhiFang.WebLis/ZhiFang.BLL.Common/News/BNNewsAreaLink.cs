using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;
using ZhiFang.Model;

namespace ZhiFang.BLL.Common.News
{
    //N_NewsAreaLink
    public class BNNewsAreaLink
    {
        private readonly ZhiFang.IDAL.IDNNewsAreaLink dal = DalFactory<IDNNewsAreaLink>.GetDalByClassName("DNNewsAreaLink");
        public BNNewsAreaLink()
        { }

        #region  Method		

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_NewsAreaLink model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long NewsAreaLinkId)
        {

            return dal.Delete(NewsAreaLinkId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.N_NewsAreaLink GetModel(long NewsAreaLinkId)
        {

            return dal.GetModel(NewsAreaLinkId);
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
        public List<Model.N_NewsAreaLink> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.N_NewsAreaLink> DataTableToList(DataTable dt)
        {
            List<Model.N_NewsAreaLink> modelList = new List<Model.N_NewsAreaLink>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.N_NewsAreaLink model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.N_NewsAreaLink();
                    if (dt.Rows[n]["LabID"].ToString() != "")
                    {
                        model.LabID = long.Parse(dt.Rows[n]["LabID"].ToString());
                    }
                    if (dt.Rows[n]["NewsAreaLinkId"].ToString() != "")
                    {
                        model.NewsAreaLinkId = long.Parse(dt.Rows[n]["NewsAreaLinkId"].ToString());
                    }
                    if (dt.Rows[n]["NewsAreaId"].ToString() != "")
                    {
                        model.NewsAreaId = long.Parse(dt.Rows[n]["NewsAreaId"].ToString());
                    }
                    model.NewsAreaName = dt.Rows[n]["NewsAreaName"].ToString();
                    if (dt.Rows[n]["NewsId"].ToString() != "")
                    {
                        model.NewsId = long.Parse(dt.Rows[n]["NewsId"].ToString());
                    }
                    model.NewsIdName = dt.Rows[n]["NewsIdName"].ToString();
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

        public EntityListEasyUI<N_NewsAreaLink> GetModelList(string where, string sort, int page, int limit)
        {
            EntityListEasyUI<N_NewsAreaLink> list = new EntityListEasyUI<N_NewsAreaLink>();
            DataSet ds = new DataSet();
            if (sort == null || sort.Trim() == "")
            {
                ds = dal.GetList(where, page, limit, null);
            }
            else
            {
                ds = dal.GetList(where, page, limit, sort);
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count >= 0)
            {
                list.total = dal.GetCount(where);
                list.rows = DataTableToList(ds.Tables[0]);
            }
            else
            {
                list.total = 0;
                list.rows = new List<N_NewsAreaLink>();
            }
            return list;
        }

        public EntityListEasyUI<N_NewsAreaLink> GetModelList(string where, int page, int limit)
        {
            return GetModelList(where, null, page, limit);
        }
        public EntityListEasyUI<N_News_Area> GetUnSelectList(string NewsId, string sort, int page, int limit)
        {
            EntityListEasyUI<N_News_Area> list = new EntityListEasyUI<N_News_Area>();
            DataSet ds = new DataSet();

            ds = dal.GetNewsAreaIdUnSelectList(NewsId, page, limit, sort);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count >= 0)
            {
                BNNews_Area c = new BNNews_Area();
                list.total = dal.GetNewsAreaIdUnSelectListCount(NewsId);
                list.rows = c.DataTableToList(ds.Tables[0]);
            }
            else
            {
                list.total = 0;
                list.rows = new List<N_News_Area>();
            }
            return list;
        }
        #endregion

    }
}