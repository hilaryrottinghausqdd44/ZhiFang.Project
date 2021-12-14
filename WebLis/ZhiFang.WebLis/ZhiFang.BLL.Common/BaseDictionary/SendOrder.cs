using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.IBLL.Common.BaseDictionary;
using System.Data;

namespace ZhiFang.BLL.Common.BaseDictionary
{
    public partial class SendOrder : IBSendOrder
    {
        IDAL.IDSendOrder dal;
        public SendOrder()
        {
            dal = DALFactory.DalFactory<IDAL.IDSendOrder>.GetDal("SendOrder", ZhiFang.Common.Dictionary.DBSource.LisDB());
        }

        public bool IsExist(string OrderNo)
        {
            return dal.Exists(OrderNo);
        }
        public string GetOrderNo(string LabCode, string Operdate)
        {
            string LastNum = dal.ExecStoredProcedure(LabCode, Operdate);
            string tempLabCode = string.Empty;
            if (LabCode.Length == 1)
                tempLabCode = "00" + LabCode;
            else if (LabCode.Length == 2)
                tempLabCode = "0" + LabCode;
            else
                tempLabCode = LabCode;
            //Order 输入查询条件不方便 改成O  ganwh edit 2015-11-11
            return "O" + tempLabCode + DateTime.Now.ToString("yyMMdd") + LastNum;
        }

        public string GetMaxBarCodeOrderNum(string LabCode, string Operdate)
        {
            return dal.ExecStoredProcedure(LabCode, Operdate);
        }

        public int Add(Model.SendOrder model)
        {
            return dal.Add(model);
        }

        public int Update(Model.SendOrder model)
        {
            return dal.Update(model);
        }

        public int Delete(string OrderNo)
        {
            return dal.Delete(OrderNo);
        }

        public System.Data.DataSet GetList(Model.SendOrder model)
        {
            return dal.GetList(model);
        }

