<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatModel.aspx.cs" Inherits="Report.ReportPrint.CreatModel" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>技师站打印</title>
    <link href="../Css/Default.css" type="text/css" rel="stylesheet" />
	<script type="text/javascript" src="../js/calendarb.js"></script>
	<script type="text/javascript" src="../js/Tools.js"></script>
	
	<script language="javascript">
	    function aaa() {
	        var o = document.getElementById('linkGetAllFrom');
	        o.click();
	    }
	    function ShowReprotFrom(FormNo, SectionNo) {
	        document.getElementById('ShowFormHtml').innerHTML = "加载中...";
	        Report.Ashx.ReportPrint.ShowForm(FormNo, SectionNo, callback);
	    }
	    function callback(result) {
	        if (result != null && result.value != null) {
	            //alert(result.value);
	            document.getElementById('ShowFormHtml').innerHTML = result.value;
	        }
	    } 
	     function OpenWindowModal()
	    {
	         var Fromno= GetCheckBoxValue('FromNoCheckBox');
	         if (String (Fromno )=="")
	         {
	           alert ('请选择要生成HTML的项目');
	           return false ;
	         }
	         window .open ('Modellist.aspx?formno='+Fromno,'neww', 'width=450px,height=400px,top=200px,left=300px,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no');
	         
	    }
	   function OpenWindowModalPrint()
	    {
	         var Fromno= GetCheckBoxValue('FromNoCheckBox');
	         if (String (Fromno )=="")
	         {
	           alert ('请选择要打印的项目');
	           return false ;
	         }
	         window .open ('PrintModal.aspx?formno='+Fromno,'neww', ' width=1000,height=900,toolbar=no,menubar=no,scrollbars=yes,resizable=no,location=no,status=no');
	         
	    }
	</script>
    <style type="text/css">
