<%@ Page Language="c#" CodeBehind="Module.aspx.cs" AutoEventWireup="True" Inherits="ZhiFang.WebLis.Modules.Module" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ģ�������ҳ��</title>
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript" id="clientEventHandlersJS">
<!--

function window_onload() {
	if('<%=NeedRefresh.ToString().ToLower()%>'=='true1')
	{
		//parent.frames["left"].location.reload();
		parent.frames["left"].location="ModuleList.aspx?Refresh=true&selectedIndex=<%=thisModuleID%>";
	}
}
function ChooseTemplate(TemName)
{
	r = window.showModalDialog ('../../RBAC/Roles/ChooseButtonTheme.aspx','','dialogWidth=25;dialogHeight=20;resizable=no;scroll=no;status=no');
	if(r != ''&& typeof(r)!='undefined')
	{
		//document.all['TempName'].value=r;
		frames["frmButtons"].location = "ModuleButtons.aspx?buttTempName=" + TemName + "&buttTempNameAdd=" + r;
	}
}
function IsValidate()
{
	
	var temname=Form1.CName.value;
	if (temname.length == 0 || Form1.ModuleCode.value == '' || Form1.ModuleCode.value.indexOf(' ')>-1)
	{
		alert('ģ��������Ų���Ϊ�գ�');
		return false;
	}
	if(temname.indexOf('\'')>-1)
	{
		alert('ģ�������ܰ���������!');
		return false;
	}
	
	var allButtons="";
	var iButtons=parseInt(document.frames["frmButtons"].document.all["hButtons"].value);
	var frmDocument = document.frames["frmButtons"].document;
	//�����д���Ӧ����������ʽ�滻������
	for(var i=0;i<iButtons;i++)
	{
	    allButtons += frmDocument.all["h" + i].value + "," + frmDocument.all["butt" + i].value.replace("'","") + "," + frmDocument.all["butt" + i].title
	    + "," + frmDocument.all["buttCode" + i].value.replace("'", "") + "," + frmDocument.all["buttDesc" + i].value.replace("'", "").replace(/\,/g,"��") + ";";
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

function ChooseButtonsAdd(TemName) {
    frames["frmButtons"].location = "ModuleButtons.aspx?buttTempName=" + TemName + "&buttAdd=buttAdd";
    return;
}

function ChooseButtons(TemName) {
    //TemName='+TemName
	r = window.showModalDialog ('../Themes/ChooseButtContainer.aspx','','dialogWidth=45;dialogHeight=30;resizable=no;scroll=no;status=no');
	if(r != ''&& typeof(r)!='undefined') {

	    frames["frmButtons"].location = "ModuleButtons.aspx?buttTempName=" + TemName + "&buttsAdd=" + r;
	    return;
	    
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
		
		Form1.submit();
	}
}
function ButtSelectModule_onclick() {
    var r;
    var r = window.showModalDialog('SelectModuleDialog.aspx', '', 'dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
    if (r == '' || typeof (r) == 'undefined' || typeof (r) == 'object') {
        return;
    }
    else {
        var returns = r.split("\=");
        var s = returns[0].split("\v");
        document.getElementById("tbRefModuleID").value = returns[1]; //ģ��ID
        document.all["tbRefModuleName"].value = s[0]; //ģ������
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

    <style type="text/css">
        .style1
        {
            font-weight: bold;
        }
        .style3
        {
            font-weight: bold;
            color: #FF0000;
        }
        .style4
        {
            color: #FF0000;
        }
        .styleCursorHand
        {
            cursor:hand;
        }
    </style>

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
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25" 
                class="style1">
                <span class="style4">ģ����</span>
            </td>
            <td style="width: 106px" width="106" height="25">
                <asp:TextBox ID="ModuleCode" runat="server"></asp:TextBox>
            </td>
            <td style="width: 113px" nowrap align="right" width="113" bgcolor="#f0f0f0" height="25">
                <b>ģ���ʶ</b></td>
            <td bgcolor="#f0f0f0" height="25">
                <asp:TextBox ID="ModuleID" runat="server" Enabled="False" CssClass="SingleBorder"
                    BackColor="#F0F0F0" Width="101px"></asp:TextBox>
                ֻ��</td>
        </tr>
        <tr bgcolor="white">
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25">
                <b><span class="style4">ģ����������</span> </b>
            </td>
            <td style="width: 106px" width="106" height="25">
                <asp:TextBox ID="CName" runat="server"></asp:TextBox>
            </td>
            <td style="width: 113px" nowrap align="right" width="113" bgcolor="#f0f0f0" height="25">
                ϵͳ����</td>
            <td bgcolor="#f0f0f0" height="25">
                <asp:CheckBox ID="Owner" runat="server" Checked="True"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td style="width: 98px" nowrap align="right" width="98" bgcolor="#f0f0f0" height="25">
                ģ��Ӣ������
            </td>
            <td style="width: 106px" width="106" height="25">
                <asp:TextBox ID="EName" runat="server"></asp:TextBox>
            </td>
            <td style="width: 114px" nowrap align="right" width="114" bgcolor="#f0f0f0" 
                height="25">
                    <u>ѡ��ͼƬ</u></td>
            <td width="155" bgcolor="#f0f0f0" height="25">
                <div id="Div1" title="<%=imageSelected%>" onclick="javascript:selectImage()">
                    <u><font face="����">
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
                                    &nbsp;</div>
                            </td>
                        </tr>
                    </table>
                </font>
                    </u></div>
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
                ģ������
            </td>
            <td width="64" bgcolor="#f0f0f0" height="16">
                <asp:DropDownList ID="Type" runat="server">
                    <asp:ListItem Value="main" Selected="True">ģ��</asp:ListItem>
                    <asp:ListItem Value="menu">�˵�</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr bgcolor="white">
            <td style="width: 98px" nowrap align="right" width="98" bgcolor="#f0f0f0" height="25">
                <b>�ù��ܷ��ʵ�ַ</b>
            </td>
            <td width="409" colspan="3" height="25">
                <asp:TextBox ID="URL" runat="server" Width="272px" Height="24px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25">
                ����ʱ���ݲ���
            </td>
            <td align="right" width="409" bgcolor="#f0f0f0" colspan="3" height="25">
                <p align="left">
                    <asp:TextBox ID="Para" runat="server" Width="265px"></asp:TextBox>
                    <input style="width: 56px; height: 22px" type="button" value="��ѯͳ��" onclick="selectModule('Analysis');"
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
            <td align="right" bgcolor="#f0f0f0" height="120" style="height: 120px">
                <b>&nbsp;&nbsp;&nbsp;
                <!--img src="../../news/Images/SizeMinus.gif"-->
                </b>
                <p>
                    <b>ģ�鰴ť
                    </b>
                    <p>
                        <b>
                        <!--img src="../../news/Images/SizePlus.gif"-->
                        </b>
                    </p>
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
       
        <tr>
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25">
                <b>�ο�λ��</b></td>
            <td align="left" width="409" bgcolor="#f0f0f0" colspan="3" height="25">
                ������{<asp:TextBox ID="tbRefModuleName" runat="server"></asp:TextBox>
                }ģ���(<b><a style="display:none" href="#" onclick="Javascript:ButtSelectModule_onclick()">����ѡ��</a></b>)<asp:TextBox ID="tbRefModuleID" 
                    runat="server" BackColor="#66FF99" Width="45px"></asp:TextBox>
                &nbsp;<asp:RadioButtonList ID="RBLBLocation" runat="server" RepeatColumns="3" CssClass="styleCursorHand">
                    <asp:ListItem Value="0">ģ��ǰ</asp:ListItem>
                    <asp:ListItem Selected="True" Value="1">ģ���</asp:ListItem>
                    <asp:ListItem Value="2">��ģ��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
         <tr bgcolor="white" height="30">
            <td align="center" width="504" bgcolor="#f0f0f0" colspan="4" height="27">
                <asp:Button ID="buttCreate" runat="server" Text="����������ģ��" 
                    OnClick="buttCreate_Click">
                </asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="buttModify" runat="server" Text="�޸Ĳ�����ģ��" 
                    OnClick="buttModify_Click">
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
                <font face="����">����˵��</font><b> </b>
            </td>
            <td align="left" width="409" bgcolor="#f0f0f0" colspan="3" height="25">
                <p>
                    <font face="����">����ϵͳ�������� DataOutQuery=������
                        <br>
                        ������� WindowSize=MAX<br>
                        ���Ž�������ҳ��෽����ʾ ViewType=false</font></p>
            </td>
        </tr>
        <tr>
            <td style="width: 98px" align="right" width="98" bgcolor="#f0f0f0" height="25" 
                class="style3">
                ע��</td>
            <td align="left" width="409" bgcolor="#f0f0f0" colspan="3" height="25" 
                class="style3">
                ÿһ��ģ���б����ֻ�ܴ���99��Ԫ��</td>
        </tr>
    </table>
    <input type="hidden" id="hButtons" name="hButtons">
    <input type="hidden" id="hOperate" name="hOperate">
    </form>
</body>
</html>
