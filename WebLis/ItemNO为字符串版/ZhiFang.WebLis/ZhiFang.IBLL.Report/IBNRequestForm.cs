using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface IBNRequestForm : IBLLBase<Model.NRequestForm>
    {
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string SerialNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(long NRequestFormNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.NRequestForm GetModel(long NRequestFormNo);
        /// <summary>
        /// 得到一个实体（2014-09-10 赵晓龙）
        /// </summary>
        Model.NRequestForm GetModelBySerialNo(string SerialNo);

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        Model.NRequestForm GetModelByCache(long NRequestFormNo);

        string GetNCode(int p);

        DataSet GetListByBarCodeNo(string BarCodeNo);

        DataTable GetAllData(Model.NRequestForm model, int StartPage, int PageSize, out int intPageCount, out int iCount);
        DataTable GetAll(Model.NRequestForm Model);
        bool CheckNReportFormCenter(DataSet dsForm, string DestiOrgID, out string ReturnDescription);
        bool CheckNReportFormLab(DataSet dsForm, string DestiOrgID, out string ReturnDescription);
        DataSet GetListByModel(ZhiFang.Model.NRequestForm NRequestForm, ZhiFang.Model.BarCodeForm BarCodeForm);
        DataSet GetListBy(ZhiFang.Model.NRequestForm model);
        int UpdateByList(List<string> listStrColumnNf, List<string> listStrDataNf);
        int AddByList(List<string> listStrColumnNf, List<string> listStrDataNf);
        List<ZhiFang.Model.StaticRecOrgSamplePrice> OperatorWorkDataTableToList(DataTable dt);
        DataTable GetNRequstFormList(Model.NRequestForm nrf_m, int StartPage, int PageSize, out int intPageCount, out int iCount);
        DataTable GetNRequstFormList2(Model.NRequestForm nrf_m, int StartPage, int PageSize, out int intPageCount, out int iCount);
        string GetBarCodeByNRequestFormNo(string p);

        string GetBarCodeByNRequestFormNo(string nrequestformno, string barCode, out string barcodeformno, out string colorname, out string itemname, out string itemno, out string samplytypename);
        string GetBarCodeByNRequestFormNo(string nrequestformno, out string barcodeformno, out string colorname, out string itemname, out string itemno, out string samplytypename);
        bool CheckNReportFormStatus(long NRequestFromNo);

        bool AddNrequest(Model.NRequestForm nrf_m, List<Model.BarCodeForm> bcf_List);
        bool UpdateNrequest(Model.NRequestForm nrf_m, List<Model.BarCodeForm> bcf_List);
        bool UpdatePrintTimesByNrequestNo(long nrequestFormNo);
        List<Model.UiModel.SampleTypeBarCodeInfo> GetBarCodeAndCNameByNReuqestFormNo(DataTable NrequestFformDs);
        //工作量统计
        DataSet GetStaticRecOrgSamplePrice(Model.StaticRecOrgSamplePrice model, int rows, int page);
        List<ZhiFang.Model.StaticRecOrgSamplePrice> DataTableToList(DataTable dt);
        DataSet GetStaticRecOrgSamplePrice(Model.StaticRecOrgSamplePrice model);

        DataSet GetBarcodePrice(Model.StaticRecOrgSamplePrice model, int rows, int page);
        DataSet GetBarcodePrice(Model.StaticRecOrgSamplePrice model);
        DataSet GetOpertorWorkCount(Model.StaticRecOrgSamplePrice model, int rows, int page);


        //个人检验信息统计
        DataSet GetStaticPersonTestItemPriceList(int rows, int page, Model.StaticPersonTestItemPrice model);
        DataSet GetStaticPersonTestItemPriceList(Model.StaticPersonTestItemPrice model);
        List<Model.StaticPersonTestItemPrice> GetStaticPersonDataTableToList(DataTable dt);
        int Add(string strSql);
        DataSet GetList(string strWhere);
        DataSet GetRefuseList(string strSql);

        List<Model.StaticRecOrgSamplePrice> BarcodeDataTableToList(DataTable dataTable);
        int Add_PKI(Model.NRequestForm model);
        DataTable GetNRequstFormListByDetailsAndPage(Model.NRequestForm nrf_m, int StartPage, int PageSize, out int intPageCount, out int iCount);
    }
}
