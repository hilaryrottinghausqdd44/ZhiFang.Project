<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailDataInputUserControl.ascx.cs" Inherits="ECDS.UserControlLib.DetailDataInputUserControl" %>

<%@ Register TagPrefix="uc" Namespace="ECDS.DataServerControl" Assembly="DataServerControl" %>

    <%
    if (cssFile.Trim() == "")
    {
    %>
        <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />
    <%
    }
    else
    {
    %>
        <link href="<%=cssFile%>" type="text/css" rel="stylesheet"/>
    <%
    }
    %>

    
<script language="javascript" type="text/javascript">
    //编辑,浏览新闻
	function editNewsFunction(objForm, newsID, newsFileName)
	{
	    var url = "../eWebEditor/eWebEditorForm.aspx?style=popup&newsFileName=" + escape(newsFileName);
	    var width=600;
	    var height=400;
        var r = window.showModalDialog(url, "_blank", "dialogWidth=" + width + ";dialogHeight=" + height + ";center=yes;resizable=no;scroll=yes;status=yes");
        if (r == null || typeof(r) == 'undefined'||typeof(r)=='object')
        {
            return;
        }
        //显示
    	//window.open(url, "", "width="+width+",height="+height+",toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=yes");
   	    var obj = window.document.getElementById(newsID);
   	    if(obj == null)
   	    {
   	        //alert(newsID);
   	        return;
   	    }
   	    else
   	    {
   	        //alert("给传进来的隐藏字段赋值");
   	        //给传进来的隐藏字段赋值
   	        obj.value = r;
   	    }
	}
    //删除“上传文件”
	function deleteUploadFileFunction(obj, newsID, newsFileName)
	{
	    alert("deleteUploadFileFunction");
	    alert(newsFileName);
	}
    //显示“处理功能”
	function showInputFunction(obj, textBoxID, url)
	{
		try
		{
		    //拆分URL.以便对参数进行编码
		    var splitUrl = url.split("?");
		    //取到页面
		    var urlNew = splitUrl[0]
		    if(splitUrl.length > 1)
		    { 
		        urlNew = urlNew + "?";
		        //以下是参数
		        var splitParams = splitUrl[1].split("&");//取到参数列表并拆分
		        //从 url 中取 参数配置情况
		        for(var i=0;i<splitParams.length;i++)
		        {
			        var splitParam = splitParams[i].split("=");//拆分当前参数参数
			        var paramName = splitParam[0];
			        var paramValue = splitParam[1];
			        //对参数值进行编码
			        paramValue = escape(paramValue);
			        //加到URL
			        urlNew = urlNew + "&" + paramName + "=" + paramValue;
		        }
		    }
		    //alert(urlNew);
			var ret = window.showModalDialog(urlNew);
		    //alert(ret);
			if(ret != void 0)
			{
			    //alert(textBoxID);
			    var showObj = window.document.getElementById(textBoxID);
			    if(showObj == null)
			        alert("NULL");
			    else
			    {
				    showObj.value = ret;
				    //obj.value = ret;
				}
			}
		}
		catch(e)
		{
			alert('出错了!');
		}
	}

</script>
    
    

<table id="tableParent" runat="server" border="1" cellpadding="1" cellspacing="1" style="width:100%">
    <tr>
        <td id="tdRepeatColumns" style="white-space:nowrap; width:1%; background-color:#f1e3ff"　>
            <asp:Label ID="lblTitle" runat="server" CssClass="LabelTitle" Visible="True" Font-Bold="True">控件的标题</asp:Label>
            <!-- 每行显示的字段个数 -->
            <asp:Label ID="lblRepeatColumns" runat="server" Text="每行显示字段数:"></asp:Label>
            <asp:LinkButton ID="btnRepeatColumns1" runat="server" CommandName="RepeatColumns" OnClick="btnSelectLinkButtonClick">1</asp:LinkButton>
            <asp:LinkButton ID="btnRepeatColumns2" runat="server" CommandName="RepeatColumns" OnClick="btnSelectLinkButtonClick">2</asp:LinkButton>
            <asp:LinkButton ID="btnRepeatColumns3" runat="server" CommandName="RepeatColumns" OnClick="btnSelectLinkButtonClick">3</asp:LinkButton>
            <asp:LinkButton ID="btnRepeatColumns4" runat="server" CommandName="RepeatColumns" OnClick="btnSelectLinkButtonClick">4</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td id="tdContent">
            <asp:Table ID="tableDetailDataInput" runat="server" GridLines="Both" CssClass="GridView" Width="100%" EnableViewState="false">
            </asp:Table>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td id="tdSave" align="center">
            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" Enabled="false"/>
            <asp:Button ID="btnClose" runat="server" Text="关闭" OnClick="btnClose_Click"/>
         </td>
    </tr>
    <tr>
        <td id="tdShowErrorMessage" align="center">
            <asp:PlaceHolder ID="placeHolderRegularExpressionValidator" runat="server"></asp:PlaceHolder>
            <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
