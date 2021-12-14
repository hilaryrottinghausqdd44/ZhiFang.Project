Ext.ns('Ext.main');
Ext.define('Ext.main.ModuleImg',{
	extend:'Ext.Component',
	alias:'widget.moduleimg',
	width:78,
	height:67,
	/**
	 * 模块图标地址
	 * @type String
	 */
	src:'',
	/**
	 * 模块功能名称
	 * @type String
	 */
	text:'',
	/**
	 * 模块链接地址
	 * @type String
	 */
	url:'',
	/**
	 * 功能描述
	 * @type String
	 */
	comment:'',
	/**
	 * 初始化模块
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.initView();
		me.initListeners();
		me.callParent(arguments);
	},
	/**
	 * 初始化视图
	 * @private
	 */
	initView:function(){
		var me = this;
		me.renderTpl = [
	        '<img src="{src}" style="padding:5px 0 0 23px;" onerror="mofindModuleImg(this);"></img><br>',
	        '<h1 align="center" style="color:white">{text}</h1>'
	    ];
	    me.renderData = {
	    	src:me.src,
	        text:me.text.length > 4 ? me.text.substring(0,3)+"..." : me.text
	    };
	},
	/**
	 * 监听
	 * @private
	 */
	initListeners:function(){
		var me = this;
		me.listeners = me.listeners || [];
		//鼠标悬浮
		me.listeners.mouseover = {
			element:'el',
			fn:function(e,t){
				var html = "";
				if(me.cls == 'main-moduleBg2'){
					html += '<h1 align="center" style="color:red">没有权限</h1>';
				}
				html += '<h1 color="blue">功能名称：</h1><a>'+me.text+'</a>'+
				'<h1 color="blue">功能说明：</h1><a>'+me.comment+'</a>';
				
				    	 
				if(me.tooltip){
					me.tooltip.update(html);
				}else{
					me.tooltip = Ext.create('Ext.tip.ToolTip',{
					    target:me.getEl(),
					    html:html
					});
				}
			}
        };
	}
});