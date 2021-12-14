/**
 * 实验室试剂-采购统计
 * @author longfc
 * @version 2018-09-30
 */
Ext.define('Shell.class.rea.client.reasale.lab.statis.GridRight', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	title: '实验室试剂-采购统计-明细',
	requires: [
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger'
	],

	/**获取数据服务路径*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchReaBmsCenSaleDtlSummaryByHQL?isPlanish=true',

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**序号列宽度*/
	rowNumbererWidth: 35,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsCenSaleDoc_OperDate',
		direction: 'DESC'
	}],

	/**开始时间*/
	Start: null,
	/**结束时间*/
	End: null,
	/**试剂ID*/
	GoodsID: null,
	/**供应商ID*/
	CompID: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('toMaxClick', 'toMinClick');

		var end = new Date();
		var start = JShell.Date.getNextDate(end, -30);

		me.buttonToolbarItems = [{
			itemId: 'toMaxClick',
			iconCls: 'button-right',
			text: '放大',
			tooltip: '<b>放大</b>',
			handler: function() {
				this.hide();
				me.fireEvent('toMaxClick', me);
				this.ownerCt.getComponent('toMinClick').show();
			}
		}, {
			itemId: 'toMinClick',
			iconCls: 'button-left',
			text: '还原',
			tooltip: '<b>还原</b>',
			hidden: true,
			handler: function() {
				this.hide();
				me.fireEvent('toMinClick', me);
				this.ownerCt.getComponent('toMaxClick').show();
			}
		}, '-', 'refresh', '-',{
			width: 160,
			labelWidth: 60,
			labelAlign: 'right',
			xtype: 'textfield',
			itemId: 'GoodsLotNo',
			fieldLabel: '货品批号',
			enableKeyEvents: true,
			listeners: {
				keyup: function(field, e) {
					if(e.getKey() == Ext.EventObject.ESC) {
						field.setValue('');
						me.onSearch();
					} else if(e.getKey() == Ext.EventObject.ENTER) {
						me.onSearch();
					}
				}
			}
		}, '-', 'searchb'];

		//创建数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsCenSaleDtl_ReaBmsCenSaleDoc_OperDate',
			sortable: false,
			text: '采购日期',
			align: 'center',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ReaBmsCenSaleDoc_CompanyName',
			sortable: false,
			text: '供应商',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_LotNo',
			sortable: false,
			text: '货品批号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_GoodsQty',
			sortable: false,
			text: '采购数量',
			align: 'right',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_SumTotal',
			sortable: false,
			text: '采购金额',
			align: 'right',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ReaBmsCenSaleDoc_SaleDocNo',
			sortable: false,
			text: '供货单号',
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtl_ReaBmsCenSaleDoc_Id',
			sortable: false,
			text: '主单主键ID',
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);
		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		if(!docHql) {
			docHql = "";
		}
		if(!dtlHql) {
			dtlHql = "";
		}
		url += (url.indexOf('?') == -1 ? '?' : '&') + "docHql=" + JShell.String.encode(docHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "dtlHql=" + JShell.String.encode(dtlHql);
		url += "&groupType=2";
		return url;
	},
	/**获取主单查询条件*/
	getParamsDocHql: function() {
		var me = this;
		var docHql = [];

		if(me.Start) {
			var start = Ext.Date.format(me.Start, "Y-m-d");
			if(start) {
				docHql.push("reabmscensaledoc.DataAddTime>='" + start + " 00:00:00'");
			}
		}
		if(me.End) {
			var end = Ext.Date.format(me.End, "Y-m-d");
			if(end) {
				docHql.push("reabmscensaledoc.DataAddTime<='" + end + " 23:59:59'");
			}
		}
		if(me.CompID) {
			var value = me.CompID;
			if(value) {
				docHql.push("reabmscensaledoc.CompID=" + value + "");
			}
		}
		if(docHql && docHql.length > 0) {
			docHql = docHql.join(" and ");
		} else {
			docHql = "";
		}
		return docHql;
	},
	/**获取明细查询条件*/
	getParamsDtlHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			GoodsLotNo = buttonsToolbar.getComponent('GoodsLotNo');
		var dtlHql = [];

		if(me.GoodsID) {
			var value = me.GoodsID;
			if(value) {
				dtlHql.push("reabmscensaledtl.ReaGoodsID=" + value + "");
			}
		}
		if(GoodsLotNo) {
			var value = GoodsLotNo.getValue();
			if(value) {
				url += "reabmscensaledtl.LotNo='" + value + "'";
			}
		}
		if(dtlHql && dtlHql.length > 0) {
			dtlHql = dtlHql.join(" and ");
		} else {
			dtlHql = "";
		}
		return dtlHql;
	},
	/**查询*/
	onSearchBClick: function() {
		this.onSearch();
	},
	/**
	 * @public 对外查询
	 * @param {Object} Start 开始时间
	 * @param {Object} End 结束时间
	 * @param {Object} GoodsID 试剂ID
	 * @param {Object} CompID 供应商ID
	 */
	onSearchByParams: function(Start, End, GoodsID, CompID) {
		var me = this;
		me.Start = Start;
		me.End = End;
		me.GoodsID = GoodsID;
		me.CompID = CompID;
		me.onSearch();
	}
});