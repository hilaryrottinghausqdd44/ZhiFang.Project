using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBParameter : IBGenericManager<BParameter>
    {

        /// <summary>
        /// 根据参数paraNo获取参数信息
        /// </summary>
        /// <param name="paraNo"></param>
        /// <returns></returns>
        BParameter GetParameterByParaNo(string paraNo);

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
        /// <summary>
        /// 获取到当前新增保存申请单号值
        /// </summary>
        /// <param name="totalWidth">位数长度</param>
        /// <returns></returns>
        string GetAddBloodBReqFormId(int totalWidth);
        /// <summary>
        /// 按分组获取用户设置的系统运行参数集合信息(HQL)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        BaseResultDataValue SearchBParameterOfUserSetByHQL(string where, string sort);
        /// <summary>
        /// 批量修改用户设置的系统运行参数值
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        BaseResultBool EditBParameterListByBatch(IList<BParameter> entityList,ref IList<BParameter> editEntityList);
    }
}