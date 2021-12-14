/**
 * 盘库管理
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.basic.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '盘库明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsCheckDtlByHQL?isPlanish=true',

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
		property: 'ReaBmsCheckDtl_StorageID',
		direction: 'ASC'
	}, {
		property: 'ReaBmsCheckDtl_PlaceID',
		direction: 'ASC'
	}, {
		property: 'ReaBmsCheckDtl_ReaGoodsNo',
		direction: 'ASC'
	}, {
		property: 'ReaGoods_DispOrder',
		direction: 'ASC'
	}, {
		property: 'ReaBmsCheckDtl_LotNo',
		direction: 'ASC'
	}],

	/**默认选中*/
	autoSelect: true,

	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	Status: null,
	canEdit: true,
	CheckQtyReadOnly: false,
	/**用户UI配置Key*/
	userUIKey: 'inventory.basic.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "盘库明细列表",
	/**盘库时实盘数是否取库存数 1:是;2:否;*/
	isTakenFromQty: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			beforeedit: function(editor, e) {
				return me.canEdit;
			}
		});

	},
	initComponent: function() {
		var me = this;
		//JShell.REA.RunParams.getRunParamsValue("InventoryIsTakenFromQty", false, function(data1) {});
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsCheckDtl_ReaGoodsNo',
			text: '货品编码',
			width: 75,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsSort',
			text: '货品序号',
			width: 65,
			defaultRenderer: true,
			doSort: function(state) {
				//自定义排序字段,因为联合查询机构货品信息,可按机构货品序号排序
				var field = "ReaGoods_GoodsSort";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsCheckDtl_DispOrder',
			text: '显示次序',
			width: 65,
			defaultRenderer: true,
			doSort: function(state) {
				//自定义排序字段,因为联合查询机构货品信息,可按机构货品序号排序
				var field = "ReaGoods_DispOrder";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsCheckDtl_ProdGoodsNo',
			text: '厂商货品编码',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsNo',
			text: '货品平台编码',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_BarCodeType',
			text: '条码类型',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsName',
			text: '货品名称',
			width: 150,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsCheckDtl_BarCodeType");
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
			dataIndex: 'ReaBmsCheckDtl_GoodsSName',
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
			dataIndex: 'ReaBmsCheckDtl_ProdOrgName',
			text: '品牌',
			width: 90,
			defaultRenderer: true,
			sortable: false
		},{
			dataIndex: 'ReaBmsCheckDtl_GoodsUnit',
			text: '包装单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_UnitMemo',
			text: '包装规格',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_StorageName',
			text: '所属库房',
			width: 90,
			//hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_PlaceName',
			text: '所属货架',
			width: 90,
			//hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_LotNo',
			text: '货品批号',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_SumTotal',
			sortable: false,
			text: '库存总计',
			align: 'right',
			type: 'float',
			width: 75,
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsQty',
			text: '库存数',
			width: 75,
			type: 'float',
			align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_Price',
			text: '平均价格',
			width: 75,
			type: 'float',
			align: 'center',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_CheckQty',
			text: '<b style="color:blue;">实盘数</b>',
			width: 75,
			type: 'float',
			align: 'center',
			editor: {
				xtype: 'numberfield',
				listeners: {
					focus: function(field, e, eOpts) {
						field.setReadOnly(me.CheckQtyReadOnly);
					}
				}
			},
			renderer: function(value, meta, record) {
				var checkQty = parseFloat(value);
				var goodsQty = parseFloat(record.get("ReaBmsCheckDtl_GoodsQty"));

				if(checkQty < goodsQty) {
					var bColor = "";
					var fColor = "red";
					var style = 'font-weight:bold;';
					if(bColor)
						style = style + "background-color:" + bColor + ";";
					if(fColor)
						style = style + "color:" + fColor + ";";
					meta.tdAttr = 'data-qtip="<b>' + checkQty + '</b>"';
					meta.style = style;
				}
				return value;
			}
		}, {
			//xtype: 'checkcolumn',
			dataIndex: 'ReaBmsCheckDtl_IsException',
			text: '是否异常',
			width: 65,
			align: 'center',
			renderer: function(value, meta) {
				var v = value + '';
				if(v == '1') {
					meta.style = 'color:red';
					v = JShell.All.TRUE;
				} else if(v == '0') {
					meta.style = 'color:green';
					v = JShell.All.FALSE;
				} else {
					v == '';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCheckDtl_IsHandleException',
			text: '异常是否已处理',
			width: 100,
			align: 'center',
			renderer: function(value, meta) {
				var v = value + '';
				if(v == '1') {
					meta.style = 'color:green';
					v = JShell.All.TRUE;
				} else if(v == '0') {
					meta.style = 'color:red';
					v = JShell.All.FALSE;
				} else {
					v == '';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCheckDtl_Memo',
			sortable: false,
			text: '<b style="color:blue;">备注信息</b>',
			width: 120,
			hidden: false,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_CompanyName',
			text: '所属供应商',
			hidden: true,
			width: 105,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_CheckDocID',
			text: '盘库单ID',
			hidden: true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ReaCompanyID',
			sortable: false,
			text: '供应商ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ReaCompCode',
			sortable: false,
			text: '供应商机构码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ReaServerCompCode',
			sortable: false,
			text: '供应商机平台构码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_StorageID',
			sortable: false,
			text: '库房ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_PlaceID',
			sortable: false,
			text: '货架ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsID',
			sortable: false,
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_CompGoodsLinkID',
			sortable: false,
			text: '货品机构关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_LotSerial',
			text: '一维批号条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_LotQRCode',
			text: '试剂二维批条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsLotID',
			text: '批号ID',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ProdDate',
			text: '生产日期',
			hidden: true,
			isDate: true,
			hasTime: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_InvalidDate',
			text: '有效期',
			hidden: true,
			isDate: true,
			hasTime: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ZX1',
			text: 'ZX1',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ZX2',
			text: 'ZX2',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ZX3',
			text: 'ZX3',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}];
		for(var i = 0; i < columns.length; i++) {
			if(columns[i].editor) {
				columns[i].editor.listeners = {
					beforeedit: function(editor, e) {
						return me.canEdit;
					}
				}
			}
		}
		return columns;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		return me.changeResult2(data);
		//系统运行参数"盘库时实盘数是否取库存数"
//		var value1 = JcallShell.REA.RunParams.Lists.InventoryIsTakenFromQty.Value;
//		if(!value1) {
//			JShell.REA.RunParams.getRunParamsValue("InventoryIsTakenFromQty", false, function(result) {
//				var value1 = "" + JcallShell.REA.RunParams.Lists.InventoryIsTakenFromQty.Value;
//				if(value1 == "1" || value1 == "true") {
//					me.isTakenFromQty = true;
//				}
//				return me.changeResult2(data);
//			});
//		} else {
//			if(value1 == "1" || value1 == "true") {
//				me.isTakenFromQty = true;
//			}
//			return me.changeResult2(data);
//		}
	},
	/**@description 返回数据处理方法--盘库时实盘数是否取库存数*/
	changeResult2: function(data) {
		var me = this;
		var len = data.count;
		if(data&&data.list)len = data.list.length;
		if(!len)len=0;
		
		for(var i = 0; i < len; i++) {
			var IsException = data.list[i].ReaBmsCheckDtl_IsException;
			if(IsException == "1")
				IsException = 1;
			else
				IsException = 0;
			data.list[i].ReaBmsCheckDtl_IsException = IsException;

			var IsHandleException = data.list[i].ReaBmsCheckDtl_IsHandleException;
			if(IsHandleException == "1")
				IsHandleException = 1;
			else
				IsHandleException = 0;
			data.list[i].ReaBmsCheckDtl_IsHandleException = IsHandleException;
			//库存数
			var goodsQty = data.list[i].ReaBmsCheckDtl_GoodsQty;
			if(!goodsQty) goodsQty = 0;
			data.list[i].ReaBmsCheckDtl_GoodsQty = goodsQty;

			var checkQty = data.list[i].ReaBmsCheckDtl_CheckQty;
			if(!checkQty) checkQty = 0;
			//盘库时实盘数是否取库存数
			/* if(me.canEdit == true && me.isTakenFromQty == true&&checkQty<=0) {
				checkQty = goodsQty;
				data.list[i].ReaBmsCheckDtl_IsException =0;
				data.list[i].ReaBmsCheckDtl_IsHandleException = 0;
			} */
			data.list[i].ReaBmsCheckDtl_CheckQty = checkQty;
		}
		return data;
	},
	/**@description 实盘数值改变后联动*/
	onCheckQtyChanged: function(record) {
		var me = this;
		var CheckQty = record.get('ReaBmsCheckDtl_CheckQty');
		var GoodsQty = record.get('ReaBmsCheckDtl_GoodsQty');
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;
		if(CheckQty) CheckQty = parseFloat(CheckQty);
		else CheckQty = 0;

		//是否异常,是否已处理
		var IsException = 0;
		//CheckQty && 
		if(CheckQty != GoodsQty) IsException = 1;
		record.set('ReaBmsCheckDtl_IsException', IsException);
		record.commit();
	}
});