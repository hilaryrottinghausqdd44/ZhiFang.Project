<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Main.Desktop.selectTarget" Codebehind="selectTarget.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>����������ҳ��</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript" id="clientEventHandlersJS">
<!--

function buttConfirm_onclick() {

	for(var i=0;i<document.all.length;i++)
	{	
		
		var ename=document.all[i].name
		if(ename=='Radio')
		{		
			if(document.all[i].checked)
			{
				
				window.parent.returnValue=document.all[i].value
				
			}
		}
	}
	//window.parent.returnValue=target.ToolTip;
	window.close();
}

//-->
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" style="width: 152px; height: 165px" cellspacing="1" cellpadding="1"
        width="152" border="0">
        <tr>
            <td style="height: 21px">
                <font face="����">
                    <input id="Radiotop" type="radio" checked value="_top" name="Radio">&nbsp; ��������</font>
            </td>
        </tr>
        <tr>
            <td>
                <input id="Radiopartent" type="radio" value="_parent" name="Radio">&nbsp; ������
            </td>
        </tr>
        <tr>
            <td>
                <input id="Radioblank" type="radio" value="_blank" name="Radio">&nbsp;&nbsp;�´���
            </td>
        </tr>
        <tr>
            <td>
                <font face="����">
                    <input id="Radioself" type="radio" value="_self" name="Radio"><font face="����">&nbsp;��ǰ����</font></font>
            </td>
        </tr>
        <tr>
            <td style="height: 1px">
                <p>
                    <input id="RadioOthers" type="radio" value="" name="Radio"><font face="����">&nbsp;�������</font></p>
            </td>
        </tr>
        <tr>
            <td>
                <font face="����"></font>
            </td>
        </tr>
        <tr>
            <td>
                <input id="buttConfirm" type="button" value="ȷ��" onclick="buttConfirm_onclick()">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
