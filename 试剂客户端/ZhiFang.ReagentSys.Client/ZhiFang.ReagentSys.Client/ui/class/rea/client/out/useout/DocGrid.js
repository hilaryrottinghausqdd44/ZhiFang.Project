/**
 * 出库总单
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.out.useout.DocGrid', {
	extend: 'Shell.class.rea.client.out.basic.DocGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	/**出库类型*/
	outTypeList: [],
	selectUrl: '/ReaManageService.svc/GetPlatformOutDocListByDBClient?isPlanish=true',
	/**用户UI配置Key*/
	userUIKey: 'out.useout.DocGrid',
	/**用户UI配置Name*/
	userUIName: "出库总单列表",
	/**出库单状态默认选择值*/
	defaultStatus: '9',
	/**默认加载数据*/
	defaultLoad: false,
	/**出库单数据标志Key*/
	IOFlagKey: "ReaBmsOutDocIOFlag",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaBmsOutDocStatus, false, true, null);
		//初始化参数
		me.initOutParams();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	//初始化参数
	initOutParams: function() {
		var me = this;
		me.initRunParams();
		/*me.changeType();
		 var isUseEmpOut = me.IsEmpOut ? 1 : 2;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.selectUrl += '&empId=' + userId + '&type=' + me.typeByHQL + '&isUseEmpOut=' + isUseEmpOut; */
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this;
		var items = []; // me.callParent(arguments);
		if(!items) items = [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDataTypeToolbarItems());
		items.push(me.createDateAreaToolbarItems());
		items.push(me.createDefaultButtonToolbarItems());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			items = [];
		return items;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems: function() {
		var me = this;
		var items = [];
		//查询框信息
		me.searchInfo = {
			emptyText: '出库总单号',
			itemId: 'search',
			flex: 1,
			isLike: true,
			fields: ['reabmsoutdoc.OutDocNo']
		};
		var StatusList = me.removeSomeStatusList();
		items.push({
			name: 'Status',
			itemId: 'Status',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			labelWidth: 60,
			labelAlign: 'right',
			data: StatusList,
			width: 160,
			value: me.defaultStatus,
			listeners: {
				change: function() {
					me.onSearch();
				}
			}
		}, '-', {
			type: 'search',
			info: me.searchInfo
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		var buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			buttonsToolbar4 = me.getComponent('buttonsToolbar4'),
			dateareaToolbar = me.getComponent('dateareaToolbar');

		var date = dateareaToolbar.getComponent('date');
		var dateType = dateareaToolbar.getComponent('dateType');
		var search = buttonsToolbar2.getComponent('search');
		var Status = buttonsToolbar2.getComponent('Status');

		var where = [];
		//where.push("reabmsoutdoc.OutType=7");
		if(Status.getValue()) {
			where.push("reabmsoutdoc.Status=" + Status.getValue());
		}
		
		if(date) {
			var dateValue = date.getValue();
			var dateTypeValue = dateType.getValue();
			if(dateValue && dateTypeValue) {
				if(dateValue.start) {
					where.push(dateTypeValue + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push(dateTypeValue + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
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
		return where.join(" and ");
	},
	/* //根据类型，赋值
	changeType: function() {
		var me = this;
		me.typeByHQL = '4';
	}, */
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List));
		var  outStatusList= [];
		//暂存
		if(tempList[9]) outStatusList.push(tempList[9]);
		if(tempList[10]) outStatusList.push(tempList[10]);
		if(tempList[11]) outStatusList.push(tempList[11]);
		me.searchStatusValue = outStatusList;
		return outStatusList;
	}
});