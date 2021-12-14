/**
 * 出库申请表单
 * @author longfc	
 * @version 2019-03-27
 */
Ext.define('Shell.class.rea.client.out.apply.Form', {
	extend: 'Shell.class.rea.client.out.basic.Form',

	title: '出库信息',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsOutDoc',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsOutDocByField',
	PK: null,
	/**权限库房数据*/
	StorageData: [],
	/**是否按出库人权限出库 false否,TRUE是*/
	IsEmpOut: false,
	/**出库类型默认值*/
	defaluteOutType: '1',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		JShell.Action.delay(function() {
			me.initStorageData();
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

		items.push({
			fieldLabel: 'Id',
			name: 'ReaBmsOutDoc_Id',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '出库单号',
			name: 'ReaBmsOutDoc_OutDocNo',
			hidden: true,
			colspan: 2,
			width: me.defaults.width * 2
		});
		items.push({
			fieldLabel: '库房',
			name: 'ReaBmsOutDoc_StorageID',
			itemId: 'ReaBmsOutDoc_StorageID',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: [],
			emptyText: '必填项',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.fireEvent('setDefaultStorage', newValue, com.getRawValue());
				}
			}
		}, {
			fieldLabel: '领用人',
			name: 'ReaBmsOutDoc_TakerName',
			itemId: 'ReaBmsOutDoc_TakerName',
			emptyText: '必填项',
			allowBlank: false,
			readOnly: true,
			locked: true,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.out.user.CheckApp',
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
			fieldLabel: '领用人id',
			name: 'ReaBmsOutDoc_TakerID',
			itemId: 'ReaBmsOutDoc_TakerID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '领用部门',
			name: 'ReaBmsOutDoc_DeptName',
			itemId: 'ReaBmsOutDoc_DeptName',
			xtype: 'uxCheckTrigger',
			/**
			 * 修改人: longfc
			 * 修改日期:2020-01-15
			 * 修改需求说明:四川大家出库申请，领用部门允许选择
			 */
			/* readOnly: true,
			locked: true, */
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
		}, {
			fieldLabel: '部门id',
			name: 'ReaBmsOutDoc_DeptID',
			itemId: 'ReaBmsOutDoc_DeptID',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		var StatusList = JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List;
		StatusList = me.removeSomeStatusList();
		items.push({
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
			locked: true
		}, {
			fieldLabel: '货品总额',
			name: 'ReaBmsOutDoc_TotalPrice',
			itemId: 'ReaBmsOutDoc_TotalPrice',
			readOnly: true,
			locked: true,
			xtype: 'numberfield',
			value: 0
		}, {
			fieldLabel: '单据状态',
			name: 'ReaBmsOutDoc_Status',
			itemId: 'ReaBmsOutDoc_Status',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: StatusList,
			colspan: 2,
			hidden: false,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true,
			value: ''
		});

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
	/**获取库房权限关系的Hql*/
	getStorageLinkHql: function(takerId) {
		var me = this;
		var operType = "3";
		var linkHql = "reauserstoragelink.OperType=" + operType;
		linkHql += ' and reauserstoragelink.OperID=' + takerId;
		return linkHql;
	},
	/**获取库房权限关系的Url*/
	getStorageLinkUrl: function(takerId) {
		var me = this;
		var params = me.callParent(arguments);
		params.push("operType=3");
		return params;
	},
	/**@overwrite 获取修改的数据*/
	getEditFields: function() {
		var me = this;
		var fields = "Id,OperDate,DeptID,DeptName,TakerID,TakerName,OutDocNo,TotalPrice,Memo,DataUpdateTime,DataAddTime";
		return fields;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		JShell.Action.delay(function() {
			me.initData();
		}, null, 500);
	},
	initData: function() {
		var me = this;
		if(me.formtype == "add") {
			var username = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
			var usernId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
			var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
			var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
			me.getComponent('ReaBmsOutDoc_TakerID').setValue(usernId);
			me.getComponent('ReaBmsOutDoc_TakerName').setValue(username);
			me.getComponent('ReaBmsOutDoc_DeptID').setValue(deptId);
			me.getComponent('ReaBmsOutDoc_DeptName').setValue(deptName);
			me.getComponent('ReaBmsOutDoc_OutType').setValue("1");
		}
		me.initStorageData();
	},
	/**初始化库房信息*/
	initStorageData: function(isRefresh) {
		var me = this;
		if(!me.StorageData) me.StorageData = [];
		//绑定数据
		if(isRefresh == true) me.StorageData = [];
		var empId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || "";
		me.loadJurisdiction(empId, isRefresh);
	}
});