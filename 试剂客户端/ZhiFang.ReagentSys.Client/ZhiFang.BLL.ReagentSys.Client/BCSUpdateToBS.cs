using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn;
using System.Collections;
using System.Text;

namespace ZhiFang.BLL.ReagentSys.Client
{
    public class BCSUpdateToBS : BaseBLL<SServiceClient>, IBCSUpdateToBS
    {
        IDReaCenOrgDao IDReaCenOrgDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IBSServiceClient IBSServiceClient { get; set; }
        IBCenOrg IBCenOrg { get; set; }
        IBHRDept IBHRDept { get; set; }
        IBHREmployee IBHREmployee { get; set; }
        IBRBACUser IBRBACUser { get; set; }
        IBRBACRole IBRBACRole { get; set; }
        IBReaCenOrg IBReaCenOrg { get; set; }
        IBReaStorage IBReaStorage { get; set; }
        IBReaPlace IBReaPlace { get; set; }
        IBReaGoods IBReaGoods { get; set; }
        IBReaEquipReagentLink IBReaEquipReagentLink { get; set; }
        IBReaGoodsOrgLink IBReaGoodsOrgLink { get; set; }
        IBReaTestEquipLab IBReaTestEquipLab { get; set; }
        IBReaBmsQtyDtl IBReaBmsQtyDtl { get; set; }
        IBReaBmsInDtl IBReaBmsInDtl { get; set; }
        IBReaBmsInDoc IBReaBmsInDoc { get; set; }
        IBReaBmsQtyDtlOperation IBReaBmsQtyDtlOperation { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        IBReaDeptGoods IBReaDeptGoods { get; set; }
        IBReaBmsSerial IBReaBmsSerial { get; set; }
        IBReaGoodsLot IBReaGoodsLot { get; set; }
        IBBParameter IBBParameter { get; set; }

        public BaseResultDataValue DeleteCSUpdateToBSQtyDtlInfo(long labId, string entity, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            SServiceClient sServiceClient = IBSServiceClient.Get(labId);
            CenOrg cenOrg = IBCenOrg.SearchListByHQL("LabId=" + labId)[0];
            switch (entity)
            {
                case "DeleteOldQtyInfo":
                    DeleteOldQtyDtlInfo(cenOrg, empID, empName, ref baseResultDataValue);
                    break;
                default:
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "传入参数(entity)不匹配!";
                    break;
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// CS客户端升级到BS客户端分步处理
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultDataValue AddCSUpdateToBSByStep(long labId, string entity, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            SServiceClient sServiceClient = IBSServiceClient.Get(labId);
            CenOrg cenOrg = IBCenOrg.SearchListByHQL("LabId=" + labId)[0];

            switch (entity)
            {
                case "HRDept":
                    AddHRDeptList(cenOrg, ref baseResultDataValue);
                    break;
                case "HREmployee|RBACUser":
                    try
                    {
                        AddHREmployeeAndRBACUserList(cenOrg, ref baseResultDataValue);
                    }
                    catch (Exception ex)
                    {
                        ZhiFang.Common.Log.Log.Error("CS试剂客户端升级BS失败.AddHREmployeeAndRBACUserList:Message:" + ex.Message);
                        ZhiFang.Common.Log.Log.Error("CS试剂客户端升级BS失败.AddHREmployeeAndRBACUserList:StackTrace:" + ex.StackTrace);
                        throw ex;
                    }
                    break;
                case "RBACRole":
                    AddRBACRoleList(cenOrg, ref baseResultDataValue);
                    break;
                case "ReaCenOrg":
                    AddReaCenOrgList(cenOrg, ref baseResultDataValue);
                    break;
                case "ReaStorage":
                    AddReaStorageList(cenOrg, ref baseResultDataValue);
                    break;
                case "ReaPlace":
                    AddReaPlaceList(cenOrg, ref baseResultDataValue);
                    break;
                case "ReaTestEquipLab":
                    AddReaTestEquipLabList(cenOrg, ref baseResultDataValue);
                    break;
                case "ReaGoods":
                    AddReaGoodsList(cenOrg, ref baseResultDataValue);
                    break;
                case "ReaEquipReagentLink":
                    AddReaEquipReagentLinkList(cenOrg, ref baseResultDataValue);
                    break;
                case "ReaGoodsOrgLink":
                    AddReaGoodsOrgLinkList(cenOrg, ref baseResultDataValue);
                    break;
                case "ReaDeptGoods":
                    AddReaDeptGoodsList(cenOrg, ref baseResultDataValue);
                    break;
                case "DeleteOldQtyInfo":
                    DeleteOldQtyDtlInfo(cenOrg, empID, empName, ref baseResultDataValue);
                    break;
                case "ReaBmsQtyDtl":
                    DeleteOldQtyDtlInfo(cenOrg, empID, empName, ref baseResultDataValue);
                    AddReaBmsQtyDtlList(cenOrg, empID, empName, ref baseResultDataValue);
                    AddReaBmsSerialList(cenOrg, ref baseResultDataValue);
                    break;
                default:
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "传入参数(entity)不匹配!";
                    break;
            }
            return baseResultDataValue;
        }
        public void AddHRDeptList(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();
            IList<HRDept> bsList = IBHRDept.LoadAll();
            IList<HRDept> csList = dao.GetHRDeptList("");
            HRDept sys_admin = bsList.Where(p => p.IsUse == true && p.DeveCode == "sys_admin").ElementAt(0);
            foreach (HRDept model in csList)
            {
                model.LabID = cenOrg.LabID;
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "导入CS的部门编码为:" + model.StandCode + ",部门名称为:" + model.CName + "失败!";
                    break;
                }

                var tempList = bsList.Where(p => p.StandCode == model.StandCode);
                if (tempList == null || tempList.Count() <= 0)
                {
                    model.ParentID = sys_admin.Id;
                    model.DataAddTime = DateTime.Now;
                    model.DataUpdateTime = DateTime.Now;
                    model.UseCode = cenOrg.OrgNo.ToString();
                    model.OrgCode = cenOrg.OrgNo.ToString();
                    model.IsUse = true;
                    IBHRDept.Entity = model;
                    baseResultDataValue.success = IBHRDept.Add();
                }
                else if (tempList != null && tempList.Count() > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的部门编码为:" + model.Id + ",名称为:" + model.CName + "已存在!");
                    continue;
                }
            }
        }
        public void AddHREmployeeAndRBACUserList(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();

            IList<RBACUser> bsUserList = IBRBACUser.LoadAll();
            IList<HREmployee> bsEmployeeList = IBHREmployee.LoadAll();

            IList<RBACUser> csList = dao.GetRBACUserAndHREmployeeList("");
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            HREmployee sys_admin = bsEmployeeList.Where(p => p.IsUse == true && p.DeveCode == "sys_admin").ElementAt(0);
            HRDept sysHRDept = IBHRDept.SearchListByHQL("hrdept.DeveCode='sys_admin'").ElementAt(0);

            foreach (RBACUser model in csList)
            {
                model.LabID = cenOrg.LabID;
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "导入CS的人员编码为:" + model.HREmployee.UseCode + ",名称为:" + model.CName + "失败!";
                    break;
                }

                var tempList = bsEmployeeList.Where(p => p.UseCode == model.HREmployee.UseCode);
                if (tempList == null || tempList.Count() <= 0)
                {
                    //人员信息
                    HREmployee employee = model.HREmployee;
                    employee.IsUse = true;
                    employee.IsEnabled = 1;

                    if (employee.HRDept != null)
                        employee.HRDept = IBHRDept.Get(employee.HRDept.Id);
                    else
                        employee.HRDept = sysHRDept;

                    employee.DataAddTime = DateTime.Now;
                    employee.DataUpdateTime = DateTime.Now;
                    employee.LabID = cenOrg.LabID;
                    if (employee.CName.Length > 1)
                    {
                        employee.NameL = employee.CName.Substring(0, 1);
                        employee.NameF = employee.CName.Substring(1);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Error("CS人员编号为:" + model.HREmployee.UseCode + ",姓名信息为:"+ employee.CName);
                        ZhiFang.Common.Log.Log.Error("CS人员编号为:" + model.HREmployee.UseCode + ",姓名信息为空，默认调整为未知!");
                        employee.NameL = "未";
                        employee.NameF = "知";
                        employee.CName = "未知";
                    }

                    IBHREmployee.Entity = employee;
                    baseResultDataValue.success = IBHREmployee.Add();
                    if (baseResultDataValue.success == false)
                    {
                        baseResultDataValue.ErrorInfo = "导入CS的人员编码为:" + model.HREmployee.UseCode + ",名称为:" + model.CName + "失败!";
                        break;
                    }

                    //人员所属帐号信息
                    model.LabID = cenOrg.LabID;
                    model.HREmployee = employee;
                    if (model.HREmployee.DataTimeStamp == null)
                        model.HREmployee.DataTimeStamp = dataTimeStamp;
                    model.DataAddTime = DateTime.Now;
                    model.DataUpdateTime = DateTime.Now;
                    model.IsUse = true;
                    model.AccLock = false;
                    model.AccBeginTime = DateTime.Now;
                    model.PWD = Common.Public.SecurityHelp.MD5Encrypt("123456", Common.Public.SecurityHelp.PWDMD5Key);
                    IBRBACUser.Entity = model;
                    baseResultDataValue.success = IBRBACUser.Add();
                }
                else if (tempList != null && tempList.Count() > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的人员编码为:" + model.Id + ",名称为:" + model.CName + "已存在!");
                    continue;
                }
            }
        }
        public void AddRBACRoleList(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();
            IList<RBACRole> bsList = IBRBACRole.LoadAll();
            IList<RBACRole> csList = dao.GetRBACRoleList("");
            foreach (RBACRole model in csList)
            {
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "导入CS的角色编码为:" + model.UseCode + ",名称为:" + model.CName + "失败!";
                    break;
                }

                var tempList = bsList.Where(p => p.UseCode == model.UseCode);
                if (tempList == null || tempList.Count() <= 0)
                {
                    model.LabID = cenOrg.LabID;
                    model.DataAddTime = DateTime.Now;
                    model.DataUpdateTime = DateTime.Now;
                    model.IsUse = true;
                    IBRBACRole.Entity = model;
                    baseResultDataValue.success = IBRBACRole.Add();
                }
                else if (tempList != null && tempList.Count() > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的角色编码为:" + model.Id + ",名称为:" + model.CName + "已存在!");
                    continue;
                }
            }
        }
        public void AddReaCenOrgList(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();
            IList<ReaCenOrg> bsList = IBReaCenOrg.LoadAll();
            IList<ReaCenOrg> csList = dao.GetReaCenOrgList("");

            int orgNo = IDReaCenOrgDao.GetMaxOrgNo();

            foreach (ReaCenOrg model in csList)
            {
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "导入CS的供应商编码为:" + model.Id + ",名称为:" + model.CName + "失败!";
                    break;
                }

                var tempList = bsList.Where(p => p.Id == model.Id);
                if (tempList == null || tempList.Count() <= 0)
                {
                    model.LabID = cenOrg.LabID;
                    model.DataAddTime = DateTime.Now;
                    model.DataUpdateTime = DateTime.Now;
                    model.Visible = 1;
                    model.OrgType = int.Parse(ReaCenOrgType.供货方.Key);
                    model.OrgNo = orgNo;
                    model.PlatformOrgNo = orgNo;
                    model.POrgID = 0;
                    IBReaCenOrg.Entity = model;
                    baseResultDataValue.success = IBReaCenOrg.Add();
                    orgNo = orgNo + 1;
                }
                else if (tempList != null && tempList.Count() > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的供应商编码为:" + model.Id + ",名称为:" + model.CName + "已存在!");
                    continue;
                }
            }
        }
        public void AddReaStorageList(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();
            IList<ReaStorage> bsList = IBReaStorage.LoadAll();
            IList<ReaStorage> csList = dao.GetReaStorageList("");

            foreach (ReaStorage model in csList)
            {
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "导入CS的库房编码为:" + model.Id + ",名称为:" + model.CName + "失败!";
                    break;
                }

                var tempList = bsList.Where(p => p.Id == model.Id);
                if (tempList == null || tempList.Count() <= 0)
                {
                    model.LabID = cenOrg.LabID;
                    model.DataAddTime = DateTime.Now;
                    model.DataUpdateTime = DateTime.Now;
                    model.Visible = true;
                    IBReaStorage.Entity = model;
                    baseResultDataValue.success = IBReaStorage.Add();
                }
                else if (tempList != null && tempList.Count() > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的库房编码为:" + model.Id + ",名称为:" + model.CName + "已存在!");
                    continue;
                }
            }
        }
        public void AddReaPlaceList(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();
            IList<ReaPlace> bsList = IBReaPlace.LoadAll();
            IList<ReaPlace> csList = dao.GetReaPlaceList("");

            foreach (ReaPlace model in csList)
            {
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "导入CS的货架编码为:" + model.Id + ",名称为:" + model.CName + "失败!";
                    break;
                }

                var tempList = bsList.Where(p => p.Id == model.Id);
                if (tempList == null || tempList.Count() <= 0)
                {
                    model.LabID = cenOrg.LabID;
                    model.DataAddTime = DateTime.Now;
                    model.DataUpdateTime = DateTime.Now;
                    model.Visible = true;
                    if (model.ReaStorage != null)
                        model.ReaStorage = IBReaStorage.Get(model.ReaStorage.Id);
                    IBReaPlace.Entity = model;
                    baseResultDataValue.success = IBReaPlace.Add();
                }
                else if (tempList != null && tempList.Count() > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的货架编码为:" + model.Id + ",名称为:" + model.CName + "已存在!");
                    continue;
                }
            }
        }
        public void AddReaTestEquipLabList(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();
            IList<ReaTestEquipLab> bsList = IBReaTestEquipLab.LoadAll();
            IList<ReaTestEquipLab> csList = dao.GetReaTestEquipLabList("");

            foreach (ReaTestEquipLab model in csList)
            {
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "导入CS的仪器编码为:" + model.Id + ",名称为:" + model.CName + "失败!";
                    break;
                }

                var tempList = bsList.Where(p => p.Id == model.Id);
                if (tempList == null || tempList.Count() <= 0)
                {
                    model.LabID = cenOrg.LabID;
                    model.DataAddTime = DateTime.Now;
                    model.DataUpdateTime = DateTime.Now;
                    model.Visible = 1;
                    if (model.DeptID.HasValue && string.IsNullOrEmpty(model.DeptName))
                    {
                        HRDept dept = IBHRDept.Get(model.DeptID.Value);
                        if (dept != null)
                            model.DeptName = dept.CName;
                    }
                    IBReaTestEquipLab.Entity = model;
                    baseResultDataValue.success = IBReaTestEquipLab.Add();
                }
                else if (tempList != null && tempList.Count() > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的仪器编码为:" + model.Id + ",名称为:" + model.CName + "已存在!");
                    continue;
                }
            }
        }
        public void AddReaGoodsList(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();
            IList<ReaGoods> bsList = IBReaGoods.LoadAll();
            IList<ReaGoods> csList = dao.GetReaGoodsList("");
            int goodsSort = IDReaGoodsDao.GetMaxGoodsSort();
            foreach (ReaGoods model in csList)
            {
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "导入CS的货品编码为:" + model.Id + ",名称为:" + model.CName + "失败!";
                    break;
                }
                model.LabID = cenOrg.LabID;
                var tempList = bsList.Where(p => p.Id == model.Id);
                if (tempList == null || tempList.Count() <= 0)
                {
                    model.LabID = cenOrg.LabID;
                    model.DataAddTime = DateTime.Now;
                    model.DataUpdateTime = DateTime.Now;
                    model.Visible = 1;
                    model.IsMinUnit = true;
                    model.GoodsSort = goodsSort;
                    if (string.IsNullOrEmpty(model.ReaGoodsNo))
                        model.ReaGoodsNo = model.Id.ToString();

                    model.GonvertQty = 1;
                    model.IsPrintBarCode = 1;
                    IBReaGoods.Entity = model;
                    baseResultDataValue.success = IBReaGoods.Add();
                    goodsSort = goodsSort + 1;
                }
                else if (tempList != null && tempList.Count() > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的货品编码为:" + model.Id + ",名称为:" + model.CName + "已存在!");
                    continue;
                }
            }
        }
        public void AddReaEquipReagentLinkList(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();
            IList<ReaEquipReagentLink> bsList = IBReaEquipReagentLink.LoadAll();
            IList<ReaEquipReagentLink> csList = dao.GetReaEquipReagentLinkList("");
            IList<ReaGoods> bsReaGoodsList = IBReaGoods.LoadAll();
            //仪器ID+货品ID合并(过滤CS重复的仪器货品关系)
            IList<string> equipIDGoodsIDList = new List<string>();

            foreach (ReaEquipReagentLink model in csList)
            {
                //先将model.GoodsID(从CS获取时取CS的GoodsUnitID)转换为BS的ReaGoods的ID
                var tempReaGoodsList = bsReaGoodsList.Where(p => p.GoodsUnitID == model.GoodsID);
                if (tempReaGoodsList != null && tempReaGoodsList.Count() > 0)
                {
                    model.GoodsID = tempReaGoodsList.ElementAt(0).Id;
                }
                model.LabID = cenOrg.LabID;
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "导入CS的仪器编码为:" + model.TestEquipID + ",货品编码为:" + model.GoodsID + " 失败!";
                    break;
                }
                string idStr = model.TestEquipID + "," + model.GoodsID;
                if (!equipIDGoodsIDList.Contains(idStr))
                {
                    var tempList = bsList.Where(p => p.LabID == model.LabID && p.TestEquipID == model.TestEquipID && p.GoodsID == model.GoodsID);
                    if (tempList == null || tempList.Count() <= 0)
                    {
                        model.DataAddTime = DateTime.Now;
                        model.DataUpdateTime = DateTime.Now;
                        model.Visible = 1;
                        IBReaEquipReagentLink.Entity = model;
                        baseResultDataValue.success = IBReaEquipReagentLink.Add();
                    }
                    else if (tempList != null && tempList.Count() > 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("导入CS的仪器编码为: " + model.TestEquipID + ", 货品编码为: " + model.GoodsID + ", 已存在!");
                        continue;
                    }
                    equipIDGoodsIDList.Add(idStr);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的仪器编码为:" + model.TestEquipID + ",货品编码为:" + model.GoodsID + ",已存在!");
                }
            }
        }
        public void AddReaGoodsOrgLinkList(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();
            IList<ReaGoodsOrgLink> bsList = IBReaGoodsOrgLink.LoadAll();
            IList<ReaGoodsOrgLink> csList = dao.GetReaGoodsOrgLinkList("");
            IList<ReaGoods> bsReaGoodsList = IBReaGoods.LoadAll();
            foreach (ReaGoodsOrgLink model in csList)
            {
                ReaGoods reaGoods = null;
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "导入CS的供应商货品ID为:" + model.Id + ", 失败!";
                    break;
                }

                if (model.ReaGoods == null)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的供应商货品ID为:" + model.Id + ",货品编码为空!");
                    continue;
                }
                if (model.CenOrg == null)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的供应商货品ID为:" + model.Id + ",供应商编码为空!");
                    continue;
                }
                //先将model.GoodsID(从CS获取时取CS的GoodsUnitID)转换为BS的ReaGoods的ID
                var tempReaGoodsList = bsReaGoodsList.Where(p => p.GoodsUnitID == model.ReaGoods.Id);
                if (tempReaGoodsList != null && tempReaGoodsList.Count() > 0)
                {
                    reaGoods = tempReaGoodsList.ElementAt(0);
                    model.ReaGoods.Id = reaGoods.Id;
                }
                model.LabID = cenOrg.LabID;
                var tempList = bsList.Where(p => p.LabID == model.LabID && p.CenOrg.Id == model.CenOrg.Id && p.ReaGoods.Id == model.ReaGoods.Id);
                if (tempList == null || tempList.Count() <= 0)
                {
                    model.DataAddTime = DateTime.Now;
                    model.DataUpdateTime = DateTime.Now;
                    model.Visible = 1;
                    model.BeginTime = DateTime.Now;
                    if (reaGoods == null) reaGoods = IBReaGoods.Get(model.ReaGoods.Id);
                    model.ReaGoods = reaGoods;// IBReaGoods.Get(model.ReaGoods.Id);
                    model.CenOrg = IBReaCenOrg.Get(model.CenOrg.Id);
                    if (model.ReaGoods != null && model.CenOrg != null)
                    {
                        if (string.IsNullOrEmpty(model.CenOrgGoodsNo))
                        {
                            model.CenOrgGoodsNo = model.ReaGoods.ReaGoodsNo;
                        }
                        model.BarCodeType = model.ReaGoods.BarCodeMgr;
                        model.IsPrintBarCode = model.ReaGoods.IsPrintBarCode;
                        model.DataUpdateTime = DateTime.Now;
                        IBReaGoodsOrgLink.Entity = model;
                        baseResultDataValue.success = IBReaGoodsOrgLink.Add();
                    }
                    else
                    {
                        if (model.ReaGoods == null)
                        {
                            ZhiFang.Common.Log.Log.Debug("导入CS的供应商货品ID为:" + model.Id + ",货品编码为:" + model.ReaGoods.Id + ",不存在机构货品信息里!");
                        }
                        if (model.CenOrg == null)
                        {
                            ZhiFang.Common.Log.Log.Debug("导入CS的供应商货品ID为:" + model.Id + ",供应商编码为:" + model.CenOrg.Id + ",不存在供应商信息里!");
                        }
                    }
                }
                else if (tempList != null && tempList.Count() > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的供应商货品ID为:" + model.Id + "供应商编码为:" + model.CenOrg.Id + ",货品编码为:" + model.ReaGoods.Id + ", 已存在!");
                    continue;
                }
            }
        }
        public void AddReaDeptGoodsList(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();
            IList<ReaDeptGoods> bsList = IBReaDeptGoods.LoadAll();
            IList<ReaDeptGoods> csList = dao.GetReaDeptGoodsList("");
            IList<ReaGoods> bsReaGoodsList = IBReaGoods.LoadAll();
            int dispOrder = 0;
            foreach (ReaDeptGoods model in csList)
            {
                ReaGoods reaGoods = null;
                dispOrder = dispOrder + 1;
                if (baseResultDataValue.success == false)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的部门ID为:" + model.DeptID + ",货品编码为:" + model.ReaGoods.Id + ", 失败!");
                    break;
                }

                if (model.ReaGoods == null)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的机构货品ID为空!");
                    continue;
                }
                if (model.DeptID == null)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的部门ID为空!");
                    continue;
                }
                //先将model.GoodsID(从CS获取时取CS的GoodsUnitID)转换为BS的ReaGoods的ID
                var tempReaGoodsList = bsReaGoodsList.Where(p => p.GoodsUnitID == model.ReaGoods.Id);
                if (tempReaGoodsList != null && tempReaGoodsList.Count() > 0)
                {
                    reaGoods = tempReaGoodsList.ElementAt(0);
                    model.ReaGoods.Id = reaGoods.Id;
                }
                model.LabID = cenOrg.LabID;
                var tempList = bsList.Where(p => p.LabID == model.LabID && p.DeptID == model.DeptID && p.ReaGoods.Id == model.ReaGoods.Id);
                if (tempList == null || tempList.Count() <= 0)
                {
                    model.DataAddTime = DateTime.Now;
                    if (reaGoods == null) reaGoods = IBReaGoods.Get(model.ReaGoods.Id);
                    model.ReaGoods = reaGoods;// 
                    model.DeptID = model.DeptID; //IBHRDept.Get(model.DeptID.Value);
                    model.DeptName = model.DeptName;
                    //model.GoodsCName = model.ReaGoods.CName;
                    model.DispOrder = dispOrder;
                    if (model.ReaGoods != null && model.DeptID != null)
                    {
                        IBReaDeptGoods.Entity = model;
                        baseResultDataValue.success = IBReaDeptGoods.Add();
                    }
                    else
                    {
                        if (model.ReaGoods == null)
                        {
                            ZhiFang.Common.Log.Log.Debug("导入CS的机构货品ID为:" + model.ReaGoods.Id + ",不存在机构货品信息里!");
                        }
                        if (model.DeptID == null)
                        {
                            ZhiFang.Common.Log.Log.Debug("导入CS的部门ID为:" + model.DeptID + ",不存在部门信息里!");
                        }
                    }
                }
                else if (tempList != null && tempList.Count() > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的部门ID为:" + model.DeptID + ",货品编码为:" + model.ReaGoods.Id + ", 已存在!");
                    continue;
                }
            }
        }
        #region 库存货品导入处理
        public void DeleteOldQtyDtlInfo(CenOrg cenOrg, long empID, string empName, ref BaseResultDataValue baseResultDataValue)
        {
            ZhiFang.Common.Log.Log.Debug("执行删除当前机构的原入库及库存信息(包括库存变化操作记录及盒条码操作记录信息)!");
            IBReaGoodsBarcodeOperation.DeleteByHql("From ReaGoodsBarcodeOperation reagoodsbarcodeoperation where reagoodsbarcodeoperation.LabID=" + cenOrg.LabID);
            IBReaBmsQtyDtlOperation.DeleteByHql("From ReaBmsQtyDtlOperation reabmsqtydtloperation where reabmsqtydtloperation.LabID =" + cenOrg.LabID);
            IBReaBmsQtyDtl.DeleteByHql("From ReaBmsQtyDtl reabmsqtydtl where reabmsqtydtl.LabID =" + cenOrg.LabID);
            IBReaBmsInDtl.DeleteByHql("From ReaBmsInDtl reabmsindtl where reabmsindtl.LabID =" + cenOrg.LabID);
            //IBReaGoodsLot.DeleteByHql("From ReaGoodsLot reagoodslot where reagoodslot.LabID =" + cenOrg.LabID);
            IBReaBmsInDoc.DeleteByHql("From ReaBmsInDoc reabmsindoc where reabmsindoc.LabID =" + cenOrg.LabID);
            IBReaBmsSerial.DeleteByHql("From ReaBmsSerial reabmsserial where reabmsserial.BmsType='ReaBmsQtyDtl' and reabmsserial.LabID =" + cenOrg.LabID);
            ZhiFang.Common.Log.Log.Debug("执行删除当前机构的原入库及库存信息完成!");
        }

        public void AddReaBmsQtyDtlList(CenOrg cenOrg, long empID, string empName, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();
            IList<ReaBmsQtyDtl> csQtyDtlList = dao.GetReaBmsQtyDtlList("");
            ZhiFang.Common.Log.Log.Debug("获取CS当前库存货品记录数为:" + csQtyDtlList.Count);
            IList<ReaGoods> bsReaGoodsList = IBReaGoods.LoadAll();
            //先将model.GoodsID(从CS获取时取CS的GoodsUnitID)转换为BS的ReaGoods的ID
            for (int i = 0; i < csQtyDtlList.Count; i++)
            {
                var tempReaGoodsList = bsReaGoodsList.Where(p => p.GoodsUnitID == csQtyDtlList[i].GoodsUnitID);
                if (tempReaGoodsList != null && tempReaGoodsList.Count() > 0)
                {
                    csQtyDtlList[i].GoodsID = tempReaGoodsList.ElementAt(0).Id;
                }
            }
            var groupByQtyDtlList = csQtyDtlList.GroupBy(p => new
            {
                p.ReaCompanyID,
                p.StorageID,
                p.PlaceID,
                p.GoodsID,
                p.BarCodeType,
                p.GoodsUnit,
                p.UnitMemo,
                p.LotNo,
                p.InvalidDate
            });
            ZhiFang.Common.Log.Log.Debug("将CS当前库存货品信息按(供应商ID+条码类型+货品ID+包装单位+货品批号+库房ID+货架ID)合并记录数为:" + groupByQtyDtlList.Count());
            IList<ReaGoods> reaGoodsList = IBReaGoods.LoadAll();
            IList<ReaGoodsOrgLink> reaGoodsOrgLinkList = IBReaGoodsOrgLink.LoadAll();
            Dictionary<ReaBmsInDtl, ReaBmsQtyDtl> inDtlList = new Dictionary<ReaBmsInDtl, ReaBmsQtyDtl>();
            Dictionary<ReaBmsQtyDtl, IList<ReaGoodsBarcodeOperation>> qtyDtlList = new Dictionary<ReaBmsQtyDtl, IList<ReaGoodsBarcodeOperation>>();
            ZhiFang.Common.Log.Log.Debug("封装导入的入库及库存信息开始!");
            //获取系统运行参数的货品效期预警天数值
            double? beforeWarningDay = null;
            BParameter param = IBBParameter.GetParameterByParaNo(SYSParaNo.货品效期预警天数.Key);
            if (param != null && !string.IsNullOrEmpty(param.ParaValue))
                beforeWarningDay = double.Parse(param.ParaValue);

            ReaBmsInDoc inDoc = AddGetReaBmsInDoc(cenOrg, empID, empName);
            foreach (var groupByQtyDtl in groupByQtyDtlList)
            {
                var bsQtyDtl = groupByQtyDtl.ElementAt(0);
                bsQtyDtl.LabID = cenOrg.LabID;
                ReaGoodsOrgLink reaGoodsOrgLink = null;
                var tempGoodsOrgLinkList = reaGoodsOrgLinkList.Where(p => p.CenOrg.Id == bsQtyDtl.ReaCompanyID.Value && p.ReaGoods.Id == bsQtyDtl.GoodsID.Value);
                if (tempGoodsOrgLinkList != null && tempGoodsOrgLinkList.Count() > 0)
                {
                    reaGoodsOrgLink = tempGoodsOrgLinkList.ElementAt(0);
                    bsQtyDtl.CompGoodsLinkID = reaGoodsOrgLink.Id;
                    if (string.IsNullOrEmpty(bsQtyDtl.CenOrgGoodsNo))
                        bsQtyDtl.CenOrgGoodsNo = reaGoodsOrgLink.CenOrgGoodsNo;
                    bsQtyDtl.ReaCompCode = reaGoodsOrgLink.CenOrg.OrgNo.Value.ToString();
                    if (reaGoodsOrgLink.CenOrg.PlatformOrgNo.HasValue)
                        bsQtyDtl.ReaServerCompCode = reaGoodsOrgLink.CenOrg.PlatformOrgNo.Value.ToString();
                }
                ReaGoods reaGoods = null;
                var tempGoodsList = reaGoodsList.Where(p => p.Id == bsQtyDtl.GoodsID.Value);// IBReaGoods.Get(bsQtyDtl.GoodsID.Value);
                if (tempGoodsList != null && tempGoodsList.Count() > 0)
                {
                    reaGoods = tempGoodsList.ElementAt(0);
                    bsQtyDtl.GoodsSort = reaGoods.GoodsSort;
                    bsQtyDtl.ReaGoodsNo = reaGoods.ReaGoodsNo;
                }
                ReaBmsInDtl inDtl = AddGetReaBmsInDtl(cenOrg, inDoc, bsQtyDtl, reaGoods, empID, empName);
                bsQtyDtl.InDtlID = inDtl.Id;
                bsQtyDtl.InDocNo = inDtl.InDocNo;
                bsQtyDtl.GoodsQty = groupByQtyDtl.Sum(p => p.GoodsQty);
                bsQtyDtl.SumTotal = groupByQtyDtl.Sum(p => p.SumTotal);
                //平均单价
                if (bsQtyDtl.GoodsQty.HasValue && bsQtyDtl.GoodsQty.Value > 0)
                    bsQtyDtl.Price = bsQtyDtl.SumTotal / bsQtyDtl.GoodsQty;
                bsQtyDtl.PQtyDtlID = bsQtyDtl.Id;
                bsQtyDtl.Visible = true;
                bsQtyDtl.CreaterID = empID;
                bsQtyDtl.CreaterName = empName;
                bsQtyDtl.DataAddTime = DateTime.Now;
                bsQtyDtl.DataUpdateTime = DateTime.Now;
                double? beforeWarningDay2 = beforeWarningDay;
                if (!reaGoods.BeforeWarningDay.HasValue)
                    beforeWarningDay2 = reaGoods.BeforeWarningDay;
                bsQtyDtl.InvalidWarningDate = EditCalcInvalidWarningDate(beforeWarningDay2, bsQtyDtl.InvalidDate);
                bsQtyDtl.Memo = "CS导入;" + bsQtyDtl.Memo;
                IList<ReaGoodsBarcodeOperation> barcodeList = new List<ReaGoodsBarcodeOperation>();
                //CS原盒条码处理
                if (bsQtyDtl.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key))
                {
                    int dispOrder = 0;
                    foreach (ReaBmsQtyDtl csQtyDtl in groupByQtyDtl)
                    {
                        AddGetBarcodeList(cenOrg, inDtl, bsQtyDtl, csQtyDtl, reaGoods, empID, empName, dispOrder, ref barcodeList);
                        dispOrder = dispOrder + 1;
                    }
                }
                else if (bsQtyDtl.BarCodeType == int.Parse(ReaGoodsBarCodeType.批条码.Key))
                {
                    bsQtyDtl.LotSerial = bsQtyDtl.GoodsSerial;
                }
                //重置GoodsSerial
                inDtl.GoodsSerial = "";
                bsQtyDtl.GoodsSerial = "";
                qtyDtlList.Add(bsQtyDtl, barcodeList);
                inDtlList.Add(inDtl, bsQtyDtl);
            }
            ZhiFang.Common.Log.Log.Debug("封装导入的入库及库存信息结束!");
            //新增入库,入库明细,库存货品,库存货品操作记录,库存货品盒条码
            AddInDocAndQtyDtlList(cenOrg, inDoc, inDtlList, qtyDtlList, empID, empName, ref baseResultDataValue);
        }
        public DateTime? EditCalcInvalidWarningDate(double? beforeWarningDay, DateTime? invalidDate)
        {
            DateTime? invalidWarningDate = null;
            if (!beforeWarningDay.HasValue)
            {
                beforeWarningDay = 10;
            }
            if (!invalidDate.HasValue || !beforeWarningDay.HasValue)
                return DateTime.Now.AddDays(-10);

            if (invalidDate.HasValue && beforeWarningDay.HasValue)
                invalidWarningDate = invalidDate.Value.AddDays(-beforeWarningDay.Value);
            return invalidWarningDate;
        }
        public void AddReaBmsSerialList(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            IDAO.NHB.ReagentSys.Client.IDCSUpdateToBSDao_SQL dao = IDAO.NHB.ReagentSys.Client.DataAccess_SQL.CreateCSUpdateToBSDao_SQL();
            //IBReaBmsSerial.DeleteByHql("From ReaBmsSerial reabmsserial where reabmsserial.BmsType='ReaBmsQtyDtl' and reabmsserial.LabID =" + cenOrg.LabID);
            IList<ReaBmsSerial> bsList = IBReaBmsSerial.LoadAll();
            IList<ReaBmsSerial> csList = dao.GetReaBmsSerialList("");
            int dispOrder = 0;
            foreach (ReaBmsSerial model in csList)
            {
                model.LabID = cenOrg.LabID;
                model.DataAddTime = DateTime.Now;
                dispOrder = dispOrder + 1;
                if (baseResultDataValue.success == false)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的条码类型为:" + model.BmsType + ", 失败!");
                    break;
                }
                //库存条码类型
                if (model.BmsType == "BmsInDtl")
                {
                    model.BmsType = "ReaBmsQtyDtl";
                }
                var tempList = bsList.Where(p => p.LabID == model.LabID && p.BmsType == model.BmsType);
                if (tempList == null || tempList.Count() <= 0)
                {
                    IBReaBmsSerial.Entity = model;
                    baseResultDataValue.success = IBReaBmsSerial.Add();
                }
                else if (tempList != null && tempList.Count() > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("导入CS的条码类型为:" + model.BmsType + ", 已存在!");
                    continue;
                }
            }
        }
        private void AddInDocAndQtyDtlList(CenOrg cenOrg, ReaBmsInDoc inDoc, Dictionary<ReaBmsInDtl, ReaBmsQtyDtl> inDtlList, Dictionary<ReaBmsQtyDtl, IList<ReaGoodsBarcodeOperation>> qtyDtlList, long empID, string empName, ref BaseResultDataValue baseResultDataValue)
        {
            ZhiFang.Common.Log.Log.Debug("新增导入的入库及库存信息开始!");
            inDoc.TotalPrice = inDtlList.Keys.Sum(p => p.SumTotal);
            IBReaBmsInDoc.Entity = inDoc;
            baseResultDataValue.success = IBReaBmsInDoc.Add();
            if (baseResultDataValue.success == false)
            {
                baseResultDataValue.ErrorInfo = "导入CS库存信息保存转换入库失败!";
                return;
            }

            foreach (var inDtl in inDtlList)
            {
                //入库明细保存
                IBReaBmsInDtl.Entity = inDtl.Key;
                ReaGoodsLot reaGoodsLot = null;
                IBReaBmsInDtl.AddReaGoodsLot(ref reaGoodsLot, empID, empName);
                if (reaGoodsLot != null)
                {
                    inDtl.Key.GoodsLotID = reaGoodsLot.Id;
                    IBReaBmsInDtl.Entity.GoodsLotID = reaGoodsLot.Id;
                }
                baseResultDataValue.success = IBReaBmsInDtl.Add();
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "导入CS库存信息保存转换入库明细信息失败!";
                    break;
                }
                //库存货品信息保存
                IBReaBmsQtyDtl.Entity = inDtl.Value;
                if (reaGoodsLot != null)
                {
                    inDtl.Key.GoodsLotID = reaGoodsLot.Id;
                    IBReaBmsQtyDtl.Entity.GoodsLotID = reaGoodsLot.Id;
                }
                baseResultDataValue.success = IBReaBmsQtyDtl.Add();
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "导入CS库存信息保存转换的库存货品信息失败!";
                    break;
                }
                //库存货品变化操作记录
                BaseResultBool baseResultBool = IBReaBmsQtyDtl.AddReaBmsQtyDtlOperation(inDtl.Key, inDtl.Value, long.Parse(ReaBmsQtyDtlOperationOperType.库存初始化.Key), empID, empName);
                if (baseResultBool.success == false)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "导入CS库存信息保存转换的库存操作记录信息失败!";
                    break;
                }

                //CS原盒条码处理
                if (inDtl.Value.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key) && qtyDtlList[inDtl.Value].Count > 0)
                {
                    IList<ReaGoodsBarcodeOperation> dtAddList = qtyDtlList[inDtl.Value];
                    if (reaGoodsLot != null)
                    {
                        for (int i = 0; i < dtAddList.Count; i++)
                        {
                            dtAddList[i].GoodsLotID = reaGoodsLot.Id;
                        }
                    }
                    baseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperationOfList(dtAddList, 0, empID, empName, cenOrg.LabID);
                    if (baseResultBool.success == false)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "导入CS库存信息保存转换的盒条码信息失败!";
                        break;
                    }
                }
            }
            ZhiFang.Common.Log.Log.Debug("新增导入的入库及库存信息结束!");
        }
        /// <summary>
        /// 入库总单号
        /// </summary>
        /// <returns></returns>
        private string GetInDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        private ReaBmsInDoc AddGetReaBmsInDoc(CenOrg cenOrg, long empID, string empName)
        {
            ReaBmsInDoc inDoc = new ReaBmsInDoc();
            inDoc.LabID = cenOrg.LabID;
            inDoc.Visible = true;
            inDoc.InDocNo = this.GetInDocNo();
            inDoc.CreaterID = empID;
            inDoc.CreaterName = empName;
            inDoc.OperDate = DateTime.Now;
            inDoc.DataUpdateTime = DateTime.Now;
            inDoc.UserID = empID;
            inDoc.UserName = empName;

            inDoc.Status = int.Parse(ReaBmsInDocStatus.已入库.Key);
            inDoc.StatusName = ReaBmsInDocStatus.GetStatusDic()[inDoc.Status.ToString()].Name;
            inDoc.SourceType = int.Parse(ReaBmsInSourceType.库存初始化.Key);
            inDoc.InType = int.Parse(ReaBmsInDocInType.库存初始化.Key);
            if (inDoc.InType.HasValue)
                inDoc.InTypeName = ReaBmsInDocInType.GetStatusDic()[inDoc.InType.Value.ToString()].Name;
            return inDoc;
        }
        private ReaBmsInDtl AddGetReaBmsInDtl(CenOrg cenOrg, ReaBmsInDoc inDoc, ReaBmsQtyDtl bsQtyDtl, ReaGoods reaGoods, long empID, string empName)
        {
            ReaBmsInDtl model = new ReaBmsInDtl();
            model.LabID = cenOrg.LabID;
            model.ReaGoods = reaGoods;
            model.InDocNo = inDoc.InDocNo;
            model.Visible = true;
            model.InDtlNo = GetInDocNo();
            model.CreaterID = empID;
            model.CreaterName = empName;

            model.DataAddTime = DateTime.Now;
            model.DataUpdateTime = DateTime.Now;
            model.InDocID = inDoc.Id;
            model.GoodsQty = bsQtyDtl.GoodsQty;
            model.SumTotal = bsQtyDtl.SumTotal;
            //平均单价
            if (bsQtyDtl.GoodsQty.HasValue && bsQtyDtl.GoodsQty.Value > 0)
                model.Price = bsQtyDtl.SumTotal / bsQtyDtl.GoodsQty;

            model.GoodsUnit = bsQtyDtl.GoodsUnit;
            model.UnitMemo = bsQtyDtl.UnitMemo;
            model.ReaCompanyID = bsQtyDtl.ReaCompanyID;
            model.CompanyName = bsQtyDtl.CompanyName;
            model.GoodsID = bsQtyDtl.GoodsID;
            model.GoodsCName = bsQtyDtl.GoodsName;
            model.LotSerial = bsQtyDtl.LotSerial;

            model.TaxRate = bsQtyDtl.TaxRate;
            model.LotNo = bsQtyDtl.LotNo;
            model.StorageID = bsQtyDtl.StorageID;
            model.PlaceID = bsQtyDtl.PlaceID;
            model.StorageName = bsQtyDtl.StorageName;

            model.PlaceName = bsQtyDtl.PlaceName;
            model.ZX1 = bsQtyDtl.ZX1;
            model.ZX2 = bsQtyDtl.ZX2;
            model.ZX3 = bsQtyDtl.ZX3;
            model.StorageName = bsQtyDtl.StorageName;

            model.GoodsNo = bsQtyDtl.GoodsNo;
            model.ProdDate = bsQtyDtl.ProdDate;
            model.InvalidDate = bsQtyDtl.InvalidDate;
            model.ProdGoodsNo = bsQtyDtl.ProdGoodsNo;
            model.ReaGoodsNo = model.ReaGoods.ReaGoodsNo;

            model.CenOrgGoodsNo = bsQtyDtl.CenOrgGoodsNo;
            model.GoodsSort = bsQtyDtl.GoodsSort;
            model.BarCodeType = bsQtyDtl.BarCodeType;
            model.CompGoodsLinkID = bsQtyDtl.CompGoodsLinkID;
            model.ReaCompCode = bsQtyDtl.ReaCompCode;
            model.ReaServerCompCode = bsQtyDtl.ReaServerCompCode;
            model.Memo = "CS导入;" + model.Memo;
            return model;
        }
        private void AddGetBarcodeList(CenOrg cenOrg, ReaBmsInDtl inDtl, ReaBmsQtyDtl bsQtyDtl, ReaBmsQtyDtl csQtyDtl, ReaGoods reaGoods, long empID, string empName, int dispOrder, ref IList<ReaGoodsBarcodeOperation> barcodeList)
        {
            string usePackSerial = csQtyDtl.GoodsSerial;
            //生成二维盒条码
            string usePackQRCode = usePackSerial;// AddGetSysPackSerial(qtyDtl, reaGoods);

            ReaGoodsBarcodeOperation barcode = new ReaGoodsBarcodeOperation();
            barcode.LabID = cenOrg.LabID;
            barcode.BDocID = inDtl.InDocID;
            barcode.BDocNo = inDtl.InDocNo;
            barcode.BDtlID = inDtl.Id;
            barcode.QtyDtlID = bsQtyDtl.Id;
            barcode.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key);
            barcode.OperTypeID = long.Parse(ReaGoodsBarcodeOperType.验货入库.Key);
            barcode.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[barcode.OperTypeID.ToString()].Name;

            barcode.DispOrder = dispOrder + 1;
            barcode.Visible = true;
            barcode.CreaterID = empID;
            barcode.CreaterName = empName;
            barcode.DataUpdateTime = DateTime.Now;

            barcode.GoodsID = bsQtyDtl.GoodsID;
            barcode.ScanCodeGoodsID = bsQtyDtl.GoodsID;
            barcode.GoodsCName = bsQtyDtl.GoodsName;
            barcode.GoodsUnit = bsQtyDtl.GoodsUnit;
            barcode.LotNo = bsQtyDtl.LotNo;

            barcode.ReaCompanyID = bsQtyDtl.ReaCompanyID;
            barcode.CompanyName = bsQtyDtl.CompanyName;
            barcode.SysPackSerial = usePackQRCode;
            barcode.UsePackQRCode = "";
            barcode.UsePackSerial = usePackSerial;
            barcode.OtherPackSerial = usePackSerial;

            barcode.GoodsQty = bsQtyDtl.GoodsQty;
            barcode.StorageID = bsQtyDtl.StorageID;
            barcode.PlaceID = bsQtyDtl.PlaceID;
            var gonvertQty = reaGoods.GonvertQty;
            if (gonvertQty <= 0)
            {
                ZhiFang.Common.Log.Log.Warn("货品编码为:" + barcode.ReaGoodsNo + ",货品名称为:" + barcode.GoodsCName + ",货品包装单位的换算系数值为" + gonvertQty + ",维护不合理!");
                gonvertQty = 1;
            }
            barcode.MinBarCodeQty = gonvertQty;
            if (barcode.MinBarCodeQty <= 0) barcode.MinBarCodeQty = 1;
            barcode.OverageQty = barcode.MinBarCodeQty;

            barcode.UnitMemo = bsQtyDtl.UnitMemo;
            barcode.PUsePackSerial = barcode.Id.ToString();
            barcode.ReaGoodsNo = reaGoods.ReaGoodsNo;
            barcode.ProdGoodsNo = bsQtyDtl.ProdGoodsNo;
            barcode.CenOrgGoodsNo = bsQtyDtl.CenOrgGoodsNo;
            barcode.GoodsNo = bsQtyDtl.GoodsNo;
            barcode.GoodsSort = bsQtyDtl.GoodsSort;

            barcode.BarCodeType = bsQtyDtl.BarCodeType;
            barcode.CompGoodsLinkID = bsQtyDtl.CompGoodsLinkID;
            barcode.ReaCompCode = bsQtyDtl.ReaCompCode;
            barcode.DataAddTime = DateTime.Now;
            barcode.DataUpdateTime = DateTime.Now;
            barcode.Memo = "CS导入;" + barcode.Memo;
            barcodeList.Add(barcode);
        }
        #endregion

        public BaseResultDataValue GetCSUpdateToBSInfo()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            try
            {

            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }
    }
}
