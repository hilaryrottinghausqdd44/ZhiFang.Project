using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.WeiXin.IDAO;

namespace ZhiFang.WeiXin.BLL
{
    public class BBRuleNumber : BaseBLL<BRuleNumber>, IBBRuleNumber
    {
        #region 属性
        IBBParameter IBBParameter { get; set; }
        public DateTime? StartDate { get; set; }//起始日期
        public int SubRuleIndex { get; set; }//规则因子在规则字段里的索引位置
        //规则号码，示例：固定字符|日期格式|流水号最大号,T|0928|1
        public Dictionary<string, string> RuleNumberList = new Dictionary<string, string>();

        public string MRefundFormCode = "A^T|B^yyMMdd|C^5^0-99999^1^1^1|B^HHmmssfff";//退费单编号
        public string OSUserConsumerFormCode = "B^yyMMdd|C^5^0-99999^1^1^1|B^HHmmssfff";//消费单编号
        #endregion

        #region 对外公开
        /// <summary>
        /// 消费单编号
        /// </summary>
        /// <returns></returns>
        public string GetOSUserConsumerFormCode()
        {
            string strSubNumber = "";
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool = GetNextRuleNumber(NumberRuleType.消费单编号.Key, this.OSUserConsumerFormCode, out strSubNumber);
            //ZhiFang.Common.Log.Log.Debug("消费单编号:" + strSubNumber);
            return strSubNumber;
        }
        /// <summary>
        /// 退费单编号
        /// </summary>
        /// <returns></returns>
        public string GetMRefundFormCode()
        {
            string strSubNumber = "";
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool = GetNextRuleNumber(NumberRuleType.退费单编号.Key, this.MRefundFormCode, out strSubNumber);
            ZhiFang.Common.Log.Log.Debug("退费单编号:" + strSubNumber);
            return strSubNumber;
        }
        #endregion

