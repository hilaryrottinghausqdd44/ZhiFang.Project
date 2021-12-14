using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data;

namespace ZhiFang.WebLis.Class
{
    public class ModuleManager
    {
        IBLL.Common.BaseDictionary.IBModules ibm = BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBModules>.GetBLL();
        public List<Model.Modules> ModuleCheck(List<string> rbac_moduleslist)
        {
            List<Model.Modules> a = new List<Model.Modules>();
            return a;
        }
        public XmlDocument GetModulesXml(List<string> rbac_moduleslist)
        {
            XmlDocument xd = new XmlDocument();
            ZhiFang.Common.Log.Log.Info("###1");
            string sXML = "<TREENODES/>";
            xd.LoadXml(sXML);
            ZhiFang.Common.Log.Log.Info("###2");
            DataSet ds = ibm.GetListByRBACModulesList(rbac_moduleslist);
            XmlNode node = xd.DocumentElement;
            ZhiFang.Common.Log.Log.Info("###3");
            initNode(node, ds, false);
            ZhiFang.Common.Log.Log.Info("###4");
            return xd;
        }
        public void initNode(XmlNode node, DataSet ds, bool checkbox)
        {
            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    node = LoopAddNodeWebService(node, dr, checkbox);
                }
            }
        }
        public XmlNode LoopAddNodeWebService(XmlNode node, DataRow dr, bool checkbox)
        {
            XmlDocument xd = new XmlDocument();
            xd = node.OwnerDocument;
            XmlNode nodeParent = xd.CreateElement("treenode");
            XmlNode myNode = xd.CreateElement("treenode");
            XmlAttribute xt;
            ///节点文本
            xt = xd.CreateAttribute("Text");
            xt.InnerXml = dr["CName"].ToString();
            myNode.Attributes.Append(xt);

            ///节点提示
            //xt=xd.CreateAttribute("tooltip");
            //xt.InnerXml="最高权限" + dr["CName"].ToString();
            //myNode.Attributes.Append(xt);

            try
            {
                xt = xd.CreateAttribute("Para");
                string strPara = dr["Para"].ToString();
                if (strPara == null)
                    strPara = "";
                //此处将&替换成＆amp;其中＆是全角的就行了,在xml中html中的有些字符需要转义
                //其中< & 是危险字符
                //传入的参数在其它程序中也能Request.querystring到
                strPara = strPara.Replace("&", "＆amp;");                
                xt.InnerXml = strPara;
                myNode.Attributes.Append(xt);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
            try
            {
                if (checkbox)
                {
                    xt = xd.CreateAttribute("Checkbox");
                    xt.InnerXml = checkbox.ToString().ToLower();
                    myNode.Attributes.Append(xt);


                    bool bInherit = false;
                    if (dr.Table.Columns.Contains("RoleInheritNames") && !Convert.IsDBNull(dr["RoleInheritNames"]))
                    {
                        //myNode.Attributes.GetNamedItem("text").Value = dr["RoleInheritNames"].ToString();
                        xt = xd.CreateAttribute("RoleInheritNames");
                        xt.InnerXml = dr["RoleInheritNames"].ToString();
                        if (xt.InnerXml.Trim() != "")
                            bInherit = true;
                        myNode.Attributes.Append(xt);
                    }

                    if (bInherit || dr.Table.Columns.Contains("RoleID") && !Convert.IsDBNull(dr["RoleID"]))
                    {
                        xt = xd.CreateAttribute("checked");
                        xt.InnerXml = "true"; //其实是选定，应该写为"true";
                        myNode.Attributes.Append(xt);
                    }
                }
            }
            catch { }
            string mySN = (Convert.IsDBNull(dr["SN"]) ? "00" : dr["SN"].ToString());
            xt = xd.CreateAttribute("SN");
            xt.InnerXml = mySN;
            myNode.Attributes.Append(xt);

            string ModuleCode = (Convert.IsDBNull(dr["ModuleCode"]) ? "00" : dr["ModuleCode"].ToString());
            xt = xd.CreateAttribute("ModuleCode");
            xt.InnerXml = ModuleCode;
            myNode.Attributes.Append(xt);

            if (!Convert.IsDBNull(dr["Image"]))
            {
                xt = xd.CreateAttribute("ImageUrl");
                xt.InnerXml = dr["Image"].ToString();
                myNode.Attributes.Append(xt);
            }
            xt = xd.CreateAttribute("NavigateUrl");
            xt.Value = dr["URL"].ToString();
            if (dr["ModuleCode"].ToString().IndexOf("SCDMLM") >= 0)
            {
                string db = HttpUtility.UrlEncode("四川大家财务统计", System.Text.Encoding.GetEncoding("GB2312"));
                string name = HttpUtility.UrlEncode("日志查询", System.Text.Encoding.GetEncoding("GB2312"));
                xt.Value = xt.Value + "&db=" + db + "&name=" + name;
            }
            myNode.Attributes.Append(xt);

            xt = xd.CreateAttribute("NodeData");
            xt.InnerXml = dr["Id"].ToString();
            myNode.Attributes.Append(xt);

            XmlNode nodeSN = node.Attributes.GetNamedItem("SN");

            if (nodeSN == null)
                nodeParent = node;
            else
            {
                if (mySN.Length < nodeSN.InnerXml.Length)
                {
                    for (int i = 0; i < (nodeSN.InnerXml.Length - mySN.Length) / 2; i++)
                    {
                        node = node.ParentNode;
                        nodeParent = node.ParentNode;
                    }
                    //nodeParent=node.OwnerDocument.SelectSingleNode('a');
                }
                else if (mySN.Length == nodeSN.InnerXml.Length)
                    nodeParent = node.ParentNode;
                else
                    nodeParent = node;
            }

            //.SelectSingleNode(nodeSN.InnerXml.Substring(0,nodeSN.InnerXml.Length-2));

            nodeParent.AppendChild(myNode);
            return myNode;
        }
        public enum Type
        {
            Main,
            Menu
        } 
        /// <summary>
        /// 创建新模块
        /// </summary>
        /// <param name="parentID">模块父节点ID</param>
        /// <param name="ModuleCode">模块编号</param>
        /// <param name="CName">模块中文名称</param>
        /// <param name="EName">模块英文名称</param>
        /// <param name="SName">模块简称</param>
        /// <param name="ModuleType">模块类型</param>
        /// <param name="Image">模块图片文件</param>
        /// <param name="URL">模块入口地址</param>
        /// <param name="Para">模块入口参数</param>
        /// <param name="Descr">模块描述</param>
        /// <param name="Owner">员工号,0为系统默认</param>
        /// <param name="TempName">按钮模版名</param>
        /// <param name="refModuleID">相对位置模块标识</param>
        /// <param name="refLocation">相对位置0前面，1后面；2子模块</param>
        /// <param name="desc">操作错误后描述</param>
        /// <returns></returns>
        public int Add( int parentID, string ModuleCode, string CName,string EName,string SName,int ModuleType,string Image,string URL,string Para,string Descr,int Owner,string TempName,string refModuleID, string refLocation, out string desc)
        {
            if (ibm.CheckModuleCode(ModuleCode))
            {
                desc = "模块编号[" + ModuleCode + "]已经存在!";
                return -1;
            }
            int modulesid = Add(
                 parentID,      //模块父节点ID
                 CName,		    //模块中文名称
                 EName,		    //模块英文名称
                 SName,		    //模块简称
                 ModuleType,	//模块类型
                 Image,		    //模块图片文件
                 URL,			//模块入口地址
                 Para,		    //模块入口参数
                 Descr,		    //模块描述
                 Owner,	    	//员工号,0为系统默认
                 TempName,		//按钮模版名
                 ModuleCode      //模块代码
            );

            if (modulesid > 0)
            {
                desc = "";
                if (modulesid.ToString() == refModuleID)
                    return 1;
                else
                {
                    if (Move(Convert.ToInt32(refModuleID), refLocation))
                        return 1;
                    else
                    {
                        desc = "创建到指定位置时出错";
                        return -1;
                    }
                }

            }

            desc = "创建模块时出错";
            return 0;

        }
        /// <summary>
        /// 移动模块
        /// </summary>
        /// <param name="tbRefModuleID">目标模块号</param>
        /// <param name="refLocation">相对位置0前面，1后面；2子模块</param>
        /// <returns></returns>
        private bool Move(int tbRefModuleID, string refLocation)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 创建新模块
        /// </summary>
        /// <param name="modules">模块实体参数</param>
        /// <param name="tbRefModuleID">目标模块号</param>
        /// <param name="refLocation">相对位置0前面，1后面；2子模块</param>
        /// <param name="desc">操钮模版名</param>
        /// <returns></returns>
        public int Add(Model.Modules modules, string tbRefModuleID,string refLocation, out string desc)
        {
            return this.Add(modules.PID, modules.ModuleCode, modules.CName, modules.EName, modules.SName, modules.Type.Value,
                modules.Image, modules.URL, modules.Para, modules.Descr, modules.Owner, modules.ButtonsTheme, tbRefModuleID, refLocation, out desc);

        }
        /// <summary>
        /// 创建新模块
        /// </summary>
        /// <param name="parentID">模块父节点ID</param>
        /// <param name="CName">模块中文名称</param>
        /// <param name="EName">模块英文名称</param>
        /// <param name="SName">模块简称</param>
        /// <param name="ModuleType">模块类型</param>
        /// <param name="Image">模块图片文件</param>
        /// <param name="URL">模块入口地址</param>
        /// <param name="Para">模块入口参数</param>
        /// <param name="Descr">模块描述</param>
        /// <param name="Owner">员工号,0为系统默认</param>
        /// <param name="TempName">按钮模版名</param>
        /// <param name="ModuleCode">模块代码</param>
        /// <returns></returns>
        public int Add(int parentID, string CName,	string EName,string SName,int ModuleType,string Image,string URL,string Para,string Descr,int Owner,	string TempName,string ModuleCode)
        {
            if (CName.Trim() == "")
                throw new System.Exception("模块名不能为空");
            Model.Modules m = new Model.Modules();
            m.PID = parentID;
            m.CName = CName;
            m.EName = EName;
            m.SName = SName;
            m.Type = ModuleType;
            m.Image = Image;
            m.URL = URL;
            m.Para = Para;
            m.Descr = Descr;
            m.Owner = Owner;
            m.ModuleCode = ModuleCode;
            return ibm.Add(m);
        }
        
        internal int Update(Model.Modules m)
        {
            return ibm.Update(m);
        }

        internal bool CheckModuleCode(string p)
        {
            if (ibm.GetTotalCount(new Model.Modules() { ModuleCode = p }) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool Del(int p)
        {
            if (ibm.Delete(p) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="m">相对位置模块标识</param>
        /// <param name="refModuleID">目标模块号</param>
        /// <param name="refLocation">相对位置0前面，1后面；2子模块</param>
        /// <param name="desc">操作错误后描述</param>
        /// <returns></returns>
        internal int Modify(Model.Modules m, string refModuleID,string refLocation,out string desc)
        {
            if (ibm.CheckModuleCode(m.ModuleCode))
            {
                desc = "模块编号[" + m.ModuleCode + "] 已经存在，不能修改为此编号[编号唯一]";
                return -1;
            }

            if (ibm.Update(m) > 0)
            {
                desc = "";
                if (m.ID.ToString() == refModuleID)
                    return 1;
                else
                {
                    if (Move(Convert.ToInt32(refModuleID), refLocation))
                        return 1;
                    else
                    {
                        desc = "保存到指定位置时出错";
                        return -1;
                    }
                }
            }
            desc = "保存模块信息时出错";
            return 0;
        }
    }
}