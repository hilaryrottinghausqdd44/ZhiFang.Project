using System;
using System.Data;
using System.Collections.Generic;

namespace ZhiFang.IDAL
{
    public interface IDNRequestItem : IDataBase<ZhiFang.Model.NRequestItem>, IDataPage<ZhiFang.Model.NRequestItem>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int NRequestItemNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int NRequestItemNo);

        bool DeleteList(string NRequestItemNolist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.NRequestItem GetModel(int NRequestItemNo);
        #endregion  成员方法

        bool DeleteList_ByNRequestFormNo(long NRequestItemNolist);

        DataSet GetList_ClientNo(Model.NRequestItem nRequestItem);

        int UpdateByBarcodeFormNo(Model.NRequestItem nRequestItem);
        int UpdateByList(List<string> listStrColumnNi, List<string> listStrDataNi);
        int AddByList(List<string> listStrColumnNi, List<string> listStrDataNi);
        DataSet GetNrequestItemByNrequestNo(long nrequestFormNo);
        DataSet GetNrequestItemByBarCodeFormNo(string BarCodeFormNo);
        int Add(string strSql);
        DataSet GetRefuseList(string strSql);
        int Add_TaiHe(ZhiFang.Model.NRequestItem model);
        int Update_TaiHe(ZhiFang.Model.NRequestItem model);
        DataSet GetCombiItemByNrequestFormNo(long nrequestFormNo);
        DataSet GetList_TaiHe(ZhiFang.Model.NRequestItem model);
        DataSet GetList_PKI(string strWhere);
    }
}
