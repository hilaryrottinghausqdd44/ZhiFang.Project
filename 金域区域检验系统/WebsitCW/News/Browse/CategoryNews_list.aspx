<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryNews_list.aspx.cs"
    Inherits="OA.News.Browse.CategoryNews_list" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>信息目录导航</title>   
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" Content="C#">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <asp:Literal runat="server" ID="litcss"></asp:Literal>		
    <script language="javascript">
     var sel;
			function DelUser(id)
		{
			if (confirm('您真的要删除此信息吗？'))
			{
				
				FormDelUser.delID.value=id;
				FormDelUser.submit();
			}
		}
		
		function EditPerson(eid)
			{
				if(eid!='')	
				{				
					window.open('../publish/NewsAddModify.aspx?Catagory=&id='+eid,'','width=680px,height=620px,scrollbars=yes,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=0' );
				
				}
			}
			function PubPrev(eid,boolBrowseType)
			{
				if(eid!='')
				{
					if(boolBrowseType!="false")
						window.open('eachnews.aspx?id='+eid,'','width='+screen.availWidth+',height='+screen.availHeight+',status=1,scrollbars=yes,resizable=yes,top=0,left=0' );	
					else
					    window.open('homepage.aspx?id=' + eid, '', 'width=' + screen.availWidth + ',height=' + screen.availHeight + ',status=1,scrollbars=yes,resizable=yes,top=0,left=0');	
						
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
		 
    </script>

</head>
<body bottommargin="0" topmargin="0" leftmargin="0" rightmargin="0">
    <form id="form1" runat="server">
    <font face="宋体">
       <table width="100%" border="0" cellspacing="0" cellpadding="0" class="style-tableall">
				<tr>
					<td colspan="2">
						<table  width="100%" class="style-titletable" border="0" cellspacing="0" cellpadding="0">
							<tr>
							    <td width="13"><div class="style-titlepic"></div></td>
								<td nowrap><div class="style-titletext"><asp:Label runat="server" ID="Label1"></asp:Label></div></td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
				    <td width="13px"></td>
					<td align="left">
                            <asp:DataGrid ID="myDataGrid" runat="server" CssClass="style-texttable"
                                PageSize="20" CellSpacing="0" CellPadding="0" BorderWidth="0px" BorderStyle="None" AutoGenerateColumns="False"
                                 Width="100%" AllowPaging="True" onpageindexchanged="myDataGrid_PageIndexChanged" onitemdatabound="myDataGrid_ItemDataBound">                                
                               
                                <Columns>                                                
                                    <asp:TemplateColumn>
                                        <ItemTemplate>  
                                <div class="style-textpic"></div>     
								
								<div style="cursor:hand" onclick="javascript:PubPrev('<%# DataBinder.Eval(Container, "DataItem.id") %>','true');" class="style-text">
								<asp:Label runat="server" Height="10px" ID="dgtxtname" Text='<%# DataBinder.Eval(Container, "DataItem.title") %>' />&nbsp;&nbsp;&nbsp;<asp:Label Height="10px" runat="server" ID="labdate" Text='<%# DataBinder.Eval(Container, "DataItem.buildtime") %>' /></div>
                                  
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle CssClass="style-text" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                   </td>
				</tr>
			</table>
    </font>
    <asp:Label runat="server" ID="labmessage"></asp:Label>
    </form>
</body>
</html>
