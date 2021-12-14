using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IBClientEleArea	
    /// </summary>
    public interface IBClientEleArea : IBBase<ZhiFang.Model.ClientEleArea>, IBDataPage<ZhiFang.Model.ClientEleArea>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int AreaID);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int AreaID);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.ClientEleArea GetModel(int AreaID);

        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);

        #endregion  成员方法

        List<ZhiFang.Model.ClientEleArea> DataTableToList(DataTable dt);	
        string GetPClientNoBySClientNo(string hiddenClient);
    }
}