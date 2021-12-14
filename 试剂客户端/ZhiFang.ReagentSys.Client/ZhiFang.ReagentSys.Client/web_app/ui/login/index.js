$(function () {
	//外部参数
	var params = JcallShell.getRequestParams(true);
    //登录服务地址
    var LOGIN_URL = JcallShell.System.Path.ROOT + "/RBACService.svc/RBAC_BA_Login";
    
    //登录按钮
    $("#submit").on("click", function () {
        var account = getAccountValue();
		var password = getPassword();
		
		if(!account || !password){
			JcallShell.Msg.error("账号和密码不能为空！");
			return;
		}
		login(account,password);//登录
    });
    //忘记密码
    $("#forgetPassword").on("click",function(){
    	//location.href = '../user/editPassword.html';
    });
	
    //获取账户
	function getAccountValue(){
		var value = $("#account").val(),
			value = value ? $.trim(value) : "";
		return value;
	}
	//获取密码
	function getPassword(){
		var value = $("#password").val(),
			value = value ? $.trim(value) : "";
		return value;
	}
	
    //登录
    function login(account,password) {
        var url = LOGIN_URL + '?strUserAccount=' + account + '&strPassWord=' + password;

        $.showLoading("正在加载中...");
		JcallShell.Server.ajax({
            url: url
        }, function (data) {
            $.hideLoading();
            if (data == true) {
                loginSuccess(account,password);//登录成功
            } else {
                JcallShell.Msg.error("账号或密码错误！");
            }
        });
    }
    //登录成功
    function loginSuccess(account,password) {
    	//记录账户信息
		JcallShell.LocalStorage.User.setAccount({
    		UserName:JcallShell.Cookie.get(JcallShell.Cookie.map.USERNAME),
    		AccountName:JcallShell.Cookie.get(JcallShell.Cookie.map.ACCOUNTNAME),
    		DeptName:JcallShell.Cookie.get(JcallShell.Cookie.map.DEPTNAME)
    	});
    	
    	//记录历史账户
    	JcallShell.LocalStorage.User.setAccountList(account,password);
    	//主页默认为1
    	JcallShell.LocalStorage.Home.setButtonIndex(1);
        //跳转到主页
        location.href = "../home/index.html";
    }
    
    //初始化账户列表
	function initAccountList(){
		var list = JcallShell.LocalStorage.User.getAccountList(),
			len = list.length,
			html = [];
			
		//没有历史账户
		if(len == 0){return;}
		
		//存在历史账号
		$("#account-button").css("left",0);
		$("#account-button").parent().addClass("input-group");
		
		for(var i=0;i<len;i++){
			html.push('<li data="' + list[i] + '"><a href="#">' + list[i].split(",")[0] + '</a></li>');
		}
		
		$("#account-button ul").html(html.join('<li role="separator" class="divider"></li>'));
		
		$("#account-button ul li").on("click", function(){
			var data = $(this).attr("data");
			var arr = data.split(",");
			$("#account").val(arr[0]);
			$("#password").val(arr[1]);
		});
		
		//默认选中第一个
		var first = $("#account-button ul li")[0];
		if(first){first.click();}
	}
    //初始化账户列表
	initAccountList();
});