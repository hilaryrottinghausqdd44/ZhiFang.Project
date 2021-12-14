/**
 * @name：modules/pre/sample/basic/hosttype 站点类型
 * @author：Jcall
 * @version 2020-08-17
 * @author：liangyl  增加paraTypeCode对外参数      增加的原因:  进入模块只看当前模块对应的站点类型
 */
layui.extend({
	uxutil:'ux/util',
}).define(['uxutil'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		MOD_NAME = 'PreSampleBarcodeBasicHostType';
	
	//获取站点列表服务地址
	var GET_HOST_TYPE_USER_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_SearchBHostTypeUserAndHostTypeNameByHQL";
	
	//站点类型
	var PreSampleBarcodeBasicHostType = {
		//对外参数
		config:{
			//站点信息列表
			HostTypeUserList:null,
			paraTypeCode:'Pre_OrderBarCode_DefaultPara'
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,PreSampleBarcodeBasicHostType.config,setings);
	};
	//获取站点信息列表
	Class.prototype.getDataList = function(callback){
		var me = this;
		if(me.config.HostTypeUserList){
			callback(me.config.HostTypeUserList);
		}else{
			me._getHostTypeUserListFromServer(function(){
				callback(me.config.HostTypeUserList);
			});
		}
	};
	//从服务器获取站点信息列表
	Class.prototype._getHostTypeUserListFromServer = function(callback){
		var me = this,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID);
		
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_HOST_TYPE_USER_LIST_URL,
			type:'get',
			data:{
				page:1,
				limit:1000,
				fields:'BHostTypeUser_HostTypeID,BHostTypeUser_HostTypeName',
				systemTypeCode:'1',
				paraTypeCode:me.config.paraTypeCode,
				where:'bhosttypeuser.EmpID=' + empId
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				me.config.HostTypeUserList = (data.value ||{}).list || [];
				callback();
			}else{
				layer.msg(data.msg,{icon:5});
			}
		},true);
	};
	
	//获取人员站点信息
	Class.prototype.getHistoryInfo = function(){
		var me = this,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID);
			
		return uxutil.localStorage.get('PreSampleBarcodeBasicHostType_'+me.config.paraTypeCode + empId,true);
	};
	//记录人员站点信息{HostTypeID:'',HostTypeName:''}
	Class.prototype.insertHistoryInfo = function(info){
		var me = this,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID);
			
		uxutil.localStorage.set('PreSampleBarcodeBasicHostType_'+me.config.paraTypeCode + empId,JSON.stringify(info));
	};
	
	//核心入口
	PreSampleBarcodeBasicHostType.render = function(options){
		var me = new Class(options);
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,PreSampleBarcodeBasicHostType);
});