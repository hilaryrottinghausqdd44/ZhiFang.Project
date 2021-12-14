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
	//查询
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreGetSampleExchangeInspectFormListByWhere";
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
		me.tableConfig.cols = me.config.cols;
		
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
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
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
		    cols = me.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
	    return fields.join(',');
	};

	//新增行  
	Class.prototype.addRow = function(data){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
		list.push.apply(list, data);
		me.uxtable.instance.reload({
			url: '',
			data:list || []
		});
	};
	 //数据加载()
    Class.prototype.loadData = function(data){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    
        for(var i=0;i<list.length;i++){
    		if(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID >'4'){
    			list[i].LisBarCodeFormVo_IsConfirm = '1';
    		}
    	}
	    for(var i=0;i<data.length;i++){  //新增数据
	    	var isExec=true;
	    	for(var j=0;j<list.length;j++){
	    		if(data[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == list[j].LisBarCodeFormVo_LisBarCodeForm_BarCode){
	    			list[j].LisBarCodeFormVo_IsConfirm = '1';
	    			data[i].LAY_CHECKED='true';
	    			isExec = false;	
	    		}
	    	}
	    	if(isExec)list.push(data[i]);
	    }
		me.uxtable.instance.reload({
			url: '',
			data:list || []
		});
	};
	 //撤销送检后数据加载,撤销行去掉已确认标识
    Class.prototype.revokeLoadData = function(barcode){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	  
    	for(var i=0;i<list.length;i++){
			if(barcode== list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode){
				list[i].LisBarCodeFormVo_IsConfirm = '';
				break;
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
	//根据送检信息特定字段匹配数据是否加入列表(参数)，Field --匹配字段，FieldValue -匹配值    ，true - 能加入
	Class.prototype.isSpecificField = function(Field,FieldValue){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    var isExec = false;
    	for(var i=0;i<list.length;i++){
    		 //样本送检-匹配字段，相同内容才可以刷入条码，如ZDY1
    		if(list[i][Field] == FieldValue){
    			isExec = true;
    			break;
    		}
    	}
        return isExec;
	};
	 //改变高度
	Class.prototype.changeSize = function(height){
		var me = this;
	   	height = height-30; 
        $('#'+me.config.domId).find('div.layui-table-body.layui-table-main').css('height',height+'px');
	};
	 //查询数据
	Class.prototype.onSearch = function(where,relationForm,callback){
		var me = this;
		var config = {
			type:'post',
			url:GET_LIST_URL,
			data:JSON.stringify({
				nodetype:me.config.nodetype,//站点类型
				where:where,
				relationForm:relationForm,
				fields:me.getFields(),
			    isPlanish:true 
			})
		};
        var loadIndex = layer.load();
		uxutil.server.ajax(config,function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = (data.value ||{}).list || [];
				for(var i=0;i<list.length;i++){
					var BarCodeStatusID = list[i].LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID;
		    		if(Number(BarCodeStatusID) >4){
		    			list[i].LisBarCodeFormVo_IsConfirm = '1';
		    		}
		    	}
				callback(list);
			}else{
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}
		});
	};
	 //打印清单
	Class.prototype.onListPrint = function(IsOrderListPrinter){
		var me = this,
			CheckedList = me.getCheckedList();
			
		if(CheckedList.length == 0){
			layer.msg("请勾选样本条码！",{icon:5});
		}else{
			var data = [];
			for(var i in CheckedList){
				//已确认的数据行
				if(CheckedList[i].LisBarCodeFormVo_IsConfirm =='1')data.push(CheckedList[i]);
			}
			if(data.length==0){
				layer.msg('请先确认送检再打印清单',{ icon: 5 });
				return false;
			}
			var title = '打印清单',
			  ModelType = '11',
			  ModelTypeName='样本送检_样本清单';
			me.onPrint(IsOrderListPrinter,data,title,ModelType,ModelTypeName);
		}
	}; 
	//打印
	Class.prototype.onPrint = function(IsOrderListPrinter,data,title,ModelType,ModelTypeName){
		var me = this;
		data = data || [];
		if(data.length==0)return false;
		 //去除前缀
		data = JSON.stringify([data]).replace(RegExp("LisBarCodeFormVo_", "g"), "").replace(RegExp("LisBarCodeForm_", "g"), "");

		var PrinterName = me.config.PrinterName;
		var IsPreview = me.config.IsPreview;
		var url = uxutil.path.LAYUI + '/views/system/comm/JsonPrintTemplateManage/print/index.html?BusinessType=3&ModelType='+ModelType+'&ModelTypeName='+ModelTypeName+'&isDownLoadPDF=true&IsPreview='+IsPreview+ (PrinterName ? ("&PrinterName=" + PrinterName) : "");
		layer.open({
			title:title,
			type:2,
			content:url,
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['500px','380px'],
			success:function(layero,index){
				var iframe = $(layero).find('iframe')[0].contentWindow;
				iframe.PrintDataStr = data;
			},
			end:function(){
				//打印附加清单
		        if(IsOrderListPrinter)me.onOrderListPrint()
			}
		});
	};
	 //打印附加清单
	Class.prototype.onOrderListPrint = function(){
		var me = this,
			CheckedList = me.getCheckedList();
			
		if(CheckedList.length == 0){
			layer.msg("请勾选样本条码！",{icon:5});
		}else{
			var data = [];
			for(var i in CheckedList){
				//已确认的数据行
				if(CheckedList[i].LisBarCodeFormVo_IsConfirm =='1')data.push(CheckedList[i]);
			}
			if(data.length==0){
				layer.msg('请先确认送检再打印附加清单',{ icon: 5 });
				return false;
			}
			var title = '打印附加清单',
			  ModelType = '12',
			  ModelTypeName='样本送检_附加清单';
			me.onPrint(null,data,title,ModelType,ModelTypeName);
		}
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