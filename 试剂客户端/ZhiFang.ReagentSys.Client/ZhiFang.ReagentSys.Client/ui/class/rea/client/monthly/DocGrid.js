/**
 * 库存结转报表
 * @author longfc
 * @version 2018-04-13
 */
Ext.define('Shell.class.rea.client.monthly.DocGrid', {
	//extend: 'Shell.ux.grid.Panel',
	extend: 'Shell.class.rea.client.SearchGrid',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.YearAndMonthComboBox',
		'Shell.ux.form.field.DateArea'
	],
	title: '库存结转报表',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyMonthBalanceDocByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsQtyMonthBalanceDoc_DataAddTime',
		direction: 'DESC'
	}],
	/**是否有收缩面板按钮*/
	hasCollapse: false,
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
	/**用户UI配置Key*/
	userUIKey: 'monthly.DocGrid',
	/**用户UI配置Name*/
	userUIName: "库存结转报表列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(-30);
	},
	initComponent: function() {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		me.maxYearValue = Sysdate.getFullYear();
		me.roundMaxValue = Ext.util.Format.date(Sysdate, "Y-m");
		me.roundMinValue = me.minYearValue + "-01";

		//查询框信息
		me.searchInfo = {
			emptyText: '结转周期/报表单号/操作人',
			itemId: 'Search',
			//flex: 1,
			width: "72%",
			isLike: true,
			fields: ['reabmsqtymonthbalancedoc.Round', 'reabmsqtymonthbalancedoc.QtyMonthBalanceDocNo', 'reabmsqtymonthbalancedoc.OperName']
		};
		JShell.REA.StatusList.getStatusList(me.TypeIDKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.StatisticalTypeIDKey, false, true, null);
		//创建功能按钮栏Items
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
			dataIndex: 'ReaBmsQtyMonthBalanceDoc_TypeID',
			text: '结转类型',
			width: 80,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.TypeIDKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.TypeIDKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.TypeIDKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.TypeIDKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.TypeIDKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.TypeIDKey].FColor[value];
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
			dataIndex: 'ReaBmsQtyMonthBalanceDoc_StartDate',
			text: '起始日期',
			width: 135,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsQtyMonthBalanceDoc_EndDate',
			text: '结束日期',
			width: 135,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsQtyMonthBalanceDoc_Visible',
			text: '启用',
			width: 50,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyMonthBalanceDoc_StatisticalTypeID',
			text: '合并方式',
			width: 140,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.StatisticalTypeIDKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.StatisticalTypeIDKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.StatisticalTypeIDKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.StatisticalTypeIDKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.StatisticalTypeIDKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.StatisticalTypeIDKey].FColor[value];
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
			dataIndex: 'ReaBmsQtyMonthBalanceDoc_StatisticalTypeName',
			text: '合并方式',
			width: 140,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyMonthBalanceDoc_QtyBalanceDocNo',
			text: '结转单号',
			hidden: true,
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyMonthBalanceDoc_StorageName',
			text: '库房',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyMonthBalanceDoc_PlaceName',
			text: '货架',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyMonthBalanceDoc_QtyMonthBalanceDocNo',
			text: '结转单号',
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyMonthBalanceDoc_OperName',
			text: '操作人',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyMonthBalanceDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());

		items.push(me.createQuickSearchButtonToolbar());
		items.push(me.createDateAreaToolbarItems());
		items.push(me.createButtonToolbarItems3());
		items.push(me.createButtonsToolbarSearch());
		return items;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		items.push({
			width: 80,
			iconCls: 'button-add',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '新增报表',
			tooltip: '<b>按照自然月去制单</b>',
			handler: function() {
				me.onAddClick();
			}
		});
		items.push({
			xtype: 'button',
			itemId: 'btnDelete',
			iconCls: 'button-del',
			text: "取消报表",
			tooltip: "对选择月结单禁用",
			handler: function() {
				me.onCancelClick();
			}
		});
		items.push("-", {
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: '预览',
			tooltip: '预览打印结转报表',
			handler: function() {
				me.onPrintClick();
			}
		});
		items.push('->', {
			iconCls: 'button-right',
			tooltip: '<b>收缩面板</b>',
			handler: function() {
				me.collapse();
			}
		});
		return items;
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
	/**查询输入栏*/
	createButtonsToolbarSearch: function() {
		var me = this;
		var items = [];
		items.push({
			fieldLabel: '',
			labelWidth: 0,
			width: 75,
			hasStyle: true,
			hasAll: true,
			xtype: 'uxBoolComboBox',
			value: true,
			itemId: 'cboISVisible',
			emptyText: '是否启用',
			style: {
				marginLeft: "5px"
			},
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push('-', {
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
	createButtonToolbarItems3: function() {
		var me = this;
		var items = [];
		items.push({
			fieldLabel: '结转类型',
			labelWidth: 65,
			width: 170,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DocBalanceDocType',
			emptyText: '结转类型选择',
			data: JShell.REA.StatusList.Status[me.TypeIDKey].List,
			value: "",
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push({
			width: 95,
			labelWidth: 0,
			fieldLabel: '',
			xtype: 'uxYearAndMonthComboBox',
			itemId: 'BonusFormRound',
			minYearValue: me.minYearValue,
			maxYearValue: me.maxYearValue,
			minValue: me.roundMinValue,
			maxValue: me.roundMaxValue,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && newValue != null && newValue != "") {
						setTimeout(function() {

						}, 500);
					}
				}
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar3',
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

		var buttonsToolbar3 = me.getComponent('buttonsToolbar3');
		var balanceDocType = buttonsToolbar3.getComponent('DocBalanceDocType');
		var bonusFormRound = buttonsToolbar3.getComponent('BonusFormRound');

		var buttonsToolbarSearch = me.getComponent('buttonsToolbarSearch');
		var search = buttonsToolbarSearch.getComponent('Search');
		var visible = buttonsToolbarSearch.getComponent('cboISVisible');

		var dateareaToolbar = me.getComponent('dateareaToolbar')
		var date = dateareaToolbar.getComponent('date');

		var date = "";
		var where = [];
		//取库存结转单为空的报表
		where.push("reabmsqtymonthbalancedoc.QtyBalanceDocID is not null");
		if(visible) {
			var value = visible.getValue();
			if(value == true || value == 1) value = "1";
			else if(value == false || value == 0) value = "0";
			if(value)
				where.push("reabmsqtymonthbalancedoc.Visible=" + value);
		}
		if(balanceDocType) {
			var value = balanceDocType.getValue();
			if(value)
				where.push("reabmsqtymonthbalancedoc.TypeID=" + value);
		}
		if(bonusFormRound) {
			var value = bonusFormRound.getValue();
			if(value)
				where.push("reabmsqtymonthbalancedoc.Round='" + value + "'");
		}
		if(date) {
			var dateValue = date.getValue();
			if(dateValue) {
				if(dateValue.start) {
					where.push("reabmsqtymonthbalancedoc.StartDate>='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push("reabmsqtymonthbalancedoc.EndDate<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
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
		return where.join(" and ");
	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	/**@description 取消月结单按钮*/
	onCancelClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var visible = records[0].get("ReaBmsQtyMonthBalanceDoc_Visible");
		if(visible == "true" || visible == true || visible == "1" || visible == 1) visible = true;
		else visible = false;

		if(visible == false) {
			JShell.Msg.alert("当前结转报表已被禁用!", null, 2000);
			return;
		}
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">取消结转报表确认</div>',
			msg: '将选择的结转报表的禁用!<br />请确认是否继续执行?按【确定】按钮继续执行',
			closable: false,
			multiline: false
		}, function(but, text) {
			if(but != "ok") return;
			var id = records[0].get(me.PKField);
			me.onCancel(id);
		});
	},
	/**@description 取消月结单*/
	onCancel: function(id) {
		var me = this;
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/ST_UDTO_UpdateCancelReaBmsQtyMonthBalanceDocById?id=" + id);
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				me.onSearch();
			} else {
				JShell.Msg.error('取消结转报表出错！' + data.msg);
			}
		});
	},
	/**@description 打印月结单*/
	onPrintClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		if(!id) {
			JShell.Msg.error("请先保存打印结转报表后,再打印结转报表!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_GetQtyMonthBalanceAndDtlOfPdf");
		url += '?operateType=1&id=' + id;
		window.open(url);
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