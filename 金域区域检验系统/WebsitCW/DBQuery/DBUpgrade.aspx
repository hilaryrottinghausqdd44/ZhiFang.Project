<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DBUpgrade" Codebehind="DBUpgrade.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DBUpgrade</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	

        <script language="javascript" type="text/javascript">
// <!CDATA[

            function window_onload() {

            }

            function ConfirmSubmit() {
                if (confirm("确定要升级吗？升级为新版本后将不能再恢复到旧版本，是这样吗？"))
                    return true;
                return false;
            }
// ]]>
        </script>
</HEAD>
	<body onload="return window_onload()">
		<form id="Form1" method="post" runat="server" onsubmit="javascript:return ConfirmSubmit();">
			<P>
				<asp:Button ID="ButtonSystemUpdate" runat="server" Text="程序升级" 
                    onclick="ButtonSystemUpdate_Click" />
&nbsp;<br />
                升级服务器地址<asp:TextBox ID="TextBoxUpdateUrl" runat="server" Width="410px">/DownLoadServerWebService/DownLoadService.asmx</asp:TextBox>
			&nbsp;<br />
                目前程序版本号<asp:TextBox ID="TextBoxVersion" runat="server" Width="410px">1.0</asp:TextBox>
			</P>
            <P>
				<input id="buttonDbUpate" type="button" value="数据库升级" onclick="Javascript:window.open('../ModuleManage/DBUpdater.aspx','_blank')" /></P>
            <P>
				<TEXTAREA style="WIDTH: 672px; HEIGHT: 208px" rows="13" cols="81">-------升级说明-------------
////内核升级(第二次)////////////////////////////////////////////////
描述：系统版本升级
时间：2009-04-16 至今
背景：为qms,oa等多套系统作手动在线升级程序

1,把OA或QMS升级包压缩(winRar格式)，保证解压到当前文件夹时仍是OA或QMS根目录
  文件包括*.aspx,*.asp,*.htm,*.js,*.dll
  排除 unrar.dll,sayeahapi.dll,源码文件等
  
2,把数据库升级文件.sql文件放入\OA***\ModuleManage\DbUpGrade目录中进行压缩
2+,把OA***.rar上传至http://dns/DownloadCenter/软件升级中心
3,先执行程序升级，再运行数据库升级
4,建议版本号手动修改为0.0,1.0~1.9,2.0~2.9,~.~...9.9,进行顺序升级
5,如果通过本地localhost方式访问,升程序将产生于\OA***_Upadate\目录中
////////////////////////////////////////////////////////////////////

////内核升级(第一次)////////////////////////////////////////////////
描述：数据共享前数据库升级
时间：2005-08-29 至 2006-01-01
背景：单表系统主要功能已经完成
源库：ConfigurationXml + 域 + 系统单表名 
		a:TablesConfig.xml 
		b:TablesData.xml

升级后：ConfigurationXml + 域 + 系统单表名 
			a:TablesConfig.xml 
	ConfigurationData + 域 + 系统编号（数据库名）
			b:TablesConfig.xml 
			c:TablesData.xml
////////////////////////////////////////////////////////////////////
				</TEXTAREA>
			</P>
			<P>
				<asp:Button id="buttDBUpgrade" runat="server" Text="执行早期共享升级动作" 
                    onclick="buttDBUpgrade_Click"></asp:Button></P>
			<P><FONT face="宋体"></FONT>&nbsp;</P>
		</form>
	</body>
</HTML>
