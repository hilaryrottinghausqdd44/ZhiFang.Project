/**
 * 库存结转
 * @author longfc
 * @version 2018-04-13
 */
Ext.define('Shell.class.rea.client.qtybalance.DocGrid', {
	extend: 'Shell.class.rea.client.SearchGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '库存结转',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyBalanceDocByHQL?isPlanish=true',
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
		property: 'ReaBmsQtyBalanceDoc_DataAddTime',
		direction: 'DESC'
	}],
	/**是否有收缩面板按钮*/
	hasCollapse: false,
	/**用户UI配置Key*/
	userUIKey: 'qtybalance.DocGrid',
	/**用户UI配置Name*/
	userUIName: "库存结转列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(-30);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			emptyText: '结转单号/操作人',
			itemId: 'Search',
			//flex: 1,
			width: '70%',
			isLike: true,
			fields: ['reabmsqtybalancedoc.QtyBalanceDocNo', 'reabmsqtybalancedoc.OperName']
		};
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
			dataIndex: 'ReaBmsQtyBalanceDoc_PreBalanceDateTime',
			text: '上次结转日期',
			width: 135,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDoc_PreQtyBalanceDocNo',
			text: '上次结转单号',
			width: 130,
			hidden:true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDoc_DataAddTime',
			text: '结转日期',
			width: 135,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDoc_QtyBalanceDocNo',
			text: '结转单号',
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDoc_Visible',
			text: '启用',
			width: 50,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDoc_OperName',
			text: '操作人',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDoc_Id',
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
		items.push(me.createButtonsToolbarSearch());
		return items;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		items.push("-", {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnAdd",
			text: '新增',
			tooltip: '新增库存结转',
			handler: function() {
				me.onAddClick();
			}
		});
		items.push('-',{
			xtype: 'button',
			itemId: 'btnEdit',
			iconCls: 'button-edit',
			text: "启用",
			tooltip: "对选择禁用的库存结转单启用",
			handler: function() {
				me.onSetVisibleClick(true);
			}
		}, {
			xtype: 'button',
			itemId: 'btnDelete',
			iconCls: 'button-del',
			text: "禁用",
			tooltip: "对选择库存结转单禁用",
			handler: function() {
				me.onSetVisibleClick(false);
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

		var buttonsToolbarSearch = me.getComponent('buttonsToolbarSearch'),
			visible = buttonsToolbarSearch.getComponent('cboISVisible');

		var dateareaToolbar = me.getComponent('dateareaToolbar')
		var date = dateareaToolbar.getComponent('date');

		var search = buttonsToolbarSearch.getComponent('Search');
		var where = [];

		if(date) {
			var dateValue = date.getValue();
			if(dateValue) {
				if(dateValue.start) {
					where.push('reabmsqtybalancedoc.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push('reabmsqtybalancedoc.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
		if(visible) {
			var value = visible.getValue();
			if(value == true || value == 1) value = "1";
			else if(value == false || value == 0) value = "0";
			if(value)
				where.push("reabmsqtybalancedoc.Visible=" + value);
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
	onShowOperation: function(record) {},
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
	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	/**@description 启用/禁用库存结转单处理*/
	onSetVisibleClick: function(value) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var visible = records[0].get("ReaBmsQtyBalanceDoc_Visible");
		if(visible == "true" || visible == true || visible == "1" || visible == 1) visible = true;
		else visible = false;
		var info = value == true ? "启用" : "禁用";
		if(visible == value) {
			JShell.Msg.alert("当前库存结转单已是" + info+"状态!", null, 2000);
			return;
		}

		JShell.Msg.confirm({
			title: '<div style="text-align:center;">操作确认</div>',
			msg: "将选择的库存结转单的启用状态设置为:"+info+"!<br />请确认是否继续执行?按【确定】按钮继续执行",
			closable: false,
			multiline: false
		}, function(but, text) {
			if(but != "ok") return;
			var id = records[0].get(me.PKField);
			me.onSetVisible(id, value);
		});
	},
	/**@description 取消库存结转单*/
	onSetVisible: function(id, visible) {
		var me = this;
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_UpdateVisibleReaBmsQtyBalanceDocById?id=" + id + "&visible=" + visible);
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				me.onSearch();
			} else {
				JShell.Msg.error('更新库存结转单出错！' + data.msg);
			}
		});
	}
});