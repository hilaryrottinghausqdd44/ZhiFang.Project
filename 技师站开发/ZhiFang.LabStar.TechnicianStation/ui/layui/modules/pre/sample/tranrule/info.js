/**
 * @name：modules/pre/sample/tranrule/info 选择样本单具体信息 
 * @author：liangyl
 * @version 2021-10-14
 */
layui.extend({
}).define(['uxutil','uxtable'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		MOD_NAME = 'InfoList';
	//内部列表+表头dom
	var TABLE_DOM = [
		'<div class="{tableId}-table">',
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'</div>',
		'<style>',
			'.layui-table-select{background-color:#5FB878;}',
		'</style>'
	];
	//查询分发规则小组
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleHostSectionByHQL?isPlanish=true";
   	//检验小组枚举自定义,方便匹配小组名称
    var ENUM = {};
	//样本单列表
	var InfoList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null,
			page: false,
			limit: 1000,
			ordercols:null,//可查看字段
			nodetype:null
		},
		//内部列表参数
		tableConfig:{
			elem:null,
			size:'sm',//小尺寸的表格
			height:'full-110',
			where:{},
			page: false,
			limit: 1000,
			limits: [100, 200, 500, 1000, 1500],
			cols:[[
			    {field:'field',title:'字段',width:80,hide:true},
				{field:'name',title:'属性',width:100},
				{field:'value',title:'值',flex:1,minWidth:100}
			]],
			text: {none: '暂无相关数据'}
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,InfoList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,InfoList.tableConfig);
		
		if(me.config.height){
			me.tableConfig.height = me.config.height;
		}
		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
		//数据渲染完的回调
		me.tableConfig.done = function(res, curr, count){
		};
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = TABLE_DOM.join("").replace(/{tableId}/g,me.tableId).replace(/{tableToolbarId}/g,me.tableToolbarId);
		$('#' + me.config.domId).append(html);
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
	};

    //获取查询字段
	Class.prototype.getFields = function(){
		var me = this,
		    cols = me.tableConfig.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
	    return fields.join(',');
	};

	//数据加载
	Class.prototype.loadData = function(data,height){
		var me = this;
		me.uxtable.instance.reload({data:data});
		me.changeSize(height);
	};
	 //改变高度
	Class.prototype.changeSize = function(height){
		var me = this;
        $('#'+me.config.domId).find('div.layui-table-body.layui-table-main').css('height',height+'px');
	};
	//数据清空
	Class.prototype.clearData = function(){
		var me = this;
		me.uxtable.instance.reload({
			data:[]
		});
	};
	//核心入口
	InfoList.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		me.uxtable = uxtable.render(me.tableConfig);
		me.uxtable.instance.reload({
			data:[]
		});
		//监听事件
		me.initListeners();
		
		return me;
	};
	//暴露接口
	exports(MOD_NAME,InfoList);
});