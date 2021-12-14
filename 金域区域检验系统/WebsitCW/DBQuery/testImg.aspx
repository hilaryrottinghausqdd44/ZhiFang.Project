<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.testImg" Codebehind="testImg.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>testImg</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	

        <script language="javascript" type="text/javascript">
// <!CDATA[

            function window_onload() {
                alert('a1b2c3'.substr(0, 2));
                alert(Date('2003-10-10'));
            }

// ]]>
        </script>
</HEAD>
	<body onload="return window_onload()">
		<%//Response.Write("<br>时间：主页面" + DateTime.Now.ToLocalTime());%>
		<form id="Form1" method="post" runat="server">
			<P>
				<img alt="" src="../Images/icons/0000_a.gif" 
                    style="width: 32px; height: 32px" /><IMG src="images/icons/0001_a.gif"> <IMG src="images/icons/0014_a.gif"> <IMG src="images/icons/0001_b.gif">
				<IMG src="images/icons/0002_a.gif"> <IMG src="images/icons/0002_b.gif"> <IMG src="images/icons/0003_a.gif">
				<IMG src="images/icons/0003_b.gif"> <IMG src="images/icons/0004_a.gif"> <IMG src="images/icons/0004_b.gif">
				<IMG src="images/icons/0005_a.gif"> <IMG src="images/icons/0005_b.gif"> <IMG src="images/icons/0006_a.gif">
				<IMG src="images/icons/0006_b.gif"> <IMG src="images/icons/0007_a.gif"> <IMG src="images/icons/0007_b.gif">
				<IMG src="images/icons/0008_a.gif"> <IMG src="images/icons/0009_a.gif"> <IMG src="images/icons/0009_b.gif">
				<IMG src="images/icons/0010_a.gif"> <IMG src="images/icons/0010_b.gif"> <IMG src="images/icons/0011_a.gif">
				<IMG src="images/icons/0011_b.gif"> <IMG src="images/icons/0012_a.gif"> <IMG src="images/icons/0012_b.gif">
				<IMG src="images/icons/0013_a.gif"> <IMG src="images/icons/0013_b.gif"></P>
			<P>&nbsp;
				<asp:TextBox id="TextBox1" runat="server" Width="368px" Height="288px"></asp:TextBox><TEXTAREA style="WIDTH: 400px; HEIGHT: 286px" rows="18" cols="47">
				</TEXTAREA></P>
			<P>
				<asp:CheckBox id="CheckBox1" runat="server" Text="abc"></asp:CheckBox>
				<asp:TextBox id="TextBox2" runat="server"></asp:TextBox>
				<asp:Button id="Button1" runat="server" Text="Button" onclick="Button1_Click"></asp:Button><FONT face="宋体" color="#EBEBEB">AASDF&nbsp;&nbsp; 
                汉字</FONT><img alt="" src="../Images/icons/0000_a.gif" 
                    style="width: 32px; height: 32px" /><img src="../Images/icons/0000_a.gif" 
                    style="width: 32px; height: 32px" /></P>
		    <p>
                汉字<asp:TextBox ID="TextBox3" runat="server" 
                    ontextchanged="TextBox3_TextChanged"></asp:TextBox>
                转为拼音<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            &nbsp;&nbsp; readonly
                <input id="Text1" type="text" readonly/><textarea id="TextArea1" cols="20" name="S1" 
                    rows="2" readonly="readonly">a
                    b
                    c
                    d
                    s
                    d
                    d
                    d
                    </textarea><select id="Select1" name="D1" readonly>
                    <option style="background-color:#fefefe">abc</option>
                </select>disabled
                <input id="Text2" type="text" disabled/><textarea id="TextArea2" cols="20" name="S2" 
                    rows="2" disabled></textarea><select id="Select2" name="D2" disabled>
                    <option style="background-color:Transparent"> a b c</option>
                </select>&nbsp; readonly and disabled
                <input id="Text3" type="text" readonly disabled/><textarea id="TextArea3" cols="20" name="S3" 
                    rows="2" readonly disabled></textarea><select id="Select3" name="D3" readonly disabled>
                    <option style="background-color:White">abc</option>
                </select></p>
                <select id="Select4" name="D3" readonly disabled>
                    <option style="background-color:skyblue">abc</option>
                </select></p>
                <select id="Select5" name="D3" readonly disabled>
                    <option style="background-color:gray">abc</option>
                </select></p>
            <iframe name="frmStuffPhoto" id="frmStuffPhoto" border="0"
                style="BORDER:solid 1px #EFEFEF; width: 35%;" scrolling="no" height="132" 
                src="http://g.cn" 
                frameborder="0"></iframe>
		</form>
	</body>
</HTML>
