/**
 * 退供应商出库表单
 * @author longfc	
 * @version 2019-03-27
 */
Ext.define('Shell.class.rea.client.out.retreatsuppliers.Form', {
	extend: 'Shell.class.rea.client.out.basic.Form',
	title: '退供应商出库信息',

	width: 250,
	height: 390,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsOutDocById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsOutDoc',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsOutDocByField',
	/**获取获取库房服务路径*/
	selectStorageUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaStorageByHQL?isPlanish=true',
	/**获取获取库房货架权限服务路径*/
	selectStorageLinkUrl: '/ReaManageService.svc/RS_UDTO_SearchListByStorageAndLinHQL?isPlanish=true',

	PK: null,
	TakerObj: {},
	/**权限库房数据*/
	StorageData: [],
	/**是否按出库人权限出库 false否,TRUE是*/
	IsEmpOut: false,
	/**直接出库时是否需要出库确认,1是,0否*/
	IsCheck: '1',
	/**出库类型*/
	defaluteOutType: '4',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		JShell.Action.delay(function() {
			me.initData();
		}, null, 500);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		var StatusList = JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List;
		StatusList = me.removeSomeStatusList();

		items.push({
			fieldLabel: 'Id',
			name: 'ReaBmsOutDoc_Id',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '出库单号',
			name: 'ReaBmsOutDoc_OutDocNo',
			colspan: 2,
			width: me.defaults.width * 2
		},{
			fieldLabel: '货品总额',
			name: 'ReaBmsOutDoc_TotalPrice',
			itemId: 'ReaBmsOutDoc_TotalPrice',
			emptyText: '必填项',
			allowBlank: false,
			readOnly: true,
			locked: true,
			xtype: 'numberfield',
			value: 0,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '单据状态',
			name: 'ReaBmsOutDoc_Status',
			itemId: 'ReaBmsOutDoc_Status',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: StatusList,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			value: ''
		}, {
			fieldLabel: '出库类型',
			emptyText: '必填项',
			name: 'ReaBmsOutDoc_OutType',
			itemId: 'ReaBmsOutDoc_OutType',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.ReaBmsOutDocOutType].List,
			colspan: 1,
			hidden: false,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			value: me.defaluteOutType
		}, {
			fieldLabel: '领用人id',
			name: 'ReaBmsOutDoc_TakerID',
			itemId: 'ReaBmsOutDoc_TakerID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		var TakerObj = {
			fieldLabel: '操作人',
			name: 'ReaBmsOutDoc_TakerName',
			itemId: 'ReaBmsOutDoc_TakerName',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.user.CheckApp',
			colspan: 1,
			width: me.defaults.width * 1,
			classConfig: {
				title: '操作人选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					me.onUserAccept(record);
					p.close();
				}
			}
		};
		var StorageObj = {
			fieldLabel: '库房',
			name: 'ReaBmsOutDoc_StorageID',
			itemId: 'ReaBmsOutDoc_StorageID',
			xtype: 'uxSimpleComboBox',
			value: '',
			hasStyle: true,
			emptyText: '必填项',
			allowBlank: false,
			//			data:[],readOnly: true,locked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.fireEvent('setDefaultStorage', newValue, com.getRawValue());
				}
			},
			colspan: 1,
			width: me.defaults.width * 1
		};
		var CheckObj = {
			fieldLabel: '确认人',
			name: 'ReaBmsOutDoc_ConfirmName',
			itemId: 'ReaBmsOutDoc_ConfirmName',
			emptyText: '必填项',
			allowBlank: false,
			//			readOnly: true,locked: true,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.user.CheckApp',
			colspan: 1,
			width: me.defaults.width * 1,
			classConfig: {
				title: '确认人选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					me.onConfirmAccept(record);
					p.close();
				}
			}
		};
		if(me.IsCheck == '1' || me.IsCheck == 1) { //需要审核，不允许为空
			CheckObj.emptyText = '必填项';
			CheckObj.allowBlank = false;
		}
		if(me.formtype == 'add' && (me.IsCheck == '1' || me.IsCheck == 1)) {
			items.push(TakerObj);
			items.push(StorageObj);
			items.push(CheckObj);

		} else if(me.formtype == 'add' && (me.IsCheck != '1' || me.IsCheck != 1)) {
			items.push(TakerObj);
			StorageObj.colspan = 2;
			StorageObj.width = me.defaults.width * 1;
			items.push(StorageObj);
		} else {
			items.push(TakerObj);
			items.push(StorageObj);
			items.push(CheckObj);
		}

		items.push({
			fieldLabel: '确认人ID',
			name: 'ReaBmsOutDoc_ConfirmId',
			itemId: 'ReaBmsOutDoc_ConfirmId',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			height: 50,
			fieldLabel: '出库说明',
			emptyText: '出库说明',
			name: 'ReaBmsOutDoc_Memo',
			xtype: 'textarea',
			colspan: 4,
			width: me.defaults.width * 4
		});
		return items;
	},
	/**获取库房权限关系的Url*/
	getStorageLinkUrl: function(takerId) {
		var me = this;
		var params = me.callParent(arguments);
		params.push("operType=1");
		return params;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
		var entity = me.callParent(arguments);
		return entity;
	},
	/**重写原因，没有部门概念*/
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.enableControl(); //启用所有的操作功能	
		JShell.Action.delay(function() {
			me.initData();
		}, null, 500);
	},
	initData: function() {
		var me = this;
		if(me.formtype == "add") {
			var Sysdate = JcallShell.System.Date.getDate();
			var value = JcallShell.Date.toString(Sysdate, true);
			var TakerID = me.getComponent('ReaBmsOutDoc_TakerID');
			var TakerName = me.getComponent('ReaBmsOutDoc_TakerName');
			TakerID.setValue(me.TakerObj.TakerId);
			TakerName.setValue(me.TakerObj.TakerName);
		}
		if(me.TakerObj.TakerId) {
			if(me.IsEmpOut) {
				me.loadJurisdiction(me.TakerObj.TakerId, true);
			} else {
				//不按领用人过滤权限
				me.loadAllJurisdiction(true);
			}
		}
	},
	/**领用人选择*/
	onUserAccept: function(record) {
		var me = this;
		var UseID = me.getComponent('ReaBmsOutDoc_TakerID');
		var UseName = me.getComponent('ReaBmsOutDoc_TakerName');
		UseName.setValue(record ? record.get('HREmployee_CName') : '');
		UseID.setValue(record ? record.get('HREmployee_Id') : '');
		if(me.IsEmpOut) {
			me.loadJurisdiction(record.get('HREmployee_Id'), true);
		} else {
			//不按领用人过滤权限
			me.loadAllJurisdiction(true);
		}
	},
	/**库房选择后领用人处理*/
	onStorageChange: function(newValue) {

	}
});