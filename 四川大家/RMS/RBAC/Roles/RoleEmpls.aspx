<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.RoleEmpls" Codebehind="RoleEmpls.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>PersonList</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft FrontPage 4.0" name="GENERATOR">
    <meta content="FrontPage.Editor.Document" name="ProgId">
    <style>
        TABLE
        {
            font-weight: normal;
            font-size: 14px;
            color: #000000;
            text-decoration: none;
        }
        #frmSaveAllModules
        {
            width: 124px;
        }
    </style>

    

    <script language="javascript">
	<!--
        
		
		function firSubmit()
		{
			if(PostModules.txtRoleType.value==""
				||PostModules.txtRoleTypeName.value==""
				||PostModules.txtRoleID.value==""
				||PostModules.txtRoleName.value=="")
			{
				alert('没有选择员工，部门，职位或岗位');
				return false;
			}
			document.getElementById('oToolTip').style.display = '';
			var strUrl="";
			strUrl = "../Organizations/searchEmpls.aspx?txtRoleType=" + PostModules.txtRoleType.value;
			strUrl= strUrl + "&txtRoleID=" + PostModules.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + PostModules.txtRoleName.value;
			window.frames['frmAllModules'].location = strUrl;
			window.status = "正在读取权限，请稍候......";
			return false;
			
		}

		function SaveModules() {
		    NextStep('34', true);
		    return;

		    document.getElementById('oToolTip').style.display = '';
			if(PostModules.txtRoleType.value==""
				||PostModules.txtRoleTypeName.value==""
				||PostModules.txtRoleID.value==""
				||PostModules.txtRoleName.value=="")
			{
			    alert('没有选择员工，部门，职位或岗位');
			    document.getElementById('oToolTip').style.display = 'none';
				return false;
			}
			
			var CheckModules;
			CheckModules=window.frames['frmAllModules'].document.all["TreeView1"].getChildren();
			PostModules.hArrayModuleID.value="";
			PostModules.hArrayModuleIDRemoved.value="";
			collectModules(CheckModules)
			
			
			
			var strUrl="";
			strUrl="SaveAllModules.aspx?hArrayModuleID=" + PostModules.hArrayModuleID.value;
			strUrl +="&hArrayModuleIDRemoved=" + PostModules.hArrayModuleIDRemoved.value;
			strUrl +="&txtRoleType=" + PostModules.txtRoleType.value;
			strUrl= strUrl + "&txtRoleID=" + PostModules.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + PostModules.txtRoleName.value;
			
			window.frames['frmSaveAllModules'].location=strUrl;
			
			PostModules.buttSave.value="正在保存...";
			PostModules.buttSave.disabled=true;
			document.getElementById('oToolTip').style.display = 'none';
			return false;
		}
		function collectModules(TreeNodes) {
		    //alert(PostModules.hArrayModuleID.value);debugger;
			if(TreeNodes!=null&&typeof(TreeNodes) != "undefined")
			{
				for(var i=0;i<	TreeNodes.length;i++)
				{
					var currentChild;
					currentChild = TreeNodes[i];
					//alert(PostModules.hArrayModuleID.value);
					//&& currentChild.getAttribute("Text").indexOf("<b>")<0  继承模块也保存
					if (currentChild.getAttribute("Checked"))
						PostModules.hArrayModuleID.value +="," + currentChild.getAttribute("NodeData");
					else
						PostModules.hArrayModuleIDRemoved.value +="," + currentChild.getAttribute("NodeData");
					
					var childNodesSub;
					childNodesSub = currentChild.getChildren();
					collectModules(childNodesSub);
				}
			}
				//添加的模块
			if(PostModules.hArrayModuleID.value.substring(0,1)==",")
				PostModules.hArrayModuleID.value=PostModules.hArrayModuleID.value.substring(1,PostModules.hArrayModuleID.value.length);
				
			//删除的模块
			if(PostModules.hArrayModuleIDRemoved.value.substring(0,1)==",")
				PostModules.hArrayModuleIDRemoved.value=PostModules.hArrayModuleIDRemoved.value.substring(1,PostModules.hArrayModuleIDRemoved.value.length);
		}
		function reloadAll()
		{
			if(PostModules.txtRoleType.value==""
					||PostModules.txtRoleTypeName.value==""
					||PostModules.txtRoleID.value==""
					||PostModules.txtRoleName.value=="")
				{
					alert('没有选择员工，部门，职位或岗位');
					return false;
				}	
			window.frames['frmAllModules'].location.reload();
		}


		function SaveButtons() {

		    var hAccess = window.frames['frmAccess'].document.all["hAccess"];
		    if (typeof (hAccess) == 'undefined' || PostModules.hModuleIDSelected.value=='') {
		        alert('请选择模块');
		        return false;
		    }
		    var CheckModules;

		    var showButton = window.frames['frmAccess'].document.all["showButton"];
		    if (showButton.value == "") {
		        alert('没有操作按钮等权限');
		        return false;
		    }
		    var sAccess = "";

		    for (var i = 0; i < parseInt(window.frames['frmAccess'].document.all["hAccess"].value, 10); i++) {
		        if (window.frames['frmAccess'].document.all["checkbox" + i].checked)
		            sAccess = "1" + sAccess;
		        else
		            sAccess = "0" + sAccess;
		    }

		    var strUrl = "";
		    strUrl = "SaveOperateButtons.aspx?hArrayModuleID=" + PostModules.hModuleIDSelected.value;
		    strUrl += "&txtRoleType=" + PostModules.txtRoleType.value;
		    strUrl = strUrl + "&txtRoleID=" + PostModules.txtRoleID.value;
		    strUrl = strUrl + "&txtRoleName=" + PostModules.txtRoleName.value;
		    strUrl = strUrl + "&hAccess=" + sAccess;
		    strUrl = strUrl + "&TemName=" + window.frames['frmAccess'].document.all["showButton"].value;



		    //alert(strUrl);
		    //return false;
		    window.frames['frmSaveAllModules'].location = strUrl;

		    PostModules.buttSave.value = "正在保存...";
		    PostModules.buttSave.disabled = true;

		    return false;
		}
		function collectModulesButtons(TreeNodes) {
		    if (TreeNodes != null && typeof (TreeNodes) != "undefined") {
		        for (var i = 0; i < TreeNodes.length; i++) {
		            var currentChild;
		            currentChild = TreeNodes[i];
		            if (currentChild.getAttribute("Checked"))
		                PostModules.hArrayModuleID.value += "," + currentChild.getAttribute("NodeData");
		            else
		                PostModules.hArrayModuleIDRemoved.value += "," + currentChild.getAttribute("NodeData");

		            var childNodes;
		            childNodes = currentChild.getChildren();
		            collectModulesButtons(childNodes);
		        }
		    }
		    //添加的模块
		    if (PostModules.hArrayModuleID.value.substring(0, 1) == ",")
		        PostModules.hArrayModuleID.value = PostModules.hArrayModuleID.value.substring(1, PostModules.hArrayModuleID.value.length);

		    //删除的模块
		    if (PostModules.hArrayModuleIDRemoved.value.substring(0, 1) == ",")
		        PostModules.hArrayModuleIDRemoved.value = PostModules.hArrayModuleIDRemoved.value.substring(1, PostModules.hArrayModuleIDRemoved.value.length);
		}
		
	//-->
    </script>

    <script language="javascript" id="clientEventHandlersJS">
