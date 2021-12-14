using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model.RBAC.Entity
{
    //RBAC_Users
    public class RBAC_Users
    {

        /// <summary>
        /// 用户标识号
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 用户帐号
        /// </summary>		
        private string _account;
        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>		
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        /// <summary>
        /// 员工标识号
        /// </summary>		
        private int _empid;
        public int EmpID
        {
            get { return _empid; }
            set { _empid = value; }
        }
        /// <summary>
        /// 用户描述
        /// </summary>		
        private string _userdesc;
        public string UserDesc
        {
            get { return _userdesc; }
            set { _userdesc = value; }
        }
        /// <summary>
        /// 允许修改密码
        /// </summary>		
        private bool _enmpwd;
        public bool EnMPwd
        {
            get { return _enmpwd; }
            set { _enmpwd = value; }
        }
        /// <summary>
        /// 密码永不过期
        /// </summary>		
        private bool _pwdexprd;
        public bool PwdExprd
        {
            get { return _pwdexprd; }
            set { _pwdexprd = value; }
        }
        /// <summary>
        /// 账号永不过期
        /// </summary>		
        private bool _accexprd;
        public bool AccExprd
        {
            get { return _accexprd; }
            set { _accexprd = value; }
        }
        /// <summary>
        /// 帐号被锁定
        /// </summary>		
        private bool _acclock;
        public bool AccLock
        {
            get { return _acclock; }
            set { _acclock = value; }
        }
        /// <summary>
        /// LockedPeriod
        /// </summary>		
        private int _lockedperiod;
        public int LockedPeriod
        {
            get { return _lockedperiod; }
            set { _lockedperiod = value; }
        }
        /// <summary>
        /// 自动解锁
        /// </summary>		
        private int _auunlock;
        public int AuUnlock
        {
            get { return _auunlock; }
            set { _auunlock = value; }
        }
        /// <summary>
        /// 锁定日期
        /// </summary>		
        private DateTime? _acclockdt;
        public DateTime? AccLockDt
        {
            get { return _acclockdt; }
            set { _acclockdt = value; }
        }
        /// <summary>
        /// 上次登录时间
        /// </summary>		
        private DateTime? _logintm;
        public DateTime? LoginTm
        {
            get { return _logintm; }
            set { _logintm = value; }
        }
        /// <summary>
        /// 帐号到期时间
        /// </summary>		
        private DateTime? _accexptm;
        public DateTime? AccExpTm
        {
            get { return _accexptm; }
            set { _accexptm = value; }
        }
        /// <summary>
        /// AccCreateTime
        /// </summary>		
        private DateTime? _acccreatetime;
        public DateTime? AccCreateTime
        {
            get { return _acccreatetime; }
            set { _acccreatetime = value; }
        }

    }
}

