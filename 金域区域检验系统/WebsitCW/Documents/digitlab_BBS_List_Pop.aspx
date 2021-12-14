<%@ Page language="c#" Codebehind="digitlab_BBS_List_Pop.aspx.cs" AutoEventWireup="True" Inherits="OA.Documents.digitlab_BBS_List_Pop" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>digitlab_BBS_List_Pop</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../ModuleManage/style.css" rel="stylesheet">
		<script language="javascript" type="text/javascript">
        var prev = null;
        var empID = null;
        var empName = null;
        function selectx(row,index)   /**//*�ı�ѡ���е���ɫ��ԭΪѡ���е���ɫ*/
        {
            if(prev!=null)
            {
                prev.style.backgroundColor='#fff';
            }
            row.style.backgroundColor='#e4ecf1';
            prev=row;
            
            empID = $('ID_' + index).value;
            empName = $('NAME_' + index).value;
            //setParent();
            //��׼ id,name;id1,name1;id2,name2
            window.returnValue = empID+','+empName;
            window.close();
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
			<FONT face="����">
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
																				<TD align="left">�ؼ���:<asp:TextBox ID="txSearchText" CssClass="searchin" runat="server"></asp:TextBox>
																					<asp:button id="btnAdd" runat="server" CssClass="buttonstyle" Text="�� ��" onclick="btnAdd_Click"></asp:button>
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
																	<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
																	<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
																	<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
																	<HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000"></HeaderStyle>
																	<Columns>
																		<asp:BoundColumn DataField="boardid">
																			<HeaderStyle CssClass="hideClass"></HeaderStyle>
																			<ItemStyle CssClass="hideClass"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn Visible="False" SortExpression="����" HeaderText="����">
																			<HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																			<ItemTemplate>
																				id=<%# DataBinder.Eval(Container, "DataItem.boardid") %>&amp;&amp;NewsNum=<%=listcount%>&amp;&amp;ChaNum=<%=characount%>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn SortExpression="����" HeaderText="����">
																			<HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																			<ItemTemplate>
																				<asp:Label Runat="server" Visible="False" ID="labid" Text='<%# DataBinder.Eval(Container, "DataItem.boardid") %>'/>
																				<asp:Label Runat="server" ID="labaccount" Text='<%# DataBinder.Eval(Container, "DataItem.BoardType") %>'/>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn>
																			<HeaderStyle CssClass="hideClass"></HeaderStyle>
																			<ItemStyle CssClass="hideClass"></ItemStyle>
																			<ItemTemplate>
																				<input id='<%#"ID_" + DataBinder.Eval(Container, "DataItem.boardid")%>' value='<%#DataBinder.Eval(Container, "DataItem.boardid")%>'
																					type="text" /> <input id='<%#"NAME_" + DataBinder.Eval(Container, "DataItem.boardid")%>' value='<%#DataBinder.Eval(Container, "DataItem.BoardType")%>'
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
