<%@ Page Language="c#" CodeBehind="Module.aspx.cs" AutoEventWireup="True" Inherits="OA.RBAC.Modules.Module" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ģ�������ҳ��</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript" id="clientEventHandlersJS">
<!--

function window_onload() {
	if('<%=NeedRefresh.ToString().ToLower()%>'=='true')
	{
		//parent.frames["left"].location.reload();
		parent.frames["left"].location="ModuleList.aspx?Refresh=true&selectedIndex=<%=thisModuleID%>";
	}
}
function ChooseTemplate()
{
	r = window.showModalDialog ('../../RBAC/Roles/ChooseButtonTheme.aspx','','dialogWidth=25;dialogHeight=20;resizable=no;scroll=no;status=no');
	if(r != ''&& typeof(r)!='undefined')
	{
		document.all['TempName'].value=r;
		frames["frmButtons"].location="ModuleButtons.aspx?buttTempName=" + r;
	}
}
function IsValidate()
{
	
	var temname=Form1.CName.value;
	if(temname.length==0)
	{
		alert('ģ��������Ϊ�գ�');
		return false;
	}
	if(temname.indexOf('\'')>-1)
	{
		alert('ģ�������ܰ���������!');
		return false;
	}
	
	var allButtons="";
	var iButtons=parseInt(document.frames["frmButtons"].document.all["hButtons"].value);
	var frmDocument=document.frames["frmButtons"].document;
	for(var i=0;i<iButtons;i++)
	{
		allButtons +=frmDocument.all["butt" + i].value + "," + frmDocument.all["butt" + i].title + ";";
	}
	if(allButtons!="")
		allButtons=allButtons.substring(0,allButtons.length-1);
	Form1.hButtons.value=allButtons;
	//	
}

function selectImage()
{
	r=window.showModalDialog('selectModuleImage.aspx','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:auto;status:no');
	if (r == '' || typeof(r) == 'undefined')
	{
		return;
	}
	else
	{
		document.all("ModuleImage").src="../../images/icons/" + r;
		document.all("selectImg").title=r;
		document.all("imgPickPic").value= r;
		//document.all("buttPickPic").title=r;
		
	}
}

