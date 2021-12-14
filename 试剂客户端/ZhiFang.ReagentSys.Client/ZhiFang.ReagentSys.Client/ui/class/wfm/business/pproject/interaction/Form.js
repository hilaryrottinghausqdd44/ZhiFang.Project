/**
 * 互动信息
 * @author liangyl
 * @version 2017-03-21
 */
Ext.define('Shell.class.wfm.business.pproject.interaction.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.UMeditor',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '互动信息',
	width: 600,
	height: 120,

	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddPProjectTaskProgress',
	/**附件对象名*/
	objectName: '',
	/**附件关联对象名*/
	fObejctName: '',
	/**附件关联对象主键*/
	fObjectValue: '',
	/**交流关联对象是否ID,@author Jcall,@version 2016-08-19*/
	fObjectIsID: false,

	/** 每个组件的默认属性*/
	layout: 'fit',
	bodyPadding: 1,
	/**内容自动显示*/
	autoScroll: false,
	/**表单的默认状态*/
	formtype: 'add',
	/**启用表单状态初始化*/
	openFormType: true,
	/**显示成功信息*/
	showSuccessInfo: false,
	/**话题标志*/
	IsCommunication: false,
	/**话题标题*/
	TopicTitle: "",
	RiskGrade:"RiskGrade",
	/**任务记录类型*/
	TaskTypeDict: [
		['日志', '日志'],
		['交流', '交流']],
	/**项目ID*/
	ProjectID:null,
	/**项目任务ID*/
	ProjectTaskID:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//解决在线编辑器换行出现滚动条后工具栏会被隐藏,需要手工调整高度,工具栏才不会被隐藏
		setTimeout(function() {
			me.setHeight(me.height+1);
		}, 800);
		me.initListeners();
	},
	/**初始化监听*/
	initListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		//发票类型
		var RiskGrade = buttonsToolbar.getComponent('PProjectTaskProgress_TaskRisk');
