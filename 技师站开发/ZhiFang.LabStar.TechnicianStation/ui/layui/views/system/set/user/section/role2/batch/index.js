/**
 * @name： 多选模式
 * @author：liangyl
 * @version 2021-11-03
 */
layui.extend({
	BataEmpList:'views/system/set/user/section/role2/batch/emp',	 //人员列表
	BataRoleList:'views/system/set/user/section/role2/batch/role', //角色列表
	BataSectionsList:'views/system/set/user/section/role2/batch/section'//检验中权限列表
}).define(['uxutil','form','BataEmpList','BataRoleList','BataSectionsList'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		table = layui.table,
		BataEmpList = layui.BataEmpList,
		BataRoleList = layui.BataRoleList,
		BataSectionsList = layui.BataSectionsList,
		MOD_NAME = 'BatchIndex';
	//模块DOM
	var MOD_DOM = [
	    '<div class="layui-row" style="margin:0px;padding:0px;">',
			'<div class="layui-col-xs5">',
                '<div id="{domId}-Emp-Bata"></div>',
			'</div>',
			'<div class="layui-col-xs3" style="padding-left: 2px;">',
                '<div id="{domId}-Section-Bata"></div>',
			'</div>',
			'<div class="layui-col-xs4" style="padding-left: 2px;">',
                '<div id="{domId}-Role-Bata"></div>',
			'</div>',
		'</div>'
	].join('');
	//员工实例
	var BataEmpListInstance = null;
	//角色实例
	var BataRoleListInstance = null;
	//检验中权限实例
	var BataSectionsListInstance = null;
	
	//员工选择行
	var EMP_CHECK_ROW_DATA = [];
	var BatchIndex = {
		//对外参数
		config:{
			domId:null,
			height:null
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,BatchIndex.config,setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = MOD_DOM.replace(/{domId}/g,me.config.domId);
		$('#' + me.config.domId).append(html);
		var win = $(window),
		    maxheight = win.height()-110;
		//员工列表实例 
		BataEmpListInstance = BataEmpList.render({
			domId: me.config.domId+'-Emp-Bata',
			height:maxheight+'px',
			MODEL:'batch',
			checkClickFun:function(list){
				var ids =[],names =[];
				for(var i in list){
					ids.push(list[i].HREmpIdentity_HREmployee_Id);
					names.push(list[i].HREmpIdentity_HREmployee_CName);
				}
				EMP_CHECK_ROW_DATA = [];
				if(ids.length>0){
					EMP_CHECK_ROW_DATA.push({ids:ids.join(','),names:names.join(',')});
					//小组加载
					BataSectionsListInstance.onSearch(ids.join(','),names.join(','));
					//角色加载
					BataRoleListInstance.onSearch(ids.join(','));
				}else{
					//清空小组数据
					if(BataSectionsListInstance)BataSectionsListInstance.clearData();
					//清空角色数据
					if(BataRoleListInstance)BataRoleListInstance.clearData();
				}
			},
			done : function(res, curr, count){
				if(count==0){
					EMP_CHECK_ROW_DATA = [];
					//清空小组数据
					if(BataSectionsListInstance)BataSectionsListInstance.clearData();
					//清空角色数据
					if(BataRoleListInstance)BataRoleListInstance.clearData();
				}
			}
		});
		//检验中权限
		BataSectionsListInstance =  BataSectionsList.render({
			domId: me.config.domId+'-Section-Bata',
			height:maxheight+'px',
		    checkClickFun:function(list){
			},
			done : function(res, curr, count){
			},
			sectionClickFun:function(){
				//获取勾选人员
				var empList = BataEmpListInstance.getCheckedList();
				if(empList.length==0){
					layer.msg('请先勾选员工',{icon:5});
					return false;
				}
				BataSectionsListInstance.OpenWin();
			}
		});
		//角色实例
		BataRoleListInstance = BataRoleList.render({
			domId: me.config.domId+'-Role-Bata',
			height:maxheight+'px',
			saveClick:function(roleIDList){
				var empIDList = [],sectionIDList = [];
				//获取勾选人员
				var empList = BataEmpListInstance.getCheckedList();
				//获取勾选的小组
				var sectionList = BataSectionsListInstance.getCheckedList();
			    if(empList.length==0){
					layer.msg('请先勾选员工',{icon:5});
					return false;
				}
				if(sectionList.length==0){
					layer.msg('请先勾选小组',{icon:5});
					return false;
				}
				for(var i in empList){
					empIDList.push(empList[i].HREmpIdentity_HREmployee_Id);
				}
				for(var i in sectionList){
					sectionIDList.push(sectionList[i].LBSection_Id);
				}
				BataRoleListInstance.onSaveClick(empIDList.join(','),sectionIDList.join(','),roleIDList);
			},
			delClick:function(roleIDList){
				var empIDList = [],sectionIDList = [];
				//获取勾选人员
				var empList = BataEmpListInstance.getCheckedList();
				//获取勾选的小组
				var sectionList = BataSectionsListInstance.getCheckedList();
			    if(empList.length==0){
					layer.msg('请先勾选员工',{icon:5});
					return false;
				}
				if(sectionList.length==0){
					layer.msg('请先勾选小组',{icon:5});
					return false;
				}
				for(var i in empList){
					empIDList.push(empList[i].HREmpIdentity_HREmployee_Id);
				}
				for(var i in sectionList){
					sectionIDList.push(sectionList[i].LBSection_Id);
				}
				BataRoleListInstance.onDelClick(empIDList.join(','),sectionIDList.join(','),roleIDList);
			}
		});
	};

	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		BataEmpListInstance.loadData();
	};
	//数据加载
	Class.prototype.loadData = function(){
		var me = this;
		
	};

	//核心入口
	BatchIndex.render = function(options){
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
	exports(MOD_NAME,BatchIndex);
});