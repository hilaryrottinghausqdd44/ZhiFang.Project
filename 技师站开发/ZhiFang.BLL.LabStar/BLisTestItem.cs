using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLisTestItem : BaseBLL<LisTestItem>, ZhiFang.IBLL.LabStar.IBLisTestItem
    {
        IBLBDict IBLBDict { get; set; }

        IBLBItemRange IBLBItemRange { get; set; }

        IBLBItemRangeExp IBLBItemRangeExp { get; set; }

        public IList<LisTestItem> QueryLisTestItem(string strHqlWhere, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            return (DBDao as IDLisTestItemDao).QueryLisTestItemDao(strHqlWhere, listEntityName);
        }

        public EntityList<LisTestItem> QueryLisTestItem(string strHqlWhere, string order, int start, int count, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            listEntityName = LisCommonMethod.GetJoinEntityNameByOrderFields(listEntityName, ref order);
            return (DBDao as IDLisTestItemDao).QueryLisTestItemDao(strHqlWhere, order, start, count, listEntityName);
        }

        public EntityList<LisTestItem> QueryLisTestItem(string beginSampleNo, string endSampleNo, string strHqlWhere, string order, int start, int count, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            listEntityName = LisCommonMethod.GetJoinEntityNameByOrderFields(listEntityName, ref order);

            string strHqlSampleNo = "";
            if (!string.IsNullOrWhiteSpace(beginSampleNo))
                strHqlSampleNo = " and listestform.GSampleNoForOrder>=\'" + LisCommonMethod.DisposeSampleNo(beginSampleNo) + "\'";
            if (!string.IsNullOrWhiteSpace(endSampleNo))
                strHqlSampleNo += " and listestform.GSampleNoForOrder<=\'" + LisCommonMethod.DisposeSampleNo(endSampleNo) + "\'";
            if (!string.IsNullOrWhiteSpace(strHqlWhere))
                strHqlWhere += strHqlSampleNo;
            else if (!string.IsNullOrWhiteSpace(strHqlSampleNo))
                strHqlWhere = " 1=1 " + strHqlSampleNo;

            return (DBDao as IDLisTestItemDao).QueryLisTestItemDao(strHqlWhere, order, start, count, listEntityName);
        }

        public IList<LisTestItem> QueryLisTestItem(string strHqlWhere)
        {
            return (DBDao as IDLisTestItemDao).QueryLisTestItemDao(strHqlWhere);
        }

        public EntityList<LisTestItem> QueryLisTestItem(string strHqlWhere, string order, int start, int count)
        {
            return (DBDao as IDLisTestItemDao).QueryLisTestItemDao(strHqlWhere, order, start, count);
        }

        public bool QueryIsExistTestItemResult(long sectionID, long itemID)
        {
            string strWhere = " and listestitem.GTestDate>=\'" + DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + "\'";
            EntityList<LisTestItem> tempList = (this.DBDao as IDLisTestItemDao).QuerySectionItemResultDao(strWhere, sectionID, itemID, 1, 1);
            return (tempList != null && tempList.count > 0);
        }

        public string QueryCommonItemByTestFormID(string testFormIDList)
        {
            return (DBDao as IDLisTestItemDao).QueryCommonItemByTestFormIDDao(testFormIDList);
        }

        public LisTestItem AddLisTestCalcItem(LisTestForm lisTestForm, LisTestItem lisTestItem, LBItem calcItem)
        {
            LisTestItem newLisTestItem = new LisTestItem
            {
                LabID = lisTestForm.LabID,
                LisTestForm = lisTestForm,
                LBItem = calcItem,
                OrdersItemID = 0,
                ISource = 1
            };
            this.Entity = newLisTestItem;
            if (this.Add())
                return newLisTestItem;
            else
                return null;
        }

        public BaseResultDataValue EditLisTestItemResultByDilution(string testItemID, double? dilutionTimes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(testItemID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "没有选择要稀释的样本项目！";
                ZhiFang.LabStar.Common.LogHelp.Warn(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            if (dilutionTimes == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "稀释倍数不能为空！";
                ZhiFang.LabStar.Common.LogHelp.Warn(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            string[] array = testItemID.Split(',');
            string itemNameError = "";
            bool isError = false;
            foreach (string id in array)
            {
                LisTestItem testItem = this.Get(long.Parse(id));
                if (testItem != null)
                {
                    double reportValue = 0;
                    try
                    {
                        reportValue = double.Parse(testItem.ReportValue);
                    }
                    catch
                    {
                        isError = true;
                        if (itemNameError == "")
                            itemNameError = testItem.LBItem.CName + "-报告值【" + testItem.ReportValue + "】";
                        else
                            itemNameError += "," + testItem.LBItem.CName + "-报告值【" + testItem.ReportValue + "】";
                    }
                    testItem.QuanValue = reportValue * dilutionTimes;
                    testItem.ReportValue = LisCommonMethod.DisposeReportValueFormat(testItem.QuanValue.Value, testItem.LBItem.Prec);
                    this.Entity = testItem;
                    baseResultDataValue.success = this.Edit();
                }
            }
            if (isError)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "项目结果稀释失败：" + itemNameError;
                ZhiFang.LabStar.Common.LogHelp.Warn(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;

        }

        public BaseResultDataValue EditLisTestItemResultByOffset(string testItemOffsetInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (testItemOffsetInfo != null && testItemOffsetInfo.Trim().Length > 0)
            {
                JArray jsonArray = JArray.Parse(testItemOffsetInfo);
                if (jsonArray != null && jsonArray.Count > 0)
                {
                    double dbCoefficient = 1;
                    double dbAddValue = 0;
                    foreach (JObject jsonObject in jsonArray)
                    {
                        string lisTestItemID = jsonObject["LisTestItemID"] != null ? jsonObject["LisTestItemID"].ToString() : "";
                        string lbItemID = jsonObject["LBItemID"] != null ? jsonObject["LBItemID"].ToString() : "";
                        string coefficient = jsonObject["Coefficient"] != null ? jsonObject["Coefficient"].ToString() : "";
                        string addValue = jsonObject["AddValue"] != null ? jsonObject["AddValue"].ToString() : "";

                        if (coefficient == "" && addValue == "")
                        {
                            baseResultDataValue.success = false;
                            return baseResultDataValue;
                        }
                        if (coefficient != "")
                            dbCoefficient = double.Parse(coefficient);
                        if (addValue != "")
                            dbAddValue = double.Parse(addValue);
                        LisTestItem testItem = this.Get(long.Parse(lisTestItemID));
                        if (testItem != null && testItem.QuanValue != null)
                        {
                            testItem.QuanValue = testItem.QuanValue * dbCoefficient + dbAddValue;
                            testItem.ReportValue = LisCommonMethod.DisposeReportValueFormat(testItem.QuanValue.Value, testItem.LBItem.Prec);
                            this.Entity = testItem;
                            baseResultDataValue.success = this.Edit();
                        }
                    }
                }
            }
            return baseResultDataValue;

        }

        #region 参考值范围

        /// <summary>
        /// 编辑并获取项目参考值范围
        /// </summary>
        /// <param name="testForm"></param>
        /// <param name="listTestItem"></param>
        /// <returns></returns>
        public BaseResultDataValue EditLisTestItemReferenceValueRangeOld(LisTestForm testForm, ref IList<LisTestItem> listTestItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ZhiFang.LabStar.Common.LogHelp.Info("获取检验单项目参考值范围信息！");
            if (testForm != null)
            {
                if (testForm.MainStatusID == int.Parse(TestFormMainStatus.检验中.Key))
                {

                }
                if (listTestItem != null && listTestItem.Count > 0)
                {
                    IList<LBDict> listDict = IBLBDict.SearchListByHQL(" lbdict.DictType=\'ResultStatus\'");
                    listTestItem = listTestItem.Where(p => p.MainStatusID == int.Parse(TestItemMainStatus.正常.Key) || p.MainStatusID == int.Parse(TestItemMainStatus.复检.Key)).ToList();
                    for (int i = 0; i < listTestItem.Count; i++)
                    {
                        LisTestItem testItem = listTestItem[i];
                        double? testItemQuanValue = null;
                        if (testItem.QuanValue != null)
                            testItemQuanValue = testItem.QuanValue;
                        LBItemRange itemRange = null;
                        IList<LBItemRange> listItemRange = IBLBItemRange.SearchListByHQL(" lbitem.Id=" + testItem.LBItem.Id, new string[] { " lbitemrange.LBItem lbitem" });
                        IList<LBItemRangeExp> listItemRangeExp = IBLBItemRangeExp.SearchListByHQL(" (lbitemrangeexp.LBItem.Id=" + testItem.LBItem.Id + " or  lbitemrangeexp.LBItem is null)");
                        if (listItemRange != null && listItemRange.Count > 0)
                        {

                            IList<LBItemRange> tempList = listItemRange.Where(
                                p => (p.EquipID == null || testItem.EquipID == null || p.EquipID == testItem.EquipID)                      //仪器
                                && (p.SectionID == null || testForm.LBSection == null || p.SectionID == testForm.LBSection.Id)             //小组
                                && (p.SampleTypeID == null || testForm.GSampleTypeID == null || p.SampleTypeID == testForm.GSampleTypeID)  //样本类型
                                && (p.GenderID == null || testForm.GenderID == null || p.GenderID == testForm.GenderID)                    //性别  
                                && (p.AgeUnitID == null || testForm.AgeUnitID == null || p.AgeUnitID == testForm.AgeUnitID)                 //年龄单位
                                && (testForm.Age == null || ((p.LowAge == null || testForm.Age >= p.LowAge) && (p.HighAge == null || testForm.Age <= p.HighAge)))           //年龄
                                && (testForm.CollectTime == null || ((p.BCollectTime == null || testForm.CollectTime >= p.BCollectTime) && (p.ECollectTime == null || testForm.CollectTime <= p.ECollectTime)))  //采样时间                      //采样时间
                            ).OrderBy(p => p.DispOrder).ToList();

                            if (tempList == null || tempList.Count == 0)
                            {
                                ZhiFang.LabStar.Common.LogHelp.Info("匹配到检验项目【" + testItem.LBItem.CName + "】的参考值范围信息！");
                                tempList = listItemRange.Where(p => p.IsDefault == true).ToList();
                            }
                            if (tempList != null && tempList.Count > 0)
                            {
                                ZhiFang.LabStar.Common.LogHelp.Info("采用检验项目【" + testItem.LBItem.CName + "】的默认参考值范围信息！");
                                itemRange = listItemRange[0];
                            }
                            if (itemRange != null)
                            {
                                testItem.TestMethod = itemRange.DiagMethod; //检测方法,需要在结果后处理之后赋值（参考范围判断后得到）
                                testItem.Unit = itemRange.Unit; //结果单位,需要在结果后处理之后赋值（参考范围判断后得到）
                                testItem.RefRange = itemRange.RefRange; //参考范围,需要在结果后处理之后赋值（参考范围判断后得到）
                            }
                            else
                            {
                                ZhiFang.LabStar.Common.LogHelp.Info("匹配不到检验项目【" + testItem.LBItem.CName + "】的参考值范围信息！");
                            }
                        }
                        else
                        {
                            ZhiFang.LabStar.Common.LogHelp.Info("获取不到检验项目【" + testItem.LBItem.CName + "】的参考值范围信息！");
                        }
                        IBLBItemRange.EditItemResultStatus(itemRange, ref testItem, listDict, listItemRangeExp);
                        this.Entity = testItem;
                        this.Edit();
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取检验单项目信息！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取样本单信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 编辑并获取项目参考值范围
        /// </summary>
        /// <param name="testForm"></param>
        /// <param name="listTestItem"></param>
        /// <returns></returns>
        public BaseResultDataValue EditLisTestItemReferenceValueRange(IBLisTestForm IBLisTestForm, LisTestForm testForm, ref IList<LisTestItem> listTestItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ZhiFang.LabStar.Common.LogHelp.Info("获取检验单项目参考值范围信息！");
            if (testForm != null)
            {
                if (testForm.MainStatusID == int.Parse(TestFormMainStatus.检验中.Key))
                {
                    if (listTestItem != null && listTestItem.Count > 0)
                    {
                        int oldAlarmLevel = testForm.AlarmLevel;
                        //检验单警示级别设为正常
                        testForm.AlarmLevel = 0;
                        IList<LBDict> listDict = IBLBDict.SearchListByHQL(" lbdict.DictType=\'ResultStatus\'");
                        listTestItem = listTestItem.Where(p => p.MainStatusID == int.Parse(TestItemMainStatus.正常.Key) || p.MainStatusID == int.Parse(TestItemMainStatus.复检.Key)).ToList();
                        for (int i = 0; i < listTestItem.Count; i++)
                        {
                            LisTestItem testItem = listTestItem[i];
                            if (string.IsNullOrWhiteSpace(testItem.ReportValue))
                            {
                                testItem.ResultStatus = null;
                                testItem.ResultStatusCode = null;
                                testItem.ResultComment = null;
                                testItem.BAlarmColor = false;
                                testItem.AlarmColor = null;
                                continue;
                            }
                            double? testItemQuanValue = null;
                            if (testItem.QuanValue != null)
                                testItemQuanValue = testItem.QuanValue;
                            LBItemRange itemRange = null;
                            IList<LBItemRange> listItemRange = IBLBItemRange.SearchListByHQL(" lbitem.Id=" + testItem.LBItem.Id, new string[] { " lbitemrange.LBItem lbitem" });
                            IList<LBItemRangeExp> listItemRangeExp = IBLBItemRangeExp.SearchListByHQL(" (lbitemrangeexp.LBItem.Id=" + testItem.LBItem.Id + " or  lbitemrangeexp.LBItem is null)");
                            if (listItemRange != null && listItemRange.Count > 0)
                            {
                                if (testForm.Age >= 0 && testForm.AgeUnitID > 0)
                                {
                                    itemRange = GetLBItemRangeByItem(testForm, testItem, listItemRange);
                                }
                                else
                                {
                                    IList<LBItemRange> tempList = listItemRange.Where(
                                                p => (p.EquipID == null || p.EquipID == testItem.EquipID)                    //仪器
                                                && (p.SectionID == null || p.SectionID == testForm.LBSection.Id)             //小组
                                                && (p.SampleTypeID == null || p.SampleTypeID == testForm.GSampleTypeID)      //样本类型
                                                && (p.GenderID == null || p.GenderID == testForm.GenderID)                   //性别  
                                                && (p.AgeUnitID == null || p.AgeUnitID == testForm.AgeUnitID)                //年龄单位
                                                && ((p.LowAge == null || testForm.Age >= p.LowAge) && (p.HighAge == null || testForm.Age <= p.HighAge))           //年龄
                                                && ((p.BCollectTime == null || testForm.CollectTime >= p.BCollectTime) && (p.ECollectTime == null || testForm.CollectTime <= p.ECollectTime))  //采样时间
                                            ).OrderBy(p => p.DispOrder).ToList();

                                    if (tempList != null && tempList.Count > 0)
                                    {
                                        ZhiFang.LabStar.Common.LogHelp.Info("匹配到检验项目【" + testItem.LBItem.CName + "】的参考值范围信息！");
                                        itemRange = tempList[0];
                                    }
                                }
                                if (itemRange == null)
                                {
                                    IList<LBItemRange> tempList = listItemRange.Where(p => p.IsDefault == true).ToList();
                                    if (tempList != null && tempList.Count > 0)
                                    {
                                        ZhiFang.LabStar.Common.LogHelp.Info("采用检验项目【" + testItem.LBItem.CName + "】的默认参考值范围信息！");
                                        itemRange = listItemRange[0];
                                    }
                                }
                                if (itemRange != null)
                                {
                                    testItem.TestMethod = itemRange.DiagMethod; //检测方法,需要在结果后处理之后赋值（参考范围判断后得到）
                                    testItem.Unit = itemRange.Unit; //结果单位,需要在结果后处理之后赋值（参考范围判断后得到）
                                    testItem.RefRange = itemRange.RefRange; //参考范围,需要在结果后处理之后赋值（参考范围判断后得到）
                                }
                                else
                                {
                                    ZhiFang.LabStar.Common.LogHelp.Info("匹配不到检验项目【" + testItem.LBItem.CName + "】的参考值范围信息！");
                                }
                            }
                            else
                            {
                                ZhiFang.LabStar.Common.LogHelp.Info("获取不到检验项目【" + testItem.LBItem.CName + "】的参考值范围信息！");
                            }
                            IBLBItemRange.EditItemResultStatus(itemRange, ref testItem, listDict, listItemRangeExp);
                            if (testItem.AlarmLevel > testForm.AlarmLevel)
                                testForm.AlarmLevel = testItem.AlarmLevel;
                            this.Entity = testItem;
                            this.Edit();
                        }

                        if (oldAlarmLevel != testForm.AlarmLevel)
                        {
                            IBLisTestForm.Entity = testForm;
                            IBLisTestForm.Edit();
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "无法获取检验单项目信息！";
                        ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "检验单为【" + TestFormMainStatus.GetStatusDic()[testForm.MainStatusID.ToString()].Name + "】状态，不能计算参考值范围！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取样本单信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        private LBItemRange GetLBItemRangeByItem(LisTestForm testForm, LisTestItem testItem, IList<LBItemRange> listItemRange)
        {
            LBItemRange itemRange = null;
            double tempAge = (double)testForm.Age;
            //long curAgeUnitID = (long)testForm.AgeUnitID;
            long tempAgeUnitID = (long)testForm.AgeUnitID;
            while (tempAgeUnitID > 0 && tempAgeUnitID <= 5)
            {
                IList<LBItemRange> tempList = listItemRange.Where(
                            p => (p.EquipID == null || p.EquipID == testItem.EquipID)                    //仪器
                            && (p.SectionID == null || p.SectionID == testForm.LBSection.Id)             //小组
                            && (p.SampleTypeID == null || p.SampleTypeID == testForm.GSampleTypeID)      //样本类型
                            && (p.GenderID == null || p.GenderID == testForm.GenderID)                   //性别  
                            && (p.AgeUnitID == null || p.AgeUnitID == tempAgeUnitID)                     //年龄单位
                            && ((p.LowAge == null || tempAge >= p.LowAge) && (p.HighAge == null || tempAge <= p.HighAge))           //年龄
                            && ((p.BCollectTime == null || testForm.CollectTime >= p.BCollectTime) && (p.ECollectTime == null || testForm.CollectTime <= p.ECollectTime))  //采样时间
                        ).OrderBy(p => p.DispOrder).ToList();

                if (tempList == null || tempList.Count == 0 || (tempList.Where(p => p.AgeUnitID == tempAgeUnitID).ToList().Count == 0))
                {
                    tempAgeUnitID--;
                    if (tempAgeUnitID >= 1)
                    {
                        switch (tempAgeUnitID)
                        {
                            case 4://分钟转换为小时
                                tempAge = tempAge / 60;
                                break;
                            case 3://小时转换为天
                                tempAge = tempAge / 24;
                                break;
                            case 2://天转换为月
                                tempAge = tempAge / 30;
                                break;
                            case 1://月转换为年
                                tempAge = tempAge / 12;
                                break;
                        }
                    }
                }
                else
                {
                    itemRange = tempList[0];
                    ZhiFang.LabStar.Common.LogHelp.Info("匹配到检验项目【" + testItem.LBItem.CName + "】的参考值范围信息！");
                    break;
                }
            }// while

            return itemRange;
        }

        #endregion

    }
}