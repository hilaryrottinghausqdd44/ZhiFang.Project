using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //SickType		
    public partial class SickType : IBSynchData, IBSickType, IBBatchCopy, IBDataDownload
    {
        IDAL.IDSickType dal;
        IDAL.IDBatchCopy dalCopy;

        public SickType()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDSickType>.GetDal("SickType", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("SickType", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDSickType>.GetDal("B_SickType", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_SickType", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int SickTypeNo)
        {
            return dal.Exists(SickTypeNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.SickType model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.SickType model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int SickTypeNo)
        {
            return dal.Delete(SickTypeNo);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string SickTypeIDlist)
        {
            return dal.DeleteList(SickTypeIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.SickType GetModel(int SickTypeNo)
        {
            return dal.GetModel(SickTypeNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.SickType GetModelByCache(int SickTypeNo)
        {

            string CacheKey = "B_SickTypeModel-" + SickTypeNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(SickTypeNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.SickType)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.SickType> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.SickType> modelList = new List<ZhiFang.Model.SickType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.SickType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.SickType();
                    //if (dt.Rows[n]["SickTypeID"].ToString() != "")
                    //{
                    //    model.SickTypeID = int.Parse(dt.Rows[n]["SickTypeID"].ToString());
                    //}
                    if (dt.Columns.Contains("SickTypeNo") && dt.Rows[n]["SickTypeNo"].ToString() != "")
                    {
                        model.SickTypeNo = int.Parse(dt.Rows[n]["SickTypeNo"].ToString());
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    if (dt.Columns.Contains("HisOrderCode") && dt.Rows[n]["HisOrderCode"].ToString() != "")
                    {
                        model.HisOrderCode = dt.Rows[n]["HisOrderCode"].ToString();
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
            return dal.GetAllList();
        }
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.SickType model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.SickType model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.SickType model, int nowPageNum, int nowPageSize)
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

        #region IBDataDownload 成员

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("B_SickType", ZhiFang.Common.Dictionary.DBSource.LisDB());
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
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.SickType.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
                strXML = "";
                strXMLSchema = "";
                strMsg = "失败，SickType获取最新字典数据出现异常";
                return 0;
            }
        }

        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBBase<SickType> 成员


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


        public int Add(List<Model.SickType> modelList)
        {
            return dal.Add(modelList);
        }

        public int Update(List<Model.SickType> modelList)
        {
            return dal.Update(modelList);
        }

        public BaseResult CopyToLabByItemNoList(List<string> itemNos, List<string> labCodeNo)
        {
            BaseResult br = new BaseResult();
            DataSet ds = dal.GetAllList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (itemNos.Contains(dt.Rows[i]["SickTypeNo"].ToString().Trim()))
                    {
                        var labsicktypdal = DALFactory.DalFactory<IDAL.IDLab_SickType>.GetDal("B_Lab_SickType", ZhiFang.Common.Dictionary.DBSource.LisDB());
                        var sicktypecontroldal = DALFactory.DalFactory<IDAL.IDSickTypeControl>.GetDal("B_SickTypeControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
                        foreach (var labno in labCodeNo)
                        {
                            if (!labsicktypdal.Exists(labno, int.Parse(dt.Rows[i]["SickTypeNo"].ToString().Trim())))
                            {
                                var labsicktype = new Model.Lab_SickType();
                                labsicktype.LabCode = labno;
                                labsicktype.CName = dt.Rows[i]["CName"].ToString().Trim();
                                if (dt.Rows[i]["DispOrder"] != null&& dt.Rows[i]["DispOrder"].ToString().Trim()!="")
                                    labsicktype.DispOrder = int.Parse(dt.Rows[i]["DispOrder"].ToString().Trim());
                                else
                                    labsicktype.DispOrder = null;
                                labsicktype.LabSickTypeNo = int.Parse(dt.Rows[i]["SickTypeNo"].ToString().Trim());
                                labsicktype.AddTime = DateTime.Now;
                                labsicktype.StandCode = labsicktype.LabSickTypeNo.ToString();
                                labsicktype.ZFStandCode = labsicktype.LabSickTypeNo.ToString();
                                labsicktype.UseFlag = 1;
                                if (labsicktypdal.Add(labsicktype) > 0)
                                {
                                    ZhiFang.Common.Log.Log.Debug($"SickType.CopyToLabByItemNoList:新增就诊类型成功！LabCode={labno};SickTypeNo={dt.Rows[i]["SickTypeNo"].ToString().Trim()};CName={dt.Rows[i]["CName"].ToString().Trim()}");
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug($"SickType.CopyToLabByItemNoList:新增就诊类型失败！LabCode={labno};SickTypeNo={dt.Rows[i]["SickTypeNo"].ToString().Trim()};CName={dt.Rows[i]["CName"].ToString().Trim()}");
                                }
                            }
                            DataSet dscontrol = sicktypecontroldal.GetList(new Model.SickTypeControl() { ControlLabNo=labno,ControlSickTypeNo= int.Parse(dt.Rows[i]["SickTypeNo"].ToString().Trim()),SickTypeNo=-1});
                            if (!(dscontrol!=null && dscontrol.Tables.Count>0 && dscontrol.Tables[0].Rows.Count>0))
                            {
                                var sicktypecontrol = new Model.SickTypeControl();
                                sicktypecontrol.ControlLabNo = labno;
                                sicktypecontrol.SickTypeNo = int.Parse(dt.Rows[i]["SickTypeNo"].ToString().Trim());
                                sicktypecontrol.ControlSickTypeNo = int.Parse(dt.Rows[i]["SickTypeNo"].ToString().Trim());
                                sicktypecontrol.SickTypeControlNo = labno + "_"+ int.Parse(dt.Rows[i]["SickTypeNo"].ToString().Trim()) + "_" + int.Parse(dt.Rows[i]["SickTypeNo"].ToString().Trim());
                                sicktypecontrol.AddTime = DateTime.Now;
                                sicktypecontrol.UseFlag = 1;
                                if (sicktypecontroldal.Add(sicktypecontrol) > 0)
                                {
                                    ZhiFang.Common.Log.Log.Debug($"SickType.CopyToLabByItemNoList:新增就诊类型对照关系成功！LabCode={labno};SickTypeNo={dt.Rows[i]["SickTypeNo"].ToString().Trim()};CName={dt.Rows[i]["CName"].ToString().Trim()}");
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug($"SickType.CopyToLabByItemNoList:新增就诊类型对照关系失败！LabCode={labno};SickTypeNo={dt.Rows[i]["SickTypeNo"].ToString().Trim()};CName={dt.Rows[i]["CName"].ToString().Trim()}");
                                }
                            }
                        }
                    }
                }

            }
            return br;
        }
    }
}
