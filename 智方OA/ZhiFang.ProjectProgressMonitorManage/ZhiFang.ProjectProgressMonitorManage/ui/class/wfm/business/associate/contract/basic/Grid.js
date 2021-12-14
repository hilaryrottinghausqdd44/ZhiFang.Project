/**
 * 合同对比关联客户及付款单位
 * @author longfc
 * @version 2017-03-23
 */
Ext.define('Shell.class.wfm.business.associate.contract.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '合同列表',
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractByHQL?isPlanish=false',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractByField',
	defaultOrderBy: [{ property: 'PContract_DispOrder', direction: 'ASC' }],
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
	/**查询合同信息*/
	openShowForm: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		JShell.Win.open('Shell.class.wfm.business.contract.basic.ShowTabPanel', {
			SUB_WIN_NO: '101', //内部窗口编号
			//resizable:false,
			title: '合同信息',
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
			text: '合同编号',
			dataIndex: 'ContractNumber',
			width: 100,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '合同名称',
			dataIndex: 'Name',
			width: 80,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '签署时间',
			dataIndex: 'SignDate',isDate:true,width:85,
			hideable: false,defaultRenderer: true
		}, {
			text: '合同金额',
			dataIndex: 'Amount',
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
			width: 140,
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
			dataIndex: 'PayOrg',
			flex: 1,
			minWidth: 110,
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
			fieldLabel: '',
			emptyText: '省份选择',
			name: 'ProvinceName',
			itemId: 'ProvinceName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.sysbase.country.province.CheckGrid',
			labelWidth: 0,
			width: 120,
			classConfig: {
				height: 450,
				checkOne: true,
				defaultWhere: "bprovince.BCountry.Id=5742820397511247346",
				title: '省份选择'
			},
			listeners: {
				check: function(p, record) {
					var ProvinceName = me.getComponent('buttonsToolbar').getComponent('ProvinceName'),
						ProvinceID = me.getComponent('buttonsToolbar').getComponent('ProvinceID');
					ProvinceName.setValue(record ? record.get('BProvince_Name') : '');
					ProvinceID.setValue(record ? record.get('BProvince_Id') : '');
					p.close();
				},
				change: function() {
					me.onSearch();
				}
			}
		}, {
			fieldLabel: '所属省份ID',
			name: 'ProvinceID',
			itemId: 'ProvinceID',
			hidden: true,
			xtype: 'textfield'
		}, {
			boxLabel: '已对比客户',
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
			boxLabel: '已对比付款单位',
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
			emptyText: '合同编号/合同名称',
			isLike: true,
			itemId: 'search',
			fields: ['pcontract.ContractNumber', 'pcontract.Name']
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
			ProvinceID = buttonsToolbar.getComponent('ProvinceID').getValue();
			isCheckPClient = buttonsToolbar.getComponent('isCheckPClient').getValue();
			isCheckPayOrg = buttonsToolbar.getComponent('isCheckPayOrg').getValue();
			isCheckCheckId = buttonsToolbar.getComponent('isCheckCheckId').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
		}

		if(ProvinceID) {
			params.push("pcontract.ProvinceID=" + ProvinceID + "");
		}

		var tempWhere = "";

		tempWhere = tempWhere + (isCheckPClient == false ? " or pcontract.PClientID is null" : " or pcontract.PClientID is not null");

		tempWhere = tempWhere + (isCheckPayOrg == false ? " or pcontract.PayOrgID is null" : " or pcontract.PayOrgID is not null");
		
		if(me.hiddenCheckCheckId == false) {
			tempWhere = tempWhere + (isCheckCheckId == false ? " or pcontract.CheckId is null" : " or pcontract.CheckId is not null");
			//var temphql=(isCheckCheckId == false ? "pcontract.CheckId is null" : "pcontract.CheckId is not null");
			//params.push(temphql);
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