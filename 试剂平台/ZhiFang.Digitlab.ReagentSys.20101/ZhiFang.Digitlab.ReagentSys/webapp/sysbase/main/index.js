$(function() {
	//当前触摸的模块
	var TOUCHED_MODULE = null;
	var TOUCHED_ICON = null;
	var SHOW_DIV = null;
	//用户机构类型
	var CENORGTYPE_SHORTCODE = Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGTYPE_SHORTCODE);
	//初始化用户信息
	function initUserInfo(){
		var info = $($("#cog").children("div")[0]).children("div");
		$(info).children("div").html(Shell.util.Cookie.get(Shell.util.Cookie.map.USERNAME));
		$(info).children("span").text(Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGNAME));
		
		$("#cog-list div").on(Shell.util.Event.click, function() {
			location.href = $(this).attr("data");
		});
		$("#close-safe").on(Shell.util.Event.click, function() {
			//清空当前用户密码
			Shell.util.LocalStorage.addAccount(Shell.util.Cookie.get(Shell.util.Cookie.map.ACCOUNTNAME),"");
			//默认选中首页
			Shell.util.LocalStorage.set(Shell.util.LocalStorage.map.INDEXTYPE,0);
			//转向到登录页面
			location.href = Shell.util.Path.UI + '/sysbase/main/login.html';
		});
		$("#change-account").on(Shell.util.Event.click, function() {
			//默认选中首页
			Shell.util.LocalStorage.set(Shell.util.LocalStorage.map.INDEXTYPE,0);
			//转向到登录页面
			location.href = Shell.util.Path.UI + '/sysbase/main/login.html';
		});
	}
	//初始化用户功能
	function initUserModule(){
		var moduleList = [];
		
		if(CENORGTYPE_SHORTCODE == "1"){//厂商
			moduleList = initProdModule();
		}else if(CENORGTYPE_SHORTCODE == "2"){//供应商
			moduleList = initCompModule();
		}else if(CENORGTYPE_SHORTCODE == "3"){//实验室
			moduleList = initLabModule();
		}
		//意见反馈
		moduleList.push({
			name:'意见反馈',
			icon:'../../img/icon/169ada/feedback.png',
			url:'/sysbase/feedback/index.html'
		});
		
		//更新每个页面
		$("#home").html(createModuleList(moduleList));
		
		//初始化监听
		initListeners();
	}
	//初始化厂商功能
	function initProdModule(){
		var module = [];
		
		return module;
	}
	//初始化供应商功能
	function initCompModule(){
		var module = [
			{name:'订单确认',icon:'../../img/icon/169ada/验收入库.png',url:'/rea/order/list_comp.html?type=1'},
			{name:'订单发货',icon:'../../img/icon/169ada/发货.png',url:'/rea/order/list_comp.html?type=2'},
			{name:'待验收订单',icon:'../../img/icon/169ada/验证.png',url:'/rea/order/list_comp.html?type=3'},
			{name:'历史订单',icon:'../../img/icon/169ada/历史订单.png',url:'/rea/order/list_comp.html?type=4'}
		];
		
		return module;
	}
	//初始化实验室功能
	function initLabModule(){
		var module = [
			//{name:'新增订单',icon:'../../img/icon/e0e0e0/新增.png',url:'/rea/order1/porglist.html'},
			{name:'新增订单',icon:'../../img/icon/169ada/新增.png',url:'/rea/order/porglist.html'},
			{name:'提交订单',icon:'../../img/icon/169ada/提交修改.png',url:'/rea/order/list_lab.html?type=0'},
			{name:'验收订单',icon:'../../img/icon/169ada/验证.png',url:'/rea/order/list_lab.html?type=1'},
			{name:'历史订单',icon:'../../img/icon/169ada/历史订单.png',url:'/rea/order/list_lab.html?type=2'}
		];
		
		return module;
	}
	//创建模块列表
	function createModuleList(list){
		var moduleList = list || [],
			len = moduleList.length,
			html= [];
		
		for(var i=0;i<len;i++){
			html.push(createModule(moduleList[i]));
		}
		
		return html.join("");
	}
	//创建模块
	function createModule(data){
		var html = [];
		
		html.push('<div class="col-xs-6 col-md-3 index-module" moduleUrl="' + data.url + '">');
		html.push('<div class="thumbnail">');
		html.push('<img src="' + data.icon + '" alt="...">');
		//html.push('<div class="caption"><span>' + data.name + '</span></div>');
		html.push('<div class="caption"><span');
		
		if(data.textStyle){
			html.push(' style="' + data.textStyle + '"');
		}
		
		html.push('>' + data.name + '</span></div>');
		html.push('</div>');
		html.push('</div>');
		
		return html.join("");
	}
	//初始化监听
	function initListeners(){
		//模块触摸监听
		$(".index-module").on(Shell.util.Event.click, function() {
			if (TOUCHED_MODULE){
				TOUCHED_MODULE.removeClass("index-module-touch");
			}
	
			TOUCHED_MODULE = $(this);
			TOUCHED_MODULE.addClass("index-module-touch");
	
			onModuleClick(TOUCHED_MODULE); //模块触摸
		});
		
//	$("#navbar-bottom li").on(Shell.util.Event.click, function(){
//		console.log(new Date().getTime());
//		//底部图标变化
//		if(TOUCHED_ICON){
//			TOUCHED_ICON.removeClass("navbar-bottom-icon-touch");
//		}
//		TOUCHED_ICON = $(this);
//		TOUCHED_ICON.addClass("navbar-bottom-icon-touch");
//		
//		//内容DIV变化
//		if(SHOW_DIV){
//			SHOW_DIV.hide();
//		}
//		SHOW_DIV = $("#" + TOUCHED_ICON.attr("data"));
//		SHOW_DIV.show();
//	});
		$("#navbar-bottom li").on("click", function(){
			var type = $(this).attr("index");
			Shell.util.LocalStorage.set(Shell.util.LocalStorage.map.INDEXTYPE,type);
			
			//底部图标变化
			if(TOUCHED_ICON){
				TOUCHED_ICON.removeClass("navbar-bottom-icon-touch");
			}
			TOUCHED_ICON = $(this);
			TOUCHED_ICON.addClass("navbar-bottom-icon-touch");
			
			//内容DIV变化
			if(SHOW_DIV){
				SHOW_DIV.hide();
			}
			SHOW_DIV = $("#" + TOUCHED_ICON.attr("data"));
			SHOW_DIV.show();
		});
		if(Shell.util.Event.click != "click"){
			$("#navbar-bottom li").on(Shell.util.Event.click, function(){
				$(this).click();
			});
		}
		var type = Shell.util.LocalStorage.get(Shell.util.LocalStorage.map.INDEXTYPE);
		$("#navbar-bottom li[index='" + (type ? type : 0) + "']").click();
		
	}
	//模块触摸
	function onModuleClick(id) {
		var url = TOUCHED_MODULE.attr("moduleUrl");
		url = Shell.util.Path.getUiUrl(url);
		
		if (!url) {
			ShellComponent.messagebox.msg("没有绑定模块",500);
			return;
		}
		//url += (url.indexOf("?") ? "&" : "?") + "dt=" + new Date().getTime();

		location.href = url;
	}
	
	//初始化用户信息
	initUserInfo();
	//初始化用户功能
	initUserModule();
});