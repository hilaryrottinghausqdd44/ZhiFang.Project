/**
 * 结转报表
 * @author longfc
 * @version 2018-04-13
 */
Ext.define('Shell.class.rea.client.monthly.basic.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.YearAndMonthComboBox'
	],
	title: '结转报表',
	formtype: 'show',
	width: 680,
	height: 155,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyMonthBalanceDocById?isPlanish=true',

	buttonDock: "top",
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**内容周围距离*/
	bodyPadding: '5px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 70,
		width: 195,
		labelAlign: 'right'
	},

	/**月结最小年份*/
	minYearValue: 2018,
	/**月结最大年份*/
	maxYearValue: 2018,
	/**月结最小选择项*/
	roundMinValue: null,
	/**月结最大选择项*/
	roundMaxValue: null,
	/**月结类型*/
	TypeIDKey: "ReaBmsQtyMonthBalanceDocType",
	/**月结库存货品合并方式*/
	StatisticalTypeIDKey: "ReaBmsQtyMonthBalanceDocStatisticalType",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 185) me.defaults.width = 185;

		JShell.REA.StatusList.getStatusList(me.TypeIDKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.StatisticalTypeIDKey, false, true, null);

		var Sysdate = JcallShell.System.Date.getDate();
		me.maxYearValue = Sysdate.getFullYear();
		me.roundMaxValue = Ext.util.Format.date(Sysdate, "Y-m");
		me.roundMinValue = me.minYearValue + "-01";

		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '结转单号',
			emptyText: '库存结转单选择',
			name: 'ReaBmsQtyMonthBalanceDoc_QtyBalanceDocNo',
			itemId: 'ReaBmsQtyMonthBalanceDoc_QtyBalanceDocNo',
			colspan: 2,
			width: me.defaults.width * 2,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			emptyText: '必填项',
			allowBlank: false,
			className: 'Shell.class.rea.client.qtybalance.CheckGrid',
			classConfig: {
				title: '库存结转选择',
				checkOne: true,
				width: 480,
			},
			listeners: {
				check: function(p, record) {
					me.onQtyBalanceDocAccept(p, record);
				}
			}
		}, {
			xtype: 'textfield',
			itemId: 'ReaBmsQtyMonthBalanceDoc_QtyBalanceDocID',
			name: 'ReaBmsQtyMonthBalanceDoc_QtyBalanceDocID',
			fieldLabel: '库存结转ID',
			hidden: true
		});
		//月结周期
		items.push({
			fieldLabel: '结转周期',
			name: 'ReaBmsQtyMonthBalanceDoc_Round',
			itemId: 'ReaBmsQtyMonthBalanceDoc_Round',
			colspan: 1,
			width: me.defaults.width * 1,
			xtype: 'uxYearAndMonthComboBox',
			minYearValue: me.minYearValue,
			maxYearValue: me.maxYearValue,
			minValue: me.roundMinValue,
			maxValue: me.roundMaxValue,
			hidden: true
		}, {
			//xtype: 'datefield',
			fieldLabel: '起始日期',
			//format: 'Y-m-d H:m:s',
			name: 'ReaBmsQtyMonthBalanceDoc_StartDate',
			itemId: 'ReaBmsQtyMonthBalanceDoc_StartDate',
			colspan: 1,
			readOnly: true,
			locked: true,
			width: me.defaults.width * 1
		}, {
			//xtype: 'datefield',
			fieldLabel: '结束日期',
			//format: 'Y-m-d H:m:s',// 
			name: 'ReaBmsQtyMonthBalanceDoc_EndDate',
			itemId: 'ReaBmsQtyMonthBalanceDoc_EndDate',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//月结类型
		items.push({
			fieldLabel: '结转类型',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsQtyMonthBalanceDoc_TypeID',
			itemId: 'ReaBmsQtyMonthBalanceDoc_TypeID',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.TypeIDKey].List,
			colspan: 1,
			width: me.defaults.width * 1,
			emptyText: '必填项',
			allowBlank: false,
			listeners: {
				select: function(com, records, eOpts) {
					if(me.formtype != "show") {
						me.onTypeSelect(com, records);
					}
				}
			}
		});
		//月结类型
		items.push({
			fieldLabel: '合并方式',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsQtyMonthBalanceDoc_StatisticalTypeID',
			itemId: 'ReaBmsQtyMonthBalanceDoc_StatisticalTypeID',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.StatisticalTypeIDKey].List,
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		});
		//启用
		items.push({
			fieldLabel: '启用',
			name: 'ReaBmsQtyMonthBalanceDoc_Visible',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		items.push({
			fieldLabel: '库房选择',
			emptyText: '库房选择',
			name: 'ReaBmsQtyMonthBalanceDoc_StorageName',
			itemId: 'ReaBmsQtyMonthBalanceDoc_StorageName',
			colspan: 1,
			width: me.defaults.width * 1,
			xtype: 'uxCheckTrigger',
			readOnly: true,
			locked: true,
			className: 'Shell.class.rea.client.shelves.storage.CheckGrid',
			classConfig: {
				title: '库房选择',
				checkOne: true,
				width: 300
			},
			listeners: {
				check: function(p, record) {
					me.onStorageAccept(record);
					p.close();
				}
			}
		}, {
			xtype: 'textfield',
			itemId: 'ReaBmsQtyMonthBalanceDoc_StorageID',
			name: 'ReaBmsQtyMonthBalanceDoc_StorageID',
			fieldLabel: '库房ID',
			hidden: true
		});
		items.push({
			fieldLabel: '货架选择',
			emptyText: '货架选择',
			name: 'ReaBmsQtyMonthBalanceDoc_PlaceName',
			itemId: 'ReaBmsQtyMonthBalanceDoc_PlaceName',
			colspan: 1,
			width: me.defaults.width * 1,
			xtype: 'uxCheckTrigger',
			readOnly: true,
			locked: true,
			className: 'Shell.class.rea.client.shelves.place.CheckGrid',
			classConfig: {
				title: '货架选择',
				/**是否单选*/
				checkOne: true,
				width: 300
			},
			listeners: {
				check: function(p, record) {
					me.onPlaceAccept(record);
					p.close();
				}
			}
		}, {
			xtype: 'textfield',
			itemId: 'ReaBmsQtyMonthBalanceDoc_PlaceID',
			name: 'ReaBmsQtyMonthBalanceDoc_PlaceID',
			fieldLabel: '货架ID',
			hidden: true
		});

		//操作者
		items.push({
			fieldLabel: '操作人',
			name: 'ReaBmsQtyMonthBalanceDoc_CreaterName',
			itemId: 'ReaBmsQtyMonthBalanceDoc_CreaterName',
			colspan: 1,
			width: me.defaults.width * 1,
			//hidden:true,
			readOnly: true,
			locked: true
		});
		//操作日期
		items.push({
			//xtype: 'datefield',
			fieldLabel: '操作日期',
			//format: 'Y-m-d',
			name: 'ReaBmsQtyMonthBalanceDoc_OperDate',
			itemId: 'ReaBmsQtyMonthBalanceDoc_OperDate',
			colspan: 1,
			width: me.defaults.width * 1,
			//hidden: true,
			readOnly: true,
			locked: true
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsQtyMonthBalanceDoc_Id',
			hidden: true,
			type: 'key'
		});

		items.push({
			fieldLabel: '一级分类',
			emptyText: '一级分类',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaBmsQtyMonthBalanceDoc_GoodsClass',
			name: 'ReaBmsQtyMonthBalanceDoc_GoodsClass',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '一级分类',
				ClassType: "GoodsClass"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'ReaBmsQtyMonthBalanceDoc_GoodsClass');
				}
			}
		}, {
			fieldLabel: '二级分类',
			emptyText: '二级分类',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaBmsQtyMonthBalanceDoc_GoodsClassType',
			name: 'ReaBmsQtyMonthBalanceDoc_GoodsClassType',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '二级分类',
				ClassType: "GoodsClassType"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'ReaBmsQtyMonthBalanceDoc_GoodsClassType');
				}
			}
		});
		//报表单号
		items.push({
			fieldLabel: '报表单号',
			name: 'ReaBmsQtyMonthBalanceDoc_QtyMonthBalanceDocNo',
			colspan: 2,
			width: me.defaults.width * 2,
			//hidden:true,
			readOnly: true,
			locked: true
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsQtyMonthBalanceDoc_Memo',
			itemId: 'ReaBmsQtyMonthBalanceDoc_Memo',
			colspan: 4,
			width: me.defaults.width * 4,
			height: 38
		});
		return items;
	},
	/**@desc 一级分类/二级分类选择*/
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var classTypeCom = me.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var DataAddTime = data.ReaBmsQtyMonthBalanceDoc_DataAddTime;
		var OperDate = data.ReaBmsQtyMonthBalanceDoc_OperDate;
		if(OperDate) data.ReaBmsQtyMonthBalanceDoc_OperDate = JcallShell.Date.toString(OperDate);
		if(DataAddTime) data.ReaBmsQtyMonthBalanceDoc_DataAddTime = JcallShell.Date.toString(DataAddTime);

		var StartDate = data.ReaBmsQtyMonthBalanceDoc_StartDate;
		var EndDate = data.ReaBmsQtyMonthBalanceDoc_EndDate;
		if(StartDate) data.ReaBmsQtyMonthBalanceDoc_StartDate = JcallShell.Date.toString(StartDate);
		if(EndDate) data.ReaBmsQtyMonthBalanceDoc_EndDate = JcallShell.Date.toString(EndDate);

		var reg = new RegExp("<br />", "g");
		data.ReaBmsQtyMonthBalanceDoc_Memo = data.ReaBmsQtyMonthBalanceDoc_Memo.replace(reg, "\r\n");
		var visible = data.ReaBmsQtyMonthBalanceDoc_Visible;
		if(visible == "1" || visible == 1 || visible == "true" || visible == true) visible = true;
		else visible = false;
		data.ReaBmsQtyMonthBalanceDoc_Visible = visible;
		return data;
	},
	/**@description 库存结转选择后处理*/
	onQtyBalanceDocAccept: function(p, record) {
		var me = this;
		var QtyBalanceDocNo = me.getComponent('ReaBmsQtyMonthBalanceDoc_QtyBalanceDocNo');
		var QtyBalanceDocID = me.getComponent('ReaBmsQtyMonthBalanceDoc_QtyBalanceDocID');
		var StartDate = me.getComponent('ReaBmsQtyMonthBalanceDoc_StartDate');
		var EndDate = me.getComponent('ReaBmsQtyMonthBalanceDoc_EndDate');

		var docNoValue = record ? record.get('ReaBmsQtyBalanceDoc_QtyBalanceDocNo') : '';
		var docIDValeue = record ? record.get('ReaBmsQtyBalanceDoc_Id') : '';
		//上次结转日期
		var startDateV = record ? record.get('ReaBmsQtyBalanceDoc_PreBalanceDateTime') : '';
		if(startDateV) {
			startDateV = JcallShell.Date.toString(startDateV);
		}
		//结转单的结转日期
		var endDateV = record ? record.get('ReaBmsQtyBalanceDoc_DataAddTime') : '';
		if(endDateV) {
			endDateV = JcallShell.Date.toString(endDateV);
		}
		QtyBalanceDocNo.setValue(docNoValue);
		QtyBalanceDocID.setValue(docIDValeue);
		StartDate.setValue(startDateV);
		EndDate.setValue(endDateV);
	},
	/**@description 月结类型选择后处理*/
	onTypeSelect: function(com, records) {

	},
	/****@description库房选择*/
	onStorageAccept: function(record) {
		var me = this;
	},
	/**货架选择*/
	onPlaceAccept: function(record) {
		var me = this;
	}
});