using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层IDClientEleArea	
    /// </summary>
    public interface IDClientEleArea : IDataBase<ZhiFang.Model.ClientEleArea>, IDataPage<ZhiFang.Model.ClientEleArea>
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

        DataSet GetListLike(ZhiFang.Model.ClientEleArea model);

        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);

        #endregion  成员方法

        DataSet GetPClientNoList(string sclientno);
        List<ZhiFang.Model.ClientEleArea> DataTableToList(DataTable dt);
    }
}