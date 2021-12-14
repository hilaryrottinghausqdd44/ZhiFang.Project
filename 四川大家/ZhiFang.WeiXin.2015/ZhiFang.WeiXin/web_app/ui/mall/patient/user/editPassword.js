$(function () {
	//获取验证码
	var GET_CODE_URL = JcallShell.System.Path.ROOT + 
		"/ServerWCF/ZhiFangWeiXinAppService.svc/GetVerificationCode";
	//修改密服务
	var EDIT_PASSWORD_URL = JcallShell.System.Path.ROOT + 
		"/ServerWCF/ZhiFangWeiXinAppService.svc/WXAS_BA_ChangePasswordByVerificationCode";
	//获取校验码间隔时间（秒）
	var CODE_TIMES = 6;
	//获取验证码按钮
	$("#getCode").on("click",function(){
		getCode(function(){
			changeCodeInfo(CODE_TIMES);
		});
	});
	//校验码信息变化
	function changeCodeInfo(lastTimes){
		if(lastTimes <= 0){
			$("#getCode").removeAttr("disabled");
			$("#getCode").parent().removeClass("butDisabled");
			$("#getCode").html("获取验证码");
			return;
		}
		$("#getCode").html(lastTimes + "秒后重新获取");
		$("#getCode").attr("disabled","disabled");
		$("#getCode").parent().addClass("butDisabled");
		setTimeout(function(){
			changeCodeInfo(--lastTimes);
		},1000);
	}
	//确定按钮
	$("#submit").on("click",function(){
		onSubmit(function(){
			location.href = "../login/login.html";
		});
	});
	
	//获取验证码
	function getCode(callback){
		var MobileCode = $("#phone").val();
		if(!MobileCode){
			$.toptip('请先输入正确的手机号码！', 'error');
			return;
		}
		var url = GET_CODE_URL + '?MobileCode=' + MobileCode;
		$("#loading-div").modal({ backdrop: 'static', keyboard: false });
		JcallShell.Server.ajax({
			url:url,
			showError:true
		},function(data){
			$("#loading-div").modal("hide");
			if(data.success){
				callback();
			}else{
				$.toptip(data.msg, 'error');
			}
		});
	}
	//修改密码信息提交后台
	function onSubmit(callback){
		hideError();//隐藏错误信息
	    var data = {
	    	VerificationCode:$("#code").val(),
	    	newPassword:$("#password").val()
	    };
	    var error = [];
	    if(!data.VerificationCode){
	    	error.push("请填写校验码！");
	    }
	    if(!data.newPassword){
	    	error.push("请填写新的密码！");
	    }
	    if(error.length > 0){
	    	showError(error.join("</br>"));
	    	return;
	    }
	    
	    data = JcallShell.JSON.encode(data);
	    $("#loading-div").modal({ backdrop: 'static', keyboard: false });
	    JcallShell.Server.ajax({
			url:EDIT_PASSWORD_URL,
			type:'post',
			showError:true,
            data:data
		},function(data){
			$("#loading-div").modal("hide");
			if(data.success){
				callback();
			}else{
				$.toptip(data.msg, 'error');
			}
		});
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
});