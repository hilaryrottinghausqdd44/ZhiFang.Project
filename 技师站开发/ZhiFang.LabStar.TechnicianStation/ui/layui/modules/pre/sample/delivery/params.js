/**
 * @name：modules/pre/sample/barcode/basic/params 功能参数
 * @author：Jcall
 * @version 2020-08-17
 */
layui.extend({
	uxutil:'ux/util'
}).define(['uxutil'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		MOD_NAME = 'PreSampleDeliveryParams';
	
	//获取功能参数服务地址
	var GET_PARAMS_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_GetParParaAndParaItem";
	
	//功能参数
	var Module = {
		//对外参数
		config: {
			//站点类型
			nodetype: null,
			//功能参数列表
			ParamsList:null,
			//是否加载
			isLoaded:false,
			//枚举
			map: {
				Pre_OrderExchangeDelivery_DefaultPara_0001: null,//就诊类型
				Pre_OrderExchangeDelivery_DefaultPara_0005: null,//已送达提示  0:否；1:是
				Pre_OrderExchangeDelivery_DefaultPara_0006: null,//使用非HG护工号
				Pre_OrderExchangeDelivery_DefaultPara_0007: null,//LIS条码号最小位数
				Pre_OrderExchangeDelivery_DefaultPara_0008: null,//一个条码号多个样本是否累加列表  0:否；1:是
				Pre_OrderExchangeDelivery_DefaultPara_0009: null,//样本列表字段
				Pre_OrderExchangeDelivery_DefaultPara_0010: null,//样本状态节点补录
				Pre_OrderExchangeDelivery_DefaultPara_0011: null,//未条码确认的不能送达确认  CL|||0&不参与校验#1&不允许且提示#2&不允许不提示#3&允许且提示#4&用户自行选择
				Pre_OrderExchangeDelivery_DefaultPara_0012: null,//未采集确认的不能送达确认  CL|||0&不参与校验#1&不允许且提示#2&不允许不提示#3&允许且提示#4&用户自行选择
				Pre_OrderExchangeDelivery_DefaultPara_0013: null,//未送检确认的不能送达确认  CL|||0&不参与校验#1&不允许且提示#2&不允许不提示#3&允许且提示#4&用户自行选择
				Pre_OrderExchangeDelivery_DefaultPara_0014: null,//已签收的不能送达确认  CL|||0&不参与校验#1&不允许且提示#2&不允许不提示#3&允许且提示#4&用户自行选择
				Pre_OrderExchangeDelivery_DefaultPara_0015: null,//已核收的不能送达确认  CL|||0&不参与校验#1&不允许且提示#2&不允许不提示#3&允许且提示#4&用户自行选择
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
		var me = this,
			nodetype = me.config.nodetype;
		if (!nodetype) {
			layer.msg("未找到找点类型!", { icon: 0, anim: 0 });
			return;
		}
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_PARAMS_LIST_URL,
			type:'get',
			data:{
				nodetype: nodetype,
				typecode:'Pre_OrderExchangeDelivery_DefaultPara',
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