function ChooseButtons(TemName)
{
	r = window.showModalDialog ('../Themes/ChooseButtContainer.aspx?TemName='+TemName,'','dialogWidth=45;dialogHeight=30;resizable=no;scroll=no;status=no');
	if(r != ''&& typeof(r)!='undefined')
	{
		var allParam=r.split("|");
		var operateIds=allParam[0];
		var operateNames=allParam[1];
		var operateID=operateIds.split(",");
		var operateName=operateNames.split(",");
		
		var allButtons="";
		var iButtons=parseInt(document.frames["frmButtons"].document.all["hButtons"].value);
		var frmDocument=document.frames["frmButtons"].document;
		
		var bExist=-1;
		for(var iCount=0;iCount<operateID.length;iCount++)
		{
			bExist=-1;
			for(var i=0;i<iButtons;i++)
			{
				if(operateID[iCount]==frmDocument.all["butt" + i].title)
				{
					bExist=i;
					break;
				}
			}
			if(bExist!=-1)
			{
				allButtons +=frmDocument.all["butt" + bExist].value + "," + frmDocument.all["butt" + bExist].title + ";";
			}
			else
			{
				allButtons +=operateName[iCount] + "," + operateID[iCount] + ";";
			}
		}
		
		
		if(allButtons!="")
			allButtons=allButtons.substring(0,allButtons.length-1);
			
		if(allButtons==",")
			allButtons="";
		Form1.hButtons.value=allButtons;
		Form1.hOperate.value="���İ�ť";
		
		
		//if(document.all['TempName'].value=="")
		//	document.all['TempName'].value="~" + '<=thisModuleID>';
		Form1.submit();
	}
}
function selectModule(objType)
{
	switch(objType)
	{
		case "News":
			var sUrl="../../news/browse/CategoryNews.aspx";
			var sPara="Catagory=" + document.all["CName"].value;
			document.all["URL"].value=sUrl;
			document.all["Para"].value=sPara;
			
			break;
		case "NewsID":
			var sUrl="../../news/browse/homepage.aspx";
			r = window.showModalDialog ('../../PopupSelectDialog.aspx?SystemModules/SelectNewsPage.aspx?NewsID=��ҳ&CloseWindow=true','','dialogWidth=45;dialogHeight=130;resizable=no;scroll=no;status=no');
			if(r != ''&& typeof(r)!='undefined')
			{
				document.all["URL"].value=sUrl;
				rArray=r.split(",");
				if(rArray.length==1)
				{
					alert("�����������ȷ");
					return;
				}
				document.all["Para"].value='ID=' + rArray[0];
				document.all["Descr"].value= "WindowSize=Max ���Ե��������С";
			}
			
			break;
		case "OneTable":
			var sUrl="../../dbquery/default.aspx";
			document.all["URL"].value=sUrl;
//			r = window.showModalDialog ('../../PopupSelectDialog.aspx?DBQuery/DBSettings/?ReturnValueDB=true','','dialogWidth=145;dialogHeight=130;resizable=no;scroll=no;status=no');
//			if(r != ''&& typeof(r)!='undefined')
//			{
//				document.all["URL"].value=sUrl;
//				rArray=r.split(",");
//				if(rArray.length==1)
//				{
//					alert("�����������ȷ");
//					return;
//				}
//				var sPara="db=" + rArray[0] + "&name=" + rArray[1];//db=rstDB&name=�����Ǽ�
//				document.all["Para"].value=sPara;
//				document.all["Descr"].value= "WindowSize=Max ���Ե��������С";
//			}
			break;
		case "Analysis":
			break;
		case "Others":
			break;
		case "OneTableNoFrame":
			var sUrl="../../Main/DesktopContent.aspx";
			document.all["URL"].value=sUrl;
//			r = window.showModalDialog ('../../PopupSelectDialog.aspx?DBQuery/DBSettings/?ReturnValueDB=true','','dialogWidth=145;dialogHeight=130;resizable=no;scroll=no;status=no');
//			if(r != ''&& typeof(r)!='undefined')
//			{
//				document.all["URL"].value=sUrl;
//				rArray=r.split(",");
//				if(rArray.length==1)
//				{
//					alert("�����������ȷ");
//					return;
//				}
//				var sPara="db=" + rArray[0] + "&name=" + rArray[1];//db=rstDB&name=�����Ǽ�
//				document.all["Para"].value=sPara;
//				document.all["Descr"].value= "WindowSize=Max ���Ե��������С";
//			}
			break;
	}
}
//-->
    </script>

