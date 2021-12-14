<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Themes.Mode" Codebehind="Mode.aspx.cs" %>

<html>
<head>
    <title>权限操作管理 添加/修改/删除</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		function ChooseColor()
		{
			<%if(ModeId!=""&&ModeId!=null)%>
				var Eid=<%=ModeId%>
			if('<%=ModeId%>'==null&&'<%=ModeId%>'=='')
			{
				r = window.showModalDialog ('ModeColor.aspx','','dialogWidth=20;dialogHeight=14;resizable=no;scroll=no;status=no');
			}
			else
			{
				r = window.showModalDialog ('ModeColor.aspx?Id='+'<%=ModeId%>','','dialogWidth=20;dialogHeight=14;resizable=no;scroll=no;status=no');
			}
			if(typeof(r)!='undefined')
				Mode1.OperateColor.value=r;
		}
		
    </script>

</head>
<body language="javascript" bottommargin="0" background="../../Images/vdisk/images/vdisk-bg.gif"
    bgcolor="#f0f0f0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="Mode1" name="Mode1" method="post" runat="server">
    <table border="0" width="321" style="width: 321px; height: 163px">
        <tr height="30">
            <td width="100" nowrap>
                &nbsp;<img src="../../images/icons/0019_a.gif" align="absBottom">&nbsp;
            </td>
            <td>
                <font color="highlight" size="2"><b>添加/修改操作</b></font>
            </td>
            <td align="right">
            </td>
        </tr>
        <tr>
            <td width="100" height="25">
                <p align="right">
                    <b><font size="2">标识编号</font></b></p>
            </td>
            <td width="183" colspan="2" height="25">
                <%if (DT.Rows.Count != 0)
                  {%><input id="SN" type="text" name="SN" style="width: 176px; height: 22px" size="24"
                    value="<%=DT.Rows[0]["SN"].ToString()%>">
                <%}
                  else
                  {%>
                <input id="SN" type="text" name="SN" style="width: 176px; height: 22px" size="24"><%}%>
            </td>
        </tr>
        <tr>
            <td width="65">
                <p align="right">
                    <b><font size="2">名称</font></b></p>
            </td>
            <td width="183" colspan="2">
                <%if (DT.Rows.Count != 0)
                  {%><input id="CName" style="width: 175px; height: 22px" type="text" size="23" name="CName"
                    value="<%=DT.Rows[0]["CName"].ToString()%>">
                <%}
                  else
                  {%>
                <input id="CName" type="text" name="CName" style="width: 176px; height: 22px" size="24"><%}%>
            </td>
        </tr>
        <tr>
            <td width="65">
                <p align="right">
                    <font size="2"><b>颜色</b></font></p>
            </td>
            <td width="94">
                <%if (DT.Rows.Count != 0)
                  {%><input id="OperateColor" style="width: 140px; height: 22px" type="text" size="23"
                    name="OperateColor" value="<%=DT.Rows[0]["OperateColor"].ToString()%>">
                <%}
                  else
                  {%>
                <input id="OperateColor" style="width: 140px; height: 22px" type="text" size="23"
                    name="OperateColor"><%}%>
            </td>
            <td width="83">
                <input style="width: 61px; height: 25px" onclick="ChooseColor();" type="button" value="选择">
            </td>
        </tr>
        <tr>
            <td width="65">
                <p align="right">
                    <font size="2"><b>描述</b></font></p>
            </td>
            <td width="94">
                <%if (DT.Rows.Count != 0)
                  {%><input id="Descr" style="width: 175px; height: 22px" type="text" size="23" name="Descr"
                    value="<%=DT.Rows[0]["Descr"].ToString()%>">
                <%}
                  else
                  {%>
                <input id="Descr" style="width: 175px; height: 22px" type="text" size="23" name="Descr">
                <%}%>
            </td>
        </tr>
        <tr>
            <td width="254" colspan="3">
                <p align="center">
                    <asp:Button ID="Button1" Text="添加/修改" runat="server" OnClick="Button1_Click"></asp:Button>&nbsp;&nbsp;&nbsp;<input
                        onclick="window.close();" type="button" value="取消"></p>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
