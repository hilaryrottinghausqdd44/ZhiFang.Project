<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.TopQueryGroup" Codebehind="TopQueryGroup.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���ݷ���</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="clientEventHandlersJS">
		<!--

		function Form1_onsubmit() {
			if(Form1.TextBoxName.value.indexOf(" ")>=0||Form1.TextBoxName.value.length==0)
			{
				alert("���ݷ������Ʋ��ܰ����ո�");
				return false;
			}
			if(Form1.hAction.value!="New")
			{
				if(Form1.hID.value=="")
				{
					alert("�����޸Ļ�ɾ��");
					return false;
				}
			}
			return true;
		}
		function AddNew(obj)
		{
			Form1.hAction.value="New";
			Form1.hID.value="";
			Form1.ButtonSave.value="��������";
			Form1.ButtonSave.disabled=false;
			obj.disabled=true;
		}
		var ObjPrevious;
		function ModifyQuery(iPosition,cname,whereClause,SQL,descr)
		{
			if(ObjPrevious!=null)
				;
				//alert(ObjPrevious.style.backgroundColor);
				
			Form1.hID.value=iPosition;
			Form1.hAction.value="Modify";
			Form1.ButtonSave.value="�����޸�";
			Form1.buttonAddNew.disabled=false;
			Form1.TextBoxName.value=cname;
			whereClause=whereClause.replace(/&gt;/g,">");
			whereClause=whereClause.replace(/&lt;/g,"<");
			whereClause=whereClause.replace(/&amp;/g,"&");
			
			Form1.TextBoxWhereclause.value=whereClause;
			
			
			SQL=SQL.replace(/&gt;/g,">");
			SQL=SQL.replace(/&lt;/g,"<");
			SQL=SQL.replace(/&amp;/g,"&");
			
			Form1.TextboxSql.value=SQL;
			
			
			descr=descr.replace(/&gt;/g,">");
			descr=descr.replace(/&lt;/g,"<");
			descr=descr.replace(/&amp;/g,"&");
			Form1.TextBoxDescr.value=descr;
			Form1.ButtonSave.disabled=false;
			if(ObjPrevious!=null)
				ObjPrevious.style.border="red 0px solid";
			hover.innerHTML=document.all["QueryName_" + iPosition].innerHTML;
			
			if(Form1.chkMove.checked)
			{	if(ObjPrevious!=null)
				{
					Form1.hPreviousID.value=ObjPrevious.id.substr(ObjPrevious.id.indexOf("_")+1);
					hover.style.display="";
					//alert(Form1.hPreviousID.value + "--" + Form1.hID.value);
					Form1.submit();
				}
				hover.style.display="";
			}
			ObjPrevious=document.all["QueryName_" + iPosition];
			ObjPrevious.style.border="red thin solid";
			//alert(ObjPrevious.style.border);
			//alert(event.button);
			
			hover.style.top=document.body.scrollTop+event.clientY + 20;
			hover.style.left=document.body.scrollLeft+event.clientX;
		}
		function MouseMoveQueryName()
		{
			if(ObjPrevious!=null)
			{//ObjPrevious=document.all["showTip"];
				hover.style.top=document.body.scrollTop+event.clientY + 20;
				hover.style.left=document.body.scrollLeft+event.clientX;
			}
		}
		function SelectQueryGroup()
		{
			r = window.showModalDialog ('../../PopupSelectDialog.aspx?DBQuery/admin/TopQueryGroupSelection.aspx?<%=Request.ServerVariables["Query_string"]%>','','dialogWidth=145;dialogHeight=130;resizable=no;scroll=no;status=no');
			if(r != ''&& typeof(r)!='undefined')
			{
				var rS=r.split("\v");
				if(rS.length>1)
				{
					if(Form1.TextBoxName.value.length==0)
						Form1.TextBoxName.value=rS[0];
					if(Form1.TextBoxWhereclause.value.length>0)
						rS[1] =" and " + rS[1];
					Form1.TextBoxWhereclause.value +=rS[1];
				}
			}
		}
		function ShowGroupQuery()
		{
			var sql=Form1.TextboxSql.value;
			//sql=sql.replace(/\'/g,'\\\'');
			r = window.showModalDialog ('../../PopupSelectDialog.aspx?DBQuery/admin/TopQuerySQL.aspx?<%=Request.ServerVariables["Query_string"]%>&$$SQL=' 
				+sql ,'','dialogWidth=155;dialogHeight=150;resizable=no;scroll=no;status=no');
			if(r != ''&& typeof(r)!='undefined')//&&type(r)!="object")
			{
				Form1.TextboxSql.value=r;
            }
         }
         function ShowGroupQueryXML() {
             var sql = Form1.TextboxSql.value;
             //sql=sql.replace(/\'/g,'\\\'');
             r = window.showModalDialog('../DataRight/XmlEditConfig.aspx?<%=Request.ServerVariables["Query_string"]%>&FileName=TopUserDefinedQuery.xml'
				, '', 'dialogWidth=155;dialogHeight=150;resizable=no;scroll=no;status=no');
             document.location.href = document.location.href;
         }

		//-->
		</script>
		<script language="javascript" for="chkMove" event="onclick">
			if(this.checked)
			{
				Form1.ButtonSave.disabled=true;
				Form1.buttonAddNew.disabled=true;
				Form1.buttonDelete.disabled=true;
				hover.style.display="";
			}
			else
			{
				Form1.ButtonSave.disabled=false;
				Form1.buttonAddNew.disabled=false;
				Form1.buttonDelete.disabled=false;
				hover.style.display="none";
			}
		</script>
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<div id="hover" style="BORDER-RIGHT: #ffffcc 1px double; BORDER-TOP: #ffffcc 1px double; DISPLAY: none; FONT-SIZE: 8pt; BORDER-LEFT: #ffffcc 1px double; COLOR: #ffffff; BORDER-BOTTOM: #ffffcc 1px double; POSITION: absolute; BACKGROUND-COLOR: #0000ff">aaa</div>
		<form language="javascript" id="Form1" onsubmit="return Form1_onsubmit()" method="post"
			runat="server" onmousemove="MouseMoveQueryName()">
			<TABLE id="Table1" style="HEIGHT: 175px" cellSpacing="1" cellPadding="1" width="100%" border="1">
				<TR>
					<TD><FONT face="����">����</FONT></TD>
					<TD style="WIDTH: 260px"><FONT face="����"></FONT><INPUT type="button" value=".">
						<asp:textbox id="TextBoxName" runat="server" Width="208px"></asp:textbox><INPUT type="button" value="."></TD>
					<TD><FONT face="����"><FONT face="����">ϵͳ���Ʒ������ĺ�,���ݷ���Ҫ��������</FONT></FONT></TD>
				</TR>
				<TR>
					<TD><FONT face="����">׼����</FONT></TD>
					<TD style="WIDTH: 260px"><FONT face="����"><asp:textbox id="TextBoxWhereclause" runat="server" Width="261px" Height="48px" TextMode="MultiLine"></asp:textbox></FONT></TD>
					<TD vAlign="top">
						<P><FONT face="����"><BR>
							</FONT><FONT face="����"><INPUT onclick="SelectQueryGroup()" type="button" value="ѡ������"><BR>
							</FONT>
						</P>
					</TD>
				</TR>
				<TR>
					<TD><FONT face="����">SQL���</FONT></TD>
					<TD style="WIDTH: 260px"><FONT face="����">
							<asp:textbox id="TextboxSql" runat="server" Width="261px" TextMode="MultiLine" Height="100px"></asp:textbox></FONT></TD>
					<TD rowspan="2"><FONT face="����">	<INPUT onclick="ShowGroupQuery()" type="button" value="ѡ������ֶ�(*Ԥ������)" style="WIDTH: 192px; HEIGHT: 53px"><BR>
						һЩ��ʱ���йصĻ�����������(�ֶ���Ϊrdate)
