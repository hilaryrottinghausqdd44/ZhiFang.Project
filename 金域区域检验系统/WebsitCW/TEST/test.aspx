<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.TEST.test" Codebehind="test.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>test</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../DbQuery/css/style/NEWallblue.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Includes/JS/Calendar.js"></script>

</head>
<body ms_positioning="GridLayout" class="QueryBody" onkeydown="findNextFocus();">
    <table id="TableTopQuery" border="0" align="center" cellpadding="0" cellspacing="0"
        width="98%">
        <tr height="30px">
            <td colspan="3">
                <div id="SortSearch" style="hand: CURSOR">
                    <a class="QueryGroup" href="javascript:SetSortSearch('PDocu.�ļ����!=')">ȫ��</a> <a class="QueryGroup"
                        href="javascript:SetSortSearch('PDocu.�ļ�״̬=ʹ����')">��Ч�ļ�</a> <a class="QueryGroup"
                            href="javascript:SetSortSearch('PDocu.�ļ�״̬=��ʧЧ')">ʧЧ�ļ�</a> <a class="QueryGroup"
                                href="javascript:SetSortSearch('PDocu.�ļ�����=�����ֲ�')">�����ֲ�</a> <a class="QueryGroup"
                                    href="javascript:SetSortSearch('PDocu.�ļ�����=�����ļ�')">�����ļ�</a> <a class="QueryGroup"
                                        href="javascript:SetSortSearch('PDocu.�ļ�����=��ҵָ����')">��ҵָ����</a>
                    <a class="QueryGroup" href="javascript:SetSortSearch('PDocu.�ļ�����=���')">���</a> <a
                        class="QueryGroup" href="javascript:SetSortSearch('PDocu.�ļ�����=�����ƶ�')">�����ƶ�</a>
                </div>
            </td>
        </tr>
        <tr>
            <td align="right">
                <table id="TableData" border="0" align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr class="Query">
                        <td width="12px">
                            &nbsp;
                        </td>
                        <td nowrap align="right">
                            ����&nbsp;
                        </td>
                        <td nowrap width="28%">
                            <input title="����" type="text" style="width: 100%" method="!!" id="PDocu.����" value="">
                        </td>
                        <td width="12px">
                            &nbsp;
                        </td>
                        <td nowrap align="right">
                            ��������&nbsp;
                        </td>
                        <td nowrap width="28%">
                            <input title="��������" type="text" style="width: 48%" method="">=" id="PDocu.��������"
                            value="" onfocus="setTody(this,'2008-04-16'); this.select();"' >-<input title="��������"
                                type="text" style="width: 48%" method="<=" id="PDocu.��������" value="" onfocus="setTody(this,'2008-04-16'); this.select();"'">
                        </td>
                    </tr>
                </table>
            </td>
            <td width="10%" align="center">
                <div align="center">
                    <img src="../image/middle/search.jpg" width="63" height="22" border="0" style="cursor: hand"
                        onmouseover="this.style.border='#ccccff thin outset'" onmouseout="this.style.border='#ccccff 0px outset'"
                        onmousedown="this.style.border='#ccccff thin inset'" onmouseup="this.style.border='#ccccff thin outset'"
                        onclick="FireQuery()"></div>
            </td>
            <td align="center" width="10%">
                <img src="../image/middle/Reset.jpg" width="63" height="22" border="0" style="cursor: hand"
                    onmouseover="this.style.border='#ccccff thin outset'" onmouseout="this.style.border='#ccccff 0px outset'"
                    onmousedown="this.style.border='#ccccff thin inset'" onmouseup="this.style.border='#ccccff thin outset'"
                    onclick="FireReset()">
            </td>
        </tr>
    </table>
    <input type="hidden" id="hAllIds" name="hAllIds" value=",PDocu.����,PDocu.��������">

    <script>
			parent.frames["Top"].frameElement.height=TableTopQuery.clientHeight+5;//'58';            //�����ѯ�ĸ߶���ʱ����Ϊ30 TableTopQuery.style.height//
			//alert(document.all["TableTopQuery"].clientHeight);
			
			if(!true)
				parent.frames["Top"].frameElement.height=0;//'58';            //�����ѯ�ĸ߶���ʱ����Ϊ30 TableTopQuery.style.height//
			
    </script>

    <!--input type="text" id="text1" onfocus="setday(this)" name="text1" style="WIDTH: 0px; HEIGHT:0px;	LEFT: 10px; POSITION: relative; TOP: -194px" size="78"></input���ڿؼ�-->
    <form id="Form1" method="post" action="">
    <input type="hidden" id="hTableFields" name="hTableFields" value=",����,��������">
    <input type="hidden" id="hQueryCollection" name="hQueryCollection">
    </form>
</body>
</html>
