/**
	@name：参数（站点类型）列表
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
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_QueryParaSystemTypeInfoAndAddDefultPara';
	//删除个性设置(整个)
    var DEL_PARAITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DeleteSystemParaItem';

	var paraTypeCode ="";
	var paraTypeName ="";
	var table_ind = null;

	var ParameterList = {
		//参数配置
		config:{
            page: true,
			limit: 50,
			loading : true,
			title:'系统列表',
			initSort:{field:'Id',type:'asc'},
			size: 'sm', //小尺寸的表格
			cols:[[
		        {field:'Id',title:'ID',width:150,sort:true,hide:true},				
				{field:'Code',title:'Code',width:150,sort:true,hide:true},
				{field:'Name',title:'站点类型',minWidth:150,flex:1}
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
		},me.config,ParameterList.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(paraTypeCode,paraTypeName,callback){
		var  me = this;
		table_ind.paraTypeName = paraTypeName;
		table_ind.paraTypeCode = paraTypeCode;
		
		var index =layer.load();
	    //获取类型列表
		Class.pt.onLoadTypeList(paraTypeCode,function(data){
			layer.close(index);
			if(data.success){
				var list = data.value || [];
				var arr = [];
				for(var i=0;i<list.length;i++){
					var obj={
						Code:list[i][0],
						Name:list[i][1]
					};
					if(obj.Code && obj.Code.indexOf("_DefaultPara") == -1)arr.push(obj);
				}
				if(callback)callback(arr);
				else
				    me.instance.reload({data:arr});
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		}); 
	};
	Class.pt.clearData = function(){
		var  me = this;		
		Class.pt.isBtnEnable(false);
		me.instance.reload({
			url:'',
			data:[]
		});
	};
	//获取类型列表
	Class.pt.onLoadTypeList = function(paraTypeCode,callback){
		var url  =  GET_LIST_URL+'?systemTypeCode=1&paraTypeCode='+paraTypeCode;
		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};

    Class.pt.initListeners= function(){
    	var me = this;
    };
	//主入口
	ParameterList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_ind = result;
		result.loadData = me.loadData;
		result.clearData = me.clearData;
		me.initListeners();
		return result;
	};
	//暴露接口
	exports('ParameterList',ParameterList);
});