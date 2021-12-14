/**
 * @name：modules/pre/sample/delivery/list 签收列表
 * @author：zhangda
 * @version 2021-07-28
 */
layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table'
}).define(['uxutil','laydate','uxtable','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		laydate = layui.laydate,
		form = layui.form,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		MOD_NAME = 'list';

	//内部列表+表头dom
	var TABLE_DOM = [
		'<div class="{tableId}-table">',
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'</div>',
		'<div class="layui-form" style="padding:2px;margin-bottom:2px;border:1px solid #e6e6e6;">',
			'<div class="layui-form-item" style="margin-bottom:0;">',
				'<div id="{tableId}-div" style="height:30px;overflow-y:auto;"></div>' +
			'</div>',
		'</div>',
		'<style>',
			'.layui-table-select{background-color:#5FB878;color:#fff;}',
			'.{tableId}-table .layui-table-body .layui-table-cell{height:18px !important;}',
			//'.{tableId}-table .layui-table-body .layui-table-cell .layui-form-checkbox{margin-top:30px !important;}',
		'</style>'
	];

	//医嘱单列表
	var list = {
		tableId:null,//列表ID
		tableToolbarId: null,//列表功能栏ID

		//对外参数
		config:{
			domId:null,
			height: null,

			modelType: null,
			nodetype: null,
			colsStr: null
		},
		//内部列表参数
		tableConfig:{
			elem:null,
			toolbar:null,
			skin:'line',//行边框风格
			//even:true,//开启隔行背景
			size:'sm',//小尺寸的表格
			defaultToolbar:null,
			height:'full-4',
			defaultLoad:true,
			url: '',
			data:[],
			where:{},
			cols:[[]],
			response: function () {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) {//res即为原始返回的数据
				return {
					"code": res.code, //解析接口状态
					"msg": res.msg, //解析提示文本
					"count": res.count || 0, //解析数据长度
					"data": res.data || []
				};
			},
		}
	};
	//构造器
	var Class = function(setings)
	{  
		var me = this;
		me.config = $.extend({},me.config,list.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,list.tableConfig);
		
		if(me.config.height){
			me.tableConfig.height = me.config.height;
		}
		me.tableId = me.config.domId + "-table";
		//me.tableToolbarId = me.config.domId + "-table-toolbar-top";
		me.tableConfig.elem = "#" + me.tableId;
		//me.tableConfig.toolbar = "#" + me.tableToolbarId;
		//配置列
		me.tableConfig.cols = me.initCols(me.config.colsStr);

		//数据渲染完的回调
		me.tableConfig.done = function(res, curr, count){
			if(count > 0){
				//默认选中第一行
				me.onClickFirstRow();
			}
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
		//me.uxtable.table.on('row(' + me.tableId + ')', function (obj) {
		//	obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		//});
	};
	//查询处理
	Class.prototype.onSearch = function (list){
		var me = this;
		me.uxtable.instance.reload({
			url: '',
			data:list || []
		});
	};
	//默认选中第一行
	Class.prototype.onClickFirstRow = function(){
		var me = this;
		setTimeout(function () {
			$("#" + me.tableId + "+div").find('.layui-table-main tr[data-index="0"]').click();
		}, 0);
		
	};
	//转换配置列
	Class.prototype.initCols = function (colsStr) {
		var me = this,
			colsStr = colsStr || '',
			colsStrArr = colsStr.split(",") || [],
			cols = [];
		//不存在 默认加上
		if (colsStr.indexOf('LisBarCodeFormVo_LisBarCodeForm_BarCode') == -1) colsStrArr.unshift("LisBarCodeFormVo_LisBarCodeForm_BarCode");

		//行号+复选
		cols.unshift({ type: 'checkbox', fixed: 'left' },{ type: 'numbers', fixed: 'left', title: '行号' });
		//LisBarCodeFormVo_LisBarCodeForm_BarCode&条码号&100&show,LisBarCodeFormVo_LisBarCodeForm_OrderExecTime&医嘱指定执行时间&100&show
		$.each(colsStr.split(","), function (i, item) {
			var obj = {};
			var arr = ["field", "title", "width", "hide"];
			var data = item.split("&");
			for (var i in data) {
				if (i == 3)
					obj[arr[i]] = data[i] == 'show' ? false : (data[i] == 'hide' ? true : data[i]);
				else
					obj[arr[i]] = data[i];

			}
			cols.push(obj);
		});
		cols.push({ field: 'LisBarCodeFormVo_failureInfo', title: '错误信息', with:60,hide:true });
		return [cols];
	};
	//获得列字段
	Class.prototype.getStoreFields = function (isString) {
		var me = this,
			columns = me.tableConfig["cols"][0] || [],
			length = columns.length,
			fields = [];
		for (var i = 0; i < length; i++) {
			if (columns[i].field) {
				var obj = isString ? columns[i].field : {
					name: columns[i].field,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	};

	//核心入口
	list.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		//实例化列表
		me.uxtable = uxtable.render(me.tableConfig);
		//监听事件
		me.initListeners();
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,list);
});