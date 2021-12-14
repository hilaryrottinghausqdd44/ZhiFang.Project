/**
 * 项目监控
 * @author longfc
 * @version 2017-03-23
 */
Ext.define('Shell.class.wfm.business.pproject.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '项目监控',
	width: 785,
	height: 555,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPProjectById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddPProject',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePProjectByField',

	/**布局方式*/
	layout: 'anchor',
	defaults: {
		anchor: '100%'
	},
	/**fieldset布局*/
	fieldsetLayout: {
		type: 'table',
		columns: 3
	},
	/**fieldset内部组件初始属性*/
	fieldsetDefaults: {
		labelWidth: 100,
		width: 230,
		labelAlign: 'right'
	},
	/**每个组件的默认属性*/

	defaultsCom: { labelWidth: 100, width: 230, labelAlign: 'right' },

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
		me.comboStoreLoad();
	},
	/**编辑时预加载下拉框的数据,以便还原显示值*/
	comboStoreLoad: function() {
		var me = this;
		//if(me.formtype == "add") return;

		var BasicInfoItemsArr = ["PProject_TypeID", "PProject_UrgencyID", "PProject_ProvinceID", "PProject_RiskLevelID", "PProject_ContentID"];

		Ext.Array.each(BasicInfoItemsArr, function(itemId) {
			var combo = me.getComponent("BasicInfo").getComponent(itemId);
			combo.store.load();
		});

		var ProgressManagementArr = ["PProject_PaceEvalID", "PProject_PhaseID", "PProject_DynamicRiskLevelID", "PProject_DelayLevelID"];
		Ext.Array.each(ProgressManagementArr, function(itemId) {
			var combo = me.getComponent("ProgressManagement").getComponent(itemId);
			combo.store.load();
		});
	},
	initComponent: function() {
		var me = this;
		me.buttonToolbarItems = ['->'];

		me.buttonToolbarItems.push({
			text: '提交',
			iconCls: 'button-save',
			tooltip: '提交',
			handler: function() {
				me.onSave(true);
			}
		}, 'reset');
		
		me.callParent(arguments);
	},
	/**创建合同选择*/
	createPContractName: function() {
		var me = this;
		var com = {
			fieldLabel: "合同选择",
			name: 'PProject_PContractName',
			itemId: 'PProject_PContractName',
			allowBlank: true,
			emptyText: "合同选择  必填项",
			xtype: 'uxCheckTrigger',
			allowBlank: false,
			colspan: 2,
			width: me.defaultsCom.width * 2,
			className: 'Shell.class.wfm.business.pproject.contract.CheckGrid',
			classConfig: {
				title: "合同选择",
				defaultLoad: true,
				width:935	
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('PProject_PContractName');
					var Id = me.getComponent('PProject_PContractID');
					var objValue = {
						PProject_PContractID: record.data.Id,
						PProject_PContractName: record.data.Name,
						PProject_PClientID: record.data.PClientID,
						PProject_PClientName: record.data.PClientName,
						PProject_Content: record.data.Content,
						PProject_ContentID: record.data.ContentID,
						PProject_ProvinceName: record.data.ProvinceName,
						PProject_ProvinceID: record.data.ProvinceID,
						PProject_Principal: record.data.Principal,
						PProject_PrincipalID: record.data.PrincipalID,
						PProject_SignDate: record.data.SignDate
					};
					if(me.formtype == "add") {
						objValue.PProject_ProjectLeader = record.data.PManager;
						objValue.PProject_ProjectLeaderID = record.data.PManagerID;
					}
					if(objValue.PProject_SignDate) objValue.PProject_SignDate = JcallShell.Date.toString(objValue.PProject_SignDate, true);
					objValue = JShell.Server.Mapping(objValue);
					
					
					me.getForm().setValues(objValue);
					p.close();
				}
			}
		};
		return com;
	},

	/**项目基本信息*/
	createBasicInfoFieldSet: function() {
		var me = this;
		var items = [];
		items.push({
			fieldLabel: '项目名称',
			labelWidth: 85,
			name: 'PProject_CName',
			emptyText: '必填项',
			allowBlank: false,
			colspan: 3,
			width: me.defaultsCom.width * 3
		});
		var typeID = me.createComboBox("项目类型", "ProjectType", "PProject_TypeID");
		typeID.labelWidth = 85;
		typeID.emptyText='必填项';
		typeID.allowBlank=false;
		
		var UrgencyID = me.createComboBox("紧急程度", "Urgency", "PProject_UrgencyID");
		UrgencyID.labelWidth = 85;

		var RiskLevelID = me.createComboBox("项目风险等级", "RiskGrade", "PProject_RiskLevelID");
		RiskLevelID.labelWidth = 85;
		items.push(typeID, UrgencyID, RiskLevelID);

		var PContractName = me.createPContractName();
		PContractName.labelWidth = 85;

		var ContentID = me.createComboBox("合同类型", "ContracType", "PProject_ContentID");
		ContentID.forceSelection = false;
		ContentID.labelWidth = 85;
		ContentID.locked = true;
		ContentID.readOnly = true;
		ContentID.emptyText = '选择合同后自动匹配';
		items.push(PContractName, ContentID);

		var ProvinceID = me.createComboBox("所属省份", "ProvinceID", "PProject_ProvinceID");
		ProvinceID.forceSelection = false;
		ProvinceID.labelWidth = 85;
		ProvinceID.locked = true;
		ProvinceID.readOnly = true;
		ProvinceID.emptyText = '选择合同后自动匹配';

		items.push({
			fieldLabel: '用户名称',
			labelWidth: 85,
			name: 'PProject_PClientName',
			itemId: 'PProject_PClientName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.business.invoice.basic.CheckGrid',
			emptyText: '选择合同后自动匹配',
			locked: true,
			readOnly: true,
			colspan: 3,
			width: me.defaultsCom.width * 3
		});

		items.push(ProvinceID, {
			fieldLabel: '签署日期',
			labelWidth: 85,
			name: 'PProject_SignDate',
			itemId: 'PProject_SignDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			emptyText: '选择合同后自动匹配',
			locked: true,
			readOnly: true,
			colspan: 1,
			width: me.defaultsCom.width * 1
		}, {
			fieldLabel: '要求完成时间',
			labelWidth: 85,
			name: 'PProject_ReqEndTime',
			itemId: 'PProject_ReqEndTime',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaultsCom.width * 1
		});

		var Principal = me.createHREmployeeCName('销售负责人', 'PProject_Principal', 'PProject_PrincipalID', 'BasicInfo');
		Principal.locked = true;
		Principal.readOnly = true;
		Principal.emptyText = '选择合同后自动匹配';

		var fieldset = {
			xtype: 'fieldset',
			title: '项目基本信息',
			collapsible: false,
			defaultType: 'textfield',
			itemId: 'BasicInfo',
			layout: me.fieldsetLayout,
			defaults: me.fieldsetDefaults,
			items: items
		};
		return fieldset;
	},
	/**项目实施计划*/
	createImplementationPlanFieldSet: function() {
		var me = this,
			items = [];
		var ProjectLeader = me.createHREmployeeCName('实施负责人', 'PProject_ProjectLeader', 'PProject_ProjectLeaderID', 'ImplementationPlan');
		var ProjectExec = me.createHREmployeeCName('实施人员', 'PProject_ProjectExec', 'PProject_ProjectExecID', 'ImplementationPlan');
		var PhaseManager = me.createHREmployeeCName('进度管理人', 'PProject_PhaseManager', 'PProject_PhaseManagerID', 'ImplementationPlan');

		items.push(ProjectLeader, ProjectExec, PhaseManager);

		items.push({
			fieldLabel: '预计总开始时间',
			labelWidth: 97,
			name: 'PProject_EstiStartTime',
			itemId: 'PProject_EstiStartTime',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaultsCom.width * 1+7,
			emptyText: '必填项',
			allowBlank: false,
			style: {
				marginLeft: '-7px'
			},	
			
		 	validator: function(value) {
				var EndDate = me.getComponent("ImplementationPlan").getComponent("PProject_EstiEndTime");
				if(value) {
					var EstiEndTime = EndDate.getValue();
					var EndDateValue = JcallShell.Date.toString(EstiEndTime, true);
					var StartDateValue = JcallShell.Date.toString(value, true);
					var EstiWorkload= me.getComponent("ImplementationPlan").getComponent('PProject_EstiWorkload');
					if(StartDateValue > EndDateValue) {
						value = "";
						EstiWorkload.setValue('');
						return '预计总开始时间不能大于预计总完成时间';
					}
					else {
						return true;
					}
				} else {
					return true;
				}
			},
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					var StartDate=me.getComponent("ImplementationPlan").getComponent("PProject_EstiStartTime");
					var EndDate = me.getComponent("ImplementationPlan").getComponent("PProject_EstiEndTime");
					var EstiStartTime = StartDate.getValue();
					var EstiEndTime = EndDate.getValue();
			    	var EndDateValue = JcallShell.Date.toString(EstiEndTime, true);
					var StartDateValue = JcallShell.Date.toString(EstiStartTime, true);
                    if(EndDateValue && StartDateValue){
                    	EndDate.validate();
                    	me.calcEstiAllDays();
                    }
				}
			}
		}, {
			fieldLabel: '预计总完成时间',
			labelWidth: 97,
			name: 'PProject_EstiEndTime',
			itemId: 'PProject_EstiEndTime',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaultsCom.width * 1+7,
			emptyText: '必填项',
			allowBlank: false,
			style: {
				marginLeft: '-7px'
			},		
			validator: function(value) {
				var StartDate=me.getComponent("ImplementationPlan").getComponent("PProject_EstiStartTime");
				if(value) {
					var EstiStartTime = StartDate.getValue();
					var StartDateValue = JcallShell.Date.toString(EstiStartTime, true);
					var EndDateValue = JcallShell.Date.toString(value, true);
					var EstiWorkload= me.getComponent("ImplementationPlan").getComponent('PProject_EstiWorkload');
					if(StartDateValue > EndDateValue) {
						value = "";
						EstiWorkload.setValue('');
						return '预计总完成时间不能小于预计总开始时间';
					} else {
						return true;
					}
				} else {
					return true;
				}
			},
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					var StartDate=me.getComponent("ImplementationPlan").getComponent("PProject_EstiStartTime");
					var EndDate = me.getComponent("ImplementationPlan").getComponent("PProject_EstiEndTime");
					var EstiStartTime = StartDate.getValue();
					var EstiEndTime = EndDate.getValue();
					var EndDateValue = JcallShell.Date.toString(EstiEndTime, true);
					var StartDateValue = JcallShell.Date.toString(EstiStartTime, true);
                    if(EndDateValue && StartDateValue){
                    	StartDate.validate();
                    	me.calcEstiAllDays();
                    }
				}
			}
		}, {
			fieldLabel: '预计总工作量',
			labelWidth: 95,
			name: 'PProject_EstiWorkload',
			itemId: 'PProject_EstiWorkload',
			xtype: 'numberfield',
			emptyText: '',
			colspan: 1,
			hideTrigger: true,
			emptyText: '必填项',
			allowBlank: false,
			width: me.defaultsCom.width * 1
		});
		var fieldset = {
			xtype: 'fieldset',
			title: '项目实施计划',
			collapsible: true,
			defaultType: 'textfield',
			itemId: 'ImplementationPlan',
			layout: me.fieldsetLayout,
			defaults: me.fieldsetDefaults,
			items: items
		};
		return fieldset;
	},
	/**项目进度管理*/
	createProgressManagementFieldSet: function() {
		var me = this,
			items = [];

		var DynamicRiskLevelID = me.createComboBox("动态风险等级", "RiskGrade", "PProject_DynamicRiskLevelID");
		DynamicRiskLevelID.labelWidth = 85;
		//项目完成度对应进度评估
		var PaceEvalID = me.createComboBox("项目完成度", "Completion degree", "PProject_PaceEvalID");
		PaceEvalID.labelWidth = 85;
		items.push({
			fieldLabel: '实际开始时间',
			labelWidth: 85,
			name: 'PProject_StartTime',
			itemId: 'PProject_StartTime',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaultsCom.width * 1
		}, {
			fieldLabel: '进场日期',
			labelWidth: 85,
			name: 'PProject_EntryTime',
			itemId: 'PProject_EntryTime',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaultsCom.width * 1
		}, DynamicRiskLevelID);

		var PhaseID = me.createComboBox("项目阶段", "ProjectPhase", "PProject_PhaseID");
		PhaseID.labelWidth = 85;
		var DelayLevelID = me.createComboBox("延期程度", "Delay degree", "PProject_DelayLevelID");
		DelayLevelID.labelWidth = 85;
		items.push(PaceEvalID, PhaseID, DelayLevelID);

		items.push({
			fieldLabel: '动态完成时间',
			labelWidth: 85,
			name: 'PProject_EndTime',
			itemId: 'PProject_EndTime',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaultsCom.width * 1
		}, {
			fieldLabel: '动态剩余工作量',
			labelWidth: 115,
			name: 'PProject_DynamicRemainingWorkDays',
			itemId: 'PProject_DynamicRemainingWorkDays',
			xtype: 'numberfield',
			hideTrigger: true,
			emptyText: '',
			colspan: 1,
			width: me.defaultsCom.width * 1
		}, {
			fieldLabel: '已投入工作量',
			labelWidth: 105,
			name: 'PProject_Workload',
			itemId: 'PProject_Workload',
			xtype: 'numberfield',
			hideTrigger: true,
			colspan: 1,
			width: me.defaultsCom.width * 1
		});

		var fieldset = {
			xtype: 'fieldset',
			title: '项目进度管理',
			collapsible: true,
			defaultType: 'textfield',
			itemId: 'ProgressManagement',
			layout: me.fieldsetLayout,
			defaults: me.fieldsetDefaults,
			items: items
		};
		return fieldset;
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;
		var items = [];

		//项目基本信息
		items.push(me.createBasicInfoFieldSet());
		//项目实施计划
		items.push(me.createImplementationPlanFieldSet());
		//项目进度管理
		items.push(me.createProgressManagementFieldSet());
		items.push({
			fieldLabel: '备注',
			fieldWidth: 40,
			labelAlign: 'right',
			name: 'PProject_Memo',
			itemId: 'PProject_Memo',
			xtype: 'textarea',
			height: 45,
			colspan: 3,
			width: me.defaultsCom.width * 3
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'PProject_Id',
			hidden: true
		}, {
			fieldLabel: '客户主键ID',
			name: 'PProject_PClientID',
			itemId: 'PProject_PClientID',
			hidden: true
		},  {
			fieldLabel: '销售负责人ID',
			name: 'PProject_PrincipalID',
			itemId: 'PProject_PrincipalID',
			hidden: true
		}, {
			fieldLabel: '实施负责人ID',
			name: 'PProject_ProjectLeaderID',
			itemId: 'PProject_ProjectLeaderID',
			hidden: true
		}, {
			fieldLabel: '实施人员ID',
			name: 'PProject_ProjectExecID',
			itemId: 'PProject_ProjectExecID',
			hidden: true
		}, {
			fieldLabel: '进度管理人员ID',
			name: 'PProject_PhaseManagerID',
			itemId: 'PProject_PhaseManagerID',
			hidden: true
		}, {
			fieldLabel: '创建人Id',
			name: 'PProject_CreatEmpIdID',
			itemId: 'PProject_CreatEmpIdID',
			hidden: true
		},{
			fieldLabel: '合同ID',
			name: 'PProject_PContractID',
			itemId: 'PProject_PContractID',
			hidden: true
		});
		return items;
	},
	/**更改标题*/
	changeTitle: function() {},
	/**返回数据处理方法*/
	changeResult: function(data) {
		if(data.PProject_EntryTime) data.PProject_EntryTime = JShell.Date.getDate(data.PProject_EntryTime);
		if(data.PProject_SignDate) data.PProject_SignDate = JShell.Date.getDate(data.PProject_SignDate);
		if(data.PProject_EstiStartTime) data.PProject_EstiStartTime = JShell.Date.getDate(data.PProject_EstiStartTime);
		if(data.PProject_EstiEndTime) data.PProject_EstiEndTime = JShell.Date.getDate(data.PProject_EstiEndTime);

		if(data.PProject_StartTime) data.PProject_StartTime = JShell.Date.getDate(data.PProject_StartTime);
		if(data.PProject_EndTime) data.PProject_EndTime = JShell.Date.getDate(data.PProject_EndTime);
		if(data.PProject_AcceptTime) data.PProject_AcceptTime = JShell.Date.getDate(data.PProject_AcceptTime);
		if(data.PProject_ReqEndTime) data.PProject_ReqEndTime = JShell.Date.getDate(data.PProject_ReqEndTime);

		var reg = new RegExp("<br />", "g");
		if(data.PProject_Memo) data.PProject_Memo = data.PProject_Memo.replace(reg, "\r\n");
		if(data.PProject_ExtraMsg) data.PProject_ExtraMsg = data.PProject_ExtraMsg.replace(reg, "\r\n");
		if(data.PProject_OtherMsg) data.PProject_OtherMsg = data.PProject_OtherMsg.replace(reg, "\r\n");
		return data;
	},
	/**初始化监听*/
	initListeners: function() {
		var me = this;
	},

	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		if(!JShell.System.Cookie.map.USERID) {
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}

		var entity = {
			CName: values.PProject_CName,
			IsStandard:0
//			PContractName: values.PContractName, //合同名称
//			PClientName: values.PClientName //客户
		};
		if(me.formtype = "add") {
			entity.CreatEmpIdID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
			entity.IsUse = 1;
		}
		if(values.PProject_PContractID) {
			entity.PContractID = values.PProject_PContractID; //合同
			entity.PContractName = values.PProject_PContractName;
		}
		if(values.PProject_ContentID) {
			entity.ContentID = values.PProject_ContentID; //合同类型Id
			entity.Content = me.getComponent("BasicInfo").getComponent("PProject_ContentID").getRawValue(); //合同类型
		}
		if(values.PProject_PClientID) {
			entity.PClientID = values.PProject_PClientID;
			entity.PClientName = values.PProject_PClientName;
		}
		if(values.PProject_PhaseID) {
			entity.PhaseID = values.PProject_PhaseID;
			entity.PhaseName = me.getComponent("ProgressManagement").getComponent("PProject_PhaseID").getRawValue();
		}
		if(values.PProject_RiskLevelID) {
			entity.RiskLevelID = values.PProject_RiskLevelID;
			entity.RiskLevelName = me.getComponent("BasicInfo").getComponent("PProject_RiskLevelID").getRawValue();
		}
		if(values.PProject_PaceID) {
			entity.PaceID = values.PProject_PaceID;
			entity.PaceName = me.getComponent("ProgressManagement").getComponent("PProject_PaceID").getRawValue();
		}
		if(values.PProject_DynamicRiskLevelID) {
			entity.DynamicRiskLevelID = values.PProject_DynamicRiskLevelID;
			entity.DynamicRiskLevelName = me.getComponent("ProgressManagement").getComponent("PProject_DynamicRiskLevelID").getRawValue();
		}
		if(values.PProject_DelayLevelID) {
			entity.DelayLevelID = values.PProject_DelayLevelID;
			entity.DelayLevelName = me.getComponent("ProgressManagement").getComponent("PProject_DelayLevelID").getRawValue();
		}
		if(values.PProject_EntryTime) {
			entity.EntryTime = JShell.Date.toServerDate(values.PProject_EntryTime);
		}
		if(values.PProject_SignDate) {
			entity.SignDate = JShell.Date.toServerDate(values.PProject_SignDate); //签署日期
		}
		if(values.PProject_PrincipalID) {
			entity.PrincipalID = values.PProject_PrincipalID;
			entity.Principal = values.PProject_Principal;
		}
		if(values.PProject_ProjectLeaderID) {
			entity.ProjectLeaderID = values.PProject_ProjectLeaderID;
			entity.ProjectLeader = values.PProject_ProjectLeader;
		}
		if(values.PProject_ProjectExecID) {
			entity.ProjectExecID = values.PProject_ProjectExecID;
			entity.ProjectExec = values.PProject_ProjectExec;
		}
		if(values.PProject_PhaseManagerID) {
			entity.PhaseManagerID = values.PProject_PhaseManagerID;
			entity.PhaseManager = values.PProject_PhaseManager;
		}
		if(values.PProject_EstiStartTime) {
			entity.EstiStartTime = JShell.Date.toServerDate(values.PProject_EstiStartTime);
		}
		if(values.PProject_EstiEndTime) {
			entity.EstiEndTime = JShell.Date.toServerDate(values.PProject_EstiEndTime);
		}
		if(values.PProject_StartTime) {
			entity.StartTime = JShell.Date.toServerDate(values.PProject_StartTime);
		}
		if(values.PProject_EndTime) {
			entity.EndTime = JShell.Date.toServerDate(values.PProject_EndTime);
		}
		if(values.PProject_AcceptTime) {
			entity.AcceptTime = JShell.Date.toServerDate(values.PProject_AcceptTime);
		}
		if(values.PProject_DynamicRemainingWorkDays) {
			entity.DynamicRemainingWorkDays = values.PProject_DynamicRemainingWorkDays;
		}
		if(values.PProject_ScheduleDelayPercent) {
			entity.ScheduleDelayPercent = values.PProject_ScheduleDelayPercent;
		}
		if(values.PProject_ScheduleDelayDays) {
			entity.ScheduleDelayDays = values.PProject_ScheduleDelayDays;
		}
		if(values.PProject_MoreWorkPercent) {
			entity.MoreWorkPercent = values.PProject_MoreWorkPercent;
		}
		if(values.PProject_MoreWorkDays) {
			entity.MoreWorkDays = values.PProject_MoreWorkDays;
		}

		if(values.PProject_OtherMsg) {
			entity.OtherMsg = values.PProject_OtherMsg.replace(/\\/g, '&#92');
			entity.OtherMsg = entity.OtherMsg.replace(/[\r\n]/g, '<br />');
		}
		if(values.PProject_ExtraMsg) {
			entity.ExtraMsg = values.PProject_ExtraMsg.replace(/\\/g, '&#92');
			entity.ExtraMsg = entity.ExtraMsg.replace(/[\r\n]/g, '<br />');
		}
		if(values.PProject_Memo) {
			entity.Memo = values.PProject_Memo.replace(/\\/g, '&#92');
			entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		}
		if(values.PProject_ReqEndTime) {
			entity.ReqEndTime = JShell.Date.toServerDate(values.PProject_ReqEndTime);
		}
		if(values.PProject_EstiWorkload) {
			entity.EstiWorkload = values.PProject_EstiWorkload;
		}
		if(values.PProject_Workload) {
			entity.Workload = values.PProject_Workload;
		}
		if(values.PProject_PaceEvalID) {
			entity.PaceEvalID = values.PProject_PaceEvalID;
			entity.PaceEvalName = me.getComponent("ProgressManagement").getComponent("PProject_PaceEvalID").getRawValue();
		}
		if(values.PProject_UrgencyID) {
			entity.UrgencyID = values.PProject_UrgencyID;
			entity.UrgencyName = me.getComponent("BasicInfo").getComponent("PProject_UrgencyID").getRawValue();
		}
		if(values.PProject_TypeID) {
			entity.TypeID = values.PProject_TypeID;
		}

		if(values.PProject_ProvinceID) {
			entity.ProvinceID = values.PProject_ProvinceID;
			entity.ProvinceName = me.getComponent("BasicInfo").getComponent("PProject_ProvinceID").getRawValue();
		}

		return { entity: entity };
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();

		var fields = [
			'Id', 'CName', 'PContractID', 'PContractName', 'PClientID', 'PClientName', 'PhaseID', 'PhaseName', 'RiskLevelID', 'RiskLevelName', 'PaceID', 'PaceName', 'DynamicRiskLevelID', 'DynamicRiskLevelName', 'DelayLevelID', 'DelayLevelName', 'EntryTime', 'SignDate', 'PrincipalID', 'Principal', 'ProjectLeaderID', 'ProjectLeader', 'ProjectExecID', 'ProjectExec', 'PhaseManagerID', 'PhaseManager', 'EstiStartTime', 'EstiEndTime', 'StartTime', 'EndTime', 'AcceptTime', 'DynamicRemainingWorkDays', 'ScheduleDelayPercent', 'ScheduleDelayDays', 'MoreWorkPercent', 'MoreWorkDays', 'OtherMsg', 'Memo', 'ReqEndTime', 'EstiWorkload', 'Workload', 'PaceEvalID', 'PaceEvalName', 'UrgencyID', 'UrgencyName', 'TypeID', 'ContentID', 'Content', 'ProvinceID', 'ProvinceName'
		];
		entity.fields = fields.join(',');

		entity.entity.Id = values.PProject_Id;
		return entity;
	},
	/**保存按钮点击处理方法*/
	onSave: function(isSubmit) {
		var me = this,
			values = me.getForm().getValues();
		if(!me.getForm().isValid()) {
			me.fireEvent('isValid', me);
			return;
		}
		me.onSaveClick();
	},
	/**
	 * 保存按钮点击处理方法
	 * idKey:itemId对应该的隐藏组件
	 * setItemId:itemId所在的分组Id
	 * */
	createHREmployeeCName: function(fieldLabel, itemId, idKey, setItemId) {
		var me = this;
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var employeeCName = {
			fieldLabel: fieldLabel,
			name: itemId,
			itemId: itemId,
			colspan: 1,
			width: me.defaultsCom.width * 1,
			xtype: 'uxCheckTrigger',
			labelWidth: 85,
			className: 'Shell.class.sysbase.user.CheckApp',
			classConfig: {
				title: fieldLabel,
				height: 420,
				defaultLoad: true,
				defaultWhere: ""
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent(setItemId).getComponent(itemId);
					var Id = me.getComponent(idKey); //getComponent(setItemId)
					if(CName) CName.setValue(record ? record.get('HREmployee_CName') : '');
					if(Id) Id.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				}
			}
		};
		return employeeCName;
	},
	/**创建下拉*/
	createComboBox: function(fieldLabel, dictTypeCode, itemId) {
		var me = this;
		var fields = "",
			selectUrl = "",
			displayField = 'BDict_CName',
			valueField = 'BDict_Id',
			defaultOrderBy = [{ property: 'BDict_DispOrder', direction: 'ASC' }],
			where = "";
		var storeFields = ['BDict_CName', 'BDict_Id', 'BDict_DispOrder'];
		switch(dictTypeCode) {
			case "ProvinceID":
				displayField = 'BProvince_Name';
				valueField = 'BProvince_Id';
				fields = "BProvince_Id,BProvince_Name";
				storeFields = ['BProvince_Id', 'BProvince_Name'];
				where = "bprovince.BCountry.Id=5742820397511247346";
				selectUrl = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_SearchBProvinceByHQL?isPlanish=true';
				selectUrl = selectUrl + "&fields=" + fields;
				selectUrl = selectUrl + "&where=" + where;
				defaultOrderBy = [{ property: 'BProvince_Shortcode', direction: 'ASC' }];
				break;
			default:
				fields = "BDict_CName,BDict_Id,BDict_DispOrder";
				selectUrl = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true';
				selectUrl = selectUrl + "&fields=" + fields;
				where = "pdict.IsUse=1 and pdict.BDictType.DictTypeCode='" + dictTypeCode + "'";
				selectUrl = selectUrl + "&where=" + where;
				break;
		}
		var multiCombo = Ext.create('Ext.form.field.ComboBox', {
			fieldLabel: fieldLabel,
			colspan: 1,
			itemId: itemId,
			name: itemId,
			width: me.defaultsCom.width * 1,
			displayField: displayField,
			valueField: valueField,
			labelWidth: 65,
			labelAlign: 'right',
			//设置输入值是否严格为待选列表中存在的值,true要求输入值必须在列表中存在,默认为false
			forceSelection: true,
			valueNotFoundText: '选择项不存在',
			emptyText: "",
			//mode: 'local',
			queryMode: 'remote', //
			store: new Ext.data.Store({
				fields: storeFields,
				pageSize: 5000,
				remoteSort: true,
				sorters: defaultOrderBy,
				listeners: {
					load: function() {
						//combobox的store中添加listener回填显示值
						//var typeCombo = me.getComponent(itemId);
						//typeCombo.setValue(typeCombo.getValue());
					}
				},
				proxy: {
					type: 'ajax',
					url: selectUrl,
					timeout: 30000,
					autoLoad: true,
					reader: {
						type: 'json',
						root: 'list'
					},
					extractResponseData: function(response) {
						var data = JShell.Server.toJson(response.responseText);
						if(data.success) {
							var info = data.value;
							if(info) {
								var type = Ext.typeOf(info);
								if(type == 'object') {
									info = info;
								} else if(type == 'array') {
									info.list = info;
									info.count = info.list.length;
								} else {
									info = {};
								}

								data.count = info.count || 0;
								data.list = info.list || [];
							} else {
								data.count = 0;
								data.list = [];
							}
							//data = me.changeResult(data);
						} else {
							me.errorInfo = data.msg;
						}
						response.responseText = Ext.JSON.encode(data);
						return response;
					}
				}
			})
		});
		return multiCombo;
	},
	/**计算计划人工作量*/
	calcEstiAllDays: function() {
		var me = this;
		var EstiStartTime= me.getComponent("ImplementationPlan").getComponent('PProject_EstiStartTime').getValue();
		var EstiEndTime= me.getComponent("ImplementationPlan").getComponent('PProject_EstiEndTime').getValue();
		var EstiWorkload= me.getComponent("ImplementationPlan").getComponent('PProject_EstiWorkload');
        if(!EstiStartTime || !EstiEndTime){
        	EstiWorkload.setValue('');
        }
         var sd = JcallShell.Date.getDate(EstiStartTime);
         var et = JcallShell.Date.getDate(EstiEndTime);

        if(EstiStartTime && EstiEndTime){
        	var val=me.diffDate(EstiStartTime,EstiEndTime)+1;
        	EstiWorkload.setValue(val);
        }
	},
	/**计算实际人工作量*/
	calcAllDays: function(com, newValue) {
		var me = this;
	},
	 /**两个日期相差的天数*/
    diffDate :function( sd,et ){//两个日期相差的天数
    	var StartTime = JcallShell.Date.getDate(sd);	
    	var EndTime = JcallShell.Date.getDate(et);	
    	if(EndTime && StartTime){
    		var st = StartTime.getTime(),et = EndTime.getTime();
	        if( st - et > 0 ) return 0;
	        var sds = parseFloat(st / (3600*24*1000)).toFixed(0),eds = parseFloat(et / (3600*24*1000)).toFixed(0);
    	}
        return eds - sds;
    },
	/**计算两日期的天数*/
	getDayCount: function(startDate, endDate) {
		var me = this;
		//讲时间字符串转化为距离1970年1月1日午夜零时的时间间隔的毫秒数  
		var startTime = Date.parse(startDate);
		var endTime = Date.parse(endDate);
		//讲两个时间相减，求出相隔的天数  
		var dayCount = (Math.abs(endTime - startTime)) / 1000 / 60 / 60 / 24;
		return dayCount;
	},
		/**创建数据字段*/
	getStoreFields: function() {
		var me = this;
		var fields = ['Id', 'Memo', 'PClientID', 'ContractID',
			'PrincipalID', 'ProjectLeaderID', 'ProjectExecID',
			'PhaseManagerID', 'CreatEmpIdID', 'CName', 'PClientName',
			'EstiWorkload', 'EstiEndTime', 'EstiStartTime', 'StartTime',
			'EntryTime', 'EndTime','DynamicRemainingWorkDays', 'Workload',
			'PContractID','PContractName', 'PhaseID', 'PhaseName', 'RiskLevelID', 'RiskLevelName', 
			'PaceID', 'PaceName', 'DynamicRiskLevelID', 'DynamicRiskLevelName', 
			'DelayLevelID', 'DelayLevelName', 'SignDate', 'Principal', 'ProjectLeader', 
			 'ProjectExec', 'PhaseManager', 'AcceptTime', 'ScheduleDelayPercent', 
			 'ScheduleDelayDays', 'MoreWorkPercent', 'MoreWorkDays', 'OtherMsg', 'ReqEndTime',
			 'PaceEvalID', 'PaceEvalName', 'UrgencyID', 'UrgencyName', 'TypeID', 'ContentID', 
			 'Content', 'ProvinceID', 'ProvinceName'];
		fields = "PProject_" + fields.join(",PProject_");
		fields = fields.split(',');
		return fields;
	}
	
});