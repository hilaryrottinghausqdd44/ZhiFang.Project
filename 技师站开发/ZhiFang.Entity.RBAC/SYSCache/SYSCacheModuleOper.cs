using System.Runtime.InteropServices;

namespace ZhiFang.Entity.RBAC
{
    /// <summary>
    /// 测试用--模块服务缓存信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class SYSCacheModuleOper
    {
        public long ModuleId { get; set; }
        public long? ModuleOperId { get; set; }
        public string ServiceURLEName { get; set; }
        public string RowFilterBaseCName { get; set; }
        //protected IList<SYSCacheRoleRight> _sYSCacheRoleRightList;
        //public IList<SYSCacheRoleRight> SYSCacheRoleRightList
        //{
        //    get
        //    {
        //        if (_sYSCacheRoleRightList == null)
        //        {
        //            _sYSCacheRoleRightList = new List<SYSCacheRoleRight>();
        //        }
        //        return _sYSCacheRoleRightList;
        //    }
        //    set { _sYSCacheRoleRightList = value; }
        //}
        public SYSCacheModuleOper() { }

    }
}
