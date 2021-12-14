/**
 * @name：人员列表 
 * @author：liangyl
 * @version 2021-11-03
 */
layui.extend({
}).define(['uxutil','uxtable'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		MOD_NAME = 'EmpList';
	//内部列表+表头dom
	var TABLE_DOM = [
      	'<div class="layui-form">',
			'<div class="layui-form-item" style="margin-bottom:0;">',
				'<div class="layui-inline">',
				  '<div class="layui-input-inline">',
					'<input type="text" name="{tableId}-Name" id="{tableId}-Name" placeholder="员工姓名/员工代码" autocomplete="off" class="layui-input" />', 
				  '</div>',
				'</div>',
				'<div class="layui-inline">',
		          '<button type="button" id="{tableId}-btn" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-search"></i>查询</button>', 
				'</div>',
			'</div>',
		'</div>',
		'<div class="{tableId}-table">',
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		    '<script type="text/html" id="isuseTpl">',
	          '<input type="checkbox" name="IsUse" title="" disabled="disabled" lay-skin="primary" lay-filter="IsUse" {{ d.HREmpIdentity_HREmployee_IsUse == "true" ? "checked" : "" }} >',
	        '</script>',
		'</div>',
		'<style>',
			'.layui-table-select{background-color:#5FB878;}',
		'</style>'
	];
	//获取人员表列表数据
	var GET_EMP_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?isPlanish=true';
	//员工列表选择行
	var EMP_CHECK_ROW_DATA =[];
	//样本单列表
	var EmpList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null,
			MODEL:'single', //选择模式,默认单选
			//行选择触发事件
			rowClickFun:function(){},
			//复选勾选触发事件
			checkClickFun:function(){},
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
				{field:'HREmpIdentity_HREmployee_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'HREmpIdentity_HREmployee_CName',title:'员工姓名',width:150,sort:true},
				{field:'HREmpIdentity_HREmployee_UseCode',title:'员工代码',width:100,sort:true},
				{field:'HREmpIdentity_HREmployee_HRDept_CName',title:'隶属部门',width:150,sort:true},
				{field:'HREmpIdentity_HREmployee_IsUse',title:'使用',width:100,sort:true,align:'center',templet: '#isuseTpl'}
			]],
			text: {none: '暂无相关数据'}
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,EmpList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,EmpList.tableConfig);
		
		if(me.config.height){
			me.tableConfig.height = me.config.height;
		}
		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
		console.log(me.config.MODEL);
		//多选模式增加复选框
		if(me.config.MODEL!='single'){
			me.tableConfig.cols[0].splice(0,0,{type: 'checkbox', fixed: 'left'});
		}
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
		var me = this,
		    iTime = null;
		//触发行单击事件
		me.uxtable.table.on('row(' + me.tableId + ')', function(obj){
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        	me.config.rowClickFun && me.config.rowClickFun(obj);
		});
		me.uxtable.table.on('checkbox(' + me.tableId + ')', function(obj){
            clearTimeout(iTime);
            iTime = setTimeout(function () {
            	me.config.checkClickFun && me.config.checkClickFun(me.getCheckedList());
            }, 500);
		});
		$('#'+me.tableId+'-btn').on('click',function(){
			me.loadData();
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
	Class.prototype.loadData = function(){
		var me = this;
	    var where = "(hrempidentity.IsUse=1 and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.TSysCode='1001001')";
		var searchText = $('#'+me.tableId+'-Name').val();
		if(searchText){
			where+=" and (hrempidentity.HREmployee.CName like '%" + searchText +
	    		"%' or hrempidentity.HREmployee.UseCode like '%" + searchText +"%')";
		}
		var whereObj ={"where":where};
		me.uxtable.instance.reload({
			url: GET_EMP_LIST_URL,
			where:$.extend({},whereObj,{
				fields:me.getFields()
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
	//获取勾选的数组
	Class.prototype.getCheckedList = function(){
		var me = this;
		var checkedList = me.uxtable.table.checkStatus(me.tableId).data;
		return checkedList;
	};
	//核心入口
	EmpList.render = function(options){
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
	exports(MOD_NAME,EmpList);
});