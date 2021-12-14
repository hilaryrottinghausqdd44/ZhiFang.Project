using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
    //ClientEleArea		
    [DataContract]
    public class ClientEleArea
    {
        public ClientEleArea()
        { }

        /// <summary>
        /// AreaID
        /// </summary>

        private int _areaid;
        [DataMember]
        public int AreaID
        {
            get { return _areaid; }
            set { _areaid = value; }
        }

        /// <summary>
        /// AreaCName
        /// </summary>
        private string _areacname;
        [DataMember]
        public string AreaCName
        {
            get { return _areacname; }
            set { _areacname = value; }
        }

        /// <summary>
        /// AreaShortName
        /// </summary>
        private string _areashortname;
        [DataMember]
        public string AreaShortName
        {
            get { return _areashortname; }
            set { _areashortname = value; }
        }

        /// <summary>
        /// ClientNo
        /// </summary>

        private long? _clientno;
        [DataMember]
        public long? ClientNo
        {
            get { return _clientno; }
            set { _clientno = value; }
        }

        private string _clientname;
        [DataMember]
        public string ClientName
        {
            get { return _clientname; }
            set { _clientname = value; }
        }
        /// <summary>
        /// AddTime
        /// </summary>

        private DateTime? _addtime = DateTime.Now;
        [DataMember]
        public DateTime? AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }

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

        private string _orderfield = "ClientEleArea.AreaID";//排序
        [DataMember]
        public string OrderField
        {
            get { return _orderfield; }
            set { _orderfield = value; }
        }
        private string _searchlikekey;//模糊查询字段
        [DataMember]
        public string SearchLikeKey
        {
            get { return _searchlikekey; }
            set { _searchlikekey = value; }
        }

    }
}