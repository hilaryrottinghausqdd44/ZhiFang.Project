using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class LisCommonMethod
    {
        /// <summary>
        /// 获取字符串中满足指定格式的字符串的列表
        /// </summary>
        /// <param name="strSource">源字符串</param>
        /// <param name="pattern">某种指定的格式</param>
        /// <returns>字符串列表</returns>
        public static IList<string> GetContentByExpression(string strSource, string pattern)
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

        #region 计算公式
        public static string CalcFormulaByJScript(string expression)
        {
            Microsoft.JScript.Vsa.VsaEngine ve = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();
            object returnValue = Microsoft.JScript.Eval.JScriptEvaluate(expression, ve);
            return returnValue.ToString();
        }

        public static string ItemFormulaCalc(string strFormula)
        {
            StringBuilder strCalcFormula = new StringBuilder();
            string func_Sqr = "function sqr(x){ return x*x;};";
            string func_log10 = "function log10(x){ return Math.log(x)/Math.log(10);};";
            string func_logn = "function logn(x,y){ return Math.log(y)/Math.log(x);};";
            strCalcFormula.Append(strFormula.ToLower());
            strCalcFormula = strCalcFormula.Replace("abs", "Math.abs")
                .Replace("exp", "Math.exp")
                .Replace("ln", "Math.log")
                .Replace("sqrt", "Math.sqrt")
                .Replace("power", "Math.pow")
                .Replace("min", "Math.min")
                .Replace("max", "Math.max");
            return CalcFormulaByJScript(func_Sqr + func_log10 + func_logn + strCalcFormula.ToString());
        }

        #endregion

        /// <summary>
        /// 判断计算条件公式是否为定量计算
        /// </summary>
        /// <param name="formulaCondition">计算条件公式字符串</param>
        /// <returns></returns>
        public static bool JudgeFormulaConditionIsQuantity(string formulaCondition)
        {
            char[] array = { '>', '<', '=' };
            return (formulaCondition.IndexOfAny(array) >= 0);
        }

        #region 计报告结果处理-报告值转定量值

        /// <summary>
        /// 根据报告值返回定量值
        /// </summary>
        /// <param name="strReportValue">报告值</param>
        /// <returns></returns>
        public static string DisposeReportValue(string strReportValue)
        {
            string[] resultArray = new string[] { "", "" };
            try
            {
                int decimalBit = 2;
                StringBuilder reportValue = new StringBuilder(strReportValue.Trim());
                reportValue = DisposeSpecialChar(reportValue);
                string[] arrayRV = DisposeReportValueString(reportValue.ToString(), decimalBit);
                return arrayRV[1];
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("报告值【" + strReportValue + "】处理失败：" + ex.Message);
                return "";
            }
        }

        /// <summary>
        /// 根据报告值返回处理过的报告值和定量值
        /// </summary>
        /// <param name="strReportValue"></param>
        /// <param name="decimalBit"></param>
        /// <returns></returns>
        public static string[] DisposeReportValue(string strReportValue, int decimalBit)
        {
            string[] resultArray = new string[] { "", "" };
            try
            {
                StringBuilder reportValue = new StringBuilder(strReportValue.Trim());
                reportValue = DisposeSpecialChar(reportValue);
                return DisposeReportValueString(reportValue.ToString(), decimalBit);
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("报告值【" + strReportValue + "】处理失败：" + ex.Message);
                return resultArray;
            }
        }

        private static string[] DisposeReportValueString(string strSource, int decimalBit)
        {
            char[] arraySpecialChar = { '*', '^', ':', '/', 'E', 'e', '+', '-' };
            string[] resultArray = new string[] { "", "" };
            string tempSource = strSource.Trim();
            if (!IsContainsNumer(tempSource))//字符串不包含数值
            {
                resultArray[0] = tempSource;
                return resultArray;
            }
            if (IsAllNumber(tempSource))//字符串是否为全部数值
            {
                resultArray[0] = DisposeReportValueFormat(double.Parse(tempSource), decimalBit);
                resultArray[1] = tempSource;
                return resultArray;
            }

            StringBuilder sbNumber = new StringBuilder();
            bool isNumberChar = false;//是否为数字字符
            bool isSpaceChar = false;//是否为空格
            bool isSpecialChar = false;//是否为特殊字符
            bool isOtherChar = false;//是否为其他普通字符
            for (int i = 0; i < tempSource.Length; i++)
            {
                if ((isNumberChar && isOtherChar))
                    break;
                if (char.IsDigit(tempSource[i]) || (tempSource[i] == '.' && isNumberChar && sbNumber.ToString().IndexOf(".") < 0)) //判断字符是否为数字
                {
                    if (isSpaceChar && sbNumber.Length > 0 && (!isSpecialChar))
                        break;
                    sbNumber.Append(tempSource[i]);
                    isNumberChar = true;
                    isSpaceChar = false;
                    isSpecialChar = false;
                    isOtherChar = false;
                }
                else if (tempSource[i] == ' ')
                {
                    isSpaceChar = true;
                    //isSpecialChar = false;
                    isOtherChar = false;
                }
                else if (arraySpecialChar.Contains(tempSource[i]))
                {
                    if (tempSource[i] != '-' && sbNumber.Length > 0) //防止出现E3、*3等出现
                    {
                        sbNumber.Append(tempSource[i]);
                    }
                    //isSpaceChar = false;
                    isSpecialChar = true;
                    isOtherChar = false;
                }
                else
                {
                    //isSpaceChar = false;
                    isSpecialChar = false;
                    isOtherChar = true;
                }
            }

            if (sbNumber.Length > 0)
            {
                string strNumber = sbNumber.ToString();
                //去掉数字后面单独一个小数点（3.或5.等）
                if ((strNumber[strNumber.Length - 1] == '.' || arraySpecialChar.Contains(strNumber[strNumber.Length - 1]))
                    && char.IsDigit(tempSource[strNumber.Length - 2]))
                    strNumber = strNumber.Substring(0, strNumber.Length - 1);

                if (IsAllNumber(strNumber))//字符串是否为全部数值
                {
                    resultArray[0] = tempSource.Replace(strNumber, DisposeReportValueFormat(double.Parse(strNumber), decimalBit));
                    resultArray[1] = strNumber;
                }
                else
                {
                    resultArray[0] = tempSource;
                    resultArray[1] = DisposeString(strNumber);
                }
            }
            return resultArray;
        }

        //报告结果设置小数位数
        public static string DisposeReportValueFormat(double reportValue, int decimalCount)
        {
            string strZero = "";
            if (decimalCount < 0)
                decimalCount = 2;
            if (decimalCount > 0)
                strZero = "0." + strZero.PadRight(decimalCount, '0');
            else
                strZero = "0";
            return reportValue.ToString(strZero);
        }

        /// <summary>
        /// 替换报告值中的特定字符
        /// </summary>
        /// <param name="reportValue">报告值</param>
        /// <returns></returns>
        private static StringBuilder DisposeSpecialChar(StringBuilder reportValue)
        {
            reportValue = reportValue.Replace("\t", " ").Replace("\r", " ").Replace("\n", " ").Replace("：", ":").Replace("。", ".");
            return reportValue;
        }

        private static string DisposeString(string strSource)
        {
            string tempStr = strSource;
            if (!IsContainsNumer(tempStr))//字符串不包含数值
            {
                return "";
            }
            if (IsAllNumber(tempStr))//字符串是否为全部数值
            {
                return tempStr;
            }
            if (tempStr.IndexOf(":") > 0)
                tempStr = tempStr.Replace(":", "/"); ;
            if (tempStr.IndexOf("/") > 0)
            {
                string str = tempStr.Replace("/", "");
                if (IsAllNumber(str))
                    return ItemFormulaCalc(tempStr);
                else
                    return "";
            }
            if (tempStr.IndexOf("E") > 0 || tempStr.IndexOf("e") > 0)
            {
                string str = tempStr.Replace("E", "").Replace("e", "").Replace("+", "").Replace("-", "");
                if (IsAllNumber(str))
                {
                    string strE = strSource.Replace("E", "*10^").Replace("e", "*10^");
                    string[] array = strE.Split('*');
                    return ItemFormulaCalc(array[0] + "*power(" + array[1].Replace("^", ",") + ")");
                }
                else
                    return "";
            }
            if (tempStr.IndexOf("*") > 0 && tempStr.IndexOf("^") > 0)
            {
                string str = tempStr.Replace("*", "").Replace("^", "");
                if (IsAllNumber(str))
                {
                    string[] array = strSource.Split('*');
                    return ItemFormulaCalc(array[0] + "*power(" + array[1].Replace("^", ",") + ")");
                }
                else
                    return "";
            }
            return "";
        }

        private static string DisposeStringArray(string strSource)
        {
            string[] array = strSource.Split(' ');
            if (array != null && array.Length > 0)
                return array[0];
            else
                return strSource;
        }

        //处理字符串特殊计算字符（* ^ / :等）的前后空格
        private static string DisposeSpecialCalcChar(string strSource)
        {
            string tempStr = strSource;
            int length = tempStr.Length;
            string[] array = { "*", "^", ":", "/", "E", "e", "+", "-" };
            foreach (string str in array)
            {
                int index = tempStr.IndexOf(str);
                if (index < 0)
                    continue;
                int startIndex = -1;
                int endIndex = -1;
                for (int i = index + 1; i < length; i++)
                {
                    if (tempStr[i] != ' ')
                    {
                        startIndex = index + 1;
                        endIndex = i;
                        break;
                    }
                }
                if (startIndex < endIndex)
                {
                    tempStr = tempStr.Remove(startIndex, endIndex - startIndex);
                }

                for (int i = index - 1; i >= 0; i--)
                {
                    if (tempStr[i] != ' ')
                    {
                        startIndex = i + 1;
                        endIndex = index;
                        break;
                    }
                }
                if (startIndex < endIndex)
                {
                    tempStr = tempStr.Remove(startIndex, endIndex - startIndex);
                }
            }
            return tempStr;
        }

        //判断字符串是否包含数字字符（0-9）
        private static bool IsContainsNumer(string strSource)
        {
            foreach (char c in strSource)
            {
                if (Char.IsNumber(c))
                {
                    return true;
                }
            }
            return false;
        }

        //判断字符串是否全部是数字字符
        private static bool IsAllNumber(string strSource)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(strSource) &&
                   !objTwoDotPattern.IsMatch(strSource) &&
                   !objTwoMinusPattern.IsMatch(strSource) &&
                   objNumberPattern.IsMatch(strSource);
        }
        #endregion

        #region 样本号补位处理

        /// <summary>
        /// 样本号补位处理定制方法
        /// </summary>
        /// <param name="sampleNo">要补位的样本号</param>
        /// <returns>返回补位样本号</returns>
        public static string DisposeSampleNo(string sampleNo)
        {
            return DisposeSampleNo(sampleNo, '-', '0', 20);
        }

        /// <summary>
        /// 样本号补位处理
        /// </summary>
        /// <param name="sampleNo">要补位的样本号</param>
        /// <param name="splitChar">分隔符，例如：-</param>
        /// <param name="coveringChar">补位字符，例如：0</param>
        /// <param name="strLength">补位后的位数，例如：20</param>
        /// <returns></returns>
        public static string DisposeSampleNo(string sampleNo, char splitChar, char coveringChar, int strLength)
        {
            if (IsZeroToNineNumber(sampleNo))
            {
                int length = sampleNo.Length;
                if (strLength > length)
                {
                    sampleNo = sampleNo.PadLeft((strLength), '0');
                }
            }
            else
            {
                sampleNo = sampleNo.Replace('_', splitChar);
                if (sampleNo.IndexOf("-") >= 0)
                {
                    sampleNo = StringSplit(sampleNo, splitChar, coveringChar, strLength);
                }
                else
                {
                    sampleNo = StringCovering(sampleNo, coveringChar, strLength);
                }
            }
            return sampleNo;
        }

        //判断字符串全部是0-9数字字符
        private static bool IsZeroToNineNumber(string strSource)
        {
            Regex objNumberPattern = new Regex("[^0-9]");
            return !objNumberPattern.IsMatch(strSource);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="splitChar"></param>
        /// <param name="coveringChar"></param>
        /// <param name="strLength"></param>
        /// <returns></returns>
        private static string StringSplit(string strSource, char splitChar, char coveringChar, int strLength)
        {
            string[] array = strSource.Split(splitChar);
            int lenth = array.Length;

            array[0] = StringCovering(array[0], coveringChar, strLength);
            array[lenth - 1] = StringCovering(array[lenth - 1], coveringChar, strLength);
            string result = array[0];
            for (int i = 1; i < lenth; i++)
            {
                result += splitChar + array[i];
            }
            return result;
        }

        /// <summary>
        /// 字符串补位
        /// </summary>
        /// <param name="strSource">源字符串</param>
        /// <param name="coveringChar">补位字符</param>
        /// <param name="strLength">补位后字符串长度</param>
        /// <returns>补位后字符串</returns>
        private static string StringCovering(string strSource, char coveringChar, int strLength)
        {
            bool isAllNum = true;
            string strOne = "";
            string strTwo = "";
            for (int i = strSource.Length - 1; i >= 0; i--)
            {
                char c = strSource[i];
                if (!Char.IsNumber(c))
                {
                    strOne = strSource.Substring(0, i + 1);
                    strTwo = strSource.Substring(i + 1, strSource.Length - i - 1);
                    isAllNum = false;
                    break;
                }
            }
            if (isAllNum)
            {
                strOne = "";
                strTwo = strSource;
            }
            if (string.IsNullOrEmpty(strTwo))
                return strOne.PadRight((strLength), coveringChar);
            else
                return strOne + strTwo.PadLeft((strLength - strOne.Length), coveringChar);
        }

        #endregion

        #region 根据初始样本号获取可以自动增长的初始数值样本号
        public static void GetIntSampleNo(string sampleNo, ref string headSampleNo, ref int intSampleNo, ref int numLength)
        {
            headSampleNo = "";
            if (IsZeroToNineNumber(sampleNo))
            {
                numLength = sampleNo.Length;
                intSampleNo = int.Parse(sampleNo);
            }
            else
            {
                for (int i = sampleNo.Length - 1; i >= 0; i--)
                {
                    char c = sampleNo[i];
                    if (!Char.IsNumber(c))
                    {
                        headSampleNo = sampleNo.Substring(0, i + 1);
                        string tempSampleNo = sampleNo.Substring(i + 1, sampleNo.Length - i - 1);
                        numLength = tempSampleNo.Length;
                        intSampleNo = int.Parse(tempSampleNo);
                        break;
                    }
                }
            }
        }

        public static string GetNewSampleNo(string headSampleNo, int intSampleNo, int numLength)
        {
            string sampleNo = intSampleNo.ToString();
            int lenth = sampleNo.Length;
            if (lenth >= numLength)
                sampleNo = headSampleNo + sampleNo;
            else
                sampleNo = headSampleNo + sampleNo.PadLeft(numLength, '0');
            return sampleNo;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IList<string> GetJoinEntityNameByFields(string fields)
        {
            IList<string> listHQLEntityName = new List<string>();
            if (string.IsNullOrEmpty(fields))
                return listHQLEntityName;
            string[] array = fields.Split(',');
            foreach (string field in array)
            {
                string[] arrayMember = field.Split('_');
                int count = arrayMember.Length - 1;
                if (count > 1)
                {
                    for (int i = 1; i < count; i++)
                    {
                        string strHQLEntityName = arrayMember[i - 1].ToLower() + "." + arrayMember[i] + " " + arrayMember[i].ToLower();
                        if (!listHQLEntityName.Contains(strHQLEntityName))
                            listHQLEntityName.Add(strHQLEntityName);
                    }
                }
            }
            return listHQLEntityName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IList<string> GetJoinEntityNameByOrderFields(IList<string> listHQLEntityName, ref string orderFields)
        {
            if (listHQLEntityName == null)
                listHQLEntityName = new List<string>();
            if (orderFields != null && orderFields.Trim().Length > 0)
            {
                string tempOrderFields = "";
                string[] array = orderFields.Split(',');
                foreach (string field in array)
                {
                    string[] arrayMember = field.Split('.');
                    int count = arrayMember.Length - 1;
                    if (count > 1)
                    {
                        for (int i = 1; i < count; i++)
                        {
                            string strHQLEntityName = arrayMember[i - 1].ToLower() + "." + arrayMember[i] + " " + arrayMember[i].ToLower();
                            if (!listHQLEntityName.Contains(strHQLEntityName))
                                listHQLEntityName.Add(strHQLEntityName);
                            if ((i + 1) == count)
                            {
                                tempOrderFields += "," + arrayMember[i].ToLower() + "." + arrayMember[i + 1];
                            }
                        }
                    }
                    else
                        tempOrderFields += "," + field;
                }
                if (tempOrderFields.Length > 0)
                {
                    orderFields = tempOrderFields.Remove(0, 1);
                }
            }
            return listHQLEntityName;
        }

        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="birthday">出生日期</param>
        public static string GetAge(string birthday, string curDateTime)
        {
            #region 变量初始化
            int intYearBorder = 3;       //显示年龄单位岁的边界值，>=3岁显示多少岁
            int intYearMonthBorder = 1;  //显示年龄单位几岁几月的边界值，>=1岁和<3岁显示几岁几月
            int intMonthBorder = 3;      //显示年龄月的边界值，>=3个月和<1岁显示多少月          
            int intMonthDayBorder = 1;   //显示年龄几月几天的边界值，天数>=1天和月数>=3个月显示几月几天
            int intDayBorder = 3;        //显示年龄天的边界值，天数>=3天和月数<3个月显示多少天
            int intHourBorder = 1;       //显示年龄小时的边界值，>=1小时和<3天显示多少小时

            int intYear = 0;    // 岁
            int intMonth = 0;   // 月
            int intDay = 0;     // 天
            int intHour = 0;    //小时
            int intMin = 0;     //分钟

            int age = 0;
            int ageUnitID = 1;
            string ageUnitName = ""; //年龄单位
            string ageDesc = "";     //年龄备注

            DateTime dtBirthday = DateTime.Parse(birthday);
            DateTime dtNow = DateTime.Now;
            if (!string.IsNullOrEmpty(curDateTime))
                dtNow = DateTime.Parse(curDateTime);
            #endregion

            if (dtBirthday > dtNow)
                return "{\"Age\":-1\",\"AgeUnitID\":\"-1\",\"AgeUnitName\":\"-1\",\"AgeDesc\":\"-1\"}";

            #region 年龄计算
            //用于天数判断
            int totalDefDay = (dtNow - dtBirthday).Days;

            // 计算分钟
            intMin = dtNow.Minute - dtBirthday.Minute;
            if (intMin < 0)
            {
                intMin += 60;
            }

            // 计算小时
            intHour = dtNow.Hour - dtBirthday.Hour;
            if (intHour < 0)
            {
                intHour += 24;
                dtNow = dtNow.AddDays(-1);
            }

            // 计算天数
            intDay = dtNow.Day - dtBirthday.Day;
            if (intDay < 0) //如果当前日期某日的数字小于出生日期某日的数字，当前日期月份减一
            {
                dtNow = dtNow.AddMonths(-1);
                intDay += DateTime.DaysInMonth(dtNow.Year, dtNow.Month);
            }

            // 计算月数
            intMonth = dtNow.Month - dtBirthday.Month;
            if (intMonth < 0)//如果当前日期某月的数字小于出生日期某月的数字，当前日期年份减一
            {
                intMonth += 12;
                dtNow = dtNow.AddYears(-1);
            }
            // 计算年数
            intYear = dtNow.Year - dtBirthday.Year;
            #endregion

            #region 格式化年龄输出
            if (intYear >= intYearMonthBorder)
            {
                age = intYear;
                ageUnitID = int.Parse(AgeUnitType.岁.Key);
                ageUnitName = AgeUnitType.岁.Value.Name;
                if (intYear >= intYearBorder) //年龄>=intYearBorder岁的以岁为单位  
                    ageDesc = age.ToString() + ageUnitName;
                else //如果年龄在intYearMonthBorder-intYearBorder岁之间，描述为*岁*月
                {
                    if (intMonth > 0)
                        ageDesc = intYear.ToString() + AgeUnitType.岁.Value.Name + intMonth.ToString() + AgeUnitType.月.Value.Name;
                    else
                        ageDesc = intYear.ToString() + AgeUnitType.岁.Value.Name;
                }
            }
            else if (intMonth >= intMonthBorder)//年龄>=intMonthBorder个月且<intYearMonthBorder岁的以月计算
            {
                age = intYear * 12 + intMonth;
                ageUnitID = int.Parse(AgeUnitType.月.Key);
                ageUnitName = AgeUnitType.月.Value.Name;
                if (intDay >= intMonthDayBorder)
                    ageDesc = age.ToString() + AgeUnitType.月.Value.Name + intDay.ToString() + AgeUnitType.天.Value.Name;
                else
                    ageDesc = age.ToString() + ageUnitName;
            }
            else if (totalDefDay >= intDayBorder) //年龄>=intDayBorder天且小于<intMonthBorder个月的以天计算
            {
                age = totalDefDay;
                ageUnitID = int.Parse(AgeUnitType.天.Key);
                ageUnitName = AgeUnitType.天.Value.Name;
                ageDesc = age.ToString() + ageUnitName;
            }
            else if (intYear == 0 && intMonth == 0 && totalDefDay >= 1)//年龄>=intHourBorder小时且<intDayBorder天的以小时计算 
            {
                age = totalDefDay * 24 + intHour;
                ageUnitID = int.Parse(AgeUnitType.小时.Key);
                ageUnitName = AgeUnitType.小时.Value.Name;
                ageDesc = age.ToString() + ageUnitName;
            }
            else if (intYear == 0 && intMonth == 0 && totalDefDay == 0 && intHour >= intHourBorder) //年龄>=intHourBorder小时且<intDayBorder天的以小时计算   
            {
                age = intHour;
                ageUnitID = int.Parse(AgeUnitType.小时.Key);
                ageUnitName = AgeUnitType.小时.Value.Name;
                ageDesc = age.ToString() + ageUnitName;
            }
            else if (intYear == 0 && intMonth == 0 && totalDefDay == 0 && intHour == 0 && intMin > 0) //不足intHourBorder小时按分钟计算   
            {
                age = intHour * 60 + intMin;
                ageUnitID = int.Parse(AgeUnitType.分钟.Key);
                ageUnitName = AgeUnitType.分钟.Value.Name;
                ageDesc = age.ToString() + ageUnitName;
            }
            else
            {
                age = 0;
                ageUnitID = 6;
                ageUnitName = "";
                ageDesc = "<1分钟";
            }
            #endregion

            return "{\"Age\":\"" + age.ToString() + "\"" +
                   ",\"AgeUnitID\":\"" + ageUnitID.ToString() + "\"" +
                   ",\"AgeUnitName\":\"" + ageUnitName + "\"" +
                   ",\"AgeDesc\":\"" + ageDesc + "\"}";
        }

    }
}