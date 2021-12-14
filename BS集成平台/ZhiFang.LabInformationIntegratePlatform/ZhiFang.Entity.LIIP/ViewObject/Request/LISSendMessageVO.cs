using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.Entity.LIIP.ViewObject.Request
{
    public class LISSendMessageVO
    {
        // {
        //“SenderID”：”1234567890” ,                        发送者ID。
        //“SenderName”：”张三” ,                            发送者名称。
        //“SenderAccount”：”P1” ,                           发送者帐号，关键信息。平台帐号应该跟LIS帐号一致。
        //“SendSectionID”：”111” ,                          消息发送小组ID，关键信息。
        //“SendSectionName”：”生化小组” ,                   消息发送小组名称，关键信息。
        //“MsgTypeID”：”1” ,                                消息类型ID，关键信息。
        //“MsgTypeName”：”报告延迟” ,                       消息类型名称，关键信息。
        //“MsgTypeCode”：”ReportDelay” ,                    消息类型代码，关键信息
        //“RecLabID”：”1001” ,                              接收机构ID，关键信息。
        //“RecLabName”：”社区医院” ,                        接收机构名称，关键信息。
        //“RecLabCode”：”1001” ,                            接收机构编码，关键信息。
        //“LabBarCode”：”1001” ,                            送检单位条码，关键信息。
        //“CenterBarCode”：”1001” ,                         中心实验室条码，关键信息。
        //“MsgContent”：”” ,                                消息内容，关键信息。
        //“SystemID”：”” ,                                  所属系统ID，关键信息。
        //“SystemCName”：”” ,                               所属系统名称，关键信息。
        //“SystemCode”：”” ,                                所属系统代码，关键信息。
        //}

        /// <summary>
        /// 发送者ID
        /// </summary>
        public long? SenderID { get; set; }

        /// <summary>
        /// 发送者名称
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// 发送者帐号，关键信息。平台帐号应该跟LIS帐号一致。
        /// </summary>
        public string SenderAccount { get; set; }

        /// <summary>
        /// 消息发送小组ID
        /// </summary>
        public long? SendSectionID { get; set; }

        /// <summary>
        /// 消息发送小组名称
        /// </summary>
        public string SendSectionName { get; set; }

        /// <summary>
        /// 消息类型ID
        /// </summary>
        public long? MsgTypeID { get; set; }

        /// <summary>
        /// 消息类型名称
        /// </summary>
        public string MsgTypeName { get; set; }

        /// <summary>
        /// 消息类型代码
        /// </summary>
        public string MsgTypeCode { get; set; }

        /// <summary>
        /// 接收机构ID
        /// </summary>
        public long? RecLabID { get; set; }

        /// <summary>
        /// 接收机构名称
        /// </summary>
        public string RecLabName { get; set; }

        /// <summary>
        /// 接收机构编码，应该跟LIS编码一致
        /// </summary>
        public string RecLabCode { get; set; }

        /// <summary>
        /// 送检单位条码
        /// </summary>
        public string LabBarCode { get; set; }

        /// <summary>
        /// 中心实验室条码
        /// </summary>
        public string CenterBarCode { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MsgContent { get; set; }

        /// <summary>
        /// 所属系统ID
        /// </summary>
        public long? SystemID { get; set; }

        /// <summary>
        /// 所属系统名称
        /// </summary>
        public string SystemCName { get; set; }

        /// <summary>
        /// 所属系统代码
        /// </summary>
        public string SystemCode { get; set; }

        public long SCMsgID { get; set; }
    }
}
