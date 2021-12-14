using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IBLL.Business;

namespace ZhiFang.Digitlab.BLL.Business
{
    /// <summary>
    /// 用于解析各种号码生成规则
    /// </summary>
    public class NumberRule: IBNumberRule
    {
        private string _strZero = "00000000000000000000";
        private string _ruleOrder;
        private DateTime? _startDate; 
        private Dictionary<string, string> _ruleDictionary;

        //号码生成规则
        public string NumberRuleContent { get; set; }
        //号码子规则的顺序
        public string RuleOrder 
        {
            get 
            { 
                if (string.IsNullOrEmpty(_ruleOrder))
                    _ruleOrder = "ABC";
                return _ruleOrder;
            }
            set { _ruleOrder = value; } 
        }
        //当前号码规则的最大顺序号
        public string CurrentMaxNo { get; set; }
        //号码子规则字典
        public Dictionary<string, string> RuleDictionary
        {
            get 
            { 
                if (_ruleDictionary == null)
                    _ruleDictionary = new Dictionary<string,string>();
                 return _ruleDictionary;
            }
            set 
            {
                _ruleDictionary = value; 
            }        
        }

        public string StartNumber { get; set; }
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        /// <summary>
        /// 获取下一个号码
        /// </summary>
        /// <returns></returns>
        public string GetNextNumber()
        {
            string tempNumber = "";
            AnalyseNumberRule(NumberRuleContent);
            foreach (char tempChar in RuleOrder)
            {
                string tempStr = tempChar.ToString().ToUpper();
                if (RuleDictionary.ContainsKey(tempStr))
                    tempNumber += GetSubNumber(tempStr, RuleDictionary[tempStr]);
            }
            return tempNumber;
        }

        /// <summary>
        /// 分析当前规则
        /// </summary>
        /// <param name="strRule"></param>
        /// <param name="StrCurrentNo"></param>
        public void AnalyseNumberRule(string strNumberRule)
        {
            //分解设置的号码规则
            string[] strNumberRules = strNumberRule.Split('|');

            if (strNumberRules != null && strNumberRules.Length > 0)
            {
                RuleDictionary.Clear();
                foreach (string tempStr in strNumberRules)
                {
                    //string key = tempStr.Substring(0, 1);
                    //string value = tempStr.Remove(0, 2);
                    RuleDictionary.Add(tempStr.Substring(0, 1), tempStr);
                }
            }
        }


        private string GetSubNumber(string ruleFirstName, string ruleContent)
        {
            string strSubNumber = "";
            switch (ruleFirstName)
            {
                case "A":
                    strSubNumber = AnalyseA(ruleContent);
                    break;
                case "B":
                    strSubNumber = AnalyseB(ruleContent);
                    break;
                case "C":
                    strSubNumber = AnalyseC(ruleContent);
                    break;
            }
            return strSubNumber;
        }

        /// <summary>
        /// 获取固定字符规则字符串(A规则 A^R )
        /// </summary>
        /// <param name="strSubRule">子规则</param>
        /// <returns></returns>
        private string AnalyseA(string strSubRule)
        {
            string strFixChar = "";

            string[] strSubRules = strSubRule.Split('^');
            if (strSubRules.Length > 1)
            {
                strFixChar = strSubRules[1];
            }
            return strFixChar;
        }

        /// <summary>
        /// 获取日期规则字符串(B规则 B^yyMMdd)
        /// </summary>
        /// <param name="strSubRule">子规则</param>
        /// <returns></returns>
        private string AnalyseB(string strSubRule)
        {
            string strDate = "";
            string[] strSubRules = strSubRule.Split('^');
            if (strSubRules.Length > 1)
            {
                string dateRule = strSubRules[1];
                strDate = DateTime.Now.ToString(dateRule);
            }
            return strDate;
        }


        /// <summary>
        /// 获取顺序号规则字符串(C规则 C^3^1^100-300^0^1)
        /// </summary>
        /// <param name="strSubRule"></param>
        /// <param name="strCurrentNo"></param>
        /// <returns></returns>
        private string AnalyseC(string strSubRule)
        {
            //顺序号标识^顺序号长度^顺序号区间^是否复位^是否补零
            //顺序号标识为C
            //顺序号长度大于2且小于12(整形)
            //顺序号区间大于0小于1千亿的整数,只要设置区间,必须有最大和最小值,否则设置顺序号即可.顺序号和区间设置一个即可,同时设置时,忽略顺序号
            //复位 0不复位 1复位
            //补零 0不补零 1补零
            //C^6^^0^1 顺序号标识为C 长度为6 无区间 不复位  补零
            //C^5^100-10000^1^0 顺序号标识为C 长度为5 无区间100-10000 复位  不补零
            string strOrderNumber = "";
            int orderNumLen = 5;//顺序号长度
            string orderNumArea = "";//顺序号区间(范围) 
            int isReset = 1; //是否复位
            int isZeroFill = 1; //是否补零
            int startNum = 0; //顺序号区间开始值
            int endNum = 0;   //顺序号区间结束值

            if (!string.IsNullOrEmpty(strSubRule))
            {
                string[] strSubRules = strSubRule.Split('^');
                if (strSubRules.Length >= 5)
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
                        }
                    }
                    if (!string.IsNullOrEmpty(strSubRules[3]))
                        isReset = Convert.ToInt32(strSubRules[3]);
                    if (!string.IsNullOrEmpty(strSubRules[4]))
                        isZeroFill = Convert.ToInt32(strSubRules[4]);
                }
            }
            string ResetNumber = CurrentMaxNo;
            int currentNo = Convert.ToInt32(CurrentMaxNo);
            if (startNum >= endNum)
            {
                currentNo = currentNo + 1;
            }
            else
            {
                if (currentNo >= startNum && startNum < endNum)
                {
                    currentNo = currentNo + 1;
                }
                else if (currentNo == endNum)
                {
                    throw new Exception("已经达到最大号");
                }
            }
            CurrentMaxNo = currentNo.ToString();
            //复位
            if (isReset == 1)
            {
                if (StartDate != null && StartDate.Value.Date != DateTime.Now.Date)
                {
                    StartDate = DateTime.Now.Date;
                    StartNumber = ResetNumber;
                }
                currentNo = currentNo - Convert.ToInt32(StartNumber);
            }
            //补零
            if (isZeroFill == 1)
            {
                int tempLenth = orderNumLen - currentNo.ToString().Length;
                if (tempLenth > 0)
                    strOrderNumber = _strZero.Substring(0, tempLenth) + currentNo.ToString();
            }
            else
            {
                strOrderNumber = currentNo.ToString();
            }
            return strOrderNumber;
        }

    }
}
