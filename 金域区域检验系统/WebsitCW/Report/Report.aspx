<%@ Import Namespace="System.Xml" %>

<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Report.Report" Codebehind="Report.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>报告库</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">

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
				document.Form1.reportIDs.value = reportID;
				//window.close();
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
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table width="610" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <label>
                    <textarea name="reportInfo" id="reportInfo" cols="83" rows="5" runat="server"></textarea>
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
                &nbsp;&nbsp;<input type="button" onclick="javascript:getData()" value="确 定"><font
                    face="宋体">&nbsp;
                    <asp:Button ID="addBtn" runat="server" Text="添 加" OnClick="addBtn_Click"></asp:Button>
                    <asp:Button ID="Button1" runat="server" Text="修 改" OnClick="Button1_Click"></asp:Button>&nbsp;
                    <asp:Button ID="delBtn" runat="server" Text="删 除" OnClick="delBtn_Click"></asp:Button>&nbsp;<input
                        type="button" onclick="javascript:window.close();" value="取 消">&nbsp;
                    <asp:TextBox ID="reportIDs" runat="server" Width="48px" Style="display: none"></asp:TextBox></font>
            </td>
        </tr>
        <tr>
            <td valign="top">
                &nbsp;
                <div style="padding-right: 10px; overflow-y: auto; padding-left: 10px; scrollbar-face-color: #ffffff;
                    font-size: 11pt; padding-bottom: 0px; scrollbar-highlight-color: #ffffff; overflow: auto;
                    width: 650px; scrollbar-shadow-color: #919192; color: blue; scrollbar-3dlight-color: #ffffff;
                    line-height: 100%; scrollbar-arrow-color: #919192; padding-top: 0px; scrollbar-track-color: #ffffff;
                    font-family: 宋体; scrollbar-darkshadow-color: #ffffff; letter-spacing: 1pt; height: 200px;
                    text-align: left">
                    <table width="600" id="reportTable" border="0" cellspacing="0" cellpadding="0">
                        <% 
                            foreach (XmlNode xn in xmlList)
                            {
                                string ids = xn.Attributes["id"].Value;
                                string content = xn.InnerText.ToString().Trim();
						
                                           
                        %>
                        <tr>
                            <td width="37" align="center" valign="top" class="reportFont">
                                <input type="radio" name="radiobtn" onclick="javascript:returnValue2()" value="<%=content%>">
                            </td>
                            <td width="563" class="reportFont">
                                &nbsp;<%=content%>
                            </td>
                            <td width="1" class="reportFont">
                                <input type="hidden" value="<%=ids%>">
                            </td>
                        </tr>
                        <%
                            }
                        %>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
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
