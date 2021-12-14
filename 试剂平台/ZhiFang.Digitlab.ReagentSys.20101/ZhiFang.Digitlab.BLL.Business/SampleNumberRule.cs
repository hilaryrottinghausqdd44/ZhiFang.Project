using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IBLL.Business;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.Business
{
    /// <summary>
    /// 用于解析样本号码生成规则
    /// </summary>
    public class SampleNumberRule : IBSampleNumberRule
    {
        /// <summary>
        /// 号码生成规则
        /// </summary>
        public string NumberRuleContent { get; set; }
        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 规则号码，示例：固定字符|日期格式|流水号最大号,T|0928|1
        /// </summary>
        public string RuleNumber { get; set; }
        /// <summary>
        /// 规则因子在规则字段里的索引位置
        /// </summary>
        public int SubRuleIndex { get; set; }

        /// <summary>
        /// 获取样本号码
        /// </summary>
        /// <param name="strSampleNumber">返回生成的样本号</param>
        /// <param name="endNum">样本号生成规则的数值范围最大结束值</param>
        /// <param name="isMaxNum">获取生成的样本号是否已经达到最大样本号</param>
        /// <returns>BaseResultBool</returns>
        public BaseResultBool GetSampleNumber(out string strSubNumber, out int endNum, out bool isMaxNum)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool.BoolFlag = true;

            baseResultBool.BoolInfo = "";
            string tempNumber = "";
            string tempPrefix = "";
            string strRule = "";
            strSubNumber = "";
            endNum = 0;
            isMaxNum = false;
            string[] strNumberRules = NumberRuleContent.Split('|');
            for (int i = 0; i < strNumberRules.Length; i++)
            {
                strRule = strNumberRules[i];
                SubRuleIndex = i;
                tempPrefix = strRule.Split('^')[0].Trim().ToUpper();
                tempNumber = "";
                isMaxNum = false;
                baseResultBool = GetSubNumber(tempPrefix, strRule, out tempNumber, out endNum, out isMaxNum);
                strSubNumber = strSubNumber + tempNumber;
                if (baseResultBool.BoolFlag == false)
                {
                    break;
                }
            }
            return baseResultBool;
        }
        /// <summary>
        /// 根据每个子规则获取其相应的号码
        /// </summary>
        /// <param name="rulePrefix">规则前缀：A、B、C</param>
        /// <param name="ruleContent">规则详细</param>
        /// <param name="strSubNumber">下一个样本号</param>
        /// <param name="endNum">顺序号区间结束值</param>
        /// <returns>BaseResultBool</returns>
        private BaseResultBool GetSubNumber(string rulePrefix, string ruleContent, out string strSubNumber, out int endNum, out bool isMaxNum)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool.BoolFlag = true;
            baseResultBool.BoolInfo = "";
            strSubNumber = "";
            endNum = 0;
            isMaxNum = false;
            switch (rulePrefix)
            {
                case "A":
                    baseResultBool = AnalyseA(ruleContent, out strSubNumber);
                    break;
                case "B":
                    baseResultBool = AnalyseB(ruleContent, out strSubNumber);
                    break;
                case "C":
                    baseResultBool = AnalyseC(ruleContent, out strSubNumber, out endNum, out isMaxNum);
                    break;
            }
            if (isMaxNum)
            {
                baseResultBool.BoolInfo = "生成的下一样本号为:" + strSubNumber + ",该样本号已达到样本号生成规则的最大结束值:" + endNum;
            }
            return baseResultBool;
        }

        /// <summary>
        /// 设置RuleNumber的值
        /// </summary>
        /// <param name="strSubRuleValue">规则值</param>
        /// <param name="strDate">开始日期，流水号使用</param>
        private void SetRuleNumber(string strSubRuleValue, string strDate)
        {
            if (RuleNumber == null || RuleNumber.Trim().Length == 0)
            {
                if (strDate.Trim().Length > 0)
                    RuleNumber = strDate + "^" + strSubRuleValue;
                else
                    RuleNumber = strSubRuleValue;
            }
            else
            {
                string tmpRule = "";
                string[] strRuleNumberArr = RuleNumber.Split('|');
                if (strRuleNumberArr.Length > SubRuleIndex)
                {
                    for (int i = 0; i < strRuleNumberArr.Length; i++)
                    {
                        if (i == SubRuleIndex)
                        {
                            if (strDate.Trim().Length > 0)
                                tmpRule += strDate + "^" + strSubRuleValue + "|";
                            else
                                tmpRule += strSubRuleValue + "|";
                        }
                        else
                        {
                            tmpRule += strRuleNumberArr[i] + "|";
                        }
                    }
                    if (tmpRule.Trim().Length > 0)
                    {
                        RuleNumber = tmpRule.TrimEnd('|');
                    }
                }
                else
                {
                    if (strDate.Trim().Length > 0)
                        RuleNumber += "|" + strDate + "^" + strSubRuleValue;
                    else
                        RuleNumber += "|" + strSubRuleValue;
                }
            }
        }

        /// <summary>
        /// 获取固定字符规则字符串(A规则 A^R )
        /// </summary>
        /// <param name="strSubRule">子规则</param>
        /// <returns>号码</returns>
        private BaseResultBool AnalyseA(string strSubRule, out string strFixChar)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool.BoolFlag = true;
            baseResultBool.BoolInfo = "";
            strFixChar = "";
            string[] strSubRules = strSubRule.Split('^');
            if (strSubRules.Length > 1)
            {
                strFixChar = strSubRules[1];
                SetRuleNumber(strFixChar, "");
            }
            return baseResultBool;
        }

        /// <summary>
        /// 获取日期规则字符串(B规则 B^yyMMdd)
        /// </summary>
        /// <param name="strSubRule">子规则</param>
        /// <returns>号码</returns>
        private BaseResultBool AnalyseB(string strSubRule, out string strDate)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool.BoolFlag = true;
            baseResultBool.BoolInfo = "";
            strDate = "";
            string[] strSubRules = strSubRule.Split('^');
            if (strSubRules.Length > 1)
            {
                string dateRule = strSubRules[1];
                strDate = DateTime.Now.ToString(dateRule);
                SetRuleNumber(strDate, "");
            }
            return baseResultBool;
        }
        /// <summary>
        /// 获取顺序号规则字符串(C规则 C^3^1^100-300^0^1)
        /// </summary>
        /// <param name="strSubRule">子规则</param>
        /// <returns>号码</returns>
        /// <param name="strOrderNumber">生成的样本号</param>
        /// <param name="endNum">顺序号区间结束值</param>
        /// <param name="isMaxNum">获取生成的样本号是否已经达到最大样本号</param>
        /// <returns>BaseResultBool</returns>
        private BaseResultBool AnalyseC(string strSubRule, out string strOrderNumber, out int endNum, out bool isMaxNum)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool.BoolFlag = true;
            baseResultBool.BoolInfo = "";
            //顺序号标识^顺序号长度^顺序号区间^复位周期^是否补零^进位周期
            //顺序号标识为C
            //顺序号长度大于2且小于12(整形)
            //顺序号区间大于0小于1千亿的整数,只要设置区间,必须有最大和最小值,否则设置顺序号即可.顺序号和区间设置一个即可,同时设置时,忽略顺序号
            //复位周期 0不复位 1每天复位 2每周复位 3每月复位 4每年复位 
            //是否补零 0不补零 1补零
            //进位周期 0按次累计 1按天累计

            //示例：
            //C^6^^0^1^0 顺序号标识为C 长度为6 无区间 不复位 补零 按次累计
            //C^5^100-10000^1^0^0 顺序号标识为C 长度为5 区间100-10000 每天复位 不补零 按天累计

            strOrderNumber = "";
            int orderNumLen = 5;//顺序号长度
            string orderNumArea = "";//顺序号区间(范围) 
            int isReset = 1; //复位周期
            int isCarry = 1; //进位周期
            int isZeroFill = 1; //是否补零
            int startNum = 0; //顺序号区间开始值
            endNum = 0;   //顺序号区间结束值
            isMaxNum = false;//获取生成的样本号是否已经达到最大样本号
            if (!string.IsNullOrEmpty(strSubRule))
            {
                string[] strSubRules = strSubRule.Split('^');
                if (strSubRules.Length >= 6)
                {
                    if (!string.IsNullOrEmpty(strSubRules[1]))
                        orderNumLen = Convert.ToInt32(strSubRules[1]);
                    orderNumArea = strSubRules[2];
                    if ((!string.IsNullOrEmpty(orderNumArea)) && (orderNumArea.IndexOf('-') >= 0))
                    {
                        try
                        {
                            startNum = Convert.ToInt32(orderNumArea.Split('-')[0].Trim());
                            endNum = Convert.ToInt32(orderNumArea.Split('-')[1].Trim());
                        }
                        catch
                        {
                            startNum = 0;
                            endNum = 0;
                            //baseResultBool.BoolFlag = false;
                            baseResultBool.BoolInfo = "处理顺序号区间开始值及顺序号区间结束值出错";
                        }
                    }
                    if (!string.IsNullOrEmpty(strSubRules[3]))
                        isReset = Convert.ToInt32(strSubRules[3]);
                    if (!string.IsNullOrEmpty(strSubRules[4]))
                        isZeroFill = Convert.ToInt32(strSubRules[4]);
                    if (!string.IsNullOrEmpty(strSubRules[5]))
                        isCarry = Convert.ToInt32(strSubRules[5]);
                }
            }
            //开始日期
            if (RuleNumber != null)
            {
                if (RuleNumber.Split('|').Length > SubRuleIndex)
                {
                    string strTmpDate = RuleNumber.Split('|')[SubRuleIndex].Split('^')[0].Trim();
                    if (strTmpDate.Length > 0)
                    {
                        StartDate = Convert.ToDateTime(strTmpDate);
                    }
                    else
                    {
                        StartDate = DateTime.Now.Date;
                    }
                }
                else
                {
                    StartDate = DateTime.Now.Date;
                    RuleNumber += "|" + DateTime.Now.ToString("yyyy-MM-dd") + "^0";
                }
            }
            else
            {
                StartDate = DateTime.Now.Date;
                RuleNumber += DateTime.Now.ToString("yyyy-MM-dd") + "^0";
            }
            //复位的起始号、最大号
            string strResetNumber = startNum == 0 ? "1" : startNum.ToString();
            string strCurrentMaxNo = "0";
            //复位周期 0不复位 1每天复位 2每周复位 3每月复位 4每年复位 
            switch (isReset)
            {
                case 1:
                    if (StartDate.Value.Date < DateTime.Now.Date)
                    {
                        StartDate = DateTime.Now.Date;
                        SetRuleNumber(strCurrentMaxNo, StartDate.Value.ToString("yyyy-MM-dd"));
                    }
                    break;
                case 2:
                    if (Convert.ToInt32(DateTime.Now.Date.DayOfWeek) == 1 && StartDate.Value.Date != DateTime.Now.Date)
                    {
                        StartDate = DateTime.Now.Date;
                        SetRuleNumber(strCurrentMaxNo, StartDate.Value.ToString("yyyy-MM-dd"));
                    }
                    break;
                case 3:
                    if (DateTime.Now.Date.Month == 1 && StartDate.Value.Date != DateTime.Now.Date)
                    {
                        StartDate = DateTime.Now.Date;
                        SetRuleNumber(strCurrentMaxNo, StartDate.Value.ToString("yyyy-MM-dd"));
                    }
                    break;
                case 4:
                    DateTime dtTemp = DateTime.Parse(StartDate.Value.AddYears(1).ToString("yyyy-01-01"));
                    DateTime dtNow = DateTime.Parse(DateTime.Now.ToString("yyyy-01-01"));
                    if (dtTemp.Date == dtNow.Date && StartDate.Value.Date != DateTime.Now.Date)
                    {
                        StartDate = DateTime.Now.Date;
                        SetRuleNumber(strCurrentMaxNo, StartDate.Value.ToString("yyyy-MM-dd"));
                    }
                    break;
            }
            //号码生成
            int currentNo = 0;
            try
            {
                if (!String.IsNullOrEmpty(RuleNumber))
                {
                    currentNo = Convert.ToInt32(RuleNumber.Split('|')[SubRuleIndex].Split('^')[1]);
                }

            }
            catch (Exception ee)
            {
                baseResultBool.BoolFlag = false;
                baseResultBool.BoolInfo = "样本号生成出错";
                ZhiFang.Common.Log.Log.Debug("样本号生成出错:" + ee.ToString());
                throw ee;
            }
            if (currentNo == 0)
            {
                currentNo = Convert.ToInt32(strResetNumber);
            }
            else if (startNum == 0 && endNum == 0)
            {
                //进位周期 0按次累计 1按天累计
                if (isCarry == 0)
                {
                    currentNo = currentNo + 1;
                }
            }
            else if (currentNo >= startNum && currentNo < endNum)
            {
                //进位周期 0按次累计 1按天累计
                if (isCarry == 0)
                {
                    currentNo = currentNo + 1;
                }
            }
            else if (currentNo == endNum)
            {
                //ZhiFang.Common.Log.Log.Debug("已经达到最大号:" + currentNo);
                baseResultBool.BoolFlag = false;
                isMaxNum = true;
                baseResultBool.BoolInfo = "生成的下一样本号为:" + currentNo + ",已达到最大号";
            }
            SetRuleNumber(currentNo.ToString(), StartDate.Value.ToString("yyyy-MM-dd"));
            //补零
            if (isZeroFill == 1)
            {
                strOrderNumber = currentNo.ToString().PadLeft(orderNumLen, '0');
            }
            else
            {
                strOrderNumber = currentNo.ToString();
            }
            return baseResultBool;
        }

    }
}
