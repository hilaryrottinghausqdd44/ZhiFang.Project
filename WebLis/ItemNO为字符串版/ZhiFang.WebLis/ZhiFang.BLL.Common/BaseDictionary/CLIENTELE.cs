using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //CLIENTELE		
    public partial class CLIENTELE : IBSynchData, IBCLIENTELE, IBBatchCopy, IBDataDownload
    {
        IDAL.IDCLIENTELE dal;
        IDAL.IDBatchCopy dalCopy;

        public CLIENTELE()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDCLIENTELE>.GetDal("CLIENTELE", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("CLIENTELE", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDCLIENTELE>.GetDal("B_CLIENTELE", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_CLIENTELE", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ClIENTNO)
        {
            return dal.Exists(ClIENTNO);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.CLIENTELE model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.CLIENTELE model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(long ClIENTNO)
        {
            return dal.Delete(ClIENTNO);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string CLIENTIDlist)
        {
            return dal.DeleteList(CLIENTIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.CLIENTELE GetModel(long ClIENTNO)
        {
            return dal.GetModel(ClIENTNO);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.CLIENTELE GetModelByCache(int ClIENTNO)
        {

            string CacheKey = "B_CLIENTELEModel-" + ClIENTNO;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ClIENTNO);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.CLIENTELE)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.CLIENTELE> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.CLIENTELE> modelList = new List<ZhiFang.Model.CLIENTELE>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.CLIENTELE model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.CLIENTELE();
                    if (dt.Columns.Contains("CLIENTID") && dt.Rows[n]["CLIENTID"].ToString() != "")
                    {
                        model.CLIENTID = int.Parse(dt.Rows[n]["CLIENTID"].ToString());
                    }
                    if (dt.Columns.Contains("ClIENTNO") && dt.Rows[n]["ClIENTNO"].ToString() != "")
                    {
                        model.ClIENTNO = dt.Rows[n]["ClIENTNO"].ToString();
                    }
                    if (dt.Columns.Contains("CNAME") && dt.Rows[n]["CNAME"].ToString() != "")
                    {
                        model.CNAME = dt.Rows[n]["CNAME"].ToString();
                    }
                    if (dt.Columns.Contains("ENAME") && dt.Rows[n]["ENAME"].ToString() != "")
                    {
                        model.ENAME = dt.Rows[n]["ENAME"].ToString();
                    }
                    if (dt.Columns.Contains("SHORTCODE") && dt.Rows[n]["SHORTCODE"].ToString() != "")
                    {
                        model.SHORTCODE = dt.Rows[n]["SHORTCODE"].ToString();
                    }
                    if (dt.Columns.Contains("ISUSE") && dt.Rows[n]["ISUSE"].ToString() != "")
                    {
                        model.ISUSE = int.Parse(dt.Rows[n]["ISUSE"].ToString());
                    }
                    if (dt.Columns.Contains("LINKMAN") && dt.Rows[n]["LINKMAN"].ToString() != "")
                    {
                        model.LINKMAN = dt.Rows[n]["LINKMAN"].ToString();
                    }
                    if (dt.Columns.Contains("PHONENUM1") && dt.Rows[n]["PHONENUM1"].ToString() != "")
                    {
                        model.PHONENUM1 = dt.Rows[n]["PHONENUM1"].ToString();
                    }
                    if (dt.Columns.Contains("ADDRESS") && dt.Rows[n]["ADDRESS"].ToString() != "")
                    {
                        model.ADDRESS = dt.Rows[n]["ADDRESS"].ToString();
                    }
                    if (dt.Columns.Contains("MAILNO") && dt.Rows[n]["MAILNO"].ToString() != "")
                    {
                        model.MAILNO = dt.Rows[n]["MAILNO"].ToString();
                    }
                    if (dt.Columns.Contains("EMAIL") && dt.Rows[n]["EMAIL"].ToString() != "")
                    {
                        model.EMAIL = dt.Rows[n]["EMAIL"].ToString();
                    }
                    if (dt.Columns.Contains("PRINCIPAL") && dt.Rows[n]["PRINCIPAL"].ToString() != "")
                    {
                        model.PRINCIPAL = dt.Rows[n]["PRINCIPAL"].ToString();
                    }
                    if (dt.Columns.Contains("PHONENUM2") && dt.Rows[n]["PHONENUM2"].ToString() != "")
                    {
                        model.PHONENUM2 = dt.Rows[n]["PHONENUM2"].ToString();
                    }
                    if (dt.Columns.Contains("CLIENTTYPE") && dt.Rows[n]["CLIENTTYPE"].ToString() != "")
                    {
                        model.CLIENTTYPE = int.Parse(dt.Rows[n]["CLIENTTYPE"].ToString());
                    }
                    if (dt.Columns.Contains("bmanno") && dt.Rows[n]["bmanno"].ToString() != "")
                    {
                        model.bmanno = int.Parse(dt.Rows[n]["bmanno"].ToString());
                    }
                    if (dt.Columns.Contains("romark") && dt.Rows[n]["romark"].ToString() != "")
                    {
                        model.romark = dt.Rows[n]["romark"].ToString();
                    }
                    if (dt.Columns.Contains("titletype") && dt.Rows[n]["titletype"].ToString() != "")
                    {
                        model.titletype = int.Parse(dt.Rows[n]["titletype"].ToString());
                    }
                    if (dt.Columns.Contains("uploadtype") && dt.Rows[n]["uploadtype"].ToString() != "")
                    {
                        model.uploadtype = int.Parse(dt.Rows[n]["uploadtype"].ToString());
                    }
                    if (dt.Columns.Contains("printtype") && dt.Rows[n]["printtype"].ToString() != "")
                    {
                        model.printtype = int.Parse(dt.Rows[n]["printtype"].ToString());
                    }
                    if (dt.Columns.Contains("InputDataType") && dt.Rows[n]["InputDataType"].ToString() != "")
                    {
                        model.InputDataType = int.Parse(dt.Rows[n]["InputDataType"].ToString());
                    }
                    if (dt.Columns.Contains("reportpagetype") && dt.Rows[n]["reportpagetype"].ToString() != "")
                    {
                        model.reportpagetype = int.Parse(dt.Rows[n]["reportpagetype"].ToString());
                    }
                    if (dt.Columns.Contains("clientarea") && dt.Rows[n]["clientarea"].ToString() != "")
                    {
                        model.clientarea = dt.Rows[n]["clientarea"].ToString();
                    }
                    if (dt.Columns.Contains("clientstyle") && dt.Rows[n]["clientstyle"].ToString() != "")
                    {
                        model.clientstyle = dt.Rows[n]["clientstyle"].ToString();
                    }
                    if (dt.Columns.Contains("FaxNo") && dt.Rows[n]["FaxNo"].ToString() != "")
                    {
                        model.FaxNo = dt.Rows[n]["FaxNo"].ToString();
                    }
                    if (dt.Columns.Contains("AutoFax") && dt.Rows[n]["AutoFax"].ToString() != "")
                    {
                        model.AutoFax = int.Parse(dt.Rows[n]["AutoFax"].ToString());
                    }
                    if (dt.Columns.Contains("ClientReportTitle") && dt.Rows[n]["ClientReportTitle"].ToString() != "")
                    {
                        model.ClientReportTitle = dt.Rows[n]["ClientReportTitle"].ToString();
                    }
                    if (dt.Columns.Contains("IsPrintItem") && dt.Rows[n]["IsPrintItem"].ToString() != "")
                    {
                        model.IsPrintItem = int.Parse(dt.Rows[n]["IsPrintItem"].ToString());
                    }
                    if (dt.Columns.Contains("CZDY1") && dt.Rows[n]["CZDY1"].ToString() != "")
                    {
                        model.CZDY1 = dt.Rows[n]["CZDY1"].ToString();
                    }
                    if (dt.Columns.Contains("CZDY2") && dt.Rows[n]["CZDY2"].ToString() != "")
                    {
                        model.CZDY2 = dt.Rows[n]["CZDY2"].ToString();
                    }
                    if (dt.Columns.Contains("CZDY3") && dt.Rows[n]["CZDY3"].ToString() != "")
                    {
                        model.CZDY3 = dt.Rows[n]["CZDY3"].ToString();
                    }
                    if (dt.Columns.Contains("CZDY4") && dt.Rows[n]["CZDY4"].ToString() != "")
                    {
                        model.CZDY4 = dt.Rows[n]["CZDY4"].ToString();
                    }
                    if (dt.Columns.Contains("CZDY5") && dt.Rows[n]["CZDY5"].ToString() != "")
                    {
                        model.CZDY5 = dt.Rows[n]["CZDY5"].ToString();
                    }
                    if (dt.Columns.Contains("CZDY6") && dt.Rows[n]["CZDY6"].ToString() != "")
                    {
                        model.CZDY6 = dt.Rows[n]["CZDY6"].ToString();
                    }
                    if (dt.Columns.Contains("LinkManPosition") && dt.Rows[n]["LinkManPosition"].ToString() != "")
                    {
                        model.LinkManPosition = dt.Rows[n]["LinkManPosition"].ToString();
                    }
                    if (dt.Columns.Contains("LinkMan1") && dt.Rows[n]["LinkMan1"].ToString() != "")
                    {
                        model.LinkMan1 = dt.Rows[n]["LinkMan1"].ToString();
                    }
                    if (dt.Columns.Contains("LinkManPosition1") && dt.Rows[n]["LinkManPosition1"].ToString() != "")
                    {
                        model.LinkManPosition1 = dt.Rows[n]["LinkManPosition1"].ToString();
                    }
                    if (dt.Columns.Contains("clientcode") && dt.Rows[n]["clientcode"].ToString() != "")
                    {
                        model.clientcode = dt.Rows[n]["clientcode"].ToString();
                    }
                    if (dt.Columns.Contains("CWAccountDate") && dt.Rows[n]["CWAccountDate"].ToString() != "")
                    {
                        model.CWAccountDate = dt.Rows[n]["CWAccountDate"].ToString();
                    }
                    if (dt.Columns.Contains("NFClientType") && dt.Rows[n]["NFClientType"].ToString() != "")
                    {
                        model.NFClientType = dt.Rows[n]["NFClientType"].ToString();
                    }
                    if (dt.Columns.Contains("WebLisSourceOrgId") && dt.Rows[n]["WebLisSourceOrgId"].ToString() != "")
                    {
                        model.WebLisSourceOrgId = dt.Rows[n]["WebLisSourceOrgId"].ToString();
                    }
                    if (dt.Columns.Contains("AreaID"))
                    {
                        model.AreaID = (dt.Rows[n]["AreaID"].ToString() == "") ? 0 : int.Parse(dt.Rows[n]["AreaID"].ToString());
                    }
                    //if (dt.Columns.Contains("DTimeStampe") &&dt.Rows[n]["DTimeStampe"].ToString() != "")
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
        public DataSet GetList(ZhiFang.Model.CLIENTELE model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.CLIENTELE model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.CLIENTELE model, int nowPageNum, int nowPageSize)
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


        #region IBCLIENTELE 成员


        public DataSet GetList(int p, Model.CLIENTELE c)
        {
            return this.GetListByPage(c, 0, p);
        }

        #endregion

        #region IBDataDownload 成员

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("B_CLIENTELE", ZhiFang.Common.Dictionary.DBSource.LisDB());
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
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.CLIENTELE.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
                strXML = "";
                strXMLSchema = "";
                strMsg = "失败，CLIENTELE获取最新字典数据出现异常";
                return 0;
            }
        }

        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBBase<CLIENTELE> 成员


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
        public int DeleteByDataRow(DataRow dr)
        {
            return dalCopy.DeleteByDataRow(dr);
        }
        #endregion

        public DataSet GetClientNo(string CLIENTIDlist, string CName)
        {
            return dal.GetClientNo(CLIENTIDlist, CName);
        }


        public bool IsExist(string labCodeNo)
        {
            return dalCopy.IsExist(labCodeNo);
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            return dalCopy.DeleteByLabCode(LabCodeNo);
        }


        public int Add(List<Model.CLIENTELE> modelList)
        {
            return dal.Add(modelList);
        }
        public int Update(List<Model.CLIENTELE> modelList)
        {
            return dal.Update(modelList);
        }
    }
}
