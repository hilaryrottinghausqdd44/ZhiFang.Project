using System.Runtime.InteropServices;

namespace ZhiFang.Entity.RBAC
{
    /// <summary>
    /// 测试用--行数据条件缓存信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class SYSCacheRoleRight
    {
        public long RightID { get; set; }
        public long? ModuleOperId { get; set; }
        public long? RoleID { get; set; }
        public long? RowFilterID { get; set; }
        public SYSCacheRoleRight() { }

    }
}
