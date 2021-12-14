using System;
namespace Model
{
	/// <summary>
	/// 实体类MicroOperationStepFlow 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class MicroOperationStepFlow
	{
		public MicroOperationStepFlow()
		{}
		#region Model
        private int _id;
        private int _stepid;
        private string _stepname;
        private string _stepinfo;
        private int? _stepflowsort;
        private string _value;
        private string _sampleno;
        private string _parentsampleno;
        private string _microno;
        private string _microantiid;
        private string _formno;
        private int? _endflag;
        private int? _userno;
        private DateTime _addtime;
        private int _sampletypeno;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int StepId
        {
            set { _stepid = value; }
            get { return _stepid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StepName
        {
            set { _stepname = value; }
            get { return _stepname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StepInfo
        {
            set { _stepinfo = value; }
            get { return _stepinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? StepFlowSort
        {
            set { _stepflowsort = value; }
            get { return _stepflowsort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            set { _value = value; }
            get { return _value; }
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
        public string ParentSampleNo
        {
            set { _parentsampleno = value; }
            get { return _parentsampleno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MicroNo
        {
            set { _microno = value; }
            get { return _microno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MicroAntiId
        {
            set { _microantiid = value; }
            get { return _microantiid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormNo
        {
            set { _formno = value; }
            get { return _formno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? EndFlag
        {
            set { _endflag = value; }
            get { return _endflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UserNo
        {
            set { _userno = value; }
            get { return _userno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SampleTypeNo
        {
            set { _sampletypeno = value; }
            get { return _sampletypeno; }
        }
		#endregion Model

	}
}

