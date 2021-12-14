using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using System.Collections.Generic;
using System;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Linq;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using System.IO;
using System.Text;
using System.ServiceModel.Web;
using Newtonsoft.Json.Linq;
using System.Web;
using ZhiFang.ReagentSys.Client.Common;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BCenOrg : BaseBLL<CenOrg>, ZhiFang.IBLL.ReagentSys.Client.IBCenOrg
    {
        IBSServiceClient IBSServiceClient { get; set; }
        IBHRDept IBHRDept { get; set; }
        IBHREmployee IBHREmployee { get; set; }
        IBRBACUser IBRBACUser { get; set; }
        IBRBACRole IBRBACRole { get; set; }
        IBRBACEmpRoles IBRBACEmpRoles { get; set; }
        IBRBACRoleModule IBRBACRoleModule { get; set; }
        //IDRBACModuleDao IDRBACModuleDao { get; set; }
        IBRBACModule IBRBACModule { get; set; }
        IBBParameter IBBParameter { get; set; }
        IBSCAttachment IBSCAttachment { get; set; }
        IBSCOperation IBSCOperation { get; set; }
        IBBDictType IBBDictType { get; set; }
        IBReaCenBarCodeFormat IBReaCenBarCodeFormat { get; set; }
        IBReaBmsSerial IBReaBmsSerial { get; set; }
        IBReaStorage IBReaStorage { get; set; }

        #region 平台上的客户机构初始化注册
        public BaseResultDataValue AddCenOrgOfInitializeOfPlatform(long zfLabId, CenOrg cenOrg, RBACUser user, string roleIdStrOfZf, string moduleIdStr, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (cenOrg == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入参数(cenOrg)信息为空!";
                return tempBaseResultDataValue;
            }
            if (user == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入参数(user)信息为空!";
                return tempBaseResultDataValue;
            }
            if (string.IsNullOrEmpty(moduleIdStr.Trim()))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入参数(moduleIdStr)信息为空!";
                return tempBaseResultDataValue;
            }
            string[] tempArr = moduleIdStr.TrimEnd(',').Split(',');
            if (tempArr.Length <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入参数(moduleIdStr)信息为空!";
                return tempBaseResultDataValue;
            }
            //if (string.IsNullOrEmpty(user.Account))
            //{
            //    tempBaseResultDataValue.success = false;
            //    tempBaseResultDataValue.ErrorInfo = "传入参数(user.Account)信息为空!";
            //    return tempBaseResultDataValue;
            //}
            SServiceClient client = new SServiceClient();
            client.IsUse = true;
            client.LabID = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
            client.Name = cenOrg.CName;
            IBSServiceClient.Entity = client;
            tempBaseResultDataValue.success = IBSServiceClient.Add();
            if (tempBaseResultDataValue.success == false)
            {
                tempBaseResultDataValue.ErrorInfo = "初始化机构信息(SServiceClient)失败!";
                return tempBaseResultDataValue;
            }

            tempBaseResultDataValue = AddCenOrgOfPlatform(client, ref cenOrg);
            if (tempBaseResultDataValue.success == false)
            {
                tempBaseResultDataValue.ErrorInfo = "新增机构信息(CenOrg)失败!";
                return tempBaseResultDataValue;
            }
            //添加部门
            HRDept dept = new HRDept();
            tempBaseResultDataValue = AddDeptOfPlatform(client, cenOrg, ref dept);
            if (tempBaseResultDataValue.success == false)
                return tempBaseResultDataValue;

            //添加部门员工加帐号
            HREmployee emp = new HREmployee();
            tempBaseResultDataValue = AddEmployeeOfPlatform(client, cenOrg, dept, user, ref emp);
            if (tempBaseResultDataValue.success == false)
                return tempBaseResultDataValue;

            //添加角色及角色模块信息
            tempBaseResultDataValue = AddRoleAndModuleOfPlatform(client, roleIdStrOfZf, emp, moduleIdStr);
            if (tempBaseResultDataValue.success == false)
                return tempBaseResultDataValue;

            //添加机构运行参数
            tempBaseResultDataValue = AddBParameterOfPlatform(client.LabID);
            if (tempBaseResultDataValue.success == false)
                return tempBaseResultDataValue;

            //添加机构字典类型信息
            tempBaseResultDataValue = AddBDictTypeListOfPlatform(zfLabId, client);
            if (tempBaseResultDataValue.success == false)
                return tempBaseResultDataValue;

            //添加机构的一维条码序号信息
            tempBaseResultDataValue = AddReaBmsSerialOfPlatform(client.LabID);
            if (tempBaseResultDataValue.success == false)
                return tempBaseResultDataValue;

            //添加机构的总库房信息
            tempBaseResultDataValue = AddMainReaStorageOfPlatform(client.LabID);
            if (tempBaseResultDataValue.success == false)
                return tempBaseResultDataValue;

            tempBaseResultDataValue.ErrorInfo = "初始化机构信息成功!";

            //AddSCOperation(client, empID, empName, long.Parse(SServiceClientOperation.机构注册.Key), "机构注册");
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue AddCenOrgOfPlatform(SServiceClient client, ref CenOrg cenOrg)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            cenOrg.LabID = client.LabID;
            cenOrg.DataUpdateTime = DateTime.Now;
            cenOrg.Visible = 1;
            cenOrg.OrgNo = GetMaxOrgNo();
            this.Entity = cenOrg;
            tempBaseResultDataValue.success = this.Add();
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue AddDeptOfPlatform(SServiceClient client, CenOrg cenOrg, ref HRDept dept)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            dept.LabID = client.LabID;
            dept.ParentID = 0;
            dept.UseCode = cenOrg.OrgNo.ToString();
            dept.CName = cenOrg.CName;
            dept.IsUse = true;
            dept.DispOrder = 1;
            dept.DeveCode = "sys_admin";
            IBHRDept.Entity = dept;
            tempBaseResultDataValue.success = IBHRDept.Add();
            if (tempBaseResultDataValue.success == false)
            {
                tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增部门出错)!";
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue AddEmployeeOfPlatform(SServiceClient client, CenOrg cenOrg, HRDept dept, RBACUser user, ref HREmployee emp)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };

            #region 添加初始化部门员工
            emp.LabID = client.LabID;
            emp.CName = user.CName;
            if (string.IsNullOrEmpty(emp.CName))
                emp.CName = "系统管理员";
            emp.IsUse = true;
            emp.IsEnabled = 1;

            emp.NameL = "系统";
            emp.NameF = "管理员";
            emp.DeveCode = "sys_admin";
            emp.HRDept = dept;
            emp.DispOrder = 1;
            if (emp.HRDept.DataTimeStamp == null)
                emp.HRDept.DataTimeStamp = dataTimeStamp;
            IBHREmployee.Entity = emp;
            tempBaseResultDataValue.success = IBHREmployee.Add();
            if (tempBaseResultDataValue.success == false)
            {
                tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增部门员工出错)!";
                return tempBaseResultDataValue;
            }
            #endregion

            #region 添加初始化员工帐号
            if (string.IsNullOrEmpty(user.Account))
                user.Account = cenOrg.OrgNo.ToString();
            user.LabID = client.LabID;
            user.IsUse = true;
            user.AccLock = false;
            user.AccBeginTime = DateTime.Now;

            user.HREmployee = emp;
            if (user.HREmployee.DataTimeStamp == null)
                user.HREmployee.DataTimeStamp = dataTimeStamp;
            if (string.IsNullOrEmpty(user.PWD))
            {
                user.PWD = "123456";
            }


            user.PWD = Common.Public.SecurityHelp.MD5Encrypt(user.PWD, Common.Public.SecurityHelp.PWDMD5Key);
            IBRBACUser.Entity = user;
            tempBaseResultDataValue.success = IBRBACUser.Add();
            if (tempBaseResultDataValue.success == false)
            {
                tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增员工帐号出错)!";
            }
            #endregion

            return tempBaseResultDataValue;
        }
        public BaseResultDataValue AddRoleAndModuleOfPlatform(SServiceClient client, string roleIdStrOfZf, HREmployee emp, string moduleIdStr)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };

            #region 添加初始化角色
            RBACRole role = new RBACRole();
            role.LabID = client.LabID;
            role.IsUse = true;
            role.DispOrder = 1;
            role.DeveCode = "sys_admin";
            role.CName = "机构管理员";
            role.Comment = "【" + client.Name + "】的机构管理员";
            IBRBACRole.Entity = role;
            tempBaseResultDataValue.success = IBRBACRole.Add();
            if (tempBaseResultDataValue.success == false)
            {
                tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增机构角色出错)!";
                return tempBaseResultDataValue;
            }
            #endregion

            #region 添加初始化员工角色
            RBACEmpRoles empRoles = new RBACEmpRoles();
            empRoles.LabID = client.LabID;
            empRoles.IsUse = true;
            empRoles.DispOrder = 1;
            empRoles.HREmployee = emp;
            if (empRoles.HREmployee.DataTimeStamp == null)
                empRoles.HREmployee.DataTimeStamp = dataTimeStamp;
            empRoles.RBACRole = role;
            if (empRoles.RBACRole.DataTimeStamp == null)
                empRoles.RBACRole.DataTimeStamp = dataTimeStamp;
            IBRBACEmpRoles.Entity = empRoles;
            tempBaseResultDataValue.success = IBRBACEmpRoles.Add();
            if (tempBaseResultDataValue.success == false)
            {
                tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增员工角色出错)!";
                return tempBaseResultDataValue;
            }
            #endregion

            #region 添加初始化角色模块
            string[] tempArr = moduleIdStr.TrimEnd(',').Split(',');
            foreach (var moduleId in tempArr)
            {
                if (tempBaseResultDataValue.success == false) break;

                RBACRoleModule roleModule = new RBACRoleModule();
                RBACModule module = IBRBACModule.Get(long.Parse(moduleId));
                if (module == null)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(获取模块信息出错)!";
                    break;
                }
                roleModule.RBACModule = module;
                roleModule.LabID = client.LabID;
                roleModule.IsUse = true;
                roleModule.RBACRole = role;
                if (roleModule.RBACRole.DataTimeStamp == null)
                    roleModule.RBACRole.DataTimeStamp = dataTimeStamp;
                IBRBACRoleModule.Entity = roleModule;
                tempBaseResultDataValue.success = IBRBACRoleModule.Add();
                if (tempBaseResultDataValue.success == false)
                    tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增角色模块信息出错)!";
            }
            #endregion

            //初始化机构添加从智方机构选择的角色信息,员工角色及角色模块信息
            if (!string.IsNullOrEmpty(roleIdStrOfZf))
            {
                tempBaseResultDataValue = AddRoleAndModuleOfZf(client, emp, roleIdStrOfZf, moduleIdStr);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue AddRoleAndModuleOfZf(SServiceClient client, HREmployee emp, string roleIdStrOfZf, string moduleIdStr)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };

            //先获取原智方机构选择的角色信息
            IList<RBACRole> roleOfZfList = IBRBACRole.SearchListByHQL("rbacrole.IsUse=1 and rbacrole.Id in (" + roleIdStrOfZf.TrimEnd(',') + ")");

            foreach (var roleOfZf in roleOfZfList)
            {
                if (tempBaseResultDataValue.success == false) break;

                #region 添加角色
                RBACRole role = new RBACRole();
                role.LabID = client.LabID;
                role.EName = roleOfZf.EName;
                role.SName = roleOfZf.SName;
                role.Shortcode = roleOfZf.Shortcode;

                role.PinYinZiTou = roleOfZf.PinYinZiTou;
                role.StandCode = roleOfZf.StandCode;
                role.CName = roleOfZf.CName;
                role.Comment = "所属机构为：【" + client.Name + "】；" + roleOfZf.Comment;

                role.IsUse = true;
                role.DispOrder = 1;
                role.DeveCode = "";

                IBRBACRole.Entity = role;
                tempBaseResultDataValue.success = IBRBACRole.Add();

                if (tempBaseResultDataValue.success == false)
                {
                    tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增机构角色出错)!";
                    break;
                }
                #endregion

                #region 添加员工角色
                RBACEmpRoles empRoles = new RBACEmpRoles();
                empRoles.LabID = client.LabID;
                empRoles.IsUse = true;
                empRoles.DispOrder = 1;
                empRoles.HREmployee = emp;
                if (empRoles.HREmployee.DataTimeStamp == null)
                    empRoles.HREmployee.DataTimeStamp = dataTimeStamp;
                empRoles.RBACRole = role;
                if (empRoles.RBACRole.DataTimeStamp == null)
                    empRoles.RBACRole.DataTimeStamp = dataTimeStamp;
                IBRBACEmpRoles.Entity = empRoles;
                tempBaseResultDataValue.success = IBRBACEmpRoles.Add();
                if (tempBaseResultDataValue.success == false)
                {
                    tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增员工角色出错)!";
                    break;
                }
                #endregion

                #region 添加角色模块(角色范围模块只能在moduleIdStr里)
                var roleModuleOfZfList = roleOfZf.RBACRoleModuleList;
                if (roleModuleOfZfList != null)
                {
                    foreach (var roleModuleOfZf in roleModuleOfZfList)
                    {
                        if (tempBaseResultDataValue.success == false) break;

                        if (roleModuleOfZf.RBACModule == null && roleModuleOfZf.RBACRole == null)
                            continue;

                        if (moduleIdStr.Contains(roleModuleOfZf.RBACModule.Id.ToString()))
                        {
                            RBACRoleModule roleModule = new RBACRoleModule();
                            RBACModule module = roleModuleOfZf.RBACModule;
                            roleModule.RBACModule = module;
                            roleModule.LabID = client.LabID;
                            roleModule.IsUse = true;
                            roleModule.RBACRole = role;
                            if (roleModule.RBACRole.DataTimeStamp == null)
                                roleModule.RBACRole.DataTimeStamp = dataTimeStamp;

                            IBRBACRoleModule.Entity = roleModule;
                            tempBaseResultDataValue.success = IBRBACRoleModule.Add();
                            if (tempBaseResultDataValue.success == false)
                            {
                                tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增角色模块信息出错)!";
                                break;
                            }
                        }
                    }
                    if (tempBaseResultDataValue.success == false) break;
                }
                #endregion

            }

            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 运行参数的默认用户设置信息赋值
        /// </summary>
        /// <param name="param"></param>
        private void EditItemEditInfo(ref BParameter param)
        {
            if (param.ParaNo == SYSParaNo.盘库时实盘数是否取库存数.Key)
            {
                param.ItemEditInfo = "{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{'1':'是'},{'2':'否'}]\"}";
            }
            else if (param.ParaNo == SYSParaNo.是否开启近效期.Key)
            {
                param.ItemEditInfo = "{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{'1':'开启'},{'2':'不开启'},{'3':'界面选择默认开启'},{'4':'界面选择默认不开启'}]\"}";
            }
            else if (param.ParaNo == SYSParaNo.是否强制近效期出库.Key)
            {
                param.ItemEditInfo = "{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"4\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{'1':'强制'},{'2':'不强制'},{'3':'界面选择默认强制'},{'4':'界面选择默认不强制'}]\"}";
            }
        }
        public BaseResultDataValue AddBParameterOfPlatform(long labId)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            Dictionary<string, BaseClassDicEntity> tempDict = SYSParaNo.GetStatusDic();
            foreach (var dict in tempDict)
            {
                if (tempBaseResultDataValue.success == false)
                    break;

                BaseClassDicEntity dicEntity = dict.Value;
                BParameter param = new BParameter();
                param.DispOrder = 0;
                param.LabID = labId;
                param.IsUse = true;
                param.ParaType = "CONFIG";
                param.Name = dicEntity.Name;
                param.SName = dicEntity.SName;
                param.ParaNo = dicEntity.Id;
                param.ParaValue = dicEntity.DefaultValue;
                param.ParaDesc = dicEntity.Memo;
                EditItemEditInfo(ref param);
                IBBParameter.Entity = param;
                tempBaseResultDataValue.success = IBBParameter.Add();
                if (tempBaseResultDataValue.success == false)
                    tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增机构运行参数出错)!";
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue AddBDictTypeListOfPlatform(long zfLabId, SServiceClient client)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            IList<BDictType> allList = IDAO.RBAC.DataAccess_SQL.CreateBDictTypeDao_SQL().GetListByHQL("LabID=" + zfLabId); //IBBDictType.SearchListByHQL("bdicttype.LabID=" + labId);
            if (allList == null || allList.Count <= 0)
                allList = new List<BDictType>();

            foreach (BDictType dictType in allList)
            {
                if (tempBaseResultDataValue.success == false)
                    break;

                BDictType model = new BDictType();
                //model.Id = dictType.Id;
                model.DispOrder = dictType.DispOrder;
                model.LabID = client.LabID;
                model.IsUse = true;
                model.CName = dictType.CName;
                model.Memo = dictType.Memo;
                model.DictTypeCode = dictType.DictTypeCode;
                IBBDictType.Entity = model;
                tempBaseResultDataValue.success = IBBDictType.Add();
                if (tempBaseResultDataValue.success == false)
                    tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增机构字典类型信息出错)!";
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue AddReaBmsSerialOfPlatform(long labID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            Dictionary<string, BaseClassDicEntity> tempDict = ReaBmsSerialType.GetStatusDic();

            foreach (var dict in tempDict)
            {
                if (tempBaseResultDataValue.success == false)
                    break;

                BaseClassDicEntity dicEntity = dict.Value;
                ReaBmsSerial entity = new ReaBmsSerial();
                entity.LabID = labID;
                entity.BmsType = dicEntity.Code;
                entity.DataUpdateTime = DateTime.Now;
                entity.CurBarCode = 0;
                IBReaBmsSerial.Entity = entity;
                tempBaseResultDataValue.success = IBReaBmsSerial.Add();
                if (tempBaseResultDataValue.success == false)
                    tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增一维条码序号信息出错)!";
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue AddMainReaStorageOfPlatform(long labID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            ReaStorage entity = new ReaStorage();
            entity.LabID = labID;
            entity.CName = "总库";
            entity.DataUpdateTime = DateTime.Now;
            entity.DispOrder = 1;
            entity.Memo = "初始化总库";
            entity.IsMainStorage = true;
            entity.ShortCode = "1";
            entity.Visible = true;
            IBReaStorage.Entity = entity;
            tempBaseResultDataValue.success = IBReaStorage.Add();
            if (tempBaseResultDataValue.success == false)
                tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增初始化总库信息出错)!";

            return tempBaseResultDataValue;
        }
        #endregion

        #region 平台上客户机构的授权变更
        public EntityList<RBACRoleModule> SearchRBACRoleModuleByLabIDAndSysRoleId(long labId)
        {
            EntityList<RBACRoleModule> entityList = new EntityList<RBACRoleModule>();
            //角色为"机构管理员"(一个机构只能存在一个机构管理员,DeveCode=sys_admin)
            IList<RBACRole> sysRoleList = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleDao_SQL().GetListByHQL("IsUse=1 and LabID=" + labId + " and DeveCode='sys_admin'");
            if (sysRoleList.Count() != 1)
            {
                string errorInfo = "获取LabID为:" + labId + ",角色为【机构管理员】的记录数为:" + sysRoleList.Count() + "。";
                return entityList;
            }
            RBACRole sysRole = sysRoleList[0];
            entityList.list = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleModuleDao_SQL().GetListByHQL("LabID=" + labId + " and RoleID=" + sysRole.Id);
            if (entityList.list != null && entityList.list.Count > 0)
            {
                entityList.list = entityList.list.OrderBy(p => p.RBACModule.ParentID).ThenBy(p => p.RBACModule.DispOrder).ToList();
                entityList.count = entityList.list.Count;
            }
            return entityList;
        }
        public BaseResultBool EditCenOrgAuthorizationModifyOfPlatform(long labId, long cenOrgId, IList<RBACModule> moduleList, long empID, string empName)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (moduleList == null || moduleList.Count <= 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "传入参数(labId)信息为空!";
                return baseResultBool;
            }
            if (labId <= 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "获取传入参数(cenOrg)信息的LabID值为空!";
                return baseResultBool;
            }
            //授权机构信息
            SServiceClient client = IDAO.RBAC.DataAccess_SQL.CreateSServiceClientDao_SQL().Get(labId);
            if (client == null)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "获取LabID为:" + labId + ",(SServiceClient)不存在!请联系机构管理员再操作。";
                return baseResultBool;
            }
            //获取授权机构信息
            CenOrg cenOrg = IDAO.RBAC.DataAccess_SQL.CreateCenOrgDao_SQL().Get(cenOrgId);
            if (cenOrg == null)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "获取机构ID(cenOrgId)为:" + cenOrgId + ",机构信息(CenOrg)为空!请注册后再操作。"; ;
                return baseResultBool;
            }
            //获取授权机构的所有角色信息
            IList<RBACRole> roleList = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleDao_SQL().GetListByHQL("IsUse=1 and LabID=" + client.LabID);
            //角色为"机构管理员"(一个机构只能存在一个机构管理员,DeveCode=sys_admin)
            var sysRoleList = roleList.Where(p => p.DeveCode == "sys_admin");
            if (sysRoleList.Count() != 1)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "获取LabID为:" + client.LabID + ",角色为【机构管理员】的记录数为:" + sysRoleList.Count() + "。";
                return baseResultBool;
            }
            RBACRole sysRole = sysRoleList.ElementAt(0);

            //获取授权机构的所有角色模块信息
            IList<RBACRoleModule> roleModuleList = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleModuleDao_SQL().GetListByHQL("LabID=" + client.LabID);
            //角色名称为"机构管理员"的角色模块信息
            var sysRoleModuleList = roleModuleList.Where(p => p.RBACRole.Id == sysRole.Id);
            //角色名称为"机构管理员"的当前授权的角色模块集合
            IList<RBACRoleModule> curSysRoleModuleList = new List<RBACRoleModule>();
            //角色名称为"机构管理员"的当前授权的模块集合
            IList<RBACModule> curModuleList = new List<RBACModule>();

            //区分出角色名称为"机构管理员"的角色模块信息的删除模块集合
            StringBuilder delMemo = new StringBuilder();
            IList<RBACRoleModule> delRoleModuleList = new List<RBACRoleModule>();
            foreach (RBACRoleModule sysRoleModule in sysRoleModuleList)
            {
                var tempModuleList = moduleList.Where(p => p.Id == sysRoleModule.RBACModule.Id);
                if (tempModuleList == null || tempModuleList.Count() <= 0)
                {
                    if (delMemo.Length <= 0) delMemo.Append("移除角色模块有:");

                    if (!delRoleModuleList.Contains(sysRoleModule))
                    {
                        delRoleModuleList.Add(sysRoleModule);
                        delMemo.Append("模块为:【" + sysRoleModule.RBACModule.CName + "】;");
                    }
                }
                else
                {
                    if (!curSysRoleModuleList.Contains(sysRoleModule))
                        curSysRoleModuleList.Add(sysRoleModule);
                    if (!curModuleList.Contains(sysRoleModule.RBACModule))
                        curModuleList.Add(sysRoleModule.RBACModule);
                }
            }

            //区分出角色为"机构管理员"的角色模块信息的新增模块集合
            StringBuilder addMemo = new StringBuilder();
            IList<RBACModule> addModuleList = new List<RBACModule>();
            foreach (RBACModule module in moduleList)
            {
                var tempRoleModuleList = sysRoleModuleList.Where(p => p.RBACModule.Id == module.Id);
                if (tempRoleModuleList == null || tempRoleModuleList.Count() <= 0)
                {
                    if (addMemo.Length <= 0) addMemo.Append("新增角色模块有:");
                    addMemo.Append("模块为:【" + module.CName + "】;");

                    RBACModule curModule = GetRBACModule(IBRBACModule.Get(module.Id)); //IBRBACModule.Get(module.Id);// IDAO.RBAC.DataAccess_SQL.CreateRBACModuleDao_SQL().Get(module.Id);
                    if (!curModuleList.Contains(curModule))
                        curModuleList.Add(curModule);
                    if (!addModuleList.Contains(curModule))
                        addModuleList.Add(curModule);
                }
            }

            if (addModuleList.Count <= 0 && delRoleModuleList.Count <= 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "本次授权变更没有新增的角色模块及被移除的角色模块。";
                return baseResultBool;
            }

            if (curSysRoleModuleList.Count <= 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "获取本次授权变更的角色模块信息为空。";
                return baseResultBool;
            }

            if (addModuleList.Count > 0)
                baseResultBool = AddRBACRoleModuleOfAuthorization(client, sysRole, addModuleList, ref curSysRoleModuleList);
            if (baseResultBool.success == false) return baseResultBool;

            if (delRoleModuleList.Count > 0)
                baseResultBool = DelRBACRoleModuleOfAuthorization(delRoleModuleList);
            if (baseResultBool.success == false) return baseResultBool;

            string memo = addMemo.Append(delMemo.ToString()).ToString();
            //授权变更操作记录
            //AddSCOperation(client, empID, empName, long.Parse(SServiceClientOperation.授权变更.Key), memo);

            //授权变更时处理一维条码序号信息,判断是否存在新加的一维条码序号
            baseResultBool = AddReaBmsSerialOfLabID(client.LabID);
            if (baseResultBool.success == false) return baseResultBool;

            //授权变更文件处理
            baseResultBool = AddSCAttachmentOfAuthorization(client, cenOrg, sysRole, curModuleList, curSysRoleModuleList, memo);
            if (baseResultBool.success == true)
                baseResultBool.ErrorInfo = memo;
            return baseResultBool;
        }
        private BaseResultBool AddRBACRoleModuleOfAuthorization(SServiceClient client, RBACRole sysRole, IList<RBACModule> addModuleList, ref IList<RBACRoleModule> curRoleModuleList)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (var module in addModuleList)
            {
                if (baseResultBool.success == false) break;

                RBACRoleModule sysRoleModule = new RBACRoleModule();
                sysRoleModule.DataTimeStamp = dataTimeStamp;
                sysRoleModule.RBACModule = module;// IBRBACModule.Get(module.Id);
                if (sysRoleModule.RBACModule.DataTimeStamp == null)
                    sysRoleModule.RBACModule.DataTimeStamp = dataTimeStamp;

                sysRoleModule.LabID = client.LabID;
                sysRoleModule.IsUse = true;
                sysRoleModule.RBACRole = sysRole;// IBRBACRole.Get(sysRole.Id); //sysRole;
                if (sysRoleModule.RBACRole.DataTimeStamp == null)
                    sysRoleModule.RBACRole.DataTimeStamp = dataTimeStamp;
                IBRBACRoleModule.Entity = sysRoleModule;
                curRoleModuleList.Add(sysRoleModule);
                baseResultBool.success = IBRBACRoleModule.Add();
                if (baseResultBool.success == false)
                    baseResultBool.ErrorInfo = "机构授权变更保存失败(新增角色模块信息出错)!";
            }
            return baseResultBool;
        }
        private BaseResultBool DelRBACRoleModuleOfAuthorization(IList<RBACRoleModule> delRoleModuleList)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            foreach (var roleModule in delRoleModuleList)
            {
                if (baseResultBool.success == false) break;
                ZhiFang.Common.Log.Log.Debug("移除角色模块信息:LabID=" + roleModule.LabID + ",ModuleVisiteID:" + roleModule.Id + ",RBACModuleId:" + roleModule.RBACModule.Id + "," + roleModule.RBACRole.Id);
                IBRBACRoleModule.Entity = roleModule;
                baseResultBool.success = IBRBACRoleModule.Remove();
                if (baseResultBool.success == false)
                    baseResultBool.ErrorInfo = "机构授权变更保存失败(移除角色模块信息出错)!";
            }
            return baseResultBool;
        }
        public BaseResultBool AddReaBmsSerialOfLabID(long labID)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            Dictionary<string, BaseClassDicEntity> tempDict = ReaBmsSerialType.GetStatusDic();
            IList<ReaBmsSerial> allList = IBReaBmsSerial.GetListOfNoLabIDByHql("reabmsserial.LabID=" + labID);

            foreach (var dict in tempDict)
            {
                if (baseResultBool.success == false)
                    break;

                BaseClassDicEntity dicEntity = dict.Value;
                if (allList != null && allList.Count > 0)
                {
                    var tempList = allList.Where(p => p.LabID == labID && p.BmsType == dicEntity.Code);
                    if (tempList != null && tempList.Count() > 0) continue;
                }

                ReaBmsSerial entity = new ReaBmsSerial();
                entity.LabID = labID;
                entity.BmsType = dicEntity.Code;
                entity.DataUpdateTime = DateTime.Now;
                entity.CurBarCode = 0;
                IBReaBmsSerial.Entity = entity;
                baseResultBool.success = IBReaBmsSerial.Add();
                if (baseResultBool.success == false)
                    baseResultBool.ErrorInfo = "初始化机构信息失败(新增一维条码序号信息出错)!";
            }
            return baseResultBool;
        }
        private BaseResultBool AddSCAttachmentOfAuthorization(SServiceClient client, CenOrg cenOrg, RBACRole sysRole, IList<RBACModule> curModuleList, IList<RBACRoleModule> curRoleModuleList, string memo)
        {
            BaseResultBool baseResultBool = new BaseResultBool();

            JObject jresult = new JObject();
            //机构客户所属ID
            jresult.Add("LabID", client.LabID);
            jresult.Add("CenOrgId", cenOrg.Id);
            //机构所属机构平台编码
            jresult.Add("OrgNo", cenOrg.OrgNo);
            //机构所属机构平台编码
            jresult.Add("OrgCName", cenOrg.CName);
            //授权文件类型:1:系统初始化;2:授权变更
            jresult.Add("FileType", 2);

            jresult.Add("SServiceClient", JObject.FromObject(client));
            jresult.Add("CenOrg", JObject.FromObject(cenOrg));
            jresult.Add("SysRole", JObject.FromObject(GetRBACRole(sysRole)));

            IList<RBACModule> moduleList = new List<RBACModule>();
            IList<RBACRoleModule> roleModuleList = new List<RBACRoleModule>();
            foreach (RBACRoleModule roleModule in curRoleModuleList)
            {
                var tempRoleModule = GetRoleModule(roleModule, ref moduleList);
                //tempRoleModule.RBACRole = GetRBACRole(sysRole);
                roleModuleList.Add(tempRoleModule);
            }
            jresult.Add("RBACRoleModuleList", JArray.FromObject(roleModuleList));
            jresult.Add("RBACModuleList", JArray.FromObject(moduleList));
            //ZhiFang.Common.Log.Log.Debug("RBACRoleModuleList:"+ jresult.SelectToken("RBACRoleModuleList").ToList());

            //获取最新的系统运行参数(客户端需要按LabID+ParaType+ParaNo作判断)
            IList<BParameter> bparameterList = GetBParameterList(client);
            jresult.Add("BParameterList", JArray.FromObject(bparameterList));
            ZhiFang.Common.Log.Log.Debug(cenOrg.OrgNo + "授权变更内容:" + jresult.ToString());

            //授权文件存放路径
            string subDir = "平台授权变更";
            string basePath = GetFilePath(client.LabID, subDir);
            // string filePath = DateTime.Now.ToString("yyyy-MM-dd");
            string filePath = DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\" + DateTime.Now.ToString("HHmmss") + "\\";

            if (!Directory.Exists(basePath + "\\" + filePath))
                Directory.CreateDirectory(basePath + "\\" + filePath);
            string fileCName = cenOrg.CName + "授权变更.json." + FileExt.zf;
            string filePathAll = Path.Combine(basePath + "\\" + filePath, fileCName);
            if (File.Exists(filePathAll))
            {
                File.Delete(filePathAll);
            }
            //平台保存授权文件
            FileStream fsWriter = new FileStream(filePathAll, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            byte[] buffer = Encoding.UTF8.GetBytes(Common.Public.SecurityHelp.MD5Encrypt(jresult.ToString(), Common.Public.SecurityHelp.PWDMD5Key));
            fsWriter.Write(buffer, 0, buffer.Length);
            fsWriter.Close();

            //读取导出的授权文件
            FileStream fileStream = new FileStream(filePathAll, FileMode.Open);
            StreamReader sr = new StreamReader(fileStream, Encoding.UTF8);
            long fileSize = fileStream.Length;

            SCAttachment attachment = new SCAttachment();
            attachment.LabID = client.LabID;
            attachment.BobjectID = client.LabID;
            attachment.BusinessModuleCode = "SServiceClient";//业务模块代码

            attachment.IsUse = true;
            attachment.FileName = cenOrg.CName + "授权变更.json";
            attachment.FileType = "text/plain";
            attachment.FileExt = ".json";
            attachment.FileSize = fileSize;// / 1024;
            attachment.FilePath = Path.Combine(filePath, fileCName);
            attachment.Memo = memo;
            IBSCAttachment.Entity = attachment;
            baseResultBool.success = IBSCAttachment.Add();

            sr.Close();
            fileStream.Close();

            return baseResultBool;
        }

        #endregion

        #region 从平台上导出客户机构授权文件
        public Stream SearchExportAuthorizationFileOfPlatform(long labId, long cenOrgId, long fileType, ref bool result, ref string fileCName, long empID, string empName)
        {
            Stream fileStream = null;
            result = true;
            //获取待授权机构信息
            SServiceClient client = IDAO.RBAC.DataAccess_SQL.CreateSServiceClientDao_SQL().Get(labId);
            if (client == null)
            {
                string errorInfo = "获取LabID为:" + labId + ",(SServiceClient)不存在!请注册后再操作。";
                MemoryStream memoryStream = GetErrMemoryStreamInfo(errorInfo);
                result = false;
                return memoryStream;
            }
            //获取待授权机构信息
            CenOrg cenOrg = IDAO.RBAC.DataAccess_SQL.CreateCenOrgDao_SQL().Get(cenOrgId);
            if (cenOrg == null)
            {
                string errorInfo = "获取机构ID(cenOrgId)为:" + cenOrgId + ",机构信息(CenOrg)为空!请注册后再操作。";
                MemoryStream memoryStream = GetErrMemoryStreamInfo(errorInfo);
                result = false;
                return memoryStream;
            }

            if (fileType == 1)
            {
                //机构初始化授权文件导出
                fileStream = GetExportAuthorizationFileOfInitialize(client, cenOrg, ref result, ref fileCName, empID, empName);
            }
            else if (fileType == 2)
            {
                //最新的授权变更文件导出
                fileStream = SearchExportAuthorizationFileOfModify(client, cenOrg, ref result, ref fileCName, empID, empName);
            }
            else
            {
                string errorInfo = "传入参数(fileType)的授权文件类型值为:" + fileType + "。";
                MemoryStream memoryStream = GetErrMemoryStreamInfo(errorInfo);
                result = false;
                return memoryStream;
            }

            return fileStream;
        }
        private Stream GetExportAuthorizationFileOfInitialize(SServiceClient client, CenOrg cenOrg, ref bool result, ref string fileCName, long empID, string empName)
        {
            FileStream fileStream = null;
            result = true;

            //获取待授权机构部门信息(该部门及其所有子部门)
            IList<HRDept> deptList = new List<HRDept>();
            IList<HRDept> deptList2 = IDAO.RBAC.DataAccess_SQL.CreateHRDeptDao_SQL().GetListByHQL("LabID=" + client.LabID);
            if (deptList2 == null || deptList2.Count <= 0)
            {
                string errorInfo = "获取LabID为:" + client.LabID + ",部门信息为空!请注册后再操作。";
                MemoryStream memoryStream = GetErrMemoryStreamInfo(errorInfo);
                result = false;
                return memoryStream;
            }
            foreach (HRDept dept in deptList2)
            {
                deptList.Add(GetHRDept(dept));
            }

            //获取待授权机构的所有角色信息
            IList<RBACRole> roleList = new List<RBACRole>();
            IList<RBACRole> roleList2 = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleDao_SQL().GetListByHQL("IsUse=1 and LabID=" + client.LabID);
            foreach (RBACRole role in roleList2)
            {
                roleList.Add(GetRBACRole(role));
            }
            //角色为"机构管理员"(一个机构只能存在一个机构管理员,DeveCode=sys_admin)
            var sysRoleList = roleList.Where(p => p.DeveCode == "sys_admin");
            if (sysRoleList.Count() != 1)
            {
                string errorInfo = "获取LabID为:" + client.LabID + ",角色为【机构管理员】的记录数为:" + sysRoleList.Count() + "。";
                MemoryStream memoryStream = GetErrMemoryStreamInfo(errorInfo);
                result = false;
                return memoryStream;
            }
            RBACRole sysRole = ClassMapperHelp.GetMapper<RBACRole, RBACRole>(sysRoleList.ElementAt(0));

            //获取待授权机构的所有员工信息
            IList<HREmployee> employeeList = new List<HREmployee>();
            IList<HREmployee> employeeList2 = IDAO.RBAC.DataAccess_SQL.CreateHREmployeeDao_SQL().GetListByHQL("LabID=" + client.LabID);
            foreach (HREmployee emp in employeeList2)
            {
                employeeList.Add(GetHREmployee(emp));
            }

            //获取待授权机构的所有员工用户信息
            IList<RBACUser> userList = new List<RBACUser>();
            IList<RBACUser> userList2 = IDAO.RBAC.DataAccess_SQL.CreateRBACUserDao_SQL().GetListByHQL("LabID=" + client.LabID);
            foreach (RBACUser user in userList2)
            {
                userList.Add(GetRBACUser(user));
            }

            //获取待授权机构的所有员工角色信息
            IList<RBACEmpRoles> empRolesList = new List<RBACEmpRoles>();
            IList<RBACEmpRoles> empRolesList2 = IDAO.RBAC.DataAccess_SQL.CreateRBACEmpRolesDao_SQL().GetListByHQL("LabID=" + client.LabID);
            foreach (RBACEmpRoles empRoles in empRolesList2)
            {
                empRolesList.Add(GetRBACEmpRoles(empRoles));
            }

            //获取最新的系统运行参数(客户端需要按LabID+ParaType+ParaNo作判断)
            IList<BParameter> bparameterList = GetBParameterList(client);

            //获取待授权机构的所有角色模块信息
            IList<RBACRoleModule> roleModuleList = new List<RBACRoleModule>();
            IList<RBACRoleModule> roleModuleList2 = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleModuleDao_SQL().GetListByHQL("LabID=" + client.LabID);
            //获取当前机构的全部授权模块信息(平台可能有新增的模块,但待授权机构的数据库可能还没有添加上)
            IList<RBACModule> moduleList = new List<RBACModule>();// IDAO.RBAC.DataAccess_SQL.CreateRBACModuleDao_SQL().GetListByHQL("");
            foreach (RBACRoleModule roleModule in roleModuleList2)
            {
                roleModuleList.Add(GetRoleModule(roleModule, ref moduleList));
            }
            if (moduleList.Count > 0)
                moduleList = moduleList.OrderBy(p => p.ParentID).ThenBy(p => p.DispOrder).ToList();
            //字典类型
            IList<BDictType> dictTypeList = IDAO.RBAC.DataAccess_SQL.CreateBDictTypeDao_SQL().GetListByHQL("LabID=" + client.LabID);
            //条码规则信息(不区分LabID)"LabID=" + client.LabID
            IList<ReaCenBarCodeFormat> barCodeFormatList = IDAO.RBAC.DataAccess_SQL.CreateReaCenBarCodeFormatDao_SQL().GetListByHQL("");

            JObject jresult = new JObject();
            //机构客户所属ID
            jresult.Add("LabID", client.LabID);
            jresult.Add("CenOrgId", cenOrg.Id);
            //机构所属机构平台编码
            jresult.Add("OrgNo", cenOrg.OrgNo);
            //机构所属机构平台编码
            jresult.Add("OrgCName", cenOrg.CName);
            //授权文件类型:1:系统初始化;2:授权变更
            jresult.Add("FileType", 1);

            jresult.Add("SServiceClient", JObject.FromObject(client));
            jresult.Add("CenOrg", JObject.FromObject(cenOrg));
            jresult.Add("HRDeptList", JArray.FromObject(deptList));
            //ParseObjectProperty pop = new ParseObjectProperty();
            //var employeeListStr=pop.GetObjectPropertyNoPlanish(employeeList);
            jresult.Add("HREmployeeList", JArray.FromObject(employeeList));
            jresult.Add("RBACUserList", JArray.FromObject(userList));

            jresult.Add("SysRole", JObject.FromObject(sysRole));
            jresult.Add("RBACRoleList", JArray.FromObject(roleList));
            jresult.Add("RBACEmpRolesList", JArray.FromObject(empRolesList));
            jresult.Add("RBACRoleModuleList", JArray.FromObject(roleModuleList));
            jresult.Add("BParameterList", JArray.FromObject(bparameterList));
            jresult.Add("RBACModuleList", JArray.FromObject(moduleList));
            jresult.Add("BDictTypeList", JArray.FromObject(dictTypeList));
            jresult.Add("ReaCenBarCodeFormatList", JArray.FromObject(barCodeFormatList));
            //ZhiFang.Common.Log.Log.Debug(cenOrg.CName + "初始化授权信息:" + jresult.ToString());

            //授权文件存放路径
            string subDir = "平台初始化";
            string basePath = GetFilePath(client.LabID, subDir);
            string filePath = basePath;
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            fileCName = cenOrg.CName + "初始化授权.json." + FileExt.zf;
            string filePathAll = Path.Combine(filePath, fileCName);
            if (File.Exists(filePathAll))
            {
                File.Delete(filePathAll);
            }
            //平台服务器保存初始化授权文件
            FileStream fsWriter = new FileStream(filePathAll, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            byte[] buffer = Encoding.UTF8.GetBytes(Common.Public.SecurityHelp.MD5Encrypt(jresult.ToString(), Common.Public.SecurityHelp.PWDMD5Key));
            fsWriter.Write(buffer, 0, buffer.Length);
            fsWriter.Close();
            //读取导出的初始化授权文件
            fileStream = new FileStream(filePathAll, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fileStream, Encoding.UTF8);
            //sr.Close();

            //AddSCOperation(client, empID, empName, long.Parse(SServiceClientOperation.授权导出.Key), cenOrg.CName + "初始化授权导出");
            return fileStream;
        }
        private HRDept GetHRDept(HRDept model)
        {
            HRDept dept = ClassMapperHelp.GetMapper<HRDept, HRDept>(model);
            dept.DataTimeStamp = null;
            dept.HRDeptEmpList = new List<HRDeptEmp>();
            dept.HRDeptIdentityList = new List<HRDeptIdentity>();
            dept.HREmployeeList = new List<HREmployee>();
            return dept;
        }
        private RBACRole GetRBACRole(RBACRole role)
        {
            RBACRole role2 = ClassMapperHelp.GetMapper<RBACRole, RBACRole>(role);
            role2.DataTimeStamp = null;
            role2.RBACEmpRoleList = new List<RBACEmpRoles>();
            role2.RBACRoleModuleList = new List<RBACRoleModule>();
            role2.RBACRoleRightList = new List<RBACRoleRight>();
            return role2;
        }
        private HREmployee GetHREmployee(HREmployee emp)
        {
            HREmployee emp2 = ClassMapperHelp.GetMapper<HREmployee, HREmployee>(emp);
            emp2.HRDept = null;
            emp2.DataTimeStamp = null;
            if (emp.HRDept != null)
            {
                HRDept dept = new HRDept();
                dept.Id = emp.HRDept.Id;
                dept.DataTimeStamp = emp.HRDept.DataTimeStamp;
                emp2.HRDept = dept;
            }
            emp2.HRDeptEmpList = new List<HRDeptEmp>();
            emp2.RBACUserList = new List<RBACUser>();
            emp2.RBACEmpOptionsList = new List<RBACEmpOptions>(); ;
            emp2.RBACEmpRoleList = new List<RBACEmpRoles>();
            emp2.HREmpIdentityList = new List<HREmpIdentity>();
            return emp2;
        }
        private RBACUser GetRBACUser(RBACUser user)
        {
            RBACUser user2 = ClassMapperHelp.GetMapper<RBACUser, RBACUser>(user);
            HREmployee emp = ClassMapperHelp.GetMapper<HREmployee, HREmployee>(user2.HREmployee);

            emp.HRDept = null;
            emp.HRDeptEmpList = new List<HRDeptEmp>();
            emp.RBACUserList = new List<RBACUser>();
            emp.RBACEmpOptionsList = new List<RBACEmpOptions>();
            emp.RBACEmpRoleList = new List<RBACEmpRoles>();
            emp.HREmpIdentityList = new List<HREmpIdentity>();

            user2.DataTimeStamp = null;
            user2.HREmployee = emp;
            return user;
        }
        private RBACEmpRoles GetRBACEmpRoles(RBACEmpRoles empRoles)
        {
            RBACEmpRoles empRoles2 = ClassMapperHelp.GetMapper<RBACEmpRoles, RBACEmpRoles>(empRoles);

            HREmployee emp = new HREmployee();
            emp.Id = empRoles.HREmployee.Id;
            emp.LabID = empRoles.HREmployee.LabID;
            emp.DataTimeStamp = empRoles.HREmployee.DataTimeStamp;

            RBACRole role = new RBACRole();
            role.Id = empRoles.RBACRole.Id;
            role.LabID = empRoles.RBACRole.LabID;
            role.DataTimeStamp = empRoles.RBACRole.DataTimeStamp;

            empRoles2.RBACRole = role;
            empRoles2.HREmployee = emp;
            empRoles2.DataTimeStamp = null;
            return empRoles2;
        }
        private RBACRoleModule GetRoleModule(RBACRoleModule roleModule, ref IList<RBACModule> moduleList)
        {
            RBACRoleModule roleModule2 = ClassMapperHelp.GetMapper<RBACRoleModule, RBACRoleModule>(roleModule);

            RBACModule module = GetRBACModule(roleModule.RBACModule);
            if (!moduleList.Contains(module))
                moduleList.Add(module);

            RBACModule module2 = new RBACModule();
            module2.LabID = roleModule.RBACModule.LabID;
            module2.Id = roleModule.RBACModule.Id;
            module2.CName = roleModule.RBACModule.CName;
            module2.Url = roleModule.RBACModule.Url;
            module2.DataTimeStamp = roleModule2.RBACModule.DataTimeStamp;

            RBACRole role = new RBACRole();
            role.Id = roleModule.RBACRole.Id;
            role.LabID = roleModule.RBACRole.LabID;
            role.CName = roleModule.RBACRole.CName;
            roleModule2.DataTimeStamp = roleModule2.RBACRole.DataTimeStamp;

            roleModule2.Id = roleModule.Id;
            roleModule2.LabID = roleModule.LabID;
            roleModule2.RBACRole = role;
            roleModule2.RBACModule = module2;
            roleModule2.DataTimeStamp = null;
            return roleModule2;
        }
        private RBACModule GetRBACModule(RBACModule module)
        {
            RBACModule module2 = ClassMapperHelp.GetMapper<RBACModule, RBACModule>(module);
            module2.BTDAppComponents = null;
            module2.RBACEmpOptionsList = new List<RBACEmpOptions>();
            module2.RBACModuleOperList = new List<RBACModuleOper>();
            module2.RBACRoleModuleList = new List<RBACRoleModule>();
            return module2;
        }
        private Stream SearchExportAuthorizationFileOfModify(SServiceClient client, CenOrg cenOrg, ref bool result, ref string fileCName, long empID, string empName)
        {
            FileStream fileStream = null;
            result = true;

            ZhiFang.Common.Log.Log.Debug("client.LabID:" + client.LabID + ",cenOrg.LabID:" + cenOrg.LabID);
            //只获取最后加入的授权变更文件的附件信息
            string hql = "scattachment.BobjectID=" + cenOrg.LabID + " and scattachment.BusinessModuleCode='SServiceClient'";
            ZhiFang.Common.Log.Log.Debug("SCAttachment.hql:" + hql);
            EntityList<SCAttachment> attachmentList = IBSCAttachment.SearchListByHQL(hql, "scattachment.DataAddTime DESC", 1, 1);
            if (attachmentList.count <= 0)
            {
                string errorInfo = "获取机构ID(LabID)为:" + cenOrg.LabID + ",授权变更文件的附件信息为空!";
                MemoryStream memoryStream = GetErrMemoryStreamInfo(errorInfo);
                result = false;
                return memoryStream;
            }

            fileCName = cenOrg.CName + "授权变更.json." + FileExt.zf;
            SCAttachment attachment = attachmentList.list.OrderByDescending(p => p.DataAddTime).ElementAt(0);
            //授权文件存放路径
            string subDir = "平台授权变更";
            string basePath = GetFilePath(cenOrg.LabID, subDir);
            string filePathAll = Path.Combine(basePath, attachment.FilePath);
            ZhiFang.Common.Log.Log.Debug("filePathAll:" + filePathAll);
            if (!File.Exists(filePathAll))
            {
                string errorInfo = "获取机构ID(LabID)为:" + cenOrg.LabID + ",授权变更文件的附件不存在!";
                MemoryStream memoryStream = GetErrMemoryStreamInfo(errorInfo);
                result = false;
                return memoryStream;
            }

            try
            {
                //读取授权变更文件               
                fileStream = new FileStream(filePathAll, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fileStream, Encoding.UTF8);
                //sr.Close();
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("读取授权文件错误:" + ex.Message);
                string errorInfo = "获取机构ID(LabID)为:" + client.LabID + ",读取授权变更文件失败!";
                MemoryStream memoryStream = GetErrMemoryStreamInfo(errorInfo);
                result = false;
                return memoryStream;
            }
            //if (result)
            //AddSCOperation(client, empID, empName, long.Parse(SServiceClientOperation.授权导出.Key), cenOrg.CName + "授权变更导出");

            return fileStream;
        }
        private IList<BParameter> GetBParameterList(SServiceClient client)
        {
            Dictionary<string, BaseClassDicEntity> tempDict = SYSParaNo.GetStatusDic();
            IList<BParameter> bparameterList = new List<BParameter>();

            bparameterList = IDAO.RBAC.DataAccess_SQL.CreateBParameterDao_SQL().GetListByHQL("LabID=" + client.LabID);
            if (bparameterList != null && bparameterList.Count > 0) return bparameterList;

            foreach (var dict in tempDict)
            {
                BaseClassDicEntity dicEntity = dict.Value;
                BParameter param = new BParameter();
                param.DispOrder = 0;
                param.LabID = client.LabID;
                param.IsUse = true;
                param.ParaType = "CONFIG";
                param.Name = dicEntity.Name;
                param.SName = dicEntity.SName;
                param.ParaNo = dicEntity.Id;
                param.ParaValue = dicEntity.DefaultValue;
                //param.ItemEditInfo= dicEntity.ItemEditInfo;
                param.ParaDesc = dicEntity.Memo;
                bparameterList.Add(param);
            }
            return bparameterList;
        }
        /// <summary>
        /// 获取授权文件的存放目录路径
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="subDir">二级存放路径:初始化授权或者授权变更</param>
        /// <returns></returns>
        private string GetFilePath(long labId, string subDir)
        {
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "License\\";
            if (!string.IsNullOrEmpty(subDir))
            {
                filePath = filePath + subDir;
            }
            if (labId > 0)
                filePath = filePath + "\\" + labId;

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            return filePath;
        }
        public MemoryStream GetErrMemoryStreamInfo(string errorInfo)
        {
            MemoryStream memoryStream = null;
            string fileName = "ErrFile.html";
            if (String.IsNullOrEmpty(errorInfo))
                errorInfo = "授权文件不存在!请联系管理员。";
            StringBuilder strb = new StringBuilder("<div class='alert alert-warning' style='margin:40px 20px;text-align:center;padding-top:40px;padding-bottom:40px;'><h4>错误提示信息</h4><p style='color: red; padding: 5px; word -break:break-all; word - wrap:break-word; '>");
            strb.Append(errorInfo);
            strb.Append("</p></div>");
            byte[] infoByte = Encoding.UTF8.GetBytes(strb.ToString());
            memoryStream = new MemoryStream(infoByte);
            Encoding code = Encoding.GetEncoding("UTF-8");
            System.Web.HttpContext.Current.Response.ContentEncoding = code;
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html; charset=UTF-8";
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
            return memoryStream;
        }
        #endregion

        #region 客户端机构授权文件导入
        public BaseResultDataValue AddUploadAuthorizationFileOfClient(HttpPostedFile file, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            tempBaseResultDataValue.ResultDataValue = this.GetCenOrgDataValue("");
            //读取授权文件内容
            Stream sf = file.InputStream;
            StreamReader sr = new StreamReader(sf, Encoding.UTF8);
            //解密授权文件内容
            string tempStr = Common.Public.SecurityHelp.MD5Decrypt(sr.ReadToEnd(), Common.Public.SecurityHelp.PWDMD5Key);

            //将授权内容转换为对象
            JObject jresult = JObject.Parse(tempStr);

            if (jresult["LabID"] == null || string.IsNullOrEmpty(jresult["LabID"].ToString()))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "授权文件内容没有包含机构(LabID)信息!";
                return tempBaseResultDataValue;
            }
            if (jresult["CenOrgId"] == null || string.IsNullOrEmpty(jresult["CenOrgId"].ToString()))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "授权文件内容没有包含机构ID(CenOrgId)信息!";
                return tempBaseResultDataValue;
            }
            if (jresult["OrgNo"] == null || string.IsNullOrEmpty(jresult["OrgNo"].ToString()))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "授权文件内容没有包含机构所属平台编码(OrgNo)信息!";
                return tempBaseResultDataValue;
            }
            if (jresult["FileType"] == null || string.IsNullOrEmpty(jresult["FileType"].ToString()))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "授权文件内容没有包含授权文件类型(FileType)信息!";
                return tempBaseResultDataValue;
            }

            //判断授权文件类型:初始化授权还是授权变更
            string fileType = jresult["FileType"].ToString();

            //file.FileName处理:如果是IE传回来的是"H:\常用.txt"格式,需要处理为常用.txt;火狐传回的是"常用.txt"
            string fileName = "";
            int startIndex = file.FileName.LastIndexOf(@"\");
            startIndex = startIndex > -1 ? startIndex + 1 : startIndex;
            if (string.IsNullOrEmpty(fileName))
                fileName = startIndex > -1 ? file.FileName.Substring(startIndex) : file.FileName;
            string fileExt = fileName.Substring(fileName.LastIndexOf("."));

            SCAttachment attachment = new SCAttachment();
            attachment.BusinessModuleCode = "SServiceClient";//业务模块代码
            attachment.FileSize = file.ContentLength;// / 1024;
            attachment.IsUse = true;
            attachment.FileType = file.ContentType;
            attachment.FileName = fileName;
            attachment.FileExt = fileExt;

            switch (fileType)
            {
                case "1"://初始化授权
                    ZhiFang.Common.Log.Log.Debug("客户端机构授权文件导入内容(初始化授权):" + tempStr);
                    tempBaseResultDataValue = AddFileOfInitializeOfClient(jresult, file);
                    break;
                case "2"://授权变更
                    ZhiFang.Common.Log.Log.Debug("客户端机构授权文件导入内容(授权变更):" + tempStr);
                    tempBaseResultDataValue = EditCenOrgOfInitializeOfClient(jresult, file, ref attachment, empID, empName);
                    break;
                default:
                    break;
            }
            tempBaseResultDataValue.ResultDataValue = this.GetCenOrgDataValue(tempStr);
            if (tempBaseResultDataValue.success == false) return tempBaseResultDataValue;

            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 机构初始化授权文件导入
        /// </summary>
        /// <returns></returns>
        private BaseResultDataValue AddFileOfInitializeOfClient(JObject jresult, HttpPostedFile file)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //授权机构信息
            SServiceClient client = jresult["SServiceClient"].ToObject<SServiceClient>();
            //授权机构的机构信息
            CenOrg cenOrg = jresult["CenOrg"].ToObject<CenOrg>();
            string fileCName = cenOrg.CName + "初始化授权.json." + FileExt.zf.ToString();

            string subDir = "客户端初始化";
            string basePath = GetFilePath(-1, subDir);

            //先删除多余的或旧的初始化授权文件
            if (Directory.Exists(basePath))
            {
                ZhiFang.Common.Log.Log.Debug("先删除多余的或旧的初始化授权文件,basePath:" + basePath);
                Directory.Delete(basePath, true);
            }

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            string filePathAll = Path.Combine(basePath, fileCName);
            if (File.Exists(filePathAll))
            {
                File.Delete(filePathAll);
            }
            if (file != null)
                file.SaveAs(filePathAll);

            tempBaseResultDataValue.ResultDataValue = GetResultDataValue(cenOrg, null);
            return tempBaseResultDataValue;
        }
        #endregion

        #region 客户端机构初始化
        public BaseResultDataValue GetCenOrgInitializeInfo()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string subDir = "客户端初始化";
            string basePath = GetFilePath(-1, subDir);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            ZhiFang.Common.Log.Log.Debug("初始化授权文件存放目录,basePath:" + basePath);

            System.IO.DirectoryInfo dir = new DirectoryInfo(basePath);
            if (dir.Exists)
            {
                FileInfo[] fiList = dir.GetFiles();
                if (fiList != null && fiList.Length == 1)
                {
                    FileStream fs = new FileStream(fiList[0].FullName, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                    //解密授权文件内容
                    string tempStr = Common.Public.SecurityHelp.MD5Decrypt(sr.ReadToEnd(), Common.Public.SecurityHelp.PWDMD5Key);
                    baseResultDataValue.ResultDataValue = GetCenOrgDataValue(tempStr);

                    sr.Close();
                    fs.Close();
                }
                else
                {
                    //先删除多余的或旧的初始化授权文件
                    if (Directory.Exists(basePath))
                    {
                        ZhiFang.Common.Log.Log.Debug("先删除多余的或旧的初始化授权文件,basePath:" + basePath);
                        Directory.Delete(basePath, true);
                    }
                    if (!Directory.Exists(basePath))
                        Directory.CreateDirectory(basePath);
                    baseResultDataValue.ResultDataValue = GetCenOrgDataValue("");
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "货品系统的初始化文件总数为:" + fiList.Length + ",系统已经自动删除旧的所有初始化授权文件,请重新上传初始化授权文件后再操作!";
                }
            }
            else
            {
                baseResultDataValue.ResultDataValue = GetCenOrgDataValue("");
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "货品系统指定目录下不存在授权机构初始化文件,请上传后再操作!";
            }
            return baseResultDataValue;
        }
        public string GetCenOrgDataValue(string tempStr)
        {
            string labId = "", cenOrgId = "", orgNo = "", orgCName = "";
            //将授权内容转换为对象
            if (!string.IsNullOrEmpty(tempStr))
            {
                JObject jresult = JObject.Parse(tempStr);
                labId = jresult["LabID"].ToString();
                cenOrgId = jresult["CenOrgId"].ToString();
                orgNo = jresult["OrgNo"].ToString();
                orgCName = jresult["OrgCName"].ToString();
            }
            JObject jResultData = new JObject();
            //机构客户所属ID
            jResultData.Add("LabID", labId);
            jResultData.Add("Id", cenOrgId);
            //机构所属机构平台编码
            jResultData.Add("OrgNo", orgNo);
            //机构所属机构
            jResultData.Add("CName", orgCName);
            return jResultData.ToString();
        }
        /// <summary>
        /// 授权机构功能模块信息初始化分步处理
        /// </summary>
        /// <param name="cenOrg">授权机构基本信息</param>
        /// <param name="entity">当前初始化的分步实体</param>
        /// <returns></returns>
        public BaseResultDataValue AddCenOrgInitializeByStep(CenOrg cenOrg, string entity)
        {

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            switch (entity)
            {
                case "RBACModule|SServiceClient|HRDept":
                    //第一部分:模块|机构|部门
                    baseResultDataValue = AddRBACModuleAndCenOrgHRDeptOfInitialize(cenOrg);
                    break;
                case "SServiceClient|HRDept":
                    //第一部分:机构|部门
                    baseResultDataValue = AddSServiceClientAndCenOrgOfInitialize(cenOrg);
                    baseResultDataValue = AddHRDeptOfInitialize(cenOrg);
                    break;
                case "HREmployee|RBACUser|RBACRole":
                    //机构初始化第二部分:人员|员工帐号|角色
                    baseResultDataValue = AddHREmployeeAndRBACUserRBACRoleOfInitialize(cenOrg);
                    break;
                case "RBACEmpRoles|RBACRoleModule":
                    //机构初始化第三部分:员工角色|角色模块
                    baseResultDataValue = AddRBACEmpRolesAndRBACRoleModuleOfInitialize(cenOrg);
                    break;
                case "BParameter|Others":
                    //机构初始化第四部分:系统运行参数|其他
                    baseResultDataValue = AddBParameterAndOthersOfInitialize(cenOrg);
                    break;
                case "RBACModule":
                    baseResultDataValue = EditRBACModuleOfInitialize(cenOrg);
                    break;
                case "SServiceClient":
                    baseResultDataValue = AddSServiceClientAndCenOrgOfInitialize(cenOrg);
                    break;
                case "HRDept":
                    baseResultDataValue = AddHRDeptOfInitialize(cenOrg);
                    break;
                case "HREmployee":
                    baseResultDataValue = AddEmployeeAndUserOfInitialize(cenOrg);
                    break;
                case "RBACRole":
                    baseResultDataValue = AddRBACRoleOfInitialize(cenOrg);
                    break;
                case "RBACEmpRoles":
                    baseResultDataValue = AddRBACEmpRolesOfInitialize(cenOrg);
                    break;
                case "RBACRoleModule":
                    baseResultDataValue = AddRBACRoleModuleOfInitialize(cenOrg);
                    break;
                case "BParameter":
                    baseResultDataValue = AddBParameterOfInitialize(cenOrg);
                    break;
                case "BDictType":
                    baseResultDataValue = AddBDictTypeOfInitialize(cenOrg);
                    break;
                case "ReaCenBarCodeFormat":
                    baseResultDataValue = AddReaCenBarCodeFormatOfInitialize(cenOrg);
                    break;
                case "ReaBmsSerial":
                    BaseResultBool baseResultBool = new BaseResultBool();
                    baseResultBool = AddReaBmsSerialOfLabID(cenOrg.LabID);
                    baseResultDataValue.success = baseResultBool.success;
                    baseResultDataValue.ErrorInfo = baseResultBool.ErrorInfo;
                    break;
                case "MainReaStorage":
                    baseResultDataValue = AddMainReaStorageOfPlatform(cenOrg.LabID);
                    break;
                default:
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "传入参数(entity)不匹配!";
                    break;
            }
            return baseResultDataValue;
        }
        private string GetResultDataValue(CenOrg cenOrg, RBACUser user)
        {
            JObject jResultData = new JObject();
            //机构客户所属ID
            jResultData.Add("LabID", cenOrg.LabID);
            jResultData.Add("Id", cenOrg.Id);
            //机构所属机构平台编码
            jResultData.Add("OrgNo", cenOrg.OrgNo);
            //机构所属机构
            jResultData.Add("CName", cenOrg.CName);
            string account = "", pwd = "";
            if (user != null)
            {
                account = user.Account;
                pwd = Common.Public.SecurityHelp.MD5Decrypt(user.PWD, Common.Public.SecurityHelp.PWDMD5Key); ;
            }
            jResultData.Add("Account", account);
            jResultData.Add("PWD", pwd);
            return jResultData.ToString();
        }
        /// <summary>
        /// 获取客户端机构初始化授权文件
        /// </summary>
        /// <returns></returns>
        private void GetFileOfInitialize(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue, ref JObject jresult)
        {
            string subDir = "客户端初始化";
            string basePath = GetFilePath(-1, subDir);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            string fileCName = cenOrg.CName + "初始化授权.json." + FileExt.zf.ToString();
            string filePathAll = Path.Combine(basePath, fileCName);
            if (!File.Exists(filePathAll))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = GetResultDataValue(cenOrg, null);
                jresult = null;
                return;
            }

            FileStream fs = new FileStream(filePathAll, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            //解密授权文件内容
            string tempStr = Common.Public.SecurityHelp.MD5Decrypt(sr.ReadToEnd(), Common.Public.SecurityHelp.PWDMD5Key);
            //将授权内容转换为对象
            jresult = JObject.Parse(tempStr);

            sr.Close();
            fs.Close();
        }
        /// <summary>
        /// 获取当前授权机构的授权进度信息
        /// </summary>
        /// <param name="cenOrg"></param>
        /// <returns></returns>
        public EntityList<LicenseGuideVO> SearchLicenseGuideVOByCenOrg(CenOrg cenOrg, ref BaseResultDataValue baseResultDataValue)
        {
            EntityList<LicenseGuideVO> voList = new EntityList<LicenseGuideVO>();
            voList.list = new List<LicenseGuideVO>();

            LicenseGuideVO moduleVO = new LicenseGuideVO();
            moduleVO.Id = "card-module";
            moduleVO.CName = "功能模块信息初始化";
            moduleVO.DispOrder = 1;

            LicenseGuideVO cenOrgVO = new LicenseGuideVO();
            cenOrgVO.Id = "card-cenOrg";
            cenOrgVO.CName = "机构信息初始化";
            cenOrgVO.DispOrder = 2;

            LicenseGuideVO deptVO = new LicenseGuideVO();
            deptVO.Id = "card-dept";
            deptVO.CName = "机构管理部门初始化";
            deptVO.DispOrder = 3;

            LicenseGuideVO employeeVO = new LicenseGuideVO();
            employeeVO.Id = "card-employee";
            employeeVO.CName = "系统管理员初始化";
            employeeVO.DispOrder = 4;

            LicenseGuideVO userVO = new LicenseGuideVO();
            userVO.Id = "card-user";
            userVO.CName = "管理帐号初始化";
            userVO.DispOrder = 5;

            LicenseGuideVO roleVO = new LicenseGuideVO();
            roleVO.Id = "card-role";
            roleVO.CName = "管理角色信息初始化";
            roleVO.DispOrder = 6;

            LicenseGuideVO empRolesVO = new LicenseGuideVO();
            empRolesVO.Id = "card-empRoles";
            empRolesVO.CName = "管理员角色权限初始化";
            empRolesVO.DispOrder = 7;

            LicenseGuideVO rolemoduleVO = new LicenseGuideVO();
            rolemoduleVO.Id = "card-rolemodule";
            rolemoduleVO.CName = "角色模块权限初始化";
            rolemoduleVO.DispOrder = 8;

            LicenseGuideVO bparameterVO = new LicenseGuideVO();
            bparameterVO.Id = "card-bparameter";
            bparameterVO.CName = "系统运行参数初始化";
            bparameterVO.DispOrder = 9;

            LicenseGuideVO barcodeformatVO = new LicenseGuideVO();
            barcodeformatVO.Id = "card-barcodeformat";
            barcodeformatVO.CName = "系统条码规则信息初始化";
            barcodeformatVO.DispOrder = 10;

            LicenseGuideVO bdictTypeVO = new LicenseGuideVO();
            bdictTypeVO.Id = "card-bdictType";
            bdictTypeVO.CName = "机构字典类型初始化";
            barcodeformatVO.DispOrder = 11;

            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return voList;
            if (jresult == null) return voList;

            GetModuleVOSataus(cenOrg, jresult, ref moduleVO);
            GetCenOrgVOSataus(cenOrg, jresult, ref cenOrgVO);
            GetDeptVOSataus(cenOrg, jresult, ref deptVO);
            GetEmployeeVOSataus(cenOrg, jresult, ref employeeVO);
            GetUserVOSataus(cenOrg, jresult, ref userVO);
            GetRoleVOSataus(cenOrg, jresult, ref roleVO);

            voList.list.Add(moduleVO);
            voList.list.Add(cenOrgVO);
            voList.list.Add(deptVO);
            voList.list.Add(employeeVO);
            voList.list.Add(userVO);
            voList.list.Add(roleVO);

            GetEmpRolesVOSataus(cenOrg, jresult, ref empRolesVO);
            GetRolemoduleVOSataus(cenOrg, jresult, ref rolemoduleVO);
            GetBParameterVOSataus(cenOrg, jresult, ref bparameterVO);
            GetBarcodeformatVOSataus(cenOrg, jresult, ref barcodeformatVO);
            GetBDictTypeVOSataus(cenOrg, jresult, ref bdictTypeVO);

            voList.list.Add(empRolesVO);
            voList.list.Add(rolemoduleVO);
            voList.list.Add(bparameterVO);
            voList.list.Add(barcodeformatVO);
            voList.list.Add(bdictTypeVO);
            voList.count = voList.list.Count();

            return voList;
        }
        private void GetCenOrgVOSataus(CenOrg cenOrg, JObject jresult, ref LicenseGuideVO vo)
        {
            SServiceClient client = IBSServiceClient.Get(cenOrg.LabID);
            CenOrg tempCenOrg = IDAO.RBAC.DataAccess_SQL.CreateCenOrgDao_SQL().Get(cenOrg.Id);
            if (client == null || tempCenOrg == null)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
            }
            else if (client != null && tempCenOrg != null)
            {
                vo.Sataus = 2;
                vo.SatausName = "初始化成功";
            }
            else if (client != null && tempCenOrg == null)
            {
                vo.Sataus = 3;
                vo.SatausName = "初始化失败";
                vo.Memo = "SServiceClient初始化成功,CenOrg初始化失败";
            }
            else if (client == null && tempCenOrg != null)
            {
                vo.Sataus = 3;
                vo.SatausName = "初始化失败";
                vo.Memo = "CenOrg初始化成功,SServiceClient初始化失败";
            }
        }
        private void GetModuleVOSataus(CenOrg cenOrg, JObject jresult, ref LicenseGuideVO vo)
        {
            var moduleList = jresult.SelectToken("RBACModuleList").ToList();
            if (moduleList == null || moduleList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
                vo.Memo = "系统模块信息为空!";
                return;
            }
            IList<RBACModule> allList = IBRBACModule.LoadAll();
            if (allList == null) allList = new List<RBACModule>();

            var tempList = from m in moduleList
                           join a in allList on m.ToObject<RBACModule>().Id equals a.Id
                           select m.ToObject<RBACModule>();
            if (tempList == null || tempList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
            }
            else if (tempList.Count() == moduleList.Count())
            {
                vo.Sataus = 2;
                vo.SatausName = "初始化成功";
            }
            else
            {
                vo.Sataus = 3;
                vo.SatausName = "初始化失败";
            }
        }
        private void GetDeptVOSataus(CenOrg cenOrg, JObject jresult, ref LicenseGuideVO vo)
        {
            var deptList = jresult.SelectToken("HRDeptList").ToList();
            if (deptList == null || deptList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
                vo.Memo = "机构的部门信息为空!";
                return;
            }
            IList<HRDept> allList = IDAO.RBAC.DataAccess_SQL.CreateHRDeptDao_SQL().GetListByHQL("LabID=" + cenOrg.LabID);//IBHRDept.SearchListByHQL("hrdept.LabID=" + cenOrg.LabID);
            if (allList == null) allList = new List<HRDept>();

            var tempList = from m in deptList
                           join a in allList on m.ToObject<HRDept>().Id equals a.Id
                           select m.ToObject<HRDept>();
            if (tempList == null || tempList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
            }
            else if (tempList.Count() == deptList.Count())
            {
                vo.Sataus = 2;
                vo.SatausName = "初始化成功";
            }
            else
            {
                vo.Sataus = 3;
                vo.SatausName = "初始化失败";
            }
        }
        private void GetEmployeeVOSataus(CenOrg cenOrg, JObject jresult, ref LicenseGuideVO vo)
        {
            var employeeList = jresult.SelectToken("HREmployeeList").ToList();
            if (employeeList == null || employeeList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
                vo.Memo = "用户信息为空!";
                return;
            }
            IList<HREmployee> allList = IDAO.RBAC.DataAccess_SQL.CreateHREmployeeDao_SQL().GetListByHQL("LabID=" + cenOrg.LabID);// IBHREmployee.SearchListByHQL("hremployee.LabID=" + cenOrg.LabID);
            if (allList == null) allList = new List<HREmployee>();

            var tempList = from m in employeeList
                           join a in allList on m.ToObject<HREmployee>().Id equals a.Id
                           select m.ToObject<HREmployee>();
            if (tempList == null || tempList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
            }
            else if (tempList.Count() == employeeList.Count())
            {
                vo.Sataus = 2;
                vo.SatausName = "初始化成功";
            }
            else
            {
                vo.Sataus = 3;
                vo.SatausName = "初始化失败";
            }
        }
        private void GetUserVOSataus(CenOrg cenOrg, JObject jresult, ref LicenseGuideVO vo)
        {
            //var employeeList = jresult.SelectToken("HREmployeeList").ToList();
            var userList = jresult.SelectToken("RBACUserList").ToList();

            if (userList == null || userList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
                vo.Memo = "用户信息为空!";
                return;
            }
            IList<RBACUser> allList = IDAO.RBAC.DataAccess_SQL.CreateRBACUserDao_SQL().GetListByHQL("LabID=" + cenOrg.LabID);//IBRBACUser.SearchListByHQL("rbacuser.LabID=" + cenOrg.LabID);
            if (allList == null) allList = new List<RBACUser>();

            var tempList = from m in userList
                           join a in allList on m.ToObject<RBACUser>().Id equals a.Id
                           select m.ToObject<RBACUser>();
            if (tempList == null || tempList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
            }
            else if (tempList.Count() == userList.Count())
            {
                vo.Sataus = 2;
                vo.SatausName = "初始化成功";
            }
            else
            {
                vo.Sataus = 3;
                vo.SatausName = "初始化失败";
            }
        }
        private void GetRoleVOSataus(CenOrg cenOrg, JObject jresult, ref LicenseGuideVO vo)
        {
            var roleList = jresult.SelectToken("RBACRoleList").ToList();
            if (roleList == null || roleList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
                vo.Memo = "角色信息为空!";
                return;
            }
            IList<RBACRole> allList = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleDao_SQL().GetListByHQL("LabID=" + cenOrg.LabID);// IBRBACRole.SearchListByHQL("rbacrole.LabID=" + cenOrg.LabID);
            if (allList == null) allList = new List<RBACRole>();

            var tempList = from m in roleList
                           join a in allList on m.ToObject<RBACRole>().Id equals a.Id
                           select m.ToObject<RBACRole>();
            if (tempList == null || tempList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
            }
            else if (tempList.Count() == roleList.Count())
            {
                vo.Sataus = 2;
                vo.SatausName = "初始化成功";
            }
            else
            {
                vo.Sataus = 3;
                vo.SatausName = "初始化失败";
            }
        }
        private void GetEmpRolesVOSataus(CenOrg cenOrg, JObject jresult, ref LicenseGuideVO vo)
        {
            var empRolesList = jresult.SelectToken("RBACEmpRolesList").ToList();

            if (empRolesList == null || empRolesList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
                vo.Memo = "员工角色信息为空!";
                return;
            }
            IList<RBACEmpRoles> allList = IDAO.RBAC.DataAccess_SQL.CreateRBACEmpRolesDao_SQL().GetListByHQL("LabID=" + cenOrg.LabID);// IBRBACEmpRoles.SearchListByHQL("rbacemproles.LabID=" + cenOrg.LabID);
            if (allList == null) allList = new List<RBACEmpRoles>();

            var tempList = from m in empRolesList
                           join a in allList on m.ToObject<RBACEmpRoles>().Id equals a.Id
                           select m.ToObject<RBACEmpRoles>();
            if (tempList == null || tempList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
            }
            else if (tempList.Count() == empRolesList.Count())
            {
                vo.Sataus = 2;
                vo.SatausName = "初始化成功";
            }
            else
            {
                vo.Sataus = 3;
                vo.SatausName = "初始化失败";
            }
        }
        private void GetRolemoduleVOSataus(CenOrg cenOrg, JObject jresult, ref LicenseGuideVO vo)
        {
            var roleModuleList = jresult.SelectToken("RBACRoleModuleList").ToList();
            //ZhiFang.Common.Log.Log.Debug("RBACRoleModuleList:" + jresult.SelectToken("RBACRoleModuleList").ToString());
            if (roleModuleList == null || roleModuleList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
                vo.Memo = "角色模块信息为空!";
                return;
            }
            IList<RBACRoleModule> allList = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleModuleDao_SQL().GetListByHQL("LabID=" + cenOrg.LabID);// IBRBACRoleModule.SearchListByHQL("rbacrolemodule.LabID=" + cenOrg.LabID);
            if (allList == null) allList = new List<RBACRoleModule>();

            //var templist = from m in rolemodulelist
            //               join a in alllist on m.ToObject<rbacrolemodule>().id equals a.id
            //               select m.ToObject<rbacrolemodule>();

            IList<RBACRoleModule> noAddList = new List<RBACRoleModule>();
            foreach (var item in roleModuleList)
            {
                //ZhiFang.Common.Log.Log.Debug("RBACRoleModule.Id:" + item.SelectToken("Id").ToString());
                RBACRoleModule roleModule = item.ToObject<RBACRoleModule>();
                var tempList = allList.Where(p => p.Id == roleModule.Id);
                if (tempList == null || tempList.Count() <= 0)
                {
                    noAddList.Add(roleModule);
                }
            }
            if (noAddList == null || noAddList.Count() <= 0)
            {
                vo.Sataus = 2;
                vo.SatausName = "初始化成功";
            }
            else if (noAddList.Count() == roleModuleList.Count())
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
            }
            else
            {
                vo.Sataus = 3;
                vo.SatausName = "初始化失败";
            }
        }
        private void GetBParameterVOSataus(CenOrg cenOrg, JObject jresult, ref LicenseGuideVO vo)
        {
            var bparameterList = jresult.SelectToken("BParameterList").ToList();
            if (bparameterList == null || bparameterList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
                vo.Memo = "系统运行参数信息为空!";
                return;
            }
            IList<BParameter> allList = IDAO.RBAC.DataAccess_SQL.CreateBParameterDao_SQL().GetListByHQL("LabID=" + cenOrg.LabID);//  IBBParameter.SearchListByHQL("bparameter.LabID=" + cenOrg.LabID);
            if (allList == null) allList = new List<BParameter>();

            var tempList = from m in bparameterList
                           join a in allList on m.ToObject<BParameter>().Id equals a.Id
                           select m.ToObject<BParameter>();
            if (tempList == null || tempList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
            }
            else if (tempList.Count() == bparameterList.Count())
            {
                vo.Sataus = 2;
                vo.SatausName = "初始化成功";
            }
            else
            {
                vo.Sataus = 3;
                vo.SatausName = "初始化失败";
            }
        }
        private void GetBarcodeformatVOSataus(CenOrg cenOrg, JObject jresult, ref LicenseGuideVO vo)
        {
            var jsonList = jresult.SelectToken("ReaCenBarCodeFormatList").ToList();
            if (jsonList == null || jsonList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
                vo.Memo = "系统条码规则信息为空!";
                return;
            }
            IList<ReaCenBarCodeFormat> allList = IDAO.RBAC.DataAccess_SQL.CreateReaCenBarCodeFormatDao_SQL().GetListByHQL("");// "LabID=" + cenOrg.LabIDIBReaCenBarCodeFormat.SearchListByHQL("reacenbarcodeformat.LabID=" + cenOrg.LabID);
            if (allList == null) allList = new List<ReaCenBarCodeFormat>();

            var tempList = from m in jsonList
                           join a in allList on m.ToObject<ReaCenBarCodeFormat>().Id equals a.Id
                           select m.ToObject<ReaCenBarCodeFormat>();
            if (tempList == null || tempList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
            }
            else if (tempList.Count() == jsonList.Count())
            {
                vo.Sataus = 2;
                vo.SatausName = "初始化成功";
            }
            else
            {
                vo.Sataus = 3;
                vo.SatausName = "初始化失败";
            }
        }
        private void GetBDictTypeVOSataus(CenOrg cenOrg, JObject jresult, ref LicenseGuideVO vo)
        {
            var jsonList = jresult.SelectToken("BDictTypeList").ToList();
            if (jsonList == null || jsonList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
                vo.Memo = "系统字典类型信息为空!";
                return;
            }
            IList<BDictType> allList = IDAO.RBAC.DataAccess_SQL.CreateBDictTypeDao_SQL().GetListByHQL("LabID=" + cenOrg.LabID);//IBBDictType.SearchListByHQL("bdicttype.LabID=" + cenOrg.LabID);
            if (allList == null) allList = new List<BDictType>();

            var tempList = from m in jsonList
                           join a in allList on m.ToObject<BDictType>().Id equals a.Id
                           select m.ToObject<BDictType>();
            if (tempList == null || tempList.Count() <= 0)
            {
                vo.Sataus = 1;
                vo.SatausName = "初始化未完成";
            }
            else if (tempList.Count() == jsonList.Count())
            {
                vo.Sataus = 2;
                vo.SatausName = "初始化成功";
            }
            else
            {
                vo.Sataus = 3;
                vo.SatausName = "初始化失败";
            }
        }
        /// <summary>
        /// 机构初始化第一部分:模块|机构|部门
        /// </summary>
        /// <param name="cenOrg"></param>
        /// <returns></returns>
        public BaseResultDataValue AddRBACModuleAndCenOrgHRDeptOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            //先处理授权机构的新增模块信息
            var moduleList = jresult.SelectToken("RBACModuleList").ToList();
            EditRBACModuleList(moduleList, ref baseResultDataValue);
            if (!baseResultDataValue.success) return baseResultDataValue;

            //授权机构信息
            baseResultDataValue = AddSServiceClientAndCenOrgOfInitialize(cenOrg);
            if (!baseResultDataValue.success) return baseResultDataValue;

            //授权部门信息
            SServiceClient client = jresult["SServiceClient"].ToObject<SServiceClient>();
            CenOrg saveCenOrg = jresult["CenOrg"].ToObject<CenOrg>();
            var deptList = jresult.SelectToken("HRDeptList").ToList();
            if (deptList == null || deptList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的管理部门信息为空!";
                return baseResultDataValue;
            }
            AddHRDeptList(client, saveCenOrg, deptList, ref baseResultDataValue);
            if (!baseResultDataValue.success) return baseResultDataValue;

            return baseResultDataValue;

        }
        /// <summary>
        /// 机构初始化第二部分:人员|员工帐号|角色
        /// </summary>
        /// <param name="cenOrg"></param>
        /// <returns></returns>
        public BaseResultDataValue AddHREmployeeAndRBACUserRBACRoleOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            //获取授权文件的机构信息(机构所属ID,所属机构平台编码等)
            long labID = long.Parse(jresult["LabID"].ToString());
            long cenOrgId = long.Parse(jresult["CenOrgId"].ToString());
            string orgNo = jresult["OrgNo"].ToString();

            SServiceClient client = IBSServiceClient.Get(labID);// IDAO.RBAC.DataAccess_SQL.CreateSServiceClientDao_SQL().Get(labID);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "(LabID)为:" + labID + "授权机构未初始化!";
                return baseResultDataValue;
            }
            CenOrg tempCenOrg = IDAO.RBAC.DataAccess_SQL.CreateCenOrgDao_SQL().Get(cenOrgId);
            if (tempCenOrg == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "(CenOrgId)为:" + cenOrgId + "授权机构未初始化!";
                return baseResultDataValue;
            }

            var employeeList = jresult.SelectToken("HREmployeeList").ToList();
            var userList = jresult.SelectToken("RBACUserList").ToList();
            if (employeeList == null || employeeList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的部门员工信息为空!";
                return baseResultDataValue;
            }
            if (userList == null || userList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的员工帐号信息为空!";
                return baseResultDataValue;
            }
            AddHREmployeeList(client, employeeList, ref baseResultDataValue);
            if (baseResultDataValue.success == false) return baseResultDataValue;

            AddRBACUserList(client, userList, ref baseResultDataValue);
            if (baseResultDataValue.success == false) return baseResultDataValue;

            //角色信息处理
            var roleList = jresult.SelectToken("RBACRoleList").ToList();
            if (roleList == null || roleList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的角色信息为空!";
                return baseResultDataValue;
            }
            AddRBACRoleList(client, roleList, ref baseResultDataValue);

            if (baseResultDataValue.success)
            {
                var tempList = userList.Where(p => p.ToObject<RBACUser>().HREmployee.DeveCode == "sys_admin");
                if (tempList.Count() >= 1)
                {
                    baseResultDataValue.ResultDataValue = GetResultDataValue(tempCenOrg, tempList.ElementAt(0).ToObject<RBACUser>());
                }
            }

            return baseResultDataValue;

        }
        /// <summary>
        /// 机构初始化第三部分:员工角色|角色模块
        /// </summary>
        /// <param name="cenOrg"></param>
        /// <returns></returns>
        public BaseResultDataValue AddRBACEmpRolesAndRBACRoleModuleOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            //获取授权文件的机构信息(机构所属ID,所属机构平台编码等)
            long labID = long.Parse(jresult["LabID"].ToString());
            long cenOrgId = long.Parse(jresult["CenOrgId"].ToString());
            string orgNo = jresult["OrgNo"].ToString();

            SServiceClient client = IBSServiceClient.Get(labID);// IDAO.RBAC.DataAccess_SQL.CreateSServiceClientDao_SQL().Get(labID);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "(LabID)为:" + labID + "授权机构未初始化!";
                return baseResultDataValue;
            }
            CenOrg tempCenOrg = IDAO.RBAC.DataAccess_SQL.CreateCenOrgDao_SQL().Get(cenOrgId);
            if (tempCenOrg == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "(CenOrgId)为:" + cenOrgId + "授权机构未初始化!";
                return baseResultDataValue;
            }

            var empRolesList = jresult.SelectToken("RBACEmpRolesList").ToList();
            if (empRolesList == null || empRolesList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的员工角色信息为空!";
                return baseResultDataValue;
            }
            AddRBACEmpRolesList(client, empRolesList, ref baseResultDataValue);
            if (baseResultDataValue.success == false) return baseResultDataValue;

            var roleModuleList = jresult.SelectToken("RBACRoleModuleList").ToList();
            if (roleModuleList == null || roleModuleList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的角色模块信息为空!";
                return baseResultDataValue;
            }
            AddRBACRoleModulesList(client, roleModuleList, ref baseResultDataValue);

            return baseResultDataValue;
        }
        /// <summary>
        /// 机构初始化第四部分:系统运行参数|其他
        /// </summary>
        /// <param name="cenOrg"></param>
        /// <returns></returns>
        public BaseResultDataValue AddBParameterAndOthersOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            //获取授权文件的机构信息(机构所属ID,所属机构平台编码等)
            long labID = long.Parse(jresult["LabID"].ToString());
            long cenOrgId = long.Parse(jresult["CenOrgId"].ToString());
            string orgNo = jresult["OrgNo"].ToString();

            SServiceClient client = IBSServiceClient.Get(labID);// IDAO.RBAC.DataAccess_SQL.CreateSServiceClientDao_SQL().Get(labID);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "(LabID)为:" + labID + "授权机构未初始化!";
                return baseResultDataValue;
            }
            CenOrg tempCenOrg = IDAO.RBAC.DataAccess_SQL.CreateCenOrgDao_SQL().Get(cenOrgId);
            if (tempCenOrg == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "(CenOrgId)为:" + cenOrgId + "授权机构未初始化!";
                return baseResultDataValue;
            }

            var bparameterList = jresult.SelectToken("BParameterList").ToList();
            AddBParameterList(client, bparameterList, ref baseResultDataValue);
            if (baseResultDataValue.success == false) return baseResultDataValue;

            var barCodeFormatList = jresult.SelectToken("ReaCenBarCodeFormatList").ToList();
            AddReaCenBarCodeFormatList(client, barCodeFormatList, ref baseResultDataValue);

            var dictTypeList = jresult.SelectToken("BDictTypeList").ToList();
            AddBDictTypeList(client, dictTypeList, ref baseResultDataValue);
            if (baseResultDataValue.success == false) return baseResultDataValue;

            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool = AddReaBmsSerialOfLabID(cenOrg.LabID);
            baseResultDataValue.success = baseResultBool.success;
            baseResultDataValue.ErrorInfo = baseResultBool.ErrorInfo;

            //添加机构的总库房信息
            baseResultDataValue = AddMainReaStorageOfPlatform(client.LabID);

            return baseResultDataValue;
        }
        /// <summary>
        /// 功能模块信息初始化处理
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue EditRBACModuleOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            var moduleList = jresult.SelectToken("RBACModuleList").ToList();
            //先处理授权机构的新增模块信息
            EditRBACModuleList(moduleList, ref baseResultDataValue);

            return baseResultDataValue;
        }
        private void EditRBACModuleList(List<JToken> moduleList, ref BaseResultDataValue baseResultDataValue)
        {
            if (moduleList == null || moduleList.Count <= 0) return;
            IList<RBACModule> allList = IDAO.RBAC.DataAccess_SQL.CreateRBACModuleDao_SQL().ObtainLoadAll(); //IDRBACModuleDao.GetListByHQL("");//IBRBACModule.LoadAll();
            if (allList == null) allList = new List<RBACModule>();

            try
            {
                IList<long> addIDList = new List<long>();

                foreach (JToken jtoken in moduleList)
                {
                    RBACModule model = jtoken.ToObject<RBACModule>();
                    if (model == null) continue;

                    model.DataTimeStamp = null;
                    if (baseResultDataValue.success == false)
                    {
                        baseResultDataValue.ErrorInfo = "授权机构新增模块信息失败!";
                        break;
                    }
                    var tempModule = allList.Where(p => p.Id == model.Id);
                    //ZhiFang.Common.Log.Log.Debug(model.Id.ToString());
                    if (tempModule == null || tempModule.Count() <= 0)
                    {
                        if (!addIDList.Contains(model.Id))
                        {
                            ZhiFang.Common.Log.Log.Debug("Id=" + model.Id + " and ParentID=" + model.ParentID);
                            model.BTDAppComponents = null;
                            model.RBACModuleOperList = null;
                            model.RBACRoleModuleList = null;
                            IBRBACModule.Entity = model;
                            baseResultDataValue.success = IBRBACModule.Add();
                            addIDList.Add(model.Id);
                        }
                    }
                    else if (tempModule.Count() == 1)
                    {
                        RBACModule editModel = tempModule.ElementAt(0);
                        //客户端的模块入口与平台的模块入口不一到时
                        bool isEdit = false;
                        if (!string.IsNullOrEmpty(model.Url) && model.Url != editModel.Url)
                        {
                            isEdit = true;
                        }
                        if (isEdit == false && model.PicFile != editModel.PicFile)
                        {
                            isEdit = true;
                        }
                        if (isEdit == false && model.ParentID != editModel.ParentID)
                        {
                            isEdit = true;
                        }
                        if (isEdit == false && model.CName != editModel.CName)
                        {
                            isEdit = true;
                        }
                        if (isEdit == false && model.ModuleType != editModel.ModuleType)
                        {
                            isEdit = true;
                        }
                        if (isEdit == false && model.IsUse != editModel.IsUse)
                        {
                            isEdit = true;
                        }
                        if (isEdit == true)
                        {
                            editModel.Url = model.Url;
                            editModel.ModuleType = model.ModuleType;
                            editModel.ParentID = model.ParentID;
                            editModel.IsLeaf = model.IsLeaf;
                            editModel.PicFile = model.PicFile;
                            editModel.Para = model.Para;
                            editModel.CName = model.CName;
                            editModel.PinYinZiTou = model.PinYinZiTou;
                            editModel.IsUse = model.IsUse;
                            editModel.DispOrder = model.DispOrder;
                            editModel.Comment = model.Comment;
                            IBRBACModule.Entity = editModel;
                            IBRBACModule.Edit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("授权导入处理系统模块失败，错误信息为：" + ex.StackTrace);
            }
        }
        /// <summary>
        /// 机构信息初始化处理
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddSServiceClientAndCenOrgOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            if (string.IsNullOrEmpty(jresult["LabID"].ToString()))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的LabID为空!";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(jresult["OrgNo"].ToString()))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的OrgNo为空!";
                return baseResultDataValue;
            }
            //获取授权文件的机构信息(机构所属ID,所属机构平台编码等)
            long labID = long.Parse(jresult["LabID"].ToString());
            long cenOrgId = long.Parse(jresult["CenOrgId"].ToString());
            string orgNo = jresult["OrgNo"].ToString();
            string orgCName = jresult["OrgCName"].ToString();

            SServiceClient tempSServiceClient = IDAO.RBAC.DataAccess_SQL.CreateSServiceClientDao_SQL().ObtainById(labID);// IBSServiceClient.Get(labID);// 
            if (tempSServiceClient != null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "授权机构ID(LabID)为:" + labID + ",授权机构名称为:" + orgCName + ",已存在!";
                return baseResultDataValue;
            }
            CenOrg tempCenOrg = IDAO.RBAC.DataAccess_SQL.CreateCenOrgDao_SQL().ObtainById(cenOrgId);
            if (tempCenOrg != null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "授权机构ID(CenOrgId)为:" + cenOrgId + ",授权机构名称为:" + orgCName + ",已存在!";
                return baseResultDataValue;
            }
            //IList<CenOrg> tempCenOrgList = IDAO.RBAC.DataAccess_SQL.CreateCenOrgDao_SQL().GetListByHQL(" OrgNo=" + orgNo + "");
            //if (tempCenOrgList.Count > 0)
            //{
            //    baseResultDataValue.success = false;
            //    baseResultDataValue.ErrorInfo = "授权机构编码(OrgNo)为:" + orgNo + ",授权机构名称为:" + orgCName + ",已存在!";
            //    return baseResultDataValue;
            //}

            //授权机构信息
            SServiceClient client = jresult["SServiceClient"].ToObject<SServiceClient>();
            //授权机构的机构信息
            CenOrg saveCenOrg = jresult["CenOrg"].ToObject<CenOrg>();

            //client.LabID = client.LabID;
            //client.Id = client.LabID;
            client.DataAddTime = DateTime.Now;
            IBSServiceClient.Entity = client;
            baseResultDataValue.success = IBSServiceClient.Add();
            if (baseResultDataValue.success == false)
            {
                baseResultDataValue.ErrorInfo = "新增授权机构信息(SServiceClient)失败!";
                return baseResultDataValue;
            }

            //saveCenOrg.DataTimeStamp = null;
            saveCenOrg.DataAddTime = DateTime.Now;
            saveCenOrg.DataUpdateTime = DateTime.Now;
            saveCenOrg.LabID = client.LabID;
            this.Entity = saveCenOrg;
            baseResultDataValue.success = this.Add();
            if (baseResultDataValue.success == false)
            {
                baseResultDataValue.ErrorInfo = "新增授权机构信息(CenOrg)失败!";
                return baseResultDataValue;
            }

            return baseResultDataValue;
        }
        /// <summary>
        /// 授权机构管理部门初始化处理
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddHRDeptOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            var deptList = jresult.SelectToken("HRDeptList").ToList();
            if (deptList == null || deptList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的管理部门信息为空!";
                return baseResultDataValue;
            }
            //授权机构信息
            SServiceClient client = IBSServiceClient.Get(cenOrg.LabID);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权机构信息(SServiceClient)为空!";
                return baseResultDataValue;
            }
            //授权机构的机构信息
            CenOrg cenOrg2 = this.Get(cenOrg.Id);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权机构信息(CenOrg)为空!";
                return baseResultDataValue;
            }
            AddHRDeptList(client, cenOrg, deptList, ref baseResultDataValue);

            return baseResultDataValue;
        }
        private void AddHRDeptList(SServiceClient client, CenOrg cenOrg, List<JToken> deptList, ref BaseResultDataValue baseResultDataValue)
        {
            if (deptList == null || deptList.Count <= 0) return;

            IList<HRDept> allList = IDAO.RBAC.DataAccess_SQL.CreateHRDeptDao_SQL().GetListByHQL("LabID=" + cenOrg.LabID);// IBHRDept.SearchListByHQL("hrdept.LabID=" + client.LabID);
            foreach (JToken jtoken in deptList)
            {
                HRDept model = jtoken.ToObject<HRDept>();
                if (model == null) continue;

                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "新增部门信息失败!";
                    break;
                }
                if (allList != null)
                {
                    var tempList = allList.Where(p => p.Id == model.Id);
                    if (tempList != null && tempList.Count() > 0) continue;
                }

                model.DataTimeStamp = null;
                model.DataAddTime = DateTime.Now;
                model.DataUpdateTime = DateTime.Now;
                model.LabID = client.LabID;
                IBHRDept.Entity = model;
                baseResultDataValue.success = IBHRDept.Add();
            }
        }
        /// <summary>
        /// 授权机构管理员及帐号初始化处理
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddEmployeeAndUserOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            var employeeList = jresult.SelectToken("HREmployeeList").ToList();
            var userList = jresult.SelectToken("RBACUserList").ToList();
            if (employeeList == null || employeeList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的部门员工信息为空!";
                return baseResultDataValue;
            }
            if (userList == null || userList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的员工帐号信息为空!";
                return baseResultDataValue;
            }
            //授权机构信息
            SServiceClient client = IBSServiceClient.Get(cenOrg.LabID);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权机构信息(SServiceClient)为空!";
                return baseResultDataValue;
            }
            //授权机构的机构信息
            CenOrg cenOrg2 = this.Get(cenOrg.Id);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权机构信息(CenOrg)为空!";
                return baseResultDataValue;
            }

            AddHREmployeeList(client, employeeList, ref baseResultDataValue);
            if (baseResultDataValue.success == false) return baseResultDataValue;

            AddRBACUserList(client, userList, ref baseResultDataValue);
            if (baseResultDataValue.success == false) return baseResultDataValue;

            return baseResultDataValue;
        }
        private void AddHREmployeeList(SServiceClient client, List<JToken> employeeList, ref BaseResultDataValue baseResultDataValue)
        {
            if (employeeList == null || employeeList.Count <= 0) return;

            IList<HREmployee> allList = IDAO.RBAC.DataAccess_SQL.CreateHREmployeeDao_SQL().GetListByHQL("LabID=" + client.LabID);// IBHREmployee.SearchListByHQL("hremployee.LabID=" + client.LabID);
            foreach (JToken jtoken in employeeList)
            {
                HREmployee model = jtoken.ToObject<HREmployee>();
                if (model == null) continue;

                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "新增部门员工信息失败!";
                    break;
                }
                if (allList != null)
                {
                    var tempList = allList.Where(p => p.Id == model.Id);
                    if (tempList != null && tempList.Count() > 0) continue;
                }
                if (model.HRDept != null)
                {
                    if (IDAO.RBAC.DataAccess_SQL.CreateHRDeptDao_SQL().Get(model.HRDept.Id) == null)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "新增员工信息时,员工的所属部门信息未保存!";
                        break;
                    }
                }
                model.DataAddTime = DateTime.Now;
                model.DataUpdateTime = DateTime.Now;
                model.DataTimeStamp = null;
                model.LabID = client.LabID;
                IBHREmployee.Entity = model;
                baseResultDataValue.success = IBHREmployee.Add();
            }
        }
        private void AddRBACUserList(SServiceClient client, List<JToken> userList, ref BaseResultDataValue baseResultDataValue)
        {
            if (userList == null || userList.Count <= 0) return;

            IList<RBACUser> allList = IDAO.RBAC.DataAccess_SQL.CreateRBACUserDao_SQL().GetListByHQL("LabID=" + client.LabID); // IBRBACUser.SearchListByHQL("rbacuser.LabID=" + client.LabID);
            foreach (JToken jtoken in userList)
            {
                RBACUser model = jtoken.ToObject<RBACUser>();
                if (model == null) continue;

                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "新增员工帐号信息失败!";
                    break;
                }
                if (allList != null)
                {
                    var tempList = allList.Where(p => p.Id == model.Id);
                    if (tempList != null && tempList.Count() > 0) continue;
                }
                model.DataAddTime = DateTime.Now;
                model.DataUpdateTime = DateTime.Now;
                model.DataTimeStamp = null;
                model.LabID = client.LabID;
                IBRBACUser.Entity = model;
                baseResultDataValue.success = IBRBACUser.Add();
            }
        }
        /// <summary>
        /// 授权机构角色信息初始化处理
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddRBACRoleOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            var roleList = jresult.SelectToken("RBACRoleList").ToList();
            if (roleList == null || roleList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的角色信息为空!";
                return baseResultDataValue;
            }
            //授权机构信息
            SServiceClient client = IBSServiceClient.Get(cenOrg.LabID);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权机构信息(SServiceClient)为空!";
                return baseResultDataValue;
            }
            AddRBACRoleList(client, roleList, ref baseResultDataValue);

            return baseResultDataValue;
        }
        private void AddRBACRoleList(SServiceClient client, List<JToken> roleList, ref BaseResultDataValue baseResultDataValue)
        {
            if (roleList == null || roleList.Count <= 0) return;

            IList<RBACRole> allList = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleDao_SQL().GetListByHQL("LabID=" + client.LabID);//IBRBACRole.SearchListByHQL("rbacrole.LabID=" + client.LabID);
            foreach (JToken jtoken in roleList)
            {
                RBACRole model = jtoken.ToObject<RBACRole>();
                if (model == null) continue;

                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "新增角色信息失败!";
                    break;
                }
                if (allList != null)
                {
                    var tempList = allList.Where(p => p.Id == model.Id);
                    if (tempList != null && tempList.Count() > 0) continue;
                }

                model.DataUpdateTime = DateTime.Now;
                model.DataTimeStamp = null;
                model.LabID = client.LabID;
                IBRBACRole.Entity = model;
                baseResultDataValue.success = IBRBACRole.Add();
            }
        }
        /// <summary>
        /// 授权机构员工角色信息初始化处理
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddRBACEmpRolesOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            var empRolesList = jresult.SelectToken("RBACEmpRolesList").ToList();
            if (empRolesList == null || empRolesList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的员工角色信息为空!";
                return baseResultDataValue;
            }
            //授权机构信息
            SServiceClient client = IBSServiceClient.Get(cenOrg.LabID);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权机构信息(SServiceClient)为空!";
                return baseResultDataValue;
            }
            AddRBACEmpRolesList(client, empRolesList, ref baseResultDataValue);

            return baseResultDataValue;
        }
        private void AddRBACEmpRolesList(SServiceClient client, List<JToken> empRolesList, ref BaseResultDataValue baseResultDataValue)
        {
            if (empRolesList == null || empRolesList.Count <= 0) return;

            IList<RBACEmpRoles> allList = IDAO.RBAC.DataAccess_SQL.CreateRBACEmpRolesDao_SQL().GetListByHQL("LabID=" + client.LabID); // IBRBACEmpRoles.SearchListByHQL("rbacemproles.LabID=" + client.LabID);
            foreach (JToken jtoken in empRolesList)
            {
                RBACEmpRoles model = jtoken.ToObject<RBACEmpRoles>();
                if (model == null) continue;
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "新增员工角色信息失败!";
                    break;
                }
                if (allList != null)
                {
                    var tempList = allList.Where(p => p.Id == model.Id);
                    if (tempList != null && tempList.Count() > 0) continue;
                }
                //HREmployee,RBACRole是否存在的处理
                if (model.HREmployee != null)
                {
                    HREmployee emp = IDAO.RBAC.DataAccess_SQL.CreateHREmployeeDao_SQL().Get(model.HREmployee.Id);// IBHREmployee.Get(model.HREmployee.Id);
                    if (emp == null)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "请先初始化完员工帐号信息后再操作!";
                        break;
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "新增员工角色的所属员工信息为空!";
                    break;
                }

                if (model.RBACRole != null)
                {
                    RBACRole role = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleDao_SQL().Get(model.RBACRole.Id);//IBRBACRole.Get(model.RBACRole.Id);
                    if (role == null)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "请先初始化完系统角色信息后再操作!";
                        break;
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "新增员工角色的所属角色信息为空!";
                    break;
                }

                model.DataUpdateTime = DateTime.Now;
                model.DataTimeStamp = null;
                model.LabID = client.LabID;
                IBRBACEmpRoles.Entity = model;
                baseResultDataValue.success = IBRBACEmpRoles.Add();
            }
        }
        /// <summary>
        /// 授权机构角色模块信息初始化处理
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddRBACRoleModuleOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            var roleModuleList = jresult.SelectToken("RBACRoleModuleList").ToList();
            if (roleModuleList == null || roleModuleList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的角色模块信息为空!";
                return baseResultDataValue;
            }
            //授权机构信息
            SServiceClient client = IBSServiceClient.Get(cenOrg.LabID);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权机构信息(SServiceClient)为空!";
                return baseResultDataValue;
            }
            AddRBACRoleModulesList(client, roleModuleList, ref baseResultDataValue);

            return baseResultDataValue;
        }
        private void AddRBACRoleModulesList(SServiceClient client, List<JToken> roleModuleList, ref BaseResultDataValue baseResultDataValue)
        {
            if (roleModuleList == null || roleModuleList.Count <= 0) return;

            IList<RBACRoleModule> allList = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleModuleDao_SQL().GetListByHQL("LabID=" + client.LabID); // IBRBACRoleModule.SearchListByHQL("rbacrolemodule.LabID=" + client.LabID);
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (JToken jtoken in roleModuleList)
            {
                RBACRoleModule model = jtoken.ToObject<RBACRoleModule>();
                if (model == null) continue;

                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "新增角色模块信息失败!";
                    break;
                }
                if (allList != null)
                {
                    var tempList = allList.Where(p => p.Id == model.Id);
                    if (tempList != null && tempList.Count() > 0) continue;
                }
                RBACRole serverRole = null;
                if (model.RBACRole != null)
                {
                    serverRole = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleDao_SQL().Get(model.RBACRole.Id);// IBRBACRole.Get(model.RBACRole.Id);
                    if (serverRole == null)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "请先初始化完系统角色信息后再操作!";
                        break;
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "新增角色模块的所属角色信息为空!";
                    break;
                }
                RBACModule serverModule = null;
                if (model.RBACModule != null)
                {
                    serverModule = IDAO.RBAC.DataAccess_SQL.CreateRBACModuleDao_SQL().Get(model.RBACModule.Id);// IBRBACModule.Get(model.RBACModule.Id);
                    if (serverModule == null)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "请先初始化完系统模块信息后再操作!";
                        break;
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "新增角色模块的所属模块信息为空!";
                    break;
                }

                RBACRoleModule roleModule = new RBACRoleModule();
                RBACModule module = serverModule;
                roleModule.RBACModule = module;
                if (roleModule.RBACModule.DataTimeStamp == null)
                    roleModule.RBACModule.DataTimeStamp = dataTimeStamp;
                roleModule.LabID = client.LabID;
                roleModule.IsUse = true;
                RBACRole role = serverRole;
                roleModule.RBACRole = role;
                if (roleModule.RBACRole.DataTimeStamp == null)
                    roleModule.RBACRole.DataTimeStamp = dataTimeStamp;

                roleModule.Id = model.Id;
                roleModule.IsDefaultOpen = model.IsDefaultOpen;
                roleModule.IsUse = model.IsUse;
                roleModule.DataUpdateTime = DateTime.Now;
                roleModule.DataTimeStamp = null;
                roleModule.LabID = client.LabID;
                IBRBACRoleModule.Entity = roleModule;
                baseResultDataValue.success = IBRBACRoleModule.Add();
            }
        }
        /// <summary>
        /// 授权机构系统运行参数初始化处理
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddBParameterOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            //授权机构信息
            SServiceClient client = IBSServiceClient.Get(cenOrg.LabID);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权机构信息(SServiceClient)为空!";
                return baseResultDataValue;
            }

            var bparameterList = jresult.SelectToken("BParameterList").ToList();
            AddBParameterList(client, bparameterList, ref baseResultDataValue);

            return baseResultDataValue;
        }
        private void AddBParameterList(SServiceClient client, List<JToken> bparameterList, ref BaseResultDataValue baseResultDataValue)
        {
            if (bparameterList == null || bparameterList.Count <= 0)
            {
                baseResultDataValue = AddBParameterOfPlatform(client.LabID);
                return;
            }

            IList<BParameter> allList = IDAO.RBAC.DataAccess_SQL.CreateBParameterDao_SQL().GetListByHQL("LabID=" + client.LabID); // IBBParameter.SearchListByHQL(" bparameter.ParaType='CONFIG' and bparameter.LabID=" + client.LabID);
            if (allList == null) allList = new List<BParameter>();

            foreach (JToken jtoken in bparameterList)
            {
                BParameter model = jtoken.ToObject<BParameter>();
                if (model == null) continue;
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "新增系统运行参数信息失败!";
                    break;
                }
                var tempModelList = allList.Where(p => p.ParaType == model.ParaType && p.ParaNo == model.ParaNo);
                if (tempModelList == null || tempModelList.Count() <= 0)
                {
                    model.DataUpdateTime = DateTime.Now;
                    model.LabID = client.LabID;
                    EditItemEditInfo(ref model);
                    IBBParameter.Entity = model;
                    baseResultDataValue.success = IBBParameter.Add();
                }
            }
        }
        /// <summary>
        /// 授权机构字典类型信息初始化处理
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddBDictTypeOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            var dictTypeList = jresult.SelectToken("BDictTypeList").ToList();
            if (dictTypeList == null || dictTypeList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的字典类型信息为空!";
                return baseResultDataValue;
            }
            SServiceClient client = IBSServiceClient.Get(cenOrg.LabID);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权机构信息(SServiceClient)为空!";
                return baseResultDataValue;
            }
            AddBDictTypeList(client, dictTypeList, ref baseResultDataValue);

            return baseResultDataValue;
        }
        public void AddBDictTypeList(SServiceClient client, List<JToken> dictTypeList, ref BaseResultDataValue baseResultDataValue)
        {
            if (dictTypeList == null || dictTypeList.Count <= 0) return;

            IList<BDictType> allList = IDAO.RBAC.DataAccess_SQL.CreateBDictTypeDao_SQL().GetListByHQL("LabID=" + client.LabID); // IBBDictType.SearchListByHQL("bdicttype.LabID=" + client.LabID);
            foreach (JToken jtoken in dictTypeList)
            {
                BDictType model = jtoken.ToObject<BDictType>();
                if (model == null) continue;
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "新增字典类型信息失败!";
                    break;
                }
                var tempModule = allList.Where(p => p.DictTypeCode == model.DictTypeCode);
                if (tempModule == null || tempModule.Count() <= 0)
                {
                    model.LabID = client.LabID;
                    IBBDictType.Entity = model;
                    baseResultDataValue.success = IBBDictType.Add();
                }
            }
        }
        /// <summary>
        /// 授权机构条码规则信息初始化处理
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddReaCenBarCodeFormatOfInitialize(CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            GetFileOfInitialize(cenOrg, ref baseResultDataValue, ref jresult);
            if (!baseResultDataValue.success) return baseResultDataValue;
            if (jresult == null) return baseResultDataValue;

            var barCodeFormatList = jresult.SelectToken("ReaCenBarCodeFormatList").ToList();
            if (barCodeFormatList == null || barCodeFormatList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权文件的条码规则信息为空!";
                return baseResultDataValue;
            }
            SServiceClient client = IBSServiceClient.Get(cenOrg.LabID);
            if (client == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取授权机构信息(SServiceClient)为空!";
                return baseResultDataValue;
            }
            AddReaCenBarCodeFormatList(client, barCodeFormatList, ref baseResultDataValue);

            return baseResultDataValue;
        }
        public void AddReaCenBarCodeFormatList(SServiceClient client, List<JToken> barCodeFormatList, ref BaseResultDataValue baseResultDataValue)
        {
            if (barCodeFormatList == null || barCodeFormatList.Count <= 0) return;
            //每一份条码规则信息在同一数据库只能存在一份
            IList<ReaCenBarCodeFormat> allList = IDAO.RBAC.DataAccess_SQL.CreateReaCenBarCodeFormatDao_SQL().GetListByHQL(""); //"LabID=" + client.LabIDIBReaCenBarCodeFormat.SearchListByHQL("reacenbarcodeformat.IsUse=1 and reacenbarcodeformat.LabID=" + client.LabID);
            foreach (JToken jtoken in barCodeFormatList)
            {
                ReaCenBarCodeFormat model = jtoken.ToObject<ReaCenBarCodeFormat>();
                if (model == null) continue;

                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = "新增条码规则信息失败!";
                    break;
                }
                if (allList != null)
                {
                    var tempList = allList.Where(p => p.Id == model.Id);
                    if (tempList != null && tempList.Count() > 0)
                    {
                        ReaCenBarCodeFormat model2 = tempList.ElementAt(0);
                        model2.SName = model.SName;
                        model2.SplitCount = model.SplitCount;
                        model2.ShortCode = model.ShortCode;
                        model2.Type = model.Type;
                        model2.CName = model.CName;
                        model2.BarCodeFormatExample = model.BarCodeFormatExample;
                        model2.RegularExpression = model.RegularExpression;
                        model2.Memo = model.Memo;
                        IBReaCenBarCodeFormat.Entity = model2;
                        baseResultDataValue.success = IBReaCenBarCodeFormat.Edit();
                        //continue;
                    }
                    else
                    {
                        model.LabID = client.LabID;
                        IBReaCenBarCodeFormat.Entity = model;
                        baseResultDataValue.success = IBReaCenBarCodeFormat.Add();
                    }
                }
                else
                {
                    model.LabID = client.LabID;
                    IBReaCenBarCodeFormat.Entity = model;
                    baseResultDataValue.success = IBReaCenBarCodeFormat.Add();
                }
            }
        }
        #endregion

        #region 客户端机构授权变更
        /// <summary>
        /// 客户端机构授权变更
        /// </summary>
        /// <returns></returns>
        private BaseResultDataValue EditCenOrgOfInitializeOfClient(JObject jresult, HttpPostedFile file, ref SCAttachment attachment, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            /*
              * 只针对已授权的机构管理员进行授权变更,授权变更内容项有
                 1.客户端系统新增的模块处理
                 2.客户端的机构管理员角色的角色模块变更处理(新增了哪些模块, 减少了哪些模块)
                 3.授权文件的保存处理
                     (1)客户端服务器授权文件保存
                     (2)客户端的授权文件数据库附件保存
             */

            //获取授权文件的机构信息(机构所属ID,所属机构平台编码等)
            long labId = long.Parse(jresult["LabID"].ToString());
            long cenOrgId = long.Parse(jresult["CenOrgId"].ToString());
            long orgNo = long.Parse(jresult["OrgNo"].ToString());

            SServiceClient client = IBSServiceClient.Get(labId);
            //授权机构的角色名称为"机构管理员"的角色信息
            RBACRole sysRole = jresult["SysRole"].ToObject<RBACRole>();
            //IList<BParameter> bparameterList = jresult["BParameterList"].ToObject<IList<BParameter>>();
            var bparameterList = jresult.SelectToken("BParameterList").ToList();

            //授权机构的角色名称为"机构管理员"的角色模块信息
            IList<RBACRoleModule> sysRoleModuleList = jresult["RBACRoleModuleList"].ToObject<IList<RBACRoleModule>>();
            //授权机构的角色名称为"机构管理员"的模块信息
            //IList<RBACModule> sysModuleList = jresult["RBACModuleList"].ToObject<IList<RBACModule>>();
            var sysModuleList = jresult.SelectToken("RBACModuleList").ToList();

            //先处理授权机构的新增模块信息
            EditRBACModuleList(sysModuleList, ref tempBaseResultDataValue);
            if (tempBaseResultDataValue.success == false) return tempBaseResultDataValue;

            //授权机构的角色名称为"机构管理员"的角色模块信息的新增及删除处理            
            tempBaseResultDataValue = EditRBACRoleModulesList(client, sysRole, sysRoleModuleList, empID, empName);
            if (tempBaseResultDataValue.success == false) return tempBaseResultDataValue;

            AddBParameterList(client, bparameterList, ref tempBaseResultDataValue);
            if (tempBaseResultDataValue.success == false) return tempBaseResultDataValue;

            //机构一维条码的序号信息处理
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool = AddReaBmsSerialOfLabID(client.LabID);
            tempBaseResultDataValue.success = baseResultBool.success;
            tempBaseResultDataValue.ErrorInfo = baseResultBool.ErrorInfo;
            if (tempBaseResultDataValue.success == false) return tempBaseResultDataValue;

            //授权文件的保存处理
            string fileCName = attachment.FileName.Substring(0, attachment.FileName.LastIndexOf(".")) + "_" + attachment.Id + attachment.FileExt + "." + FileExt.zf.ToString();
            string subDir = "客户端授权变更";
            string basePath = GetFilePath(client.LabID, subDir);
            string filePath = DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\" + DateTime.Now.ToString("HHmmss") + "\\";

            if (!Directory.Exists(basePath + "\\" + filePath))
                Directory.CreateDirectory(basePath + "\\" + filePath);
            string filePathAll = Path.Combine(basePath + "\\" + filePath, fileCName);
            if (File.Exists(filePathAll))
            {
                File.Delete(filePathAll);
            }
            if (file != null)
                file.SaveAs(filePathAll);

            attachment.FilePath = Path.Combine(filePath, fileCName);
            attachment.LabID = client.LabID;
            attachment.BobjectID = client.LabID;
            IBSCAttachment.Entity = attachment;
            tempBaseResultDataValue.success = IBSCAttachment.Add();

            return tempBaseResultDataValue;
        }
        private BaseResultDataValue EditRBACRoleModulesList(SServiceClient client, RBACRole sysRole, IList<RBACRoleModule> sysRoleModuleList, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            IList<RBACRoleModule> allList = IDAO.RBAC.DataAccess_SQL.CreateRBACRoleModuleDao_SQL().GetListByHQL("LabID=" + sysRole.LabID);
            //.GetListByHQL("LabID=" + sysRole.LabID);//.LoadAll();
            #region 修改3.0.0.58版本授权变更的角色ID为空BUG
            var nullRoleList = allList.Where(p => p.LabID == sysRole.LabID && p.RBACRole == null).ToList();
            foreach (var roleModule in nullRoleList)
            {
                roleModule.RBACRole = sysRole;
                IBRBACRoleModule.Entity = roleModule;
                List<string> parms = new List<string>();
                parms.Add("Id=" + roleModule.Id);
                parms.Add("RBACRole.Id=" + sysRole.Id);
                tempBaseResultDataValue.success = IBRBACRoleModule.Update(parms.ToArray());
            }
            #endregion

            //客户端数据库上当前的角色名称为"机构管理员"的角色模块信息
            IList<RBACRoleModule> curSysRoleModuleList = allList.Where(p => p.LabID == sysRole.LabID && p.RBACRole != null && p.RBACRole.Id == sysRole.Id).ToList();

            //新增的角色模块保存
            StringBuilder addMemo = new StringBuilder();
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (RBACRoleModule roleModule in sysRoleModuleList)
            {
                if (tempBaseResultDataValue.success == false)
                {
                    tempBaseResultDataValue.ErrorInfo = "新增角色模块信息失败!";
                    break;
                }
                var tempModelList = allList.Where(p => p.Id == roleModule.Id);
                if (tempModelList == null || tempModelList.Count() <= 0)
                {
                    if (addMemo.Length <= 0) addMemo.Append("新增角色模块有:");
                    addMemo.Append("模块为:【" + roleModule.RBACModule.CName + "】;");

                    RBACModule module = IBRBACModule.Get(roleModule.RBACModule.Id);
                    if (module != null)
                        roleModule.RBACModule = module;
                    if (roleModule.RBACModule.DataTimeStamp == null)
                        roleModule.RBACModule.DataTimeStamp = dataTimeStamp;

                    roleModule.RBACRole = IBRBACRole.Get(roleModule.RBACRole.Id);
                    roleModule.LabID = client.LabID;
                    IBRBACRoleModule.Entity = roleModule;
                    tempBaseResultDataValue.success = IBRBACRoleModule.Add();
                    ZhiFang.Common.Log.Log.Debug("新增角色模块信息:LabID=" + roleModule.LabID + ",ModuleVisiteID:" + roleModule.Id + ",RBACModuleId:" + roleModule.RBACModule.Id + "," + roleModule.RBACRole.Id);
                }
            }
            //移除角色模块信息
            StringBuilder delMemo = new StringBuilder();
            foreach (RBACRoleModule model in curSysRoleModuleList)
            {
                if (tempBaseResultDataValue.success == false)
                {
                    tempBaseResultDataValue.ErrorInfo = "移除角色模块信息失败!";
                    break;
                }
                var tempModelList = sysRoleModuleList.Where(p => p.Id == model.Id);
                if (tempModelList == null || tempModelList.Count() <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("移除角色模块信息:LabID=" + model.LabID + ",ModuleVisiteID:" + model.Id + ",RBACModuleId:" + model.RBACModule.Id + "," + model.RBACRole.Id);
                    if (delMemo.Length <= 0) delMemo.Append("移除角色模块有:");
                    delMemo.Append("模块为:【" + model.RBACModule.CName + "】;");
                    IBRBACRoleModule.Entity = model;
                    tempBaseResultDataValue.success = IBRBACRoleModule.Remove();
                }
            }
            if (tempBaseResultDataValue.success == true)
                AddSCOperation(client, empID, empName, long.Parse(SServiceClientOperation.授权变更.Key), addMemo.Append(delMemo.ToString()).ToString());

            return tempBaseResultDataValue;
        }

        #endregion

        private int GetMaxOrgNo()
        {
            IList<int> list = this.DBDao.Find<int>("select max(cenorg.OrgNo) as OrgNo  from CenOrg cenorg where 1=1 ");
            if (list != null && list.Count > 0)
            {
                int maxOrgNo = list[0];
                maxOrgNo = maxOrgNo < 100002 ? 100002 : ++maxOrgNo;
                //if (maxOrgNo < 100001)
                //    maxOrgNo = 100001;
                //else
                //    maxOrgNo++;
                return maxOrgNo;
            }
            else
                return 0;
        }
        private void AddSCOperation(SServiceClient client, long empID, string empName, long status, string memo)
        {
            SCOperation sco = new SCOperation();
            sco.LabID = client.LabID;
            sco.BobjectID = client.LabID;
            sco.CreatorID = empID;
            sco.CreatorName = empName;
            sco.BusinessModuleCode = "SServiceClient";
            sco.Memo = memo;
            sco.IsUse = true;
            sco.Type = status;
            sco.TypeName = SServiceClientOperation.GetStatusDic()[status.ToString()].Name;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }
        /// <summary>
        /// 测试用
        /// </summary>
        public void GetCenOrgListTest()
        {
            IList<CenOrg> list = ((IDCenOrgDao)base.DBDao).GetCenOrgOfNoLabIDList("");
            JObject jresult = new JObject();
            jresult.Add("CenOrgList", JArray.FromObject(list));
            //ZhiFang.Common.Log.Log.Debug("CenOrgCounts:" + list.Count);
            //ZhiFang.Common.Log.Log.Debug("CenOrgList:" + jresult.ToString());
        }

    }
}