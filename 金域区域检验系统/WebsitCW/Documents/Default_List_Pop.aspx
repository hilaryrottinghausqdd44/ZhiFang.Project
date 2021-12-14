<%@ Page language="c#" Codebehind="Default_List_Pop.aspx.cs" AutoEventWireup="True" Inherits="OA.Documents.Default_List_Pop" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Default_List_Pop</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../ModuleManage/style.css" rel="stylesheet">
		<script language="javascript" type="text/javascript">
        var prev = null;
        var empID = null;
        var empName = null;
        function selectx(row,index)   /**//*改变选中行的颜色还原为选中行的颜色*/
        {
            if(prev!=null)
            {
                prev.style.backgroundColor='#fff';
            }
            row.style.backgroundColor='#e4ecf1';
            prev=row;
            
            empID = $('ID_' + index).value;
            empName = $('NAME_' + index).value;
            setParent();
        }

        function $(s){
            return document.getElementById?document.getElementById(s):document.all[s];
        }
        
        function setParent(){
            var pa = window.dialogArguments;
            if(empID != null && empName != null)
            {
                if(pa){
                    pa.setValue('0',empID,empName);
                    window.close();
                } 
            }  
        }
        
        
		</script>
		<base target="_self">
		<style type="text/css">
        .hideClass { DISPLAY: none }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE height="100%" cellSpacing="10" cellPadding="0" width="95%" align="center" bgColor="#efefef"
					border="0">
					<TBODY>
						<TR>
							<TD vAlign="top" align="left" bgColor="#ffffff">
								<TABLE borderColor="#003366" height="100%" cellSpacing="0" borderColorDark="#ffffff" cellPadding="1"
									width="100%" align="center" bgColor="#f6f6f6" borderColorLight="#aecdd5" border="1">
									<TBODY>
										<TR>
											<TD>
												<TABLE borderColor="#003366" height="100%" cellSpacing="0" borderColorDark="#ffffff" cellPadding="10"
													width="100%" align="center" bgColor="#fcfcfc" borderColorLight="#aecdd5" border="1">
													<TBODY>
														<TR>
															<TD vAlign="top">
																<DIV align="center">
																	<TABLE class="font10" borderColor="#a7c4f7" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0"
																		width="95%" bgColor="#efefef" borderColorLight="#a7c4f7" border="1">
																		<TBODY>
																			<TR align="left">
																				<TD align="left">关键字:<asp:TextBox ID="txSearchText" CssClass="searchin" runat="server"></asp:TextBox>
																					<asp:button id="btnAdd" runat="server" CssClass="buttonstyle" Text="查 找" onclick="btnAdd_Click"></asp:button>
																				</TD>
																			</TR>
																		</TBODY>
																	</TABLE>
																</DIV>
															</TD>
														</TR>
														<TR vAlign="top">
															<TD align="center" height="100%">
																<asp:datagrid id="myDataGrid" runat="server" Width="95%" BorderColor="#A7C4F7" AutoGenerateColumns="False"
																	BackColor="White" BorderStyle="None" CellPadding="4" BorderWidth="1px" Font-Size="Smaller"
																	PageSize="3">
																	<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
																	<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
																	<HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000"></HeaderStyle>
																	<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
																	<Columns>
																		<asp:BoundColumn DataField="id">
																			<HeaderStyle CssClass="hideClass"></HeaderStyle>
																			<ItemStyle CssClass="hideClass"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn SortExpression="名称" HeaderText="名称">
																			<HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																			<ItemTemplate>
																				<%# DataBinder.Eval(Container, "DataItem.description") %>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn SortExpression="参数" HeaderText="参数">
																			<HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																			<ItemTemplate>
																				<asp:Label Runat="server" Visible="False" ID="labid" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'/>
																				<asp:Label Runat="server" ID="labaccount" Text='<%# DataBinder.Eval(Container, "DataItem.vvalue") %>'/>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn>
																			<HeaderStyle CssClass="hideClass"></HeaderStyle>
																			<ItemStyle CssClass="hideClass"></ItemStyle>
																			<ItemTemplate>
																				<input id='<%#"ID_" + DataBinder.Eval(Container, "DataItem.id")%>' value='<%#DataBinder.Eval(Container, "DataItem.vvalue")%>'
																					type="text" /> <input id='<%#"NAME_" + DataBinder.Eval(Container, "DataItem.id")%>' value='<%#DataBinder.Eval(Container, "DataItem.description")%>'
																					type="text" />
																			</ItemTemplate>
																		</asp:TemplateColumn>
																	</Columns>
																	<PagerStyle Mode="NumericPages"></PagerStyle>
																</asp:datagrid>
															</TD>
														</TR>
													</TBODY>
												</TABLE>
											</TD>
										</TR>
									</TBODY>
								</TABLE>
							</TD>
						</TR>
					</TBODY>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
