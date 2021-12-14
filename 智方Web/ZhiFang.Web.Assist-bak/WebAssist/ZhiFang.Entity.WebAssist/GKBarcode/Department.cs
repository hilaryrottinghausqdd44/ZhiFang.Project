
using System;

namespace ZhiFang.Entity.WebAssist.GKBarcode
{
    /// <summary>
    /// Department:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Department
    {
        public Department()
        { }
        #region Model
        private int _depid;
        private string _depname;
        private string _querycode;
        private string _users;
        private int? _operatetypeno;
        private int? _dglab_index;
        private string _update;
        private byte[] _exefileupdate;
        /// <summary>
        /// 
        /// </summary>
        public int DepID
        {
            set { _depid = value; }
            get { return _depid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DepName
        {
            set { _depname = value; }
            get { return _depname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QueryCode
        {
            set { _querycode = value; }
            get { return _querycode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Users
        {
            set { _users = value; }
            get { return _users; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OperateTypeNo
        {
            set { _operatetypeno = value; }
            get { return _operatetypeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Dglab_Index
        {
            set { _dglab_index = value; }
            get { return _dglab_index; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Update
        {
            set { _update = value; }
            get { return _update; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] ExeFileUpdate
        {
            set { _exefileupdate = value; }
            get { return _exefileupdate; }
        }
        #endregion Model

    }
}

