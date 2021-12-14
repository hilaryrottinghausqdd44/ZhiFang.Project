using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
	/// <summary>
	/// ҵ���߼���PGroup ��ժҪ˵����
	/// </summary>
    public class BPGroup 
	{
        private readonly IDPGroup dal = DalFactory<IDPGroup>.GetDal("PGroup");
        public BPGroup()
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
		public bool Exists(int SectionNo,int Visible)
		{
			return dal.Exists(SectionNo,Visible);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(Model.PGroup model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
        public int Update(Model.PGroup model)
		{
            return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
        public int Delete(int SectionNo, int Visible)
		{

            return dal.Delete(SectionNo, Visible);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public Model.PGroup GetModel(int SectionNo,int Visible)
		{
			
			return dal.GetModel(SectionNo,Visible);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public Model.PGroup GetModelByCache(int SectionNo,int Visible)
		{
			
			string CacheKey = "PGroupModel-" + SectionNo+Visible;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SectionNo,Visible);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.PGroup)objModel;
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
        public DataSet GetList(Model.PGroup model)
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
		public List<Model.PGroup> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<Model.PGroup> DataTableToList(DataTable dt)
		{
			List<Model.PGroup> modelList = new List<Model.PGroup>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.PGroup model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.PGroup();
					if(dt.Rows[n]["SectionNo"].ToString()!="")
					{
						model.SectionNo=int.Parse(dt.Rows[n]["SectionNo"].ToString());
					}
					if(dt.Rows[n]["SuperGroupNo"].ToString()!="")
					{
						model.SuperGroupNo=int.Parse(dt.Rows[n]["SuperGroupNo"].ToString());
					}
					model.CName=dt.Rows[n]["CName"].ToString();
					model.ShortName=dt.Rows[n]["ShortName"].ToString();
					model.ShortCode=dt.Rows[n]["ShortCode"].ToString();
					model.SectionDesc=dt.Rows[n]["SectionDesc"].ToString();
					if(dt.Rows[n]["SectionType"].ToString()!="")
					{
						model.SectionType=int.Parse(dt.Rows[n]["SectionType"].ToString());
					}
					if(dt.Rows[n]["Visible"].ToString()!="")
					{
						model.Visible=int.Parse(dt.Rows[n]["Visible"].ToString());
					}
					if(dt.Rows[n]["DispOrder"].ToString()!="")
					{
						model.DispOrder=int.Parse(dt.Rows[n]["DispOrder"].ToString());
					}
					if(dt.Rows[n]["onlinetime"].ToString()!="")
					{
						model.onlinetime=int.Parse(dt.Rows[n]["onlinetime"].ToString());
					}
					if(dt.Rows[n]["KeyDispOrder"].ToString()!="")
					{
						model.KeyDispOrder=int.Parse(dt.Rows[n]["KeyDispOrder"].ToString());
					}
					if(dt.Rows[n]["dummygroup"].ToString()!="")
					{
						model.dummygroup=int.Parse(dt.Rows[n]["dummygroup"].ToString());
					}
					if(dt.Rows[n]["uniontype"].ToString()!="")
					{
						model.uniontype=int.Parse(dt.Rows[n]["uniontype"].ToString());
					}
					if(dt.Rows[n]["SectorTypeNo"].ToString()!="")
					{
						model.SectorTypeNo=int.Parse(dt.Rows[n]["SectorTypeNo"].ToString());
					}
					if(dt.Rows[n]["SampleRule"].ToString()!="")
					{
						model.SampleRule=int.Parse(dt.Rows[n]["SampleRule"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Model.PGroup> GetModelList(Model.PGroup model)
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

		#endregion  ��Ա����

        #region IBPGroup ��Ա


        public Model.PGroup GetModel(string ClientNo, int SectionNo, int Visible)
        {
            return dal.GetModel(ClientNo, SectionNo, Visible);
        }

        #endregion
    }
}