1��	����     rdate= getdate() <br />
2��	���n��  rdate>=getdate()-n <br />
3��	��ǰ��  DATEPART(year, rdate)=DATEPART(year, getDate()) and DATEPART(month, rdate)=DATEPART(month, getDate()) and DATEPART(week, rdate)=DATEPART(week, getDate())
4��	��ǰ�� DATEPART(year, rdate)=DATEPART(year, getDate()) and DATEPART(month, rdate)=DATEPART(month, getDate())
5��	���m�� DATEDIFF(month, rdate, getdate())>m
6��	һ����(����) DATEPART(month, rdate)>=1 and DATEPART(month, rdate)<=3 and (����Ⱥ���)
7��	��,��������
10��	����� DATEPART(year, rdate)=DATEPART(year, getDate())
11��	����� DATEPART(year, rdate)=DATEPART(year, getDate())-1
							
						</FONT>
					</TD>
				</TR>
				<TR>
					<TD><FONT face="����"><FONT face="����">����</FONT></FONT></TD>
					<TD style="WIDTH: 260px"><asp:textbox id="TextBoxDescr" runat="server" Width="261px" Height="56px" TextMode="MultiLine"></asp:textbox></TD>
					
				</TR>
				<TR>
					<TD></TD>
					<TD style="WIDTH: 260px"><FONT face="����">&nbsp;</FONT><INPUT id="buttonAddNew" onclick="AddNew(this)" type="button" value="����"><FONT face="����">&nbsp;&nbsp;</FONT>
						<asp:button id="ButtonSave" runat="server" Text="����" Enabled="false" onclick="ButtonSave_Click"></asp:button><INPUT id="hAction" style="WIDTH: 60px; HEIGHT: 22px" type="hidden" size="4" value="Modify"
							runat="server"><INPUT id="hID" style="WIDTH: 60px; HEIGHT: 22px" type="hidden" size="4" name="Hidden1"
							runat="server"></TD>
					<TD><asp:label id="LabelMsg" runat="server"></asp:label></TD>
				</TR>
			</TABLE>
			<br>
			<br>
			<input type="hidden" id="hPreviousID" runat="server"> <STRONG>���ݷ�����ʾ, ѡ��, �ƶ�����</STRONG>
			<asp:table id="TablePanel" runat="server" BorderWidth="1" BackColor="Moccasin" Font-Size="14"
				Font-Bold="true">
				<asp:TableRow>
					<asp:TableCell Text="aa"></asp:TableCell>
				</asp:TableRow>
			</asp:table><span style="Z-INDEX: 102"><input id="chkMove" type="checkbox" name="chkMove" runat="server"><label for="chkMove">��������</label></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT disabled type="button" value="������ݷ���Ϸ���">
			<br>
			<br>
			<asp:button id="buttonDelete" runat="server" Text="ɾ��" onclick="buttonDelete_Click"></asp:button>
            <input id="ButtonXmlEdit" type="button" value="�߼�����༭" onclick="ShowGroupQueryXML();" />
		</form>
	</body>
</HTML>
