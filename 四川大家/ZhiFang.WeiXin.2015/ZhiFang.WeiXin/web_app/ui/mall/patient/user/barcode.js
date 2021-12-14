$(function () {
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
	//是否已阅读并同意用户协议
	IsConfirmAgreement(function(){
		//我的账户信息
		var MyInfo = JcallShell.LocalStorage.User.getAccount(true);
		var barcode = JcallShell.String.toUtf8(MyInfo.BWeiXinAccount_WeiXinAccount);
		$("#show-barcode").html("");
		$("#show-barcode").qrcode({
			render: "table",
			width: 200,
			height: 200,
			text: barcode
		});
		$("#show-barcode-value").html(barcode);
		
		$("#Name").html("<b>" + (MyInfo.BWeiXinAccount_Name || "") + "</b>");
		$("#UserName").html("<b>(" + (MyInfo.BWeiXinAccount_UserName || "") + ")</b>");
	});
});