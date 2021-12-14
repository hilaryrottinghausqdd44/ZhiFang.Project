/**
 * 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.basic.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '供货信息',
	formtype: 'show',
	width: 680,
	height: 155,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocById?isPlanish=true',

	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	buttonDock: "top",
	/**内容周围距离*/
	bodyPadding: '8px 5px 0px 0px',

	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 175,
		labelAlign: 'right'
	},

	/**供货单数据来源默认值*/
	defaultSourceValue: "",
	/**订货方的所属机构类型值:0:供货商;1:订货方;*/
	LabOrgTypeValue:"",
	/**供货商的所属机构类型值:0:供货方;1:订货方;*/
	CompOrgTypeValue:"",
	/**客户端供货单及明细状态Key*/
	StatusKey: "ReaBmsCenSaleDocAndDtlStatus",
	/**客户端供货单数据来源Key*/
	SourceKey: "ReaBmsCenSaleDocSource",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 175) me.defaults.width = 175;
		
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.SourceKey, false, true, null);
		
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//订货方
		items.push({
			fieldLabel: '订货方',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaBmsCenSaleDoc_LabcName',
			itemId: 'ReaBmsCenSaleDoc_LabcName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: me.LabOrgTypeValue=="1"?true:false,//订货方的所属机构类型为订货方时,不可以选择
			locked: me.LabOrgTypeValue=="1"?true:false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.reacenorg.CheckTree', {
					/**是否显示根节点*/
					rootVisible: false,
					/**机构类型*/
					OrgType:me.LabOrgTypeValue,
					resizable: false,
					listeners: {
						accept: function(p, record) {
							if(record && record.get("tid") == 0) {
								JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onLabcAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '订货方主键ID',
			hidden: true,
			name: 'ReaBmsCenSaleDoc_LabcID',
			itemId: 'ReaBmsCenSaleDoc_LabcID'
		}, {
			fieldLabel: '实验室平台机构编码',
			hidden: true,
			name: 'ReaBmsCenSaleDoc_ReaServerLabcCode',
			itemId: 'ReaBmsCenSaleDoc_ReaServerLabcCode'
		}, {
			fieldLabel: '实验室机构编码',
			hidden: true,
			name: 'ReaBmsCenSaleDoc_ReaLabcCode',
			itemId: 'ReaBmsCenSaleDoc_ReaLabcCode'
		});
		//供货商
		items.push({
			fieldLabel: '供货商',
			emptyText: '供货商选择',
			//allowBlank: false,
			name: 'ReaBmsCenSaleDoc_CompanyName',
			itemId: 'ReaBmsCenSaleDoc_CompanyName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: me.LabOrgTypeValue=="1"?true:false,//订货方的所属机构类型为订货方时,不可以选择
			locked: me.LabOrgTypeValue=="1"?true:false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.reacenorg.CheckTree', {
					/**是否显示根节点*/
					rootVisible: false,
					/**机构类型*/
					OrgType: me.CompOrgTypeValue,
					resizable: false,
					listeners: {
						accept: function(p, record) {
							if(record && record.get("tid") == 0) {
								JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
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
			name: 'ReaBmsCenSaleDoc_CompID',
			itemId: 'ReaBmsCenSaleDoc_CompID'
		}, {
			fieldLabel: '供应商机平台构码',
			hidden: true,
			name: 'ReaBmsCenSaleDoc_ReaServerCompCode',
			itemId: 'ReaBmsCenSaleDoc_ReaServerCompCode'
		}, {
			fieldLabel: '供应商机构码',
			hidden: true,
			name: 'ReaBmsCenSaleDoc_ReaCompCode',
			itemId: 'ReaBmsCenSaleDoc_ReaCompCode'
		});
		//冗余,本地供应商信息
		items.push({
			fieldLabel: '本地供货商主键ID',
			hidden: true,
			name: 'ReaBmsCenSaleDoc_ReaCompID',
			itemId: 'ReaBmsCenSaleDoc_ReaCompID'
		}, {
			fieldLabel: '本地供货商名称',
			hidden: true,
			name: 'ReaBmsCenSaleDoc_ReaCompanyName',
			itemId: 'ReaBmsCenSaleDoc_ReaCompanyName'
		});

		//紧急标志
		items.push({
			fieldLabel: '紧急标志',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsCenSaleDoc_UrgentFlag',
			itemId: 'ReaBmsCenSaleDoc_UrgentFlag',
			data: JShell.REA.Enum.getComboboxList('BmsCenOrderDoc_UrgentFlag'),
			colspan: 1,
			width: me.defaults.width * 1,
			allowBlank: false,
			value: '0'
		});
		//所属部门
		items.push({
			fieldLabel: '所属部门',
			name: 'ReaBmsCenSaleDoc_DeptName',
			itemId: 'ReaBmsCenSaleDoc_DeptName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//数据来源
		items.push({
			fieldLabel: '数据来源',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsCenSaleDoc_Source',
			itemId: 'ReaBmsCenSaleDoc_Source',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.SourceKey].List,
			value: me.defaultSourceValue,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//供货单号
		items.push({
			fieldLabel: '供货单号',
			name: 'ReaBmsCenSaleDoc_SaleDocNo',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//订货单号
		items.push({
			fieldLabel: '订货单号',
			name: 'ReaBmsCenSaleDoc_OrderDocNo',
			hidden:true
		});

		//发票号
		items.push({
			xtype: 'textarea',
			fieldLabel: '发票号',
			name: 'ReaBmsCenSaleDoc_InvoiceNo',
			itemId: 'ReaBmsCenSaleDoc_InvoiceNo',
			colspan: 2,
			width: me.defaults.width * 2,
			height: 20
		});

		//操作日期
		items.push({
			xtype: 'datefield',
			fieldLabel: '供货日期',
			format: 'Y-m-d',
			name: 'ReaBmsCenSaleDoc_DataAddTime',
			itemId: 'ReaBmsCenSaleDoc_DataAddTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//操作者
		items.push({
			fieldLabel: '操作人',
			name: 'ReaBmsCenSaleDoc_UserName',
			itemId: 'ReaBmsCenSaleDoc_UserName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '操作人员ID',
			hidden: true,
			name: 'ReaBmsCenSaleDoc_UserID',
			itemId: 'ReaBmsCenSaleDoc_UserID'
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsCenSaleDoc_Id',
			hidden: true,
			type: 'key'
		});
		//LabID
		items.push({
			fieldLabel: 'LabID',
			hidden: true,
			name: 'ReaBmsCenSaleDoc_LabID',
			itemId: 'ReaBmsCenSaleDoc_LabID'
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsCenSaleDoc_Memo',
			itemId: 'ReaBmsCenSaleDoc_Memo',
			colspan: 4,
			width: me.defaults.width * 4,
			height: 40
		});

		//单据状态
		items.push({
			fieldLabel: '单据状态',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsCenSaleDoc_Status',
			itemId: 'ReaBmsCenSaleDoc_Status',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.StatusKey].List,
			colspan: 1,
			width: me.defaults.width * 1,
			//allowBlank: false,
			readOnly: true,
			locked: true
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '审核人员',
			name: 'ReaBmsCenSaleDoc_Checker',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			xtype: 'displayfield',
			fieldLabel: '审核时间',
			name: 'ReaBmsCenSaleDoc_CheckTime',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '总价',
			itemId: 'ReaBmsCenSaleDoc_TotalPrice',
			name: 'ReaBmsCenSaleDoc_TotalPrice',
			colspan: 1,
			width: me.defaults.width * 1
		});

		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(data.ReaBmsCenSaleDoc_DataAddTime) data.ReaBmsCenSaleDoc_DataAddTime = JcallShell.Date.toString(data.ReaBmsCenSaleDoc_DataAddTime, true);
		if(data.ReaBmsCenSaleDoc_CheckTime) data.ReaBmsCenSaleDoc_CheckTime = JcallShell.Date.toString(data.ReaBmsCenSaleDoc_CheckTime, true);

		var reg = new RegExp("<br />", "g");
		data.ReaBmsCenSaleDoc_Memo = data.ReaBmsCenSaleDoc_Memo.replace(reg, "\r\n");
		return data;
	}
});