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
		MOD_NAME = 'PreSampleSignParams';
	
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
				Pre_OrderSignFor_DefaultPara_0009: null,//签收方式  1:自动签收；0：手工签收
				Pre_OrderSignFor_DefaultPara_0010: null,//手工签收需要身份验证  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0011: null,//身份验证有效时间  分钟数
				Pre_OrderSignFor_DefaultPara_0012: null,//手动签收后是否清空界面  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0013: null,//自动签收是否自动打印签收单  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0014: null,//打印签收清单同时保存文件  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0015: null,//签收失败是否显示样本信息  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0016: null,//签收显示图片信息  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0017: null,//是否使用打包号查询  格式 1(0)|P 0:不使用，1:使用；P:打包号标识符；中间用|隔开
				Pre_OrderSignFor_DefaultPara_0018: null,//是否按打包号自动签收  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0019: null,//签收时记录送达人  0:否；1:是
				//Pre_OrderSignFor_DefaultPara_0020: null,//送达人为空是否允许签收  0:否；1:是

				Pre_OrderSignFor_DefaultPara_0031: null,//签收后是否自动打印取单凭证  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0032: null,//签收打印取单凭证过滤条件配置  自定义NRequestForm查询条件，如： AND jztype=1 AND ZDY1='外院' AND SampleTypeNo IN (5,6)
				Pre_OrderSignFor_DefaultPara_0033: null,//急查标本签收提示  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0034: null,//签收时是否判断提示医嘱作废信息  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0035: null,//界面排序字段  排序仅限于v_NRequestForm字段
				Pre_OrderSignFor_DefaultPara_0036: null,//是否累加显示签收样本  0:否；1:是

				Pre_OrderSignFor_DefaultPara_0037: null,//标签打印机名
				Pre_OrderSignFor_DefaultPara_0038: null,//签收样本清单打印机名
				Pre_OrderSignFor_DefaultPara_0039: null,//是否显示查询功能  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0040: null,//是否显示就诊类型条件  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0041: null,//就诊类型默认取历史记录值  0:否；1:是
				Pre_OrderSignFor_DefaultPara_0042: null,//是否显示刷新按钮  0:否；1:是  -- 只针对模式4

				Pre_OrderSignFor_DefaultPara_0052: null,//默认查询已签收样本日期范围  >0  为天数
				Pre_OrderSignFor_DefaultPara_0053: null,//过滤的开单科室条件  编号，多个之间需要英文逗号分隔
				Pre_OrderSignFor_DefaultPara_0054: null,//过滤的执行科室条件  编号，多个之间需要英文逗号分隔
				Pre_OrderSignFor_DefaultPara_0055: null,//过滤的检验小组条件  编号，多个之间需要英文逗号分隔
				Pre_OrderSignFor_DefaultPara_0056: null,//过滤的样本类型条件  编号，多个之间需要英文逗号分隔
				Pre_OrderSignFor_DefaultPara_0057: null,//过滤的采样组条件  编号，多个之间需要英文逗号分隔
				Pre_OrderSignFor_DefaultPara_0058: null,//签收样本列表字段 BarCode&条码号&150&hide,BarCode&条码号&150&hide

				Pre_OrderSignFor_DefaultPara_0062: null,//查询条件时间类型
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
			data: {
				nodetype: me.config.nodetype,
				typecode:'Pre_OrderSignFor_DefaultPara',
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