<!--
function ChoosePost(roleName,RoleID) {
    PostModules.txtRoleTypeName.value = '按角色分配';
    PostModules.txtRoleName.value = roleName;
    PostModules.txtRoleID.value = RoleID;
    PostModules.txtRoleType.value = "3";
    RoleNameDiv.innerHTML = "【" + roleName + "】角色有人员：";
    firSubmit();
}
var SelEmpl = '';

function SelectRow(objRow) {


    if (SelEmpl != '') {
        SelEmpl.style.backgroundColor = '';
        SelEmpl.style.color = '';
    }

    SelEmpl = objRow;
    SelEmpl.style.backgroundColor = 'gold';
    SelEmpl.style.color = 'black';
}

//-->
    </script>
    <script language="javascript" type="text/javascript">
         var xmlHttp;
         
         function createXMLHttpRequest()
         {
             //判断浏览器类型并创建对象
             //IE
             if (window.ActiveXObject)
             {
                 xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
             }
             //FF
             else if (window.XMLHttpRequest)
             {
                 xmlHttp = new XMLHttpRequest();
             }
         }
         
         //光标处于输入框时引发的动作
         function updateTotal(txtRoleID, objID, boolCheck) {
             if (xmlHttp == null)
                 createXMLHttpRequest();
             url = "RoleEmpls_.aspx?txtRoleID=" + txtRoleID + "&objID=" + objID + "&boolCheck=" + boolCheck;
             xmlHttp.open("GET",url,true);
             xmlHttp.onreadystatechange = doUpdate;
             xmlHttp.send();
             return false;
         }
         
         function doUpdate()
         {
             if (xmlHttp.readyState == 4)
             {
                document.getElementById('oToolTip').style.display = 'none';
                //s document.forms[0].elements["TOT"].value = xmlHttp.responseText;
             }
         }
     </script>
 
