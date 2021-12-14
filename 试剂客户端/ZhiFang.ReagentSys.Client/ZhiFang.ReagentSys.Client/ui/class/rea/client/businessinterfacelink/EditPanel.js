/**
 * 业务接口关系配置信息
 * @author longfc	
 * @version 2018-11-19
 */
Ext.define('Shell.class.rea.client.businessinterfacelink.EditPanel', {
	extend: 'Ext.panel.Panel',
	title: '业务接口关系配置信息',
	width: 700,
	height: 480,
	autoScroll: false,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding: '1px',
	//业务接口ID
	BusinessID: null,
	//业务接口名称
	BusinessCName: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				}, null, 500);
			},
			addclick: function(p) {
				me.Form.BusinessID = me.BusinessID;
				me.Form.BusinessCName = me.BusinessCName;
				me.Form.isAdd();
			},
			editclick: function(val, CName) {
				var records = me.getSelectionModel().getSelection();
				if(records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id = records[0].get(me.Grid.PKField);
				me.Form.isEdit(id);
			},
			nodata: function(p) {
				me.Form.clearData();
			}
		});
		me.Form.on({
			save: function(p, id) {
				me.Grid.onSearch();
			}
		});
	},
	initComponent: function() {
		var me = this;
		//内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		var maxWidth = document.body.clientWidth - 560;
		if(maxWidth < 0) maxWidth = 620;

		me.Form = Ext.create('Shell.class.rea.client.businessinterfacelink.Form', {
			region: 'south', //east
			title: '业务接口关系配置',
			height: 300,
			width: maxWidth,
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});

		me.Grid = Ext.create('Shell.class.rea.client.businessinterfacelink.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			//业务接口ID
			BusinessID: me.BusinessID,
			//业务接口名称
			BusinessCName: me.BusinessCName
		});
		return [me.Form, me.Grid];
	},
	loadData: function() {
		var me = this;
		me.Grid.BusinessID = me.BusinessID;
		me.Grid.BusinessCName = me.BusinessCName;

		me.Form.BusinessID = me.BusinessID;
		me.Form.BusinessCName = me.BusinessCName;

		me.Grid.onSearch();
	},
	clearData: function() {
		var me = this;

		me.BusinessID = "";
		me.BusinessCName = "";

		me.Grid.BusinessID = "";
		me.Grid.BusinessCName = "";

		me.Form.BusinessID = "";
		me.Form.BusinessCName = "";

		me.Grid.onSearch();
	}
});