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
	/// NRequestForm
	/// </summary>
	public partial class BNRequestForm
	{
        //private readonly IDNRequestForm dal = DalFactory<IDNRequestForm>.GetDal("NRequestForm");
		private readonly NRequestForm dal = new NRequestForm();
		public BNRequestForm()
		{}
		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.NRequestForm model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(Model.NRequestForm model)
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
		public List<Model.NRequestForm> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.NRequestForm> DataTableToList(DataTable dt)
		{
			List<Model.NRequestForm> modelList = new List<Model.NRequestForm>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.NRequestForm model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.NRequestForm();
					if(dt.Rows[n]["SerialNo"]!=null && dt.Rows[n]["SerialNo"].ToString()!="")
					{
					model.SerialNo=dt.Rows[n]["SerialNo"].ToString();
					}
					if(dt.Rows[n]["ReceiveFlag"]!=null && dt.Rows[n]["ReceiveFlag"].ToString()!="")
					{
						model.ReceiveFlag=int.Parse(dt.Rows[n]["ReceiveFlag"].ToString());
					}
					if(dt.Rows[n]["StatusNo"]!=null && dt.Rows[n]["StatusNo"].ToString()!="")
					{
						model.StatusNo=int.Parse(dt.Rows[n]["StatusNo"].ToString());
					}
					if(dt.Rows[n]["SampleTypeNo"]!=null && dt.Rows[n]["SampleTypeNo"].ToString()!="")
					{
						model.SampleTypeNo=int.Parse(dt.Rows[n]["SampleTypeNo"].ToString());
					}
					if(dt.Rows[n]["PatNo"]!=null && dt.Rows[n]["PatNo"].ToString()!="")
					{
					model.PatNo=dt.Rows[n]["PatNo"].ToString();
					}
					if(dt.Rows[n]["CName"]!=null && dt.Rows[n]["CName"].ToString()!="")
					{
					model.CName=dt.Rows[n]["CName"].ToString();
					}
					if(dt.Rows[n]["GenderNo"]!=null && dt.Rows[n]["GenderNo"].ToString()!="")
					{
						model.GenderNo=int.Parse(dt.Rows[n]["GenderNo"].ToString());
					}
					if(dt.Rows[n]["Birthday"]!=null && dt.Rows[n]["Birthday"].ToString()!="")
					{
						model.Birthday=DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
					}
					if(dt.Rows[n]["Age"]!=null && dt.Rows[n]["Age"].ToString()!="")
					{
						model.Age=decimal.Parse(dt.Rows[n]["Age"].ToString());
					}
					if(dt.Rows[n]["AgeUnitNo"]!=null && dt.Rows[n]["AgeUnitNo"].ToString()!="")
					{
						model.AgeUnitNo=int.Parse(dt.Rows[n]["AgeUnitNo"].ToString());
					}
					if(dt.Rows[n]["FolkNo"]!=null && dt.Rows[n]["FolkNo"].ToString()!="")
					{
						model.FolkNo=int.Parse(dt.Rows[n]["FolkNo"].ToString());
					}
					if(dt.Rows[n]["DistrictNo"]!=null && dt.Rows[n]["DistrictNo"].ToString()!="")
					{
						model.DistrictNo=int.Parse(dt.Rows[n]["DistrictNo"].ToString());
					}
					if(dt.Rows[n]["WardNo"]!=null && dt.Rows[n]["WardNo"].ToString()!="")
					{
						model.WardNo=int.Parse(dt.Rows[n]["WardNo"].ToString());
					}
					if(dt.Rows[n]["Bed"]!=null && dt.Rows[n]["Bed"].ToString()!="")
					{
					model.Bed=dt.Rows[n]["Bed"].ToString();
					}
					if(dt.Rows[n]["DeptNo"]!=null && dt.Rows[n]["DeptNo"].ToString()!="")
					{
						model.DeptNo=int.Parse(dt.Rows[n]["DeptNo"].ToString());
					}
					if(dt.Rows[n]["Doctor"]!=null && dt.Rows[n]["Doctor"].ToString()!="")
					{
						model.Doctor=int.Parse(dt.Rows[n]["Doctor"].ToString());
					}
					if(dt.Rows[n]["DiagNo"]!=null && dt.Rows[n]["DiagNo"].ToString()!="")
					{
						model.DiagNo=int.Parse(dt.Rows[n]["DiagNo"].ToString());
					}
					if(dt.Rows[n]["ChargeNo"]!=null && dt.Rows[n]["ChargeNo"].ToString()!="")
					{
						model.ChargeNo=int.Parse(dt.Rows[n]["ChargeNo"].ToString());
					}
					if(dt.Rows[n]["Charge"]!=null && dt.Rows[n]["Charge"].ToString()!="")
					{
						model.Charge=decimal.Parse(dt.Rows[n]["Charge"].ToString());
					}
					if(dt.Rows[n]["CollecterID"]!=null && dt.Rows[n]["CollecterID"].ToString()!="")
					{
					model.CollecterID=dt.Rows[n]["CollecterID"].ToString();
					}
					if(dt.Rows[n]["Collecter"]!=null && dt.Rows[n]["Collecter"].ToString()!="")
					{
					model.Collecter=dt.Rows[n]["Collecter"].ToString();
					}
					if(dt.Rows[n]["CollectDate"]!=null && dt.Rows[n]["CollectDate"].ToString()!="")
					{
						model.CollectDate=DateTime.Parse(dt.Rows[n]["CollectDate"].ToString());
					}
					if(dt.Rows[n]["CollectTime"]!=null && dt.Rows[n]["CollectTime"].ToString()!="")
					{
						model.CollectTime=DateTime.Parse(dt.Rows[n]["CollectTime"].ToString());
					}
					if(dt.Rows[n]["Operator"]!=null && dt.Rows[n]["Operator"].ToString()!="")
					{
					model.Operator=dt.Rows[n]["Operator"].ToString();
					}
					if(dt.Rows[n]["OperDate"]!=null && dt.Rows[n]["OperDate"].ToString()!="")
					{
						model.OperDate=DateTime.Parse(dt.Rows[n]["OperDate"].ToString());
					}
					if(dt.Rows[n]["OperTime"]!=null && dt.Rows[n]["OperTime"].ToString()!="")
					{
						model.OperTime=DateTime.Parse(dt.Rows[n]["OperTime"].ToString());
					}
					if(dt.Rows[n]["FormMemo"]!=null && dt.Rows[n]["FormMemo"].ToString()!="")
					{
					model.FormMemo=dt.Rows[n]["FormMemo"].ToString();
					}
					if(dt.Rows[n]["RequestSource"]!=null && dt.Rows[n]["RequestSource"].ToString()!="")
					{
					model.RequestSource=dt.Rows[n]["RequestSource"].ToString();
					}
					if(dt.Rows[n]["Artificerorder"]!=null && dt.Rows[n]["Artificerorder"].ToString()!="")
					{
					model.Artificerorder=dt.Rows[n]["Artificerorder"].ToString();
					}
					if(dt.Rows[n]["sickorder"]!=null && dt.Rows[n]["sickorder"].ToString()!="")
					{
					model.sickorder=dt.Rows[n]["sickorder"].ToString();
					}
					if(dt.Rows[n]["chargeflag"]!=null && dt.Rows[n]["chargeflag"].ToString()!="")
					{
					model.chargeflag=dt.Rows[n]["chargeflag"].ToString();
					}
					if(dt.Rows[n]["jztype"]!=null && dt.Rows[n]["jztype"].ToString()!="")
					{
						model.jztype=int.Parse(dt.Rows[n]["jztype"].ToString());
					}
					if(dt.Rows[n]["zdy1"]!=null && dt.Rows[n]["zdy1"].ToString()!="")
					{
					model.zdy1=dt.Rows[n]["zdy1"].ToString();
					}
					if(dt.Rows[n]["zdy2"]!=null && dt.Rows[n]["zdy2"].ToString()!="")
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
					if(dt.Rows[n]["FlagDateDelete"]!=null && dt.Rows[n]["FlagDateDelete"].ToString()!="")
					{
						model.FlagDateDelete=DateTime.Parse(dt.Rows[n]["FlagDateDelete"].ToString());
					}
					if(dt.Rows[n]["FormComment"]!=null && dt.Rows[n]["FormComment"].ToString()!="")
					{
					model.FormComment=dt.Rows[n]["FormComment"].ToString();
					}
					if(dt.Rows[n]["nurseflag"]!=null && dt.Rows[n]["nurseflag"].ToString()!="")
					{
					model.nurseflag=dt.Rows[n]["nurseflag"].ToString();
					}
					if(dt.Rows[n]["diag"]!=null && dt.Rows[n]["diag"].ToString()!="")
					{
					model.diag=dt.Rows[n]["diag"].ToString();
					}
					if(dt.Rows[n]["CaseNo"]!=null && dt.Rows[n]["CaseNo"].ToString()!="")
					{
					model.CaseNo=dt.Rows[n]["CaseNo"].ToString();
					}
					if(dt.Rows[n]["refuseopinion"]!=null && dt.Rows[n]["refuseopinion"].ToString()!="")
					{
					model.refuseopinion=dt.Rows[n]["refuseopinion"].ToString();
					}
					if(dt.Rows[n]["refusereason"]!=null && dt.Rows[n]["refusereason"].ToString()!="")
					{
					model.refusereason=dt.Rows[n]["refusereason"].ToString();
					}
					if(dt.Rows[n]["signintime"]!=null && dt.Rows[n]["signintime"].ToString()!="")
					{
						model.signintime=DateTime.Parse(dt.Rows[n]["signintime"].ToString());
					}
					if(dt.Rows[n]["signer"]!=null && dt.Rows[n]["signer"].ToString()!="")
					{
					model.signer=dt.Rows[n]["signer"].ToString();
					}
					if(dt.Rows[n]["signflag"]!=null && dt.Rows[n]["signflag"].ToString()!="")
					{
						model.signflag=int.Parse(dt.Rows[n]["signflag"].ToString());
					}
					if(dt.Rows[n]["SamplingGroupNo"]!=null && dt.Rows[n]["SamplingGroupNo"].ToString()!="")
					{
						model.SamplingGroupNo=int.Parse(dt.Rows[n]["SamplingGroupNo"].ToString());
					}
					if(dt.Rows[n]["PrintCount"]!=null && dt.Rows[n]["PrintCount"].ToString()!="")
					{
						model.PrintCount=int.Parse(dt.Rows[n]["PrintCount"].ToString());
					}
					if(dt.Rows[n]["PrintInfo"]!=null && dt.Rows[n]["PrintInfo"].ToString()!="")
					{
					model.PrintInfo=dt.Rows[n]["PrintInfo"].ToString();
					}
					if(dt.Rows[n]["SampleCap"]!=null && dt.Rows[n]["SampleCap"].ToString()!="")
					{
						model.SampleCap=decimal.Parse(dt.Rows[n]["SampleCap"].ToString());
					}
					if(dt.Rows[n]["IsPrep"]!=null && dt.Rows[n]["IsPrep"].ToString()!="")
					{
						model.IsPrep=int.Parse(dt.Rows[n]["IsPrep"].ToString());
					}
					if(dt.Rows[n]["IsAffirm"]!=null && dt.Rows[n]["IsAffirm"].ToString()!="")
					{
						model.IsAffirm=int.Parse(dt.Rows[n]["IsAffirm"].ToString());
					}
					if(dt.Rows[n]["IsSampling"]!=null && dt.Rows[n]["IsSampling"].ToString()!="")
					{
						model.IsSampling=int.Parse(dt.Rows[n]["IsSampling"].ToString());
					}
					if(dt.Rows[n]["IsSend"]!=null && dt.Rows[n]["IsSend"].ToString()!="")
					{
						model.IsSend=int.Parse(dt.Rows[n]["IsSend"].ToString());
					}
					if(dt.Rows[n]["incepter"]!=null && dt.Rows[n]["incepter"].ToString()!="")
					{
					model.incepter=dt.Rows[n]["incepter"].ToString();
					}
					if(dt.Rows[n]["inceptTime"]!=null && dt.Rows[n]["inceptTime"].ToString()!="")
					{
						model.inceptTime=DateTime.Parse(dt.Rows[n]["inceptTime"].ToString());
					}
					if(dt.Rows[n]["inceptDate"]!=null && dt.Rows[n]["inceptDate"].ToString()!="")
					{
						model.inceptDate=DateTime.Parse(dt.Rows[n]["inceptDate"].ToString());
					}
					if(dt.Rows[n]["isByHand"]!=null && dt.Rows[n]["isByHand"].ToString()!="")
					{
						if((dt.Rows[n]["isByHand"].ToString()=="1")||(dt.Rows[n]["isByHand"].ToString().ToLower()=="true"))
						{
						model.isByHand=true;
						}
						else
						{
							model.isByHand=false;
						}
					}
					if(dt.Columns.Contains("AssignFlag") && dt.Rows[n]["AssignFlag"]!=null && dt.Rows[n]["AssignFlag"].ToString()!="")
					{
						model.AssignFlag=int.Parse(dt.Rows[n]["AssignFlag"].ToString());
					}
					if(dt.Rows[n]["OldSerialNo"]!=null && dt.Rows[n]["OldSerialNo"].ToString()!="")
					{
					model.OldSerialNo=dt.Rows[n]["OldSerialNo"].ToString();
					}
					if(dt.Rows[n]["TestTypeNo"]!=null && dt.Rows[n]["TestTypeNo"].ToString()!="")
					{
						model.TestTypeNo=int.Parse(dt.Rows[n]["TestTypeNo"].ToString());
					}
					if(dt.Rows[n]["DispenseFlag"]!=null && dt.Rows[n]["DispenseFlag"].ToString()!="")
					{
						model.DispenseFlag=int.Parse(dt.Rows[n]["DispenseFlag"].ToString());
					}
					if(dt.Rows[n]["refuseUser"]!=null && dt.Rows[n]["refuseUser"].ToString()!="")
					{
					model.refuseUser=dt.Rows[n]["refuseUser"].ToString();
					}
					if(dt.Rows[n]["refuseTime"]!=null && dt.Rows[n]["refuseTime"].ToString()!="")
					{
						model.refuseTime=DateTime.Parse(dt.Rows[n]["refuseTime"].ToString());
					}
					if(dt.Rows[n]["jytype"]!=null && dt.Rows[n]["jytype"].ToString()!="")
					{
					model.jytype=dt.Rows[n]["jytype"].ToString();
					}
					if(dt.Columns.Contains("SerialScanTime_old") && dt.Rows[n]["SerialScanTime_old"]!=null && dt.Rows[n]["SerialScanTime_old"].ToString()!="")
					{
					model.SerialScanTime_old=dt.Rows[n]["SerialScanTime_old"].ToString();
					}
					if(dt.Rows[n]["IsCheckFee"]!=null && dt.Rows[n]["IsCheckFee"].ToString()!="")
					{
						model.IsCheckFee=int.Parse(dt.Rows[n]["IsCheckFee"].ToString());
					}
					if(dt.Rows[n]["Dr2Flag"]!=null && dt.Rows[n]["Dr2Flag"].ToString()!="")
					{
						model.Dr2Flag=int.Parse(dt.Rows[n]["Dr2Flag"].ToString());
					}
					if(dt.Rows[n]["ExecDeptNo"]!=null && dt.Rows[n]["ExecDeptNo"].ToString()!="")
					{
						model.ExecDeptNo=int.Parse(dt.Rows[n]["ExecDeptNo"].ToString());
					}
					if(dt.Rows[n]["ClientHost"]!=null && dt.Rows[n]["ClientHost"].ToString()!="")
					{
					model.ClientHost=dt.Rows[n]["ClientHost"].ToString();
					}
					if(dt.Rows[n]["PreNumber"]!=null && dt.Rows[n]["PreNumber"].ToString()!="")
					{
						model.PreNumber=int.Parse(dt.Rows[n]["PreNumber"].ToString());
					}
					if(dt.Rows[n]["UrgentState"]!=null && dt.Rows[n]["UrgentState"].ToString()!="")
					{
					model.UrgentState=dt.Rows[n]["UrgentState"].ToString();
					}
					if(dt.Rows[n]["ZDY6"]!=null && dt.Rows[n]["ZDY6"].ToString()!="")
					{
					model.ZDY6=dt.Rows[n]["ZDY6"].ToString();
					}
					if(dt.Rows[n]["ZDY7"]!=null && dt.Rows[n]["ZDY7"].ToString()!="")
					{
					model.ZDY7=dt.Rows[n]["ZDY7"].ToString();
					}
					if(dt.Rows[n]["ZDY8"]!=null && dt.Rows[n]["ZDY8"].ToString()!="")
					{
					model.ZDY8=dt.Rows[n]["ZDY8"].ToString();
					}
					if(dt.Rows[n]["ZDY9"]!=null && dt.Rows[n]["ZDY9"].ToString()!="")
					{
					model.ZDY9=dt.Rows[n]["ZDY9"].ToString();
					}
					if(dt.Rows[n]["ZDY10"]!=null && dt.Rows[n]["ZDY10"].ToString()!="")
					{
					model.ZDY10=dt.Rows[n]["ZDY10"].ToString();
					}
					if(dt.Rows[n]["phoneCode"]!=null && dt.Rows[n]["phoneCode"].ToString()!="")
					{
					model.phoneCode=dt.Rows[n]["phoneCode"].ToString();
					}
					if(dt.Rows[n]["IsNode"]!=null && dt.Rows[n]["IsNode"].ToString()!="")
					{
						model.IsNode=int.Parse(dt.Rows[n]["IsNode"].ToString());
					}
					if(dt.Rows[n]["PhoneNodeCount"]!=null && dt.Rows[n]["PhoneNodeCount"].ToString()!="")
					{
						model.PhoneNodeCount=int.Parse(dt.Rows[n]["PhoneNodeCount"].ToString());
					}
					if(dt.Rows[n]["AutoNodeCount"]!=null && dt.Rows[n]["AutoNodeCount"].ToString()!="")
					{
						model.AutoNodeCount=int.Parse(dt.Rows[n]["AutoNodeCount"].ToString());
					}
					if(dt.Rows[n]["clientno"]!=null && dt.Rows[n]["clientno"].ToString()!="")
					{
						model.clientno=int.Parse(dt.Rows[n]["clientno"].ToString());
					}
					if(dt.Rows[n]["SerialScanTime"]!=null && dt.Rows[n]["SerialScanTime"].ToString()!="")
					{
						model.SerialScanTime=DateTime.Parse(dt.Rows[n]["SerialScanTime"].ToString());
					}
					if(dt.Rows[n]["CountNodesFormSource"]!=null && dt.Rows[n]["CountNodesFormSource"].ToString()!="")
					{
					model.CountNodesFormSource=dt.Rows[n]["CountNodesFormSource"].ToString();
					}
					if(dt.Rows[n]["StateFlag"]!=null && dt.Rows[n]["StateFlag"].ToString()!="")
					{
						model.StateFlag=int.Parse(dt.Rows[n]["StateFlag"].ToString());
					}
					if(dt.Rows[n]["AffirmTime"]!=null && dt.Rows[n]["AffirmTime"].ToString()!="")
					{
						model.AffirmTime=DateTime.Parse(dt.Rows[n]["AffirmTime"].ToString());
					}
					if(dt.Rows[n]["IsNurseDo"]!=null && dt.Rows[n]["IsNurseDo"].ToString()!="")
					{
						model.IsNurseDo=int.Parse(dt.Rows[n]["IsNurseDo"].ToString());
					}
					if(dt.Rows[n]["NurseSender"]!=null && dt.Rows[n]["NurseSender"].ToString()!="")
					{
					model.NurseSender=dt.Rows[n]["NurseSender"].ToString();
					}
					if(dt.Rows[n]["NurseSendTime"]!=null && dt.Rows[n]["NurseSendTime"].ToString()!="")
					{
						model.NurseSendTime=DateTime.Parse(dt.Rows[n]["NurseSendTime"].ToString());
					}
					if(dt.Rows[n]["NurseSendCarrier"]!=null && dt.Rows[n]["NurseSendCarrier"].ToString()!="")
					{
					model.NurseSendCarrier=dt.Rows[n]["NurseSendCarrier"].ToString();
					}
					if(dt.Rows[n]["CollectCount"]!=null && dt.Rows[n]["CollectCount"].ToString()!="")
					{
						model.CollectCount=int.Parse(dt.Rows[n]["CollectCount"].ToString());
					}
					if(dt.Rows[n]["ForeignSendFlag"]!=null && dt.Rows[n]["ForeignSendFlag"].ToString()!="")
					{
						model.ForeignSendFlag=int.Parse(dt.Rows[n]["ForeignSendFlag"].ToString());
					}
					if(dt.Rows[n]["HisAffirm"]!=null && dt.Rows[n]["HisAffirm"].ToString()!="")
					{
						model.HisAffirm=int.Parse(dt.Rows[n]["HisAffirm"].ToString());
					}
					if(dt.Rows[n]["PatPhoto"]!=null && dt.Rows[n]["PatPhoto"].ToString()!="")
					{
						model.PatPhoto=(byte[])dt.Rows[n]["PatPhoto"];
					}
					if(dt.Rows[n]["ChargeOrderNo"]!=null && dt.Rows[n]["ChargeOrderNo"].ToString()!="")
					{
					model.ChargeOrderNo=dt.Rows[n]["ChargeOrderNo"].ToString();
					}
					if(dt.Rows[n]["ReportFlag"]!=null && dt.Rows[n]["ReportFlag"].ToString()!="")
					{
						model.ReportFlag=int.Parse(dt.Rows[n]["ReportFlag"].ToString());
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

        public DataSet GetList(Model.NRequestForm model)
        {
            return dal.GetList(model);
        }

        public List<Model.NRequestForm> GetModelList(Model.NRequestForm model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }

		#endregion

		public int GetCountFormFull(string strWhere)
		{
		    int aa  = dal.GetCountForm(strWhere);
			return aa;
		}
		public DataSet GetList_FormFull(string fields, string strWhere)
		{
			return dal.GetList_FormFull(fields, strWhere);
		}

		public List<Model.NRequestForm> GetModelList_FormFull(string fields, string strWhere)
		{
			DataSet ds = dal.GetList_FormFull(fields, strWhere);
			return DataTableToList(ds.Tables[0]);
		}
	}
}

