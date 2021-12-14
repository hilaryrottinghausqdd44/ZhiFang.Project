using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace ZhiFang.Model
{
  	//PGroupControl		
    [DataContract]
    public class PGroupFormat
    {
        public PGroupFormat()
        { }
        #region Model
        private int _id;
        private int? _sectionno;
        private string _sectionname;
        private string _clientname;

      
    
        private int? _printformatno;
        private DateTime? _addtime;
        private string _clientno;
        private int? _sort;
        private int? _specialtyitemno;
        private string _specialtyitemname;

        private int? _useflag;
        private int? _imageflag;
        private int? _antiflag;
        private int? _itemminnumber;
        private int? _itemmaxnumber;
        private int? _batchprint;
        private int? _modelTitleType;

        private string _printformatname;
        private string _pintformataddress;
        private string _pintformatfilename;
        private int? _itemparalinenum;
        private string _papersize;
        private string _printformatdesc;

        [DataMember]
        public string SpecialtyItemName
        {
            get { return _specialtyitemname; }
            set { _specialtyitemname = value; }
        }
        [DataMember]
        public int? ModelTitleType
        {
            get { return _modelTitleType; }
            set { _modelTitleType = value; }
        }
        [DataMember]
        public string SectionName
        {
            get { return _sectionname; }
            set { _sectionname = value; }
        }
        [DataMember]
        public string ClientName
        {
            get { return _clientname; }
            set { _clientname = value; }
        }
        /// <summary>
        /// Id
        /// </summary>
       [DataMember]
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 小组号
        /// </summary>
        [DataMember]
        public int? SectionNo
        {
            set { _sectionno = value; }
            get { return _sectionno; }
        }
        /// <summary>
        /// 模板号
        /// </summary>
       [DataMember]
        public int? PrintFormatNo
        {
            set { _printformatno = value; }
            get { return _printformatno; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        [DataMember]
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 客户号
        /// </summary>
        [DataMember]
        public string ClientNo
        {
            set { _clientno = value; }
            get { return _clientno; }
        }
        /// <summary>
        /// 优先级
        /// </summary>
        [DataMember]
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 特殊项目
        /// </summary>
        [DataMember]
        public int? SpecialtyItemNo
        {
            set { _specialtyitemno = value; }
            get { return _specialtyitemno; }
        }
        /// <summary>
        /// 使用标志
        /// </summary>
        [DataMember]
        public int? UseFlag
        {
            set { _useflag = value; }
            get { return _useflag; }
        }
        /// <summary>
        /// 带图标志
        /// </summary>
        [DataMember]
        public int? ImageFlag
        {
            set { _imageflag = value; }
            get { return _imageflag; }
        }
        /// <summary>
        /// 带抗生素标志
        /// </summary>
        [DataMember]
        public int? AntiFlag
        {
            set { _antiflag = value; }
            get { return _antiflag; }
        }
        /// <summary>
        /// 最小项目数
        /// </summary>
        [DataMember]
        public int? ItemMinNumber
        {
            set { _itemminnumber = value; }
            get { return _itemminnumber; }
        }
        /// <summary>
        /// 最大项目数
        /// </summary>
        [DataMember]
        public int? ItemMaxNumber
        {
            set { _itemmaxnumber = value; }
            get { return _itemmaxnumber; }
        }
        /// <summary>
        /// 套打
        /// </summary>
        [DataMember]
        public int? BatchPrint
        {
            set { _batchprint = value; }
            get { return _batchprint; }
        }
        /// <summary>
        /// 就诊类型
        /// </summary>
        [DataMember]
        public int? SickTypeNo { get; set; }
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
        #endregion Model

        #region Model
 


        /// <summary>
        /// 模板名
        /// </summary>
        [DataMember]
        public string PrintFormatName
        {
            set { _printformatname = value; }
            get { return _printformatname; }
        }
        /// <summary>
        /// 模板存放地址
        /// </summary>
        [DataMember]
        public string PintFormatAddress
        {
            set { _pintformataddress = value; }
            get { return _pintformataddress; }
        }
        /// <summary>
        /// 模板文件名
        /// </summary>
        [DataMember]
        public string PintFormatFileName
        {
            set { _pintformatfilename = value; }
            get { return _pintformatfilename; }
        }
        /// <summary>
        /// 项目个数参数
        /// </summary>
        [DataMember]
        public int? ItemParaLineNum
        {
            set { _itemparalinenum = value; }
            get { return _itemparalinenum; }
        }
        /// <summary>
        /// 纸张大小
        /// </summary>
        [DataMember]
        public string PaperSize
        {
            set { _papersize = value; }
            get { return _papersize; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string PrintFormatDesc
        {
            set { _printformatdesc = value; }
            get { return _printformatdesc; }
        }
      
        #endregion Model
    }
}
