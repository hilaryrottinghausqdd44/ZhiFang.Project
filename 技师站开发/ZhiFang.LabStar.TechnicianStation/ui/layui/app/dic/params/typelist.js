/**
	@name：参数类型
	@author：liangyl
	@version 2021-07-0
 */
layui.extend({
	uxtable:'ux/table'
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
		
	//获取所有枚举
	var GET_TYPE_LIST_URL = '/ServerWCF/CommonService.svc/GetClassDic';
    //类型字典编码
	var TYPE_ENUM_CODE ="Pre_AllModules";
	
	var ParameterTypeList = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			title:'系统列表',
			initSort:{field:'Id',type:'asc'},
			size: 'sm', //小尺寸的表格
			cols:[[
		        {field:'Id',title:'ID',width:150,sort:true,hide:true},				
				{field:'Code',title:'Code',width:150,sort:true,hide:true},
				{field:'DefaultValue',title:'DefaultValue',width:150,sort:true,hide:true},
				{field:'Name',title:'模块名称',minWidth:150,sort:true,flex:1},
				
			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			afterRender:function(that){
				var filter = $(that.config.elem).attr("lay-filter");
				if(filter){
					//监听行双击事件
					that.table.on('row(' + filter + ')', function(obj){
						//标注选中样式
	                    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
					});
				}
			}
		},me.config,ParameterTypeList.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(){
		var  me = this;
		var index =layer.load();
	    //获取类型列表
		Class.pt.onLoadTypeList(TYPE_ENUM_CODE,function(data){
			layer.close(index);
			if(data.success){
				var list = data.value || [];
				me.instance.reload({data:list});
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		}); 
	};
	//获取类型列表
	Class.pt.onLoadTypeList = function(classname,callback){
		var url  = uxutil.path.ROOT + GET_TYPE_LIST_URL;
		url += '?classnamespace=ZhiFang.Entity.LabStar&classname=' + classname;
		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};
	
	//主入口
	ParameterTypeList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		result.loadData = me.loadData;
		return result;
	};
	//暴露接口
	exports('ParameterTypeList',ParameterTypeList);
});