//			RiskGradeID = buttonsToolbar.getComponent('PProjectTaskProgress_TaskRiskID');
		if(RiskGrade) {
			RiskGrade.on({
				check: function(p, record) {
					RiskGrade.setValue(record ? record.get('BDict_CName') : '');
//					RiskGradeID.setValue(record ? record.get('BDict_Id') : '');
					p.close();
				}
			});
		}
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onTopicClick');
		me.buttonToolbarItems = [{
			fieldLabel: '任务记录类型',name: 'PProjectTaskProgress_TaskTypeDict',itemId: 'PProjectTaskProgress_TaskTypeDict',
			emptyText: "任务记录类型",xtype: 'uxSimpleComboBox',labelWidth: 80,width: 170,
			labelAlign: 'right',hasStyle: true,allowBlank: false,
			data:me.TaskTypeDict
		},{
			fieldLabel: "任务风险",name: 'PProjectTaskProgress_TaskRisk',itemId: 'PProjectTaskProgress_TaskRisk',
			emptyText: "任务风险",xtype: 'uxCheckTrigger',allowBlank: false,
			labelWidth: 65,width: 170,labelAlign: 'right',className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '任务风险选择',
				defaultWhere: "pdict.BDictType.DictTypeCode='" + this.RiskGrade + "'"
			}
		},{
			fieldLabel: "任务风险",name: 'PProjectTaskProgress_TaskRiskID',itemId: 'PProjectTaskProgress_TaskRiskID',
			emptyText: "任务风险",xtype: 'textfield',hidden:true,width: 170
		},{
	        xtype: 'radiogroup', fieldLabel: '工作时间', columns: 3, vertical: true,labelWidth: 70,width: 220,
			height:22,labelAlign: 'right',name:'PProjectTaskProgress_ExecuteTime',itemId:'PProjectTaskProgress_ExecuteTime',
	        items: [
	            { boxLabel: '今天', name: 'rb', inputValue: '1', checked: true },
	            { boxLabel: '昨天', name: 'rb', inputValue: '2'},
	            { boxLabel: '前天', name: 'rb', inputValue: '3' }
	        ]
	    },{
			fieldLabel: '工作量(单位:小时)',name: 'PProjectTaskProgress_Workload',itemId: 'PProjectTaskProgress_Workload',
			xtype:'numberfield',value:0,
//			emptyText:'必填项',allowBlank:false,
		   	labelWidth: 120,width: 200,labelAlign: 'right'
		},'->', {
			xtype: 'button',text: '提交话题',iconCls: 'button-save',
			tooltip: '提交话题',itemId: 'btnTopic',hidden:true,
			handler: function() {
//				me.fireEvent('onTopicClick', me);
//				JShell.Msg.confirm({
//					title: '<div style="text-align:center;">话题标题</div>',
//					msg: '',
//					closable: false,
//					multiline: true //多行输入框
//				}, function(but, text) {
//					if(but != "ok") return;
//					me.TopicTitle = "" + text;
//					if(me.TopicTitle == "") {
//						JShell.Msg.alert("话题标题为空");
//					} else {
//						me.IsCommunication = true;
//						me.onSaveClick();
//					}
//				});
			}
		}, {
			xtype: 'button',text: '提交',itemId: 'btnSaveInteraction',iconCls: 'button-save',
			disabled: true,tooltip: '提交',
			handler: function() {
				me.TopicTitle = "";
				me.IsCommunication = false;
				me.onSaveClick();
			}
		}];
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		var height = me.height;
		height = (height < 80 ? 80 : height);
		items.push({
			name: 'Content',
			itemId: 'Content',
			border: false,
			xtype: 'umeditor',
			width: '100%',
			height: height,
			emptyText: '工作说明...'
		});
		return items;
	},
	setHeight: function(height) {
		var me = this;
		if(height) height = height < 120 ? 120 : height;
		return me.setSize(undefined, height);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		if(!JShell.System.Cookie.map.USERID) {
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
		if(!values.Content || !values.Content.trim()) {
			JShell.Msg.error('请填写工作说明！');
			return null;
		}
		values.Content = values.Content.replace(/\\/g, '&#92');
		var entity = {
			TaskWorkInfo: values.Content,
			IsUse: 1
		};
    	var buttonsToolbar = me.getComponent('buttonsToolbar');
    	//工作量
        var Workload= buttonsToolbar.getComponent('PProjectTaskProgress_Workload').getValue();
        //任务类型
        var TaskTypeDict= buttonsToolbar.getComponent('PProjectTaskProgress_TaskTypeDict').getValue();
        //任务风险
        var TaskRisk= buttonsToolbar.getComponent('PProjectTaskProgress_TaskRisk').getValue();
        //工作时间
        var ExecuteTime= buttonsToolbar.getComponent('PProjectTaskProgress_ExecuteTime').getValue().rb;
        //系统当前时间
    	var Sysdate = JcallShell.System.Date.getDate();
        var ExecuteTimeValue='';
        switch (ExecuteTime){
        	case 2:
        	//昨天
        	    ExecuteTimeValue=JShell.Date.toServerDate(JShell.Date.getNextDate(Sysdate, -1));
        		break;
        	case 3:
        	//前天
        	    ExecuteTimeValue=JShell.Date.toServerDate(JShell.Date.getNextDate(Sysdate, -2));
        		break;	
        	default:
        	   //今天
        	    ExecuteTimeValue=JShell.Date.toServerDate(JcallShell.Date.toString(Sysdate, true));
        		break;
        }
		if(Workload){
			entity.Workload=Workload
		}
		if(TaskTypeDict){
			entity.TaskTypeDict=TaskTypeDict
		}
		if(TaskRisk){
			entity.TaskRisk=TaskRisk
		}

		if(me.ProjectID){
			entity.PProject = {
				Id:me.ProjectID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}

		if(me.ProjectTaskID){
			entity.PProjectTask = {
				Id:me.ProjectTaskID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		var userId= JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		if(userId){
		    entity.Register = {
				Id:userId,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
	   var Sysdate = JcallShell.System.Date.getDate();
	   var RegisterTime=JcallShell.Date.toString(Sysdate);
	   if(RegisterTime){
		    entity.RegisterTime=JShell.Date.toServerDate(RegisterTime);
		}
		if(ExecuteTimeValue){
			entity.ExecuteTime=ExecuteTimeValue
		}
		return { entity: entity };
	},
		/**更改标题*/
	changeTitle:function(){
		var me = this;
	}
});