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
	/// ҵ���߼���TestItem ��ժҪ˵����
	/// </summary>
    public class BTestItem 
    {
        private readonly IDTestItem dal = DalFactory<IDTestItem>.GetDal("TestItem");
        public BTestItem()
        { }
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
        public bool Exists(int ItemNo)
        {
            return dal.Exists(ItemNo);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.TestItem model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Update(Model.TestItem model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public int Delete(int ItemNo)
        {

            return dal.Delete(ItemNo);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.TestItem GetModel(int ItemNo)
        {

            return dal.GetModel(ItemNo);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public Model.TestItem GetModelByCache(int ItemNo)
        {

            string CacheKey = "TestItemModel-" + ItemNo;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ItemNo);
                    if (objModel != null)
                    {
                        int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.TestItem)objModel;
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
        public DataSet GetList(Model.TestItem model)
        {
            return dal.GetList(model);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetListLike(Model.TestItem model)
        {
            return dal.GetListLike(model);
        }
        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Model.TestItem> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Model.TestItem> GetModelList(Model.TestItem model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Model.TestItem> DataTableToList(DataTable dt)
        {
            List<Model.TestItem> modelList = new List<Model.TestItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.TestItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.TestItem();
                    if (dt.Rows[n]["ItemNo"].ToString() != "")
                    {
                        model.ItemNo = int.Parse(dt.Rows[n]["ItemNo"].ToString());
                    }
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.EName = dt.Rows[n]["EName"].ToString();
                    model.ShortName = dt.Rows[n]["ShortName"].ToString();
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    model.DiagMethod = dt.Rows[n]["DiagMethod"].ToString();
                    model.Unit = dt.Rows[n]["Unit"].ToString();
                    if (dt.Rows[n]["IsCalc"].ToString() != "")
                    {
                        model.IsCalc = int.Parse(dt.Rows[n]["IsCalc"].ToString());
                    }
                    if (dt.Rows[n]["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
                    }
                    if (dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    if (dt.Rows[n]["Prec"].ToString() != "")
                    {
                        model.Prec = int.Parse(dt.Rows[n]["Prec"].ToString());
                    }
                    if (dt.Rows[n]["IsProfile"].ToString() != "")
                    {
                        model.IsProfile = int.Parse(dt.Rows[n]["IsProfile"].ToString());
                    }
                    model.OrderNo = dt.Rows[n]["OrderNo"].ToString();
                    model.StandardCode = dt.Rows[n]["StandardCode"].ToString();
                    model.ItemDesc = dt.Rows[n]["ItemDesc"].ToString();
                    if (dt.Rows[n]["FWorkLoad"].ToString() != "")
                    {
                        model.FWorkLoad = decimal.Parse(dt.Rows[n]["FWorkLoad"].ToString());
                    }
                    if (dt.Rows[n]["Secretgrade"].ToString() != "")
                    {
                        model.Secretgrade = int.Parse(dt.Rows[n]["Secretgrade"].ToString());
                    }
                    if (dt.Rows[n]["Cuegrade"].ToString() != "")
                    {
                        model.Cuegrade = int.Parse(dt.Rows[n]["Cuegrade"].ToString());
                    }
                    if (dt.Rows[n]["IsDoctorItem"].ToString() != "")
                    {
                        model.IsDoctorItem = int.Parse(dt.Rows[n]["IsDoctorItem"].ToString());
                    }
                    if (dt.Rows[n]["IschargeItem"].ToString() != "")
                    {
                        model.IschargeItem = int.Parse(dt.Rows[n]["IschargeItem"].ToString());
                    }
                    if (dt.Rows[n]["HisDispOrder"].ToString() != "")
                    {
                        model.HisDispOrder = int.Parse(dt.Rows[n]["HisDispOrder"].ToString());
                    }
                    if (dt.Rows[n]["Price"].ToString() != "")
                    {
                        model.Price = decimal.Parse(dt.Rows[n]["Price"].ToString());
                    }
                    if (dt.Rows[n]["SuperGroupNo"].ToString() != "")
                    {
                        model.SuperGroupNo = int.Parse(dt.Rows[n]["SuperGroupNo"].ToString());
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

        public DataSet GetList(string strWhere,string fields)
        {
            return dal.GetList(strWhere,fields);
        }

        #endregion  ��Ա����
    }
}

