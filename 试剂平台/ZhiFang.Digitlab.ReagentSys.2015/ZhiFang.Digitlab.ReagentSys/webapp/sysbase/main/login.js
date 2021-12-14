$(function() {
	//初始化账户列表
	function initAccountList(){
		var userList = Shell.util.LocalStorage.get(Shell.util.LocalStorage.map.USERLIST),
			userList = userList ? Shell.util.JSON.decode(userList) : [],
			len = userList.length,
			userListHtml = [];
			
		//没有历史账户
		if(len == 0){return;}
		
		//存在历史账号
		$("#account-button").css("left",0);
		$("#account-button").parent().addClass("input-group");
		
		for(var i=0;i<len;i++){
			userListHtml.push('<li data="' + userList[i] + '"><a href="#">' + 
				userList[i].split(",")[0] + '</a></li>');
		}
		
		$("#account-button ul").html(userListHtml.join('<li role="separator" class="divider"></li>'));
		
		//账户选择监听
//		$("#account-button ul li").on(Shell.util.Event.click,function(){
//			var data = $(this).attr("data");
//			var arr = data.split(",");
//			$("#account").val(arr[0]);
//			$("#password").val(arr[1]);
//		});
		
		$("#account-button ul li").on("click", function(){
			var data = $(this).attr("data");
			var arr = data.split(",");
			$("#account").val(arr[0]);
			$("#password").val(arr[1]);
		});
		if(Shell.util.Event.click != "click"){
			$("#account-button ul li").on(Shell.util.Event.click, function(){
				$(this).click();
			});
		}
		
		
		//默认选中第一个
		var first = $("#account-button ul li")[0];
		if(first){first.click();}
	}
	
	//登录按钮
	$("#submit").on(Shell.util.Event.click,function(){
		var account = getAccountValue();
		var password = getPassword();
		
		if(!account || !password){
			showError("账号和密码不能为空！");
			return;
		}else if(account == Shell.Rea.ADMINNAME){
			showError("账号错误！");
			return;
		}
		login(account,password);//登录
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
		
	//显示错误信息
	function showError(msg){
		$("#error").html(msg);
		$("#error").show();
	}
	//隐藏错误信息
	function hideError(){
		$("#error").hide();
	}
	//登录
	function login(account,password){
		var url = Shell.util.Path.ROOT + '/RBACService.svc/RBAC_BA_Login' + 
			'?strUserAccount=' + account + '&strPassWord=' + password;
		
		hideError();//隐藏错误信息
		ShellComponent.mask.submit();
		//获取账户信息
		Shell.util.Server.ajax({
			async:true,
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			if(data == true){
				loginSuccess(account,password);//登录成功
			}else{
				showError("账号或密码错误！");
			}
		});
	}
	//登录成功
	function loginSuccess(account,password){
		//历史用户追加
		var userList = Shell.util.LocalStorage.get(Shell.util.LocalStorage.map.USERLIST),
			userList = userList ? Shell.util.JSON.decode(userList) : [],
			len = userList.length,
			hasUser = false;
			
		for(var i=0;i<len;i++){
			if(userList[i].split(',')[0] == account){
				userList.splice(i,1);
				break;
			}
		}
		userList.unshift(account + ',' + password);
		userList = Shell.util.JSON.encode(userList.slice(0,3));
		Shell.util.LocalStorage.set(Shell.util.LocalStorage.map.USERLIST,userList);
		
		Shell.Rea.onAfterLogin(function(data){
			if(data.success){
				location.href = "index.html?dt=";// + new Date().getTime();
			}else{
				showError(data.msg);
			}
		});
	}
	
	//初始化账户列表
	initAccountList();
});