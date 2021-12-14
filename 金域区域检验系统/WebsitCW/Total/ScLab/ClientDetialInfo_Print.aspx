<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientDetialInfo_Print.aspx.cs" Inherits="OA.Total.ScLab.ClientDetialInfo_Print" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>客户：<%=ClientName%> 明细打印</title>    
    
    <link href="../../Css/style.css" rel="stylesheet" /> 
    
		<script type="text/javascript">
		  
        function $(s)
        {
            return document.getElementById?document.getElementById(s):document.all[s];
        }	
         function Print(cmdid,cmdexecopt)
			{
				//setUnShowPageTags();
				//document.all("printmenu").style.display="none";
				
				document.body.focus();
				
				try
				{
					var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>'; 

					document.body.insertAdjacentHTML('BeforeEnd', WebBrowser); 
					
					WebBrowser1.ExecWB(cmdid,cmdexecopt,null,null); 
					WebBrowser1.outerHTML = ""; 
					
					if(cmdid==6 && cmdexecopt==6)
					{
						alert('打印完成');
					}
				}
				catch(e){}
				finally
				{
					//document.all("printmenu").style.display="";
					//setShowPageTags();
				}
}


		</script>
		<style type="text/css">
		    @media Print { .prt { VISIBILITY: hidden }}
	
		
        .table_head td {BORDER-RIGHT:black 1px  solid;BORDER-TOP:black 1px  solid;BORDER-LEFT:black 1px  solid;BORDER-BOTTOM:black 1px  solid;background-color:Silver;}
        #table_list { background-color:White;border-color:#A7C4F7;border-width:1px;border-style:None;font-size:Smaller;width:98%;border-collapse:collapse;}
        #table_list tr {color:Black;background-color:White;font-size:Smaller;font-weight:bold;height:26px;}
        #table_list td {BORDER-RIGHT:black 1px  solid;BORDER-TOP:black 1px  solid;BORDER-LEFT:black 1px  solid;BORDER-BOTTOM:black 1px  solid}
        
       
    </style>
    
</head>
<body >
    <form id="form1" runat="server">
    <div id="wait"></div>
    <div style="margin-top:1px; margin-left:1px; margin-right:1px; margin-bottom:1px;">
        <DIV class="prt" id="printmenu" align="left">
            <INPUT class="prt" onclick="javascript:Print(6,1);" type="button" value="打印" name="button_print">
			<INPUT class="prt" onclick="javascript:Print(7,1);" type="button" value="打印预览" name="button_setup">
			<INPUT class="prt" onclick="javascript:Print(8,1);" type="button" value="打印页面设置" name="button_show">
			<BR>
		</DIV>
        <table  border="0" cellspacing="0" cellpadding="0" id="table_list">
            <tr class="table_head">
                <td>
                    姓名
                </td>
                <td>
                    日期
                </td>
                <td>
                    条码号
                </td>
                <td>
                    项目名称
                </td>
                <td>
                    项目价格
                </td>
                <td>
                    项目折扣
                </td>
                <td>
                    折后价格
                </td>
            </tr>
            <asp:Repeater ID="rep_list" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("CName")%>
                        </td>
                        <td>
                            <%# Convert.ToDateTime(Eval("inceptDate").ToString()).ToString("yyyy-MM-dd")%>
                        </td>
                        <td>
                            <%# Eval("SerialNo")%>
                        </td>
                        <td>
                            <%# Eval("ItemNamecw")%>
                        </td>
                        <td>
                            <%# Eval("ItemPrice")%>
                        </td>
                        <td>
                           <%# Eval("ItemAgio")%>
                        </td>
                        <td>
                            <%# Eval("ItemAgioPrice")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                     <tr>
                        <td></td>
                        <td></td>
                        <td>
                            标本汇总：<asp:Label ID="lblFormCount" runat="server" Text=""></asp:Label>
                        </td>
                        <td></td>
                        <td>
                            折前汇总：<asp:Label ID="lblSumItemPrice" runat="server" Text=""></asp:Label>
                        </td>
                        <td></td>
                        <td>
                            折后汇总：<asp:Label ID="lblSumItemAgioPrice" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </FooterTemplate>
            </asp:Repeater>
         </table>
    </div>
    </form>
</body>
</html>
