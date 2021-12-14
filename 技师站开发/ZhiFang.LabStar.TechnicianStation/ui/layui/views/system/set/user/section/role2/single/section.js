/**
 * @name：选择小组列表 
 * @author：liangyl
 * @version 2021-11-03
 */
layui.extend({
}).define(['uxutil','uxtable'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		MOD_NAME = 'SectionList';
	//内部列表+表头dom
	var TABLE_DOM = [
	    '<div class="layui-form" >',
			'<div class="layui-form-item" style="margin-bottom:0;">',
				'<div class="layui-inline"  style="height:25px">',
				  '<button type="button" id="{tableId}-btn" class="layui-btn layui-btn-xs"><i class="iconfont">&#xe664;</i>&nbsp;选择小组</button>', 
				'</div>',
			'</div>',
		'</div>',
		'<div class="{tableId}-table">',
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'</div>',
		'<style>',
			'.layui-table-select{background-color:#5FB878;}',
		'</style>'
	];
	//获取检验中权限列表数据 --- 单模式
	var GET_LBRIGHT_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL?isPlanish=true';
	//员工ID
	var EmpID = null; 
	//员工姓名
	var EmpName = null;
	//样本单列表
	var SectionList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null,
			//行选择触发事件
			rowClickFun:function(){},
			done:function(){}
		},
		//内部列表参数
		tableConfig:{
			elem:null,
			size:'sm',//小尺寸的表格
			height:'full-110',
			where:{},
			page: false,
			limit: 5000,
			limits: [50,100, 200, 500, 1000, 1500],
		    cols:[[
	            {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBRight_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBRight_EmpID',title:'LBRight_EmpID',width:150,sort:true,hide:true},
				{field:'LBRight_RoleID',title:'LBRight_RoleID',width:150,sort:true,hide:true},
				{field:'LBRight_LBSection_Id',title:'LBRight_LBSection_Id',width:150,sort:true,hide:true},
				{field:'LBRight_LBSection_CName',title:'小组名称',minWidth:120,flex:1,sort:true}
			]],
			text: {none: '暂无相关数据'}
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,SectionList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,SectionList.tableConfig);
		
		if(me.config.height){
			me.tableConfig.height = me.config.height;
		}
		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
		//数据渲染完的回调
		//数据渲染完的回调
		me.tableConfig.done = function(res, curr, count){
			if(count>0)me.onClickFirstRow();
			me.config.done && me.config.done(res, curr, count);
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
			//触发行单击事件
		me.uxtable.table.on('row(' + me.tableId + ')', function(obj){
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        	me.config.rowClickFun && me.config.rowClickFun(obj);
		});
		//选择小组
	    $('#'+me.tableId+'-btn').on('click',function(){
	    	me.OpenWin();
		});
		
	};
		//默认选中第一行
	Class.prototype.onClickFirstRow = function(){
		var me = this;
		setTimeout(function () {
			$("#" + me.tableId + "+div").find('.layui-table-main tr[data-index="0"]').click();
		}, 0);
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
	Class.prototype.onSearch = function(id,name){
		var me = this;
		EmpID = id;
		EmpName = name;
		var whereObj ={"where":"lbright.EmpID="+EmpID+' and lbright.LBSection.Id is not null and lbright.RoleID is null'};
		me.loadData(whereObj);
	};
	//数据加载
	Class.prototype.loadData = function(whereObj){
		var me = this;
		me.uxtable.instance.reload({
			url: GET_LBRIGHT_LIST_URL,
			where:$.extend({},whereObj,{
				fields:me.getFields(),
				page:1,
				limit:1000,
				sort: JSON.stringify(me.tableConfig.defaultOrderBy)
			})
		});
	};
	//数据清空
	Class.prototype.clearData = function(){
		var me = this;
		me.uxtable.instance.reload({
			data:[]
		});
	};
	//弹出小组选择框
	Class.prototype.OpenWin = function(){
		var me = this,
		    isF = true;
		var win = $(window),
			maxWidth = win.width()-100,
			maxHeight = win.height()-80,
			width = maxWidth > 800 ? maxWidth : 800,
			height = maxHeight > 600 ? maxHeight : 600;
		layer.open({
			title:'选择小组',
			type:2,
			content: '../role2/single/transfer/app.html?EmpID='+EmpID+'&EmpName='+EmpName,
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[width+'px',height+'px'],
			success: function(layero, index){
       	    },
	        end: function () {
	        	if(isF)me.onSearch(EmpID,EmpName);
            },
	        cancel: function (index, layero) {
	        	isF = false;
	        	parent.layer.closeAll('iframe');
            }
		});
	};
	//核心入口
	SectionList.render = function(options){
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
	exports(MOD_NAME,SectionList);
});