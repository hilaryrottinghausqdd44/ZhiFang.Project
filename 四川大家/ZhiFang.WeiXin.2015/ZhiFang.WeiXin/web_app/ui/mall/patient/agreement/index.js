$(function() {
	//用户服务协议地址
	var AGREEMENT_URL = 'http://mp.weixin.qq.com/s/yN83HBhYzrpFqlsK4SVn8g';
	//阅读并同意用户协议
	var READ_AGREEMENT_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppService.svc/WXAS_BA_UserReadAgreement";
	
	//确认用户服务协议
	function IsConfirmAgreement(callback) {
		//是否已阅读并同意用户协议
		var IsReadAgreement = JcallShell.Cookie.get(JcallShell.Cookie.map.IsReadAgreement);
		if(IsReadAgreement == '1') {
			callback();
		} else {
			var height = $(window).height() - 50;
			$("#agreement-iframe").height(height);
			$("#agreement-iframe").attr("src", AGREEMENT_URL);
			$("#Agreement").show();
		}
	}
	//页面大小变化监听
	$(window).resize(function() {
		var height = $(window).height() - 50;
		$("#agreement-iframe").height(height);
	});
	//是否同意用户协议
	$("#IsConfirm").on("click",function(){
		var checked = $(this).is(":checked");
		if(checked){
			$("#IsReadAgreement").removeAttr("disabled");
		}else{
			$("#IsReadAgreement").attr({"disabled":"disabled"});
		}
	});
	//用户协议同意按钮
	$("#IsReadAgreement").on("click",function(){
		$("#IsReadAgreementModal").modal({ backdrop: 'static', keyboard: false });
	});
	//同意用户协议按钮
	$("#IsReadAgreementModal-button").on("click",function(){
		onReadAgreement();
	});
	//阅读并同意用户协议
	function onReadAgreement(){
		var url = READ_AGREEMENT_URL;
		JcallShell.Server.ajax({
			url: url,
			showError: true
		}, function(data) {
			if(data.success) {
				$("#IsReadAgreementModal").modal("hide");
				history.go(-1);
			} else {
				alert(data.msg);
			}
		});
	}

	//初始化首页信息
	(function() {
		//确认用户服务协议
		IsConfirmAgreement(function() {
			history.go(-1);
		});
	})();
});