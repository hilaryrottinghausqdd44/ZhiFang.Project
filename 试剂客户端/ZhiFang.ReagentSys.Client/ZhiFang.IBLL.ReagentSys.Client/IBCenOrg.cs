using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    public interface IBCenOrg : IBGenericManager<CenOrg>
    {

        /// <summary>
        /// 智方平台对客户进行机构信息初始化
        /// </summary>
        /// <param name="labId">智方机构的LabId</param>
        /// <param name="cenOrg">机构基本信息</param>
        /// <param name="user">机构用户信息</param>
        /// <param name="roleIdStrOfZf">智方机构的标准角色选择</param>
        /// <param name="moduleIdStr"></param>
        /// <returns></returns>
        BaseResultDataValue AddCenOrgOfInitializeOfPlatform(long labId, CenOrg cenOrg, RBACUser user, string roleIdStrOfZf, string moduleIdStr, long empID, string empName);
        /// <summary>
        /// 授权变更定制,获取机构授权的系统角色的角色模块访问权限
        /// </summary>
        /// <param name="labId"></param>
        /// <returns></returns>
        EntityList<RBACRoleModule> SearchRBACRoleModuleByLabIDAndSysRoleId(long labId);
        /// <summary>
        /// 修改客户机构的授权变更
        /// (只针对客户机构的机构角色的角色模块进行变更)
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="moduleList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditCenOrgAuthorizationModifyOfPlatform(long labId, long cenOrgId, IList<RBACModule> moduleList, long empID, string empName);
        /// <summary>
        /// 机构授权文件从平台导出
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="cenOrgId"></param>
        /// <param name="fileType">授权文件类型:1为初始化授权;2:为授权变更</param>
        /// <returns></returns>
        Stream SearchExportAuthorizationFileOfPlatform(long labId, long cenOrgId, long fileType, ref bool result, ref string fileCName, long empID, string empName);
        /// <summary>
        /// 授权机构授权文件导入
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        BaseResultDataValue AddUploadAuthorizationFileOfClient(HttpPostedFile file, long empID, string empName);

        #region 客户端机构初始化
        string GetCenOrgDataValue(string tempStr);
        /// <summary>
        /// 获取当前授权机构的授权进度信息
        /// </summary>
        /// <param name="cenOrg"></param>
        /// <returns></returns>
        EntityList<LicenseGuideVO> SearchLicenseGuideVOByCenOrg(CenOrg cenOrg,ref BaseResultDataValue baseResultDataValue);
        /// <summary>
        /// 从授权初始化文件获取授权机构的基本信息
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue GetCenOrgInitializeInfo();
        /// <summary>
        /// 授权机构初始化信息分步处理
        /// </summary>
        /// <param name="cenOrg">授权机构基本信息</param>
        /// <param name="entity">当前初始化的分步实体</param>
        /// <returns></returns>
        BaseResultDataValue AddCenOrgInitializeByStep(CenOrg cenOrg, string entity);
        /// <summary>
        /// 授权机构功能模块信息初始化处理
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue EditRBACModuleOfInitialize(CenOrg cenOrg);
        /// <summary>
        /// 授权机构信息初始化处理
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue AddSServiceClientAndCenOrgOfInitialize(CenOrg cenOrg);
        /// <summary>
        /// 授权机构管理部门初始化处理
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue AddHRDeptOfInitialize(CenOrg cenOrg);
        /// <summary>
        /// 授权机构管理员及帐号初始化处理
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue AddEmployeeAndUserOfInitialize(CenOrg cenOrg);
        /// <summary>
        /// 授权机构角色信息初始化处理
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue AddRBACRoleOfInitialize(CenOrg cenOrg);
        /// <summary>
        /// 授权机构员工角色信息初始化处理
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue AddRBACEmpRolesOfInitialize(CenOrg cenOrg);
        /// <summary>
        /// 授权机构角色模块信息初始化处理
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue AddRBACRoleModuleOfInitialize(CenOrg cenOrg);
        /// <summary>
        /// 授权机构系统运行参数初始化处理
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue AddBParameterOfInitialize(CenOrg cenOrg);
        /// <summary>
        /// 授权机构字典类型信息初始化处理
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue AddBDictTypeOfInitialize(CenOrg cenOrg);
        /// <summary>
        /// 授权机构条码规则信息初始化处理
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue AddReaCenBarCodeFormatOfInitialize(CenOrg cenOrg);
        #endregion
        BaseResultBool AddReaBmsSerialOfLabID(long labID);
    }
}
