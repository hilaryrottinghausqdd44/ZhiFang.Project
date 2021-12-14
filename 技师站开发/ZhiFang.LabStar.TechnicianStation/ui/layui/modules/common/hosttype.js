/**
 * @name：modules/common/hosttype 站点类型
 * @author：Jcall
 * @version 2020-08-17
 */
layui.extend({
	uxutil:'ux/util',
}).define(['uxutil','dropdown'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		dropdown = layui.dropdown,
		MOD_NAME = 'CommonHostType';
	
	//获取站点列表服务地址
	var GET_HOST_TYPE_USER_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_SearchBHostTypeUserAndHostTypeNameByHQL";
	
	//站点类型
	var Module = {
		//对外参数
		config:{
			//站点类型上级功能栏ID，可以不设置站点类型下拉框ID属性，自动生成下拉框
			selectParentDomId:'',
			//站点类型下拉框ID，如果已经设置站点类型上级功能栏ID，本参数可以不设置，自动生成下拉框
			selectDomId:'',
			//站点类型列表点击触发事件
			listClickFun:function(){},
			//站点类型下拉框选择触发事件
			selectClickFun:function(){},
			//站点信息列表
			HostTypeUserList:null,
			//站点类型下拉框ID
			listDomId:'',
			//当前被选中站点类型
			HostTypeID:null,
			//枚举类型名称
			paraTypeCode:''
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,Module.config,setings);
		
		me.config.listDomId = 'hosttype-list-' + new Date().getTime();//站点类型列表ID
		me.config.selectDomId = 'hosttype-select-' + new Date().getTime();//站点类型下拉框ID
	};
	
	//初始化站点列表
	Class.prototype._initHostTypeList = function(){
		var me = this;
		me.getDataList(function(){
			me._initHostTypeListHtml();
		});
	};
	//初始化站点列表HTML
	Class.prototype._initHostTypeListHtml = function(){
		var me = this,
			HostTypeList = me.config.HostTypeUserList,
			html = [];
		
		if(HostTypeList.length == 0){
			html.push('<div id="' + me.config.listDomId + '" style="padding:50px;text-align:center;font-size:24px;">该人员没有配置任何站点类型，请联系管理员！</div>');
		}else{
			html.push('<ul id="' + me.config.listDomId + '" class="layui-row layui-col-space15" style="padding:20px;margin:0;">');
		
			for(var i in HostTypeList){
				html.push(
					'<li class="layui-col-xs4 layui-col-sm3 layui-col-md2" id="' + HostTypeList[i].HostTypeID + '" name="' + HostTypeList[i].HostTypeName + '">',
						'<div style="background-color:#5FB878;padding:20px;color:#ffffff;text-align:center;font-size:24px;">',
							'<i class="layui-icon layui-icon-chart-screen" style="font-size:64px;"></i>',
							'<p>' + HostTypeList[i].HostTypeName + '</p>',
						'</div>',
					'</li>'	
				);
			}
			
			html.push('</ul>');
		}
		
		$("body").append(html.join(''));
		
		$("#" + me.config.listDomId).find("li").each(function(){
			$(this).on("click",function(){
				me.insertHistoryInfo({
					HostTypeID:$(this).attr("id"),
					HostTypeName:$(this).attr("name")
				});
				$("#" + me.config.listDomId).remove();
				//下拉框点击后触发
				me._afterListClick();
			});
		});
	};
	//下拉框点击后触发
	Class.prototype._afterListClick = function(){
		var me = this;
		$($("body").find('div')[0]).show();
        me.config.HostTypeID =me.getHistoryInfo().HostTypeID; //获取被选择的站点类型ID
		me.config.listClickFun && me.config.listClickFun();
		//初始化站点下拉框
		me._initHostTypeSelect();
	};
	
	//初始化站点下拉框
	Class.prototype._initHostTypeSelect = function(){
		var me = this,
			HostTypeList = me.config.HostTypeUserList,
			HistoryInfo = me.getHistoryInfo();
		//当前所在站点
		$("#" + me.config.selectDomId) && $("#" + me.config.selectDomId).remove();
		
		if(me.config.selectParentDomId){
			$("#" + me.config.selectParentDomId).append(
				'<button class="layui-btn layui-btn-sm layui-btn-primary" style="float:right;" id="' + me.config.selectDomId + '">' +
					(HistoryInfo ? HistoryInfo.HostTypeName : '请选择站点') + 
					'<i class="layui-icon layui-icon-down layui-font-12"></i>' +
				'</button>'
			);
		}else{
			$(me.config.selectDomId).html(
				(HistoryInfo ? HistoryInfo.HostTypeName : '请选择站点') + 
				'<i class="layui-icon layui-icon-down layui-font-12"></i>'
			);
		}
		
		var data = [];
		for(var i in HostTypeList){
			if(HistoryInfo && HistoryInfo.HostTypeID == HostTypeList[i].HostTypeID) continue;
			data.push({
				title:HostTypeList[i].HostTypeName,
				id:HostTypeList[i].HostTypeID,
				href:'#'
			});
		}
		if(data.length > 0){
			dropdown.render({
				elem:"#"+me.config.selectDomId,
				data:data,
				click: function(obj){
					me.insertHistoryInfo({
						HostTypeID:obj.id,
						HostTypeName:obj.title
					});
					me.config.HostTypeID = obj.id; //获取被选择的站点类型ID
					me._initHostTypeSelect();
					me.config.selectClickFun && me.config.selectClickFun();
				}
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
				limit:100,
				fields:'BHostTypeUser_HostTypeID,BHostTypeUser_HostTypeName',
				where: 'bhosttypeuser.EmpID=' + empId,
				systemTypeCode: '1',
				paraTypeCode: me.config.paraTypeCode//需要改为页面传输的枚举类型名称
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
	//获取人员站点信息
	Class.prototype.getHistoryInfo = function(){
		var me = this,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID);
			
		return uxutil.localStorage.get('PreSampleBarcodeBasicHostType_' + me.config.paraTypeCode + empId,true);
	};
	//记录人员站点信息{HostTypeID:'',HostTypeName:''}
	Class.prototype.insertHistoryInfo = function(info){
		var me = this,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID);
			
		uxutil.localStorage.set('PreSampleBarcodeBasicHostType_' + me.config.paraTypeCode + empId,JSON.stringify(info));
	};
	
	//核心入口
	Module.render = function(options){
		var me = new Class(options);
		
		//获取站点信息列表
		me.getDataList(function(){
			//获取人员站点信息,如果存在，只渲染站点类型下拉框，并显示功能页面，如果不存在，渲染站点类型类表+站点类型下拉框
			var HistoryInfo = me.getHistoryInfo();
			if(HistoryInfo){
				//初始化站点下拉框
				me._initHostTypeSelect();
				//下拉框点击后触发
				me._afterListClick();
			}else{
				//初始化站点列表
				me._initHostTypeList();
				//初始化站点下拉框
				me._initHostTypeSelect();
			}
		});
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,Module);
});