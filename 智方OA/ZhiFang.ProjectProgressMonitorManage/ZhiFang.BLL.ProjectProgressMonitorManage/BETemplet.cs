using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.IO;
using System.Reflection;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.Common.Public;
using ZhiFang.ProjectProgressMonitorManage.Common;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BETemplet : BaseBLL<ETemplet>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBETemplet
    {
        ZhiFang.IBLL.RBAC.IBHRDept IBHRDept { get; set; }
        IBETempletEmp IBETempletEmp { get; set; }
        IBETempletRes IBETempletRes { get; set; }
        IBEResEmp IBEResEmp { get; set; }
        IBEParameter IBEParameter { get; set; }

        string[] arrayObject = new string[] { "EQ", "ES", "PG", "RD", "FC" };
        string[] arrayNoShow = new string[] { "EI" };

        public BaseResultDataValue AddETemplet(ETemplet entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath");
            GetTempletStruct(entity, parentPath + entity.TempletPath);
            this.Entity = entity;
            baseResultDataValue.success = this.Add();
            return baseResultDataValue;
        }

        public BaseResultBool EditETemplet(ETemplet entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            ETemplet templet = this.Get(entity.Id);
            this.Entity = GetEntityByField<ETemplet>(templet, entity, fields);
            baseResultBool.success = this.Edit();
            return baseResultBool;
        }

        public BaseResultDataValue EditETempletFillStruct(long id)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ETemplet templet = this.Get(id);
            string filePath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath") + templet.TempletPath;
            if (File.Exists(filePath))
            {
                GetTempletStruct(templet, filePath);
                this.Entity = templet;
                brdv.success = this.Edit();
                if (brdv.success)
                    brdv.ResultDataValue = templet.FillStruct;
            }
            return brdv;
        }

        public T GetEntityByField<T>(T entity, T entityField, string fields)
        {
            int tempIndex = -1;
            PropertyInfo fromPropertyInfo = null;
            PropertyInfo toPropertyInfo = null;
            string[] fieldArray = fields.Split(',');
            string FieldName = "";
            T fromEntity = entityField;
            T toEntity = entity;
            foreach (string tempFieldName in fieldArray)
            {
                tempIndex++;
                FieldName = tempFieldName;
                if (tempFieldName.IndexOf("_") > 0)
                {
                    List<string> tempList = tempFieldName.Split('_').ToList();
                    string childFieldName = tempList[tempList.Count - 1];
                    for (int i = 0; i < tempList.Count - 1; i++)
                    {
                        fromPropertyInfo = fromEntity.GetType().GetProperty(tempList[i].Trim());
                        toPropertyInfo = toEntity.GetType().GetProperty(tempList[i].Trim());
                        if (fromPropertyInfo != null && toPropertyInfo != null)
                            toPropertyInfo.SetValue(toEntity, fromPropertyInfo.GetValue(fromEntity, null), null);
                        else
                            break;
                    }
                }
                else
                {
                    fromPropertyInfo = fromEntity.GetType().GetProperty(FieldName.Trim());
                    toPropertyInfo = toEntity.GetType().GetProperty(FieldName.Trim());
                    if (fromPropertyInfo != null && toPropertyInfo != null)
                        toPropertyInfo.SetValue(toEntity, fromPropertyInfo.GetValue(fromEntity, null), null);
                }
            }
            return toEntity;
        }

        public void GetTempletStruct(ETemplet entity, string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                IList<string> listCellValue = MyNPOIHelper.ReadExcelMoudleFile(fileName);
                entity.ShowFillItem = _GetShowItem(listCellValue);
                tree cellTree = _GetModuleContentTree(listCellValue);
                if (cellTree != null)
                    entity.TempletStruct = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(cellTree);
                entity.TempletFillStruct = _GetModuleContent(listCellValue);
                entity.FillStruct = _GetModuleFillStruct(listCellValue);
            }
        }

        //private string _GetModuleContent(IList<string> listCellValue)
        //{
        //    string resultStr = "";
        //    Dictionary<string, string> dicFirstTree = new Dictionary<string, string>();
        //    Dictionary<string, string> dicSecondTree = new Dictionary<string, string>();
        //    Dictionary<string, string> dicThreeTree = new Dictionary<string, string>();
        //    try
        //    {
        //        foreach (string cellValue in listCellValue)
        //        {
        //            if (!string.IsNullOrEmpty(cellValue))
        //            {
        //                if (cellValue.IndexOf("[") >= 0 && cellValue.IndexOf("]") >= 0)
        //                {
        //                    IList<string> listMoudleCell = _GetNeedContentByExpression(cellValue, @"(?<=\[)[^\[\]]+(?=\])");
        //                    IList<string> listMoudleCotent = listMoudleCell[0].Split('|').ToList();
        //                    if (listMoudleCotent != null && listMoudleCotent.Count >= 2)
        //                    {
        //                        if (!string.IsNullOrEmpty(listMoudleCotent[0]) && listMoudleCotent[0].Length == 2)
        //                        {
        //                            if (!dicFirstTree.ContainsKey(listMoudleCotent[0].ToUpper()))
        //                            {
        //                                if (listMoudleCotent.Count == 2)
        //                                    dicFirstTree.Add(listMoudleCotent[0].ToUpper(), listMoudleCotent[1]);
        //                                else if (listMoudleCotent.Count >= 3)
        //                                    dicFirstTree.Add(listMoudleCotent[0].ToUpper(), listMoudleCotent[1]+"|"+listMoudleCotent[2]);
        //                            }
        //                        }
        //                        else if (!string.IsNullOrEmpty(listMoudleCotent[0]) && listMoudleCotent[0].Length > 2)
        //                        {
        //                            if (!dicSecondTree.ContainsKey(listMoudleCotent[0].ToUpper()))
        //                            {
        //                                //if (listMoudleCotent.Count == 2)
        //                                //    dicSecondTree.Add(listMoudleCotent[0].ToUpper(), listMoudleCotent[1]);
        //                                //else if (listMoudleCotent.Count == 3)
        //                                //    dicSecondTree.Add(listMoudleCotent[0].ToUpper(), listMoudleCotent[1] + "|" + listMoudleCotent[2]);

        //                                string tempStr = listMoudleCotent[1];
        //                                for (int i = 2; i < listMoudleCotent.Count; i++)
        //                                    tempStr += "|" + listMoudleCotent[i];
        //                                dicSecondTree.Add(listMoudleCotent[0].ToUpper(), tempStr);
        //                            }
        //                        }
        //                    }
        //                }
        //                else if (cellValue.IndexOf("{") >= 0 && cellValue.IndexOf("}") >= 0)
        //                {
        //                    string[] array = cellValue.Split(',');
        //                    IList<string> listMoudleCell = _GetNeedContentByExpression(cellValue, "(?<={)[^{}]+(?=})");
        //                    IList<string> listMoudleCotent = listMoudleCell[0].Split('|').ToList();
        //                    if (listMoudleCotent != null && listMoudleCotent.Count >= 2)
        //                    {
        //                        string key = listMoudleCotent[0].ToUpper() + "&" + array[0] + array[1];
        //                        if (!dicThreeTree.ContainsKey(key))
        //                            dicThreeTree.Add(key, cellValue);
        //                    }
        //                }
        //            }
        //        }
        //        StringBuilder stringBuilder = new StringBuilder();
        //        foreach (KeyValuePair<string, string> kvp in dicThreeTree)
        //        {
        //            string dicKey = kvp.Key.Substring(0, kvp.Key.IndexOf("&"));
        //            string firstKey = dicKey.Substring(0, 2);
        //            string[] arrayValue = kvp.Value.Split(',');
        //            stringBuilder.Append("," + arrayValue[0] + "|" + arrayValue[1]);
        //            IList<string> listMoudleCell = _GetNeedContentByExpression(kvp.Value, "(?<={)[^{}]+(?=})");
        //            if (listMoudleCell != null && listMoudleCell.Count > 0)
        //            {
        //                string strFormat = listMoudleCell[0].Remove(0, listMoudleCell[0].IndexOf("|")+1);
        //                if (dicFirstTree.ContainsKey(firstKey))
        //                    stringBuilder.Append("&" + firstKey + "|" + dicFirstTree[firstKey]);
        //                if (dicSecondTree.ContainsKey(dicKey))
        //                    stringBuilder.Append("&" + dicKey + "|" + dicSecondTree[dicKey]);
        //                else if (dicKey.Length == 2)
        //                    stringBuilder.Append("&" + dicKey + "|" + dicFirstTree[dicKey]); ;
        //                stringBuilder.Append("&" + strFormat);
        //            }
        //        }
        //        if (stringBuilder.Length > 0)
        //            resultStr = stringBuilder.ToString().Remove(0, 1);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return resultStr;
        //}

        private string _GetShowItem(IList<string> listCellValue)
        {
            string resultStr = "";
            try
            {
                for (int i = listCellValue.Count - 1; i >= 0; i--)
                {
                    string cellValue = listCellValue[i];
                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        if (cellValue.IndexOf("[") >= 0 && cellValue.IndexOf("]") >= 0)
                        {
                            IList<string> listMoudleCell = GetNeedContentByExpression(cellValue, @"(?<=\[)[^\[\]]+(?=\])");
                            IList<string> listMoudleCotent = listMoudleCell[0].Split('|').ToList();
                            if (listMoudleCotent != null && listMoudleCotent.Count >= 1)
                            {
                                if (!string.IsNullOrEmpty(listMoudleCotent[0]) && listMoudleCotent[0].Length == 2 && arrayNoShow.Contains(listMoudleCotent[0].ToUpper()))
                                {
                                    resultStr = listMoudleCell[0];
                                    listCellValue.Remove(cellValue);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resultStr;
        }
        private string _GetModuleContent(IList<string> listCellValue)
        {
            string resultStr = "";
            Dictionary<string, string> dicFirstTree = new Dictionary<string, string>();
            Dictionary<string, string> dicSecondTree = new Dictionary<string, string>();
            Dictionary<string, string> dicThreeTree = new Dictionary<string, string>();
            try
            {
                foreach (string cellValue in listCellValue)
                {
                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        if (cellValue.IndexOf("[") >= 0 && cellValue.IndexOf("]") >= 0)
                        {
                            IList<string> listMoudleCell = GetNeedContentByExpression(cellValue, @"(?<=\[)[^\[\]]+(?=\])");
                            IList<string> listMoudleCotent = listMoudleCell[0].Split('|').ToList();
                            if (listMoudleCotent != null && listMoudleCotent.Count >= 2)
                            {
                                if (!string.IsNullOrEmpty(listMoudleCotent[0]) && listMoudleCotent[0].Length == 2)
                                {

                                    if (!dicFirstTree.ContainsKey(listMoudleCotent[0].ToUpper()))
                                    {
                                        if (listMoudleCotent.Count == 2)
                                            dicFirstTree.Add(listMoudleCotent[0].ToUpper(), listMoudleCotent[1]);
                                        else if (listMoudleCotent.Count >= 3)
                                            dicFirstTree.Add(listMoudleCotent[0].ToUpper(), listMoudleCotent[1] + "|" + listMoudleCotent[2]);
                                    }
                                }
                                else if (!string.IsNullOrEmpty(listMoudleCotent[0]) && listMoudleCotent[0].Length > 2)
                                {
                                    if (!dicSecondTree.ContainsKey(listMoudleCotent[0].ToUpper()))
                                    {
                                        string tempStr = listMoudleCotent[1];
                                        for (int i = 2; i < listMoudleCotent.Count; i++)
                                            tempStr += "|" + listMoudleCotent[i];
                                        dicSecondTree.Add(listMoudleCotent[0].ToUpper(), tempStr);
                                    }
                                }
                            }
                        }
                        else if (cellValue.IndexOf("{") >= 0 && cellValue.IndexOf("}") >= 0)
                        {
                            string[] array = cellValue.Split(',');
                            IList<string> listMoudleCell = GetNeedContentByExpression(cellValue, "(?<={)[^{}]+(?=})");
                            IList<string> listMoudleCotent = listMoudleCell[0].Split('|').ToList();
                            if (listMoudleCotent != null && listMoudleCotent.Count >= 2)
                            {
                                string key = listMoudleCotent[0].ToUpper() + "&" + array[0] + array[1];
                                if (!dicThreeTree.ContainsKey(key))
                                    dicThreeTree.Add(key, cellValue);
                            }
                        }
                    }
                }
                StringBuilder stringBuilder = new StringBuilder();
                foreach (KeyValuePair<string, string> kvp in dicThreeTree)
                {
                    string dicKey = kvp.Key.Substring(0, kvp.Key.IndexOf("&"));
                    string firstKey = dicKey.Substring(0, 2);
                    string[] arrayValue = kvp.Value.Split(',');
                    stringBuilder.Append("," + arrayValue[0] + "|" + arrayValue[1]);
                    IList<string> listMoudleCell = GetNeedContentByExpression(kvp.Value, "(?<={)[^{}]+(?=})");
                    if (listMoudleCell != null && listMoudleCell.Count > 0 && (!string.IsNullOrWhiteSpace(listMoudleCell[0])))
                    {
                        string firstFormat = "";
                        string secondFormat = "";
                        string strFormat = listMoudleCell[0].Remove(0, listMoudleCell[0].IndexOf("|") + 1);
                        if (dicFirstTree.ContainsKey(firstKey))
                            firstFormat = dicFirstTree[firstKey];
                        if (dicSecondTree.ContainsKey(dicKey))
                            secondFormat = dicSecondTree[dicKey];
                        stringBuilder.Append("&" + firstKey + "|" + firstFormat + "&" + dicKey + "|" + secondFormat + "&" + strFormat);
                    }
                }
                if (stringBuilder.Length > 0)
                    resultStr = stringBuilder.ToString().Remove(0, 1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resultStr;
        }

        private string _GetModuleFillStruct(IList<string> listCellValue)
        {
            string resultStr = "";
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (string cellValue in listCellValue)
                {
                    if (!string.IsNullOrEmpty(cellValue) && cellValue.IndexOf("{") >= 0 && cellValue.IndexOf("}") >= 0)
                        stringBuilder.Append("&" + cellValue);
                }
                if (stringBuilder.Length > 0)
                    resultStr = stringBuilder.ToString().Remove(0, 1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resultStr;
        }

        private tree _GetModuleContentTree(IList<string> listCellValue)
        {
            Dictionary<string, string> dicFirstTree = new Dictionary<string, string>();
            Dictionary<string, string> dicSecondTree = new Dictionary<string, string>();
            Dictionary<string, string> dicThreeTree = new Dictionary<string, string>();
            tree rootTree = new tree();
            rootTree.pid = "0";
            rootTree.tid = "1";
            rootTree.text = "";
            rootTree.value = "";
            rootTree.Tree = new List<tree>();
            rootTree.leaf = (rootTree.Tree.Count <= 0);
            try
            {
                foreach (string cellValue in listCellValue)
                {
                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        if (cellValue.IndexOf("[") >= 0 && cellValue.IndexOf("]") >= 0)
                        {
                            IList<string> listMoudleCell = GetNeedContentByExpression(cellValue, @"(?<=\[)[^\[\]]+(?=\])");
                            for (int j = 0; j < listMoudleCell.Count; j++)
                            {
                                IList<string> listMoudleCotent = listMoudleCell[j].Split('|').ToList();
                                if (listMoudleCotent != null && listMoudleCotent.Count >= 2)
                                {
                                    if (!string.IsNullOrEmpty(listMoudleCotent[0]) && listMoudleCotent[0].Length == 2 && (!arrayObject.Contains(listMoudleCotent[0].ToUpper())))
                                    {
                                        if (!dicFirstTree.ContainsKey(listMoudleCotent[0].ToUpper()))
                                            dicFirstTree.Add(listMoudleCotent[0].ToUpper(), listMoudleCotent[1]);
                                    }
                                    else if (!string.IsNullOrEmpty(listMoudleCotent[0]) && listMoudleCotent[0].Length > 2)
                                    {
                                        if (!dicSecondTree.ContainsKey(listMoudleCotent[0].ToUpper()))
                                        {
                                            string tempStr = listMoudleCotent[1];
                                            for (int i = 2; i < listMoudleCotent.Count; i++)
                                                tempStr += "|" + listMoudleCotent[i];
                                            dicSecondTree.Add(listMoudleCotent[0].ToUpper(), tempStr);
                                        }
                                    }
                                }
                            }//for
                        }
                        else if (cellValue.IndexOf("{") >= 0 && cellValue.IndexOf("}") >= 0)
                        {
                            IList<string> listMoudleCell = GetNeedContentByExpression(cellValue, "(?<={)[^{}]+(?=})");
                            for (int j = 0; j < listMoudleCell.Count; j++)
                            {
                                IList<string> listMoudleCotent = listMoudleCell[j].Split('|').ToList();
                                if (listMoudleCotent != null && listMoudleCotent.Count >= 2)
                                {
                                    if (!dicThreeTree.ContainsKey(listMoudleCotent[0].ToUpper()))
                                        dicThreeTree.Add(listMoudleCotent[0].ToUpper(), listMoudleCell[j]);
                                }
                            }
                        }
                    }
                }
                string para = IBEParameter.QueryPara("QualityRecord", "TypeCodeSort");
                if (!(string.IsNullOrWhiteSpace(para)) && para.Trim() == "1")
                {
                    dicFirstTree = (from entry in dicFirstTree orderby entry.Key ascending select entry).ToDictionary(p => p.Key, p => p.Value);
                    dicSecondTree = (from entry in dicSecondTree orderby entry.Key ascending select entry).ToDictionary(p => p.Key, p => p.Value);
                }
                foreach (KeyValuePair<string, string> kvp in dicFirstTree)
                {
                    IList<KeyValuePair<string, string>> kvList = dicSecondTree.Where(p => p.Key.Length > 2 && p.Key.Substring(0, 2) == kvp.Key).ToList();
                    if ((kvList == null || kvList.Count == 0) && (kvp.Key.ToUpper() != "ET") && (!arrayNoShow.Contains(kvp.Key.ToUpper())))
                        throw new Exception("质量记录登记项目[" + kvp.Key + "|" + kvp.Value + "]，没有维护对应的子项目信息");
                    _AddModuleContentTree(rootTree, kvp.Key, kvp.Value);
                }

                foreach (KeyValuePair<string, string> kvp in dicSecondTree)
                {
                    string parentTreeID = kvp.Key.Substring(0, 2);
                    IList<tree> tempList = rootTree.Tree.Where(p => p.tid == parentTreeID).ToList();
                    if (tempList != null && tempList.Count > 0)
                        _AddModuleContentTree(tempList[0], kvp.Key, kvp.Value);
                    else
                    {
                        throw new Exception("质量记录登记项目[" + kvp.Key + "|" + kvp.Value + "]，没有维护对应的父类型[" + parentTreeID + "]信息");
                    }
                }

                foreach (KeyValuePair<string, string> kvp in dicThreeTree)
                {
                    if (kvp.Key.Length > 2)
                    {
                        string parentTreeID = kvp.Key.Substring(0, 2);
                        IList<tree> tempList = rootTree.Tree.Where(p => p.tid == parentTreeID).ToList();
                        if (tempList != null && tempList.Count > 0 && tempList[0].Tree != null && tempList[0].Tree.Count > 0)
                        {
                            IList<tree> leafList = tempList[0].Tree.Where(p => p.tid == kvp.Key).ToList();
                            if (leafList != null && leafList.Count > 0)
                                leafList[0].value = kvp.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rootTree;
        }

        private void _AddModuleContentTree(tree parentTree, string treeID, string treeValue)
        {
            IList<tree> tempList = parentTree.Tree.Where(p => p.tid == treeID).ToList();
            if (parentTree.Tree.Count < 1 || tempList == null || tempList.Count == 0)
            {
                tree tree = new tree();
                tree.pid = parentTree.tid;
                tree.tid = treeID;
                tree.text = treeValue;
                tree.Tree = new List<tree>();
                tree.leaf = (tree.Tree.Count <= 0);
                _GetTempletItemProperty(tree);
                parentTree.Tree.Add(tree);
                parentTree.leaf = (parentTree.Tree.Count <= 0);

            }
        }

        private void _GetTempletItemProperty(tree tree)
        {
            TempletItemProperty templetItemProperty = new TempletItemProperty();
            templetItemProperty.InitItemCode = tree.tid + "|" + tree.text;
            templetItemProperty.ItemCode = tree.tid;
            string[] listValue = tree.text.Split('|');
            if (listValue != null && listValue.Length > 0)
            {
                tree.text = listValue[0];
                templetItemProperty.ItemName = listValue[0];
                string[] itemNameList = listValue[0].Split('/');
                if (itemNameList != null && itemNameList.Length > 1)
                {
                    if (templetItemProperty.ItemCode.ToUpper() != "ET")
                    {
                        if (itemNameList.Length > 1)
                            templetItemProperty.ItemSplitChar = itemNameList[1];
                        if (itemNameList.Length > 2)
                            templetItemProperty.ItemtMemo = itemNameList[2];
                        if (itemNameList.Length > 3)
                            templetItemProperty.ItemMemoPosition = itemNameList[3];
                        if (itemNameList.Length > 4)
                            templetItemProperty.RowItemCode = itemNameList[4];
                        if (string.IsNullOrEmpty(templetItemProperty.ItemMemoPosition))
                            templetItemProperty.ItemMemoPosition = "R";
                    }
                    else
                    {
                        if (itemNameList.Length > 0)
                            templetItemProperty.TempletMemoTitle = itemNameList[0];
                        if (itemNameList.Length > 1)
                            templetItemProperty.TempletMemo = itemNameList[1];
                        if (itemNameList.Length > 2)
                            templetItemProperty.TempletMemoPosition = itemNameList[2];
                        if (string.IsNullOrEmpty(templetItemProperty.TempletMemoPosition))
                            templetItemProperty.TempletMemoPosition = "B";
                    }
                    tree.Para = templetItemProperty;
                }
                if (listValue.Length > 1)
                {
                    string itemDataType = listValue[1].ToUpper();
                    templetItemProperty.ItemDataType = itemDataType;
                    if (itemDataType == "E" || itemDataType == "E1" || itemDataType == "S" || itemDataType == "S1")
                    {
                        if (listValue.Length > 2)
                            templetItemProperty.DefaultValue = listValue[2];
                        if (listValue.Length > 3)
                            templetItemProperty.ItemValueList = listValue[3];
                        if (listValue.Length > 4)
                            templetItemProperty.IsSpreadItemList = listValue[4];
                        if (listValue.Length > 5)
                            templetItemProperty.IsInputItemValue = listValue[5];

                    }
                    else if (itemDataType == "I" || itemDataType == "L" || itemDataType == "F")
                    {
                        if (listValue.Length > 2)
                            templetItemProperty.DefaultValue = listValue[2];
                        if (listValue.Length > 3)
                            templetItemProperty.MinValue = listValue[3];
                        if (listValue.Length > 4)
                            templetItemProperty.MaxValue = listValue[4];
                        if (listValue.Length > 5)
                            templetItemProperty.DecimalLength = listValue[5];
                        if (listValue.Length > 6)
                            templetItemProperty.AddValue = listValue[6];
                    }
                    else if (itemDataType == "C" || itemDataType == "CL" || itemDataType == "M" || itemDataType == "O")
                    {
                        if (listValue.Length > 2)
                            templetItemProperty.DefaultValue = listValue[2];
                        if (listValue.Length > 3)
                            templetItemProperty.DataLength = listValue[3];
                        if (listValue.Length > 4 && (!string.IsNullOrEmpty(listValue[4])))
                        {
                            string[] arrayList = listValue[4].Split('/');
                            if (arrayList.Length > 0)
                                templetItemProperty.ItemValueList = arrayList[0];
                            if (arrayList.Length > 1)
                                templetItemProperty.IsInputItemValue = arrayList[1];
                            if (arrayList.Length > 2)
                                templetItemProperty.IsSpreadItemList = arrayList[2];
                            if (arrayList.Length > 3)
                                templetItemProperty.IsMultiSelect = arrayList[3];
                        }
                        if (listValue.Length > 5 && (!string.IsNullOrEmpty(listValue[5])))
                        {
                            string[] arrayList = listValue[5].Split('/');
                            if (arrayList.Length > 0)
                                templetItemProperty.ItemHeight = arrayList[0];
                            if (arrayList.Length > 1)
                                templetItemProperty.ItemWidth = arrayList[1];
                        }
                    }
                    else if (itemDataType == "D" || itemDataType == "T" || itemDataType == "DT")
                    {
                        if (listValue.Length > 2)
                            templetItemProperty.DefaultValue = listValue[2];
                        if (listValue.Length > 3)
                            templetItemProperty.DateType = listValue[3];
                    }
                    tree.Para = templetItemProperty;
                }
            }
        }

        public IList<string> GetNeedContentByExpression(string strSource, string pattern)
        {
            string value = "";
            IList<string> listStr = new List<string>();
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(strSource);
            foreach (Match match in matches)
            {
                value = match.Value;
                if (!string.IsNullOrEmpty(value))
                    listStr.Add(value);
            }
            return listStr;
        }

        public string QueryEquipNameByID(long templetID, bool isAddID)
        {
            string equipName = "";
            ETemplet templet = this.Get(templetID);
            if (templet.EEquip != null)
            {
                equipName = templet.EEquip.CName;
                if (isAddID && (!string.IsNullOrEmpty(equipName)))
                    equipName += "_" + templet.EEquip.Id.ToString();
            }
            return equipName;
        }

        public string QueryEquipTempletNameByID(long templetID, bool isAddID)
        {
            string equipName = "";
            string templetName = "";
            ETemplet templet = this.Get(templetID);
            if (templet.EEquip != null)
            {
                equipName = templet.EEquip.CName;
                if (isAddID && (!string.IsNullOrEmpty(equipName)))
                    equipName += "_" + templet.EEquip.Id.ToString();
            }
            if (!string.IsNullOrEmpty(templet.SName))
                templetName = templet.SName;
            else if (!string.IsNullOrEmpty(templet.UseCode))
                templetName = templet.UseCode;
            else if (!string.IsNullOrEmpty(templet.CName))
                templetName = templet.CName;
            return equipName + "\\" + templetName;
        }

        public EntityList<ETemplet> SearchETempletByHRDeptID(string where, int page, int limit, string sort)
        {
            EntityList<ETemplet> tempList = new EntityList<ETemplet>();
            if (where != null && where.Length > 0)
            {
                string[] tempHQLList = where.Split('^');
                if (tempHQLList.Length > 0)
                {
                    long tempHRDeptID = 0;
                    string tempOtherHQL = "";
                    string strWhereSQL = "";
                    string[] tempIDHQL = tempHQLList[0].Split('=');
                    tempHRDeptID = Int64.Parse(tempIDHQL[1]);
                    if (tempHQLList.Length > 1 && (!string.IsNullOrEmpty(tempHQLList[1])))
                        tempOtherHQL = " and " + tempHQLList[1];
                    BaseResultTree tempBaseResultTree = IBHRDept.SearchHRDeptTree(tempHRDeptID);
                    strWhereSQL = GetPropertySQLByTree(tempBaseResultTree.Tree);
                    if (!string.IsNullOrEmpty(strWhereSQL))
                    {
                        strWhereSQL = "(" + strWhereSQL.Remove(0, 3) + ")" + tempOtherHQL;
                        tempList = this.SearchListByHQL(strWhereSQL, sort, page, limit);
                    }
                }
            }
            return tempList;
        }

        private string GetPropertySQLByTree(List<tree> treeList)
        {
            string strWhereSQL = "";

            foreach (tree tempTree in treeList)
            {
                strWhereSQL = strWhereSQL + " or etemplet.Section.Id=" + tempTree.tid.ToString();
                if (tempTree.Tree.Count > 0)
                    strWhereSQL = strWhereSQL + GetPropertySQLByTree(tempTree.Tree);
            }
            return strWhereSQL;
        }


        private EntityList<ETemplet> SearchTempletEmpByEmp(long empID, string where, int page, int limit, string sort)
        {
            EntityList<ETemplet> entityList = new EntityList<ETemplet>();
            entityList.list = new List<ETemplet>();
            IList<ETempletEmp> listTemplet = IBETempletEmp.SearchListByHQL(" etempletemp.HREmployee.Id=" + empID.ToString());
            if (listTemplet != null && listTemplet.Count > 0)
            {
                string templetID = "";
                foreach (ETempletEmp entity in listTemplet)
                {
                    if (templetID == "")
                        templetID = entity.ETemplet.Id.ToString();
                    else
                        templetID += "," + entity.ETemplet.Id.ToString();
                }
                if (string.IsNullOrEmpty(where))
                    where = " etemplet.Id in (" + templetID + ")";
                else
                    where += " and etemplet.Id in (" + templetID + ")";
                if (string.IsNullOrEmpty(sort))
                    entityList = this.SearchListByHQL(where, page, limit);
                else
                    entityList = this.SearchListByHQL(where, sort, page, limit);
            }
            return entityList;
        }

        public EntityList<ETemplet> SearchTempletResByEmp(long empID, string where, string resWhere, int page, int limit, string sort)
        {
            EntityList<ETemplet> entityList = new EntityList<ETemplet>();
            entityList.list = new List<ETemplet>();
            IList<EResEmp> listResEmp = IBEResEmp.SearchListByHQL(" eresemp.HREmployee.Id=" + empID.ToString());
            if (listResEmp != null && listResEmp.Count > 0)
            {
                string resID = "";
                foreach (EResEmp entity in listResEmp)
                    if (resID == "")
                        resID = entity.EResponsibility.Id.ToString();
                    else
                        resID += "," + entity.EResponsibility.Id.ToString();
                if (string.IsNullOrEmpty(resWhere))
                    resWhere = " etempletres.EResponsibility.Id in (" + resID + ")";
                //else
                //    resWhere += " and etempletres.EResponsibility.Id in (" + resID + ")";
                IList<ETempletRes> lisTempletRes = IBETempletRes.SearchListByHQL(resWhere);
                if (lisTempletRes != null && lisTempletRes.Count > 0)
                {
                    string templetID = "";
                    foreach (ETempletRes entity in lisTempletRes)
                    {
                        if (templetID == "")
                            templetID = entity.ETemplet.Id.ToString();
                        else
                            templetID += "," + entity.ETemplet.Id.ToString();
                    }
                    if (string.IsNullOrEmpty(where))
                        where = " etemplet.Id in (" + templetID + ")";
                    else
                        where += " and etemplet.Id in (" + templetID + ")";
                    if (string.IsNullOrEmpty(sort))
                        entityList = this.SearchListByHQL(where, page, limit);
                    else
                        entityList = this.SearchListByHQL(where, sort, page, limit);
                }
            }
            return entityList;
        }

        public EntityList<ETemplet> SearchTempletByEmp(int relationType, long empID, string where, string resWhere, int page, int limit, string sort)
        {
            EntityList<ETemplet> entityList = new EntityList<ETemplet>();
            entityList.list = new List<ETemplet>();
            if (empID > 0)
            {
                if (relationType == 1)
                    entityList = SearchTempletEmpByEmp(empID, where, page, limit, sort);
                else if (relationType == 2)
                    entityList = SearchTempletResByEmp(empID, where, resWhere, page, limit, sort);
                else
                {
                    EntityList<ETemplet> entityList1 = SearchTempletEmpByEmp(empID, where, 0, 0, sort);
                    EntityList<ETemplet> entityList2 = SearchTempletResByEmp(empID, where, resWhere, 0, 0, sort);
                    if (entityList1 != null && entityList2 != null && entityList1.count > 0 && entityList2.count > 0)
                    {
                        entityList.list = entityList1.list.Union(entityList2.list).ToList();
                        entityList.count = entityList.list.Count;
                        if (page > 0 && limit > 0)
                        {
                            entityList.list = entityList.list.Skip((page - 1) * limit).Take(limit).ToList();
                        }
                    }
                    else if (entityList1 != null && entityList1.count > 0)
                        entityList = entityList1;
                    else if (entityList2 != null && entityList2.count > 0)
                        entityList = entityList2;
                }
            }
            return entityList;
        }
    }

    public class TempletItemProperty
    {
        [DataMember]
        [DataDesc(CName = "初始化项目代码", ShortCode = "InitItemCode", Desc = "初始化项目代码", ContextType = SysDic.All, Length = 200)]
        public virtual string InitItemCode { get; set; }

        [DataMember]
        [DataDesc(CName = "项目代码", ShortCode = "ItemCode", Desc = "项目代码", ContextType = SysDic.All, Length = 200)]
        public virtual string ItemCode { get; set; }

        [DataMember]
        [DataDesc(CName = "项目名称", ShortCode = "ItemName", Desc = "项目名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ItemName { get; set; }

        [DataMember]
        [DataDesc(CName = "数据类型", ShortCode = "ItemDataType", Desc = "数据类型", ContextType = SysDic.All, Length = 200)]
        public virtual string ItemDataType { get; set; }

        [DataMember]
        [DataDesc(CName = "默认值", ShortCode = "DefaultValue", Desc = "默认值", ContextType = SysDic.All, Length = 200)]
        public virtual string DefaultValue { get; set; }

        [DataMember]
        [DataDesc(CName = "最小值", ShortCode = "MinValue", Desc = "最小值", ContextType = SysDic.All, Length = 200)]
        public virtual string MinValue { get; set; }

        [DataMember]
        [DataDesc(CName = "最大值", ShortCode = "MaxValue", Desc = "最大值", ContextType = SysDic.All, Length = 200)]
        public virtual string MaxValue { get; set; }

        [DataMember]
        [DataDesc(CName = "数值长度", ShortCode = "DataLength", Desc = "数值长度", ContextType = SysDic.All, Length = 10)]
        public virtual string DataLength { get; set; }

        [DataMember]
        [DataDesc(CName = "小数位数", ShortCode = "DecimalLength", Desc = "小数位数", ContextType = SysDic.All, Length = 10)]
        public virtual string DecimalLength { get; set; }

        [DataMember]
        [DataDesc(CName = "累加数值", ShortCode = "AddValue", Desc = "累加数值", ContextType = SysDic.All, Length = 10)]
        public virtual string AddValue { get; set; }

        [DataMember]
        [DataDesc(CName = "取值列表", ShortCode = "ItemValueList", Desc = "取值列表", ContextType = SysDic.All, Length = 200)]
        public virtual string ItemValueList { get; set; }

        [DataMember]
        [DataDesc(CName = "列表内容可输入", ShortCode = "IsInputItemValue", Desc = "列表内容可输入", ContextType = SysDic.All, Length = 200)]
        public virtual string IsInputItemValue { get; set; }

        [DataMember]
        [DataDesc(CName = "列表是否展开", ShortCode = "IsSpreadItemList", Desc = "列表是否展开", ContextType = SysDic.All, Length = 200)]
        public virtual string IsSpreadItemList { get; set; }

        [DataMember]
        [DataDesc(CName = "列表是否多选", ShortCode = "IsMultiSelect", Desc = "列表是否多选", ContextType = SysDic.All, Length = 200)]
        public virtual string IsMultiSelect { get; set; }

        [DataMember]
        [DataDesc(CName = "日期格式", ShortCode = "DateType", Desc = "日期格式", ContextType = SysDic.All, Length = 200)]
        public virtual string DateType { get; set; }

        [DataMember]
        [DataDesc(CName = "输入框高度", ShortCode = "ItemHeight", Desc = "输入框高度", ContextType = SysDic.All, Length = 200)]
        public virtual string ItemHeight { get; set; }

        [DataMember]
        [DataDesc(CName = "输入框宽度", ShortCode = "ItemWidth", Desc = "输入框宽度", ContextType = SysDic.All, Length = 200)]
        public virtual string ItemWidth { get; set; }

        [DataMember]
        [DataDesc(CName = "分隔符", ShortCode = "ItemSplitChar", Desc = "项目名称与输入框之间分隔符", ContextType = SysDic.All, Length = 200)]
        public virtual string ItemSplitChar { get; set; }

        [DataMember]
        [DataDesc(CName = "项目备注", ShortCode = "ItemMemo", Desc = "项目备注", ContextType = SysDic.All, Length = 200)]
        public virtual string ItemtMemo { get; set; }

        [DataMember]
        [DataDesc(CName = "项目备注位置", ShortCode = "ItemMemoPosition", Desc = "项目备注位置", ContextType = SysDic.All, Length = 200)]
        public virtual string ItemMemoPosition { get; set; }

        [DataMember]
        [DataDesc(CName = "模板备注标题", ShortCode = "TempletMemoTitle", Desc = "模板备注标题", ContextType = SysDic.All, Length = 200)]
        public virtual string TempletMemoTitle { get; set; }

        [DataMember]
        [DataDesc(CName = "模板备注", ShortCode = "TempletMemo", Desc = "模板备注", ContextType = SysDic.All, Length = 200)]
        public virtual string TempletMemo { get; set; }

        [DataMember]
        [DataDesc(CName = "模板备注位置", ShortCode = "TempletMemoPosition", Desc = "模板备注位置", ContextType = SysDic.All, Length = 200)]
        public virtual string TempletMemoPosition { get; set; }

        [DataMember]
        [DataDesc(CName = "同行项目代码", ShortCode = "RowItemCode", Desc = "同行项目代码", ContextType = SysDic.All, Length = 200)]
        public virtual string RowItemCode { get; set; }
    }
}