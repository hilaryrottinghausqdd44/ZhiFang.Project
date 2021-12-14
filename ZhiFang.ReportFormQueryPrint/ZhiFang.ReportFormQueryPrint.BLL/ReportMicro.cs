using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
	/// <summary>
	/// ҵ���߼���ReportMicro ��ժҪ˵����
	/// </summary>
    public class BReportMicro 
	{
        private readonly IDReportMicro dal = DalFactory<IDReportMicro>.GetDal("ReportMicro");
        public BReportMicro()
		{}
		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
        public bool Exists(int ResultNo, int ItemNo, string FormNo)
		{
			return dal.Exists(ResultNo,ItemNo,FormNo);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(Model.ReportMicro model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
        public int Update(Model.ReportMicro model)
		{
            return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
        public int Delete(int ResultNo, int ItemNo, string FormNo)
		{

            return dal.Delete(ResultNo, ItemNo, FormNo);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
        public Model.ReportMicro GetModel(int ResultNo, int ItemNo, string FormNo)
		{
			
			return dal.GetModel(ResultNo,ItemNo,FormNo);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
        public Model.ReportMicro GetModelByCache(int ResultNo, int ItemNo, string FormNo)
		{
			
			string CacheKey = "ReportMicroModel-" + ResultNo+ItemNo+FormNo;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ResultNo,ItemNo,FormNo);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.ReportMicro)objModel;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(Model.ReportMicro model)
        {
            return dal.GetList(model);
        }
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<Model.ReportMicro> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<Model.ReportMicro> DataTableToList(DataTable dt)
		{
			List<Model.ReportMicro> modelList = new List<Model.ReportMicro>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.ReportMicro model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.ReportMicro();
					if(dt.Rows[n]["ResultNo"].ToString()!="")
					{
						model.ResultNo=int.Parse(dt.Rows[n]["ResultNo"].ToString());
					}
					if(dt.Rows[n]["ItemNo"].ToString()!="")
					{
						model.ItemNo=int.Parse(dt.Rows[n]["ItemNo"].ToString());
					}
					if(dt.Rows[n]["DescNo"].ToString()!="")
					{
						model.DescNo=int.Parse(dt.Rows[n]["DescNo"].ToString());
					}
					if(dt.Rows[n]["MicroNo"].ToString()!="")
					{
						model.MicroNo=int.Parse(dt.Rows[n]["MicroNo"].ToString());
					}
					model.MicroDesc=dt.Rows[n]["MicroDesc"].ToString();
					if(dt.Rows[n]["AntiNo"].ToString()!="")
					{
						model.AntiNo=int.Parse(dt.Rows[n]["AntiNo"].ToString());
					}
					model.Suscept=dt.Rows[n]["Suscept"].ToString();
					if(dt.Rows[n]["SusQuan"].ToString()!="")
					{
						model.SusQuan=decimal.Parse(dt.Rows[n]["SusQuan"].ToString());
					}
					model.SusDesc=dt.Rows[n]["SusDesc"].ToString();
					model.RefRange=dt.Rows[n]["RefRange"].ToString();
					if(dt.Rows[n]["ItemDate"].ToString()!="")
					{
						model.ItemDate=DateTime.Parse(dt.Rows[n]["ItemDate"].ToString());
					}
					if(dt.Rows[n]["ItemTime"].ToString()!="")
					{
						model.ItemTime=DateTime.Parse(dt.Rows[n]["ItemTime"].ToString());
					}
					model.ItemDesc=dt.Rows[n]["ItemDesc"].ToString();
					if(dt.Rows[n]["EquipNo"].ToString()!="")
					{
						model.EquipNo=int.Parse(dt.Rows[n]["EquipNo"].ToString());
					}
					if(dt.Rows[n]["Modified"].ToString()!="")
					{
						model.Modified=int.Parse(dt.Rows[n]["Modified"].ToString());
					}
					if(dt.Rows[n]["IsMatch"].ToString()!="")
					{
						model.IsMatch=int.Parse(dt.Rows[n]["IsMatch"].ToString());
					}
					if(dt.Rows[n]["CheckType"].ToString()!="")
					{
						model.CheckType=int.Parse(dt.Rows[n]["CheckType"].ToString());
					}
					if(dt.Rows[n]["isReceive"].ToString()!="")
					{
						model.isReceive=int.Parse(dt.Rows[n]["isReceive"].ToString());
					}
					if(dt.Rows[n]["FormNo"].ToString()!="")
					{
						model.FormNo=dt.Rows[n]["FormNo"].ToString();
					}
					model.microcountdesc=dt.Rows[n]["microcountdesc"].ToString();
					if(dt.Rows[n]["mresulttype"].ToString()!="")
					{
						model.mresulttype=int.Parse(dt.Rows[n]["mresulttype"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Model.ReportMicro> GetModelList(Model.ReportMicro model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

        public DataTable GetReportMicroList(string FormNo)
        {
            //throw new NotImplementedException();
            return dal.GetReportMicroList(FormNo);
        }


        #region IBReportMicro ��Ա


        public DataSet GetListByDataSource(Model.ReportMicro rm_m)
        {
            throw new NotImplementedException();
        }

        #endregion

        public DataTable GetReportMicroGroupList(string FormNo)
        {
            return dal.GetReportMicroGroupList(FormNo);
        }

        internal DataTable GetReportMicroGroupListForSTestType(string FormNo)
        {
            return dal.GetReportMicroGroupListForSTestType(FormNo);
        }

        public DataSet GetReportMicroFullByReportFormId(string reportformid) {
           return dal.GetReportMicroFullByReportFormId(reportformid);
        }

        public int UpdateReportMicroFull(ReportMicroFull model) {

            return dal.UpdateReportMicroFull(model); ;
        }
    }
}

