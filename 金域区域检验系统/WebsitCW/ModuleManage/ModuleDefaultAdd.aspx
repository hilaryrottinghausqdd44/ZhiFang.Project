<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleDefaultAdd.aspx.cs"
    Inherits="OA.ModuleManage.ModuleDefaultAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模块编辑/新增功能</title>
    <link href="style.css" rel="stylesheet" />

    <script type="text/javascript">	
        function setValue(tag,id,name)
        {
            //tag 
            if(id != null && name != null)
            {
                if(tag == 0)
                {
                    $('txtid').value = id;
                    $('txtvalue').value = name;              
                }
            }
        }
        function openchild(subWindow,subWidth,subHeight)
		{
			window.showModalDialog(subWindow,window,"dialogWidth=" + subWidth + "px;dialogHeight=" + subHeight + "px;help:no;status:no");
			
		}
		function inputpamselect()
		{
		    var tmpmid = $('litid').value;
		    		    var url = "ModuleParameterEdit.aspx";
		    if(tmpmid.length > 0)
		    {
		       url = 'ModuleParameterEdit.aspx?mid='+tmpmid;
		   }
		   var vValue = window.showModalDialog(url, this, 'dialogWidth:800px;dialogHeight:550px;');
		   //alert(vValue);
		   if (vValue) {
		       $('txtvalue').value = vValue;
		   }
		    //openchild1(url,800,500);
	}
	    
	    function inputpamselect1() {
	        var tmpmid = $('litid').value;
	        var txtvalue;
	        var url = "ModuleParameterPop.aspx";
	        if (tmpmid.length > 0) {
	            url = 'ModuleParameterPop.aspx?mid=' + tmpmid;
	        }
	        openchild1(url, 800, 500);
	    }
	    
		function openchild1(subWindow,subWidth,subHeight)
		{			
			window.open(subWindow,'newwindow','height='+subHeight+',width='+subWidth+',top=30,left=30,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no, status=no');			
		}
		function showUserDialog(tag)
        {
            var strModuleUrl = $('txturl').value;
            var strurl = $('txtpopurl').value;
            var strParaName=$('txtreturnpam').value;
            var ModuleName=$('txtname').value;
            if (strModuleUrl.length > 5)
            {
                strModuleUrl = "ModulePreview.aspx?ModuleUrl=" + strModuleUrl
                + "&DataSelecturl=" + strurl + "&DataPara=" + strParaName + "&ModuleName=" + ModuleName;
               //var url = strurl.substring(0,strurl.length-5)+"_Pop.aspx";
               window.showModalDialog(strModuleUrl, this, 'dialogWidth:800px;dialogHeight:700px;');
            }
            else
            {
               alert("请输入正确的弹出链接地址!");
               document.Form1.txtpopurl.focus();
            }
        }
        function showUserDialogEdit(tag) {
            var strurl = $('txtvalue').value;
            strurl = "InputParaEdit.aspx" + "?moreurl=" + strurl;
            //var url = strurl.substring(0,strurl.length-5)+"_Pop.aspx";
            var rFunPara = window.showModalDialog(strurl, this, 'dialogWidth:600px;dialogHeight:500px;');
            if (rFunPara) {
                $('txtpopurl').value = rFunPara;
            }
        }
        
        function check()
        {
            if(document.Form1.txtnum.value == null || document.Form1.txtnum.value == "")
			{
				alert('请输入模块编号!');
				document.Form1.txtnum.focus();
				return false;
			}   
			if(document.Form1.txtname.value == null || document.Form1.txtname.value == "")
			{
				alert('请输入模块名称!');
				document.Form1.txtname.focus();
				return false;
			}   
            if(document.Form1.txturl.value == null || document.Form1.txturl.value == "")
		    {
		       alert('请输入链接地址!');
		       document.Form1.txturl.focus();
		       return false;
		    }	
		    /*	    
		     if(document.Form1.txtpopurl.value == null || document.Form1.txtpopurl.value == "")
		    {
		       alert('请输入弹出链接地址!');
		       document.Form1.txtpopurl.focus();
		       return false;
		    }		    
            if(document.Form1.txtvalue.value == null || document.Form1.txtvalue.value == "")
		    {
		       alert('请选择输入参数!');
		       document.Form1.txtvalue.focus();
		       return false;
		    }
		   
		    if(document.Form1.txtid.value == null || document.Form1.txtid.value == "")
		    {
		       alert('请选择输入参数!');
		       document.Form1.txtvalue.focus();
		       return false;
		    }
		    if(document.Form1.txtreturnpam.value == null || document.Form1.txtreturnpam.value == "")
		    {
		       alert('请输入条目接收参数名!');
		       document.Form1.txtreturnpam.focus();
		       return false;
		    }
		    if(document.Form1.txtunionpam.value == null || document.Form1.txtunionpam.value == "")
		    {
		       alert('请输入关联输出参数说明!');
		       document.Form1.txtunionpam.focus();
		       return false;
		    }		    
		    if(document.Form1.txtdes.value == null || document.Form1.txtdes.value == "")
		    {
		       alert('请输入模块说明!');
		       document.Form1.txtdes.focus();
		       return false;
		    } */     
           return true;
        }
        function $(s)
        {
            return document.getElementById?document.getElementById(s):document.all[s];
        }
       
    </script>

    <style type="text/css">
        .hideClass
        {
            display: none;
        }
    </style>
    <base target="_self" />
