/**
 * 库存货品选择-待选盘库货品明细
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.add.choose.LeftGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '待选盘库货品明细',
	width: 530,
	height: 620,
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**是否带确认按钮*/
	hasAcceptButton: false,
	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchAddReaBmsCheckDtlByHQL?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsCheckDtl_StorageID',
		direction: 'ASC'
	}, {
		property: 'ReaBmsCheckDtl_PlaceID',
		direction: 'ASC'
	},  {
		property: 'ReaBmsCheckDtl_ReaGoodsNo',
		direction: 'ASC'
	},{
		property: 'ReaGoods_DispOrder',
		direction: 'ASC'
	},{
		property: 'ReaBmsCheckDtl_LotNo',
		direction: 'ASC'
	}],
	/**盘库实体条件*/
	docEntity: "",
	/**机构货品条件*/
	reaGoodHql: "",
	/**用户UI配置Key*/
	userUIKey: 'inventory.add.choose.LeftGrid',
	/**用户UI配置Name*/
	userUIName: "待选盘库货品明细列表",

	initComponent: function() {
		var me = this;
		me.addEvents('onBeforeSearch');
		//查询框信息
		me.searchInfo = {
			width: 225,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品编码/名称/简称/拼音字头',
			fields: ['reagoods.ReaGoodsNo', 'reagoods.CName', 'reagoods.SName', 'reagoods.PinYinZiTou']
		};
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
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsSort',
			text: '货品序号',
			hidden:true,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_DispOrder',
			text: '显示次序',
			width: 65,
			defaultRenderer: true
		},{
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
			width: 135,
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
			sortable: false
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
			width: 85,
			//hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_PlaceName',
			text: '所属货架',
			width: 75,
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
			hidden: true,
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
			hidden: true,
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
			hidden: true,
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
			hidden: true,
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

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
		me.buttonToolbarItems.unshift('refresh', '-', {
			emptyText: '条码类型',
			labelWidth: 0,
			width: 70,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'BarCodeMgr',
			hasStyle: true,
			data: [
				['', '请选择', 'color:green;'],
				['0', '批条码', 'color:green;'],
				['1', '盒条码', 'color:orange;'],
				['2', '无条码', 'color:black;']
			],
			value: '',
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		me.buttonToolbarItems.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});
		me.buttonToolbarItems.push('->', {
			iconCls: 'button-check',
			text: '全部选择',
			tooltip: '将当前页货品全部选择',
			handler: function() {
				me.onAcceptClick();
			}
		});
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		items.unshift(me.createToolbarItems2());
		return items;
	},
	createToolbarItems2: function() {
		var me = this;
		var items = [];
		items.push({
			boxLabel: '仅显示相同盘库条件最近',
			labelWidth: 0,
			name: 'cboIsCheckDays',
			itemId: 'cboIsCheckDays',
			xtype: 'checkboxfield',
			inputValue: true,
			checked: true,
			width: 155,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {

				}
			}
		}, {
			fieldLabel: '',
			labelWidth: 0,
			width: 45,
			labelSeparator: '',
			labelAlign: 'right',
			emptyText: '',
			name: 'CheckDays',
			itemId: 'CheckDays',
			xtype: 'numberfield',
			value: 3,
			minValue: 0,
			maxValue: 7,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER)
						me.onSearch();
				}
			}
		}, {
			xtype: 'displayfield',
			fieldLabel: '',
			labelWidth: 0,
			value: '天内未盘库的试剂',
			labelSeparator: ''
			//width: 195
		});
		//入库时间范围作为盘库数据过滤条件，添加过滤选择项：包含最近___天内库存数为0的货品（生效的前提是盘库条件的“包括库存数为0的试剂勾选上”）;
		items.push({
			fieldLabel: '包含最近',
			labelWidth: 60,
			width: 115,
			style:{
				marginLeft:"10px"
			},
			labelSeparator: '',
			labelAlign: 'right',
			emptyText: '',
			name: 'ZeroQtyDays',
			itemId: 'ZeroQtyDays',
			xtype: 'numberfield',
			value: 0,
			minValue: 0,
			maxValue: 365,
			isLocked:true,
			readOnly:true,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER)
						me.onSearch();
				}
			}
		}, {
			xtype: 'displayfield',
			fieldLabel: '',
			labelWidth: 0,
			value: '天内库存数为0的试剂',
			labelSeparator: ''
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	/**@description 左列表查询项处理*/
	setZeroQtyDays:function(isHasZeroQty){
		var me = this;
		var buttonsToolbar2 = me.getComponent('buttonsToolbar2')
		var zeroQtyDays= buttonsToolbar2.getComponent('ZeroQtyDays');
		if(zeroQtyDays) {
			zeroQtyDays.setReadOnly(!zeroQtyDays);
			var value1=0;
			if(isHasZeroQty==true){
				value1=180;
			}
			zeroQtyDays.setValue(value1);
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		var fields = me.getStoreFields(true).join(',');
		fields = fields.replace(/ReaBmsCheckDtl_/g, "VO_");
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + fields + "&preIsVO=true";
		var days = 0;
		var buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			isCheckDays = buttonsToolbar2.getComponent('cboIsCheckDays').getValue(),
			checkDays = buttonsToolbar2.getComponent('CheckDays').getValue();
		if(isCheckDays && checkDays) {
			days = parseInt(checkDays);
			if(days < 0) days = 0;
		}
		//入库时间范围作为盘库数据过滤条件，添加过滤选择项：包含最近___天内库存数为0的货品（生效的前提是盘库条件的“包括库存数为0的试剂勾选上”）;
		//var cboIsHasZeroQty= buttonsToolbar2.getComponent('cboIsHasZeroQty').getValue();
		var zeroQtyDays= buttonsToolbar2.getComponent('ZeroQtyDays').getValue();
		if(zeroQtyDays) {
			zeroQtyDays = parseInt(zeroQtyDays);
		}
		if(!zeroQtyDays)zeroQtyDays=0;
		
		if(!me.docEntity) me.docEntity = "";
		url += '&docEntity=' + JShell.String.encode(me.docEntity);
		url += '&reaGoodHql=' + JShell.String.encode(me.reaGoodHql);
		url += '&days=' + days;
		url += '&zeroQtyDays=' + zeroQtyDays;
		return url;
	},
	/**获取机构货品条件*/
	getReaGoodHql: function() {
		var me = this,
			where = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = buttonsToolbar.getComponent('Search'),
			barCodeMgr = buttonsToolbar.getComponent('BarCodeMgr');
		if(barCodeMgr) {
			var value = barCodeMgr.getValue();
			if(value) {
				where.push("reagoods.BarCodeMgr=" + value);
			}
		}
		if(search) {
			var value = search.getValue();
			var searchHql = me.getSearchWhere(value);
			if(searchHql) {
				searchHql = "(" + searchHql + ")";
				where.push(searchHql);
			}
		}
		where = where.join(" and ");
		return where;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		me.fireEvent('onBeforeSearch', me);
	},
	/**加载左列表数据*/
	loadData: function(params) {
		var me = this;
		me.docEntity = params.docEntity;
		me.reaGoodHql = params.reaGoodHql;
		me.load(null, null, true);
	},
	/**确定按钮处理*/
	onAcceptClick: function() {
		var me = this;
		var records = [];
		me.store.each(function(rec) {
			records.push(rec);
		});
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.fireEvent('onAccept', me, records);
	}
});