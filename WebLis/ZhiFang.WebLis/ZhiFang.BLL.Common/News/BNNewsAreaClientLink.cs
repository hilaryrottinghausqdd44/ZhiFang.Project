using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;
using ZhiFang.Model;

namespace ZhiFang.BLL.Common.News
{
    //N_NewsAreaClientLink
    public class BNNewsAreaClientLink
    {
        private readonly ZhiFang.IDAL.IDNNewsAreaClientLink dal = DalFactory<IDNNewsAreaClientLink>.GetDalByClassName("DNNewsAreaClientLink");
        public BNNewsAreaClientLink()
        { }

        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_NewsAreaClientLink model)
        {
            return dal.Add(model);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long NewsAreaClientLinkId)
        {

            return dal.Delete(NewsAreaClientLinkId);
        }

        public Model.N_NewsAreaClientLink GetModel(long NewsAreaClientLinkId)
        {
            return dal.GetModel(NewsAreaClientLinkId);
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
        public List<Model.N_NewsAreaClientLink> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.N_NewsAreaClientLink> DataTableToList(DataTable dt)
        {
            List<Model.N_NewsAreaClientLink> modelList = new List<Model.N_NewsAreaClientLink>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.N_NewsAreaClientLink model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.N_NewsAreaClientLink();
                    if (dt.Rows[n]["LabId"].ToString() != "")
                    {
                        model.LabId = long.Parse(dt.Rows[n]["LabId"].ToString());
                    }
                    if (dt.Rows[n]["NewsAreaClientLinkId"].ToString() != "")
                    {
                        model.NewsAreaClientLinkId = long.Parse(dt.Rows[n]["NewsAreaClientLinkId"].ToString());
                    }
                    if (dt.Rows[n]["NewsAreaId"].ToString() != "")
                    {
                        model.NewsAreaId = long.Parse(dt.Rows[n]["NewsAreaId"].ToString());
                    }
                    model.NewsAreaName = dt.Rows[n]["NewsAreaName"].ToString();
                    if (dt.Rows[n]["ClientNo"].ToString() != "")
                    {
                        model.ClientNo = long.Parse(dt.Rows[n]["ClientNo"].ToString());
                    }
                    model.ClientName = dt.Rows[n]["ClientName"].ToString();
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

        public EntityListEasyUI<N_NewsAreaClientLink> GetModelList(string where, string sort, int page, int limit)
        {
            EntityListEasyUI<N_NewsAreaClientLink> list = new EntityListEasyUI<N_NewsAreaClientLink>();
            DataSet ds = new DataSet();
            if (sort == null || sort.Trim() == "")
            {
                ds = dal.GetList(where, page, limit,null);
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
                list.rows = new List<N_NewsAreaClientLink>();
            }
            return list;
        }

        public EntityListEasyUI<N_NewsAreaClientLink> GetModelList(string where, int page, int limit)
        {
            return GetModelList(where, null, page, limit);
        }

        public EntityListEasyUI<CLIENTELE> GetUnSelectList(string NewsAreaId, string where, string sort, int page, int limit)
        {
            EntityListEasyUI<CLIENTELE> list = new EntityListEasyUI<CLIENTELE>();
            DataSet ds = new DataSet();
           
                ds = dal.GetNewsAreaIdUnSelectList(NewsAreaId, where, page, limit, sort);
           
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count >= 0)
            {
                BaseDictionary.CLIENTELE c = new BaseDictionary.CLIENTELE();
                list.total = dal.GetNewsAreaIdUnSelectListCount(NewsAreaId, where);               
                list.rows = c.DataTableToList(ds.Tables[0]);
            }
            else
            {
                list.total = 0;
                list.rows = new List<CLIENTELE>();
            }
            return list;
        }
        #endregion

    }
}