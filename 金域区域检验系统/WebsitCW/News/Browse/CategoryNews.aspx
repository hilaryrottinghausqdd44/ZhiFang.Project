<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Browse.CategoryNews" Codebehind="CategoryNews.aspx.cs" %>

<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationSettings.AppSettings["MyTitle"]%>-��ϢĿ¼����
    </title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <link rel="stylesheet" type="text/css" href="../Main.css">
    <style type="text/css">
    	.trStyle td
    	{
    		border-bottom:solid 1px #eeddcc;
    	}
    </style>
    <script language="javascript" for="Toolbar1" event="onbuttonclick">
			switch (event.srcNode.getAttribute('Id'))
			{
				case 'new':
					window.open('../publish/NewsAddModify.aspx?Catagory=<%=catagory%>','','width='+window.screen.availWidth+',height='+window.screen.availHeight+',scrollbars=yes,resizable=yes' );		
					break;
				case 'modify':
					if(SelEmpl=='')
					{
						alert('��ѡ����Ϣ��');
						return false
					}
					else
						EditPerson(SelEmpl);
					
					break;
				case 'Remove':
					if(SelEmpl=='')
					{
						alert('��ѡ����Ϣ��');
						return false
					}
					else		
					if (confirm('ȷ��Ҫɾ��������Ϣ��'))
					
						FormDelUser.submit();
					 
					break;
				case 'PublishPreview':
				    if(SelEmpl=='')
				    {
						alert('��ѡ����Ϣ��');
						return false
				    }
				    else
						PubPrev(SelEmpl,'true','showpagelast.aspx','');
				    break;
				case "BrowseFeedback"://������
					if(SelEmpl=='')
				    {
						alert('��ѡ����Ϣ��');
						return false
				    }
				    else
						PubPrev(SelEmpl,'true','eachnews.aspx','');
					break;
				case "BrowseLast"://���
					if(SelEmpl=='')
				    {
						alert('��ѡ����Ϣ��');
						return false
				    }
				    else
						PubPrev(SelEmpl,'true','showpagelast.aspx','');
					break;
				case "BrowseOnly"://ȫ��
					if(SelEmpl=='')
				    {
						alert('��ѡ����Ϣ��');
						return false
				    }
				    else
						PubPrev(SelEmpl,'true','homepage.aspx','��,��,100%,100%');
					break;
				case "ButtHelp":
					window.open('../../Help/NewsModuleHelp.aspx','','width='+screen.availWidth+',height='+screen.availHeight+',scrollbars=yes,resizable=yes,top=0,left=0' );	
					break;
					
				
			}
    </script>

    <script language="javascript">
