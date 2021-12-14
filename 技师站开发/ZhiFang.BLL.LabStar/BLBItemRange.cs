using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBItemRange : BaseBLL<LBItemRange>, ZhiFang.IBLL.LabStar.IBLBItemRange
    {

        public IBLBItemRangeExp IBLBItemRangeExp;

        /// <summary>
        /// 获取项目参考值设置
        /// </summary>
        /// <param name="testForm"></param>
        /// <param name="testItem"></param>
        /// <param name="listItemRange"></param>
        /// <returns></returns>
        public LBItemRange QueryItemRange(LisTestForm testForm, LisTestItem testItem, IList<LBItemRange> listItemRange)
        {
            LBItemRange itemRange = null;
            string strHQLWhere = "";
            long itemID = 0;
            if (testItem != null && testItem.LBItem != null)
                itemID = testItem.LBItem.Id;
            else
                return itemRange;
            if (listItemRange != null && listItemRange.Count > 0)
            {
                listItemRange = listItemRange.Where(p => p.LBItem.Id == itemID).ToList();
            }
            else
            {
                strHQLWhere = " lbitemrange.LBItem.Id=" + itemID;
                listItemRange = this.SearchListByHQL(strHQLWhere);
            }

            if (listItemRange != null && listItemRange.Count > 0)
            {
                IList<LBItemRange> tempList = listItemRange.Where(p => (p.EquipID == null || testItem.EquipID == null || p.EquipID == testItem.EquipID) &&
                (p.SectionID == null || p.SectionID == testForm.LBSection.Id) &&
                (p.SampleTypeID == null || testForm.GSampleTypeID == null || p.SampleTypeID == testForm.GSampleTypeID) &&
                (p.GenderID == null || testForm.GenderID == null || p.GenderID == testForm.GenderID) &&
                (p.AgeUnitID == null || testForm.AgeUnitID == null || p.AgeUnitID == testForm.AgeUnitID) &&
                ((testForm.Age == null || testForm.Age <= 0) || (p.LowAge == null || p.LowAge <= testForm.Age) && (p.HighAge == null || p.HighAge >= testForm.Age))
                ).OrderBy(p => p.DispOrder).ToList();
                if (tempList != null && tempList.Count > 0)
                    itemRange = tempList[0];
                if (itemRange == null)
                {
                    listItemRange = listItemRange.Where(p => p.IsDefault).ToList();
                    if (listItemRange != null && listItemRange.Count > 0)
                        itemRange = listItemRange[0];
                    else
                    {
                        listItemRange = listItemRange.OrderBy(p => p.DispOrder).ToList();
                        itemRange = listItemRange[0];
                    }
                }
            }
            return itemRange;
        }

        /// <summary>
        /// 编辑检验项目结果状态
        /// </summary>
        /// <param name="itemRange">项目参考值范围配置信息</param>
        /// <param name="testItem">检验项目</param>
        /// <param name="listDict">结果状态数据列表</param>
        public void EditItemResultStatus(LBItemRange itemRange, ref LisTestItem testItem, IList<LBDict> listDict, IList<LBItemRangeExp> listItemRangeExp)
        {
            if (testItem.LBItem == null)
            {
                ZhiFang.LabStar.Common.LogHelp.Info("检验项目实体为空，不能判断结果状态！");
                return;
            }
            string itemValueType = testItem.LBItem.ValueType.ToString();

            if (itemValueType == ResultValueType.定量.Key)
            {
                EditItemQuanValueStatus(itemRange, ref testItem, listDict, listItemRangeExp);
                //当检验项目复检标志处于未复检的状态(0)时，判断是否需要建议复检
                if (testItem.RedoStatus == 0)
                    EditItemResultIsRedo(itemRange, ref testItem);
            }
            else if (itemValueType == ResultValueType.定性.Key || itemValueType == ResultValueType.描述.Key)
            {
                if (testItem.QuanValue != null)//如果定量值不为空，按定量值判断参考范围
                {
                    EditItemQuanValueStatus(itemRange, ref testItem, listDict, listItemRangeExp);
                }
                EditItemReportResultStatusByAfterTreatment(ref testItem, listDict, listItemRangeExp);
            }
            //ResultValueType.图形.Key  图形暂时不做参考值范围判断
        }

        /// <summary>
        /// 编辑检验项目定量结果状态
        /// </summary>
        /// <param name="itemRange">项目参考值范围配置信息</param>
        /// <param name="testItem">检验项目</param>
        /// <param name="listDict">结果状态数据列表</param>
        /// <param name="listItemRangeExp">参考值范围配置信息列表</param>
        public void EditItemQuanValueStatus(LBItemRange itemRange, ref LisTestItem testItem, IList<LBDict> listDict, IList<LBItemRangeExp> listItemRangeExp)
        {
            bool isValidFlag = false;//结果异常值有效判断标志
            string resultStatusCode = "N";
            if (testItem.QuanValue == null)
                return;
            double itemRsult = (double)testItem.QuanValue;
            if (itemRange != null)
            {
                isValidFlag = ItemRsultNormalRangeCompare(itemRsult, itemRange);//判断项目结果是否在正常范围之内
                if (isValidFlag)
                {
                    testItem.ResultStatus = null;
                    testItem.ResultStatusCode = null;
                    testItem.ResultComment = null;
                }
                else
                {
                    if (itemRange.InvalidHValue != null)//无效高值
                    {
                        resultStatusCode = "U";
                        isValidFlag = ItemRsultCompare(itemRsult, itemRange.InvalidHValue, itemRange.InvalidHValueComp);
                        EditItemResultStatusByStatusCode(ref testItem, listDict, isValidFlag, resultStatusCode, "无效");
                    }

                    if ((!isValidFlag) && itemRange.InvalidLValue != null)//无效低值
                    {
                        resultStatusCode = "U";
                        isValidFlag = ItemRsultCompare(itemRsult, itemRange.InvalidLValue, itemRange.InvalidLValueComp);
                        EditItemResultStatusByStatusCode(ref testItem, listDict, isValidFlag, resultStatusCode, "无效");
                    }

                    if ((!isValidFlag) && itemRange.HHValue != null)//异常高值
                    {
                        resultStatusCode = "HH";
                        isValidFlag = ItemRsultCompare(itemRsult, itemRange.HHValue, itemRange.HHValueComp);
                        EditItemResultStatusByStatusCode(ref testItem, listDict, isValidFlag, "HH", "HH");
                        if (itemRange.BCritical)
                            testItem.AlarmLevel = int.Parse(TestFormReportValueAlarmLevel.危急.Key);
                    }

                    if ((!isValidFlag) && itemRange.LLValue != null)//异常低值
                    {
                        resultStatusCode = "LL";
                        isValidFlag = ItemRsultCompare(itemRsult, itemRange.LLValue, itemRange.LLValueComp);
                        EditItemResultStatusByStatusCode(ref testItem, listDict, isValidFlag, "LL", "LL");
                        if (itemRange.BCritical)
                            testItem.AlarmLevel = int.Parse(TestFormReportValueAlarmLevel.危急.Key);
                    }

                    if ((!isValidFlag) && itemRange.HValue != null)//范围高值
                    {
                        resultStatusCode = "H";
                        isValidFlag = ItemRsultCompare(itemRsult, itemRange.HValue, itemRange.HValueComp);
                        EditItemResultStatusByStatusCode(ref testItem, listDict, isValidFlag, "H", "H");
                    }

                    if ((!isValidFlag) && itemRange.LValue != null)//范围低值
                    {
                        resultStatusCode = "L";
                        isValidFlag = ItemRsultCompare(itemRsult, itemRange.LValue, itemRange.LValueComp);
                        EditItemResultStatusByStatusCode(ref testItem, listDict, isValidFlag, "L", "L");
                    }
                    if (!isValidFlag)
                        resultStatusCode = "N";
                }
            }
            //参考范围后处理
            EditItemQuanResultStatusByAfterTreatment(resultStatusCode, ref testItem, listDict, listItemRangeExp);
        }

        private void EditItemResultStatusByStatusCode(ref LisTestItem testItem, IList<LBDict> listDict, bool isValidFlag, string statusCode, string statusName)
        {
            if (isValidFlag)
            {
                IList<LBDict> tempList = listDict.Where(p => p.DictCode.ToUpper() == statusCode).ToList();
                if (tempList != null && tempList.Count > 0)
                {
                    testItem.ResultStatus = tempList[0].SName;//检验结果状态,需要在结果后处理之后赋值（参考范围判断后得到）
                    testItem.ResultStatusCode = tempList[0].DictCode;//检验结果状态代码,需要在结果后处理之后赋值（参考范围判断后得到）
                }
                else
                {
                    testItem.ResultStatus = statusName;
                    testItem.ResultStatusCode = statusCode;
                }
            }
        }

        /// <summary>
        /// 项目定量结果参考值范围后处理
        /// </summary>
        /// <param name="resultStatusCode">结果状态编码</param>
        /// <param name="testItem">检验单项目记录</param>
        /// <param name="listItemRangeExp">项目参考值范围处理配置信息</param>
        private void EditItemQuanResultStatusByAfterTreatment(string resultStatusCode, ref LisTestItem testItem, IList<LBDict> listDict, IList<LBItemRangeExp> listItemRangeExp)
        {
            if (listItemRangeExp == null || listItemRangeExp.Count == 0)
                return;
            IList<LBItemRangeExp> tempList = listItemRangeExp.Where(p => p.JudgeType == 0 && p.JudgeValue == resultStatusCode).ToList();
            if (tempList != null && tempList.Count > 0)
            {
                LBItemRangeExp itemRangeExp = tempList[0];
                if (itemRangeExp.ResultReport != null && itemRangeExp.ResultReport.Length > 0)
                {
                    if (itemRangeExp.IsAddReport)
                        testItem.ReportValue += itemRangeExp.ResultReport;//追加报告值
                    else
                        testItem.ReportValue = itemRangeExp.ResultReport;//替换报告值
                }
                if (itemRangeExp.ResultStatus != null && itemRangeExp.ResultStatus.Trim().Length > 0)
                {
                    LBDict dict = GetItemResultStatusByCode(itemRangeExp.ResultStatus, listDict);
                    if (dict != null)
                    {
                        testItem.ResultStatus = dict.SName;//替换结果状态
                        testItem.ResultStatusCode = itemRangeExp.ResultStatus;//替换结果状态编码
                    }
                }
                if (itemRangeExp.AlarmLevel != null)
                {
                    testItem.AlarmLevel = itemRangeExp.AlarmLevel.Value;
                }
                testItem.BAlarmColor = itemRangeExp.BAlarmColor;//采用特殊提示色,需要在结果后处理之后赋值（参考范围判断后得到）
                if (itemRangeExp.BAlarmColor)
                    testItem.AlarmColor = tempList[0].AlarmColor;
                testItem.ResultComment = itemRangeExp.ResultComment;
            }
        }

        /// <summary>
        /// 项目定性结果参考值范围后处理
        /// </summary>
        /// <param name="testItem">检验单项目记录</param>
        /// <param name="listItemRangeExp">项目参考值范围处理配置信息</param>
        private void EditItemReportResultStatusByAfterTreatment(ref LisTestItem testItem, IList<LBDict> listDict, IList<LBItemRangeExp> listItemRangeExp)
        {
            if (listItemRangeExp == null || listItemRangeExp.Count == 0)
                return;
            bool isValidFlag = false;
            long itemID = testItem.LBItem.Id;
            IList<LBItemRangeExp> tempList = listItemRangeExp.Where(p => p.JudgeType == 1 && p.LBItem != null && p.LBItem.Id == itemID).OrderBy(p => p.DispOrder).ToList();//DispOrder按判定次序排序
            if (tempList != null && tempList.Count > 0)
            {
                isValidFlag = EditItemReportValueStatus(ref testItem, listDict, tempList);
            }
            if (!isValidFlag)
            {
                tempList = listItemRangeExp.Where(p => p.LBItem is null && p.JudgeType == 1).OrderBy(p => p.DispOrder).ToList();//DispOrder按判定次序排序
                if (tempList != null && tempList.Count > 0)
                    isValidFlag = EditItemReportValueStatus(ref testItem, listDict, tempList);
            }
        }

        private bool EditItemReportValueStatus(ref LisTestItem testItem, IList<LBDict> listDict, IList<LBItemRangeExp> tempList)
        {
            bool isValidFlag = false;
            string reportVlaue = testItem.ReportValue;
            if (reportVlaue == null || reportVlaue.Trim().Length == 0)
                return isValidFlag;
            foreach (LBItemRangeExp itemRangeExp in tempList)
            {
                string judgeValue = itemRangeExp.JudgeValue;
                if (judgeValue == null || judgeValue.Trim().Length == 0)
                    continue;
                judgeValue = judgeValue.Replace("；", ";");
                string[] arrayJudgeValue = judgeValue.Split(';');
                foreach (string strJudgeValue in arrayJudgeValue)
                {
                    if (strJudgeValue == null || strJudgeValue.Trim().Length == 0)
                        continue;
                    if (strJudgeValue.IndexOf("%") >= 0)
                        isValidFlag = (reportVlaue.IndexOf(strJudgeValue.Replace("%", "")) >= 0);
                    else
                        isValidFlag = (strJudgeValue == reportVlaue);
                    if (isValidFlag)
                        break;
                }
                if (isValidFlag)
                {
                    if (itemRangeExp.ResultReport != null && itemRangeExp.ResultReport.Length > 0)
                    {
                        if (itemRangeExp.IsAddReport)
                            testItem.ReportValue += itemRangeExp.ResultReport;//追加报告值
                        else
                            testItem.ReportValue = itemRangeExp.ResultReport;//替换报告值
                    }
                    if (itemRangeExp.ResultStatus != null && itemRangeExp.ResultStatus.Trim().Length > 0)
                    {
                        LBDict dict = GetItemResultStatusByCode(itemRangeExp.ResultStatus, listDict);
                        if (dict != null)
                        {
                            testItem.ResultStatus = dict.SName;//替换结果状态
                            testItem.ResultStatusCode = itemRangeExp.ResultStatus;//替换结果状态编码
                        }
                    }
                    testItem.BAlarmColor = itemRangeExp.BAlarmColor;//采用特殊提示色,需要在结果后处理之后赋值（参考范围判断后得到）
                    testItem.ResultComment = itemRangeExp.ResultComment;
                    if (itemRangeExp.BAlarmColor)
                        testItem.AlarmColor = tempList[0].AlarmColor;
                    break;
                }
            }
            return isValidFlag;
        }

        public void EditItemResultIsRedo(LBItemRange itemRange, ref LisTestItem testItem)
        {
            bool isValidFlag = false;//结果异常值有效判断标志
            if (itemRange == null)
                return;

            if (testItem.QuanValue == null)
                return;
            double itemRsult = (double)testItem.QuanValue;

            if (itemRange.RedoHValue != null)//复检高值
            {
                isValidFlag = ItemRsultCompare(itemRsult, itemRange.RedoHValue, itemRange.RedoHValueComp);
            }

            if ((!isValidFlag) && itemRange.RedoLValue != null)//复检低值
            {
                isValidFlag = ItemRsultCompare(itemRsult, itemRange.RedoLValue, itemRange.RedoLValueComp);
            }
            if (isValidFlag && testItem.RedoStatus == 0)
            {
                testItem.RedoStatus = 2;                 
                testItem.RedoDesc = "建议复检";
            }
        }

        /// <summary>
        /// 项目定量结果值参考范围值比较
        /// </summary>
        /// <param name="itemRsult">定量结果值</param>
        /// <param name="compareValue">比较运算符字符串：<,>,<=,>=等</param>
        /// <param name="compareChar">参考范围值</param>
        /// <returns>bool</returns>
        private bool ItemRsultCompare(double itemRsult, double? compareValue, string compareChar)
        {
            bool isValidFlag = false;
            if (compareValue != null)
            {
                string strExpression = itemRsult + compareChar + compareValue;
                string strBoolValue = LisCommonMethod.CalcFormulaByJScript(strExpression);
                bool.TryParse(strBoolValue, out isValidFlag);
            }
            return isValidFlag;
        }

        /// <summary>
        /// 项目定量结果值是否在正常范围（高低值都存在）之内
        /// </summary>
        /// <param name="itemRsult">定量结果值</param>
        /// <param name="compareValue">比较运算符字符串：<,>,<=,>=等</param>
        /// <param name="compareChar">参考范围值</param>
        /// <returns>bool</returns>
        private bool ItemRsultNormalRangeCompare(double itemRsult, LBItemRange itemRange)
        {
            bool isValidFlag = false;
            if (itemRange.LValue != null && itemRange.HValue != null)
            {
                string tempHValueComp = "<=";
                if (itemRange.HValueComp.IndexOf(">=") >= 0)
                    tempHValueComp = "<";
                string strExpression = itemRange.LValue + itemRange.LValueComp + itemRsult + " && " + itemRsult + tempHValueComp + itemRange.HValue;
                string strBoolValue = LisCommonMethod.CalcFormulaByJScript(strExpression);
                bool.TryParse(strBoolValue, out isValidFlag);
            }
            return isValidFlag;
        }

        /// <summary>
        /// 项目定量结果参考值范围后处理
        /// </summary>
        /// <param name="resultStatusCode">结果状态编码</param>
        /// <param name="testItem">检验单项目记录</param>
        /// <param name="listItemRangeExp">项目参考值范围处理配置信息</param>
        private LBDict GetItemResultStatusByCode(string resultStatusCode, IList<LBDict> listDict)
        {
            LBDict dict = null;
            if (listDict == null || listDict.Count == 0)
                return dict;
            IList<LBDict> tempList = listDict.Where(p => p.DictCode == resultStatusCode).ToList();
            if (tempList != null && tempList.Count > 0)
                dict = tempList[0];
            return dict;
        }

        public IList<Line> QueryItemRangeChartLinePoint(LisTestForm testForm, IList<LisTestItem> listLisTestItem, IList<LBItemRange> listItemRange)
        {
            IList<Line> listLine = new List<Line>();
            if (listLisTestItem != null && listLisTestItem.Count > 0)
            {
                IList<Point> listPointValue = new List<Point>();
                IList<Point> listPointRangeL = new List<Point>();
                IList<Point> listPointRangeH = new List<Point>();
                IList<Point> listPointYCRangeL = new List<Point>();
                IList<Point> listPointYCRangeH = new List<Point>();
                foreach (LisTestItem testItem in listLisTestItem)
                {
                    string itemID = testItem.LBItem.Id.ToString();
                    Point pointValue = new Point();
                    Point pointRangeL = new Point();
                    Point pointRangeH = new Point();
                    Point pointYCRangeL = new Point();
                    Point pointYCRangeH = new Point();
                    LBItemRange itemRange = QueryItemRange(testForm, testItem, listItemRange);
                    if (itemRange != null)
                    {
                        //pointValue.PointName = "";
                        pointValue.X = itemID;
                        pointValue.Y = testItem.ReportValue;
                        //pointValue.IsUse = true;
                        listPointValue.Add(pointValue);
                        if (itemRange.LValue != null)
                        {
                            //pointRangeL.PointName = "";
                            pointRangeL.X = itemID;
                            pointRangeL.Y = itemRange.LValue.ToString();
                            //pointRangeL.IsUse = true;
                            listPointRangeL.Add(pointRangeL);
                        }
                        if (itemRange.HValue != null)
                        {
                            //pointRangeH.PointName = "";
                            pointRangeH.X = itemID;
                            pointRangeH.Y = itemRange.HValue.ToString();
                            //pointRangeH.IsUse = true;
                            listPointRangeH.Add(pointRangeH);
                        }
                        if (itemRange.LLValue != null)
                        {
                            //pointYCRangeL.PointName = "";
                            pointYCRangeL.X = itemID;
                            pointYCRangeL.Y = itemRange.LLValue.ToString();
                            //pointYCRangeL.IsUse = true;
                            listPointYCRangeL.Add(pointYCRangeL);
                        }
                        if (itemRange.HHValue != null)
                        {
                            //pointYCRangeH.PointName = "";
                            pointYCRangeH.X = itemID;
                            pointYCRangeH.Y = itemRange.HHValue.ToString();
                            //pointYCRangeH.IsUse = true;
                            listPointYCRangeH.Add(pointYCRangeH);
                        }
                    }
                }//foreach  
                if (listPointValue.Count > 0)
                {
                    Line line = new Line();
                    line.LineName = "PointValue";
                    line.LineCode = "10";
                    line.LinePoint = listPointValue;
                    line.IsUse = true;
                    listLine.Add(line);
                }
                if (listPointRangeL.Count > 0)
                {
                    Line line = new Line();
                    line.LineName = "PointRangeL";
                    line.LineCode = "20";
                    line.LinePoint = listPointRangeL;
                    line.IsUse = true;
                    listLine.Add(line);
                }
                if (listPointRangeH.Count > 0)
                {
                    Line line = new Line();
                    line.LineName = "PointRangeH";
                    line.LineCode = "21";
                    line.LinePoint = listPointRangeH;
                    line.IsUse = true;
                    listLine.Add(line);
                }
                if (listPointYCRangeL.Count > 0)
                {
                    Line line = new Line();
                    line.LineName = "PointYCRangeL";
                    line.LineCode = "30";
                    line.LinePoint = listPointYCRangeL;
                    line.IsUse = true;
                    listLine.Add(line);
                }
                if (listPointYCRangeH.Count > 0)
                {
                    Line line = new Line();
                    line.LinePoint = listPointValue;
                    line.LineName = "PointYCRangeH";
                    line.LineCode = "31";
                    line.LinePoint = listPointYCRangeH;
                    line.IsUse = true;
                    listLine.Add(line);
                }
            }
            return listLine;
        }

    }
}