using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;
using ZhiFang.IBLL.Common.BaseDictionary;

namespace ZhiFang.BLL.Common.BaseDictionary
{
    /// <summary>
    /// TB_CheckClientAccount
    /// </summary>
    public partial class TB_CheckClientAccount:IBTB_CheckClientAccount
    {
        private readonly IDTB_CheckClientAccount dal = DalFactory<IDTB_CheckClientAccount>.GetDalByClassName("TB_CheckClientAccount");
        //private readonly ITB_CheckClientAccount dal = DataAccess.CreateTB_CheckClientAccount();
        //public TB_CheckClientAccount()
        //{ }
         public TB_CheckClientAccount()
        {
            
        }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.TB_CheckClientAccount model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZhiFang.Model.TB_CheckClientAccount model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.TB_CheckClientAccount GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.TB_CheckClientAccount GetModelByCache(int id)
        {
            ZhiFang.Model.TB_CheckClientAccount model = new Model.TB_CheckClientAccount(); ;
            //string CacheKey = "TB_CheckClientAccountModel-" + id;
            //object objModel = CacheKey;
            //if (objModel == null)
            //{
            //    try
            //    {
            //        objModel = dal.GetModel(id);
            //        if (objModel != null)
            //        {
            //            int ModelCache = ZhiFang.Common.ConfigHelper.GetConfigInt("ModelCache");
            //            ommon.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
            //        }
            //    }
            //    catch { }
            //}
            return model;
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
        public List<ZhiFang.Model.TB_CheckClientAccount> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.TB_CheckClientAccount> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.TB_CheckClientAccount> modelList = new List<ZhiFang.Model.TB_CheckClientAccount>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.TB_CheckClientAccount model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
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

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere,ZhiFang.Model.TB_CheckClientAccount model)
        {
            return dal.GetRecordCount(strWhere,model);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, ZhiFang.Model.TB_CheckClientAccount model, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, model, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod


        int IBLL.Common.IBBase<Model.TB_CheckClientAccount>.Update(Model.TB_CheckClientAccount model)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.TB_CheckClientAccount model)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.TB_CheckClientAccount model)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListByPage(Model.TB_CheckClientAccount t, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }
    }
}