var sel;
			function DelUser(id)
		{
			if (confirm('�����Ҫɾ������Ϣ��'))
			{
				
				FormDelUser.delID.value=id;
				FormDelUser.submit();
			}
		}
		
		function EditPerson(eid)
			{
				if(eid!='')	
				{
				    window.open('../publish/NewsAddModify.aspx?Catagory=<%=catagory%>&id=' + eid, '', 'width=' + window.screen.availWidth + ',height=' + window.screen.availHeight + ',scrollbars=yes,resizable=yes');
				
				}
			}
			function PubPrev(eid,boolBrowseType,strOpenPage,strLocSize)
			{
				if(eid!='')
				{
//					if(boolBrowseType!="false")
//						window.open('eachnews.aspx?id='+eid,'','width='+screen.availWidth+',height='+screen.availHeight+',status=1,scrollbars=yes,resizable=yes,top=0,left=0' );	
//					else
				    //					    window.open('showpagelast.aspx?id=' + eid, '', 'width=' + screen.availWidth + ',height=' + screen.availHeight + ',status=1,scrollbars=yes,resizable=yes,top=0,left=0');
				    //window.open(strOpenPage +'?id=' + eid, '', 'width=' + screen.availWidth + ',height=' + screen.availHeight + ',status=1,scrollbars=yes,resizable=yes,top=0,left=0');
				    SelectTddb(strOpenPage + '?id=' + eid, strLocSize, true, '');
				}
			}

			var SelEmpl = '';
			function SelectEmpl(eid)
			{
				if (SelEmpl != '')
				{
					document.all['NM'+SelEmpl].style.backgroundColor = '';
					document.all['NM'+SelEmpl].style.color = '';
				}
				
				SelEmpl = eid;		
				document.all['NM'+eid].style.backgroundColor = 'gold';
				document.all['NM'+SelEmpl].style.color = 'black';
				FormDelUser.delID.value=eid;
			}
			function openWinPositionSize(url, ileft, itop, iwidth, iheight, WindowfrmName) {
			    //alert(" " + window.screen.availWidth + "," + ileft + "," + itop + "," + iwidth + "," + iheight);
			    //var mywin=window.open(url,"MyWin","toolbar=no,location=no,menubar=no,resizable=no,top="+py+",left="+px+",width="+w+",height="+h+",scrollbars=yes,status=yes");
			    var mywin = window.open(url, WindowfrmName, "toolbar=no,location=no,status=yes,menubar=no,resizable=yes,top=" + itop + ",left=" + ileft + ",width=" + iwidth + ",height=" + iheight + ",scrollbars=yes");
			}

			function SelectTddb(urlPop, strPositionSize, bOpen, windowFrmName) {
			    //alert('a');
			    //if (urlPop == "" || !bOpen)
			    //     return true;
			    //var strPositionSize=openPositionSize.value;
			    if (strPositionSize == '') {
			        strPositionSize = '��,��,80%,80%';
			    }
			    var arrPositionSize = strPositionSize.split(",");
			    var PositionLeft = window.screen.availWidth * .1;
			    var PositionTop = window.screen.availHeight * .1;
			    var PositionWidth = window.screen.availWidth * .8;
			    var PositionHeight = window.screen.availHeight * .8;

			    if (arrPositionSize.length == 4) {
			        try {
			            var iPositionLeft = arrPositionSize[0];
			            var iPositionTop = arrPositionSize[1];
			            var iPositionWidth = parseFloat(arrPositionSize[2]);
			            //alert(Math.round(iPositionWidth));
			            var iPositionHeight = parseFloat(arrPositionSize[3]);
			            switch (arrPositionSize[0]) {
			                case "��":
			                    PositionLeft = 0;
			                    break;
			                case "��":
			                    PositionLeft = window.screen.availWidth * (100 - iPositionWidth) / 200;
			                    PositionLeft = Math.round(PositionLeft);
			                    //alert(PositionHeight);
			                    break;
			                case "��":
			                    PositionLeft = window.screen.availWidth * (100 - iPositionWidth) / 100;
			                    PositionLeft = Math.round(PositionLeft);
			                    break;
			                default:
			                    try {
			                        PositionLeft = parseInt(iPositionLeft);
			                    }
			                    catch (e) { }
			                    break;
			            }
			            switch (arrPositionSize[1]) {
			                case "��":
			                    PositionTop = 0;
			                    break;
			                case "��":
			                    PositionTop = window.screen.availHeight * (100 - iPositionHeight) / 200;
			                    PositionTop = Math.round(PositionTop);
			                    break;
			                case "��":
			                    PositionTop = window.screen.availHeight * (100 - iPositionHeight) / 100;
			                    PositionTop = Math.round(PositionTop);
			                    break;
			                default:
			                    try {
			                        PositionTop = parseInt(iPositionTop);
			                    }
			                    catch (e) { }
			                    break;
			            }

			            switch (arrPositionSize[2]) {
			                default:
			                    try {
			                        PositionWidth = window.screen.availWidth * iPositionWidth / 100;
			                        PositionWidth = Math.round(PositionWidth);
			                    }
			                    catch (e) { }
			                    break;
			            }

			            switch (arrPositionSize[2]) {
			                default:
			                    try {
			                        PositionHeight = window.screen.availHeight * iPositionHeight / 100;
			                        PositionHeight = Math.round(PositionHeight);
			                    }
			                    catch (e) { }
			                    break;
			            }
			        }
			        catch (e) {
			            alert(e);
			        }
			    }
			    openWinPositionSize(urlPop, PositionLeft, PositionTop, PositionWidth, PositionHeight, windowFrmName);
			}
    </script>

