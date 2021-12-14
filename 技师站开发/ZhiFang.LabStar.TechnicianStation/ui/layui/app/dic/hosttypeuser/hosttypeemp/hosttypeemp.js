/**
	@name：站点类型与人员关系
	@author：liangyl
	@version 2021-08-04
 */
layui.extend({
	ParameterList:'app/dic/hosttypeuser/list',//模块
	ParameterTypeList:'app/dic/hosttypeuser/typelist',//站点类型
	HostTypeUserList:'app/dic/hosttypeuser/hosttypeemp/hosttypeuserlist' //站点类型和人员
}).define(['uxutil','ParameterList','ParameterTypeList','HostTypeUserList'],function(exports){
	"use strict";
	var $=layui.$,
	    ParameterList = layui.ParameterList,
	    ParameterTypeList = layui.ParameterTypeList,
	    HostTypeUserList = layui.HostTypeUserList,
	    uxutil = layui.uxutil;

	//模块类型实例
	var table_ind0=null;
	//站点类型参数实例
	var table_ind1=null;
    //人员设置实例
	var table_ind2=null;
	//模块类型选择行
	var ROW_CHECK_DATA = [];
	//站点类型选择行
	var ROW_CHECK_DATA_T = [];
	
	var HostTypeEmp  = {
		//全局项
		config:{
		},
		//设置全局项
		set:function(options){
			var me = this;
			me.config = $.extend({},me.config,options);
			return me;
		}
	};
	
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,HostTypeEmp.config,setings);
	};
	Class.pt = Class.prototype;
		
	Class.pt.initFilterListeners =  function(){
		var me = this;
		table_ind0.loadData();//模块类型实例初始化
		//模块类型列表
		table_ind0.table.on('row(table)', function(obj){
			//标注选中样式
		    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
			ROW_CHECK_DATA =[obj.data];
			table_ind1.loadData(obj.data.DefaultValue,obj.data.Name);
		});
		//类型分组列表
		table_ind1.table.on('row(para_table)', function(obj){
			//标注选中样式
		    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');		
			ROW_CHECK_DATA_T = [obj.data];
			table_ind2.loadData(obj.data.Code);
		});
	}; 
	//初始化模块类型列表实例
	Class.pt.inittable_ind0 = function(){
		var me = this;
		//模块类型
		table_ind0 = ParameterTypeList.render({
			elem:'#table',
	    	title:'模块类型',
	    	height:'full-65',
	    	size: 'sm', //小尺寸的表格
	    	done: function(res, curr, count) {
				setTimeout(function(){
					var tr = table_ind0.instance.config.instance.layBody.find('tr:eq(0)');
					if(tr.length > 0){
						tr.click();
					}else{
						ROW_CHECK_DATA = [];
						
					}
				},0);
			}
		});
		table_ind0.instance.reload({data:[]});
	};
	//初始化站点类型列表实例
	Class.pt.inittable_ind1 = function(){
		var me = this;
		//站点类型列表实例
	    table_ind1 = ParameterList.render({
			elem:'#para_table',
	    	title:'站点类型',
	    	height:'full-65',
	    	size: 'sm', //小尺寸的表格
	    	done: function(res, curr, count) {
				setTimeout(function(){
					var rowIndex = 0;
					var tr = table_ind1.instance.config.instance.layBody.find('tr:eq('+rowIndex+')');
					if(tr.length > 0){
						tr.click();
					}else{
						ROW_CHECK_DATA_T = [];
						if(table_ind2)table_ind2.clearData();
					}
				},0);
			}
		});
		table_ind1.instance.reload({data:[]});
	};
	//初始化站点类型与人员关系列表实例
	Class.pt.inittable_ind2 = function(){
		var me = this;
		//站点类型列表实例
	    table_ind2 = HostTypeUserList.render({
			elem:'#emp_table',
	    	title:'站点类型',
	    	height:'full-87',
	    	size: 'sm', //小尺寸的表格
	    	done: function(res, curr, count) {
				setTimeout(function(){
					var rowIndex = 0;
					var tr = table_ind2.instance.config.instance.layBody.find('tr:eq('+rowIndex+')');
					if(tr.length > 0){
						tr.click();
					}else{
						ROW_CHECK_DATA_T = [];
					}
				},0);
			}
		});
		table_ind2.instance.reload({data:[]});
	};
		
	Class.pt.init = function(){
		var me = this;
        me.inittable_ind0(); //模块实例初始化
		me.inittable_ind1(); //站点类型实例初始化
		me.inittable_ind2(); //站点类型与人员关系初始化
		me.initFilterListeners();//联动
	};
    //核心入口
	HostTypeEmp.render = function(options){
		var me = new Class(options);
		me.init();//页面列表初始化
		return me;
	};
	//传递数据给子窗体,已设置的人员
	function ChildEmpData(){
		var arr = table_ind2.table.cache.emp_table;
		return arr;
	}
	window.ChildEmpData = ChildEmpData;
	
    //暴露接口
	exports('HostTypeEmp',HostTypeEmp);
});