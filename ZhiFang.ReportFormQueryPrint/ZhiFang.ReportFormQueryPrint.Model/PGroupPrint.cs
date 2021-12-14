using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    /// <summary>
    /// 实体类PGroupPrint 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PGroupPrint
    {
        public PGroupPrint()
        { }
        #region Model
        private int _id;
        private int? _sectionno;
        private int? _printformatno;
        private DateTime? _addtime;
        private int? _clientno;
        private int? _sort;
        private int? _specialtyitemno;
        private int? _useflag;
        private int? _imageflag;
        private int? _antiflag;
        private int? _itemminnumber;
        private int? _itemmaxnumber;
        private int? _batchprint;
     
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
        /// 客户号
        /// </summary>
        public int? ClientNo
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
        #endregion Model

    }
}

