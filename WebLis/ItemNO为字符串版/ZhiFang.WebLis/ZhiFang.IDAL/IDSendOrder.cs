using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang;
using System.Data;
namespace ZhiFang.IDAL
{
    public interface IDSendOrder : IDataBase<Model.SendOrder>, IDataPage<Model.SendOrder>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string OrderNo);
      
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string OrderNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.SendOrder GetModel(string OrderNo);
       
        DataSet GetList(ZhiFang.Model.SendOrder model);

        string ExecStoredProcedure(string LabCode, string Operdate);

        int UpdateNoteByOrderNo(string OrderNo, string Note);

        int OrderConFrim(string OrderNo);

        int ConFrimPrint(string OrderNo);
        Model.SendOrder GetModelByOrderPrint(string OrderNo);
    }
}
