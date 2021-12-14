<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRoleManage.aspx.cs"
    Inherits="OA.Total.UserRoleManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>帐户送检单位权限管理</title>
    <link href="../Css/style.css" rel="stylesheet" />
    <link href="../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../Includes/JS/jquery-1.3.2.min.js"></script>

    <%--<script src="../JavaScriptFile/jquery-1.8.0.min.js" type="text/javascript"></script>--%>

    <% if (false)
       { %><script type="text/javascript" src="../Includes/JS/jquery-1.3.2-vsdoc2.js"></script><%} %>
       
       <STYLE TYPE="text/css">
          .fixedHeader     
          {         
           table-layout:fixed;
           border-color:gray;
           top:expression(this.offsetParent.scrollTop);  
           z-index: 10;
          }

          .fixedHeader td{
           text-overflow:ellipsis;
           overflow:hidden;
           white-space: nowrap;
          }
           body,html
        {
            font-family:Calibri;
            font-size:12px;
            line-height:18px;
            }
        #showBox
        {
         background-color:#F0FBEB;
         border:1px solid #9BDF70;
         display:none;
         width:500px;
         height:400px;
         position:absolute;
         z-index:200;
         }       
         #fullbg
        {
            position:absolute;
            cursor:pointer;
            background-color: Gray;
            display: none;
            z-index: 100;
            left: 0px;
            top: 0px;
            filter: Alpha(Opacity=30);
            -moz-opacity: 0.4;
            opacity: 0.4;
        }
         </STYLE>
         <script type="text/javascript">
             $(document).ready(function() {
                 GetUserList();                   
                    $("#chkall").click(function(){
                       $("input[type='checkbox']").attr("checked", $("#chkall").attr("checked"));
                   })

                   //关闭
                   $("#close").click(function() {
                       $("#showBox").hide("slow");
                       $("#fullbg").fadeOut("slow");
                   });
             });
             //得到帐户列表信息
             function GetUserList() 
             {
                 $.ajax({
                     type: "Get",
                     url: "HandlerBLL/UserRoleManageHandler.ashx",
                     data: "searchuser=" + $("#txtsearchuser").val(),
                     beforeSend: function() { ShowFullBg(); $("#divload").show(); },
                     complete: function() {
                         $("#fullbg").fadeOut("fast");
                         $("#divload").hide();
                     },
                     success: function(data, state) {
                         //alert(data);
                     if (state == "success") {
                             $("#listtable tr:gt(0)").remove();
                             $("#listtr").append(data);
                             GetClientList(1,$("#listtable tr:eq(1)").find("td:eq(0)").html(), $("#listtable tr:eq(1)").find("td:eq(2)").text());                             
                         }
                     }
                 });
             }
             //得到送检单位列表信息
             function GetClientList(type, id, name) {
                 $("#txtuserid").val(id);
                 //LemonChiffon 改变选中行背景色
                 $("#listtable tr[id=" + id + "]").css("background-color", "LemonChiffon");
                 //将未选中行背景色还原
                 $("#listtable tr:gt(0):not(tr[id=" + id + "])").css("background-color", "white");
                 $("#chkall").attr("checked", false);
//                 if (type == 3)
//                 { $("#btnSave").attr("disabled", "true"); }
//                 else { $("#btnSave").attr("disabled", "false"); }
                 $.ajax({
                     type: "POST",
                     url: "HandlerBLL/ClientList.ashx",
                     data: "type="+type+"&id=" + id,
                     beforeSend: function() { ShowFullBg(); $("#divload").show(); },
                     complete: function() {
                         $("#fullbg").fadeOut("fast");
                         $("#divload").hide();
                     },
                     success: function(data, state) {
                         //alert(data);
                         if (state == "success") {
                             $("#clienttable tr:gt(0)").remove();
                             $("#clienttr").append(data);
                         }
                     }
                 });
                 $("#selectuser").text(name);
             }
             //弹出遮层
             function ShowFullBg() 
             {
                 var bH = $(document).height();
                 var bW = $(document).width();
                 $("#fullbg").css({ width: bW, height: bH });
                 $("#fullbg").fadeTo(10, 0.1);
                 $("#fullbg").fadeIn("fast");                 
             }
             //全选
             function SelectChild(chkarea) {
                 //alert($("#" + chkarea).attr("checked"));
                 $("input[id^=" + chkarea + "]").attr("checked", $("#"+chkarea).attr("checked"));
             }
             function ShowDialog(status, msg) {
                 $("#showBox").slideDown("slow").css("position", "absolute").css("top", ($(document).height() - 200) / 2).css("left", ($(document).width() - 800) / 2);
                 $("#spanmsg").text(msg);
                 if (status == 0) {//成功
                     $("#showstateimg").attr("src", "../images/accept.png");
                 }
                 else if (status == 1) { 
                 //失败
                 $("#showstateimg").attr("src", "../images/delete.png"); }
                 else {
                     //警告
                     $("#showstateimg").attr("src", "../images/warning.png");
                 }
             }
              //保存
              function Save() {
                  var schk = "";
                 $("input[name='chkclient']").each(function(){ 
                        if($(this).attr("checked")) 
                        {
                            schk = schk + $(this).attr("title")+","; 
                        }
                    })
                    //if (schk.length > 1) {
                        $.ajax({
                            type: "POST",
                            url: "HandlerBLL/ClientList.ashx",
                            data: "type=2&id=" + $("#txtuserid").val() + "&client=" + schk,
                            beforeSend: function() { ShowFullBg(); $("#divload").show(); },
                            complete: function() {
                                //$("#fullbg").fadeOut("fast");
                                $("#divload").hide();
                            },
                            success: function(data, state) {
                                //alert(data);
                            if (state == "success") {                                    
                                    ShowDialog(0,data);
                                }
                            }
                        });
                      
                     //}
                     //else { ShowFullBg(); ShowDialog(2, '请选择送检单位!'); }                   

              }
              function SpecialAccount(specialid) 
              {
                  $.ajax({
                      type: "Get",
                      url: "HandlerBLL/SpecialAccountHandle.ashx",
                      data: "specialaccountid=" + specialid + "&specialvalue=" + $("#sepcealaccount" + specialid).html(),
                      beforeSend: function() { ShowFullBg(); $("#divload").show(); },
                      complete: function() {
                          $("#divload").hide();
                      },
                      success: function(data, state) {
                          //alert(data);
                          if (state == "success") {
                              if ($("#sepcealaccount" + specialid).html() == "是") { $("#sepcealaccount" + specialid).html("否"); }
                              else { $("#sepcealaccount" + specialid).html("是"); }
                              if (data == "设置成功") {
                                  ShowDialog(0,data);
                              }
                              else { ShowDialog(1, data); }
                          }
                      }
                  });
              }
              function SearchClientKey() {
                  var ckey = $("#txtclientkey").val();
                  if (ckey.length <= 0) {
                      alert('请输入关键字');
                      $("#txtclientkey").focus();
                  }
                  else {
                      if ($("#clienttable tr").find("td:eq(0):contains('" + ckey + "')").length > 0) {
                          $("#clienttable tr").find("td:eq(0)").css("color", "Black").css("font-weight", "normal").css("font-size", "1.0em");
                          $("#clienttable tr").find("td:eq(0):contains('" + ckey + "')").css("color", "red").css("font-size", "1.2em").css("font-weight", "bold");
                          $("#clienttable tr").find("td:eq(0):contains('" + ckey + "'):first input:eq(0)")[0].focus();
                      } else {alert('没有找到相关信息');}
//                      $("#clienttable tr").each(function(i) {                         
//                           if ($(this).children("td:eq(0)").text().indexOf(ckey) >= 0)
//                           { 
//                              $(this).children("td:eq(0)").css("color", "red").css("font-size","1.2em").css("font-weight","bold");
//                           }

//                      });
                  }
              }
            
         </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div id="fullbg">
    </div>
     <div id="showBox" ondblclick='$("#showBox").hide("slow");$("#fullbg").fadeOut("slow");' style="width:382px;height:auto;background-color:#F0FBEB;cursor: hand;display:none;z-index:100">
         <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
            border-top: #aecdd5 solid 8px; border-left: #aecdd5 solid 4px; border-right: #aecdd5 solid 4px;
            border-bottom: #aecdd5 solid 8px;" border="0">
            <tbody>
                <tr>
                    <td align="left" style="border-bottom: #aecdd5 solid 2px; width: 100; background:#aecdd5;">
                        <img src="../images/001_50.png" />
                    </td>
                    <td align="right" style="border-bottom: #aecdd5 solid 2px;background:#aecdd5">                      
                        <img src="../images/001_05.png" id="close" />
                    </td>
                </tr>
                <tr>
                    <td width="50px">
                      <img id="showstateimg" src="../images/accept.png" />                        
                    </td>
                    <td align="left">&nbsp;<span id="spanmsg" style="text-align:left">
                             </span></td>
                </tr>
            </tbody>
        </table>
     </div>
    <div id="divload" style="display:none;top:40%; right:60%;position:absolute; padding:0px; margin:0px; z-index:999;">
        <img src="../Css/indicator_big.gif" />
    </div>
    <table border="0" width="100%" cellspacing="0" cellpadding="2" align="left" style="border-collapse: collapse">
        <tr>
            <td colspan="3">
                <asp:Label ID="Label1" runat="server" Height="16px" Font-Size="Small" Font-Bold="True">查找用户:</asp:Label>
                <input type="text" id="txtsearchuser"/>
                &nbsp;
                <input type="button" id="btnsearch" value="查找" onclick="GetUserList();"/>(请输入账号名或者员工名)
                &nbsp;&nbsp;&nbsp;&nbsp; 
                &nbsp;&nbsp;&nbsp;&nbsp;
                查找客户:<input type="text" id="txtclientkey" />&nbsp;
                         <input type="button" id="btnCZclient" value="查找"  onclick="SearchClientKey();" />  
                         &nbsp;&nbsp;&nbsp;&nbsp;   
                         <input type="button" id="btnSave" onclick="Save();" value="保 存" />          
            </td>
        </tr>
        <tr>
            <td width="35%" valign="top" >
            <div id="div1" align="center" style="overflow: auto; width: 100%; height: 540px">     
                <table id="listtable" border="1" width="100%" cellspacing="0" cellpadding="2" align="left" style="cursor:hand;border-collapse: collapse">
                    <tr id="listtr" bgcolor="#e0e0e0" class="fixedHeader">
                        <td align="center" nowrap>
                            编号
                        </td>
                        <td align="center" nowrap>
                            账号名
                        </td>
                        <td align="center" nowrap>
                            员工姓名
                        </td>
                        <td align="center" nowrap>
                            已有客户
                        </td>
                         <td align="center" nowrap>
                            全部权限 
                        </td>
                    </tr>   
                           
                </table>
                </div>     
            </td>
            <td width="10px">
            </td>
            <td width="65%" valign="top" >
             <div id="divdg" align="center" style="overflow: auto; width: 100%; height: 430px">                                                 
                <table id="clienttable" border="1" width="100%" cellspacing="0" cellpadding="2" align="left" style="border-collapse: collapse">
                    <tr id="clienttr" bgcolor="#e0e0e0" class="fixedHeader">
                        <td align="left" nowrap><input type="hidden" id="txtuserid" />
                        <input type="checkbox" id="chkall" />
                          <span id="selectuser"></span>&nbsp;&nbsp;&nbsp;
                         </td>                   
                    </tr>
                  
                </table>
                </div>
            </td>
        </tr>
    </table>
   
    </form>
</body>
</html>
