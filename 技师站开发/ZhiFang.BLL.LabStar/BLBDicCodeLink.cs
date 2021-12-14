using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.Entity.LIIP;
using ZhiFang.IDAO.LabStar;
using ZhiFang.LabStar.Common;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBDicCodeLink : BaseBLL<LBDicCodeLink>, ZhiFang.IBLL.LabStar.IBLBDicCodeLink
    {
        #region 注入
        IDLBDiagDao IDLBDiagDao { get; set; }
        IDLBSampleTypeDao IDLBSampleTypeDao { get; set; }
        IDLBFolkDao IDLBFolkDao { get; set; }
        IDLBSickTypeDao IDLBSickTypeDao { get; set; }
        IDLBSamplingGroupDao IDLBSamplingGroupDao { get; set; }
        IDLBDestinationDao IDLBDestinationDao { get; set; }
        IDLBSectionDao IDLBSectionDao { get; set; }
        IDLBItemCodeLinkDao IDLBItemCodeLinkDao { get; set; }
        #endregion
        public EntityList<LBDicCodeLinkVO> SearchBasicsDicAndLBDicCodeLink(long SickTypeId, string type, string DicInfo, string CName, string sort, int page, int limit)
        {
            EntityList<LBDicCodeLinkVO> entityList = new EntityList<LBDicCodeLinkVO>();
            List<LBDicCodeLinkVO> diccodelinklist = new List<LBDicCodeLinkVO>();
            string labid = ZhiFang.Common.Public.Cookie.Get("000100");
            if (type == ContrastDicType.人员.Key)
            {
                List<HREmpIdentity> hREmps = new List<HREmpIdentity>();
                if (DicInfo == "1")
                {
                    string where = "";
                    if (string.IsNullOrEmpty(CName))
                    {
                        where = $"IsUse = 1 and LabID = {labid}";
                    }
                    else
                    {
                        where = $"IsUse = 1 and LabID = {labid} and CName like '%{CName}%'";
                    }
                    IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHREmployeeByHQL(where).toList<HREmpIdentity>();
                    hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;
                }
                else
                {
                    string where = "";
                    if (string.IsNullOrEmpty(CName))
                    {
                        where = $"hrempidentity.TSysCode='{DicInfo}' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = {labid}";
                    }
                    else
                    {
                        where = $"hremployee.CName like '%{CName}%' and hrempidentity.TSysCode='{DicInfo}' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = {labid}";
                    }
                    IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHREmpIdentityByHQL(where).toList<HREmpIdentity>();
                    hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;
                }
                if (hREmps != null && hREmps.Count > 0)
                {
                    IList<LBDicCodeLink> lBDicCodeLinks = DBDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.人员.Value.Code + "' and LinkSystemID = " + SickTypeId);
                    foreach (var emp in hREmps)
                    {
                        List<LBDicCodeLink> disposelist = lBDicCodeLinks.Where(a => a.DicDataID == emp.Id.ToString()).ToList();
                        if (disposelist.Count > 0)
                        {
                            foreach (var dcl in disposelist)
                            {
                                diccodelinklist.Add(ClassMapperHelp.GetMapper<LBDicCodeLinkVO, LBDicCodeLink>(dcl));
                            }
                        }
                        else
                        {
                            LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                            lBDicCodeLinkVO.Id = 0;
                            lBDicCodeLinkVO.DicTypeCode = ContrastDicType.人员.Value.Code;
                            lBDicCodeLinkVO.DicTypeName = ContrastDicType.人员.Value.Name;
                            lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                            lBDicCodeLinkVO.DicDataID = emp.Id.ToString();
                            lBDicCodeLinkVO.DicDataCode = emp.StandCode;
                            lBDicCodeLinkVO.DicDataName = emp.CName;
                            diccodelinklist.Add(lBDicCodeLinkVO);
                        }
                    }
                }
            }
            else if (type == ContrastDicType.部门.Key)
            {
                List<HREmpIdentity> hREmps = new List<HREmpIdentity>();
                if (DicInfo == "1")
                {
                    string where = "";
                    if (string.IsNullOrEmpty(CName))
                    {
                        where = $"IsUse = 1 and LabID = {labid}";
                    }
                    else
                    {
                        where = $"IsUse = 1 and LabID = {labid} and CName like '%{CName}%'";
                    }
                    IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHRDeptByHQL(where).toList<HREmpIdentity>();
                    hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;
                }
                else
                {
                    string where = "";
                    if (string.IsNullOrEmpty(CName))
                    {
                        where = $"hrempidentity.TSysCode='{DicInfo}' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = {labid}";
                    }
                    else
                    {
                        where = $"hrdept.CName like '%{CName}%'  and  hrempidentity.TSysCode='{DicInfo}' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = {labid}";
                    }
                    IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL(where).toList<HREmpIdentity>();
                    hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;
                }
                if (hREmps != null && hREmps.Count > 0)
                {
                    IList<LBDicCodeLink> lBDicCodeLinks = DBDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.部门.Value.Code + "' and LinkSystemID = " + SickTypeId);
                    foreach (var emp in hREmps)
                    {
                        List<LBDicCodeLink> disposelist = lBDicCodeLinks.Where(a => a.DicDataID == emp.Id.ToString()).ToList();
                        if (disposelist.Count > 0)
                        {
                            foreach (var dcl in disposelist)
                            {
                                diccodelinklist.Add(ClassMapperHelp.GetMapper<LBDicCodeLinkVO, LBDicCodeLink>(dcl));
                            }
                        }
                        else
                        {
                            LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                            lBDicCodeLinkVO.Id = 0;
                            lBDicCodeLinkVO.DicTypeCode = ContrastDicType.部门.Value.Code;
                            lBDicCodeLinkVO.DicTypeName = ContrastDicType.部门.Value.Name;
                            lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                            lBDicCodeLinkVO.DicDataID = emp.Id.ToString();
                            lBDicCodeLinkVO.DicDataCode = emp.StandCode;
                            lBDicCodeLinkVO.DicDataName = emp.CName;
                            diccodelinklist.Add(lBDicCodeLinkVO);
                        }
                    }
                }
            }
            else if (type == ContrastDicType.年龄单位.Key)
            {
                IList<LBDicCodeLink> lBDicCodeLinks = DBDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.年龄单位.Value.Code + "' and LinkSystemID = " + SickTypeId);
                var aut = new Dictionary<string, BaseClassDicEntity>();
                if (string.IsNullOrEmpty(CName))
                {
                    aut = AgeUnitType.GetStatusDic();
                }
                else
                {
                    var kvaut = AgeUnitType.GetStatusDic().Where(a => a.Value.Name.IndexOf(CName) != -1).ToList();
                    foreach (var item in kvaut)
                    {
                        aut.Add(item.Key, item.Value);
                    }
                }
                if (aut.Count > 0)
                {
                    foreach (var item in aut)
                    {
                        List<LBDicCodeLink> disposelist = lBDicCodeLinks.Where(a => a.DicDataID == item.Value.Id.ToString()).ToList();
                        if (disposelist.Count > 0)
                        {
                            foreach (var dcl in disposelist)
                            {
                                diccodelinklist.Add(ClassMapperHelp.GetMapper<LBDicCodeLinkVO, LBDicCodeLink>(dcl));
                            }
                        }
                        else
                        {
                            LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                            lBDicCodeLinkVO.Id = 0;
                            lBDicCodeLinkVO.DicTypeCode = ContrastDicType.年龄单位.Value.Code;
                            lBDicCodeLinkVO.DicTypeName = ContrastDicType.年龄单位.Value.Name;
                            lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                            lBDicCodeLinkVO.DicDataID = item.Value.Id.ToString();
                            lBDicCodeLinkVO.DicDataCode = item.Value.Code;
                            lBDicCodeLinkVO.DicDataName = item.Value.Name;
                            diccodelinklist.Add(lBDicCodeLinkVO);
                        }
                    }
                }
            }
            else if (type == ContrastDicType.性别.Key)
            {
                IList<LBDicCodeLink> lBDicCodeLinks = DBDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.性别.Value.Code + "' and LinkSystemID = " + SickTypeId);
                var aut = new Dictionary<string, BaseClassDicEntity>();
                if (string.IsNullOrEmpty(CName))
                {
                    aut = GenderType.GetStatusDic();
                }
                else
                {
                    var kvaut = GenderType.GetStatusDic().Where(a => a.Value.Name.IndexOf(CName) != -1).ToList();
                    foreach (var item in kvaut)
                    {
                        aut.Add(item.Key, item.Value);
                    }
                }
                if (aut.Count > 0)
                {
                    foreach (var item in aut)
                    {
                        List<LBDicCodeLink> disposelist = lBDicCodeLinks.Where(a => a.DicDataID == item.Value.Id.ToString()).ToList();
                        if (disposelist.Count > 0)
                        {
                            foreach (var dcl in disposelist)
                            {
                                diccodelinklist.Add(ClassMapperHelp.GetMapper<LBDicCodeLinkVO, LBDicCodeLink>(dcl));
                            }
                        }
                        else
                        {
                            LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                            lBDicCodeLinkVO.Id = 0;
                            lBDicCodeLinkVO.DicTypeCode = ContrastDicType.性别.Value.Code;
                            lBDicCodeLinkVO.DicTypeName = ContrastDicType.性别.Value.Name;
                            lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                            lBDicCodeLinkVO.DicDataID = item.Value.Id.ToString();
                            lBDicCodeLinkVO.DicDataCode = item.Value.Code;
                            lBDicCodeLinkVO.DicDataName = item.Value.Name;
                            diccodelinklist.Add(lBDicCodeLinkVO);
                        }
                    }
                }

            }
            else if (type == ContrastDicType.诊断类型.Key)
            {
                string where = "";
                if (string.IsNullOrEmpty(CName))
                {
                    where = $"IsUse = 1";
                }
                else
                {
                    where = $"IsUse = 1 and CName like '%{CName}%'";
                }
                IList<LBDiag> lBDiags = IDLBDiagDao.GetListByHQL(where);
                IList<LBDicCodeLink> lBDicCodeLinks = DBDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.诊断类型.Value.Code + "' and LinkSystemID = " + SickTypeId);
                if (lBDiags.Count > 0)
                {
                    foreach (var item in lBDiags)
                    {
                        List<LBDicCodeLink> disposelist = lBDicCodeLinks.Where(a => a.DicDataID == item.Id.ToString()).ToList();
                        if (disposelist.Count > 0)
                        {
                            foreach (var dcl in disposelist)
                            {
                                diccodelinklist.Add(ClassMapperHelp.GetMapper<LBDicCodeLinkVO, LBDicCodeLink>(dcl));
                            }
                        }
                        else
                        {
                            LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                            lBDicCodeLinkVO.Id = 0;
                            lBDicCodeLinkVO.DicTypeCode = ContrastDicType.诊断类型.Value.Code;
                            lBDicCodeLinkVO.DicTypeName = ContrastDicType.诊断类型.Value.Name;
                            lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                            lBDicCodeLinkVO.DicDataID = item.Id.ToString();
                            lBDicCodeLinkVO.DicDataCode = item.Shortcode;
                            lBDicCodeLinkVO.DicDataName = item.CName;
                            diccodelinklist.Add(lBDicCodeLinkVO);
                        }
                    }
                }

            }
            else if (type == ContrastDicType.样本类型.Key)
            {
                string where = "";
                if (string.IsNullOrEmpty(CName))
                {
                    where = $"IsUse = 1";
                }
                else
                {
                    where = $"IsUse = 1 and CName like '%{CName}%'";
                }
                IList<LBSampleType> lBSampleTypes = IDLBSampleTypeDao.GetListByHQL(where);
                IList<LBDicCodeLink> lBDicCodeLinks = DBDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.样本类型.Value.Code + "' and LinkSystemID = " + SickTypeId);
                if (lBSampleTypes.Count > 0)
                {
                    foreach (var item in lBSampleTypes)
                    {
                        List<LBDicCodeLink> disposelist = lBDicCodeLinks.Where(a => a.DicDataID == item.Id.ToString()).ToList();
                        if (disposelist.Count > 0)
                        {
                            foreach (var dcl in disposelist)
                            {
                                diccodelinklist.Add(ClassMapperHelp.GetMapper<LBDicCodeLinkVO, LBDicCodeLink>(dcl));
                            }
                        }
                        else
                        {
                            LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                            lBDicCodeLinkVO.Id = 0;
                            lBDicCodeLinkVO.DicTypeCode = ContrastDicType.样本类型.Value.Code;
                            lBDicCodeLinkVO.DicTypeName = ContrastDicType.样本类型.Value.Name;
                            lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                            lBDicCodeLinkVO.DicDataID = item.Id.ToString();
                            lBDicCodeLinkVO.DicDataCode = item.Shortcode;
                            lBDicCodeLinkVO.DicDataName = item.CName;
                            diccodelinklist.Add(lBDicCodeLinkVO);
                        }
                    }
                }
            }
            else if (type == ContrastDicType.民族.Key)
            {
                string where = "";
                if (string.IsNullOrEmpty(CName))
                {
                    where = $"IsUse = 1";
                }
                else
                {
                    where = $"IsUse = 1 and CName like '%{CName}%'";
                }
                IList<LBFolk> lBFolks = IDLBFolkDao.GetListByHQL(where);
                IList<LBDicCodeLink> lBDicCodeLinks = DBDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.民族.Value.Code + "' and LinkSystemID = " + SickTypeId);
                if (lBFolks.Count > 0)
                {
                    foreach (var item in lBFolks)
                    {
                        List<LBDicCodeLink> disposelist = lBDicCodeLinks.Where(a => a.DicDataID == item.Id.ToString()).ToList();
                        if (disposelist.Count > 0)
                        {
                            foreach (var dcl in disposelist)
                            {
                                diccodelinklist.Add(ClassMapperHelp.GetMapper<LBDicCodeLinkVO, LBDicCodeLink>(dcl));
                            }
                        }
                        else
                        {
                            LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                            lBDicCodeLinkVO.Id = 0;
                            lBDicCodeLinkVO.DicTypeCode = ContrastDicType.民族.Value.Code;
                            lBDicCodeLinkVO.DicTypeName = ContrastDicType.民族.Value.Name;
                            lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                            lBDicCodeLinkVO.DicDataID = item.Id.ToString();
                            lBDicCodeLinkVO.DicDataCode = item.Shortcode;
                            lBDicCodeLinkVO.DicDataName = item.CName;
                            diccodelinklist.Add(lBDicCodeLinkVO);
                        }
                    }
                }
            }
            diccodelinklist = diccodelinklist.OrderBy(a => a.DicDataID).ToList();
            entityList.count = diccodelinklist.Count;
            entityList.list = (diccodelinklist.Skip((page - 1) * limit).Take(limit)).ToList();
            return entityList;
        }

        public EntityList<LBDicCodeLinkVO> SearchBasicsDicDataBySickTypeId(long SickTypeId, string type, string DicInfo, string CName)
        {
            EntityList<LBDicCodeLinkVO> entityList = new EntityList<LBDicCodeLinkVO>();
            List<LBDicCodeLinkVO> diccodelinklist = new List<LBDicCodeLinkVO>();
            string labid = ZhiFang.Common.Public.Cookie.Get("000100");
            if (type == ContrastDicType.人员.Key)
            {
                List<HREmpIdentity> hREmps = new List<HREmpIdentity>();
                if (DicInfo == "1")
                {
                    string where = "";
                    if (string.IsNullOrEmpty(CName))
                    {
                        where = $"IsUse = 1 and LabID = {labid}";
                    }
                    else
                    {
                        where = $"IsUse = 1 and LabID = {labid} and CName like '%{CName}%'";
                    }
                    IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHREmployeeByHQL(where).toList<HREmpIdentity>();
                    hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;
                }
                else
                {
                    string where = "";
                    if (string.IsNullOrEmpty(CName))
                    {
                        where = $"hrempidentity.TSysCode='{DicInfo}' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = {labid}";
                    }
                    else
                    {
                        where = $"hremployee.CName like '%{CName}%' and hrempidentity.TSysCode='{DicInfo}' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = {labid}";
                    }
                    IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHREmpIdentityByHQL(where).toList<HREmpIdentity>();
                    hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;
                }
                if (hREmps != null && hREmps.Count > 0)
                {
                    foreach (var emp in hREmps)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicTypeCode = ContrastDicType.人员.Value.Code;
                        lBDicCodeLinkVO.DicTypeName = ContrastDicType.人员.Value.Name;
                        lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                        lBDicCodeLinkVO.DicDataID = emp.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = emp.StandCode;
                        lBDicCodeLinkVO.DicDataName = emp.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
            }
            else if (type == ContrastDicType.部门.Key)
            {
                List<HREmpIdentity> hREmps = new List<HREmpIdentity>();
                if (DicInfo == "1")
                {
                    string where = "";
                    if (string.IsNullOrEmpty(CName))
                    {
                        where = $"IsUse = 1 and LabID = {labid}";
                    }
                    else
                    {
                        where = $"IsUse = 1 and LabID = {labid} and CName like '%{CName}%'";
                    }
                    IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHRDeptByHQL(where).toList<HREmpIdentity>();
                    hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;
                }
                else
                {
                    string where = "";
                    if (string.IsNullOrEmpty(CName))
                    {
                        where = $"hrempidentity.TSysCode='{DicInfo}' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = {labid}";
                    }
                    else
                    {
                        where = $"hrdept.CName like '%{CName}%'  and  hrempidentity.TSysCode='{DicInfo}' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = {labid}";
                    }
                    IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL(where).toList<HREmpIdentity>();
                    hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;
                }
                if (hREmps != null && hREmps.Count > 0)
                {
                    foreach (var emp in hREmps)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicTypeCode = ContrastDicType.部门.Value.Code;
                        lBDicCodeLinkVO.DicTypeName = ContrastDicType.部门.Value.Name;
                        lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                        lBDicCodeLinkVO.DicDataID = emp.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = emp.StandCode;
                        lBDicCodeLinkVO.DicDataName = emp.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
            }
            else if (type == ContrastDicType.年龄单位.Key)
            {
                var aut = new Dictionary<string, BaseClassDicEntity>();
                if (string.IsNullOrEmpty(CName))
                {
                    aut = AgeUnitType.GetStatusDic();
                }
                else
                {
                    var kvaut = AgeUnitType.GetStatusDic().Where(a => a.Value.Name.IndexOf(CName) != -1).ToList();
                    foreach (var item in kvaut)
                    {
                        aut.Add(item.Key, item.Value);
                    }
                }
                if (aut.Count > 0)
                {
                    foreach (var item in aut)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicTypeCode = ContrastDicType.年龄单位.Value.Code;
                        lBDicCodeLinkVO.DicTypeName = ContrastDicType.年龄单位.Value.Name;
                        lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                        lBDicCodeLinkVO.DicDataID = item.Value.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = item.Value.Code;
                        lBDicCodeLinkVO.DicDataName = item.Value.Name;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
            }
            else if (type == ContrastDicType.性别.Key)
            {
                var aut = new Dictionary<string, BaseClassDicEntity>();
                if (string.IsNullOrEmpty(CName))
                {
                    aut = GenderType.GetStatusDic();
                }
                else
                {
                    var kvaut = GenderType.GetStatusDic().Where(a => a.Value.Name.IndexOf(CName) != -1).ToList();
                    foreach (var item in kvaut)
                    {
                        aut.Add(item.Key, item.Value);
                    }
                }
                if (aut.Count > 0)
                {
                    foreach (var item in aut)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicTypeCode = ContrastDicType.性别.Value.Code;
                        lBDicCodeLinkVO.DicTypeName = ContrastDicType.性别.Value.Name;
                        lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                        lBDicCodeLinkVO.DicDataID = item.Value.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = item.Value.Code;
                        lBDicCodeLinkVO.DicDataName = item.Value.Name;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }

            }
            else if (type == ContrastDicType.诊断类型.Key)
            {
                string where = "";
                if (string.IsNullOrEmpty(CName))
                {
                    where = $"IsUse = 1";
                }
                else
                {
                    where = $"IsUse = 1 and CName like '%{CName}%'";
                }
                IList<LBDiag> lBDiags = IDLBDiagDao.GetListByHQL(where);
                if (lBDiags.Count > 0)
                {
                    foreach (var item in lBDiags)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicTypeCode = ContrastDicType.诊断类型.Value.Code;
                        lBDicCodeLinkVO.DicTypeName = ContrastDicType.诊断类型.Value.Name;
                        lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                        lBDicCodeLinkVO.DicDataID = item.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = item.Shortcode;
                        lBDicCodeLinkVO.DicDataName = item.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }

            }
            else if (type == ContrastDicType.样本类型.Key)
            {
                string where = "";
                if (string.IsNullOrEmpty(CName))
                {
                    where = $"IsUse = 1";
                }
                else
                {
                    where = $"IsUse = 1 and CName like '%{CName}%'";
                }
                IList<LBSampleType> lBSampleTypes = IDLBSampleTypeDao.GetListByHQL(where);
                if (lBSampleTypes.Count > 0)
                {
                    foreach (var item in lBSampleTypes)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicTypeCode = ContrastDicType.样本类型.Value.Code;
                        lBDicCodeLinkVO.DicTypeName = ContrastDicType.样本类型.Value.Name;
                        lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                        lBDicCodeLinkVO.DicDataID = item.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = item.Shortcode;
                        lBDicCodeLinkVO.DicDataName = item.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
            }
            else if (type == ContrastDicType.民族.Key)
            {
                string where = "";
                if (string.IsNullOrEmpty(CName))
                {
                    where = $"IsUse = 1";
                }
                else
                {
                    where = $"IsUse = 1 and CName like '%{CName}%'";
                }
                IList<LBFolk> lBFolks = IDLBFolkDao.GetListByHQL(where);
                if (lBFolks.Count > 0)
                {
                    foreach (var item in lBFolks)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicTypeCode = ContrastDicType.民族.Value.Code;
                        lBDicCodeLinkVO.DicTypeName = ContrastDicType.民族.Value.Name;
                        lBDicCodeLinkVO.LinkSystemID = SickTypeId;
                        lBDicCodeLinkVO.DicDataID = item.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = item.Shortcode;
                        lBDicCodeLinkVO.DicDataName = item.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
            }
            diccodelinklist = diccodelinklist.OrderBy(a => a.DicDataID).ToList();
            entityList.count = diccodelinklist.Count;
            entityList.list = diccodelinklist;
            return entityList;
        }

        public EntityList<LBDicCodeLinkVO> GetParaDicData(string type)
        {
            #region 
            EntityList<LBDicCodeLinkVO> entityList = new EntityList<LBDicCodeLinkVO>();
            List<LBDicCodeLinkVO> diccodelinklist = new List<LBDicCodeLinkVO>();
            string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
            if (type == Pre_ParaDicType.科室.Value.Code)
            {
                #region 科室
                List<HREmpIdentity> hREmps = new List<HREmpIdentity>();
                string where = $"hrempidentity.TSysCode='{DeptSystemType.检验科室.Key}' and hrempidentity.SystemCode='ZF_PRE' and hrempidentity.LabID = {labid}";
                IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL(where).toList<HREmpIdentity>();
                hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;

                if (hREmps != null && hREmps.Count > 0)
                {
                    foreach (var emp in hREmps)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicDataID = emp.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = emp.StandCode;
                        lBDicCodeLinkVO.DicDataName = emp.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
                #endregion
            }
            else if (type == Pre_ParaDicType.执行科室.Value.Code)
            {
                #region 执行科室
                List<HREmpIdentity> hREmps = new List<HREmpIdentity>();
                string where = $"hrempidentity.TSysCode='{DeptSystemType.执行科室.Key}' and hrempidentity.SystemCode='ZF_PRE' and hrempidentity.LabID = {labid}";
                IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL(where).toList<HREmpIdentity>();
                hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;

                if (hREmps != null && hREmps.Count > 0)
                {
                    foreach (var emp in hREmps)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicDataID = emp.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = emp.StandCode;
                        lBDicCodeLinkVO.DicDataName = emp.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
                #endregion
            }
            else if (type == Pre_ParaDicType.病区.Value.Code)
            {
                #region 病区
                List<HREmpIdentity> hREmps = new List<HREmpIdentity>();
                string where = $"hrempidentity.TSysCode='{DeptSystemType.病区.Key}' and hrempidentity.SystemCode='ZF_PRE' and hrempidentity.LabID = {labid}";
                IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL(where).toList<HREmpIdentity>();
                hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;

                if (hREmps != null && hREmps.Count > 0)
                {
                    foreach (var emp in hREmps)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicDataID = emp.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = emp.StandCode;
                        lBDicCodeLinkVO.DicDataName = emp.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
                #endregion
            }
            else if (type == Pre_ParaDicType.院区.Value.Code)
            {
                #region 院区
                List<HREmpIdentity> hREmps = new List<HREmpIdentity>();
                string where = $"hrempidentity.TSysCode='{DeptSystemType.院区.Key}' and hrempidentity.SystemCode='ZF_PRE' and hrempidentity.LabID = {labid}";
                IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL(where).toList<HREmpIdentity>();
                hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;

                if (hREmps != null && hREmps.Count > 0)
                {
                    foreach (var emp in hREmps)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicDataID = emp.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = emp.StandCode;
                        lBDicCodeLinkVO.DicDataName = emp.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
                #endregion
            }
            else if (type == Pre_ParaDicType.送检目的地.Value.Code)
            {
                #region 送检目的地
                string where = $"IsUse = 1";
                IList<LBDestination> lBDestinations = IDLBDestinationDao.GetListByHQL(where);
                if (lBDestinations.Count > 0)
                {
                    foreach (var item in lBDestinations)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicDataID = item.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = item.Shortcode;
                        lBDicCodeLinkVO.DicDataName = item.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
                #endregion
            }
            else if (type == Pre_ParaDicType.样本类型.Value.Code)
            {
                #region 样本类型
                string where = $"IsUse = 1";
                IList<LBSampleType> lBSampleTypes = IDLBSampleTypeDao.GetListByHQL(where);
                if (lBSampleTypes.Count > 0)
                {
                    foreach (var item in lBSampleTypes)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicDataID = item.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = item.Shortcode;
                        lBDicCodeLinkVO.DicDataName = item.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
                #endregion
            }
            else if (type == Pre_ParaDicType.就诊类型.Value.Code)
            {
                #region 就诊类型
                string where = $"IsUse = 1";
                IList<LBSickType> lBSickTypes = IDLBSickTypeDao.GetListByHQL(where);
                if (lBSickTypes.Count > 0)
                {
                    foreach (var item in lBSickTypes)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicDataID = item.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = item.Shortcode;
                        lBDicCodeLinkVO.DicDataName = item.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
                #endregion
            }
            else if (type == Pre_ParaDicType.采样组.Value.Code)
            {
                #region 采样组
                string where = $"IsUse = 1";
                IList<LBSamplingGroup> lBSamplingGroups = IDLBSamplingGroupDao.GetListByHQL(where);
                if (lBSamplingGroups.Count > 0)
                {
                    foreach (var item in lBSamplingGroups)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicDataID = item.Id.ToString();
                        lBDicCodeLinkVO.DicDataName = item.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
                #endregion
            }
            else if (type == Pre_ParaDicType.样本状态.Value.Code)
            {
                #region 样本状态
                foreach (var item in BarCodeStatus.GetStatusDic())
                {
                    LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                    lBDicCodeLinkVO.Id = 0;
                    lBDicCodeLinkVO.DicDataID = item.Key;
                    lBDicCodeLinkVO.DicDataName = item.Value.Name;
                    diccodelinklist.Add(lBDicCodeLinkVO);
                }
                #endregion
            }
            else if (type == Pre_ParaDicType.检验小组.Value.Code)
            {
                #region 检验小组
                string where = $"IsUse = 1";
                IList<LBSection> lBSections = IDLBSectionDao.GetListByHQL(where);
                if (lBSections.Count > 0)
                {
                    foreach (var item in lBSections)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicDataID = item.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = item.Shortcode;
                        lBDicCodeLinkVO.DicDataName = item.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
                #endregion
            } 
            else if (type == Pre_ParaDicType.叫号系统流水号.Value.Code) {
                #region 叫号系统流水号
                var  list = LBTGetMaxNOBmsTypes.GetStatusDic().Where(a => a.Value.Name.IndexOf("叫号系统排队号预留流水号") > -1);
                foreach (var item in list)
                {
                    LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                    lBDicCodeLinkVO.Id = 0;
                    lBDicCodeLinkVO.DicDataID = item.Value.Code;
                    lBDicCodeLinkVO.DicDataCode = item.Value.Code;
                    lBDicCodeLinkVO.DicDataName = item.Value.Name;
                    diccodelinklist.Add(lBDicCodeLinkVO);
                }
                #endregion
            }
            else if(type == Pre_ParaDicType.开单科室.Value.Code)
            {
                #region 开单科室
                List<HREmpIdentity> hREmps = new List<HREmpIdentity>();
                string where = $"hrempidentity.TSysCode='{DeptSystemType.送检科室.Key}' and hrempidentity.SystemCode='ZF_PRE' and hrempidentity.LabID = {labid}";
                IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL(where).toList<HREmpIdentity>();
                hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;

                if (hREmps != null && hREmps.Count > 0)
                {
                    foreach (var emp in hREmps)
                    {
                        LBDicCodeLinkVO lBDicCodeLinkVO = new LBDicCodeLinkVO();
                        lBDicCodeLinkVO.Id = 0;
                        lBDicCodeLinkVO.DicDataID = emp.Id.ToString();
                        lBDicCodeLinkVO.DicDataCode = emp.StandCode;
                        lBDicCodeLinkVO.DicDataName = emp.CName;
                        diccodelinklist.Add(lBDicCodeLinkVO);
                    }
                }
                #endregion
            }
            diccodelinklist = diccodelinklist.OrderBy(a => a.DicDataID).ToList();
            entityList.count = diccodelinklist.Count;
            entityList.list = diccodelinklist;
            return entityList;
            #endregion
        }

        public BaseResultDataValue AddCopyLBDicCodeLinkContrast(string sickTypeIds, string thisSickTypeId, string dictype)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            if (string.IsNullOrEmpty(dictype))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "字典类型不可为空！";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(sickTypeIds))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "请选择要复制的对接系统不可为空！";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(thisSickTypeId))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "对接系统不可为空！";
                return baseResultDataValue;
            }
            var keyValuePairs = ContrastDicType.GetStatusDic().Where(a => a.Key == dictype);
            if (keyValuePairs == null || keyValuePairs.Count() == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "未找到字典类型！";
                return baseResultDataValue;
            }
            IList<LBDicCodeLink> lBDicCodeLinks = DBDao.GetListByHQL("LinkSystemID in (" + sickTypeIds + "," + thisSickTypeId + ") and DicTypeCode='" + keyValuePairs.First().Value.Code+"'");
            if (lBDicCodeLinks.Count > 0)
            {
                var lBSickType = IDLBSickTypeDao.Get(long.Parse(thisSickTypeId));
                //被复制的数据
                var copylbitemcodes = lBDicCodeLinks.Where(a => a.LinkSystemID != long.Parse(thisSickTypeId));
                //本数据
                var thislbitemcode = lBDicCodeLinks.Where(a => a.LinkSystemID == long.Parse(thisSickTypeId));
                if (copylbitemcodes != null && copylbitemcodes.Count() > 0)
                {
                    var copylbitemcodes2 = copylbitemcodes.ToList();
                    if (thislbitemcode != null && thislbitemcode.Count() > 0)
                    {
                        for (int i = 0; i < copylbitemcodes2.Count(); i++)
                        {
                            LBDicCodeLink lBItemCodeLink = new LBDicCodeLink();
                            lBItemCodeLink = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBDicCodeLink, LBDicCodeLink>(copylbitemcodes2[i]); ;
                            lBItemCodeLink.LinkSystemID = lBSickType.Id;
                            lBItemCodeLink.LinkSystemName = lBSickType.CName;
                            lBItemCodeLink.LinkSystemCode = lBSickType.Shortcode;
                            var lBItemCodeLinksByDicData = thislbitemcode.Where(a => a.DicDataID == copylbitemcodes2[i].DicDataID && a.LinkDicDataCode == copylbitemcodes2[i].LinkDicDataCode);
                            if (lBItemCodeLinksByDicData != null && lBItemCodeLinksByDicData.Count() > 0)
                            {
                                lBItemCodeLink.Id = lBItemCodeLinksByDicData.First().Id;
                                lBItemCodeLink.DataUpdateTime = DateTime.Now;
                                if (!DBDao.Update(lBItemCodeLink))
                                {
                                    baseResultDataValue.success = false;
                                    baseResultDataValue.ErrorInfo = "复制失败！";
                                    return baseResultDataValue;
                                }
                            }
                            else
                            {
                                lBItemCodeLink.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                                lBItemCodeLink.DataAddTime = DateTime.Now;
                                if (!DBDao.Save(lBItemCodeLink))
                                {
                                    baseResultDataValue.success = false;
                                    baseResultDataValue.ErrorInfo = "复制失败！";
                                    return baseResultDataValue;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < copylbitemcodes2.Count(); i++)
                        {
                            LBDicCodeLink lBItemCodeLink = new LBDicCodeLink();
                            lBItemCodeLink = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBDicCodeLink, LBDicCodeLink>(copylbitemcodes2[i]);
                            lBItemCodeLink.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                            lBItemCodeLink.LinkSystemID = lBSickType.Id;
                            lBItemCodeLink.LinkSystemName = lBSickType.CName;
                            lBItemCodeLink.LinkSystemCode = lBSickType.Shortcode;
                            lBItemCodeLink.DataAddTime = DateTime.Now;
                            lBItemCodeLink.DataTimeStamp = null;
                            if (!DBDao.Save(lBItemCodeLink))
                            {
                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "复制失败！";
                                return baseResultDataValue;
                            }
                        }
                    }
                }
            }
            return baseResultDataValue;
        }

        public HISInterfaceHISOrderFromVO HISOrderFormDataDicContrast(HISInterfaceHISOrderFromVO hISInterfaceHISOrderFrom,out bool DataFormatISOK)
        {
            DataFormatISOK = true;
            foreach (var lisPatient in hISInterfaceHISOrderFrom.LisPatientList)
            {
                if (lisPatient.SickTypeID != null && lisPatient.SickTypeID != 0) {
                    var lBDicCodeLinks = DBDao.GetListByHQL("LinkSystemID="+ lisPatient.SickTypeID);
                    var lBItemCodeLinks = IDLBItemCodeLinkDao.GetListByHQL("LinkSystemID=" + lisPatient.SickTypeID);
                    if (lBDicCodeLinks.Count() > 0) {
                        #region 病人就诊信息表中字典对照
                        //年龄单位
                        if (lisPatient.AgeUnitID != null && lisPatient.AgeUnitID != 0) {
                            var  ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.年龄单位.Value.Code && a.LinkDicDataCode == lisPatient.AgeUnitID.ToString());
                            if (ageunit.Count() > 0) {
                                lisPatient.AgeUnitID = long.Parse(ageunit.First().DicDataID);
                                lisPatient.AgeUnitName = ageunit.First().DicDataName;
                            }
                        }
                        //性别
                        if (lisPatient.GenderID != null && lisPatient.GenderID != 0)
                        {
                            var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.性别.Value.Code && a.LinkDicDataCode == lisPatient.GenderID.ToString());
                            if (ageunit.Count() > 0)
                            {
                                lisPatient.GenderID = int.Parse(ageunit.First().DicDataID);
                                lisPatient.GenderName = ageunit.First().DicDataName;
                            }
                        }
                        //民族
                        if (lisPatient.FolkID != null && lisPatient.FolkID != 0)
                        {
                            var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.民族.Value.Code && a.LinkDicDataCode == lisPatient.FolkID.ToString());
                            if (ageunit.Count() > 0)
                            {
                                lisPatient.FolkID = long.Parse(ageunit.First().DicDataID);
                                lisPatient.FolkName = ageunit.First().DicDataName;
                            }
                        }
                        //诊断
                        if (lisPatient.DiagID != null && lisPatient.DiagID != 0)
                        {
                            var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.诊断类型.Value.Code && a.LinkDicDataCode == lisPatient.DiagID.ToString());
                            if (ageunit.Count() > 0)
                            {
                                lisPatient.DiagID = long.Parse(ageunit.First().DicDataID);
                                lisPatient.DiagName = ageunit.First().DicDataName;
                            }
                        }
                        //医生
                        if (lisPatient.DoctorID != null && lisPatient.DoctorID != 0)
                        {
                            var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.人员.Value.Code && a.LinkDicDataCode == lisPatient.DoctorID.ToString());
                            if (ageunit.Count() > 0)
                            {
                                lisPatient.DoctorID = long.Parse(ageunit.First().DicDataID);
                                lisPatient.DoctorName = ageunit.First().DicDataName;
                            }
                        }
                        //执行科室
                        if (lisPatient.ExecDeptID != null && lisPatient.ExecDeptID != 0)
                        {
                            var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.部门.Value.Code && a.LinkDicDataCode == lisPatient.ExecDeptID.ToString());
                            if (ageunit.Count() > 0)
                            {
                                lisPatient.ExecDeptID = long.Parse(ageunit.First().DicDataID);
                            }
                        }
                        //科室
                        if (lisPatient.DeptID != null && lisPatient.DeptID != 0)
                        {
                            var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.部门.Value.Code && a.LinkDicDataCode == lisPatient.DeptID.ToString());
                            if (ageunit.Count() > 0)
                            {
                                lisPatient.DeptID = long.Parse(ageunit.First().DicDataID);
                                lisPatient.DeptName = ageunit.First().DicDataName;
                            }
                        }
                        //病区
                        if (lisPatient.DistrictID != null && lisPatient.DistrictID != 0)
                        {
                            var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.部门.Value.Code && a.LinkDicDataCode == lisPatient.DistrictID.ToString());
                            if (ageunit.Count() > 0)
                            {
                                lisPatient.DistrictID = long.Parse(ageunit.First().DicDataID);
                                lisPatient.DistrictName = ageunit.First().DicDataName;
                            }
                        }
                        //病房
                        if (lisPatient.WardID != null && lisPatient.WardID != 0)
                        {
                            var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.部门.Value.Code && a.LinkDicDataCode == lisPatient.WardID.ToString());
                            if (ageunit.Count() > 0)
                            {
                                lisPatient.WardID = long.Parse(ageunit.First().DicDataID);
                                lisPatient.WardName = ageunit.First().DicDataName;
                            }
                        }
                        #endregion
                        if (lisPatient.LisOrderFormList.Count > 0) {
                            foreach (var lisOrderFormVO in lisPatient.LisOrderFormList)
                            {
                                #region 医嘱单
                                //科室
                                if (lisOrderFormVO.HospitalID != null && lisOrderFormVO.HospitalID != 0)
                                {
                                    var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.部门.Value.Code && a.LinkDicDataCode == lisOrderFormVO.HospitalID.ToString());
                                    if (ageunit.Count() > 0)
                                    {
                                        lisOrderFormVO.HospitalID = long.Parse(ageunit.First().DicDataID);
                                    }
                                }
                                //检验执行科室
                                if (lisOrderFormVO.ExecDeptID != null && lisOrderFormVO.ExecDeptID != 0)
                                {
                                    var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.部门.Value.Code && a.LinkDicDataCode == lisOrderFormVO.ExecDeptID.ToString());
                                    if (ageunit.Count() > 0)
                                    {
                                        lisOrderFormVO.ExecDeptID = long.Parse(ageunit.First().DicDataID);
                                    }
                                }
                                //送检目的地
                                if (lisOrderFormVO.DestinationID != null && lisOrderFormVO.DestinationID != 0)
                                {
                                    var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.部门.Value.Code && a.LinkDicDataCode == lisOrderFormVO.DestinationID.ToString());
                                    if (ageunit.Count() > 0)
                                    {
                                        lisOrderFormVO.DestinationID = long.Parse(ageunit.First().DicDataID);
                                    }
                                }
                                //送检目的地
                                if (lisOrderFormVO.ClientID != null && lisOrderFormVO.ClientID != 0)
                                {
                                    var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.部门.Value.Code && a.LinkDicDataCode == lisOrderFormVO.ClientID.ToString());
                                    if (ageunit.Count() > 0)
                                    {
                                        lisOrderFormVO.ClientID = long.Parse(ageunit.First().DicDataID);
                                    }
                                }
                                #endregion
                                #region 医嘱项目
                                if (lisOrderFormVO.LisOrderItemList.Count > 0)
                                {
                                    foreach (var lisOrderItemVO in lisOrderFormVO.LisOrderItemList)
                                    {
                                        //样本类型
                                        if (lisOrderItemVO.SampleTypeID != null && lisOrderItemVO.SampleTypeID != 0)
                                        {
                                            var ageunit = lBDicCodeLinks.Where(a => a.DicTypeCode == ContrastDicType.样本类型.Value.Code && a.LinkDicDataCode == lisOrderItemVO.SampleTypeID.ToString());
                                            if (ageunit.Count() > 0)
                                            {
                                                lisOrderItemVO.SampleTypeID = long.Parse(ageunit.First().DicDataID);
                                            }
                                        }
                                        //项目ID
                                        if (lisOrderItemVO.OrdersItemID != null && lisOrderItemVO.OrdersItemID != 0)
                                        {
                                            var ageunit = lBItemCodeLinks.Where(a => a.LinkDicDataCode == lisOrderItemVO.OrdersItemID.ToString());
                                            if (ageunit.Count() > 0)
                                            {
                                                lisOrderItemVO.OrdersItemID = ageunit.First().DicDataID;
                                            }
                                        }
                                    }
                                }
                                else {
                                    DataFormatISOK = false;
                                    return hISInterfaceHISOrderFrom;
                                }
                                #endregion
                            }
                        }
                        else {
                            DataFormatISOK = false;
                            return hISInterfaceHISOrderFrom;
                        }
                    }
                }
            }
            return hISInterfaceHISOrderFrom;
        }
    }
}