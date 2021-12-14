<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chooseRoles.aspx.cs" Inherits="OA.RBAC.Organizations.chooseRoles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
    function selectThis(strobj,obj)
    {
        var chkbox = document.getElementById(strobj);
        if(chkbox.checked)
        {
            obj.style.backgroundColor="";
        }
        else
        {
            obj.style.backgroundColor="#a2a3a4";
        }
    }
    function checkThis(obj,strobj)
    {
        var chkbox = document.getElementById(strobj);
        if(obj.checked)
        {
            chkbox.style.backgroundColor="#a2a3a4";
        }
        else
        {
            chkbox.style.backgroundColor="";
        }
    }
    function selectAll()
    {
        for (var i=0;i<document.all.length;i++)
		{
			ename = document.all[i].name;
			if (typeof(ename) != 'undefined')
			{
				if (ename.indexOf('chk')==0)
				{
					
					if(!document.all[i].checked)
					{
						document.all[i].click();
					}
				}
			}

		}
    }
    function selectNone()
    {
        for (var i=0;i<document.all.length;i++)
		{
			ename = document.all[i].name;
			if (typeof(ename) != 'undefined')
			{
				if (ename.indexOf('chk')==0)
				{
					
					if(document.all[i].checked)
					{
						document.all[i].click();
					}
				}
			}

		}
    }
    function checkNewValue()
    {
        var resultDep='部门:', resultPost='岗位:', resultEmp='人员:';

		for (i=0;i<document.all.length;i++)
		{
		    var id= "";
			eid = document.all[i].id;
			if (typeof(eid) != 'undefined' && typeof(eid) != null)
			{
				if (eid.indexOf('Dep_')==0)
				{
					if (document.all[i].checked)
					{
					    id = eid.split('_')[1];
						resultDep +=  id+',' + document.all[i].value+";";
					}
				}
				if (eid.indexOf('Post_')==0)
				{
					if (document.all[i].checked)
					{
					    id = eid.split('_')[1];
						resultPost +=  id+',' + document.all[i].value+";";
					}
				}
				if (eid.indexOf('Emp_')==0)
				{
					if (document.all[i].checked)
					{
					    id = eid.split('_')[1];
						resultEmp +=  id+',' + document.all[i].value+";";
					}
				}
			}
		}
		
		var backValue = "";
		if(resultDep != '部门:')
		{
		    backValue += resultDep;
		    backValue += "@";
		}
		if(resultPost != '岗位:')
		{
		    backValue += resultPost;
		    backValue += "@";
		}
		if(resultEmp != '人员:')
		{
		    backValue += resultEmp;
		}

		if(backValue.lastIndexOf('@')==backValue.length-1)
		{
		    backValue = backValue.substring(0,backValue.length-1);
		}
//		alert(backValue);
		window.parent.returnValue = backValue;
		window.parent.close();
    }
    function checkAll(obj)
    {
        var id=obj.id;
        var depID = id.split('_')[1];
        var lab = document.getElementById("labAll_"+id);//部门名
        var allElementInDep = document.getElementById("td_"+depID).all;

        if(obj.checked)
        {
            lab.style.backgroundColor="#a2a3a4";
        }
        else
        {
            lab.style.backgroundColor="";
        }
        
        for (i=0;i<allElementInDep.length;i++)
	    {
	        var curElem = allElementInDep[i].name;
		    if (typeof(curElem) != 'undefined' && typeof(curElem) != null)
		    {
			    if (curElem.indexOf('chk')==0)
			    {
			        if (allElementInDep[i].checked != obj.checked)
				    {
				        allElementInDep[i].click();
				    }
			    }
		    }
		}
    }
    var emps;
    function getEmp()
    {
        var DeptID = document.getElementById("lstDepts").value;
        var PName = document.getElementById("txtKeyWord").value;
        var str = OA.RBAC.Organizations.chooseRoles.searchEmp(DeptID, PName);
        
        if(str.value == "")
        {
            return;
        }
        emps= str.value.split(';');
        for(var i=0;i<emps.length;i++)
        {
            var chkBox = "Emp_"+emps[i];
            var lab = "lab_Emp_"+emps[i];
            var curEmp = document.getElementById(chkBox);
            var curlab = document.getElementById(lab);
            if(curlab != undefined)
            {
                curlab.style.backgroundColor = "#a0b3c8";
            }
        }
    }
    function cancelSearch()
    {
        if(emps.length>0)
        {
            for(var i=0;i<emps.length;i++)
            {
                var chkBox = "Emp_"+emps[i];
                var lab = "lab_Emp_"+emps[i];
                var curEmp = document.getElementById(chkBox);
                var curlab = document.getElementById(lab);
                if(curEmp != undefined && !curEmp.checked)
                {
                    if(curlab != undefined)
                    {
                        curlab.style.backgroundColor = "white";
                    }
                }
            }
        }
    }
    function chkSearch(obj)
    {
        if(emps!=undefined && emps.length>0)
        {
            for(var i=0;i<emps.length;i++)
            {
                var chkBox = "Emp_"+emps[i];
                var lab = "lab_Emp_"+emps[i];
                var curEmp = document.getElementById(chkBox);
                var curlab = document.getElementById(lab);
                if(curEmp != undefined)
                {
                    if(curlab != undefined)
                    {
                        if(curEmp.checked != obj.checked)
                        {
                            curEmp.click();
                        }
                    }
                }
            }
        }
    }
    </script>