</head>
<body>
    <form id="Form1" runat="server">
    <div>
        <table bordercolor="#003366" height="100%" cellspacing="0" bordercolordark="#ffffff"
            cellpadding="1" width="98%" align="center" bgcolor="#f6f6f6" bordercolorlight="#aecdd5"
            border="1">
            <tbody>
                <tr>
                    <td valign="top">
                         <asp:TextBox ID="litid" CssClass="hideClass" runat="server"></asp:TextBox>
                        <table bordercolor="#003366" cellspacing="0" bordercolordark="#ffffff" cellpadding="1"
                            width="100%" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5" border="1">
                            <tbody>
                                <tr>
                                    <td align="left" width="60px">
                                        模块编号:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtnum" MaxLength="50" runat="server"></asp:TextBox>
                                    </td>
                                   
                                </tr>
                                <tr>
                                 <td width="60px">
                                        模块名称:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtname" MaxLength="100" runat="server" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        模块地址:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txturl" MaxLength="100" runat="server" Width="400px"></asp:TextBox>
                                        <input type="button" id="btnselect" onclick="showUserDialog(0);" value="预览">
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td align="left">
                                        条目接收参数名:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox runat="server" Width="70px" ID="txtreturnpam"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        条目选择程序:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtpopurl" runat="server" Width="400px"></asp:TextBox>
                                        <input type="button" id="btnFunEdit" onclick="showUserDialogEdit(0);" value="编辑">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        更多链接地址:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtmore" MaxLength="100" runat="server" Width="400px"></asp:TextBox>
                                    </td>
                                </tr><tr>
                                    <td>
                                        输入参数:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox runat="server" CssClass="hideClass" ID="txtid"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtvalue" Height="77px" TextMode="MultiLine" 
                                            Width="346px"></asp:TextBox>
                                        <input type="button" id="btnselectpam1" value="编辑" onclick="inputpamselect();" />   
                                       
                                      
                                    </td>
                                </tr>
                                <tr>
                                   
                                    <td>
                                        关联输出参数说明:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox runat="server" ID="txtunionpam" Height="64px" TextMode="MultiLine" 
                                            Width="349px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        模块说明:
                                    </td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtdes" MaxLength="100" runat="server" Height="150px" TextMode="MultiLine"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:Button ID="btnSave" runat="server" CssClass="buttonstyle" Text="保 存" OnClick="btnSave_Click">
                                        </asp:Button>
                                        &nbsp;&nbsp;&nbsp;&nbsp;<input type="button" class="buttonstyle" id="btnclose" value="关 闭" onclick="window.returnValue=0;window.close();" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
