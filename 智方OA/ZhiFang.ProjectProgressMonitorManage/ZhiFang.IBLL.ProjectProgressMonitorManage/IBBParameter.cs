

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBParameter : IBGenericManager<BParameter>
    {
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