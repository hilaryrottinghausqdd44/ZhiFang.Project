/**
 * 客户端验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.basic.DocGrid', {
	extend: 'Shell.class.rea.client.SearchGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '验货单信息列表',

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocConfirmByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDocConfirm',
	/**修改服务地址*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDocConfirmByField',

	/**默认加载*/
	defaultLoad: true,
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
		property: 'BmsCenSaleDocConfirm_DataAddTime',
		direction: 'DESC'
	}],

	/**默认单据状态*/
	defaultStatusValue: "1",
	StatusList: [],
	/**状态枚举*/
	StatusEnum: {},
	/**状态背景颜色枚举*/
	StatusBGColorEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	/**状态查询按钮选中值*/
	searchStatusValue: null,
	/**验收双确认方式:secAccepterType：默认本实验室(1),供应商(2),供应商或实验室(3)*/
	secAccepterType: 1,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('checkclick');
		me.getStatusListData();
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.dockedItems.push(me.createStatusSearchButtonToolbar());
		me.dockedItems.push(me.createQuickSearchButtonToolbar());
		me.dockedItems.push(me.createDateAreaToolbarItems());
		me.dockedItems.push(me.createButtonToolbarItems2());
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'BmsCenSaleDocConfirm_DataAddTime',
			text: '加入时间',
			align: 'center',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_SaleDocConfirmNo',
			hidden: true,
			text: '验收单号',
			width: 80,
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
			dataIndex: 'BmsCenSaleDocConfirm_Status',
			text: '单据状态',
			align: 'center',
			width: 75,
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
			dataIndex: 'BmsCenSaleDocConfirm_ReaCompName',
			text: '供货方',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_AccepterName',
			text: '主验收人',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_Memo',
			text: '备注',
			hidden: true,
			hideable: false,
			width: 200,
			renderer: function(value, meta) {
				return "";
			}
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_Id',
			text: '主键ID',
			hidden: true,
			isKey: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_ReaCompID',
			text: '供货方ID',
			hidden: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		return null;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems2: function() {
		var me = this;
		var items = [];
		var tempStatus = me.StatusList;
		tempStatus = me.removeSomeStatusList();
		items.push({
			fieldLabel: '状态',
			labelWidth: 40,
			width: 125,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DocStatus',
			data: tempStatus,
			value: me.defaultStatusValue,
			hidden: true,
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
			itemId: 'buttonsToolbar2',
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
			fieldLabel: '日期范围',
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		}, {
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
	/**创建验收状态按钮查询栏*/
	createStatusSearchButtonToolbar: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'button',
			text: '全部',
			tooltip: '全部',
			itemId: "AllStatus",
			handler: function() {
				me.onStatusSearch(null);
			}
		}, {
			xtype: 'button',
			text: '待继续验收',
			tooltip: '查询状态为待继续验收的验收单',
			itemId: "Apply",
			enableToggle: false,
			handler: function() {
				me.onStatusSearch(0);
			}
		}, {
			xtype: 'button',
			text: '已验收 ',
			tooltip: '查询状态为已验收的验收单',
			itemId: "Accept",
			enableToggle: false,
			handler: function() {
				me.onStatusSearch(1);
			}
		}, {
			xtype: 'button',
			text: '部分入库',
			tooltip: '查询状态为部分入库的验收单',
			itemId: "PartStorage",
			enableToggle: false,
			handler: function() {
				me.onStatusSearch(2);
			}
		}, {
			xtype: 'button',
			text: '全部入库',
			tooltip: '查询状态为全部入库的验收单',
			itemId: "Storage",
			enableToggle: false,
			handler: function() {
				me.onStatusSearch(3);
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'statusSearchButtonToolbar',
			items: items
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);;
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
		//status = buttonsToolbar2.getComponent('DocStatus');
		var search = buttonsToolbar2.getComponent('search');

		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date'),
			dateTypeValue = "bmscensaledocconfirm.AcceptTime"; // dateareaToolbar.getComponent('date');

		var where = [];
		if(me.searchStatusValue != null && parseInt(me.searchStatusValue) > -1)
			where.push("bmscensaledocconfirm.Status=" + me.searchStatusValue);
		var dateValue = date.getValue();
		if(dateValue) {
			if(dateValue.start) {
				where.push(dateTypeValue + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
			}
			if(dateValue.end) {
				where.push(dateTypeValue + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
			}
		}
		if(search) {
			var value = search.getValue();
			if(value) {
				where.push(me.getSearchWhere(value));
			}
		}
		where.push("bmscensaledocconfirm.ReaCompID is not null");
		return where.join(" and ");
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
		var id = record.get("BmsCenSaleDocConfirm_Id");
		var config = {
			title: '验货单操作记录',
			resizable: true,
			width: 428,
			height: 390,
			PK: id,
			className: "BmsCenSaleDocConfirmStatus"
		};
		var win = JShell.Win.open('Shell.class.rea.client.reacheckinoperation.Panel', config);
		win.show();
	},
	/**@description 获取供货验收总单状态参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "BmsCenSaleDocConfirmStatus",
				"classnamespace": "ZhiFang.Digitlab.Entity.ReagentSys"
			}]
		};
		return params;
	},
	/**@description 获取状态信息*/
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
					if(data.value[0].BmsCenSaleDocConfirmStatus.length > 0) {
						me.StatusList.push(["", '全部', 'font-weight:bold;color:black;text-align:center;']);
						Ext.Array.each(data.value[0].BmsCenSaleDocConfirmStatus, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.FontColor) {
								me.StatusFColorEnum[obj.Id] = obj.FontColor;
							}
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor);
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
	/**@description 状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempStatus = me.StatusList;
		return tempStatus;
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
	setBtnDisabled: function(com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if(btn) btn.setDisabled(disabled);
		}
	},
	/**@description 继续验收按钮*/
	onContinueToAcceptClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var status = records[0].get("BmsCenSaleDocConfirm_Status");
		if(status != "0") {
			var statusName = "";
			if(me.StatusEnum != null)
				statusName = me.StatusEnum[status];
			JShell.Msg.alert("当前验收单状态为【" + statusName + "】,不能继续验收操作!", null, 2000);
			return;
		}
		me.fireEvent('onContinueToAcceptClick', me, records[0]);
	},
	onStorageClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var status = records[0].get("BmsCenSaleDocConfirm_Status");
		//已验收,部分入库
		if(status != "1" && status != "2") {
			var statusName = "";
			if(me.StatusEnum != null)
				statusName = me.StatusEnum[status];
			JShell.Msg.alert("当前验收单状态为【" + statusName + "】,不能入库操作!", null, 2000);
			return;
		}
		me.fireEvent('onStorageClick', me, records[0]);
	},
	/**按验收状态快捷查询*/
	onStatusSearch: function(status) {
		var me = this;
		me.setStatusSearchToggle(status);
		me.searchStatusValue = status;
		me.onSearch();
	},
	/**按验收状态按钮状态点击后样式设置*/
	setStatusSearchToggle: function(status) {
		var me = this;
		var buttonsToolbar = me.getComponent('statusSearchButtonToolbar');
		var allStatus = buttonsToolbar.getComponent('AllStatus');
		var apply = buttonsToolbar.getComponent('Apply');
		var accept = buttonsToolbar.getComponent('Accept');
		var partStorage = buttonsToolbar.getComponent('PartStorage');
		var storage = buttonsToolbar.getComponent('Storage');

		switch(status) {
			case 0:
				allStatus.toggle(false);
				apply.toggle(true);
				accept.toggle(false);
				partStorage.toggle(false);
				storage.toggle(false);
				break;
			case 1:
				allStatus.toggle(false);
				apply.toggle(false);
				accept.toggle(true);
				partStorage.toggle(false);
				storage.toggle(false);
				break;
			case 2:
				allStatus.toggle(false);
				apply.toggle(false);
				accept.toggle(false);
				partStorage.toggle(true);
				storage.toggle(false);
				break;
			case 3:
				allStatus.toggle(false);
				apply.toggle(false);
				accept.toggle(false);
				partStorage.toggle(false);
				storage.toggle(true);
				break;
			default:
				allStatus.toggle(true);
				apply.toggle(false);
				accept.toggle(false);
				partStorage.toggle(false);
				storage.toggle(false);
				break;
		}
	},
	/**@description 弹出确认验收录入信息*/
	onOpenCheckForm: function(record) {
		var me = this,
			isError = false;
		JShell.Win.open('Shell.class.rea.client.confirm.basic.CheckForm', {
			resizable: false,
			isError: isError,
			listeners: {
				accept: function(p, data) {
					me.secAccepterType = p.secAccepterType;
					me.onSaveCheckData(p, data,record);
				}
			}
		}).show();
	},
	/**@description 弹出确认验收录入信息确认后*/
	onSaveCheckData: function(p, confirmData, record) {
		var me = this;
		//secAccepterType：默认本实验室(1),供应商(2),供应商或实验室(3)
		var invoiceNoValue = confirmData.InvoiceNo;
		if(invoiceNoValue) {
			invoiceNoValue = invoiceNoValue.replace(/"/g, "");
			invoiceNoValue = invoiceNoValue.replace(/\\/g, '');
			invoiceNoValue = invoiceNoValue.replace(/[\r\n]/g, '');
		}
		var accepterMemoValue = confirmData.AcceptMemo;
		if(accepterMemoValue) {
			accepterMemoValue = accepterMemoValue.replace(/"/g, "");
			accepterMemoValue = accepterMemoValue.replace(/\\/g, '');
			accepterMemoValue = accepterMemoValue.replace(/[\r\n]/g, '');
		}
		if(confirmData.Account) confirmData.Account = confirmData.Account.replace(/"/g, '');
		if(confirmData.Pwd) confirmData.Pwd = confirmData.Pwd.replace(/"/g, '');
		var entity = {
			"Id": record.get("BmsCenSaleDocConfirm_Id"),
			"Status": 1,
			"InvoiceNo": invoiceNoValue,
			"AcceptMemo": accepterMemoValue
		};
		var params = {
			"entity": entity,
			"secAccepterType": me.secAccepterType,
			"secAccepterAccount": confirmData.Account,
			"secAccepterPwd": confirmData.Pwd,
			"fields": "Id,Status,AcceptMemo,InvoiceNo"
		};
		var url = me.confirmUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		JShell.Server.post(url, JcallShell.JSON.encode(params), function(data) {
			if(data.success) {
				JShell.Msg.alert('确认验收成功', null, 1000);
				me.onSearch();
			} else {
				JShell.Msg.error('确认验收失败！' + data.msg);
			}
		});
	}
});