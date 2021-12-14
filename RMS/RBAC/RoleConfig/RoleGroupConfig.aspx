<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleGroupConfig.aspx.cs" Inherits="OA.RBAC.RoleConfig.RoleGroupConfig" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>角色分类管理</title>
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function window_onload() {
            document.getElementById("TextBoxSearch").focus();
            document.getElementById("TextBoxSearch").select();
        }
        function AddNew() {
            document.getElementById("ButtonSave").value = "保存 (新增)";
            document.getElementById("tdAddModify").style.display = "";
            document.getElementById("TextBoxGroupName").value = "";
            document.getElementById("TextBoxGroupNo").value = "";
            document.getElementById("TextBoxGroupDesc").value = "";

            document.getElementById("LabelMsg").innerHTML = "";

            document.getElementById("hAction").value = "";
            document.getElementById("hRoleNo").value = "";
            //alert(document.getElementById("hAction").value);
        }
        function Modify(RoleNo,RoleName,RoleDesc) {
            document.getElementById("ButtonSave").value = "保存 (修改)";
            document.getElementById("tdAddModify").style.display = "";
            document.getElementById("TextBoxGroupName").value = RoleName;
            document.getElementById("TextBoxGroupNo").value = RoleNo;
            document.getElementById("TextBoxGroupDesc").value = RoleDesc;

            document.getElementById("hAction").value = "修改";
            document.getElementById("hRoleNo").value = RoleNo;
            document.getElementById("LabelMsg").innerHTML = "";
        }

        function Delete(RoleNo, RoleName) {
            if (confirm("确认要删除 [" + RoleName + "] 吗？？？")) {
                document.getElementById("hAction").value = "删除";
                document.getElementById("hRoleNo").value = RoleNo;
                form1.submit();
            }
            document.getElementById("LabelMsg").innerHTML = "";
        }

        var id = "";
        var status = 0;
        
        function beginDrag(i, txt) {
            id = i;
            status = 1;

            hover.style.top = document.body.scrollTop + event.clientY + 10;
            hover.style.left = document.body.scrollLeft + event.clientX;
            hover.innerHTML = txt;
            hover.style.display = "";
            document.getElementById("hAction").value = "";
            document.getElementById("hRoleNo").value = "";
        }

        function move() {
            if (status == 1) {
                hover.style.top = document.body.scrollTop + event.clientY + 10;
                hover.style.left = document.body.scrollLeft + event.clientX;
            }
        }

        function endDrag(i) {
            hover.style.display = "none";
            if (status == 1) {
                document.getElementById("hAction").value = "拖动";
                document.getElementById("hRoleNo").value = id + "," + i;
            }
            status = 0;
            if (id == i)
                return;
            else
                form1.submit();  
        }

        function stop() {
            hover.style.display = "none";
            status = 0;

        }
        var objClick = null;
        function onOver(obj) {
            if (objClick != obj)
                obj.bgColor = 'LemonChiffon'
        }
        function onOut(obj) {
            if (objClick != obj)
                obj.bgColor = ''
        }
        function onClickThis(obj) {
            if (objClick != null)
                objClick.bgColor = "";
            obj.bgColor = 'gold';
            objClick = obj
        }


// ]]>
    </script>
    <script for="TextBoxSearch" language="javascript" event="onselectstart">
          return true;
    </script>
