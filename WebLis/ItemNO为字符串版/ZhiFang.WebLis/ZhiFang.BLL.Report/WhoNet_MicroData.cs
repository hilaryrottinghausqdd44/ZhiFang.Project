using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;
namespace ZhiFang.BLL.Report
{
    //WhoNet_MicroData
    public partial class WhoNet_MicroData : ZhiFang.IBLL.Report.IBWhoNet_MicroData
    {
        private readonly IWhoNet_MicroData dal = DalFactory<IWhoNet_MicroData>.GetDalByClassName("WhoNet_MicroData");
        public WhoNet_MicroData()
        { }

        #region  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(ZhiFang.Model.WhoNet_MicroData model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.WhoNet_MicroData model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(long MicroID)
        {

            return dal.Delete(MicroID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.WhoNet_MicroData GetModel(long MicroID)
        {

            return dal.GetModel(MicroID);
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
        public List<ZhiFang.Model.WhoNet_MicroData> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.WhoNet_MicroData> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.WhoNet_MicroData> modelList = new List<ZhiFang.Model.WhoNet_MicroData>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.WhoNet_MicroData model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.WhoNet_MicroData();
                    if (dt.Rows[n]["LabID"].ToString() != "")
                    {
                        model.LabID = long.Parse(dt.Rows[n]["LabID"].ToString());
                    }
                    if (dt.Rows[n]["MicroID"].ToString() != "")
                    {
                        model.MicroID = long.Parse(dt.Rows[n]["MicroID"].ToString());
                    }
                    if (dt.Rows[n]["FomID"].ToString() != "")
                    {
                        model.FomID = long.Parse(dt.Rows[n]["FomID"].ToString());
                    }
                    if (dt.Rows[n]["date_data"].ToString() != "")
                    {
                        model.date_data = DateTime.Parse(dt.Rows[n]["date_data"].ToString());
                    }
                    model.organism = dt.Rows[n]["organism"].ToString();
                    model.org_type = dt.Rows[n]["org_type"].ToString();
                    model.origin = dt.Rows[n]["origin"].ToString();
                    model.ESBL = dt.Rows[n]["ESBL"].ToString();
                    model.comment = dt.Rows[n]["comment"].ToString();
                    if (dt.Rows[n]["DataAddTime"].ToString() != "")
                    {
                        model.DataAddTime = DateTime.Parse(dt.Rows[n]["DataAddTime"].ToString());
                    }
                    if (dt.Rows[n]["DataUpdateTime"].ToString() != "")
                    {
                        model.DataUpDateTime = DateTime.Parse(dt.Rows[n]["DataUpdateTime"].ToString());
                    }
                    if (dt.Rows[n]["DataTimeStamp"].ToString() != "")
                    {
                        model.DataTimeStamp = DateTime.Parse(dt.Rows[n]["DataTimeStamp"].ToString());
                    }
                    model.beta_lact = dt.Rows[n]["beta_lact"].ToString();



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