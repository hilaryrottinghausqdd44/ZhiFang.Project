using ZhiFang.Entity.Base;
using ZhiFang.Entity.Common;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.Common
{
    public interface IDFFileDao : IDBaseDao<FFile, long>
    {
        /// <summary>
        /// 更新文档的总阅读次数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        bool UpdateFFileCountsById(long id);
        /// <summary>
        /// 删除文档信息
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="isUse"></param>
        /// <returns></returns>
        bool UpdateFFileIsUseByIds(string strIds, bool isUse);
        /// <summary>
        /// 置顶/撤消置顶文档信息
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="IsTop"></param>
        /// <returns></returns>
        bool UpdateFFileIsTopByIds(string strIds, bool IsTop);
        EntityList<FFile> SearchFFileCopyUserListByHQLAndEmployeeIDCookie(string strHqlWhere, int page, int count);

        EntityList<FFile> SearchFFileCopyUserListByHQLAndEmployeeIDCookie(string strHqlWhere, string Order, int page, int count);
        EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeIDCookie(string strHqlWhere, int page, int count);

        EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeIDCookie(string strHqlWhere, string Order, int page, int count);
        EntityList<FFile> SearchListByHQL(string where, int page, int limit, string sort);

        EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeID(string strHqlWhere, string order, int page, int count, string employeeID, string employeeName);
    }
}