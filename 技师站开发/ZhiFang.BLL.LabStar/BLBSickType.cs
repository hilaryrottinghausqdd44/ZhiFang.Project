using System.Collections.Generic;
using System.Data;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LIIP;
using ZhiFang.LabStar.Common;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBSickType : BaseBLL<LBSickType>, ZhiFang.IBLL.LabStar.IBLBSickType
    {
        IDAO.LabStar.IDLBItemDao IDLBItemDao { get; set; }
        IDAO.LabStar.IDLBSectionItemDao IDLBSectionItemDao { get; set; }
        IDAO.LabStar.IDLBItemCodeLinkDao IDLBItemCodeLinkDao { get; set; }
        IDAO.LabStar.IDLBDiagDao IDLBDiagDao { get; set; }
        IDAO.LabStar.IDLBSampleTypeDao IDLBSampleTypeDao { get; set; }
        IDAO.LabStar.IDLBDicCodeLinkDao IDLBDicCodeLinkDao { get; set; }
        IDAO.LabStar.IDLBFolkDao IDLBFolkDao { get; set; }

        public DataTable LS_UDTO_SearchLBSickTypeDicContrastNumByHQL(long SectionID, int GroupType, string CName)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SickTypeID");
            dataTable.Columns.Add("CName");
            dataTable.Columns.Add("Shortcode");
            dataTable.Columns.Add("DicCNum");
            dataTable.Columns.Add("AllNum");

            IList<LBSickType> lBSickTypes = DBDao.GetListByHQL("IsUse=1");
            if (lBSickTypes.Count > 0)
            {
                foreach (var item in lBSickTypes.OrderBy(a => a.DispOrder))
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["SickTypeID"] = item.Id;
                    dataRow["CName"] = item.CName;
                    dataRow["Shortcode"] = item.Shortcode;

                    IList<LBItem> lBItems = IDLBItemDao.GetListByHQL("IsUse = 1");
                    if (lBItems.Count > 0)
                    {
                        dataRow["AllNum"] = lBItems.Count;
                        dataRow["DicCNum"] = IDLBItemCodeLinkDao.GetListByHQL("LinkSystemID = " + item.Id + " and LinkSystemCode = '" + item.Shortcode + "'").GroupBy(a => a.DicDataID).Count();
                    }
                    else
                    {
                        dataRow["AllNum"] = 0;
                        dataRow["DicCNum"] = 0;
                    }

                    #region 废弃
                    /*if (SectionID != 0)
					{
						string sectionwhere = "LBSection.Id = "+ SectionID+ " and IsUse = 1 and LBItem.IsUse = 1 ";
						if (GroupType != 0) {
							sectionwhere += " and LBItem.GroupType = " + (GroupType-1);
						}
						if (!string.IsNullOrEmpty(CName)) {
							sectionwhere += " and (LBItem.CName like '%"+ CName + "%' or LBItem.SName like '%" + CName + "%')";
						}
						IList< LBSectionItem >  lBSectionItems = IDLBSectionItemDao.GetListByHQL(sectionwhere);
						dataRow["AllNum"] = lBSectionItems.Count;
						if (lBSectionItems.Count > 0) 
						{
							List<long> itemid = new List<long>();
							lBSectionItems.ToList().ForEach(a=> itemid.Add(a.LBItem.Id));
							dataRow["DicCNum"] = IDLBItemCodeLinkDao.GetListByHQL("LinkSystemID = " + item.Id + " and LinkSystemCode = '" + item.Shortcode + "' and DicDataID in (" + string.Join(",", itemid) + ")").GroupBy(a => a.DicDataID).Count();
						}
						else 
						{
							dataRow["DicCNum"] = 0;
						}
					}
					else {
						string sectionwhere = " IsUse = 1 ";
						if (GroupType != 0)
						{
							sectionwhere += "  and lbitem.GroupType = " + (GroupType - 1);
						}
						if (!string.IsNullOrEmpty(CName))
						{
							sectionwhere += " and (lbitem.CName like '%" + CName + "%' or lbitem.SName like '%"+CName+"%')";
						}
						IList<LBItem> lBItems = IDLBItemDao.GetListByHQL(sectionwhere);
						dataRow["AllNum"] = lBItems.Count;
						if (lBItems.Count > 0)
						{
							List<long> itemid = new List<long>();
							lBItems.ToList().ForEach(a => itemid.Add(a.Id));
							dataRow["DicCNum"] = IDLBItemCodeLinkDao.GetListByHQL("LinkSystemID = " + item.Id + " and LinkSystemCode = '" + item.Shortcode + "' and DicDataID in (" + string.Join(",", itemid) + ")").GroupBy(a => a.DicDataID).Count();
						}
						else
						{
							dataRow["DicCNum"] = 0;
						}
					}*/
                    #endregion
                    dataTable.Rows.Add(dataRow);
                }
            }
            else
            {
                return new DataTable();
            }
            return dataTable;
        }

        public DataTable LS_UDTO_SearchLBSickTypeOtherDicContrastNum(string type)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SickTypeID");
            dataTable.Columns.Add("CName");
            dataTable.Columns.Add("Shortcode");
            dataTable.Columns.Add("DicCNum");
            dataTable.Columns.Add("AllNum");
            string labid = ZhiFang.Common.Public.Cookie.Get("000100");
            IList<LBSickType> lBSickTypes = DBDao.GetListByHQL("IsUse=1");
            if (lBSickTypes.Count > 0)
            {
                foreach (var item in lBSickTypes.OrderBy(a => a.DispOrder))
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["SickTypeID"] = item.Id;
                    dataRow["CName"] = item.CName;
                    dataRow["Shortcode"] = item.Shortcode;
                    if (type == ContrastDicType.人员.Key)
                    {
                        string where = $"IsUse = 1 and LabID = {labid}";
                        IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHREmployeeByHQL(where).toList<HREmpIdentity>();
                        if (hREmpIdentities != null && hREmpIdentities.Count > 0)
                        {
                            dataRow["AllNum"] = hREmpIdentities.Count;
                            dataRow["DicCNum"] = IDLBDicCodeLinkDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.人员.Value.Code + "' and LinkSystemID = " + item.Id + " and LinkSystemCode = '" + item.Shortcode + "'").GroupBy(a => a.DicDataID).Count();
                        }
                        else
                        {
                            dataRow["AllNum"] = 0;
                            dataRow["DicCNum"] = 0;
                        }
                    }
                    else if (type == ContrastDicType.部门.Key)
                    {
                        string where = $"IsUse = 1 and LabID = {labid}";
                        IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHRDeptByHQL(where).toList<HREmpIdentity>();
                        if (hREmpIdentities != null && hREmpIdentities.Count > 0)
                        {
                            dataRow["AllNum"] = hREmpIdentities.Count;
                            dataRow["DicCNum"] = IDLBDicCodeLinkDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.部门.Value.Code + "' and LinkSystemID = " + item.Id + " and LinkSystemCode = '" + item.Shortcode + "'").GroupBy(a => a.DicDataID).Count();
                        }
                        else
                        {
                            dataRow["AllNum"] = 0;
                            dataRow["DicCNum"] = 0;
                        }
                    }
                    else if (type == ContrastDicType.年龄单位.Key)
                    {
                        dataRow["AllNum"] = AgeUnitType.GetStatusDic().Count;
                        dataRow["DicCNum"] = IDLBDicCodeLinkDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.年龄单位.Value.Code + "' and LinkSystemID = " + item.Id + " and LinkSystemCode = '" + item.Shortcode + "'").GroupBy(a => a.DicDataID).Count();

                    }
                    else if (type == ContrastDicType.性别.Key)
                    {
                        dataRow["AllNum"] = GenderType.GetStatusDic().Count;
                        dataRow["DicCNum"] = IDLBDicCodeLinkDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.性别.Value.Code + "' and LinkSystemID = " + item.Id + " and LinkSystemCode = '" + item.Shortcode + "'").GroupBy(a => a.DicDataID).Count();
                    }
                    else if (type == ContrastDicType.诊断类型.Key)
                    {
                        long count = IDLBDiagDao.GetTotalCount();
                        if (count > 0)
                        {
                            dataRow["AllNum"] = count;
                            dataRow["DicCNum"] = IDLBDicCodeLinkDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.诊断类型.Value.Code + "' and LinkSystemID = " + item.Id + " and LinkSystemCode = '" + item.Shortcode + "'").GroupBy(a => a.DicDataID).Count();
                        }
                        else
                        {
                            dataRow["AllNum"] = 0;
                            dataRow["DicCNum"] = 0;
                        }
                    }
                    else if (type == ContrastDicType.样本类型.Key)
                    {
                        long count = IDLBSampleTypeDao.GetTotalCount();
                        if (count > 0)
                        {
                            dataRow["AllNum"] = count;
                            dataRow["DicCNum"] = IDLBDicCodeLinkDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.样本类型.Value.Code + "' and LinkSystemID = " + item.Id + " and LinkSystemCode = '" + item.Shortcode + "'").GroupBy(a => a.DicDataID).Count();
                        }
                        else
                        {
                            dataRow["AllNum"] = 0;
                            dataRow["DicCNum"] = 0;
                        }
                    }
                    else if (type == ContrastDicType.民族.Key)
                    {
                        long count = IDLBFolkDao.GetTotalCount();
                        if (count > 0)
                        {
                            dataRow["AllNum"] = count;
                            dataRow["DicCNum"] = IDLBDicCodeLinkDao.GetListByHQL("DicTypeCode = '" + ContrastDicType.民族.Value.Code + "' and LinkSystemID = " + item.Id + " and LinkSystemCode = '" + item.Shortcode + "'").GroupBy(a => a.DicDataID).Count();
                        }
                        else
                        {
                            dataRow["AllNum"] = 0;
                            dataRow["DicCNum"] = 0;
                        }
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            else
            {
                return new DataTable();
            }
            return dataTable;
        }
    }
}