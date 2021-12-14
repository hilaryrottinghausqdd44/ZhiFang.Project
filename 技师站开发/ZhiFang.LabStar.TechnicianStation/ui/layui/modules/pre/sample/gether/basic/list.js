/**
 * @name：modules/pre/sample/inspect/list 样本单列表
 * @author：liangyl
 * @version 2021-09-07
 */
layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table'
}).define(['uxutil','uxtable'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		MOD_NAME = 'BarCodeFormList';
	//内部列表+表头dom
	var TABLE_DOM = [
		'<div class="{tableId}-table">',
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'</div>',
		'<style>',
			'.layui-table-select{background-color:#5FB878;}',
		'</style>'
	];
	//获样本数据列表服务地址
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreGetSampleGatherFormListByWhere";
	//样本单列表
	var BarCodeFormList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null,
			cols:[],
			page: false,
			limit: 1000,
			nodetype:null,//站点类型
			PrinterName:null,//采集清单打印机
			IsPreview:1 //是否预览，1是默认预览  0就是不预览
		},
		//内部列表参数
		tableConfig:{
			elem:null,
//			toolbar:null,
//			skin:'line',//行边框风格
			size:'sm',//小尺寸的表格
			height:'full-110',
			where:{},
			page: false,
			limit: 1000,
			limits: [100, 200, 500, 1000, 1500],
			cols:[[
				{type:'checkbox'}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,BarCodeFormList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,BarCodeFormList.tableConfig);
		
		if(me.config.height){
			me.tableConfig.height = me.config.height;
		}
		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
		me.tableConfig.cols = [me.getCols()];
		//数据渲染完的回调
		me.tableConfig.done = function(res, curr, count){
			res.data.forEach(function (item, index) {
				var ColorValue = item.LisBarCodeFormVo_LisBarCodeForm_ColorValue;
				if (ColorValue){//采样管颜色
					//背景色-红色
					$('div[lay-id="'+me.tableId+'"]').
					find('tr[data-index="' + index + '"]').
					find('td[data-field="LisBarCodeFormVo_LisBarCodeForm_Color"]').
					css('background-color', ColorValue);
				}
			});
		};
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = TABLE_DOM.join("").replace(/{tableId}/g,me.tableId).replace(/{tableToolbarId}/g,me.tableToolbarId);
		$('#' + me.config.domId).append(html);
	};
		//列表列
	Class.prototype.getCols = function(){
		var me = this;
		var cols = [
			{type: 'checkbox', fixed: 'left'},
			{field:'LisBarCodeFormVo_IsConfirm', width:80, title: '是否已采集', hide: true},
			{field:'LisBarCodeFormVo_CollectPackNo', width:80, title: '打包号', hide: true},
			{field:'LisBarCodeFormVo_LisBarCodeForm_Id', width:80, title: 'LisBarCodeForm_Id', hide: true},
			{field:'LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID', width:80, title: '状态', hide: true},
			{field:'LisBarCodeFormVo_failureInfo', width:80, title: 'LisBarCodeFormVo_failureInfo', hide: true},
		    {field:'LisBarCodeFormVo_LisBarCodeForm_ColorValue', width:80, title: 'LisBarCodeFormVo_LisBarCodeForm_ColorValue', hide: true}];
		
		for(var i in me.config.cols){
			//BarCode&条码号&100&show
			var arr = me.config.cols[i].split('&');
			cols.push({
				field:arr[0],title:arr[1],width:arr[2],
				hide:(arr[3] == 'show' ? false : true)
			});
		}
		return cols;
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
//		//触发行单击事件
//		me.uxtable.table.on('row(' + me.tableId + ')', function(obj){
//			if(me.checkedTr){
//				me.checkedTr.removeClass('layui-table-select');
//			}
//			me.checkedTr = obj.tr;
//			me.checkedTr.addClass('layui-table-select');
//		});
	};
	 //查询数据
	Class.prototype.onSearch = function(where,callback){
		var me = this;
		var config = {
			type:'post',
			url:GET_LIST_URL,
			data:JSON.stringify({
				nodetype:me.config.nodetype,//站点类型
				where:where,
				fields:me.getFields(),
			    isPlanish:true 
			})
		};
        var loadIndex = layer.load();
		uxutil.server.ajax(config,function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = (data.value ||{}).list || [];
				me.uxtable.instance.reload({
					data:list
				});
//				callback(list);
			}else{
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}
		});
	};
	//获取勾选的条码数组
	Class.prototype.getCheckedBarcodes = function(){
		var me = this,
			barcodes = [];
		
		var checkedList = me.uxtable.table.checkStatus(me.config.domId + '-table').data;
		for(var i in checkedList){
			barcodes.push(checkedList[i].LisBarCodeFormVo_LisBarCodeForm_BarCode);
		}
		return barcodes;
	};
	 //获取勾选的数组
	Class.prototype.getCheckedList = function(){
		var me = this,
			barcodes = [];
		var checkedList = me.uxtable.table.checkStatus(me.config.domId + '-table').data;
		return checkedList;
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
	 //数据加载(新增数据)
    Class.prototype.loadData = function(data){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    for(var i=0;i<data.length;i++){  //新增数据
	    	var isExec=true;
	    	for(var j=0;j<list.length;j++){
	    		if(data[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == list[j].LisBarCodeFormVo_LisBarCodeForm_BarCode){
	    			isExec = false;	
	    			data[i].LisBarCodeFormVo_IsConfirm  =  data[i].LisBarCodeFormVo_IsConfirm;
	    			data[i].LAY_CHECKED='true';
	    			list[j] = data[i];
	    		}
	    	}
	    	if(isExec)list.push(data[i]);
	    }
		me.uxtable.instance.reload({
			url: '',
			data:list || []
		});
	};
	 //如果条码已存在，只勾选行
    Class.prototype.checkRow = function(BarCode){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    for(var i=0;i<list.length;i++){  //新增数据
	    	if(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == BarCode){
    		    list[i].LAY_CHECKED='true';
    		}
	    }
		me.uxtable.instance.reload({
			url: '',
			data:list || []
		});
	};
	
	
	
	//数据清空
	Class.prototype.clearData = function(){
		var me = this;
		me.uxtable.instance.reload({
			data:[]
		});
	};
	//列表总数
	Class.prototype.getNum = function(){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    return list.length;
	};
	//获取列表数据
	Class.prototype.getListData = function(){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    return list;
	};
		 //改变高度
	Class.prototype.changeSize = function(height){
		var me = this;
		height = height-28; 
        $('#'+me.config.domId).find('div.layui-table-body.layui-table-main').css('height',height+'px');
	};
	
	//核心入口
	BarCodeFormList.render = function(options){
		var me = new Class(options);
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		me.uxtable = uxtable.render(me.tableConfig);
		me.uxtable.instance.reload({
			url: '',
			data:[]
		});
		//监听事件
		me.initListeners();
		
		return me;
	};
	//暴露接口
	exports(MOD_NAME,BarCodeFormList);
});