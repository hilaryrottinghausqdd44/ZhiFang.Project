<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Themes.ModeColor" Codebehind="ModeColor.aspx.cs" %>
<HTML>
	<HEAD>
		<title>红色</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		t=new Array;
		o=new Array;
		d=new Array;
		function hex(a,c)
		{
			 t[a]=Math.floor(c/16);
			 o[a]=c%16;
			switch (t[a])
			{
				case 10:
					t[a]='A';
					break;
				case 11:
					t[a]='B';
					break;
				case 12:
					t[a]='C';
					break;
				case 13:
					t[a]='D';
					break;
				case 14:
					t[a]='E';
					break;
				case 15:
					t[a]='F';
					break;
				default:
					break;
			}
			switch (o[a])
			{
				case 10:
					o[a]='A';
					break;
				case 11:
					o[a]='B';
					break;
				case 12:
					o[a]='C';
					break;
				case 13:
					o[a]='D';
					break;
				case 14:
					o[a]='E';
					break;
				case 15:
					o[a]='F';
					break;
				default:
					break;
			}
		}
		</script>
	</HEAD>
	<body background="../../Images/vdisk/images/vdisk-bg.gif">
		<form name="form1" id="form1" method="post" runat="server">
			<table border="0" height="142">
				<tr>
					<td width="74" align="right">红色</td>
					<td width="59"><font size="2"><input id="redColor" value="25"  onchange="ChangeColor();" name="redColor" size="6" type="text">&nbsp;</font></td>
					<TD width="177" bgColor="#ff9933"></TD>
				</tr>
				<tr>
					<td width="74" align="right">绿色</td>
					<td width="59"><font size="2"><input id="greenColor" value="211" onchange="ChangeColor();" name="greenColor" size="4" type="text"></font></td>
					<TD width="177" bgColor="#00ff00"></TD>
				</tr>
				<tr>
					<td width="74" align="right" height="27">蓝色</td>
					<td width="59" height="27"><font size="2"><input id="buleColor" value="111" onchange="ChangeColor();"  name="buleColor" size="6" type="text"></font></td>
					<TD width="177" bgColor="#0000cc" height="27"></TD>
				</tr>
				
				<tr>
					<td width="74" align="right">返回颜色</td>
					<td width="59"><font size="2">
					<%if(Dt.Rows.Count!=0)
					{	
					%>
					<input id="returnColor" name="returnColor" type="text" size="4" value="<%=Dt.Rows[0]["OperateColor"].ToString()%>"><%}else{%>
					<input id="returnColor" value="" name="returnColor" type="text" size="4" ><%}%>
					</font></td>
					<TD width="177" bgColor="#00ffcc"></TD>
				</tr>
				<tr>
					<td width="130" align="right" colspan="3">
						<P align="center"><FONT size="2"><INPUT type="button" value="确定" onclick="window.returnValue=returnColor.value;window.close();"></FONT></P>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">		
				function ChangeColor()
				{	
					
					hex(1,form1.redColor.value)
					hex(2,form1.greenColor.value)
					hex(3,form1.buleColor.value)
					form1.returnColor.value="#"+t[1]+o[1]+t[2]+o[2]+t[3]+o[3]
				}
				</script>
	</body>
</HTML>
