<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfAdvise.aspx.cs" Inherits="OA.DBQuery.RunExec.ProfAdvise" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>单表录入</title>
		
		<LINK href="../css/style/NEWbluecray.css" type="text/css" rel="stylesheet" />	
		<script type="text/javascript" src="../js/dialog.js"></script>
		<!--#include file="../../Util/Calendar.js"-->	
	</head>
	<body class="InputBody"  ondblclick="fireEdit();" onkeydown="findNextFocus();">
		<form id="Form1"  runat=server >
				
		
			<table width="100%"  cellspacing=0 cellpadding=0 border=0 align=center style="DISPLAY:">
				<tr>
					<td valign="top" align="center">	
						<table id="TableData" cellSpacing="1" cellPadding="1" border="0" width="98%">
							<tr class="top">
								<td valign="top" align="center" width="100%">
									<TABLE id="TableData0" name="TableData0" cellSpacing="1" cellPadding="1" width="100%" border="0" class="InputTable">
									
										<tr>
										<td width="12" >&nbsp;</td>
										<td nowrap align=right width="20%" title="ProfessionalCheckView">评审意见&nbsp;</td>
										<td nowrap class="small" style="FONT-SIZE: 9pt" width="79%">
										
														<textarea style="width:100%" title="评审意见" keyIndex="No" NoChange="No"  runat=server
														method="="  AllowNull="Yes" id="ProfessionalCheckView"  onfocus="window.status='可以输入字符'" ONKEYPRESS="window.status='可以输入字符';"  ColumnDefault="" rows="6"></textarea>
													
										</td>
										</tr>
										
										<tr>
										<td width="12" >&nbsp;</td>
										<td nowrap align=right width="20%" title="ProfessionalCheckDate">评审时间&nbsp;</td>
										<td nowrap class="small" style="FONT-SIZE: 9pt" width="79%">
										
													<input title="评审时间" keyIndex="No" NoChange="No"  runat=server
													type="text" style="width:100%" method="="  AllowNull="Yes"
													id="ProfessionalCheckDate"  onchange="IsDate(this);"  onfocus="setday(this);window.status='只能输入日期,格式yyyy-MM-DD';"
													value="" ColumnDefault="">
												
										</td>
										</tr>
										

										
										<tr>
										<td width="12" >&nbsp;</td>
										<td nowrap align=right width="20%" title="ProfessionalName">评审专家&nbsp;</td>
										<td nowrap class="small" style="FONT-SIZE: 9pt" width="79%">
										
													<input title="评审专家" disabled keyIndex="No" NoChange="No" runat=server
													type="text" style="width:100%" method="="  AllowNull="Yes"
													id="ProfessionalName" onfocus="window.status='可以输入字符'"  ONKEYPRESS="window.status='可以输入字符';"  
													value="" ColumnDefault="">
												
										</td>
										</tr>
										
										</table>
										
								</td>
							</tr>
						</table>	
						<table >
						<tr>
						<br />
						    <td  align=center>
                                <asp:Button ID="Button1" runat="server" Text="保存评审专家意见" 
                                    onclick="Button1_Click" />&nbsp;</td>
						</tr>
						<tr><td>
                            <asp:Label ID="strErr" runat="server"></asp:Label>
                            </td></tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="no" name="no" value="" runat=server>
			<input type="hidden" id="AF_no" name="no" value="" runat=server>
			<br>
		</form>
	</body>
</HTML>



