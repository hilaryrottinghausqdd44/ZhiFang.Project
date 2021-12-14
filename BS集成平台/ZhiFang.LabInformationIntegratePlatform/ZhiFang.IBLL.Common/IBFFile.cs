using System.Collections.Generic;
using System.IO;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.Common;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.Common
{
    /// <summary>
    ///
    /// </summary>
    public interface IBFFile : IBGenericManager<FFile>
    {
        FFile EditFFileOperationInfo(FFile entity,string typeStr,ref string fields);
        /// <summary>
        /// 根据ID获取实体
        /// 在文档浏览时需要添加文件浏览的操作记录
        /// </summary>
        /// <param name="longID"></param>
        /// <param name="isAddFFileReadingLog">是否需要添加文档阅读记录信息:1需要,0:不需要</param>
        /// <param name="isAddFFileOperation">是否需要添加文档操作记录信息:1需要,0:不需要</param>
        /// <returns></returns>
        FFile _GetFFileAndAddFFileCopyUser(long longID, int isAddFFileReadingLog, int isAddFFileOperation);
        /// <summary>
        /// 新增文档信息及文档抄送对象信息表
        /// <param name="type">操作记录的操作类型值</param>
        /// <param name="ffileCopyUserType">搅抄送对象类型,(=-1默认没有选择)</param>
        /// <returns></returns>
       // BaseResultDataValue AddFFileAndFFileCopyUser(FFile entity, IList<FFileCopyUser> fFileCopyUserList, int fFileOperationType, int ffileCopyUserTypeAddFFileAndFFileCopyUser, string ffileOperationMemo, IList<FFileReadingUser> fFileReadingUserList, int ffileReadingUserType, HttpPostedFile newThumbnails);
        /// <summary>
        /// 更新文档基本信息时,更新文档抄送对象或更新文档阅读对象信息
        /// </summary>
        /// <param name="tempArray"></param>
        /// <param name="fFileCopyUserList"></param>
        /// <param name="fFileReadingUserList"></param>
        /// <param name="fFileOperationType"></param>
        /// <returns></returns>
        BaseResultBool SaveFFileAndFFileCopyUserAndFFileReadingUser(string[] tempArray, IList<FFileCopyUser> fFileCopyUserList, IList<FFileReadingUser> fFileReadingUserList, int fFileOperationType, int ffileCopyUserType, int ffileReadingLogType, string ffileOperationMemo, FFile entity);
        /// <summary>
        /// 删除文档信息
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="fFileOperationType"></param>
        /// <returns></returns>
        BaseResultBool UpdateFFileIsUseByIds(string strIds, bool isUse, int fFileOperationType);
        /// <summary>
        /// 置顶/撤消置顶文档信息
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="IsTop"></param>
        /// <returns></returns>
        BaseResultBool UpdateFFileIsTopByIds(string strIds, bool IsTop, int fFileOperationType);
        EntityList<FFile> SearchFFileCopyUserListByHQLAndEmployeeID(string strHqlWhere, bool isSearchChildNode, int page, int count);

        EntityList<FFile> SearchFFileCopyUserListByHQLAndEmployeeID(string strHqlWhere, bool isSearchChildNode, string Order, int page, int count);
        EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeID(string strHqlWhere, bool isSearchChildNode, int page, int count);
        EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeID(string strHqlWhere, bool isSearchChildNode, string Order, int page, int count);
        EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeID(string dictreeids, string strHqlWhere, bool isSearchChildNode, string Order, int page, int count);
        /// <summary>
        /// 查询某一类型树的直属文档列表(包含某一类型树的所有子类型树)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isSearchChildNode">是否查询传入节点的所有子孙节点</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <param name="maxLevelStr"></param>
        /// <returns></returns>
        EntityList<FFile> SearchFFileByBDictTreeId(string where,bool isSearchChildNode, int page, int limit, string sort, string maxLevelStr);
        bool FFileWeiXinMessagePushById(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long id);
        /// <summary>
        /// 依文档Id及查询类型获取文档的抄送对象信息或阅读对象信息
        /// </summary>
        /// <param name="ffileId">文档Id</param>
        /// <param name="searchType">查询类型</param>
        /// <returns></returns>
        BaseResultDataValue SearchFFileCopyUserOrReadingUserByFFileId(long ffileId, string searchType);
        /// <summary>
        /// 上传新闻缩略图
        /// </summary>
        /// <param name="newThumbnails"></param>
        /// <param name="type">add:为新增新闻时上传;update为修改新闻时上传</param>
        /// <returns></returns>
        //BaseResultDataValue UploadNewsThumbnails(HttpPostedFile newThumbnails, string type);
        /// <summary>
        /// 新闻缩略图下载
        /// </summary>
        /// <param name="id"></param>
        FileStream DownLoadNewsThumbnailsById(long id);
        /// <summary>
        /// 根据员工ID获取有阅读权限的新闻
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isSearchChildNode"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="employeeID"></param>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeID(string where, bool isSearchChildNode, int page, int count, string employeeID, string employeeName);
        /// <summary>
        /// 依员工Id目录树ID查询阅读文档信息
        /// </summary>
        /// <param name="dictreeids"></param>
        /// <param name="where"></param>
        /// <param name="isSearchChildNode"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="employeeID"></param>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeID(string dictreeids, string where, bool isSearchChildNode, int page, int count, string employeeID, string employeeName);
        BaseResultDataValue AddFFileAndFFileCopyUser(FFile entity, IList<FFileCopyUser> fFileCopyUserList, int fFileOperationType, int ffileCopyUserType, string ffileOperationMemo, IList<FFileReadingUser> fFileReadingUserList, int ffileReadingUserType, object p);
        BaseResultDataValue UploadNewsThumbnails(object file, string v);

        int UnHREmployeeReaderCount(string where);
    }
}