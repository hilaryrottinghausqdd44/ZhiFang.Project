using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Model.UiModel
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
        public Model.NRequestForm NrequestForm { get; set; }

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
    public class NrequestCombiItemBarCodeEntity_RBAC: NrequestCombiItemBarCodeEntity
    {
        [DataMember]
        public string Account { get; set; }

        [DataMember]
        public string PWD { get; set; }
    }

}
