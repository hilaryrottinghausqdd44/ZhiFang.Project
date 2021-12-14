/**
 * @description 用户订单(给供应商查看客户端用户已上传的订单信息)
 * @author longfc
 * @version 2018-03-06
 */
Ext.define('Shell.class.rea.client.order.comp.OrderGrid', {
	extend: 'Shell.class.rea.client.SearchGrid',

	requires: [
		'Shell.ux.form.field.DateArea',
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '用户订单',
	height: 340,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenOrderDocByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**是否多选行*/
	checkOne: true,
	hasDel: false,
	/**是否是查看列表*/
	isShow: true,

	/**录入:entry/审核:check*/
	OTYPE: "comp",
	/**是否禁用状态选项*/
	disabledStatus: false,
	/**是否禁用启用选项*/
	disabledDeleteFlag: false,
	disabledIOFlag: false,
	/**下拉状态默认值*/
	defaultStatusValue: "",
	/**数据上传标志默认值*/
	defaultIOFlagValue: "",

	/**订单状态Key*/
	StatusKey: "ReaBmsOrderDocStatus",
	/**订单数据标志Key*/
	IOFlagKey: "ReaBmsOrderDocIOFlag",
	/**排序字段*/
	defaultOrderBy: [{
		property: 'reabmscenorderdoc_OperDate',
		direction: 'DESC'
	}, {
		property: 'reabmscenorderdoc_Status',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'order.comp.OrderGrid',
	/**用户UI配置Name*/
	userUIName: "用户订单列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(-10);
	},
	initComponent: function() {
		var me = this;
		//机构所属平台编码
		var labOrgNo = JShell.REA.System.CENORG_CODE;
		if(!labOrgNo) labOrgNo = '-1';
		//数据标志为不等于未提取,不等于取消上传
		me.defaultWhere = "(reabmscenorderdoc.IOFlag!=0 and reabmscenorderdoc.IOFlag!=2) and reabmscenorderdoc.ReaServerCompCode='" + labOrgNo + "'";
		//查询框信息
		me.searchInfo = {
			width: "84%",
			isLike: true,
			itemId: 'search',
			emptyText: '订货单号',
			fields: ['reabmscenorderdoc.OrderDocNo']
		};
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.IOFlagKey, false, true, null);

		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建挂靠功能栏
		me.dockedItems = [];
		me.dockedItems.push(me.createQuickSearchButtonToolbar());
		me.dockedItems.push(me.createDateAreaToolbarItems());
		me.dockedItems.push(me.createDockedItems());

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
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsCenOrderDoc_DataAddTime',
			text: '申请日期',
			align: 'center',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_LabcName',
			text: '订货方',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaCompCode',
			text: '供货商编码',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_Status',
			text: '单据状态',
			align: 'center',
			width: 80,
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
			dataIndex: 'ReaBmsCenOrderDoc_SupplyStatus',
			text: '供货状态',
			align: 'center',
			width: 80,
			renderer: function(value, meta) {
				var info = JShell.REA.Enum.BmsCenOrderDoc_SupplyStatus['E' + value] || {};
				var v = info.value || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'font-weight:bold;' + 'background-color:' + (info.bcolor || '#FFFFFF') +
					';color:' + (info.color || '#000000');
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_UrgentFlag',
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
			dataIndex: 'ReaBmsCenOrderDoc_IOFlag',
			text: '数据标志',
			align: 'center',
			width: 60,
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
			dataIndex: 'ReaBmsCenOrderDoc_DeptName',
			text: '所属部门',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_OrderDocNo',
			text: '订货单号',
			width: 135,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_TotalPrice',
			text: '总价',
			width: 65,
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
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
			dataIndex: 'ReaBmsCenOrderDoc_UserName',
			text: '操作人',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_Checker',
			text: '审核人员',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_CheckTime',
			text: '审核时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_Confirm',
			text: '确认人',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ConfirmTime',
			text: '确认时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_Memo',
			text: '备注',
			hidden: true,
			width: 100,
			renderer: function(value, meta) {
				return "";
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_CompID',
			text: '供货商ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaCompID',
			text: '本地供货商ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaCompanyName',
			text: '本地供货商',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaServerCompCode',
			text: '供货商所属机构编码',
			width: 105,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaServerLabcCode',
			text: '订货方所属机构编码',
			width: 105,
			hidden: true,
			defaultRenderer: true
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
	createDateAreaToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
			fieldLabel: '',
			labelWidth: 0,
			width: 75,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'dateType',
			value: "reabmscenorderdoc.OperDate",
			data: [
				["", "请选择"],
				["reabmscenorderdoc.DataAddTime", "加入日期"],
				["reabmscenorderdoc.OperDate", "申请日期"],
				["reabmscenorderdoc.CheckTime", "审核日期"]
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
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');

		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			dateType = dateareaToolbar.getComponent('dateType'),
			date = dateareaToolbar.getComponent('date');

		var where = [];

		if(date) {
			var dateValue = date.getValue();
			var dateTypeValue = dateType.getValue();
			if(dateValue && dateTypeValue) {
				if(dateValue.start) {
					where.push(dateTypeValue + ">='" + JcallShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push(dateTypeValue + "<'" + JcallShell.Date.toString(JcallShell.Date.getNextDate(dateValue.end), true) + "'");
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
	onShowOperation: function(record) {
		var me = this;
		if(!record) {
			var records = me.getSelectionModel().getSelection();
			if(records.length != 1) {
				JcallShell.Msg.error(JcallShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		//临时,已撤销申请,已撤消审核,可以修改
		var id = record.get("ReaBmsCenOrderDoc_Id");
		var config = {
			title: '订单操作记录',
			resizable: true,
			width: 428,
			height: 390,
			PK: id,
			className: 'ReaBmsOrderDocStatus' //类名
		};
		var win = JcallShell.Win.open('Shell.class.rea.client.reareqoperation.Panel', config);
		win.show();
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
			JcallShell.Msg.alert("请选择日期类型后再查询!", null, 1000);
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