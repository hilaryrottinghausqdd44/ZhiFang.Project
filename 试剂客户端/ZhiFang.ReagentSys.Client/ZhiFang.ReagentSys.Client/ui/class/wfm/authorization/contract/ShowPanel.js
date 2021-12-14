/**
 * 合同信息
 * @author longfc
 * @version 2016-12-28
 */
Ext.define('Shell.class.wfm.authorization.contract.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	requires: [
		'Shell.class.wfm.business.contract.basic.ContentPanel'
	],
	width: 220,
	height: 100,

	title: '合同信息',
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	PClientID: null,
	/**默认加载*/
	defaultLoad: true,
	PK: null,
	/**是隐藏合同名称列*/
	hiddenPContractName:true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
		if(me.defaultLoad == true)
			me.loadData();
	},
	initListeners: function() {
		var me = this;
		me.Grid.on({
			itemclick: function(v, record, item, index, e, eOpts) {
				JShell.Action.delay(function() {
					me.Form.PK = record.get("PContract_Id");
					me.Form.initHtml();
				}, null, 500);
			},
			select: function(rowModel, record, index, eOpts) {
				JShell.Action.delay(function() {
					me.Form.PK = record.get("PContract_Id");
					me.Form.initHtml();
				}, null, 500);
			},
			nodata: function(p) {
				//me.Grid.clearData();
			}
		});
	},
	initComponent: function() {
		var me = this;
		
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Grid = Ext.create('Shell.class.wfm.authorization.contract.Grid', {
			region: 'north',
			width: 300,
			height: 130,
			header: false,
			split: true,
			collapsible: false,
			hiddenPContractName:me.hiddenPContractName,
			/**带功能按钮栏*/
	        hasButtontoolbar: false,
			itemId: 'Grid',
			PClientID: me.PClientID
		});
		me.Form = Ext.create('Shell.class.wfm.business.contract.basic.ContentPanel', {
			region: 'center',
			border:false,
			header: false,
			itemId: 'Form'
		});

		return [me.Grid, me.Form];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			items = me.createButtontoolbar();
			return Ext.create('Ext.toolbar.Toolbar', {
				dock: 'bottom',
				itemId: 'buttonsToolbar',
				items: items
			});
		}
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		return items;
	},
	/**@public加载数据*/
	loadData: function() {
		var me = this;
		if(me.PClientID != null) {
			me.Grid.defaultWhere = 'pcontract.PClientID=' + me.PClientID;
			me.Grid.load(null, true, true);
		}
	}
});