$(function () {
	//外部参数
	var params = JcallShell.getRequestParams(true);
	//用户绑定账号服务地址
    var BIND_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppDoctService.svc/WXADS_DoctorAccountBind";
    
    //用户信息
	var UserInfo = JcallShell.LocalStorage.User.getAccount(true);
	$("#user-head-image").width("100px");
	if(UserInfo){
		$("#user-head-image").attr("src",UserInfo.BWeiXinAccount_HeadImgUrl);
		$("#user-name").html(UserInfo.BWeiXinAccount_Name + '(' + UserInfo.BWeiXinAccount_UserName + ')');
	}
	
	//版本信息
	$("#version").html("UI版本:" + JcallShell.System.JS_VERSION);
	
    //提交按钮
    $("#submit").on("click", function () {
    	var account = getAccount();
        var password = getPassword();
		
		if(!account){
			showError("手机号不能为空！");
            return;
		}
        if (!password) {
            showError("密码不能为空！");
            return;
        }
        bind(account,password);//首次绑定
    });
	
	//获取账号
    function getAccount() {
        var value = $("#account").val(),
			value = value ? $.trim(value) : "";
        return value;
    }
    //获取密码
    function getPassword() {
        var value = $("#password").val(),
			value = value ? $.trim(value) : "";
        return value;
    }

    //显示错误信息
    function showError(msg) {
        $("#error").html(msg);
        $("#error").show();
    }
    //隐藏错误信息
    function hideError() {
        $("#error").hide();
    }
    //绑定
    function bind(account,password) {
        var url = BIND_URL + '?AccountCode=' + account + '&password=' + password;

        hideError();//隐藏错误信息

        $("#loading-div").modal({ backdrop: 'static', keyboard: false });
		
		JcallShell.Server.ajax({
            url: url
        }, function (data) {
            $("#loading-div").modal("hide");
            if (data.success == true) {
                bindSuccess();//绑定成功
            } else {
                showError("用户绑定账号失败！");
            }
        });
    }
    //绑定成功处理
    function bindSuccess() {
    	//主页默认为1
    	//JcallShell.LocalStorage.Home.setButtonIndex(1);
        //跳转到主页
        //location.href = "../home/home.html";
        $("#page").hide();
        $("#info").html("账号绑定成功<br>请重新从医生入口进入");
        $("#info").show();
    }
});