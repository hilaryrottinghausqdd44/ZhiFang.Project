/**
 * 服务器授权
 * @author longfc
 * @version 2016-12-26
 */
Ext.define('Shell.class.wfm.authorization.ahserver.basic.ServerGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '服务器授权',
	width: 800,
	height: 500,
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchAHServerLicenceByHQL',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdateAHServerLicenceByField',
	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelAHServerLicence',
	/**文件下载服务路径*/
	downloadUrl: "/SingleTableService.svc/ST_UDTO_DownLoadAHServerLicenceFile",
	/**默认加载*/
	defaultLoad: false,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**状态默认值*/
	defaultStatusValue: '',
	/**状态默认值*/
	defaultTypeValue: '',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'ApplyDataTime',
		direction: 'DESC'
	}, {
		property: 'PClientID',
		direction: 'ASC'
	}, {
		property: 'ApplyID',
		direction: 'ASC'
	}],
	/**是否管理员模块*/
	isAdmin: false,
	/**是否有日期范围*/
	hasDates: false,
	/**是否单选*/
	checkOne: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		Ext.QuickTips.init();
		Ext.override(Ext.ToolTip, {
			maxWidth: 680
		});
		me.initListeners();
		if(!JShell.System.ClassDict.LicenceType) {
			JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceType', function() {
				//JShell.System.ClassDict.LicenceType;
			});
		}
		if(!JShell.System.ClassDict.LicenceStatus) {
			JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceStatus', function() {});
		}
	},
	initComponent: function() {
		var me = this;
		if(!me.checkOne) {
			me.multiSelect = true;
			me.selType = 'checkboxmodel';
		}
		if(me.hasDates == true) me.initDate();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '用户名称',
			dataIndex: 'PClientName',
			width: 120,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '当前授权申请号',
			dataIndex: 'LRNo',
			width: 100,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '主授权申请号',
			dataIndex: 'LRNo1',
			width: 100,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '备份授权申请号',
			dataIndex: 'LRNo2',
			width: 100,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '特批',
			dataIndex: 'IsSpecially',
			width: 45,
			align: 'center',
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				var v = "否";
				if(value != "" && value != null) {
					switch(value.toString()) {
						case "1":
							v = "是";
							break;
						case "true":
							v = "是";
							break;
						default:
							break;
					}
				}
				return v;
			}
		}, {
			text: '流程状态',
			dataIndex: 'Status',
			width: 95,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				var v = value || '';
				if(v) {
					var info = JShell.System.ClassDict.getClassInfoById('LicenceStatus', v);
					if(info) {
						v = info.Name;
						meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
					}
				}
				return v;
			}
		}, {
			text: ' ',
			dataIndex: 'LicenceStatusId',
			width: 75,
			hidden: true,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				value = value || '';
				var BGColor = "";
				switch(value) {
					case "1":
						BGColor = "#1c8f36";
						break;
					case "2":
						BGColor = "#f4c600";
						break;
					case "3":
						BGColor = "red";
						break;
					default:
						break;
				}
				if(BGColor != "")
					meta.style = 'background-color:' + BGColor + ';color:#ffffff;';
				return record.get("LicenceStatusName");
			}
		}, {
			text: '授权状态名称',
			dataIndex: 'LicenceStatusName',
			width: 90,
			hidden: true,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}];
		//交流列
		columns.push(me.createInteraction());
		//查看操作列
		columns.push(me.createShowCcolumn());
		columns.push(me.createDownLoadCcolumn());
		//操作记录查看列
		columns.push(me.createOperation());

		columns.push({
			text: '授权时间',
			dataIndex: 'GenDateTime',
			width: 135,
			sortable: false,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '申请人',
			dataIndex: 'ApplyName',
			width: 70,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '申请时间',
			dataIndex: 'ApplyDataTime',
			width: 135,
			sortable: false,
			defaultRenderer: true,
			isDate: true,
			hasTime: true
		}, {
			text: '审核人',
			dataIndex: 'OneAuditName',
			width: 70,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '审核时间',
			dataIndex: 'OneAuditDataTime',
			width: 135,
			sortable: false,
			defaultRenderer: true,
			isDate: true,
			hasTime: true
		}, {
			text: '审批人',
			dataIndex: 'TwoAuditName',
			width: 70,
			sortable: false,
			defaultRenderer: true
		}, {
			text: 'ID',
			dataIndex: 'Id',
			hidden: true,
			isKey: true,
			hideable: false
		}, {
			text: 'PClientID',
			dataIndex: 'PClientID',
			hidden: true,
			hideable: false
		}, {
			text: '审批时间',
			dataIndex: 'TwoAuditDataTime',
			width: 135,
			sortable: false,
			defaultRenderer: true,
			isDate: true,
			hasTime: true
		});

		return columns;
	},
	/*创建交流列**/
	createInteraction: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '交流',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-interact hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.showInteractionById(rec);
				}
			}]
		};
	},

	/*创建交流列**/
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
					var id = rec.get(me.PKField);
					me.openShowForm(rec);
				}
			}]
		};
	},
	/*创建下载授权文件列**/
	createDownLoadCcolumn: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '下载',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-exp hand',
				getClass: function(v, meta, record) {
					//是否可以下载
					var Status = record.get('Status');
					if(Status == null || Status == undefined)
						Status = "";
					var IsSpecially = record.get('IsSpecially');
					if(IsSpecially == null || IsSpecially == undefined)
						IsSpecially = false;
					switch(IsSpecially) {
						case "1":
							IsSpecially = true;
							break;
						case "true":
							IsSpecially = true;
							break;
						default:
							IsSpecially = false;
							break;
					}
					var isShow = false;
					//商务授权通过不需要特批
					if(Status.toString() == "4" && IsSpecially == false) {
						isShow = true;
					} else if(Status.toString() == "7") { //特批授权通过
						isShow = true;
					} else if(Status.toString() == "9") {
						isShow = true;
					} else {
						isShow = false;
					}
					if(isShow) {
						meta.tdAttr = 'data-qtip="<b>下载授权文件</b>"';
						return 'button-exp hand';
					} else {
						meta.tdAttr = 'data-qtip="<b>没有授权文件</b>"';
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.onDwonload(id);
				}
			}]
		};
	},
	onDwonload: function(id) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.downloadUrl);
		url += '?operateType=0&id=' + id;
		window.open(url);
	},
	/**根据ID查看交流*/
	showInteractionById: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		var maxWidth = document.body.clientWidth - 380;
		var height = document.body.clientHeight - 60;
		JShell.Win.open('Shell.class.sysbase.scinteraction.App', {
			PK: id,
			height: height,
			width: maxWidth
		}).show();
	},
	/*创建操作记录列**/
	createOperation: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '操作记录',
			align: 'center',
			width: 55,
			hidden: false,
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
	/**打开操作记录列表*/
	openOperationGrid: function(rec) {
		var me = this;
		var id = rec.get(me.PKField);
		var config = {
			showSuccessInfo: false,
			resizable: false,
			hasButtontoolbar: false,
			PK: id
		};
		var win = JShell.Win.open('Shell.class.wfm.authorization.basic.SCOperation', config).show();
	},
	showQtipValue: function(value, meta, record, rowIndex, colIndex, store, view) {
		var me = this;
		var PClientName = record.get("PClientName");
		var LRNo = record.get("LRNo");
		var LRNo1 = record.get("LRNo1");
		var LRNo2 = record.get("LRNo2");

		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>用户名称:</b>" + PClientName + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>当前服务器授权申请号:</b>" + LRNo + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>主服务器授权申请号:</b>" + LRNo1 + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>备份服务器授权申请号:</b>" + LRNo2 + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>申请时间:</b>" + record.get("ApplyDataTime") + "</p>";

		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>授权时间:</b>" + record.get("GenDateTime") + "</p>";

		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>审核人:</b>" + record.get("OneAuditName") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>审核时间:</b>" + record.get("OneAuditDataTime") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>审批人:</b>" + record.get("TwoAuditName") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>审批时间:</b>" + record.get("TwoAuditDataTime") + "</p>";

		if(qtipValue) {
			meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		}
		return meta;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		if(me.isAdmin == true) {
			buttonToolbarItems.push({
				iconCls: 'button-save',
				text: "重新生成授权文件",
				tooltip: "重新生成授权文件",
				handler: function() {
					me.onRegenerateAHServerLicence();
				}
			}, '-');
		}
		buttonToolbarItems.push('refresh', '-', {
			width: 145,
			labelWidth: 35,
			labelAlign: 'right',
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'CheckStatus',
			fieldLabel: '状态',
			value: me.defaultStatusValue
		});
		buttonToolbarItems.push({
			fieldLabel: '用户选择',
			emptyText: '用户选择',
			name: 'PClientName',
			itemId: 'PClientName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.wfm.client.CheckGrid',
			labelWidth: 65,
			width: 240,
			classConfig: {
				height: 450,
				title: '用户选择'
			}
		}, {
			fieldLabel: '用户Id',
			emptyText: '用户',
			name: 'PClientID',
			itemId: 'PClientID',
			hidden: true,
			xtype: 'textfield'
		});
		if(me.hasDates) {
			buttonToolbarItems.push({
				width: 175,
				itemId: 'BeginDate',
				xtype: 'datefield',
				value: me.defaultBeginDateDate,
				fieldLabel: '申请日期',
				labelWidth: 65,
				format: 'Y-m-d',
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(newValue) me.onSearch();
					}
				}
			}, {
				width: 110,
				labelWidth: 5,
				fieldLabel: '-',
				labelSeparator: '',
				itemId: 'EndDate',
				xtype: 'datefield',
				value: me.defaultEndDateDate,
				format: 'Y-m-d',
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(newValue) me.onSearch();
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
			})
		}
		//查询框信息
		me.searchInfo = {
			width: 165,
			itemId: 'search',
			emptyText: '用户名称/申请人',
			isLike: true,
			fields: ['PClientName', 'ApplyName']
		};
		buttonToolbarItems.push('-', '->', {
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		var defaultDate = defaultDate = JcallShell.Date.getNextDate(Sysdate, -7);
		me.defaultBeginDateDate = JcallShell.Date.toString(defaultDate, true);
		me.defaultEndDateDate = JcallShell.Date.toString(Sysdate, true);
	},
	onRegenerateAHServerLicence: function(autoSelect) {
		var me = this;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) {
			return;
		}
		if(me.hasDates == true) {
			var BeginDate = buttonsToolbar.getComponent('BeginDate').getValue();
			var EndDate = buttonsToolbar.getComponent('EndDate').getValue();
			var StartDateValue = JcallShell.Date.toString(BeginDate, true);
			var EndDateValue = JcallShell.Date.toString(EndDate, true);
			if(StartDateValue > EndDateValue) {
				JShell.Msg.error('结束日期不能小于开始日期!');
				return;
			}
		}
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceType', function() {
			if(!JShell.System.ClassDict.LicenceType) {
				JShell.Msg.error('未获取到授权类型，请刷新列表');
				return;
			}
		});
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceStatus', function() {
			if(!JShell.System.ClassDict.LicenceStatus) {
				JShell.Msg.error('未获取到授权状态，请刷新列表');
				return;
			}
			var Status = buttonsToolbar.getComponent('CheckStatus');
			var StatusList = JShell.System.ClassDict.LicenceStatus;
			if(Status.store.data.items.length == 0) {
				Status.loadData(me.getLicenceStatusData(StatusList));
			}
			me.load(null, true, autoSelect);
		});
	},
	/**获取授权类型列表*/
	getLicenceTypeData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
			var style = ['font-weight:bold;text-align:center'];
			if(obj.BGColor) {
				style.push('color:' + obj.BGColor);
			}
			data.push([obj.Id, obj.Name, style.join(';')]);
		}
		return data;
	},
	/**获取授权状态列表*/
	getLicenceStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
			var style = ['font-weight:bold;text-align:center'];
			if(obj.BGColor) {
				style.push('color:' + obj.BGColor);
			}
			data.push([obj.Id, obj.Name, style.join(';')]);
		}
		return data;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Status = null,
			PClientID = null,
			BeginDate = null,
			EndDate = null,
			params = [],
			search = null;
		me.internalWhere = '';
		if(buttonsToolbar) {
			search = buttonsToolbar.getComponent('search').getValue();
			Status = buttonsToolbar.getComponent('CheckStatus').getValue();
			PClientID = buttonsToolbar.getComponent('PClientID').getValue();
			if(me.hasDates == true) {
				BeginDate = buttonsToolbar.getComponent('BeginDate').getValue();
				EndDate = buttonsToolbar.getComponent('EndDate').getValue();
			}
		}
		switch(Status) {
			case '3': //授权中
				params.push("((IsSpecially=1 and Status=4) or Status in(2,5,8))");
				break;
			case '6': //特批授权中
				params.push("((IsSpecially=1 and Status=4) or Status=8)");
				break;
			case '9': //授权完成
				params.push("((IsSpecially=0 and Status=4) or Status=7)");
				break;
			default:
				if(Status) {
					params.push("Status=" + Status + "");
				}
				break;
		}
		//用户
		if(PClientID) {
			params.push("PClientID='" + PClientID + "'");
		}
		if(me.hasDates == true) {
			if(BeginDate) {
				params.push("DataAddTime" + ">='" + JShell.Date.toString(BeginDate, true) + "'");
			}
			if(EndDate) {
				params.push("DataAddTime" + "<'" + JShell.Date.toString(EndDate, true) + "  23:59:59'");
			}
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
		return me.callParent(arguments);
	},
	/**初始化监听*/
	initListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) {
			return;
		}
		//客户
		var PClientName = buttonsToolbar.getComponent('PClientName'),
			PClientID = buttonsToolbar.getComponent('PClientID');

		PClientName.on({
			check: function(p, record) {
				PClientName.setValue(record ? record.get('PClient_Name') : '');
				PClientID.setValue(record ? record.get('PClient_Id') : '');
				me.onSearch();
				p.close();
			}
		});
		//状态
		var CheckStatus = buttonsToolbar.getComponent('CheckStatus');
		CheckStatus.on({
			change: function(com, newValue, oldValue, eOpts) {
				me.onSearch();
			}
		});
	},
	/**查询信息*/
	openShowForm: function(record) {
		var me = this;
		//var maxWidth = document.body.clientWidth * 0.82;
		var width = document.body.clientWidth * 0.72;
		if(width < 928)
			width = 928;
		var height = document.body.clientHeight - 18;
		var id = record.get(me.PKField);
		var PClientID = record.get("PClientID");
		if(PClientID == "" || PClientID == undefined)
			PClientID = null;
		JShell.Win.open('Shell.class.wfm.authorization.ahserver.show.Panel', {
			SUB_WIN_NO: '203', //内部窗口编号
			//resizable:false,
			height: height,
			width: width,
			title: '查看服务器授权信息',
			PK: id,
			PClientID: PClientID
		}).show();
	},
	/**修改*/
	openEditForm: function(record, editPanel) {
		var me = this;
		var id = record.get(me.PKField);
		var PClientID = record.get("PClientID");
		if(PClientID == "" || PClientID == undefined)
			PClientID = null;
		var AHServerLicence = {
			Id: id,
			PClientName: record.get("PClientName"),
			LRNo: record.get("LRNo"),
			LRNo1: record.get("LRNo1"),
			LRNo2: record.get("LRNo2"),
			Status: record.get("Status"),
			LicenceStatusId: record.get("LicenceStatusId"),
			LicenceStatusName: record.get("LicenceStatusName")
		};
		var IsSpecially = record.get("IsSpecially");
		switch(IsSpecially) {
			case "true":
				IsSpecially = 1;
				break;
			case "false":
				IsSpecially = 0;
				break;
			default:
				break;
		}
		AHServerLicence.IsSpecially = IsSpecially;
		var width = document.body.clientWidth * 0.72;
		if(width < 928)
			width = 928;
		var height = document.body.clientHeight - 18;
		JShell.Win.open(editPanel, {
			SUB_WIN_NO: '202', //内部窗口编号
			//resizable:false,
			AHServerLicence: AHServerLicence,
			height: height,
			width: width,
			title: '服务器授权编辑',
			PK: id,
			PClientID: PClientID,
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	}
});