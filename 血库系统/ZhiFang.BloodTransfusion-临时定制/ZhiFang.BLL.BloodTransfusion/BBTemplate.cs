using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.BloodTransfusion.Common;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBTemplate : BaseBLL<BTemplate>, ZhiFang.IBLL.BloodTransfusion.IBBTemplate
    {
        /// <summary>
        /// 获取公共模板目录的子文件夹中的所有报表模板文件
        /// </summary>
        /// <param name="templateType">模板分类:Excel模板,Frx模板</param>
        /// <returns></returns>
        public EntityList<JObject> GetPublicTemplateDirFile(string templateType)
        {
            EntityList<JObject> jentityList = new EntityList<JObject>();
            jentityList.list = new List<JObject>();
            IList<JObject> jObjectList = new List<JObject>();
            if (string.IsNullOrEmpty(ReportBTemplateHelp.ReportBaseDir))
                ReportBTemplateHelp.ReportBaseDir = "Report";
            string path = System.AppDomain.CurrentDomain.BaseDirectory;

            path = path + ReportBTemplateHelp.ReportBaseDir + "\\" + templateType;
            string type = (templateType == FrxToPdfReportHelp.PublicTemplateDir ? FrxToPdfReportHelp.PublicTemplateDir : ExcelReportHelp.PublicTemplateDir);
            GetPublicTemplateDirFile(type, templateType, path, jObjectList);
            jentityList.list = jObjectList;
            jentityList.count = jObjectList.Count;
            return jentityList;
        }
        /// <summary>  
        /// 获取路径下所有文件以及子文件夹中文件    
        /// </summary> 
        /// <param name="templateType">报表模板类型信息</param> 
        /// <param name="curTemplateType">当前模板存放目录</param> 
        /// <param name="path">全路径根目录</param>  
        /// <param name="fileList">存放所有的FileInfo</param>  
        /// <returns></returns>  
        private IList<JObject> GetPublicTemplateDirFile(string templateType, string curTemplateType, string path, IList<JObject> fileList)
        {
            if (string.IsNullOrEmpty(path))
                return fileList;
            if (!Directory.Exists(path))
                return fileList;
            //Directory.CreateDirectory(path);

            bool isAdd = false;
            if (curTemplateType == FrxToPdfReportHelp.PublicTemplateDir || curTemplateType == ExcelReportHelp.PublicTemplateDir)
            {
                isAdd = true;
            }
            else
            {
                foreach (var dic in BTemplateType.GetStatusDic())
                {
                    if (dic.Value.Name == curTemplateType)
                    {
                        isAdd = true;
                        break;
                    }
                }
            }
            if (isAdd)
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] fil = dir.GetFiles();
                //子文件夹
                DirectoryInfo[] dii = dir.GetDirectories();
                foreach (FileInfo f in fil)
                {
                    if (f.FullName.Contains(".frx") || f.FullName.Contains(".xls"))
                    {
                        JObject jresult = new JObject();
                        string cName = f.Name.Substring(0, f.Name.LastIndexOf("."));
                        jresult.Add("BTemplateType", curTemplateType);
                        jresult.Add("CName", cName);
                        jresult.Add("FileName", f.Name);
                        jresult.Add("FullPath", f.FullName);
                        fileList.Add(jresult);
                    }
                }
                //获取子文件夹内的文件列表，递归遍历  
                foreach (DirectoryInfo subDir in dii)
                {
                    //ZhiFang.Common.Log.Log.Debug("templateType:" + subDir.Name);
                    this.GetPublicTemplateDirFile(templateType, subDir.Name, subDir.FullName, fileList);
                }
            }
            return fileList;
        }
        public EntityList<JObject> SearchBTemplateByLabIdAndType(long labId, long breportType, string templateType)
        {
            EntityList<JObject> jentityList = new EntityList<JObject>();
            jentityList.list = new List<JObject>();
            IList<JObject> jObjectList = new List<JObject>();

            templateType = templateType == FrxToPdfReportHelp.PublicTemplateDir ? FrxToPdfReportHelp.PublicTemplateDir : ExcelReportHelp.PublicTemplateDir;
            string hql = "btemplate.TypeID=" + breportType + " and btemplate.TemplateType='" + templateType + "'";
            IList<BTemplate> tempList = this.SearchListByHQL(hql);
            string btemplateTypeDir = BTemplateType.GetStatusDic()[breportType.ToString()].Name;
            if (tempList != null && tempList.Count > 0)
            {
                foreach (BTemplate btemplate in tempList)
                {
                    JObject jresult = new JObject();
                    jresult.Add("BTemplateType", btemplateTypeDir);
                    jresult.Add("FileName", btemplate.FileName);
                    jresult.Add("CName", btemplate.CName);
                    string fullPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, btemplate.FilePath);
                    jresult.Add("FullPath", fullPath);
                    jObjectList.Add(jresult);
                }
            }
            else
            {
                //取公共模板下的某一模板类型的报表模板文件信息
                if (string.IsNullOrEmpty(ReportBTemplateHelp.ReportBaseDir))
                    ReportBTemplateHelp.ReportBaseDir = "Report";
                string path = System.AppDomain.CurrentDomain.BaseDirectory + ReportBTemplateHelp.ReportBaseDir + "\\" + templateType + "\\" + btemplateTypeDir;

                GetPublicTemplateDirFile(templateType, btemplateTypeDir, path, jObjectList);
            }
            jentityList.list = jObjectList;
            jentityList.count = jObjectList.Count;
            return jentityList;
        }
        public BaseResultBool AddBTemplateOfPublicTemplate(JArray jarray, long labId, string labCName, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();
            string baseDir = System.AppDomain.CurrentDomain.BaseDirectory;

            foreach (var item in jarray)
            {
                string curTemplateType = item["BTemplateType"].ToString();
                string fileName = item["FileName"].ToString();
                string cName = item["CName"].ToString();
                string fullPath = item["FullPath"].ToString();
                fullPath = fullPath.Replace("\\\\", "\\");
                if (!File.Exists(fullPath))
                {
                    ZhiFang.Common.Log.Log.Error("模板路径为:" + fullPath + ",不存在!");
                    continue;
                }
                try
                {
                    Stream fs = File.Open(fullPath, FileMode.Open, FileAccess.Read);
                    //string[] files = Directory.GetFiles(fullPath);
                    BTemplate entity = new BTemplate();
                    foreach (var dic in BTemplateType.GetStatusDic())
                    {
                        if (dic.Value.Name == curTemplateType)
                        {
                            entity.TypeID = long.Parse(dic.Key);
                            entity.TypeName = dic.Value.Name;
                            break;
                        }
                    }
                    string fileExt = fileName.Substring(fileName.LastIndexOf("."));
                    string reportSubDir = curTemplateType;// GetReportSubDirByFileExt(fileExt);
                    //reportSubDir = reportSubDir + "\\" + curTemplateType;
                    string templateType = FrxToPdfReportHelp.PublicTemplateDir;
                    //获取excel模板的规则内容信息
                    if (fileExt.ToLower().Contains(".xls"))
                    {
                        templateType = ExcelReportHelp.PublicTemplateDir;
                        IList<string> listCell = ExcelRuleInfoHelp.ReadExcelMoudleFile(fs);
                        fs = File.Open(fullPath, FileMode.Open, FileAccess.Read);
                        JObject jresult = ExcelRuleInfoHelp.GetExcelRuleInfo(listCell);
                        //ZhiFang.Common.Log.Log.Debug("jresult:" + jresult.ToString());
                        entity.ExcelRuleInfo = jresult.ToString();
                    }
                    string fullSavePath = ReportBTemplateHelp.GetBTemplateFullDir(labId, templateType, reportSubDir) + "\\" + fileName;

                    entity.LabID = labId;
                    entity.IsUse = true;
                    entity.CName = cName;
                    if (!string.IsNullOrEmpty(entity.CName))
                        entity.PinYinZiTou = GetPinYinConverter(entity.CName);
                    entity.Shortcode = entity.PinYinZiTou;
                    entity.FileName = fileName;
                    entity.FileSize = fs.Length;
                    entity.TemplateType = ExcelReportHelp.PublicTemplateDir;
                    if (fileExt == ".frx")
                    {
                        entity.TemplateType = FrxToPdfReportHelp.PublicTemplateDir;
                        entity.ContentType = "application/octet-stream";
                    }
                    else if (fileExt == ".xlsx")
                    {
                        entity.ContentType = "application/octet-stream";//application/ms-excel
                    }
                    else if (fileExt == ".xls")
                    {
                        entity.ContentType = "application/vnd.ms-excel";
                    }
                    else
                    {
                        entity.ContentType = "application/octet-stream";
                    }
                    entity.FileExt = fileExt;// ".frx";
                    entity.FilePath = ReportBTemplateHelp.GetBTemplateSubDir(labId, templateType, reportSubDir) + "\\" + fileName;

                    this.Entity = entity;
                    this.Add();
                    fs.Close();

                    File.Copy(fullPath, fullSavePath, true);

                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "新增报表模板保存失败!" + ex.Message;
                    break;
                }
            }
            return brdv;
        }
        /// <summary>
        /// 获取上传保存的模板
        /// </summary>
        /// <returns></returns>
        public FileStream GetTemplateFileStream(long id, ref string fileName)
        {
            FileStream fileStream = null;
            BTemplate entity = this.Get(id);
            string reportSubDir = "";
            reportSubDir = BTemplateType.GetStatusDic()[entity.TypeID.ToString()].Name;
            string templateType = FrxToPdfReportHelp.PublicTemplateDir;
            if (entity.FileExt.ToLower().Contains(".xls"))
            {
                templateType = ExcelReportHelp.PublicTemplateDir;
            }
            string fullPath = ReportBTemplateHelp.GetBTemplateFullDir(entity.LabID, templateType, reportSubDir);//this.GetFileParentPath();

            if (entity == null || string.IsNullOrEmpty(entity.FilePath))
            {
                return fileStream;
            }
            fileName = entity.FileName;
            fileStream = new FileStream(fullPath + "\\" + entity.FileName, FileMode.Open, FileAccess.Read);
            return fileStream;
        }
        /// <summary>
        /// 过滤文件名中的非法字符,并返回新的名字. 
        /// </summary>
        private string filterFileName(string fileName)
        {
            List<char> charArr = new List<char>() { '\\', '/', '*', '?', '"', '<', '>', '|', ':' };
            return charArr.Aggregate(fileName, (current, c) => current.Replace(c, '#'));
        }
        private string GetPinYinConverter(string chinese)
        {
            char[] tmpstr = chinese.ToCharArray();
            string pinYinZiTou = "";
            foreach (char a in tmpstr)
            {
                pinYinZiTou += ZhiFang.Common.Public.StringPlus.Chinese2Spell.SingleChs2Spell(a.ToString()).Substring(0, 1);
            }
            return pinYinZiTou;
        }
        private string GetReportSubDirByFileExt(string fileExt)
        {
            string reportSubDir = "";
            if (fileExt.Contains("frx"))
            {
                reportSubDir = FrxToPdfReportHelp.PublicTemplateDir;// "Frx模板";
            }
            else if (fileExt.Contains("xls"))
            {
                //fileExt == "xls" && fileExt == "xlsx"
                reportSubDir = ExcelReportHelp.PublicTemplateDir;//"Excel模板";
            }
            return reportSubDir;
        }
    }
}