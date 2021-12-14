using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhiFang.ProjectProgressMonitorManage.WeiXin.webapp.ui.Common
{
    public partial class EmpPhoneList : System.Web.UI.Page
    {
        public string aaa = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string sort_list = "<header class=\"fixed\"><div class=\"header\">通讯录<span id=\"submitspan\" style=\"float:right\">确定</span></div></header><div id=\"letter\"></div><div class=\"sort_box\">";

            Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
            var IBHREmployee = context.GetObject("BHREmployee") as ZhiFang.IBLL.RBAC.IBHREmployee;
            var emplist = IBHREmployee.SearchListByHQL(" 1=1 and IsUse=true ", " CName ",1,10000);
            for (int i = 0; i < emplist.list.Count; i++)
            {
                string logo = "<div class=\"num_logo\"><img src=\"../img/icon/user.png\" /></div>";
                string phone = emplist.list[i] != null && emplist.list[i].MobileTel != null ? "<br>电话：" + emplist.list[i].MobileTel : "";
                string deptname = emplist.list[i] != null && emplist.list[i].HRDept != null && emplist.list[i].HRDept.CName != null ? "("+ emplist.list[i].HRDept.CName + ")":"";
                string empname = "<div id = \"empname" + emplist.list[i].Id + "\" class=\"num_name\" >" + emplist.list[i].CName+ deptname + phone + "</div>";
                sort_list += "<div id=\"sort_list_" + emplist.list[i].Id + "\" empid=\"" + emplist.list[i].Id + "\"  class=\"sort_list\" >" + logo + empname + "</div>";
            }
            //for (int i = 0; i < 100; i++)
            //{
            //    string logo = "<div class=\"num_logo\"><img src=\"../img/icon/user.png\" /></div>";
            //    string empname = "<div id = \"empname" + i + "\" class=\"num_name\" >a九" + i + "</div>";
            //    string num_checkbox = "<div class=\"num_checkbox\"  empid=\"" + i + "\" ><img id = \"checked" + i + "\" src=\"../img/icon/unchecked.png\" /></div>";
            //    sort_list += "<div id=\"sort_list_" + i + "\" empid=\"" + i + "\"  class=\"sort_list\" >" + logo + empname + num_checkbox + "</div>";

            //    //sort_list += " <div class=\"sort_list\"><div class=\"num_logo\"><img src = \"js/img.png\"  /></div><div class=\"num_name\">1十</div></div>";
            //}
            sort_list += "</div>";
            sort_list += " <div class=\"initials\"><ul> <li style=\"height: 15px\">A</li><li style=\"height: 15px\">B</li><li style=\"height: 15px\">C</li><li style=\"height: 15px\">D</li><li style=\"height: 15px\">E</li><li style=\"height: 15px\">F</li><li style=\"height: 15px\">G</li><li style=\"height: 15px\">H</li><li style=\"height: 15px\">I</li><li style=\"height: 15px\">J</li><li style=\"height: 15px\">K</li><li style=\"height: 15px\">L</li><li style=\"height: 15px\">M</li><li style=\"height: 15px\">N</li><li style=\"height: 15px\">O</li><li style=\"height: 15px\">P</li><li style=\"height: 15px\">Q</li><li style=\"height: 15px\">R</li><li style=\"height: 15px\">S</li><li style=\"height: 15px\">T</li><li style=\"height: 15px\">U</li><li style=\"height: 15px\">V</li><li style=\"height: 15px\">W</li><li style=\"height: 15px\">X</li><li style=\"height: 15px\">Y</li><li style=\"height: 15px\">Z</li><li style=\"height: 15px\">#</li></ ul ></ div > ";
            aaa = sort_list;
        }
    }
}