using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model.RBAC.Entity
{
    //HR_Departments
    public class HR_Departments
    {

        /// <summary>
        /// 部门标识号
        /// </summary>		
        private int _id;
        public int ID
        { 
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 部门部号
        /// </summary>		
        private string _sn;
        public string SN
        {
            get { return _sn; }
            set { _sn = value; }
        }

        /// <summary>
        /// 父级部门编号
        /// </summary>		
        private string _psn;
        public string PSN
        {
            get { return _psn; }
            set { _psn = value; }
        }

        /// <summary>
        /// 父级部门ID
        /// </summary>	
        private int _PID;
        public int PID
        {
            get { return _PID; }
            set { _PID = value; }
        }

        /// <summary>
        /// 中文名称
        /// </summary>		
        private string _cname;
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }
        /// <summary>
        /// 英文名称
        /// </summary>		
        private string _ename;
        public string EName
        {
            get { return _ename; }
            set { _ename = value; }
        }
        /// <summary>
        /// 部门简称
        /// </summary>		
        private string _sname;
        public string SName
        {
            get { return _sname; }
            set { _sname = value; }
        }
        /// <summary>
        /// 部门描述
        /// </summary>		
        private string _descr;
        public string Descr
        {
            get { return _descr; }
            set { _descr = value; }
        }
        /// <summary>
        /// 部门电话
        /// </summary>		
        private string _tel;
        public string Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }
        /// <summary>
        /// 部门传真
        /// </summary>		
        private string _fax;
        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        /// <summary>
        /// 部门邮编
        /// </summary>		
        private string _zip;
        public string Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }
        /// <summary>
        /// 部门地址
        /// </summary>		
        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// 部门联系人
        /// </summary>		
        private string _contact;
        public string Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }
        /// <summary>
        /// DeptDesktopID
        /// </summary>		
        private int _deptdesktopid;
        public int DeptDesktopID
        {
            get { return _deptdesktopid; }
            set { _deptdesktopid = value; }
        }
        /// <summary>
        /// DeptDesktopName
        /// </summary>		
        private string _deptdesktopname;
        public string DeptDesktopName
        {
            get { return _deptdesktopname; }
            set { _deptdesktopname = value; }
        }
        /// <summary>
        /// 上级机构
        /// </summary>		
        private string _parentorg;
        public string ParentOrg
        {
            get { return _parentorg; }
            set { _parentorg = value; }
        }
        /// <summary>
        /// 机构类型
        /// </summary>		
        private string _orgtype;
        public string orgType
        {
            get { return _orgtype; }
            set { _orgtype = value; }
        }
        /// <summary>
        /// 机构编码
        /// </summary>		
        private string _orgcode;
        public string OrgCode
        {
            get { return _orgcode; }
            set { _orgcode = value; }
        }
        /// <summary>
        /// 部门与送检单位对照
        /// </summary>		
        private string _relationname;
        public string RelationName
        {
            get { return _relationname; }
            set { _relationname = value; }
        }
    }
}

