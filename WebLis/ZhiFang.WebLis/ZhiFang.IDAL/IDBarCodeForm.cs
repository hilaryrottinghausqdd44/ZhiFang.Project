using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层IDBarCodeForm

    /// </summary>
    public interface IDBarCodeForm : IDataBase<ZhiFang.Model.BarCodeForm>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long BarCodeFormNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(long BarCodeFormNo);

        DataSet GetWeblisOrgName(ZhiFang.Model.BarCodeForm model);
        DataSet GetAllList(ZhiFang.Model.BarCodeForm model);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.BarCodeForm GetModel(long BarCodeFormNo);

        #endregion  成员方法

        string GetNewBarCode(string ClientNo);

        int UpdateByBarCode(Model.BarCodeForm barCodeForm);
        int UpdatePrintFlag(Model.BarCodeForm model);

        int UpdateByBarCodeFormNo(Model.BarCodeForm barCode);
        DataSet GetBarCodeView(string BarCode);
        int UpdateByList(List<string> lisStrColumn, List<string> lisStrData);
        int AddByList(List<string> lisStrColumn, List<string> lisStrData);


        DataSet GetListByNRequestFormNo(long p);

        int DeleteList_ByNRequestFormNo(long NRequestFormNo);
        DataSet GetBarCodeByOrderNo(string OrderNo);
        int UpdateByOrderNo(Model.BarCodeForm barCode);
        bool OtherOrderUser(string BarCode);
        int UpdateOrderNoByBarCodeFormNo(string BarCodeFormNo, string OrderNo);
        int Add(string strSql);
        DataSet GetRefuseList(string strSql);
        int Add_TaiHe(ZhiFang.Model.BarCodeForm model);
        int Update_TaiHe(ZhiFang.Model.BarCodeForm model);

        int UpdateWebLisFlagByBarCode(string WebLisFlag, string BarCode, string WebLisOrgID);
        int SendBarCodeFromByBarCodeList(string barCodeList, string userId, string employeeName);
        int DeliveryBarCodeFromByBarCodeList(string barCodeList, string userId, string employeeName, bool flag, string reason);
        int ReceiveBarCodeFromByBarCodeList(string barCodeList, string userId, string employeeName, bool flag, string reason, string weblisorgid, string weblisorgname);
    }
}