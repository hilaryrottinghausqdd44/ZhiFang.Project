<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
 <HEAD>
  <TITLE> New Document </TITLE>
  <META NAME="Generator" CONTENT="EditPlus">
  <META NAME="Author" CONTENT="">
  <META NAME="Keywords" CONTENT="">
  <META NAME="Description" CONTENT="">
 </HEAD>
<script>
function aaa()
{
//alert(document.location.href);
var a=document.location.href;
var aa=String(a).split('?');
if(aa.length>=2)
{
document.location.href('http://localhost/ReportFormQueryPrint/Other/hemei.ashx?'+aa[1]);
}
else
{
alert("²ÎÊý´íÎó£¡");
}
}
</script>
 <BODY onload="aaa();">
  
 </BODY>
</HTML>
