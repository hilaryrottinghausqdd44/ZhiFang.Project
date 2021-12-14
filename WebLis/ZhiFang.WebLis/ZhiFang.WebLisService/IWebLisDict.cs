using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ZhiFang.WebLisService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IWebLisDictClient”。
    [ServiceContract]
    public interface IWebLisDict
    {
        [OperationContract]
        void DoWork();
        #region 医生字典

        [OperationContract]
        bool AddDoctor(Model.Doctor model);
        [OperationContract]
        bool AddDoctorList(List<Model.Doctor> modellist);

        [OperationContract]
        bool ModifyDoctor(Model.Doctor model);
        [OperationContract]
        bool ModifyDoctorList(List<Model.Doctor> modelList);
        [OperationContract]
        bool DelDoctor(Model.Doctor model);
        [OperationContract]
        List<Model.Doctor> GetDoctor();
        #endregion

        #region 部门字典

        [OperationContract]
        bool AddDepartment(Model.Department model);
        [OperationContract]
        bool AddDepartmentList(List<Model.Department> modelList);
        [OperationContract]
        bool ModifyDepartment(Model.Department model);
        [OperationContract]
        bool ModifyDepartmentList(List<Model.Department> modelList);

        [OperationContract]
        bool DelDepartment(Model.Department model);
        [OperationContract]
        List<Model.Department> GetDepartment();
        #endregion

        #region 项目字典

        [OperationContract]
        bool AddTestItem(Model.TestItem model);
        [OperationContract]
        bool AddTestItemList(List<Model.TestItem> modelList);
        [OperationContract]
        bool ModifyTestItem(Model.TestItem model);
        [OperationContract]
        bool ModifyTestItemList(List<Model.TestItem> modelList);
        [OperationContract]
        bool DelTestItem(Model.TestItem model);
        [OperationContract]
        List<Model.TestItem> GetTestItem();
        #endregion


        #region 送检单位字典

        [OperationContract]
        bool AddClientele(Model.CLIENTELE model);

        [OperationContract]
        bool AddClienteleList(List<Model.CLIENTELE> modelList);
        [OperationContract]
        bool ModifyClientele(Model.CLIENTELE model);
        [OperationContract]
        bool ModifyClienteleList(List<Model.CLIENTELE> modelList);
        [OperationContract]
        bool DelClientele(Model.CLIENTELE model);
        [OperationContract]
        List<Model.CLIENTELE> GetClientele();
        #endregion

        #region 样本类型字典

        [OperationContract]
        bool AddSampleType(Model.SampleType model);
        [OperationContract]
        bool AddSampleTypeList(List<Model.SampleType> modelList);
        [OperationContract]
        bool ModifySampleType(Model.SampleType model);
        [OperationContract]
        bool ModifySampleTypeList(List<Model.SampleType> modelList);
        [OperationContract]
        bool DelSampleType(Model.SampleType model);
        [OperationContract]
        List<Model.SampleType> GetSampleType();
        #endregion


        #region 就诊类型字典
        [OperationContract]
        bool AddSickType(Model.SickType model);

        [OperationContract]
        bool AddSickTypeList(List<Model.SickType> modelList);
        [OperationContract]
        bool ModifySickType(Model.SickType model);
        [OperationContract]
        bool ModifySickTypeList(List<Model.SickType> modelList);
        [OperationContract]
        bool DelSickType(Model.SickType model);
        [OperationContract]
        List<Model.SickType> GetSickType();
        #endregion

        #region 小组字典

        [OperationContract]
        bool AddPGroup(Model.PGroup model);
        [OperationContract]
        bool AddPGroupList(List<Model.PGroup> modelList);
        [OperationContract]
        bool ModifyPGroup(Model.PGroup model);
        [OperationContract]
        bool ModifyPGroupList(List<Model.PGroup> modelList);
        [OperationContract]
        bool DelPGroup(Model.PGroup model);
        [OperationContract]
        List<Model.PGroup> GetPGroup();
        #endregion


    }
}
