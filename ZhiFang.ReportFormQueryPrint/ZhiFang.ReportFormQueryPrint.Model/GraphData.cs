using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    /// <summary>
    /// GraphData:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GraphData
    {
        public GraphData()
        { }
        #region Model
        private DateTime _receivedate;
        private int _sectionno;
        private int _testtypeno;
        private string _sampleno;
        private string _graphname;
        private int _graphno;
        private int? _equipno;
        private int _pointtype = 1;
        private int? _showpoint = 0;
        private int? _mcolor = 0;
        private string _scolor;
        private int? _showaxis = 0;
        private int? _showlable = 0;
        private decimal? _minx = 0M;
        private decimal? _maxx = 0M;
        private decimal? _miny = 0M;
        private decimal? _maxy = 0M;
        private int? _showtitle = 0;
        private string _stitle;
        private string _graphvalue;
        private string _graphmemo;
        private string _graphf1;
        private string _graphf2;
        private int? _charttop = 1;
        private int? _chartheight = 200;
        private int? _chartleft = 1;
        private int? _chartwidth = 300;
        private byte[] _graphjpg;
        private int? _isfile;
        private string _graphfilename;
        private DateTime? _graphfiletime;
        private int? _isfiletoserver;
        /// <summary>
        /// 
        /// </summary>
        public DateTime ReceiveDate
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SectionNo
        {
            set { _sectionno = value; }
            get { return _sectionno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TestTypeNo
        {
            set { _testtypeno = value; }
            get { return _testtypeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleNo
        {
            set { _sampleno = value; }
            get { return _sampleno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GraphName
        {
            set { _graphname = value; }
            get { return _graphname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GraphNo
        {
            set { _graphno = value; }
            get { return _graphno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? EquipNo
        {
            set { _equipno = value; }
            get { return _equipno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PointType
        {
            set { _pointtype = value; }
            get { return _pointtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ShowPoint
        {
            set { _showpoint = value; }
            get { return _showpoint; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MColor
        {
            set { _mcolor = value; }
            get { return _mcolor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SColor
        {
            set { _scolor = value; }
            get { return _scolor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ShowAxis
        {
            set { _showaxis = value; }
            get { return _showaxis; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ShowLable
        {
            set { _showlable = value; }
            get { return _showlable; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MinX
        {
            set { _minx = value; }
            get { return _minx; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MaxX
        {
            set { _maxx = value; }
            get { return _maxx; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MinY
        {
            set { _miny = value; }
            get { return _miny; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MaxY
        {
            set { _maxy = value; }
            get { return _maxy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ShowTitle
        {
            set { _showtitle = value; }
            get { return _showtitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STitle
        {
            set { _stitle = value; }
            get { return _stitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GraphValue
        {
            set { _graphvalue = value; }
            get { return _graphvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GraphMemo
        {
            set { _graphmemo = value; }
            get { return _graphmemo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GraphF1
        {
            set { _graphf1 = value; }
            get { return _graphf1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GraphF2
        {
            set { _graphf2 = value; }
            get { return _graphf2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ChartTop
        {
            set { _charttop = value; }
            get { return _charttop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ChartHeight
        {
            set { _chartheight = value; }
            get { return _chartheight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ChartLeft
        {
            set { _chartleft = value; }
            get { return _chartleft; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ChartWidth
        {
            set { _chartwidth = value; }
            get { return _chartwidth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] Graphjpg
        {
            set { _graphjpg = value; }
            get { return _graphjpg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsFile
        {
            set { _isfile = value; }
            get { return _isfile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GraphFileName
        {
            set { _graphfilename = value; }
            get { return _graphfilename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? GraphFileTime
        {
            set { _graphfiletime = value; }
            get { return _graphfiletime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isFileToServer
        {
            set { _isfiletoserver = value; }
            get { return _isfiletoserver; }
        }
        #endregion Model

    }
}

