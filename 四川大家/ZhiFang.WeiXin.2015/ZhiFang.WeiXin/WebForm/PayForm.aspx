<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayForm.aspx.cs" Inherits="ZhiFang.WeiXin.WebForm.PayForm" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
	<meta name="viewport" content="width=device-width, initial-scale=1"/>
	<title>微信支付</title>
	<script type="text/javascript">
		//调用微信JS api 支付
		function jsApiCall(){
			//wxJsApiParam,JSON字符串
			WeixinJSBridge.invoke('getBrandWCPayRequest',<%=wxJsApiParam%>,function (res){
				WeixinJSBridge.log(res.err_msg);
	            if(res.err_msg == "get_brand_wcpay_request:ok"){
	            	//给本地缓存打标志
	            	localStorage.setItem("USERORDER_MASK<%=UOFID%>","1");
	                alert("微信支付成功!"); 
	            }else if(res.err_msg == "get_brand_wcpay_request:cancel"){  
	                alert("用户取消支付!");  
	            }else{  
	                alert("支付失败!");  
	            }
	            history.go(-1);
			});
	    }
		function callpay(){
			if (typeof WeixinJSBridge == "undefined"){
				if (document.addEventListener){
					document.addEventListener('WeixinJSBridgeReady', jsApiCall, false);
				}else if (document.attachEvent){
					document.attachEvent('WeixinJSBridgeReady', jsApiCall);
					document.attachEvent('onWeixinJSBridgeReady', jsApiCall);
				}
			}else{
				jsApiCall();
			}
		}     
	</script>
</head>
<body style="font-family:Helvetica Neue,Helvetica,Arial,sans-serif;">
    <form id="form1" runat="server">
    <div style="margin:50px 0;text-align:center;">
    	<div style="padding:10px;">订单ID：<%=UOFID%></div>
    	<div style="padding:10px;">金额：￥<%=total_fee%></div>
    	<div style="padding:10px;margin:10px;background-color:#169ada;color:#ffffff;" onclick="callpay();">立即支付</div>
    </div>
    <asp:Button ID="submit" runat="server" Text="立即支付" OnClientClick="callpay()" 
		style="width:210px;height:50px;border-radius:15px;background-color:#00CD00; 
		border:0px #FE6714 solid;cursor:pointer;color:white;font-size:16px;display:none"/>
    </form>
</body>
</html>