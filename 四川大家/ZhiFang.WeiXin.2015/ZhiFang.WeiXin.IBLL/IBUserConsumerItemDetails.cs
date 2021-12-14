

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.Statistics;

namespace ZhiFang.WeiXin.IBLL
{
    /// <summary>
    ///
    /// </summary>
    public interface IBUserConsumerItemDetails : IBGenericManager<UserConsumerItemDetails>
    {
        /// <summary>
        /// 查询项目统计报表数据
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<UserConsumerItemDetails> SearchUserConsumerItemDetails(UserConsumerFormSearch searchEntity, int page, int count);
        /// <summary>
        /// 获取项目统计报表Excel导出文件
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        FileStream GetExportExcelUserConsumerItemDetails(UserConsumerFormSearch searchEntity, ref string fileName);
        /// <summary>
        /// 获取项目统计报表Excel转PDF的文件
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        BaseResultDataValue GetUserConsumerItemExcelToPdfFile(UserConsumerFormSearch searchEntity, ref string fileName);

    }
}
