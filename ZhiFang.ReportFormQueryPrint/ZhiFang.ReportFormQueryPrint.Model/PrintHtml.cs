using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    /// <summary>
    /// by spf 2010-08-27
    /// </summary>
   public   class PrintHtml
    {
       private string formno;

        public string  Formno
        {
            get { return formno; }
            set { formno = value; }
        }
        private int printfomatno;

        public int Printfomatno
        {
            get { return printfomatno; }
            set { printfomatno = value; }
        }
        private int sectionno;

        public int Sectionno
        {
            get { return sectionno; }
            set { sectionno = value; }
        }
        private int clientno;

        public int Clientno
        {
            get { return clientno; }
            set { clientno = value; }
        }
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        private string burl;

        public string Burl
        {
            get { return burl; }
            set { burl = value; }
        }
    }
}
