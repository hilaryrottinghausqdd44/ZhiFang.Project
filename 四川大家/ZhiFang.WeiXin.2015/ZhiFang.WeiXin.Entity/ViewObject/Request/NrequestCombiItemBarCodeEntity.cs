using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.Entity.ViewObject.Request
{
    [DataContract]
    public class NrequestCombiItemBarCodeEntity
    {
        public NrequestCombiItemBarCodeEntity() { }
        /// <summary>
        /// Edit/Add
        /// </summary>
        [DataMember]
        public string flag { get; set; }

        /// <summary>
        /// 表单信息
        /// </summary>
        [DataMember]
        public NRequestForm NrequestForm { get; set; }

        /// <summary>
        /// 组合项目
        /// </summary>
        [DataMember]
        public List<UiCombiItem> CombiItems { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        [DataMember]
        public List<UiBarCode> BarCodeList { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        [DataMember]
        public string PayCode { get; set; }

    }

    [DataContract]
    public class NrequestCombiItemBarCodeEntity_RBAC : NrequestCombiItemBarCodeEntity
    {
        [DataMember]
        public string Account { get; set; }

        [DataMember]
        public string PWD { get; set; }
    }

    public class UiCombiItem
    {
        public UiCombiItem() { }
        public string CombiItemName { get; set; }
        public string CombiItemNo { get; set; }
        public string Prices { get; set; }
        public List<UiCombiItemDetail> CombiItemDetailList { get; set; }
        public string ColorName { get; set; }
        public string ColorValue { get; set; }
    }
    public class UiBarCode
    {
        public UiBarCode() { }

        public string BarCode { get; set; }

        public string SampleType { get; set; }
        public string SampleTypeName { get; set; }
        public List<string> ItemList { get; set; }
        public List<string> ItemNameList { get; set; }

        public string ColorName { get; set; }

        public string ColorValue { get; set; }

        public List<SampleTypeDetail> SampleTypeDetailList { get; set; }
    }

    public class UiCombiItemDetail
    {
        public UiCombiItemDetail() { }
        //[DataMember]
        public string CombiItemDetailNo { get; set; }
        //[DataMember]
        public string CombiItemDetailName { get; set; }
    }
}
