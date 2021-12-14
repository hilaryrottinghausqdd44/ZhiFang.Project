/**
 * 订货总单列表
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.basic.OrderGrid', {
	extend: 'Shell.class.rea.client.SearchGrid',
	title: '订货单列表',
	requires: [
		'Shell.ux.form.field.DateArea',
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	height: 340,
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenOrderDoc',
	/**修改服务地址*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDocByField',
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**是否多选行*/
	checkOne: false,
	hasDel: false,

	/**是否是查看列表*/
	isShow: false,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'BmsCenOrderDoc_OperDate',
		direction: 'DESC'
	}, {
		property: 'BmsCenOrderDoc_Status',
		direction: 'ASC'
	}],
	StatusList: [],
	/**申请单状态枚举*/
	StatusEnum: {},
	/**申请单状态背景颜色枚举*/
	StatusBGColorEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	/**下拉状态默认值*/
	defaultStatusValue: "",
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 285,
			isLike: true,
			itemId: 'Search',
			emptyText: '订购人员/订货单号',
			fields: ['bmscenorderdoc.UserName', 'bmscenorderdoc.OrderDocNo']
		};
		me.getStatusListData();
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();		
		me.dockedItems.push(me.createQuickSearchButtonToolbar());
		me.dockedItems.push(me.createDateAreaToolbarItems());
		me.dockedItems.push(me.createButtonToolbarItems2());
		if(!me.checkOne) me.setCheckboxModel();
		//数据列
		me.columns = me.createGridColumns();
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
			dataIndex: 'BmsCenOrderDoc_OperDate',
			text: '申请日期',
			align: 'center',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'BmsCenOrderDoc_OrderDocNo',
			text: '订货单号',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDoc_TotalPrice',
			text: '总价',
			width: 65,
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
			dataIndex: 'BmsCenOrderDoc_Status',
			text: '单据状态',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				var v = value;
				if(me.StatusEnum != null)
					v = me.StatusEnum[value];
				var bColor = "";
				if(me.StatusBGColorEnum != null)
					bColor = me.StatusBGColorEnum[value];
				var fColor = "";
				if(me.StatusFColorEnum != null)
					fColor = me.StatusFColorEnum[value];
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
			dataIndex: 'BmsCenOrderDoc_UrgentFlag',
			text: '紧急标志',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				var info = JShell.REA.Enum.BmsCenOrderDoc_UrgentFlag['E' + value] || {};
				var v = info.value || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + (info.bcolor || '#FFFFFF') +
					';color:' + (info.color || '#000000');
				return v;
			}
		}, {
			dataIndex: 'BmsCenOrderDoc_ReaCompName',
			text: '供货方',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDoc_UserName',
			text: '订购人员',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDoc_Checker',
			text: '审核人员',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDoc_CheckTime',
			text: '审核时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'BmsCenOrderDoc_Memo',
			text: '备注',
			hidden: true,
			width: 100,
			renderer: function(value, meta) {
				return "";
			}
		}, {
			dataIndex: 'BmsCenOrderDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BmsCenOrderDoc_ReaCompID',
			text: '供货方ID',
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
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
		var tempStatus = me.StatusList;
		tempStatus = me.removeSomeStatusList();
		items.push({
			fieldLabel: '标志',
			labelWidth: 40,
			width: 100,
			xtype: 'uxSimpleComboBox',
			itemId: 'DocUrgentFlag',
			data: [
				["", "全部"],
				["0", "正常"],
				["1", "紧急"]
			], // JShell.REA.Enum.getList('BmsCenSaleDoc_UrgentFlag'),
			value: '',
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		}, {
			fieldLabel: '状态',
			labelWidth: 40,
			width: 125,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DocStatus',
			data: tempStatus,
			value: me.defaultStatusValue,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push({
			fieldLabel: '是否启用',
			labelWidth: 65,
			width: 125,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DeleteFlag',
			data: [
				["", "全部"],
				["0", "启用"],
				["1", "禁用"]
			],
			value: "0",
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
			value: "bmscenorderdoc.OperDate",
			data: [
				["", "全部"],
				["bmscenorderdoc.DataAddTime", "加入日期"],
				["bmscenorderdoc.OperDate", "申请日期"],
				["bmscenorderdoc.CheckTime", "审核日期"]
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
			fieldLabel: '日期范围',
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
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');

		var buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			status = buttonsToolbar2.getComponent('DocStatus'),
			urgentFlag = buttonsToolbar2.getComponent('DocUrgentFlag'),
			DeleteFlag = buttonsToolbar2.getComponent('DeleteFlag');

		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			dateType = dateareaToolbar.getComponent('dateType'),
			date = dateareaToolbar.getComponent('date');

		var where = [];
		if(urgentFlag) {
			var value = urgentFlag.getValue();
			if(value) {
				where.push("bmscenorderdoc.UrgentFlag=" + value);
			}
		}
		if(status) {
			var value = status.getValue();
			if(value) {
				where.push("bmscenorderdoc.Status=" + value);
			}
		}
		if(DeleteFlag) {
			var value = DeleteFlag.getValue();
			if(value) {
				where.push("bmscenorderdoc.DeleteFlag=" + value);
			}
		}

		if(date) {
			var dateValue = date.getValue();
			var dateTypeValue = dateType.getValue();
			//if(!dateTypeValue) dateTypeValue = "bmscenorderdoc.DataAddTime";
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
				where.push(me.getSearchWhere(value));
			}
		}
		me.internalWhere = where.join(" and ");

		return me.callParent(arguments);
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
		var id = record.get("BmsCenOrderDoc_Id");
		var config = {
			title: '订单操作记录',
			resizable: true,
			width: 428,
			height: 390,
			PK: id,
			className: 'ReaBmsOrderDocStatus' //类名
		};
		var win = JShell.Win.open('Shell.class.rea.client.reareqoperation.Panel', config);
		win.show();
	},
	/**获取申请总单状态参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "ReaBmsOrderDocStatus",
				"classnamespace": "ZhiFang.Digitlab.Entity.ReagentSys"
			}]
		};
		return params;
	},
	/**获取借款状态信息*/
	getStatusListData: function(callback) {
		var me = this;
		if(me.StatusList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode(me.getParams());
		me.StatusList = [];
		me.StatusEnum = {};
		me.StatusFColorEnum = {};
		me.StatusBGColorEnum = {};
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].ReaBmsOrderDocStatus.length > 0) {
						me.StatusList.push(["", '全部', 'font-weight:bold;color:black;text-align:center;']); //, 'font-weight:bold;text-align:center;'
						Ext.Array.each(data.value[0].ReaBmsOrderDocStatus, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.FontColor) {
								//style.push('color:' + obj.FontColor);
								me.StatusFColorEnum[obj.Id] = obj.FontColor;
							}
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor); //background-
								me.StatusBGColorEnum[obj.Id] = obj.BGColor;
							}
							me.StatusEnum[obj.Id] = obj.Name;
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.StatusList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempStatus = me.StatusList;
		return tempStatus;
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