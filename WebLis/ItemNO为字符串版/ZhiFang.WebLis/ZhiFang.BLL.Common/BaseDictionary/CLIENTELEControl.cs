using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //CLIENTELEControl		
    public partial class CLIENTELEControl : IBCLIENTELEControl, IBSynchData
    {
        IDAL.IDCLIENTELEControl dal = DALFactory.DalFactory<IDAL.IDCLIENTELEControl>.GetDal("B_CLIENTELEControl", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public CLIENTELEControl()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ClIENTControlNo)
        {
            return dal.Exists(ClIENTControlNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.CLIENTELEControl model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.CLIENTELEControl model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string ClIENTControlNo)
        {
            return dal.Delete(ClIENTControlNo);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.CLIENTELEControl GetModel(string ClIENTControlNo)
        {
            return dal.GetModel(ClIENTControlNo);
        }


        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.CLIENTELEControl GetModelByCache(string ClIENTControlNo)
        {

            string CacheKey = "B_CLIENTELEControlModel-" + ClIENTControlNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ClIENTControlNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.CLIENTELEControl)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.CLIENTELEControl> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(new ZhiFang.Model.CLIENTELEControl());
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.CLIENTELEControl> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.CLIENTELEControl> modelList = new List<ZhiFang.Model.CLIENTELEControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.CLIENTELEControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.CLIENTELEControl();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("ClIENTControlNo") && dt.Rows[n]["ClIENTControlNo"].ToString() != "")
                    {
                        model.ClIENTControlNo = dt.Rows[n]["ClIENTControlNo"].ToString();
                    }
                    if (dt.Columns.Contains("ClIENTNO") && dt.Rows[n]["ClIENTNO"].ToString() != "")
                    {
                        model.ClIENTNO = int.Parse(dt.Rows[n]["ClIENTNO"].ToString());
                    }
                    if (dt.Columns.Contains("ControlLabNo") && dt.Rows[n]["ControlLabNo"].ToString() != "")
                    {
                        model.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlClIENTNO") && dt.Rows[n]["ControlClIENTNO"].ToString() != "")
                    {
                        model.ControlClIENTNO = int.Parse(dt.Rows[n]["ControlClIENTNO"].ToString());
                    }
                    //    if (dt.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe=DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
                    if (dt.Columns.Contains("AddTime") && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    if (dt.Columns.Contains("UseFlag") && dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
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
            return dal.GetAllList();
        }
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.CLIENTELEControl model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.CLIENTELEControl model)
        {
            return dal.GetTotalCount(model);
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }
        #endregion


        #region IBSynchData 成员


        public bool Exists(System.Collections.Hashtable ht)
        {
            return dal.Exists(ht);
        }

        public int AddByDataRow(DataRow dr)
        {
            return dal.AddByDataRow(dr);
        }

        public int UpdateByDataRow(DataRow dr)
        {
            return dal.UpdateByDataRow(dr);
        }

        #endregion
    }
}