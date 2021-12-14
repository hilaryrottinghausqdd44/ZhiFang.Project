using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.IBLL.Report;

namespace IBLL
{
    public interface IBMicroOperationStepFlow : IBLLBase<Model.MicroOperationStepFlow> 
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int Id);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int Id);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.MicroOperationStepFlow GetModel(int Id);

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        Model.MicroOperationStepFlow GetModelByCache(int Id);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
    }
}
