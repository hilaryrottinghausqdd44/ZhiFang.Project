using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;

namespace ZhiFang.BLL.Report
{
	/// <summary>
	/// ReportMarrowFull
	/// </summary>
    public partial class ReportMarrowFull : ZhiFang.IBLL.Report.IBReportMarrowFull
	{
        private readonly IDReportMarrowFull dal = DalFactory<IDReportMarrowFull>.GetDalByClassName("ReportMarrowFull");
        private readonly IDTestItemControl idtic = DalFactory<IDTestItemControl>.GetDalByClassName("B_TestItemControl.");
        private readonly IDSampleTypeControl idstc = DalFactory<IDSampleTypeControl>.GetDalByClassName("B_SampleTypeControl");
        private readonly IDGenderTypeControl idgtc = DalFactory<IDGenderTypeControl>.GetDalByClassName("B_GenderTypeControl");
		public ReportMarrowFull()
		{
           
        }
		#region  Method

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
        public bool Exists(string ReportFormID, string ReportItemID)
		{
			return dal.Exists(ReportFormID,ReportItemID);
		}

        public int DeleteByWhere(string Strwhere)
        {
            return dal.DeleteByWhere(Strwhere);
        }

        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.ReportMarrowFull model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(Model.ReportMarrowFull model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(string ReportFormID, string ReportItemID)
		{			
			return dal.Delete(ReportFormID,ReportItemID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Model.ReportMarrowFull GetModel(string ReportFormID, string ReportItemID)
		{
			
			return dal.GetModel(ReportFormID,ReportItemID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        public Model.ReportMarrowFull GetModelByCache(string ReportFormID, string ReportItemID)
		{
			
			string CacheKey = "ReportMarrowFullModel-" + ReportFormID+ReportItemID;
			object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReportFormID,ReportItemID);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.ReportMarrowFull)objModel;
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.ReportMarrowFull> DataTableToList(DataTable dt)
		{
			List<Model.ReportMarrowFull> modelList = new List<Model.ReportMarrowFull>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.ReportMarrowFull model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.ReportMarrowFull();
					if(dt.Rows[n]["ReportFormID"]!=null && dt.Rows[n]["ReportFormID"].ToString()!="")
					{
					model.ReportFormID=dt.Rows[n]["ReportFormID"].ToString();
					}
					if(dt.Rows[n]["ReportItemID"]!=null && dt.Rows[n]["ReportItemID"].ToString()!="")
					{
						model.ReportItemID=int.Parse(dt.Rows[n]["ReportItemID"].ToString());
					}
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
						model.FormNo=int.Parse(dt.Rows[n]["FormNo"].ToString());
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
			return dal.GetAllList();
		}

        public DataSet GetColumns()
        {
            return dal.GetColumns();
        }
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

        public DataSet GetList(Model.ReportMarrowFull t)
        {
            return dal.GetList(t);
        }

        public List<Model.ReportMarrowFull> GetModelList(Model.ReportMarrowFull t)
        {
            DataSet ds = dal.GetList(t);
            return DataTableToList(ds.Tables[0]);
        }

        #endregion


        public bool CheckReportMarrowCenter(DataSet dsReportMarrowFull, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            if (dsReportMarrowFull.Tables[0].Columns.Contains("SampleTypeNo"))
            {
                for (int i = 0; i < dsReportMarrowFull.Tables[0].Rows.Count; i++)
                {
                    if (dsReportMarrowFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsReportMarrowFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                    {
                                stringList.Add(dsReportMarrowFull.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                    }
                }
                if (stringList.Count > 0)
                {
                    result = idstc.CheckIncludeCenterCode(stringList, DestiOrgID);
                    if (!result)
                    {
                        for (int j = 0; j < stringList.Count; j++)
                        {
                            SamplleTypeControl.SampleTypeNo = Convert.ToInt32(stringList[j].Trim());
                            SamplleTypeControl.LabCode = DestiOrgID;
                            int count = idstc.GetTotalCount(SamplleTypeControl);
                            if (count <= 0)
                            {
                                ReturnDescription = String.Format("中心端的SampleTypeNo={0}的编号未和实验室的对照", SamplleTypeControl.SampleTypeNo);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("实验室内样本类型为空，无法进行对照");
                    return false;
                }
            }
            if (dsReportMarrowFull.Tables[0].Columns.Contains("ParItemNo"))
            {
                for (int count = 0; count < dsReportMarrowFull.Tables[0].Rows.Count; count++)
                {
                    if (dsReportMarrowFull.Tables[0].Rows[count]["ParItemNo"].ToString() != null && dsReportMarrowFull.Tables[0].Rows[count]["ParItemNo"].ToString() != "")
                    {
                        l.Add(dsReportMarrowFull.Tables[0].Rows[count]["ParItemNo"].ToString());
                    }
                }
                if (l.Count > 0)
                {
                    result = idtic.CheckIncludeCenterCode(l, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < l.Count; n++)
                        {
                            TestItemControl.ItemNo = l[n].Trim();
                            TestItemControl.ControlLabNo = DestiOrgID;
                            int count = idtic.GetTotalCount(TestItemControl);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("中心端的ParItemNo={0}的编号和实验室的未对照", TestItemControl.ItemNo);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("CheckReportMarrowCenter实验室内项目编码为空，无法进行对照");
                    return false;
                }
            }
             if (dsReportMarrowFull.Tables[0].Columns.Contains("GenderNo"))
            {
                for (int count = 0; count < dsReportMarrowFull.Tables[0].Rows.Count; count++)
                {
                    if (dsReportMarrowFull.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsReportMarrowFull.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                    {
                        ListStr.Add(dsReportMarrowFull.Tables[0].Rows[count]["GenderNo"].ToString());
                    }
                }
                if (ListStr.Count > 0)
                {
                    result = idgtc.CheckIncludeCenterCode(ListStr, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < ListStr.Count; n++)
                        {
                            GenderType.GenderNo = Convert.ToInt32(ListStr[n].Trim());
                            GenderType.LabCode = DestiOrgID;
                            int count = idgtc.GetTotalCount(GenderType);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("中心端的GenderNo={0}的编号和实验室的未对照", GenderType.GenderNo);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("实验室内性别编号为空，无法进行对照");
                    return false;
                }
            }
             return true;
        }

        public bool CheckReportMarrowLab(DataSet dsReportMarrowFull, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            if (dsReportMarrowFull.Tables[0].Columns.Contains("SampleType"))
            {
                for (int i = 0; i < dsReportMarrowFull.Tables[0].Rows.Count; i++)
                {
                    if (dsReportMarrowFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsReportMarrowFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                    {
                        stringList.Add(dsReportMarrowFull.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                    }
                }
                if (stringList.Count > 0)
                {
                    result = idstc.CheckIncludeLabCode(stringList, DestiOrgID);
                    if (!result)
                    {
                        for (int j = 0; j < stringList.Count; j++)
                        {
                            SamplleTypeControl.SampleTypeControlNo = stringList[j].Trim();
                            SamplleTypeControl.LabCode = DestiOrgID;
                            int count = idstc.GetTotalCount(SamplleTypeControl);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("实验室内SampleTypeNo={0}的编号未和中心端的对照", SamplleTypeControl.SampleTypeControlNo);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("实验室内样本类型为空，无法进行对照");
                    return false;
                }
            }
             if (dsReportMarrowFull.Tables[0].Columns.Contains("ParItemNo"))
            {
                for (int count = 0; count < dsReportMarrowFull.Tables[0].Rows.Count; count++)
                {
                    if (dsReportMarrowFull.Tables[0].Rows[count]["ParItemNo"].ToString() != null && dsReportMarrowFull.Tables[0].Rows[count]["ParItemNo"].ToString() != "")
                    {
                        l.Add(dsReportMarrowFull.Tables[0].Rows[count]["ParItemNo"].ToString());
                    }
                }
                if (l.Count > 0)
                {
                    result = idtic.CheckIncludeLabCode(l, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < l.Count; n++)
                        {
                            TestItemControl.ControlItemNo = l[n].Trim();
                            TestItemControl.ControlLabNo = DestiOrgID;
                            int count = idtic.GetTotalCount(TestItemControl);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("实验室内ParItemNo={0}的编号和中心的未对照", TestItemControl.ControlItemNo);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription = String.Format("CheckReportMarrowLab实验室内项目编码为空，无法进行对照");
                    return false;
                }
            }
            if (dsReportMarrowFull.Tables[0].Columns.Contains("GenderNo"))
            {
                for (int count = 0; count < dsReportMarrowFull.Tables[0].Rows.Count; count++)
                {
                    if (dsReportMarrowFull.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsReportMarrowFull.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                    {
                        ListStr.Add(dsReportMarrowFull.Tables[0].Rows[count]["GenderNo"].ToString());
                    }
                }
                if (ListStr.Count > 0)
                {
                    result = idgtc.CheckIncludeLabCode(ListStr, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < ListStr.Count; n++)
                        {
                            GenderType.GenderControlNo = ListStr[n].Trim();
                            GenderType.LabCode = DestiOrgID;
                            int count = idgtc.GetTotalCount(GenderType);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("实验室内GenderNo={0}的编号和中心的未对照", GenderType.GenderControlNo);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("实验室内性别编号为空，无法进行对照");
                    return false;
                }
            }
            return true;
        }

        public int BackUpReportMarrowFullByWhere(string Strwhere)
        {
            return dal.BackUpReportMarrowFullByWhere(Strwhere);
        }
    }
}

