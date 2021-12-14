/**
 * 待选货品列表(中间列表,库存表)
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.transfer.apply.QtyDtlGrid', {
	extend: 'Shell.class.rea.client.transfer.QtyDtlGrid',

	/**用户UI配置Key*/
	userUIKey: 'transfer.apply.QtyDtlGrid',
	/**用户UI配置Name*/
	userUIName: "移库申请待选库存货品列表",

	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [{
			xtype: 'label',
			text: '源货品列表',
			hidden: true,
			style: "font-weight:bold;color:blue;",
			margin: '0 0 5 5'
		}, 'refresh', '-', {
			emptyText: '一级分类',
			labelWidth: 0,
			width: 95,
			fieldLabel: '',
			itemId: 'GoodsClass',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '一级分类',
				ClassType: "GoodsClass"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClass');
				}
			}
		}, {
			emptyText: '二级分类',
			labelWidth: 0,
			width: 95,
			fieldLabel: '',
			itemId: 'GoodsClassType',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '二级分类',
				ClassType: "GoodsClassType"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClassType');
				}
			}
		}, {
			xtype: 'uxSimpleComboBox',
			itemId: 'cboSearch',
			margin: '0 0 0 5',
			emptyText: '检索条件选择',
			fieldLabel: '检索',
			labelWidth: 35,
			width: 130,
			value: "1",
			data: [
				["1", "按机构货品"],
				["2", "按货品批号"]
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue) {
						var buttonsToolbar = me.getComponent('buttonsToolbar');
						var txtSearch = buttonsToolbar.getComponent('txtSearch');
						if(newValue == "2") {
							txtSearch.emptyText = '货品批号';
						} else {
							txtSearch.emptyText = '货品名称/货品编码/拼音字头';
						}
						txtSearch.applyEmptyText();
						if(txtSearch.getValue()) me.onSearch();
					}
				}
			}
		}, {
			name: 'txtSearch',
			itemId: 'txtSearch',
			emptyText: '货品名称/货品编码/拼音字头',
			width: 160,
			xtype: 'textfield',
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER) {
						var buttonsToolbar = me.getComponent("buttonsToolbar");
						var txtScanCode = buttonsToolbar.getComponent("txtScanCode");
						if(!me.SStorageObj.StorageID) {
							var info = "源库房不能为空!";
							JShell.Msg.alert(info, null, 2000);
							me.store.removeAll();
							me.fireEvent('nodata', me);
							return;
						}
						txtScanCode.setValue('');
						me.onSearch();
					}
				}
			}
		}, '-', {
			name: 'txtScanCode',
			itemId: 'txtScanCode',
			emptyText: '按货品条码扫码',
			margin: '0 0 0 5',
			width: 180,
			labelAlign: 'right',
			xtype: 'textfield',
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			hidden: true,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER) {
						//防止扫码时,自动出现触发多个回车事件
						JShell.Action.delay(function() {
							if(!me.SStorageObj.StorageID) {
								JShell.Msg.alert('源库房不能为空!', null, 2000);
								return;
							}
							var buttonsToolbar = me.getComponent("buttonsToolbar");
							var txtSearch = buttonsToolbar.getComponent("txtSearch");
							txtSearch.setValue('');
							if(!field.getValue()) {
								var info = "请输入条码号!";
								JShell.Msg.alert(info, null, 2000);
								me.store.removeAll();
								return;
							}
							me.onScanCode(field.getValue());
						}, null, 30);
					}
				}
			}
		}, {
			xtype: 'checkboxfield',
			margin: '0 0 0 10',
			boxLabel: '开启近效期',
			name: 'testCheck',
			itemId: 'testCheck',
			checked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.fireEvent('testClick', me, newValue);
				}
			}
		}];
		return items;
	}
});