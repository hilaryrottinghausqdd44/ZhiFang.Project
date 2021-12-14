/**
 * 商务收款对比关联客户及付款单位
 * @author longfc
 * @version 2017-03-23
 */
Ext.define('Shell.class.wfm.business.associate.finance.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '商务收款',
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPFinanceReceiveByHQL?isPlanish=false',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePFinanceReceiveOfAssociateByField',
	defaultOrderBy: [{ "property": "PFinanceReceive_ReceiveDate", "direction": "DESC" }],
	/**默认加载*/
	defaultLoad: true,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用查询框*/
	hasSearch: false,
	defaultDisableControl: false,
	hiddenCheckCheckId: true,
	/**是否启用序号列*/
	//hasRownumberer: false,
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(view, record, item, index, e, eOpts) {
				me.openShowForm(record, item, index, e, eOpts);
			}
		});
	},
	/**财务收款信息*/
	openShowForm: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		JShell.Win.open('Shell.class.wfm.business.associate.finance.basic.ContentPanel', {
			SUB_WIN_NO: '101', //内部窗口编号
			width: 800,
			height: 380,
			//resizable:false,
			title: '财务收款信息',
			PK: id
		}).show();
	},
	/**改变默认条件*/
	changeDefaultWhere: function() {
		this.defaultWhere = '';
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '收款日期',
			dataIndex: 'ReceiveDate',
			width: 90,
			isDate: true,
			hasTime: false,
			//sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '收款金额',
			dataIndex: 'ReceiveAmount',
			hideable: false,defaultRenderer: true
		},{
			text: '客户ID',
			width: 50,
			dataIndex: 'PClientID',
			sortable: true,
			menuDisabled: true,
			hideable: false
		}, {
			text: '客户名称',
			dataIndex: 'PClientName',
			width: 150,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '付款单位ID',
			dataIndex: 'PayOrgID',
			width: 70,
			sortable: true,
			menuDisabled: true,
			hideable: false
		}, {
			text: '付款单位',
			dataIndex: 'PayOrgName',
			flex: 1,
			minWidth: 120,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '对比人',
			dataIndex: 'ContrastCName',
			width: 80,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '审核人',
			dataIndex: 'CheckCName',
			width: 80,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '对比人Id',
			dataIndex: 'ContrastId',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			hideable: false
		}, {
			text: '主键ID',
			dataIndex: 'Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '审核人Id',
			dataIndex: 'CheckId',
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];

		buttonToolbarItems.push('refresh', '-');
		buttonToolbarItems.push({
			boxLabel: '未对比客户',
			name: 'isCheckPClient',
			itemId: 'isCheckPClient',
			xtype: 'checkbox',
			checked: false,
			value: false,
			fieldLabel: '&nbsp;',
			labelSeparator: '',
			labelWidth: 0,
			width: 100,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			boxLabel: '未对比付款单位',
			name: 'isCheckPayOrg',
			itemId: 'isCheckPayOrg',
			xtype: 'checkbox',
			checked: false,
			value: false,
			fieldLabel: '&nbsp;',
			labelSeparator: '',
			labelWidth: 0,
			width: 120,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			boxLabel: '已审核',
			name: 'isCheckCheckId',
			itemId: 'isCheckCheckId',
			xtype: 'checkbox',
			checked: false,
			value: false,
			fieldLabel: '&nbsp;',
			labelSeparator: '',
			labelWidth: 0,
			width: 80,
			hidden: me.hiddenCheckCheckId,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '客户名称/付款单位',
			isLike: true,
			itemId: 'search',
			fields: ['pfinancereceive.PClientName', 'pfinancereceive.PayOrgName']
		};
		buttonToolbarItems.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			ProvinceID = null,
			isCheckPClient = false,
			isCheckPayOrg = false,
			isCheckCheckId = false,
			search = null,
			params = [];
		//改变默认条件
		me.changeDefaultWhere();
		me.internalWhere = '';
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		if(buttonsToolbar) {
			//ProvinceID = buttonsToolbar.getComponent('ProvinceID').getValue();
			isCheckPClient = buttonsToolbar.getComponent('isCheckPClient').getValue();
			isCheckPayOrg = buttonsToolbar.getComponent('isCheckPayOrg').getValue();
			isCheckCheckId = buttonsToolbar.getComponent('isCheckCheckId').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
		}

		if(ProvinceID) {
			params.push("pfinancereceive.ProvinceID=" + ProvinceID + "");
		}

		var tempWhere = "";
		tempWhere = tempWhere + (isCheckPClient == false ? " or pfinancereceive.PClientID is null" : " or pfinancereceive.PClientID is not null");

		tempWhere = tempWhere + (isCheckPayOrg == false ? " or pfinancereceive.PayOrgID is null" : " or pfinancereceive.PayOrgID is not null");
		if(me.hiddenCheckCheckId == false) {
			tempWhere = tempWhere + (isCheckCheckId == false ? " or pfinancereceive.CheckId is null" : " or pfinancereceive.CheckId is not null");
		}
		
		if(tempWhere.length > 0) {
			tempWhere = tempWhere.substring(3, tempWhere.length);
			params.push("(" + tempWhere + ")");
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
});