using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IBLL.Common;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBSendOrder : IBBase<ZhiFang.Model.SendOrder>, IBDataPage<ZhiFang.Model.SendOrder>
    {

        string GetOrderNo(string LabCode, string Operdate);

        string GetMaxBarCodeOrderNum(string LabCode, string Operdate);

        List<Model.SendOrder> DataTableToList(DataTable dt);
        bool IsExist(string OrderNo);
        List<Model.UiModel.SampleTypeBarCodeInfo> DataTableToSampleTypeBarCode(DataTable dt);
        List<Model.UiModel.SampleItemInfo> DataTableToSampleItemInfo(DataTable dt);
     
        int Delete(string OrderNo);
        int UpdateNoteByOrderNo(string OrderNo, string Note);
        int OrderConFrim(string OrderNo);
        int ConFrimPrint(string OrderNo);
        Model.SendOrder GetModel(string OrderNo);
        Model.SendOrder GetModelByOrderPrint(string OrderNo);
        DataTable OrderPrintToDataTable(Model.UiModel.OrderPrint orderPrint);
        DataTable NrequestFormToDataTable(List<Model.NRequestFormResult> nrequestformlist);
    }
}
