/**
 * @name：modules/pre/sample/inspect/params 功能参数
 * @author：liangyl
 * @version 2020-09-23
 */
layui.extend({
	uxutil:'ux/util'
}).define(['uxutil'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		MOD_NAME = 'PreSampleInspectBasicParams';
	
	//获取功能参数服务地址
	var GET_PARAMS_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_GetParParaAndParaItem";

	//功能参数
	var Module = {
		//对外参数
		config:{
			//功能参数列表
			ParamsList:null,
			//是否加载
			isLoaded:false,
			//枚举
			map:{
				Pre_OrderExchangeInspect_DefaultPara_0003:null,//是否记录护送人员
				Pre_OrderExchangeInspect_DefaultPara_0005:null,//是否显示打印清单按钮
				Pre_OrderExchangeInspect_DefaultPara_0006:null,//批量送检确认验证送检人
				Pre_OrderExchangeInspect_DefaultPara_0011:null,//查询默认科室
				Pre_OrderExchangeInspect_DefaultPara_0015:null, //允许手工录入护工
				Pre_OrderExchangeInspect_DefaultPara_0019:null,//是否显示撤销送检按钮
				Pre_OrderExchangeInspect_DefaultPara_0021:null,//手动控制护送人员
				Pre_OrderExchangeInspect_DefaultPara_0022:null,//打印附加清单
				Pre_OrderExchangeInspect_DefaultPara_0023:null,//送检清单打印机
				Pre_OrderExchangeInspect_DefaultPara_0024:null,//送检查询字段
				Pre_OrderExchangeInspect_DefaultPara_0025:null,//使用非HG护工号
		        Pre_OrderExchangeInspect_DefaultPara_0026:null,//LIS条码号最小位数
				Pre_OrderExchangeInspect_DefaultPara_0027:null,//样本列表字段
				Pre_OrderExchangeInspect_DefaultPara_0013:null,  //运送人显示方式
				Pre_OrderExchangeInspect_DefaultPara_0034:null,  //是否开启撤销送检校验
				Pre_OrderExchangeInspect_DefaultPara_0016:null,//是否开启送检校验
				Pre_OrderExchangeInspect_DefaultPara_0017:null //根据送检信息特定字段匹配数据是否加入列表
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
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_PARAMS_LIST_URL,
			type:'get',
			data:{
				nodetype:me.config.nodetype,
				typecode:'Pre_OrderExchangeInspect_DefaultPara',//'Pre_OrderBarCode',
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