<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Modules.ModuleRoleNew" CodeBehind="ModuleRoleNew.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>模块[<%=EName %>]的使用权限，操作权限</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link href="../../Includes/CSS/ioffice.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            input
            {
            	border-width:0;
            	border:none;background:#cccccc;
            }
        </style>
         <STYLE TYPE="text/css">
          .fixedHeader11 {
           position:relative ;
           table-layout:fixed;
           border-style:solid;
           border-top:1px;
           border-color:gray;
           top:expression(this.offsetParent.scrollTop);  
           z-index: 10;
          }

          .fixedHeader11 td{
           text-overflow:ellipsis;
           overflow:hidden;
           white-space: nowrap;
          }
          .btn3_mouseout {
            BORDER-RIGHT: #2C59AA 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #2C59AA 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 12px; FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=#ffffff, EndColorStr=#C3DAF5); BORDER-LEFT: #2C59AA 1px solid; CURSOR: hand; COLOR: black; PADDING-TOP: 2px; BORDER-BOTTOM: #2C59AA 1px solid
            }
            .btn3_mouseover {
            BORDER-RIGHT: #2C59AA 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #2C59AA 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 12px; FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=#ffffff, EndColorStr=#D7E7FA); BORDER-LEFT: #2C59AA 1px solid; CURSOR: hand; COLOR: black; PADDING-TOP: 2px; BORDER-BOTTOM: #2C59AA 1px solid
            }
            .btn3_mousedown
            {
            BORDER-RIGHT: #FFE400 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #FFE400 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 12px; FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=#ffffff, EndColorStr=#C3DAF5); BORDER-LEFT: #FFE400 1px solid; CURSOR: hand; COLOR: black; PADDING-TOP: 2px; BORDER-BOTTOM: #FFE400 1px solid
            }
            .btn3_mouseup {
            BORDER-RIGHT: #2C59AA 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #2C59AA 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 12px; FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=#ffffff, EndColorStr=#C3DAF5); BORDER-LEFT: #2C59AA 1px solid; CURSOR: hand; COLOR: black; PADDING-TOP: 2px; BORDER-BOTTOM: #2C59AA 1px solid
            }
            .checkbox
            {
            	  border:dotted 2px #ff0000;
                  background-color:#996633;
            }
            td.myNowrap
            {
            	white-space:nowrap; 
            }
            td { white-space:nowrap; }
         </STYLE>


	

        <script language="javascript" type="text/javascript">
