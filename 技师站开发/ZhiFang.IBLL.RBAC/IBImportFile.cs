using System.IO;

namespace ZhiFang.IBLL.RBAC
{
    public interface IBImportFile
    {
        IBHREmployee IBHREmployee { get; set; }
        IBRBACUser IBRBACUser { get; set; }
        IBHRDept IBHRDept { get; set; }

        /// <summary>
        /// 批量导入员工（可支持账户同步生成）支持EXCEL\XML两种方式
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="filetype"></param>
        /// <param name="isCreateAccount"></param>
        /// <returns></returns>
        string RJ_AddInEmpList(FileStream fs, ZhiFang.Common.Public.FileType type, bool isCreateAccount);

        /// <summary>
        /// 批量导入部门,支持EXCEL\XML两种方式
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="filetype"></param>
        /// <returns></returns>
        string RJ_AddInDeptList(FileStream fs, ZhiFang.Common.Public.FileType type);
    }
}
