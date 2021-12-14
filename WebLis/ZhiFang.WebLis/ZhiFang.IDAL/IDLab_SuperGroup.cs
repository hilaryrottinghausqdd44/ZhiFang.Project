using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLab_SuperGroup	
	/// </summary>
    public interface IDLab_SuperGroup : IDataBase<ZhiFang.Model.Lab_SuperGroup>, IDataPage<ZhiFang.Model.Lab_SuperGroup>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode, int LabSuperGroupNo);
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
        int Delete(string LabCode, int LabSuperGroupNo);

        int DeleteList(string SuperGroupIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.Lab_SuperGroup GetModel(string LabCode, int LabSuperGroupNo);

        DataSet GetListByLike(ZhiFang.Model.Lab_SuperGroup model);
        /// <summary>
        /// 根据实验室字典检验大组的名称获取实验室检验大组的编码
        /// </summary>
        /// <param name="LabCode">送检单位</param>
        /// <param name="LabCname">检验大组的名称</param>
        /// <returns>检验大组实验室的编码</returns>
        DataSet GetLabCodeNo(string LabCode, List<string> LabCname);
        #endregion  成员方法
        DataSet GetParentSuperGroupNolist();
    }
}