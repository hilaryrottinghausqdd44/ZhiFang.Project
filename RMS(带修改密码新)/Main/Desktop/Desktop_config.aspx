<%@ Page Language="c#" AutoEventWireup="True" Inherits="DesktopSys.Desktop_config" Codebehind="Desktop_config.aspx.cs" %>

<%@ Import Namespace="System.Xml" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>桌面配置系统</title>
    <link media="screen" href="../include/style_temp.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../include/desktop.js" type="text/javascript"></script>

    <script>
		var modules="";
    </script>

</head>
<body>
    <form id="form1" method="post" runat="server">
    </form>
    <div id="nav">
        <div class="nav">
            <div class="navbar">
                <a class="lnknav" onclick="return ShowWinList()" href="#"><b>添加/删除</b></a> <a class="lnknav"
                    onclick="return ShowExpdata()" href="#"><b>导入/导出</b></a> <a class="lnknav" onclick=""
                        href="#"><b>更改样式</b></a> <a class="lnknav" onclick="return SaveBody()" href="#"><b>保存</b></a>
                <a class="lnknav" href="Desktop_new.aspx"><b>返回桌面</b></a>
            </div>
        </div>
    </div>
    <%=strBodyHTML%>
</body>

<script defer> 
		function init_edit()//初始化定制按钮，显示
		{
			var edit = "edit_";
			for(i=0;i<20;i++)
			{
				if(document.getElementById(edit+i))
				{
					var obj = document.getElementById(edit+i);
					obj.className="edit";  
				}
			}
		}
		function inint()//初始化
		{ 
			//var parentTable = document.getElementById("parentTable");
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
			} 
			init_edit();
		} 
		inint(); 
			
		function SaveBody()
		{
			var obj = document.getElementById("parentTable");
			//alert(obj.outerHTML);
			var result = DesktopSys.Desktop_config.SaveBody(obj.outerHTML).value;
			if(result == 0)
			{
				alert("保存成功！");
			}
			else
			{
				alert("保存失败");
			}
			
		}
		var strModules = '<%=strModules%>';             //可以无限扩充
		strModules = strModules.substring(0,strModules.length-1);  //去掉最后一个+
		
		var ModulesConfig1={"module1":{"title":"智方新闻","id":"module1"},"module2":{"title":"销售新闻","id":"moudle2"}};
		
		
		var  xxx = "module1";
		//alert(eval("ModulesConfig1."+xxx+".title"));
		//alert(ModulesConfig1.module1.id);
		//alert(ModulesConfig1.module2.title);
		//alert(ModulesConfig1.module2.id);
		
		
		function ModulesConfig(obj)
		{
			var module_id = obj.id;
			//alert(module_id);
			module_id = module_id.substring(0,module_id.indexOf('_'));
			ModuleConfig(module_id);
			
		}
</script>

</html>
