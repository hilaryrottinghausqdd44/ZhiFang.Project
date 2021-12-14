using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Tools;

namespace ZhiFang.Model
{
    [DataContract]
    public class BPhysicalExamType
    {
        public BPhysicalExamType()
        { }
        #region Model
        private long? _id;
        private string _cname;
        private string _shortCode;
        [DataMember]
        public long? Id
        {
            set { _id = value; }
            get { return _id; }
        }
        [DataMember]
        public string CName
        {
            set { _cname = value; }
            get { return _cname; }
        }
        [DataMember]
        public string ShortCode
        {
            set { _shortCode = value; }
            get { return _shortCode; }
        }
        private int? _disporder;
        [DataMember]
        public int? DispOrder
        {
            get { return _disporder; }
            set { _disporder = value; }
        }

        private int? _visible;
        [DataMember]
        public int? Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        private byte[] _dtimestampe;
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public byte[] DTimeStampe
        {
            get { return _dtimestampe; }
            set { _dtimestampe = value; }
        }
        private DateTime? _dataAddTime;

        [DataMember]
        public DateTime? DataAddTime
        {
            get { return _dataAddTime; }
            set { _dataAddTime = value; }
        }
        private string _orderfield = "DispOrder";//排序
        [DataMember]
        public string OrderField
        {
            get { return _orderfield; }
            set { _orderfield = value; }
        }
        private string _searchlikekey;//模糊查询字段
        [DataMember]
        public string SearchLikeKey
        {
            get { return _searchlikekey; }
            set { _searchlikekey = value; }
        }
        #endregion Model
    }
}
