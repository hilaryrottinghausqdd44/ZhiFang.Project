<%@ Page Language="c#" AutoEventWireup="True" Inherits="DesktopSys.Desktop_config" Codebehind="Desktop_config.aspx.cs" %>

<%@ Import Namespace="System.Xml" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>��������ϵͳ</title>
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
                <a class="lnknav" onclick="return ShowWinList()" href="#"><b>���/ɾ��</b></a> <a class="lnknav"
                    onclick="return ShowExpdata()" href="#"><b>����/����</b></a> <a class="lnknav" onclick=""
                        href="#"><b>������ʽ</b></a> <a class="lnknav" onclick="return SaveBody()" href="#"><b>����</b></a>
                <a class="lnknav" href="Desktop_new.aspx"><b>��������</b></a>
            </div>
        </div>
    </div>
    <%=strBodyHTML%>
</body>

<script defer> 
		function init_edit()//��ʼ�����ư�ť����ʾ
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
		function inint()//��ʼ��
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
				alert("����ɹ���");
			}
			else
			{
				alert("����ʧ��");
			}
			
		}
		var strModules = '<%=strModules%>';             //������������
		strModules = strModules.substring(0,strModules.length-1);  //ȥ�����һ��+
		
		var ModulesConfig1={"module1":{"title":"�Ƿ�����","id":"module1"},"module2":{"title":"��������","id":"moudle2"}};
		
		
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
