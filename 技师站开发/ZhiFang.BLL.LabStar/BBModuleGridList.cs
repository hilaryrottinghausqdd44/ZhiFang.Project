
using System;
using System.Collections.Generic;
using System.Data;
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
	public  class BBModuleGridList : BaseBLL<BModuleGridList>,  IBBModuleGridList
	{
        private readonly string GridPath = AppDomain.CurrentDomain.BaseDirectory + "/ModuleConfigs";
        private readonly string GridFileName = "/GridConfig.json";
        IDBModuleFormListDao IDBModuleFormListDao { get; set; }
        IDBModuleGridControlListDao IDBModuleGridControlListDao { get; set; }
        public DataTable SearchModuleAggregateList(string gridCodes, string formCodes, string cheartCodes)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("AggregateType"); //集合类型
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("TypeName");
            dataTable.Columns.Add("ClassName");
            dataTable.Columns.Add("DispOrder");
            dataTable.Columns.Add("IsUse");

            if (!string.IsNullOrEmpty(gridCodes))
            {
                var gdcodes = gridCodes.Split(',');
                var Grids = DBDao.GetListByHQL("GridCode in ('" + string.Join("','", gdcodes) + "')");
                foreach (var girdcode in gdcodes)
                {
                    var gridwheres = Grids.Where(a => a.GridCode == girdcode);
                    if (gridwheres.Count() > 0)
                    {
                        var grid = gridwheres.First();
                        var row = dataTable.NewRow();
                        row["AggregateType"] = "grid";
                        row["Id"] = grid.Id;
                        row["Name"] = grid.CName;
                        row["Code"] = grid.GridCode;
                        row["TypeName"] = grid.TypeName;
                        row["ClassName"] = grid.ClassName;
                        row["DispOrder"] = grid.DispOrder;
                        row["IsUse"] = grid.IsUse;
                        dataTable.Rows.Add(row);
                    }
                    else
                    {
                        var row = dataTable.NewRow();
                        row["AggregateType"] = "grid";
                        row["Code"] = girdcode;
                        dataTable.Rows.Add(row);
                    }
                }
            }
            if (!string.IsNullOrEmpty(formCodes))
            {
                var frcodes = formCodes.Split(',');
                var Forms = IDBModuleFormListDao.GetListByHQL("FormCode in ('" + string.Join("','", frcodes) + "')");
                foreach (var formCode in frcodes)
                {
                    var formwheres = Forms.Where(a => a.FormCode == formCode);
                    if (formwheres.Count() > 0)
                    {
                        var form = formwheres.First();
                        var row = dataTable.NewRow();
                        row["AggregateType"] = "form";
                        row["Id"] = form.Id;
                        row["Name"] = form.CName;
                        row["Code"] = form.FormCode;
                        row["TypeName"] = form.TypeName;
                        row["ClassName"] = form.ClassName;
                        row["DispOrder"] = form.DispOrder;
                        row["IsUse"] = form.IsUse;
                        dataTable.Rows.Add(row);
                    }
                    else
                    {
                        var row = dataTable.NewRow();
                        row["AggregateType"] = "form";
                        row["Code"] = formCode;
                        dataTable.Rows.Add(row);
                    }
                }
            }

            return dataTable;
        }
        /// <summary>
        /// 读取数据库配置项 生成默认配置文件
        /// </summary>
        /// <returns></returns>
        public bool GetModuleGridConfigDefault()
        {
            bool flag = true;
            //表格配置
            var GridLists = DBDao.LoadAll();
            var GridCLists = IDBModuleGridControlListDao.LoadAll();
            List<ModuleGridConifg> GridConifgs = new List<ModuleGridConifg>();
            foreach (var GridList in GridLists)
            {
                ModuleGridConifg gridConifg = new ModuleGridConifg();
                var gridclists = GridCLists.Where(a => a.GridCode == GridList.GridCode);
                gridConifg.BModuleGridList = GridList;
                gridConifg.BModuleGridControlList = gridclists.ToList();
                GridConifgs.Add(gridConifg);
            }
            //转成json
            var jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(GridConifgs);
            //生成文件
            Directory.CreateDirectory(GridPath);
            File.WriteAllText(GridPath + GridFileName, jsonstr, Encoding.UTF8);
            return flag;
        }
        /// <summary>
        /// 读取表格默认配置文件
        /// </summary>
        private List<ModuleGridConifg> ReadModuleGridConfigDefault()
        {
            string filepath = GridPath + GridFileName;
            if (!File.Exists(filepath))
            {
                ZhiFang.Common.Log.Log.Debug("BBModuleGridList.ReadModuleGridConfigDefault:未找到默认表格配置文件!");
                return null;
            }
            var filestr = File.ReadAllText(filepath, Encoding.UTF8);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModuleGridConifg>>(filestr);
        }
        public void AddSetModuleGridConfigDefault()
        {
            var count = DBDao.GetListCountByHQL("");
            if (count == 0)
            {
                var ModuleGrids = ReadModuleGridConfigDefault();
                if (ModuleGrids != null && ModuleGrids.Count > 0)
                {
                    foreach (var item in ModuleGrids)
                    {
                        var moduleGridlist = item.BModuleGridList;
                        moduleGridlist.LabID = new BModuleGridList().LabID;
                        moduleGridlist.Id = new BModuleGridList().Id;
                        var flag = DBDao.Save(moduleGridlist);
                        if (flag)
                        {
                            foreach (var BModuleGridControlList in item.BModuleGridControlList)
                            {
                                BModuleGridControlList.Id = new BModuleGridControlList().Id;
                                BModuleGridControlList.LabID = new BModuleGridControlList().LabID;
                                IDBModuleGridControlListDao.Save(BModuleGridControlList);
                            }
                        }
                    }
                }
            }
        }
        public void EditSetModuleGridConfigDefault()
        {
            var gridLists = DBDao.LoadAll();
            if (gridLists.Count == 0)
            {
                AddSetModuleGridConfigDefault();
            }
            else
            {
                var ModuleGrids = ReadModuleGridConfigDefault();
                if (ModuleGrids != null && ModuleGrids.Count > 0)
                {
                    foreach (var item in ModuleGrids)
                    {
                        var gridcount = gridLists.Count(a => a.GridCode == item.BModuleGridList.GridCode);
                        if (gridcount == 0)
                        {
                            var moduleGridlist = item.BModuleGridList;
                            moduleGridlist.LabID = new BModuleGridList().LabID;
                            moduleGridlist.Id = new BModuleGridList().Id;
                            var flag = DBDao.Save(moduleGridlist);
                            if (flag)
                            {
                                foreach (var BModuleGridControlList in item.BModuleGridControlList)
                                {
                                    BModuleGridControlList.Id = new BModuleGridControlList().Id;
                                    BModuleGridControlList.LabID = new BModuleGridControlList().LabID;
                                    IDBModuleGridControlListDao.Save(BModuleGridControlList);
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}