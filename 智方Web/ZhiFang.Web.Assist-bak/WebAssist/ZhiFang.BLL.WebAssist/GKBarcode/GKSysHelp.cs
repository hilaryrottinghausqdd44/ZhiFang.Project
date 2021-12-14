using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.WebAssist;

namespace ZhiFang.BLL.WebAssist
{
    /// <summary>
    /// 院感系统全局配置
    /// </summary>
    public class GKSysHelp
    {
        /// <summary>
        /// 院感申请的就诊类型对照字段，默认为code_4
        /// </summary>
        public static string ContractCode = "";

        /// <summary>
        /// 院感申请的就诊类型信息
        /// </summary>
        public static SickType SickType = null;

        /// <summary>
        /// 院感申请的LIS默认核收小组信息
        /// </summary>
        public static PGroup PGroup = null;

        /// <summary>
        /// 院感申请的LIS默认检验者信息
        /// </summary>
        public static PUser MainTester = null;


    }
}
