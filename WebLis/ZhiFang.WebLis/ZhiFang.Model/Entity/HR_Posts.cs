using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model.RBAC.Entity
{
    //HR_Posts
    public class HR_Posts
    {

        /// <summary>
        /// 岗位标识号
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 岗位编号
        /// </summary>		
        private string _sn;
        public string SN
        {
            get { return _sn; }
            set { _sn = value; }
        }
        /// <summary>
        /// 岗位中文名称
        /// </summary>		
        private string _cname;
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }
        /// <summary>
        /// 岗位英文名称
        /// </summary>		
        private string _ename;
        public string EName
        {
            get { return _ename; }
            set { _ename = value; }
        }
        /// <summary>
        /// 岗位简称
        /// </summary>		
        private string _sname;
        public string SName
        {
            get { return _sname; }
            set { _sname = value; }
        }
        /// <summary>
        /// 岗位描述
        /// </summary>		
        private string _descr;
        public string Descr
        {
            get { return _descr; }
            set { _descr = value; }
        }
        /// <summary>
        /// GroupName
        /// </summary>		
        private string _groupname;
        public string GroupName
        {
            get { return _groupname; }
            set { _groupname = value; }
        }
        /// <summary>
        /// GroupOrder
        /// </summary>		
        private int _grouporder;
        public int GroupOrder
        {
            get { return _grouporder; }
            set { _grouporder = value; }
        }

    }
}

