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
                if (confirm("ȷ��Ҫ����������Ϊ�°汾�󽫲����ٻָ����ɰ汾����������"))
                    return true;
                return false;
            }
// ]]>
        </script>
</HEAD>
	<body onload="return window_onload()">
		<form id="Form1" method="post" runat="server" onsubmit="javascript:return ConfirmSubmit();">
			<P>
				<asp:Button ID="ButtonSystemUpdate" runat="server" Text="��������" 
                    onclick="ButtonSystemUpdate_Click" />
&nbsp;<br />
                ������������ַ<asp:TextBox ID="TextBoxUpdateUrl" runat="server" Width="410px">/DownLoadServerWebService/DownLoadService.asmx</asp:TextBox>
			&nbsp;<br />
                Ŀǰ����汾��<asp:TextBox ID="TextBoxVersion" runat="server" Width="410px">1.0</asp:TextBox>
			</P>
            <P>
				<input id="buttonDbUpate" type="button" value="���ݿ�����" onclick="Javascript:window.open('../ModuleManage/DBUpdater.aspx','_blank')" /></P>
            <P>
				<TEXTAREA style="WIDTH: 672px; HEIGHT: 208px" rows="13" cols="81">-------����˵��-------------
////�ں�����(�ڶ���)////////////////////////////////////////////////
������ϵͳ�汾����
ʱ�䣺2009-04-16 ����
������Ϊqms,oa�ȶ���ϵͳ���ֶ�������������

1,��OA��QMS������ѹ��(winRar��ʽ)����֤��ѹ����ǰ�ļ���ʱ����OA��QMS��Ŀ¼
  �ļ�����*.aspx,*.asp,*.htm,*.js,*.dll
  �ų� unrar.dll,sayeahapi.dll,Դ���ļ���
  
2,�����ݿ������ļ�.sql�ļ�����\OA***\ModuleManage\DbUpGradeĿ¼�н���ѹ��
2+,��OA***.rar�ϴ���http://dns/DownloadCenter/�����������
3,��ִ�г������������������ݿ�����
4,����汾���ֶ��޸�Ϊ0.0,1.0~1.9,2.0~2.9,~.~...9.9,����˳������
5,���ͨ������localhost��ʽ����,�����򽫲�����\OA***_Upadate\Ŀ¼��
////////////////////////////////////////////////////////////////////

////�ں�����(��һ��)////////////////////////////////////////////////
���������ݹ���ǰ���ݿ�����
ʱ�䣺2005-08-29 �� 2006-01-01
����������ϵͳ��Ҫ�����Ѿ����
Դ�⣺ConfigurationXml + �� + ϵͳ������ 
		a:TablesConfig.xml 
		b:TablesData.xml

������ConfigurationXml + �� + ϵͳ������ 
			a:TablesConfig.xml 
	ConfigurationData + �� + ϵͳ��ţ����ݿ�����
			b:TablesConfig.xml 
			c:TablesData.xml
////////////////////////////////////////////////////////////////////
				</TEXTAREA>
			</P>
			<P>
				<asp:Button id="buttDBUpgrade" runat="server" Text="ִ�����ڹ�����������" 
                    onclick="buttDBUpgrade_Click"></asp:Button></P>
			<P><FONT face="����"></FONT>&nbsp;</P>
		</form>
	</body>
</HTML>
