/**
 * 客户端供货单验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.set.StorageForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '验货单信息',
	width: 325,
	height: 220,

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 1 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 285,
		labelAlign: 'right'
	},
	/**新增/编辑/查看*/
	formtype: 'add',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onSave');
		me.width = me.width || 325;
		if(me.defaults.width < 285) me.defaults.width = 285;
		me.buttonToolbarItems=[];
		me.buttonToolbarItems.push('->',{			
			xtype: 'button',
			itemId: 'btnSave',
			iconCls: 'button-save',
			text: "确定",
			tooltip: "确定",
			handler: function(btn, e) {
				me.onSaveClick(btn, e);
			}
		});
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '库房选择',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaStorage_CName',
			itemId: 'ReaStorage_CName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			colspan: 1,
			width: me.defaults.width * 1,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.confirm.set.storage.CheckGrid', {
					resizable: false,
					listeners: {
						accept: function(p, record) {
							me.onReaStorageAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '库房ID',
			hidden: true,
			name: 'ReaStorage_Id',
			itemId: 'ReaStorage_Id'
		});
		items.push({
			fieldLabel: '货架选择',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaPlace_CName',
			itemId: 'ReaPlace_CName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			colspan: 1,
			width: me.defaults.width * 1,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.confirm.set.place.CheckGrid', {
					resizable: false,
					listeners: {
						accept: function(p, record) {
							me.onReaPlaceAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '货架ID',
			hidden: true,
			name: 'ReaPlace_Id',
			itemId: 'ReaPlace_Id'
		});
		items.push({
			fieldLabel: '类型选择',
			labelWidth: 65,
			colspan: 1,
			width: me.defaults.width * 1,
			xtype: 'uxSimpleComboBox',
			itemId: 'CboType',
			name: 'CboType',
			data: [
				["1", "当前勾选明细行设置为当前选择"],
				["2", "目标全部明细行设置为当前选择"],
				["3", "目标相同货品设置为当前选择"]
			],
			value: '1',
			listeners: {
				select: function(com, records, eOpts) {

				}
			}
		});
		return items;
	},
	/**库房选择*/
	onReaStorageAccept: function(record) {
		var me = this;
		var ComId = me.getComponent('ReaStorage_Id');
		var ComName = me.getComponent('ReaStorage_CName');

		ComId.setValue(record ? record.get('ReaStorage_Id') : '');
		ComName.setValue(record ? record.get('ReaStorage_CName') : '');
	},
	/**库房选择*/
	onReaPlaceAccept: function(record) {
		var me = this;
		var ComId = me.getComponent('ReaPlace_Id');
		var ComName = me.getComponent('ReaPlace_CName');

		ComId.setValue(record ? record.get('ReaPlace_Id') : '');
		ComName.setValue(record ? record.get('ReaPlace_CName') : '');
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			ReaStorageId: values.ReaStorage_Id,
			ReaStorageCName: values.ReaStorage_CName,
			ReaPlaceId: values.ReaPlace_Id,
			ReaPlaceCName: values.ReaPlace_CName,
			CboType: values.CboType
		};
		entity;
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;		
		if(!me.getForm().isValid()) return;		
		var entity =me.getAddParams();
		me.fireEvent('onSave',me,entity);
	}
});