	
layui.config({
	base:'layuiadmin/' //静态资源所在路径
}).extend({
	index:'lib/index',//主入口模块
	uxutil: '../../../ux/util',
	system: '../../../ux/zf/system',
	zfAccount: '../../../ux/zf/account'
}).use(['index','uxutil','system','zfAccount','element'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		system = layui.system,
		zfAccount = layui.zfAccount,
		element = layui.element;
	
	//获取登陆者模块树服务
	var GET_MODULE_TREE_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpID';
	//获取系统列表服务
	var GET_SYSTEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL';
	//系统列表
	var SYSTEM_LIST = [];
	//系统MAP
	var SYSTEM_MAP = {};
	//模块组件JSON_MAP
	var MODULE_COMPONENTSLISTJSON_MAP = {};
	
	//退出按钮清空cookie
	$("#Close").on('click',function(){
		uxutil.cookie.clearCookie();
	});
	
	//初始化模块树
	function initModuleTree(){
		getModuleTree();
	}
	//获取登陆者模块树
	function getModuleTree(){
		layer.load();
		uxutil.server.ajax({
			url:GET_MODULE_TREE_URL
		},function(data){
			layer.closeAll('loading');
			if(data.success){
				changeModuleTree(data.value || {});
			}else{
				layer.msg(data.ErrorInfo,{icon:5},function(){
					location.href = 'user/login.html';//登录界面
				});
			}
		});
	}
	//模块树变更
	function changeModuleTree(data){
		var tree = data.Tree || [],
			len = tree.length,
			html = [];
			
		for(var i=0;i<len;i++){
			var info = tree[i];
			var Module = getParentMuduleTemplet();
			
			Module = Module.replace(/{DataName}/g,info.tid);//ID
			Module = Module.replace(/{Name}/g,info.text);//名称
			//Module = Module.replace(/{Expanded}/g,info.expanded ? 'layui-nav-itemed' : '');//是否展开
			Module = Module.replace(/{Expanded}/g,'');//是否展开
			Module = Module.replace(/{IconName}/g,'layui-icon-component');//图标
			
			if(info.url){
				var Url = getUrlHtml(info.url,info.Para,info.tid);
				Module = Module.replace(/{Url}/g,Url);//功能地址
			}else{
				Module = Module.replace(/{Url}/g,'');//功能地址
			}
			//子节点
			if(info.Tree && info.Tree.length > 0){
				var childrenHtml = createChildModlue(info);
				Module = Module.replace(/{Children}/g,childrenHtml);//子节点
			}else{
				Module = Module.replace(/{Children}/g,'');//子节点
			}
			
			html.push(Module);
		}
		
		$("#ModuleTree").html(html.join(''));
		
		//导航的hover效果、二级菜单等功能，需要依赖element模块
		layui.use('element', function(){
			var element = layui.element;
			element.init();
		});
		//模块组件JOSN
		$("#ModuleTree").find("dd").each(function(){
			$(this).on("click",function(){
				setComponentsListJson($(this).attr("data-name"));
			});
		});
	}
	//创建子节点
	function createChildModlue(data){
		var tree = data.Tree || [],
			len = tree.length,
			html = [];
		
		if(!data) return;
		
		for(var i=0;i<len;i++){
			var info = tree[i];
			var Module = getChildMuduleTemplet();
			
			Module = Module.replace(/{DataName}/g,info.tid);//ID
			Module = Module.replace(/{Name}/g,info.text);//名称
			Module = Module.replace(/{Expanded}/g,'');//是否展开
			if(info.url){
				var Url = getUrlHtml(info.url,info.Para,info.tid);
				Module = Module.replace(/{Url}/g,Url);//功能地址
			}else{
				Module = Module.replace(/{Url}/g,'');//功能地址
			}
			//子节点
			Module = Module.replace(/{Children}/g,createChildModlue(info) || '');//子节点
			//模块组件JSON
			MODULE_COMPONENTSLISTJSON_MAP[info.tid] = info.ComponentsListJson;
			
			html.push(Module);
		}
		if(html.length > 0){
			html.unshift('<dl class="layui-nav-child">');
			html.push('</dl>');
		}
		
		return html.join('');
	}
	//监听右侧tab页切换 显示菜单对应项
	element.on('tab(layadmin-layout-tabs)', function () {
		var thisTab = $("#LAY_app_tabsheader>li.layui-this");
		if (thisTab.length <= 0) return;
		var url = thisTab.attr("lay-id");
		$("#ModuleTree *[data-name]").removeClass("layui-this");
		$("#ModuleTree *[lay-href]").each(function () {
			if ($(this).attr("lay-href") == url) {
				var parentElems = $(this).parent("*[data-name]").parents("*[data-name]");//获得当前模块所有父文件夹
				$(parentElems).each(function (i, item) {
					$(item).siblings().removeClass("layui-nav-itemed");//其他同级文件夹全部折叠
					if (!$(item).hasClass("layui-nav-itemed")) $(item).addClass("layui-nav-itemed");//父文件夹展开
				});
				//当前模块加选中样式
				$(this).parent("*[data-name]").addClass("layui-this");
				return false;
			}
		});
	});
	//菜单文件夹展开加图标 关闭去掉图标
	element.on("nav(layadmin-system-side-menu)", function (e) {
		if ($(e).parent('li').length == 0 && $(e).siblings("dl").length > 0) {
			$(".layui-nav-tree[lay-filter=layadmin-system-side-menu] i.layui-icon-spread-left").remove();
			$("dl .layui-nav-itemed>a").prepend('<i class="layui-icon layui-icon-spread-left"></i>');
		}
	});

	//获取一层模块模板
	function getParentMuduleTemplet(){
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
	function getChildMuduleTemplet(){
		var module = 
		'<dd data-name="{DataName}" class="{Expanded}">' +
			'<a {Url} >{Name}</a>' +
			'{Children}' +
		'</dd>';
		
		return module;
	}
	//获取功能路径	
	function getUrlHtml(url,para,moduleId){
		var url = url || '',
			start = url.indexOf('{'),
			end = url.indexOf('}'),
			href = '';
			
		//如果参数存在，URL加上参数
		if(para){
			url += (url.indexOf('?') == -1 ? '?' : '&') + para;
		}
    	
		if(url.slice(0,4) == 'http'){//互联网路径
			href = url;
		}else if(start == -1 || end == -1){//本系统
			if(url.slice(0,1) == '#'){//ExtJS页面
				url = url.replace(/\?/g,'&');
				href = uxutil.path.ROOT + '/ui/extjs/interface/one/index.html?className=' + url.slice(1);
	    	}else if(url.slice(0,1) == '/'){//本地相对路径
	    		href = uxutil.path.ROOT + url;
	    	}
		}else if(start != -1 && end != -1){//同域其他系统
			var systemCode = url.slice(start+1,end);
			var systemInfo = SYSTEM_MAP[systemCode];
			url = url.slice(end+1);
			
			if(!systemInfo){//系统编码没有配置
				href = uxutil.path.ROOT + '/ui/layui/views/html/nosystem.html?code=' + systemCode;
			}else{
				if(url.slice(0,1) == '#'){//ExtJS页面
					url = url.replace(/\?/g,'&');
					href = uxutil.path.LOCAL + '/' + systemInfo.SystemHost + '/ui/extjs/interface/one/index.html?className=' + url.slice(1);
		    	}else if(url.slice(0,1) == '/'){//相对路径
		    		href = uxutil.path.LOCAL + '/' + systemInfo.SystemHost + url;
		    	}
			}
		}
    	//加上时间防止缓存
    	href += (href.indexOf('?') == -1 ? '?' : '&') + 't=' + new Date().getTime();
    	
		href += '&moduleId=' + moduleId;//模块ID传递
		
		return 'lay-href="' + href + '"';
	};
	//获取系统列表
	function getSystemList(callback){
		var fields = ['SystemCode','SystemName','SystemHost'];
		var url = GET_SYSTEM_LIST_URL + '?fields=IntergrateSystemSet_' + fields.join(',IntergrateSystemSet_');
		
		layer.load();
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.closeAll('loading');
			if(data.success){
				SYSTEM_LIST = (data.value || {}).list || [];
				for(var i in SYSTEM_LIST){
					SYSTEM_MAP[SYSTEM_LIST[i].SystemCode] = SYSTEM_LIST[i];
				}
				callback();
			}else{
				layer.msg(data.ErrorInfo,{icon:5});
			}
		});
	};
	
	//初始化用户信息
	function initUserInfo(){
		var USERNAME = uxutil.cookie.get(uxutil.cookie.map.USERNAME) || '无';
		$("#UserName").html(USERNAME);
		
		uxutil.server.date.init();//启动服务器时间
	};
	
	//cookie信息存在显示页面信息，不存在返回登录页面
	var isLogin = uxutil.cookie.get(uxutil.cookie.map.ACCOUNTNAME);
	if(isLogin){
		//判断当前登录账号是否与cookie中一致，如果不一致弹出重新登录提示
		zfAccount.initValid(function(){
			var html = 
			'<div style="padding:50px;line-height:22px;background-color:#393D49;color:#fff;font-weight:300;text-align:center;">' +
				'重新登录提醒<br><br>当前登录账号丢失或被覆盖<br>请重新登录！' +
			'</div>';
			layer.open({
				type:1,
				title:false,
				closeBtn:false,
				area:'300px;',shade:0.8,
				btn:['重新登录'],
				btnAlign:'c',
				moveType:1,//拖拽模式，0或者1
				content:html,
				success:function(layero){
					var btn = layero.find('.layui-layer-btn0');
					btn.on("click",function(){
						location.href = 'user/login.html?t=' + new Date().getTime();
					});
				}
			});
		});
		//获取系统列表
		getSystemList(function(){
			//初始化服务器时间
			system.date.init(function(){
				$("#LAY_app").show();
				//初始化用户信息
				initUserInfo();
				//初始化模块树
				initModuleTree();
			});
		});
	}else{
		//导航的hover效果、二级菜单等功能，需要依赖element模块
		layui.use('layer', function(){
			var layer = layui.layer;
			layer.msg('不存在用户信息，返回登录页面中...',{
				icon:1,
				time:2000
			},function(){
				location.href = 'user/login.html?t=' + new Date().getTime();
			});
		});
	}

	function OpenLayerIFrame(title,url,width,height) {
		layer.open({
			type: 2,
			title: title,
			shadeClose: true,
			shade: 0.3,
			maxmin: false, //开启最大化最小化按钮
			area: [width, height],
			content: url
		});
	};
	window.OpenLayerIFrame = OpenLayerIFrame;
	
	//模块组件JSON
	var ComponentsListJsonMap = {};
	//MAP保存模块组件JSON
	setComponentsListJson = function(moduleId){
		var ComponentsListJson = MODULE_COMPONENTSLISTJSON_MAP[moduleId];
		ComponentsListJsonMap[moduleId + ''] = ComponentsListJson;
	};
	//获取模块组件JSON
	window.getComponentsListJson = function(key){
		return ComponentsListJsonMap[key + ''] || null;
	};
});