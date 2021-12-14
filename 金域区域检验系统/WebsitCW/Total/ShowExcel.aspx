<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowExcel.aspx.cs" Inherits="OA.Total.ShowExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

    <script src="../JavaScriptFile/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../JavaScriptFile/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <link href="../Css/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <SCRIPT   language=JavaScript>
        $(function() {

            var progressbar = $("#progressbar"), progressLabel = $(".progress-label");
            progressbar.progressbar({
                value: false,
                change: function() {
                    //progressLabel.text(progressbar.progressbar("value") + "%");
                },
                complete: function() {
                    progressLabel.text("完成");
                    progressbar.hide();
                }
            });
            progressbar.hide();

            $("#Button3").click(function(event) {
                $("#Button2").trigger("click");
            });

            $("#btnGet").click(function(event) {
                $.ajax({
                    type: "post",
                    contentType: "application/json;charset=UTF-8",
                    url: "HandlerBLL/RequestDataPage.aspx/GetClientData",
                    dataType: "json",
                    success: function(data) {
                        if (data.d != null && data.d != "") {
                            progressbar.show();
                            var json = data.d.ResultDataValue;
                            var count = data.d.count
                            var result = $.parseJSON(json);
                            var res = "";
                            $.each(result, function(i, n) {
                                //调用服务生成Excel
                                $.ajax({
                                    type: "post",
                                    //async: false,
                                    contentType: "application/json;charset=UTF-8",
                                    url: "HandlerBLL/RequestDataPage.aspx/GenerateExcel",
                                    data: "{'clientno':'" + n.clientno + "','clientname':'" + n.cname + "','cwaccountdate':'" + n.cwaccountdate + "','accountmonth':'2015-12'}",
                                    dataType: "json",
                                    success: function(data) {
                                    },
                                    complete: function(XMLHttpRequest, textStatus) {
                                    }
                                })
                            })
                        } else {
                            alert("未找到客户信息！");
                        }
                    }
                })
            });



            function GetClientDataCallBack(result) {
                
             }


       
        });
    
    
  function   Run(strPath)   {   
  //exe.value=strPath;   
  try   {   
  var   objShell   =   new   ActiveXObject("wscript.shell");   
  objShell.Run(strPath);   
  objShell   =   null;   
  }   
  catch   (e){alert('找不到文件"'+strPath+'"(或它的组件之一)。请确定路径和文件名是否正确，而且所需的库文件均可用。')   
    
  }   
  }   
  </SCRIPT>  
  
  
  
  <style>
  .ui-progressbar {
    position: relative;
  }
  .progress-label {
    position: absolute;
    left: 50%;
    top: 4px;
    font-weight: bold;
    text-shadow: 1px 1px 0 #fff;
  }
  </style> 


</head>
<body>
    <form id="form1" runat="server">
   
    <br />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="Button2" />
&nbsp;<input id="Button3" type="button" value="button3" /><br />
    
    
    
    
    <input id="btnGet" type="button" value="获取数据" /><br />
    <input id="btnBegin" type="button" value="开始进度条" /><br />
    <div id="progressbar"><div class="progress-label">加载...</div></div>
    <br />




     <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    </form>
</body>
</html>
