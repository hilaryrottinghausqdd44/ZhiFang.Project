using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    /// <summary>
    /// 实体类SamplingGroup 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SamplingGroup
    {
        public SamplingGroup()
		{}
        /// <summary>
        /// SamplingGroupNo
        /// </summary>		
        private int? _samplinggroupno;
        public int? SamplingGroupNo
        {
            get { return _samplinggroupno; }
            set { _samplinggroupno = value; }
        }
        /// <summary>
        /// SamplingGroupName
        /// </summary>		
        private string _samplinggroupname;
        public string SamplingGroupName
        {
            get { return _samplinggroupname; }
            set { _samplinggroupname = value; }
        }
        /// <summary>
        /// SampleTypeNo
        /// </summary>		
        private int? _sampletypeno;
        public int? SampleTypeNo
        {
            get { return _sampletypeno; }
            set { _sampletypeno = value; }
        }
        /// <summary>
        /// CubeType
        /// </summary>		
        private int? _cubetype;
        public int? CubeType
        {
            get { return _cubetype; }
            set { _cubetype = value; }
        }
        /// <summary>
        /// CubeColor
        /// </summary>		
        private string _cubecolor;
        public string CubeColor
        {
            get { return _cubecolor; }
            set { _cubecolor = value; }
        }
        /// <summary>
        /// SpecialtyType
        /// </summary>		
        private int? _specialtytype;
        public int? SpecialtyType
        {
            get { return _specialtytype; }
            set { _specialtytype = value; }
        }
        /// <summary>
        /// ShortName
        /// </summary>		
        private string _shortname;
        public string ShortName
        {
            get { return _shortname; }
            set { _shortname = value; }
        }
        /// <summary>
        /// ShortCode
        /// </summary>		
        private string _shortcode;
        public string ShortCode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }
        /// <summary>
        /// destination
        /// </summary>		
        private string _destination;
        public string destination
        {
            get { return _destination; }
            set { _destination = value; }
        }
        /// <summary>
        /// capability
        /// </summary>		
        private decimal? _capability;
        public decimal? capability
        {
            get { return _capability; }
            set { _capability = value; }
        }
        /// <summary>
        /// Unit
        /// </summary>		
        private string _unit;
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        /// <summary>
        /// AppCap
        /// </summary>		
        private decimal? _appcap;
        public decimal? AppCap
        {
            get { return _appcap; }
            set { _appcap = value; }
        }
        /// <summary>
        /// synopsis
        /// </summary>		
        private string _synopsis;
        public string synopsis
        {
            get { return _synopsis; }
            set { _synopsis = value; }
        }
        /// <summary>
        /// disporder
        /// </summary>		
        private int? _disporder;
        public int? disporder
        {
            get { return _disporder; }
            set { _disporder = value; }
        }
        /// <summary>
        /// mincapability
        /// </summary>		
        private decimal _mincapability;
        public decimal mincapability
        {
            get { return _mincapability; }
            set { _mincapability = value; }
        }
        /// <summary>
        /// PrinterName
        /// </summary>		
        private string _printername;
        public string PrinterName
        {
            get { return _printername; }
            set { _printername = value; }
        }
        /// <summary>
        /// ShortCode2
        /// </summary>		
        private string _shortcode2;
        public string ShortCode2
        {
            get { return _shortcode2; }
            set { _shortcode2 = value; }
        }
        /// <summary>
        /// PrintCount
        /// </summary>		
        private int? _printcount;
        public int? PrintCount
        {
            get { return _printcount; }
            set { _printcount = value; }
        }        
    }
}
