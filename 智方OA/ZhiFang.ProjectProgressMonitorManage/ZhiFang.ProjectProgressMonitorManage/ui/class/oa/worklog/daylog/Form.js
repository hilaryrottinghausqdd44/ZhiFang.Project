/**
 * 工作日志表单
 * @author liangyl
 * @version 2016-08-02
 */
Ext.define('Shell.class.oa.worklog.daylog.Form', {
	extend: 'Shell.class.oa.worklog.basic.Form',
     /**查询对象*/
	objectEName: 'WorkLogDay',
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkDayLogById?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkDayLogByField',
		/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkDayLogByWeiXin',
	PTaskId:'',
	WorkLogDay_Workload:0,
	formtype: "edit",
		Workload:[['0.1', '10%'],['0.15', '15%'],
		['0.2', '20%'],
		['0.3', '30%'],
		['0.4', '40%'],
		['0.5', '50%'],
		['0.6', '60%'],
		['0.7', '70%'],
		['0.8', '80%'],
		['0.9', '90%'],
		['1.0', '100%']
	],
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
		var values = me.getForm().getValues();
		var params = {
			entity: {}
		};
		var EmpID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var EmpName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);

		var nextDayContent = "";
		var nameKey = me.objectEName + '_NextDayContent';
		if(values[nameKey]) {
			nextDayContent = values[nameKey].replace(/\\/g, '&#92');
			//nextDayContent = nextDayContent.replace(/[\r\n]/g, '<br />');
		}
		var toDayContent = "";
		nameKey = me.objectEName + '_ToDayContent';
		if(values[nameKey]) {
			toDayContent = values[nameKey].replace(/\\/g, '&#92');
			//toDayContent = toDayContent.replace(/[\r\n]/g, '<br />');
		}
		var workLogExportLevel = "";
		nameKey = me.objectEName + '_WorkLogExportLevel';
		if(values[nameKey]) {
			workLogExportLevel = values[nameKey];
		}

		var WorkLogExportLevel = me.getComponent(me.objectEName + '_WorkLogExportLevel').getValue();
		var entity = {
			NextDayContent: nextDayContent,
			ToDayContent: toDayContent,
			IsUse: true,
			WorkLogExportLevel: workLogExportLevel,
			Status: values[me.objectEName + '_Status'] ? true : false,
			CopyForEmpIdList: null, //抄送人Id集合
			CopyForEmpNameList: null //抄送人名称集合
		};
		switch(me.objectEName) {
			case "WorkWeekLog":
				entity.Status = svalues[me.objectEName + '_Status'] ? true : false;
				break;
			case "WorkMonthLog":
				entity.Status = svalues[me.objectEName + '_Status'] ? true : false;
				break;
			default:
				break;
		}
		//抄送人
		var copyUserValue = null;
		var copyUser = me.getComponent('PWorkLogCopyFor');
		copyUserValue = copyUser.getValue();
		if(copyUserValue && copyUserValue != null) {
			var EmpIdArr = [],
				EmpNameArr = [];
			EmpNameArr = copyUserValue.EmpNameArr;
			EmpIdArr = copyUserValue.EmpIdArr;
			entity.CopyForEmpIdList = EmpIdArr;
			entity.CopyForEmpNameList = EmpNameArr;
		}
		var PTaskDataTimeStamp = '1,2,3,4,5,6,7,8';
		//任务状态
		if(me.PTaskId!=''){
			entity.PTask = {
				Id:me.PTaskId,
				DataTimeStamp:PTaskDataTimeStamp.split(',')
			};
		}
		
	    var nameKey = me.objectEName + '_WorkLogExportLevel';
        var HasRiskKey = me.objectEName + '_HasRisk';
		if(values[HasRiskKey]){
			entity.HasRisk = values[HasRiskKey] ? true : false;
		}
		var IsFinish = me.objectEName + '_IsFinish';
		if(values[IsFinish]){
			entity.IsFinish = values[IsFinish] ? true : false;
		}
		var IsOver = me.objectEName + '_IsOver';
		if(values[IsOver]){
			entity.IsOver = values[IsOver] ? true : false;
		}
		
		var Workload = me.objectEName + '_Workload';
		if(values[Workload]){
			entity.Workload = values[Workload];
		}

		params.AttachmentUrlList = [];
		params.entity = entity;
		return params;
	},
	
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this;
		entity = me.getAddParams();
		var values = me.getForm().getValues();
		var fields = [];
		var fields = ['Id', 'ToDayContent', 'NextDayContent', 'WorkLogExportLevel','IsFinish','Workload','HasRisk','IsOver'];
		switch(me.objectEName) {
			case 'PWorkWeekLog':
				fields.push('Status');
				break;
			case "PWorkMonthLog":
			case '':
				fields.push('Status');
				break;
			default:
				break;
		}
		entity.fields = fields.join(',');
		delete entity.AttachmentUrlList;
		entity.entity.Id = values[me.objectEName + '_Id'];
		return entity;
		return;
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddTableLayoutItems: function() {
		var me = this,
			items = [];
		//当天/本周/本月计划
		me.ToDayContent.colspan = 3;
		me.ToDayContent.width = me.defaults.width * me.ToDayContent.colspan;
		items.push(me.ToDayContent);

		//明天/下周/下月计划
		me.NextDayContent.colspan = 3;
		me.NextDayContent.width = me.defaults.width * me.NextDayContent.colspan;
		items.push(me.NextDayContent);

		//抄送人
		me.PWorkLogCopyFor.colspan = 3;
		me.PWorkLogCopyFor.width = me.defaults.width * me.PWorkLogCopyFor.colspan;
		items.push(me.PWorkLogCopyFor);

		//可见级别
		me.WorkLogExportLevel.colspan = 1;
		me.WorkLogExportLevel.width = 220; //me.defaults.width * me.WorkLogExportLevel.colspan;
		items.push(me.WorkLogExportLevel);

	//是否已完成
		me.IsFinish.colspan = 1;
		me.IsFinish.hidden = true;
		me.IsFinish.width = 80 * me.IsFinish.colspan;
		items.push(me.IsFinish);
		
		//完成工作量
		me.Workload.colspan =1;
		me.Workload.hidden = true;
		me.Workload.width = 189 * me.Workload.colspan;
		items.push(me.Workload);
		
		//完成工作量（显示）
		me.Workload2.colspan = 1;
//		me.Workload2.hidden = true;
		me.Workload2.width =  220 * me.Workload.colspan;
		items.push(me.Workload2);
		
		me.HasRisk.colspan = 1;
		me.HasRisk.hidden = true;
		me.HasRisk.width = me.defaults.width * me.HasRisk.colspan;
		items.push(me.HasRisk);
		//是否结束
		me.IsOver.colspan = 1;
		me.IsOver.hidden = true;
		me.IsOver.width = 80 * me.IsOver.colspan;
		items.push(me.IsOver);
		
		return items;
	},
 /**设置任务日志显示隐藏*/
	SetVisible: function(bo) {
		var me = this;
		var HasRisk = me.getComponent(me.objectEName + '_HasRisk');
		var IsOver = me.getComponent(me.objectEName + '_IsOver');
		var Workload = me.getComponent(me.objectEName + '_Workload');
		var IsFinish = me.getComponent(me.objectEName + '_IsFinish');
		var Workload2=me.getComponent(me.objectEName + '_Workload2');

		HasRisk.setVisible(bo);
		IsOver.setVisible(bo);
		Workload.setVisible(bo);
		Workload2.setVisible(bo);
		IsFinish.setVisible(bo);
	},
	
	/**查看时任务日志进度条显示隐藏*/
	ShowVisible: function(bo) {
		var me=this;
		var Workload2=me.getComponent(me.objectEName + '_Workload2');
		var Workload=me.getComponent(me.objectEName + '_Workload');
		var HasRisk = me.getComponent(me.objectEName + '_HasRisk');
		var IsOver = me.getComponent(me.objectEName + '_IsOver');
		var Workload = me.getComponent(me.objectEName + '_Workload');
		var IsFinish = me.getComponent(me.objectEName + '_IsFinish');

			Workload2.setVisible(bo);
            Workload.setVisible(!bo);
            HasRisk.setVisible(bo);
			IsOver.setVisible(bo);
			IsFinish.setVisible(bo);

	},
	
	EditVisible: function(bo) {
		var me=this;
		var Workload2=me.getComponent(me.objectEName + '_Workload2');
		var Workload=me.getComponent(me.objectEName + '_Workload');
		var HasRisk = me.getComponent(me.objectEName + '_HasRisk');
		var IsOver = me.getComponent(me.objectEName + '_IsOver');
		var Workload = me.getComponent(me.objectEName + '_Workload');
		var IsFinish = me.getComponent(me.objectEName + '_IsFinish');
        Workload2.setVisible(bo);
        Workload.setVisible(!bo);
		HasRisk.setVisible(!bo);
		IsOver.setVisible(!bo);
		IsFinish.setVisible(!bo);
	},
	/**创建文档可见组件*/
	createAddShowItems: function() {
		var me = this;
		var ToDayContentLabel = "总结";
		var NextDayContentLabel = "计划";
		me.createToDayContent(ToDayContentLabel);
		me.createNextDayContent(NextDayContentLabel);
		me.createWorkLogExportLevel("范围");
		me.createStatus('是否提交');
		me.createCopyUser("抄送人");
	
		me.createIsFinish();
		me.createWorkload();
//		me.createDateCode();
		me.createHasRisk();
		me.createIsOver();
		me.createWorkprogress();
	},
	/**是否存在风险*/
	createHasRisk: function(fieldLabel) {
		var me = this;
		me.HasRisk = {
			boxLabel: '是否存在风险',
			name: me.objectEName + '_HasRisk',
			itemId: me.objectEName + '_HasRisk',
			xtype: 'checkbox',
			checked: false,
			style: {
				marginLeft: '70px'
			}
		};
	},
	/**是否完成*/
	createIsFinish: function() {
		var me = this;
		me.IsFinish = {
			boxLabel: '是否完成',
			name: me.objectEName + '_IsFinish',
			itemId: me.objectEName + '_IsFinish',
			xtype: 'checkbox',
			checked: false,
			style: {
				marginLeft: '0px'
			}
		};
	},
	/**日期标记*/
	createDateCode: function() {
		var me = this;
		me.DateCode = {
			fieldLabel: '日期标识',
			xtype: 'datefield',
			format: 'Y-m-d',
			name: me.objectEName + '_DateCode',
			itemId: me.objectEName + '_DateCode',
			emptyText: '日期标识'
		};
	},

	/**已完成工作量*/
	createWorkload: function() {
		var me = this;
		me.Workload = {
		    xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: me.Workload,
			value: '0',
			colspan: 1,
			width: me.defaults.width * 1,
			labelSeparator: '',
			fieldLabel: '已完成工作量',
			labelWidth: 80,
			name: me.objectEName + '_Workload',
			itemId: me.objectEName + '_Workload',
			emptyText: '完成度',
			style: {
				marginLeft: '20px'
			}
		};
	},
	/**是否结束*/
	createIsOver: function() {
		var me = this;
		me.IsOver = {
			boxLabel: '是否结束',
			name: me.objectEName + '_IsOver',
			itemId: me.objectEName + '_IsOver',
			xtype: 'checkbox',
			checked: false,
			style: {
				marginLeft: '0px'
			}
		};
	},
	/**进度条*/
	createWorkprogress: function() {
		var me = this;
		me.Workload2 = {
			xtype: 'label',
			hidden:true,
			labelWidth: 80,
			text: '已完成工作量',
			name: me.objectEName + '_Workload2',
			itemId: me.objectEName + '_Workload2',
			margin: '0 0 0 0',
			style: {
				marginLeft: '0px'
			}
		};
	},
	changeWorkload: function(data) {
		var me=this;
		me.WorkLogDay_Workload= data.value.WorkLogDay_Workload * 100;
			var Workload2=me.getComponent(me.objectEName + '_Workload2');
        var templet =
			'<div><div style="float:left;width:35%;"><span> &nbsp; &nbsp; &nbsp; 完成度：</span></div><div class="progress progress-mini" style="float:left;width:30%;height:6px;margin:0;margin-top:3px;">' +
            '<div style="width: {WorkLogDay_Workload};" class="progress-bar"></div>' +
			'</div><div style="float:left;width:33%;">&nbsp;{WorkLogDay_Workload}</div>';
		var html = templet.replace(/{WorkLogDay_Workload}/g, (me.WorkLogDay_Workload ) +'%');
		Workload2.update(html);
		return data;
		
	}
});