/**
 * @name： 单选模式
 * @author：liangyl
 * @version 2021-11-03
 */
layui.extend({
	EmpList:'views/system/set/user/section/role2/single/emp',	 //人员列表
	RoleList:'views/system/set/user/section/role2/single/role', //角色列表
	SectionList:'views/system/set/user/section/role2/single/section'//检验中权限列表
}).define(['uxutil','form','EmpList','RoleList','SectionList'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		table = layui.table,
		EmpList = layui.EmpList,
		RoleList = layui.RoleList,
		SectionList = layui.SectionList,
		MOD_NAME = 'SingleIndex';
	//模块DOM
	var MOD_DOM = [
	    '<div class="layui-row" style="margin:0px;padding:0px;">',
			'<div class="layui-col-xs5">',
                '<div id="{domId}-Emp-Single"></div>',
			'</div>',
			'<div class="layui-col-xs3" style="padding-left: 2px;">',
                '<div id="{domId}-Section-Single"></div>',
			'</div>',
			'<div class="layui-col-xs4" style="padding-left: 2px;">',
                '<div id="{domId}-Role-Single"></div>',
			'</div>',
		'</div>'
	].join('');
	//员工实例
	var EmpListInstance = null;
	//角色实例
	var RoleListInstance = null;
	//检验中权限实例
	var SectionListInstance = null;
	
	//员工选择行
	var EMP_CHECK_ROW_DATA = [];
	var SingleIndex = {
		//对外参数
		config:{
			domId:null,
			height:null
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,SingleIndex.config,setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = MOD_DOM.replace(/{domId}/g,me.config.domId);
		$('#' + me.config.domId).append(html);
		var win = $(window),
		    maxheight = win.height()-110;
		//员工列表实例 
		EmpListInstance = EmpList.render({
			domId: me.config.domId+'-Emp-Single',
			height:maxheight+'px',
			rowClickFun:function(obj){
				EMP_CHECK_ROW_DATA = [obj.data];
				SectionListInstance.onSearch(obj.data.HREmpIdentity_HREmployee_Id,obj.data.HREmpIdentity_HREmployee_CName);
			   //还原权限
				RoleListInstance.onSearch(obj.data.HREmpIdentity_HREmployee_Id);
			},
			done : function(res, curr, count){
				if(count==0){
					EMP_CHECK_ROW_DATA = [];
					//清空小组数据
					if(SectionListInstance)SectionListInstance.clearData();
					//清空角色数据
					if(RoleListInstance)RoleListInstance.clearData();
				}
			}
		});
		
		//检验中权限
		SectionListInstance =  SectionList.render({
			domId: me.config.domId+'-Section-Single',
			height:maxheight+'px',
			rowClickFun:function(obj){
				//还原权限
				RoleListInstance.linkData(obj.data.LBRight_EmpID,obj.data.LBRight_LBSection_Id);
			},
			done : function(res, curr, count){
				if(count==0){
					if(RoleListInstance)RoleListInstance.clearData();
				}
			}
		});
		//角色实例
		RoleListInstance = RoleList.render({
			domId: me.config.domId+'-Role-Single',
			height:maxheight+'px'
		});
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		EmpListInstance.loadData();
	};
	//数据加载
	Class.prototype.loadData = function(){
		var me = this;
		
	};
	//核心入口
	SingleIndex.render = function(options){
		var me = new Class(options);
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		me.initHtml();
		me.initListeners();
		return me;
	};
	//暴露接口
	exports(MOD_NAME,SingleIndex);
});