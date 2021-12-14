<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Input.InputUploadFileDialog" Codebehind="InputUploadFileDialog.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>上传文件</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
	<body MS_POSITIONING="GridLayout" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<TABLE cellSpacing="0" cellPadding="0" align="center" border="0">
			<TBODY>
				<TR>
					<TD>
						<FIELDSET style="WIDTH: 308px; HEIGHT: 64px"><LEGEND>文件来源</LEGEND>
							<TABLE cellSpacing="0" cellPadding="0" border="0" style="WIDTH: 388px; HEIGHT: 56px">
								<TBODY>
									<TR>
										<TD width="7"></TD>
										<TD onclick="RadioClick('file')" align="right" width="34">上传:</TD>
										<TD width="15"><IMG src="../../News/news/manage/htmledit/ButtonImage/standard/File.gif"></TD>
										<TD colSpan="5">
											<Script Language="JavaScript">
												document.write('<iframe id=d_file frameborder=0 src="InputUploadFile.aspx" width="100%" height="52" scrolling=no></iframe>');
											</Script>
											<FONT face="宋体"></FONT>
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
					<TD align="right"><INPUT id="Ok" onclick="ok()" type="submit" value="  确定  ">&nbsp;&nbsp;<INPUT onclick="window.close();" type="button" value="  取消  "></TD>
				</TR>
				<TR>
					<TD align="right"><FONT face="宋体"></FONT></TD>
				</TR>
			</TBODY>
		</TABLE>
		<div id=divProcessing style="width:200px;height:30px;position:absolute;left:50px;top:20px;display:none">
			<table border=0 cellpadding=0 cellspacing=1 bgcolor="#000000" width="100%" height="100%"><tr><td bgcolor=#3A6EA5><marquee align="middle" behavior="alternate" scrollamount="5"><font color=#FFFFFF>...文件上传中...请等待...</font></marquee></td></tr></table>
		</div>
	</body>
</HTML>
