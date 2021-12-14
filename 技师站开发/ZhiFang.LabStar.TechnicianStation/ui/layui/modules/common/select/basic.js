/**
 * @name：modules/common/select/basic 下拉框基础类
 * @author：Jcall
 * @version 2021-09-01
 */
layui.extend({
	uxutil:'ux/util'
}).define(['uxutil','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		MOD_NAME = 'CommonSelectBasic';
	
	//下拉框基础类
	var Module = {
		//对外参数
		config:{
			domId:null,
			url:'',//服务地址，包含所有参数
			keyField:'',//值字段
			valueField:'',//显示文字字段
			defaultName:'请选择',//默认文字显示内容
			isFromRender:true,//加载完数据后是否立即表单渲染
			afterLoad:function(data){return data;},//数据加载后未渲染前处理
			done:function(instance){}//下拉框渲染完毕后回调
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,Module.config,setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function(list){
		var me = this,
			len = list.length,
			htmls = [];
			
		if(me.config.defaultName){
			htmls.push('<option value="">' + me.config.defaultName + '</option>');
		}
			
		for(var i=0;i<len;i++){
			htmls.push('<option value="' + list[i][me.config.keyField] + '">' + list[i][me.config.valueField] + '</option>');
		}
		$("#" + me.config.domId).html(htmls.join(""));
		
		if(me.config.isFromRender){
			form.render('select');
		}
		me.config.done(me);
	};
	//加载数据
	Class.prototype.loadData = function(callback){
		var me = this;
		uxutil.server.ajax({
			url:me.config.url
		},function(data){
			data = me.config.afterLoad(data);
			if(data.success){
				callback((data.value || {}).list || []);
			}else{
				callback([]);
				layer.msg(data.msg);
			}
		});
	};
	
	//核心入口
	Module.render = function(options){
		var me = new Class(options);
		
		var error = [];
		if(!me.config.domId){
			error.push("参数config.domId缺失！");
		}
		if(!me.config.url){
			error.push("参数config.url！");
		}
		if(!me.config.keyField){
			error.push("参数config.keyField缺失！");
		}
		if(!me.config.valueField){
			error.push("参数config.valueField缺失！");
		}
		if(error.length > 0){
			if(window.console && console.error){
				for(var i in error){
					console.error(MOD_NAME + "模块组件错误：" + error[i]);
				}
			}else{
				layer.alert(error.join("<BR>"),{title:MOD_NAME + "模块组件错误"})
			}
			return me;
		}
		
		//加载数据
		me.loadData(function(list){
			//初始化HTML
			me.initHtml(list);
		});
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,Module);
});