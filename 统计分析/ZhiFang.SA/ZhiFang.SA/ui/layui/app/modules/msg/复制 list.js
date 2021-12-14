/**
	@name：layui.ux.modules.msg.card 消息卡片
	@author：Jcall
	@version 2019-05-06
 */
layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table'
}).define(['uxutil','uxtable','layer'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxtable = layui.uxtable;
	
	//外部接口
	var msglist = {
		//全局项
		config:{
			cols:[[
				{field:'Id',title: 'ID',hide:true},
				{field:'MsgContent',width:100,title:'消息内容'},
				{field:'MsgTypeID',width:100,title:'消息类型ID'},
				{field:'MsgTypeName',width:80,title:'消息类型'},
				{field:'MsgTypeCode',width:80,title:'消息类型代码'},
				{field:'SystemID',width:100,title:'所属系统ID'},
				{field:'SystemCName',width:80,title:'系统名称'},
				{field:'SystemCode',width:80,title:'系统代码'},
				{field:'MsgLevel',width:80,title:'消息级别'},
				{field:'SendNodeName',width:100,title:'发送站点'},
				{field:'SendIPAddress',width:100,title:'发送IP地址'},
				{field:'SenderID',width:80,title:'发送者ID'},
				{field:'SenderName',width:80,title:'发送者'},
				
				{field:'SendSectionID',width:100,title:'消息发送小组ID'},
				{field:'SendSectionName',width:100,title:'发送小组'},
				{field:'RecNodeName',width:100,title:'接收站点名称'},
				{field:'RecIPAddress',width:100,title:'接收IP地址'},
				{field:'RecSectionID',width:100,title:'接收小组ID'},
				{field:'RecSectionName',width:100,title:'接收小组姓名'},
				{field:'RecLabID',width:100,title:'接收机构ID'},
				{field:'RecLabName',width:100,title:'接收机构名称'},
				{field:'RecDeptID',width:100,title:'接收科室ID'},
				{field:'RecDeptName',width:100,title:'接收科室名称'},
				{field:'RecDeptCode',width:100,title:'接收科室编码'},
				{field:'RecDeptCodeHIS',width:100,title:'接收科室编码HIS'},
				{field:'ReceiverID',width:100,title:'接收者ID'},
				{field:'ReceiverName',width:100,title:'接收者姓名'},
				{field:'RecSickTypeID',width:150,title:'消息接收就诊类型编号'},
				{field:'RecSickTypeName',width:150,title:'消息接收就诊类型名称'},
				{field:'RecDistrictID',width:100,title:'消息接收病区ID'},
				{field:'RecDistrictName',width:100,title:'消息接收病区名称'},
				{field:'RecDoctorID',width:100,title:'接收消息的医生ID'},
				{field:'RecDoctorName',width:100,title:'接收消息的医生姓名'},
				{field:'RecDoctorCodeHIS',width:150,title:'接收消息的医生编码HIS'},
				{field:'RecDoctorCode',width:100,title:'接收消息的医生编码'},
				{field:'RequireReplyTime',width:100,title:'要求回复时间'},
				{field:'UnRecSectorTypeID',width:150,title:'拒收消息接收实验类型ID'},
				{field:'UnRecSectorTypeName',width:150,title:'拒收消息接收实验类型名称'},
				{field:'ReadFlag',width:100,title:'已读标志'},//0未查阅，1已查阅
				{field:'ConfirmDateTime',width:100,title:'消息确认时间'},
				{field:'ConfirmMemo',width:100,title:'消息确认备注'},
				{field:'ConfirmFlag',width:100,title:'消息确认标志'},
				{field:'RequireConfirmTime',width:100,title:'要求确认时间'},
				{field:'ConfirmNotifyDoctorID',width:100,title:'确认时通知医生ID'},
				{field:'ConfirmIPAddress',width:100,title:'消息确认IP地址'},
				{field:'ConfirmerID',width:100,title:'确认人ID'},
				{field:'ConfirmerName',width:100,title:'确认人姓名'},
				{field:'RecDeptPhoneCode',width:100,title:'接收科室电话'},
				{field:'WarningUploaderFlag',width:100,title:'警告上报标志'},
				{field:'WarningUploaderID',width:100,title:'警告上报者ID'},
				{field:'WarningUploaderName',width:100,title:'警告上报者姓名'},
				{field:'WarningUpLoadNotifyNurseIDS',width:150,title:'警告上报时通知护士ID'},
				{field:'WarningUpLoadNotifyNurseName',width:150,title:'警告上报时通知护士编号'},
				{field:'WarningUpLoadDateTime',width:100,title:'警告上报时间'},
				{field:'WarningUpLoadMemo',width:100,title:'警告上报备注'},
				{field:'LoginReadUserID',width:100,title:'查阅人ID'},
				{field:'LoginReadUserName',width:100,title:'查阅人姓名'},
				{field:'LoginReadDateTime',width:100,title:'查阅时间'},
				{field:'SendToMsgCentre',width:100,title:'SendToMsgCentre'},
				{field:'HandlingFlag',width:100,title:'处理中标志'},
				{field:'HandlingNodeName',width:100,title:'处理中站点名称'},
				{field:'HandlingDateTime',width:100,title:'处理时间'},
				{field:'RequireHandleTime',width:100,title:'要求处理时间'},
				{field:'HandleFlag',width:100,title:'处理标志'},
				{field:'FirstHandleDateTime',width:150,title:'第一次处理消息时间'},
				{field:'TimeOutCallFlag',width:150,title:'超时未处理消息时提示标志'},
				{field:'TimeOutCallUserID',width:150,title:'超时未处理消息时提示人ID'},
				{field:'TimeOutCallUserName',width:150,title:'超时未处理消息时提示人名称'},
				{field:'TimeOutCallRecUserID',width:150,title:'超时未处理消息时提示接收人ID'},
				{field:'TimeOutCallRecUserName',width:150,title:'超时未处理消息时提示接收人姓名'},
				{field:'TimeOutCallDateTime',width:150,title:'超时未处理消息时提示时间'},
				{field:'SendToWebService',width:100,title:'SendToWebService'},
				{field:'SendToHisFlag',width:150,title:'发送到His数据库标志'},//0未发送   1已发送
				{field:'SendLinkMsg',width:150,title:'发送关联危机值标志'},
				{field:'CreateToCheckTakeTime',width:150,title:'产生结果到审核时间'},
				{field:'SendToPhone',width:100,title:'发送到手机标志'},
				{field:'Memo',width:100,title:'备注'},
				{field:'DispOrder',width:100,title:'DispOrder'},
				{field:'RebackerID',width:100,title:'撤回人ID'},
				{field:'RebackerName',width:100,title:'撤回人名称'},
				{field:'RebackTime',width:100,title:'撤回时间'},
				{field:'RebackMemo',width:100,title:'撤回原因'},
				{field:'RebackFalg',width:100,title:'撤回标志'},
				{field:'IsUse',width:100,title:'是否使用'},
				{field:'CreatorID',width:100,title:'创建者'},
				{field:'CreatorName',width:100,title:'创建者姓名'},
				{field:'DataUpdateTime',width:100,title:'数据修改时间'},
				{field:'WarningUpLoadNotifyNurseCode',width:200,title:'警告上报时通知护士编码'},
				{field:'WarningUpLoadNotifyNurseCodeHIS',width:200,title:'警告上报时通知护士编码HIS'},
				{field:'ConfirmerCode',width:100,title:'确认人编码'},
				{field:'ConfirmerCodeHIS',width:100,title:'确认人编码HIS'},
				{field:'ConfirmNotifyDoctorCodeHIS',width:150,title:'确认时通知医生编码HIS'},
				{field:'ConfirmNotifyDoctorCode',width:150,title:'确认时通知医生编码'},
				{field:'ConfirmNotifyDoctorName',width:150,title:'确认时通知医生姓名'}
			]]
		},
		//设置全局项
		set:function(options){
			var me = this;
			me.config = $.extend({},me.config,options);
			return me;
		}
	};
	
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,msglist.config,setings);
	};
	
	Class.pt = Class.prototype;
	//默认配置
	Class.pt.config = {
		//类型
		type:1,
		//元素闪烁样式名
		flickerClass:"flicker",
		maxWidth:"95%",
		maxHeight:"95%",
		width:1000,
		height:800,
		maxmin:false
	};
	//初始化消息列表
	Class.pt.initList = function(){
		var me = this;
		
		me.config.content = '<div style="width:' + me.config.width + 'px;height:' + me.config.height + 'px;"><div itemid="table"></div></div>';
		
		//弹出层
		var index = layer.open(me.config);
		me.config.index = index;
		
		var table_div = $("#layui-layer" + me.config.index).find("div [itemid='table']");
		
		uxtable.render($.extend({},me.config,{
			elem:$(table_div),
			width:me.config.width,
			height:me.config.height
		}));
		$(table_div).next().css("border","unset").css("margin","unset");
	}
	//追加数据
	Class.pt.inset = function(row){
		
	};
	//核心入口
	msglist.render = function(options){
		var me = new Class(options);
		me.initList();
		return me.config.index;
	};
	
	//暴露接口
	exports('msglist',msglist);
});