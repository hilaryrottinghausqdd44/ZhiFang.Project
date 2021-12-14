$(function () {
	//登录服务地址
    var LOGIN_URL = JcallShell.System.Path.UI + "/service/login.json";//"/ServerWCF/RBACService.svc/RBAC_BA_Login";
    //外部参数
	var params = JcallShell.getRequestParams(true);
	
	//医生、病人标志
	var TYPE = null;
	switch(params.TYPE){
		case '1' : TYPE = "doctor";break;
		case '2' : TYPE = "patient";break;
		default : break;
	}
	//本地缓存中存储用户类型
	JcallShell.LocalStorage.User.setType(TYPE);
	
    //初始化账户列表
    function initAccountList() {
        var userList = JcallShell.LocalStorage.User.getAccountList(),
			len = userList.length,
			userListHtml = [];

        //没有历史账户
        if (len == 0) { return; }

        //存在历史账号
        $("#account-button").css("left", 0);
        $("#account-button").parent().addClass("input-group");

        for (var i = 0; i < len; i++) {
            userListHtml.push('<li data="' + userList[i] + '"><a href="#">' +
				userList[i].split(",")[0] + '</a></li>');
        }

        $("#account-button ul").html(userListHtml.join('<li role="separator" class="divider"></li>'));

        $("#account-button ul li").on("click", function () {
            var data = $(this).attr("data");
            var arr = data.split(",");
            $("#account").val(arr[0]);
            $("#password").val(arr[1]);
        });

        //默认选中第一个
        var first = $("#account-button ul li")[0];
        if (first) { first.click(); }
    }

    //登录按钮
    $("#submit").on("click", function () {
        var account = getAccountValue();
        var password = getPassword();

        if (!account || !password) {
            showError("账号和密码不能为空！");
            return;
        } else if (account == JcallShell.System.ADMINNAME) {
            showError("账号错误！");
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
        var url = LOGIN_URL + '?strUserAccount=' + account + '&strPassWord=' + password;

        hideError();//隐藏错误信息

        $("#loading-div").modal({ backdrop: 'static', keyboard: false });
        JcallShell.Server.ajax({
            url: url
        }, function (data) {
            $("#loading-div").modal("hide");
            if (data.success == true) {
                loginSuccess(account, password);//登录成功
            } else {
                showError("账号或密码错误！");
            }
        });
    }
    //登录成功
    function loginSuccess(account, password) {
        //历史用户追加
        JcallShell.LocalStorage.User.addAccount(account, password);
        //跳转到主页
        location.href = "home.html";
    }

    //初始化账户列表
    initAccountList();
});