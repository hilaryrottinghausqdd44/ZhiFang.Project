using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    /// <summary>
    /// 接口层IPGroupPrint 的摘要说明。
    /// </summary>
    public interface IDPGroupPrint : IDataBase<Model.PGroupPrint>
    {
        #region  成员方法
        //int PrintFormatno(int sectionno,int clientno,string  itemno);
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
        Model.PGroupPrint GetModel(int Id);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList_No_Name(Model.PGroupPrint model);
        #endregion  成员方法
    }
}
