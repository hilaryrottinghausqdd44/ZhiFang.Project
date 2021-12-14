using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LIIP;
using ZhiFang.IDAO.LabStar;
using ZhiFang.LabStar.Common;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBTranRule : BaseBLL<LBTranRule>, ZhiFang.IBLL.LabStar.IBLBTranRule
    {
        IDLBTranRuleTypeDao IDLBTranRuleTypeDao { get; set; }
        IDLBSickTypeDao IDLBSickTypeDao { get; set; }
        IDLBSampleTypeDao IDLBSampleTypeDao { get; set; }
        IDLBSamplingGroupDao IDLBSamplingGroupDao { get; set; }
        IDLBTranRuleItemDao IDLBTranRuleItemDao { get; set; }
        public EntityList<LBTranRule> GetLBTranRuleList(string where, string sort, int page, int limit)
        {
            EntityList<LBTranRule> entityList = new EntityList<LBTranRule>();
            entityList = this.SearchListByHQL(where,sort,page,limit);
            List<long?> stids = new List<long?>();
            List<long?> smtids = new List<long?>();
            List<long?> sgids = new List<long?>();
            List<long?> deptids = new List<long?>();
            List<long?> clientIds = new List<long?>();
            foreach (var item in entityList.list)
            {
                if (item.SickTypeID > 0 && !stids.Contains(item.SickTypeID)) { stids.Add(item.SickTypeID); }
                if (item.SampleTypeID > 0 && !smtids.Contains(item.SampleTypeID)){ smtids.Add(item.SampleTypeID); }
                if (item.SamplingGroupID > 0 && !sgids.Contains(item.SamplingGroupID)){ sgids.Add(item.SamplingGroupID); }
                if (item.DeptID > 0 && !deptids.Contains(item.DeptID)) { deptids.Add(item.DeptID); }
                if (item.ClientID > 0 && !clientIds.Contains(item.ClientID)) { clientIds.Add(item.ClientID); }
            }
            string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
            IList<LBSickType> lBSickTypes = null;
            IList<LBSampleType> lBSampleTypes = null;
            IList<LBSamplingGroup> lBSamplingGroups = null;
            IList<HREmpIdentity> deptlist = null;
            IList<HREmpIdentity> clientlist = null;
            if (stids.Count > 0) {
                lBSickTypes = IDLBSickTypeDao.GetListByHQL("Id in (" + string.Join(",", stids) + ")");
            }
            if (smtids.Count > 0) {
                lBSampleTypes = IDLBSampleTypeDao.GetListByHQL("Id in (" + string.Join(",", smtids) + ")");
            }
            if (sgids.Count > 0)
            {
                lBSamplingGroups = IDLBSamplingGroupDao.GetListByHQL("Id in (" + string.Join(",", sgids) + ")");
            }
            if (deptids.Count > 0)
            {
                string deptwhere = $"hrempidentity.TSysCode='{DeptSystemType.送检科室.Key}' and hrempidentity.SystemCode='ZF_PRE' and hrempidentity.LabID = {labid} and HRDeptIdentity_HRDept_Id in ({string.Join(",", deptids)})";
                deptlist = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL(where).toList<HREmpIdentity>();
            }
            if (clientIds.Count > 0) {
                string clientwhere = $"hrempidentity.TSysCode='{DeptSystemType.送检客户.Key}' and hrempidentity.SystemCode='ZF_PRE' and hrempidentity.LabID = {labid} and HRDeptIdentity_HRDept_Id in ({string.Join(",", clientIds)})";
                clientlist = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL(clientwhere).toList<HREmpIdentity>();
            }
            foreach (var item in entityList.list)
            {
                if (item.TestTypeID > 0) 
                {
                    item.TestTypeName = TestType.GetStatusDic().Where(a => a.Key == item.TestTypeID.ToString()).First().Value.Name;
                }
                if (item.SickTypeID > 0)
                {
                    if (lBSickTypes != null && lBSickTypes.Count > 0) {
                        item.SickTypeName = lBSickTypes.Where(a => a.Id == item.SickTypeID).First().CName;
                    }
                }
                if (item.SampleTypeID > 0)
                {
                    if (lBSampleTypes != null && lBSampleTypes.Count > 0)
                    {
                        item.SampleTypeName = lBSampleTypes.Where(a => a.Id == item.SampleTypeID).First().CName;
                    }
                }
                if (item.SamplingGroupID > 0)
                {
                    if (lBSamplingGroups != null && lBSamplingGroups.Count > 0)
                    {
                        item.SamplingGroupName = lBSamplingGroups.Where(a => a.Id == item.SamplingGroupID).First().CName;
                    }
                }
                if (item.DeptID > 0) {
                    if (deptlist != null && deptlist.Count > 0)
                    {
                        item.DeptName = deptlist.Where(a => a.Id == item.DeptID).First().CName;
                    }
                }
                if (item.ClientID > 0)
                {
                    if (clientlist != null && clientlist.Count > 0)
                    {
                        item.ClientName = clientlist.Where(a => a.Id == item.ClientID).First().CName;
                    }
                }
            }
            return entityList;
        }

        public BaseResultDataValue GetLBTranRuleNextSampleNo(int? SampleNoSection, string SampleNoPrefix)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            if (SampleNoSection == null) {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "样本号区间不能为空！";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(SampleNoPrefix))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "样本号前缀不能为空！";
                return baseResultDataValue;
            }
            string prefixstr = "";
            if (SampleNoPrefix.IndexOf("[") >-1 && SampleNoPrefix.IndexOf("]") > -1 && SampleNoPrefix.IndexOf("[") < SampleNoPrefix.IndexOf("]")) {
                var SampleNoPrefixarr = SampleNoPrefix.Split('[');
                if (SampleNoPrefixarr.Count() > 1)
                {
                    var SampleNoPrefixarr1 = SampleNoPrefixarr[1].Split(']');
                    if (SampleNoPrefixarr1.Count() > 1)
                    {
                        prefixstr = SampleNoPrefixarr[0];
                        string datetimetype = SampleNoPrefixarr1[0];
                        bool isok = true;
                        switch (datetimetype)
                        {
                            case "yy":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "yyyy":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "MM":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "dd":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "yyMM":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "yy-MM":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "yy/MM":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "MMdd":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "MM-dd":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "MM/dd":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "yyMMdd":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "yy-MM-dd":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "yy/MM/dd":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "yyyyMMdd":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "yyyy-MM-dd":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            case "yyyy/MM/dd":
                                prefixstr += DateTime.Now.ToString(datetimetype);
                                break;
                            default:
                                isok = false;
                                break;
                        }
                        prefixstr += SampleNoPrefixarr1[1];
                        if (!isok)
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "样本号前缀中日期格式不正确不能为空！";
                            return baseResultDataValue;
                        }
                    }
                    else
                    {
                        prefixstr = SampleNoPrefix;
                    }
                }
                else
                {
                    prefixstr = SampleNoPrefix;
                }
            }
            else
            {
                prefixstr = SampleNoPrefix;
            }
            baseResultDataValue.ResultDataValue = prefixstr + SampleNoSection;
            return baseResultDataValue;
        }

        public BaseResultBool DelLBTranRuleAndTranRuleItem(long LBTranRuleTypeID)
        {
            BaseResultBool baseResultBool = new BaseResultBool();

            var lBTranRules = DBDao.GetListByHQL("LBTranRuleType.Id = "+ LBTranRuleTypeID);
            if (lBTranRules.Count > 0) {
                List<long> ids = new List<long>();
                foreach (var lBTranRule in lBTranRules)
                {
                    ids.Add(lBTranRule.Id);
                }
                IDLBTranRuleItemDao.DeleteByHql("From LBTranRuleItem lbtranruleitem where lbtranruleitem.LBTranRule.Id in (" + string.Join(",", ids)+")");
                DBDao.DeleteByHql("Form LBTranRule lbtranrule where lbtranrule.Id in ("+ string.Join(",", ids) + ")");
            }
            baseResultBool.success = true;
            return baseResultBool;
        }

    }
}