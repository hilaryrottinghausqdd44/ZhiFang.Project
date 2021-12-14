var shell_win_all = null;
$(function() {
	//页面所有功能对象
	var shell_win = {
		/**系统*/
		system: {
			/**默认头像图片*/
			default_user_photo:Shell.util.Path.uiPath + "/img/icon/default_user_photo.png",
			/**微信ID*/
			open_id: Shell.util.Cookie.getCookie("openId"),
			/**用户ID*/
			user_id: Shell.util.Cookie.getCookie("userId"),
			/**系统初始化*/
			init: function() {
				
			}
		},
		/**缓存*/
		memory:{
			/**成员*/
			member: {
				/**是否已加载*/
				has_load: false,
				/**成员列表*/
				list: [],
				/**获取成员列表*/
				get_list: function(callback) {
					if (!shell_win.memory.member.has_load) {
						shell_win.memory.member.load(function(){
							if(callback){
								callback(shell_win.memory.member.list.concat());
							}
						});
						return;
					}
					
					if(callback){
						callback(shell_win.memory.member.list.concat());
						return;
					}else{
						return shell_win.memory.member.list.concat();
					}
				},
				/**加载数据*/
				load: function(callback) {
					ShellComponent.mask.loading();
					Shell.util.Server.ajax({
						url: Shell.util.Path.rootPath + 
						"/WeiXinAppService.svc/ST_UDTO_GetBSearchAccountVOListByWeiXinAccountId"
					}, function(data) {
						ShellComponent.mask.hide();
						if (data.success) {
							shell_win.memory.member.list = data.value || [];
							shell_win.memory.member.has_load = true;
							callback();
						} else {
							shell_win.memory.member.list = [];
							callback();
							ShellComponent.messagebox.msg(data.msg);
						}
					});
				},
				remove:function(member_id,report_id){
					//shell_win.memory.member.list
					var data = shell_win.memory.member.list,
						len = data.length,
						info = {};
					for(var i=0;i<len;i++){
						if(data[i].Id == member_id){
							info = data[i];
							break;
						}
					}
					var list = (info.RFIndexList || "").split(","),
						length = list.length;
						
					for(var i=0;i<length;i++){
						if(list[i] == report_id){
							list.splice(i,1);
							info.RFIndexList = list.join(",");
							break;
						}
					}
				}
			},
			/**账号*/
			account:{
				/**是否已加载*/
				has_load:false,
				/**账号信息*/
				info:{},
				/**获取账号信息*/
				get_info:function(callback){
					if (!shell_win.memory.account.has_load) {
						shell_win.memory.account.load(function(){
							if(callback){
								callback(shell_win.memory.account.info);
							}
						});
						return;
					}
					
					if(callback){
						callback(shell_win.memory.account.info);
						return;
					}else{
						return shell_win.memory.account.info;
					}
				},
				/**加载数据*/
				load:function(callback){
					ShellComponent.mask.loading();
					Shell.util.Server.ajax({
						url: Shell.util.Path.rootPath + 
						"/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountById?" + 
						"fields=LoginInputPasswordFlag&id=" + shell_win.system.user_id
					}, function(data) {
						ShellComponent.mask.hide();
						if (data.success) {
							shell_win.memory.account.info = data.value;
							shell_win.memory.account.has_load = true;
							callback();
						} else {
							shell_win.memory.account.info = {};
							callback();
							ShellComponent.messagebox.msg(data.msg);
						}
					});
				}
			}
		},
		/**首页*/
		home:{
			/**图标ID前缀*/
			icon_id:"home_icon_",
			/**首页初始化*/
			init:function(){
				var list = shell_win.home.get_icon_list(),
					len = list.length,
					html = [],
					num = 2;
				
				html.push('<table class="home_icon_table">');
				for(var i=0;i<len;i++){
					if(i % num == 0){
						if(i == 0){
							html.push('<tr>');
						}else{
							html.push('</tr><tr>');
						}
					}
					html.push('<td>');
					html.push(shell_win.home.create_icon(list[i]));
					html.push('</td>');
				}
				html.push('</table>');
				
				$("#page_home").html(html.join(""));
			},
			/**获取功能列表*/
			get_icon_list:function(){
				var list = [{
					id:"barcode",
					name:"扫一扫",
					icon:Shell.util.Path.uiPath + "/img/home/barcode.png"
				},{
					id:"report",
					name:"报告查询",
					icon:Shell.util.Path.uiPath + "/img/home/report.png"
				},{
					id:"patient",
					name:"就诊人",
					icon:Shell.util.Path.uiPath + "/img/home/patient.png"
				},{
					id:"account",
					name:"账号",
					icon:Shell.util.Path.uiPath + "/img/home/account.png"
				}];
				return list;
			},
			/**创建图标*/
			create_icon:function(info){
				
				if(!info || !info.id || !info.name) return null;
				
				var icon_id = shell_win.home.icon_id + info.id,
					html = [];
				
				html.push('<div id="' + icon_id + '" class="home_icon" on' + Shell.util.Event.touch + 
					'="shell_win_all.home.on_icon_touch(\'' + info.id + '\');">');
				html.push('<div class="home_icon_img_div"><img class="home_icon_img" src="' + info.icon + '"/></div>');
				html.push('<div class="home_icon_text">' + info.name + '</div>');
				html.push('</div>');
				
				return html.join("");
			},
			/**图标触摸处理*/
			on_icon_touch:function(id){
				shell_win[id].to_page();
			}
		},
		/**页面*/
		page:{
			lev: {
				"L1": {
					now: "page_info_1",
					back: "page_home"
				},
				"L2": {
					now: "page_info_2",
					back: "page_info_1"
				},
				"L3": {
					now: "page_info_3",
					back: "page_info_2"
				}
			},
			/**回退页面*/
			back: function(page_now, back_to) {
				$(page_now).hide();
				$(back_to).show();
			},
			/**显示页面*/
			show: function(html, title, num,after) {
				var L = "L" + (num ? num : "1");
				$("#" + shell_win.page.lev[L].back).hide();
				shell_win.page.show_content(html, title, L,function(data){
					$("#" + shell_win.page.lev[L].now).html(data).show();
					if(after) after();
				});
			},
			/**显示内容*/
			show_content:function(html, title, L,callback){
				if(typeof(html) === "function"){
					html(function(data){
						callback(shell_win.page.get_content(data,title,L));
					});
				}else if(typeof(html) === "string"){
					callback(shell_win.page.get_content(html,title,L));
				}
			},
			/**获取内容*/
			get_content:function(html,title,L){
				var div = [];
				
				div.push('<div class="page_head_fixed">');
				
//				if(L != "L1"){
					div.push('<div class="page_info_head_back" on' + Shell.util.Event.touch +
					'="shell_win_all.page.back(\'#' + shell_win.page.lev[L].now +
					'\',\'#' + shell_win.page.lev[L].back + '\');">返回 ></div>');
//				}

				if (title) {
					div.push('<div class="page_info_head_title">');
					div.push(title);
					div.push('</div>');
				}

				div.push('</div>');
				div.push('<div class="page_content" style="margin-bottom: 0;">');
				div.push(html);
				div.push('</div>');
				
				return div.join("");
			}
		},
		/**动作*/
		event: {
			/**开始触摸点X位置*/
			touch_start_x: null,
			/**开始触摸点Y位置*/
			touch_start_y: null,
			/**结束触摸点X位置*/
			touch_end_x: null,
			/**结束触摸点Y位置*/
			touch_end_y: null,
			/**X轴移动距离*/
			move_x:function(){
				if(shell_win.event.touch_end_x == null || 
					shell_win.event.touch_start_x == null) return null;
				return shell_win.event.touch_end_x - shell_win.event.touch_start_x;
			},
			/**Y轴移动距离*/
			move_y:function(){
				if(shell_win.event.touch_end_y == null || 
					shell_win.event.touch_start_y == null) return null;
				return shell_win.event.touch_end_y - shell_win.event.touch_start_y;
			},
			/**初始化*/
			init:function(){
				var page = window.document.body;
				page.addEventListener("touchstart", function(e) {
					if (e.targetTouches.length == 1 && !self.busy) {
						var xy = e.targetTouches[0];
						shell_win.event.touch_start_x = xy.pageX;
						shell_win.event.touch_start_y = xy.pageY;
						shell_win.event.touch_end_x = xy.pageX;
						shell_win.event.touch_end_y = xy.pageY;
					}
				}, false);
				page.addEventListener("touchmove", function(e) {
					if (e.targetTouches.length == 1 && !self.busy) {
						var xy = e.targetTouches[0];
						shell_win.event.touch_end_x = xy.pageX;
						shell_win.event.touch_end_y = xy.pageY;
					}
				}, false);
				page.addEventListener("touchend", function(e) {
					
				}, false);
			}
		},
		/**初始化*/
		init:function(){
			//系统初始化
			shell_win.system.init();
			
			if(!shell_win.system.open_id){
				window.location.href = "login.html";
				return;
			}
			
			//首页初始化
			shell_win.home.init();
			//初始化页面动作
			shell_win.event.init();
			//定位功能
			var arr = window.document.location.href.split("#");
			if(arr.length >= 2){
				shell_win.home.on_icon_touch(arr[1]);
			}
		}
	};
	//公开全局对象
	shell_win_all = shell_win;
	//初始化页面
	shell_win_all.init();
});