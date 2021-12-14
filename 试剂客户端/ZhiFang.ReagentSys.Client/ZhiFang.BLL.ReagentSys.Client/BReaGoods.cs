using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Reflection;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.ReagentSys.Client.Common;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaGoods : BaseBLL<ReaGoods>, ZhiFang.IBLL.ReagentSys.Client.IBReaGoods
    {
        IBBDict IBBDict { get; set; }
        IBBDictType IBBDictType { get; set; }
        IBSCOperation IBSCOperation { get; set; }
        IDReaCenOrgDao IDReaCenOrgDao { get; set; }

        /// <summary>
        /// 获取产品编号最大值
        /// </summary>
        /// <returns></returns>
        public long GetMaxGoodsId(long labID)
        {
            long curId = ((IDReaGoodsDao)base.DBDao).GetMaxGoodsId(labID);
            if (curId.ToString().Length >= 19)
            {
                curId = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
            }
            else if (curId <= -1)
            {
                IList<ReaCenOrg> tempList = IDReaCenOrgDao.GetListByHQL("reacenorg.Id=" + labID);
                if (tempList.Count > 0)
                {
                    curId = tempList[0].Id * 10;
                }
            }
            return curId;
        }

        public override bool Add()
        {
            int goodsSort = ((IDReaGoodsDao)base.DBDao).GetMaxGoodsSort();
            if (this.Entity.GoodsSort <= 0)
                this.Entity.GoodsSort = goodsSort;
            else if (this.Entity.GoodsSort >= 0 && this.Entity.GoodsSort < goodsSort)
                this.Entity.GoodsSort = goodsSort;
            if (!string.IsNullOrEmpty(this.Entity.GoodsNo))
            {
                bool result2 = EditVerificationGoodsNo(this.Entity);
                if (!result2) return result2;
            }

            bool result = DBDao.Save(this.Entity);
            return result;
        }
        /// <summary>
        /// 机构货品保存时,验证货品平台编码是否在当前机构内惟一
        /// </summary>
        /// <param name="reaGoods"></param>
        /// <returns></returns>
        public bool EditVerificationGoodsNo(ReaGoods reaGoods)
        {
            bool result = true;
            //为空不验证
            if (string.IsNullOrEmpty(reaGoods.GoodsNo))
                result = true;

            IList<ReaGoods> tempList = this.SearchListByHQL("reagoods.Visible=1 and reagoods.GoodsNo='" + reaGoods.GoodsNo + "' and reagoods.Id!=" + reaGoods.Id);
            if (tempList != null && tempList.Count > 0)
            {
                string info = "机构货品为:" + reaGoods.CName + ",的货品平台编码:" + reaGoods.GoodsNo + ",已经存在,货品平台编码不允许出现重复!";
                ZhiFang.Common.Log.Log.Error(info);
                result = false;
                throw new Exception(info);
            }

            return result;
        }
        //public override bool Update(string[] strParas)
        //{
        //    ReaGoods serverReaGoods = this.Get(this.Entity.Id);
        //    bool result = DBDao.Update(strParas);
        //    if (result)
        //    {
        //        this.AddReaReqOperation(serverReaGoods);
        //    }
        //    return result;
        //}
        public IList<ReaGoodsClassVO> SearchGoodsClassListByClassTypeAndHQL(string classType, bool hasNull, string whereHql, string sort, int page, int limit)
        {
            return ((IDReaGoodsDao)base.DBDao).SearchGoodsClassListByClassTypeAndHQL(classType, hasNull, whereHql, sort, page, limit);
        }
        public EntityList<ReaGoodsClassVO> SearchGoodsClassEntityListByClassTypeAndHQL(string classType, bool hasNull, string whereHql, string sort, int page, int limit)
        {
            return ((IDReaGoodsDao)base.DBDao).SearchGoodsClassEntityListByClassTypeAndHQL(classType, hasNull, whereHql, sort, page, limit);
        }
        public void AddSCOperation(ReaGoods serverReaGoods, string[] arrFields)
        {
            //string fields=_getUpdateFields();
            StringBuilder strbMemo = new StringBuilder();
            EditGetUpdateMemo<ReaGoods>(serverReaGoods, this.Entity, this.Entity.GetType(), arrFields, ref strbMemo);
            if (strbMemo.Length > 0)
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)); ;
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                SCOperation sco = new SCOperation();
                sco.LabID = serverReaGoods.LabID;
                sco.BobjectID = this.Entity.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = "ReaGoods";
                strbMemo.Insert(0, "本次机构货品修改记录:" + System.Environment.NewLine);
                ZhiFang.Common.Log.Log.Debug("修改人:" + empName + "," + strbMemo.ToString());
                sco.Memo = strbMemo.ToString();
                sco.IsUse = true;
                sco.Type = long.Parse(ReaGoodsOperation.机构货品修改记录.Key);
                sco.TypeName = ReaGoodsOperation.GetStatusDic()[sco.Type.ToString()].Name;
                IBSCOperation.Entity = sco;
                IBSCOperation.Add();
            }
        }
        private void EditGetUpdateMemo<T>(T serverReaGoods, T updateReaGoods, Type type, string[] arrFields, ref StringBuilder strbMemo)
        {
            //为空判断
            if (serverReaGoods == null && updateReaGoods == null)
                return;
            else if (serverReaGoods == null || updateReaGoods == null)
                return;

            Type t = type;
            System.Reflection.PropertyInfo[] props = t.GetProperties();
            foreach (var po in props)
            {
                if (po.Name == "Id" || po.Name == "LabID" || po.Name == "DataAddTime" || po.Name == "DataUpdateTime" || po.Name == "DataTimeStamp")
                    continue;

                if (arrFields.Contains(po.Name) == true && IsCanCompare(po.PropertyType))
                {
                    object serverValue = po.GetValue(serverReaGoods, null);
                    object updateValue = po.GetValue(updateReaGoods, null);
                    if (serverValue == null)
                        serverValue = "";
                    if (updateValue == null)
                        updateValue = "";
                    //!string.IsNullOrEmpty(serverValue.ToString()) && !string.IsNullOrEmpty(updateValue.ToString()) && 
                    if (!serverValue.ToString().Equals(updateValue.ToString()))
                    {
                        string cname = po.Name;
                        foreach (var pattribute in po.GetCustomAttributes(false))
                        {
                            if (pattribute.ToString() == "ZhiFang.Entity.Base.DataDescAttribute")
                            {
                                cname = ((DataDescAttribute)pattribute).CName;
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(serverValue.ToString()))
                            serverValue = "空";
                        if (string.IsNullOrEmpty(updateValue.ToString()))
                            updateValue = "空";
                        strbMemo.Append("【" + cname + "】由原来:" + serverValue.ToString() + ",修改为:" + updateValue.ToString() + ";" + System.Environment.NewLine);
                    }
                }
            }
        }
        /// <summary>
        /// 该类型是否可直接进行值的比较
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool IsCanCompare(Type t)
        {
            if (t.IsValueType)
            {
                return true;
            }
            else
            {
                //String是特殊的引用类型，它可以直接进行值的比较
                if (t.FullName == typeof(String).FullName)
                {
                    return true;
                }
                return false;
            }
        }
        public BaseResultDataValue CheckGoodsExcelFormat(string excelFilePath, string serverPath)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string xmlGoods = serverPath + "\\BaseTableXML\\ReaGoods.xml";
            if (System.IO.File.Exists(xmlGoods))
            {
                IList<string> dicColumn = ExcelDataCommon.GetRequiredFieldByXml(xmlGoods);
                Dictionary<string, Type> dicType = ExcelDataCommon.GetFieldTypeByXml<ReaGoods>(xmlGoods);
                baseResultDataValue.success = MyNPOIHelper.CheckExcelFile(excelFilePath, dicColumn, dicType);
                if (!baseResultDataValue.success)
                {
                    baseResultDataValue.ErrorInfo = "Error001";
                    baseResultDataValue.ResultDataValue = System.IO.Path.GetFileName(excelFilePath);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "货品表导入配置信息不存在！";
                ZhiFang.Common.Log.Log.Info("货品表导入配置信息不存在！");
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue AddGoodsDataFormExcel(string prodID, string excelFilePath, string serverPath)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            DataTable dt = MyNPOIHelper.ImportExceltoDataTable(excelFilePath);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dataColumn.ColumnName = dataColumn.ColumnName.Trim();
                }
                string xmlGoods = serverPath + "\\BaseTableXML\\ReaGoods.xml";
                if (System.IO.File.Exists(xmlGoods))
                {
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(dt);

                    BDict prodEntity = null;
                    if (!string.IsNullOrWhiteSpace(prodID) && Int64.Parse(prodID) > 0)
                        prodEntity = IBBDict.Get(Int64.Parse(prodID));
                    IList<string> listPrimaryKey = new List<string>();
                    Dictionary<string, string> dicDefaultValue = new Dictionary<string, string>();
                    Dictionary<string, string> dicColumn = ExcelDataCommon.GetColumnNameByDataSet(dataSet, xmlGoods, listPrimaryKey, dicDefaultValue);

                    if (prodEntity != null)
                        baseResultDataValue = _AddGoodsDataTable(dt, prodEntity, dicColumn, listPrimaryKey, dicDefaultValue);
                    else
                        baseResultDataValue = _AddGoodsDataTable(dt, dicColumn, listPrimaryKey, dicDefaultValue);
                    baseResultDataValue.success = MyNPOIHelper.GetInputExcelFileState(excelFilePath, dt);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ErrorInfo = baseResultDataValue.ResultDataValue;
                        baseResultDataValue.ResultDataValue = System.IO.Path.GetFileName(excelFilePath);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "货品表导入配置信息不存在！";
                    ZhiFang.Common.Log.Log.Info("货品表导入配置信息不存在！");
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "货品表数据信息为空！";
                ZhiFang.Common.Log.Log.Info("货品表数据信息为空！");
            }
            return baseResultDataValue;
        }//

        public BaseResultDataValue _AddGoodsDataTable(DataTable dataTable, BDict prodEntity, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string prodColumn = "-9999";
            Dictionary<string, BDict> dicProd = new Dictionary<string, BDict>();
            dicProd.Add(prodColumn, prodEntity);
            baseResultDataValue = _AddGoodsData(dataTable, prodColumn, dicProd, dicColumn, listPrimaryKey, dicDefaultValue);
            return baseResultDataValue;
        }

        public BaseResultDataValue _AddGoodsDataTable(DataTable dataTable, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            string prodColumn = "";
            if (dicColumn.Values.Contains("ProdOrgName"))
                prodColumn = dicColumn.FirstOrDefault(q => q.Value == "ProdOrgName").Key;
            if (string.IsNullOrWhiteSpace(prodColumn))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "货品表配置文件没有配置货品机构名称信息！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            IList<BDictType> listBDictType = IBBDictType.SearchListByHQL(" bdicttype.DictTypeCode=\'ProdOrg\'");
            if (listBDictType == null || listBDictType.Count == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "字典类型表中不存在类型为【ProdOrg】的记录！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            BDictType dictType = listBDictType[0];
            Dictionary<string, BDict> dicProd = new Dictionary<string, BDict>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (string.IsNullOrWhiteSpace(dataRow[prodColumn].ToString()))
                {
                    baseResultDataValue.ErrorInfo = "导入失败：Excel中【" + prodColumn + "】列存在为空的信息，请补充完整！";
                    dataRow["ExcelRowInputFlag"] = -2;
                    dataRow["ExcelRowInputInfo"] = baseResultDataValue.ErrorInfo;
                    ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                    continue;
                }
            }
            foreach (DataRow dataRow in dataTable.Rows)
            {
                string prodOrgName = "";
                if (dataRow[prodColumn] != null)
                {
                    prodOrgName = dataRow[prodColumn].ToString();
                    if (dicProd.Keys.Contains(prodOrgName))
                        continue;
                    IList<BDict> listProd = IBBDict.SearchListByHQL("bdict.BDictType.DictTypeCode = \'ProdOrg\' and bdict.CName = \'" + prodOrgName + "\'");
                    if (listProd != null && listProd.Count > 0)
                    {
                        BDict dict = listProd[0];
                        if (!dicProd.Keys.Contains(prodOrgName))
                            dicProd.Add(prodOrgName, dict);
                    }
                    else
                    {
                        if (!dicProd.Keys.Contains(prodOrgName))
                        {
                            BDict dict = new BDict();
                            dict.CName = prodOrgName;
                            dict.BDictType = dictType;
                            dict.IsUse = true;
                            dict.DataAddTime = DateTime.Now;
                            IBBDict.Entity = dict;
                            if (IBBDict.Add())
                                dicProd.Add(prodOrgName, dict);
                        }
                    }
                }
            }
            baseResultDataValue = _AddGoodsData(dataTable, prodColumn, dicProd, dicColumn, listPrimaryKey, dicDefaultValue);
            return baseResultDataValue;
        }

        public BaseResultDataValue _AddGoodsData(DataTable dataTable, string prodColumn, Dictionary<string, BDict> dicProd, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            int isExistCount = 0;
            int isErrorCount = 0;
            int isSuccCount = 0;

            IList<ReaGoods> allReaGoodsList = this.LoadAll();

            Dictionary<string, long> dicMain = new Dictionary<string, long>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                //DataRow dataRow =dataTable.Rows[i];
                dataTable.Rows[i]["ExcelRowInputFlag"] = 0;
                dataTable.Rows[i]["ExcelRowInputInfo"] = "导入成功";

                if (listPrimaryKey.Count > 0)
                {
                    bool isAdd = AddJudgmentUnitNameAndReaGoodsNoSame(dicColumn, allReaGoodsList, ref dataTable, i, ref isExistCount);
                    if (isAdd)
                        AddJudgmentGonvertQtyOfNull(dicColumn, allReaGoodsList, ref dataTable, i, ref isAdd);
                    if (isAdd)
                        AddJudgmentGonvertQtyMoreThanOne(dicColumn, allReaGoodsList, ref dataTable, i, ref isAdd, ref isExistCount);

                    if (isAdd)
                    {
                        string keyValue = "";
                        foreach (string strKey in listPrimaryKey)
                        {
                            if (!string.IsNullOrEmpty(dataTable.Rows[i][strKey].ToString()))
                            {
                                keyValue += "_" + dataTable.Rows[i][strKey].ToString().Trim();
                            }
                        }
                        int GoodsSort = ((IDReaGoodsDao)base.DBDao).GetMaxGoodsSort();
                        ReaGoods Goods = ExcelDataCommon.AddExcelDataToDataBase<ReaGoods>(dataTable.Rows[i], dicColumn, dicDefaultValue);
                        if (Goods != null)
                        {
                            //Goods.Prod = prodEntity;
                            Goods.DataAddTime = DateTime.Now;
                            Goods.DataUpdateTime = DateTime.Now;
                            Goods.Visible = 1;
                            //Goods.IsMinUnit = true;
                            //Goods.GonvertQty = 1;
                            Goods.GoodsSort = GoodsSort + i;
                            this.Entity = Goods;
                            bool add = DBDao.Save(this.Entity);
                            // if (this.Add())
                            if (add)
                            {
                                dicMain[keyValue] = Goods.Id;
                                isSuccCount++;
                            }
                            else
                            {
                                isErrorCount++;
                                dataTable.Rows[i]["ExcelRowInputFlag"] = -2;
                                dataTable.Rows[i]["ExcelRowInputInfo"] = "导入失败：产品信息保存失败";
                                ZhiFang.Common.Log.Log.Info("导入失败：产品信息保存失败！");
                            }
                        }
                    }
                    else
                    {
                        baseResultDataValue.ErrorInfo = dataTable.Rows[i]["ExcelRowInputInfo"].ToString();
                        ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                        isErrorCount++;
                        continue;
                    }
                    #region MyRegion
                    //string keyValue = "";
                    //string keyHQL = "";
                    //foreach (string strKey in listPrimaryKey)
                    //{
                    //    if (!string.IsNullOrEmpty(dataTable.Rows[i][strKey].ToString()))
                    //    {
                    //        keyValue += "_" + dataTable.Rows[i][strKey].ToString().Trim();
                    //        keyHQL += " and " + " reagoods." + dicColumn[strKey] + "=\'" + dataTable.Rows[i][strKey].ToString().Trim() + "\'";
                    //    }
                    //}
                    //if ((!string.IsNullOrEmpty(keyValue)) && (!string.IsNullOrEmpty(keyHQL)))
                    //{
                    //    if (!dicMain.ContainsKey(keyValue))
                    //    {
                    //        keyHQL = keyHQL.Remove(0, 4);
                    //        //keyHQL += " and " + " reagoods.Prod.Id=" + prodEntity.Id.ToString();
                    //        IList<ReaGoods> listGoods = null;
                    //        dicMain.Add(keyValue, 0);
                    //        listGoods = this.SearchListByHQL(keyHQL);
                    //        if (listGoods != null && listGoods.Count > 0)
                    //        {
                    //            dicMain[keyValue] = listGoods[0].Id;
                    //            isExistCount++;
                    //            dataTable.Rows[i]["ExcelRowInputFlag"] = 1;
                    //            dataTable.Rows[i]["ExcelRowInputInfo"] = "未导入：该产品信息已经存在且换算系数相同";
                    //            ZhiFang.Common.Log.Log.Info("未导入：产品信息已经存在且换算系数相同！产品名称为：" + listGoods[0].CName + " 编码为：" + listGoods[0].GoodsNo);
                    //        }
                    //        else
                    //        {
                    //Add
                    //        }
                    //    }
                    //} 
                    #endregion
                }
                else
                    ZhiFang.Common.Log.Log.Info("货品表导入对照表没有设置唯一键！");

            }
            baseResultDataValue.ResultDataValue = string.Format("共需导入货品信息{0}条，其中：导入成功{1}条，导入失败{3}条，未导入货品{2}条！", dataTable.Rows.Count, isSuccCount, isExistCount, isErrorCount);
            return baseResultDataValue;
        }
        private bool AddJudgmentUnitNameAndReaGoodsNoSame(Dictionary<string, string> dicColumn, IList<ReaGoods> allReaGoodsList, ref DataTable dataTable, int i, ref int isExistCount)
        {
            bool isAdd = true;
            if (dicColumn.Values.Contains("UnitName") && dicColumn.Values.Contains("ReaGoodsNo"))
            {
                string unitNameStr = dicColumn.FirstOrDefault(q => q.Value == "UnitName").Key;
                string reaGoodsNoStr = dicColumn.FirstOrDefault(q => q.Value == "ReaGoodsNo").Key;
                string unitName = dataTable.Rows[i][unitNameStr].ToString();
                string reaGoodsNo = dataTable.Rows[i][reaGoodsNoStr].ToString();
                //先在excel里查找,如果存在ReaGoodsNo+UnitName大于1,不能导入
                DataRow[] mixRowArr = dataTable.Select(unitNameStr + "='" + unitName + "' and " + reaGoodsNoStr + "='" + reaGoodsNo + "'");
                var tempList = allReaGoodsList.Where(p => (p.ReaGoodsNo == reaGoodsNo && p.UnitName == unitName));

                int count = mixRowArr.Count() + tempList.Count();
                if (count > 1)
                {
                    isAdd = false;
                }
                if (!isAdd)
                {
                    isExistCount++;
                    dataTable.Rows[i]["ExcelRowInputFlag"] = -1;
                    dataTable.Rows[i]["ExcelRowInputInfo"] = string.Format("产品编号为：{0},包装单位为:{1},存在{2}条记录数,不能导入！", reaGoodsNo, unitName, count);
                }
            }
            return isAdd;
        }
        private void AddJudgmentGonvertQtyOfNull(Dictionary<string, string> dicColumn, IList<ReaGoods> allReaGoodsList, ref DataTable dataTable, int i, ref bool isAdd)
        {
            if (dicColumn.Values.Contains("GonvertQty"))
            {
                string gonvertQtyStr = dicColumn.FirstOrDefault(q => q.Value == "GonvertQty").Key;
                string reaGoodsNoStr = dicColumn.FirstOrDefault(q => q.Value == "ReaGoodsNo").Key;
                bool gonvertQtyIsNull = false;
                if (string.IsNullOrEmpty(dataTable.Rows[i][gonvertQtyStr].ToString()) || dataTable.Rows[i][gonvertQtyStr].ToString().Trim() == "0")
                    gonvertQtyIsNull = true;
                string reaGoodsNo = dataTable.Rows[i][reaGoodsNoStr].ToString();
                //当前的换算系数为空时的处理
                if (gonvertQtyIsNull)
                {
                    //先在excel里查找,是否存在换算系数为1的相同产品编号的货品,如果存在,可以导入
                    DataRow[] mixRowArr = dataTable.Select(gonvertQtyStr + "='1' and " + reaGoodsNoStr + "='" + reaGoodsNo + "'");
                    var tempList = allReaGoodsList.Where(p => (p.ReaGoodsNo == reaGoodsNo && p.GonvertQty == 1));
                    int count = mixRowArr.Length + tempList.Count();
                    //当批次货品+数据库货品的换算系数等于1的货品记录数为0
                    if (count == 0)
                    {
                        dataTable.Rows[i][gonvertQtyStr] = 1;
                    }
                    else
                    {
                        dataTable.Rows[i][gonvertQtyStr] = 0;
                    }
                }
            }
        }
        private void AddJudgmentGonvertQtyMoreThanOne(Dictionary<string, string> dicColumn, IList<ReaGoods> allReaGoodsList, ref DataTable dataTable, int i, ref bool isAdd, ref int isExistCount)
        {
            if (dicColumn.Values.Contains("GonvertQty"))
            {
                string gonvertQtyStr = dicColumn.FirstOrDefault(q => q.Value == "GonvertQty").Key;
                string reaGoodsNoStr = dicColumn.FirstOrDefault(q => q.Value == "ReaGoodsNo").Key;
                double gonvertQty = 0;
                if (!string.IsNullOrEmpty(dataTable.Rows[i][gonvertQtyStr].ToString()))
                    gonvertQty = double.Parse(dataTable.Rows[i][gonvertQtyStr].ToString());

                string reaGoodsNo = dataTable.Rows[i][reaGoodsNoStr].ToString();
                if (gonvertQty == 1)
                {
                    //先在excel里查找,是否存在换算系数为1的相同产品编号的货品,如果存在,可以导入                    
                    DataRow[] mixRowArr = dataTable.Select(gonvertQtyStr + "='1' and " + reaGoodsNoStr + "='" + reaGoodsNo + "'");
                    var tempList = allReaGoodsList.Where(p => (p.GonvertQty == 1 && p.ReaGoodsNo == reaGoodsNo));
                    int count = mixRowArr.Length + tempList.Count();
                    if (mixRowArr.Length == 1 && tempList.Count() <= 0)
                    {
                        isAdd = true;
                    }
                    else if (mixRowArr.Length > 1 || count > 1)
                    {
                        isAdd = false;
                    }

                    if (!isAdd)
                    {
                        isExistCount++;
                        dataTable.Rows[i]["ExcelRowInputFlag"] = -1;
                        dataTable.Rows[i]["ExcelRowInputInfo"] = "产品编号为：" + reaGoodsNo + "换算系数大于1,但不存在换算系数为1的最小单位货品，不能导入！";
                    }
                }
                else if (gonvertQty > 1)
                {
                    //先在excel里查找,是否存在换算系数为1的相同产品编号的货品,如果存在,可以导入                    
                    DataRow[] mixRowArr = dataTable.Select(gonvertQtyStr + "=1 and " + reaGoodsNoStr + "='" + reaGoodsNo + "'");
                    var tempList = allReaGoodsList.Where(p => (p.GonvertQty == 1 && p.ReaGoodsNo == reaGoodsNo));
                    int count = mixRowArr.Length + tempList.Count();
                    if (count >= 1)
                    {
                        isAdd = true;
                        return;
                    }
                    else
                    {
                        isAdd = false;
                        isExistCount++;
                        dataTable.Rows[i]["ExcelRowInputFlag"] = -1;
                        dataTable.Rows[i]["ExcelRowInputInfo"] = "产品编号为：" + reaGoodsNo + "换算系数大于1,但不存在换算系数为1的最小单位货品，不能导入！";
                    }
                }
            }
        }
        /// <summary>
        ///产品序号=最大值+1
        /// </summary>
        public int GetMaxGoodsSort()
        {
            try
            {
                IList<int> list = this.DBDao.Find<int>("select max(reagoods.GoodsSort) as GoodsSort  from ReaGoods reagoods where 1=1 ");
                if (list != null && list.Count > 0)
                {
                    int maxOrgNo = list[0];
                    maxOrgNo = maxOrgNo + 1;
                    return maxOrgNo;
                }
                else
                    return 1;
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// 获取导出货品信息列表
        /// </summary>
        /// <param name="idList">货品ID字符串列表</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <param name="xmlPath">导出信息配置文件路径</param>
        /// <returns></returns>
        public DataSet GetReaGoodsInfoByID(string idList, string where, string sort, string xmlPath)
        {
            EntityList<ReaGoods> entityList = null;
            if (string.IsNullOrEmpty(idList) && string.IsNullOrEmpty(where))
                return null;
            else
            {
                if (!string.IsNullOrEmpty(idList))
                    entityList = this.SearchListByHQL(" reagoods.Id in (" + idList + ")", sort, 0, 0);
                else if (!string.IsNullOrEmpty(where))
                    entityList = this.SearchListByHQL(where, sort, 0, 0);
                if (entityList != null && entityList.count > 0)
                    return CommonRS.GetListObjectToDataSet<ReaGoods>(entityList.list, xmlPath);
                else
                    return null;
            }
        }

        /// <summary>
        /// 获取最大的时间戳，接口同步货品使用
        /// </summary>
        /// <returns></returns>
        public string GetMaxTS()
        {
            string maxTS = ((IDReaGoodsDao)base.DBDao).GetMaxTS();
            if (maxTS == "0")
            {
                maxTS = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return maxTS;
        }

        /// <summary>
        /// 货品表结合库存表，查询二级分类并返回
        /// </summary>
        /// <returns></returns>
        public EntityList<ReaGoodsClassVO> SearchGoodsClassTypeJoinQtyDtl(string where, string sort, int page, int limit)
        {
            return ((IDReaGoodsDao)base.DBDao).SearchGoodsClassTypeJoinQtyDtl(where, sort, page, limit);
        }

        #region 物资接口同步
        public BaseResultData AddReaGoodsSyncByInterface(string syncField, string syncFieldValue, Dictionary<string, object> dicFieldAndValue)
        {
            BaseResultData baseResultData = new BaseResultData();
            EntityList<ReaGoods> listReaGoods = this.SearchListByHQL(" reagoods." + syncField + "=\'" + syncFieldValue + "\'", 0, 0);
            bool isEdit = (listReaGoods != null && listReaGoods.count > 0);
            ReaGoods reaGoods = null;
            if (isEdit)
                reaGoods = listReaGoods.list[0];
            else
                reaGoods = new ReaGoods();

            foreach (KeyValuePair<string, object> kv in dicFieldAndValue)
            {
                try
                {
                    System.Reflection.PropertyInfo propertyInfo = reaGoods.GetType().GetProperty(kv.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo != null && kv.Value != null)
                        propertyInfo.SetValue(reaGoods, ExcelDataCommon.DataConvert(propertyInfo, kv.Value), null);
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("货品实体属性赋值失败：" + kv.Key + "---" + kv.Value.ToString() + "。 Error：" + ex.Message);
                }
            }
            //reaGoods.Visible = 1;
            this.Entity = reaGoods;
            if (isEdit)
            {
                reaGoods.DataUpdateTime = DateTime.Now;
                baseResultData.success = this.Edit();
            }
            else
            {
                reaGoods.DataAddTime = DateTime.Now;
                baseResultData.success = this.Add();
            }

            return baseResultData;
        }

        public BaseResultData AddReaGoodsSyncByInterface(string syncField, string syncFieldValue, Dictionary<string, object> dicFieldAndValue, ref ReaCenOrg reaCenOrg, ref ReaGoods reaGoods)
        {
            BaseResultData baseResultData = new BaseResultData();
            EntityList<ReaGoods> listReaGoods = this.SearchListByHQL(" reagoods." + syncField + "=\'" + syncFieldValue + "\'", 0, 0);
            bool isEdit = (listReaGoods != null && listReaGoods.count > 0);

            if (isEdit)
                reaGoods = listReaGoods.list[0];
            string PRODNO = "";
            foreach (KeyValuePair<string, object> kv in dicFieldAndValue)
            {
                try
                {
                    System.Reflection.PropertyInfo propertyInfo = reaGoods.GetType().GetProperty(kv.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo != null && kv.Value != null)
                        propertyInfo.SetValue(reaGoods, ExcelDataCommon.DataConvert(propertyInfo, kv.Value), null);
                    if (kv.Key.ToLower() == "reacompcode")
                    {
                        reaCenOrg.MatchCode = DataConvert(kv.Value);
                    }
                    else if (kv.Key.ToLower() == "reacompanyname")
                    {
                        reaCenOrg.CName = DataConvert(kv.Value);
                    }

                    if (kv.Key.ToLower() == "prodno")
                        PRODNO = DataConvert(kv.Value);
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("货品实体属性赋值失败：" + kv.Key + "---" + kv.Value.ToString() + "。 Error：" + ex.Message);
                }
            }
            if (string.IsNullOrEmpty(reaGoods.ReaCompCode) && (!string.IsNullOrEmpty(PRODNO)))
            {
                reaCenOrg.MatchCode = PRODNO;
                reaGoods.ReaCompCode = PRODNO;
            }
            reaGoods.Visible = 1;
            return baseResultData;
        }

        public BaseResultData SaveReaGoodsByMatchInterface(IList<ReaGoods> editReaGoodsList, long empID, string empName)
        {
            ReaGoods reaGoods = null;
            return SaveReaGoodsByMatchInterface(editReaGoodsList, empID, empName, ref reaGoods);
        }
        private string GetUpdateFields()
        {
            string fields = "";
            string xmlGoods = System.Web.HttpContext.Current.Server.MapPath("~/") + "BaseTableXML\\Interface\\EntityFieldConfig.xml";
            if (System.IO.File.Exists(xmlGoods))
            {
                DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(xmlGoods);
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    if (!string.IsNullOrEmpty(dataRow["EntityFieldList"].ToString()))
                        fields = dataRow["EntityFieldList"].ToString().Trim();
                    break;
                }
            }
            if (string.IsNullOrEmpty(fields))
            {
                fields = "CName,EName,ShortCode,ReaGoodsNo,ProdGoodsNo,ProdEara,SName,GoodsNo,GoodsClass,GoodsClassType,UnitName,UnitMemo,DeptName,ApproveDocNo,Standard,MonthlyUsage,Price,Visible,StorageType,Constitute,Purpose,GoodsDesc,BarCodeMgr,IsRegister,IsPrintBarCode,SuitableType,ReaCompanyName,RegistNo,RegistDate,RegistNoInvalidDate,ProdID,ProdOrgName,PinYinZiTou";
            }
            return fields;
        }
        public BaseResultData SaveReaGoodsByMatchInterface(IList<ReaGoods> editReaGoodsList, long empID, string empName, ref ReaGoods reaGoods)
        {
            BaseResultData baseResultData = new BaseResultData();
            IList<ReaGoods> reaGoodsAllList = this.LoadAll();
            //按机构货品编码+转换系数进行排序
            reaGoodsAllList = reaGoodsAllList.OrderBy(p => p.ReaGoodsNo).ThenBy(p => p.GonvertQty).ToList();
            editReaGoodsList = editReaGoodsList.OrderBy(p => p.ReaGoodsNo).ThenBy(p => p.GonvertQty).ToList();//.GroupBy(p => new { p.ReaGoodsNo, p.GonvertQty });
            bool isEdit = false;
            bool isHasGonvertQty = false;
            //机构货品对照的数据项名称
            string fields = GetUpdateFields();
            string[] arrFields = fields.Split(',');
            ReaGoods serverEntity = null;
            int goodsSort = ((IDReaGoodsDao)base.DBDao).GetMaxGoodsSort();
            foreach (ReaGoods editEntity in editReaGoodsList)
            {
                isEdit = false;
                isHasGonvertQty = false;
                serverEntity = null;

                if (string.IsNullOrEmpty(editEntity.MatchCode))
                {
                    ZhiFang.Common.Log.Log.Info("同步物资接口的机构货品名称为：" + editEntity.CName + ",物资对照码(MatchCode)值为空");
                    continue;
                }
                if (string.IsNullOrEmpty(editEntity.UnitName))
                {
                    ZhiFang.Common.Log.Log.Info("同步物资接口的机构货品名称为：" + editEntity.CName + ",包装单位(UnitName)值为空");
                    continue;
                }
                if (string.IsNullOrEmpty(editEntity.ReaGoodsNo))
                {
                    ZhiFang.Common.Log.Log.Info("同步物资接口的机构货品名称为：" + editEntity.CName + ",机构货品编码(ReaGoodsNo)值为空");
                    continue;
                }

                #region 判断当前机构货品是否存在最小包装单位货品信息
                if (editEntity.GonvertQty == 1)
                {
                    isHasGonvertQty = true;
                }
                else if (editEntity.GonvertQty > 1)
                {
                    var tempList2 = editReaGoodsList.Where(p => p.ReaGoodsNo == editEntity.ReaGoodsNo && p.GonvertQty == 1);
                    if (tempList2.Count() > 0)
                    {
                        isHasGonvertQty = true;
                    }
                    else
                    {
                        tempList2 = reaGoodsAllList.Where(p => p.ReaGoodsNo == editEntity.ReaGoodsNo && p.GonvertQty == 1);
                        if (tempList2.Count() > 0)
                        {
                            isHasGonvertQty = true;
                        }
                    }
                }
                else
                {
                    isHasGonvertQty = true;
                    editEntity.GonvertQty = 1;
                }
                #endregion
                if (!isHasGonvertQty)
                {
                    ZhiFang.Common.Log.Log.Info("同步物资接口的机构货品名称为：" + editEntity.CName + ",不存在最小包装单位货品信息");
                }
                IList<ReaGoods> tempList3 = null;
                if (editEntity.Id > 0)
                    tempList3 = reaGoodsAllList.Where(p => p.Id == editEntity.Id).ToList();
                if (tempList3 == null || tempList3.Count == 0)
                    tempList3 = reaGoodsAllList.Where(p => p.ReaGoodsNo == editEntity.ReaGoodsNo && p.UnitName == editEntity.UnitName).ToList();
                if (tempList3.Count() > 0)
                {
                    serverEntity = tempList3.ElementAt(0);
                    isEdit = true;
                }

                if (isEdit)
                {
                    ZhiFang.Common.Log.Log.Debug("调试日志1：IsPrintBarCode=" + serverEntity.IsPrintBarCode);
                    bool isSave = false;
                    serverEntity.DataUpdateTime = DateTime.Now;
                    Type t = serverEntity.GetType();
                    System.Reflection.PropertyInfo[] props = t.GetProperties();
                    foreach (var po in props)
                    {

                        if (po.Name == "MatchCode" || po.Name == "BarCodeMgr" || po.Name == "ReaCompanyName" || po.Name == "ReaGoodsNo" || po.Name == "ProdOrgName" ||
                            po.Name == "CName" || po.Name == "GoodsNo" || po.Name == "Price" || po.Name == "UnitName" || po.Name == "UnitMemo" || po.Name == "ProdGoodsNo" ||
                            po.Name == "StorageType" || po.Name == "ApproveDocNo" || po.Name == "RegistNo" || po.Name == "RegistNoInvalidDate" || po.Name == "GoodsSort")
                        {
                            if (arrFields.Contains(po.Name) == true && IsCanCompare(po.PropertyType))
                            {
                                object serverValue = po.GetValue(serverEntity, null);
                                object updateValue = po.GetValue(editEntity, null);
                                if (serverValue == null)
                                    serverValue = "";
                                if (updateValue == null)
                                    updateValue = "";
                                if (!serverValue.Equals(updateValue))
                                {
                                    try
                                    {
                                        isSave = true;
                                        po.SetValue(serverEntity, updateValue, null);
                                        ZhiFang.Common.Log.Log.Info("调试日志-SaveReaGoodsByMatchInterface方法货品属性赋值：" + po.Name + "---" + updateValue);
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Info("SaveReaGoodsByMatchInterface方法货品属性赋值失败：" + po.Name + "---" + updateValue + "。 Error：" + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                    reaGoods = serverEntity;
                    if (isSave)
                    {
                        ZhiFang.Common.Log.Log.Debug("调试日志2：IsPrintBarCode=" + serverEntity.IsPrintBarCode);
                        //serverEntity = ClassMapperHelp.GetMapper<ReaGoods, ReaGoods>(editEntity);
                        this.Entity = serverEntity;
                        baseResultData.success = this.Edit();
                        baseResultData.data = editEntity.Id.ToString();
                        //机构货品修改记录
                        AddSCOperation(serverEntity, arrFields);
                    }
                }
                else
                {
                    editEntity.Visible = 1;
                    editEntity.DataAddTime = DateTime.Now;
                    editEntity.GoodsSort = goodsSort;
                    this.Entity = editEntity;
                    reaGoods = editEntity;
                    baseResultData.success = this.Add();
                    baseResultData.data = editEntity.Id.ToString();
                    goodsSort = goodsSort + 1;
                }
            }

            return baseResultData;
        }

        public string DataConvert(object dataColumnValue)
        {
            string resultStr = "";
            if (dataColumnValue != null)
                resultStr = dataColumnValue.ToString();
            return resultStr;
        }

        #endregion

    }
}