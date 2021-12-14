using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;
using ZhiFang.Model;

namespace ZhiFang.BLL.Common.News
{
    //N_News_Type
    public class BNNews_Type
    {
        private readonly ZhiFang.IDAL.IDNNews_Type dal = DalFactory<IDNNews_Type>.GetDalByClassName("DNNews_Type");
        public BNNews_Type()
        { }

        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_News_Type model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.N_News_Type model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long DictID)
        {

            return dal.Delete(DictID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.N_News_Type GetModel(long DictID)
        {

            return dal.GetModel(DictID);
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
        public List<Model.N_News_Type> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.N_News_Type> DataTableToList(DataTable dt)
        {
            List<Model.N_News_Type> modelList = new List<Model.N_News_Type>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.N_News_Type model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.N_News_Type();
                    if (dt.Rows[n]["TypeID"].ToString() != "")
                    {
                        model.TypeID = long.Parse(dt.Rows[n]["TypeID"].ToString());
                    }
                    if (dt.Rows[n]["LabID"].ToString() != "")
                    {
                        model.LabID = long.Parse(dt.Rows[n]["LabID"].ToString());
                    }
                    model.SName = dt.Rows[n]["SName"].ToString();
                    model.ShortCode = dt.Rows[n]["Shortcode"].ToString();
                    model.PinYinZiTou = dt.Rows[n]["PinYinZiTou"].ToString();
                    model.CName = dt.Rows[n]["CName"].ToString();
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
                    model.Memo = dt.Rows[n]["Memo"].ToString();
                    if (dt.Rows[n]["DataAddTime"].ToString() != "")
                    {
                        model.DataAddTime = DateTime.Parse(dt.Rows[n]["DataAddTime"].ToString());
                    }
                    model.StandCode = dt.Rows[n]["StandCode"].ToString();
                    model.DeveCode = dt.Rows[n]["DeveCode"].ToString();


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

        public EntityListEasyUI<N_News_Type> GetModelList(string where, string sort, int page, int limit)
        {
            EntityListEasyUI<N_News_Type> list = new EntityListEasyUI<N_News_Type>();
            DataSet ds = new DataSet();
            if (sort == null || sort.Trim() == "")
            {
                ds = dal.GetList(where, page, limit);
            }
            else
            {
                ds = dal.GetList(where, page, limit, sort);
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count >=0)
            {
                list.total= dal.GetCount(where);
                list.rows=DataTableToList(ds.Tables[0]);
            }
            else
            {
                list.total = 0;
                list.rows = new List<N_News_Type>();
            }
            return list;
        }
        public int GetCount(string where)
        {
            return dal.GetCount(where);
        }
        #endregion

    }
}