

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

        bool AddAndSetCache();
        bool UpdateAndSetCache(string[] strParas);
        /// <summary>
        /// 同步webconfig的系统参数配置项(从webconfig过渡到数据库的系统参数表使用)
        /// </summary>
        void SyncWebConfigToBParameter();
        /// <summary>
        /// 依参数编码获取系统参数信息
        /// </summary>
        /// <param name="paraNo"></param>
        /// <returns></returns>
        IList<BParameter> SearchListByParaNo(string paraNo);
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        object GetCache(string CacheKey);
        /// <summary>
        /// 添加或者更新指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        void SetCache(string CacheKey, object objObject);
        /// <summary>
        /// 清除单一键缓存
        /// </summary>
        /// <param name="key"></param>
        void RemoveOneCache(string CacheKey);
        void RemoveAllCache();
    }
}