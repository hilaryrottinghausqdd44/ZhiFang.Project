<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateClientCode.aspx.cs" Inherits="OA.RBAC.Organizations.CreateClientCode" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>生成客户编码</title>
    <link href="../../Css/style.css" rel="stylesheet" />
      <style type="text/css">
        *
        {
        	 font :12px;}
        .style6
        {
            width: 101px;
        }
        .style8
        {
            width: 87px;
        }
        .style10
        {
            width: 94px;
        }
        .style11
        {
            width: 117px;
        }
        .style12
        {
            width: 129px;
        }
        .width1
        {
           width:30px;	
        }
        .width2
        {
           width:60px;	
        }
    </style>
      <script language ="javascript"  type ="text/javascript" >
    function Trans()
    {
       var style=document .getElementById ("ddclientstyle").value;
       var area=document .getElementById ("ddclientarea").value;
       var city=document .getElementById ("ddclientcity").value;
       if (style ==""||style ==null   )
       {
       alert ("客户类型不能为空，请选择");
        return false ;
       }
       else  if (area ==""||area ==null)
       {
         alert ("客户地区不能为空，请选择");
        return false ;
       }
       else if (city ==""||city ==null)
       {
           alert ("客户城市不能为空，请选择");
        return false ;
       }
       OA.RBAC.Organizations.CreateClientCode.ReturnCode(style, area, city, CallBack);
    }
    function CallBack(result)
    {
      if (result.value !=null&&result.value !="")
      {
        window.returnValue=result .value;   
        window.close ();
     }
     
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <table bordercolor="#003366" cellspacing="0" bordercolordark="#ffffff"
            width="100%" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5" >
            <tr>
                <td class="width1">
                    类型:</td>
                <td class="width2">
                    <asp:DropDownList ID="ddclientstyle" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="width1">
                    地区:</td>
                <td>
                    <asp:DropDownList ID="ddclientarea" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    城市:</td>
                <td>
                    <asp:DropDownList ID="ddclientcity" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        
            <tr>
                <td align="left" colspan="6" height="50px">               
                    <input id="Button2" type="button" class="buttonstyle" onclick ="Trans()" value="生成客户编号" /></td>
            </tr>          
        </table>
    </form>
</body>
</html>
