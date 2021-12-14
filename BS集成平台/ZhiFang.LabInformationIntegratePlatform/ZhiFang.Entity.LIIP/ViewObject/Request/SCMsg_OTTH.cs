using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.Entity.LIIP.ViewObject.Request
{
    public class SCMsg_OTTH
    {
        //LIS消息平台的消息类型编码
        public string MsgTypeCode { get; set; }
        //接收科室的HIS编码
        public string RecDeptCodeHIS { get; set; }
        //消息内容体
        public string MsgContent { get; set; }
        //要求处理时间
        public string RequireHandleTime { get; set; }
        //要求确认时间
        public string RequireConfirmTime { get; set; }
        //发送者帐号
        public string SenderAccount { get; set; }
        //发送者密码
        public string SenderPWD { get; set; }
        public string SignCode { get; set; }
        /// <summary>
        /// 就诊类型ID
        /// </summary>
        public string SickTypeID{ get; set; }
        /// <summary>
        /// 就诊类型名称
        /// </summary>
        public string SickTypeName { get; set; }

        public SCMsg SCMsg { get; set; }
    }

    public class SCMsg_OTTH_Search
    {
        //LIS消息平台的消息类型编码
        public string MsgTypeCode { get; set; }
        //接收科室的HIS编码
        public string MsgCode { get; set; }
        //发送者帐号
        public string SenderAccount { get; set; }
        //发送者密码
        public string SenderPWD { get; set; }
        public string SignCode { get; set; }
    }
}
