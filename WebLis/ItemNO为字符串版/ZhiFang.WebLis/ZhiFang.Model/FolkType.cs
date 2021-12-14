using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
    //FolkType		
    [DataContract]
    public class FolkType
    {
        public FolkType()
        { }
        private int _folkid;
        private int _folkno;
        private string _cname;
        private string _shortcode;
        private int? _visible;
        private int? _disporder;
        private string _hisordercode;
        private DateTime? _dtimestampe;
        private DateTime? _addtime;
        private string _standcode;
        private string _zfstandcode;
        private int? _useflag;

        /// <summary>
        /// FolkID
        /// </summary>
        [DataMember] 
        public int FolkID
        {
            get { return _folkid; }
            set { _folkid = value; }
        }

        /// <summary>
        /// FolkNo
        /// </summary>
        [DataMember] 
        public int FolkNo
        {
            get { return _folkno; }
            set { _folkno = value; }
        }

        /// <summary>
        /// CName
        /// </summary>
        [DataMember] 
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }

        /// <summary>
        /// ShortCode
        /// </summary>
        [DataMember] 
        public string ShortCode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        /// <summary>
        /// Visible
        /// </summary>
        [DataMember] 
        public int? Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        /// <summary>
        /// DispOrder
        /// </summary>
        [DataMember] 
        public int? DispOrder
        {
            get { return _disporder; }
            set { _disporder = value; }
        }

        /// <summary>
        /// HisOrderCode
        /// </summary>
        [DataMember] 
        public string HisOrderCode
        {
            get { return _hisordercode; }
            set { _hisordercode = value; }
        }

        /// <summary>
        /// DTimeStampe
        /// </summary>
        [DataMember] 
        public DateTime? DTimeStampe
        {
            get { return _dtimestampe; }
            set { _dtimestampe = value; }
        }

        /// <summary>
        /// AddTime
        /// </summary>
        [DataMember] 
        public DateTime? AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }

        /// <summary>
        /// StandCode
        /// </summary>
        [DataMember] 
        public string StandCode
        {
            get { return _standcode; }
            set { _standcode = value; }
        }

        /// <summary>
        /// ZFStandCode
        /// </summary>
        [DataMember] 
        public string ZFStandCode
        {
            get { return _zfstandcode; }
            set { _zfstandcode = value; }
        }

        /// <summary>
        /// UseFlag
        /// </summary>
        [DataMember] 
        public int? UseFlag
        {
            get { return _useflag; }
            set { _useflag = value; }
        }

        /// <summary>
        /// 中心--实验室  对照关系使用
        /// </summary>
        private string _labcode;
        [DataMember] 
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }

        private string _orderfield = "FolkNo";//排序
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

        /// <summary>
        /// FolkTypeControl/////////
        /// </summary>
        [DataMember]
        public FolkTypeControl FolkTypeControl { get; set; }

    }
}