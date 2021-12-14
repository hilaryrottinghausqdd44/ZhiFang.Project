using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDSampleType : IDataBase<ZhiFang.Model.SampleType>, IDataPage<ZhiFang.Model.SampleType>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int SampleTypeNo);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int SampleTypeNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.SampleType GetModel(int SampleTypeNo);

        int DeleteList(string SampleTypeIDlist);

        DataSet GetSampleTypeByColorName(string colorName);
        int Add(List<ZhiFang.Model.SampleType> modelList);
        int Update(List<ZhiFang.Model.SampleType> modelList);
    }
}
