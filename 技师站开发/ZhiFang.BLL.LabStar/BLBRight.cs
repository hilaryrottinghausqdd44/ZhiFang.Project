using System;
using System.Linq;
using System.Collections.Generic;
using ZhiFang.IDAO.LabStar;
using ZhiFang.IBLL.LabStar;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBRight : BaseBLL<LBRight>, ZhiFang.IBLL.LabStar.IBLBRight
    {
        IBLisCommon IBLisCommon { get; set; }

        IBLBSection IBLBSection { get; set; }

        IBLisOperate IBLisOperate { get; set; }

        public IList<string> QueryEmpIDBySectionID(long sectionID)
        {
            IList<LBRight> listLBRight = this.SearchListByHQL(" lbright.LBSection.Id=" + sectionID + " and lbright.EmpID is not null ");
            return listLBRight.Select(p => p.EmpID.ToString()).ToList();
        }

        //public EntityList<LBSection> QueryCommoSectionByEmpID(string empIDList)
        //{
        //    EntityList<LBSection> entityList = new EntityList<LBSection>();
        //    if (string.IsNullOrWhiteSpace(empIDList))
        //        return entityList;
        //    try
        //    {
        //        long[] arrayEmpID = Array.ConvertAll<string, long>(empIDList.Split(','), p => long.Parse(p));

        //        IList<LBRight> listLBRight = this.SearchListByHQL(" lbright.EmpID in (" + String.Join(",", arrayEmpID) + ")");

        //        if (listLBRight == null || listLBRight.Count == 0)
        //            return entityList;
        //        var listGroupByEntity = from SectionRight in listLBRight
        //                                group SectionRight by
        //                                new
        //                                {
        //                                    LBSection = SectionRight.LBSection,
        //                                } into g
        //                                select new
        //                                {
        //                                    LBSection = g.Key.LBSection,
        //                                    Count = g.Count()
        //                                };
        //        IList<LBSection> listLBSection = listGroupByEntity.Where(p => p.Count == arrayEmpID.Length).Select(p => p.LBSection).ToList();

        //        if (listLBSection != null)
        //        {
        //            entityList.count = listLBSection.Count;
        //            entityList.list = listLBSection;
        //        }
        //        return entityList;
        //    }
        //    catch
        //    {
        //        return entityList;
        //    }
        //}

        public EntityList<LBSection> QueryCommoSectionByEmpID(string empIDList)
        {
            EntityList<LBSection> entityList = new EntityList<LBSection>();
            if (string.IsNullOrWhiteSpace(empIDList))
                return entityList;
            try
            {
                long[] arrayEmpID = Array.ConvertAll<string, long>(empIDList.Split(','), p => long.Parse(p));
                return (DBDao as IDLBRightDao).QueryCommoSectionByEmpIDDao(arrayEmpID);
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error(ex.Message);
                return entityList;
            }          
        }

        public BaseResultDataValue AddEmpSectionDataRight(string empIDList, string sectionIDList, string roleIDList, string operateID, string operater)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            
            if ((!string.IsNullOrWhiteSpace(empIDList)) && (!string.IsNullOrWhiteSpace(sectionIDList)))
            {
                string strWhere = " 1=1 ";
                string strWhereSection = "";

                long[] arrayEmpID = Array.ConvertAll<string, long>(empIDList.Split(','), p => long.Parse(p));
                strWhere += " and lbright.EmpID in (" + String.Join(",", arrayEmpID) + ")";

                long[] arraySectionID = Array.ConvertAll<string, long>(sectionIDList.Split(','), p => long.Parse(p));
                strWhere += " and lbright.LBSection.Id in (" + String.Join(",", arraySectionID) + ")";
                strWhereSection = " lbsection.Id in (" + String.Join(",", arraySectionID) + ")";

                long[] arrayRoleID = null;
                if (!string.IsNullOrWhiteSpace(roleIDList))
                {
                    arrayRoleID = Array.ConvertAll<string, long>(roleIDList.Split(','), p => long.Parse(p));
                    strWhere += " and lbright.RoleID in (" + String.Join(",", arrayRoleID) + ")";
                }

                IList<LBRight> listOldRight = this.SearchListByHQL(strWhere);
                IList<LBSection> listLBSection = IBLBSection.SearchListByHQL(strWhereSection);
                foreach (long empID in arrayEmpID)
                {
                    foreach (long sectionID in arraySectionID)
                    {
                        if (arrayRoleID != null)
                        {
                            foreach (long roleID in arrayRoleID)
                            {
                                IList<LBRight> tempRightList = listOldRight.Where(p => p.EmpID == empID && p.LBSection.Id == sectionID && p.RoleID == roleID).ToList();
                                if (tempRightList == null || tempRightList.Count == 0)
                                {
                                    LBRight right = new LBRight();
                                    right.EmpID = empID;
                                    right.LBSection = listLBSection.FirstOrDefault(p => p.Id == sectionID);
                                    right.RoleID = roleID;
                                    right.Operator = operater;
                                    if (!string.IsNullOrWhiteSpace(operateID))
                                        right.OperatorID = long.Parse(operateID);
                                    this.Entity = right;
                                    this.Add();
                                }
                            }
                        }
                        else
                        {
                            IList<LBRight> tempRightList = listOldRight.Where(p => p.EmpID == empID && p.LBSection.Id == sectionID).ToList();
                            if (tempRightList == null || tempRightList.Count == 0)
                            {
                                LBRight right = new LBRight();
                                right.EmpID = empID;
                                right.LBSection = listLBSection.FirstOrDefault(p => p.Id == sectionID);
                                right.Operator = operater;
                                if (!string.IsNullOrWhiteSpace(operateID))
                                    right.OperatorID = long.Parse(operateID);
                                this.Entity = right;
                                this.Add();
                            }
                        }
                    }
                }
            }
            return brdv;
        }

        public BaseResultDataValue DelelteEmpSectionDataRight(string empIDList, string sectionIDList, string roleIDList, string empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrWhiteSpace(empIDList) && string.IsNullOrWhiteSpace(sectionIDList) && string.IsNullOrWhiteSpace(roleIDList))
            {
                brdv.success = false;
                brdv.ErrorInfo = "无任何要删除的数据！";
                return brdv;
            }
            string strWhere = "";
            if ((!string.IsNullOrWhiteSpace(empIDList)) && (!string.IsNullOrWhiteSpace(sectionIDList)) && (!string.IsNullOrWhiteSpace(roleIDList)))
            {

                long[] arrayEmpID = Array.ConvertAll<string, long>(empIDList.Split(','), p => long.Parse(p));
                strWhere += " and EmpID in (" + String.Join(",", arrayEmpID) + ")";

                long[] arraySectionID = Array.ConvertAll<string, long>(sectionIDList.Split(','), p => long.Parse(p));
                strWhere += " and SectionID in (" + String.Join(",", arraySectionID) + ")";

                long[] arrayRoleID = Array.ConvertAll<string, long>(roleIDList.Split(','), p => long.Parse(p));
                strWhere += " and RoleID in (" + String.Join(",", arrayRoleID) + ")";
            }
            else if ((!string.IsNullOrWhiteSpace(empIDList)) && (!string.IsNullOrWhiteSpace(sectionIDList)))
            {
                long[] arrayEmpID = Array.ConvertAll<string, long>(empIDList.Split(','), p => long.Parse(p));
                strWhere += " and EmpID in (" + String.Join(",", arrayEmpID) + ")";

                long[] arraySectionID = Array.ConvertAll<string, long>(sectionIDList.Split(','), p => long.Parse(p));
                strWhere += " and SectionID in (" + String.Join(",", arraySectionID) + ")";
            }

            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                IBLisCommon.ExecSQL(" delete LB_Right from LB_Right lbright where 1=1 " + strWhere + DBDao.GetBaseDataFilter());
            }
            return brdv;

        }

    }
}