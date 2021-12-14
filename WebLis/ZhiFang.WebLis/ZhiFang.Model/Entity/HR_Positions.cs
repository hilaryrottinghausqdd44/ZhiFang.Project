using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model.RBAC.Entity
{
    //HR_Positions
    public class HR_Positions
    {

        /// <summary>
        /// 职位标识号
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 职位编号
        /// </summary>		
        private string _sn;
        public string SN
        {
            get { return _sn; }
            set { _sn = value; }
        }
        /// <summary>
        /// 职位中文名称
        /// </summary>		
        private string _cname;
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }
        /// <summary>
        /// 职位英文名称
        /// </summary>		
        private string _ename;
        public string EName
        {
            get { return _ename; }
            set { _ename = value; }
        }
        /// <summary>
        /// 职位简称
        /// </summary>		
        private string _sname;
        public string SName
        {
            get { return _sname; }
            set { _sname = value; }
        }
        /// <summary>
        /// 职位描述
        /// </summary>		
        private string _descr;
        public string Descr
        {
            get { return _descr; }
            set { _descr = value; }
        }
        /// <summary>
        /// 职位等级
        /// </summary>		
        private int _grade;
        public int Grade
        {
            get { return _grade; }
            set { _grade = value; }
        }

    }
}

