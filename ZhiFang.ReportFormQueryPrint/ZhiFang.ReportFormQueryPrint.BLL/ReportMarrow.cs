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
	/// <summary>
	/// ReportMarrow
	/// </summary>
    public partial class BReportMarrow 
	{
        private readonly IDReportMarrow dal = DalFactory<IDReportMarrow>.GetDal("ReportMarrow");
        public BReportMarrow()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string ItemNo, string FormNo)
		{
			return dal.Exists(ItemNo,FormNo);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.ReportMarrow model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(Model.ReportMarrow model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(string ItemNo, string FormNo)
		{
			
			return dal.Delete(ItemNo,FormNo);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Model.ReportMarrow GetModel(string ItemNo, string FormNo)
		{
			
			return dal.GetModel(ItemNo,FormNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        public Model.ReportMarrow GetModelByCache(string ItemNo, string FormNo)
		{
			
			string CacheKey = "ReportMarrowModel-" + ItemNo+FormNo;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ItemNo,FormNo);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.ReportMarrow)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.ReportMarrow> DataTableToList(DataTable dt)
		{
			List<Model.ReportMarrow> modelList = new List<Model.ReportMarrow>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.ReportMarrow model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.ReportMarrow();
					if(dt.Rows[n]["ParItemNo"]!=null && dt.Rows[n]["ParItemNo"].ToString()!="")
					{
						model.ParItemNo=int.Parse(dt.Rows[n]["ParItemNo"].ToString());
					}
					if(dt.Rows[n]["ItemNo"]!=null && dt.Rows[n]["ItemNo"].ToString()!="")
					{
						model.ItemNo=int.Parse(dt.Rows[n]["ItemNo"].ToString());
					}
					if(dt.Rows[n]["BloodNum"]!=null && dt.Rows[n]["BloodNum"].ToString()!="")
					{
						model.BloodNum=int.Parse(dt.Rows[n]["BloodNum"].ToString());
					}
					if(dt.Rows[n]["BloodPercent"]!=null && dt.Rows[n]["BloodPercent"].ToString()!="")
					{
						model.BloodPercent=decimal.Parse(dt.Rows[n]["BloodPercent"].ToString());
					}
					if(dt.Rows[n]["MarrowNum"]!=null && dt.Rows[n]["MarrowNum"].ToString()!="")
					{
						model.MarrowNum=int.Parse(dt.Rows[n]["MarrowNum"].ToString());
					}
					if(dt.Rows[n]["MarrowPercent"]!=null && dt.Rows[n]["MarrowPercent"].ToString()!="")
					{
						model.MarrowPercent=decimal.Parse(dt.Rows[n]["MarrowPercent"].ToString());
					}
					if(dt.Rows[n]["BloodDesc"]!=null && dt.Rows[n]["BloodDesc"].ToString()!="")
					{
					model.BloodDesc=dt.Rows[n]["BloodDesc"].ToString();
					}
					if(dt.Rows[n]["MarrowDesc"]!=null && dt.Rows[n]["MarrowDesc"].ToString()!="")
					{
					model.MarrowDesc=dt.Rows[n]["MarrowDesc"].ToString();
					}
					if(dt.Rows[n]["StatusNo"]!=null && dt.Rows[n]["StatusNo"].ToString()!="")
					{
						model.StatusNo=int.Parse(dt.Rows[n]["StatusNo"].ToString());
					}
					if(dt.Rows[n]["RefRange"]!=null && dt.Rows[n]["RefRange"].ToString()!="")
					{
					model.RefRange=dt.Rows[n]["RefRange"].ToString();
					}
					if(dt.Rows[n]["EquipNo"]!=null && dt.Rows[n]["EquipNo"].ToString()!="")
					{
						model.EquipNo=int.Parse(dt.Rows[n]["EquipNo"].ToString());
					}
					if(dt.Rows[n]["IsCale"]!=null && dt.Rows[n]["IsCale"].ToString()!="")
					{
						model.IsCale=int.Parse(dt.Rows[n]["IsCale"].ToString());
					}
					if(dt.Rows[n]["Modified"]!=null && dt.Rows[n]["Modified"].ToString()!="")
					{
						model.Modified=int.Parse(dt.Rows[n]["Modified"].ToString());
					}
					if(dt.Rows[n]["ItemDate"]!=null && dt.Rows[n]["ItemDate"].ToString()!="")
					{
						model.ItemDate=DateTime.Parse(dt.Rows[n]["ItemDate"].ToString());
					}
					if(dt.Rows[n]["ItemTime"]!=null && dt.Rows[n]["ItemTime"].ToString()!="")
					{
						model.ItemTime=DateTime.Parse(dt.Rows[n]["ItemTime"].ToString());
					}
					if(dt.Rows[n]["IsMatch"]!=null && dt.Rows[n]["IsMatch"].ToString()!="")
					{
						model.IsMatch=int.Parse(dt.Rows[n]["IsMatch"].ToString());
					}
					if(dt.Rows[n]["ResultStatus"]!=null && dt.Rows[n]["ResultStatus"].ToString()!="")
					{
					model.ResultStatus=dt.Rows[n]["ResultStatus"].ToString();
					}
					if(dt.Rows[n]["FormNo"]!=null && dt.Rows[n]["FormNo"].ToString()!="")
					{
						model.FormNo=dt.Rows[n]["FormNo"].ToString();
					}
					if(dt.Rows[n]["ItemName"]!=null && dt.Rows[n]["ItemName"].ToString()!="")
					{
					model.ItemName=dt.Rows[n]["ItemName"].ToString();
					}
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
			return dal.GetList("");
		}

		#endregion  Method

        #region IBReportMarrow 成员


        public DataTable GetReportItemList_DataTable(string FormNo)
        {
            return dal.GetReportMarrowFullList(FormNo).Tables[0];
        }

        public SortedList GetReportItemList_SortedList(string FormNo)
        {
            SortedList itemsl = new SortedList();
            var t = dal.GetReportMarrowFullList(FormNo).Tables[0].Select().GroupBy<DataRow, string>(a => a["ParItemNo"].ToString());
            foreach (var tt in t)
            {
                itemsl.Add(tt.Key.ToString(), tt.ElementAt<DataRow>(0)["ParItemName"]);
            }
            return itemsl;
        }

        public SortedList GetReportItemList_ItemGroup(string FormNo)
        {
            SortedList itemsl = new SortedList();
            var t = dal.GetReportMarrowFullList(FormNo).Tables[0].Select().GroupBy<DataRow, string>(a => a["ParItemNo"].ToString());
            foreach (var tt in t)
            {
                itemsl.Add(tt.Key.ToString(), tt.ElementAt<DataRow>(0)["ParItemName"]);
            }
            return itemsl;
        }

        public DataSet GetListByDataSource(Model.ReportMarrow rmarrow_m)
        {
            return dal.GetReportMarrowFullList(rmarrow_m.FormNo);
        }

        #endregion

        #region IBLLBase<ReportMarrow> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.ReportMarrow t)
        {
            return dal.GetList(t);
        }

        public List<Model.ReportMarrow> GetModelList(Model.ReportMarrow t)
        {
            DataSet ds = dal.GetList(t);
            return DataTableToList(ds.Tables[0]);
        }

        #endregion

        internal DataSet GetList(string FormNo)
        {
            throw new NotImplementedException();
        }
        internal DataTable GetReportMarrowItemList(string FormNo) {
            return dal.GetReportMarrowItemList(FormNo);
        }
        internal DataTable GetListByFormNo(string FormNo)
        {
            throw new NotImplementedException();
        }

        public DataSet GetReportMarrowFullByReportFormID(string reportformid) {
            return dal.GetReportMarrowFullByReportFormID(reportformid);
        }

        public int UpdateReportMarrowFull(ReportMarrowFull model) {
            return dal.UpdateReportMarrowFull(model);
        }
    }
}

