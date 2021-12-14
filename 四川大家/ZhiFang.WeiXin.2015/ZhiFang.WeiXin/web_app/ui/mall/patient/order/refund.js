$(function () {
	//外部参数
	var params = JcallShell.getRequestParams(true);
	//获取订单信息服务地址
	var USER_ORDER_INFO_URL = JcallShell.System.Path.ROOT + 
		"/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderFormById";
	//退费页面地址
	var REFUND_HTML_URL = "list.html?statusStr=8,9,10,11,12";
	//获取可选退费说明数据服务地址
	var GET_REFUND_MSG_DATA_URL = "../config/refund_msg_data.json";
	//退费服务地址
	var REFUND_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppService.svc/ST_UDTO_OSUserOrderFormRefundU";
	
	//订单信息数据
	var INFO_DATA = null;
	//可选退费说明数据
	var MSG_DATA = null;
	//选中的退费说明DIV
	var CHECKED_MSG_DIV = null;
	//选择的退费说明文字
	var CHECKED_MSG_TEXT = null;
	
	//获取订单数据
	function getInfoData(callback){
		var url = USER_ORDER_INFO_URL;
		var FIELDS = ['Id','Status','UOFCode'];
		url += '?fields=OSUserOrderForm_' + FIELDS.join(',OSUserOrderForm_') + '&id=' + params.ID;
		
        $("#loading-div").modal({ backdrop: 'static', keyboard: false });
        JcallShell.Server.ajax({
        	showError:true,
            url: url
        }, function (data) {
            $("#loading-div").modal("hide");
            callback(data);
        });
	}
	//获取可选退费说明数据
	function getMsgData(callback){
		var url = GET_REFUND_MSG_DATA_URL;
		
        $("#loading-div").modal({ backdrop: 'static', keyboard: false });
        JcallShell.Server.ajax({
        	showError:true,
            url: url
        }, function (data) {
            $("#loading-div").modal("hide");
            callback(data);
        });
	}
	//初始化数据
	function initData(callback){
		//获取订单数据
		getInfoData(function(data){
			if (data.success == true) {
				INFO_DATA = data.value || {};//可选数据
				
				//状态不是"已缴费"，不做退费处理
				if((INFO_DATA.Status + "") != "2"){
					showError("该订单已经不是'已缴费'状态,请查看退费流程！");
					return;
				}
				
				//获取可选退费说明数据
                getMsgData(function(data){
					if (data.success == true) {
						MSG_DATA = data.value || [];
						callback();
		           } else {
		                showError(data.msg);
		            }
				});
			} else {
                showError(data.msg);
            }
		});
	}
	
	//提交按钮处理
	function onSubmitClick(){
		//退费
		onRefund(function(data){
			if(data.success){
				location.href = REFUND_HTML_URL;
			}else{
				alert(data.msg);
			}
		});
	}
	//退费
	function onRefund(callback){
		if(!CHECKED_MSG_TEXT){
			alert("请选择一个退费说明再提交！");
			return;
		}
		if(!INFO_DATA.UOFCode){
			alert("订单号码不存在，请重新刷新页面获取！");
			return;
		}
		var url = REFUND_URL + "?OrderFormCode=" + INFO_DATA.UOFCode + "&MessageStr=" + CHECKED_MSG_TEXT;
		
		alert(url);
		//return;
		
		$("#loading-div").modal({ backdrop: 'static', keyboard: false });
        JcallShell.Server.ajax({
        	showError:true,
            url: url
        }, function (data) {
            $("#loading-div").modal("hide");
            callback(data);
        });
	}
	
	//显示错误信息
    function showError(msg) {
        $("#info").html('<div style="margin:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
    }
    
    //初始化内容
	function initContent(){
		var items = getInfoHtml();
		$("#info").html(items);
		$("#submit-div").show();
		$("#submit-button").on("click",function(){
			onSubmitClick();
		});
		//默认选中第一个退费说明
		var first = $("#info :first");
		if(first){first.click();}
	}
	//更改订单信息内容
	function getInfoHtml(){
		var list = MSG_DATA,
			len = list.length,
			html = [];
			
		for(var i=0;i<len;i++){
			var item = getItemTemplet();
			item = item.replace(/{Msg}/g, list[i].Msg || "");
			html.push(item);
		}
		
		return html.join("");
	}
	//获取订单信息模板
	function getItemTemplet() {
		return '<div class="msg_div_uncheck" onclick="checkMsgDiv(this);">{Msg}</div>';
	}
	//选中退费说明DIV处理
	function checkMsgDiv(div){
		if(CHECKED_MSG_DIV){
			CHECKED_MSG_DIV.removeClass("msg_div_checked");
		}
		CHECKED_MSG_DIV = $(div);
		CHECKED_MSG_DIV.addClass("msg_div_checked");
		CHECKED_MSG_TEXT = CHECKED_MSG_DIV.text();
	}
	window.checkMsgDiv = checkMsgDiv;
    
	//初始化页面
	function initHtml(){
		initData(function(){
			//初始化内容
			initContent();
		});
	}
	
	//初始化页面
	initHtml();
});