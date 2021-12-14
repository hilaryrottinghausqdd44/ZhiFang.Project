<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.library.desktop.toolbox" Codebehind="toolbox.aspx.cs" %>

<%
    string ID = System.Guid.NewGuid().ToString().Replace("-", "");
%>

<script language>
	function newEmail()
	{
		window.open("/email/MailMessage.aspx",'','width=650,height=480,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );
	}
	function NewEvent()
	{
		window.open ('/calendar/EditEvent.aspx','','width=600,height=450,status=no,toolbar=no,scrollbars=no');
	}
	function newtask()
	{
		window.open('/project/PersonalTaskView.aspx?TaskID=','PersonalTaskView','Width=485,Height=325,scroll=no,status=no');
	}
	function StartTSS()
	{
		window.open ('/workflow/run/OpenMessage.aspx?TemplateID=23682f91-555d-4965-85ae-9597947b6df3','_blank','width=620,height=470,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );
	}
	function GoSMS()
	{
		location = '/email/Default.aspx?Node=1.0&Url=/sms/SMSList.aspx';
	}
	function GoMyDoc()
	{
		location = '/collabration/Default.aspx?Node=2.0&Url=/vdisk/FileList.aspx?fid=2';
	}
</script>

<table border="0" width="95%" cellspacing="0" align="center">
    <tr>
        <td class="DESKTOPITEM">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <font color="Teal"><b>工具箱</b></font>
                    </td>
                    <td align="right" nowrap>
                        <a style="cursor: hand">
                            <img id="imgOn" src="/images/hidden-on.gif" border="0" align="absbottom"></a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr style="height: 1px" bgcolor="Teal">
        <td>
        </td>
    </tr>
    <tr style="height: 5px">
        <td>
        </td>
    </tr>
    <tr id="trContent_">
        <td valign="top">
            <table width="90%" height="100" border="1" style="border-collapse: collapse" align="center"
                bordercolor="silver">
                <tr>
                    <td>
                        <table width="100%" height="100" cellpadding="5">
                            <tr>
                                <!--
					<td align="center" nowrap style="cursor:hand" onmouseover="this.bgColor='#eee8aa'" onmouseout="this.bgColor=''">
						<img src="/images/icons/0019_a.gif" border="0"><br>
						个人地址簿
					</td>
					//-->
                                <td align="center" nowrap style="cursor: hand" onmouseover="this.bgColor='#eee8aa'"
                                    onmouseout="this.bgColor=''" onclick="GoMyDoc()">
                                    <img src="/images/icons/0059_a.gif" border="0"><br>
                                    我的文档
                                </td>
                                <td align="center" nowrap style="cursor: hand" onmouseover="this.bgColor='#eee8aa'"
                                    onmouseout="this.bgColor=''" onclick="window.open('/organization/AddPerson.aspx?id=<%=CurrentEmployee.EmplID%>','', 'width=600px,height=460px,resizable=no,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-430)/2 );">
                                    <img src="/images/icons/0042_a.gif" border="0"><br>
                                    我的资料
                                </td>
                                <td align="center" nowrap style="cursor: hand" onmouseover="this.bgColor='#eee8aa'"
                                    onmouseout="this.bgColor=''" onclick="window.open ('/library/ChangeUserPwd.aspx','','width=300,height=250,resizable=no');">
                                    <img src="/images/icons/0033_a.gif" border="0"><br>
                                    修改密码
                                </td>
                                <td align="center" nowrap style="cursor: hand" onmouseover="this.bgColor='#eee8aa'"
                                    onmouseout="this.bgColor=''" onclick="location='/desktop'">
                                    <img src="/images/icons/0045_a.gif" border="0"><br>
                                    桌面定制
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
