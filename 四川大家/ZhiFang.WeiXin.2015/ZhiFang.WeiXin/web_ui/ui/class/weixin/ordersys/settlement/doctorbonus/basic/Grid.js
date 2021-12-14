/**
 * 医生奖金结算单基本列表
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.YearAndMonthComboBox',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.TextSearchTrigger'
	],

	title: '医生奖金结算单',
	width: 1200,
	height: 800,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusFormByHQL?isPlanish=false',
	/**修改服务地址*/
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusFormByField',
	/**删除服务地址*/
	delUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelOSDoctorBonusFormAndDetails',

	/**获取获取类字典列表服务路径*/
	classdicSelectUrl: '/ServerWCF/SystemCommonService.svc/GetClassDicList',
	EditTabPanelCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.EditTabPanel',
	/**错误信息样式*/
	errorFormat: '<div style="color:red;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'BonusFormCode',
		direction: 'ASC'
	}, {
		property: 'Status',
		direction: 'ASC'
	}],
	/**后台排序*/
	remoteSort: true,
	/**删除标志字段*/
	DelField: 'delState',
	multiSelect: true,
	defaultWhere: '',
	checkOne: true,
	autoHeight: false,
	autoScroll: true,
	/**默认每页数量*/
	defaultPageSize: 20,
	/**默认加载数据*/
	defaultLoad: true,

	hasAdd: false,
	hasShow: false,
	hasEdit: false,
	hasPrint: false,
	hasRefresh: true,
	/**是否包含结算功能按钮*/
	hasSettlement: false,
	hasDel: false,
	hasDeleteColumn: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否隐藏工具栏查询条件*/
	hiddenbuttonsToolbar: false,
	hasToolbarDate: false,
	/**是否显示被禁用的数据*/
	isShowDel: false,
	StatusList: [],
	StatusEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	/**导出excel*/
	hasExportExcel: false,
	/**序号列宽度*/
	rowNumbererWidth: 40,
	/**状态选择项的默认值*/
	defaultStatusValue: "",

	downLoadExcelUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_ExportExcelOSDoctorBonusFormDetail',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(me.hiddenbuttonsToolbar) {
			buttonsToolbar.setVisible(false);
		}
		Ext.QuickTips.init();
		Ext.override(Ext.ToolTip, {
			maxWidth: 680
		});
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				me.onItemDblClick(grid, record, item, index, e, eOpts);
			}
		});
	},

	initComponent: function() {
		var me = this;
		if(!me.checkOne) {
			me.multiSelect = true;
			me.selType = 'checkboxmodel';
		}
		me.initDefaultValue();
		me.getStatusListData();
		//创建数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//初始化默认条件
		me.initDefaultWhere();
		me.addEvents('onEditClick', me);
		me.addEvents('onShowTabPanelClick', me);
		me.callParent(arguments);
	},
	/**初始化时间*/
	initDefaultValue: function() {
		var me = this;
		var date = JcallShell.System.Date.getDate();
		if(!date) date = JShell.Date.getNextDate(new Date());
		date = JShell.Date.getNextDate(date, -60);
		var year = date.getFullYear();
		var minMonth = date.getMonth() + 1;
		minMonth = (minMonth <= 9 ? "0" + minMonth : "" + minMonth);
		//me.minYearValue = year;
		//me.bonusFormRoundMinValue = year + "-" + minMonth;
		me.BeginDateDefaultValue = year + "-" + "01";
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasSettlement) items.push(me.createToolbarSettlementItems());
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		if(me.hasToolbarDate) items.push(me.createToolbarDateItems());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.buttonToolbarItems || [];

		var tempStatus = me.StatusList;
		tempStatus = me.removeSomeStatusList();
		items.push({
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: tempStatus,
			value: me.defaultStatusValue,
			width: 145,
			labelWidth: 45,
			fieldLabel: '状态',
			tooltip: '状态选择',
			emptyText: '状态选择',
			itemId: 'selectStatus',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '结算周期/结算单号',
			isLike: true,
			itemId: 'search',
			fields: ['BonusFormCode', 'BonusFormRound']
		};
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		if(me.hasRefresh) {
			items.push("-");
			items.push('refresh');
		}
		if(me.hasDel) {
			items.push("-");
			items.push('del');
		}

		return items;
	},
	/**初始化状态查询栏内容*/
	createToolbarSettlementItems: function() {
		var me = this,
			items = [];
		items = me.createSettlementItems();
		var toolbarSettlement = {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'toolbarSettlement',
			items: items
		};
		return toolbarSettlement;
	},
	/**初始化日期查询栏内容*/
	createToolbarDateItems: function() {
		var me = this,
			items = [];
		items.push({
			width: 95,
			labelAlign: 'right',
			minValue: 2016,
			itemId: 'BeginDate',
			value: me.BeginDateDefaultValue,
			xtype: 'uxYearAndMonthComboBox',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			width: 95,
			minValue: 2016,
			labelAlign: 'right',
			itemId: 'EndDate',
			xtype: 'uxYearAndMonthComboBox',
			margin: '0 2px 0 10px',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			width: 60,
			iconCls: 'button-search',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '查询',
			tooltip: '<b>查询</b>',
			handler: function() {
				me.onSearch();
			}
		});
		if(me.hasExportExcel) {
			items.push({
				xtype: 'button',
				itemId: 'btnExportExcel',
				iconCls: 'file-excel hand',
				text: "导出",
				tooltip: '导出清单为excel',
				handler: function() {
					me.onDownLoadExcel();
				}
			});
		}
		if(me.hasPrint) {
			items.push({
				xtype: 'button',
				itemId: 'btnPrint',
				iconCls: 'button-print hand',
				text: "预览及打印",
				tooltip: '预览及打印',
				handler: function() {
					me.onPrintClick();
				}
			});
		}
		var toolbarDate = {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'toolbarDate',
			items: items
		};
		return toolbarDate;
	},
	createGridColumns: function() {
		var me = this;
		//创建数据列
		var columns = [];
		columns = me.createDefaultColumns();
		return columns;
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempStatus = me.StatusList;
		return tempStatus;
	},
	/**获取申请状态参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "OSDoctorBonusFormStatus",
				"classnamespace": "ZhiFang.WeiXin.Entity"
			}]
		};
		return params;
	},
	/**获取申请状态信息*/
	getStatusListData: function(callback) {
		var me = this;
		if(me.StatusList&&me.StatusList.length>0)return;
			var params = {},
			url = JShell.System.Path.getRootUrl(me.classdicSelectUrl);
		params = Ext.encode(me.getParams());
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].OSDoctorBonusFormStatus.length > 0) {
						me.StatusList = [];
						me.StatusEnum = {};
						me.StatusFColorEnum = {};
						me.StatusBGColorEnum = {};
						var tempArr = [];
						me.StatusList.push(["", '全部', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].OSDoctorBonusFormStatus, function(obj, index) {
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
	showQtipValue: function(meta, record) {
		var me = this;
		var qtipValue = "";

		if(qtipValue) {
			meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		}
		return meta;
	},
	getMetaStyle: function(index) {
		var me = this;
		var bColor = "";
		if(me.StatusBGColorEnum != null)
			bColor = me.StatusBGColorEnum[index];
		var fColor = "";
		if(me.StatusFColorEnum != null)
			fColor = me.StatusFColorEnum[index];

		var style = 'font-weight:bold;';
		if(bColor) {
			style = style + "background-color:" + bColor + ";";
		}
		if(fColor) {
			style = style + "color:" + fColor + ";";
		}
		return style;
	},
	/**创建数据列*/
	createDefaultColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			text: '状态',
			dataIndex: 'Status',
			width: 100,
			align: 'center',
			sortable: false,
			menuDisabled: true,
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
		});

		if(me.hasDeleteColumn) {
			columns.push(me.createDelete());
		}
		columns.push({
			text: '结算周期',
			dataIndex: 'BonusFormRound',
			width: 65,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		},{
			text: '医生数量',
			dataIndex: 'DoctorCount',
			width: 60,
			hideable: false
		}, {
			text: '开单数量',
			dataIndex: 'OrderFormCount',
			width: 60,
			hideable: false
		}, {
			text: '结算金额',
			dataIndex: 'Amount',
			width: 70,
			hideable: false
		}, {
			text: '开单金额',
			dataIndex: 'OrderFormAmount',
			width: 75,
			hideable: false
		});

		columns.push({
			text: '申请时间',
			dataIndex: 'BonusApplytTime',
			width: 130,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '结算申请人',
			dataIndex: 'BonusApplyManName',
			width: 85,
			hidden: true,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '主键ID',
			dataIndex: 'Id',
			isKey: true,
			hidden: true,
			hideable: false
		});

		//查看操作列
		columns.push(me.createShowCcolumn());
		//操作记录查看列
		columns.push(me.createOperation());

		columns.push({
			text: '结算单号',
			dataIndex: 'BonusFormCode',
			hidden: false,
			width: 80,
			hidden: true,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '处理人',
			dataIndex: 'BonusOneReviewManName',
			hidden: false,
			width: 85,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '审批人',
			dataIndex: 'BonusTwoReviewManName',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					var status = record.get("Status").toString();
					meta.style = (status == "6" ? me.getMetaStyle(status) : me.getMetaStyle(5));
				}
				return value;
			}
		}, {
			text: '发放人',
			dataIndex: 'BonusThreeReviewManName',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					var status = record.get("Status").toString();
					meta.style = (status == "8" ? me.getMetaStyle(status) : me.getMetaStyle(7));
				}
				return value;
			}
		});
		return columns;
	},
	/*创建删除列**/
	createDelete: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:red;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				getClass: function(v, meta, rec) {
					var status = "" + rec.get("Status");
					//暂存,一审退回
					if(status == "1" || status == "5") {
						meta.tdAttr = 'data-qtip="<b>删除</b>"';
						//meta.style = 'background-color:green;';
						return 'button-del hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var status = rec.get("Status");
					var msg = "是否要删除选择的医生奖金结算单";
					Ext.MessageBox.show({
						title: '操作确认消息',
						msg: msg,
						width: 300,
						icon: Ext.MessageBox.QUESTION,
						buttons: Ext.MessageBox.OKCANCEL,
						fn: function(btn) {
							if(btn == 'ok') {
								me.onDeleteByStrIds(rec);
							}
						}
					});
				}
			}]
		};
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var delArr = [];
		for(var i in records) {
			var status = "" + records[i].get("Status");
			//暂存,一审退回
			if(status == "1" || status == "4") {
				delArr.push(records[i])
			}
		}
		if(delArr.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		JShell.Msg.del(function(but) {
			if(but != "ok") return;
			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = delArr.length;
			me.showMask(me.delText); //显示遮罩层
			for(var i in delArr) {
				var id = delArr[i].get(me.PKField);
				me.delOneById(i, id);
			}
		});
	},
	/*删除**/
	onDeleteByStrIds: function(rec) {
		var me = this;
		var id = rec.get(me.PKField);
		me.delErrorCount = 0;
		me.delCount = 0;
		me.delLength = 1;
		me.delOneById(1, id);
	},
	/*创建查看列**/
	createShowCcolumn: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '查看',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openShowTabPanel(rec);
				}
			}]
		};
	},
	/*创建操作记录列**/
	createOperation: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openOperationGrid(rec);
				}
			}]
		};
	},
	/**打开申请单操作记录列表*/
	openOperationGrid: function(rec) {
		var me = this;
		var id = rec.get(me.PKField);
		var config = {
			showSuccessInfo: false,
			width: 460,
			height: 280,
			resizable: false,
			hasButtontoolbar: false,
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			PK: id
		};
		var win = JShell.Win.open('Shell.class.weixin.ordersys.settlement.doctorbonus.operation.Panel', config).show();
	},

	/**查看应用*/
	openShowTabPanel: function(record) {
		var me = this;
		var hasExportExcel = false;
		var id = "";
		if(record != null) {
			id = record.get('Id');
			var status = "" + record.get('Status');
			if(status == "9" || status == "11") hasExportExcel = true;
		}
		var maxWidth = document.body.clientWidth * 0.86;
		var minWidth = (maxWidth < 780 ? 780 : maxWidth);
		var height = document.body.clientHeight - 30;
		var config = {
			showSuccessInfo: false,
			PK: id,
			hasOperation: true,
			hasExportExcel: hasExportExcel,
			height: height,
			minWidth: minWidth,
			width: maxWidth,
			resizable: true,
			formtype: 'show',
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			listeners: {
				save: function(win) {
					//me.onSearch();
					win.close();
				}
			}
		};
		JShell.Win.open('Shell.class.weixin.ordersys.settlement.doctorbonus.show.TabPanel', config).show();
	},
	/**奖金结算[审核*/
	openEditTabPanel: function(record, hiddenPass) {
		var me = this;
		var id = "";
		if(record != null) {
			id = record.get('Id');
		}
		if(hiddenPass == null) {
			hiddenPass = false;
		}
		var maxWidth = document.body.clientWidth * 0.86;
		var minWidth = (maxWidth < 780 ? 780 : maxWidth);
		maxWidth = maxWidth <= minWidth ? minWidth : maxWidth;
		var height = document.body.clientHeight - 35;
		var entity = record.data;

		var applyInfo = {
			IsSettlement: true,
			BonusFormRound: record.get('BonusFormRound'),
			OSDoctorBonusForm: entity,
			OperationMemo: '',
			OSDoctorBonusList: null
		};
		delete applyInfo.OSDoctorBonusForm.DataTimeStamp;
		delete applyInfo.OSDoctorBonusForm.DataAddTime;
		delete applyInfo.OSDoctorBonusForm.DataUpdateTime;
		delete applyInfo.OSDoctorBonusForm.DispOrder;
		delete applyInfo.OSDoctorBonusForm.StatusName;
		delete applyInfo.OSDoctorBonusForm.BonusApplytTime;

		delete applyInfo.OSDoctorBonusForm.BonusOneReviewFinishTime;
		delete applyInfo.OSDoctorBonusForm.BonusOneReviewStartTime;
		delete applyInfo.OSDoctorBonusForm.BonusOneReviewManID;

		delete applyInfo.OSDoctorBonusForm.BonusTwoReviewFinishTime;
		delete applyInfo.OSDoctorBonusForm.BonusTwoReviewStartTime;
		delete applyInfo.OSDoctorBonusForm.BonusTwoReviewManID;

		delete applyInfo.OSDoctorBonusForm.BonusThreeReviewFinishTime;
		delete applyInfo.OSDoctorBonusForm.BonusThreeReviewStartTime;
		delete applyInfo.OSDoctorBonusForm.BonusThreeReviewManID;

		var config = {
			showSuccessInfo: false,
			PK: id,
			hasOperation: true,
			SUB_WIN_NO: '1',
			height: height,
			width: maxWidth,
			resizable: true,
			formtype: 'edit',
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			hiddenPass: hiddenPass,
			applyInfo: applyInfo,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		JShell.Win.open(me.EditTabPanelCalss, config).show();
	},
	/**导出EXCEL文件*/
	onDownLoadExcel: function() {
		var me = this;
		var params = [];
		params = me.getSearchWhereHQL();
		var showInfo = "";
		var isExec = true;
		var where = "";
		if(params.length > 0) {
			where = params.join(") and (");
			if(where) where = "(" + where + ")";
		} else {
			showInfo = "查询条件不能为空!";
			isExec = false;
		}
		if(isExec && where != "") {
			var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;
			url += "?operateType=1" + "&where=" + where;
			window.open(url);
		} else {
			JShell.Msg.alert(showInfo, null, 2000);
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var arr = [];
		arr = me.getSearchWhereHQL();
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		if(where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	},
	getSearchWhereHQL: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			toolbarDate = me.getComponent('toolbarDate'),
			toolbarSearch = me.getComponent('toolbarSearch');
		var selectStatus = "",
			BeginDate = "",
			EndDate = "",
			search = null;
		var params = [];

		if(buttonsToolbar) {
			selectStatus = buttonsToolbar.getComponent('selectStatus').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
		}
		if(toolbarDate) {
			BeginDate = toolbarDate.getComponent('BeginDate').getValue();
			EndDate = toolbarDate.getComponent('EndDate').getValue();
		}
		if(selectStatus) {
			switch(selectStatus) {
				case '3':
					//一审中:申请,一审中,一审通过,一审退回
					params.push("(Status=2 or Status=3 or Status=4 or Status=5)");
					break;
				case '6':
					//二审中:一审通过,二审中,二审退回
					params.push("(Status=4 or Status=6 or Status=8)");
					break;
				case '10':
					//完成:检查并打款,完成
					params.push("(Status=9 or Status=10)");
					break;
				default:
					params.push("Status=" + selectStatus + "");
					break;
			}
		}

		//结算周期
		if(BeginDate) {
			params.push("BonusFormRound>='" + BeginDate + "'");
		}
		if(EndDate) {
			params.push("BonusFormRound<='" + EndDate + "'");
		}

		//是否显示被禁用的数据,如果不显示
		if(me.isShowDel == false) {
			params.push("IsUse=1");
		}

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			params.push(me.defaultWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			params.push(me.externalWhere);
		}
		return params;
	},
	/**初始化默认条件*/
	initDefaultWhere: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') ';
		}
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onEditClick', me, records[0]);
	},
	onShowClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.openShowTabPanel(records[0]);
	},
	onDelClick: function() {
		var me = this;
		me.fireEvent('onDelClick', me);
	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {

	},
	onPrintClick: function() {

	}
});