layui.config({
	base: 'layuiadmin/' //静态资源所在路径
}).extend({
	index: 'lib/index', //主入口模块
	uxutil: '../../../ux/util'
}).use(['index', 'uxutil'], function() {
	var $ = layui.$,
		uxutil = layui.uxutil;

	//获取登陆者模块树服务/ServerWCF/RBACService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpID
	var GET_MODULE_TREE_URL = uxutil.path.LIIP_ROOT + '/RBACService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpID';

	//退出按钮清空cookie
	$("#Close").on('click', function() {
		uxutil.cookie.clearCookie();
	});

	//初始化模块树
	function initModuleTree() {
		getModuleTree();
	}
	//获取登陆者模块树
	function getModuleTree() {
		uxutil.server.ajax({
			url: GET_MODULE_TREE_URL
		}, function(data) {
			if(data.success) {
				changeModuleTree(data.value || {});
			} else {
				layer.msg(data.ErrorInfo, {
					icon: 1,
					time: 3000
				}, function() {
					location.href = 'user/login.html'; //登录界面
				});
			}
		});
	}
	//模块树变更
	function changeModuleTree(data) {
		var tree = data.Tree || [],
			len = tree.length,
			html = [];

		for(var i = 0; i < len; i++) {
			var info = tree[i];
			var Module = getParentMuduleTemplet();

			Module = Module.replace(/{DataName}/g, info.tid); //ID
			Module = Module.replace(/{Name}/g, info.text); //名称
			//Module = Module.replace(/{Expanded}/g,info.expanded ? 'layui-nav-itemed' : '');//是否展开
			Module = Module.replace(/{Expanded}/g, (i == 0 ? "layui-nav-itemed" : "")); //是否展开,功能菜单树第一个节点默认展开
			Module = Module.replace(/{IconName}/g, 'layui-icon-component'); //图标

			if(info.url) {
				var Url = getUrlHtml(info.url);
				Module = Module.replace(/{Url}/g, Url); //功能地址
			} else {
				Module = Module.replace(/{Url}/g, ''); //功能地址
			}
			//子节点
			if(info.Tree && info.Tree.length > 0) {
				var childrenHtml = createChildModlue(info);
				Module = Module.replace(/{Children}/g, childrenHtml); //子节点
			} else {
				Module = Module.replace(/{Children}/g, ''); //子节点
			}

			html.push(Module);
		}

		$("#ModuleTree").html(html.join(''));

		//导航的hover效果、二级菜单等功能，需要依赖element模块
		layui.use('element', function() {
			var element = layui.element;
			element.init();
		});
	}
	//创建子节点
	function createChildModlue(data) {
		var tree = data.Tree || [],
			len = tree.length,
			html = [];

		if(!data) return;

		for(var i = 0; i < len; i++) {
			var info = tree[i];
			var Module = getChildMuduleTemplet();

			Module = Module.replace(/{DataName}/g, info.tid); //ID
			Module = Module.replace(/{Name}/g, info.text); //名称
			Module = Module.replace(/{Expanded}/g, ''); //是否展开
			if(info.url) {
				var Url = getUrlHtml(info.url);
				Module = Module.replace(/{Url}/g, Url); //功能地址
			} else {
				Module = Module.replace(/{Url}/g, ''); //功能地址
			}
			//子节点
			Module = Module.replace(/{Children}/g, createChildModlue(info) || ''); //子节点

			html.push(Module);
		}
		if(html.length > 0) {
			html.unshift('<dl class="layui-nav-child">');
			html.push('</dl>');
		}

		return html.join('');
	}

	//获取一层模块模板
	function getParentMuduleTemplet() {
		var module =
			'<li data-name="{DataName}" class="layui-nav-item {Expanded}">' +
			'<a href="javascript:;" {Url} lay-tips="{Name}" lay-direction="2">' +
			'<i class="layui-icon {IconName}"></i>' +
			'<cite>{Name}</cite>' +
			'</a>' +
			'{Children}' +
			'</li>';

		return module;
	}
	//获取多层模块模板
	function getChildMuduleTemplet() {
		var module =
			'<dd data-name="{DataName}" class="{Expanded}">' +
			'<a {Url}>{Name}</a>' +
			'{Children}' +
			'</dd>';

		return module;
	}
	//获取功能路径	
	function getUrlHtml(url) {
		var href = url || '';
		if(href.slice(0, 1) == '#') { //ExtJS页面
			href = href.replace(/\?/g, '&');
			href = '../../extjs/interface/one/index.html?className=' + href.slice(1);
		} else if(href.slice(0, 1) == '/') { //本地相对路径
			href = uxutil.path.ROOT + url;
		} else if(href.slice(0, 4) == 'http') { //互联网路径
			href = url;
		} else {
			var arr = href.split('#');
			if(arr.length == 2) { //同域的其他系统
				var key = arr[0].toLocaleUpperCase();
				var className = arr[1];

				var SystemName = JcallShell.System.Map[key];
				if(SystemName) {
					var classNameArr = className.split('?');
					var classConfigStr = null;
					if(classNameArr.length == 2) {
						var pars = classNameArr[1].split('&');
						className = classNameArr[0];
						classConfigStr = classNameArr[1];
					}
					href = uxutil.path.LOCAL + '/' + SystemName +
						'/ui/interface/one/index.html?' + 'className=' + className;
					if(classConfigStr) {
						href += '&' + classConfigStr;
					}
				}
			}
		}

		return 'lay-href="' + href + '"';
	}

	//初始化用户信息
	function initUserInfo() {
		var USERNAME = uxutil.cookie.get(uxutil.cookie.map.USERNAME) || '无';
		$("#UserName").html(USERNAME);
	}

	//cookie信息存在显示页面信息，不存在返回登录页面
	var isLogin = uxutil.cookie.get(uxutil.cookie.map.USERNAME);
	//测试
	//isLogin = true;
	if(isLogin) {
		$("#LAY_app").show();
		//初始化用户信息
		initUserInfo();
		//初始化模块树
		initModuleTree();
	} else {
		//导航的hover效果、 二级菜单等功能， 需要依赖element模块
		layui.use('layer', function() {
			var layer = layui.layer;
			layer.msg('不存在用户信息，返回登录页面中...', {
				icon: 1,
				time: 2000
			}, function() {
				location.href = 'user/login.html';
			});
		});
	}
});