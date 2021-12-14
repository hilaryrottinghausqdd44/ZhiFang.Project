layui.extend({
	uxutil: 'ux/util',
	uxform: 'ux/form',
	zfAccount: 'ux/zf/account'
}).use(['uxform','uxutil','zfAccount'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxform = layui.uxform,
		zfAccount = layui.zfAccount;
		
	//获取验证码图片服务地址
	var GET_VALIDATE_CODE_IMG_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/GetValidateCode';
	//验证码识别服务
	var CHECK_VALIDATE_CODE_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/CheckValidateCode';
	//请求登入服务地址
	var LOGIN_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_BA_Login';
	//历史用户列表
	var USER_LIST = [];
	
	//验证码加载
	$("#ValidateCodeImg").attr("src",GET_VALIDATE_CODE_IMG_URL + '?t=' + new Date().getTime());
	$("#ValidateCodeReferesh").on("click",function(){
		$("#ValidateCodeImg").attr("src",GET_VALIDATE_CODE_IMG_URL + '?t=' + new Date().getTime());
	});
	
	//用户名回车监听
	$("#account").keypress(function(e){
		if(e.which == 13){
			$("#Button_Login").click();
		}
	});
	//密码回车监听
	$("#password").keypress(function(e){
		if(e.which == 13){
			$("#Button_Login").click();
		}
	});
	//注册
	$("#reg_button").on("click",function(){
		var url = "reg.html?t=" + new Date().getTime();
		layer.open({
			title:'注册',
			type:2,
			content:url,
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['100%','100%']
		});
	});
	
	//提交
	$("#Button_Login").on("click",function(obj){
		//onLogin();
		//输入项不为空判定
		if(isNotEmpety()){
			//先验证码识别，通过后再提交登录
			var ValidateCode = $("#ValidateCode").val();
			onCheckValidateCode(ValidateCode,function(){
				onLogin();
			});
		}
	});
	//验证码识别
	function onCheckValidateCode(value,callback){
		layer.load();
		uxutil.server.ajax({
			url:CHECK_VALIDATE_CODE_URL,
			cache:false,
			type:'post',
			data:JSON.stringify({
				ValidateCode:value
			})
		},function(data){
			layer.closeAll('loading')
			if(data.success){
				callback();
			}else{
				if(data.ResultCode == '105'){//验证超次数
					layer.msg(data.msg,{time:5000});
				}else{
					layer.msg(data.msg);
				}
			}
		},true);
	};
	//输入项不为空判定
	function isNotEmpety(){
		var accounttagName = $("#account")[0].tagName == 'INPUT',
			account = accounttagName ? $("#account").val() : $("#account").next().first().find('input').val(),
			account = account ? $.trim(account) : '',
			password = $("#password").val(),
			ValidateCode = $("#ValidateCode").val();
		
		if(!account || !password){
			layer.msg('账号密码不能为空！',{time:1000});
			return false;
		}
		if(!ValidateCode){
			layer.msg('验证码不能为空！',{time:1000});
			return false;
		}
		return true;
	};
	//登录提交
	function onLogin(){
		var accounttagName = $("#account")[0].tagName == 'INPUT',
			account = accounttagName ? $("#account").val() : $("#account").next().first().find('input').val(),
			account = account ? $.trim(account) : '',
			password = $("#password").val(),
			ValidateCode = $("#ValidateCode").val();
		
		layer.load();
		//请求登入接口
		uxutil.server.ajax({
			url:LOGIN_URL,
			cache:false,
			data:{
				strUserAccount:account,
				strPassWord:password,
				ValidateCode:ValidateCode
			}
		},function(data){
			layer.closeAll('loading');
			
			if(data === true){
				//记录用户信息
				onChangeUserData(account,password);
				//登录账号信息记录
				zfAccount.set(account);
				
				layer.msg('登入成功',{
					icon:1,
					time:500
				},function(){
					location.href = '../../index.html?t=' + new Date().getTime();//主页
				});
			}else{
				layer.msg('账号或密码错误！');
			}
		});
	};
	//记录用户信息
	function onChangeUserData(account,password){
		var userList1 = uxutil.localStorage.get('userList1',true) || [],
			firstUserKey = null,
			MAX = 3;
			
		for(var i in userList1){
			if(userList1[i].account == account){
				userList1.splice(i,1);
			}
		}
		//最大数量已满，删除最前面的账号信息
		if(userList1.length >= MAX){
			userList1.splice(0,1);
		}
		
		var userInfo = {"account":account},
			remember = $("#remember").prop("checked");
			
		//记住密码
		if(remember){
			userInfo.password = password;
		}
		userList1.unshift(userInfo)
		uxutil.localStorage.set('userList1',JSON.stringify(userList1));
		uxutil.localStorage.set('remember',remember);
	};
	
	function initHtml(){
		var remember = uxutil.localStorage.get('remember',true),
			userList1 = uxutil.localStorage.get('userList1',true) || [],
			select = ['<option value="">账号</option>'];
		
		USER_LIST = userList1;
		
		if(remember){
			$("#remember").attr("checked","checked");
		}
		
		if(userList1.length > 0){
			for(var i in userList1){
				select.push('<option value="' + userList1[i].account + '">' + userList1[i].account + '</option>');
			}
			select.unshift('<select name="account" id="account" lay-filter="account" lay-search lay-noclear>');
			select.push('</select>');
			
			$("#account_div").html(select.join(''));
		}
		
		uxform.render();
		uxform.on('select(account)', function(data){
			changePasssword();
		});
		if(userList1.length > 0){
			uxform.val({
				account:userList1[0].account
			});
			//用户名回车监听
			var input = $("#account").next().first().find('input');
			input.keypress(function(e){
				if(e.which == 13){
					setTimeout(function(){
						changePasssword();
						$("#Button_Login").click();
					},0);
				}
			});
		}
	};
	//根据账号变更密码
	function changePasssword(){
		var account = $("#account").next().find("input").val(),
			password = '';
			
		for(var i in USER_LIST){
			if(account == USER_LIST[i].account){
				password = USER_LIST[i].password;
				break;
			}
		}
		
		$("#password").val(password);
	};
	initHtml();
});
