using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;

namespace ZhiFang.BLL.Common
{
    public class SectionTypeCommon
    {        
        public static string GetFormat(string SectionTypeName)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(@"../XmlConfig/ReportFromShowXslConfig.xml"));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["ReportType"].ToString().Trim() == SectionTypeName)
                        {
                            return ds.Tables[0].Rows[i]["XSLName"].ToString().Trim();
                        }
                    }
                    return "";
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }

        }
    }
    public class Lib
    {        
        public static Dictionary<string, ColorSampleType> ItemColor()
        {
            Dictionary<string, ColorSampleType> d = new Dictionary<string, ColorSampleType>();
            d.Add("无色", new ColorSampleType() { ColorName = "无色" });
            d.Add("绿色", new ColorSampleType() { ColorName = "绿色", ColorValue = "#58B681", SampleType = new List<Model.SampleType>() { new Model.SampleType() { CName = "血浆", SampleTypeID = 6 }, new Model.SampleType() { CName = "全血", SampleTypeID = 7 } } });
            d.Add("紫色", new ColorSampleType() { ColorName = "紫色", ColorValue = "#4C2953", SampleType = new List<Model.SampleType>() { new Model.SampleType() { CName = "血浆", SampleTypeID = 6 }, new Model.SampleType() { CName = "全血", SampleTypeID = 7 } } });
            d.Add("黑色", new ColorSampleType() { ColorName = "黑色", ColorValue = "#000000", SampleType = new List<Model.SampleType>() { new Model.SampleType() { CName = "血清", SampleTypeID = 1 }, new Model.SampleType() { CName = "全血", SampleTypeID = 7 } } });
            d.Add("灰色", new ColorSampleType() { ColorName = "灰色", ColorValue = "#3E3F44", SampleType = new List<Model.SampleType>() { new Model.SampleType() { CName = "血浆", SampleTypeID = 6 } } });
            d.Add("红色", new ColorSampleType() { ColorName = "红色", ColorValue = "#CC0000", SampleType = new List<Model.SampleType>() { new Model.SampleType() { CName = "血清", SampleTypeID = 1 } } });
            d.Add("蓝色", new ColorSampleType() { ColorName = "蓝色", ColorValue = "#091853", SampleType = new List<Model.SampleType>() { new Model.SampleType() { CName = "血浆", SampleTypeID = 6 } } });
            d.Add("黄色", new ColorSampleType() { ColorName = "黄色", ColorValue = "#E29F36", SampleType = new List<Model.SampleType>() { new Model.SampleType() { CName = "血清", SampleTypeID = 1 } } });
            return d;
        }

        public static List<Model.SampleType> GetSampleTypeByColorName(string colorName)
        {
            IBSampleType samptype = BLLFactory<IBSampleType>.GetBLL();
            List<Model.SampleType> sampleTypeList = new List<Model.SampleType>();
            Model.SampleType sampleTypeModel = new Model.SampleType();
            DataSet ds = samptype.GetSampleTypeByColorName(colorName);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRowCollection drs = ds.Tables[0].Rows;
                foreach (DataRow dr in drs)
                {
                    sampleTypeModel = new Model.SampleType();
                    sampleTypeModel.CName = dr["CName"].ToString();
                    if (dr["SampleTypeNo"].ToString() != "")
                    {
                        sampleTypeModel.SampleTypeID = int.Parse(dr["SampleTypeNo"].ToString());
                    }
                    sampleTypeList.Add(sampleTypeModel);
                }

            }

            return sampleTypeList;

        }
    }
    public class ColorSampleType
    {
        public string ColorName { get; set; }
        public string ColorValue { get; set; }
        public List<ZhiFang.Model.SampleType> SampleType { get; set; }
    }
}
