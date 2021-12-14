/**
 * @description 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.basic.DocGrid', {
	extend: 'Shell.class.rea.client.SearchGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '供货信息',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**状态查询按钮选中值*/
	searchStatusValue: null,
	/**是否有收缩面板按钮*/
	hasCollapse: false,
	/**客户端供货单及明细状态Key*/
	StatusKey: "ReaBmsCenSaleDocAndDtlStatus",
	/**客户端供货单及明细数据标志Key*/
	IOFlagKey: "ReaBmsCenSaleDocIOFlag",
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsCenSaleDoc_DataAddTime',
		direction: 'DESC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			emptyText: '供货单号/发票号',
			itemId: 'Search',
			//flex: 1,
			width: "72%",
			isLike: true,
			fields: ['reabmscensaledoc.SaleDocNo', 'reabmscensaledoc.InvoiceNo']
		};
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.IOFlagKey, false, true, null);
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsCenSaleDoc_DataAddTime',
			text: '供货日期',
			align: 'center',
			width: 95,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_Status',
			text: '单据状态',
			width: 90,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.StatusKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.StatusKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.StatusKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.StatusKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.StatusKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.StatusKey].FColor[value];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_SaleDocNo',
			text: '供货单号',
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_OrderDocNo',
			text: '订货单号',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_TotalPrice',
			text: '总价',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_UrgentFlag',
			text: '紧急标志',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_UrgentFlag['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_IOFlag',
			text: '数据标志',
			align: 'center',
			width: 65,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.IOFlagKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.IOFlagKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.IOFlagKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.IOFlagKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.IOFlagKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.IOFlagKey].FColor[value];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_CompanyName',
			text: '供货商',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_CompID',
			text: '供货商主键ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_ReaCompCode',
			text: '供货商编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_ReaServerCompCode',
			text: '供货商平台编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_LabcName',
			text: '订货方',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_LabcID',
			text: '订货方主键ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_ReaLabcCode',
			text: '订货方编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_ReaServerLabcCode',
			text: '订货方平台编码',
			hidden: true,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}];

		return columns;
	},
	onShowOperation: function(record) {
		var me = this;
		if(!record) {
			var records = me.getSelectionModel().getSelection();
			if(records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get("ReaBmsCenSaleDoc_Id");
		var config = {
			title: '供货操作记录',
			resizable: true,
			width: 428,
			height: 450,
			PK: id,
			className: me.StatusKey //类名
		};
		var win = JShell.Win.open('Shell.class.rea.client.reareqoperation.Panel', config);
		win.show();
	},
	/**查询输入栏*/
	createButtonsToolbarSearch: function() {
		var me = this;
		var items = ['refresh', '-', ];

		items.push({
			type: 'search',
			info: me.searchInfo
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbarSearch',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createDateAreaToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'uxdatearea',
			itemId: 'date',
			labelWidth: 55,
			labelAlign: 'right',
			fieldLabel: '日期范围',
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'dateareaToolbar',
			items: items
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		var buttonsToolbarSearch = me.getComponent('buttonsToolbarSearch'),
			dateareaToolbar = me.getComponent('dateareaToolbar'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2');

		var status = buttonsToolbar2.getComponent('DocStatus'),
			urgentFlag = buttonsToolbar2.getComponent('DocUrgentFlag'),
			deleteFlag = buttonsToolbar2.getComponent('DeleteFlag'),
			iOFlag = buttonsToolbar2.getComponent('IOFlag');

		var search = buttonsToolbarSearch.getComponent('Search');
		var date = dateareaToolbar.getComponent('date');

		var where = [];
		if(urgentFlag) {
			var value = urgentFlag.getValue();
			if(value) {
				where.push("reabmscensaledoc.UrgentFlag=" + value);
			}
		}
		if(status) {
			var value = status.getValue();
			if(value) {
				where.push("reabmscensaledoc.Status=" + value);
			}
		}
		if(deleteFlag) {
			var value = deleteFlag.getValue();
			if(value) {
				where.push("reabmscensaledoc.DeleteFlag=" + value);
			}
		}
		if(iOFlag) {
			var value = iOFlag.getValue();
			if(value) {
				where.push("reabmscensaledoc.IOFlag=" + value);
			}
		}

		if(me.searchStatusValue != null && parseInt(me.searchStatusValue) > -1)
			where.push("reabmscensaledoc.Status=" + me.searchStatusValue);

		if(date) {
			var dateValue = date.getValue();
			if(dateValue) {
				if(dateValue.start) {
					where.push('reabmscensaledoc.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push('reabmscensaledoc.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
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
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		return tempList;
	},
	/**@description 验证日期类型是否选择*/
	validDateType: function() {
		var me = this;
		return true;
	},
	/**@description 设置日期范围值*/
	onSetDateArea: function(day) {
		var me = this;
		var dateAreaValue = me.calcDateArea(day);
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateAreaValue) date.setValue(dateAreaValue);
	},
	/**初始化日期范围*/
	initDateArea: function(day) {
		var me = this;
		if(!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		//sdate=Ext.Date.format(sdate,"Y-m-d");
		//edate=Ext.Date.format(edate,"Y-m-d");
		var dateArea = {
			start: sdate,
			end: edate
		};
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateArea) date.setValue(dateArea);
	}
});