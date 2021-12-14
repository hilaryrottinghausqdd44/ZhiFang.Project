using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBExpertRule : BaseBLL<LBExpertRule>, ZhiFang.IBLL.LabStar.IBLBExpertRule
    {
        ZhiFang.IBLL.LabStar.IBLBExpertRuleList IBLBExpertRuleList { get; set; }


        public EntityList<LBExpertRule> QueryLBExpertRuleByHQL(string where, string sort, int page, int limit)
        {
            EntityList<LBExpertRule> entityList = this.SearchListByHQL(where, sort, page, limit);
            if (entityList != null && entityList.count > 0)
            {
                IList<LBExpertRuleList> listExpertRuleList = IBLBExpertRuleList.LoadAll();
                if (listExpertRuleList != null && listExpertRuleList.Count > 0)
                {
                    foreach (LBExpertRule expertRule in entityList.list)
                    {
                        
                        IList<LBExpertRuleList> tempList = listExpertRuleList.Where(p => p.LBExpertRule.Id == expertRule.Id).ToList();
                        if (tempList != null && tempList.Count > 0)
                        {
                            string ExpertRuleListInfo = "";
                            foreach (LBExpertRuleList expertRuleList in tempList)
                            {
                                if (ExpertRuleListInfo == "")
                                    ExpertRuleListInfo = expertRuleList.RuleName;
                                else
                                    ExpertRuleListInfo += "," + expertRuleList.RuleName;
                            }
                            expertRule.ExpertRuleListInfo = ExpertRuleListInfo;
                        }
                    }               
                }
            }
            return entityList;
        }

        public BaseResultDataValue AddNewLBExpertRuleByLBExpertRule(long expertRuleID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            LBExpertRule expertRule = this.Get(expertRuleID);
            if (expertRule != null)
            {
                LBExpertRule newExpertRule = ZhiFang.LabStar.Common.ClassMapperHelp.GetEntityMapper<LBExpertRule, LBExpertRule>(expertRule);
                newExpertRule.CName += "_复制";
                this.Entity = newExpertRule;
                this.Add();
                IList<LBExpertRuleList> listLBExpertRuleList = IBLBExpertRuleList.SearchListByHQL(" lbexpertrulelist.LBExpertRule.Id=" + expertRuleID);
                if (listLBExpertRuleList != null && listLBExpertRuleList.Count > 0)
                {
                    foreach (LBExpertRuleList expertRuleList in listLBExpertRuleList)
                    {
                        LBExpertRuleList newExpertRuleList = ZhiFang.LabStar.Common.ClassMapperHelp.GetEntityMapper<LBExpertRuleList, LBExpertRuleList>(expertRuleList);
                        newExpertRuleList.LBExpertRule = newExpertRule;
                        IBLBExpertRuleList.Entity = newExpertRuleList;
                        IBLBExpertRuleList.Add();
                    }
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法根据专家规则ID获取专家规则信息";
            }
            return brdv;
        }

    }
}