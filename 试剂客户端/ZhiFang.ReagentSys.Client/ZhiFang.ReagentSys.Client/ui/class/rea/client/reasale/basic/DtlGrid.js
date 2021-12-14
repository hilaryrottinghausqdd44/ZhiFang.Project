/**
 * 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.basic.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.DtlGrid',
	title: '供货明细列表',

	/**获取数据服务路径(不添加LabId过滤)*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlByHQL?isPlanish=true',
	
	/**获取数据服务路径(不添加LabId过滤)*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsCenSaleDtl',
	
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**默认每页数量*/
	defaultPageSize: 10000,
	/**分页栏下拉框数据*/
	pageSizeList: [
		[10000, 10000]
	],
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsCenSaleDtl_ReaGoodsNo',
		direction: 'ASC'
	}],
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**默认选中*/
	autoSelect: true,
	/**总单Id*/
	PK: null,
	/**是否多选行*/
	checkOne: true,
	canEdit: true,
	/**客户端货品的条码类型*/
	BarCodeTypeKey: "ReaGoodsBarCodeType",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata: function() {
				me.enableControl(true);
			},
			beforeedit: function(editor, e) {
				return me.canEdit;
			}
		});
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.BarCodeTypeKey, false, true, null);

		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		if(!me.checkOne) me.setCheckboxModel();
		//数据列
		//me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
		//只能点击复选框才能选中
		//		me.selModel = new Ext.selection.CheckboxModel({
		//			checkOnly: true
		//		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsCenSaleDtl_ReaCompanyName',
			text: '所属供应商',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ReaCompID',
			text: '所属供应商Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ReaServerCompCode',
			text: '供应商平台码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_CenOrgGoodsNo',
			text: '供货商货品编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_GoodsNo',
			text: '货品平台编号',
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ReaGoodsName',
			text: '货品名称',
			width: 150,
			columnCountKey: me.columnCountKey,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsCenSaleDtl_BarCodeType");
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
			dataIndex: 'ReaBmsCenSaleDtl_GoodsUnit',
			text: '单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_UnitMemo',
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_GoodsQty',
			text: '<b style="color:blue;">供货数</b>',
			width: 75,
			//type: 'float',
			align: 'center',
			editor: {
				xtype: 'numberfield',
				allowBlank: false,
				minValue: 0,
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('ReaBmsCenSaleDtl_GoodsQty', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_Price',
			text: '<b style="color:blue;">单价</b>',
			width: 80,
			//type: 'float',
			align: 'center',
			editor: {
				xtype: 'numberfield',
				//minValue: 0,
				decimalPrecision: 3,
				//allowBlank: false
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('ReaBmsCenSaleDtl_Price', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_SumTotal',
			sortable: false,
			text: '金额',
			align: 'center',
			type: 'float',
			width: 80,
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_LotNo',
			text: '<b style="color:blue;">货品批号</b>',
			width: 100,
			width: 80,
			editor: {
				allowBlank: false,
				emptyText: '双击选择批号',
				listeners: {
					render: function(field, eOpts) {
						field.getEl().on('dblclick', function(p, el, e) {
							me.onChooseLotNo();
						});
					}
				}
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_InvalidDate',
			text: '<b style="color:blue;">有效期</b>',
			width: 80,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_Memo',
			sortable: false,
			text: '<b style="color:blue;">备注信息</b>',
			width: 80,
			hidden: true,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_TempRange',
			sortable: false,
			text: '<b style="color:blue;">温度范围</b>',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ProdDate',
			sortable: false,
			text: '<b style="color:blue;">生产日期</b>',
			width: 100,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ProdOrgName',
			sortable: false,
			text: '<b style="color:blue;">生产厂家</b>',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_BiddingNo',
			sortable: false,
			text: '<b style="color:blue;">招标号</b>',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_RegisterNo',
			sortable: false,
			text: '<b style="color:green;">注册证编号</b>',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_RegisterInvalidDate',
			sortable: false,
			text: '<b style="color:green;">注册证有效期</b>',
			width: 100,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_TaxRate',
			text: '税率',
			align: 'right',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsCenSaleDtl_BarCodeType',
			text: '<b style="color:blue;">条码类型</b>',
			width: 65,
			editor: {
				xtype: 'uxSimpleComboBox',
				value: '0',
				hasStyle: true,
				data: [
					['0', '批条码', 'color:green;'],
					['1', '盒条码', 'color:orange;'],
					['2', '无条码', 'color:black;']
				]
			},
			renderer: function(value, meta, record) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_IsPrintBarCode',
			text: '<b style="color:blue;">是否打印条码</b>',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_LotSerial',
			sortable: false,
			text: '批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_SysLotSerial',
			sortable: false,
			text: '系统内部批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ReaGoodsID',
			sortable: false,
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_CompGoodsLinkID',
			sortable: false,
			text: '供应商与货品关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ApproveDocNo',
			sortable: false,
			text: '批准文号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_StorageType',
			text: '储藏条件',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_GoodsSort',
			text: '货品序号',
			hidden: true,
			defaultRenderer: true
		}];
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.createFullscreenItems();
		items.push('-', 'refresh');

		items.push('-', {
			fieldLabel: '',
			labelWidth: 0,
			width: 100,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DocBarCodeType',
			emptyText: '条码类型选择',
			data: JShell.REA.StatusList.Status[me.BarCodeTypeKey].List,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		return items;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		if(me.formtype == "add") {
			me.store.removeAll();
			return false;
		}
		me.getView().update();
		if(!me.PK) {
			var error = me.errorFormat.replace(/{msg}/, "获取供货单ID值为空!");
			me.getView().update(error);
			return false;
		};

		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**@description 获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var barCodeType = buttonsToolbar.getComponent('DocBarCodeType');
		var search = buttonsToolbar.getComponent('Search');

		var where = [];

		if(barCodeType) {
			var value = barCodeType.getValue();
			if(value)
				where.push("reabmscensaledtl.BarCodeType=" + value);
		}
		if(search) {
			var value = search.getValue();
			if(value) {
				var searchHql = me.getSearchWhere(value);
				if(searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		return where.join(" and ");
	},
	/**@description 选择货品批号*/
	onChooseLotNo: function() {
		var me = this;
		var selected = me.getSelectionModel().getSelection();
		if(!selected || selected.length <= 0) return;
		var record = selected[0];
		var lotNo = record.get("ReaBmsCenSaleDtl_LotNo");
		var reaGoodsID = record.get("ReaBmsCenSaleDtl_ReaGoodsID");
		var reaGoodsName = record.get("ReaBmsCenSaleDtl_ReaGoodsName");
		var reaGoodsNo = record.get("ReaBmsCenSaleDtl_ReaGoodsNo");
		var maxWidth = 620; //document.body.clientWidth * 0.68;
		var height = document.body.clientHeight * 0.78;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			GoodsID: reaGoodsID,
			ReaGoodsNo: reaGoodsNo,
			GoodsCName: reaGoodsName,
			CurLotNo: lotNo,
			listeners: {
				accept: function(p, rec) {
					me.IsShowDtlInfo = true;
					if(rec) {
						record.set("ReaBmsCenSaleDtl_LotNo", rec.get("ReaGoodsLot_LotNo"));
						record.set("ReaBmsCenSaleDtl_ProdDate", rec.get("ReaGoodsLot_ProdDate"));
						record.set("ReaBmsCenSaleDtl_InvalidDate", rec.get("ReaGoodsLot_InvalidDate"));
						record.commit();
					}
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.goodslot.CheckGrid', config);
		win.show();
	}
});