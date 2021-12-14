<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.ModuleRoles" Codebehind="ModuleRoles.aspx.cs" %>
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
		
		
		
		
	//-->
    </script>

    <script language="javascript" id="clientEventHandlersJS">
    <!--
        function ChoosePost(obj) {
            objcheck=obj.previousSibling.firstChild;
            if (objcheck != null) {
                objcheck.checked = !objcheck.checked;
            }
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
        var moduleID = '<%=Request.QueryString["ID"] %>';
        function NextStep(objID, boolCheck, obj) {
            document.getElementById('oToolTip').style.display = '';
            
            initOpChecked(objID,obj);
            //alert("EmplID=" + roleTypeID + ":::PostID=" + objID + "::::checked=" + boolCheck);
            //return;
            var ret = OA.RBAC.Roles.ModuleRoles.changeRole(objID, moduleID, boolCheck);
            document.getElementById('oToolTip').style.display = 'none';
        }
        function initOpChecked(objID, objRun) {
            var objRunParent = document.getElementById("NM" + objID);
            var objOpCheckes = objRunParent.getElementsByTagName("input");
            
            if (objOpCheckes != null && objOpCheckes.length > 0) {
                for (var i = 0; i < objOpCheckes.length; i++) {
                    if (objRun == objOpCheckes[i])
                        continue;
                    if (objRun.checked) {
                        objOpCheckes[i].disabled = false;
                    }
                    else {
                        objOpCheckes[i].checked = false;
                        objOpCheckes[i].disabled = true;
                    }
                }
            }
        }
        function checkAll(objOP) {
            var objOPParent = objOP.parentNode;
            var objOpCheckes = objOPParent.getElementsByTagName("input");

            if (objOpCheckes != null && objOpCheckes.length > 0) {
                var opValues = "";
                for (var i = 0; i < objOpCheckes.length; i++) {
                    if (objOpCheckes[i].checked)
                        opValues = "1" + opValues;
                    else
                        opValues = "0" + opValues;
                }
                //alert(objOPParent.id);
                var ret = OA.RBAC.Roles.ModuleRoles.changeRoleOP(objOPParent.id, moduleID, opValues);
            }
        }
    //-->
    </script>
     

</head>
<body leftmargin="0" topmargin="0" onunload="document.getElementById('oToolTip').style.display = '';">
    <form id="PostModules" name="PostModules" method="post" runat="server">
    <DIV id="oToolTip" style="DISPLAY: none;POSITION: absolute;left:30px;top:30px">
			    <div style="BORDER-RIGHT: black 2px solid; PADDING-RIGHT: 10px; BORDER-TOP: black 2px solid; PADDING-LEFT: 10px; FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=skyblue, EndColorStr=#FFFFFF); LEFT: 200px; PADDING-BOTTOM: 10px; FONT: 10pt tahoma; BORDER-LEFT: black 2px solid; WIDTH: 170px; PADDING-TOP: 10px; BORDER-BOTTOM: black 2px solid; TOP: 200px; HEIGHT: 120px">
				    <b>正在显示角色<br />结构:</b>
				    <hr style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
					    SIZE="1">
				    <label id="oToolTipContent">:::请稍候:::</label>
				    <div></div>
			    </div>
		    </DIV>
    <table height="100%" width="100%" bgcolor="#ffffff">
        
        <tr>
            <td valign="top" align="center" width="200" bgcolor="#f0f0f0" colspan="1" height="392" rowspan="2">
                <table border="1" width="98%" cellspacing="0" cellpadding="2" align="center" style="border-collapse: collapse">
                    
                    <%
                        for (int iGroup = 0; iGroup < dtGroups.Rows.Count; iGroup++)
                        {
                            %>
                            <tr  bgcolor="#f0f0f0" align=center>
                                <td colspan="2" style="font-weight:bold"><%=iGroup + 1 %>,<%=dtGroups.Rows[iGroup]["RoleGroupName"]%></td>
                                <td colspan="2"></td>
                            </tr>
                            <%
                            System.Data.DataRow[] drs = Dt.Select("GroupName='" + dtGroups.Rows[iGroup]["RoleGroupName"] + "'", "GroupOrder");
                            for (int k = 0; k < drs.Length; k++)
                            {
                                string strChecked = "";
                                if (!Convert.IsDBNull(drs[k]["ModuleID"]))
                                    strChecked = "checked";
                                %>
                            <tr id="NM<%=drs[k]["Id"].ToString()%>" bgcolor="white" onmouseover="this.bgColor='LemonChiffon'"
                                
                                onmouseout="this.bgColor='white'"  style="cursor:hand" onclick="SelectRow(this)">
                                <td><input type="checkbox" onpropertychange="NextStep('<%=drs[k]["ID"] %>',this.checked,this)" <%=strChecked %>/></td>
                                <td align="left" style="text-decoration: underline; color: #0000FF"  nowrap onclick="javascript:ChoosePost(this)">
                                    <%=drs[k]["CName"]%>
                                </td>
                                <td nowrap id="<%=drs[k]["ID"] %>"><%=RetrieveButtons(drs[k]["AccAbility"].ToString(), drs[k]["OpAbility"].ToString())%>
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
                            <tr  bgcolor="#f0f0f0" align=center>
                                <td colspan="2" style="font-weight:bold;color:Red">XX未知分类</td>
                                <td colspan="2"></td>
                            </tr>
                            <%
                            System.Data.DataRow[] drs = Dt.Select("", "GroupOrder");
                            for (int k = 0; k < drs.Length; k++)
                            {
                                string strChecked = "";
                                if (!Convert.IsDBNull(drs[k]["ModuleID"]))
                                    strChecked = "checked";
                                %>
                            <tr id="Tr1" bgcolor="white" onmouseover="this.bgColor='LemonChiffon'"
                                ondblclick="Editposts('<%=Convert.IsDBNull(drs[k]["Id"])?"":drs[k]["Id"]%>')"
                                onmouseout="this.bgColor='white'"  style="cursor:hand" onclick="SelectRow(this)">
                                <td><input type="checkbox" onpropertychange="NextStep('<%=drs[k]["ID"] %>',this.checked,this)" <%=strChecked %>/></td>
                                <td align="left"  style="text-decoration: underline; color: #0000FF"  nowrap onclick="javascript:ChoosePost(this)">
                                    <%=drs[k]["CName"]%>
                                </td>
                                 <td nowrap><%=RetrieveButtons(drs[k]["AccAbility"].ToString(), drs[k]["OpAbility"].ToString())%>
                                </td>
                            </tr>
                            <%
                            Dt.Rows.Remove(drs[k]);
                            }
                        }%>
                        
                </table>
            </td>
            
        </tr>
        
    </table>

    <input id="hArrayModuleID" type="hidden" name="hArrayModuleID"><input id="hArrayModuleIDRemoved"
        type="hidden" name="hArrayModuleIDRemoved">
    <input id="hModuleIDSelected" type="hidden" name="hModuleIDSelected" />
    </form>
</body>
</html>
