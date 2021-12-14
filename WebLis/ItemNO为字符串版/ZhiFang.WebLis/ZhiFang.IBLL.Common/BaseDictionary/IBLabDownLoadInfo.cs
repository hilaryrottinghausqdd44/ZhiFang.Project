using System;
using System.Data;
using ZhiFang.IBLL.Common;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IBLabDownLoadInfo	
    /// </summary>
    public interface IBLabDownLoadInfo : IBBase<ZhiFang.Model.LabDownLoadInfo>, IBDataPage<ZhiFang.Model.LabDownLoadInfo>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string TableName);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string TableName);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.LabDownLoadInfo GetModel(string TableName);
        
        /// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.LabDownLoadInfo model);
        /// <summary>
        /// 获取最大的时间戳
        /// </summary>
        string GetMaxDTimeStampe();
        #endregion  成员方法
    }
}