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
    public class BaseResultBool : BaseResult
    {
        [DataMember]
        public bool BoolFlag { get; set; }
        [DataMember]
        public string BoolInfo { get; set; }
    }
    public class ReceiveResultBool : BaseResult
    {
        [DataMember]
        public bool BoolFlag { get; set; }
        [DataMember]
        public string BoolInfo { get; set; }

        private string gSampleNo = "";
        /// <summary>
        /// 核收成功后的小组样本号
        /// </summary>
        [DataMember]
        public string GSampleNo
        {
            get { return gSampleNo; }
            set { gSampleNo = value; }
        }
    }
    [DataContract]
    public class BaseResultForm<T> : BaseResult
    {
        [DataMember]
        public T data { get; set; }
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
    [DataContract]
    public class BaseResultChartLine<T> : BaseResult
    {
        [DataMember]
        public IList<ChartLine<T>> list { get; set; }

    }
    [DataContract]
    public class ChartLine<T>
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Info { get; set; }
        [DataMember]
        public IList<Point<T>> List { get; set; }
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

        //其他业务操作代码,默认为空
        [DataMember]
        public string DataCode { get; set; }
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
