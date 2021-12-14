using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDSickType : IDataBase<ZhiFang.Model.SickType>,IDataPage<ZhiFang.Model.SickType>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int SickTypeNo);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int SickTypeNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.SickType GetModel(int SickTypeNo);
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
        int DeleteList(string SickTypeIDlist);
        int Add(List<ZhiFang.Model.SickType> modelList);
        int Update(List<ZhiFang.Model.SickType> modelList);
    }
}
