<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Main.Index" Codebehind="Index.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN">
<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationSettings.AppSettings["MyTitle"]%>
       </title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script id="clientEventHandlersJS" language="javascript">
<!--

function window_onbeforeunload() {
		
		var frmMain=top.fsetMain;
		if(frmMain.rows=="0,*")
		{
			//if(!confirm("系统正处于窗口最大化,要关闭全部应用功能吗?"))
			//frmMain.rows="100,*";
			//frmMain.rows="100,81%";
			//frmMain.fset.cols="209,0,*";
			//event.returnValue=false;
			return "请点击取消\n**否则将关闭整个浏览器";
		//	alert(frmMain.rows);
		}
			
	//return true;
}
//function  window.onbeforeunload{){           
 // event.returnValue='您真的要退出吗？'
//}          
//function window.onunload()//注意写法,不是window_onunload
//{
	
  //top.window.close();     
 // window.location.href="logout.jsp";
//} 

//-->
    </script>

    <script language="javascript" for="window" event="onbeforeunload">
<!--
	event.returnValue =window_onbeforeunload();
	//alert(event.returnValue);
	var frmTop=top.fset;
	frmTop.cols="209,0,*";
	
	var frmMain=top.fsetMain;
	frmMain.rows="100,81%";
//-->
     
    </script>
   
</head>
<frameset id="fsetMain" rows="100,81%" frameborder="NO" border="0" framespacing="0">
		<frame src="IndexTop.aspx" name="topFrame" id="topFrame" scrolling="no" noresize>
		<FRAMESET id="fset" border="0" frameSpacing="0" frameBorder="0" cols="209,0,*" >
			<frame name="leftFrame" scrolling="auto" noresize src="IFrames/Indexleft.aspx">
			<frame name="midFrame" scrolling="auto" src="../RBAC/Organizations/DeptTree.aspx">
			
		  <%
		  //1　员工首页，2　员工启动模块，　3部门启动首页，4，公司启动首页      
		  if (strfirstid.Length > 0)
          { %>              
             <frame name="MainList" src="../News/Browse/homepage.aspx?<%=strfirstid%>" scrolling="auto">
          <%              
          }
          else	    
          {    
          %>
                <%if (DefaultModule == true)
                {%>
			        <frame name="MainList" src="../RBAC/MODULES/ModuleRun.aspx?ModuleID=<%=xmlnode.InnerXml%>" scrolling="auto">
			    <%}
                else　if (strDeptPageID.Length > 0)
                {%>
			        <frame name="MainList" src="../News/Browse/homepage.aspx?<%=strDeptPageID%>" scrolling="auto">
			    <%}
                else
                {%>
			        <frame name="MainList" src="Desktop/Desktop.aspx" scrolling="auto">
			    <%}%>
		  <%}%>
		</FRAMESET>
	</frameset>
</html>
