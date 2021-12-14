/**
	@name：layui.ux.modules.msg.card 消息列表
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
		
	//卡片初始模板
	var _LIST_TEMPLET = 
	'<div style="width:{width};height:{height};">' +
		'<div itemid="table"></div>' +
	'</div>';
	
	//外部接口
	var msglist = {
		//患者+
		//全局项
		config:{
			initSort: {
				field:'DataAddTime',//排序字段
				type:'desc'
			},
			cols:[[
				{field:'SendSectionName',width:100,title:'小组名称',sort:true},
				{field:'SampleNo',width:100,title:'样本号',sort:true},
				{field:'JzType',width:60,title:'就诊类型',sort:true},
				{field:'DeptName',width:100,title:'开单科室',sort:true},
				{field:'DoctorName',width:100,title:'开单医生',sort:true},
				{field:'PatNo',width:100,title:'病历号'},
				{field:'PatientName',width:90,title:'病人姓名'},
				{field:'PatientSex',width:60,title:'性别'},
				{field:'PatientAge',width:60,title:'年龄'},
				{field:'ItemName',width:100,title:'检查项目'},
				{field:'ResultValue',width:100,title:'结果值'},
				{field:'ResultStatus',width:100,title:'状态'},
				{field:'DataAddTime',width:150,title:'产生时间',sort:true},
				{field:'OutTimes',width:100,title:'超时分钟'},
				
				//{field:'Msg',minWidth:150,title:'消息内容'},
				//{field:'SendSectionPhone',width:130,title:'发送小组电话'},
				//{field:'SenderName',width:80,title:'发送者'},
				//{field:'ReceiverName',width:80,title:'接收者'},
				
				{field:'Id',title:'ID',width:180,hide:true},
				{field:'MsgTypeCode',width:80,title:'类型代码',hide:true},
				{field:'MsgContent',width:100,title:'原始消息内容',hide:true}
//				{field:'MsgTypeID',width:100,title:'消息类型ID',hide:true},
//				{field:'MsgTypeName',width:80,title:'消息类型',hide:true},
//				{field:'SystemID',width:100,title:'所属系统ID',hide:true},
//				{field:'SystemCName',width:80,title:'系统名称',hide:true},
//				{field:'SystemCode',width:80,title:'系统代码',hide:true},
//				{field:'MsgLevel',width:80,title:'消息级别',hide:true},
//				{field:'SendNodeName',width:100,title:'发送站点',hide:true},
//				{field:'SendIPAddress',width:100,title:'发送IP地址',hide:true},
//				{field:'SenderID',width:80,title:'发送者ID',hide:true},
//				{field:'SendSectionID',width:100,title:'消息发送小组ID',hide:true},
//				{field:'RecNodeName',width:100,title:'接收站点',hide:true},
//				{field:'RecIPAddress',width:100,title:'接收IP地址',hide:true},
//				{field:'RecSectionID',width:100,title:'接收小组ID',hide:true},
//				{field:'RecSectionName',width:100,title:'接收小组',hide:true},
//				{field:'RecLabID',width:100,title:'接收机构ID',hide:true},
//				{field:'RecLabName',width:100,title:'接收机构',hide:true},
//				{field:'RecDeptID',width:100,title:'接收科室ID',hide:true},
//				{field:'RecDeptName',width:100,title:'接收科室',hide:true},
//				{field:'RecDeptCode',width:100,title:'接收科室编码',hide:true},
//				{field:'RecDeptCodeHIS',width:100,title:'接收科室编码HIS',hide:true},
//				{field:'RecDeptPhoneCode',width:140,title:'接收科室电话',hide:true},
//				{field:'ReceiverID',width:100,title:'接收者ID',hide:true},
//				{field:'RecSickTypeID',width:150,title:'消息接收就诊类型编号',hide:true},
//				{field:'RecSickTypeName',width:150,title:'消息接收就诊类型名称',hide:true},
//				{field:'RecDistrictID',width:100,title:'消息接收病区ID',hide:true},
//				{field:'RecDistrictName',width:100,title:'消息接收病区',hide:true},
//				{field:'RecDoctorID',width:100,title:'接收消息的医生ID',hide:true},
//				{field:'RecDoctorName',width:100,title:'接收消息的医生',hide:true},
//				{field:'RecDoctorCodeHIS',width:150,title:'接收消息的医生编码HIS',hide:true},
//				{field:'RecDoctorCode',width:100,title:'接收消息的医生编码',hide:true},
//				{field:'RequireReplyTime',width:100,title:'要求回复时间',hide:true},
//				{field:'UnRecSectorTypeID',width:150,title:'拒收消息接收实验类型ID',hide:true},
//				{field:'UnRecSectorTypeName',width:150,title:'拒收消息接收实验类型',hide:true},
//				{field:'ReadFlag',width:100,title:'已读标志',hide:true},//0未查阅，1已查阅
//				{field:'ConfirmDateTime',width:100,title:'消息确认时间',hide:true},
//				{field:'ConfirmMemo',width:100,title:'消息确认备注',hide:true},
//				{field:'ConfirmFlag',width:100,title:'消息确认标志',hide:true},
//				{field:'RequireConfirmTime',width:100,title:'要求确认时间',hide:true},
//				{field:'ConfirmNotifyDoctorID',width:100,title:'确认时通知医生ID',hide:true},
//				{field:'ConfirmIPAddress',width:100,title:'消息确认IP地址',hide:true},
//				{field:'ConfirmerID',width:100,title:'确认人ID',hide:true},
//				{field:'ConfirmerName',width:100,title:'确认人',hide:true},
//				{field:'WarningUploaderFlag',width:100,title:'警告上报标志',hide:true},
//				{field:'WarningUploaderID',width:100,title:'警告上报者ID',hide:true},
//				{field:'WarningUploaderName',width:100,title:'警告上报者',hide:true},
//				{field:'WarningUpLoadNotifyNurseIDS',width:150,title:'警告上报时通知护士ID',hide:true},
//				{field:'WarningUpLoadNotifyNurseName',width:150,title:'警告上报时通知护士编号',hide:true},
//				{field:'WarningUpLoadDateTime',width:100,title:'警告上报时间',hide:true},
//				{field:'WarningUpLoadMemo',width:100,title:'警告上报备注',hide:true},
//				{field:'LoginReadUserID',width:100,title:'查阅人ID',hide:true},
//				{field:'LoginReadUserName',width:100,title:'查阅人姓名',hide:true},
//				{field:'LoginReadDateTime',width:100,title:'查阅时间',hide:true},
//				{field:'SendToMsgCentre',width:100,title:'SendToMsgCentre',hide:true},
//				{field:'HandlingFlag',width:100,title:'处理中标志',hide:true},
//				{field:'HandlingNodeName',width:100,title:'处理中站点名称',hide:true},
//				{field:'HandlingDateTime',width:100,title:'处理时间',hide:true},
//				{field:'HandleFlag',width:100,title:'处理标志',hide:true},
//				{field:'FirstHandleDateTime',width:150,title:'第一次处理消息时间',hide:true},
//				{field:'TimeOutCallFlag',width:150,title:'超时未处理消息时提示标志',hide:true},
//				{field:'TimeOutCallUserID',width:150,title:'超时未处理消息时提示人ID',hide:true},
//				{field:'TimeOutCallUserName',width:150,title:'超时未处理消息时提示人名称',hide:true},
//				{field:'TimeOutCallRecUserID',width:150,title:'超时未处理消息时提示接收人ID',hide:true},
//				{field:'TimeOutCallRecUserName',width:150,title:'超时未处理消息时提示接收人姓名',hide:true},
//				{field:'TimeOutCallDateTime',width:150,title:'超时未处理消息时提示时间',hide:true},
//				{field:'SendToWebService',width:100,title:'SendToWebService',hide:true},
//				{field:'SendToHisFlag',width:150,title:'发送到His数据库标志',hide:true},//0未发送   1已发送
//				{field:'SendLinkMsg',width:150,title:'发送关联危机值标志',hide:true},
//				{field:'CreateToCheckTakeTime',width:150,title:'产生结果到审核时间',hide:true},
//				{field:'SendToPhone',width:100,title:'发送到手机标志',hide:true},
//				{field:'Memo',width:100,title:'备注',hide:true},
//				{field:'DispOrder',width:100,title:'DispOrder',hide:true},
//				{field:'RebackerID',width:100,title:'撤回人ID',hide:true},
//				{field:'RebackerName',width:100,title:'撤回人名称',hide:true},
//				{field:'RebackTime',width:100,title:'撤回时间',hide:true},
//				{field:'RebackMemo',width:100,title:'撤回原因',hide:true},
//				{field:'RebackFalg',width:100,title:'撤回标志',hide:true},
//				{field:'IsUse',width:100,title:'是否使用',hide:true},
//				{field:'CreatorID',width:100,title:'创建者',hide:true},
//				{field:'CreatorName',width:100,title:'创建者姓名',hide:true},
//				{field:'DataUpdateTime',width:100,title:'数据修改时间',hide:true},
//				{field:'WarningUpLoadNotifyNurseCode',width:200,title:'警告上报时通知护士编码',hide:true},
//				{field:'WarningUpLoadNotifyNurseCodeHIS',width:200,title:'警告上报时通知护士编码HIS',hide:true},
//				{field:'ConfirmerCode',width:100,title:'确认人编码',hide:true},
//				{field:'ConfirmerCodeHIS',width:100,title:'确认人编码HIS',hide:true},
//				{field:'ConfirmNotifyDoctorCodeHIS',width:150,title:'确认时通知医生编码HIS',hide:true},
//				{field:'ConfirmNotifyDoctorCode',width:150,title:'确认时通知医生编码',hide:true},
//				{field:'ConfirmNotifyDoctorName',width:150,title:'确认时通知医生姓名',hide:true}
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
		width:1350,
		height:800,
		maxmin:false,
		toolbar:true
	};
	//初始化消息列表
	Class.pt.initList = function(){
		var me = this;
		
		me.config.content = _LIST_TEMPLET
			.replace(/{width}/g,me.config.width + 'px')
			.replace(/{height}/g,me.config.height + 'px');
			
		//弹出层
		var index = layer.open(me.config);
		me.config.index = index;
		
		var table_div = $("#layui-layer" + me.config.index).find("div[itemid='table']");
		
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
	//对外公开返回对象
	Class.pt.result = function(that){
		return {
			index:that.config.index,
			inset:that.inset
		}
	};
	//核心入口
	msglist.render = function(options){
		var me = new Class(options);
		me.initList();
		return me.result(me);
	};
	
	//暴露接口
	exports('msglist',msglist);
});