using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace ZhiFang.Model
{
  public  class StaticRecOrgSamplePrice
    {
        #region
        private string _clientname;
        private string _cname;
        private string _paritemno;
        private string _samplenum;
        private string _price;
        private string _itemtotalprice;
        private string _barcode;
      
        private string _operdatebegin;
        private string _operdateend;
        private string _operdate;
        private string _datetype;
        #endregion

        /// <summary>
        /// ClientNo
        /// </summary>
        [DataMember]
        public string ClientName
        {
            get { return _clientname; }
            set { _clientname = value; }
        }
        public string CName
        {
            get{return _cname;}
            set { _cname = value; }
        }
        public string ParItemNo
        {
            get { return _paritemno; }
            set { _paritemno = value; }
        }

        public string SampleNum
        {
            get { return _samplenum; }
            set { _samplenum = value; }
        }

        public string Price
        {
            get { return _price; }
            set { _price = value; }
        }


        public string ItemTotalPrice
        {
            get { return _itemtotalprice; }
            set { _itemtotalprice = value; }
        }


        public string OperDateBegin
        {
            get { return _operdatebegin; }
            set { _operdatebegin = value; }
        }

        public string OperDateEnd
        {
            get { return _operdateend; }
            set { _operdateend = value; }
        }
        public string Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }
        public string OperDate
        {
            get { return _operdate; }
            set { _operdate = value; }
        }
        public string DateType
        {
            get { return _datetype; }
            set { _datetype = value; }
        }
        public string Operator { set; get; }
        public string ClientNo { set; get; }
        public int? BarcodeNum { set; get; }
        public float? SumMoney { set; get; }
    }
}
