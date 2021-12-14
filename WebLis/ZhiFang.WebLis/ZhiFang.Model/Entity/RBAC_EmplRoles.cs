using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model.RBAC.Entity
{
    //RBAC_EmplRoles
    public class RBAC_EmplRoles
    {

        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 序号
        /// </summary>		
        private int? _sn;
        public int? SN
        {
            get { return _sn; }
            set { _sn = value; }
        }
        /// <summary>
        /// 用户标识
        /// </summary>		
        private int? _emplid;
        public int? EmplID
        {
            get { return _emplid; }
            set { _emplid = value; }
        }
        /// <summary>
        /// 部门标识
        /// </summary>		
        private long? _deptid;
        public long? DeptID
        {
            get { return _deptid; }
            set { _deptid = value; }
        }
        /// <summary>
        /// 职位标识
        /// </summary>		
        private int? _positionid;
        public int? PositionID
        {
            get { return _positionid; }
            set { _positionid = value; }
        }
        /// <summary>
        /// 岗位标识
        /// </summary>		
        private int? _postid;
        public int? PostID
        {
            get { return _postid; }
            set { _postid = value; }
        }
        /// <summary>
        /// 排序
        /// </summary>		
        private int? _sort;
        public int? Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }
        /// <summary>
        /// 有效期
        /// </summary>		
        private string _validity;
        public string Validity
        {
            get { return _validity; }
            set { _validity = value; }
        }

    }
}

