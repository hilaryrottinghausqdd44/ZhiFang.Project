var ShellComponent = ShellComponent || {
	/**蒙版*/
	mask: {
		id:"page_mask_div",
		mask_text_id:"page_mask_div_text_id",
		/**隐藏信息*/
		hide: function() {
			this.init_mask();
			$("#" + this.id).modal('hide');
		},
		/**加载数据*/
		loading:function(){
			this.to_server("数据加载中...");
		},
		/**保存数据*/
		save:function(){
			this.to_server("数据保存中...");
		},
		/**删除数据*/
		del:function(){
			this.to_server("数据删除中...");
		},
		/**提交数据*/
		submit:function(){
			this.to_server("数据提交中...");
		},
		/**交互数据*/
		to_server:function(msg){
			this.init_mask();
			$("#" + this.mask_text_id).html(msg || "");
			//$("#" + this.id).modal('show');
			$("#" + this.id).modal({
				backdrop: 'static',
				keyboard:false
			});
		},
		init_mask:function(config){
			var mask = $("#" + this.id);
			if(mask.length == 0){
				$(document.body).append(this.create_mask(config));
			}
		},
		create_mask:function(){
			var html = [];
			
			//模态框
			html.push('<div class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" id="' + this.id + '">');
			html.push('<div class="modal-dialog" style="width:110px;left:50%;margin-left:-55px;">');
			html.push('<div class="modal-content">');
			html.push('<img src="' + Shell.util.Path.UI + '/img/icon/loading.gif"></img>');
			html.push('<span id="' + this.mask_text_id + '"></span>');
			html.push('</div>');
			html.push('</div>');
			html.push('</div>');
			
			return html.join("");
		}
	},
	/**对话框*/
	messagebox:{
		id:"page_messagebox_div",
		default_title:"消息框",
		messagebox_dom:null,
		buttons:{
			"OK": "确定",
			"CANCEL": "取消"
		},
		
		/**
		 * 消息框
		 * @param {Object} msg 消息内容,也可以是config
		 */
		msg:function(msg,times){
			var con = msg;
			if(Shell.util.typeOf(msg) == "string"){
				con = {};
				con.msg = msg;
				con.buttons = ["OK"];
			}else{
				con.buttons = ["OK"];
			}
			this.show(con,times);
		},
		/**
		 * 显示提示信息
		 * @param {Object} config
		 * config:{
		 * 	width 消息框宽度
		 * 	hieght 消息框高度
		 *  title 消息框标题
		 *  titleAlign 标题位置 left/center/right,默认center
		 *  msg 消息内容
		 *  msgAlign 消息位置 left/center/right,默认center
		 *  input 输入框
		 *  buttons 按钮数组
		 *  action 按钮点击处理
		 *  touchHide 触摸隐藏消息框
		 * }
		 */
		show:function(config,times){
			this.init_messagebox(config);
			$("#" + this.id).modal('show');
			if(times){setTimeout(function(){ShellComponent.messagebox.hide();},times)}
		},
		hide:function(){
			if(!this.messagebox_dom) return;
			$("#" + this.id).modal('hide');
		},
		init_messagebox:function(config){
			var html = this.create_messagebox(config),
				div = $("#" + this.id);
				
			if(div.length > 0){
				div.replaceWith(html);
			}else{
				$(document.body).append(html);
			}
			
			//ShellComponent.mask.init_mask();
			this.messagebox_dom = $("#" + this.id);
		},
		create_messagebox:function(config){
			var con = config || {},
				div_width = con.width,
				div_height = con.height,
				title = con.title || '提示信息',
				titleAlign = config.titleAlign || "center",
				msgAlign = config.msgAlign || "center",
				html = [];
				
			//模态框
			html.push('<div class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" id="' + this.id + '">');
			html.push('<div class="modal-dialog"');
			
			//样式
			var style = [];
			if(div_width){
				style.push('width:' + div_width + 'px;');
				style.push('left:50%;margin-left:-' + div_width / 2 + 'px;');
			}
			if(div_height){
				style.push('height:' + div_height + 'px;');
			}
			if(style.length > 0){
				html.push(' style="' + style.join('') + '"');
			}
			
			html.push('><div class="modal-content"><div class="modal-header">');
			html.push('<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>');
			html.push('<h4 class="modal-title">' + title + '</h4>');
			html.push('</div><div class="modal-body">' + con.msg + '</div></div>');
			
			html.push('</div></div>');
			
			return html.join("");
		}
	},
	/**
	 * 获取带图标输入框DIV
	 * @param {Object} config
	 * config:{
	 * 	id 输入框ID
	 *  icon 图标地址
	 *  placeholder 背景值
	 *  value 默认值
	 *  type input的type值
	 * }
	 */
	create_input: function(config) {
		var html = [];
		
		//输入框
		html.push('<div class="input-div">');
		html.push('<input type="');
		html.push(config.type || 'text');
		html.push('" id="');
		html.push(config.id );
		html.push('" placeholder="');
		html.push(config.placeholder);
		html.push('" value="');
		html.push(config.value || '');
		html.push('" style="');
		//前置图标
		if(config.icon){
			html.push('background-image:url(\'');
			html.push(config.icon);
			html.push('\');background-position:5px 5px;' +
				'padding:0 10px 0 40px;');
		}
		if(config.inputStyle){
			html.push(config.inputStyle);
		}
		html.push('">');
		html.push('</div>');
		
		return html.join("");
	}
};
