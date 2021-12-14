using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model
{
    //BusinessLogicClientControl		
    [Serializable]
    public class BusinessLogicClientControl
    {
        public BusinessLogicClientControl()
        { }
        private int _id;
        private string _account;
        private string _clientno;
        private DateTime? _addtime;
        public string ClienteleLikeKey { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Account
        /// </summary>
        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }

        /// <summary>
        /// ClientNo
        /// </summary>
        public string ClientNo
        {
            get { return _clientno; }
            set { _clientno = value; }
        }

        /// <summary>
        /// AddTime
        /// </summary>
        public DateTime? AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }

        /// <summary>
        /// 中心--实验室  对照关系使用
        /// </summary>
        private string _labcode;
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }
        public bool SelectedFlag { get; set; }
    }
}