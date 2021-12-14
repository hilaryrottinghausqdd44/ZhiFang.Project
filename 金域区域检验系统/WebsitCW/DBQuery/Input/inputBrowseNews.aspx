<%@ Page validateRequest="false" enableEventValidation="false" language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Input.inputBrowseNews" Codebehind="inputBrowseNews.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>‰Ø¿¿–≈œ¢</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<script id=clientEventHandlersJS language=javascript>
<!--

function Form1_onsubmit() {
	//document.all["TextBox1"].value=document.body.innerHTML;
	//alert(document.body.innerHTML);
	//alert(document.all["TextBox1"].value);
	return true;
}

//-->
</script>
</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<%if(doc!=null&&doc.DocumentElement!=null){
			Response.Write(doc.DocumentElement.FirstChild.InnerText);
			//Response.Write(doc.DocumentElement==null);
		}
				
			

		%>
		<form id="Form1" method="post" runat="server"  language=javascript onsubmit="return Form1_onsubmit()">
			<asp:TextBox id="TextBox1" runat="server" Width="0"></asp:TextBox>
			<!--<%Response.Write(Request.ServerVariables["Query_String"].ToString());%>-->
		</form>
	</body>
</HTML>
