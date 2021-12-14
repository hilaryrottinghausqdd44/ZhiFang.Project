using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "样本单列表VO", ClassCName = "LisBarCodeFormVo", ShortCode = "LisBarCodeFormVo", Desc = "样本单列表VO")]
    public class LisBarCodeFormVo
    {
        [DataMember]
        public LisBarCodeForm LisBarCodeForm { get; set; }
        /// <summary>
        /// 分管类型属性:1.分管成功，2.无采样组，3.没有默认采样组,4.不参与分管
        /// </summary>
        [DataMember]
        public string SampleGroupingType { get; set; }

        [DataMember]
        public string HospitalName { get; set; }
        [DataMember]
        public string ExecDeptName { get; set; }
        [DataMember]
        public string DestinationName { get; set; }
        [DataMember]
        public string SamplingGroupName { get; set; }
        [DataMember]
        public string SampleTypeName { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string OrderTypeName { get; set; }
        [DataMember]
        public string BarCodeFormStatus { get; set; }
        [DataMember]
        public string RefuseAcceptReason { get; set; }//拒收原因
        [DataMember]
        public string RefuseAcceptPerson { get; set; }//拒收人
        [DataMember]
        public string RefuseAcceptAdvice { get; set; }//拒收处理意见
        [DataMember]
        public string ItemId { get; set; }
        [DataMember]
        public int count { get; set; }//所在打包号中样本总数量
        [DataMember]
        public int hasSignForCount { get; set; }//所在打包号中已签收数量
        [DataMember]
        public int notSignForCount { get; set; }//所在打包号中未签收数量
        [DataMember]
        public string failureInfo { get; set; }//提示内容
        [DataMember]
        public bool isConstraintUpdate { get; set; }//标志是否强制更新状态使用
        [DataMember]
        public string SignForMan { get; set; }//签收人
        [DataMember]
        public string PrintDispenseTagAndFlowSheetInfo { get; set; }//样本分发要打印的分发标签和流转单相关信息List
        [DataMember]
        public bool IsPrep { get; set; }//是否预制
        [DataMember]
        public string PreInfo { get; set; }//管头信息

    }
    public class VerifyReminInfo 
    { 
        public string alterMode { get; set; }//提示模式0：不校验1：不允许且提示；2：不允许不提示；3：允许且提示；4：用户自行选择
        public string failureInfo { get; set; }
    }
    //HIS核收接口返回信息VO
    [DataContract]
    public class HISInterfaceHISOrderFromVO : BaseResult
    {
        [DataMember]
        public List<LisPatientVO> LisPatientList { get; set; }
    }
    [DataContract]
    public class LisPatientVO : LisPatient 
    {
        [DataMember]
        public long? PatID { get; set; }
        [DataMember]
        public List<LisOrderFormVO> LisOrderFormList { get; set; }
    }
    [DataContract]
    public class LisOrderFormVO : LisOrderForm 
    {
        [DataMember]
        public long? OrderFormID { get; set; }
        [DataMember]
        public List<LisOrderItemVO> LisOrderItemList { get; set; }
    }
    [DataContract]
    public class LisOrderItemVO : LisOrderItem 
    {
        [DataMember]
        public long? OrderItemID { get; set; }

    }
    public class LisBarCodeItemVo
    {
        public LisBarCodeItem LisBarCodeItem { get; set; }//项目
        
        public long BarCodeItemID { get; set; }//项目主键id
        
        public long SectionItemID { get; set; }//小组项目id
        
        public long ItemID { get; set; }//项目id
        
        public string ItemName { get; set; }//项目名称
        public long SectionId { get; set; }//小组id
        
        public int Disporder { get; set; }//显示次序
        
        public LBTranRule LBTranRule { get; set; }//项目匹配到的规则

        public LBSectionItem LBSectionItem { get; set; }//最终项目对应的小组项目
        public int DispenseSeictionDispOrder { get; set; }//规则所在分发小组顺序

        public List<LBSectionItem> tempSectionItemList { get; set; }//采样项目同时属于的小组
        
    }
    
    public class LisBarCodeItemVoResp
    {
        [DataMember]
        public string DispenseStatus { get; set; }//分发状态
        [DataMember]
        public string SampleNo { get; set; }//样本号
        [DataMember]
        public string OrderItemName { get; set; }//医嘱项目名称
        [DataMember]
        public string SectionName { get; set; }//小组名称
        [DataMember]
        public string ItemName { get; set; }//项目名称
    }
    public class DispenseTagAndFlowSheetInfoVo
    {
        //[DataMember]
        //public LisTestForm LisTestForm { get; set; }//检验单
        //[DataMember]
        //public List<LisTestItem> LisTestItems { get; set; }//分发项目集合
        [DataMember]
        public LBTranRule LBTranRule { get; set; }//匹配到的规则
        [DataMember]
        public string GSampleNo { get; set; }//样本号
        [DataMember]
        public string ItemNames { get; set; }//分发的项目名称，逗号拼接


    }
}
