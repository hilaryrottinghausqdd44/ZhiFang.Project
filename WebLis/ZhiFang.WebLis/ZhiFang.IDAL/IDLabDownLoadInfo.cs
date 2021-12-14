using System;
using System.Data;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层IDLabDownLoadInfo	
    /// </summary>
    public interface IDLabDownLoadInfo : IDataBase<ZhiFang.Model.LabDownLoadInfo>, IDataPage<ZhiFang.Model.LabDownLoadInfo>
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
        /// 获取最大的时间戳
        /// </summary>
        string GetMaxDTimeStampe();
        #endregion  成员方法
    }
}