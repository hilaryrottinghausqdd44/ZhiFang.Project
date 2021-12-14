/**
 * 帮助文档模板
 * @author Jcall
 * @version 2015-05-06
 */
var shell_win_all = shell_win_all || {};
$(function() {
	shell_win_all.help = {
		img_root:Shell.util.Path.uiPath + "/help/img/",
		/**帮助内容列表*/
		list:null,
		/**显示页面*/
		to_page:function(){
			var list = shell_win_all.help.list,
				html = null;
				
			if(list){
				html = shell_win_all.help.get_html(list);
			}else{
				html = '<div style="color:red;margin:20px;text-align:center;"><b>连接不上服务器...</b></div>';
			}
			
			shell_win_all.page.show(html,"帮助功能",1,null,true);
		},
		/**创建页面*/
		get_html:function(data){
			var list = data || [],
				len = list.length,
				html = [];
				
			for(var i=0;i<len;i++){
				html.push('<div class="report_grid_row" on');
				html.push(Shell.util.Event.touch);
				html.push('="shell_win_all.help.on_row_touch(');
				html.push(i);
				html.push(');">');
				html.push('<div class="report_grid_row_name">');
				html.push(i+1);
				html.push('.');
				html.push(list[i].title);
				html.push('</div>');
				html.push('<div class="report_grid_row_right">〉</div>');
				html.push('</div>');
			}
			
			return html.join("");
		},
		/**行触摸触发*/
		on_row_touch:function(index){
			var obj = shell_win_all.help.list[index],
				html = shell_win_all.help.get_content(obj);
				
			shell_win_all.page.show(html,"功能说明",2,null);
		},
		/**获取帮助具体内容*/
		get_content:function(data){
			var title = data.title || "",
				list = data.list || [],
				len = list.length,
				html = [];
				
			html.push('<div class="help_title_div">');
			html.push(title);
			html.push('</div>');
			for(var i=0;i<len;i++){
				var obj = list[i];
				html.push('<div class="help_step_div">');
				//步骤标题
				html.push('<div class="help_step_title_div">');
				html.push(obj.title);
				html.push('</div>');
				
				//图片
				if(obj.img){
					var imgs = [];
					if(typeof(obj.img) == 'string'){
						imgs.push(obj.img);
					}else{
						imgs = obj.img;
					}
					var imgsLen = imgs.length;
					for(var m=0;m<imgsLen;m++){
						html.push('<div class="help_step_img_div">');
						html.push('<img src="');
						html.push(shell_win_all.help.img_root);
						html.push(imgs[m]);
						html.push('" alt="缺图"/>');
						html.push('</div>');
					}
				}
				
				//说明
				html.push('<div class="help_step_text_div">');
				html.push(obj.text);
				html.push('</div>');
				
				html.push('</div>');
			}
			
			return html.join("");
		}
	};
	
	//获取帮助配置内容
	Shell.util.Server.ajax({
		url:Shell.util.Path.uiPath + "/help/help.js",
		success:function(data, textStatus) {
			shell_win_all.help.list = data || [];
			shell_win_all.help.to_page();
		},
		error:function(XMLHttpRequest, textStatus, errorThrown) {
			shell_win_all.help.list = null;
			shell_win_all.help.to_page();
		}
	});
});