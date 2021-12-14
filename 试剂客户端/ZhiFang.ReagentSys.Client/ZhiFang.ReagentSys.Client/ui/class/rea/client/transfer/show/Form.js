/**
 * 移库表单
 * @author longfc	
 * @version 2019-04-25
 */
Ext.define('Shell.class.rea.client.transfer.show.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '移库信息',
	width: 250,
	height: 390,
	/**内容周围距离*/
	bodyPadding: '10px 0px 0px 0px',
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	defaults: {
		labelWidth: 65,
		width: 180,
		labelAlign: 'right'
	},
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDocById?isPlanish=true',
	PK: null,
	/**移库总单状态Key*/
	ReaBmsTransferDocStatus: 'ReaBmsTransferDocStatus',
	/**带功能按钮栏*/
	hasButtontoolbar: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		JShell.REA.StatusList.getStatusList(me.ReaBmsTransferDocStatus, false, true, null);
		items.push({
			fieldLabel: '移库Id',
			name: 'ReaBmsTransferDoc_Id',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '移库单号',
			name: 'ReaBmsTransferDoc_TransferDocNo',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '单据状态',
			name: 'ReaBmsTransferDoc_Status',
			itemId: 'ReaBmsTransferDoc_Status',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.ReaBmsTransferDocStatus].List,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '登记时间',
			name: 'ReaBmsTransferDoc_DataAddTime',
			itemId: 'ReaBmsTransferDoc_DataAddTime',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '货品总额',
			name: 'ReaBmsTransferDoc_TotalPrice',
			itemId: 'ReaBmsTransferDoc_TotalPrice',
			readOnly: true,
			locked: true,
			xtype: 'numberfield'
		});
		items.push({
			fieldLabel: '申请人',
			name: 'ReaBmsTransferDoc_CreaterName',
			itemId: 'ReaBmsTransferDoc_CreaterName',
			hidden: false,
			readOnly: true,
			locked: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '领用人Id',
			name: 'ReaBmsTransferDoc_TakerID',
			itemId: 'ReaBmsTransferDoc_TakerID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '领用人',
			name: 'ReaBmsTransferDoc_TakerName',
			itemId: 'ReaBmsTransferDoc_TakerName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.user.CheckApp',
			colspan: 1,
			width: me.defaults.width * 1,
			classConfig: {
				title: '领用人选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					me.onUserAccept(record);
					p.close();
				}
			}
		}, {
			fieldLabel: '使用部门',
			name: 'ReaBmsTransferDoc_DeptName',
			itemId: 'ReaBmsTransferDoc_DeptName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.out.basic.CheckTree',
			classConfig: {
				title: '部门选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					if(record && record.get("tid") == 0) {
						JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
						return;
					}
					me.onDepAccept(record);
					p.close();
				}
			},
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			fieldLabel: '部门id',
			name: 'ReaBmsTransferDoc_DeptID',
			itemId: 'ReaBmsTransferDoc_DeptID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '审核人Id',
			name: 'ReaBmsTransferDoc_CheckID',
			itemId: 'ReaBmsTransferDoc_CheckID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '审核人',
			name: 'ReaBmsTransferDoc_CheckName',
			itemId: 'ReaBmsTransferDoc_CheckName',
			colspan: 1,
			width: me.defaults.width * 1,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.user.CheckApp',
			colspan: 1,
			width: me.defaults.width * 1,
			classConfig: {
				title: '审核人选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					var UseID = me.getComponent('ReaBmsTransferDoc_CheckID');
					var UseName = me.getComponent('ReaBmsTransferDoc_CheckName');
					UseName.setValue(record ? record.get('HREmployee_CName') : '');
					UseID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				}
			}
		}, {
			fieldLabel: '审核时间',
			name: 'ReaBmsTransferDoc_CheckTime',
			itemId: 'ReaBmsTransferDoc_CheckTime',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '源库房',
			name: 'ReaBmsTransferDoc_SStorageName',
			itemId: 'ReaBmsTransferDoc_SStorageName',
			readOnly: true,
			locked: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '源库房',
			name: 'ReaBmsTransferDoc_SStorageID',
			itemId: 'ReaBmsTransferDoc_SStorageID',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			hidden: true,
			readOnly: true,
			locked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					//me.fireEvent('setDefaultStorage', com.getValue(), com.getRawValue());
				}
			},
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '目的库房',
			name: 'ReaBmsTransferDoc_DStorageName',
			itemId: 'ReaBmsTransferDoc_DStorageName',
			readOnly: true,
			locked: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '目的库房',
			name: 'ReaBmsTransferDoc_DStorageID',
			itemId: 'ReaBmsTransferDoc_DStorageID',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			hidden: true,
			readOnly: true,
			locked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					//me.fireEvent('setDefaultDStorage', com.getValue(), com.getRawValue());
				}
			},
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			height: 40,
			fieldLabel: '审核备注',
			name: 'ReaBmsTransferDoc_CheckMemo',
			xtype: 'textarea',
			colspan: 2,
			width: me.defaults.width * 2
		});
		items.push({
			height: 40,
			fieldLabel: '移库说明',
			name: 'ReaBmsTransferDoc_Memo',
			xtype: 'textarea',
			colspan: 2,
			width: me.defaults.width * 2
		});
		return items;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data.ReaBmsTransferDoc_DataAddTime = JShell.Date.getDate(data.ReaBmsTransferDoc_DataAddTime);
		data.ReaBmsTransferDoc_CheckTime = JShell.Date.getDate(data.ReaBmsTransferDoc_CheckTime);
		return data;
	}
});