using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    [Serializable]
    public class ClientProfile
    {
        public ClientProfile()
        { }
        /// <summary>
        /// Id
        /// </summary>		
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// ClientNo
        /// </summary>		
        private int? _clientno;
        public int? ClientNo
        {
            get { return _clientno; }
            set { _clientno = value; }
        }
        /// <summary>
        /// ClientProfileCName
        /// </summary>		
        private string _clientprofilecname;
        public string ClientProfileCName
        {
            get { return _clientprofilecname; }
            set { _clientprofilecname = value; }
        }
        private string _mergerulename;
        public string MergeRuleName
        {
            get { return _mergerulename; }
            set { _mergerulename = value; }
        }
        public string ProfileCName { get; set; }
     
    }
}
