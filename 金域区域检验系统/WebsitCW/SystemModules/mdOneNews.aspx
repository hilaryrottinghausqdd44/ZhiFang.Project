<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mdOneNews.aspx.cs" Inherits="OA.SystemModules.SelectNewsPageModalDialog" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>选择对话框</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
<script language="javascript" type="text/javascript">
// <!CDATA[

function window_onload() {

}
function SetReturnValue(){
    var frm=document.frames["frmKid"];
    var retValue="";
    var pa = window.dialogArguments;

    var TableNews = document.frames["frmKid"].document.all["TableNews"];
    if (TableNews != null) {
        for (var i = 1; i < TableNews.rows; i++) {
            if (TableNews.rows(i).cells(1).firstChild.checked) {
                retValue +=";" +  TableNews.rows(i).cells(1).firstChild.title;
            }
        }
    }


    if (retValue.length > 0) {
        retValue = retValue.substr(1);
        var newempid=retValue.substr(0,retValue.indexOf(','));
        var empName=retValue.substr(retValue.indexOf(',')+1);
        
//        if(pa){
//            pa.setValue('0','ID=' + newempid,empName);
//            window.close();
        //        }
        window.returnValue = retValue;
        window.close();
    } 
    else
        alert('未选定内容');
    
}
// ]]>
</script>
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" onload="return window_onload()">
    <table width="100%" height="100%">
        <tr>
            <td height="1%">
                 <input id="ButtonConfirm" type="button" value="确定" onclick="javascript:SetReturnValue()" />
            </td>
        </tr>
        <tr>
            <td height="99">
                <iframe id="frmKid" name="frmKid" width="100%" height="100%" src="SelectNewsPage.aspx?NewsID=首页">
                </iframe>
            </td>
        </tr>
    </table>
</body>
</html>
