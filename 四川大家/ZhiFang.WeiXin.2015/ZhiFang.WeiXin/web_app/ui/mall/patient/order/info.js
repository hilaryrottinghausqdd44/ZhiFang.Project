$(function () {
	//外部参数
	var params = JcallShell.getRequestParams(true);
	//支付功能地址
	var PAY_URL = JcallShell.System.Path.ROOT + "/WebForm/PayForm.aspx";
	//支付成功后跳转页面地址
	var PAY_SUCCESS_URL = "list.html?statusStr=2";
	//退费申请功能地址
	var REFUND_URL = "refund.html";
	//查看报告结果功能地址
	var SHOW_REPORT_URL = "../report/list.html";
	//查看退费流程功能地址
	var SHOW_REFUND_PROCESS_URL = "refundStatic.html";
	//查看退费跟踪
	var SHOW_REFUND_TRACK_URL = "refundTrack.html";
	
	
	//获取订单信息服务地址
	var USER_ORDER_INFO_URL = JcallShell.System.Path.ROOT + 
		"/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderFormById";
	//获取订单明细服务地址
	var USER_ORDER_ITEMS_URL = JcallShell.System.Path.ROOT + 
		"/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderItemByHQL";
		
	//订单数据
	var INFO_DATA = null;
	//订单状态信息
	var STATUS_INFO = null;
	//市场价格-总和
	var MARKET_PRICE_COUNT = 0;
	//大家价格-总和
	var GREAT_MASTER_PRICE_COUNT = 0;
	//折扣价格-总和
	var DISCOUNT_PRICE_COUNT = 0;
	
    //获取订单信息
	function getInfoData(callback){
		var url = USER_ORDER_INFO_URL;
		var FIELDS = ['Id','DoctorName','UserName','Status','DataAddTime','UOFCode','PayCode'];
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
	//获取医嘱单明细内容
	function getItemsData(callback){
		var url = USER_ORDER_ITEMS_URL + '?where=osuserorderitem.UOFID=' + params.ID;
		var fields = ['MarketPrice','GreatMasterPrice','DiscountPrice','ItemCName'];
		url += '&fields=OSUserOrderItem_' + fields.join(',OSUserOrderItem_');
		
        JcallShell.Server.ajax({
        	showError:true,
            url: url
        }, function (data) {
            callback(data);
        });
	}
	
	//显示错误信息
    function showError(msg) {
        $("#info").html('<div style="margin:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
    }
	
	//初始化内容
	function initContent(){
		var info = getInfoHtml(INFO_DATA);
		var items = getItemsHtml(INFO_DATA.items);
		
		$("#info").html(info + items);
	}
	//更改订单信息内容
	function getInfoHtml(data){
		var html = getInfoTemplet();
		html = html.replace(/{UOFCode}/g, data.UOFCode);
		html = html.replace(/{Doctor}/g, data.DoctorName || '<span style="color:green;">【自主下单】</span>');
		html = html.replace(/{Patient}/g, data.UserName);
		html = html.replace(/{DataAddTime}/g, data.DataAddTime);
		
		html = html.replace(/{StatusBgColor}/g, STATUS_INFO.BGColor || "");
		html = html.replace(/{StatusColor}/g, STATUS_INFO.BGColor || "");
		html = html.replace(/{StatusName}/g, STATUS_INFO.Name || "");
		
		html = html.replace(/{MarketPriceCount}/g, MARKET_PRICE_COUNT);
		html = html.replace(/{GreatMasterPriceCount}/g, GREAT_MASTER_PRICE_COUNT);
		html = html.replace(/{DiscountPriceCount}/g, DISCOUNT_PRICE_COUNT);
		
		return html;
	}
	//获取订单信息模板
	function getInfoTemplet() {
		var templet =
			'<div style="padding:5px 0;">订单编号：{UOFCode}</div>' +
			'<div style="padding:5px 0;">医生：{Doctor}</div>' +
			'<div style="padding:5px 0;">患者：{Patient}</div>' +
			'<div style="padding:5px 0;">订单时间：{DataAddTime}</div>' +
			'<div style="padding:5px 0;">' +
				'<div style="float:left;width:50%;text-align:center;padding:5px 10px;text-align:center;border:1px solid {StatusBgColor};color:{StatusColor}">状态：{StatusName}</div>' +
    			'<div onclick="onRefresh();" id="refresh-button" class="div-button" style="margin:0;float:left;width:50%;">刷新状态</div>' +
    			'<span style="color:#999999;font-size:9px;">注：如果支付成功后状态没有更改，请手动点击"刷新状态"按钮</span>' +
    		'</div>' +
    		'<div style="padding:5px 0;">' +
    			'<span style="text-align:center;color:red;"><s>市场价格:￥<b style="color:red;">{MarketPriceCount}</b>元</s></span>' +
    		'</div>' +
    		'<div style="padding:5px 0;">' +
    			'<span style="text-align:center;">实际价格:￥<b>{DiscountPriceCount}</b>元</span>' +
    		'</div>';
			
		return templet;
	}
	
	//更改医嘱单明细
	function getItemsHtml(list){
		var len = (list || []).length,
			html = [];
			
		html.push('<table class="basic_table" style="margin:10px 0;">');
		html.push('<thead><th style="width:70%;">套餐名称</th><th>市场价格</th></thead>');
		html.push('<tbody>');
		for(var i=0;i<len;i++){
			var item = getItemTemplet();
			var data = list[i];
			item = item.replace(/{Sort}/g, i + 1);
			item = item.replace(/{ItemName}/g, data.ItemCName || "");
			item = item.replace(/{MarketPrice}/g, data.MarketPrice);
			item = item.replace(/{GreatMasterPrice}/g, data.GreatMasterPrice);
			item = item.replace(/{DiscountPrice}/g, data.DiscountPrice);
			html.push(item);
		}
		html.push('</tbody>');
		html.push('</table>');
		
		return html.join("");
	}
	//获取医嘱单明细模板
	function getItemTemplet() {
		//市场价格,大家价格,折扣价格
	    var templet =
           '<tr>' +
               '<td rowspan="2">{Sort}.   {ItemName}</td>' +
               '<td >市场价格:{MarketPrice}</td>' +
           '</tr>' +
           '<tr>' +
               '<td >实际价格:{DiscountPrice}</td>' +
           '</tr>';
			
		return templet;
	}
	
	//初始化数据
	function initData(callback){
		$("#loading-div").modal({ backdrop: 'static', keyboard: false });
		//获取医嘱单数据
		getInfoData(function(data){
			if (data.success == true) {
				INFO_DATA = data.value || {};//医嘱单数据
				//获取医嘱单状态信息
				JcallShell.LocalStorage.Enum.getInfoById('UserOrderFormStatus',INFO_DATA.Status,function(info){
					STATUS_INFO = info;//医嘱单状态信息
					//获取明细数据
	                getItemsData(function(data){
						if (data.success == true) {
							INFO_DATA.items = (data.value || {}).list || [];
							$("#loading-div").modal("hide");
							callback();
			            } else {
			            	$("#loading-div").modal("hide");
			                showError(data.msg);
			            }
					});
				});
            } else {
            	$("#loading-div").modal("hide");
                showError(data.msg);
            }
		});
	}
	function changeData(){
		//总价格计算
		var list = INFO_DATA.items || [],
			len = list.length;
			
		MARKET_PRICE_COUNT = 0;
		GREAT_MASTER_PRICE_COUNT = 0;
		DISCOUNT_PRICE_COUNT = 0;
			
		for(var i=0;i<len;i++){
    		MARKET_PRICE_COUNT += parseFloat(list[i].MarketPrice);
    		GREAT_MASTER_PRICE_COUNT += parseFloat(list[i].GreatMasterPrice);
    		DISCOUNT_PRICE_COUNT += parseFloat(list[i].DiscountPrice);
    	}
	}
	
    //初始化按钮
    function initButtons(){
    	if(!params.HASBUTTON) return;
    	
    	$("#submit-div").show();//显示功能按钮栏
		var status = INFO_DATA.Status + "";
		switch(status) {
			case "1": //未缴费订单:“订单取消“+”支付”
				onChangePayStatus();
				$("#cancel-button").show();
				$("#pay-button").show();
				break;
			case "2": //已缴费订单:“退费”+“查看消费码”
				$("#refund-button").show();
				$("#show-paycode-button").show();
				break;
			case "4": //已消费订单:已消费订单详细页：“查看退费流程”+“查看报告”
				$("#show-refund-process-button").show();
				$("#show-report-button").show();
				break;
			case "8": 
			case "9": 
			case "10": 
			case "11": 
			case "12": //退费订单:“退费跟踪”
				$("#show-refund-track-button").show();
				break;
			default:
				$("#submit-div").hide();
				break;
		}
    }
    
    //一秒一次的次数
    var TIMES_ONE_COUNT = 5;
    //五秒一次的次数
    var TIMES_FIVE_COUNT = 3;
    //一秒一次当前循环次数
    var TIME_ONE_NUM = 0;
    //五秒一次当前循环次数
    var TIME_FIVE_NUM = 0;
    //处理结束
    var PAY_IS_END = false;
    //处理的时间秒数
    var PAY_TIMES = 0;
    //支付状态的更改
    function onChangePayStatus(){
    	var mask = JcallShell.LocalStorage.get(JcallShell.LocalStorage.UserOrder.map.PAYMASK + params.ID);
    	if(mask){
    		isPayed();
    	}
    }
    //判断是否已经支付
    function isPayed(){
    	changePayMaskMsg();
    	onShowPayMask();
    	isPayedOne();
    }
    //1秒循环处理
    function isPayedOne(){
    	setTimeout(function(){
    		//1秒循环结束，转5秒循环，退出
    		if(TIME_ONE_NUM >= TIMES_ONE_COUNT){
    			isPayedFive();//5秒循环处理
    			return;
    		}
    		
			TIME_ONE_NUM++;
			getInfoData(function(data){
				if (data.success == true) {
					var info = data.value || {};//医嘱单数据
					if((info.Status + "") == "2"){
						ISEND = true;
						onHidePayMask();//隐藏支付保护层
					}
				} else {
					isPayedOne();
	            }
			});
		},1000);
    }
    //5秒循环处理
	function isPayedFive(){
		setTimeout(function(){
    		//5秒循环结束，退出
    		if(TIME_FIVE_NUM >= TIMES_FIVE_COUNT){
    			//标志置空
				ISEND = true;
				onHidePayMask();
    			return;
    		}
    		
    		TIME_FIVE_NUM++;
			getInfoData(function(data){
				if (data.success == true) {
					var info = data.value || {};//医嘱单数据
					if((info.Status + "") == "2"){
						ISEND = true;
						onHidePayMask();//隐藏支付保护层
					}
				} else {
					isPayedFive();
	            }
			});
		},5000);
	}
	//开启支付保护层
	function onShowPayMask(){
		$("#paying-div").modal({ backdrop: 'static', keyboard: false });
	}
	//隐藏支付保护层
	function onHidePayMask(){
		//标志置空
		JcallShell.LocalStorage.remove(JcallShell.LocalStorage.UserOrder.map.PAYMASK + params.ID);
		$("#cancel-button").hide();
		$("#pay-button").hide();
		$("#paying-div").modal("hide");
		location.href = PAY_SUCCESS_URL;
	}
	//更新支付保护层信息
	function changePayMaskMsg(){
		PAY_TIMES++;
		$("#paying-msg").html("支付处理中：" + PAY_TIMES + "秒");
		setTimeout(function(){
    		if(!PAY_IS_END){
    			changePayMaskMsg();
    		}
    	},1000);
	}
	
    //初始化页面
	function initHtml(){
		initData(function(){
			//数据处理
			changeData();
			//初始化内容
			initContent();
			//初始化按钮
			initButtons();
		});
	}
	window.onRefresh = initHtml;
	
	//取消订单
	$("#cancel-button").on("click",function(){
		alert("取消订单");
	});
	//支付
	$("#pay-button").on("click",function(){
		location.href = PAY_URL + "?UOFID=" + params.ID + "&v=1006";
	});
	//退费
	$("#refund-button").on("click",function(){
		location.href = REFUND_URL + "?id=" + params.ID;
	});
	//查看消费码
	$("#show-paycode-button").on("click",function(){
		var barcode = INFO_DATA.PayCode;
		if(!barcode){
			alert("消费码不存在，请缴费后再获取！");
		}else{
			$("#show-barcode-canvas").JsBarcode(barcode);
			$("#showBarcodeModal").modal({ backdrop: 'static', keyboard: false });
		}
	});
	//查看报告
	$("#show-report-button").on("click",function(){
		location.href = SHOW_REPORT_URL + "?PAYCODE=" + INFO_DATA.PayCode;
	});
	//查看退费流程
	$("#show-refund-process-button").on("click",function(){
		location.href = SHOW_REFUND_PROCESS_URL + "?UOFCode=" + INFO_DATA.UOFCode;
	});
	//查看退费跟踪
	$("#show-refund-track-button").on("click",function(){
		location.href = SHOW_REFUND_TRACK_URL + "?UOFCode=" + INFO_DATA.UOFCode;
	});
	
	//初始化页面
	initHtml();
});