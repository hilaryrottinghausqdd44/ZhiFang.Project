using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.OA.ViewObject.Response
{
    /// <summary>
    /// 服务器授权申请的程序类型
    /// </summary>
    [DataContract]
    public class LicenceProgramType
    {
        [DataMember]
        /// <summary>
        /// 程序类型Id
        /// </summary>
        public long Id { get; set; }
        [DataMember]
        /// <summary>
        /// 程序类型名称:如检验之星,仪器通讯,打印程序等
        /// </summary>
        public string CName { get; set; }
        [DataMember]
        /// <summary>
        /// 程序类型Code
        /// </summary>
        public string Code { get; set; }
        [DataMember]
        /// <summary>
        /// SQH
        /// </summary>
        public string SQH { get; set; }
    }
}