// <!CDATA[

            function window_onload() {

            }
            function expandParent(obj) {

                if (document.all['tableParent'].style.display == '') {
                    document.all['tableParent'].style.display = 'none';
                    obj.innerHTML = "上级模块描述 +>>";
                }
                else {
                    document.all['tableParent'].style.display = '';
                    obj.innerHTML = "上级模块描述 ->>";
                }
            }

            function selectRoles() {
                var strRolesPara = document.all['hRolesNew'].value;
                if (strRolesPara.indexOf("部门:") < 0)
                    strRolesPara += "@部门";
                if (strRolesPara.indexOf("人员:") < 0)
                    strRolesPara += "@人员";
                if (strRolesPara.indexOf("岗位:") < 0)
                    strRolesPara += "@岗位";
                    
                if(strRolesPara.indexOf('@')==0)
                    strRolesPara=strRolesPara.substr(1);

                var r = showModalDialog('../Organizations/chooseRoles.aspx?queryType=' + strRolesPara, '',
                    'dialogWidth=600;dialogHeight=300;center=yes;resizable=yes;scroll=yes;status=yes;');
                if (r == '' || typeof (r) == 'undefined' || typeof (r) == 'object') {
                    return;
                }
                else {
                    document.all['hRolesNew'].value = r;
                    document.all['ButtonSave'].click();
                }
            }

            function checkSubmit() {
            }


            function CheckRead(obj) {
                //return;
                if (!obj.checked) {
                    checkUnreadAll(obj, false);
                }
            }
            function checkUnreadAll(obj, bCheck) {
                var objTop=obj.parentNode.parentNode;
                if (objTop.nodeName.toUpperCase() == 'TD')
                    objTop = objTop.parentNode;
                var chks = objTop.getElementsByTagName("input");
                if (chks.length > 0) {
                    for (var i = 0; i < chks.length; i++) {
                        if(chks[i].type=='checkbox')
                            chks[i].checked = bCheck;
                    }
                }
            }

            function checkAll(obj, indexNum) {
                if (obj == null) {
                    obj = document.all['tableModuleRoles'];
                    if (indexNum <3) {
                        var objTop= obj.rows[0].getElementsByTagName("input");
                        var bcheck = objTop[indexNum].checked;
                        if (bcheck) {
                            objTop[0].checked = bcheck;
                        }
                        else if(indexNum==0){
                            CheckRead(objTop[0]);
                        }
                        var tableRows=obj.rows
                        for (var i = 1; i < tableRows.length; i++) {
                            
                            var chks = tableRows[i].getElementsByTagName("input");

                            if (chks.length > 0) {
                                chks[indexNum].checked = bcheck;
                                if (indexNum == 0) {
                                    CheckRead(chks[0]);
                                }
                                else {
                                    if(bcheck)
                                        chks[0].checked = bcheck;
                                }
                            }
                        }
                    }
                    else {
                        var objTop = obj.rows[0].getElementsByTagName("input");
                        var bcheck = objTop[indexNum].checked;
                        if (bcheck) {
                            objTop[0].checked = bcheck;
                        }
                        
                        var tableRows = obj.rows
                        for (var i = 1; i < tableRows.length; i++) {

                            var chks = tableRows[i].getElementsByTagName("input");

                            if (chks.length > 0) {
                                if (bcheck)
                                    chks[0].checked = bcheck;
                                for (var j = 3; j < chks.length; j++) {
                                    chks[j].checked = bcheck;
                                }
                            }
                        }
                        
//                        var objTop = obj.rows[0].getElementsByTagName("input");
//                        var bcheck = objTop[3].checked;
//                        
//                        for (var i = 1; i < obj.rows; i++) {
//                            var chks = obj.rows[i].getElementsByTagName("input");
//                            if (chks.length > 0) {
//                                if (bcheck) {
//                                    chks[0].checked = bcheck;
//                                }
//                                for (var j = 3; i < chks.length; j++) {
//                                    //if (chks[j].type = 'checkbox')
//                                    chks[j].checked = bcheck;  
//                                }
//                            }
//                        }
                    }
                }
                else {
                    var bcheck = obj.checked;
                    if (bcheck) {
                        var objP = null;
                        if (indexNum > 2)
                            objP=obj.parentNode.parentNode.parentNode;
                        else
                            objP = obj.parentNode.parentNode;
                        var chks = objP.getElementsByTagName("input");
                        if (chks.length > 0) {
                            chks[0].checked = true;
                        }
                    }
                }
                
            }
// ]]>
        </script>
