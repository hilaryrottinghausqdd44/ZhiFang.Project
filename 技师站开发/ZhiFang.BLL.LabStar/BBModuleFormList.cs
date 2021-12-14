using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IBLL.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
	/// <summary>
	///
	/// </summary>
	public  class BBModuleFormList : BaseBLL<BModuleFormList>,  IBBModuleFormList
	{
        private readonly string FormPath = AppDomain.CurrentDomain.BaseDirectory + "/ModuleConfigs";
        private readonly string FormFileName = "/FormConfig.json";
        IDBModuleFormControlListDao IDBModuleFormControlListDao { get; set; }
        /// <summary>
        /// 读取数据库配置项 生成默认配置文件
        /// </summary>
        /// <returns></returns>
        public bool GetModuleFormConfigDefault()
        {
            bool flag = true;
            //表单配置
            var FormLists = DBDao.LoadAll();
            var FormCLists = IDBModuleFormControlListDao.LoadAll();
            List<ModuleFormConifg> GridConifgs = new List<ModuleFormConifg>();
            foreach (var FormList in FormLists)
            {
                ModuleFormConifg formConifg = new ModuleFormConifg();
                var formclists = FormCLists.Where(a => a.FormCode == FormList.FormCode);
                formConifg.BModuleFormList = FormList;
                formConifg.BModuleFormControlList = formclists.ToList();
                GridConifgs.Add(formConifg);
            }
            //转成json
            var jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(GridConifgs);
            //生成文件
            Directory.CreateDirectory(FormPath);
            File.WriteAllText(FormPath + FormFileName, jsonstr, Encoding.UTF8);
            return flag;
        }

        public void AddSetModuleFormConfigDefault()
        {
            var count = DBDao.GetListCountByHQL("");
            if (count == 0)
            {
                var ModuleForms = ReadModuleFormConfigDefault();
                if (ModuleForms != null && ModuleForms.Count > 0)
                {
                    foreach (var item in ModuleForms)
                    {
                        var moduleFormlist = item.BModuleFormList;
                        moduleFormlist.LabID = new BModuleFormList().LabID;
                        moduleFormlist.Id = new BModuleFormList().Id;
                        var flag = DBDao.Save(moduleFormlist);
                        if (flag)
                        {
                            foreach (var BModuleFormControlList in item.BModuleFormControlList)
                            {
                                BModuleFormControlList.Id = new BModuleFormControlList().Id;
                                BModuleFormControlList.LabID = new BModuleFormControlList().LabID;
                                IDBModuleFormControlListDao.Save(BModuleFormControlList);
                            }
                        }
                    }
                }
            }
        }

        public void EditSetModuleFormConfigDefault()
        {
            var formLists = DBDao.LoadAll();
            if (formLists.Count == 0)
            {
                var ModuleForms = ReadModuleFormConfigDefault();
                if (ModuleForms != null && ModuleForms.Count > 0)
                {
                    foreach (var item in ModuleForms)
                    {
                        if (formLists.Count(a => a.FormCode == item.BModuleFormList.FormCode) == 0)
                        {
                            var moduleFormlist = item.BModuleFormList;
                            moduleFormlist.LabID = new BModuleFormList().LabID;
                            moduleFormlist.Id = new BModuleFormList().Id;
                            var flag = DBDao.Save(moduleFormlist);
                            if (flag)
                            {
                                foreach (var BModuleFormControlList in item.BModuleFormControlList)
                                {
                                    BModuleFormControlList.Id = new BModuleFormControlList().Id;
                                    BModuleFormControlList.LabID = new BModuleFormControlList().LabID;
                                    IDBModuleFormControlListDao.Save(BModuleFormControlList);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 读取默认配置文件
        /// </summary>
        private List<ModuleFormConifg> ReadModuleFormConfigDefault()
        {
            string filepath = FormPath + FormFileName;
            if (!File.Exists(filepath))
            {
                ZhiFang.Common.Log.Log.Debug("BBModuleGridList.ReadModuleFormConfigDefault:未找到默认表单配置文件!");
                return null;
            }
            var filestr = File.ReadAllText(filepath, Encoding.UTF8);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModuleFormConifg>>(filestr);
        }
    }
}