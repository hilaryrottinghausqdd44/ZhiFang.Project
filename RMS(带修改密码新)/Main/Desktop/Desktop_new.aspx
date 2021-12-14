<%@ Import Namespace="System.Xml" %>

<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Main.Desktop.Desktop_new" Codebehind="Desktop_new.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>可配置桌面系统</title>
    <link media="screen" href="../include/style_temp.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../include/desktop.js" type="text/javascript"></script>

</head>
<body>
    <%=xn_body.FirstChild.InnerText%>
</body>

<script defer> 
			function init_edit()//初始化定制按钮，不显示
			{
				var edit = "edit_";
				for(i=0;i<20;i++)
				{
					if(document.getElementById(edit+i))
					{
						var obj = document.getElementById(edit+i);
						obj.className="edit_none";  
					}
				}
			}
			function inint()//初始化
			{ 
				for(var i=0;i<parentTable.cells.length;i++)
				{ 
					var subTables=parentTable.cells[i].getElementsByTagName("table"); 
					for(var j=0;j<subTables.length;j++)
					{ 
						if(subTables[j].className!="dragTable")break; 
						subTables[j].rows[0].className="dragTR"; 
						subTables[j].rows[0].attachEvent("onmousedown",dragStart); 
						subTables[j].attachEvent("ondrag",draging); 
						subTables[j].attachEvent("ondragend",dragEnd); 
					} 
					//alert(subTables.length);
				} 
				init_edit();
			} 
			inint(); 
</script>

<script language="javascript">
				function getstring(str,n)
				{
					if(str.length > n)
					{
						//alert(str);
						str = str.substring(str,n) + "...";
					}
					return str;
				}
				
</script>

<%
    int i = 0;
    foreach (XmlNode xn in xns_zhifang)
    {
        i++;
%>

<script>
				if(document.getElementById("zfnews"+'<%=i%>')){
				document.getElementById("zfnews"+'<%=i%>').innerHTML = "<a href=" + '<%=xn.SelectSingleNode("link").FirstChild.InnerText%>' + " target=_blank >" +getstring('<%=xn.SelectSingleNode("title").FirstChild.InnerText%>',18) + "</a>";
				document.getElementById("zfnews"+'<%=i%>').title = '<%=xn.SelectSingleNode("title").FirstChild.InnerText%>';
				}
</script>

<%}
            i = 0;
            foreach (XmlNode xn in xns_forum)
            {
                i++;
%>

<script>
				if(document.getElementById("forum"+'<%=i%>')){
				document.getElementById("forum"+'<%=i%>').innerHTML = "<a href=" + '<%=xn.SelectSingleNode("link").FirstChild.InnerText%>' + " target=_blank >" +getstring('<%=xn.SelectSingleNode("title").FirstChild.InnerText%>',18) + "</a>";
				document.getElementById("forum"+'<%=i%>').title = '<%=xn.SelectSingleNode("title").FirstChild.InnerText%>';
				}
</script>

<%
    }
            i = 0;
            foreach (XmlNode xn in xns_blog)
            {
                i++;
%>

<script>
				if(document.getElementById("blog"+'<%=i%>')){
				document.getElementById("blog"+'<%=i%>').innerHTML = "<a href=" + '<%=xn.SelectSingleNode("link").FirstChild.InnerText%>' + " target=_blank >" +getstring('<%=xn.SelectSingleNode("title").FirstChild.InnerText%>',18) + "</a>";
				document.getElementById("blog"+'<%=i%>').title = '<%=xn.SelectSingleNode("title").FirstChild.InnerText%>';
				}
</script>

<%}
            i = 0;
            foreach (XmlNode xn in xn_file)
            {
                i++;
%>

<script>
				if(document.getElementById("file"+'<%=i%>')){
				document.getElementById("file"+'<%=i%>').innerHTML = "<a href=" + '<%=xn.SelectSingleNode("link").FirstChild.InnerText%>' + " target=_blank >" +getstring('<%=xn.SelectSingleNode("title").FirstChild.InnerText%>',18) + "</a>";
				document.getElementById("file"+'<%=i%>').title = '<%=xn.SelectSingleNode("title").FirstChild.InnerText%>';
				}
</script>

<%}
%>
</html>
