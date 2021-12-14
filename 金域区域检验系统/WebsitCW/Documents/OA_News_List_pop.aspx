<%@ Page language="c#" Codebehind="OA_News_List_pop.aspx.cs" AutoEventWireup="True" Inherits="OA.Documents.OA_News_List_pop" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>digitlab_BBS_List_Pop</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../ModuleManage/style.css" rel="stylesheet">
		<script language="javascript" type="text/javascript">
        var prev = null;
        var empID = "智方新闻";
        var empName = "智方新闻";
        var empNewsNum = null;
        var empChaNum = null;
        var empCss = null;
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
            $('TextBoxNewsCat').value = empName;
            //
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
        
        function setParentNews()
        {
            var pa = window.dialogArguments;
            empNewsNum = document.all["NewsNum"].value;
            empChaNum = document.all["ChaNum"].value;
            empCss = document.all["DropDownList1"].value;
            
            if(empID == null)
            {
				alert("请选择列表！");
				return;
            }
            //得到复选框是否选中的判断,此处可根据编号判断多个复选框选中
            var morepar = ""
            var more = document.getElementById("chkmore");            
            if(more.checked)
            {
               var moreurl = '<%=moreurl%>';
               morepar = "&morepar=" + escape(moreurl);
            }           
            //取得复选框更多结束
            var newempid = "id=" + empID + "&NewsNum=" + empNewsNum + "&ChaNum=" + empChaNum +"&cssID=" + empCss + morepar;
            if(empID != null && empName != null)
            {
                if(pa){
                    pa.setValue('0',newempid,empName);
                    window.close();
                } 
            }
        }       
        
		</script>
		<base target="_self">
		<style type="text/css">.hideClass { DISPLAY: none }
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
													width="100%" align="center" bgColor="#fcfcfc" borderColorLight="#aecdd5" border="0">
													<TBODY>
														<TR>
															<TD vAlign="top" align="center">&nbsp;
																<TABLE id="Table1" cellSpacing="1" style="display:none" cellPadding="1" width="400" border="1">
																	<TR>
																		<TD>标题</TD>
																		<TD><asp:textbox id="TextBoxNewsCat" runat="server" CssClass="searchin" Width="96px">智方新闻</asp:textbox></TD>
																		<TD><input type="checkbox" id="chkmore"/></TD>
																		<TD style="WIDTH: 43px">更多</TD>
																		<TD><asp:checkbox id="CheckComp" runat="server"></asp:checkbox></TD>
																		<TD>权限</TD>
																	</TR>
																	<TR>
																		<TD>行数</TD>
																		<TD>
																			<asp:TextBox id="NewsNum" runat="server" Width="96px">10</asp:TextBox></TD>
																		<TD>字数</TD>
																		<TD style="WIDTH: 43px">
																			<asp:TextBox id="ChaNum" runat="server" Width="64px">15</asp:TextBox></TD>
																		<TD>风格</TD>
																		<TD><asp:dropdownlist id="DropDownList1" runat="server" Width="72px" ondatabinding="DropDownList1_DataBinding"></asp:dropdownlist></TD>
																	</TR>
																	<TR>
																		<TD align="center" colSpan="6">
																			<INPUT onClick="javascript:return setParentNews();" type="button" value="确定"></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR vAlign="top">
															<TD align="center" height="100%"><asp:datagrid id="myDataGrid" runat="server" PageSize="3" Font-Size="Smaller" BorderWidth="1px"
																	CellPadding="4" BorderStyle="None" BackColor="White" AutoGenerateColumns="False" BorderColor="#A7C4F7" Width="95%" onselectedindexchanged="myDataGrid_SelectedIndexChanged">
																	<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
																	<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
																	<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
																	<HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000"></HeaderStyle>
																	<Columns>
																		<asp:BoundColumn DataField="id">
																			<HeaderStyle CssClass="hideClass"></HeaderStyle>
																			<ItemStyle CssClass="hideClass"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn Visible="False" SortExpression="名称" HeaderText="参数">
																			<HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																			<ItemTemplate>
																				id=<%# DataBinder.Eval(Container, "DataItem.id") %>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn SortExpression="参数" HeaderText="名称">
																			<HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																			<ItemTemplate>
																				<asp:Label Runat="server" Visible="False" ID="labid" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'/>
																				<asp:Label Runat="server" ID="labaccount" Text='<%# DataBinder.Eval(Container, "DataItem.CName") %>'/>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn>
																			<HeaderStyle CssClass="hideClass"></HeaderStyle>
																			<ItemStyle CssClass="hideClass"></ItemStyle>
																			<ItemTemplate>
																				<input id='<%#"ID_" + DataBinder.Eval(Container, "DataItem.id")%>' value='<%#DataBinder.Eval(Container, "DataItem.CName")%>'
																					type="text" /> <input id='<%#"NAME_" + DataBinder.Eval(Container, "DataItem.id")%>' value='<%# DataBinder.Eval(Container, "DataItem.CName") %>'
																					type="text" />
																			</ItemTemplate>
																		</asp:TemplateColumn>
																	</Columns>
																	<PagerStyle Mode="NumericPages"></PagerStyle>
																</asp:datagrid></TD>
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
