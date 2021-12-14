using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;
namespace ZhiFang.BLL.Report
{
    /// <summary>
    /// 业务逻辑类PrintFormat 的摘要说明。
    /// </summary>
    public class PrintFormat //: ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat
    {
        private readonly IDPrintFormat dal = DalFactory<IDPrintFormat>.GetDalByClassName("PrintFormat");
        public PrintFormat()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.PrintFormat model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.PrintFormat model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string Id)
        {

            return dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.PrintFormat GetModel(string Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Model.PrintFormat GetModelByCache(string Id)
        {

            string CacheKey = "PrintFormatModel-" + Id;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(Id);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.PrintFormat)objModel;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.PrintFormat model)
        {
            return dal.GetList(model);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.PrintFormat> DataTableToList(DataTable dt)
        {
            List<Model.PrintFormat> modelList = new List<Model.PrintFormat>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.PrintFormat model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.PrintFormat();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.PrintFormatName = dt.Rows[n]["PrintFormatName"].ToString();
                    model.PintFormatAddress = dt.Rows[n]["PintFormatAddress"].ToString();
                    model.PintFormatFileName = dt.Rows[n]["PintFormatFileName"].ToString();
                    if (dt.Rows[n]["ItemParaLineNum"].ToString() != "")
                    {
                        model.ItemParaLineNum = int.Parse(dt.Rows[n]["ItemParaLineNum"].ToString());
                    }
                    model.PaperSize = dt.Rows[n]["PaperSize"].ToString();
                    model.PrintFormatDesc = dt.Rows[n]["PrintFormatDesc"].ToString();
                    if (dt.Rows[n]["BatchPrint"].ToString() != "")
                    {
                        model.BatchPrint = int.Parse(dt.Rows[n]["BatchPrint"].ToString());
                    }

                    if (dt.Rows[n]["ImageFlag"].ToString() != "")
                    {
                        model.ImageFlag = int.Parse(dt.Rows[n]["ImageFlag"].ToString());
                    }
                    if (dt.Rows[n]["AntiFlag"].ToString() != "")
                    {
                        model.AntiFlag = int.Parse(dt.Rows[n]["AntiFlag"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.PrintFormat> GetModelList(Model.PrintFormat model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetAllList();
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.PrintFormat model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.PrintFormat model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  成员方法



        #region IBBase<PrintFormat> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion
    }
}

