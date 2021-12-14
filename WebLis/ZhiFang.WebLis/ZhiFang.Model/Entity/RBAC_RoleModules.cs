using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model.RBAC.Entity
{
    //RBAC_RoleModules
    public class RBAC_RoleModules
    {

        /// <summary>
        /// 序号
        /// </summary>		
        private int _sn;
        public int SN
        {
            get { return _sn; }
            set { _sn = value; }
        }
        /// <summary>
        /// 模块标识
        /// </summary>		
        private int _moduleid;
        public int ModuleID
        {
            get { return _moduleid; }
            set { _moduleid = value; }
        }
        /// <summary>
        /// 员工标识
        /// </summary>		
        private int _emplid;
        public int EmplID
        {
            get { return _emplid; }
            set { _emplid = value; }
        }
        /// <summary>
        /// 部门标识
        /// </summary>		
        private int _deptid;
        public int DeptID
        {
            get { return _deptid; }
            set { _deptid = value; }
        }
        /// <summary>
        /// 职位标识
        /// </summary>		
        private int _positionid;
        public int PositionID
        {
            get { return _positionid; }
            set { _positionid = value; }
        }
        /// <summary>
        /// 岗位标识
        /// </summary>		
        private int _postid;
        public int PostID
        {
            get { return _postid; }
            set { _postid = value; }
        }
        /// <summary>
        /// 模块访问能力
        /// </summary>		
        private int _accability;
        public int AccAbility
        {
            get { return _accability; }
            set { _accability = value; }
        }
        /// <summary>
        /// 模块操作能力
        /// </summary>		
        private int _opability;
        public int OpAbility
        {
            get { return _opability; }
            set { _opability = value; }
        }
        /// <summary>
        /// 有效期列表
        /// </summary>		
        private string _validity;
        public string Validity
        {
            get { return _validity; }
            set { _validity = value; }
        }

    }
}

