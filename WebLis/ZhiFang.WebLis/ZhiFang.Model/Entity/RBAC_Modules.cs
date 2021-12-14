using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model.RBAC.Entity
{
    //RBAC_Modules
    public class RBAC_Modules
    {

        /// <summary>
        /// 模块标识号
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 模块编号
        /// </summary>		
        private string _sn;
        public string SN
        {
            get { return _sn; }
            set { _sn = value; }
        }
        /// <summary>
        /// 模块中文名称
        /// </summary>		
        private string _cname;
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }
        /// <summary>
        /// 模块英文名称
        /// </summary>		
        private string _ename;
        public string EName
        {
            get { return _ename; }
            set { _ename = value; }
        }
        /// <summary>
        /// 模块简称
        /// </summary>		
        private string _sname;
        public string SName
        {
            get { return _sname; }
            set { _sname = value; }
        }
        /// <summary>
        /// 模块类型
        /// </summary>		
        private int _type;
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 模块图片文件
        /// </summary>		
        private string _image;
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }
        /// <summary>
        /// 模块入口地址
        /// </summary>		
        private string _url;
        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }
        /// <summary>
        /// 模块入口参数
        /// </summary>		
        private string _para;
        public string Para
        {
            get { return _para; }
            set { _para = value; }
        }
        /// <summary>
        /// 模块描述
        /// </summary>		
        private string _descr;
        public string Descr
        {
            get { return _descr; }
            set { _descr = value; }
        }
        /// <summary>
        /// 按钮模块
        /// </summary>		
        private string _buttonstheme;
        public string ButtonsTheme
        {
            get { return _buttonstheme; }
            set { _buttonstheme = value; }
        }
        /// <summary>
        /// 所有者
        /// </summary>		
        private int _owner;
        public int Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>		
        private DateTime _createdate;
        public DateTime CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }
        /// <summary>
        /// ModuleCode
        /// </summary>		
        private string _modulecode;
        public string ModuleCode
        {
            get { return _modulecode; }
            set { _modulecode = value; }
        }

    }
}

