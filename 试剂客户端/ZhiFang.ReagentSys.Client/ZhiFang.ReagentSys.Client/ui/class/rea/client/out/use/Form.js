/**
 * 出库表单
 * @author longfc	
 * @version 2019-03-27
 */
Ext.define('Shell.class.rea.client.out.use.Form', {
	extend: 'Shell.class.rea.client.out.basic.Form',

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
	/**库房数据*/
	StorageData: [],
	/**是否按出库人权限出库 false否,TRUE是*/
	IsEmpOut: false,
	/**直接出库时是否需要出库确认,1是,0否*/
	IsCheck: '1',
	/**出库类型默认值*/
	defaluteOutType: '1',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		JShell.Action.delay(function() {
			me.initStorageData();
		}, null, 500);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建领用人组件*/
	createTaker: function() {
		var me = this;
		var obj = {
			fieldLabel: '领用人',
			name: 'ReaBmsOutDoc_TakerName',
			itemId: 'ReaBmsOutDoc_TakerName',
			emptyText: '必填项',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.out.user.CheckApp',
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
		};
		return obj;
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
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
			fieldLabel: '货品总额',
			name: 'ReaBmsOutDoc_TotalPrice',
			itemId: 'ReaBmsOutDoc_TotalPrice',
			emptyText: '必填项',
			allowBlank: false,
			readOnly: true,
			locked: true,
			xtype: 'numberfield',
			value: 0
		}, {
			fieldLabel: '库房选择',
			name: 'ReaBmsOutDoc_StorageID',
			itemId: 'ReaBmsOutDoc_StorageID',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			emptyText: '必填项',
			allowBlank: false,
			data: [],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.fireEvent('setDefaultStorage', com.getValue(), com.getRawValue());
					me.onStorageChange(newValue);
				}
			},
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '领用人id',
			name: 'ReaBmsOutDoc_TakerID',
			itemId: 'ReaBmsOutDoc_TakerID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push(me.createTaker());
		items.push({
			fieldLabel: '部门id',
			name: 'ReaBmsOutDoc_DeptID',
			itemId: 'ReaBmsOutDoc_DeptID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '确认人Id',
			name: 'ReaBmsOutDoc_ConfirmId',
			itemId: 'ReaBmsOutDoc_ConfirmId',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});

		var DeptObj = {
			fieldLabel: '使用部门',
			name: 'ReaBmsOutDoc_DeptName',
			itemId: 'ReaBmsOutDoc_DeptName',
			xtype: 'uxCheckTrigger',
			emptyText: '必填项',
			allowBlank: false,
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
		};
		var CheckObj = {
			fieldLabel: '确认人',
			name: 'ReaBmsOutDoc_ConfirmName',
			itemId: 'ReaBmsOutDoc_ConfirmName',
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
			DeptObj.colspan = 1;
			DeptObj.width = me.defaults.width * 1;
			items.push(DeptObj);
			items.push(CheckObj);
		} else if(me.formtype == 'add' && (me.IsCheck != '1' || me.IsCheck != 1)) {
			DeptObj.colspan = 2;
			DeptObj.width = me.defaults.width * 2;
			items.push(DeptObj);
		} else {
			DeptObj.colspan = 1;
			DeptObj.width = me.defaults.width * 1;
			items.push(DeptObj);
			items.push(CheckObj);
		}
		items.push({
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
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.enableControl(); //启用所有的操作功能	
		JShell.Action.delay(function() {
			me.initData();
		}, null, 500);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.disableControl();
	},
	initData: function() {
		var me = this;
		if(me.formtype == "add") {
			var TakerID = me.getComponent('ReaBmsOutDoc_TakerID');
			var TakerName = me.getComponent('ReaBmsOutDoc_TakerName');
			var DeptID = me.getComponent('ReaBmsOutDoc_DeptID');
			var DeptName = me.getComponent('ReaBmsOutDoc_DeptName');
			if(me.TakerObj && me.TakerObj.TakerId) {
				TakerID.setValue(me.TakerObj.TakerId);
				TakerName.setValue(me.TakerObj.TakerName);
				DeptID.setValue(me.TakerObj.DeptId);
				DeptName.setValue(me.TakerObj.DeptName);
			}
		}
		me.initStorageData(false);
	}
});