using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;
namespace ZhiFang.BLL.Report
{
    //WhoNet_AntiData
    public partial class WhoNet_AntiData : ZhiFang.IBLL.Report.IBWhoNet_AntiData
    {

        private readonly IWhoNet_AntiData dal = DalFactory<IWhoNet_AntiData>.GetDalByClassName("WhoNet_AntiData");
        public WhoNet_AntiData()
        { }

        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.WhoNet_AntiData model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.WhoNet_AntiData model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(long AntiID)
        {

            return dal.Delete(AntiID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.WhoNet_AntiData GetModel(long AntiID)
        {

            return dal.GetModel(AntiID);
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
        public List<ZhiFang.Model.WhoNet_AntiData> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.WhoNet_AntiData> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.WhoNet_AntiData> modelList = new List<ZhiFang.Model.WhoNet_AntiData>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.WhoNet_AntiData model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.WhoNet_AntiData();
                    if (dt.Rows[n]["LabID"].ToString() != "")
                    {
                        model.LabID = long.Parse(dt.Rows[n]["LabID"].ToString());
                    }
                    if (dt.Rows[n]["AntiID"].ToString() != "")
                    {
                        model.AntiID = long.Parse(dt.Rows[n]["AntiID"].ToString());
                    }
                    model.AntiName = dt.Rows[n]["AntiName"].ToString();
                    model.TestMethod = dt.Rows[n]["TestMethod"].ToString();
                    model.RefRange = dt.Rows[n]["RefRange"].ToString();
                    model.Suscept = dt.Rows[n]["Suscept"].ToString();
                    if (dt.Rows[n]["DataAddTime"].ToString() != "")
                    {
                        model.DataAddTime = DateTime.Parse(dt.Rows[n]["DataAddTime"].ToString());
                    }
                    if (dt.Rows[n]["DataUpdateTime"].ToString() != "")
                    {
                        model.DataUpdateTime = DateTime.Parse(dt.Rows[n]["DataUpdateTime"].ToString());
                    }
                    if (dt.Rows[n]["DataTimeStamp"].ToString() != "")
                    {
                        model.DataTimeStamp = DateTime.Parse(dt.Rows[n]["DataTimeStamp"].ToString());
                    }
                    if (dt.Rows[n]["MicroID"].ToString() != "")
                    {
                        model.MicroID = long.Parse(dt.Rows[n]["MicroID"].ToString());
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