</HEAD>
	<body topmargin="0" onload="return window_onload()">
	    <form id="Form1" method="post" runat="server" onsubmit="return checkSubmit()">	
	    <font size="3"><b>模块角色管理</b></font>
	            <fieldset style="display:<%=ParentShow%>"><legend style="cursor:hand;" onclick="expandParent(this);">上级模块描述 +>></legend>
	            <asp:Repeater id="RepeaterModuleParent" Runat="server" 
                    onitemdatabound="RepeaterModuleParent_ItemDataBound">
			        <HeaderTemplate>
			        <table id="tableParent" width="100%" style="background-color:White;display:none;" cellpadding="0" cellspacing="1" border="0">
			             <tr style="BACKGROUND-COLOR: white;font-weight:bold;">
						    <td nowrap align="left" style="BACKGROUND-COLOR: gainsboro" width="5%">
							    模块编号
						    </td>
						    <td nowrap style="BACKGROUND-COLOR: gainsboro"  width="5%">
							    模块名称<asp:Label ID="lblCountRows" Runat="server"></asp:Label>
						    </td>
						    <td style="BACKGROUND-COLOR: gainsboro" nowrap width="4%">角色
						    </td>
						    <td style="BACKGROUND-COLOR: gainsboro" nowrap width="5%">角色名称</td>
						    <td style="BACKGROUND-COLOR: gainsboro;color:Red;font-weight:bold" align="center" nowrap width="15%">只读,运行，分配</td>
						    <td style="BACKGROUND-COLOR: gainsboro">操作权限,按钮权限</td>
						    <td style="BACKGROUND-COLOR: gainsboro" nowrap width="5%">备注</td>
					    </tr>
			        </HeaderTemplate>
			        <ItemTemplate>
			        
				        <tr style="BACKGROUND-COLOR: whitesmoke;font-weight:normal">
				            <td nowrap="nowrap" align="left">
							    <asp:Label ID="lblSN" Runat="server"></asp:Label>
						    </td>
						    <td  nowrap="nowrap" style="BACKGROUND-COLOR: gainsboro">
							    <asp:Label ID="lblModuleName" Runat="server"></asp:Label>
						    </td>
						    <td nowrap="nowrap" align="center"><asp:Label ID="lblType" Runat="server"></asp:Label>
						    </td>
						    <td nowrap="nowrap" align="center"><asp:Label ID="lblRoleCName" Runat="server"></asp:Label></td>
						    <td nowrap="nowrap" align="center">
                                <asp:Label ID="literalRun" runat="server"></asp:Label></td>
						    <td>
						        <asp:Label ID="literalOpe" runat="server"></asp:Label></td>
						    <td nowrap="nowrap">
						        <asp:Label ID="lblDescr" Runat="server"></asp:Label>
						</tr>
				    </ItemTemplate>
				    <FooterTemplate>
				        <tr>
				            <td colspan="7">&nbsp;</td>
				        </tr>
				       </table>
				    </FooterTemplate>
				</asp:Repeater>
				</fieldset>
	            <br />
	            模块名称:<font color="blue"><b><%=EName %></b></font> 操作：
	            <input type="button" value="增加/去除角色>>>"   class=btn3_mouseout onmouseover="this.className='btn3_mouseover'"
                        onmouseout="this.className='btn3_mouseout'"
                        onmousedown="this.className='btn3_mousedown'"
                              onmouseup="this.className='btn3_mouseup'" onclick="selectRoles()" /><font color="red"> *小心操作</font><br /><!--dataListOrder-->
				<asp:Repeater id="dl" Runat="server" 
                    onitemdatabound="dl_ItemDataBound1">
			        <HeaderTemplate>
			        <table width="100%" id="tableModuleRoles" style="background-color:White" cellpadding="0" cellspacing="1" border="0">
			             <tr height="45" style="BACKGROUND-COLOR: white;font-weight:bold;"><!-- class="fixedHeader"-->
						    <td nowrap align="left" style="BACKGROUND-COLOR: gainsboro" width="5%">
							    模块编号
						    </td>
						    <td nowrap style="BACKGROUND-COLOR: gainsboro"  width="11%">
							    模块名称<asp:Label ID="lblCountRows" Runat="server"></asp:Label>
						    </td>
						    <td style="BACKGROUND-COLOR: gainsboro" nowrap width="4%">角色
						    </td>
						    <td style="BACKGROUND-COLOR: gainsboro" nowrap width="8%">角色名称</td>
						    <td style="BACKGROUND-COLOR: gainsboro;color:Red;font-weight:bold" align="center" nowrap width="15%">只读，运行，分配<br />
						        <asp:CheckBox runat="server" ID="chkRead" CssClass="checkbox" onclick="checkAll(null,0)" />，
						        <asp:CheckBox runat="server" ID="chkRun" CssClass="checkbox" onclick="checkAll(null,1)" />，
						        <asp:CheckBox runat="server" ID="chkRed" CssClass="checkbox" onclick="checkAll(null,2)" />
						        </td>
						    <td style="BACKGROUND-COLOR: gainsboro" nowrap="nowrap" align="center"><asp:Label ID="LabelOP" Runat="server">操作权限,按钮权限<br /></asp:Label>
						        <asp:CheckBox runat="server" ID="chkOP" CssClass="checkbox" onclick="checkAll(null,3)"/></td>
						    <td style="BACKGROUND-COLOR: gainsboro" nowrap width="5%"></td>
					    </tr>
			        </HeaderTemplate>
			        <ItemTemplate>
			            <tr style="BACKGROUND-COLOR: white;font-weight:normal">
				            <td nowrap="nowrap" align="left">
							    <asp:Label ID="lblSN" Runat="server"></asp:Label>
						    </td>
						    <td  nowrap="nowrap" style="BACKGROUND-COLOR: gainsboro">
							    <asp:Label ID="lblModuleName" Runat="server"></asp:Label>
						    </td>
						    <td nowrap="nowrap" align="center"><asp:Label ID="lblType" Runat="server"></asp:Label>
						    </td>
						    <td nowrap="nowrap" align="center"><asp:Label ID="lblRoleCName" Runat="server"></asp:Label></td>
						    <td nowrap="nowrap" align="center">
						        <asp:CheckBox runat="server" ID="chkRead" onclick="CheckRead(this)"/>，
						        <asp:CheckBox runat="server" ID="chkRun" onclick="checkAll(this,1)"/>，
						        <asp:CheckBox runat="server" ID="chkRed" onclick="checkAll(this,2)"/>
                                </td>
						    <td nowrap="nowrap" align="center">
						        <asp:CheckBoxList ID="chkOP" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" RepeatLayout="Table"></asp:CheckBoxList></td>
						    <td nowrap="nowrap"><asp:TextBox Rows="1" ID="txtSaveRow" runat="server" TextMode="MultiLine" Columns="4" Visible="False"></asp:TextBox>
						        <asp:Label ID="lblDescr" Runat="server"></asp:Label>
						</tr>
				    </ItemTemplate>
				    <AlternatingItemTemplate>
			            <tr style="background-color:gainsboro;">
				            <td nowrap="nowrap" align="left">
							    <asp:Label ID="lblSN" Runat="server"></asp:Label>
						    </td>
						    <td  nowrap="nowrap" style="BACKGROUND-COLOR: gainsboro">
							    <asp:Label ID="lblModuleName" Runat="server"></asp:Label>
						    </td>
						    <td nowrap="nowrap" align="center"><asp:Label ID="lblType" Runat="server"></asp:Label>
						    </td>
						    <td nowrap="nowrap" align="center"><asp:Label ID="lblRoleCName" Runat="server"></asp:Label></td>
						    <td nowrap="nowrap" align="center">
						        <asp:CheckBox runat="server" ID="chkRead" onclick="CheckRead(this)"/>，
						        <asp:CheckBox runat="server" ID="chkRun" onclick="checkAll(this,1)"/>，
						        <asp:CheckBox runat="server" ID="chkRed" onclick="checkAll(this,2)"/>
                                </td>
						    <td nowrap="nowrap" align="center" class="myNowrap">
						        <asp:CheckBoxList ID="chkOP" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" RepeatLayout="Table"></asp:CheckBoxList></td>
						    <td nowrap="nowrap"><asp:TextBox Rows="1" ID="txtSaveRow" runat="server" TextMode="MultiLine" Columns="4" Visible="False"></asp:TextBox>
						        <asp:Label ID="lblDescr" Runat="server"></asp:Label>
						</tr>
				    </AlternatingItemTemplate>
				    <FooterTemplate>
				        <tr>
				        <td colspan="7"> 
				        </td>
				        </tr>
				       </table>
				    </FooterTemplate>
				</asp:Repeater>
	
		<asp:Button id="ButtonSave" runat="server"
            Text="保存" class="btn3_mouseout" onmouseover="this.className='btn3_mouseover'"
                onmouseout="this.className='btn3_mouseout'"
                onmousedown="this.className='btn3_mousedown'"
                      onmouseup="this.className='btn3_mouseup'" 
            onclick="ButtonSave_Click" />
		        <input type="hidden" id="hRoles" runat="server" name="hRoles" value=""/>
		        <input type="hidden" id="hRolesNew" runat="server" name="hRolesNew" value=""/>
		   <asp:Label ID="lblMSG" runat="server"></asp:Label>
	</form>
	</body>
</HTML>