</head>
<body language="javascript" bgcolor="#f0f0f0" leftmargin="0" topmargin="0" onload="return window_onload()"
    ms_positioning="GridLayout">
    <form id="Form1" name="Form1" onsubmit="return IsValidate()" method="post" runat="server">
    <table style="font-size: 10pt; z-index: 101" height="276" cellspacing="1" cellpadding="0"
        width="503" bgcolor="#e0e0e0" border="0">
        <tr bgcolor="lightgrey" height="40">
            <td align="right">
                <img src="../../images/icons/0028_a.gif">
            </td>
            <td colspan="3" height="18">
                <b><font size="4">ģ��ע��</font>&nbsp;<input id="ButtonWebConfig" type="button" value="��վ����" 
                onclick="Javascript:window.open('../../systemModules/WebConfig.aspx')" /></b>
            </td>
        </tr>
        <tr>
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25">
                ģ����
            </td>
            <td style="width: 106px" width="106" height="25">
                <asp:TextBox ID="ModuleID" runat="server" Enabled="False" CssClass="SingleBorder"
                    BackColor="#F0F0F0"></asp:TextBox>
            </td>
            <td style="width: 113px" nowrap align="right" width="113" bgcolor="#f0f0f0" height="25">
                <asp:CheckBox ID="Owner" runat="server" Checked="True"></asp:CheckBox>
            </td>
            <td bgcolor="#f0f0f0" height="25">
                ϵͳ����
            </td>
        </tr>
        <tr bgcolor="white">
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25">
                ģ����������
            </td>
            <td style="width: 106px" width="106" height="25">
                <asp:TextBox ID="CName" runat="server"></asp:TextBox>
            </td>
            <td style="width: 113px" nowrap align="right" width="113" bgcolor="#f0f0f0" height="25">
                <asp:CheckBox ID="chkTopModule" runat="server" Text=" "></asp:CheckBox>
            </td>
            <td bgcolor="#f0f0f0" height="25">
                ��������ģ��
            </td>
        </tr>
        <tr>
            <td style="width: 98px" nowrap align="right" width="98" bgcolor="#f0f0f0" height="25">
                ģ��Ӣ������
            </td>
            <td style="width: 106px" width="106" height="25">
                <asp:TextBox ID="EName" runat="server"></asp:TextBox>
            </td>
            <td style="width: 114px" nowrap align="right" width="114" bgcolor="#f0f0f0" height="25">
                <font face="����">
                    <table id="Table1" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td id="TDImage" style="border-right: #00ccff 1px dotted; border-top: #00ccff 1px dotted;
                                border-left: #00ccff 1px dotted; border-bottom: #00ccff 1px dotted" valign="bottom"
                                align="center" width="24" height="16">
                                <img id="ModuleImage" ondblclick="javascript:selectImage()" height="16" src="../../images/icons/htmlicon.gif"
                                    width="16" border="0" runat="server">
                            </td>
                            <td>
                                <div id="selectImg" title="<%=imageSelected%>" onclick="javascript:selectImage()">
                                    <u></u>&nbsp;</div>
                            </td>
                        </tr>
                    </table>
                </font>
            </td>
            <td width="155" bgcolor="#f0f0f0" height="25">
                <div id="Div1" title="<%=imageSelected%>" onclick="javascript:selectImage()">
                    <u>ѡ��ͼƬ</u></div>
                <input id="imgPickPic" style="width: 37px; height: 21px" type="hidden" size="1" value="<%=imageSelected%>"
                    name="imgPickPic">
            </td>
        </tr>
        <tr bgcolor="white">
            <td style="width: 98px" nowrap align="right" width="98" bgcolor="#f0f0f0" height="16">
                ���ܼ��&nbsp;
            </td>
            <td style="width: 106px" valign="top" width="106" height="16">
                <asp:TextBox ID="SName" runat="server"></asp:TextBox>
            </td>
            <td style="width: 114px" nowrap align="right" bgcolor="#f0f0f0" height="16">
                <asp:DropDownList ID="Type" runat="server">
                    <asp:ListItem Value="main" Selected="True">ģ��</asp:ListItem>
                    <asp:ListItem Value="menu">�˵�</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="64" bgcolor="#f0f0f0" height="16">
                ģ������
            </td>
        </tr>
        <tr bgcolor="white">
            <td style="width: 98px" nowrap align="right" width="98" bgcolor="#f0f0f0" height="25">
                �ù��ܷ��ʵ�ַ
            </td>
            <td width="409" colspan="3" height="25">
                <asp:TextBox ID="URL" runat="server" Width="272px" Height="24px"></asp:TextBox><br />
                <input style="width: 56px; height: 22px" type="button" value="������Ŀģ��" onclick="selectModule('News');">
                <font face="����">&nbsp;</font>
                <input style="width: 84px; height: 22px" type="button" value="�Զ���ҳ��" onclick="selectModule('NewsID');">
                <font face="����">&nbsp;</font>
                <input style="width: 84px; height: 22px" type="button" value="������ģʽ" onclick="selectModule('OneTable');">
                <font face="����">&nbsp;</font>
                <input style="width: 84px; height: 22px" type="button" value="�����޿��ģʽ" onclick="selectModule('OneTableNoFrame');">
                
            </td>
        </tr>
        <tr>
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25">
                ����ʱ���ݲ���
            </td>
            <td align="right" width="409" bgcolor="#f0f0f0" colspan="3" height="25">
                <p align="left">
                    <asp:TextBox ID="Para" runat="server" Width="272px"></asp:TextBox><input style="width: 56px;
                        height: 22px" type="button" value="��ѯͳ��" onclick="selectModule('Analysis');"
                        disabled><font face="����">&nbsp;</font><input style="width: 65px; height: 22px" type="button"
                            value="����ģ��" onclick="selectModule('Others');" disabled></p>
            </td>
        </tr>
        <tr bgcolor="white">
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25">
                ��������
            </td>
            <td align="right" width="409" bgcolor="#f0f0f0" colspan="3" height="25">
                <p align="left">
                    <asp:TextBox ID="Descr" runat="server" Width="400px" Height="45px" TextMode="MultiLine"></asp:TextBox></p>
            </td>
        </tr>
        <tr bgcolor="white">
            <td align="right" bgcolor="#f0f0f0" height="175" style="height: 175px">
                &nbsp;&nbsp;&nbsp;
                <!--img src="../../news/Images/SizeMinus.gif"-->
                <p>
                    ģ�鰴ť
                    <p>
                        <!--img src="../../news/Images/SizePlus.gif"-->
                    </p>
            </td>
            <td align="left" width="409" bgcolor="#f0f0f0" colspan="3" height="175" style="height: 175px"
                valign="top">
                <input onclick="ChooseTemplate();" type="button" value="ѡ��Ԥ���尴ť��" style="width: 112px;
                    height: 22px">
                <input onclick="javascript:ChooseButtons('<%=TemName%>')" type="button" value="����/���İ�ť"
                    style="width: 112px; height: 22px">
                <iframe name="frmButtons" id="frmButtons" src="ModuleButtons.aspx?buttTempName=<%=TemName%>"
                    width="100%" height="80%"></iframe>
            </td>
        </tr>
        <tr>
            <td align="right" width="409" bgcolor="#f0f0f0" height="25">
                <asp:TextBox ID="TempName" runat="server" Width="0px" contentEditable="false"></asp:TextBox>
            </td>
            <td align="left" width="409" bgcolor="#f0f0f0" colspan="3" height="25">
                <font face="����">
                    <asp:Label ID="lblMSG" runat="server" Width="400px" Height="21px" ForeColor="Red"
                        BorderColor="Silver" BorderWidth="1px"></asp:Label></font>
            </td>
        </tr>
        <tr bgcolor="white" height="30">
            <td align="center" width="504" bgcolor="#f0f0f0" colspan="4" height="27">
                <asp:Button ID="buttCreate" runat="server" Text="�����¼�ģ��" OnClick="buttCreate_Click">
                </asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="buttModify" runat="server" Text="����ģ��" OnClick="buttModify_Click">
                </asp:Button>&nbsp;
                <asp:Button ID="buttDelete" runat="server" Text="ɾ��ģ��" OnClick="buttDelete_Click">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25">
                ���Ե�ַ
            </td>
            <td align="left" width="409" bgcolor="#f0f0f0" colspan="3" height="25">
                <font face="����">
                    <asp:Label ID="LabelAbsPath" runat="server" Width="400px" Height="21px" ForeColor="blue"
                        BorderColor="Silver" BorderWidth="1px"></asp:Label></font>
            </td>
        </tr>
        <tr>
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25">
                ��Ե�ַ
            </td>
            <td align="left" width="409" bgcolor="#f0f0f0" colspan="3" height="25">
                <font face="����">
                    <asp:Label ID="LabelRlvPath" runat="server" Width="400px" Height="21px" ForeColor="blue"
                        BorderColor="Silver" BorderWidth="1px"></asp:Label></font>
            </td>
        </tr>
        <tr>
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25">
                ����չ��ַ
            </td>
            <td align="left" width="409" bgcolor="#f0f0f0" colspan="3" height="25">
                <font face="����">
                    <asp:Label ID="LabelRealPath" runat="server" Width="400px" Height="21px" ForeColor="skyblue"
                        BorderColor="Silver" BorderWidth="1px"></asp:Label></font>
            </td>
        </tr>
        <tr>
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25">
                <font face="����">����˵��</font>
            </td>
            <td align="left" width="409" bgcolor="#f0f0f0" colspan="3" height="25">
                <p>
                    <font face="����">����ϵͳ�������� DataOutQuery=������
                        <br>
                        ������� WindowSize=MAX<br>
                        ���Ž�������ҳ��෽����ʾ ViewType=false</font></p>
            </td>
        </tr>
    </table>
    <input type="hidden" id="hButtons" name="hButtons">
    <input type="hidden" id="hOperate" name="hOperate">
    </form>
</body>
</html>
