/**
 * 地址栏登录界面
 * @author zhangda
 * @version 2021-06-09
 */
layui.extend({
	uxutil: 'ux/util',
	uxform: 'ux/form',
	zfAccount: 'ux/zf/account'
}).use(['uxutil','uxform', 'zfAccount'], function () {
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxform = layui.uxform,
		zfAccount = layui.zfAccount;
	//请求登入服务地址
	var LOGIN_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_BA_Login';
	//请求登入服务地址-验证码方式
	var LOGIN_GET_URL = uxutil.path.ROOT + '/ServerWCF/Customization/RBACService_ZhuHai.svc/GetUserInfoAndLogin';
	//历史用户列表
	var USER_LIST = [];
	//清空cookie
	uxutil.cookie.clearCookie();
	
	GetUserInfoAndLogin();
	initListeners();
	//获得验证码
	function GetUserInfoAndLogin() {
		var url = location.search; //获取url中"?"符后的字串
		if (url.indexOf("?") == -1) {
			layer.msg('缺少必要的参数!', { icon: 0, anim: 0, time:2000 });
			return;
		}
		var params = uxutil.params.get(true);
		var verifyCode = params.VERIFYCODE || "";
		if (verifyCode == "") {
			layer.msg("缺少必要的参数!", { icon: 0, anim: 0, time: 2000 });
			return;
		}
		verifyCodeLogin(verifyCode);
	};
	//验证码登录
	function verifyCodeLogin(verifyCode) {
		if (!verifyCode) return;
		layer.load();
		//请求登入接口
		uxutil.server.ajax({
			url: LOGIN_GET_URL + "?verifyCode=" + verifyCode,
			cache: false
		}, function (res) {
			layer.closeAll('loading');
			if (res.success) {
				layer.msg('登入成功', {
					icon: 1,
					time: 500
				}, function () {
					//登录账号信息记录
					var account = uxutil.cookie.get(uxutil.cookie.map.ACCOUNTNAME);
					zfAccount.set(account);
					if (!$("#loginForm").hasClass("layui-hide")) $("#loginForm").addClass("layui-hide");
					location.href = '../index_zhuhai.html?t=' + new Date().getTime();//主页
				});
			} else {
				if ($("#loginForm").hasClass("layui-hide")) $("#loginForm").removeClass("layui-hide");
				var errMsg = res.ErrorInfo || "账号或密码错误！";
				layer.msg(errMsg, { icon: 5, anim: 0 });
			}
		});
	};
	//获得账户名和密码 //type:1:地址栏参数；2：表单参数
	function getAccountAndPassword(type) {
		var account = null,
			password = null,
			type = type || 1;
		switch (type) {
			case 1:
				var url = location.search; //获取url中"?"符后的字串
				if (url.indexOf("?") == -1 || url.indexOf("&") == -1) {
					layer.msg('请在地址栏中输入用户名和密码<br>格式：?account=用户名&password=密码');
					return;
				}
				var str = url.substr(1),
					arr = str.split("&");
				$.each(arr, function (i, item) {
					var itemArr = item.split("=");
					switch (itemArr[0].toLocaleUpperCase()) {
						case 'ACCOUNT':
							account = itemArr[1];
							break;
						case 'PASSWORD':
							password = itemArr[1];
							break;
						default:
							break;
					}
				});
				break;
			case 2:
				var accounttagName = $("#account")[0].tagName == 'INPUT';
				account = accounttagName ? $("#account").val() : $("#account").next().first().find('input').val(),
				account = account ? $.trim(account) : '',
				password = $("#password").val();
				break;
			default:
				break;
		}
		
		return { account: account, password: password };
	};
	//登录
	function onLogin(type) {
		var type = type || 2,
			obj = getAccountAndPassword(type),
			account = obj["account"],
			password = obj["password"];

		if (!account || !password) {
			layer.msg('账号密码不能为空！', { time: 1000 });
			return;
		}

		layer.load();
		//请求登入接口
		uxutil.server.ajax({
			url: LOGIN_URL,
			cache: false,
			data: {
				strUserAccount: account,
				strPassWord: password
			}
		}, function (data) {
			layer.closeAll('loading');

			if (data === true) {
				//记录用户信息
				onChangeUserData(account, password);
				//登录账号信息记录
				zfAccount.set(account);

				layer.msg('登入成功', {
					icon: 1,
					time: 500
				}, function () {
						if (!$("#loginForm").hasClass("layui-hide")) $("#loginForm").addClass("layui-hide");
						location.href = '../index_zhuhai.html?t=' + new Date().getTime();//主页
				});
			} else {
				if ($("#loginForm").hasClass("layui-hide")) $("#loginForm").removeClass("layui-hide");
				$("#account").val(account);
				$("#password").val(password);
				uxform.render('select');
				layer.msg('账号或密码错误！');
			}
		});
	};
	//记录用户信息
	function onChangeUserData(account, password) {
		var userList1 = uxutil.localStorage.get('userList1', true) || [],
			firstUserKey = null,
			MAX = 3;

		for (var i in userList1) {
			if (userList1[i].account == account) {
				userList1.splice(i, 1);
			}
		}
		//最大数量已满，删除最前面的账号信息
		if (userList1.length >= MAX) {
			userList1.splice(0, 1);
		}

		var userInfo = { "account": account },
			remember = $("#remember").prop("checked");

		//记住密码
		if (remember) {
			userInfo.password = password;
		}
		userList1.unshift(userInfo)
		uxutil.localStorage.set('userList1', JSON.stringify(userList1));
		uxutil.localStorage.set('remember', remember);
	};
	//监听
	function initListeners() {
		//用户名回车监听
		$("#account").keypress(function (e) {
			if (e.which == 13) {
				$("#Button_Login").click();
			}
		});
		//密码回车监听
		$("#password").keypress(function (e) {
			if (e.which == 13) {
				$("#Button_Login").click();
			}
		});
		//小眼睛图标
		$("#eye_img").on("click", function () {
			var type = $("#password").attr("type");
			if (type == "password") {
				$("#password").attr("type", "text");
				$("#eye_img").attr("src", "eye_open.png");// 图片路径（睁眼图片）
			} else {
				$("#password").attr("type", "password");
				$("#eye_img").attr("src", "eye_close.png");//图片路径（闭眼图片）
			}
		});
		//申请状态查询
		$("#apply_status_button").on("click", function () {
			var url = uxutil.path.LAYUI + "/views/user/account/apply/status.html?t=" + new Date().getTime();
			layer.open({
				title: '申请状态查询',
				type: 2,
				content: url,
				maxmin: false,
				toolbar: true,
				resize: false,
				area: ['450px', '280px']
			});
		});
		//账号申请
		$("#apply_button").on("click", function () {
			var url = uxutil.path.LAYUI + "/views/user/account/apply/index.html?t=" + new Date().getTime();
			layer.open({
				title: '账号申请',
				type: 2,
				content: url,
				maxmin: true,
				toolbar: true,
				resize: true,
				area: ['700px', '530px']
			});
		});
		//提交
		$("#Button_Login").on("click", function (obj) {
			onLogin(2);
		});
	};

	function initHtml() {
		var remember = uxutil.localStorage.get('remember', true),
			userList1 = uxutil.localStorage.get('userList1', true) || [],
			select = ['<option value="">账号</option>'];

		USER_LIST = userList1;

		if (remember) {
			$("#remember").attr("checked", "checked");
		}

		if (userList1.length > 0) {
			for (var i in userList1) {
				select.push('<option value="' + userList1[i].account + '">' + userList1[i].account + '</option>');
			}
			select.unshift('<select name="account" id="account" lay-filter="account" lay-search lay-noclear>');
			select.push('</select>');

			$("#account_div").html(select.join(''));
		}

		uxform.render();
		uxform.on('select(account)', function (data) {
			changePasssword();
		});
		if (userList1.length > 0) {
			uxform.val({
				account: userList1[0].account
			});
			//用户名回车监听
			var input = $("#account").next().first().find('input');
			input.keypress(function (e) {
				if (e.which == 13) {
					setTimeout(function () {
						changePasssword();
						$("#Button_Login").click();
					}, 0);
				}
			});
		}
	};
	//根据账号变更密码
	function changePasssword() {
		var account = $("#account").next().find("input").val(),
			password = '';

		for (var i in USER_LIST) {
			if (account == USER_LIST[i].account) {
				password = USER_LIST[i].password;
				break;
			}
		}

		$("#password").val(password);
	};
	initHtml();
});