</head>
<body bottommargin="0" bgcolor="#eefff9" leftmargin="0" topmargin="0" rightmargin="0"
    language="javascript">
    <form id="FormDelUser" name="FormDelUser" method="post" runat="server">
    <table border="0" width="100%" align="center" cellspacing="0" cellpadding="0" style="color: white;
        border-collapse: collapse" bgcolor="steelblue">
        <tr height="30">
            <td width="1%" nowrap>
                &nbsp;<img src="../publish/icons/0034_b.gif" align="Bottom">&nbsp;
            </td>
            <td>
                <strong>�������/&nbsp;<%=catagory%></strong>
            </td>
            <td align="right">
            </td>
        </tr>
    </table>
    <iewc:Toolbar ID="Toolbar1" runat="server" BorderStyle="Double" BorderColor="Blue"
        BackColor="#eefff9">
        <iewc:ToolbarButton Text="&amp;nbsp;�����Ϣ" ImageUrl="../publish/icons/0013_b.gif"
            DefaultStyle="display;border:solid 1px white;" ID="new" HoverStyle="border:solid 1px red;"
            Enabled="true"></iewc:ToolbarButton>
        <iewc:ToolbarButton Text="&amp;nbsp;�޸���Ϣ" ImageUrl="../publish/icons/0015_b.gif"
            DefaultStyle="display;border:solid 1px white;" ID="modify" HoverStyle="border:solid 1px red;"
            Enabled="true"></iewc:ToolbarButton>
        <iewc:ToolbarButton Text="&amp;nbsp;ɾ����Ϣ" ImageUrl="../publish/icons/0014_b.gif"
            DefaultStyle="display;border:solid 1px white;" ID="Remove" HoverStyle="border:solid 1px red;"
            Enabled="true"></iewc:ToolbarButton>
        <iewc:ToolbarButton Text="&amp;nbsp;���(����)" ImageUrl="../publish/icons/0034_b.gif"
            DefaultStyle="display;border:solid 1px white;" ID="BrowseFeedback" HoverStyle="border:solid 1px red;"
            Enabled="true"></iewc:ToolbarButton>
        <iewc:ToolbarButton Text="&amp;nbsp;���(ȫ��)" ImageUrl="../publish/icons/0034_b.gif"
            DefaultStyle="display;border:solid 1px white;" ID="BrowseOnly" HoverStyle="border:solid 1px red;"
            Enabled="true"></iewc:ToolbarButton>
        <iewc:ToolbarButton Text="&amp;nbsp;���(���)" ImageUrl="../publish/icons/0034_b.gif"
            DefaultStyle="display;border:solid 1px white;" ID="BrowseLast" HoverStyle="border:solid 1px red;"
            Enabled="true"></iewc:ToolbarButton>
        <iewc:ToolbarButton Text="&amp;nbsp;����" ImageUrl="../publish/icons/0034_b.gif" DefaultStyle="display;border:solid 1px white;"
            ID="ButtHelp" HoverStyle="border:solid 1px red;" Enabled="true"></iewc:ToolbarButton>
    </iewc:Toolbar>
    <asp:Label ID="Label2" runat="server"></asp:Label>
    <br>
    <table border="0" width="98%" cellspacing="0" cellpadding="2" align="center" style="border-collapse: collapse">
        <tr bgcolor="#e0e0e0">
            <td align="center" nowrap width="16">
                <img src="../publish/icons/0000_b.gif" align="Bottom">
            </td>
            <td align="center" nowrap width="16">
                <font color="#A20010">NO.</font>
            </td>
            <td align="center" nowrap width="16">
                <font color="#A20010">
                    <img src="../publish/icons/fujian.jpg" align="Bottom"></font>
            </td>
            <td align="center" nowrap width="16">
                [<font color="red">ͼ</font>]
            </td>
            <td align="center" nowrap width="32">
                <font color="#A20010">����</font>
            </td>
            <td align="center" nowrap width="60%">
                <font color="#A20010">����</font>
            </td>
            <td align="center" nowrap width="130">
                <font color="#A20010">����</font>
            </td>
            <td align="center" nowrap width="65">
                <font color="#A20010">����ʱ��</font>
            </td>
            <td align="center" nowrap width="30">
                <font color="#A20010">���</font>
            </td>
        </tr>
        <%
            if (iCount == 0)
            {
                Response.Write("<tr><td nowrap colspan=\"5\">������Ϣû�о�������</td></tr>");
                Response.End();
            }
        %>
        <%int i1;
          if (dt != null && dt.Rows.Count > 0)
          {
              for (i1 = 0; i1 < dt.Rows.Count; i1++)
              {
                  DateTime dateTimeValue, dateTimeValue2;
                  dateTimeValue2 = DateTime.Now;
                  dateTimeValue = Convert.ToDateTime(dt.Rows[i1]["buildtime"]);
                  System.TimeSpan subtractTime = (dateTimeValue2 - dateTimeValue);
                  int days = Convert.ToInt32(subtractTime.TotalDays);
        %>
        <tr id="NM<%=dt.Rows[i1]["id"].ToString()%>" bgcolor="#eefff9" onclick="javascript:SelectEmpl('<%=dt.Rows[i1]["id"].ToString()%>')"
            <%if(true)
						{%> ondblclick="javascript:PubPrev('<%=dt.Rows[i1]["id"].ToString()%>','<%=ViewType.ToLower()%>','<%=strOpenPage %>','<%=locationSize %>')"
            <%}%> onmouseover="javascript:this.bgColor='LemonChiffon'" onmouseout="this.bgColor=''"
            title="��������Ԥ����ϸ���ݣ������ѡ�д������ɽ����޸ġ�ɾ��" class="trStyle">
            <td align="center" nowrap>
                <img src="../publish/icons/0000_b.gif" align="bottom">
            </td>
            <td align="center" nowrap>
                <%=dt.Rows[i1]["id"].ToString()%><%////=(iCount-(iRealBegins-1)*iPageSize-i1).ToString() %>
            </td>
            <td align="center" nowrap>
                <%if (dt.Rows[i1]["atatchs"].ToString() != "")
                  {%><img src="../publish/icons/fujian.jpg"
                    align="bottom"><%}%>
            </td>
            <td align="center" nowrap>
                <%if (dt.Rows[i1]["pic"].ToString() == "True")
                  {%>[<font color="red">ͼ</font>]<%}%>
            </td>
            <td align="center" nowrap>
                <%if (days < 7)
                  {%><img src="../publish/icons/new.gif" align="bottom"><%}%>
            </td>
            <td align="left">
                <a href="javascript:PubPrev('<%=dt.Rows[i1]["id"].ToString()%>','<%=ViewType.ToLower()%>','<%=strOpenPage %>','<%=locationSize %>')">
                    <%=dt.Rows[i1]["title"].ToString()%></a>
            </td>
            <td align="center">
                <%=dt.Rows[i1]["writer"].ToString()%>
            </td>
            <td align="center" nowrap>
                <%=Convert.ToDateTime(dt.Rows[i1]["buildtime"]).ToString("yyyy-MM-dd hh:mm")%>
            </td>
            <td align="center" nowrap>
                <%if (dt.Rows[i1]["passed"].ToString() == "True")
                  {%><img src="../publish/icons/SPELL.jpg"
                    align="Bottom" onmouseover="this.bgColor=''" title="��ͨ�����"><%}
                  else
                  {%><img src="../publish/icons/DELETE.jpg"
                        align="Bottom" onmouseover="this.bgColor=''" title="δͨ�����"><%}%>
            </td>
        </tr>
        <%}
          }%>
    </table>
    <input id="delID" name="delID" type="hidden" value="<%=newID%>">
    <input id="delID1" name="delID1" type="hidden" value="<%=catagory%>">
    <input id="locationSize" name="locationSize" type="hidden" value="<%=locationSize %>" />
    </form>
    <iframe name="datafrm" width="0" height="300" src="../library/blank.htm"></iframe>
    <br>
    <iewc:TreeView ID="TreeView1" runat="server" Width="0" Height="0"></iewc:TreeView>
</body>
</html>
