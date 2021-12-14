using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //ClientEleArea		
    public partial class ClientEleArea : IBClientEleArea, IBBatchCopy, IBDataDownload, IBSynchData
    {
        IDAL.IDClientEleArea dal = DALFactory.DalFactory<IDAL.IDClientEleArea>.GetDal("ClientEleArea", ZhiFang.Common.Dictionary.DBSource.LisDB());
        IDAL.IDBatchCopy dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("ClientEleArea", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public ClientEleArea()
        {
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AreaID)
        {
            return dal.Exists(AreaID);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.ClientEleArea model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.ClientEleArea model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int AreaID)
        {
            return dal.Delete(AreaID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.ClientEleArea GetModel(int AreaID)
        {
            return dal.GetModel(AreaID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.ClientEleArea GetModelByCache(int AreaID)
        {

            string CacheKey = "ClientEleAreaModel-" + AreaID;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AreaID);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.ClientEleArea)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.ClientEleArea> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(new ZhiFang.Model.ClientEleArea());
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.ClientEleArea> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.ClientEleArea> modelList = new List<ZhiFang.Model.ClientEleArea>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.ClientEleArea model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.ClientEleArea();
                    if (dt.Columns.Contains("AreaID") && dt.Rows[n]["AreaID"].ToString() != "")
                    {
                        model.AreaID = int.Parse(dt.Rows[n]["AreaID"].ToString());
                    }
                    if (dt.Columns.Contains("AreaCName") && dt.Rows[n]["AreaCName"].ToString() != "")
                    {
                        model.AreaCName = dt.Rows[n]["AreaCName"].ToString();
                    }
                    if (dt.Columns.Contains("AreaShortName") && dt.Rows[n]["AreaShortName"].ToString() != "")
                    {
                        model.AreaShortName = dt.Rows[n]["AreaShortName"].ToString();
                    }
                    if (dt.Columns.Contains("ClientNo") && dt.Rows[n]["ClientNo"].ToString() != "")
                    {
                        model.ClientNo = int.Parse(dt.Rows[n]["ClientNo"].ToString());
                    }
                    if (dt.Columns.Contains("AreaID") && dt.Rows[n]["AddTime"].ToString() != "")
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
        public DataSet GetList(ZhiFang.Model.ClientEleArea model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.ClientEleArea model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.ClientEleArea model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }

        public bool CopyToLab(List<string> lst)
        {
            return dalCopy.CopyToLab(lst);
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }
        public bool Exists(System.Collections.Hashtable ht)
        {
            return dal.Exists(ht);
        }
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        public int AddByDataRow(DataRow dr)
        {
            return dal.AddByDataRow(dr);
        }
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        public int UpdateByDataRow(DataRow dr)
        {
            return dal.UpdateByDataRow(dr);
        }

        #region IBDataDownload 成员

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("ClientEleArea", ZhiFang.Common.Dictionary.DBSource.LisDB());
            try
            {
                DataSet dsAll = dalGetBytStampe.GetListByTimeStampe(LabCode.Trim(), time);
                strXMLSchema = dsAll.GetXmlSchema(); //xml结构文件
                strXML = dsAll.GetXml();//数据内容xml文件
                strMsg = "通过服务获取XML成功";
                return 1;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.ientEleArea.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
                strXML = "";
                strXMLSchema = "";
                strMsg = "失败，TestItem获取最新字典数据出现异常";
                return 0;
            }
        }

        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            throw new NotImplementedException();
        }

        #endregion


        #endregion


        #region IBBatchCopy 成员


        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        public string GetPClientNoBySClientNo(string sclientno)
        {
            DataSet ds = dal.GetPClientNoList(sclientno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["ClientNo"].ToString().Trim();
            }
            return null;
        }

        #endregion


        List<Model.ClientEleArea> IBClientEleArea.DataTableToList(DataTable dt)
        {
            return dal.DataTableToList(dt);
        }


        public bool IsExist(string labCodeNo)
        {
            return dalCopy.IsExist(labCodeNo);
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            return dalCopy.DeleteByLabCode(LabCodeNo);
        }
    }
}