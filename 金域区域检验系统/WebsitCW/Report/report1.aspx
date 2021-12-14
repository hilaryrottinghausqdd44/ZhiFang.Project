<%@ Import Namespace="System.Xml" %>

<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Report.report1" Codebehind="report1.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>报告库</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript">
		   function  getData()
		   {
		      var r = document.Form1.reportInfo.value;
		      window.returnValue = r;
		      window.close();
		   }
		   
		   function returnValue2()
			{
				var returnValue1="";
				var reportID = "";
				var TableRows=document.Form1.all["reportTable"].rows;
				for(var i=0;i<=TableRows.length-1;i++)
				{
					if(TableRows[i].cells[0].childNodes[0].tagName=="INPUT")
					{
						if(TableRows[i].cells[0].childNodes[0].checked)
						{
							returnValue1 = TableRows[i].cells[0].childNodes[0].value;
							reportID = TableRows[i].cells[2].childNodes[0].value;
						}
							
					}
				}
				
				//window.returnValue=returnValue1;
				document.Form1.reportInfo.value = returnValue1;
				 //window.returnValue = returnValue1;
				//window.close();
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
				
				//------------------------------------
				var returnValue1="";
				var reportID = "";
				var TableRows=document.Form1.all["reportTable"].rows;
			    var ids = eid-1;
					if(TableRows[ids].cells[0].childNodes[0].tagName=="INPUT")
					{
						
					returnValue1 = TableRows[ids].cells[0].childNodes[0].value;
							
					}
			
				
				//window.returnValue=returnValue1;
				document.Form1.reportInfo.value = returnValue1;
				
			}
			
			function  editReport(reportID)
			{
			  /* var r = window.showModalDialog('modifyReport.aspx?action=edit&id='+reportID,'dialogWidth=600,height=300,staus=yes');
			   if(r!='' && r!='undefined')
			   {
			      return false;
			   }*/
			   var r=reportID;
			   window.open('modifyReport.aspx?action=edit&id='+r,'','width=600,height=300,staus=yes');   
			}
			
			function  addReport()
			{
			   /*var r = window.showModalDialog('modifyReport.aspx?action=NEW','','dialogWidth=600,height=300,staus=yes');
			    if(r!='' && r!='undefined')
			   {
			      return false;
			   }*/
			   window.open('modifyReport.aspx?action=NEW','','width=600,height=300,staus=yes');
			}
    </script>

    <style type="text/css">
        .reportFont
        {
            font-size: 12px;
            font-family: "宋体";
        }
    </style>
</head>
<body leftmargin="0" topmargin="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table width="610" border="0">
        <tr>
            <td>
                <label>
                    <textarea id="reportInfo" name="reportInfo" rows="5" style="display: none" cols="83"></textarea>
                </label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;<input onclick="javascript:getData()" type="button" value="确 定"><font
                    face="宋体">&nbsp;<input onclick="javascript:window.close();" type="button" value="取 消">&nbsp;<input
                        type="button" value="新 增" onclick="addReport()">
                </font>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div style="padding-right: 10px; overflow-y: auto; padding-left: 10px; scrollbar-face-color: #ffffff;
                    font-size: 11pt; padding-bottom: 0px; scrollbar-highlight-color: #ffffff; overflow: auto;
                    width: 650px; scrollbar-shadow-color: #919192; color: blue; scrollbar-3dlight-color: #ffffff;
                    line-height: 100%; scrollbar-arrow-color: #919192; padding-top: 0px; scrollbar-track-color: #ffffff;
                    font-family: 宋体; scrollbar-darkshadow-color: #ffffff; letter-spacing: 1pt; height: 200px;
                    text-align: left">
                    <table id="reportTable" cellspacing="0" cellpadding="0" width="600" border="1">
                        <%
                            int i = 1;
                            foreach (XmlNode xn in xmlList)
                            {
                                string ids = xn.Attributes["id"].Value;
                                string content = xn.InnerText.ToString().Trim();
							
						
                                           
                        %>
                        <tr id="NM<%=i%>" ondblclick="javascript:editReport('<%=ids%>')" onmouseover="this.bgColor='LemonChiffon'"
                            onclick="javascript:SelectEmpl('<%=i%>')" onmouseout="this.bgColor=''" bgcolor="#ffffff">
                            <td class="reportFont" valign="top" align="center" width="37">
                                <input type="radio" value="<%=content%>" style="display: none" name="radiobtn"><%=i%>
                            </td>
                            <td class="reportFont" width="563">
                                &nbsp;<%=content%>
                            </td>
                        </tr>
                        <%
                            i = i + 1;
                        }
                        %>
                        <tr bgcolor="#ffffff">
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
