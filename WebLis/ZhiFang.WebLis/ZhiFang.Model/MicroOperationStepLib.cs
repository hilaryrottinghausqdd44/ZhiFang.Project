using System;
namespace Model
{
	/// <summary>
	/// 实体类MicroOperationStepLib 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class MicroOperationStepLib
	{
		public MicroOperationStepLib()
		{}
		#region Model
        private int _id;
        private string _stepname;
        private string _stepinfo;
        private int? _parentstepid;
        private byte[] _icon;
        private int _sort;
        private int? _backstepid;
        private int? _nextstepid;
        private string _valueclass;
        private string _inputclass;
        private DateTime? _addtime;
        private int? _stepflag;
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
        public int? ParentStepId
        {
            set { _parentstepid = value; }
            get { return _parentstepid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] Icon
        {
            set { _icon = value; }
            get { return _icon; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BackStepId
        {
            set { _backstepid = value; }
            get { return _backstepid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NextStepId
        {
            set { _nextstepid = value; }
            get { return _nextstepid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string valueClass
        {
            set { _valueclass = value; }
            get { return _valueclass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InputClass
        {
            set { _inputclass = value; }
            get { return _inputclass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 0单一传入单一结果。1单一传入多结果。2多传入单一结果。3多传入多结果。
        /// </summary>
        public int? StepFlag
        {
            set { _stepflag = value; }
            get { return _stepflag; }
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

