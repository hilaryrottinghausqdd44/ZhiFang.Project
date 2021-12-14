using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.Entity.RBAC;
using ZhiFang.LabStar.Common;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BBPrintModel : BaseBLL<BPrintModel>, ZhiFang.IBLL.LabStar.IBBPrintModel
    {
        int FileAutoIncrement = 0;
        public EntityList<BPrintModelVO> LS_UDTO_SearchBPrintModelAndAutoUploadModel(string BusinessTypeCode, string ModelTypeCode, string where, string sort)
        {
            EntityList<BPrintModelVO> entityList = new EntityList<BPrintModelVO>();
            List<BPrintModelVO> list = new List<BPrintModelVO>();
            string labid = ZhiFang.Common.Public.Cookie.Get(SysPublicSet.SysDicCookieSession.LabID);
            string empname = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.EmployeeName);//人员姓名
            string empid = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.EmployeeID);//人员ID
            string modelfield = ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template");//模板根目录
            string basepath = System.AppDomain.CurrentDomain.BaseDirectory + modelfield;
            string strwhere = "1=1";
            var BusinessType = PrintModelBusinessType.GetStatusDic();
            var ModelType = PrintModelModelType.GetStatusDic();
            if (!string.IsNullOrEmpty(where))
            {
                strwhere = where;
            }
            if (!string.IsNullOrEmpty(BusinessTypeCode))
            {
                strwhere += " and BusinessTypeCode = '" + BusinessTypeCode + "'";
            }
            if (!string.IsNullOrEmpty(ModelTypeCode))
            {
                strwhere += " and ModelTypeCode = '" + ModelTypeCode + "'";
            }
            EntityList<BPrintModel> bpmel = DBDao.GetListByHQL(strwhere, sort, 0, 0);
            //list = bpmel.list.ToList().ConvertAll(s => (BPrintModelVO)s);
            foreach (var item in bpmel.list)
            {
                list.Add(ClassMapperHelp.GetMapper<BPrintModelVO, BPrintModel>(item));
            }
            if (bpmel.count > 0 || Directory.Exists(basepath))
            {
                if (string.IsNullOrEmpty(BusinessTypeCode) && string.IsNullOrEmpty(ModelTypeCode))
                {
                    foreach (var bt in BusinessType)//业务类型
                    {
                        string btpath = basepath + @"\" + bt.Value.Code;
                        if (Directory.Exists(btpath))
                        {
                            foreach (var mt in ModelType)//模板类型
                            {
                                string mtpath = btpath + @"\" + mt.Value.Code + @"\" + labid;
                                if (Directory.Exists(mtpath))
                                {
                                    var files = Directory.GetFiles(mtpath, "*.frx");
                                    foreach (var file in files)
                                    {
                                        if (file.IndexOf(mt.Value.Name) == 0)
                                        {
                                            List<BPrintModelVO> bPrintModels = list.Where(a => a.BusinessTypeCode == bt.Value.Id && a.ModelTypeCode == mt.Value.Id && a.FileName == file).ToList();
                                            if (bPrintModels.Count > 0)
                                            {
                                                int index = list.FindIndex((BPrintModelVO s) => s.Id == bPrintModels[0].Id);
                                                list[index].IsFile = true;
                                            }
                                            else
                                            {
                                                BPrintModel addentity = new BPrintModel();
                                                addentity.BusinessTypeCode = bt.Value.Id;
                                                addentity.ModelTypeCode = mt.Value.Id;
                                                addentity.BusinessTypeName = bt.Value.Code;
                                                addentity.ModelTypeName = mt.Value.Code;
                                                addentity.FileName = file;
                                                addentity.FileComment = "系统自动加入";
                                                addentity.DispOrder = 0;
                                                addentity.OperaterID = long.Parse(empid);
                                                addentity.Operater = empname;
                                                addentity.FinalOperaterID = long.Parse(empid);
                                                addentity.FinalOperater = empname;
                                                addentity.IsProtect = false;
                                                addentity.IsUse = true;
                                                addentity.FileUploadTime = DateTime.Now;
                                                addentity.DataAddTime = DateTime.Now;
                                                bool isok = DBDao.Save(addentity);
                                                if (isok)
                                                {
                                                    BPrintModelVO addentityvo = ClassMapperHelp.GetMapper<BPrintModelVO, BPrintModel>(addentity);
                                                    addentityvo.IsFile = true;
                                                    list.Add(addentityvo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(BusinessTypeCode) && string.IsNullOrEmpty(ModelTypeCode))
                {
                    KeyValuePair<string, BaseClassDicEntity> dic = BusinessType.Where(a => a.Key == BusinessTypeCode).First();
                    string btpath = basepath + @"\" + dic.Value.Code;
                    if (Directory.Exists(btpath))
                    {
                        foreach (var mt in ModelType)//模板类型
                        {
                            string mtpath = btpath + @"\" + mt.Value.Code + @"\" + labid;
                            if (Directory.Exists(mtpath))
                            {
                                var files = Directory.GetFiles(mtpath, "*.frx");
                                foreach (var file in files)
                                {
                                    if (file.IndexOf(mt.Value.Name) == 0)
                                    {
                                        List<BPrintModelVO> bPrintModels = list.Where(a => a.BusinessTypeCode == dic.Value.Id && a.ModelTypeCode == mt.Value.Id && a.FileName == file).ToList();
                                        if (bPrintModels.Count > 0)
                                        {
                                            int index = list.FindIndex((BPrintModelVO s) => s.Id == bPrintModels[0].Id);
                                            list[index].IsFile = true;
                                        }
                                        else
                                        {
                                            BPrintModel addentity = new BPrintModel();
                                            addentity.BusinessTypeCode = dic.Value.Id;
                                            addentity.ModelTypeCode = mt.Value.Id;
                                            addentity.BusinessTypeName = dic.Value.Code;
                                            addentity.ModelTypeName = mt.Value.Code;
                                            addentity.FileName = file;
                                            addentity.FileComment = "系统自动加入";
                                            addentity.DispOrder = 0;
                                            addentity.OperaterID = long.Parse(empid);
                                            addentity.Operater = empname;
                                            addentity.FinalOperaterID = long.Parse(empid);
                                            addentity.FinalOperater = empname;
                                            addentity.IsProtect = false;
                                            addentity.IsUse = true;
                                            addentity.FileUploadTime = DateTime.Now;
                                            addentity.DataAddTime = DateTime.Now;
                                            bool isok = DBDao.Save(addentity);
                                            if (isok)
                                            {
                                                BPrintModelVO addentityvo = ClassMapperHelp.GetMapper<BPrintModelVO, BPrintModel>(addentity);
                                                addentityvo.IsFile = true;
                                                list.Add(addentityvo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (string.IsNullOrEmpty(BusinessTypeCode) && !string.IsNullOrEmpty(ModelTypeCode))
                {
                    foreach (var bt in BusinessType)//业务类型
                    {
                        string btpath = basepath + @"\" + bt.Value.Code;
                        if (Directory.Exists(btpath))
                        {
                            KeyValuePair<string, BaseClassDicEntity> dic = ModelType.Where(a => a.Key == ModelTypeCode).First();
                            string mtpath = btpath + @"\" + dic.Value.Code + @"\" + labid;
                            if (Directory.Exists(mtpath))
                            {
                                var files = Directory.GetFiles(mtpath, "*.frx");
                                foreach (var file in files)
                                {
                                    if (file.IndexOf(dic.Value.Name) == 0)
                                    {
                                        List<BPrintModelVO> bPrintModels = list.Where(a => a.BusinessTypeCode == bt.Value.Id && a.ModelTypeCode == dic.Value.Id && a.FileName == file).ToList();
                                        if (bPrintModels.Count > 0)
                                        {
                                            int index = list.FindIndex((BPrintModelVO s) => s.Id == bPrintModels[0].Id);
                                            list[index].IsFile = true;
                                        }
                                        else
                                        {
                                            BPrintModel addentity = new BPrintModel();
                                            addentity.BusinessTypeCode = bt.Value.Id;
                                            addentity.ModelTypeCode = dic.Value.Id;
                                            addentity.BusinessTypeName = bt.Value.Code;
                                            addentity.ModelTypeName = dic.Value.Code;
                                            addentity.FileName = file;
                                            addentity.FileComment = "系统自动加入";
                                            addentity.DispOrder = 0;
                                            addentity.OperaterID = long.Parse(empid);
                                            addentity.Operater = empname;
                                            addentity.FinalOperaterID = long.Parse(empid);
                                            addentity.FinalOperater = empname;
                                            addentity.IsProtect = false;
                                            addentity.IsUse = true;
                                            addentity.FileUploadTime = DateTime.Now;
                                            addentity.DataAddTime = DateTime.Now;
                                            bool isok = DBDao.Save(addentity);
                                            if (isok)
                                            {
                                                BPrintModelVO addentityvo = ClassMapperHelp.GetMapper<BPrintModelVO, BPrintModel>(addentity);
                                                addentityvo.IsFile = true;
                                                list.Add(addentityvo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                else if (!string.IsNullOrEmpty(BusinessTypeCode) && !string.IsNullOrEmpty(ModelTypeCode))
                {
                    KeyValuePair<string, BaseClassDicEntity> btdic = BusinessType.Where(a => a.Key == BusinessTypeCode).First();
                    string btpath = basepath + @"\" + btdic.Value.Code;
                    if (Directory.Exists(btpath))
                    {
                        KeyValuePair<string, BaseClassDicEntity> mtdic = ModelType.Where(a => a.Key == ModelTypeCode).First();
                        string mtpath = btpath + @"\" + mtdic.Value.Code + @"\" + labid;
                        if (Directory.Exists(mtpath))
                        {
                            var files = Directory.GetFiles(mtpath, "*.frx");
                            foreach (var file in files)
                            {
                                var filename = file.Split('\\')[file.Split('\\').Length - 1];
                                if (filename.IndexOf(mtdic.Value.Name) == 0)
                                {
                                    List<BPrintModelVO> bPrintModels = list.Where(a => a.BusinessTypeCode == btdic.Value.Id && a.ModelTypeCode == mtdic.Value.Id && a.FileName == filename).ToList();
                                    if (bPrintModels.Count > 0)
                                    {
                                        int index = list.FindIndex((BPrintModelVO s) => s.Id == bPrintModels[0].Id);
                                        list[index].IsFile = true;
                                    }
                                    else
                                    {
                                        BPrintModel addentity = new BPrintModel();
                                        addentity.BusinessTypeCode = btdic.Value.Id;
                                        addentity.ModelTypeCode = mtdic.Value.Id;
                                        addentity.BusinessTypeName = btdic.Value.Code;
                                        addentity.ModelTypeName = mtdic.Value.Code;
                                        addentity.FileName = filename;
                                        addentity.FileComment = "系统自动加入";
                                        addentity.DispOrder = 0;
                                        addentity.OperaterID = long.Parse(empid);
                                        addentity.Operater = empname;
                                        addentity.FinalOperaterID = long.Parse(empid);
                                        addentity.FinalOperater = empname;
                                        addentity.IsProtect = false;
                                        addentity.IsUse = true;
                                        addentity.FileUploadTime = DateTime.Now;
                                        addentity.DataAddTime = DateTime.Now;
                                        this.Entity = addentity;
                                        bool isok = DBDao.Save(this.Entity);
                                        if (isok)
                                        {
                                            BPrintModelVO addentityvo = ClassMapperHelp.GetMapper<BPrintModelVO, BPrintModel>(this.Entity);
                                            addentityvo.IsFile = true;
                                            list.Add(addentityvo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            entityList.list = list;
            entityList.count = list.Count;
            return entityList;
        }
        public string FileNameReconsitution(string path, string code, string mtcode, string labid, string filename, out string newfilename)
        {
            string afilename = path + @"\" + code + @"\" + mtcode + @"\" + labid + @"\" + filename;
            newfilename = filename;
            if (File.Exists(afilename))
            {
                FileAutoIncrement++;
                if (filename.Split('.')[0].Split('_').Count() > 1)
                {
                    filename = filename.Split('.')[0].Split('_')[0] + "_" + FileAutoIncrement + ".frx";
                }
                else
                {
                    filename = filename.Split('.')[0] + "_" + FileAutoIncrement + ".frx";
                }
                return FileNameReconsitution(path, code, mtcode, labid, filename, out newfilename);
            }
            else
            {
                FileAutoIncrement = 0;
                return afilename;
            }
        }

        public string PrintDataByPrintModel(string Data, long PrintModelID)
        {
            JArray dataarr = JArray.Parse(Data);
            DataSet ds = new DataSet();
            for (int i = 0; i < dataarr.Count; i++)
            {
                JArray dgarr = JArray.Parse(dataarr[i].ToString());
                DataTable dataTable = new DataTable();
                if (i == 0)
                {
                    dataTable.TableName = "Table";
                }
                else
                {
                    dataTable.TableName = "Table" + (i + 1);
                }

                for (int a = 0; a < dgarr.Count; a++)
                {
                    JObject jo = JObject.Parse(dgarr[a].ToString());
                    if (a == 0)
                    {
                        foreach (JProperty item in jo.Children())
                        {
                            dataTable.Columns.Add(item.Name);
                        }
                    }
                    DataRow dataRow = dataTable.NewRow();
                    foreach (JProperty item in jo.Children())
                    {
                        dataRow[item.Name] = item.Value;
                    }
                    dataTable.Rows.Add(dataRow);
                }
                ds.Tables.Add(dataTable);
            }
            BPrintModel bPrintModel = DBDao.Get(PrintModelID);
            if (bPrintModel == null)
            {
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PrintDataByPrintModel.PrintDataByPrintModel.生成失败:未找到模板文件！");
                return "";
            }
            string basedir = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = basedir + @"\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template");
            string dicpath = path + @"\" + bPrintModel.BusinessTypeName + @"\" + bPrintModel.ModelTypeName + @"\" + bPrintModel.LabID + @"\" + bPrintModel.FileName;
            if (!File.Exists(dicpath))
            {
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PrintDataByPrintModel.PrintDataByPrintModel.生成失败:未找到模板文件！");
                return "";
            }
            FastReport.Report report = new FastReport.Report();
            report.Clear();
            report.Load(dicpath);
            if (ds.Tables.Count == 1)
            {
                report.RegisterData(ds);
            }
            else
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(ds.Tables[i]);
                    report.RegisterData(ds);
                }
            }

            report.Prepare();
            string FilePath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("PDFDir");
            if (!Directory.Exists(basedir + FilePath))
            {
                Directory.CreateDirectory(basedir + FilePath);
            }
            FilePath += @"\" + bPrintModel.BusinessTypeName;
            if (!Directory.Exists(basedir + FilePath))
            {
                Directory.CreateDirectory(basedir + FilePath);
            }
            FilePath += @"\" + bPrintModel.ModelTypeName;
            if (!Directory.Exists(basedir + FilePath))
            {
                Directory.CreateDirectory(basedir + FilePath);
            }
            var ModelType = PrintModelModelType.GetStatusDic();
            KeyValuePair<string, BaseClassDicEntity> mtdic = ModelType.Where(a => a.Key == bPrintModel.ModelTypeCode).First();
            FilePath += @"\" + mtdic.Value.Name + "_" + ZhiFang.Common.Public.GUIDHelp.GetGUIDInt() + ".pdf";
            report.Export(new FastReport.Export.Pdf.PDFExport(), basedir + FilePath);
            //判断并创建目录               
            report.Dispose();
            return FilePath;
        }
        public string ExportDataByPrintModel(string Data, long PrintModelID)
        {
            JArray dataarr = JArray.Parse(Data);
            DataSet ds = new DataSet();
            for (int i = 0; i < dataarr.Count; i++)
            {
                JArray dgarr = JArray.Parse(dataarr[i].ToString());
                DataTable dataTable = new DataTable();
                if (i == 0)
                {
                    dataTable.TableName = "Table";
                }
                else
                {
                    dataTable.TableName = "Table" + (i + 1);
                }

                for (int a = 0; a < dgarr.Count; a++)
                {
                    JObject jo = JObject.Parse(dgarr[a].ToString());
                    if (a == 0)
                    {
                        foreach (JProperty item in jo.Children())
                        {
                            dataTable.Columns.Add(item.Name);
                        }
                    }
                    DataRow dataRow = dataTable.NewRow();
                    foreach (JProperty item in jo.Children())
                    {
                        dataRow[item.Name] = item.Value;
                    }
                    dataTable.Rows.Add(dataRow);
                }
                ds.Tables.Add(dataTable);
            }
            BPrintModel bPrintModel = DBDao.Get(PrintModelID);
            if (bPrintModel == null)
            {
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PrintDataByPrintModel.PrintDataByPrintModel.生成失败:未找到模板文件！");
                return "";
            }
            string basedir = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = basedir + @"\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template");
            string dicpath = path + @"\" + bPrintModel.BusinessTypeName + @"\" + bPrintModel.ModelTypeName + @"\" + bPrintModel.LabID + @"\" + bPrintModel.FileName;
            if (!File.Exists(dicpath))
            {
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PrintDataByPrintModel.PrintDataByPrintModel.生成失败:未找到模板文件！");
                return "";
            }
            FastReport.Report report = new FastReport.Report();
            report.Clear();
            report.Load(dicpath);
            if (ds.Tables.Count == 1)
            {
                report.RegisterData(ds);
            }
            else
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(ds.Tables[i]);
                    report.RegisterData(ds);
                }
            }

            report.Prepare();
            string FilePath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("PDFDir");
            if (!Directory.Exists(basedir + FilePath))
            {
                Directory.CreateDirectory(basedir + FilePath);
            }
            FilePath += @"\" + bPrintModel.BusinessTypeName;
            if (!Directory.Exists(basedir + FilePath))
            {
                Directory.CreateDirectory(basedir + FilePath);
            }
            FilePath += @"\" + bPrintModel.ModelTypeName;
            if (!Directory.Exists(basedir + FilePath))
            {
                Directory.CreateDirectory(basedir + FilePath);
            }
            var ModelType = PrintModelModelType.GetStatusDic();
            KeyValuePair<string, BaseClassDicEntity> mtdic = ModelType.Where(a => a.Key == bPrintModel.ModelTypeCode).First();
            FilePath += @"\" + mtdic.Value.Name + "_" + ZhiFang.Common.Public.GUIDHelp.GetGUIDInt() + ".xls";
            report.Export(new FastReport.Export.Xml.XMLExport(), basedir + FilePath);
            //判断并创建目录               
            report.Dispose();
            return FilePath;
        }

        public string PrePrintGatherVoucherByPrintModel(List<ProofLisBarCodeFormVo> forms, List<ProofLisBarCodeItemVo> items, string modelcode)
        {
            DataSet formds = new DataSet();
            DataSet itemds = new DataSet();
            DataTable formtalbe = ObjectToDataSet.EntityConvetDataTable<ProofLisBarCodeFormVo>(forms);
            formtalbe.TableName = "LisBarCodeForm";
            formds.Tables.Add(formtalbe);
            DataTable itemtalbe = ObjectToDataSet.EntityConvetDataTable<ProofLisBarCodeItemVo>(items);
            itemtalbe.TableName = "LisBarCodeItem";
            itemds.Tables.Add(itemtalbe);
            BPrintModel bPrintModel = DBDao.Get(long.Parse(modelcode));
            if (bPrintModel == null)
            {
                ZhiFang.Common.Log.Log.Debug("PrePrintGatherVoucherByPrintModel.生成失败:未找到模板文件！");
                return "";
            }
            string basedir = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = basedir + @"\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template");
            string dicpath = path + @"\" + bPrintModel.BusinessTypeName + @"\" + bPrintModel.ModelTypeName + @"\" + bPrintModel.LabID + @"\" + bPrintModel.FileName;
            if (!File.Exists(dicpath))
            {
                ZhiFang.Common.Log.Log.Debug("PrePrintGatherVoucherByPrintModel.生成失败:未找到模板文件！");
                return "";
            }
            FastReport.Report report = new FastReport.Report();
            report.Clear();
            report.Load(dicpath);
            report.RegisterData(formds);
            report.RegisterData(itemds);
            report.Prepare();
            string FilePath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("PDFDir");
            if (!Directory.Exists(basedir + FilePath))
            {
                Directory.CreateDirectory(basedir + FilePath);
            }
            FilePath += @"\" + bPrintModel.BusinessTypeName;
            if (!Directory.Exists(basedir + FilePath))
            {
                Directory.CreateDirectory(basedir + FilePath);
            }
            FilePath += @"\" + bPrintModel.ModelTypeName;
            if (!Directory.Exists(basedir + FilePath))
            {
                Directory.CreateDirectory(basedir + FilePath);
            }
            var ModelType = PrintModelModelType.GetStatusDic();
            KeyValuePair<string, BaseClassDicEntity> mtdic = ModelType.Where(a => a.Key == bPrintModel.ModelTypeCode).First();
            FilePath += @"\" + mtdic.Value.Name + "_" + ZhiFang.Common.Public.GUIDHelp.GetGUIDInt() + ".pdf";
            report.Export(new FastReport.Export.Pdf.PDFExport(), basedir + FilePath);
            //判断并创建目录               
            report.Dispose();
            return FilePath;
        }
    }
}