</head>
<body onload="return window_onload()"  onmouseup="stop()" onmousemove="move()">

 <form id="form1" runat="server">
    <div>
    <table border="0" width="100%">
    <tr onselectstart="return true;">
        <td align="right" width="50%">
            <asp:TextBox ID="TextBoxSearch" runat="server" Width="100%"></asp:TextBox>
        </td>
        <td align="left" width="10%">&nbsp;<asp:ImageButton ID="ImageButton2"
                runat="server"  ImageUrl="../../images/icons/0023_b.gif" 
                onclick="ImageButton2_Click" /></td>
        <td align="right" nowrap width="30%"><p>
            <a href="#" onclick="Javascript:AddNew()"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../images/icons/0013_b.gif" />新增分类</a></p></td>
        <td width="10%"><p>&nbsp;</p></td>
    </tr>
    <tr>
        <td valign="top" style="width:70%" align="middle" colspan="3">
            <table border="1" width="100%" cellspacing="0" cellpadding="2" align="center" style="border-collapse: collapse;cursor:hand"  onselectstart="return false;">
                <tr bgcolor="#e0e0e0" style="font-weight:bold">
                    <td align="left" nowrap width="5%">
                        序号
                    </td>
                    <td align="left" nowrap>
                        分类名称
                    </td>
                    <td align="left" nowrap>
                        分类编号
                    </td>
                    <td align="left" nowrap>
                        分类描述
                    </td>
                    <td align="left" nowrap width="10%">
                        操作
                    </td>
                    
                </tr>
                <%for (int k = 0; k < Dt.Rows.Count; k++)
                  {%>
                <tr id="NM<%=Dt.Rows[k]["RoleGroupID"].ToString()%>" bgcolor="white" onmouseover="onOver(this)"
                     onmousedown="beginDrag('<%=Dt.Rows[k]["RoleGroupNo"]%>','<%=Dt.Rows[k]["RoleGroupName"]%>')"  
                     onmouseup="endDrag('<%=Dt.Rows[k]["RoleGroupNo"]%>')"
                    onmouseout="onOut(this)" onclick="onClickThis(this)">
                    <td align="left">
                        <%=Dt.Rows[k]["RoleGroupOrder"]%>
                    </td>
                    <td align="left">
                        <b><%=Dt.Rows[k]["RoleGroupName"]%></b>
                    </td>
                    <td>
                        <%=Dt.Rows[k]["RoleGroupNo"]%>
                    </td>
                    <td align="left">
                        <%=Dt.Rows[k]["RoleGroupDesc"].ToString().Replace("\n","</br>")%>
                    </td>
                    <td nowrap>
                        <a href="#" onclick="Modify('<%=Dt.Rows[k]["RoleGroupNo"]%>','<%=Dt.Rows[k]["RoleGroupName"]%>','<%=Dt.Rows[k]["RoleGroupDesc"].ToString().Replace("\r\n","\\r\\n")%>')">修改</a>&nbsp;&nbsp; 
                        <a href="#" onclick="Delete('<%=Dt.Rows[k]["RoleGroupNo"]%>','<%=Dt.Rows[k]["RoleGroupName"]%>')">删除</a>
                    </td>
                </tr>
                <%}%>
            </table>
            <p style="text-align:left">* 请拖动进行排序,如果编号相同，则不能排序
            <br />
            &nbsp;&nbsp;往上拖，搁前面
            <br />
            &nbsp;&nbsp;往下拖，搁后面
            </p>
            
            <div id="hover" style="border-right: #ffffcc 1px double; border-top: #ffffcc 1px double;
                    display: none; font-size: 16pt; font-weight:bold;border-left: #ffffcc 1px double; color: #ffffff;
                    border-bottom: #ffffcc 1px double; position: absolute; background-color: #0000ff">
                    
                </div>
        </td>
        <td id="tdAddModify" style="display:<%=displayStyle%>" valign="top">
            
             <table border="1" width="100%" cellspacing="0" cellpadding="2" align="center" style="border-collapse: collapse">
                    <tr>
                        <td align="left" nowrap  bgcolor="#e0e0e0" style="color:Red"><b>分类名称*</b></td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="TextBoxGroupName" runat="server" Width="65"></asp:TextBox>
                        </td>
                        <td align="left" nowrap>分类名称</td>
                    </tr>
                    <tr>
                        <td align="left" nowrap><b>分类编号</b></td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="TextBoxGroupNo" runat="server" Width="65"></asp:TextBox>
                        </td>
                        <td align="left" nowrap>要求各编号唯一,有编码规则<br />如不输入，则自动产生</td>
                    </tr>
                    <tr>
                        <td align="left" nowrap>描述</td>
                        <td align="left" nowrap colspan="2">
                            <asp:TextBox ID="TextBoxGroupDesc" runat="server" Width="98%" Rows="6" 
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="ButtonSave" runat="server" Text="保存" 
                                onclick="ButtonSave_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="LabelMsg" runat="server" ForeColor="#FF3300"></asp:Label></td>
                    </tr>
                </table>
        </td>
    </tr>
    </table>
    
    <input type=hidden runat="server" id="hAction" />
    <input type="hidden" runat=server id="hRoleNo" />
    </div>
    </form>
</body>
</html>
