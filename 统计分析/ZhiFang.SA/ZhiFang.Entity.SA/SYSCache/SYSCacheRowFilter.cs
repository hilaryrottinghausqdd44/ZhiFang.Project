using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ZhiFang.Entity.SA
{
    /// <summary>
    /// 测试用--行数据条件缓存信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class SYSCacheRowFilter
    {
        public long Id { get; set; }
        public long ModuleOperId { get; set; }
        public string EntityCode { get; set; }
        public string RowFilterCondition { get; set; }
        public SYSCacheRowFilter() { }

    }
}
