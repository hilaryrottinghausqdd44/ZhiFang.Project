/**
 * 出库库存货品选择列表
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.apply.QtyDtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.QtyDtlGrid',

	/**条码类型*/
	barcodeOperType: '7',
	/**表单选中的库房*/
	StorageObj: {},
	/**用户UI配置Key*/
	userUIKey: 'out.apply.QtyDtlGrid',
	/**用户UI配置Name*/
	userUIName: "出库库存货品选择列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//扫码只有一行数据时，自动添加到明细列表
		me.store.on({
			load: function(com, records, successful, eOpts) {
				var buttonsToolbar = me.getComponent('buttonsToolbar');
				var txtScanCode = buttonsToolbar.getComponent('txtScanCode');
				//"\s"匹配任何不可见字符，包括空格、制表符、换页符等等
				var barCode = txtScanCode.getValue().trim().replace(/\s+/g, '');
				if(records && records.length == 1 && barCode) {
					me.fireEvent('dbitemclick', me, records[0]);
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('checkchange', 'dbitemclick', 'NObarcode', 'dbselectclick', 'scanCodeClick');
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		//自定义按钮功能栏
		var items = [{
			xtype: 'label',
			text: '待选货品列表',
			hidden: true,
			style: "font-weight:bold;color:blue;",
			margin: '0 0 10 10'
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
							txtSearch.emptyText = '货品名称/货品编码/拼音字头/简称';
						}
						txtSearch.applyEmptyText();
						if(txtSearch.getValue()) me.onSearch();
					}
				}
			}
		}, {
			name: 'txtSearch',
			itemId: 'txtSearch',
			emptyText: '货品名称/货品编码/拼音字头/简称',
			width: 190,
			xtype: 'textfield',
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER) {
						var buttonsToolbar = me.getComponent("buttonsToolbar");
						var txtScanCode = buttonsToolbar.getComponent("txtScanCode");
						if(!me.StorageObj.StorageID) {
							var info = "请选择库房!";
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
		}, {
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
					me.onBarCodeClick(field, e);
				}
			}
		}, {
			xtype: 'checkboxfield',
			margin: '0 0 0 10',
			boxLabel: '开启近效期检测',
			name: 'testCheck',
			itemId: 'testCheck',
			isLocked:true,
			checked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.fireEvent('testClick', me, newValue);
				}
			}
		}];
		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.push({
			dataIndex: 'ReaBmsQtyDtl_SName',
			text: '简称',
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ProdOrgName',
			text: '厂家',
			sortable: true,
			width: 100,
			defaultRenderer: true
		});
		return columns;
	},
	getReaGoodsHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var GoodsClassType = buttonsToolbar.getComponent('GoodsClassType').getValue();
		var GoodsClass = buttonsToolbar.getComponent('GoodsClass').getValue();
		var cboSearch = buttonsToolbar.getComponent('cboSearch').getValue();
		var txtSearch = buttonsToolbar.getComponent('txtSearch').getValue();
	
		var reaGoodsHql = [];
		if(GoodsClass) {
			reaGoodsHql.push("reagoods.GoodsClass='" + GoodsClass + "'");
		}
		if(GoodsClassType) {
			reaGoodsHql.push("reagoods.GoodsClassType='" + GoodsClassType + "'");
		}
		if(txtSearch && cboSearch == "1") {
			// or reagoods.SName like '%"+txtSearch+"%'
			reaGoodsHql.push("(reagoods.PinYinZiTou like '%" + txtSearch.toUpperCase() + "%' or reagoods.CName like'%" + txtSearch + "%'" +
				" or reagoods.ReaGoodsNo like'%" + txtSearch + "%' or reagoods.SName like'%" + txtSearch + "%')");
		}
		if(reaGoodsHql && reaGoodsHql.length > 0) {
			reaGoodsHql = reaGoodsHql.join(" and ");
		} else {
			reaGoodsHql = "1=1";
		}
		return reaGoodsHql;
	},
	/**
	 * 货品扫码调用服务返回库存货品
	 * 只有一条库存记录数据时选择行出来
	 * */
	oneRecSelect: function(barcode, qtyDtlID) {
		var me = this;
		var records = me.store.data.items,
			len = records.length;
		//记录当次扫码操作
		var CurArr = me.getCurReaGoodsScanCodeList(barcode);
		records[0].set('ReaBmsQtyDtl_CurReaGoodsScanCodeList', CurArr);
		//ui默认选择一行(第一行)
		if(me.getStore().getCount() > 0) {
			me.getSelectionModel().select(0);
		}
		//不开启近效期时，只有一行数据，默认数据到出库明细中
		var NeareffectCheck = me.getNeareffectCheck();
		if(!NeareffectCheck.getValue()) {
			me.fireEvent('dbselectclick', me, records[0], barcode, qtyDtlID);
		}
	}
});