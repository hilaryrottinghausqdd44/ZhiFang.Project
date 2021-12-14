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
		MOD_NAME = 'PreSampleTranruleBasicParams';
	
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
				Pre_OrderDispense_DefaultPara_0063:null,//分发方式
				Pre_OrderDispense_DefaultPara_0040:null,//是否显示就诊类型条件(条码框后的就诊类型)
 				Pre_OrderDispense_DefaultPara_0041:null,//就诊类型默认取历史记录值(条码框后的就诊类型)
 				Pre_OrderDispense_DefaultPara_0039:null,//是否显示查询功能
 				//查询条件（默认值)
				Pre_OrderDispense_DefaultPara_0053:null,//过滤的开单科室条件
				Pre_OrderDispense_DefaultPara_0055:null,//过滤的检验小组条件
				Pre_OrderDispense_DefaultPara_0056:null, //过滤的样本类型条件
				//查询条件
				Pre_OrderDispense_DefaultPara_0052:null,//默认查询已签收样本日期范围,选已签收时 显示默认的时间范围
				Pre_OrderDispense_DefaultPara_0062:null,//查询条件时间类型  ---时间类型下拉内容
				//样本单列表
				Pre_OrderDispense_DefaultPara_0058:null,//分发样本列表字段
				Pre_OrderDispense_DefaultPara_0035:null,//界面排序字段
				Pre_OrderDispense_DefaultPara_0012:null,//手动签收后是否清空界面
				//医嘱信息
				Pre_OrderDispense_DefaultPara_0070:null,//医嘱信息显示字段
				
				Pre_OrderDispense_DefaultPara_0037:null,//标签打印机名  ---补打条码的打印机名
				Pre_OrderDispense_DefaultPara_0038:null,//样本清单打印机名
				Pre_OrderDispense_DefaultPara_0065:null,//是否显示分发日期下拉框
				Pre_OrderDispense_DefaultPara_0067:null,//分发失败是否提示
				
				Pre_OrderDispense_DefaultPara_0009:null,//签收方式
				Pre_OrderDispense_DefaultPara_0010:null,//手工签收需要身份验证
				Pre_OrderDispense_DefaultPara_0011:null, //身份验证有效时间  分钟数
								
				Pre_OrderDispense_DefaultPara_0013:null, //自动签收是否自动打印签收单
				Pre_OrderDispense_DefaultPara_0015:null,//签收失败是否显示样本信息
				Pre_OrderDispense_DefaultPara_0031:null //签收后是否自动打印取单凭证
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
				typecode:'Pre_OrderDispense_DefaultPara',
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