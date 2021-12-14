/**
	@name：layui.ux.toolbar 功能按钮栏  
	@author：Jcall
	@version 2021-06-22
 */
layui.extend({
	uxutil:'ux/util'
}).define(['jquery','layer'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		layer = layui.layer,
		MOD_NAME = 'uxtoolbar';
		
	
	var uxtoolbar = {
		config:{
			//外部容器ID
			domId:null,
			//刷新,新增,修改,查看,删除,查询,打印,下载,导出
			buttons:['refresh','add','edit','show','del','search','print','check','uncheck','upload','export'],
			//按钮MAP
			buttonsMap:{
				refresh:{type:'refresh',text:'刷新',iconCls:'layui-icon-refresh',buttonCls:''},
				add:{type:'add',text:'新增',iconCls:'layui-icon-add-1',buttonCls:''},
				edit:{type:'edit',text:'修改',iconCls:'layui-icon-edit',buttonCls:'layui-btn-normal'},
				show:{type:'show',text:'查看',iconCls:'layui-icon-form',buttonCls:''},
				del:{type:'del',text:'删除',iconCls:'layui-icon-delete',buttonCls:'layui-btn-danger'},
				search:{type:'search',text:'查询',iconCls:'layui-icon-search',buttonCls:''},
				print:{type:'print',text:'打印',iconCls:'layui-icon-print',buttonCls:''},
				check:{type:'check',text:'审核',iconCls:'layui-icon-ok',buttonCls:''},
				uncheck:{type:'uncheck',text:'取消审核',iconCls:'layui-icon-close',buttonCls:''},
				upload:{type:'upload',text:'下载',iconCls:'layui-icon-upload',buttonCls:''},
				export:{type:'export',text:'导出',iconCls:'layui-icon-export',buttonCls:''}
			},
			//事件
			event:{
				
			}
		}
	};
	//按钮点击空方法
	var buttonDefalutClick = function(name){
		layer.msg("请实现" + name + "按钮方法！");
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({},me.config,uxtoolbar.config,setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this,
			buttons = me.config.buttons || [],
			len = buttons.length,
			html = [];
		
		html.push('<div class="layui-btn-group">');
		for(var i=0;i<len;i++){
			var button = buttons[i];
			var type = typeof(button);
			var info = type == 'string' ? me.config.buttonsMap[button] : button;
			
			html.push(
				'<button id="' + me.config.domId + '-button' + i + '" class="layui-btn layui-btn-sm ' + 
					(info.buttonCls || '') + '" lay-event="search">' +
					'<i class="layui-icon ' + (info.iconCls || '') + '"></i>' + info.text + 
				'</button>'
			);
		}
		html.push('</div>');
		
		$("#" + me.config.domId).append(html.join(''));
	};
	//初始化监听
	Class.prototype.initListeners = function(){
		var me = this,
			buttons = me.config.buttons || [],
			len = buttons.length;
		
		for(var i=0;i<len;i++){
			var eventName = typeof(buttons[i]) == 'string' ? buttons[i] : buttons[i].type;
			me.initButtonClickListeners(me.config.domId + "-button" + i,eventName);
		}
	};
	//初始化按钮监听
	//初始化监听
	Class.prototype.initButtonClickListeners = function(buttonId,eventName){
		var me = this;
		$("#" + buttonId).on("click",function(){
			me.config.event[eventName] ? me.config.event[eventName]() : buttonDefalutClick(eventName);
		});
	};
	
	//主入口
	uxtoolbar.render = function(options){
		var me = new Class(options);
		//初始化HTML
		me.initHtml();
		//初始化监听
		me.initListeners();
		return me;
	}
	
	//暴露接口
	exports(MOD_NAME,uxtoolbar);
});