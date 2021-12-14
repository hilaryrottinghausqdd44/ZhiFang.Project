/**
 * 业务接口配置列表
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.businessinterface.SimpleGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
	
	title: '业务接口配置列表',
	width: 800,
	height: 500,
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBusinessInterfaceByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBusinessInterface',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBusinessInterfaceByField',
	
	/**默认加载数据*/
	defaultLoad: true,
	/**接口类型Key*/
	InterfaceType: "ReaBusinessInterfaceType",

	/**默认每页数量*/
	defaultPageSize: 50000,
	/**带分页栏*/
	hasPagingtoolbar: false,

	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.InterfaceType, false, false, null);
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBusinessInterface_InterfaceType',
			text: '接口类型',
			sortable: false,
			width: 100,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.InterfaceType].Enum != null)
					v = JShell.REA.StatusList.Status[me.InterfaceType].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.InterfaceType].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.InterfaceType].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.InterfaceType].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.InterfaceType].FColor[value];
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
			dataIndex: 'ReaBusinessInterface_CName',
			text: '接口名称',
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBusinessInterface_URL',
			text: '调用URL入口',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBusinessInterface_AppKey',
			text: 'AppKey',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBusinessInterface_AppPassword',
			text: 'AppPassword',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBusinessInterface_ReaServerLabcCode',
			text: '实验室平台机构码',
			width: 110,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBusinessInterface_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-', {

			emptyText: '接口类型',
			xtype: 'uxSimpleComboBox',
			name: 'InterfaceType',
			itemId: 'InterfaceType',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.InterfaceType].List,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onGridSearch();
				}
			},
			width: 100,
			labelWidth: 0
		}];
		//查询框信息
		me.searchInfo = {
			width: 145,
			isLike: true,
			itemId: 'Search',
			emptyText: '接口名称',
			fields: ['reabusinessinterface.CName']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**综合查询*/
	onGridSearch: function() {
		var me = this;
		JShell.Action.delay(function() {
			me.onSearch();
		}, 100);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			InterfaceType = buttonsToolbar.getComponent('InterfaceType').getValue(),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		me.internalWhere = '';

		if(InterfaceType) {
			params.push("reabusinessinterface.InterfaceType='" + InterfaceType + "'");
		}
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		}

		return me.callParent(arguments);
	}
});