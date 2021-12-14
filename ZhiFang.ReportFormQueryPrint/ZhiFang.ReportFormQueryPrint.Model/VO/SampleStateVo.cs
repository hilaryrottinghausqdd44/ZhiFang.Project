using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.ReportFormQueryPrint.Model.VO
{
    public class SampleStateVo
    {
        public SampleStateVo()
        { }
        #region Model
        private string _State;
        private string _Operator;
        private string _OperatorTime;
        private string _Explain;
        private string _Comment;

        public SampleStateVo(string state, string @operator, string operatorTime, string explain, string comment)
        {
            _State = state;
            _Operator = @operator;
            _OperatorTime = operatorTime;
            _Explain = explain;
            _Comment = comment;
        }

        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            set { _State = value; }
            get { return _State; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Operator
        {
            set { _Operator = value; }
            get { return _Operator; }
        }
        public string OperatorTime
        {
            set { _OperatorTime = value; }
            get { return _OperatorTime; }
        }
        public string Explain
        {
            set { _Explain = value; }
            get { return _Explain; }
        }
        public string Comment
        {
            set { _Comment = value; }
            get { return _Comment; }
        }
        #endregion Model
    }
}