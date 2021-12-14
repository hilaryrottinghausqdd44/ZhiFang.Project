using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Model.UiModel;
namespace ZhiFang.IBLL.Report
{
    /// <summary>
    /// NRequestItem
    /// </summary>
    public interface IBNRequestItem : IBLLBase<Model.NRequestItem>
    {
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int NRequestItemNo);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int NRequestItemNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool DeleteList(string NRequestItemNolist);
        /// <summary>
        /// 按申请单NRequestFormNo删除数据
        /// </summary>
        bool DeleteList_ByNRequestFormNo(long NRequestItemNolist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.NRequestItem GetModel(int NRequestItemNo);

        #endregion  Method

        DataSet GetList_By_NRequestFormNo(long NRequestFormNo);
        DataSet GetList_By_NRequestFormNo_ClientNo(long NRequestFormNo, string LabCode);

        bool CheckNReportItemCenter(DataSet dsItem, string DestiOrgID, out string ReturnDescription);
        bool CheckNReportItemLab(DataSet dsItem, string DestiOrgID, out string ReturnDescription);
        int UpdateByList(List<string> listStrColumnNi, List<string> listStrDataNi);
        int AddByList(List<string> listStrColumnNi, List<string> listStrDataNi);
        DataSet GetNrequestItemByNrequestNo(long nrequestFormNo);

        List<Model.NRequestItem> SetNrequestItemAndBarCodeForm(List<UiBarCode> barcodelist, Model.NRequestForm nrequestForm, string collecter, int emplID, out List<Model.BarCodeForm> bcf_List);
        List<Model.NRequestItem> SetNrequestItemAndBarCodeForm_TaiHe(List<UiBarCode> barcodelist, Model.NRequestForm nrequestForm, string collecter, int emplID, out List<Model.BarCodeForm> bcf_List);
        bool AddNrequestItem(List<Model.NRequestItem> nri_List);
        bool AddNrequestItem_TaiHe(List<Model.NRequestItem> nri_List);
        bool UpdateNrequestItem(List<Model.NRequestItem> nri_List, long nRequestFormNo);
        bool UpdateNrequestItem_TaiHe(List<Model.NRequestItem> nri_List, long nRequestFormNo);
        List<UiCombiItem> GetUiCombiItemByNrequestForm(Model.NRequestForm nrf_m);
        List<UiCombiItem> GetUiCombiItemByNrequestForm_TaiHe(Model.NRequestForm nrf_m);
        DataSet GetNrequestItemByBarCodeFormNo(string BarCodeFormNo);
        int Add(string strSql);
        DataSet GetRefuseList(string strSql);
        DataSet GetList_TaiHe(ZhiFang.Model.NRequestItem model);
        DataSet GetList_PKI(string strWhere);
    }
}

