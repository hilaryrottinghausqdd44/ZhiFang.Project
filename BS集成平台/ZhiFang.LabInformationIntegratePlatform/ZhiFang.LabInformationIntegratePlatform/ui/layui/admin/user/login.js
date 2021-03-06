layui.extend({
	uxutil: 'ux/util',
	uxform: 'ux/form',
	zfAccount: 'ux/zf/account'
}).use(['uxform','uxutil','zfAccount'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxform = layui.uxform,
		zfAccount = layui.zfAccount;
		
	//请求登入服务地址
	var LOGIN_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_BA_Login';
	//历史用户列表
	var USER_LIST = [];
	
	//清空cookie
	uxutil.cookie.clearCookie();
	
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
	
	$("#eye_img").on("click",function(){
		var type = $("#password").attr("type");
		if(type == "password"){
			$("#password").attr("type","text");
			$("#eye_img").attr("src","eye_open.png");// 图片路径（睁眼图片）
		}else{
			$("#password").attr("type","password");
			$("#eye_img").attr("src","eye_close.png");//图片路径（闭眼图片）
		}
	});
	
	
	//申请状态查询
	$("#apply_status_button").on("click",function(){
		var url = uxutil.path.LAYUI + "/views/user/account/apply/status.html?t=" + new Date().getTime();
		layer.open({
			title:'申请状态查询',
			type:2,
			content:url,
			maxmin:false,
			toolbar:true,
			resize:false,
			area:['450px','280px']
		});
	});
	//账号申请
	$("#apply_button").on("click",function(){
		var url = uxutil.path.LAYUI + "/views/user/account/apply/index.html?t=" + new Date().getTime();
		layer.open({
			title:'账号申请',
			type:2,
			content:url,
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['700px','530px']
		});
	});
	
	//提交
	$("#Button_Login").on("click",function(obj){
		onLogin();
	});
	
	function onLogin(){
		var accounttagName = $("#account")[0].tagName == 'INPUT',
			account = accounttagName ? $("#account").val() : $("#account").next().first().find('input').val(),
			account = account ? $.trim(account) : '',
			password = $("#password").val();
		
		if(!account || !password){
			layer.msg('账号密码不能为空！',{time:1000});
			return;
		}
		
		layer.load();
		//请求登入接口
		uxutil.server.ajax({
			url:LOGIN_URL,
			cache:false,
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
				
				layer.msg('登入成功',{
					icon:1,
					time:500
				},function(){
					location.href = '../index.html?t=' + new Date().getTime();//主页
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
