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
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocConfirmByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenSaleDocConfirmByField',
	/**确认验收服务路径*/
	confirmUrl: "/ReaManageService.svc/ST_UDTO_UpdateReaBmsCenSaleDocConfirmOfConfirmType",
	
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
		property: 'ReaBmsCenSaleDocConfirm_DataAddTime',
		direction: 'DESC'
	}],

	/**默认单据状态*/
	defaultStatusValue: "1",

	/**客户端验收单状态Key*/
	StatusKey: "ReaBmsCenSaleDocConfirmStatus",
	/**供货单数据来源Key*/
	SourceTypeKey: "ReaBmsCenSaleDocSource",
	/**供货单数据标志Key*/
	IOFlagKey: "ReaBmsCenSaleDocIOFlag",

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
		//查询框信息
		me.searchInfo = {
			width: "93%",
			emptyText: '供货单号/供货商',
			itemId: 'search',
			isLike: true,
			fields: ['reabmscensaledocconfirm.SaleDocNo', 'reabmscensaledocconfirm.ReaCompanyName']
		};
		me.addEvents('checkclick');

		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.SourceTypeKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.IOFlagKey, false, true, null);
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.dockedItems.push(me.createStatusSearchButtonToolbar());
		me.dockedItems.push(me.createQuickSearchButtonToolbar());
		me.dockedItems.push(me.createDateAreaToolbarItems());
		me.dockedItems.push(me.createButtonToolbarItems2());
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsCenSaleDocConfirm_DataAddTime',
			text: '验收日期',
			align: 'center',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'ReaBmsCenSaleDocConfirm_ReaCompanyName',
			text: '供货商',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDocConfirm_Status',
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
			dataIndex: 'ReaBmsCenSaleDocConfirm_SaleDocNo',
			text: '供货单号',
			width: 135,
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
			dataIndex: 'ReaBmsCenSaleDocConfirm_SaleDocConfirmNo',
			hidden: true,
			text: '验收单号',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDocConfirm_AccepterName',
			text: '主验收人',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDocConfirm_Memo',
			text: '备注',
			hidden: true,
			hideable: false,
			width: 200,
			renderer: function(value, meta) {
				return "";
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDocConfirm_Id',
			text: '主键ID',
			hidden: true,
			isKey: true
		}, {
			dataIndex: 'ReaBmsCenSaleDocConfirm_ReaCompID',
			text: '供货商ID',
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
		var tempStatus = me.removeSomeStatusList();
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
			handler: function(button, e) {
				me.onStatusSearch(null, button);
			}
		}, {
			xtype: 'button',
			text: '待继续验收',
			tooltip: '查询状态为待继续验收的验收单',
			itemId: "Apply",
			enableToggle: false,
			handler: function(button, e) {
				me.onStatusSearch(0, button);
			}
		}, {
			xtype: 'button',
			text: '已验收 ',
			tooltip: '查询状态为已验收的验收单',
			itemId: "Accept",
			enableToggle: false,
			handler: function(button, e) {
				me.onStatusSearch(1, button);
			}
		}, {
			xtype: 'button',
			text: '部分入库',
			tooltip: '查询状态为部分入库的验收单',
			itemId: "PartStorage",
			enableToggle: false,
			handler: function(button, e) {
				me.onStatusSearch(2, button);
			}
		}, {
			xtype: 'button',
			text: '全部入库',
			tooltip: '查询状态为全部入库的验收单',
			itemId: "Storage",
			enableToggle: false,
			handler: function(button, e) {
				me.onStatusSearch(3, button);
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
		var search = buttonsToolbar2.getComponent('search');

		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date'),
			dateTypeValue = "reabmscensaledocconfirm.AcceptTime";
		var where = [];
		if(me.searchStatusValue != null && parseInt(me.searchStatusValue) > -1)
			where.push("reabmscensaledocconfirm.Status=" + me.searchStatusValue);
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
				var searchHql = me.getSearchWhere(value);
				if(searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
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
		var id = record.get("ReaBmsCenSaleDocConfirm_Id");
		var config = {
			title: '验货单操作记录',
			resizable: true,
			width: 518,
			height: 390,
			PK: id,
			className: me.StatusKey
		};
		var win = JShell.Win.open('Shell.class.rea.client.reacheckinoperation.Panel', config);
		win.show();
	},
	/**@description 状态查询选择项过滤*/
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
		var status = records[0].get("ReaBmsCenSaleDocConfirm_Status");
		if(status != "0") {
			var statusEnum = JShell.REA.StatusList.Status[me.StatusKey].Enum;
			var statusName = "";
			if(statusEnum) {
				statusName = statusEnum[status];
			}
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
		var status = records[0].get("ReaBmsCenSaleDocConfirm_Status");
		//已验收,部分入库
		if(status != "1" && status != "2") {
			var statusEnum = JShell.REA.StatusList.Status[me.StatusKey].Enum;
			var statusName = "";
			if(statusEnum) {
				statusName = statusEnum[status];
			}
			JShell.Msg.alert("当前验收单状态为【" + statusName + "】,不能入库操作!", null, 2000);
			return;
		}
		me.fireEvent('onStorageClick', me, records[0]);
	},
	/**按验收状态快捷查询*/
	onStatusSearch: function(status, button) {
		var me = this;
		me.setStatusSearchToggle(button);
		me.searchStatusValue = status;
		me.onSearch();
	},
	/**按验收状态按钮状态点击后样式设置*/
	setStatusSearchToggle: function(button) {
		var me = this;
		var buttonsToolbar = me.getComponent('statusSearchButtonToolbar');
		var items = buttonsToolbar.items.items;
		Ext.Array.forEach(items, function(item, index) {
			if(item && item.xtype == "button") item.toggle(false);
		});
		button.toggle(true);
	},
	/**获取验收确认的系统参数信息*/
	getSecAccepterType: function(callback) {
		var me = this;
		var secAccepterType = Ext.util.Cookies.get("SecAccepterAccount"); //JcallShell.System.Cookie.get
		if(!secAccepterType) {
			var url = JShell.System.Path.getRootUrl("/SingleTableService.svc/ST_UDTO_SearchBParameterByByParaNo?paraNo=SecAccepterAccount");
			JcallShell.Server.get(url, function(data) {
				if(data.success) {
					var obj = data.value;
					if(obj) {
						var paraValue = parseInt(obj.ParaValue);
						if(paraValue)
							me.secAccepterType = parseInt(paraValue);
						else
							paraValue = me.secAccepterType;
						var days = 30;
						var exp = new Date();
						var expires = exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
						Ext.util.Cookies.set("SecAccepterAccount", paraValue);
					}
					if(callback) callback();
				} else {
					JShell.Msg.error('获取系统参数(验收确认信息)出错！' + data.msg);
				}
			}, false);
		} else {
			me.secAccepterType = parseInt(secAccepterType);
			if(callback) callback();
		}
	},
	/**@description 弹出确认验收录入信息*/
	onOpenCheckForm: function(record) {
		var me = this,
			isError = false;
		me.getSecAccepterType(function() {
			JShell.Win.open('Shell.class.rea.client.confirm.basic.CheckForm', {
				resizable: false,
				isError: isError,
				listeners: {
					accept: function(p, data) {
						me.secAccepterType = p.secAccepterType;
						me.onSaveCheckData(p, data, record);
					}
				}
			}).show();
		});
	},
	/**@description 弹出确认验收录入信息确认后*/
	onSaveCheckData: function(p, confirmData, record) {
		var me = this;
		//secAccepterType：默认本实验室(1),供应商(2),供应商或实验室(3)
		var accepterMemoValue = confirmData.AcceptMemo;
		if(accepterMemoValue) {
			accepterMemoValue = accepterMemoValue.replace(/"/g, "");
			//accepterMemoValue = accepterMemoValue.replace(/\\/g, '');
			//accepterMemoValue = accepterMemoValue.replace(/[\r\n]/g, '');
		}
		if(confirmData.Account) confirmData.Account = confirmData.Account.replace(/"/g, '');
		if(confirmData.Pwd) confirmData.Pwd = confirmData.Pwd.replace(/"/g, '');
		var entity = {
			"Id": record.get("ReaBmsCenSaleDocConfirm_Id"),
			"Status": 1,
			//"InvoiceNo": invoiceNoValue,
			"AcceptMemo": accepterMemoValue
		};
		switch(me.OTYPE) {
			case "reaorder":
				entity.OrderDocID = record.get("ReaBmsCenSaleDocConfirm_OrderDocID");
				break;
			case "reasale":
				entity.SaleDocID = record.get("ReaBmsCenSaleDocConfirm_SaleDocID");
				break;
			default:
				break;
		}
		var params = {
			"entity": entity,
			"secAccepterType": me.secAccepterType,
			"secAccepterAccount": confirmData.Account,
			"secAccepterPwd": confirmData.Pwd,
			"fields": "Id,Status,AcceptMemo",
			//"fields": "Id,Status,AcceptMemo,InvoiceNo",
			"confirmType": me.OTYPE
		};
		var url = me.confirmUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			if(data.success) {
				JShell.Msg.alert('确认验收成功', null, 1000);
				p.close();
				me.onSearch();
			} else {
				JShell.Msg.error('确认验收失败！' + data.msg);
			}
		});
	}
});