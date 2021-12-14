using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    /// <summary>
    /// NRequestItem
    /// </summary>
    public partial class BNRequestItem
    {
		//private readonly IDNRequestItem dal = DalFactory<IDNRequestItem>.GetDal("NRequestItem");
		private readonly NRequestItem dal = new NRequestItem();
		public BNRequestItem()
		{}
		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.NRequestItem model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(Model.NRequestItem model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string SerialNo)
		{			
			return dal.Delete(SerialNo);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.NRequestForm GetModel(string SerialNo)
		{
			
			return dal.GetModel(SerialNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Model.NRequestForm GetModelByCache(string SerialNo)
		{
			
			string CacheKey = "NRequestFormModel-" + SerialNo;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SerialNo);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.NRequestForm)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.NRequestItem> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.NRequestItem> DataTableToList(DataTable dt)
		{
			List<Model.NRequestItem> modelList = new List<Model.NRequestItem>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.NRequestItem model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.NRequestItem();
					if(dt.Rows[n]["SerialNo"]!=null && dt.Rows[n]["SerialNo"].ToString()!="")
					{
					model.SerialNo=dt.Rows[n]["SerialNo"].ToString();
					}
					if(dt.Rows[n]["AutoUnionSNo"] !=null && dt.Rows[n]["AutoUnionSNo"].ToString()!="")
					{
						model.AutoUnionSNo = dt.Rows[n]["AutoUnionSNo"].ToString();
					}
					if(dt.Rows[n]["GroupItemList"] !=null && dt.Rows[n]["GroupItemList"].ToString()!="")
					{
						model.GroupItemList = dt.Rows[n]["GroupItemList"].ToString();
					}
					if(dt.Rows[n]["ReportDateGroup"] !=null && dt.Rows[n]["ReportDateGroup"].ToString()!="")
					{
						model.ReportDateGroup = dt.Rows[n]["ReportDateGroup"].ToString();
					}
					if(dt.Rows[n]["ReportDateMemo"] !=null && dt.Rows[n]["ReportDateMemo"].ToString()!="")
					{
					model.ReportDateMemo = dt.Rows[n]["ReportDateMemo"].ToString();
					}
					if(dt.Rows[n]["iAutoUnion"] !=null && dt.Rows[n]["iAutoUnion"].ToString()!="")
					{
					model.iAutoUnion = int.Parse(dt.Rows[n]["iAutoUnion"].ToString());
					}
					if(dt.Rows[n]["ItemGroupNo"]!=null && dt.Rows[n]["ItemGroupNo"].ToString()!="")
					{
						model.ItemGroupNo = int.Parse(dt.Rows[n]["ItemGroupNo"].ToString());
					}
					if(dt.Rows[n]["FormFlagDateDelete"] !=null && dt.Rows[n]["FormFlagDateDelete"].ToString()!="")
					{
						model.FormFlagDateDelete = DateTime.Parse(dt.Rows[n]["FormFlagDateDelete"].ToString());
					}
					if(dt.Rows[n]["DispOrder"] !=null && dt.Rows[n]["DispOrder"].ToString()!="")
					{
						model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
					}
					if(dt.Rows[n]["NItemID"] !=null && dt.Rows[n]["NItemID"].ToString()!="")
					{
						//model.NItemID = int.Parse(dt.Rows[n]["NItemID"].ToString());
					}
					if(dt.Rows[n]["Mergeno"] !=null && dt.Rows[n]["Mergeno"].ToString()!="")
					{
						model.Mergeno = dt.Rows[n]["Mergeno"].ToString();
					}
					if(dt.Rows[n]["OldParItemno"] !=null && dt.Rows[n]["OldParItemno"].ToString()!="")
					{
						model.OldParItemno = dt.Rows[n]["OldParItemno"].ToString();
					}
					if(dt.Rows[n]["CheckTime"] !=null && dt.Rows[n]["CheckTime"].ToString()!="")
					{
						model.CheckTime = DateTime.Parse(dt.Rows[n]["CheckTime"].ToString());
					}
					if(dt.Rows[n]["CheckFlag"] !=null && dt.Rows[n]["CheckFlag"].ToString()!="")
					{
					model.CheckFlag = int.Parse(dt.Rows[n]["CheckFlag"].ToString());
            }
					if(dt.Rows[n]["OldNRequestItemNo"] !=null && dt.Rows[n]["OldNRequestItemNo"].ToString()!="")
					{
						model.OldNRequestItemNo = dt.Rows[n]["OldNRequestItemNo"].ToString();
					}
					if(dt.Rows[n]["TestTypeNo"] !=null && dt.Rows[n]["TestTypeNo"].ToString()!="")
					{
						model.TestTypeNo = int.Parse(dt.Rows[n]["TestTypeNo"].ToString());
					}
					if(dt.Rows[n]["SectionNo"] !=null && dt.Rows[n]["SectionNo"].ToString()!="")
					{
						model.SectionNo = int.Parse(dt.Rows[n]["SectionNo"].ToString());
					}
					if(dt.Rows[n]["ReceiveDate"] !=null && dt.Rows[n]["ReceiveDate"].ToString()!="")
					{
						model.ReceiveDate = DateTime.Parse(dt.Rows[n]["ReceiveDate"].ToString());
					}
					if(dt.Rows[n]["NRequestItemNo"] !=null && dt.Rows[n]["NRequestItemNo"].ToString()!="")
					{
						model.NRequestItemNo = dt.Rows[n]["NRequestItemNo"].ToString();
					}
					if(dt.Rows[n]["chargeflag"] !=null && dt.Rows[n]["chargeflag"].ToString()!="")
					{
					model.chargeflag = int.Parse(dt.Rows[n]["chargeflag"].ToString());
            }
					if(dt.Rows[n]["sampleno"] !=null && dt.Rows[n]["sampleno"].ToString()!="")
					{
					model.sampleno = dt.Rows[n]["sampleno"].ToString();
					}
					if(dt.Rows[n]["PItemCName"] !=null && dt.Rows[n]["PItemCName"].ToString()!="")
					{
						model.PItemCName =dt.Rows[n]["PItemCName"].ToString();					}
					if(dt.Rows[n]["PItemNo"] !=null && dt.Rows[n]["PItemNo"].ToString()!="")
					{
						model.PItemNo = dt.Rows[n]["PItemNo"].ToString();
					}
					if(dt.Rows[n]["IsNurseDo"] !=null && dt.Rows[n]["IsNurseDo"].ToString()!="")
					{
					model.IsNurseDo = int.Parse(dt.Rows[n]["IsNurseDo"].ToString());
					}
					if(dt.Rows[n]["Balance"] !=null && dt.Rows[n]["Balance"].ToString()!="")
					{
						model.Balance = int.Parse(dt.Rows[n]["Balance"].ToString());
					}
					if(dt.Rows[n]["PrepaymentFlag"] !=null && dt.Rows[n]["PrepaymentFlag"].ToString()!="")
					{
						model.PrepaymentFlag = int.Parse(dt.Rows[n]["PrepaymentFlag"].ToString());
					}
					if(dt.Rows[n]["ItemDispenseFlag"] !=null && dt.Rows[n]["ItemDispenseFlag"].ToString()!="")
					{
					model.ItemDispenseFlag = int.Parse(dt.Rows[n]["ItemDispenseFlag"].ToString());
					}
					if(dt.Rows[n]["IsCheckFee"] !=null && dt.Rows[n]["IsCheckFee"].ToString()!="")
					{
					model.IsCheckFee = int.Parse(dt.Rows[n]["IsCheckFee"].ToString());
					}
					if(dt.Rows[n]["StateFlag"] !=null && dt.Rows[n]["StateFlag"].ToString()!="")
					{
					model.StateFlag = int.Parse(dt.Rows[n]["StateFlag"].ToString());
					}
					if(dt.Rows[n]["charge"] !=null && dt.Rows[n]["charge"].ToString()!="")
					{
					model.charge = float.Parse(dt.Rows[n]["charge"].ToString());
					}
					if(dt.Rows[n]["uniteName"] !=null && dt.Rows[n]["uniteName"].ToString()!="")
					{
					model.uniteName = dt.Rows[n]["uniteName"].ToString();
					}
					if(dt.Rows[n]["uniteItemNo"] !=null && dt.Rows[n]["uniteItemNo"].ToString()!="")
					{
						model.uniteItemNo = dt.Rows[n]["uniteItemNo"].ToString();
					}
					if(dt.Rows[n]["OldSerialNo"] !=null && dt.Rows[n]["OldSerialNo"].ToString()!="")
					{
					model.OldSerialNo = dt.Rows[n]["OldSerialNo"].ToString();
					}
                    if (dt.Rows[n]["SerialNoParent"] != null && dt.Rows[n]["SerialNoParent"].ToString() != "")
                    {
                        model.SerialNoParent = dt.Rows[n]["SerialNoParent"].ToString();
                    }
                    if (dt.Rows[n]["zdy1"] != null && dt.Rows[n]["zdy1"].ToString() != "")
                    {
                        model.zdy1 = dt.Rows[n]["zdy1"].ToString();
                    }
                    if (dt.Rows[n]["zdy2"]!=null && dt.Rows[n]["zdy2"].ToString()!="")
					{
					model.zdy2=dt.Rows[n]["zdy2"].ToString();
					}
					if(dt.Rows[n]["zdy3"]!=null && dt.Rows[n]["zdy3"].ToString()!="")
					{
					model.zdy3=dt.Rows[n]["zdy3"].ToString();
					}
					if(dt.Rows[n]["zdy4"]!=null && dt.Rows[n]["zdy4"].ToString()!="")
					{
					model.zdy4=dt.Rows[n]["zdy4"].ToString();
					}
					if(dt.Rows[n]["zdy5"]!=null && dt.Rows[n]["zdy5"].ToString()!="")
					{
					model.zdy5=dt.Rows[n]["zdy5"].ToString();
					}
					if(dt.Rows[n]["ItemDate"] !=null && dt.Rows[n]["ItemDate"].ToString()!="")
					{
						model.ItemDate = DateTime.Parse(dt.Rows[n]["ItemDate"].ToString());
					}
					if(dt.Rows[n]["FlagDateDelete"] !=null && dt.Rows[n]["FlagDateDelete"].ToString()!="")
					{
					model.FlagDateDelete = DateTime.Parse(dt.Rows[n]["FlagDateDelete"].ToString());
					}
					if(dt.Rows[n]["Samplinggroupno"] !=null && dt.Rows[n]["Samplinggroupno"].ToString()!="")
					{
					model.Samplinggroupno = int.Parse(dt.Rows[n]["Samplinggroupno"].ToString());
					}
					if(dt.Rows[n]["itemno"] !=null && dt.Rows[n]["itemno"].ToString()!="")
					{
					model.itemno = int.Parse(dt.Rows[n]["itemno"].ToString());
					}
					if(dt.Rows[n]["barcode"] !=null && dt.Rows[n]["barcode"].ToString()!="")
					{
					model.barcode = dt.Rows[n]["barcode"].ToString();
					}
					if(dt.Rows[n]["sampletypeno"] !=null && dt.Rows[n]["sampletypeno"].ToString()!="")
					{
					model.sampletypeno = dt.Rows[n]["sampletypeno"].ToString();
					}
					if(dt.Rows[n]["RECEIVEFLAG"] !=null && dt.Rows[n]["RECEIVEFLAG"].ToString()!="")
					{
					model.RECEIVEFLAG = int.Parse(dt.Rows[n]["RECEIVEFLAG"].ToString());
					}
					if(dt.Rows[n]["OrderNo"] !=null && dt.Rows[n]["OrderNo"].ToString()!="")
					{
						model.OrderNo = dt.Rows[n]["OrderNo"].ToString();
					}
					if(dt.Rows[n]["ParItemNo"] !=null && dt.Rows[n]["ParItemNo"].ToString()!="")
					{
					model.ParItemNo = int.Parse(dt.Rows[n]["ParItemNo"].ToString());
					}
					if(dt.Rows[n]["SerialNo"] !=null && dt.Rows[n]["SerialNo"].ToString()!="")
					{
						model.SerialNo = dt.Rows[n]["SerialNo"].ToString();
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
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method

        #region IBLLBase<NRequestForm> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.NRequestItem model)
        {
            return dal.GetList(model);
        }

        public List<Model.NRequestItem> GetModelList(Model.NRequestItem model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }

        public int GetCountFormFull(string where) {
            return dal.GetCountFormFull(where);
        }

        public DataSet GetList_FormFull(string urlModel, string urlWhere) {
            return dal.GetList_FormFull(urlModel,urlWhere);
        }

        #endregion
    }
}

