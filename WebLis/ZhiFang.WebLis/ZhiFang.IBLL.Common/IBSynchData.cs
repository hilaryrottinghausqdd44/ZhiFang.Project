using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common
{
    public interface IBSynchData
    {
        /// <summary>
        /// 根据DataSet增加或修改数据，同步字典时使用
        /// </summary>
        int AddUpdateByDataSet(DataSet ds);
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="ht">Hashtable：key=主键字段 value=值</param>
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
    }
}
