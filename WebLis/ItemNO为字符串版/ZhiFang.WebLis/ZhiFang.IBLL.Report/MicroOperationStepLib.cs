using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IBLL.Report;
using System.Data;

namespace IBLL
{
    public interface IBMicroOperationStepLib : IBLLBase<Model.MicroOperationStepLib> 
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

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        Model.MicroOperationStepLib GetModelByCache(int Id);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
    }
}
