using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    /// <summary>
    /// ҵ���߼���PrintFormat ��ժҪ˵����
    /// </summary>
    public partial class PrintFormat : IBSynchData, IBPrintFormat, IBBatchCopy, IBDataDownload
    {
        //private readonly IDPrintFormat dal = DalFactory<IDPrintFormat>.GetDalByClassName("PrintFormat");
        IDAL.IDPrintFormat dal;
        IDAL.IDBatchCopy dalCopy;
        public PrintFormat()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDPrintFormat>.GetDal("PrintFormat", ZhiFang.Common.Dictionary.DBSource.LisDB());
                //dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("PrintFormat", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDPrintFormat>.GetDal("B_PrintFormat", ZhiFang.Common.Dictionary.DBSource.LisDB());
                //dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_PrintFormat", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }
        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.PrintFormat model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Update(Model.PrintFormat model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public int Delete(string Id)
        {

            return dal.Delete(Id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.PrintFormat GetModel(string Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
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
        /// ��������б�
        /// </summary>
        public DataSet GetList(Model.PrintFormat model)
        {
            return dal.GetList(model);
        }
        /// <summary>
        /// ��������б�
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
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("PrintFormatName") && dt.Rows[n]["PrintFormatName"].ToString() != "")
                    {
                        model.PrintFormatName = dt.Rows[n]["PrintFormatName"].ToString();
                    }
                    if (dt.Columns.Contains("PintFormatAddress") && dt.Rows[n]["PintFormatAddress"].ToString() != "")
                    {
                        model.PintFormatAddress = dt.Rows[n]["PintFormatAddress"].ToString();
                    }
                    if (dt.Columns.Contains("PintFormatFileName") && dt.Rows[n]["PintFormatFileName"].ToString() != "")
                    {
                        model.PintFormatFileName = dt.Rows[n]["PintFormatFileName"].ToString();
                    }
                    if (dt.Columns.Contains("ItemParaLineNum") && dt.Rows[n]["ItemParaLineNum"].ToString() != "")
                    {
                        model.ItemParaLineNum = int.Parse(dt.Rows[n]["ItemParaLineNum"].ToString());
                    }
                    if (dt.Columns.Contains("PaperSize") && dt.Rows[n]["PaperSize"].ToString() != "")
                    {
                        model.PaperSize = dt.Rows[n]["PaperSize"].ToString();
                    }
                    if (dt.Columns.Contains("PrintFormatDesc") && dt.Rows[n]["PrintFormatDesc"].ToString() != "")
                    {
                        model.PrintFormatDesc = dt.Rows[n]["PrintFormatDesc"].ToString();
                    }
                    if (dt.Columns.Contains("BatchPrint") && dt.Rows[n]["BatchPrint"].ToString() != "")
                    {
                        model.BatchPrint = int.Parse(dt.Rows[n]["BatchPrint"].ToString());
                    }
                    if (dt.Columns.Contains("ImageFlag") && dt.Rows[n]["ImageFlag"].ToString() != "")
                    {
                        model.ImageFlag = int.Parse(dt.Rows[n]["ImageFlag"].ToString());
                    }
                    if (dt.Columns.Contains("AntiFlag") && dt.Rows[n]["AntiFlag"].ToString() != "")
                    {
                        model.AntiFlag = int.Parse(dt.Rows[n]["AntiFlag"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Model.PrintFormat> GetModelList(Model.PrintFormat model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
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
        /// <summary>
        /// ��������б�
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  ��Ա����

        #region IBPrintFormat��Ա
        public DataSet GetList(int p, Model.PrintFormat printFormat_m)
        {
            return this.GetListByPage(printFormat_m, 0, p);
        }

        #endregion
      
        #region IBBase<PrintFormat> ��Ա


        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion

        #region IBDataDownload ��Ա

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("B_PrintFormat", ZhiFang.Common.Dictionary.DBSource.LisDB());
            try
            {
                DataSet dsAll = dalGetBytStampe.GetListByTimeStampe(LabCode.Trim(), time);
                strXMLSchema = dsAll.GetXmlSchema(); //xml�ṹ�ļ�
                strXML = dsAll.GetXml();//��������xml�ļ�
                strMsg = "ͨ�������ȡXML�ɹ�";
                return 1;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.Doctor.GetDictionaryXML---->����LabCode=" + LabCode + "��time=" + time, ex);
                strXML = "";
                strXMLSchema = "";
                strMsg = "ʧ�ܣ�Doctor��ȡ�����ֵ����ݳ����쳣";
                return 0;
            }
        }

        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBSynchData ��Ա


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

        #region IBBatchCopy ��Ա


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
    }
}