</head>
<body style="font-size:12px;">
    <form id="form1" runat="server">
    <div style="width: 100%; height: 25;">
        <input type="button" value="确定" onclick="checkNewValue();" />
                    <input type="button" value="取消" onclick="javascript:window.close();" />
            <input type="button" value="全选择" onclick="selectAll();" />
                    <input type="button" value="全不选" onclick="selectNone();" />
                
    </div>
    <div id="divDepartments" style="width: 100%;" visible="false" runat="server">
        <table width="100%">
            <tr>
                <td width="50" align="left" valign="top">
                    部门
                </td>
                <td id="tdDepList" runat="server">
                </td>
            </tr>
        </table>
    </div>
    <div id="divPosts" style="width: 100%;" visible="false" runat="server">
        <table width="100%">
            <tr>
                <td width="50" align="left" valign="top">
                    岗位
                </td>
                <td id="tdPostList" runat="server">
                </td>
            </tr>
        </table>
    </div>
    <div id="divEmployees" style="width: 100%;" visible="false" runat="server">
        <table width="100%">
            <tr height="1%">
                <td style="height: 0.02%" nowrap align="center">
                    &nbsp;选择部门&nbsp;
                </td>
                <td style="height: 0.02%">
                    <asp:DropDownList ID="lstDepts" runat="server" Width="224px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr height="1%">
                <td nowrap align="center">
                    模糊查询
                </td>
                <td>
                    <input id="txtKeyWord" type="text" size="15" name="txtKeyWord" runat="server">
                    <input id="Button1" type="button" value=" 查找 " name ="btn" onclick="getEmp();">
                    (输入姓名或用账号名)
                    <input type="button" id="cancel" value="取消" onclick="cancelSearch();" />
                    <input type="checkbox" id="chk" name="chk" onclick="chkSearch(this);" /><label>选择查找结果</label>
                </td>
            </tr>
            <tr>
                <td width="50" rowspan="2" align="left" valign="top">
                    人员
                </td>
                <td id="tdEmpList" runat="server">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <input type="button" value="确定" onclick="checkNewValue();" />
                    <input type="button" value="取消" onclick="javascript:window.close();" />
                    <input type="button" value="全选择" onclick="selectAll();" />
                    <input type="button" value="全不选" onclick="selectNone();" />
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="hiddenDep" runat="server" /><!-- 部门 -->
    <input type="hidden" id="hiddenPost" runat="server" /><!-- 岗位 -->
    <input type="hidden" id="hiddenEmployee" runat="server" /><!-- 人员 -->
    </form>
</body>
</html>
