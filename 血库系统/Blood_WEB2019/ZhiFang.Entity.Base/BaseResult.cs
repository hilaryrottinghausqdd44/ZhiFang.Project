using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Entity.Base
{
    [DataContract]
    public class BaseResult
    {
        private bool successFlag = true;
        private string errorInfo = "";
        private bool hasInterface = false;
        private bool interfaceSuccess = true;

        [DataMember]
        public bool success
        {
            get { return successFlag; }
            set { successFlag = value; }
        }
        [DataMember]
        public string ErrorInfo
        {
            get { return errorInfo; }
            set { errorInfo = value; }
        }
        /// <summary>
        /// 是否调用接口
        /// </summary>
        [DataMember]
        public bool HasInterface
        {
            get { return hasInterface; }
            set { hasInterface = value; }
        }
        /// <summary>
        /// 调用接口是否成功标志
        /// </summary>
        [DataMember]
        public bool InterfaceSuccess
        {
            get { return interfaceSuccess; }
            set { interfaceSuccess = value; }
        }
        /// <summary>
        /// 调用接口返回的信息
        /// </summary>
        [DataMember]
        public string InterfaceMsg { get; set; }
    }
    public class BaseResultBool : BaseResult
    {
        [DataMember]
        public bool BoolFlag { get; set; }
        [DataMember]
        public string BoolInfo { get; set; }
    }
    
    [DataContract]
    public class BaseResultTree : BaseResult
    {
        [DataMember]
        public List<tree> Tree { get; set; }

    }
    [DataContract]
    public class BaseResultTree<T> : BaseResult
    {
        [DataMember]
        public List<tree<T>> Tree { get; set; }

    }
    [DataContract]
    public class BaseResultList<T> : BaseResult
    {
        [DataMember]
        public EntityList<T> list { get; set; }

    }
     public class BaseResultDataValue : BaseResult
    {
        private string resultdataformattype = "JSON";

        /// <summary>
        /// 返回数据格式，例如：JSON，XML等,默认为JSON
        /// </summary>
        [DataMember]
        public string ResultDataFormatType
        {
            get { return resultdataformattype; }
            set { resultdataformattype = value; }
        }
        [DataMember]
        public string ResultDataValue { get; set; }
        /// <summary>
        /// 定制属性,输血过程记录批量录入使用
        /// </summary>
        [DataMember]
        public string BatchSignValue { get; set; }

    }

    public class BaseResultData
    {
        private string _dataformat = "JSON";
        private bool successFlag = true;

        [DataMember]
        public string dataformat
        {
            get { return _dataformat; }
            set { _dataformat = value; }
        }

        [DataMember]
        public bool success
        {
            get { return successFlag; }
            set { successFlag = value; }
        }

        [DataMember]
        public string data { get; set; }

        [DataMember]
        public string code { get; set; }

        [DataMember]
        public string message { get; set; }
    }
}