        #region 内部处理
        /// <summary>
        /// 获取生成的下一规则号
        /// </summary>
        /// <param name="numberRuleType">单号规则内容类型</param>
        /// <param name="numberRuleValue">单号规则内容</param>
        /// <param name="strSubNumber">生成返回的下一规则号</param>
        /// <returns></returns>
        private BaseResultBool GetNextRuleNumber(string numberRuleType, string numberRuleValue, out string strSubNumber)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool.BoolFlag = true;
            strSubNumber = "";
            string[] strNumberRules = numberRuleValue.Split('|');
            string tempNumber = "";
            string tempPrefix = "";
            string strRule = "";
            strSubNumber = "";
            int endNum = 0;
            bool isMaxNum = false;
            for (int i = 0; i < strNumberRules.Length; i++)
            {
                strRule = strNumberRules[i];
                SubRuleIndex = i;
                tempPrefix = strRule.Split('^')[0].Trim().ToUpper();
                tempNumber = "";
                baseResultBool = GetSubNumber(numberRuleType, tempPrefix, strRule, out tempNumber, out endNum, out isMaxNum);
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
        /// <param name="strSubNumber">下一个规则号</param>
        /// <param name="endNum">顺序号区间结束值</param>
        /// <returns>BaseResultBool</returns>
        private BaseResultBool GetSubNumber(string numberRuleType, string rulePrefix, string ruleContent, out string strSubNumber, out int endNum, out bool isMaxNum)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool.BoolFlag = true;
            baseResultBool.BoolInfo = "";
            strSubNumber = "";
            endNum = 0;
            isMaxNum = false;
            switch (rulePrefix)
            {
                case "A"://前缀
                    string[] strSubRules = ruleContent.Split('^');
                    if (strSubRules.Length > 1)
                    {
                        strSubNumber = strSubRules[1];
                        SetRuleNumber(numberRuleType, strSubNumber, "");
                    }
                    break;
                case "B"://日期
                    baseResultBool = AnalyseB(numberRuleType, ruleContent, out strSubNumber);
                    break;
                case "C"://流水号(顺序生成的整数)
                    baseResultBool = AnalyseC(numberRuleType, ruleContent, out strSubNumber, out endNum, out isMaxNum);
                    break;
                case "C1"://流水号(随机几位数)
                    baseResultBool = AnalyseC1(numberRuleType, ruleContent, out strSubNumber);
                    break;
            }
            if (isMaxNum)
            {
                baseResultBool.BoolInfo = "生成的下一规则号为:" + strSubNumber + ",该规则号已达到规则号生成规则的最大结束值:" + endNum;
            }
            return baseResultBool;
        }
        /// <summary>
        /// 获取日期规则字符串(B规则 B^yyMMdd)
        /// </summary>
        /// <param name="strSubRule">子规则</param>
        /// <returns>号码</returns>
        private BaseResultBool AnalyseB(string numberRuleType, string strSubRule, out string strDate)
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
                SetRuleNumber(numberRuleType, strDate, "");
            }
            return baseResultBool;
        }
        /// <summary>
        /// 获取顺序号规则字符串(C规则 C^3^1^100-300^0^1)
        /// </summary>
        /// <param name="strSubRule">子规则</param>
        /// <returns>号码</returns>
        /// <param name="strOrderNumber">生成的规则号</param>
        /// <param name="endNum">顺序号区间结束值</param>
        /// <param name="isMaxNum">获取生成的规则号是否已经达到最大规则号</param>
        /// <returns>BaseResultBool</returns>
        private BaseResultBool AnalyseC(string numberRuleType, string strSubRule, out string strOrderNumber, out int endNum, out bool isMaxNum)
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
            //int isCarry = 1; //进位周期
            int isZeroFill = 1; //是否补零
            int startNum = 0; //顺序号区间开始值
            endNum = 99999;   //顺序号区间结束值
            isMaxNum = false;//获取生成的规则号是否已经达到最大规则号 
            int currentNo = 0;
            string cacheStartDateKey = numberRuleType + "StartDate";
            string perNumber = "";//上一流水号值
            string RuleNumber = "";
            if (RuleNumberList.ContainsKey(numberRuleType))
                RuleNumber = RuleNumberList[numberRuleType];
            else
                RuleNumberList.Add(numberRuleType, RuleNumber);

            #region 规则内容处理
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
                            endNum = 99999;
                            baseResultBool.BoolInfo = "处理顺序号区间开始值及顺序号区间结束值出错";
                        }
                    }
                    if (!string.IsNullOrEmpty(strSubRules[3]))
                        isReset = Convert.ToInt32(strSubRules[3]);
                    if (!string.IsNullOrEmpty(strSubRules[4]))
                        isZeroFill = Convert.ToInt32(strSubRules[4]);
                    //if (!string.IsNullOrEmpty(strSubRules[5]))
                    //isCarry = Convert.ToInt32(strSubRules[5]);
                }
            }
            #endregion

            //先从系统参数里读取上一流水号值
            BParameter bparameter = null;
            perNumber = GetPerNumber(numberRuleType, ref bparameter);

            #region 开始日期
            GetCacheStartDate(cacheStartDateKey);
            if (RuleNumber != null)
            {
                if (RuleNumber.Split('|').Length <= SubRuleIndex)
                {
                    RuleNumber += "|" + DateTime.Now.ToString("yyyy-MM-dd") + "^0";
                }
            }
            else
            {
                RuleNumber += DateTime.Now.ToString("yyyy-MM-dd") + "^0";
            }
            #endregion

            #region 每天复位处理
            //复位的起始号、最大号
            string strResetNumber = startNum == 0 ? "1" : startNum.ToString();
            string strCurrentMaxNo = "0";
            //复位周期:1每天复位
            switch (isReset)
            {
                case 1:
                    if (!StartDate.HasValue || StartDate.Value.Date < DateTime.Now.Date)
                    {
                        ZhiFang.Common.Log.Log.Debug("StartDate:" + StartDate.Value.Date.ToString());
                        perNumber = startNum == 0 ? "0" : startNum.ToString();
                        StartDate = DateTime.Now.Date;
                    }
                    IBBParameter.SetCache(cacheStartDateKey, DateTime.Now.ToString("yyyy-MM-dd"));
                    SetRuleNumber(numberRuleType, strCurrentMaxNo, StartDate.Value.ToString("yyyy-MM-dd"));
                    break;
            }
            #endregion

            #region 号码生成
            if (!String.IsNullOrEmpty(perNumber))
            {
                Int32.TryParse(perNumber, out currentNo);
            }
            if (currentNo == 0)
            {
                currentNo = Convert.ToInt32(strResetNumber);
            }
            else if (startNum == 0 && endNum == 0)
            {
                currentNo = currentNo + 1;
            }
            else if (currentNo >= startNum && currentNo < endNum)
            {
                currentNo = currentNo + 1;
            }
            else if (currentNo == endNum)
            {
                string boolInfo = StartDate.Value.ToString("yyyy-MM-dd") + numberRuleType + ",已经达到最大号:" + currentNo + ",范围为:" + startNum + "-" + endNum;
                ZhiFang.Common.Log.Log.Debug(boolInfo);
                baseResultBool.BoolFlag = true;
                isMaxNum = true;
                baseResultBool.BoolInfo = boolInfo;
            }
            if (!StartDate.HasValue)
                StartDate = DateTime.Now.Date;
            SetRuleNumber(numberRuleType, currentNo.ToString(), StartDate.Value.ToString("yyyy-MM-dd"));
            //补零
            if (isZeroFill == 1)
            {
                strOrderNumber = currentNo.ToString().PadLeft(orderNumLen, '0');
            }
            else
            {
                strOrderNumber = currentNo.ToString();
            }
            #endregion

            #region 生成的当前号更新到相关的系统参数中
            if (bparameter != null)
            {
                bparameter.ParaValue = strOrderNumber;
                EditBParameter(bparameter);
            }
            #endregion
            return baseResultBool;
        }

        /// <summary>
        /// 随机数几位数生成
        /// </summary>
        /// <param name="numberRuleType"></param>
        /// <param name="strSubRule"></param>
        /// <param name="strOrderNumber"></param>
        /// <returns></returns>
        private BaseResultBool AnalyseC1(string numberRuleType, string strSubRule, out string strOrderNumber)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool.BoolFlag = true;
            baseResultBool.BoolInfo = "";
            strOrderNumber = "";
            int orderNumLen = 4;//顺序号长度
            string orderNumArea = "";//顺序号区间(范围) 

            int isZeroFill = 1; //是否补零
            int startNum = 0; //顺序号区间开始值
            int endNum = 99999;   //顺序号区间结束值
            Random ran = new Random();
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
                            endNum = 99999;
                            //baseResultBool.BoolFlag = false;
                            baseResultBool.BoolInfo = "处理顺序号区间开始值及顺序号区间结束值出错";
                        }
                    }
                }
            }
            int randKey = ran.Next(startNum, endNum);
            //补零
            if (isZeroFill == 1)
            {
                strOrderNumber = randKey.ToString().PadLeft(orderNumLen, '0');
            }
            else
            {
                strOrderNumber = randKey.ToString();
            }
            SetRuleNumber(numberRuleType, strSubRule, "");
            return baseResultBool;
        }

        /// <summary>
        /// 设置RuleNumber的值
        /// </summary>
        /// <param name="strSubRuleValue">规则值</param>
        /// <param name="strDate">开始日期，流水号使用</param>
        private void SetRuleNumber(string numberRuleType, string strSubRuleValue, string strDate)
        {
            string RuleNumber = "";
            if (RuleNumberList.ContainsKey(numberRuleType))
                RuleNumber = RuleNumberList[numberRuleType];
            else
                RuleNumberList.Add(numberRuleType, RuleNumber);

            if (RuleNumber == null || RuleNumber.Trim().Length == 0)
            {
                if (strDate.Trim().Length > 0)
                {
                    RuleNumber = strDate + "^" + strSubRuleValue;
                    RuleNumberList[numberRuleType] = RuleNumber;
                }
                else
                {
                    RuleNumber = strSubRuleValue;
                    RuleNumberList[numberRuleType] = RuleNumber;
                }
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
                        RuleNumberList[numberRuleType] = RuleNumber;
                    }
                }
                else
                {
                    if (strDate.Trim().Length > 0)
                    {
                        RuleNumber += "|" + strDate + "^" + strSubRuleValue;
                        RuleNumberList[numberRuleType] = RuleNumber;
                    }
                    else
                    {
                        RuleNumber += "|" + strSubRuleValue;
                        RuleNumberList[numberRuleType] = RuleNumber;
                    }
                }
            }
        }

        #endregion

        #region 系统参数及缓存
        /// <summary>
        /// 获取系统参数里的上一流水号
        /// </summary>
        /// <param name="numberRuleType"></param>
        /// <param name="bparameter"></param>
        /// <returns></returns>
        private string GetPerNumber(string numberRuleType, ref BParameter bparameter)
        {
            string perNumber = "";
            IList<BParameter> listBParameter = IBBParameter.SearchListByParaNo(numberRuleType);

            if (listBParameter.Count > 0)
            {
                bparameter = listBParameter[0];
                perNumber = bparameter.ParaValue.Trim();
            }
            return perNumber;
        }
        /// <summary>
        /// 获取及设置numberRuleType的流水号的缓存开始日期
        /// </summary>
        /// <param name="cacheKey"></param>
        private void GetCacheStartDate(string cacheKey)
        {
            string cacheValue = null;
            if (BParameterCache.ApplicationCache!=null && BParameterCache.ApplicationCache.Application!=null && BParameterCache.ApplicationCache.Application.AllKeys != null && BParameterCache.ApplicationCache.Application.AllKeys.Length > 0 && BParameterCache.ApplicationCache.Application.AllKeys.Contains(cacheKey))
            {
                cacheValue = (string)IBBParameter.GetCache(cacheKey);
                if (cacheValue.Length > 0)
                {
                    StartDate = Convert.ToDateTime(cacheValue);
                    ZhiFang.Common.Log.Log.Debug("GetCache--StartDate:" + StartDate.Value.Date.ToString("yyyy-MM-dd"));
                }
            }
            else
            {
                StartDate = DateTime.Now.Date;
                IBBParameter.SetCache(cacheKey, DateTime.Now.ToString("yyyy-MM-dd"));
            }
        }
        /// <summary>
        /// 生成的当前号更新到相关的系统参数中
        /// </summary>
        /// <param name="bparameter"></param>
        private void EditBParameter(BParameter bparameter)
        {
            if (bparameter != null)
            {
                IBBParameter.Entity = bparameter;
                IBBParameter.Edit();
            }
        }
        #endregion
    }
}
