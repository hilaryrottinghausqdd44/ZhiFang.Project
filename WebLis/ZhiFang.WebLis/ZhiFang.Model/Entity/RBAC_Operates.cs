using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model.RBAC.Entity
{
    //RBAC_Operates
    public class RBAC_Operates
    {

        /// <summary>
        /// 操作标识号
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 操作编号
        /// </summary>		
        private string _sn;
        public string SN
        {
            get { return _sn; }
            set { _sn = value; }
        }
        /// <summary>
        /// 操作中文名称
        /// </summary>		
        private string _cname;
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }
        /// <summary>
        /// 操作英文名称
        /// </summary>		
        private string _ename;
        public string EName
        {
            get { return _ename; }
            set { _ename = value; }
        }
        /// <summary>
        /// 操作简称
        /// </summary>		
        private string _sname;
        public string SName
        {
            get { return _sname; }
            set { _sname = value; }
        }
        /// <summary>
        /// OperateColor
        /// </summary>		
        private string _operatecolor;
        public string OperateColor
        {
            get { return _operatecolor; }
            set { _operatecolor = value; }
        }
        /// <summary>
        /// 操作类型
        /// </summary>		
        private int _type;
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 操作描述
        /// </summary>		
        private string _descr;
        public string Descr
        {
            get { return _descr; }
            set { _descr = value; }
        }

    }
}

