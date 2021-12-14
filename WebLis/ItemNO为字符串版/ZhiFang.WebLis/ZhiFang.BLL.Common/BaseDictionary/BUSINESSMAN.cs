using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //BUSINESSMAN		
    public partial class BUSINESSMAN : IBBUSINESSMAN, IBBatchCopy, IBDataDownload, IBSynchData
    {
        IDAL.IDBUSINESSMAN dal;
        IDAL.IDBatchCopy dalCopy;

        public BUSINESSMAN()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDBUSINESSMAN>.GetDal("BUSINESSMAN", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("BUSINESSMAN", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDBUSINESSMAN>.GetDal("B_BUSINESSMAN", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_BUSINESSMAN", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int BMANNO)
        {
            return dal.Exists(BMANNO);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.BUSINESSMAN model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.BUSINESSMAN model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int BMANNO)
        {
            return dal.Delete(BMANNO);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string BNANIDlist)
        {
            return dal.DeleteList(BNANIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.BUSINESSMAN GetModel(int BMANNO)
        {
            return dal.GetModel(BMANNO);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.BUSINESSMAN GetModelByCache(int BMANNO)
        {

            string CacheKey = "B_BUSINESSMANModel-" + BMANNO;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(BMANNO);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.BUSINESSMAN)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.BUSINESSMAN> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(new ZhiFang.Model.BUSINESSMAN());
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.BUSINESSMAN> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.BUSINESSMAN> modelList = new List<ZhiFang.Model.BUSINESSMAN>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.BUSINESSMAN model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.BUSINESSMAN();
                    //if (dt.Rows[n]["BNANID"].ToString() != "")
                    //{
                    //    model.BNANID = int.Parse(dt.Rows[n]["BNANID"].ToString());
                    //}
                    model.CNAME = dt.Rows[n]["CNAME"].ToString();
                    if (dt.Rows[n]["BMANNO"].ToString() != "")
                    {
                        model.BMANNO = int.Parse(dt.Rows[n]["BMANNO"].ToString());
                    }
                    model.SHORTCODE = dt.Rows[n]["SHORTCODE"].ToString();
                    if (dt.Rows[n]["ISUSE"].ToString() != "")
                    {
                        model.ISUSE = int.Parse(dt.Rows[n]["ISUSE"].ToString());
                    }
                    model.IDCODE = dt.Rows[n]["IDCODE"].ToString();
                    model.ADDRESS = dt.Rows[n]["ADDRESS"].ToString();
                    model.PHONENUM = dt.Rows[n]["PHONENUM"].ToString();
                    model.ROMARK = dt.Rows[n]["ROMARK"].ToString();
                    if (dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    //if (dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
                    //if (dt.Rows[n]["AddTime"].ToString() != "")
                    //{
                    //    model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    //}
                    //model.StandCode = dt.Rows[n]["StandCode"].ToString();
                    //model.ZFStandCode = dt.Rows[n]["ZFStandCode"].ToString();
                    //if (dt.Rows[n]["UseFlag"].ToString() != "")
                    //{
                    //    model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
                    //}


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
        public DataSet GetList(ZhiFang.Model.BUSINESSMAN model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.BUSINESSMAN model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.BUSINESSMAN model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }

        public bool CopyToLab(List<string> lst)
        {
            return dalCopy.CopyToLab(lst);
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #region IBDataDownload 成员

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("B_BUSINESSMAN", ZhiFang.Common.Dictionary.DBSource.LisDB());
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
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.BUSINESSMAN.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
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

        public int DeleteByDataRow(DataRow dr)
        {
            return dalCopy.DeleteByDataRow(dr);
        }

        #endregion


        public bool IsExist(string labCodeNo)
        {
            return dalCopy.IsExist(labCodeNo);
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            //throw new NotImplementedException();
            return dalCopy.DeleteByLabCode(LabCodeNo);
        }
    }
}