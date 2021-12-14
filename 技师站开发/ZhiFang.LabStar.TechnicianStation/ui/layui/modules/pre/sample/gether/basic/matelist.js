/**
 * @name：modules/pre/sample/inspect/matelist 样本单P匹配列表
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
		MOD_NAME = 'MateList';
	//内部列表+表头dom
	var TABLE_DOM = [
		'<div class="{tableId}-table">',
//		    '<div class="layui-row">',
//		    '<span style="font-size:small;color: #1DA296 ;">未送检列表</span>',
//		    '<button id="refresh" class="layui-btn layui-btn-xs"><i class="iconfont">&#xe669</i>&nbsp;刷新</button>', 
////			  '<div class="layui-col-xs5"><span style="font-size:small;color: #1DA296 ;">未送检列表</span></div>',
////			  '<div class="layui-col-xs4"></div>',
////			  '<div class="layui-col-xs3"><button id="refresh" class="layui-btn layui-btn-xs"><i class="iconfont">&#xe674</i>&nbsp;刷新</button> </div>',
//			'</div>',
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'</div>',
		'<style>',
			'.layui-table-select{background-color:#5FB878;}',
//			'.{tableId}-table .layui-table-body .layui-table-cell{height:80px !important;}',
//			'.{tableId}-table .layui-table-body .layui-table-cell .layui-form-checkbox{margin-top:30px !important;}',
		'</style>'
	];
	  //获样本数据列表服务地址
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreGetSampleGatherFormListByWhere";

	//样本单列表
	var MateList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null,
			page: false,
			limit: 1000,
			nodetype:null,
			//刷新
			refresh:function(){}
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
				{field:'LisBarCodeFormVo_LisBarCodeForm_BarCode',title:'条码号',flex:1},
				{field:'LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName',title:'姓名',flex:1},
				{field:'LisBarCodeFormVo_LisBarCodeForm_IsUrgent',width:70,title:'急查',templet:function(d){
					var arr = [
						'<div style="color:#FF5722;text-align:center;"></div>',
						'<div style="color:#009688;text-align:center;">急</div>'
					];
					var result = d.LisBarCodeFormVo_LisBarCodeForm_IsUrgent == '1' ? arr[1] : arr[0];
					return result;
				}},
				{field:'LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID',title:'采集状态',hide:true},
				{field:'LisBarCodeFormVo_Tab',title:'匹配标识列',hide:true},
			]],
			text: {none: '暂无相关数据' }
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,MateList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,MateList.tableConfig);
		
		if(me.config.height){
			me.tableConfig.height = me.config.height;
		}
		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
		
		//数据渲染完的回调
		me.tableConfig.done = function(res, curr, count){
			res.data.forEach(function (item, index) {
				var IsUrgent = item.LisBarCodeFormVo_LisBarCodeForm_IsUrgent
				if (IsUrgent =='1'){//急查标本条码号和急查列背景色为红色
					//背景色-红色
					$('div[lay-id="'+me.tableId+'"]').
					find('tr[data-index="' + index + '"]').
					find('td[data-field="LisBarCodeFormVo_LisBarCodeForm_BarCode"]').
					css('background-color', 'red');
					//背景色-红色
					$('div[lay-id="'+me.tableId+'"]').
					find('tr[data-index="' + index + '"]').
					find('td[data-field="LisBarCodeFormVo_LisBarCodeForm_IsUrgent"]').
					css('background-color', 'red');
				}
				var tab = item.LisBarCodeFormVo_Tab;
				if(tab=='1'){ //扫码条码匹配后姓名列背景色置为绿色
					$('div[lay-id="'+me.tableId+'"]').
					find('tr[data-index="' + index + '"]').
					find('td[data-field="LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName"]').
					css('background-color', 'green');
					
					$('div[lay-id="'+me.tableId+'"]').
					find('tr[data-index="' + index + '"]').
					find('td[data-field="LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName"]').
					css('color', 'white');
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
		//刷新
		$('#refresh').on('click',function(){
			me.config.refresh && me.config.refresh();
		});
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

	//数据重载  isadd 新增行  
	Class.prototype.loadData = function(data){
		var me = this;
		me.uxtable.instance.reload({
			data:data || []
		});
	};
		//扫码匹配,根据条码号
	Class.prototype.findBarCode = function(barcode){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    //扫码条码匹配后 姓名列背景变绿色  	LisBarCodeFormVo_Tab=1
	    for(var i in list){
	    	if(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == barcode ){
	    		list[i].LisBarCodeFormVo_Tab ='1';
	    	}
	    }
	    me.uxtable.instance.reload({
			data:list
		});
	};
	//撤销扫码匹配,根据条码号
	Class.prototype.findBarCodeRevocation = function(barcode){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    //扫码条码匹配后 姓名列背景变绿色  	LisBarCodeFormVo_Tab=1
	    for(var i in list){
	    	if(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == barcode ){
	    		list[i].LisBarCodeFormVo_Tab ='';
	    	}
	    }
	    me.uxtable.instance.reload({
			data:list
		});
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
	//获取列表数据
	Class.prototype.getListData = function(){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    return list;
	};
	//核心入口
	MateList.render = function(options){
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
	exports(MOD_NAME,MateList);
});