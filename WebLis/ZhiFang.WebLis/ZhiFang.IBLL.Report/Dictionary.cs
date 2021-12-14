using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface Dictionary
    {
        /// <summary>
        /// 科室
        /// </summary>
        /// <returns></returns>
        DataSet Department();
        /// <summary>
        /// 开单医生
        /// </summary>
        /// <returns></returns>
        DataSet Doctor();
        /// <summary>
        /// 病区
        /// </summary>
        /// <returns></returns>
        DataSet District();
        /// <summary>
        /// 就诊类型
        /// </summary>
        /// <returns></returns>
        DataSet SickType();
        /// <summary>
        /// 送检单位
        /// </summary>
        /// <returns></returns>
        DataSet Client();
    }
}
