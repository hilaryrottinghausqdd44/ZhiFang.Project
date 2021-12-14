$(function() {
	//登录判断
	if(JShell.System.isLogged()){
		window.location.href = "index.html";
	}else{
		$("#body").show();
	}
	
	//获取账户+密码
	function getData(){
		var account = $("#account").val();
		var password = $("#password").val();
		var result = null;
		
		account = account ? account.trim() : "";
		password = password ? password.trim() : "";
		
		$("#MsgDiv").hide();
		
		if(!account || !password){
			$("#error").text("账户或密码不能为空");
			$("#MsgDiv").show();
		}else{
			result = {
				account:account,
				password:password
			};
		}
		
		return result;
	}
	//用户登录
	function login(account,password){
		var url = JShell.System.Path.ROOT + '/RBACService.svc/RBAC_BA_Login';
		url += "?strUserAccount=" + account + "&strPassWord=" + password;
		
		//console.log("账号：" + account +"；密码：" + password);
		
		$.ajax({
			url:url,
			cache:false,
			dataType:'json',
			success:function(data) {
				if(data == true){
					JcallShell.System.setLogged(true);
					window.location.href = "index.html";
				}else{
					$("#error").text("账户或密码错误！");
					$("#MsgDiv").show();
				}  
			},  
			error : function() {
				alert("异常！");
			}
		});
	}
	
	//登录按钮点击处理
	$("#submit").on("click",function(){
		var data = getData();
		
		if(!data) return;
		
		login(data.account,data.password);
	});
	
	//账户+密码回车键监听
	$("#account").on("keydown",function(){
		if (event.keyCode == "13") {//keyCode=13是回车键
            $('#submit').click();
        }
	});
	$("#password").on("keydown",function(){
		if (event.keyCode == "13") {//keyCode=13是回车键
            $('#submit').click();
        }
	});
	setTimeout(function(){$("#account").focus();},300);
});