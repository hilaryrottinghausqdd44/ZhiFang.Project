/**
 * 用户列表
 * @author liangyl	
 * @version 2017-03-29
 */
Ext.define('Shell.class.wfm.business.associate.client.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '用户列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPClientByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePClientByField',
	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelPClient',

	/**默认加载*/
	defaultLoad: true,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: true,
	defaultOrderBy: [{ property: 'PClient_LicenceCode', direction: 'ASC' }],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
		me.on({
			itemdblclick: function(view, record, item, index, e, eOpts) {
				var id = record.get(me.PKField);
				me.openShowForm(id);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '区域',
			dataIndex: 'PClient_ClientAreaName',
			width: 50,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '名称',
			dataIndex: 'PClient_Name',
			flex: 1,
			minWidth:150,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '客户主键ID',
			dataIndex: 'PClient_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '省份',
			dataIndex: 'PClient_ProvinceName',
			hidden: true,
			width: 50,
			hideable: false
		},{
			text: '授权编号',
			dataIndex: 'PClient_LicenceCode',
			width: 60,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '授权名称',
			dataIndex: 'PClient_LicenceClientName',
			width: 100,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主服务器授权号',
			dataIndex: 'PClient_LRNo1',
			width: 90,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '备份服务器授权号',
			dataIndex: 'PClient_LRNo2',
			width: 105,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}];

		return columns;
	},
    /**查询客户信息*/
	openShowForm: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.associate.client.basic.ContentPanel', {
			SUB_WIN_NO: '101', //内部窗口编号
			//resizable:false,
			title: '客户信息',
			PK: id
		}).show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			isLicenceCode = false,
			search = null,
			LicenceCode=null,
			ProvinceID=null,
			params = [];
		//改变默认条件
		me.internalWhere = '';
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar) {
			ProvinceID = buttonsToolbar.getComponent('ProvinceID').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
			LicenceCode= buttonsToolbar.getComponent('LicenceCode').getValue();
			isLicenceCode= buttonsToolbar.getComponent('isLicenceCode').getValue();
		}
		if(ProvinceID) {
			params.push("pclient.ProvinceID=" + ProvinceID + "");
		}
        if(LicenceCode){
        	params.push("pclient.LicenceCode ='" + LicenceCode + "'");
        }
	    if(isLicenceCode==true){
	    	params.push("pclient.LicenceCode  is not null");
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

	
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.push('refresh', '-');
		buttonToolbarItems.push( {
			emptyText: '省份选择',
			width:110,
			labelWidth: 0,
			xtype: 'uxCheckTrigger',
			itemId: 'ProvinceName',
			fieldLabel: '',
			className: 'Shell.class.sysbase.country.province.CheckGrid',
			classConfig: {
				defaultWhere: "bprovince.BCountry.Id=5742820397511247346"
			}
		}, {
			xtype: 'textfield',
			itemId: 'ProvinceID',
			fieldLabel: '省份主键ID',
			hidden: true
		}, {
//			xtype: 'textfield',
			itemId: 'LicenceCode',
			labelWidth: 0,
			emptyText: '授权编码',
			fieldLabel: '',
			width:110,
			hidden: false,
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,
			onTriggerClick:function(){
//				if(me.ownerCt['onSearchClick']){
//					me.ownerCt['onSearchClick'](me,this.getValue());
//				}
			},
			listeners:{
	            keyup:{
	                fn:function(field,e){
	                	if(e.getKey() == Ext.EventObject.ESC){
	                		field.setValue('');
//	                		if(me.ownerCt['onSearchClick']){
//								me.ownerCt['onSearchClick'](me,field.getValue());
//							}
	                	}else if(e.getKey() == Ext.EventObject.ENTER){
							me.onSearch();
	                	}
	                }
	            }
	        }
		},{
			boxLabel: '有授权编码',
			name: 'isLicenceCode',
			itemId: 'isLicenceCode',
			xtype: 'checkbox',
			checked: false,
			value: false,
			fieldLabel: '&nbsp;',
			labelSeparator: '',
			labelWidth: 0,
			width: 90,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		//查询框信息
		me.searchInfo = {
			width: 125,
			emptyText: '名称',
			isLike: true,
			itemId: 'search',
			fields: ['pclient.Name']
		};
		buttonToolbarItems.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},

	/**省份监听*/
	doProvinceListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			CName = buttonsToolbar.getComponent('ProvinceName'),
			Id = buttonsToolbar.getComponent('ProvinceID');

		if(!CName) return;

		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('BProvince_Name') : '');
				Id.setValue(record ? record.get('BProvince_Id') : '');
				me.onSearch();
				p.close();
			}
		});
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		me.doProvinceListeners();
	}
});