using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.Common
{
    /// <summary>
    /// 小组类别
    /// </summary>
    public enum SectionType
    {
        all,
        //1.生化类
        Normal,
        //2.微生物
        Micro,
        //3.生化类（图）
        NormalIncImage,
        //4.微生物（图）
        MicroIncImage,
        //5.细胞形态学Marrow
        CellMorphology,
        //6.Fish检测（图）Marrow
        FishCheck,
        //7.院感检测（图）
        SensorCheck,
        //8.染色体检测（图）Marrow
        ChromosomeCheck,
        //9.病理检测（图）Marrow
        PathologyCheck,
        TestGroupMicroSmear = 20,
        TestGroupMicroSmearExt = 201,
        TestGroupMicroCultureAssayAntibioticSusceptibility = 21,
        TestGroupMicroCultureAssayAntibioticSusceptibilityExt = 211,
        TestGroupMicroOtherTest = 22,
    }
    public enum SectionTypeVisible
    {
        //0
        UnVisible,
        //1
        Visible,
    }
    public enum ReportFormTitle
    {
        center,
        client,
        BatchPrint,
        zhuyuan,
        menzhen,
        tijian,
        
    }
    public class SectionTypeCommon
    {
        public static string GetFormat(string SectionTypeName)
        {
            try
            {
                System.Web.UI.Page p = new System.Web.UI.Page();
                DataSet ds = new DataSet();
                ds.ReadXml(p.Server.MapPath(@"../XmlConfig/ReportFromShowXslConfig.xml"));
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
    public enum ReportFormFileType
    {
        HTML,
        JPEG,
        JPG,
        GIF,
        BMP,
        PNG,
        TIFF,
        WORD,
        EXECL,
        PDF
    }
    public class SysContractPara
    {
        public static readonly string TemplatePath =@"Template\";
        public static readonly string PrintTemplatextension=".FRX";
        public static readonly string ReportFormFilePath=@"ReportFormFiles\";
    }
}
