using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.Model;
using ZhiFang.Model.DownloadDict;

namespace ZhiFang.BLL.Common.BaseDictionary
{
    public partial class DDictionaryInfo : IBDDictionaryInfo
    {
        IDAL.IDLab_SampleType dalSampleType = DALFactory.DalFactory<IDAL.IDLab_SampleType>.GetDal("B_Lab_SampleType", ZhiFang.Common.Dictionary.DBSource.LisDB());
        IDAL.IDLab_TestItem dalLabTestItem = DALFactory.DalFactory<IDAL.IDLab_TestItem>.GetDal("B_Lab_TestItem", ZhiFang.Common.Dictionary.DBSource.LisDB());

        IBLL.Common.BaseDictionary.IBItemColorDict IBColorDict = BLLFactory<IBItemColorDict>.GetBLL();
        IBLL.Common.BaseDictionary.IBBPhysicalExamType IBExamType = BLLFactory<IBBPhysicalExamType>.GetBLL();
        public D_DictionaryInfo GetAllListByLabCode(string labcode)
        {
            D_DictionaryInfo dictInfo = new D_DictionaryInfo();
            if (string.IsNullOrEmpty(labcode)) return dictInfo;

            DataSet dsColor = IBColorDict.GetAllList();
            if (dsColor != null && dsColor.Tables[0].Rows.Count > 0)
            {
                dictInfo.ItemColorDict = IBColorDict.DataTableToList(dsColor.Tables[0]);
            }
            else
            {
                dictInfo.ItemColorDict = new List<Model.ItemColorDict>();
            }

            DataSet dsExamType = IBExamType.GetAllList();
            if (dsExamType != null && dsExamType.Tables[0].Rows.Count > 0)
            {
                dictInfo.BPhysicalExamType = DataTableToListOfBPhysicalExamType(dsExamType.Tables[0]);
            }
            else
            {
                dictInfo.BPhysicalExamType = new List<D_BPhysicalExamType>();
            }

            DataSet dsTestItem = dalLabTestItem.GetList(new Model.Lab_TestItem { LabCode = labcode });
            if (dsTestItem != null && dsTestItem.Tables[0].Rows.Count > 0)
            {
                dictInfo.LabTestItem = DataTableToListOfTestItem(dsTestItem.Tables[0]);
            }
            else { dictInfo.LabTestItem = new List<D_Lab_TestItem>(); }

            DataSet dsSampleType = dalSampleType.GetList(new Model.Lab_SampleType { LabCode = labcode });
            if (dsSampleType != null && dsSampleType.Tables[0].Rows.Count > 0)
            {
                dictInfo.LabSampleType = DataTableToListOfSampleType(dsSampleType.Tables[0]);
            }
            else { dictInfo.LabSampleType = new List<D_Lab_SampleType>(); }
            return dictInfo;
        }
        public List<ZhiFang.Model.ItemColorDict> DataTableToLisOfItemColorDictt(DataTable dt)
        {
            List<ZhiFang.Model.ItemColorDict> modelList = new List<ZhiFang.Model.ItemColorDict>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.ItemColorDict model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.ItemColorDict();
                    if (dt.Columns.Contains("ColorID") && dt.Rows[n]["ColorID"].ToString() != "")
                    {
                        model.ColorID = int.Parse(dt.Rows[n]["ColorID"].ToString());
                    }
                    if (dt.Columns.Contains("ColorName") && dt.Rows[n]["ColorName"].ToString() != "")
                    {
                        model.ColorName = dt.Rows[n]["ColorName"].ToString();
                    }
                    if (dt.Columns.Contains("ColorValue") && dt.Rows[n]["ColorValue"].ToString() != "")
                    {
                        model.ColorValue = dt.Rows[n]["ColorValue"].ToString();
                    }
                    //model.DispOrder = (n+1);
                    //if (dt.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
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
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = dt.Rows[n]["Id"].ToString();
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
                    if (dt.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    {
                        System.Byte[] tmpdts = dt.Rows[n]["DTimeStampe"] as System.Byte[];
                        model.DTimeStampe = tmpdts;
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        public IList<D_Lab_TestItem> DataTableToListOfTestItem(DataTable dt)
        {
            IList<D_Lab_TestItem> modelList = new List<D_Lab_TestItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                D_Lab_TestItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new D_Lab_TestItem();
                    if (dt.Rows[n]["ItemID"].ToString() != "")
                    {
                        model.Id = dt.Rows[n]["ItemID"].ToString();
                    }
                    model.ItemNo = dt.Rows[n]["ItemNo"].ToString();
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    if (dt.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    {
                        System.Byte[] tmpdts = dt.Rows[n]["DTimeStampe"] as System.Byte[];
                        model.DTimeStampe = tmpdts;
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
                    if (dt.Rows[n]["SampleTypeID"].ToString() != "")
                    {
                        model.Id = dt.Rows[n]["SampleTypeID"].ToString();
                    }
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    model.HisOrderCode = dt.Rows[n]["HisOrderCode"].ToString();
                    if (dt.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    {
                        System.Byte[] bytes = dt.Rows[n]["DTimeStampe"] as System.Byte[];
                        model.DTimeStampe = bytes;

                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
    }
}
