using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data;

namespace IDAL
{
    public interface IDMicroOperationStepLib : IDataBase<Model.MicroOperationStepLib> 
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int Id);
        /// <summary>
        /// 取得最大Sort
        /// </summary>
        /// <param name="SampleTypeNo"></param>
        /// <param name="ParentStepId"></param>
        /// <returns></returns>
        int GetMaxSort(int SampleTypeNo, int ParentStepId);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int Id);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.MicroOperationStepLib GetModel(int Id);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
    }
}
