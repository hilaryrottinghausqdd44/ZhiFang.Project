<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.ValidityTime" Codebehind="ValidityTime.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ʱЧ������</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">

    <script id="clientEventHandlersJS" language="javascript">
	<!--

	function openBeginEnd() {
		var r=window.showModalDialog('../Tools/BeginEndTime.aspx');

	}
	function openRecycle() {
		var r=window.open('../Tools/RecycleTime.aspx');

	}

	//-->
    </script>

</head>
<body ms_positioning="GridLayout" language="javascript">
    <select style="z-index: 100; left: 16px; width: 296px; position: absolute; top: 24px;
        height: 112px" size="7">
        <option></option>
    </select>
    <div style="display: inline; z-index: 105; left: 432px; width: 88px; position: absolute;
        top: 80px; height: 24px" ms_positioning="FlowLayout">
        ÿ�����ظ�
    </div>
    <input id="buttBeginEnd" style="z-index: 101; left: 320px; position: absolute; top: 32px"
        type="button" value="�׶���ʱЧ" onclick="javascript:openBeginEnd()">
    <input id="buttRecycle" style="z-index: 103; left: 320px; position: absolute; top: 80px"
        type="button" value="�ظ���ʱЧ" onclick="javascript:openRecycle()">
    <div style="display: inline; z-index: 104; left: 432px; width: 88px; position: absolute;
        top: 32px; height: 24px" ms_positioning="FlowLayout">
        ��ֹʱ���
    </div>
</body>
</html>
