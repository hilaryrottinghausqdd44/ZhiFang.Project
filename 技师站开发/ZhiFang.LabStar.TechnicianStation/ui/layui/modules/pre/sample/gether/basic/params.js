/**
 * @name：modules/pre/sample/gether/basic/params 功能参数
 * @author：liangyl
 * @version 2020-09-07
 */
layui.extend({
	uxutil:'ux/util'
}).define(['uxutil'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		MOD_NAME = 'PreSampleGetherBasicParams';
	
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
				Pre_OrderGether_DefaultPara_0003:null,//特定核收字段及名称
				Pre_OrderGether_DefaultPara_0005:null,//采集确认确认并打印清单
				Pre_OrderGether_DefaultPara_0006:null,//批量确认模式验证采集人
				Pre_OrderGether_DefaultPara_0008:null,//查询时间字段
				Pre_OrderGether_DefaultPara_0013:null,//样本列表字段
				Pre_OrderGether_DefaultPara_0007:null,//是否开启采集校验
				Pre_OrderGether_DefaultPara_0010:null, //采集清单打印机
				Pre_OrderGether_DefaultPara_0011:null//采集清单是否预览
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
				typecode:'Pre_OrderGether_DefaultPara',//'Pre_OrderBarCode',
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