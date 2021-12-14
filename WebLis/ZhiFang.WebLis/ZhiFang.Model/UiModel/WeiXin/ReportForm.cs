using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Model;
using ZhiFang.Tools;

namespace ZhiFang.Model.UiModel.WeiXin
{

   
    [DataContract]
    public class ReportForm
    {
        #region Member Variables

        protected ReportFormFull reportformfull;
        protected List<ReportItemFull> itemlist;
        protected List<ReportMicroFull> microlist;
        protected List<ReportMarrowFull> marrowlist;
        #endregion



        [DataMember]
        public virtual ReportFormFull ReportFromFull
        {
            get { return reportformfull; }
            set { reportformfull = value; }
        }
        [DataMember]
        public virtual List<ReportItemFull> ItemList
        {
            get { return itemlist; }
            set { itemlist = value; }
        }
        [DataMember]
        public virtual List<ReportMicroFull> MicroList
        {
            get { return microlist; }
            set { microlist = value; }
        }
        [DataMember]
        public virtual List<ReportMarrowFull> MarrowList
        {
            get { return marrowlist; }
            set { marrowlist = value; }
        }
    }
}