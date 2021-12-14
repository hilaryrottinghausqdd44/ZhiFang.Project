using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.Entity.LIIP.ViewObject.Request
{
    public class HISInterFaceVO
    {
        /// <summary>
        /// 调用动作代码
        /// </summary>
        public string action { get; set; }

        /// <summary>
        /// 调用结果
        /// </summary>
        public bool succtss { get; set; }

        /// <summary>
        /// 调用返回信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 调用结果代码
        /// </summary>
        public string resultcode { get; set; }

        /// <summary>
        /// 参数数据
        /// </summary>
        public string data { get; set; }


    }
}
