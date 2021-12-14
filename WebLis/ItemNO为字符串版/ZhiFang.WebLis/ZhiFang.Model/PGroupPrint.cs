using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    /// <summary>
    /// 实体类PGroupPrint 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>

    public class PGroupPrint
    {
        public PGroupPrint()
        { }
        #region Model
        private int _id;
        private int? _sectionno;
        private int? _printformatno;
        private DateTime? _addtime;
        private string _clientno;
        private int? _sort;
        private int? _specialtyitemno;
        private int? _useflag;
        private int? _imageflag;
        private int? _antiflag;
        private int? _itemminnumber;
        private int? _itemmaxnumber;
        private int? _batchprint;
        private int? _modelTitleType;

        public int? ModelTitleType
        {
            get { return _modelTitleType; }
            set { _modelTitleType = value; }
        }

   
        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 小组号
        /// </summary>
        public int? SectionNo
        {
            set { _sectionno = value; }
            get { return _sectionno; }
        }
        /// <summary>
        /// 模板号
        /// </summary>
        public int? PrintFormatNo
        {
            set { _printformatno = value; }
            get { return _printformatno; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 客户号
        /// </summary>
        public string ClientNo
        {
            set { _clientno = value; }
            get { return _clientno; }
        }
        /// <summary>
        /// 优先级
        /// </summary>
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 特殊项目
        /// </summary>
        public int? SpecialtyItemNo
        {
            set { _specialtyitemno = value; }
            get { return _specialtyitemno; }
        }
        /// <summary>
        /// 使用标志
        /// </summary>
        public int? UseFlag
        {
            set { _useflag = value; }
            get { return _useflag; }
        }
        /// <summary>
        /// 带图标志
        /// </summary>
        public int? ImageFlag
        {
            set { _imageflag = value; }
            get { return _imageflag; }
        }
        /// <summary>
        /// 带抗生素标志
        /// </summary>
        public int? AntiFlag
        {
            set { _antiflag = value; }
            get { return _antiflag; }
        }
        /// <summary>
        /// 最小项目数
        /// </summary>
        public int? ItemMinNumber
        {
            set { _itemminnumber = value; }
            get { return _itemminnumber; }
        }
        /// <summary>
        /// 最大项目数
        /// </summary>
        public int? ItemMaxNumber
        {
            set { _itemmaxnumber = value; }
            get { return _itemmaxnumber; }
        }
        /// <summary>
        /// 套打
        /// </summary>
        public int? BatchPrint
        {
            set { _batchprint = value; }
            get { return _batchprint; }
        }
        /// <summary>
        /// 就诊类型
        /// </summary>
        public int? SickTypeNo{ get;set;}
        /// <summary>
        /// 中心--实验室  对照关系使用
        /// </summary>
        private string _labcode;
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }

        private string _printformatname;
        public string PrintFormatName {
            set { _printformatname = value; }
            get { return _printformatname; }
        }
        #endregion Model

    }
}

