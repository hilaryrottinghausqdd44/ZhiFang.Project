<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Report.modifyReport" Codebehind="modifyReport.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>编辑报告库信息</title>
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
<body leftmargin="0" topmargin="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="610" border="0">
        <tr>
            <td>
                <label>
                    <textarea id="reportInfo" name="reportInfo" rows="5" cols="83" runat="server"></textarea>
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
                <font face="宋体">
                    <asp:Button ID="addBtn" runat="server" Text="添 加" OnClick="addBtn_Click"></asp:Button><font
                        face="Times New Roman">&nbsp;</font>
                    <asp:Button ID="editBtn" runat="server" Text="修 改" OnClick="editBtn_Click"></asp:Button><font
                        face="Times New Roman">&nbsp;</font>
                    <asp:Button ID="delBtn" runat="server" Text="删 除" OnClick="delBtn_Click"></asp:Button>&nbsp;<input
                        onclick="javascript:window.opener.location=window.opener.location;window.close();"
                        type="button" value="取 消">&nbsp; </font>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
