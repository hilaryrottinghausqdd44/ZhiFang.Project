using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.InterfaceFactory
{
    /// <summary>
    /// 定制插件实现功能接口
    /// 和第三方接口使用
    /// </summary>
    public interface DllInterface
    {
        /// <summary>
        /// 调用第三方接口获取货品信息
        /// </summary>
        /// <returns></returns>
        BaseResultBool GetReaGoodsByInterface(Hashtable ht);

        /// <summary>
        /// 给第三方接口发送订单信息
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        BaseResultBool SendBmsCenOrderByInterface(Hashtable ht);

        /// <summary>
        /// 调用第三方接口，获取出库单，写入到智方试剂平台
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        BaseResultBool GetOutOrderInfoByInterface(Hashtable ht);

        /// <summary>
        /// 给第三方接口发送出库单信息
        /// </summary>
        BaseResultBool SendOutInfoByInterface(Hashtable ht);

    }
}
