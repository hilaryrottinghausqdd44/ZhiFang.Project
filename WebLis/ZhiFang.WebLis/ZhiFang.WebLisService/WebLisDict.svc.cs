using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using System.Data;
namespace ZhiFang.WebLisService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“WebLisDictClient”。
    public class WebLisDict : IWebLisDict
    {
        public void DoWork()
        {
        }

        #region 医生字典
        IBLL.Common.BaseDictionary.IBDoctor ibDoc = BLLFactory<IBDoctor>.GetBLL();

        public bool AddDoctor(Model.Doctor model)
        {
           
            bool result = false;
            int i = ibDoc.Add(model);
            if (i > 0)
                result = true;
            return result;
        }


        public bool AddDoctorList(List<Model.Doctor> modellist)
        {
            bool result = false;
            int i = ibDoc.Add(modellist);
            if (i > 0)
                result = true;
            return result;
        }
        public bool ModifyDoctor(Model.Doctor model)
        {
            bool result = false;
            int i = ibDoc.Update(model);
            if (i > 0)
                result = true;
            return result;
        }


        public bool ModifyDoctorList(List<Model.Doctor> modelList)
        {
            bool result = false;
            int i = ibDoc.Update(modelList);
            if (i > 0)
                result = true;
            return result;
        }
        public bool DelDoctor(Model.Doctor model)
        {
            bool result = false;
            int i = ibDoc.Delete(model.DoctorNo);
            if (i > 0)
                result = true;
            return result;
        }

        public List<Model.Doctor> GetDoctor()
        {
            List<Model.Doctor> docList = new List<Model.Doctor>();
            DataSet ds = ibDoc.GetAllList();
            if (ds != null)
            {
                docList = ibDoc.DataTableToList(ds.Tables[0]);
            }
            return docList;
        }
        #endregion

        #region 部门字典
        IBLL.Common.BaseDictionary.IBDepartment ibDepart = BLLFactory<IBDepartment>.GetBLL();

        public bool AddDepartment(Model.Department model)
        {
            bool result = false;
            int i = ibDepart.Add(model);
            if (i > 0)
                result = true;
            return result;
        }
        public bool AddDepartmentList(List<Model.Department> modelList)
        {
            bool result = false;
            int i = ibDepart.Add(modelList);
            if (i > 0)
                result = true;
            return result;
        }
        public bool ModifyDepartment(Model.Department model)
        {
            bool result = false;
            int i = ibDepart.Update(model);
            if (i > 0)
                result = true;
            return result;
        }
        public bool ModifyDepartmentList(List<Model.Department> modelList)
        {
            bool result = false;
            int i = ibDepart.Update(modelList);
            if (i > 0)
                result = true;
            return result;
        }
        public bool DelDepartment(Model.Department model)
        {
            bool result = false;
            int i = ibDepart.Delete(model.DeptNo);
            if (i > 0)
                result = true;
            return result;
        }

        public List<Model.Department> GetDepartment()
        {
            List<Model.Department> departList = new List<Model.Department>();
            DataSet ds = ibDepart.GetAllList();
            if (ds != null)
            {
                departList = ibDepart.DataTableToList(ds.Tables[0]);
            }
            return departList;
        }
        #endregion

        #region 项目字典
        IBLL.Common.BaseDictionary.IBTestItem ibTestItem = BLLFactory<IBTestItem>.GetBLL();

        public bool AddTestItem(Model.TestItem model)
        {
            bool result = false;
            int i = ibTestItem.Add(model);
            if (i > 0)
                result = true;
            return result;
        }

        public bool AddTestItemList(List<Model.TestItem> modelList)
        {
            bool result = false;
            int i = ibTestItem.Add(modelList);
            if (i > 0)
                result = true;
            return result;
        }
        public bool ModifyTestItem(Model.TestItem model)
        {
            bool result = false;
            int i = ibTestItem.Update(model);
            if (i > 0)
                result = true;
            return result;
        }
        public bool ModifyTestItemList(List<Model.TestItem> modelList)
        {
            bool result = false;
            int i = ibTestItem.Update(modelList);
            if (i > 0)
                result = true;
            return result;
        }
        public bool DelTestItem(Model.TestItem model)
        {
            bool result = false;
            int i = ibTestItem.Delete(model.ItemNo);
            if (i > 0)
                result = true;
            return result;
        }

        public List<Model.TestItem> GetTestItem()
        {
            List<Model.TestItem> departList = new List<Model.TestItem>();
            DataSet ds = ibTestItem.GetAllList();
            if (ds != null)
            {
                departList = ibTestItem.DataTableToList(ds.Tables[0]);
            }
            return departList;
        }
        #endregion

        #region 送检单位字典
        IBLL.Common.BaseDictionary.IBCLIENTELE ibClientele = BLLFactory<IBCLIENTELE>.GetBLL();

        public bool AddClientele(Model.CLIENTELE model)
        {
            bool result = false;
            int i = ibClientele.Add(model);
            if (i > 0)
                result = true;
            return result;
        }

        public bool AddClienteleList(List<Model.CLIENTELE> modelList)
        {
            bool result = false;
            int i = ibClientele.Add(modelList);
            if (i > 0)
                result = true;
            return result;
        }
        public bool ModifyClientele(Model.CLIENTELE model)
        {
            bool result = false;
            int i = ibClientele.Update(model);
            if (i > 0)
                result = true;
            return result;
        }
        public bool ModifyClienteleList(List<Model.CLIENTELE> modelList)
        {
            bool result = false;
            int i = ibClientele.Update(modelList);
            if (i > 0)
                result = true;
            return result;
        }
        public bool DelClientele(Model.CLIENTELE model)
        {
            bool result = false;
            int i = ibClientele.Delete(long.Parse(model.ClIENTNO));
            if (i > 0)
                result = true;
            return result;
        }

        public List<Model.CLIENTELE> GetClientele()
        {
            List<Model.CLIENTELE> departList = new List<Model.CLIENTELE>();
            DataSet ds = ibClientele.GetAllList();
            if (ds != null)
            {
                departList = ibClientele.DataTableToList(ds.Tables[0]);
            }
            return departList;
        }
        #endregion

        #region 样本类型字典
        IBLL.Common.BaseDictionary.IBSampleType ibSampleType = BLLFactory<IBSampleType>.GetBLL();

        public bool AddSampleType(Model.SampleType model)
        {
            bool result = false;
            int i = ibSampleType.Add(model);
            if (i!=0)
                result = true;
            return result;
        }
        public bool AddSampleTypeList(List<Model.SampleType> modelList)
        {
            bool result = false;
            int i = ibSampleType.Add(modelList);
            if (i != 0)
                result = true;
            return result;
        }

        public bool ModifySampleType(Model.SampleType model)
        {
            bool result = false;
            int i = ibSampleType.Update(model);
            if (i != 0)
                result = true;
            return result;
        }
        public bool ModifySampleTypeList(List<Model.SampleType> modelList)
        {
            bool result = false;
            int i = ibSampleType.Update(modelList);
            if (i != 0)
                result = true;
            return result;
        }
        public bool DelSampleType(Model.SampleType model)
        {
            bool result = false;
            int i = ibSampleType.Delete(model.SampleTypeNo);
            if (i != 0)
                result = true;
            return result;
        }

        public List<Model.SampleType> GetSampleType()
        {
            List<Model.SampleType> departList = new List<Model.SampleType>();
            DataSet ds = ibSampleType.GetAllList();
            if (ds != null)
            {
                departList = ibSampleType.DataTableToList(ds.Tables[0]);
            }
            return departList;
        }
        #endregion

        #region 就诊类型字典
        IBLL.Common.BaseDictionary.IBSickType ibSickType = BLLFactory<IBSickType>.GetBLL();

        public bool AddSickType(Model.SickType model)
        {
            bool result = false;
            int i = ibSickType.Add(model);
            if (i > 0)
                result = true;
            return result;
        }

        public bool AddSickTypeList(List<Model.SickType> modelList)
        {
            bool result = false;
            int i = ibSickType.Add(modelList);
            if (i > 0)
                result = true;
            return result;
        }

        public bool ModifySickType(Model.SickType model)
        {
            bool result = false;
            int i = ibSickType.Update(model);
            if (i > 0)
                result = true;
            return result;
        }
        public bool ModifySickTypeList(List<Model.SickType> modelList)
        {
            bool result = false;
            int i = ibSickType.Update(modelList);
            if (i > 0)
                result = true;
            return result;
        }
        public bool DelSickType(Model.SickType model)
        {
            bool result = false;
            int i = ibSickType.Delete(model.SickTypeNo);
            if (i > 0)
                result = true;
            return result;
        }

        public List<Model.SickType> GetSickType()
        {
            List<Model.SickType> departList = new List<Model.SickType>();
            DataSet ds = ibSickType.GetAllList();
            if (ds != null)
            {
                departList = ibSickType.DataTableToList(ds.Tables[0]);
            }
            return departList;
        }
        #endregion

        #region 小组字典
        IBLL.Common.BaseDictionary.IBPGroup ibPGroup = BLLFactory<IBPGroup>.GetBLL();

        public bool AddPGroup(Model.PGroup model)
        {
            bool result = false;
            int i = ibPGroup.Add(model);
            if (i > 0)
                result = true;
            return result;
        }

        public bool AddPGroupList(List<Model.PGroup> modelList)
        {
            bool result = false;
            int i = ibPGroup.Add(modelList);
            if (i > 0)
                result = true;
            return result;
        }

        public bool ModifyPGroup(Model.PGroup model)
        {
            bool result = false;
            int i = ibPGroup.Update(model);
            if (i > 0)
                result = true;
            return result;
        }
        public bool ModifyPGroupList(List<Model.PGroup> modelList)
        {
            bool result = false;
            int i = ibPGroup.Update(modelList);
            if (i > 0)
                result = true;
            return result;
        }
        public bool DelPGroup(Model.PGroup model)
        {
            bool result = false;
            int i = ibPGroup.Delete((int)model.SectionNo);
            if (i > 0)
                result = true;
            return result;
        }

        public List<Model.PGroup> GetPGroup()
        {
            List<Model.PGroup> departList = new List<Model.PGroup>();
            DataSet ds = ibPGroup.GetAllList();
            if (ds != null)
            {
                departList = ibPGroup.DataTableToList(ds.Tables[0]);
            }
            return departList;
        }
        #endregion

                  
    }
}
