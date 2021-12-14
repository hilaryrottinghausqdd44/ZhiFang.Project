using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IDAL;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //BusinessLogicClientControl		
    public partial class BusinessLogicClientControl : IBBusinessLogicClientControl
    {
        IDAL.IDBusinessLogicClientControl dal;
        IDAL.IDBatchCopy dalCopy;

        public BusinessLogicClientControl()
		{
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDBusinessLogicClientControl>.GetDal("BusinessLogicClientControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDBusinessLogicClientControl>.GetDal("BusinessLogicClientControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_BusinessLogicClientControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Account, string ClientNo)
        {
            return dal.Exists(Account, ClientNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.BusinessLogicClientControl  model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.BusinessLogicClientControl  model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string Account, string ClientNo)
        {
            return dal.Delete(Account, ClientNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.BusinessLogicClientControl  GetModel(string Account, string ClientNo)
        {
            return dal.GetModel(Account, ClientNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.BusinessLogicClientControl  GetModelByCache(string Account, string ClientNo)
        {

            string CacheKey = "BusinessLogicClientControlModel-" + Account + ClientNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(Account, ClientNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.BusinessLogicClientControl )objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.BusinessLogicClientControl > DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.BusinessLogicClientControl > modelList = new List<ZhiFang.Model.BusinessLogicClientControl >();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.BusinessLogicClientControl  model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.BusinessLogicClientControl ();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("Account") && dt.Rows[n]["Account"].ToString() != "")
                    {
                        model.Account = dt.Rows[n]["Account"].ToString();
                    }
                    if (dt.Columns.Contains("ClientNo") && dt.Rows[n]["ClientNo"].ToString() != "")
                    {
                        model.ClientNo = dt.Rows[n]["ClientNo"].ToString();
                    }
                    if (dt.Columns.Contains("ClientNo") && dt.Rows[n]["ClientNo"].ToString() != "")
                    {
                        model.ClIENTNO = dt.Rows[n]["ClientNo"].ToString();
                    }
                    if (dt.Columns.Contains("CNAME") && dt.Rows[n]["CNAME"].ToString() != "")
                    {
                        model.CNAME = dt.Rows[n]["CNAME"].ToString();
                    }
                    if (dt.Columns.Contains("AddTime") && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
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
        public DataSet GetList(ZhiFang.Model.BusinessLogicClientControl  model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.BusinessLogicClientControl  model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.BusinessLogicClientControl  model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }

        public bool CopyToLab(List<string> lst)
        {
            return dalCopy.CopyToLab(lst);
        }
        #endregion

        #region IBBusinessLogicClientControl 成员

        public string GetClientList_String(Model.BusinessLogicClientControl l_m)
        {
            DataSet ds=this.GetList(l_m);
            string ClientListStr = "";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ClientListStr += "("+dr["ClientNo"] + ")" + dr["CName"] + ",";
                }
            }
            if (ClientListStr.Trim().Length > 0)
                ClientListStr = ClientListStr.Remove(ClientListStr.Length - 1);
            return ClientListStr;
        }

        public DataSet GetClientList_DataSet(Model.BusinessLogicClientControl l_m)
        {
            DataSet ds = this.GetList(l_m);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            return new DataSet();
        }

        public bool Add(List<Model.BusinessLogicClientControl> l)
        {
            return dal.Add(l);
        }
        
        public DataSet GetClientList_DataSet(int p, Model.BusinessLogicClientControl businessLogicClientControl)
        {
            return this.GetListByPage(businessLogicClientControl, 0, p);
        }
        
        public int Delete(string Id)
        {
            return dal.DeleteList(Id);
        }

        #endregion

        #region IBBusinessLogicClientControl 成员


        public EntityList<Model.CLIENTELE> GetBusinessLogicClientList(Model.BusinessLogicClientControl l_m, int page, int limit, string fields, string where, string sort)
        {
            EntityList<Model.CLIENTELE> entitylist = new EntityList<Model.CLIENTELE>();
            DataSet ds = dal.GetBusinessLogicClientListByPage(l_m, page, limit, fields, where, sort);
            CLIENTELE c = new CLIENTELE();
            entitylist.list=c.DataTableToList(ds.Tables[0]);
            entitylist.count = dal.GetTotalCount(l_m, where);
            return entitylist;
        }

        #endregion
    }
}
