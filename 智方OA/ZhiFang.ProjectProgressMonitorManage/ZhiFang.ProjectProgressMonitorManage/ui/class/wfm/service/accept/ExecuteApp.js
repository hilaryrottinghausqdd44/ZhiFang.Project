/**
 * 服务处理
 * @author Jcall
 * @version 2015-11-03
 */
Ext.define('Shell.class.wfm.service.accept.ExecuteApp', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '服务处理',
	width:1200,
	height:880,
	PK:null,
	Status:{},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		//详细信息
		me.Info = Ext.create('Shell.class.wfm.service.accept.Info', {
			region:'west',header:false,itemId:'Info',PK:me.PK,
			Status:me.Status,split:true,collapsible:true,width:400
		});
		//交流
		me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App', {
			region:'center',header:false,itemId:'Interaction',
			FormPosition:'s',PK:me.PK
		});

		return [me.Info,me.Interaction];
	}
});