$(function () {
	//获取每天的咨询费信息，加载数据时，当page=1时，返回已结算数据，否则不返回
	var GET_CHARGE_INFO_DAY_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppDoctService.svc/WXADS_BA_GetDoctorChargeInfoDay";
	
	//咨询费数据
	var CHARGE_DATA = null;
	//当前页码
	var PAGE = 1;
	//每页数量
	var LIMIT = 180;
	
	//获取咨询费信息
	function getChargeInfoDay(callback){
		var url = GET_CHARGE_INFO_DAY_URL + "?page=" + PAGE + "&limit=" + LIMIT;
		$("#loading-div").modal({ backdrop: 'static', keyboard: false });
		JcallShell.Server.ajax({
			url:url
		},function(data){
			setTimeout(function(){$("#loading-div").modal("hide");},500);
			callback(data);
		});
	}
	
	//查询数据
	function onSearch(){
		//获取列表数据
		getChargeInfoDay(function(data) {
			if(data.success == true) {
				//获取咨询费信息
				CHARGE_DATA = data.value || {};
				//更改页面内容
				changeHtml();
			} else {
				showError(data.msg);
			}
		});
	}
	//更改页面内容
	function changeHtml(){
		//更改余额和已结算总额信息
		changeInfoHtml();
		//更改未结算列表信息
		changeLastMoneyHtml();
		//更改已结算列表信息
		changeExtractedMoneyHtml();
	}
	//更改余额和已结算总额信息
	function changeInfoHtml(){
		var OSUserConsumerDayList = CHARGE_DATA.OSUserConsumerDayList || [],
			OSDoctorBonusList = CHARGE_DATA.OSDoctorBonusList || [],
			LastMoney = 0,
			ExtractedMoney = 0;
			
		for(var i in OSUserConsumerDayList){
			LastMoney += parseFloat(OSUserConsumerDayList[i].Price);
		}
		for(var i in OSDoctorBonusList){
			ExtractedMoney += parseFloat(OSDoctorBonusList[i].Price);
		}
		
		$("#LastMoney").html(Math.round(LastMoney));
		$("#ExtractedMoney").html(Math.round(ExtractedMoney));
		$("#AllMoney").html(Math.round(LastMoney + ExtractedMoney));
	}
	//更改未结算列表信息
	function changeLastMoneyHtml(){
		var list = CHARGE_DATA.OSUserConsumerDayList || [],
			html = [];
			
		for(var i in list){
			var info = list[i];
			var row = 
			'<div style="padding:15px;border-bottom:1px dashed #e0e0e0;">' +
				'<div>' +
					'<span style="color:#e0e0e0;">' + info.DateTime + '</span>' +
					'<span style="margin-left:10px;color:green;"> ' + info.Count + '单</span>' +
					'<span style="margin-left:10px;color:green;"> +' + Math.round(parseFloat(info.Price)) +'</span>' +
				'</div>' +
				'<div style="float:right;margin-top:-23px;width:50px;padding:2px 10px;border:1px solid orangered;border-radius:2px;"' +
					'onclick="showLastMoneyInfo(\'' + info.DateTime + '\')">详情</div>' +
			'</div>';
			html.push(row);
		}
		$("#div_1_list").html(html.join(""));
	}
	//更改已结算列表信息
	function changeExtractedMoneyHtml(){
		var list = CHARGE_DATA.OSDoctorBonusList || [],
			html = [];
			
		for(var i in list){
			var info = list[i];
			var row = 
			'<div style="padding:10px;border-bottom:1px dashed #e0e0e0;">' +
				'<div style="padding:5px 0;color:green;">结算单号：' + info.BonusCode + '</div>' + 
				'<div style="padding:5px 0;">' +
					'<span style="color:#e0e0e0;">' + info.DT + '</span>' +
					'<span style="margin-left:10px;color:red;">积分：' + Math.round(parseFloat(info.Price)) +'</span>' +
				'</div>' +
//				'<div style="float:right;margin-top:-23px;width:50px;padding:2px 10px;border:1px solid orangered;border-radius:2px;"' +
//					'onclick="showExtractedMoneyInfo(\'{Id}\')">详情</div>' +
			'</div>';
			html.push(row);
		}
		$("#div_2_list").html(html.join(""));
	}
	
	//查看未结算详情
	function showLastMoneyInfo(datetime) {
		//跳转到信息页面
		location.href = "../pay/list.html?StartDay=" + datetime + "&EndDay=" + datetime;
	}
	window.showLastMoneyInfo = showLastMoneyInfo;
	//查看已结算详情
	function showExtractedMoneyInfo(datetime) {
		//跳转到信息页面
		//location.href = "../pay/list.html?start=" + datetime + "&end=" + datetime;
	}
	window.showExtractedMoneyInfo = showExtractedMoneyInfo;
	
	//显示错误信息
	function showError(msg) {
		$("#div_1_list").html('<div style="padding:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
		$("#div_2_list").html('<div style="padding:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
	}
	
	//初始化页面
	function initHtml() {
//		var doctor = JcallShell.LocalStorage.User.getDoctor(true);
//		$("#BankAddress").html("开户银行：" + (doctor.BankAddress || "没维护"));
//		$("#BankAccount").html("银行卡号：" + (doctor.BankAccount || "没维护"));
		
		//未结算、已结算页签切换出来
		$("#tab_div_1").on("click",function(){
			$("#tab_div_1").addClass("checked");
			$("#tab_div_2").removeClass("checked");
			$("#div_2").hide();
			$("#div_1").show();
		});
		$("#tab_div_2").on("click",function(){
			$("#tab_div_2").addClass("checked");
			$("#tab_div_1").removeClass("checked");
			$("#div_1").hide();
			$("#div_2").show();
		});
		$("#tab_div_1").click();
		
		//查询数据
		onSearch();
	}
	
	//初始化页面
	initHtml();
});