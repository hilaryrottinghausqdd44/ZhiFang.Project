/**
	@name：人员与站点类型关系页签
	@author：liangyl
	@version 2021-08-04
 */
layui.extend({
	EmpList:'app/dic/hosttypeuser/emplist',//人员
	EmpHostTypeList:'app/dic/hosttypeuser/emphosttype/emphosttypelist'//站点类型
}).define(['uxutil','EmpList','EmpHostTypeList'],function(exports){
	"use strict";
	var $=layui.$,
	    EmpList = layui.EmpList,
	    EmpHostTypeList = layui.EmpHostTypeList,
	    uxutil = layui.uxutil;
    
    var table_ind0 = null; //人员实例
    var table_ind1 = null; //站点类型实例
    var ROW_CHECK_DATA = []; //选择人员
	var EmpHostType  = {
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
		me.config = $.extend({},me.config,EmpHostType.config,setings);
	};
	Class.pt = Class.prototype;
		
	Class.pt.initFilterListeners =  function(){
		var me = this;
		//人员加载
		table_ind0.loadData({},function(data){
			table_ind0.instance.reload({data:data});
		});
		//模块类型列表
		table_ind0.table.on('row(hemp_table)', function(obj){
			//标注选中样式
		    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
			ROW_CHECK_DATA =[obj.data];
			table_ind1.loadData(obj.data.Id);
		});
	};
	//初始化人员列表实例
	Class.pt.inittable_ind0 = function(){
		var me = this;
		//模块类型
		table_ind0 = EmpList.render({
			elem:'#hemp_table',
	    	title:'人员',
	    	height:'full-65',
	    	size: 'sm', //小尺寸的表格
	    	cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'Id',title:'ID',width:150,hide:true},
				{field:'CName',title:'姓名',width:100},
				{field:'StandCode',title:'工号',width:100},
				{field:'TSysName',title:'员工身份',minWidth:150,flex:1},
				{field:'SystemName',title:'系统名称',minWidth:100,hide:true}
			]],
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
		//模块类型
		table_ind1 = EmpHostTypeList.render({
			elem:'#emp_hosttype_table',
	    	title:'站点类型',
	    	height:'full-87',
	    	size: 'sm', //小尺寸的表格
	    	done: function(res, curr, count) {
				setTimeout(function(){
					var tr = table_ind1.instance.config.instance.layBody.find('tr:eq(0)');
					if(tr.length > 0){
						tr.click();
					}
				},0);
			}
		});
		table_ind1.instance.reload({data:[]});
	};
    //初始化
    Class.pt.init = function(){
		var me = this;
        me.inittable_ind0(); //人员实例初始化
		me.inittable_ind1(); //站点类型实例初始化
		me.initFilterListeners();//联动
	};
    //核心入口
	EmpHostType.render = function(options){
		var me = new Class(options);
		me.init();
		return me;
	};
	//新增保存成功后刷新
	function afterCopyUpdate(){
		table_ind1.loadData(ROW_CHECK_DATA[0].Id);
	}
	window.afterCopyUpdate = afterCopyUpdate;
    //暴露接口
	exports('EmpHostType',EmpHostType);
});