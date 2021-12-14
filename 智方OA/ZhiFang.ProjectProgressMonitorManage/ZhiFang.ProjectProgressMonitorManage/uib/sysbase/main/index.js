$(function() {
	//-----------------------------------------------------------------
	function afterLoad(){
		var js = [];
		js.push("../../src/js/bootstrap.min.js");
		js.push("../../src/js/plugins/metisMenu/jquery.metisMenu.js");
		js.push("../../src/js/plugins/slimscroll/jquery.slimscroll.min.js");
		js.push("../../src/js/plugins/layer/layer.min.js");
		js.push("../../src/js/hplus.min.js");
		js.push("../../src/js/contabs.min.js");
//		js.push("../../src/js/contabs.debug.js");
//		js.push("../../src/js/plugins/pace/pace.min.js");
		
		for(var i in js){
			var oScript= document.createElement("script");
	    oScript.type = "text/javascript";
	    oScript.src=js[i];
	    //写入当前页面中
			document.body.appendChild(oScript);
		}
	}
	//用户头像
	var accountHead = JShell.System.Path.UI + "/img/account-head.jpg";
	$("#AccountImage").attr("src",accountHead);
	//用户账号
	$("#AccountName").text(JShell.Cookie.get(JShell.Cookie.map.ACCOUNTNAME));
	//用户名称
	var USERNAME = JShell.Cookie.get(JShell.Cookie.map.USERNAME) + 
		'【' + JShell.Cookie.get(JShell.Cookie.map.DEPTNAME) + '】' + 
		'<b class="caret"></b>';
	$("#UserName").html(USERNAME);
	//
	$("#close").on("click",function(){
		JShell.LocalStorage.setItem(JShell.System.IS_LOGGED_NAME,"0");
	});
	$("#close2").on("click",function(){
		JShell.LocalStorage.setItem(JShell.System.IS_LOGGED_NAME,"0");
	});
	
	//主页面关闭后默认用户注销登录
	window.onbeforeunload=function (){
		if(event.clientX>document.body.clientWidth && event.clientY < 0 || event.altKey){
		  JShell.LocalStorage.setItem(JShell.System.IS_LOGGED_NAME,"0");
		}
	}
	
	//获取功能模块数据
	function getModuleTreeData(callback){
		var url = JShell.System.Path.ROOT + "/RBACService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpID";
		$.ajax({
			url:url,
			cache:false,
			dataType:'json',
			success:function(data) {
				if(data.success){
					var data = JShell.JSON.decode(data.ResultDataValue) || {};
					callback(data.Tree || []);
				}else{
					
				}  
			},  
			error : function() {
				alert("异常！");
			}
		});
	}
	//更新功能模块
	function refreshModule(tree){
		var len = tree.length,
			html = [];
		
		for(var i=0;i<len;i++){
			createModule(tree[i],html,0);
		}
		
		$("#side-menu").append(html.join(""));
		
		afterLoad();
	}
	function showMouleHtml(info,html){
		var list = info.Tree || [];
		var len = list.length;
		for(var i=0;i<len;i++){
			//createFirstLev(list[i],html,0);
			//createModule(list[i],html,0);
		}
	}
	function createModule(info,html,lev){
		var LEVEL = ['','second','third'];
		
		//本级
		html.push('<li>');
		
		if(info.leaf){
			html.push('<a class="J_menuItem" href="');
			html.push(JShell.System.Path.UI + info.url);
			html.push('">');
			html.push(info.text);
			html.push('</a>');
		}else{
			html.push('<a href="#"><i class="fa fa-edit"></i><span class="nav-label">');
			html.push(info.text);
			html.push('</span><span class="fa arrow"></span></a>');
		}
		
		//下级
		var list = info.Tree || [];
		var len = list.length;
		if(len > 0){
			html.push('<ul class="nav nav-' + LEVEL[++lev] + '-level">');
		}
		for(var i=0;i<len;i++){
			createModule(list[i],html,lev);
		}
		if(len > 0){
			html.push('</ul>');
		}
		
		//结束
		html.push('</li>');
	}
	//获取模块数据
	getModuleTreeData(refreshModule);
});