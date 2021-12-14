

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBParameter : IBGenericManager<BParameter>
	{
        /// <summary>
        /// 获取样本操作记录的开关
        /// </summary>
        /// <returns></returns>
        bool GetOperLogPara();

        /// <summary>
        /// 设置样本操作记录的开关
        /// </summary>
        /// <returns></returns>
        bool SetOperLogPara();
        /// <summary>
        /// 根据参数paraNo获取参数信息
        /// </summary>
        /// <param name="paraNo"></param>
        /// <returns></returns>
        BParameter GetParameterByParaNo(string paraNo);
        /// <summary>
        /// 根据参数paraNo、小组ID、站点ID获取参数信息
        /// </summary>
        /// <param name="paraNo">参数编码</param>
        /// <param name="GroupNo">小组ID</param>
        /// <param name="NodeID">站点ID</param>
        /// <returns>参数对象</returns>
        BParameter GetParameterByParaNoAndGroupNoAndNodeID(string paraNo, long? GroupNo, long? NodeID);
        /// <summary>
        /// 根据参数paraNo、小组ID、站点名称获取参数信息
        /// </summary>
        /// <param name="paraNo">参数编码</param>
        /// <param name="GroupNo">小组ID</param>
        /// <param name="NodeName">站点名称</param>
        /// <returns>参数对象</returns>
        BParameter GetParameterByParaNoAndGroupNoAndNodeName(string paraNo, long? GroupNo, string NodeName);
	}
}