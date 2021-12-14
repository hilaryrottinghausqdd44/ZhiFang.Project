using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Model
{
    public class StaticPersonTestItemPrice
    {
        #region
        private string _opendate;
        private string _age;
        private string _gendername;
        private string _clientno;
        private string _clientname;
        private string _bcname;
        private string _patno;
        private string _barcode;
        private string _paritemno;
        private string _dcname;
        private decimal _price;

        private string _operdatebegin;
        private string _operdateend;

        #endregion

        /// <summary>
        /// OperDate
        /// </summary>
        [DataMember]
        public string OperDate
        {
            get { return _opendate; }
            set { _opendate = value; }
        }

        /// <summary>
        /// Age
        /// </summary>
        [DataMember]
        public string Age
        {
            get { return _age; }
            set { _age = value; }
        }

        /// <summary>
        /// GenderNo
        /// </summary>
        [DataMember]
        public string GenderName
        {
            get { return _gendername; }
            set { _gendername = value; }
        }

        /// <summary>
        /// ClientName
        /// </summary>
        [DataMember]
        public string ClientNo
        {
            get { return _clientno; }
            set { _clientno = value; }
        }
        /// <summary>
        /// ClientName
        /// </summary>
        [DataMember]
        public string ClientName
        {
            get { return _clientname; }
            set { _clientname = value; }
        }

        /// <summary>
        /// BCName
        /// </summary>
        [DataMember]
        public string BCName
        {
            get { return _bcname; }
            set { _bcname = value; }
        }

        /// <summary>
        /// PatNo
        /// </summary>
        [DataMember]
        public string PatNo
        {
            get { return _patno; }
            set { _patno = value; }
        }

        /// <summary>
        /// BarCode
        /// </summary>
        [DataMember]
        public string BarCode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }

        /// <summary>
        /// ParItemNo
        /// </summary>
        [DataMember]
        public string ParItemNo
        {
            get { return _paritemno; }
            set { _paritemno = value; }
        }

        /// <summary>
        /// DCName
        /// </summary>
        [DataMember]
        public string DCName
        {
            get { return _dcname; }
            set { _dcname = value; }
        }

        /// <summary>
        /// Price
        /// </summary>
        [DataMember]
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        public string OperdateBegin
        {
            get { return _operdatebegin; }
            set { _operdatebegin = value; }
        }

        [DataMember]
        public string OperdateEnd
        {
            get { return _operdateend; }
            set { _operdateend = value; }
        }
    }
}
