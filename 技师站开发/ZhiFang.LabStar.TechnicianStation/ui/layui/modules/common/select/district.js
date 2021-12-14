/**
 * @name：modules/common/select/district 下拉框-病区
 * @author：Jcall
 * @version 2021-09-01
 */
layui.extend({
	uxutil:'ux/util',
	CommonSelectBasic:'modules/common/select/district'
}).define(['uxutil','CommonSelectBasic'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		CommonSelectBasic = layui.CommonSelectBasic,
		MOD_NAME = 'CommonSelectDistrict';
	
	var SELECT_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL?isPlanish=true" + 
		"&fields=HRDeptIdentity_HRDept_Id,HRDeptIdentity_HRDept_CName" + 
		"&where=hrdeptidentity.TSysCode='1001102' and hrdeptidentity.SystemCode='ZF_LAB_START'";
	
	//下拉框基础类
	var Module = {
		//对外参数
		config:{
			domId:null,
			
			url:SELECT_URL,//服务地址，包含所有参数
			keyField:'HRDeptIdentity_HRDept_Id',//值字段
			valueField:'HRDeptIdentity_HRDept_CName',//显示文字字段
			
			defaultName:'请选择',//默认文字显示内容
			isFromRender:true,//加载完数据后是否立即表单渲染
			afterLoad:function(data){return data;},//数据加载后未渲染前处理
			done:function(instance){}//下拉框渲染完毕后回调
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,Module.config,setings);
	};
	
	//核心入口
	Module.render = function(options){
		var me = new Class(options);
		
		CommonSelectBasic.render(me.config);
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,Module);
});