using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;

namespace ZhiFang.BLL.Report
{
    //public class SectionTypeCommon
    //{
    //    public static string GetFormat(string SectionTypeName)
    //    {
    //        try
    //        {
    //            DataSet ds = new DataSet();
    //            ds.ReadXml(ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(@"../XmlConfig/ReportFromShowXslConfig.xml"));
    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {
    //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                {
    //                    if (ds.Tables[0].Rows[i]["ReportType"].ToString().Trim() == SectionTypeName)
    //                    {
    //                        return ds.Tables[0].Rows[i]["XSLName"].ToString().Trim();
    //                    }
    //                }
    //                return "";
    //            }
    //            else
    //            {
    //                return "";
    //            }
    //        }
    //        catch
    //        {
    //            return "";
    //        }

    //    }
    //}

    //public class Lib
    //{
    //    public static List<Model.SampleType> GetSampleTypeByColorName(string colorName)
    //    {
    //        IBSampleType samptype = BLLFactory<IBSampleType>.GetBLL();
    //        List<Model.SampleType> sampleTypeList = new List<Model.SampleType>();
    //        Model.SampleType sampleTypeModel = new Model.SampleType();
    //        DataSet ds = samptype.GetSampleTypeByColorName(colorName);
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            DataRowCollection drs = ds.Tables[0].Rows;
    //            foreach (DataRow dr in drs)
    //            {
    //                sampleTypeModel = new Model.SampleType();
    //                sampleTypeModel.CName = dr["CName"].ToString();
    //                if (dr["SampleTypeNo"].ToString() != "")
    //                {
    //                    sampleTypeModel.SampleTypeID = int.Parse(dr["SampleTypeNo"].ToString());
    //                }
    //                sampleTypeList.Add(sampleTypeModel);
    //            }

    //        }

    //        return sampleTypeList;

    //    }
    //}
}
