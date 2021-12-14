<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.PostModule" Codebehind="PostModules.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>PersonList</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft FrontPage 4.0" name="GENERATOR">
    <meta content="FrontPage.Editor.Document" name="ProgId">
    <style type="text/css">
        .unnamed1
        {
            border-right: #0099ff 1px dashed;
            border-top: #0099ff 1px dashed;
            border-left: #0099ff 1px dashed;
            border-bottom: #0099ff 1px dashed;
        }
    </style>

    <script language="javascript" for="Treeview1" event="onselectedindexchange">
			//var parentNode = event.treeNode.getAttribute("NodeData");
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			//alert(Treeview1.clickedNodeIndex);
			frames['right'].location="ModuleRoles.aspx?id=" +node.getAttribute("NodeData");
			frames['right'].document.getElementById('oToolTip').style.display = '';
		</script>
	    <script language="javascript" for="Treeview1" event="onnodebound">
			//var parentNode = event.treeNode.getAttribute("NodeData");
			//alert(event.newTreeNodeIndex);
			var node = Treeview1.getTreeNode('0');
			frames['right'].location="ModuleRoles.aspx?id=" +node.getAttribute("NodeData");
			
		</script>

        <script language="javascript" type="text/javascript">
        // <!CDATA[

            function window_onload() {
                //
            }

        // ]]>
        </script>

</head>
<body MS_POSITIONING="GridLayout" bgcolor="#e9e9e9" onload="return window_onload()">
               <table border="0" width="100%" height="100%">
                <tr>
                    <td valign="top" width="15%">
		            <span style="FONT-SIZE: 10pt">全部模块列表</span>
		            <DIV align="left">
			            <?XML:NAMESPACE PREFIX=TVNS />
			            <?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
			            <tvns:treeview id="Treeview1"  <%if(xd!=null&&xd.DocumentElement!=null)
				            {
					            Response.Write(" treenodesrc=\"" + xd.DocumentElement.OuterXml.Replace("\"","\'") + "\" ");
				            }
				            %>  target="right" selectedNodeIndex="0" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" 
			            style="height:100%;width:150px;">
			            </tvns:treeview>
		            </DIV>
		        </td>
                <td valign="top">模块已授予的角色，通过复选框选择可以直接保存（包括按钮的授权操作）
                    <iframe id="right" width="98%" height="96%" align="left"></iframe>
                </td>
            </tr>
        </table>
		
	</body>
</html>
