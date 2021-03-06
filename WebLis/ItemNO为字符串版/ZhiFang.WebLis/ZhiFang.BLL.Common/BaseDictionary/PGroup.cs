using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //PGroup		
    public partial class PGroup : IBSynchData, IBPGroup, IBBatchCopy, IBDataDownload
    {
        IDAL.IDPGroup dal;
        IDAL.IDBatchCopy dalCopy;

        public PGroup()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDPGroup>.GetDal("PGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("PGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDPGroup>.GetDal("B_PGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_PGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int SectionNo)
        {
            return dal.Exists(SectionNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.PGroup model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.PGroup model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int SectionNo)
        {
            return dal.Delete(SectionNo);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string SectionIDlist)
        {
            return dal.DeleteList(SectionIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.PGroup GetModel(int SectionNo)
        {
            return dal.GetModel(SectionNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.PGroup GetModelByCache(int SectionNo)
        {

            string CacheKey = "B_PGroupModel-" + SectionNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(SectionNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.PGroup)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.PGroup> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.PGroup> modelList = new List<ZhiFang.Model.PGroup>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.PGroup model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.PGroup();
                    if (dt.Columns.Contains("SectionID") && dt.Rows[n]["SectionID"].ToString() != "")
                    {
                        model.SectionID = int.Parse(dt.Rows[n]["SectionID"].ToString());
                    }
                    if (dt.Columns.Contains("SectionNo") && dt.Rows[n]["SectionNo"].ToString() != "")
                    {
                        model.SectionNo = int.Parse(dt.Rows[n]["SectionNo"].ToString());
                    }
                    if (dt.Columns.Contains("SuperGroupNo") && dt.Rows[n]["SuperGroupNo"].ToString() != "")
                    {
                        model.SuperGroupNo = int.Parse(dt.Rows[n]["SuperGroupNo"].ToString());
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Columns.Contains("ShortName") && dt.Rows[n]["ShortName"].ToString() != "")
                    {
                        model.ShortName = dt.Rows[n]["ShortName"].ToString();
                    }
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    if (dt.Columns.Contains("SectionDesc") && dt.Rows[n]["SectionDesc"].ToString() != "")
                    {
                        model.SectionDesc = dt.Rows[n]["SectionDesc"].ToString();
                    }
                    if (dt.Columns.Contains("SectionType") && dt.Rows[n]["SectionType"].ToString() != "")
                    {
                        model.SectionType = int.Parse(dt.Rows[n]["SectionType"].ToString());
                    }
                    if (dt.Columns.Contains("Visible") && dt.Rows[n]["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
                    }
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    if (dt.Columns.Contains("OnlineTime") && dt.Rows[n]["OnlineTime"].ToString() != "")
                    {
                        model.OnlineTime = int.Parse(dt.Rows[n]["OnlineTime"].ToString());
                    }
                    if (dt.Columns.Contains("KeyDispOrder") && dt.Rows[n]["KeyDispOrder"].ToString() != "")
                    {
                        model.KeyDispOrder = int.Parse(dt.Rows[n]["KeyDispOrder"].ToString());
                    }
                    if (dt.Columns.Contains("DummyGroup") && dt.Rows[n]["DummyGroup"].ToString() != "")
                    {
                        model.DummyGroup = int.Parse(dt.Rows[n]["DummyGroup"].ToString());
                    }
                    if (dt.Columns.Contains("UnionType") && dt.Rows[n]["UnionType"].ToString() != "")
                    {
                        model.UnionType = int.Parse(dt.Rows[n]["UnionType"].ToString());
                    }
                    if (dt.Columns.Contains("SectorTypeNo") && dt.Rows[n]["SectorTypeNo"].ToString() != "")
                    {
                        model.SectorTypeNo = int.Parse(dt.Rows[n]["SectorTypeNo"].ToString());
                    }
                    if (dt.Columns.Contains("SampleRule") && dt.Rows[n]["SampleRule"].ToString() != "")
                    {
                        model.SampleRule = int.Parse(dt.Rows[n]["SampleRule"].ToString());
                    }
                    //if (dt.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
                    if (dt.Columns.Contains("AddTime") && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    if (dt.Columns.Contains("StandCode") && dt.Rows[n]["StandCode"].ToString() != "")
                    {
                        model.StandCode = dt.Rows[n]["StandCode"].ToString();
                    }
                    if (dt.Columns.Contains("ZFStandCode") && dt.Rows[n]["ZFStandCode"].ToString() != "")
                    {
                        model.ZFStandCode = dt.Rows[n]["ZFStandCode"].ToString();
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
            return GetList(new Model.PGroup());
        }
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.PGroup model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.PGroup model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.PGroup model, int nowPageNum, int nowPageSize)
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
        #endregion


        #region IBPGroup 成员


        public Model.PGroup GetModel(int SectionNo, int p)
        {
            DataSet ds = this.GetList(new Model.PGroup() { SectionNo = SectionNo, Visible = p });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return this.DataTableToList(ds.Tables[0])[0];
            }
            else
            {
                return new Model.PGroup();
            }
        }

        #endregion

        #region IBDataDownload 成员

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("B_PGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());
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
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.PGroup.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
                strXML = "";
                strXMLSchema = "";
                strMsg = "失败，PGroup获取最新字典数据出现异常";
                return 0;
            }
        }

        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBBase<PGroup> 成员


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

        #region IBBatchCopy 成员


        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion


        public bool IsExist(string labCodeNo)
        {
            return dalCopy.IsExist(labCodeNo);
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            return dalCopy.DeleteByLabCode(LabCodeNo);
        }


        public int Add(List<Model.PGroup> modelList)
        {
            return dal.Add(modelList);
        }


        public int Update(List<Model.PGroup> modelList)
        {
            return dal.Update(modelList);
        }
    }
}
