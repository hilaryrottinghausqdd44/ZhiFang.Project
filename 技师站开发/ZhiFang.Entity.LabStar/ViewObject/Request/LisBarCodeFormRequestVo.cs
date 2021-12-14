using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar.ViewObject.Request
{
    [DataContract]
    [DataDesc(CName = "查询样本单列表入参VO", ClassCName = "LisBarCodeFormRequestVo", ShortCode = "LisBarCodeFormRequestVo", Desc = "查询样本单列表入参VO")]
    public class LisBarCodeFormRequestVo
    {
        [DataMember]
        public long nodetypeID { get; set; }//站点类型ID
        [DataMember]
        public string fields { get; set; }//查询字段
        [DataMember]
        public bool isPlanish { get; set; }//查询字段
        [DataMember]
        public string SampleStatus { get; set; }//标本状态

        [DataMember]
        public string TimeType { get; set; }//时间类型
        [DataMember]
        public string TimeTypeValue { get; set; }//时间类型的时间值
        [DataMember]
        public long SampleTypeID { get; set; }//样本类型ID
        [DataMember]
        public long DeptID { get; set; }//开单科室ID,对应Lis_Patient表DeptID
        [DataMember]
        public long SectionID { get; set; }//检验小组ID，小组项目表
        [DataMember]
        public string BarCode { get; set; }//条码号
        [DataMember]
        public string PatNo { get; set; }//病历号
        [DataMember]
        public string CName { get; set; }//姓名
        [DataMember]
        public long SignForManID { get; set; }//签收人ID
        [DataMember]
        public long SickTypeID { get; set; }//就诊类型ID
        [DataMember]
        public long ProfessionalLargeGroupID { get; set; }//专业大组ID
        [DataMember]
        public long TestItemID { get; set; }//检验项目ID，对应Lis_BarCodeItem表的BarCodesItemID和OrdersItemID
    }
}
