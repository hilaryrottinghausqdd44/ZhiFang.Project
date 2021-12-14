using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model
{
    public class Modules
    {
        public Modules()
        { }
        private int _id;
        private string _sn;
        private string _cname;
        private string _ename;
        private string _sname;
        private int? _type;
        private string _image;
        private string _url;
        private string _para;
        private string _descr;
        private string _buttonstheme;
        private int _owner;
        private DateTime? _createdate;
        private string _modulecode;
        private int _pid;

        /// <summary>
        /// 模块标识号
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 父模块编号
        /// </summary>
        public int PID
        {
            get { return _pid; }
            set { _pid = value; }
        }

        /// <summary>
        /// 模块编号
        /// </summary>
        public string SN
        {
            get { return _sn; }
            set { _sn = value; }
        }

        /// <summary>
        /// 模块中文名称
        /// </summary>
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }

        /// <summary>
        /// 模块英文名称
        /// </summary>
        public string EName
        {
            get { return _ename; }
            set { _ename = value; }
        }

        /// <summary>
        /// 模块简称
        /// </summary>
        public string SName
        {
            get { return _sname; }
            set { _sname = value; }
        }

        /// <summary>
        /// 模块类型
        /// </summary>
        public int? Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// 模块图片文件
        /// </summary>
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

        /// <summary>
        /// 模块入口地址
        /// </summary>
        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// 模块入口参数
        /// </summary>
        public string Para
        {
            get { return _para; }
            set { _para = value; }
        }

        /// <summary>
        /// 模块描述
        /// </summary>
        public string Descr
        {
            get { return _descr; }
            set { _descr = value; }
        }

        /// <summary>
        /// 按钮模块
        /// </summary>
        public string ButtonsTheme
        {
            get { return _buttonstheme; }
            set { _buttonstheme = value; }
        }

        /// <summary>
        /// 所有者
        /// </summary>
        public int Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }

        /// <summary>
        /// ModuleCode
        /// </summary>
        public string ModuleCode
        {
            get { return _modulecode; }
            set { _modulecode = value; }
        }

        /// <summary>
        /// 中心--实验室  对照关系使用
        /// </summary>
        private string _labcode;
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }
        public int Rank { set; get; }
        public string PSN { set; get; }
    }

    public class ModulesVO : Modules
    {
        public int _parentId { set; get; }
        public string state { set; get; }
        public bool @checked { set; get; }
        public string iconCls { set; get; }
    }
}