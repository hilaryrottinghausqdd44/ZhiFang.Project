using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.RBAC
{
    /// <summary>
    ///
    /// </summary>
    public interface IBRBACPreconditions : IBGenericManager<RBACPreconditions>
    {
        /// <summary>
        /// 将选择的模块服务的预置条件项新增复制到指定的模块服务
        /// </summary>
        /// <param name="moduleoperId">待复制预置条件项所属的模块服务ID</param>
        /// <param name="copyModuleOpeIdStr">选择需要复制的模块服务Id字符串(123,222)</param>
        /// <returns></returns>
        BaseResultBool CopyPreconditionsOfRBACModuleOper(long moduleoperId, string copyModuleOpeIdStr);

    }
}