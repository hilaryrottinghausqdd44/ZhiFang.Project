using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层NRequestForm
    /// </summary>
    public interface IDNRequestForm : IDAL.IDataBase<ZhiFang.Model.NRequestForm>, IDAL.IDataPage<ZhiFang.Model.NRequestForm>
    {
        #region  成员方法

        int Delete(string SerialNo);
        int Delete(long NRequestFormNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.NRequestForm GetModel(long NRequestFormNo);
        #endregion  成员方法

        Model.NRequestForm GetModelBySerialNo(string SerialNo);
        DataSet GetListByBarCodeNo(string BarCodeNo);
        DataSet GetListByModel(ZhiFang.Model.NRequestForm NRequestForm, ZhiFang.Model.BarCodeForm BarCodeForm);
        DataSet GetListBy(ZhiFang.Model.NRequestForm model);
        int UpdateByList(List<string> listStrColumnNf, List<string> listStrDataNf);
        int AddByList(List<string> listStrColumnNf, List<string> listStrDataNf);


        int GetNRequstFormListTotalCount(Model.NRequestForm model);
        int GetNRequstFormListTotalCount2(Model.NRequestForm model);

        DataSet GetNRequstFormListByPage(Model.NRequestForm model, int StartPage, int PageSize);
        DataSet GetNRequstFormListByPage2(Model.NRequestForm model, int StartPage, int PageSize);
        DataTable GetBarCodeAndCNameByNRequestFormNo(string n);
        DataTable GetBarCodeByNRequestFormNo(string p);
        DataTable GetBarCodeByNRequestFormNo(string p, string barCode);
        int CheckNReportFormWeblisFlag(long NRequestFormNo);

        //工作量报表
        DataSet GetStaticRecOrgSamplePrice(Model.StaticRecOrgSamplePrice model, int rows, int page);
        DataSet GetStaticRecOrgSamplePrice(Model.StaticRecOrgSamplePrice model);

        DataSet GetBarcodePrice(Model.StaticRecOrgSamplePrice model,int rows,int page);
        DataSet GetBarcodePrice(Model.StaticRecOrgSamplePrice model);

        //个人检验信息统计
        DataSet GetStaticPersonTestItemPriceList(int page, int rows, ZhiFang.Model.StaticPersonTestItemPrice model);
        DataSet GetStaticPersonTestItemPriceList(ZhiFang.Model.StaticPersonTestItemPrice model);
        int Add(string strSql);
        DataSet GetList(string strWhere);
        DataSet GetRefuseList(string strSql);
        DataSet GetOpertorWorkCount(Model.StaticRecOrgSamplePrice model, int rows, int page);
        int Add_PKI(ZhiFang.Model.NRequestForm model);

        DataSet GetNRequstFormListByDetailsAndPage(Model.NRequestForm model, int StartPage, int PageSize);
        int GetNRequstFormListByDetailsTotalCount(Model.NRequestForm model);
    }
}
