<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Themes.OperateMode" Codebehind="OperateMode.aspx.cs" %>
<HTML>
	<HEAD>
		<title>PersonList</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
		<meta name="ProgId" content="FrontPage.Editor.Document">
	</HEAD>
	<body>
		<!-- Inherits="IntelligentOffice.organization.PersonList" -->
		<table width="427" bgcolor="#ffffff" height="186">
			<tr bgColor="lightgrey" height="40">
				<td colSpan="4" width="419" height="36"><img border="0" src="../../images/icons/0019_a.gif" width="32" height="32">操作按钮管理</td>
			</tr>
			<tr>
				<td align="right" bgColor="#f0f0f0" width="419" colspan="4" height="27">
					<p align="left"><b><A href="Mode.aspx">添加</A> (toolBar)</b></p>
				</td>
			</tr>
			<tr>
				<td align="center" width="94" bgColor="#f0f0f0" height="18">
					<font color="#cc3300" size="2"><b>标识编号</b></font></td>
				<td align="center" width="55" bgColor="#f0f0f0" height="18">
					<p align="center"><font color="#cc3300" size="2"><b>名称</b></font></p>
				</td>
				<td align="center" width="39" bgColor="#f0f0f0" height="18">
					<font color="#cc3300" size="2"><b>颜色</b></font></td>
				<td align="center" width="226" bgColor="#f0f0f0" valign="top" height="18">
					<p align="center"><font color="#cc3300" size="2"><b>修改与删除</b></font></p>
				</td>
			</tr>
			<tr>
				<td align="center" width="94" bgcolor="#f0f0f0" height="18">1</td>
				<td align="center" width="55" bgcolor="#f0f0f0" height="18"><font size="2">只读</font></td>
				<td align="center" width="39" bgcolor="#f0f0f0" height="18">
				</td>
				<td align="center" width="226" bgcolor="#f0f0f0" valign="top" height="18"><font size="2"><a href="http://">修改</a>&nbsp;
						<a href="http://">删除</a></font></td>
			</tr>
			<tr>
				<td align="center" width="94" bgcolor="#f0f0f0" height="18">2</td>
				<td align="center" width="55" bgcolor="#f0f0f0" height="18"><font size="2">运行</font></td>
				<td align="center" width="39" bgcolor="#f0f0f0" height="18">
				</td>
				<td align="center" width="226" bgcolor="#f0f0f0" valign="top" height="18"><font size="2"><a href="http://">修改</a>&nbsp;
						<a href="http://">删除</a></font></td>
			</tr>
			<tr>
				<td align="center" width="94" bgcolor="#f0f0f0" height="18">3</td>
				<td align="center" width="55" bgcolor="#f0f0f0" height="18"><font size="2">修改</font></td>
				<td align="center" width="39" bgcolor="#f0f0f0" height="18">
				</td>
				<td align="center" width="226" bgcolor="#f0f0f0" valign="top" height="18"><font size="2"><a href="http://">修改</a>&nbsp;
						<a href="http://">删除</a></font></td>
			</tr>
			<tr>
				<td align="center" width="94" bgcolor="#f0f0f0" height="1">4</td>
				<td align="center" width="55" bgcolor="#f0f0f0" height="1"><font size="2">删除</font></td>
				<td align="center" width="39" bgcolor="#f0f0f0" height="1">
				</td>
				<td align="center" width="226" bgcolor="#f0f0f0" valign="top" height="1"><font size="2"><a href="http://">修改</a>&nbsp;
						<a href="http://">删除</a></font></td>
			</tr>
		</table>
		&nbsp;
		<script language="javascript">
		function RoleEdit(id)
		{
			 window.open ('RoleEdit.aspx?id=' + id,'RoleEdit','width=500,height=400,scrollbars=0,top=170,left=200');
		}
		</script>
	</body>
</HTML>
