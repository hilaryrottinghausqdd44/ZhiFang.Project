using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace ZhiFang.Model
{
    [DataContract]
    public class BaseResult
    {
        private bool successFlag = true;
        private string errorInfo = "";
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
    }
    [DataContract]
    public class EntityList<T>
    {
        [DataMember]
        public virtual int count { get; set; }
        [DataMember]
        public virtual IList<T> list { get; set; }
    }

    [DataContract]
    public class BaseResultDataSet
    {
        [DataMember]
        public virtual int total { get; set; }
        [DataMember]
        public virtual DataTable rows { get; set; }
    }

    public class EntityListEasyUI<T>
    {
        public virtual int total { get; set; }
        public virtual List<T> rows { get; set; }
    }
    public class BaseResultBool : BaseResult
    {
        [DataMember]
        public bool BoolFlag { get; set; }
        [DataMember]
        public string BoolInfo { get; set; }
    }
    [DataContract]
    public class BaseResultForm<T> : BaseResult
    {
        [DataMember]
        public T data { get; set; }
    }

    [DataContract]
    public class BaseResultList<T> : BaseResult
    {
        [DataMember]
        public EntityList<T> list { get; set; }

    }
    [DataContract]
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

        [DataMember]
        public string MD5SignString { get; set; }
    }
}
