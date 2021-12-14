using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IBLL.Common;

namespace BLL
{
	/// <summary>
	/// ҵ���߼���MicroOperationStepFlow ��ժҪ˵����
	/// </summary>
    public class MicroOperationStepFlow : IBLL.IBMicroOperationStepFlow
	{
        private readonly IDMicroOperationStepFlow dal = DalFactory<IDMicroOperationStepFlow>.GetDalByClassName("MicroOperationStepFlow");
        private readonly IDataCache dataCache = DalFactory<IDataCache>.GetDalByClassName("DataCache");
        private readonly IConfigHelper ConfigHelper = DalFactory<IConfigHelper>.GetDalByClassName("ConfigHelper");
		public MicroOperationStepFlow()
		{}
		#region  ��Ա����

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
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(Model.MicroOperationStepFlow model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int Update(Model.MicroOperationStepFlow model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public int Delete(int Id)
		{
			
			return dal.Delete(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public Model.MicroOperationStepFlow GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public Model.MicroOperationStepFlow GetModelByCache(int Id)
		{
			
			string CacheKey = "MicroOperationStepFlowModel-" + Id;
            object objModel = dataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache").Value;
                        dataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.MicroOperationStepFlow)objModel;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
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
		public List<Model.MicroOperationStepFlow> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<Model.MicroOperationStepFlow> DataTableToList(DataTable dt)
		{
			List<Model.MicroOperationStepFlow> modelList = new List<Model.MicroOperationStepFlow>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.MicroOperationStepFlow model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.MicroOperationStepFlow();
					if(dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					if(dt.Rows[n]["StepId"].ToString()!="")
					{
						model.StepId=int.Parse(dt.Rows[n]["StepId"].ToString());
					}
					model.StepName=dt.Rows[n]["StepName"].ToString();
					model.StepInfo=dt.Rows[n]["StepInfo"].ToString();
					if(dt.Rows[n]["StepFlowSort"].ToString()!="")
					{
						model.StepFlowSort=int.Parse(dt.Rows[n]["StepFlowSort"].ToString());
					}
					model.Value=dt.Rows[n]["Value"].ToString();
					model.SampleNo=dt.Rows[n]["SampleNo"].ToString();
					model.ParentSampleNo=dt.Rows[n]["ParentSampleNo"].ToString();
					model.MicroNo=dt.Rows[n]["MicroNo"].ToString();
					model.MicroAntiId=dt.Rows[n]["MicroAntiId"].ToString();
					model.FormNo=dt.Rows[n]["FormNo"].ToString();
					if(dt.Rows[n]["EndFlag"].ToString()!="")
					{
						model.EndFlag=int.Parse(dt.Rows[n]["EndFlag"].ToString());
					}
					if(dt.Rows[n]["UserNo"].ToString()!="")
					{
						model.UserNo=int.Parse(dt.Rows[n]["UserNo"].ToString());
					}
					if(dt.Rows[n]["AddTime"].ToString()!="")
					{
						model.AddTime=DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
					}
					if(dt.Rows[n]["SampleTypeNo"].ToString()!="")
					{
						model.SampleTypeNo=int.Parse(dt.Rows[n]["SampleTypeNo"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
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

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(Model.MicroOperationStepFlow model)
        {
            return dal.GetList(model);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Model.MicroOperationStepFlow> GetModelList(Model.MicroOperationStepFlow model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }

		#endregion  ��Ա����
	}
}

