using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BReportItem
    {
        private readonly IDReportItem dal = DalFactory<IDReportItem>.GetDal("ReportItem");
        public BReportItem()
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
        public bool Exists(int ItemNo, string FormNo)
        {
            return dal.Exists(ItemNo, FormNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.ReportItem model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.ReportItem model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ItemNo, string FormNo)
        {

            return dal.Delete(ItemNo, FormNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ReportItem GetModel(int ItemNo, string FormNo)
        {

            return dal.GetModel(ItemNo, FormNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Model.ReportItem GetModelByCache(int ItemNo, string FormNo)
        {

            string CacheKey = "ReportItemModel-" + ItemNo + FormNo;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ItemNo, FormNo);
                    if (objModel != null)
                    {
                        int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.ReportItem)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.ReportItem model)
        {
            return dal.GetList(model);
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
        public List<Model.ReportItem> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.ReportItem> GetModelList(Model.ReportItem model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.ReportItem> DataTableToList(DataTable dt)
        {
            List<Model.ReportItem> modelList = new List<Model.ReportItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.ReportItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.ReportItem();
                    if (dt.Rows[n]["ParItemNo"].ToString() != "")
                    {
                        model.ParItemNo = int.Parse(dt.Rows[n]["ParItemNo"].ToString());
                    }
                    if (dt.Rows[n]["ItemNo"].ToString() != "")
                    {
                        model.ItemNo = int.Parse(dt.Rows[n]["ItemNo"].ToString());
                    }
                    if (dt.Rows[n]["OriginalValue"].ToString() != "")
                    {
                        model.OriginalValue = decimal.Parse(dt.Rows[n]["OriginalValue"].ToString());
                    }
                    if (dt.Rows[n]["ReportValue"].ToString() != "")
                    {
                        model.ReportValue = decimal.Parse(dt.Rows[n]["ReportValue"].ToString());
                    }
                    model.OriginalDesc = dt.Rows[n]["OriginalDesc"].ToString();
                    model.ReportDesc = dt.Rows[n]["ReportDesc"].ToString();
                    if (dt.Rows[n]["StatusNo"].ToString() != "")
                    {
                        model.StatusNo = int.Parse(dt.Rows[n]["StatusNo"].ToString());
                    }
                    model.RefRange = dt.Rows[n]["RefRange"].ToString();
                    if (dt.Rows[n]["EquipNo"].ToString() != "")
                    {
                        model.EquipNo = int.Parse(dt.Rows[n]["EquipNo"].ToString());
                    }
                    if (dt.Rows[n]["Modified"].ToString() != "")
                    {
                        model.Modified = int.Parse(dt.Rows[n]["Modified"].ToString());
                    }
                    if (dt.Rows[n]["ItemDate"].ToString() != "")
                    {
                        model.ItemDate = DateTime.Parse(dt.Rows[n]["ItemDate"].ToString());
                    }
                    if (dt.Rows[n]["ItemTime"].ToString() != "")
                    {
                        model.ItemTime = DateTime.Parse(dt.Rows[n]["ItemTime"].ToString());
                    }
                    if (dt.Rows[n]["IsMatch"].ToString() != "")
                    {
                        model.IsMatch = int.Parse(dt.Rows[n]["IsMatch"].ToString());
                    }
                    model.ResultStatus = dt.Rows[n]["ResultStatus"].ToString();
                    model.HisValue = dt.Rows[n]["HisValue"].ToString();
                    model.HisComp = dt.Rows[n]["HisComp"].ToString();
                    if (dt.Rows[n]["isReceive"].ToString() != "")
                    {
                        model.isReceive = int.Parse(dt.Rows[n]["isReceive"].ToString());
                    }
                    model.CountNodesItemSource = dt.Rows[n]["CountNodesItemSource"].ToString();
                    model.Unit = dt.Rows[n]["Unit"].ToString();
                    if (dt.Rows[n]["FormNo"].ToString() != "")
                    {
                        model.FormNo = dt.Rows[n]["FormNo"].ToString();
                    }
                    if (dt.Rows[n]["PlateNo"].ToString() != "")
                    {
                        model.PlateNo = int.Parse(dt.Rows[n]["PlateNo"].ToString());
                    }
                    if (dt.Rows[n]["PositionNo"].ToString() != "")
                    {
                        model.PositionNo = int.Parse(dt.Rows[n]["PositionNo"].ToString());
                    }
                    if (dt.Rows[n]["TollItemNo"].ToString() != "")
                    {
                        model.TollItemNo = int.Parse(dt.Rows[n]["TollItemNo"].ToString());
                    }
                    model.itemdesc = dt.Rows[n]["itemdesc"].ToString();
                    model.OldSerialNo = dt.Rows[n]["OldSerialNo"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  成员方法


        public DataTable GetReportItemList_DataTable(string FormNo)
        {
            //return dal.GetReportItemList(FormNo);
            return dal.GetReportItemFullList(FormNo);
        }
        public DataTable GetReportItemListAndSort_DataTable(string FormNo, List<string> sortFields)
        {
            return dal.GetReportItemFullListAndSort(FormNo, sortFields);
        }
        public DataTable GetProcReportItemListAndSort_DataTable(string FormNo, List<string> sortFields)
        {
            return dal.GetProcReportItemQueryDataSource(FormNo, sortFields);
        }
        public SortedList GetReportItemList_ItemGroup(string FormNo)
        {

            SortedList itemsl = new SortedList();
            var t = dal.GetReportItemList(FormNo).Select().GroupBy<DataRow, string>(a => a["ParItemNo"].ToString());
            foreach (var tt in t)
            {
                itemsl.Add(tt.Key.ToString(), tt.ElementAt<DataRow>(0)["ParItemName"]);
            }
            return itemsl;
        }

        #region IBReportItem 成员


        public SortedList GetReportItemList_SortedList(string FormNo)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListByDataSource(Model.ReportItem ri_m)
        {
            return dal.GetReportItemFullList(ri_m.FormNo).DataSet;
        }

        #endregion

        public DataSet getTestItemItemDescByitem(string ItemNo) {

            return dal.getTestItemItemDescByitem(ItemNo);
        }

        public DataSet GetReportItemByReportFormID(string ReportFormID){
            return dal.GetReportItemFullByReportFormId(ReportFormID);
        }

        public int UpdateReportItemFull(ReportItemFull model) {

            return dal.UpdateReportItemFull(model);
        }

        public DataSet GetReportItemList_DataSet(List<string> FormNo)
        {
            return dal.GetReportItemList_DataSet(FormNo);
        }
        public DataSet GetReportItemListSort_DataSet(List<string> FormNo,string sortFields)
        {
            return dal.GetReportItemListSort_DataSet(FormNo, sortFields);
        }
        public int GetReportItemListWhereCount(string FormNo,string where)
        {
            
            return dal.GetReportItemFullListWhereCount(FormNo,where);
        }
    }
}
