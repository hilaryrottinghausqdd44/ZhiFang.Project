<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.PersonPower" CodeBehind="PersonPower.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>查看账户对模块的使用权限，操作权限———<%=EName%></title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link href="../../Includes/CSS/ioffice.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            input
            {
            	border-width:thin;
            	border:  0px none red;
            }
        </style>
         <STYLE TYPE="text/css">
          .fixedHeader {
           position:relative ;
           table-layout:fixed;
           border-style:solid;
           border-top:1px;
           border-color:gray;
           top:expression(this.offsetParent.scrollTop);  
           z-index: 10;
          }

          .fixedHeader td{
           text-overflow:ellipsis;
           overflow:hidden;
           white-space: nowrap;
          }
         </STYLE>


	</HEAD>
	<body topmargin="0">
	
				<asp:Repeater id="dataListOrder" Runat="server" 
                    onitemdatabound="dataListOrder_ItemDataBound1">
			        <HeaderTemplate>
			        <table width="100%" style="background-color:White" cellpadding="0" cellspacing="1" border="0">
			             <tr style="BACKGROUND-COLOR: white;font-weight:bold;"  class="fixedHeader">
						    <td nowrap align="left" style="BACKGROUND-COLOR: gainsboro" width="5%">
							    模块编号
						    </td>
						    <td nowrap style="BACKGROUND-COLOR: gainsboro"  width="5%">
							    模块名称<asp:Label ID="lblCountRows" Runat="server"></asp:Label>
						    </td>
						    <td style="BACKGROUND-COLOR: gainsboro" nowrap width="4%">角色
						    </td>
						    <td style="BACKGROUND-COLOR: gainsboro" nowrap width="5%">角色名称</td>
						    <td style="BACKGROUND-COLOR: gainsboro;color:Red;font-weight:bold" nowrap width="5%">只读,运行，分配</td>
						    <td style="BACKGROUND-COLOR: gainsboro">操作权限,按钮权限</td>
						    <td style="BACKGROUND-COLOR: gainsboro" nowrap width="5%">备注</td>
					    </tr>
			        </HeaderTemplate>
			        <ItemTemplate>
			        
				        <tr style="BACKGROUND-COLOR: whitesmoke;font-weight:normal">
				            <td nowrap="nowrap" align="left">
							    <asp:Label ID="lblSN" Runat="server"></asp:Label>
						    </td>
						    <td  nowrap="nowrap" style="BACKGROUND-COLOR: gainsboro">
							    <asp:Label ID="lblModuleName" Runat="server"></asp:Label>
						    </td>
						    <td nowrap="nowrap" align="center"><asp:Label ID="lblType" Runat="server"></asp:Label>
						    </td>
						    <td nowrap="nowrap" align="center"><asp:Label ID="lblRoleCName" Runat="server"></asp:Label></td>
						    <td nowrap="nowrap" align="center">
                                <asp:Label ID="literalRun" runat="server"></asp:Label></td>
						    <td>
						        <asp:Label ID="literalOpe" runat="server"></asp:Label></td>
						    <td nowrap="nowrap">
						        <asp:Label ID="lblDescr" Runat="server"></asp:Label>
						</tr>
				    </ItemTemplate>
				    <FooterTemplate>
				    
				       </table>
				    </FooterTemplate>
				</asp:Repeater>
		
	
	</body>
</HTML>
