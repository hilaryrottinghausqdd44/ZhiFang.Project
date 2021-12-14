<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Style.UploadStyleDialog" Codebehind="UploadStyleDialog.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UploadStyleDialog</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function ok()
		{
			if(d_file.Form1.FileUpload.value!=""&&d_file.Form1.FileUpload.value.indexOf("\\")>0)
			{
				divProcessing.style.display="";
				d_file.Form1.submit();
			}
			//Ok.disabled=true;
		}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" align="center" border="0">
				<TBODY>
					<TR>
						<TD>
							<FIELDSET style="WIDTH: 308px; HEIGHT: 90px"><LEGEND>文件来源</LEGEND>
								<TABLE cellSpacing="0" cellPadding="0" border="0" style="WIDTH: 388px; HEIGHT: 56px">
									<TBODY>
										<TR>
											<TD width="7"></TD>
											<TD onclick="RadioClick('file')" align="right" width="34" nowrap>上传:</TD>
											<TD width="15"></TD>
											<TD colSpan="5">
												<iframe id="d_file" src="UploadStyle.aspx?Database=<%=dataBase%>" frameborder="0" width="100%" height="70" scrolling=no></iframe>
												
											</TD>
											<TD width="7"></TD>
										</TR>
										<TR>
											<TD colSpan="9" height="5"><FONT face="宋体"></FONT></TD>
										</TR>
									</TBODY>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
					<TR>
						<TD height="5"></TD>
					</TR>
					<TR>
						<TD align="right"><INPUT id="Ok" onclick="ok()" type="button" value="  确定  ">&nbsp;&nbsp;<INPUT onclick="window.close();" type="button" value="  取消  "></TD>
					</TR>
					<TR>
						<TD align="right"><FONT face="宋体"></FONT></TD>
					</TR>
				</TBODY>
			</TABLE>
			<div id="divProcessing" style="DISPLAY:none;LEFT:50px;WIDTH:200px;POSITION:absolute;TOP:20px;HEIGHT:30px">
				<table border="0" cellpadding="0" cellspacing="1" bgcolor="#000000" width="100%" height="100%">
					<tr>
						<td bgcolor="#3a6ea5"><marquee align="middle" behavior="alternate" scrollamount="5"><font color="#ffffff">...文件上传中...请等待...</font></marquee>
						</td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>
