using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.Model;
using ZhiFang.Model.DownloadDict;
using ZhiFang.Model.UiModel;

namespace ZhiFang.BLL.Common.BaseDictionary
{
    public partial class DownloadDictHelp : IBDownloadDictHelp
    {
        public DownloadDictHelp()
        { }

        IDAL.IDLab_SampleType dalSampleType = DALFactory.DalFactory<IDAL.IDLab_SampleType>.GetDal("B_Lab_SampleType", ZhiFang.Common.Dictionary.DBSource.LisDB());
        IDAL.IDLab_TestItem dalLabTestItem = DALFactory.DalFactory<IDAL.IDLab_TestItem>.GetDal("B_Lab_TestItem", ZhiFang.Common.Dictionary.DBSource.LisDB());

        IBLL.Common.BaseDictionary.IBItemColorDict IBColorDict = BLLFactory<IBItemColorDict>.GetBLL();
        IBLL.Common.BaseDictionary.IBBPhysicalExamType IBExamType = BLLFactory<IBBPhysicalExamType>.GetBLL();

        IBLL.Common.BaseDictionary.IBGroupItem IBGroupItem = BLLFactory<IBGroupItem>.GetBLL();
        IBLL.Common.BaseDictionary.IBGroupItem IBCenterGroupItem = BLLFactory<IBGroupItem>.GetBLL();
        IBLL.Common.BaseDictionary.IBItemColorAndSampleTypeDetail IBItemColorAndSampleTypeDetail = BLLFactory<IBItemColorAndSampleTypeDetail>.GetBLL();

        IBLL.Common.BaseDictionary.IBLab_GroupItem IBLabGroupItem = BLLFactory<IBLab_GroupItem>.GetBLL();
        IBLL.Common.BaseDictionary.IBLab_SickType IBLabSickType = BLLFactory<IBLab_SickType>.GetBLL();
        IBLL.Common.BaseDictionary.IBLab_Doctor IBLabDoctor = BLLFactory<IBLab_Doctor>.GetBLL();

        IBLL.Common.BaseDictionary.IBLab_FolkType IBLabFolkType = BLLFactory<IBLab_FolkType>.GetBLL();
        IBLL.Common.BaseDictionary.IBClientEleArea IBClientEleArea = BLLFactory<IBClientEleArea>.GetBLL();
        IBLL.Common.BaseDictionary.IBCLIENTELE IBCLIENTELE = BLLFactory<IBCLIENTELE>.GetBLL();
        IBLL.Common.BaseDictionary.IBDepartment IBDepartment = BLLFactory<IBDepartment>.GetBLL();

