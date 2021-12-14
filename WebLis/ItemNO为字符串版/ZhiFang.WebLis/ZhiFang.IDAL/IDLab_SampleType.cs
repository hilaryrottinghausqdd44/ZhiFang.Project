using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLab_SampleType	
	/// </summary>
    public interface IDLab_SampleType : IDataBase<ZhiFang.Model.Lab_SampleType>, IDataPage<ZhiFang.Model.Lab_SampleType>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode, int LabSampleTypeNo);
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
        int Delete(string LabCode, int LabSampleTypeNo);

        int DeleteList(string SampleTypeIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.Lab_SampleType GetModel(string LabCode, int LabSampleTypeNo);


        DataSet GetListByLike(ZhiFang.Model.Lab_SampleType model);
        DataSet GetLabCodeNo(string LabCode, List<string> LabCname);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        #endregion  成员方法
    }
}