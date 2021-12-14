<%@ Page Language="c#" CodeBehind="Public_List_PopNew.aspx.cs" AutoEventWireup="True"
    Inherits="OA.Documents.Public_List_PopNew" %>

<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Public_List_PopNew</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
			function $(s)
			{
				return document.getElementById?document.getElementById(s):document.all[s];
			}
	        
			function setParent(empID,empName)
			{
				var pa = window.dialogArguments;
				if(empID != null && empName != null)
				{
					if(pa){
						pa.setValue('0',empID,empName);
						window.close();
					} 
				}  
			}
			//oncheck事件
            function tree_oncheck(tree)
            {
               document.all.t1.value = "";
               FindCheckedFromNode(TreeView1);
            }

			//获取所有节点状态
            function FindCheckedFromNode(node) 
            {
                var i = 0;
                var nodes = new Array();
                nodes = node.getChildren();
                for (i = 0; i < nodes.length; i++) 
                {
                    var cNode;
                    cNode=nodes[i];
                    if (cNode.getAttribute("Checked"))
                    {
                        addnode(cNode)
                    } 
                    if (parseInt(cNode.getChildren().length) != 0 ) 
                    {
                       FindCheckedFromNode(cNode);
                    }
                }                
            }
            function addnode(node)
            {
               document.all.t1.value += Trim(node.getAttribute("NodeData")) + ',' + Trim(node.getAttribute("Text")) + ';';               
            }

            String.prototype.Trim = function(){ return Trim(this);} 
            String.prototype.LTrim = function(){return LTrim(this);} 
            String.prototype.RTrim = function(){return RTrim(this);} 

            //此处为独立函数 
            function LTrim(str) 
            { 
                var i; 
                for(i=0;i<str.length;i++) 
                { 
                if(str.charAt(i)!=" "&&str.charAt(i)!=" ")break; 
                } 
                str=str.substring(i,str.length); 
                return str; 
            } 
            function RTrim(str) 
            { 
                var i; 
                for(i=str.length-1;i>=0;i--) 
                { 
                   if(str.charAt(i)!=" "&&str.charAt(i)!=" ")break; 
                } 
                str=str.substring(0,i+1); 
                return str; 
            } 
            function Trim(str) 
            { 
                return LTrim(RTrim(str)); 
            } 

            function returnFun()
            {
              if(document.all.t1.value.length > 0)
              {
                 //alert(document.all.t1.value);
                 window.returnValue=document.all.t1.value;
                 window.close();
               }
               else
               {
                 alert('请选择文件夹');
               }
            }
    </script>

    <base target="_self">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table7" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
        <tr>
            <td>
                <font face="宋体" color="#8a5e00" size="2">
                    <iewc:TreeView ID="TreeView1" runat="server" AutoSelect="True"></iewc:TreeView>
                </font>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="Button1" value="选 择" onclick="returnFun();"><asp:Button
                    runat="server" Visible="false" ID="btnselect" Text="选 择" OnClick="btnselect_Click" />&nbsp;&nbsp;
                    <input type="button" id="btncancel" value="取 消" onclick="window.close();">
                <input type="hidden" id="t1" />
            </td>
        </tr>
    </table>
    <asp:Literal runat="server" ID="labpath" Visible="False"></asp:Literal>
    <asp:Label runat="server" ID="lbl_Curnodeid" Visible="False"></asp:Label>
    <span id="lblMessage" runat="server"></span>
    </form>
</body>
</html>
