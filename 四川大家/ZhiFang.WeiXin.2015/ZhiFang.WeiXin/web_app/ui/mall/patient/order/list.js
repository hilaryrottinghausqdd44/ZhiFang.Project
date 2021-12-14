$(function() {
	//外部参数
	var params = JcallShell.getRequestParams(true);
	//订单列表的默认订单状态
	var STATUSSTR = "";
	if(params.STATUSSTR) STATUSSTR = "" + params.STATUSSTR;

	//获取订单服务地址
	//var GET_USER_ORDER_LIST_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppService.svc/WXAS_BA_SearchOSUserOrderForm";
	var GET_USER_ORDER_LIST_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppService.svc/WXAS_BA_SearchOSUserOrderFormByStatusStr";
	//医生类型枚举
	var USERORDER_TYPE_ENUM = [];
	//获取列表数据
	function getDoctorOrderListData(callback) {
		var url = GET_USER_ORDER_LIST_URL;
		//用户订单状态为已交费
		var statusStr = "";
		if(STATUSSTR) statusStr = "" + STATUSSTR;
		url += '?page=1&limit=20&statusStr=' + statusStr;

		$("#loading-div").modal({
			backdrop: 'static',
			keyboard: false
		});

		var data = {
			page: 1,
			limit: 20,
			statusStr: statusStr
		};
		data = JcallShell.JSON.encode(data);
		//获取数据
		JcallShell.Server.ajax({
			url: url,
			type: 'post',
			data: data
		}, function(data) {
			$("#loading-div").modal("hide");
			callback(data);
		});
	}
	//更改列表内容
	function changeDoctorOrderListHtml(list) {
		var templet = getRowTemplet(),
			defaultPic = JcallShell.System.Path.ROOT + '/Images/dajia/logo.jpg',
			html = [];
		//用户订单编号、医生、患者、消费单编号、订单状态、消费码、备注、市场价格、大家价格、折扣价格、折扣率、实际金额、咨询费、缴费时间、类型值、类型名称
		//string fields = "UFC,DN,UN,OCC,SS,PC,MM,MP,GMP,DP,DT,PE,AP,PT，TI,TN";
		for(var i in list) {
			var row = templet;
			
			var Time = "";
			//开单时间DataAddTime、缴费时间PT、消费完成时间CFT
			if(list[i].SS == '1'){//代缴费
				Time = "开单时间：" + list[i].DataAddTime;
			}else if(list[i].SS == '2'){//已缴费
				Time = "缴费时间：" + list[i].PT;
			}else if(list[i].SS == '4'){//完全使用
				Time = "消费时间：" + list[i].CFT;
			}
			
			row = row.replace(/{UOFCode}/g, list[i].UFC);
			row = row.replace(/{Doctor}/g, list[i].DN || '<span style="color:green;">【自主下单】</span>');
			row = row.replace(/{Patient}/g, list[i].UN);
			row = row.replace(/{Time}/g, Time);
			row = row.replace(/{Id}/g, list[i].Id);
			if (list[i].SS == 2 || list[i].SS == 4) {
			    row = row.replace(/{PC}/g, list[i].PC);
			    row = row.replace(/{ShowPC}/g, "display:block");
			}else{
			    row = row.replace(/{ShowPC}/g, "display:none");
			}
			//订单类型文字和颜色
			//row = row.replace(/{Type}/g, list[i].TN);
			//var typeInfo = null;
			//for(var i in USERORDER_TYPE_ENUM){
    		//	if(USERORDER_TYPE_ENUM[i].Id == list[i].TN){
    		//		typeInfo = USERORDER_TYPE_ENUM[i];
    		//		break;
    		//	}
    		//}
			//var typeColorStyle = "";
			//if(typeInfo){
			//	typeColorStyle = "color:" + typeInfo.BGColor;
			//}
			//row = row.replace(/{typeColorStyle}/g, typeColorStyle);
			
			html.push(row);
		}

		//html.push('<div><div style="">加载更多</div></div>');

		$("#list").html(html.join(""));
	}
	//获取列表行模板
	function getRowTemplet() {
	    var templet =
			'<div class="list-div">' +
				'<div style="float:left;padding-left:5px;">' +
					'<div style="color:#169ada;"><b>订单编号：{UOFCode}</b></div>' +
					//'<div style="{typeColorStyle}"><b>订单类型：{Type}</b></div>' +
					'<div>医生：{Doctor} </div>' +
					'<div>患者：{Patient}</div>' +
					'<div>{Time}</div>' +
				'</div>' + '<div style="float:left;width:100%;">';
	    
	    templet += '<div class="list-div-button" style="float:right;margin-right:75px;{ShowPC}" onclick="ShowPayCode(\'{PC}\')">消费码</div>';

	    templet += '<div class="list-div-button" style="float:right;" onclick="showInfo(\'{Id}\')">详情</div>' +
				'</div>' +
			'</div>';
	    return templet;
	}
	function ShowPayCode(PayCode)
	{
	    var barcode = PayCode;
	    if (!barcode) {
	        alert("消费码不存在，请缴费后再获取！");
	    } else {
	        $("#show-barcode-canvas").JsBarcode(barcode);
	        $("#showBarcodeModal").modal({ backdrop: 'static', keyboard: false });
	    }
	}
	//显示错误信息
	function showError(msg) {
		$("#list").html('<div style="float:left;width:100%;padding:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
	}

	function showInfo(id) {
		//跳转到信息页面
		location.href = "info.html?hasbutton=1&id=" + id +"&statusStr=" + STATUSSTR;
	}
	window.showInfo = showInfo;
	window.ShowPayCode = ShowPayCode;
	
	//是否已阅读并同意用户协议
	function IsConfirmAgreement(callback){
		//是否已阅读并同意用户协议
		var IsReadAgreement = JcallShell.Cookie.get(JcallShell.Cookie.map.IsReadAgreement);
		if(IsReadAgreement == '1') {
			callback();
		} else {
			location.href = "../agreement/index.html";
		}
	}
	//初始化页面
	function initHtml() {
		//是否已阅读并同意用户协议
		IsConfirmAgreement(function(){
			//获取医生类型枚举
			JcallShell.LocalStorage.Enum.getListByClassName('OSUserOrderFormType',function(data){
				USERORDER_TYPE_ENUM = data;
				//获取列表数据
				getDoctorOrderListData(function(data) {
					if(data.success == true) {
						//更改列表内容
						var list = data.value ? (data.value.list || []) : [];
						if(list.length == 0) {
							showError("没有订单信息！");
						} else {
							changeDoctorOrderListHtml(list);
						}
					} else {
						showError(data.msg);
					}
				});
			});
		});
	}
	
	//不同状态的订单切换
	var CHECKED_DIV = null
	$(".div_uncheck").on("click",function(){
		if(CHECKED_DIV){
			CHECKED_DIV.removeClass("div_checked");
		}
		CHECKED_DIV = $(this);
		CHECKED_DIV.addClass("div_checked");
		STATUSSTR = CHECKED_DIV.attr("data");
		initHtml();
	});
	if(STATUSSTR){
		$(".div_uncheck").each(function(){
			if($(this).attr("data") == STATUSSTR){
				$(this).click();
			}
		});
	}else{
		$(".div_uncheck")[0].click();
	}
	
	//initHtml();
});