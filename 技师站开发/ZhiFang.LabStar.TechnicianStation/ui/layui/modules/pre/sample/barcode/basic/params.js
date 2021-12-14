/**
 * @name：modules/pre/sample/barcode/basic/params 功能参数
 * @author：Jcall
 * @version 2020-08-17
 */
layui.extend({
	uxutil:'ux/util',
	//PreSampleBarcodeBasicHostType:'modules/common/hosttype'
}).define(['uxutil',],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		//PreSampleBarcodeBasicHostType = layui.PreSampleBarcodeBasicHostType,
		MOD_NAME = 'PreSampleBarcodeBasicParams';
	
	//获取功能参数服务地址
	var GET_PARAMS_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_GetParParaAndParaItem";
	//站点类型实例
	//var PreSampleBarcodeBasicHostTypeInstance = PreSampleBarcodeBasicHostType.render();
	
	//功能参数
	var Module = {
		//对外参数
		config:{
			//功能参数列表
			ParamsList:null,
			//是否加载
			isLoaded: false,
			nodetype:null,//站点类型
			//枚举
			map:{
				Pre_OrderBarCode_DefaultPara_0015:null,//核收条件选择
				Pre_OrderBarCode_DefaultPara_0018:null,//科室选择
				Pre_OrderBarCode_DefaultPara_0019:null,//病区选择
				Pre_OrderBarCode_DefaultPara_0021:null,//样本过滤天数
				Pre_OrderBarCode_DefaultPara_0024:null,//自定义核收数据过滤天数，是否可见，1/0
				Pre_OrderBarCode_DefaultPara_0064:null//分管列表字段
			}
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,Module.config,setings);
	};
	//从服务器获取功能参数列表
	Class.prototype._getParamsFromServer = function(callback){
		var me = this;
			//HistoryInfo = PreSampleBarcodeBasicHostTypeInstance.getHistoryInfo();
		
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_PARAMS_LIST_URL,
			type:'get',
			data: {
				nodetype: me.config.nodetype,
				typecode:'Pre_OrderBarCode_DefaultPara',//'Pre_OrderBarCode',
				paranos:''
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				me.config.ParamsList = data.value || [];
				callback();
			}else{
				layer.msg(data.msg,{icon:5});
			}
		},true);
	};
	
	//根据参数编码获取参数值
	Class.prototype.init = function(callback){
		var me = this;
		me._getParamsFromServer(function(){
			var list = me.config.ParamsList;
			for(var i in list){
				me.config.map[list[i].ParaNo] = list[i].ParaValue;
			}
			callback();
		});
	};
	//根据参数编码获取参数值
	Class.prototype.get = function(key){
		return this.config.map[key];
	};
	
	//核心入口
	Module.render = function(options){
		var me = new Class(options);
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,Module);
});