        public Model.SendOrder GetModel(string OrderNo)
        {
            return dal.GetModel(OrderNo);
        }
        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.SendOrder model)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetListByPage(Model.SendOrder t, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        public List<Model.SendOrder> DataTableToList(DataTable dt)
        {
            List<Model.SendOrder> modelList = new List<Model.SendOrder>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.SendOrder model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.SendOrder();
                    if (dt.Columns.Contains("OrderNo") && dt.Rows[n]["OrderNo"].ToString() != "")
                    {
                        model.OrderNo = dt.Rows[n]["OrderNo"].ToString();
                    }
                    if (dt.Columns.Contains("CreateMan") && dt.Rows[n]["CreateMan"].ToString() != "")
                    {
                        model.CreateMan = dt.Rows[n]["CreateMan"].ToString();
                    }
                    if (dt.Columns.Contains("CreateDate") && dt.Rows[n]["CreateDate"].ToString() != "")
                    {
                        model.CreateDate = dt.Rows[n]["CreateDate"].ToString();
                    }
                    if (dt.Columns.Contains("SampleNum") && dt.Rows[n]["SampleNum"].ToString() != "")
                    {
                        model.SampleNum = int.Parse(dt.Rows[n]["SampleNum"].ToString());
                    }
                    if (dt.Columns.Contains("Status") && dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    if (dt.Columns.Contains("Note") && dt.Rows[n]["Note"].ToString() != "")
                    {
                        model.Note = dt.Rows[n]["Note"].ToString();
                    }
                    if (dt.Columns.Contains("LabCode") && dt.Rows[n]["LabCode"].ToString() != "")
                    {
                        model.LabCode = dt.Rows[n]["LabCode"].ToString();
                    }
                    if (dt.Columns.Contains("IsConfirm") && dt.Rows[n]["IsConfirm"].ToString() != "")
                    {
                        model.IsConfirm = int.Parse(dt.Rows[n]["IsConfirm"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public List<Model.UiModel.SampleTypeBarCodeInfo> DataTableToSampleTypeBarCode(DataTable dt)
        {
            List<Model.UiModel.SampleTypeBarCodeInfo> modelList = new List<Model.UiModel.SampleTypeBarCodeInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.UiModel.SampleTypeBarCodeInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.UiModel.SampleTypeBarCodeInfo();
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                        model.CName = dt.Rows[n]["CName"].ToString();

                    if (dt.Columns.Contains("BarCode") && dt.Rows[n]["BarCode"].ToString() != "")
                        model.BarCode = dt.Rows[n]["BarCode"].ToString();

                    if (dt.Columns.Contains("ReceiveDate") && dt.Rows[n]["ReceiveDate"].ToString() != "")
                        model.ReceiveDate = DateTime.Parse(dt.Rows[n]["ReceiveDate"].ToString());
                    if (dt.Columns.Contains("CollectDate") && dt.Rows[n]["CollectDate"].ToString() != "")
                        model.CollectDate = DateTime.Parse(dt.Rows[n]["CollectDate"].ToString());
                    if (dt.Columns.Contains("BarCodeFormNo") && dt.Rows[n]["BarCodeFormNo"].ToString() != "")
                        model.BarCodeFormNo = dt.Rows[n]["BarCodeFormNo"].ToString();
                    if (dt.Columns.Contains("OperDate") && dt.Rows[n]["OperDate"].ToString() != "")
                        model.OperDate = DateTime.Parse(dt.Rows[n]["OperDate"].ToString());
                    modelList.Add(model);
                }
            }

            return modelList;
        }

        public List<Model.UiModel.SampleItemInfo> DataTableToSampleItemInfo(DataTable dt)
        {
            List<Model.UiModel.SampleItemInfo> modelList = new List<Model.UiModel.SampleItemInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.UiModel.SampleItemInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.UiModel.SampleItemInfo();
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                        model.CName = dt.Rows[n]["CName"].ToString();

                    if (dt.Columns.Contains("NRequestItemNo") && dt.Rows[n]["NRequestItemNo"].ToString() != "")
                        model.ItemNo = dt.Rows[n]["NRequestItemNo"].ToString();

                    if (dt.Columns.Contains("SampleTypeName") && dt.Rows[n]["SampleTypeName"].ToString() != "")
                        model.SampleTypeName = dt.Rows[n]["SampleTypeName"].ToString();

                    if (dt.Columns.Contains("CheckMethodName") && dt.Rows[n]["CheckMethodName"].ToString() != "")
                        model.CheckMethodName = dt.Rows[n]["CheckMethodName"].ToString();


                    if (dt.Columns.Contains("price") && dt.Rows[n]["price"].ToString() != "")
                        model.Price = dt.Rows[n]["price"].ToString();
                    if (dt.Columns.Contains("Unit") && dt.Rows[n]["Unit"].ToString() != "")
                        model.ItemUnit = dt.Rows[n]["Unit"].ToString();

                    modelList.Add(model);
                }
            }

            return modelList;
        }

        public int UpdateNoteByOrderNo(string OrderNo, string Note)
        {
            return dal.UpdateNoteByOrderNo(OrderNo, Note);
        }
        public int OrderConFrim(string OrderNo)
        {
            return dal.OrderConFrim(OrderNo);
        }

        public int ConFrimPrint(string OrderNo)
        {
            return dal.ConFrimPrint(OrderNo);
        }
        public Model.SendOrder GetModelByOrderPrint(string OrderNo)
        {
            return dal.GetModelByOrderPrint(OrderNo);
        }

        public DataTable OrderPrintToDataTable(Model.UiModel.OrderPrint orderPrint)
        {
            DataTable dt = new DataTable();
            dt.TableName = "frform";
            dt.Columns.Add("OrderNo", typeof(string));
            dt.Columns.Add("SendLabName", typeof(string));
            dt.Columns.Add("OperEndDate", typeof(string));
            dt.Columns.Add("OperSartDate", typeof(string));
            dt.Columns.Add("ReciveCompany", typeof(string));
            dt.Columns.Add("BarcodeSum", typeof(int));
            DataRow dr = dt.NewRow();
            if (orderPrint.OrderNo != null)
                dr["OrderNo"] = orderPrint.OrderNo;
            if (orderPrint.SendLabName != null)
                dr["SendLabName"] = orderPrint.SendLabName;
            if (orderPrint.OperEndDate != null)
                dr["OperEndDate"] = orderPrint.OperEndDate;
            if (orderPrint.OperSartDate != null)
                dr["OperSartDate"] = orderPrint.OperSartDate;
            if (orderPrint.ReciveCompany != null)
                dr["ReciveCompany"] = orderPrint.ReciveCompany;
            if (orderPrint.BarcodeSum != null) {
                dr["BarcodeSum"] = orderPrint.BarcodeSum;
            }
            dt.Rows.Add(dr);
            return dt;
        }

        public DataTable NrequestFormToDataTable(List<Model.NRequestFormResult> nrequestformlist)
        {
            DataTable dt = new DataTable();

            dt.TableName = "fritem";
            dt.Columns.Add("BarcodeList", typeof(string));
            dt.Columns.Add("GenderName", typeof(string));
            dt.Columns.Add("CName", typeof(string));
            dt.Columns.Add("Age", typeof(string));
            dt.Columns.Add("AgeUnitName", typeof(string));
            dt.Columns.Add("SampleTypeName", typeof(string));
            dt.Columns.Add("DoctorName", typeof(string));
            dt.Columns.Add("OperTime", typeof(string));
            dt.Columns.Add("CollectTime", typeof(string));
            dt.Columns.Add("WebLisSourceOrgName", typeof(string));
            dt.Columns.Add("NRequestFormNo", typeof(string));
            dt.Columns.Add("Diag", typeof(string));
            dt.Columns.Add("ItemList", typeof(string));
            dt.Columns.Add("SickTypeName", typeof(string));
            dt.Columns.Add("PatNo", typeof(string));
            dt.Columns.Add("ColorName", typeof(string));
            dt.Columns.Add("DeptName", typeof(string));
            foreach (var nrequest in nrequestformlist)
            {
                DataRow dr = dt.NewRow();

                if (nrequest.BarcodeList != null)
                    dr["BarcodeList"] = nrequest.BarcodeList;
                if (nrequest.CName != null)
                    dr["CName"] = nrequest.CName;
                if (nrequest.Age != null)
                    dr["Age"] = nrequest.Age;
                if (nrequest.AgeUnitName != null)
                    dr["AgeUnitName"] = nrequest.AgeUnitName;
                if (nrequest.SampleTypeName != null)
                    dr["SampleTypeName"] = nrequest.SampleTypeName;
                if (nrequest.DoctorName != null)
                    dr["DoctorName"] = nrequest.DoctorName;
                if (nrequest.OperTime != null)
                    dr["OperTime"] = nrequest.OperTime;
                if (nrequest.CollectTime != null)
                    dr["CollectTime"] = nrequest.CollectTime;
                if (nrequest.WebLisSourceOrgName != null)
                    dr["WebLisSourceOrgName"] = nrequest.WebLisSourceOrgName;
                if (nrequest.NRequestFormNo != null)
                    dr["NRequestFormNo"] = nrequest.NRequestFormNo;
                if (nrequest.Diag != null)
                    dr["Diag"] = nrequest.Diag;
                if (nrequest.ItemList != null)
                    dr["ItemList"] = nrequest.ItemList;
                if (nrequest.SickTypeName != null)
                    dr["SickTypeName"] = nrequest.SickTypeName;
                if (nrequest.PatNo != null)
                    dr["PatNo"] = nrequest.PatNo;
                if (nrequest.ColorName != null)
                    dr["ColorName"] = nrequest.ColorName;
                if (nrequest.DeptName != null)
                    dr["DeptName"] = nrequest.DeptName;
                if (nrequest.GenderName != null)
                    dr["GenderName"] = nrequest.GenderName;
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
