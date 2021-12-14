/**
 * 出库明细
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.enrollment.DtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.DtlGrid',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '出库明细列表',
	width: 800,
	height: 500,
	/**新增出库单并更新库存*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddGoodsReaBmsOutDoc',
	defaultOrderBy: [{
		property: 'ReaBmsOutDtl_InvalidDate',
		direction: 'DESC'
	}],

	/**默认加载数据*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**默认每页数量*/
	defaultPageSize: 5000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	defaultDisableControl: false,
	/**库存新增仪器是否允许为空,1是,0否*/
	IsEquip: '0',
	/**序号列宽度*/
	rowNumbererWidth: 40,
	/**试剂化仪器关系信息*/
	ReaTestEquipVOList: [],
	/**出库类型默认值*/
	defaluteOutType: '1',
	/**出库扫码模式(严格模式:1,混合模式：2)*/
	OutScanCodeModel: '2',
	/**用户UI配置Key*/
	userUIKey: 'out.enrollment.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "出库明细列表",
	/**表单选中的库房*/
	StorageObj: {},
	/**是否按出库人权限出库 false否,TRUE是*/
	IsEmpOut: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.removeAll();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('changeSumTotal');
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
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
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 40,
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
			dataIndex: 'ReaBmsOutDtl_GoodsNo',
			text: '平台编码',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsCName',
			text: '货品名称',
			sortable: false,
			width: 120,
			minWidth: 100,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsOutDtl_BarCodeType");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"') >= 0) value = value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_DefaulteGoodsQty',
			text: '货品库存',
			sortable: false,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_SumCurrentQty',
			text: '剩余总库存',
			sortable: false,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsQty',
			text: '<b style="color:blue;">出库数</b>',
			sortable: false,
			xtype: 'numbercolumn',
			format: '0.00',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				listeners: {
					focus: function(field, e, eOpts) {
						me.comSetReadOnlyOfBarCodeType(field, e);
					},
					change: function(com, newValue, oldValue, eOpts) {
						var records = me.getSelectionModel().getSelection();
						if(records.length == 0) {
							JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
							return;
						}
						if(newValue < 0) newValue = 0;
						me.setSumTotal(newValue, records[0]);
						me.fireEvent('changeSumTotal');
					}
				}
			},
			width: 80,
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_TestEquipID',
			text: '仪器Id',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaTestEquipVOList',
			text: '试剂所属仪器信息',
			sortable: false,
			hidden: true,
			width: 100,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		});
		columns.push(me.createEquipNameColumn());
		columns.push({
			dataIndex: 'ReaBmsOutDtl_GoodsUnit',
			text: '单位',
			sortable: false,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_UnitMemo',
			text: '规格',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Price',
			text: '单价',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_BarCodeType',
			text: '条码类型',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SumTotal',
			text: '金额',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotNo',
			text: '批号',
			sortable: false,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_StorageName',
			text: '库房名称',
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_PlaceName',
			text: '货架名称',
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_InvalidDate',
			text: '效期',
			sortable: false,
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaCompanyID',
			text: '供应商Id',
			sortable: false,
			hidden: true,
			sortable: false,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CompanyName',
			text: '供应商',
			sortable: false,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_RegisterNo',
			text: '注册证号',
			sortable: false,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_StorageID',
			text: '库房ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_PlaceID',
			text: '货架ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsSerial',
			text: '货品条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotSerial',
			text: '批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SysLotSerial',
			text: '系统内部批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CompGoodsLinkID',
			text: '货品机构关系ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaServerCompCode',
			text: '供应商机平台构码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdDate',
			text: '生产日期',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsID',
			text: '货品iD',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Memo',
			text: 'Memo',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TaxRate',
			text: 'TaxRate',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_QtyDtlID',
			text: 'QtyDtlID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Tab',
			text: '合并标签', //供应商Id+货品Id+批号+库房Id+货架Id
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_DefaulteGoodsID',
			text: '原货品ID',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaGoodsNo',
			text: '货品编码',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotQRCode',
			text: '二维批条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaCompCode',
			text: '供货方编码',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsSort',
			text: '货品序号',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CurReaGoodsScanCodeList',
			text: '当前扫码记录',
			sortable: false,
			hidden: true,
			width: 100,
			editor: {
				readOnly: true
			},
			renderer: function(value, meta, record) {
				var v = me.showMemoText(value, meta, record);
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_BarCodeQtyDtlID',
			text: '本次扫码库存ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ISmallUnit',
			text: '当前条码是否是小包装',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		});

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [{
			xtype: 'label',
			text: '出库明细',
			style: "font-weight:bold;color:blue;",
			margin: '0 5 5 5'
		}, '-', {
			text: '刷新库存量',
			tooltip: '刷新库存量',
			iconCls: 'button-search',
			margin: '0 5 0 5',
			handler: function() {
				me.getGoodsQty();
			}
		}];
		items = me.createTestEquipItems(items);
		items.push('-', {
			text: '确认出库',
			iconCls: 'button-save',
			itemId: "btnSave",
			tooltip: '保存并完成出库',
			handler: function() {
				JShell.Action.delay(function() {
					me.onSaveClick();
				}, null, 500);
			}
		});
		return items;
	},
	/**创建新增行数据*/
	createRowObj: function(recQty, barcode) {
		var me = this;
		var outDtlObj = me.createOutDtlObj(recQty.data);
		outDtlObj = me.createBarcodeJObject(outDtlObj, recQty, barcode);
		//出库扫码模式判断
		outDtlObj = me.createScanCodeModel(outDtlObj, barcode);
		//仪器处理
		outDtlObj = me.setOutDtlObjTestEquipValue(recQty, outDtlObj);
		return outDtlObj;
	},
	/**获取明细列表数据*/
	getOutDtlInfo: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var dtlArr = [],
			dtAddList = [];
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var obj = me.getEntity(rec);
			var barCodeQtyDtlID = rec.get('ReaBmsOutDtl_BarCodeQtyDtlID');
			if(barCodeQtyDtlID) {
				obj.QtyDtlID = barCodeQtyDtlID;
			}
			//扫码明细
			var reaBmsInDtlLink = [];
			var scanCodeList = rec.get('ReaBmsOutDtl_CurReaGoodsScanCodeList');
			if(scanCodeList.length > 0) {
				reaBmsInDtlLink = Ext.JSON.decode(scanCodeList);
			}
			obj.ReaBmsOutDtlLinkList = reaBmsInDtlLink;
			dtlArr.push(obj);
		}
		return dtlArr;
	},
	//总单信息信息保存
	getDocObj: function() {
		var me = this;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var deptID = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
		var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
		var entity = {
			Visible: 1,
			CreaterID: userId,
			CreaterName: userName,
			OutBoundID: userId,
			OutBoundName: userName,
			TakerID: userId,
			TakerName: userName,
			CheckID: userId,
			CheckName: userName,
			DeptID: deptID,
			DeptName: deptName,
			StorageID: me.getStorageObj().StorageID,
			StorageName: me.getStorageObj().StorageName
		};
		//
		entity.TotalPrice = me.getSumTotal();
		//出库类型 ==使用出库
		entity.OutType = "1";
		entity.OutTypeName = "使用出库";
		//时间
		var Sysdate = JcallShell.System.Date.getDate();
		var dateStr = JcallShell.Date.toString(Sysdate);
		entity.DataUpdateTime = JShell.Date.toServerDate(dateStr) ? JShell.Date.toServerDate(dateStr) : null;
		entity.DataAddTime = JShell.Date.toServerDate(dateStr) ? JShell.Date.toServerDate(dateStr) : null;
		entity.OperDate = JShell.Date.toServerDate(dateStr) ? JShell.Date.toServerDate(dateStr) : null;
		return entity;
	},
	/**获取库房id和名称*/
	getStorageObj: function() {
		var me = this;
		var res = me.store.data.items;
		var StorageObj = {};
		//因库房只能有一个 从明细找库房id 和名称
		for(var i = 0; i < res.length; i++) {
			if(res[i].data.ReaBmsOutDtl_StorageID) {
				var id = res[i].data.ReaBmsOutDtl_StorageID;
				var name = res[i].data.ReaBmsOutDtl_StorageName;
				StorageObj.StorageID = id;
				StorageObj.StorageName = name;
				continue;
			}
		}
		return StorageObj;
	},
	//出库登记保存
	onSaveClick: function() {
		var me = this;
		//出库明细验证
		var isAllowZero = false;
		var check = me.onSaveCheck(isAllowZero);
		if(check == false) return;

		//获取总单信息
		var outDoc = me.getDocObj();
		//获取明细
		var outDtlList = me.getOutDtlInfo();
		var params = Ext.JSON.encode({
			reaBmsOutDoc: outDoc,
			listReaBmsOutDtl: outDtlList,
			isEmpOut: me.IsEmpOut
		});
		me.showMask("出库保存中...");
		var url = JShell.System.Path.getUrl(me.addUrl);
		var btnSave = me.getComponent("buttonsToolbar").getComponent("btnSave");
		if(btnSave) btnSave.setDisabled(true);
		JShell.Server.post(url, params, function(data) {
			if(btnSave) btnSave.setDisabled(false);
			me.hideMask();
			if(data.success) {
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});