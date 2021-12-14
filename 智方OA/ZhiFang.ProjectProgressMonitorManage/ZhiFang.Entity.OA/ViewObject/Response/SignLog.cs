using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.OA.ViewObject.Response
{
    public class SignLog:SignInfo
    {
        /// <summary>
        /// 签到事件位置坐标
        /// </summary>
        public string SigninATEventLogPostion { get; set; }
        /// <summary>
        /// 签到事件位置坐标名称
        /// </summary>
        public string SigninATEventLogPostionName { get; set; }
        /// <summary>
        /// 签退事件位置坐标
        /// </summary>
        public string SignoutATEventLogPostion { get; set; }
        /// <summary>
        /// 签退事件位置坐标名称
        /// </summary>
        public string SignoutATEventLogPostionName { get; set; }


    }
}
