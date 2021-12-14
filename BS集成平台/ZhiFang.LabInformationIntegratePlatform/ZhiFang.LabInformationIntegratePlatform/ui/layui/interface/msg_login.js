layui.extend({
	uxutil:'ux/util',
	msgcard:'modules/msg/card',
	uxform:'ux/form',
	zfAccount:'ux/zf/account'
}).define(['uxutil','msgcard','uxform','layer','zfAccount'],function(exports){
	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.uxform,
		msgcard = layui.msgcard,
		zfAccount = layui.zfAccount;
		
	//请求登入服务地址
	var LOGIN_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_BA_Login';
	//delphi外壳API
	var JS_DELPHI_API = window.JS_DELPHI_BOM;
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//是否自动登录
	var AUTO_LOGIN = PARAMS.AUTOLOGIN == "true" ? true : false;
	//是否默认最小化
	var DEFAULT_MIN = true;
	//外壳宽高
	var DELPHI_BOM_WIDTH = 130;
	var DELPHI_BOM_HEIGHT = 130;
	
	//账号密码(临时记录用于cookie丢失重连)
	var ACCOUNT = null;
	var PASSWORD = null;
	
	//提交
	form.on('submit(LAY-user-login-submit)',function(obj){
		var accounttagName = $("#account")[0].tagName == 'INPUT',
			account = accounttagName ? $("#account").val() : $("#account").next().first().find('input').val(),
			account = account ? $.trim(account) : '',
			password = $("#password").val();
		
		if(!account || !account){
			layer.msg('账号密码不能为空！',{time:1000});
			return;
		}
		//登录
		onLogin(account,password);
	});
	//登录
	function onLogin(account,password,isReLogin){
		layer.load();
		//请求登入接口
		uxutil.server.ajax({
			url:LOGIN_URL,
			data:{
				strUserAccount:account,
				strPassWord:password
			}
		},function(data){
			layer.closeAll('loading');
			
			if(data === true){
				//记录用户信息
				onChangeUserData(account,password);
				//登录账号信息记录
				zfAccount.set(account);
				ACCOUNT = account;
				PASSWORD = password;
				
				//是否cookie丢失重连
				if(isReLogin){
					afterLogin();
				}else{
					layer.msg('登入成功',{
						icon:1,
						time:500
					},function(){
						afterLogin();
					});
				}
			}else{
				layer.msg('账号或密码错误！');
			}
		});
	};
	//记录用户信息
	function onChangeUserData(account,password){
		var userList = uxutil.localStorage.get('userList',true) || [],
			firstUserKey = null,
			MAX = 3;
			
		for(var i in userList){
			if(userList[i].account == account){
				userList.splice(i,1);
				break;
			}
		}
		//最大数量已满，删除最前面的账号信息
		if(userList.length >= MAX){
			userList.splice(0,1);
		}
		
		var userInfo = {"account":account},
			remember = $("#remember").prop("checked");
			
		//记住密码
		if(remember){
			userInfo.password = password;
		}
		userList.unshift(userInfo)
		uxutil.localStorage.set('userList',JSON.stringify(userList));
		uxutil.localStorage.set('remember',remember);
	};
	//登陆后处理
	function afterLogin(){
		$("#LAY-user-login").remove();
		
		//判断当前登录账号是否与cookie中一致，如果不一致重新登录
		zfAccount.initValid(function(){
			$(".mine-move").remove();
			$("body").append('<div id="LAY-user-login" style="padding:20px 10px;text-align:center;">cookie丢失<br>重连中...</div>');
			onLogin(ACCOUNT,PASSWORD,true);
		});
		
		//CS外壳打开方式
		if(JS_DELPHI_API){
			var top = 5,
				left = uxutil.pc.desktop.width - DELPHI_BOM_WIDTH - top*2;
				
			JS_DELPHI_API.onResize(DELPHI_BOM_WIDTH,DELPHI_BOM_HEIGHT,left,top);
		}
		
		setTimeout(function(){
			initCradHtml();
		},500);
	};
	//初始化消息卡片
	function initCradHtml(){
		var width = $("body").width();
		var height = $("body").height();
		msgcard.render({
			width:width,
			height:height,
			menu:[
				{text:"更改用户",code:"change_user",openType:"location.href",url:uxutil.path.LAYUI+"/interface/msg_login.html"},
				{text:"修改密码",code:"edit_password",openType:"window.open",url:uxutil.path.LAYUI+"/admin/views/set/user/password.html"},
				{text:"危急值查询",code:"cv_search",openType:"window.open",url:uxutil.path.LOCAL+"/ZhiFang.LabStar.TechnicianStation/ui/layui/views/msg/search/list.html"}
			]
		});
	};
	
	//初始化页面
	function initHtml(callback){
		//CS外壳打开方式
		if(JS_DELPHI_API){
			JS_DELPHI_API.onResize(320,500,0,0);
		}
		
		var remember = uxutil.localStorage.get('remember',true),
			userList = uxutil.localStorage.get('userList',true) || [],
			select = ['<option value="">账号</option>'];
			
		if(remember){
			$("#remember").attr("checked","checked");
		}
		
		if(userList.length > 0){
			for(var i in userList){
				select.push('<option value="' + userList[i].account + '" password="' + 
					(userList[i].password || "") + '">' + userList[i].account + '</option>');
			}
			select.unshift('<select name="account" id="account" lay-filter="account" lay-search lay-noclear>');
			select.push('</select>');
			
			$("#account_div").html(select.join(''));
		}
		
		form.render();
		form.on('select(account)', function(data){
			var password = $('#account option:selected').attr("password");
			$("#password").val(password);
		});
		if(userList.length > 0){
			form.val({
				account:userList[0].account
			});
			//用户名回车监听
			var input = $("#account").next().first().find('input');
			input.keypress(function(e){
				if(e.which == 13){
					$("#Button_Login").click();
				}
			});
		}
		callback();
	};
	//自动登录处理
	function onAutoLogin(){
		var userList = uxutil.localStorage.get('userList',true) || [];
		if(userList.length > 0){
			form.val('form',{
				account:userList[0].account,
				password:userList[0].password
			});
			if(AUTO_LOGIN){
				$("#Button_Login").click();
			}
		}
	};
	
	//初始化页面
	initHtml(function(){
		onAutoLogin();
	});
});