<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.searchEmplRoles" Codebehind="searchEmplRoles.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>查找用户</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta http-equiv="Content-Language" content="zh-cn">
    <link href="../../Include/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
			var Unchoose=true;
			function List()
			{
			    window.frames['fraUserList'].location = 'searchEmplRoles_list.aspx?dept=' + lstDepts.value + '&search=dept';
				Unchoose=false;
			}
			function Search()
			{
			    window.frames['fraUserList'].location = 'searchEmplRoles_list.aspx?multiple=1&' + queryString + '&condition=' + txtKeyWord.value + '&search=condition&dept=' + lstDepts.value;
				Unchoose=false;
            }
            function enterSearch() {
                if (event.keyCode == 13) {
                    Search();
                }
            }
            var roleTypeID = '<%=Request.QueryString["txtRoleID"] %>';
            var queryString = '<%=Request.ServerVariables["query_string"].ToString()%>';
            
			function NextStep(objID,boolCheck) {
			    parent.document.getElementById('oToolTip').style.display = '';
			    AddRemove(objID, boolCheck);
			    var ret = OA.RBAC.Organizations.searchEmpls.changeRole(roleTypeID, objID, boolCheck);
			    parent.document.getElementById('oToolTip').style.display = 'none';
			}
			function window_onload() {
			    window.frames['fraUserList'].location = 'searchEmplRoles_list.aspx?multiple=1&' + queryString;
			    parent.document.getElementById('oToolTip').style.display = 'none';
			}
			function AddRemove(objID, boolCheck) {
			
			}
			function locateHref(node) {
			    if (node != null) {
			        var idDept = node.getAttribute('NodeData');
			        if (idDept == "0") {
			            lstDepts.selectedIndex = 0;
			            Search();
			        }
			        else {
			            for (var i = 0; i < lstDepts.options.length; i++) {
			                if (lstDepts.options[i].value == idDept) {
			                    lstDepts.selectedIndex = i;
			                    Search();
			                    break;
			                }
			            }
			        }
			        //alert(idDept);
			        //parent.frames['MainList'].location = '../../RBAC/Organizations/Deptlist.aspx?id=' + ;
			    }
			}
    </script>
    <script language="javascript" for="Treeview1" event="onclick">
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			locateHref(node);
	</script>

</head>
<body bgcolor="#f0f0f0" onload="return window_onload()">
    <table height="100%" cellspacing="0" width="100%" border="0">
        <tr>
            <td width="200px">组织机构图<br />
                    <?XML:NAMESPACE PREFIX=TVNS />
		        <?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
		        <tvns:treeview id="Treeview1" treenodesrc="xml/DeptTree.xml" target="MainList" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)"
		         style="height:100%;width:200px">
		        </tvns:treeview>
            </td>
            <td>
                <table height="100%" cellspacing="0" width="100%" border="0">
                    <tr height="1%">
                        <td colspan=1>
                            <table border="0">
                                <tr>
                                    <td nowrap align="center">
                                        选择部门
                                    </td>
                                    <td>
                                        <select id="lstDepts" style="width: 250px" align="top" name="lstDepts">
                                            <option value="-1" selected>======请选择======</option>
                                            <%
                                                Response.Write(selectOption);
                                            %>
                                        </select>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td rowspan=2>已经选择的角色,通过复选框选择后可以直接保存</td>
                    </tr>
                    <tr height="1%">
                        <td colspan=1>
                            <table border="0">
                               <tr>
                                    <td nowrap align="center">
                                        模糊查询
                                    </td>
                                    <td>
                                        <input id="txtKeyWord" type="text" size="15" name="txtKeyWord" onkeydown="enterSearch()">
                                        <input onclick="Search()" type="button" value=" 查找 ">
                                        (输入姓名或用账号名)
                                    </td>
                                </tr> 
                            </table>
                        </td>
                    </tr>   
                        
                        
                        <tr height="280">
                            <td valign="top" align="left" colspan="1" style="width:60%">
                                <iframe id="fraUserList" width="98%" height="100%" align="left"></iframe>
                            </td>
                            <td valign="top" align="left" colspan="1" style="width:40%">
                                <iframe id="fraEmplRoles" width="98%" height="100%" align="left"></iframe>
                            </td>
                        </tr>
                        <tr height="1%" style="visibility:hidden">
                            <td align="center" colspan="2">
                                <input type="button" value=" 确定 " onclick="NextStep()">
                                <input type="button" value=" 取消 " onclick="window.parent.close()">
                            </td>
                        </tr>
                </table>
            </td>
        </tr>
    </table>
    
</body>
</html>
