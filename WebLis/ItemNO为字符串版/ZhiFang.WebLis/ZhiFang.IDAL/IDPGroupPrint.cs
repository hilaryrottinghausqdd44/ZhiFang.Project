using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;

namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层IPGroupPrint 的摘要说明。
    /// </summary>
    public interface IDPGroupPrint : IDataBase<ZhiFang.Model.PGroupPrint>
    {
        #region  成员方法
        //int PrintFormatno(int sectionno,int clientno,string  itemno);
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string Id);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string Id);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.PGroupPrint GetModel(string Id);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList_No_Name(Model.PGroupPrint model);
        #endregion  成员方法

        DataSet GetListByPage(PGroupPrint model, int nowPageNum, int nowPageSize);
        DataSet GetList_No_Name(ZhiFang.Model.PGroupFormat model, int nowPageNum, int nowPageSize);
        int GetTotalCount(Model.PGroupFormat model);
        DataSet GetModelByID(string id);
    }
}
