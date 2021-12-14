using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common
{
    public interface IBBatchCopy
    {
        bool CopyToLab(List<string> lst);   
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时删除数据
        /// </summary>
        int DeleteByDataRow(DataRow dr);

        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="labCodeNo"></param>
        /// <returns></returns>
        bool IsExist(string labCodeNo);

        /// <summary>
        /// 删除字典表和关系中LabCode的记录
        /// </summary>
        /// <param name="LabCodeNo"></param>
        /// <returns></returns>
        bool DeleteByLabCode(string LabCodeNo);
    }
}
