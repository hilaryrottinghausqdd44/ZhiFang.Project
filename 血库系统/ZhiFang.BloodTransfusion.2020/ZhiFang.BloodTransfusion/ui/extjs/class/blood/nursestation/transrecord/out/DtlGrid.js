/**
 * 输血过程记录:发血血袋信息(出库明细列表)
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.out.DtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '发血血袋信息',
	hasPagingtoolbar: false,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemOfBloodTransByHQL?isPlanish=true',
	/**只能获取到可配置的系统参数*/
	defaultWhere: "",
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 100,
	//发血主单ID
	PK: null,
	hasRefresh: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	autoSelect: true,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodBOutItem_Bloodstyle_Id',
		direction: 'ASC'
	},{
		property: 'BloodBOutItem_CourseCompletion',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'transrecord.out.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "发血血袋信息",
	/**复选框列*/
	selModel: Ext.create("Ext.selection.CheckboxModel", {
		injectCheckbox: 1, //checkbox位于哪一列，默认值为0
		mode: "multi" //multi,simple,single；默认为多选multi
		//,checkOnly: true//如果值为true,则只用点击checkbox列才能选中此条记录
		//,allowDeselect: true,//如果值true并且mode值为单选（single）时,可通过点击checkbox取消对其的选择
		//,enableKeyNav: true
	}),
	//复选框
	/* multiSelect: true,
	selType: 'checkboxmodel', */
	/**全选、全不选状态*/
	isSelectAll: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//me.addEvents('onAddTrans');
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = [];

		if (me.hasRefresh) items.push('refresh');
		//查询框信息
		me.searchInfo = {
			width: 155,
			itemId: "search",
			emptyText: '血袋号/惟一码/血制品名称',
			isLike: true,
			fields: ['bloodboutitem.BBagCode', 'bloodboutitem.B3Code', 'bloodboutitem.Bloodstyle.CName']
		};
		items = me.createSearchButtonsToolbar(items);

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createSearchButtonsToolbar: function(items) {
		var me = this;

		if (!items) items = [];
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			text: '全勾选',
			tooltip: '',
			listeners: {
				click: function(button) {
					me.onAllCheck(button);
				}
			}
		}, '-');
		items.push({
			fieldLabel: '',
			name: 'HandoverCompletion',
			itemId: 'HandoverCompletion',
			xtype: 'uxSimpleComboBox',
			width: 105,
			labelWidth: 0,
			value: '',
			emptyText: '',
			hasStyle: true,
			data: [
				['', '请选择', 'color:black;'],
				['1', '未交接', 'color:#FFB800;'],
				//['2', '部分交接', 'color:#00BFFF;'],
				['3', '交接完成', 'color:#009688;']
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push({
			fieldLabel: '',
			name: 'CourseCompletion',
			itemId: 'CourseCompletion',
			xtype: 'uxSimpleComboBox',
			width: 85,
			labelWidth: 0,
			value: '',
			emptyText: '',
			hasStyle: true,
			data: [
				['', '请选择', 'color:black;'],
				['1', '未登记', 'color:#FFB800;'],
				['2', '部分登记', 'color:#00BFFF;'],
				['3', '登记完成', 'color:#009688;']
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			listeners: {
				click: function(button) {
					me.onSearch();
				}
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-add',
			text: '登记',
			tooltip: '对选中的第血袋进行新增登记',
			listeners: {
				click: function(but) {
					me.onAddTrans();
				}
			}
		});
		items.push('->', {
			xtype: 'splitbutton',
			iconCls: 'button-search',
			text: '查看修改记录',
			menu: [{
				iconCls: 'button-search',
				text: '输血过程基本信息',
				handler: function() {
					me.onShowOperation("EditBloodTransForm");
				}
			}, {
				iconCls: 'button-search',
				text: '病人体征记录信息',
				handler: function() {
					me.onShowOperation("EditTransfusionAntries");
				}
			}, {
				iconCls: 'button-search',
				text: '临床处理结果',
				handler: function() {
					me.onShowOperation("EditClinicalResults");
				}
			}, {
				iconCls: 'button-search',
				text: '临床处理结果描述',
				handler: function() {
					me.onShowOperation("EditClinicalResultsDesc");
				}
			}]
		});
		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			xtype: 'checkcolumn',
			dataIndex: 'BloodBOutItem_CheckColumn',
			text: '<b style="color:blue;">选择</b>',
			width: 40,
			hidden: true,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '血制品',
			dataIndex: 'BloodBOutItem_Bloodstyle_CName',
			width: 150,
			defaultRenderer: true
		}, {
			text: '交接完成度',
			dataIndex: 'BloodBOutItem_HandoverCompletion',
			width: 75,
			renderer: function(value, meta) {
				var v = "";
				if (value == "2") {
					v = "部分交接";
					meta.style = "background-color:#00BFFF;color:#ffffff;";
				} else if (value == "3") {
					v = "交接完成";
					meta.style = "background-color:#009688;color:#ffffff;";
				} else {
					v = "未交接";
					meta.style = "background-color:#FFB800;color:#ffffff;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text: '输血登记完成度',
			dataIndex: 'BloodBOutItem_CourseCompletion',
			width: 95,
			renderer: function(value, meta) {
				var v = "";
				if (value == "2") {
					v = "部分登记";
					meta.style = "background-color:#00BFFF;color:#ffffff;";
				} else if (value == "3") {
					v = "登记完成";
					meta.style = "background-color:#009688;color:#ffffff;";
				} else {
					v = "未登记";
					meta.style = "background-color:#FFB800;color:#ffffff;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '血袋号',
			dataIndex: 'BloodBOutItem_BBagCode',
			width: 110,
			defaultRenderer: true
		}, {
			text: '产品码',
			dataIndex: 'BloodBOutItem_Pcode',
			width: 90,
			defaultRenderer: true
		}, {
			text: '惟一码',
			dataIndex: 'BloodBOutItem_B3Code',
			width: 150,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '血型名称',
			dataIndex: 'BloodBOutItem_BloodABO_CName',
			width: 70,
			defaultRenderer: true
		}, {
			text: '血容量',
			dataIndex: 'BloodBOutItem_BOutCount',
			width: 55,
			defaultRenderer: true
		}, {
			text: '单位',
			dataIndex: 'BloodBOutItem_BloodBUnit_BUnitName',
			width: 50,
			defaultRenderer: true
		}, {
			text: '失效时间',
			dataIndex: 'BloodBOutItem_InvalidDate',
			width: 85,
			isDate: true,
			hasTime: false,
			defaultRenderer: true
		}, {
			text: '采集时间',
			dataIndex: 'BloodBOutItem_CollectDate',
			width: 125,
			flex: 1,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '申请主单号',
			dataIndex: 'BloodBOutItem_BloodBReqForm_Id',
			width: 70,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '申请明细编号',
			dataIndex: 'BloodBOutItem_BloodBReqItem_Id',
			width: 70,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '发血主单编号',
			dataIndex: 'BloodBOutItem_BloodBOutForm_Id',
			width: 70,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '血制品编码',
			dataIndex: 'BloodBOutItem_Bloodstyle_Id',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '血型编码',
			dataIndex: 'BloodBOutItem_BloodABO_Id',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '单位编码',
			dataIndex: 'BloodBOutItem_BloodBUnit_Id',
			width: 70,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '输血记录主单Id',
			dataIndex: 'BloodBOutItem_BloodTransForm_Id',
			width: 70,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '发血明细单号',
			dataIndex: 'BloodBOutItem_Id',
			isKey: true,
			width: 70,
			hidden: true,
			defaultRenderer: true
		}];
		return columns;
	},
	getStoreFields: function(isString) {
		var me = this,
			columns = me.columns || [],
			length = columns.length,
			fields = [];

		for (var i = 0; i < length; i++) {
			if (columns[i].dataIndex && columns[i].xtype != "checkcolumn") {
				var obj = isString ? columns[i].dataIndex : {
					name: columns[i].dataIndex,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = me.getInternalWhere();
		//内部条件
		me.internalWhere = params.join(" and ");
		return me.callParent(arguments);
	},
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');
		var handoverCompletion = buttonsToolbar.getComponent('HandoverCompletion');
		var courseCompletion = buttonsToolbar.getComponent('CourseCompletion');
		var params = [];
		if (me.PK) params.push("bloodboutitem.BloodBOutForm.Id='" + me.PK + "'");
		if (handoverCompletion) {
			var value = handoverCompletion.getValue();
			if (value) {
				if (value == "1") { //未接收
					params.push("(bloodboutitem.HandoverCompletion!=3 or bloodboutitem.HandoverCompletion is null)");
				} else {
					params.push("bloodboutitem.HandoverCompletion=" + value);
				}
			}
		}
		if (courseCompletion) {
			var value = courseCompletion.getValue();
			if (value) {
				if (value == "1") { //未输血登记
					params.push("(bloodboutitem.CourseCompletion!=3 or bloodboutitem.CourseCompletion is null)");
				} else {
					params.push("bloodboutitem.CourseCompletion=" + value);
				}
			}
		}
		if (search) {
			var value = search.getValue();
			if (value) {
				params.push("(" + me.getSearchWhere(value) + ")");
			}
		}
		return params;
	},
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		if (!me.PK) {
			var error = me.errorFormat.replace(/{msg}/, "请选择发血单后再操作!");
			me.getView().update(error);
			return false;
		}

		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	/**
	 * @description 全选/全不选
	 * @param {Object} button
	 */
	onAllCheck: function(button) {
		var me = this;
		var text = "全勾选"; //button-check
		var iconCls = 'button-check';
		if (!me.isSelectAll) {
			me.isSelectAll = true;
			me.getSelectionModel().selectAll();
		} else {
			me.isSelectAll = false;
			text = "全不选";
			iconCls = 'button-uncheck';
			me.getSelectionModel().deselectAll();
		}
		button.setIconCls(iconCls);
		button.setText(text);
	},
	onAddTrans: function() {
		var me = this;
		me.fireEvent('onAddTrans', me);
	},
	onShowOperation: function(businessModuleCode) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var record = records[0];
		var id = "" + record.get("BloodBOutItem_BloodTransForm_Id");
		if (!id) {
			JShell.Msg.error("当前选择的发血血袋还未进行输血过程记录登记!");
			return;
		}
		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.94;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			PK: id,
			classNameSpace: 'ZhiFang.Entity.BloodTransfusion', //类域
			className: 'UpdateOperationType', //类名
			title: '输血过程修改记录查看',
			defaultWhere: "scoperation.BusinessModuleCode='" + businessModuleCode + "'"
		};
		var win = JShell.Win.open('Shell.class.blood.scoperation.SCOperation', config);
		win.show();
	}
});
