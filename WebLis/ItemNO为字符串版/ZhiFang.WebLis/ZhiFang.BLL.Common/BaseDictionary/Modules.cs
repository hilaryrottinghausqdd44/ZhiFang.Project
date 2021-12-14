using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //Modules		
    public partial class Modules :IBLL.Common.IBSynchData, IBModules
    {
        IDAL.IDModules dal = DALFactory.DalFactory<IDAL.IDModules>.GetDal("S_Modules", ZhiFang.Common.Dictionary.DBSource.LisDB());
        public Modules()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Modules model)
        {
            if (model.PID == 0)
            {
                model.PSN = null;
                model.Rank = 1;
            }
            else
            {
                Model.Modules pm = this.GetModel(model.PID);
                model.PSN = pm.SN;
                model.Rank = (pm.SN.Length / 2) ;
            }
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Modules model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID)
        {
            return dal.Delete(ID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Modules GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.Modules GetModelByCache(int ID)
        {

            string CacheKey = "S_ModulesModel-" + ID;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.Modules)objModel;
        }

        
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Modules> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(null);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Modules> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.Modules> modelList = new List<ZhiFang.Model.Modules>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.Modules model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.Modules();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    model.Descr = dt.Rows[n]["Descr"].ToString();
                    model.ButtonsTheme = dt.Rows[n]["ButtonsTheme"].ToString();
                    if (dt.Rows[n]["Owner"].ToString() != "")
                    {
                        model.Owner = int.Parse(dt.Rows[n]["Owner"].ToString());
                    }
                    if (dt.Rows[n]["CreateDate"].ToString() != "")
                    {
                        model.CreateDate = DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
                    }
                    model.ModuleCode = dt.Rows[n]["ModuleCode"].ToString();
                    model.SN = dt.Rows[n]["SN"].ToString();
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.EName = dt.Rows[n]["EName"].ToString();
                    model.SName = dt.Rows[n]["SName"].ToString();
                    if (dt.Rows[n]["Type"].ToString() != "")
                    {
                        model.Type = int.Parse(dt.Rows[n]["Type"].ToString());
                    }
                    model.Image = dt.Rows[n]["Image"].ToString();
                    model.URL = dt.Rows[n]["URL"].ToString();
                    model.Para = dt.Rows[n]["Para"].ToString();


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
        public DataSet GetList(ZhiFang.Model.Modules model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.Modules model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.Modules model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }

        #endregion

        #region IBModules 成员


        public DataSet GetListByRBACModulesList(List<string> rbac_moduleslist)
        {
            return dal.GetListByRBACModulesList(rbac_moduleslist);
        }

        #endregion

        #region IBModules 成员


        public bool CheckModuleCode(string p)
        {
            if (dal.GetTotalCount(new Model.Modules() { ModuleCode = p }) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region IBBase<Modules> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion

        #region IBSynchData 成员


        public bool Exists(System.Collections.Hashtable ht)
        {
            throw new NotImplementedException();
        }

        public int AddByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        public int UpdateByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