.tableMain { width:600px; margin:50px auto 0;  }
.tableMain, .tableMain th { border-collapse:collapse; border:1px solid #000; }
.scrollTable { height: 400px; overflow-x: hidden; overflow-y: auto; width: 100%; }
.scrollTable .odd td { background:#ccc; border-bottom:1px solid #000; border-top:1px solid #000; }
.scrollTable1 { height: 450px; overflow-x: hidden; overflow-y: auto; width: 100%; }
.scrollTable1 .odd td { background:#ccc; border-bottom:1px solid #000; border-top:1px solid #000; }
        
        .style1
        {
            width: 166px;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" cellspacing="1" cellpadding="0" border="0" style="background-color:#0099cc">
    <tr style="background-color:#FFFFFF"><td align="center" colspan="2"><div style="font-size:20px; font-weight:bold; margin:10px">检验结果查询</div> </td></tr>
    <tr style="background-color:#FFFFFF"><td align="center" colspan="2">
    <table width="100%" style="background-color:#FFFFFF" height="50px" cellspacing="1" cellpadding="0" border="0">
                        <tr>
                        <td>起始日期</td><td><input id="StartDate" name="StartDate" type="text" onfocus="calendar()" style="width:90px;" runat="server"  /></td>
                        <td>截止日期</td><td><input id="EndDate" name="EndDate" type="text" onfocus="calendar()" style="width:90px;" runat="server" /></td>
                        <td>病&nbsp;&nbsp;历&nbsp;&nbsp;号</td><td><input id="PatNo" name="PatNo" type="text"  style="width:90px;"  runat="server" /></td>
                        <td>病人姓名</td><td><input id="Name" name="Name"  type="text" style="width:90px;"  runat="server" /></td>
                        <td>申请单号</td><td><input id="SerialNo" name="SerialNo"  type="text" style="width:90px;" runat="server" /></td>
                        <td></td>
                        </tr>
                        <tr>
                        <td>科　　室</td><td><select id="Dept" name="Dept" style="width:95px;" runat="server">
                            <option></option>
                        </select></td>
                        <td>开单医生</td><td><select id="Doctor" name="Doctor" style="width:95px;" runat="server">
                            <option></option>
                        </select></td>
                        <td>病　　区</td><td><select id="District" name="District" style="width:95px;" runat="server">
                            <option></option>
                        </select></td>
                        <td>病　　床</td><td><input id="Bed" name="Bed" type="text" style="width:90px;"  runat="server" /></td>
                        <td>就诊类型</td><td><select id="SickType" name="SickType" style="width:95px;" runat="server">
                            <option></option>
                        </select></td>
                        <td><input value="查询" type="button" onclick="aaa();" /></td>
                        </tr>                        
                        </table>
    </td></tr>
        <tr >
            <td  width="25%" >
                <table width="100%" cellspacing="1" cellpadding="0" border="0">                    
                      <tr style="background-color:#FFFFFF">
						<th colspan="6"><div style="font-size:16px; font-weight:bold; margin:10px">报告列表</div></th>
						</tr>
						<tr >
						<th width="5"><input type="checkbox" onclick="SelAll(this);" /></th>
						<th width="60" align="right">核收日期</th>						
						<th width="60">姓名</th>
						<th width="60">样本号</th>
						<th width="60">病历号</th>
						<th width="15">&nbsp;</th>
						</tr>
						<tr>
    <td colspan="6" style="background-color:#FFFFFF"><div class="scrollTable">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                        <ContentTemplate>
                        <asp:LinkButton ID="linkGetAllFrom" style="display:none" runat="server" OnClick="linkGetAllItem_Click"></asp:LinkButton>
                        <asp:DataList ID="DataList1" runat="server" Width="100%">
                        <ItemTemplate>
				 	<table width="100%" height="25" bgcolor="#ffffff"  onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'" onmouseout="this.bgColor='#ffffff';this.style.color='#000000'" cellspacing="1" cellpadding="0" border="0">
						<tr onclick="ShowReprotFrom('<%# DataBinder.Eval(Container.DataItem,"FormNo")%>','<%# DataBinder.Eval(Container.DataItem,"SectionNo")%>');">
						<td width="5"><input name="FromNoCheckBox" type="checkbox" value="<%# DataBinder.Eval(Container.DataItem,"FormNo")%>" /></td>
						<td width="60" align="right"><%# DataBinder.Eval(Container.DataItem, "ReceiveDate", "{0:yyyy-MM-dd}")%></td>
						<td width="60"><%# DataBinder.Eval(Container.DataItem, "CName")%></td>
						<td width="60"><%# DataBinder.Eval(Container.DataItem, "SampleNo")%></td>
						<td width="60"><%# DataBinder.Eval(Container.DataItem, "PatNo")%></td>
						</tr>
					</table>
				</ItemTemplate>
				<AlternatingItemTemplate>
					<table width="100%" height="25" bgcolor="#BFDBE1" onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'" onmouseout="this.bgColor='#BFDBE1';this.style.color='#000000'" cellspacing="1" cellpadding="0" border="0">
						<tr onclick="ShowReprotFrom('<%# DataBinder.Eval(Container.DataItem,"FormNo")%>','<%# DataBinder.Eval(Container.DataItem,"SectionNo")%>');">
						<td width="5"><input name="FromNoCheckBox" type="checkbox" value="<%# DataBinder.Eval(Container.DataItem,"FormNo")%>" /></td>
						<td width="60" align="right"><%# DataBinder.Eval(Container.DataItem, "ReceiveDate", "{0:yyyy-MM-dd}")%></td>
						<td width="60"><%# DataBinder.Eval(Container.DataItem, "CName")%></td>
						<td width="60"><%# DataBinder.Eval(Container.DataItem, "SampleNo")%></td>
						<td width="60"><%# DataBinder.Eval(Container.DataItem, "PatNo")%></td>
						</tr>
					</table>
				</AlternatingItemTemplate>
                        </asp:DataList>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                    </td>
                    </tr>
                </table>
                
            </td>
            <td valign="top" width="75%" style="background-color:#ffffff;padding:0px">
            <div class="scrollTable1">
            <div id="ShowFormHtml" ></div>
            </div>
                
            </td>
        </tr>
    </table>
    <table width="100%" height="25" bgcolor="#BFDBE1" onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'" onmouseout="this.bgColor='#BFDBE1';this.style.color='#000000'" cellspacing="1" cellpadding="0" border="0">
						<tr >
						
						<td class="style1" ><input onclick ="OpenWindowModal()" type ="button" value ="生成HTML" style="width: 70px"  /> <input type ="button" onclick="OpenWindowModalPrint()" value ="打印" style="width: 70px"  /></td>
						<td style ="width :70px" ></td>
						<td></td>
						<td></td>
						<td></td>
						
						</tr>
					</table>
    </div>
    
    </form>   
</body>
</html>

