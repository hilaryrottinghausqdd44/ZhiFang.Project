/**
 * @description 部门采购申请录入申请主单列表
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.basic.ApplyGrid', {
	extend: 'Shell.class.rea.client.SearchGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	
	title: '申请信息',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDocByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsReqDoc',
	/**修改服务地址*/
	editUrl: '/ReaManageService.svc/ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck',
	
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认加载数据*/
	defaultLoad: false,
	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "entry",

	/**下拉状态默认值*/
	defaultStatusValue: "",
	/**是否多选行*/
	checkOne: true,
	/**申请单状态Key*/
	StatusKey: "ReaBmsReqDocStatus",
	
	defaultOrderBy: [{
		property: 'ReaBmsReqDoc_ApplyTime',
		direction: 'DESC'
	}, {
		property: 'ReaBmsReqDoc_Status',
		direction: 'ASC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 255,
			isLike: true,
			itemId: 'search',
			emptyText: '申请人/申请单号',
			fields: ['reabmsreqdoc.ApplyName', 'reabmsreqdoc.ReqDocNo']
		};
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建挂靠功能栏
		me.dockedItems = []; //me.createDockedItems();
		me.dockedItems.push(me.createQuickSearchButtonToolbar());
		me.dockedItems.push(me.createDateAreaToolbarItems());
		me.dockedItems.push(me.createButtonToolbarItems2());
		me.dockedItems.push(me.createDockedItems());
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
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsReqDoc_Id',
			text: '主键ID',
			hidden: true,
			isKey: true
		}, {
			dataIndex: 'ReaBmsReqDoc_DeptID',
			text: '机构主键ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDoc_ApplyTime',
			text: '申请日期',
			align: 'center',
			width: 75,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'ReaBmsReqDoc_DeptName',
			text: '所属部门',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDoc_Status',
			text: '单据状态',
			align: 'center',
			width: 75,
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
			dataIndex: 'ReaBmsReqDoc_UrgentFlag',
			text: '标志',
			align: 'center',
			width: 40,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_UrgentFlag['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsReqDoc_ReqDocNo',
			text: '申请单号',
			width: 125,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDoc_ApplyName',
			text: '申请人',
			width: 75,
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
		}, {
			dataIndex: 'ReaBmsReqDoc_Visible',
			text: '启用',
			width: 45,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			},
			defaultRenderer: true
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
		//临时,已撤销申请,已撤消审核,可以修改
		var id = record.get("ReaBmsReqDoc_Id");
		var config = {
			resizable: true,
			width: 428,
			height: 390,
			PK: id,
			title: '申请操作记录',
			className: 'ReaBmsReqDocStatus' //类名
		};
		var win = JShell.Win.open('Shell.class.rea.client.reareqoperation.Panel', config);
		win.show();
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh','-'];
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems2: function() {
		var me = this;
		var items = [];
		var tempStatus = me.removeSomeStatusList();
		items.push({
			emptyText: '紧急标志',
			width: "22%",
			labelWidth: 0,
			xtype: 'uxSimpleComboBox',
			itemId: 'ReaBmsReqDocUrgentFlag',
			data: [
				["", "请选择"],
				["0", "正常"],
				["1", "紧急"]
			],
			value: '',
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		}, {
			emptyText: '申请状态',
			width: "53%",
			labelWidth: 0,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'ReaBmsReqDocStatus',
			data: tempStatus,
			value: me.defaultStatusValue,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push({
			emptyText: '是否启用',
			width: "22%",
			labelWidth: 0,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'ReaBmsReqDocVisible',
			data: [
				["", "请选择"],
				["1", "启用"],
				["0", "禁用"]
			],
			value: "1",
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createDateAreaToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
			fieldLabel: '',
			labelWidth: 0,
			width: 80,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'dateType',
			value: "reabmsreqdoc.ApplyTime",
			data: [
				["", "请选择"],
				["reabmsreqdoc.DataAddTime", "加入日期"],
				["reabmsreqdoc.ApplyTime", "申请日期"],
				["reabmsreqdoc.ReviewTime", "审核日期"]
			],
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
					var dateareaToolbar = me.getComponent('dateareaToolbar');
					var date = dateareaToolbar.getComponent('date');
					if(!records[0].data.value)
						date.disable();
					else
						date.enable();
				}
			}
		}, {
			xtype: 'uxdatearea',
			itemId: 'date',
			fieldLabel: '',
			labelWidth: 0,
			width: 190,
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		}, '->', {
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
	/**获取状态查询条件*/
	getStatusWhere: function() {
		var me = this;
		var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
		var status = buttonsToolbar2.getComponent('ReaBmsReqDocStatus');
		var where = "";
		if(status) {
			var value = status.getValue();
			if(value) where = "reabmsreqdoc.Status=" + value;
		}
		return where;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			dateareaToolbar = me.getComponent('dateareaToolbar'),

			search = buttonsToolbar.getComponent('search'),
			//status = buttonsToolbar2.getComponent('ReaBmsReqDocStatus'),
			urgentFlag = buttonsToolbar2.getComponent('ReaBmsReqDocUrgentFlag'),
			visible = buttonsToolbar2.getComponent('ReaBmsReqDocVisible'),

			dateType = dateareaToolbar.getComponent('dateType'),
			date = dateareaToolbar.getComponent('date'),

			where = [];
		if(urgentFlag) {
			var value = urgentFlag.getValue();
			if(value) {
				where.push("reabmsreqdoc.UrgentFlag=" + value);
			}
		}

		var statusWhere = me.getStatusWhere();
		if(statusWhere) where.push(statusWhere);

		if(visible) {
			var value = visible.getValue();
			if(value) {
				where.push("reabmsreqdoc.Visible=" + value);
			}
		}
		if(date) {
			var dateValue = date.getValue();
			var dateTypeValue = dateType.getValue();
			//if(!dateTypeValue) dateTypeValue = "reabmsreqdoc.DataAddTime";
			if(dateValue && dateTypeValue) {
				if(dateValue.start) {
					where.push(dateTypeValue + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push(dateTypeValue + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
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
		me.internalWhere = where.join(" and ");

		return me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		return tempList;
	},
	/**验证日期类型是否选择*/
	validDateType: function() {
		var me = this;
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			dateType = dateareaToolbar.getComponent('dateType');
		if(!dateType.getValue()) {
			JShell.Msg.alert("请选择日期类型后再查询!", null, 1000);
			dateType.focus();
			return false;
		}
		return true;
	},
	/**设置日期范围值*/
	onSetDateArea: function(day) {
		var me = this;
		var dateAreaValue = me.calcDateArea(day);
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateAreaValue) date.setValue(dateAreaValue);
	}
});