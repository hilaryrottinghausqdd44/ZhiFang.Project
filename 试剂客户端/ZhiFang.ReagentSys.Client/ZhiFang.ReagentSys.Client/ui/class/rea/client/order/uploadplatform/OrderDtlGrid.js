/**
 * @description 订单上传
 * @author longfc
 * @version 2017-11-21
 */
Ext.define('Shell.class.rea.client.order.uploadplatform.OrderDtlGrid', {
	extend: 'Shell.class.rea.client.order.basic.OrderDtlGrid',
	title: '订货明细列表',

	/**当前选择的供应商Id*/
	ReaCompID: null,
	/**录入:entry/审核:check*/
	OTYPE: "upload",
	/**是否多选行*/
	checkOne: true,
	/**隐藏货品平台编码列*/
	hiddenGoodsNo: false,
	/**用户UI配置Key*/
	userUIKey: 'order.uploadplatform.OrderDtlGrid',
	/**用户UI配置Name*/
	userUIName: "订单上传明细列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		if (!me.checkOne) me.setCheckboxModel();
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品中文名/货品平台编码',
			fields: ['reabmscenorderdtl.ReaGoodsName', 'reabmscenorderdtl.GoodsNo']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.push({
			dataIndex: 'ReaBmsCenOrderDtl_LabID',
			text: 'LabID',
			hidden: true,
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ProdID',
			text: '厂商Id',
			hidden: true,
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_OrderDtlNo',
			text: '订货明细单号',
			hidden: true,
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_OrderDocID',
			text: '订货总单ID',
			hidden: true,
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_OrderDocNo',
			text: '订货总单号',
			hidden: true,
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ZX1',
			text: '专项1',
			hidden: true,
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ZX2',
			text: '专项3',
			hidden: true,
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ZX3',
			text: '专项3',
			hidden: true,
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_IOFlag',
			text: '提取标志',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				if (!value) value = 0;
				if (value) value = parseInt(value);
				var info = JShell.REA.Enum.ReaBmsCenOrderDoc_IOFlag['E' + value] || {};
				var v = info || value;
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';

				return v;
			}
		});
		return columns;
	},
	setGoodstemplateClassConfig: function() {
		var me = this;
	}
});
