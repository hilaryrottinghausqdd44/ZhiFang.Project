<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.Documents.ShareAccess" Codebehind="ShareAccess.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ShareAccess</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style> table{ font-size: 12px; font-weight: normal; color: #000000; text-decoration: none;}
		</style>
		<script id="clientEventHandlersJS" language="javascript">
<!--

function window_onload() {
			<%if(AccessBit.Length!=0)
				{
				for(int i=0;i<AccessBit.Length-2;i++)
				{		
					if(AccessBit[i+2])
					{	%>
							document.all['checkbox' + <%=i%>].checked=<%=(AccessBit[i+2].ToString().ToLower())%>;
							//document.all['hAccess'].value=<%=i%>;
							var tdColor=document.all['tdd'];
							
						<%
					}
					else
					{
						%>
							document.all['checkbox' + <%=i%>].checked=<%=(AccessBit[i+2].ToString().ToLower())%>;
							document.all['checkbox'+ <%=i%>].style.borderTopStyle='solid';
							document.all['checkbox'+ <%=i%>].style.borderTopWidth=2;
							document.all['checkbox'+ <%=i%>].style.borderTopColor='red';
						<%
					}
				}
				}
				%>
}

//-->
function enableCheckBox(CheckBoxIndex){
				document.all['checkbox'+ CheckBoxIndex].checked=false;
				document.all['checkbox'+ CheckBoxIndex].style.borderTopStyle='none';
				document.all['checkbox'+ CheckBoxIndex].style.borderTopWidth=0;
				document.all['checkbox'+ CheckBoxIndex].style.borderTopColor='white';
			}
			
			function checkColor(CheckBoxIndex,AccessColor){
				var tdColor=document.all['tdd'];
				
				for(var i=0;i<4;i++)
				{
					if(document.all['checkbox'+ i].checked)
					{
						
						tdColor.style.backgroundColor=document.all['checkbox'+ i].title;
						
					}
				}
				
				
				if(document.all['checkbox'+ CheckBoxIndex].checked)
				{
					//集连前面的权限
					for(var i=0;i<=CheckBoxIndex;i++)
					{
						if(document.all['checkbox'+ i].disabled!=true)
						{
							document.all['checkbox'+ i].checked=true;
							document.all['checkbox'+ i].style.borderTopStyle='none';
							document.all['checkbox'+ i].style.borderTopWidth=0;
							document.all['checkbox'+ i].style.borderTopColor='white';
						}
					}
					
				}
				else
				{
					for(var i=3;i>0;i--)
					{	if(document.all['checkbox'+ i].checked)
							break;
						else
						{
							if(document.all['checkbox'+ i].disabled!=true)
							{
								document.all['checkbox'+ i].checked=false;
								document.all['checkbox'+ i].style.borderTopStyle='none';
								document.all['checkbox'+ i].style.borderTopWidth=0;
								document.all['checkbox'+ i].style.borderTopColor='white';
							}
						}
					}
					if(CheckBoxIndex==0)
					{
						document.all['checkbox'+ CheckBoxIndex].checked=true;
						tdColor.style.backgroundColor=document.all['checkbox'+ CheckBoxIndex].title;
						//document.all['hAccess'].value=CheckBoxIndex;
						
						//取消全部权限
						for(var i=3;i>0;i--)
						{	
							if(document.all['checkbox'+ i].disabled!=true)
							{
								document.all['checkbox'+ i].checked=false;
								document.all['checkbox'+ i].style.borderTopStyle='none';
								document.all['checkbox'+ i].style.borderTopWidth=0;
								document.all['checkbox'+ i].style.borderTopColor='white';
							}
						}
						alert('这个权限不能取消!\n\n如果要取消，请从角色中去除');
						
					}	
					else
					{
						//if(document.all['checkbox'+ i].disabled!=true)
						//{
							document.activeElement.style.borderTopStyle='dotted';
							document.activeElement.style.borderTopWidth=2;
							document.activeElement.style.borderTopColor='red';
						//}
						
					}
					var boolNextChecked=false;
					for(var i=CheckBoxIndex;i<4;i++)
					{
						if(document.all['checkbox'+ i].checked)
						{
							boolNextChecked=true;
						}
					}
					
					if(boolNextChecked&&CheckBoxIndex!=0)
					{
						if(document.all['checkbox'+ CheckBoxIndex].disabled!=true)
						{
							document.all['checkbox'+ CheckBoxIndex].style.borderTopStyle='solid';
							document.all['checkbox'+ CheckBoxIndex].style.borderTopWidth=2;
							document.all['checkbox'+ CheckBoxIndex].style.borderTopColor='red';
						}
					}
					else
					{
						if(document.all['checkbox'+ CheckBoxIndex].disabled!=true)
						{
							document.all['checkbox'+ CheckBoxIndex].style.borderTopStyle='none';
							document.all['checkbox'+ CheckBoxIndex].style.borderTopWidth=0;
							document.all['checkbox'+ CheckBoxIndex].style.borderTopColor='white';
						}
					}
						
				}
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" language="javascript" onload="return window_onload()">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 327px; POSITION: absolute; TOP: 8px; HEIGHT: 32px"
					height="32">
					<TR height="40">
						<TD style="HEIGHT: 58px" height="58">
							<TABLE id="Table3">
								<TR height="15">
									<TD noWrap><INPUT id="checkbox0" ondblclick="enableCheckBox(0)" title="darkGreen" type="checkbox"
											name="checkbox0">上传文件
									</TD>
								</TR>
							</TABLE>
						</TD>
						<TD style="HEIGHT: 58px" height="58">
							<TABLE id="Table4">
								<TR height="15">
									<TD noWrap><INPUT id="checkbox1" ondblclick="enableCheckBox(1)" title="#00ff00" type="checkbox" name="checkbox1">创建子文件夹
									</TD>
								</TR>
							</TABLE>
						</TD>
						<TD style="HEIGHT: 58px" height="58">
							<TABLE id="Table5" style="WIDTH: 80px; HEIGHT: 25px">
								<TR height="15">
									<TD noWrap><INPUT id="checkbox2" ondblclick="enableCheckBox(2)" title="gold" type="checkbox" name="checkbox2">重命名
									</TD>
								</TR>
							</TABLE>
						</TD>
						<TD style="HEIGHT: 58px" height="58">
							<TABLE id="Table6">
								<TR height="15">
									<TD noWrap><INPUT id="checkbox3" ondblclick="enableCheckBox(3)" title="yellow" type="checkbox" name="checkbox3">删除
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
