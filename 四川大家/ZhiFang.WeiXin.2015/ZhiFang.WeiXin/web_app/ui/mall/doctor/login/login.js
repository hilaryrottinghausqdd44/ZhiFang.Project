$(function () {
	//外部参数
	var params = JcallShell.getRequestParams(true);
	//账号信息服务
    var ACCOUNT_INFO_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppService.svc/WXAS_BA_GetPatientInformation";
    //登录服务地址
    var LOGIN_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppDoctService.svc/WXADS_BA_Login";
    
    //用户信息
	var UserInfo = JcallShell.LocalStorage.User.getAccount(true) || {};
	$("#user-head-image").width("100px");
	$("#user-head-image").attr("src",UserInfo.BWeiXinAccount_HeadImgUrl || '');
	var userName = (UserInfo.BWeiXinAccount_Name || '');
	if(UserInfo.BWeiXinAccount_UserName){
		userName += '(' + (UserInfo.BWeiXinAccount_UserName || '') + ')'; 
	}
	$("#user-name").html(userName);
	
	//版本信息
	$("#version").html("UI版本:" + JcallShell.System.JS_VERSION);
	
    //登录按钮
    $("#submit").on("click", function () {
        var password = getPassword();

        if (!password) {
            showError("密码不能为空！");
            return;
        }
        login(password);//登录
    });
    //忘记密码
    $("#forgetPassword").on("click",function(){
    	location.href = '../user/editPassword.html';
    });
	
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
    //登录
    function login(password) {
        var url = LOGIN_URL + '?password=' + password;

        hideError();//隐藏错误信息

        $("#loading-div").modal({ backdrop: 'static', keyboard: false });
		JcallShell.Server.ajax({
            url: url
        }, function (data) {
            $("#loading-div").modal("hide");
            if (data.success == true) {
                loginSuccess();//登录成功
            } else {
                showError("账号或密码错误！");
            }
        });
    }
    //登录成功
    function loginSuccess() {
    	//主页默认为1
    	JcallShell.LocalStorage.Home.setButtonIndex(1);
        //跳转到主页
        location.href = "../home/home.html";
    }
});