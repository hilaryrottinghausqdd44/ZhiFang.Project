/**
 * 客户端订货总单列表
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.basic.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '订单信息',

	width: 580,
	height: 240,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenOrderDocById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/ST_UDTO_AddReaBmsCenOrderDocAndDt',
	/**修改服务地址*/
	editUrl: '/ReaManageService.svc/ST_UDTO_UpdateReaBmsCenOrderDocAndDt',
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 70,
		width: 160,
		labelAlign: 'right'
	},
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**当前选择的供应商Id*/
	ReaCompID: null,
	/**申请单当前状态*/
	Status: '0',
	/**申请单当前中文名称状态*/
	StatusName: "",
	/**订单状态Key*/
	StatusKey: "ReaBmsOrderDocStatus",
	/**订单数据标志Key*/
	IOFlagKey: "ReaBmsOrderDocIOFlag",
	/**订单接口标志Key*/
	ThirdFlagKey: "ReaBmsOrderDocThirdFlag",
	/**订单申请时,订货方是否取当前登录科室*/
	ISCURDEPT: true,

	afterRender: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if (buttonsToolbar) {
			buttonsToolbar.hide();
		}
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('reacompcheck', 'isEditAfter');
		// if(!me.width)me.width=700;
		// me.defaults.width = parseInt(me.width / me.layout.columns);
		// if(me.defaults.width < 175) me.defaults.width = 175;

		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.IOFlagKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.ThirdFlagKey, false, true, null);
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsCenOrderDoc_Id',
			hidden: true,
			type: 'key'
		}, {
			fieldLabel: 'LabID',
			name: 'ReaBmsCenOrderDoc_LabID',
			itemId: 'ReaBmsCenOrderDoc_LabID',
			hidden: true
		});
		//订货方
		items.push({
			fieldLabel: '订货方',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaBmsCenOrderDoc_LabcName',
			itemId: 'ReaBmsCenOrderDoc_LabcName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			colspan: 2,
			width: me.defaults.width * 2,
			onTriggerClick: function() {
				JcallShell.Win.open('Shell.class.sysbase.org.CheckTree', {
					rootVisible: false,
					OrgType: "1",
					resizable: false,
					listeners: {
						accept: function(p, record) {
							if (record && record.get("tid") == 0) {
								JcallShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onDeptAccept(record);
							p.close();
						}
					}
				}).show();
			}
			/* 
			readOnly: true,
			locked: true,
			onTriggerClick: function() {
				JcallShell.Win.open('Shell.class.rea.client.reacenorg.CheckTree', {
					rootVisible: false,
					OrgType: "1",
					resizable: false,
					listeners: {
						accept: function(p, record) {
							if (record && record.get("tid") == 0) {
								JcallShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onLabcAccept(record);
							p.close();
						}
					}
				}).show();
			} */
		}, {
			fieldLabel: '订货方主键ID',
			hidden: true,
			name: 'ReaBmsCenOrderDoc_LabcID',
			itemId: 'ReaBmsCenOrderDoc_LabcID'
		}, {
			fieldLabel: '实验室平台机构编码',
			hidden: true,
			name: 'ReaBmsCenOrderDoc_ReaServerLabcCode',
			itemId: 'ReaBmsCenOrderDoc_ReaServerLabcCode'
		});
		//供货商
		items.push({
			fieldLabel: '供货商',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaBmsCenOrderDoc_CompanyName',
			itemId: 'ReaBmsCenOrderDoc_CompanyName',
			xtype: 'trigger',
			//triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			colspan: 2,
			width: me.defaults.width * 2,
			onTriggerClick: function() {
				JcallShell.Win.open('Shell.class.rea.client.reacenorg.CheckTree', {
					/**是否显示根节点*/
					rootVisible: false,
					/**机构类型*/
					OrgType: "0",
					resizable: false,
					listeners: {
						accept: function(p, record) {
							if (record && record.get("tid") == 0) {
								JcallShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onCompAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '供货商主键ID',
			hidden: true,
			name: 'ReaBmsCenOrderDoc_CompID',
			itemId: 'ReaBmsCenOrderDoc_CompID'
		}, {
			fieldLabel: '供货商机构编码',
			hidden: true,
			name: 'ReaBmsCenOrderDoc_ReaCompCode',
			itemId: 'ReaBmsCenOrderDoc_ReaCompCode'
		}, {
			fieldLabel: '本地供货商主键ID',
			hidden: true,
			name: 'ReaBmsCenOrderDoc_ReaCompID',
			itemId: 'ReaBmsCenOrderDoc_ReaCompID'
		}, {
			fieldLabel: '本地供货商',
			hidden: true,
			name: 'ReaBmsCenOrderDoc_ReaCompanyName',
			itemId: 'ReaBmsCenOrderDoc_ReaCompanyName'
		}, {
			fieldLabel: '供应商机平台构码',
			hidden: true,
			name: 'ReaBmsCenOrderDoc_ReaServerCompCode',
			itemId: 'ReaBmsCenOrderDoc_ReaServerCompCode'
		});

		//紧急标志
		items.push({
			fieldLabel: '紧急标志',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsCenOrderDoc_UrgentFlag',
			itemId: 'ReaBmsCenOrderDoc_UrgentFlag',
			data: JShell.REA.Enum.getComboboxList('BmsCenOrderDoc_UrgentFlag'),
			colspan: 1,
			width: me.defaults.width * 1,
			allowBlank: false,
			value: '0'
		});
		items.push({
			fieldLabel: '是否启用',
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsCenOrderDoc_DeleteFlag',
			itemId: 'ReaBmsCenOrderDoc_DeleteFlag',
			data: [
				[0, "启用"],
				[1, "禁用"]
			],
			value: 0,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//所属部门
		items.push({
			fieldLabel: '所属部门',
			name: 'ReaBmsCenOrderDoc_DeptName',
			itemId: 'ReaBmsCenOrderDoc_DeptName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '所属部门ID',
			name: 'ReaBmsCenOrderDoc_DeptID',
			itemId: 'ReaBmsCenOrderDoc_DeptID',
			hidden: true
		});
		//订货单号
		items.push({
			fieldLabel: '订货单号',
			name: 'ReaBmsCenOrderDoc_OrderDocNo',
			colspan: 1,
			width: me.defaults.width * 1
		});

		//订单日期
		items.push({
			xtype: 'datefield',
			fieldLabel: '订单日期',
			format: 'Y-m-d', // H:m:s
			name: 'ReaBmsCenOrderDoc_DataAddTime',
			itemId: 'ReaBmsCenOrderDoc_DataAddTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//数据标志
		items.push({
			xtype: 'uxSimpleComboBox',
			fieldLabel: '数据标志',
			name: 'ReaBmsCenOrderDoc_IOFlag',
			itemId: 'ReaBmsCenOrderDoc_IOFlag',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			hasStyle: true,
			border: false,
			data: JShell.REA.StatusList.Status[me.IOFlagKey].List,
		});
		items.push({
			fieldLabel: '操作人',
			name: 'ReaBmsCenOrderDoc_UserName',
			itemId: 'ReaBmsCenOrderDoc_UserName',
			colspan: 1,
			hidden: true,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '操作人ID',
			name: 'ReaBmsCenOrderDoc_UserID',
			itemId: 'ReaBmsCenOrderDoc_UserID',
			hidden: true
		});
		//单据状态
		items.push({
			xtype: 'uxSimpleComboBox',
			fieldLabel: '单据状态',
			name: 'ReaBmsCenOrderDoc_Status',
			itemId: 'ReaBmsCenOrderDoc_Status',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			hasStyle: true,
			border: false,
			data: JShell.REA.StatusList.Status[me.StatusKey].List,
			value: me.Status
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '总价',
			itemId: 'ReaBmsCenOrderDoc_TotalPrice',
			name: 'ReaBmsCenOrderDoc_TotalPrice',
			colspan: 2,
			width: me.defaults.width * 2
		});

		//第三方标志
		items.push({
			xtype: 'uxSimpleComboBox',
			fieldLabel: '第三方标志',
			name: 'ReaBmsCenOrderDoc_IsThirdFlag',
			itemId: 'ReaBmsCenOrderDoc_IsThirdFlag',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			hasStyle: true,
			border: false,
			data: JShell.REA.StatusList.Status[me.ThirdFlagKey].List,
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsCenOrderDoc_Memo',
			itemId: 'ReaBmsCenOrderDoc_Memo',
			colspan: 3,
			width: me.defaults.width * 3,
			height: 40
		});

		items.push({
			xtype: 'displayfield',
			fieldLabel: '审核人',
			name: 'ReaBmsCenOrderDoc_Checker',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			xtype: 'displayfield',
			fieldLabel: '审核日期',
			name: 'ReaBmsCenOrderDoc_CheckTime',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '审核意见',
			itemId: 'ReaBmsCenOrderDoc_CheckMemo',
			name: 'ReaBmsCenOrderDoc_CheckMemo',
			colspan: 2,
			width: me.defaults.width * 2
		});

		items.push({
			xtype: 'displayfield',
			fieldLabel: '审批人',
			name: 'ReaBmsCenOrderDoc_ApprovalCName',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			xtype: 'displayfield',
			fieldLabel: '审批日期',
			name: 'ReaBmsCenOrderDoc_ApprovalTime',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '审批意见',
			itemId: 'ReaBmsCenOrderDoc_批',
			name: 'ReaBmsCenOrderDoc_批',
			colspan: 2,
			width: me.defaults.width * 2
		});

		//供货商确认
		items.push({
			xtype: 'displayfield',
			fieldLabel: '确认人',
			name: 'ReaBmsCenOrderDoc_Confirm',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			xtype: 'displayfield',
			fieldLabel: '确认日期',
			name: 'ReaBmsCenOrderDoc_ConfirmTime',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '确认意见',
			itemId: 'ReaBmsCenOrderDoc_CompMemo',
			name: 'ReaBmsCenOrderDoc_CompMemo',
			colspan: 2,
			width: me.defaults.width * 2
		});
		return items;
	},
	/**@description 订货方选择*/
	onDeptAccept: function(record) {
		var me = this;
		var LabcID = me.getComponent('ReaBmsCenOrderDoc_LabcID');
		var LabcName = me.getComponent('ReaBmsCenOrderDoc_LabcName');
		var ReaServerLabcCode = me.getComponent('ReaBmsCenOrderDoc_ReaServerLabcCode');
	
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.UseCode : '';
		//console.log(platformOrgNo);
		if (text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		LabcID.setValue(id);
		LabcName.setValue(text);
		ReaServerLabcCode.setValue(platformOrgNo);
	},
	/**@description 订货方选择*/
	onLabcAccept: function(record) {
		var me = this;
		var LabcID = me.getComponent('ReaBmsCenOrderDoc_LabcID');
		var LabcName = me.getComponent('ReaBmsCenOrderDoc_LabcName');
		var ReaServerLabcCode = me.getComponent('ReaBmsCenOrderDoc_ReaServerLabcCode');

		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';
		var orgNo = record ? record.data.value.OrgNo : '';

		if (text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		LabcID.setValue(id);
		LabcName.setValue(text);
		ReaServerLabcCode.setValue(platformOrgNo);
	},
	onCompAccept: function(record) {
		var me = this;
		var ComId = me.getComponent('ReaBmsCenOrderDoc_CompID');
		var ComName = me.getComponent('ReaBmsCenOrderDoc_CompanyName');
		var ReaCompID = me.getComponent('ReaBmsCenOrderDoc_ReaCompID');
		var ReaCompanyName = me.getComponent('ReaBmsCenOrderDoc_ReaCompanyName');
		var ReaCompCode = me.getComponent('ReaBmsCenOrderDoc_ReaCompCode');
		var ReaServerCompCode = me.getComponent('ReaBmsCenOrderDoc_ReaServerCompCode');

		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';
		var orgNo = record ? record.data.value.OrgNo : '';
		if (text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		ComId.setValue(id);
		ComName.setValue(text);
		ReaCompCode.setValue(orgNo);

		ReaCompanyName.setValue(text);
		ReaCompID.setValue(id);
		ReaServerCompCode.setValue(platformOrgNo);
		var objValue = {
			"ReaCompID": id,
			"ReaCompCName": text,
			"ReaCompCode": orgNo,
			"PlatformOrgNo": platformOrgNo
		};
		me.fireEvent('reacompcheck', me, objValue);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.fireEvent('isEditAfter', me);
		var DeptID = me.getComponent('ReaBmsCenOrderDoc_DeptID');
		if (!DeptID.getValue()) {
			var deptId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";
			DeptID.setValue(deptId);
		}
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);

		var UserID = me.getComponent('ReaBmsCenOrderDoc_UserID');
		var UserName = me.getComponent('ReaBmsCenOrderDoc_UserName');
		var DeptName = me.getComponent('ReaBmsCenOrderDoc_DeptName');
		var DeptID = me.getComponent('ReaBmsCenOrderDoc_DeptID');

		UserID.setValue(JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID));
		UserName.setValue(JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERNAME));
		var deptName = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTNAME) || "";
		var deptId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";
		DeptID.setValue(deptId);
		DeptName.setValue(deptName);

		var DataAddTime = me.getComponent('ReaBmsCenOrderDoc_DataAddTime');
		DataAddTime.setValue(new Date());

		var ReaServerLabcCode = me.getComponent('ReaBmsCenOrderDoc_ReaServerLabcCode');
		ReaServerLabcCode.setValue(JShell.REA.System.CENORG_CODE);

		var ReaServerLabcCode = me.getComponent('ReaBmsCenOrderDoc_ReaServerLabcCode');
		var reaServerLabcCode = JShell.REA.System.CENORG_CODE;
		if (!reaServerLabcCode) reaServerLabcCode = "";
		ReaServerLabcCode.setValue(reaServerLabcCode);

		//订货方只读
		var LabcName = me.getComponent('ReaBmsCenOrderDoc_LabcName');
		//订货方取cenorg的OrgNo及CName
		if (me.ISCURDEPT == false) {
			LabcName.setReadOnly(true);
			LabcName.setValue(JShell.REA.System.CENORG_NAME);
		} else {
			LabcName.setReadOnly(false);
			LabcName.setValue(deptName);
		}
		//由前台赋值LabID
		var labId = JShell.System.Cookie.get(JShell.System.Cookie.map.LABID);
		if (labId) {
			var LabID = me.getComponent('ReaBmsCenOrderDoc_LabID');
			LabID.setValue(labId);
		}
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var DeleteFlag = data.ReaBmsCenOrderDoc_DeleteFlag;
		if (DeleteFlag == "true" || DeleteFlag == true) DeleteFlag = '1';
		DeleteFlag = (DeleteFlag == '1' ? 1 : 0);
		data.ReaBmsCenOrderDoc_DeleteFlag = DeleteFlag;
		if (data.ReaBmsCenOrderDoc_DataAddTime) data.ReaBmsCenOrderDoc_DataAddTime = JcallShell.Date.toString(data.ReaBmsCenOrderDoc_DataAddTime,
			true);
		if (data.ReaBmsCenOrderDoc_ConfirmTime) data.ReaBmsCenOrderDoc_ConfirmTime = JcallShell.Date.toString(data.ReaBmsCenOrderDoc_ConfirmTime,
			true);
		if (data.ReaBmsCenOrderDoc_CheckTime) data.ReaBmsCenOrderDoc_CheckTime = JcallShell.Date.toString(data.ReaBmsCenOrderDoc_CheckTime,
			true);
		if (data.ReaBmsCenOrderDoc_ApprovalTime) data.ReaBmsCenOrderDoc_ApprovalTime = JcallShell.Date.toString(data.ReaBmsCenOrderDoc_ApprovalTime,
			true);
		if (data.ReaBmsCenOrderDoc_PayTime) data.ReaBmsCenOrderDoc_PayTime = JcallShell.Date.toString(data.ReaBmsCenOrderDoc_PayTime,
			true);

		if (data.ReaBmsCenOrderDoc_TotalPrice) {
			data.ReaBmsCenOrderDoc_TotalPrice = parseFloat(data.ReaBmsCenOrderDoc_TotalPrice);
		}
		var reg = new RegExp("<br />", "g");
		data.ReaBmsCenOrderDoc_Memo = data.ReaBmsCenOrderDoc_Memo.replace(reg, "\r\n");

		return data;
	},
	/**@description 获取保存提交数据*/
	getSaveParams: function() {
		var me = this;
		var entity = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if (!me.saveParams) {
			me.saveParams = {
				entity: entity.entity,
				"dtAddList": []
			}
		}
		me.saveParams.entity = entity.entity;
		if (me.formtype == 'edit') {
			me.saveParams.fields = entity.fields;
		}
		return me.saveParams;
	},
	/**@description 保存前的提交参数再次处理*/
	setSaveParams: function(params) {
		var me = this;
		if (!me.saveParams) {
			me.saveParams = {
				"entity": null,
				"dtAddList": []
			}
		}
		me.saveParams = params;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			DeptName: values.ReaBmsCenOrderDoc_DeptName,
			OrderDocNo: values.ReaBmsCenOrderDoc_OrderDocNo,
			LabcName: values.ReaBmsCenOrderDoc_LabcName,
			ReaServerLabcCode: values.ReaBmsCenOrderDoc_ReaServerLabcCode,
			CompanyName: values.ReaBmsCenOrderDoc_CompanyName,
			ReaServerCompCode: values.ReaBmsCenOrderDoc_ReaServerCompCode,
			ReaCompanyName: values.ReaBmsCenOrderDoc_ReaCompanyName,
			Status: values.ReaBmsCenOrderDoc_Status,
			UrgentFlag: values.ReaBmsCenOrderDoc_UrgentFlag,
			DeleteFlag: values.ReaBmsCenOrderDoc_DeleteFlag,
			ReaCompCode: values.ReaBmsCenOrderDoc_ReaCompCode
		};
		entity.Memo = values.ReaBmsCenOrderDoc_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');

		if (values.ReaBmsCenOrderDoc_DeptID) entity.DeptID = values.ReaBmsCenOrderDoc_DeptID;
		if (values.ReaBmsCenOrderDoc_LabID) entity.LabID = values.ReaBmsCenOrderDoc_LabID;
		if (values.ReaBmsCenOrderDoc_LabcID) entity.LabcID = values.ReaBmsCenOrderDoc_LabcID;
		if (values.ReaBmsCenOrderDoc_CompID) entity.CompID = values.ReaBmsCenOrderDoc_CompID;
		if (values.ReaBmsCenOrderDoc_ReaCompID) entity.ReaCompID = values.ReaBmsCenOrderDoc_ReaCompID;
		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate);
		var applyTime = curDateTime;
		if (values.ReaBmsCenOrderDoc_DataAddTime) {
			applyTime = values.ReaBmsCenOrderDoc_DataAddTime;
			if (JcallShell.Date.toServerDate(applyTime)) {
				entity.DataAddTime = JcallShell.Date.toServerDate(applyTime);
			}
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();

		var fields = [
			'Id', 'OrderDocNo', 'Memo', 'DeleteFlag', 'Status', 'UrgentFlag', 'DeptName', 'LabcName', 'ReaServerLabcCode',
			'CompID', 'CompanyName', 'ReaCompID', 'ReaCompanyName', 'ReaServerCompCode', 'ReaCompCode', 'DeptID'
		];
		entity.fields = fields.join(',');

		entity.entity.Id = values.ReaBmsCenOrderDoc_Id;
		return entity;
	},
	setCompReadOnlys: function(readOnly) {
		var me = this;
		var companyName = me.getComponent("ReaBmsCenOrderDoc_CompanyName");
		if (readOnly == undefined || readOnly == null) readOnly = true;
		companyName.setReadOnly(readOnly);
	}
});
