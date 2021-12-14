var ShellComponent = ShellComponent || {
	/**蒙版*/
	mask: {
		mask_div_id:"page_mask_div",
		mask_loading_div_id:"page_mask_loading_div",
		mask_loading_div_text_id:"page_mask_loading_div_text",
		/**隐藏信息*/
		hide: function() {
			this.init_mask();
			$("#" + this.mask_loading_div_id).hide();
			$("#" + this.mask_div_id).removeClass("page_mask");
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
			$("#" + this.mask_div_id).addClass("page_mask");
			$("#" + this.mask_loading_div_text_id).html(msg || "");
			$("#" + this.mask_loading_div_id).show();
		},
		init_mask:function(config){
			var mask = $("#" + this.mask_div_id);
			if(mask.length == 0){
				$(document.body).append(this.create_mask(config));
			}
		},
		create_mask:function(){
			var html = [];
			//蒙版DIV
			html.push('<div id="');
			html.push(this.mask_div_id);
			html.push('"></div>');
			//加载数据DIV
			html.push('<div id="');
			html.push(this.mask_loading_div_id);
			html.push('" class="page_mask_loading" hidden="hidden">');
//			html.push('<img src="');
//			html.push(Shell.util.Path.uiPath);
//			html.push('/img/icon/loading.gif"></img>');

			html.push('<div class="loading"></div>');
			html.push('<span id="');
			html.push(this.mask_loading_div_text_id);
			html.push('"></span>');
			html.push('</div>');
			
			return html.join("");
		}
	},
	/**对话框*/
	messagebox:{
		id:"page_messagebox_div",
		default_title:"消息框",
		messagebox_dom:null,
		width:200,
		height:150,
		buttons:{
			"OK": "确定",
			"CANCEL": "取消"
		},
		
		/**
		 * 消息框
		 * @param {Object} msg 消息内容,也可以是config
		 */
		msg:function(msg){
			var con = msg;
			if(Shell.util.typeOf(msg) == "string"){
				con = {};
				con.msg = msg;
				con.buttons = ["OK"];
			}else{
				con.buttons = ["OK"];
			}
			this.show(con);
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
		show:function(config){
			this.init_messagebox(config);
			this.messagebox_dom.show();
			$("#" + ShellComponent.mask.mask_div_id).addClass("page_mask");
		},
		hide:function(){
			if(!this.messagebox_dom) return;
			this.messagebox_dom.hide();
			$("#" + ShellComponent.mask.mask_div_id).removeClass("page_mask");
		},
		init_messagebox:function(config){
			var html = this.create_messagebox(config),
				div = $("#" + this.id);
				
			if(div.length > 0){
				div.replaceWith(html);
			}else{
				$(document.body).append(html);
			}
			
			ShellComponent.mask.init_mask();
			this.messagebox_dom = $("#" + this.id);
		},
		create_messagebox:function(config){
			var con = config || {},
				div_width = con.width || this.width,
				div_height = con.height || this.height,
				titleAlign = config.titleAlign || "center",
				msgAlign = config.msgAlign || "center",
				html = [];
				
			html.push('<div class="messagebox_div" id="');
			html.push(this.id);
			html.push('" style="width:');
			html.push(div_width);
			html.push('px;height:');
			html.push(div_height);
			html.push('px;margin-left:-');
			html.push((div_width) / 2);
			html.push('px;margin-top:-');
			html.push((div_height) / 2);
			html.push('px;"');
			
			//触摸隐藏提示框
			if(config.touchHide){
				html.push(' on');
				html.push(Shell.util.Event.touch);
				html.push('="ShellComponent.messagebox.hide();"');
			}
			
			html.push('>');
			
			//标题
			html.push('<div class="messagebox_div_title" id="');
			html.push(this.id);
			html.push('_title" style="text-align:');
			html.push(titleAlign);
			html.push('">');
			html.push(con.title || this.default_title);
			html.push('</div>');
			
			//消息
			if(con.msg){
				html.push('<div class="messagebox_div_msg" id="');
				html.push(this.id);
				html.push('_msg" style="text-align:');
				html.push(msgAlign);
				html.push('">');
				html.push(con.msg);
				html.push('</div>');
			}
			//输入框
//			if(con.input){
//				html.push('<div class="messagebox_div_input"><input placeholder="');
//				if(con.input_placeholder){
//					html.push(con.input_placeholder);
//				}
//				html.push('"></input></div>');
//			}
			
			if(con.buttons){
				var buttons_height = 40,
					buttons_top = div_height - buttons_height,
					len = con.buttons.length,
					but_width = (con.width || this.width) / len - 2;
				
				html.push('<div class="messagebox_div_buttons" style="top:');
				html.push(buttons_top);
				html.push('px;height:');
				html.push(buttons_height);
				html.push('px;" id="');
				html.push(this.id);
				html.push('_buttons">');
				
				for(var i=0;i<len;i++){
					html.push('<div class="messagebox_div_button" style="width:');
					html.push(but_width);
					html.push('px;height:');
					html.push(buttons_height);
					html.push('px;padding-top:8px;border-top:1px solid #E0E0E0;');
					
					if(i > 0){
						html.push('border-left:1px solid #E0E0E0;');
					}
					
					html.push('" on');
					html.push(Shell.util.Event.touch);
					html.push('="ShellComponent.messagebox.hide();');
					
					if(con.action){
						html.push(con.action);
						html.push('(\'');
						html.push(con.buttons[i]);
						html.push('\');');
					}
					
					html.push('"');
					
					html.push('>');
					html.push(this.buttons[con.buttons[i]]);
					html.push('</div>');
				}
				
				html.push('</div>');
			}
			
			html.push('</div>');
			
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
		html.push('<div class="shell_form_input_div">');
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
