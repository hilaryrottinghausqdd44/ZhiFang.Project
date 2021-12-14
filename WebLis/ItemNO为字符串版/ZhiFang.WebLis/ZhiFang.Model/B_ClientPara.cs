using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Tools;

namespace ZhiFang.Model
{
    /// <summary>
	/// B_ClientPara:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[DataContract]
    public partial class B_ClientPara
    {
        public B_ClientPara()
        { }
        #region Model
        private long _labid;
        private long _parameterid;
        private long? _pdictid;
        private string _name;
        private string _sname;
        private string _paratype;
        private string _parano;
        private string _paravalue;
        private string _paradesc;
        private string _shortcode;
        private int? _disporder;
        private string _pinyinzitou;
        private bool _isuse;
        private bool _isuserset;
        private DateTime? _dataaddtime;
        private DateTime? _dataupdatetime;
        private byte[] _datatimestamp;
        private string _labname;
        /// <summary>
        /// 实验室ID
        /// </summary>

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long LabID
        {
            set { _labid = value; }
            get { return _labid; }
        }
        /// <summary>
        /// 实验室Name
        /// </summary>

        [DataMember]
        public string LabName
        {
            set { _labname = value; }
            get { return _labname; }
        }
        /// <summary>
        /// 参数ID
        /// </summary>

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long ParameterID
        {
            set { _parameterid = value; }
            get { return _parameterid; }
        }
        /// <summary>
        /// 字典Id
        /// </summary>

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long? PDictId
        {
            set { _pdictid = value; }
            get { return _pdictid; }
        }
        /// <summary>
        /// 名称
        /// </summary>

        [DataMember]
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 简称
        /// </summary>
        [DataMember]
        public string SName
        {
            set { _sname = value; }
            get { return _sname; }
        }
        /// <summary>
        /// 参数类型
        ///   用于查询方便可根据用户习惯对参数分类。
        /// </summary>
        [DataMember]
        public string ParaType
        {
            set { _paratype = value; }
            get { return _paratype; }
        }
        /// <summary>
        /// 参数编码
        /// </summary>
        [DataMember]
        public string ParaNo
        {
            set { _parano = value; }
            get { return _parano; }
        }
        /// <summary>
        /// 参数值
        /// </summary>
        [DataMember]
        public string ParaValue
        {
            set { _paravalue = value; }
            get { return _paravalue; }
        }
        /// <summary>
        /// 参数说明
        /// </summary>
        [DataMember]
        public string ParaDesc
        {
            set { _paradesc = value; }
            get { return _paradesc; }
        }
        /// <summary>
        /// 快捷码
        /// </summary>
        [DataMember]
        public string Shortcode
        {
            set { _shortcode = value; }
            get { return _shortcode; }
        }
        /// <summary>
        /// 显示次序
        /// </summary>
        [DataMember]
        public int? DispOrder
        {
            set { _disporder = value; }
            get { return _disporder; }
        }
        /// <summary>
        /// 汉字拼音字头
        /// </summary>
        [DataMember]
        public string PinYinZiTou
        {
            set { _pinyinzitou = value; }
            get { return _pinyinzitou; }
        }
        /// <summary>
        /// 是否使用
        /// </summary>
        [DataMember]
        public bool IsUse
        {
            set { _isuse = value; }
            get { return _isuse; }
        }
        /// <summary>
        /// 是否允许用户设置
        /// </summary>
        [DataMember]
        public bool IsUserSet
        {
            set { _isuserset = value; }
            get { return _isuserset; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime? DataAddTime
        {
            set { _dataaddtime = value; }
            get { return _dataaddtime; }
        }
        /// <summary>
        /// 数据更新时间
        /// </summary>
        [DataMember]
        public DateTime? DataUpdateTime
        {
            set { _dataupdatetime = value; }
            get { return _dataupdatetime; }
        }
        /// <summary>
        /// 时间戳
        /// </summary>
        [DataMember]
        public byte[] DataTimeStamp
        {
            set { _datatimestamp = value; }
            get { return _datatimestamp; }
        }
        #endregion Model

    }
}
