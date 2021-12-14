using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using ZhiFang.Model.UiModel;

namespace ZhiFang.IBLL.Report
{
    public interface IBBarCodeForm : IBLLBase<Model.BarCodeForm>
    {
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long BarCodeFormNo);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(ZhiFang.Model.BarCodeForm model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(long BarCodeFormNo);
        DataSet GetWeblisOrgName(ZhiFang.Model.BarCodeForm model);
        DataSet GetAllList(ZhiFang.Model.BarCodeForm model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool DeleteList(string BarCodeFormNolist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.BarCodeForm GetModel(long BarCodeFormNo);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        List<ZhiFang.Model.BarCodeForm> DataTableToList(DataTable dt);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetAllList();

        /// <summary>
        /// 分页获取数据列表
        /// </summary>		
        string GetNewBarCode(string ClientNo);
        /// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(Model.BarCodeForm model);
        #endregion  Method

        int UpdateByBarCode(Model.BarCodeForm barCodeForm);
        int UpdatePrintFlag(Model.BarCodeForm model);
        int UpdateWebLisFlagByBarCode(string WebLisFlag, string BarCode, string WebLisOrgID);
        string GetNewBarCodeFormNo(int client);
        DataSet GetBarCodeView(string BarCode);
        bool CheckBarCodeCenter(DataSet dsBarCodeForm, string DestiOrgID, out string ReturnDescription);
        bool CheckBarCodeLab(DataSet dsBarCodeForm, string DestiOrgID, out string ReturnDescription);
        int UpdateByList(List<string> lisStrColumn, List<string> lisStrData);
        int AddByList(List<string> lisStrColumn, List<string> lisStrData);

        bool DeleteBarCodeByNRequestFormNo(string NRequestFormNo);
        //List<Model.BarCodeForm> ConvertBarCodeFormList(List<UiBarCode> barCodeList, string webLisOrgID, Model.NRequestForm nrf_m, string collecter, int emplID, int flag, List<Model.NRequestItem> nri_List);
        bool AddBarCodeForm(List<Model.BarCodeForm> bcf_List);
        bool AddBarCodeForm_TaiHe(List<Model.BarCodeForm> bcf_List);
        List<UiBarCode> ConvertBarCodeFormToUiBarCode(List<Model.BarCodeForm> bcf_List, Model.NRequestForm nrf_m);
        List<UiBarCode> ConvertBarCodeFormToUiBarCode_TaiHe(List<Model.BarCodeForm> bcf_List, Model.NRequestForm nrf_m);
        bool UpdateBarCodeForm(List<Model.BarCodeForm> bcf_List);
        bool UpdateBarCodeForm_TaiHe(List<Model.BarCodeForm> bcf_List);
        List<UiBarCode> GetUiBarCodeListByNrequestFormNo(long nrequestFormNo);
        List<UiBarCode> GetUiBarCodeListByNrequestFormNo_TaiHe(long nrequestFormNo);
        List<UiBarCode> GetBatchUiBarCodeListByNrequestFormNo(long nrequestFormNo);
        DataSet GetBarCodeByOrderNo(string OrderNo);
        int UpdateByOrderNo(Model.BarCodeForm barCode);
        bool OtherOrderUser(string BarCode);
        int UpdateOrderNoByBarCodeFormNo(string BarCodeFormNo, string OrderNo);
        int Add(string strSql);
        DataSet GetRefuseList(string strSql);
        /// <summary>
        /// 条码是否存在
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="barcodelist"></param>
        /// <returns></returns>
        bool IsExistBarCode(string flag, List<UiBarCode> barcodelist, out string repeatbarcodestr);
    }
}
