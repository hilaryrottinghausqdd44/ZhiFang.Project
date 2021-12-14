/**
 * 退库入库
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.refund.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '退库入库明细列表',
	width: 800,
	height: 500,
	/**查询数据*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsInDtlByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'ReaBmsInDtl_DataAddTime',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**默认每页数量*/
	defaultPageSize: 5000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	defaultDisableControl: false,
	/**序号列宽度*/
	rowNumbererWidth: 40,
	canEdit: true,
	isLoad: true,
	/**用户UI配置Key*/
	userUIKey: 'out.refund.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "退库入库明细列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.on({
			beforeedit: function(editor, e) {
				if(!me.canEdit) return false;
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.addEvents('changeSumTotal', 'onSaveClick');
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
		var columns = [];
		columns.push({
			xtype: 'rownumberer',
			text: me.Shell_ux_grid_Panel.NumberText,
			width: me.rowNumbererWidth,
			align: 'center'
		}, {
			dataIndex: 'ReaBmsInDtl_StorageName',
			text: '库房',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_PlaceName',
			text: '货架',
			sortable: false,
			hidden: false,
			width: 85,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 35,
			hidden: true,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.store.remove(rec);
					if(me.store.getCount() == 0) {
						me.fireEvent('changeSumTotal');
					}

				}
			}]
		}, {
			dataIndex: 'ReaBmsInDtl_ReaGoodsNo',
			text: '货品编码',
			hideable: false,
			sortable: false,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsCName',
			text: '货品名称',
			sortable: false,
			width: 120,
			minWidth: 100,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsInDtl_BarCodeType");
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
			dataIndex: 'ReaBmsInDtl_GoodSName',
			text: '简称',
			width: 90,
			defaultRenderer: true,
			doSort: function(state) {
				var field="ReaGoods_SName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsInDtl_ProdOrgName',
			text: '品牌',
			width: 90,
			defaultRenderer: true,
			sortable: false
		}, {
			dataIndex: 'ReaBmsInDtl_BarCodeType',
			text: 'BarCodeType',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsUnit',
			text: '单位',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_UnitMemo',
			text: '规格',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_Price',
			text: '单价',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsQty',
			text: '出库数',
			sortable: false,
			xtype: 'numbercolumn',
			format: '0.00',
			width: 75,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_DefaulteGoodsQty',
			text: '<b style="color:blue;">退库数</b>',
			sortable: false,
			hidden: true,
			width: 75,
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						var records = me.getSelectionModel().getSelection();
						if(records.length == 0) {
							JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
							return;
						}
						if(newValue < 0) newValue = 0;

						var GoodsQty = records[0].get('ReaBmsInDtl_GoodsQty');
						if(!GoodsQty) GoodsQty = 0;
						if(newValue > GoodsQty) {
							JShell.Msg.error('退库数量不能大于出库数量');
							com.setValue(0);
							return;
						}
						me.setSumTotal(newValue, records[0]);
						me.fireEvent('changeSumTotal');
					}
				}

			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_TestEquipName',
			text: '仪器',
			hidden: true,
			sortable: false,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_Price',
			text: '单价',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_SumTotal',
			text: '总金额',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_LotNo',
			text: '批号',
			sortable: false,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_InvalidDate',
			text: '效期',
			sortable: false,
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ReaCompanyID',
			text: '供应商Id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_CompanyName',
			text: '供应商',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_RegisterNo',
			text: '注册证号',
			hidden: true,
			sortable: false,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_StorageID',
			text: '库房ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_PlaceID',
			text: '货架ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsSerial',
			text: '货品条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_LotSerial',
			text: '批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_SysLotSerial',
			text: '系统内部批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_CompGoodsLinkID',
			text: '货品机构关系ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ReaServerCompCode',
			text: '供应商机平台构码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ProdDate',
			text: '生产日期',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsID',
			text: '货品iD',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_Memo',
			text: 'Memo',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_TaxRate',
			text: 'TaxRate',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsQty',
			text: '出库数量',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_Id',
			text: '出库ID',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_LotQRCode',
			text: '二维批条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ReaCompCode',
			text: '供货方编码',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsSort',
			text: '货品序号',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_QtyDtlID',
			text: '出库关联的库存IDStr',
			sortable: false,
			hidden: true,width: 100,
			defaultRenderer: true
		});
		return columns;
	},
	/***
	 * 计算货品总额
	 */
	getSumTotal: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var count = 0;
		for(var i = 0; i < len; i++) {
			var SumTotal = records[i].get('ReaBmsInDtl_SumTotal');
			if(!SumTotal) SumTotal = 0;

			count += Number(SumTotal);
		}
		return count;
	},
	/***
	 * 根据本次入库数量计算总计金额
	 */
	setSumTotal: function(GoodsQty, record) {
		var me = this;
		var Price = record.get('ReaBmsInDtl_Price');
		if(!Price) Price = 0;
		var SumTotal = Number(GoodsQty) * Number(Price);
		record.set('ReaBmsInDtl_SumTotal', SumTotal);

	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [{
			xtype: 'radiogroup',
			fieldLabel: '',
			columns: 2,
			itemId: 'rbBtn',
			vertical: true,
			width: 150,
			items: [{
					boxLabel: '整单退库',
					name: 'rb',
					inputValue: '0'
				},
				{
					boxLabel: '部分退库',
					name: 'rb',
					inputValue: '1',
					checked: true
				}
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					var v = newValue.rb;
					var records = me.store.data.items,
						len = records.length;
					//部分出库
					if(v == '1') {
						me.canEdit = true;
						for(var i = 0; i < len; i++) {
							records[i].set('ReaBmsInDtl_DefaulteGoodsQty', '0');
							records[i].commit();
						}
						if(!me.isLoad) {
							//me.columns[1].show();
							Ext.Array.each(me.columns, function(column, index, countriesItSelf) {
								if(column.text == "删除") {
									me.columns[index].show();
									return false;
								}
							});
						}
					} else {
						me.canEdit = false;
						for(var i = 0; i < len; i++) {
							records[i].set('ReaBmsInDtl_DefaulteGoodsQty', records[i].get('ReaBmsInDtl_GoodsQty'));
							records[i].commit();
						}
						if(!me.isLoad) {
							//me.columns[1].hide();
							Ext.Array.each(me.columns, function(column, index, countriesItSelf) {
								if(column.text == "删除") {
									me.columns[index].hide();
									return false;
								}
							});
						}
						me.fireEvent('changeSumTotal');
					}
				}
			}
		}, {
			xtype: 'label',
			text: '出库明细',
			hidden: true,
			style: "font-weight:bold;color:blue;",
			margin: '0 0 5 5'
		}, '-', {
			text: '保存',
			tooltip: '保存',
			iconCls: 'button-save',
			itemId: 'saveBtn',
			handler: function() {
				me.onSaveClick();
			}
		}];
		return items;
	},
	/**
	 * 插入行
	 * */
	onUpdateData: function(records) {
		var me = this;
		me.store.removeAll();
		len = records.length;

		for(var i = 0; i < len; i++) {
			var obj = {
				ReaBmsInDtl_Id: records[i].get('ReaBmsOutDtl_Id'),
				ReaBmsInDtl_GoodsNo: records[i].get('ReaBmsOutDtl_GoodsNo'),
				ReaBmsInDtl_GoodsCName: records[i].get('ReaBmsOutDtl_GoodsCName'),
				ReaBmsInDtl_BarCodeType: records[i].get('ReaBmsOutDtl_BarCodeType'),
				ReaBmsInDtl_GoodsUnit: records[i].get('ReaBmsOutDtl_GoodsUnit'),
				ReaBmsInDtl_Price: records[i].get('ReaBmsOutDtl_Price'),
				ReaBmsInDtl_GoodsQty: records[i].get('ReaBmsOutDtl_GoodsQty'),
				ReaBmsInDtl_SumTotal: records[i].get('ReaBmsOutDtl_SumTotal'),
				ReaBmsInDtl_LotNo: records[i].get('ReaBmsOutDtl_LotNo'),
				ReaBmsInDtl_InvalidDate: records[i].get('ReaBmsOutDtl_InvalidDate'),
				ReaBmsInDtl_ReaCompanyID: records[i].get('ReaBmsOutDtl_ReaCompanyID'),
				ReaBmsInDtl_CompanyName: records[i].get('ReaBmsOutDtl_CompanyName'),
				ReaBmsInDtl_RegisterNo: records[i].get('ReaBmsOutDtl_RegisterNo'),
				ReaBmsInDtl_StorageID: records[i].get('ReaBmsOutDtl_StorageID'),
				ReaBmsInDtl_PlaceID: records[i].get('ReaBmsOutDtl_PlaceID'),
				ReaBmsInDtl_StorageName: records[i].get('ReaBmsOutDtl_StorageName'),
				ReaBmsInDtl_PlaceName: records[i].get('ReaBmsOutDtl_PlaceName'),
				ReaBmsInDtl_GoodsSerial: records[i].get('ReaBmsOutDtl_GoodsSerial'),
				ReaBmsInDtl_LotSerial: records[i].get('ReaBmsOutDtl_LotSerial'),
				ReaBmsInDtl_SysLotSerial: records[i].get('ReaBmsOutDtl_SysLotSerial'),
				ReaBmsInDtl_CompGoodsLinkID: records[i].get('ReaBmsOutDtl_CompGoodsLinkID'),
				ReaBmsInDtl_ReaServerCompCode: records[i].get('ReaBmsOutDtl_ReaServerCompCode'),
				ReaBmsInDtl_ProdDate: records[i].get('ReaBmsOutDtl_ProdDate'),
				ReaBmsInDtl_GoodsID: records[i].get('ReaBmsOutDtl_GoodsID'),
				ReaBmsInDtl_Memo: records[i].get('ReaBmsOutDtl_Memo'),
				ReaBmsInDtl_TaxRate: records[i].get('ReaBmsOutDtl_TaxRate'),
				ReaBmsInDtl_BarCodeType: records[i].get('ReaBmsOutDtl_BarCodeType'),
				//				ReaBmsInDtl_ProdGoodsNo:records[i].get('ReaBmsOutDtl_ProdGoodsNo'),
				ReaBmsInDtl_ReaGoodsNo: records[i].get('ReaBmsOutDtl_ReaGoodsNo'),
				ReaBmsInDtl_CenOrgGoodsNo: records[i].get('ReaBmsOutDtl_CenOrgGoodsNo'),
				ReaBmsInDtl_LotQRCode: records[i].get('ReaBmsOutDtl_LotQRCode'),
				ReaBmsInDtl_UnitMemo: records[i].get('ReaBmsOutDtl_UnitMemo'),
				ReaBmsInDtl_ReaCompCode: records[i].get('ReaBmsOutDtl_ReaCompCode'),
				ReaBmsInDtl_QtyDtlID: records[i].get('ReaBmsOutDtl_QtyDtlID'),
				ReaBmsInDtl_GoodsSort: records[i].get('ReaBmsOutDtl_GoodsSort')
			};
			var Price = records[i].get('ReaBmsOutDtl_Price');
			if(!Price) Price = 0;
			var GoodsQty = records[i].get('ReaBmsOutDtl_GoodsQty');
			if(!GoodsQty) GoodsQty = 0;
			var SumTotal = Number(GoodsQty) * Number(Price);
			obj.ReaBmsInDtl_SumTotal = SumTotal;
			var buttonsToolbar = me.getComponent('buttonsToolbar');
			var rbBtn = buttonsToolbar.getComponent('rbBtn');
			var v = rbBtn.getValue().rb;
			if(v == '0') { //整单
				obj.ReaBmsInDtl_DefaulteGoodsQty = records[i].get('ReaBmsOutDtl_GoodsQty');
			} else {
				obj.ReaBmsInDtl_DefaulteGoodsQty = '0';
			}
			me.store.insert(me.store.getCount(), obj);
		}

	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	},
	/**
	 * 保存校验
	 * 退库数量，不能为0
	 */
	onSaveCheck: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var isExec = true;
		var msg = '';
		if(len == 0) {
			msg = '请选择出库单!';
			isExec = false;
		}
		for(var i = 0; i < len; i++) {
			//退库数量
			var DefaulteGoodsQty = records[i].get('ReaBmsInDtl_DefaulteGoodsQty');
			if(!DefaulteGoodsQty) DefaulteGoodsQty = 0;
			//出库数量
			var GoodsQty = records[i].get('ReaBmsInDtl_GoodsQty');
			if(!GoodsQty) GoodsQty = 0;
			if(DefaulteGoodsQty == '0') {
				msg += '货品名称:【' + records[i].get('ReaBmsInDtl_GoodsCName') + '】的退库数量为0,不能保存<br>';
				isExec = false;
			}
			if(Number(DefaulteGoodsQty) < 0) {
				msg += '货品名称:【' + records[i].get('ReaBmsInDtl_GoodsCName') + '】的退库数量小于0,不能保存<br>';
				isExec = false;
			}
			if(Number(DefaulteGoodsQty) > Number(GoodsQty)) {
				msg += '货品名称:【' + records[i].get('ReaBmsInDtl_GoodsCName') + '】的退库数量大于出库数量,不能保存<br>';
				isExec = false;
			}
		}
		if(!isExec) {
			JShell.Msg.error(msg);
		}
		return isExec;
	},
	onSaveClick: function() {
		var me = this;
		me.fireEvent('onSaveClick', me);
	},
	/**获取明细列表数据*/
	getDtlInfo: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var DtlArr = [],
			dtAddList = [],
			entity = {};
		for(var i = 0; i < len; i++) {
			entity = {};
			var rec = records[i];
			var GoodsUnit = rec.get('ReaBmsInDtl_GoodsUnit');
			var ReaCompanyID = rec.get('ReaBmsInDtl_ReaCompanyID');
			var CompanyName = rec.get('ReaBmsInDtl_CompanyName');
			var TaxRate = rec.get('ReaBmsInDtl_TaxRate');
			var LotNo = rec.get('ReaBmsInDtl_LotNo');
			var GoodsID = rec.get('ReaBmsInDtl_GoodsID');
			var GoodsCName = rec.get('ReaBmsInDtl_GoodsCName');
			var QtyDtlID = rec.get('ReaBmsInDtl_QtyDtlID');
			var GoodsQty = rec.get('ReaBmsInDtl_DefaulteGoodsQty');
			var Price = rec.get('ReaBmsInDtl_Price');
			var SumTotal = rec.get('ReaBmsInDtl_SumTotal');
			if(!SumTotal) SumTotal = 0;
			var StorageID = rec.get('ReaBmsInDtl_StorageID');
			var PlaceID = rec.get('ReaBmsInDtl_PlaceID');
			var StorageName = rec.get('ReaBmsInDtl_StorageName');
			var PlaceName = rec.get('ReaBmsInDtl_PlaceName');
			var GoodsSerial = rec.get('ReaBmsInDtl_GoodsSerial');
			var SysLotSerial = rec.get('ReaBmsInDtl_SysLotSerial');
			var LotSerial = rec.get('ReaBmsInDtl_LotSerial');
			var Memo = rec.get('ReaBmsInDtl_Memo');
			var GoodsNo = rec.get('ReaBmsInDtl_GoodsNo');
			var CompGoodsLinkID = rec.get('ReaBmsInDtl_CompGoodsLinkID');
			var ReaServerCompCode = rec.get('ReaBmsInDtl_ReaServerCompCode');
			var BarCodeType = rec.get('ReaBmsInDtl_BarCodeType');
			var ProdDate = rec.get('ReaBmsInDtl_ProdDate');
			var InvalidDate = rec.get('ReaBmsInDtl_InvalidDate');
			var Sysdate = JcallShell.System.Date.getDate();
			var DataAddTime = JcallShell.Date.toString(Sysdate);
			var Id = rec.get('ReaBmsInDtl_Id');
			var ProdGoodsNo = rec.get('ReaBmsInDtl_ProdGoodsNo');
			var CenOrgGoodsNo = rec.get('ReaBmsInDtl_CenOrgGoodsNo');
			var ReaGoodsNo = rec.get('ReaBmsInDtl_ReaGoodsNo');
			var UnitMemo = rec.get('ReaBmsInDtl_UnitMemo');
			var LotQRCode = rec.get('ReaBmsInDtl_LotQRCode');
			var ReaCompCode = rec.get('ReaBmsInDtl_ReaCompCode');
			var GoodsSort = rec.get('ReaBmsInDtl_GoodsSort');
			var obj = {
				GoodsID: GoodsID,
				GoodsCName: GoodsCName,
				GoodsUnit: GoodsUnit,
				GoodsQty: GoodsQty,
				StorageName: StorageName,
				ReaCompanyID: ReaCompanyID,
				CompanyName: CompanyName,
				ReaServerCompCode: ReaServerCompCode,
				LotNo: LotNo,
				GoodsNo: GoodsNo,
				GoodsSerial: GoodsSerial,
				SysLotSerial: SysLotSerial,
				LotSerial: LotSerial,
				Memo: Memo,
				Visible: 1,
				PlaceName: PlaceName,
				Id: Id,
				CenOrgGoodsNo: CenOrgGoodsNo,
				ReaGoodsNo: ReaGoodsNo,
				UnitMemo: UnitMemo,
				LotQRCode: LotQRCode,
				ReaCompCode: ReaCompCode
			}
			if(GoodsSort) {
				obj.GoodsSort = GoodsSort;
			}
			if(PlaceID) {
				obj.PlaceID = PlaceID;
			}
			if(StorageID) {
				obj.StorageID = StorageID;
			}
			if(ProdGoodsNo) {
				obj.ProdGoodsNo = ProdGoodsNo;
			}
			if(DataAddTime) {
				obj.DataUpdateTime = JShell.Date.toServerDate(DataAddTime);
				obj.DataAddTime = JShell.Date.toServerDate(DataAddTime);
			}
			if(InvalidDate) {
				obj.InvalidDate = JShell.Date.toServerDate(InvalidDate);
			}
			if(ProdDate) {
				obj.ProdDate = JShell.Date.toServerDate(ProdDate);
			}
			if(SumTotal) {
				obj.SumTotal = SumTotal ? SumTotal : 0;
			}
			if(Price) {
				obj.Price = Price ? Price : 0;
			}
			if(TaxRate) {
				obj.TaxRate = TaxRate ? TaxRate : 0;
			}
			if(CompGoodsLinkID) {
				obj.CompGoodsLinkID = CompGoodsLinkID;
			}
			if(BarCodeType) {
				obj.BarCodeType = Number(BarCodeType);
			}
			var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
			var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
			if(userId) {
				obj.CreaterID = userId;
				obj.CreaterName = userName;
			}
			DtlArr.push(obj);
		}
		return DtlArr;
	},
	/*inputType：0整单入库 1部分入库*/
	getInputType: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var rbBtn = buttonsToolbar.getComponent('rbBtn');
		var v = rbBtn.getValue().rb;
		return v;
	}
});