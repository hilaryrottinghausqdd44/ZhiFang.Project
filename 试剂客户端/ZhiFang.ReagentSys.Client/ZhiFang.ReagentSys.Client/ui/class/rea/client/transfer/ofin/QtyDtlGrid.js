/**
 * 入库移库
 * @author longfc
 * @version 2019-03-28
 */
Ext.define('Shell.class.rea.client.transfer.ofin.QtyDtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '库存货品列表',
	width: 800,
	height: 500,
	/**查询数据*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlOfQtyGEZeroByJoinHql?isPlanish=true',

	defaultOrderBy: [{
		property: 'ReaBmsQtyDtl_InvalidDate',
		direction: 'ASC'
	}],
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: false,
	/**默认加载数据*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 5000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	defaultDisableControl: false,
	selModel: {
		injectCheckbox: 0,
		mode: "MULTI", //”SINGLE”/”SIMPLE”/”MULTI” 
		checkOnly: true //只能通过checkbox选择 
	},
	//	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**用户UI配置Key*/
	userUIKey: 'transfer.ofin.QtyDtlGrid',
	/**用户UI配置Name*/
	userUIName: "移库待选库存货品列表",
	/**当前选择的入库主单Id*/
	InDocID: null,
	/**当前选择的入库主单号*/
	InDocNo: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('checkchange', 'onConfirmTransfer');
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
		var columns = [{
			xtype: 'actioncolumn',
			text: '移除',
			align: 'center',
			width: 35,
			sortable: false,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.store.remove(rec);
				}
			}]
		}, {
			dataIndex: 'ReaBmsQtyDtl_InDocNo',
			text: '入库批次号',
			hidden: true,
			sortable: true,
			width: 100,
			minWidth: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_InDtlID',
			text: '入库明细ID',
			hidden: true,
			sortable: true,
			width: 100,
			minWidth: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_StorageID',
			text: '源库房ID',
			hidden: true,
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_StorageName',
			text: '源库房',
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_PlaceID',
			text: '源货架ID',
			hidden: true,
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_PlaceName',
			text: '源货架',
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_DStorageID',
			text: '目的库房Id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_DStorageName',
			text: '目的库房',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_DPlaceID',
			text: '目的货架Id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_DPlaceName',
			text: '目的货架',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsID',
			text: '货品ID',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_BarCodeType',
			text: '条码类型',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaGoodsNo',
			text: '货品编码',
			sortable: true,
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			sortable: true,
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsNo',
			text: '平台编码',
			hidden: true,
			sortable: true,
			width: 100,
			minWidth: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			sortable: true,
			width: 100,
			minWidth: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsName',
			text: '货品名称',
			sortable: true,
			width: 120,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsQtyDtl_BarCodeType");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"')>=0)value=value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsUnit',
			text: '单位',
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_UnitMemo',
			text: '规格',
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsQty',
			text: '库存量',
			sortable: true,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_SumCurrentQty',
			text: '剩余总库存',
			sortable: false,
			hidden: true,
			xtype: 'numbercolumn',
			format: '0.00',
			width: 80,
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				meta.tdAttr = 'data-qtip="<b>货品扫码时,如果存在多条库存记录系统将(库房货架及供货商货品批号和包装单位)都相同的库存货品,自动合并为一行显示</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_Price',
			text: '单价',
			sortable: true,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotNo',
			text: '批号',
			sortable: true,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_InvalidDate',
			text: '效期',
			sortable: true,
			width: 85,
			isDate: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_RegisterNo',
			text: '注册证号',
			sortable: true,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Id',
			text: '主键',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_TaxRate',
			text: 'TaxRate',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CompGoodsLinkID',
			text: 'CompGoodsLinkID',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaCompanyID',
			text: '本地供应商',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CompanyName',
			text: '供应商',
			sortable: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaServerCompCode',
			text: 'ReaServerCompCode',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaCompCode',
			text: '供货方编码',
			hideable: false,
			sortable: true,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ProdDate',
			text: 'ProdDate',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsSerial',
			text: 'GoodsSerial',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotSerial',
			text: 'LotSerial',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_SysLotSerial',
			text: 'SysLotSerial',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotQRCode',
			text: '二维条码号',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsSort',
			text: '货品序号',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsLotID',
			text: '货品批号ID',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}];

		return columns;
	},
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
			hidden: true,
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
			hidden: true,
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
						me.onSearch();
					}
				}
			}
		}, {
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-del',
			text: '移除选中货品',
			tooltip: '移除选择的库存货品',
			handler: function() {
				me.onRemoveSelects();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-save',
			text: '确认移库',
			tooltip: '确认移库',
			handler: function() {
				me.onConfirm();
			}
		}];
		return items;
	},
	/**
	 * 显示或隐藏"剩余总库存"列
	 * 货品扫码时,显示"剩余总库存"列;其他条件检索库存货品时,隐藏"剩余总库存"列
	 * */
	hiddenColumns: function(isHidden) {
		var me = this;
		var counts = 0;
		Ext.Array.each(me.columns, function(column, index, countriesItSelf) {
			if(counts == 1) return false;
			if(column.dataIndex == "ReaBmsQtyDtl_SumCurrentQty") {
				if(isHidden == true) me.columns[index].hide();
				else me.columns[index].show();
				counts = counts + 1;
			}
		});
	},
	/**获取入库明细条件*/
	getInDtlHql: function() {
		var me = this;
		var where = [];
		if(me.InDocNo) {
			where.push("reabmsindtl.InDocNo='" + me.InDocNo + "'");
		}
		if(me.InDocID) {
			where.push("reabmsindtl.InDocID=" + me.InDocID + "");
		}
		return where.join(" and ");
	},
	getQtyHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;

		var qtydtlHql = "reabmsqtydtl.Visible=1 and reabmsqtydtl.GoodsQty>0";
		if(me.InDocNo)
			qtydtlHql = qtydtlHql + " and reabmsqtydtl.InDocNo='" + me.InDocNo + "'";
		var cboSearch = buttonsToolbar.getComponent('cboSearch').getValue();
		var txtSearch = buttonsToolbar.getComponent('txtSearch').getValue();
		if(txtSearch && cboSearch == "2") {
			qtydtlHql += " and (reabmsqtydtl.LotNo like '%" + txtSearch + "')";
		}
		return qtydtlHql;
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
			reaGoodsHql.push("(reagoods.PinYinZiTou like '%" + txtSearch.toUpperCase() + "%' or reagoods.CName like'%" +
				txtSearch + "%'" +
				" or reagoods.ReaGoodsNo like'%" + txtSearch + "%')");
		}
		if(reaGoodsHql && reaGoodsHql.length > 0) {
			reaGoodsHql = reaGoodsHql.join(" and ");
		} else {
			reaGoodsHql = "";
		}
		return reaGoodsHql;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		if(where) {
			url += '&where=' + JShell.String.encode(where);
		}
		var reaGoodsHql = me.getReaGoodsHql();
		var inDtlHql = me.getInDtlHql();
		var qtyHql = me.getQtyHql();
		if(inDtlHql) url += '&inDtlHql=' + JShell.String.encode(inDtlHql);
		if(qtyHql) url += '&qtyHql=' + JShell.String.encode(qtyHql);
		if(reaGoodsHql) url += '&reaGoodsHql=' + JShell.String.encode(reaGoodsHql);
		return url;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		if(!me.InDocNo) {
			var info = "请选择入库单后再操作!";
			JShell.Msg.alert(info, null, 2000);
			return;
		}
		me.hiddenColumns(true);
		me.load(null, true, autoSelect);

	},
	/**重写,默认全选中*/
	doAutoSelect: function(records, autoSelect) {
		var me = this;
		//me.callParent(arguments);
		JShell.Action.delay(function() {
			me.getSelectionModel().selectAll();
		}, null, 500);
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		if(!me.InDocNo) {
			var info = "请选择入库单后再操作!";
			JShell.Msg.alert(info, null, 2000);
			me.store.removeAll();
			me.fireEvent('nodata', me);
			return;
		}
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onSearch();
	},
	/**移除选择的库存记录行*/
	onRemoveSelects: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		for(var i = 0; i < records.length; i++) {
			me.store.remove(records[i]);
		}
	},
	/**确认移库*/
	onConfirm: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.fireEvent('onConfirmTransfer', me);
	},
	/**清除查询栏数据*/
	clearToolVal: function() {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var txtSearch = buttonsToolbar.getComponent("txtSearch");
		var goodsClassType = buttonsToolbar.getComponent('GoodsClassType');
		var goodsClass = buttonsToolbar.getComponent('GoodsClass');
		txtSearch.setValue('');
		goodsClassType.setValue('');
		goodsClass.setValue('');
	},
	/**确认移库保存前验证*/
	onSaveValid: function() {
		var me = this;
		var isExec = true;
		var msg = '';
		//移库入库的移库货品的源库房必须相同且只能是一个
		var fristSStoragePlace = "";
		me.store.each(function(record) {
			var goodsName = record.get('ReaBmsQtyDtl_GoodsName');
			if(!record.get('ReaBmsQtyDtl_DStorageID')) {
				msg += '货品名称:【' + goodsName + '】的目标库房\n为空,不能移库!<br>';
				isExec = false;
				return false;
			}
			var sStorageID = record.get('ReaBmsQtyDtl_StorageID');
			var sPlaceID = record.get('ReaBmsQtyDtl_PlaceID');
			var dStorageID = record.get('ReaBmsQtyDtl_DStorageID');
			var dPlaceID = record.get('ReaBmsQtyDtl_DPlaceID');
			var dStoragePlace = dStorageID + dPlaceID;
			var sStoragePlace = sStorageID + sPlaceID;
			if(!fristSStoragePlace) fristSStoragePlace = sStoragePlace;
			if(dStoragePlace == sStoragePlace) {
				msg += '货品名称:【' + goodsName + '】的目的库房货架,源库房货架一致,不能移库!<br>';
				isExec = false;
				return false;
			}
			if(fristSStoragePlace != sStoragePlace) {
				msg += '移库入库的移库货品的源库房必须相同且只能是一个,不能移库!<br>';
				isExec = false;
				return false;
			}
		});
		if(!isExec) {
			JShell.Msg.error(msg);
		}
		return isExec;
	}
});