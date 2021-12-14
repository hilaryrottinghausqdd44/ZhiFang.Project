using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BReportForm
    {
        private readonly IDReportForm dal = DalFactory<IDReportForm>.GetDal("ReportForm");
        public BReportForm()
        { }
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string FormNo)
        {
            return dal.Exists(FormNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.ReportForm model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.ReportForm model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string FormNo)
        {

            return dal.Delete(FormNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ReportForm GetModel(string FormNo)
        {

            return dal.GetModel(FormNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Model.ReportForm GetModelByCache(string FormNo)
        {

            string CacheKey = "ReportFormModel-" + FormNo;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(FormNo);
                    if (objModel != null)
                    {
                        int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.ReportForm)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        public DataSet GetListByFormNo(string strWhere)
        {
            return dal.GetListByFormNo(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.ReportForm model)
        {
            return dal.GetList(model);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.ReportForm model,DateTime? starday,DateTime? endday)
        {
            return dal.GetList(model,starday,endday);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.ReportForm> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.ReportForm> GetModelList(Model.ReportForm model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.ReportForm> DataTableToList(DataTable dt)
        {
            List<Model.ReportForm> modelList = new List<Model.ReportForm>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.ReportForm model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.ReportForm();
                    if (dt.Rows[n]["ReceiveDate"].ToString() != "")
                    {
                        model.ReceiveDate = DateTime.Parse(dt.Rows[n]["ReceiveDate"].ToString());
                    }
                    if (dt.Rows[n]["SectionNo"].ToString() != "")
                    {
                        model.SectionNo = int.Parse(dt.Rows[n]["SectionNo"].ToString());
                    }
                    if (dt.Rows[n]["TestTypeNo"].ToString() != "")
                    {
                        model.TestTypeNo = int.Parse(dt.Rows[n]["TestTypeNo"].ToString());
                    }
                    model.SampleNo = dt.Rows[n]["SampleNo"].ToString();
                    if (dt.Rows[n]["StatusNo"].ToString() != "")
                    {
                        model.StatusNo = int.Parse(dt.Rows[n]["StatusNo"].ToString());
                    }
                    if (dt.Rows[n]["SampleTypeNo"].ToString() != "")
                    {
                        model.SampleTypeNo = int.Parse(dt.Rows[n]["SampleTypeNo"].ToString());
                    }
                    model.PatNo = dt.Rows[n]["PatNo"].ToString();
                    model.CName = dt.Rows[n]["CName"].ToString();
                    if (dt.Rows[n]["GenderNo"].ToString() != "")
                    {
                        model.GenderNo = int.Parse(dt.Rows[n]["GenderNo"].ToString());
                    }
                    if (dt.Rows[n]["Birthday"].ToString() != "")
                    {
                        model.Birthday = DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
                    }
                    if (dt.Rows[n]["Age"].ToString() != "")
                    {
                        model.Age = Convert.ToDouble(dt.Rows[n]["Age"].ToString());
                    }
                    if (dt.Rows[n]["AgeUnitNo"].ToString() != "")
                    {
                        model.AgeUnitNo = int.Parse(dt.Rows[n]["AgeUnitNo"].ToString());
                    }
                    if (dt.Rows[n]["FolkNo"].ToString() != "")
                    {
                        model.FolkNo = int.Parse(dt.Rows[n]["FolkNo"].ToString());
                    }
                    if (dt.Rows[n]["DistrictNo"].ToString() != "")
                    {
                        model.DistrictNo = int.Parse(dt.Rows[n]["DistrictNo"].ToString());
                    }
                    if (dt.Rows[n]["WardNo"].ToString() != "")
                    {
                        model.WardNo = int.Parse(dt.Rows[n]["WardNo"].ToString());
                    }
                    model.Bed = dt.Rows[n]["Bed"].ToString();
                    if (dt.Rows[n]["DeptNo"].ToString() != "")
                    {
                        model.DeptNo = int.Parse(dt.Rows[n]["DeptNo"].ToString());
                    }
                    model.Doctor = dt.Rows[n]["Doctor"].ToString();
                    if (dt.Rows[n]["ChargeNo"].ToString() != "")
                    {
                        model.ChargeNo = int.Parse(dt.Rows[n]["ChargeNo"].ToString());
                    }
                    if (dt.Rows[n]["Charge"].ToString() != "")
                    {
                        model.Charge = decimal.Parse(dt.Rows[n]["Charge"].ToString());
                    }
                    model.Collecter = dt.Rows[n]["Collecter"].ToString();
                    if (dt.Rows[n]["CollectDate"].ToString() != "")
                    {
                        model.CollectDate = DateTime.Parse(dt.Rows[n]["CollectDate"].ToString());
                    }
                    if (dt.Rows[n]["CollectTime"].ToString() != "")
                    {
                        model.CollectTime = DateTime.Parse(dt.Rows[n]["CollectTime"].ToString());
                    }
                    model.FormMemo = dt.Rows[n]["FormMemo"].ToString();
                    model.Technician = dt.Rows[n]["Technician"].ToString();
                    if (dt.Rows[n]["TestDate"].ToString() != "")
                    {
                        model.TestDate = DateTime.Parse(dt.Rows[n]["TestDate"].ToString());
                    }
                    if (dt.Rows[n]["TestTime"].ToString() != "")
                    {
                        model.TestTime = DateTime.Parse(dt.Rows[n]["TestTime"].ToString());
                    }
                    model.Operator = dt.Rows[n]["Operator"].ToString();
                    if (dt.Rows[n]["OperDate"].ToString() != "")
                    {
                        model.OperDate = DateTime.Parse(dt.Rows[n]["OperDate"].ToString());
                    }
                    if (dt.Rows[n]["OperTime"].ToString() != "")
                    {
                        model.OperTime = DateTime.Parse(dt.Rows[n]["OperTime"].ToString());
                    }
                    model.Checker = dt.Rows[n]["Checker"].ToString();
                    if (dt.Rows[n]["CheckDate"].ToString() != "")
                    {
                        model.CheckDate = DateTime.Parse(dt.Rows[n]["CheckDate"].ToString());
                    }
                    if (dt.Rows[n]["CheckTime"].ToString() != "")
                    {
                        model.CheckTime = DateTime.Parse(dt.Rows[n]["CheckTime"].ToString());
                    }
                    model.SerialNo = dt.Rows[n]["SerialNo"].ToString();
                    model.BarCode = dt.Rows[n]["BarCode"].ToString();
                    model.RequestSource = dt.Rows[n]["RequestSource"].ToString();
                    if (dt.Rows[n]["DiagNo"].ToString() != "")
                    {
                        model.DiagNo = int.Parse(dt.Rows[n]["DiagNo"].ToString());
                    }
                    if (dt.Rows[n]["PrintTimes"].ToString() != "")
                    {
                        model.PrintTimes = int.Parse(dt.Rows[n]["PrintTimes"].ToString());
                    }
                    if (dt.Rows[n]["SickTypeNo"].ToString() != "")
                    {
                        model.SickTypeNo = int.Parse(dt.Rows[n]["SickTypeNo"].ToString());
                    }
                    model.FormComment = dt.Rows[n]["FormComment"].ToString();
                    model.zdy1 = dt.Rows[n]["zdy1"].ToString();
                    model.zdy2 = dt.Rows[n]["zdy2"].ToString();
                    model.zdy3 = dt.Rows[n]["zdy3"].ToString();
                    model.zdy4 = dt.Rows[n]["zdy4"].ToString();
                    model.zdy5 = dt.Rows[n]["zdy5"].ToString();
                    if (dt.Rows[n]["inceptdate"].ToString() != "")
                    {
                        model.inceptdate = DateTime.Parse(dt.Rows[n]["inceptdate"].ToString());
                    }
                    if (dt.Rows[n]["incepttime"].ToString() != "")
                    {
                        model.incepttime = DateTime.Parse(dt.Rows[n]["incepttime"].ToString());
                    }
                    model.incepter = dt.Rows[n]["incepter"].ToString();
                    if (dt.Rows[n]["onlinedate"].ToString() != "")
                    {
                        model.onlinedate = DateTime.Parse(dt.Rows[n]["onlinedate"].ToString());
                    }
                    if (dt.Rows[n]["onlinetime"].ToString() != "")
                    {
                        model.onlinetime = DateTime.Parse(dt.Rows[n]["onlinetime"].ToString());
                    }
                    if (dt.Rows[n]["bmanno"].ToString() != "")
                    {
                        model.bmanno = int.Parse(dt.Rows[n]["bmanno"].ToString());
                    }
                    if (dt.Rows[n]["clientno"].ToString() != "")
                    {
                        model.clientno = int.Parse(dt.Rows[n]["clientno"].ToString());
                    }
                    model.chargeflag = dt.Rows[n]["chargeflag"].ToString();
                    if (dt.Rows[n]["isReceive"].ToString() != "")
                    {
                        model.isReceive = int.Parse(dt.Rows[n]["isReceive"].ToString());
                    }
                    model.ReceiveMan = dt.Rows[n]["ReceiveMan"].ToString();
                    if (dt.Rows[n]["ReceiveTime"].ToString() != "")
                    {
                        model.ReceiveTime = DateTime.Parse(dt.Rows[n]["ReceiveTime"].ToString());
                    }
                    model.concessionNum = dt.Rows[n]["concessionNum"].ToString();
                    model.Sender2 = dt.Rows[n]["Sender2"].ToString();
                    if (dt.Rows[n]["SenderTime2"].ToString() != "")
                    {
                        model.SenderTime2 = DateTime.Parse(dt.Rows[n]["SenderTime2"].ToString());
                    }
                    if (dt.Rows[n]["resultstatus"].ToString() != "")
                    {
                        model.resultstatus = int.Parse(dt.Rows[n]["resultstatus"].ToString());
                    }
                    model.testaim = dt.Rows[n]["testaim"].ToString();
                    if (dt.Rows[n]["resultprinttimes"].ToString() != "")
                    {
                        model.resultprinttimes = int.Parse(dt.Rows[n]["resultprinttimes"].ToString());
                    }
                    model.paritemname = dt.Rows[n]["paritemname"].ToString();
                    model.clientprint = dt.Rows[n]["clientprint"].ToString();
                    model.resultsend = dt.Rows[n]["resultsend"].ToString();
                    model.reportsend = dt.Rows[n]["reportsend"].ToString();
                    model.CountNodesFormSource = dt.Rows[n]["CountNodesFormSource"].ToString();
                    if (dt.Rows[n]["commsendflag"].ToString() != "")
                    {
                        model.commsendflag = int.Parse(dt.Rows[n]["commsendflag"].ToString());
                    }
                    model.ZDY6 = dt.Rows[n]["ZDY6"].ToString();
                    model.ZDY7 = dt.Rows[n]["ZDY7"].ToString();
                    model.ZDY8 = dt.Rows[n]["ZDY8"].ToString();
                    model.ZDY9 = dt.Rows[n]["ZDY9"].ToString();
                    model.ZDY10 = dt.Rows[n]["ZDY10"].ToString();
                    if (dt.Rows[n]["PrintDateTime"].ToString() != "")
                    {
                        model.PrintDateTime = DateTime.Parse(dt.Rows[n]["PrintDateTime"].ToString());
                    }
                    model.PrintOper = dt.Rows[n]["PrintOper"].ToString();
                    if (dt.Rows[n]["FormNo"].ToString() != "")
                    {
                        model.FormNo = dt.Rows[n]["FormNo"].ToString();
                    }
                    if (dt.Rows[n]["FormStateNo"].ToString() != "")
                    {
                        model.FormStateNo = int.Parse(dt.Rows[n]["FormStateNo"].ToString());
                    }
                    model.OldSerialNo = dt.Rows[n]["OldSerialNo"].ToString();
                    if (dt.Rows[n]["mresulttype"].ToString() != "")
                    {
                        model.mresulttype = int.Parse(dt.Rows[n]["mresulttype"].ToString());
                    }
                    model.Diagnose = dt.Rows[n]["Diagnose"].ToString();
                    model.TestPurpose = dt.Rows[n]["TestPurpose"].ToString();
                    if (dt.Rows[n]["IsFree"].ToString() != "")
                    {
                        model.IsFree = int.Parse(dt.Rows[n]["IsFree"].ToString());
                    }
                    model.NOperator = dt.Rows[n]["NOperator"].ToString();
                    if (dt.Rows[n]["NOperDate"].ToString() != "")
                    {
                        model.NOperDate = DateTime.Parse(dt.Rows[n]["NOperDate"].ToString());
                    }
                    if (dt.Rows[n]["NOperTime"].ToString() != "")
                    {
                        model.NOperTime = DateTime.Parse(dt.Rows[n]["NOperTime"].ToString());
                    }
                    model.PathologyNo = dt.Rows[n]["PathologyNo"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        internal bool UpdatePageInfo(string reportformlist, string pageCount, string pageName)
        {
            return dal.UpdatePageInfo(reportformlist, pageCount, pageName);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        /// <summary>
        /// 根据FormNo数组返回ReportForm列表
        /// </summary>
        /// <param name="FormNo">FormNo数组</param>
        /// <returns></returns>
        public DataTable GetReportFormList(string[] FormNo)
        {
            return dal.GetReportFormList(FormNo);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        public DataTable GetListByDataSource(string FormNo)
        {
            return dal.GetReportFormFullList(FormNo);
        }      

        public bool UpdatePrintTimes(string[] reportformlist,string uluserCName)
        {
            return dal.UpdatePrintTimes( reportformlist,uluserCName);
        }
        public bool UpdateClientPrintTimes(string[] reportformlist)
        {
            return dal.UpdateClientPrintTimes(reportformlist);
        }

        //返回普通生化类合并报告数据_视图ReportFormAllQueryDataSource
        public DataSet GetReporFormAllByReportFormIdList(List<string> IdList, string fields, string strWhere)
        {
            return dal.GetReporFormAllByReportFormIdList(IdList, fields, strWhere);
           
        }

        public DataSet GetReportFromByReportFormID(List<string> IdList, string fields, string strWhere)
        {
            return dal.GetReportFromByReportFormID(IdList, fields, strWhere);

        }

        //清除自助打印次数
        public int UpdateClientPrint(string formno)
        {
            return dal.UpdateClientPrint(formno);
        }

        public int UpdatePrintTimes(string formno)
        {
            return dal.UpdatePrintTimes(formno);
        }

        public DataTable GetReportFormListByFormId(string formNo) {
            return dal.GetReportFormListByFormId(formNo);
        }

        /// <summary>
        /// 打印样本清单查询数据
        /// </summary>
        /// <param name="IdList"></param>
        /// <param name="fields"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetSampleReportFromByReportFormID(List<string> IdList, string fields, string strWhere)
        {
            return dal.GetSampleReportFromByReportFormID(IdList, fields, strWhere);

        }

        public DataSet GetReportFormFullByReportFormID(string ReportFormID) {

            return dal.GetReportFormFullByReportFormID(ReportFormID);
        }

    
        public int UpdateReportFormFull(Model.ReportFormFull model)
        {
            return dal.UpdateReportFormFull(model);
        }

        public DataSet GetRepotFormByReportFormIDGroupByZdy15(string PatNo,string zdy15) {
            return dal.GetRepotFormByReportFormIDGroupByZdy15(PatNo,zdy15);
        }

        public DataSet GetRepotFormByReportFormIDAndZdy15AndReceiveDate(string PatNo, string zdy15,string ReceiveDate)
        {
            return dal.GetRepotFormByReportFormIDAndZdy15AndReceiveDate(PatNo, zdy15, ReceiveDate);
        }

        public DataSet GetListTopByWhereAndSort(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListTopByWhereAndSort(Top, strWhere, filedOrder);
        }
        #endregion
    }
}
