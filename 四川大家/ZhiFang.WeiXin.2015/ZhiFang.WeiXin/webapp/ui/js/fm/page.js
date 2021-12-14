/**
 * 页面功能
 */
var shell_win_all = shell_win_all || {};

shell_win_all.page = {
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
	back: function(page_now,back_to) {
		if($(back_to).length == 0) return;
		$(page_now).hide();
		$(back_to).show();
	},
	/**显示页面*/
	show: function(html,title,num,after,no_back){
		var L = "L" + (num ? num : "1");
		$("#" + shell_win_all.page.lev[L].back).hide();
		shell_win_all.page.show_content(html, title, L,function(data){
			$("#" + shell_win_all.page.lev[L].now).html(data).show();
			if(after) after();
		},no_back);
	},
	/**显示内容*/
	show_content:function(html, title, L,callback,no_back){
		if(typeof(html) === "function"){
			html(function(data){
				callback(shell_win_all.page.get_content(data,title,L,no_back));
			});
		}else if(typeof(html) === "string"){
			callback(shell_win_all.page.get_content(html,title,L,no_back));
		}
	},
	/**获取内容*/
	get_content:function(html,title,L,no_back){
		var div = [];
		
		div.push('<div class="page_head_fixed">');
		
		if(!no_back){
			div.push('<div class="page_info_head_back" on' + Shell.util.Event.touch +
			'="shell_win_all.page.back(\'#' + shell_win_all.page.lev[L].now +
			'\',\'#' + shell_win_all.page.lev[L].back + '\');">〈 返回 </div>');
		}

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
	},
	/**初始化功能*/
	init:function(){
		var lev = shell_win_all.page.lev;
		for(var i in lev){
			if($("#" + lev[i].now).length == 0){
				var html = '<div id="' + lev[i].now + '" class="page_info" hidden="hidden"></div>';
				 $(document.body).append(html);
			}
		}
	}
};
shell_win_all.page.init();
