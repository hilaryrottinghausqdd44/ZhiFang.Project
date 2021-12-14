using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
    //B_TestItemControl
    [DataContract]
    public class TestItemControl
    {
        public TestItemControl() { }

        private string _orderfield = "Id";//排序
        [DataMember]
        public string OrderField
        {
            get { return _orderfield; }
            set { _orderfield = value; }
        }

        /// <summary>
        /// Id
        /// </summary>		
        private int _id;
        [DataMember]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// ItemControlNo
        /// </summary>		
        private string _itemcontrolno;
        [DataMember]
        public string ItemControlNo
        {
            get { return _itemcontrolno; }
            set { _itemcontrolno = value; }
        }
        /// <summary>
        /// ItemNo
        /// </summary>		
        private string _itemno;
        [DataMember]
        public string ItemNo
        {
            get { return _itemno; }
            set { _itemno = value; }
        }
        /// <summary>
        /// CName
        /// </summary>		
        private string _cname;
        [DataMember]
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }/// <summary>
        /// EName
        /// </summary>		
        private string _ename;
        [DataMember]
        public string EName
        {
            get { return _ename; }
            set { _ename = value; }
        }
        /// <summary>
        /// ShortName
        /// </summary>		
        private string _shortname;
        [DataMember]
        public string ShortName
        {
            get { return _shortname; }
            set { _shortname = value; }
        }
        /// <summary>
        /// ShortCode
        /// </summary>		
        private string _shortcode;
        [DataMember]
        public string ShortCode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }
        /// <summary>
        /// LabCode
        /// </summary>		
        private string _labcode;
        [DataMember]
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }
        /// <summary>
        /// ControlLabNo
        /// </summary>		
        private string _controllabno;
        [DataMember]
        public string ControlLabNo
        {
            get { return _controllabno; }
            set { _controllabno = value; }
        }
        /// <summary>
        /// CName
        /// </summary>		
        private string _controlcname;
        [DataMember]
        public string ControlCName
        {
            get { return _controlcname; }
            set { _controlcname = value; }
        }
        /// <summary>
        /// ControlItemNo
        /// </summary>		
        private string _controlitemno;
        [DataMember]
        public string ControlItemNo
        {
            get { return _controlitemno; }
            set { _controlitemno = value; }
        }

        /// <summary>
        /// DTimeStampe
        /// </summary>		
        private DateTime? _dtimestampe;
        [DataMember]
        public DateTime? DTimeStampe
        {
            get { return _dtimestampe; }
            set { _dtimestampe = value; }
        }
        /// <summary>
        /// AddTime
        /// </summary>		
        private DateTime? _addtime;
        [DataMember]
        public DateTime? AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        /// <summary>
        /// UseFlag
        /// </summary>		
        private int? _useflag;
        [DataMember]
        public int? UseFlag
        {
            get { return _useflag; }
            set { _useflag = value; }
        }
        /// <summary>
        /// 对照状态
        /// </summary>
        private string _controlstate;
        [DataMember]
        public string ControlState
        {
            get { return _controlstate; }
            set { _controlstate = value; }
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        private string _searchlikekey;
        [DataMember]
        public string SearchLikeKey
        {
            get { return _searchlikekey; }
            set { _searchlikekey = value; }
        }


        #region 实验室
        /// <summary>
        /// ItemID
        /// </summary>		
        private int _itemid;
        [DataMember]
        public int ItemID
        {
            get { return _itemid; }
            set { _itemid = value; }
        }
        /// <summary>
        /// LabItemNo digitlab2009和digitlab8使用LabItemNo；digitlab66和weblis使用ItemNo
        /// </summary>		
        private string _labitemno;
        [DataMember]
        public string LabItemNo
        {
            get { return _labitemno; }
            set { _labitemno = value; }
        }

        private string _centeritemno;
        [DataMember]
        public string CenterItemNo
        {
            get { return _centeritemno; }
            set { _centeritemno = value; }
        }

        private string _centercname;
        [DataMember]
        public string CenterCName
        {
            get { return _centercname; }
            set { _centercname = value; }
        }
        #endregion
    }
}

