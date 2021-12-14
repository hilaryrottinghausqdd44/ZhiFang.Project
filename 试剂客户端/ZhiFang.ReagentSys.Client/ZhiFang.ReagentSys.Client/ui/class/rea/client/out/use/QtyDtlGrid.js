/**
 * 待选货品列表(中间列表,库存表)
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.use.QtyDtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.QtyDtlGrid',

	/**表单选中的库房*/
	StorageObj: {},
	/**货品条码操作类型*/
	barcodeOperType: '7',
	/**用户UI配置Key*/
	userUIKey: 'out.use.QtyDtlGrid',
	/**用户UI配置Name*/
	userUIName: "使用出库库存货品列表",

	afterRender: function() {
		var me = this;
		// 保证用户开启UI配置后，在开始是入库批次列的隐藏与isMergeInDocNo保持一致
		me.isVisibleColumn('ReaBmsQtyDtl_InDocNo',me.isMergeInDocNo); 
		me.callParent(arguments);
		//扫码只有一行数据时，自动添加到明细列表
		me.store.on({
			load: function(com, records, successful, eOpts) {
				var buttonsToolbar = me.getComponent('buttonsToolbar');
				var txtScanCode = buttonsToolbar.getComponent('txtScanCode');
				//"\s"匹配任何不可见字符，包括空格、制表符、换页符等等
				var barCode = txtScanCode.getValue().trim().replace(/\s+/g, '');
				if (records && records.length == 1 && barCode) {
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
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		items.push({
			xtype: 'checkboxfield',
			margin: '0 0 0 0',
			// boxLabel: '扫码时相同货品合并',
			boxLabel: '供应批次合并',
			tooltip: '供应批次和库房货架及供货商货品批号和包装单位都相同的库存货品,自动合并为一行显示',
			name: 'isMergeInDocNo',
			itemId: 'isMergeInDocNo',
			// isHidden: true,
			checked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.isMergeInDocNo = newValue;
					// 这里需要重新加载数据
					me.onSearch();
					// 合并就隐藏‘入库批次’，不合并就显示‘入库批次’
					me.isVisibleColumn('ReaBmsQtyDtl_InDocNo',oldValue);
					
				}
			}
		}, {
			xtype: 'checkboxfield',
			boxLabel: '开启近效期',
			margin: '0 0 0 5',
			name: 'testCheck',
			itemId: 'testCheck',
			isLocked:true,
			checked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.fireEvent('testClick', me, newValue);
				}
			}
		});
		return items;
	},
	/**
	 * 勾选供应批次合并时，隐藏‘入库批次’这个列(dataIndex:ReaBmsQtyDtl_InDocNo)
	 * @param {string} colDataIndex 要隐藏列的dataIndex的值
	 * @param {boolean} isVisible true：可见；false：隐藏 
	 * */
	 isVisibleColumn: function(colDataIndex,isVisible) {
		 var me = this;
		 Ext.Array.each(me.columns, function(column, index, countriesItSelf) {
		 	if(column.dataIndex == colDataIndex) {
		 		me.columns[index].setVisible(isVisible);
		 		// me.columns[index].disable = 
		 	}
		 });
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
		if (me.getStore().getCount() > 0) {
			me.getSelectionModel().select(0);
		}
		//不开启近效期时，只有一行数据，默认数据到出库明细中
		var NeareffectCheck = me.getNeareffectCheck();
		if (!NeareffectCheck.getValue()) {
			me.fireEvent('dbselectclick', me, records[0], barcode, qtyDtlID);
		}else{
			me.fireEvent('scanCodeDate', me, records[0], barcode, qtyDtlID);
		}
	}
	
});
