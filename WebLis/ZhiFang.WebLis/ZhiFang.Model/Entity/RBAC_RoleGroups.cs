using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model.RBAC.Entity
{
    //RBAC_RoleGroups
    public class RBAC_RoleGroups
    {

        /// <summary>
        /// RoleGroupID
        /// </summary>		
        private int _rolegroupid;
        public int RoleGroupID
        {
            get { return _rolegroupid; }
            set { _rolegroupid = value; }
        }
        /// <summary>
        /// RoleGroupOrder
        /// </summary>		
        private int _rolegrouporder;
        public int RoleGroupOrder
        {
            get { return _rolegrouporder; }
            set { _rolegrouporder = value; }
        }
        /// <summary>
        /// RoleGroupNo
        /// </summary>		
        private string _rolegroupno;
        public string RoleGroupNo
        {
            get { return _rolegroupno; }
            set { _rolegroupno = value; }
        }
        /// <summary>
        /// RoleGroupName
        /// </summary>		
        private string _rolegroupname;
        public string RoleGroupName
        {
            get { return _rolegroupname; }
            set { _rolegroupname = value; }
        }
        /// <summary>
        /// RoleGroupEnabled
        /// </summary>		
        private int _rolegroupenabled;
        public int RoleGroupEnabled
        {
            get { return _rolegroupenabled; }
            set { _rolegroupenabled = value; }
        }
        /// <summary>
        /// RoleGroupDesc
        /// </summary>		
        private string _rolegroupdesc;
        public string RoleGroupDesc
        {
            get { return _rolegroupdesc; }
            set { _rolegroupdesc = value; }
        }
        /// <summary>
        /// RoleGroupType
        /// </summary>		
        private string _rolegrouptype;
        public string RoleGroupType
        {
            get { return _rolegrouptype; }
            set { _rolegrouptype = value; }
        }

    }
}