        #region 单个调用
        public string GetClientNo(string labcode)
        {
            if (string.IsNullOrEmpty(labcode)) return "";
            string clientNo = "";
            ZhiFang.Model.CLIENTELE client = IBCLIENTELE.GetModel(long.Parse(labcode));
            if (client != null && client.AreaID.HasValue)
            {
                ZhiFang.Model.ClientEleArea clientEleArea = IBClientEleArea.GetModel(client.AreaID.Value);
                clientNo = clientEleArea.ClientNo.Value.ToString();
            }
            return clientNo;
        }
        public DownloadDictEntity<D_Department> DownloadDepartment(string maxDataTimeStamp)
        {
            DownloadDictEntity<D_Department> entityList = new DownloadDictEntity<D_Department>();
            string searchStr = "";
            if (!string.IsNullOrEmpty(maxDataTimeStamp))
            {
                searchStr += " DTimeStampe>" + maxDataTimeStamp;
            }
            DataSet ds = IBDepartment.GetList(-1, searchStr, "DTimeStampe DESC");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList.DictEntity = DataTableToListOfDepartment(ds.Tables[0]);
                if (entityList.DictEntity != null) entityList.Total = entityList.DictEntity.Count;
            }
            entityList.MaxDataTimeStamp = getMaxDataTimeStamp(ds.Tables[0]);
            return entityList;
        }
        public DownloadDictEntity<Model.GroupItem> DownloadGroupItem(string maxDataTimeStamp)
        {
            DownloadDictEntity<Model.GroupItem> entityList = new DownloadDictEntity<Model.GroupItem>();
            string searchStr = "";
            if (!string.IsNullOrEmpty(maxDataTimeStamp))
            {
                searchStr += " DataTimeStamp>" + maxDataTimeStamp;
            }
            DataSet ds = IBGroupItem.GetList(-1, searchStr, "DataTimeStamp DESC");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList.DictEntity = DataTableToListOfGroupItem(ds.Tables[0]);
                if (entityList.DictEntity != null) entityList.Total = entityList.DictEntity.Count;
            }
            entityList.MaxDataTimeStamp = getMaxDataTimeStamp(ds.Tables[0]);
            return entityList;
        }
        public DownloadDictEntity<D_ItemColorDict> DownloadItemColorDict(string maxDataTimeStamp)
        {
            DownloadDictEntity<D_ItemColorDict> entityList = new DownloadDictEntity<D_ItemColorDict>();
            string searchStr = "";
            if (!string.IsNullOrEmpty(maxDataTimeStamp))
            {
                searchStr += " DataTimeStamp>" + maxDataTimeStamp;
            }
            DataSet ds = IBColorDict.GetList(-1, searchStr, "DataTimeStamp DESC");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList.DictEntity = DataTableToListOfItemColorDict(ds.Tables[0]);
                if (entityList.DictEntity != null) entityList.Total = entityList.DictEntity.Count;
            }
            entityList.MaxDataTimeStamp = getMaxDataTimeStamp(ds.Tables[0]);
            return entityList;
        }
        public DownloadDictEntity<D_ItemColorAndSampleTypeDetail> DownloadItemColorAndSampleTypeDetail(string maxDataTimeStamp)
        {
            DownloadDictEntity<D_ItemColorAndSampleTypeDetail> entityList = new DownloadDictEntity<D_ItemColorAndSampleTypeDetail>();
            string strWhere = "";
            string searchStr = "";

            searchStr = strWhere;
            if (!string.IsNullOrEmpty(maxDataTimeStamp))
            {
                searchStr = " ItemColorAndSampleTypeDetail.DataTimeStamp>" + maxDataTimeStamp;
            }
            DataSet ds = IBItemColorAndSampleTypeDetail.DownloadItemColorAndSampleTypeDetail(-1, searchStr, "DataTimeStamp DESC");//
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList.DictEntity = DataTableToListOfItemColorAndSampleTypeDetail(ds.Tables[0]);
                if (entityList.DictEntity != null) entityList.Total = entityList.DictEntity.Count;
            }
            entityList.MaxDataTimeStamp = getMaxDataTimeStamp(ds.Tables[0]);
            return entityList;
        }

        public DownloadDictEntity<D_BPhysicalExamType> DownloadBPhysicalExamType(string maxDataTimeStamp)
        {
            DownloadDictEntity<D_BPhysicalExamType> entityList = new DownloadDictEntity<D_BPhysicalExamType>();
            string searchStr = "Visible=1";
            if (!string.IsNullOrEmpty(maxDataTimeStamp))
            {
                searchStr += " DTimeStampe>" + maxDataTimeStamp;
            }
            DataSet ds = IBExamType.GetList(-1, searchStr, "DTimeStampe DESC");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList.DictEntity = DataTableToListOfBPhysicalExamType(ds.Tables[0]);
                if (entityList.DictEntity != null) entityList.Total = entityList.DictEntity.Count;
            }
            entityList.MaxDataTimeStamp = getMaxDataTimeStamp(ds.Tables[0]);
            return entityList;
        }
        private DataSet GetLabSampleTypeDataSet(string labcode, string maxDataTimeStamp)
        {
            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(labcode)) return ds;
            string strWhere = "UseFlag=1 and LabCode='" + labcode + "'";
            string searchStr = "";

            searchStr = strWhere;
            if (!string.IsNullOrEmpty(maxDataTimeStamp))
            {
                searchStr += "and DTimeStampe>" + maxDataTimeStamp;
            }
            ds = dalSampleType.GetList(-1, searchStr, "DTimeStampe DESC");
            return ds;
        }
        public DownloadDictEntity<D_Lab_SampleType> DownloadLabSampleTypeByLabCode(string labcode, string maxDataTimeStamp)
        {
            DownloadDictEntity<D_Lab_SampleType> entityList = new DownloadDictEntity<D_Lab_SampleType>();
            if (string.IsNullOrEmpty(labcode)) return entityList;
            DataSet ds = GetLabSampleTypeDataSet(labcode, maxDataTimeStamp);
            entityList.AreaNo = labcode;
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                string strWhere = "UseFlag=1 and LabCode='" + labcode + "'";
                DataSet dsTemp = dalSampleType.GetList(-1, strWhere, "DTimeStampe DESC");
                //按区域设置查询
                string clientNo = "";
                if (dsTemp == null || dsTemp.Tables[0].Rows.Count <= 0)
                {
                    clientNo = this.GetClientNo(labcode);
                }
                if (!string.IsNullOrEmpty(clientNo))
                {
                    entityList.AreaNo = clientNo;
                    ds = GetLabSampleTypeDataSet(clientNo, maxDataTimeStamp);
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList.DictEntity = DataTableToListOfSampleType(ds.Tables[0]);
                if (entityList.DictEntity != null) entityList.Total = entityList.DictEntity.Count;
            }
            entityList.MaxDataTimeStamp = getMaxDataTimeStamp(ds.Tables[0]);
            return entityList;
        }
        private DataSet GetLabTestItemDataSet(string labcode, string maxDataTimeStamp)
        {
            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(labcode)) return ds;
            //Visible=1 and 
            string strWhere = "UseFlag=1 and LabCode='" + labcode + "'";
            string searchStr = "";

            searchStr = strWhere;
            if (!string.IsNullOrEmpty(maxDataTimeStamp))
            {
                searchStr += "and DTimeStampe>" + maxDataTimeStamp;
            }
            ds = dalLabTestItem.GetList(-1, searchStr, "DTimeStampe DESC");
            return ds;
        }
        public DownloadDictEntity<D_Lab_TestItem> DownloadLabTestItemByLabCode(string labcode, string maxDataTimeStamp)
        {
            DownloadDictEntity<D_Lab_TestItem> entityList = new DownloadDictEntity<D_Lab_TestItem>();
            if (string.IsNullOrEmpty(labcode)) return entityList;
            DataSet ds = GetLabTestItemDataSet(labcode, maxDataTimeStamp);
            entityList.AreaNo = labcode;
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                string strWhere = "UseFlag=1 and LabCode='" + labcode + "'";
                DataSet dsTemp = dalLabTestItem.GetList(-1, strWhere, "DTimeStampe DESC");
                //按区域设置的项目查询
                string clientNo = "";
                if (dsTemp == null || dsTemp.Tables[0].Rows.Count <= 0)
                {
                    clientNo = this.GetClientNo(labcode);
                }
                if (!string.IsNullOrEmpty(clientNo))
                {
                    entityList.AreaNo = clientNo;
                    ds = GetLabTestItemDataSet(clientNo, maxDataTimeStamp);
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList.DictEntity = DataTableToListOfLabTestItem(ds.Tables[0]);
                if (entityList.DictEntity != null) entityList.Total = entityList.DictEntity.Count;
            }
            entityList.MaxDataTimeStamp = getMaxDataTimeStamp(ds.Tables[0]);
            return entityList;
        }
        private DataSet GetBLabGroupItemDataSet(string labcode, string maxDataTimeStamp)
        {
            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(labcode)) return ds;
            string strWhere = "LabCode='" + labcode + "'";
            string searchStr = "";

            searchStr = strWhere;
            if (!string.IsNullOrEmpty(maxDataTimeStamp))
            {
                searchStr += "and DTimeStampe>" + maxDataTimeStamp;
            }
            ds = IBLabGroupItem.GetList(-1, searchStr, "DTimeStampe DESC");
            return ds;
        }
        public DownloadDictEntity<Model.Lab_GroupItem> DownloadBLabGroupItemByLabCode(string labcode, string maxDataTimeStamp)
        {
            DownloadDictEntity<Model.Lab_GroupItem> entityList = new DownloadDictEntity<Model.Lab_GroupItem>();
            if (string.IsNullOrEmpty(labcode)) return entityList;
            DataSet ds = GetBLabGroupItemDataSet(labcode, maxDataTimeStamp);
            entityList.AreaNo = labcode;
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Error("BLabGroupItem按区域设置查询开始!");
                string strWhere = "LabCode='" + labcode + "'";
                DataSet dsTemp = IBLabGroupItem.GetList(-1, strWhere, "DTimeStampe DESC");
                //按区域设置查询
                string clientNo = "";
                if (dsTemp == null || dsTemp.Tables[0].Rows.Count <= 0)
                {
                    clientNo = this.GetClientNo(labcode);
                }
                if (!string.IsNullOrEmpty(clientNo))
                {
                    entityList.AreaNo = clientNo;
                    ZhiFang.Common.Log.Log.Error("BLabGroupItem按区域设置查询,clientNo=" + clientNo);
                    ds = GetBLabGroupItemDataSet(clientNo, maxDataTimeStamp);
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList.DictEntity = DataTableToListOfBLabGroupItem(ds.Tables[0], labcode);
                if (entityList.DictEntity != null) entityList.Total = entityList.DictEntity.Count;
            }
            entityList.MaxDataTimeStamp = getMaxDataTimeStamp(ds.Tables[0]);
            return entityList;
        }
        private DataSet GetBLabSickTypeDataSet(string labcode, string maxDataTimeStamp)
        {
            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(labcode)) return ds;
            string strWhere = "LabCode='" + labcode + "'";
            string searchStr = "";

            searchStr = strWhere;
            if (!string.IsNullOrEmpty(maxDataTimeStamp))
            {
                searchStr += "and DTimeStampe>" + maxDataTimeStamp;
            }
            ds = IBLabSickType.GetList(-1, searchStr, "DTimeStampe DESC");
            return ds;
        }
        public DownloadDictEntity<D_BLabSickType> DownloadBLabSickTypeByLabCode(string labcode, string maxDataTimeStamp)
        {
            DownloadDictEntity<D_BLabSickType> entityList = new DownloadDictEntity<D_BLabSickType>();
            if (string.IsNullOrEmpty(labcode)) return entityList;
            DataSet ds = GetBLabSickTypeDataSet(labcode, maxDataTimeStamp);
            entityList.AreaNo = labcode;
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                string strWhere = "LabCode='" + labcode + "'";
                DataSet dsTemp = IBLabSickType.GetList(-1, strWhere, "DTimeStampe DESC");
                //按区域设置查询
                string clientNo = "";
                if (dsTemp == null || dsTemp.Tables[0].Rows.Count <= 0)
                {
                    clientNo = this.GetClientNo(labcode);
                }
                if (!string.IsNullOrEmpty(clientNo))
                {
                    entityList.AreaNo = clientNo;
                    ds = GetBLabSickTypeDataSet(clientNo, maxDataTimeStamp);
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList.DictEntity = DataTableToListOfBLabSickType(ds.Tables[0]);
                if (entityList.DictEntity != null) entityList.Total = entityList.DictEntity.Count;
            }
            entityList.MaxDataTimeStamp = getMaxDataTimeStamp(ds.Tables[0]);
            return entityList;
        }
        private DataSet GetBLabDoctorDataSet(string labcode, string maxDataTimeStamp)
        {
            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(labcode)) return ds;
            string strWhere = "LabCode='" + labcode + "'";
            string searchStr = "";

            searchStr = strWhere;
            if (!string.IsNullOrEmpty(maxDataTimeStamp))
            {
                searchStr += "and DTimeStampe>" + maxDataTimeStamp;
            }
            ds = IBLabDoctor.GetList(-1, searchStr, "DTimeStampe DESC");
            return ds;
        }
        public DownloadDictEntity<D_BLabDoctor> DownloadBLabDoctorByLabCode(string labcode, string maxDataTimeStamp)
        {
            DownloadDictEntity<D_BLabDoctor> entityList = new DownloadDictEntity<D_BLabDoctor>();
            if (string.IsNullOrEmpty(labcode)) return entityList;
            DataSet ds = GetBLabDoctorDataSet(labcode, maxDataTimeStamp);
            entityList.AreaNo = labcode;
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                string strWhere = "LabCode='" + labcode + "'";
                DataSet dsTemp = IBLabDoctor.GetList(-1, strWhere, "DTimeStampe DESC");
                //按区域设置查询
                string clientNo = "";
                if (dsTemp == null || dsTemp.Tables[0].Rows.Count <= 0)
                {
                    clientNo = this.GetClientNo(labcode);
                }
                if (!string.IsNullOrEmpty(clientNo))
                {
                    entityList.AreaNo = clientNo;
                    ds = GetBLabDoctorDataSet(clientNo, maxDataTimeStamp);
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList.DictEntity = DataTableToListOfBLabDoctor(ds.Tables[0]);
                if (entityList.DictEntity != null) entityList.Total = entityList.DictEntity.Count;
            }
            entityList.MaxDataTimeStamp = getMaxDataTimeStamp(ds.Tables[0]);
            return entityList;
        }
        private DataSet GetBLabFolkTypeDataSet(string labcode, string maxDataTimeStamp)
        {
            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(labcode)) return ds;
            string strWhere = "LabCode='" + labcode + "'";
            string searchStr = "";

            searchStr = strWhere;
            if (!string.IsNullOrEmpty(maxDataTimeStamp))
            {
                searchStr += "and DTimeStampe>" + maxDataTimeStamp;
            }
            ds = IBLabFolkType.GetList(-1, searchStr, "DTimeStampe DESC");
            return ds;
        }
        public DownloadDictEntity<D_BLabFolkType> DownloadBLabFolkTypeByLabCode(string labcode, string maxDataTimeStamp)
        {
            DownloadDictEntity<D_BLabFolkType> entityList = new DownloadDictEntity<D_BLabFolkType>();
            if (string.IsNullOrEmpty(labcode)) return entityList;
            DataSet ds = GetBLabFolkTypeDataSet(labcode, maxDataTimeStamp);
            entityList.AreaNo = labcode;
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                string strWhere = "LabCode='" + labcode + "'";
                DataSet dsTemp = IBLabFolkType.GetList(-1, strWhere, "DTimeStampe DESC");
                //按区域设置查询
                string clientNo = "";
                if (dsTemp == null || dsTemp.Tables[0].Rows.Count <= 0)
                {
                    clientNo = this.GetClientNo(labcode);
                }
                if (!string.IsNullOrEmpty(clientNo))
                {
                    entityList.AreaNo = clientNo;
                    ds = GetBLabFolkTypeDataSet(clientNo, maxDataTimeStamp);
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                entityList.DictEntity = DataTableToListOfBLabFolkType(ds.Tables[0]);
                if (entityList.DictEntity != null) entityList.Total = entityList.DictEntity.Count;
            }
            entityList.MaxDataTimeStamp = getMaxDataTimeStamp(ds.Tables[0]);
            return entityList;
        }
        private string getMaxDataTimeStamp(DataTable dt)
        {
            string maxDataTimeStampStr = "";
            if (dt.Columns.Contains("DataTimeStamp") && dt.Rows.Count > 0 && dt.Rows[0]["DataTimeStamp"].ToString() != "")
            {
                System.Byte[] bytes = dt.Rows[0]["DataTimeStamp"] as System.Byte[];
                maxDataTimeStampStr = "0x" + BitConverter.ToString((bytes)).Replace("-", "");
            }
            else if (dt.Columns.Contains("DTimeStampe") && dt.Rows.Count > 0 && dt.Rows[0]["DTimeStampe"].ToString() != "")
            {
                System.Byte[] bytes = dt.Rows[0]["DTimeStampe"] as System.Byte[];
                maxDataTimeStampStr = "0x" + BitConverter.ToString((bytes)).Replace("-", "");
            }
            return maxDataTimeStampStr;
        }
        #endregion

        #region DataTableToList
        public List<D_Department> DataTableToListOfDepartment(DataTable dt)
        {
            List<D_Department> modelList = new List<D_Department>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                D_Department model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new D_Department();
                    //model.MaxDataTimeStamp = maxDataTimeStamp;
                    if (dt.Columns.Contains("DeptNo") && dt.Rows[n]["DeptNo"].ToString() != "")
                    {
                        model.DeptNo = int.Parse(dt.Rows[n]["DeptNo"].ToString());
                    }
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    if (dt.Columns.Contains("Visible") && dt.Rows[n]["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
                    }
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.ShortName = dt.Rows[n]["ShortName"].ToString();
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    model.HisOrderCode = dt.Rows[n]["HisOrderCode"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public List<D_ItemColorDict> DataTableToListOfItemColorDict(DataTable dt)
        {
            List<D_ItemColorDict> modelList = new List<D_ItemColorDict>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                D_ItemColorDict model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new D_ItemColorDict();
                    //model.MaxDataTimeStamp = maxDataTimeStamp;
                    if (dt.Columns.Contains("ColorID") && dt.Rows[n]["ColorID"].ToString() != "")
                    {
                        model.ColorID = dt.Rows[n]["ColorID"].ToString();
                    }
                    if (dt.Columns.Contains("ColorName") && dt.Rows[n]["ColorName"].ToString() != "")
                    {
                        model.ColorName = dt.Rows[n]["ColorName"].ToString();
                    }
                    if (dt.Columns.Contains("ColorValue") && dt.Rows[n]["ColorValue"].ToString() != "")
                    {
                        model.ColorValue = dt.Rows[n]["ColorValue"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public IList<Model.GroupItem> DataTableToListOfGroupItem(DataTable dt)
        {
            IList<Model.GroupItem> modelList = new List<Model.GroupItem>();
            int rowsCount = dt.Rows.Count;
            ZhiFang.Common.Log.Log.Error("DataTableToListOfGroupItem.Count:" + rowsCount);
            if (rowsCount > 0)
            {
                Model.GroupItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.GroupItem();

                    model.Id = long.Parse(dt.Rows[n]["GroupItemID"].ToString());
                    model.ItemNo = dt.Rows[n]["ItemNo"].ToString();
                    model.PItemNo = dt.Rows[n]["PItemNo"].ToString();
                    if (dt.Rows[n]["DataAddTime"].ToString() != "")
                        model.AddTime = DateTime.Parse(dt.Rows[n]["DataAddTime"].ToString());
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        public IList<D_ItemColorAndSampleTypeDetail> DataTableToListOfItemColorAndSampleTypeDetail(DataTable dt)
        {
            IList<D_ItemColorAndSampleTypeDetail> modelList = new List<D_ItemColorAndSampleTypeDetail>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                D_ItemColorAndSampleTypeDetail model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new D_ItemColorAndSampleTypeDetail();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = dt.Rows[n]["Id"].ToString();
                    }
                    if (dt.Columns.Contains("ColorId") && dt.Rows[n]["ColorId"].ToString() != "")
                    {
                        model.ColorID = dt.Rows[n]["ColorId"].ToString();

                    }
                    if (dt.Columns.Contains("ColorValue") && dt.Rows[n]["ColorValue"].ToString() != "")
                    {
                        model.ColorValue = dt.Rows[n]["ColorValue"].ToString();
                    }
                    if (dt.Columns.Contains("ColorName") && dt.Rows[n]["ColorName"].ToString() != "")
                    {
                        model.ColorName = dt.Rows[n]["ColorName"].ToString();
                    }
                    if (dt.Columns.Contains("SampleTypeNo") && dt.Rows[n]["SampleTypeNo"].ToString() != "")
                    {
                        model.SampleTypeNo = dt.Rows[n]["SampleTypeNo"].ToString();
                    }
                    if (dt.Columns.Contains("SampleTypeName") && dt.Rows[n]["SampleTypeName"].ToString() != "")
                    {
                        model.SampleTypeName = dt.Rows[n]["SampleTypeName"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        public IList<D_Lab_SampleType> DataTableToListOfSampleType(DataTable dt)
        {
            IList<D_Lab_SampleType> modelList = new List<D_Lab_SampleType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                D_Lab_SampleType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new D_Lab_SampleType();
                    //model.MaxDataTimeStamp = maxDataTimeStamp;
                    if (dt.Rows[n]["SampleTypeID"].ToString() != "")
                    {
                        model.SampleTypeID = dt.Rows[n]["SampleTypeID"].ToString();
                    }
                    model.CName = dt.Rows[n]["CName"].ToString().Replace("\"", "”");
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    model.LabSampleTypeNo = dt.Rows[n]["LabSampleTypeNo"].ToString();
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    //model.HisOrderCode = dt.Rows[n]["HisOrderCode"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        public List<D_BPhysicalExamType> DataTableToListOfBPhysicalExamType(DataTable dt)
        {
            List<D_BPhysicalExamType> modelList = new List<D_BPhysicalExamType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                D_BPhysicalExamType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new D_BPhysicalExamType();
                    //model.MaxDataTimeStamp = maxDataTimeStamp;
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = dt.Rows[n]["Id"].ToString();
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString().Replace("\"", "”");
                    }
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        public IList<D_Lab_TestItem> DataTableToListOfLabTestItem(DataTable dt)
        {
            IList<D_Lab_TestItem> modelList = new List<D_Lab_TestItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                D_Lab_TestItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new D_Lab_TestItem();
                    // model.MaxDataTimeStamp = maxDataTimeStamp;

                    if (dt.Columns.Contains("ItemID") && dt.Rows[n]["ItemID"].ToString() != "")
                    {
                        model.ItemID = dt.Rows[n]["ItemID"].ToString();
                    }
                    if (dt.Columns.Contains("Color") && dt.Rows[n]["Color"].ToString() != "")
                    {
                        model.Color = dt.Rows[n]["Color"].ToString();
                    }
                    model.ItemNo = dt.Rows[n]["ItemNo"].ToString();
                    model.CName = dt.Rows[n]["CName"].ToString().Replace("\"", "”");
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString().Replace("\"", "");
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    if (dt.Columns.Contains("isCombiItem") && dt.Rows[n]["isCombiItem"].ToString() != "")
                    {
                        model.isCombiItem = int.Parse(dt.Rows[n]["isCombiItem"].ToString());
                    }
                    if (dt.Columns.Contains("IsProfile") && dt.Rows[n]["IsProfile"].ToString() != "")
                    {
                        model.IsProfile = int.Parse(dt.Rows[n]["IsProfile"].ToString());
                    }
                    if (dt.Columns.Contains("PhysicalFlag") && dt.Rows[n]["PhysicalFlag"].ToString() != "")
                    {
                        model.PhysicalFlag = int.Parse(dt.Rows[n]["PhysicalFlag"].ToString());
                        //if (model.PhysicalFlag == 1)
                        //ZhiFang.Common.Log.Log.Info("PhysicalFlag:" + model.PhysicalFlag);
                    }
                    else
                    {
                        model.PhysicalFlag = 0;
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        public IList<Model.Lab_GroupItem> DataTableToListOfBLabGroupItem(DataTable dt, string labcode)
        {
            IList<Model.Lab_GroupItem> modelList = new List<Model.Lab_GroupItem>();
            int rowsCount = dt.Rows.Count;
            ZhiFang.Common.Log.Log.Error("DataTableToListOfBLabGroupItem.Count:" + rowsCount);
            if (rowsCount > 0)
            {
                Model.Lab_GroupItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.Lab_GroupItem();

                    model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    model.ItemNo = dt.Rows[n]["ItemNo"].ToString();
                    model.PItemNo = dt.Rows[n]["PItemNo"].ToString();
                    model.LabCode = dt.Rows[n]["LabCode"].ToString();
                    if (dt.Columns.Contains("LabCode") && dt.Rows[n]["LabCode"].ToString() != labcode)
                    {
                        model.LabCode = labcode;
                    }
                    if (dt.Rows[n]["AddTime"].ToString() != "")
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    modelList.Add(model);
                    //GetSubLabItem(dt.Rows[n]["ItemNo"].ToString(), labcode, ref listtestitem);
                }
            }
            return modelList;
        }
        public IList<D_BLabSickType> DataTableToListOfBLabSickType(DataTable dt)
        {
            IList<D_BLabSickType> modelList = new List<D_BLabSickType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                D_BLabSickType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new D_BLabSickType();

                    model.SickTypeID = dt.Rows[n]["SickTypeID"].ToString();
                    model.LabSickTypeNo = dt.Rows[n]["LabSickTypeNo"].ToString();
                    model.CName = dt.Rows[n]["CName"].ToString().Replace("\"", "”");
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString().Replace("\"", "");
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        public IList<D_BLabDoctor> DataTableToListOfBLabDoctor(DataTable dt)
        {
            IList<D_BLabDoctor> modelList = new List<D_BLabDoctor>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                D_BLabDoctor model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new D_BLabDoctor();
                    if (dt.Columns.Contains("DoctorID") && dt.Rows[n]["DoctorID"].ToString() != "")
                    {
                        model.DoctorID = dt.Rows[n]["DoctorID"].ToString();
                    }
                    if (dt.Columns.Contains("LabDoctorNo") && dt.Rows[n]["LabDoctorNo"].ToString() != "")
                    {
                        model.LabDoctorNo = dt.Rows[n]["LabDoctorNo"].ToString();
                    }
                    model.CName = dt.Rows[n]["CName"].ToString().Replace("\"", "”");
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString().Replace("\"", "");
                    }
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public IList<D_BLabFolkType> DataTableToListOfBLabFolkType(DataTable dt)
        {
            IList<D_BLabFolkType> modelList = new List<D_BLabFolkType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                D_BLabFolkType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new D_BLabFolkType();
                    if (dt.Columns.Contains("FolkID") && dt.Rows[n]["FolkID"].ToString() != "")
                    {
                        model.FolkID = dt.Rows[n]["FolkID"].ToString();
                    }
                    if (dt.Columns.Contains("LabFolkNo") && dt.Rows[n]["LabFolkNo"].ToString() != "")
                    {
                        model.LabFolkNo = dt.Rows[n]["LabFolkNo"].ToString();
                    }
                    model.CName = dt.Rows[n]["CName"].ToString().Replace("\"", "”");
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString().Replace("\"", "");
                    }
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        #endregion
    }
}