</head>
<body leftmargin="0" topmargin="0">
    <form id="PostModules" name="PostModules" method="post" runat="server">
    <DIV id="oToolTip" style="DISPLAY: none;POSITION: absolute;left:200px;top:100px">
			    <div style="BORDER-RIGHT: black 2px solid; PADDING-RIGHT: 10px; BORDER-TOP: black 2px solid; PADDING-LEFT: 10px; FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=skyblue, EndColorStr=#FFFFFF); LEFT: 200px; PADDING-BOTTOM: 10px; FONT: 10pt tahoma; BORDER-LEFT: black 2px solid; WIDTH: 170px; PADDING-TOP: 10px; BORDER-BOTTOM: black 2px solid; TOP: 200px; HEIGHT: 120px">
				    <b>正在显示该角色的<br />人员列表:</b>
				    <hr style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
					    SIZE="1">
				    <label id="oToolTipContent">:::请稍候:::</label>
				    <div></div>
			    </div>
		    </DIV>
    <table height="100%" width="100%" bgcolor="#ffffff">
        <tr bgcolor="lightgrey">
            <td colspan="3" height="38" valign="middle">
                <b><font color="red">
                    <asp:Image ID="Image1" runat="server" ImageUrl="../../images/icons/0060_a.gif" ImageAlign="AbsMiddle">
                    </asp:Image>指定角色与人员关系</font></b>
            </td>
        </tr>
        
        <tr>
            <td valign="top" align="center" width="200" bgcolor="#f0f0f0" colspan="1" height="392" rowspan="2">
                <table border="1" width="98%" cellspacing="0" cellpadding="2" align="center" style="border-collapse: collapse">
                    <tr bgcolor="#e0e0e0">
                        <td align="center" nowrap>
                            角色名称
                        </td>
                    </tr>
                    <%
                        for (int iGroup = 0; iGroup < dtGroups.Rows.Count; iGroup++)
                        {
                            %>
                            <tr  bgcolor="#f0f0f0">
                                <td colspan="4" style="font-weight:bold"><%=iGroup + 1 %>,<%=dtGroups.Rows[iGroup]["RoleGroupName"]%></td>
                            </tr>
                            <%
                            System.Data.DataRow[] drs = Dt.Select("GroupName='" + dtGroups.Rows[iGroup]["RoleGroupName"] + "'", "GroupOrder");
                            for (int k = 0; k < drs.Length; k++)
                            {%>
                            <tr id="NM<%=drs[k]["Id"].ToString()%>" bgcolor="white" onmouseover="this.bgColor='LemonChiffon'"
                                ondblclick="Editposts('<%=Convert.IsDBNull(drs[k]["Id"])?"":drs[k]["Id"]%>')"
                                onmouseout="this.bgColor='white'"  style="cursor:hand" onclick="SelectRow(this)">
                                <td align="right" style="text-decoration: underline; color: #0000FF"  nowrap onclick="javascript:ChoosePost('<%=drs[k]["CName"] %>','<%=drs[k]["ID"] %>')">
                                    <%=drs[k]["CName"]%>
                                </td>

                            </tr>
                            <%
                            Dt.Rows.Remove(drs[k]);
                            }
                        }%>
                        
                        <%
                        if(Dt.Rows.Count>0)
                        {
                            %>
                            <tr  bgcolor="#f0f0f0">
                                <td colspan="4" style="font-weight:bold;color:Red">XX未知分类</td>
                            </tr>
                            <%
                            System.Data.DataRow[] drs = Dt.Select("", "GroupOrder");
                            for (int k = 0; k < drs.Length; k++)
                            {%>
                            <tr id="Tr1" bgcolor="white" onmouseover="this.bgColor='LemonChiffon'"
                                ondblclick="Editposts('<%=Convert.IsDBNull(drs[k]["Id"])?"":drs[k]["Id"]%>')"
                                onmouseout="this.bgColor='white'"  style="cursor:hand" onclick="SelectRow(this)">
                                <td align="right"  style="text-decoration: underline; color: #0000FF"  nowrap onclick="javascript:ChoosePost('<%=drs[k]["CName"] %>','<%=drs[k]["ID"] %>')">
                                    <%=drs[k]["CName"]%>
                                </td>

                            </tr>
                            <%
                            Dt.Rows.Remove(drs[k]);
                            }
                        }%>
                        
                </table>
            </td>
            <td valign="top" align="center" width="10%" bgcolor="#f0f0f0" colspan="1" height="392" rowspan="2">
                
                <div>
                    <div id="RoleNameDiv"></div>
                </div>
                <div id="DivRoleEmpls"></div>                    
                <table height="124" width="150" border="0" style="visibility:hidden">
                    <tr bgcolor="#ffffff">
                        <td valign="middle" align="right" width="56" bgcolor="#f0f0f0" height="37">
                            <p align="right">
                                类型</p>
                        </td>
                        <td valign="middle" align="center" width="107" bgcolor="#f0f0f0" height="37">
                            <p align="left">
                                <asp:TextBox ID="txtRoleTypeName" runat="server" contentEditable="false" Width="95px" Enabled="true"></asp:TextBox></p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right" width="56" bgcolor="#f0f0f0" height="34">
                            角色
                        </td>
                        <td class="unnamed1" valign="top" align="center" bgcolor="#f0f0f0" height="34">
                            <p align="left">
                                <asp:TextBox ID="txtRoleName" runat="server" contentEditable="false" Width="96px"></asp:TextBox></p>
                        </td>
                    </tr>
                    <tr bgcolor="#ffffff">
                        <td valign="middle" align="right" width="56" bgcolor="#f0f0f0" height="34" 
                            nowrap="nowrap">
                            <font size="2">唯一ID</font>
                        </td>
                        <td class="unnamed1" valign="top" align="center" bgcolor="#f0f0f0" height="34">
                            <p align="left">
                                <font size="2">
                                    <asp:TextBox ID="txtRoleID" runat="server" contentEditable="false" Width="97px"></asp:TextBox></font></p>
                        </td>
                    </tr>
                </table>
                 <p align="center" style="visibility:hidden">
                    &nbsp;<input id="buttSave" type="button" value="保存" onclick="return SaveModules()">&nbsp;&nbsp;
                    <input id="buttReset" type="button" value="复位" onclick="return reloadAll()">
                    <asp:TextBox ID="txtRoleType" runat="server" Width="0px"></asp:TextBox></p>
                <p align="center"  style="visibility:hidden">
                    <div id="Label1">
                    </div>
                    <p  style="visibility:hidden">
                        :>| 权限继承关系
                    
                    <p align="center">
                        <iframe id="frmSaveAllModules" name="frmSaveAllModules" frameborder="0"
                            height="0"></iframe>
                    </p>
                    <p align="left" style="visibility:hidden">
                        <input id="buttSaveButtons" onclick="return SaveButtons();" disabled type="button" value="保存操作按钮等权限" name="buttSaveButtons"></p>
               </p>
            </td>
            <td valign="top" align="left" width="70%" bgcolor="#f0f0f0" height="400">
                <iframe id="frmAllModules" name="frmAllModules" src="" frameborder="0"
                    width="100%" height="100%"></iframe>
            </td>
        </tr>
        
    </table>

    <script language="javascript">
				function RoleEdit(id)
				{
					window.open ('RoleEdit.aspx?id=' + id,'RoleEdit','width=500,height=400,scrollbars=0,top=170,left=200');
				}
    </script>

    &nbsp;
    <input id="hArrayModuleID" type="hidden" name="hArrayModuleID"><input id="hArrayModuleIDRemoved"
        type="hidden" name="hArrayModuleIDRemoved">
    <input id="hModuleIDSelected" type="hidden" name="hModuleIDSelected" />
    <script type="text/javascript" language="javascript">
        function NextStep(objID, boolCheck) {
            document.getElementById('oToolTip').style.display = '';
            //alert(PostModules.txtRoleID.value + "--" + boolCheck);
            //return;
            if (PostModules.txtRoleID.value != "" && objID != null && objID != "") {
                var ret = OA.RBAC.Roles.RoleEmpls.changeRole(PostModules.txtRoleID.value, objID, boolCheck);
                //alert(ret.value);
            }
            document.getElementById('oToolTip').style.display = 'none';
//            updateTotal(PostModules.txtRoleID.value, objID, boolCheck);
        }
    </script>
    </form>
</body>
</html>
