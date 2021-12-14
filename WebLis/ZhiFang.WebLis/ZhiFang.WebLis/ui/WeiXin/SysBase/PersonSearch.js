$(function () {
	//登录服务地址
    //var LOGIN_URL = JcallShell.System.Path.UI + "/service/login.json";//"/ServerWCF/RBACService.svc/RBAC_BA_Login";

 

    //登录按钮
    $("#submit").on("click", function () {
        var account = getAccountValue();
        var password = getPassword();

        if (!account ) {
            showError("姓名不能为空！");
            return;
        } 
        if ( !password) {
            showError("条码号不能为空！");
            return;
        } 
        login(account, password);//登录
    });

    //获取账户
    function getAccountValue() {
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
    //登录
    function login(account, password) {
        $("#loading-div").modal({ backdrop: 'static', keyboard: false });
        var url = '../ReportForm/PersonSearch.html?strUserAccount=' + account + '&strBarcode=' + password;
        location.href = url;
        hideError();//隐藏错误信息
    }
});