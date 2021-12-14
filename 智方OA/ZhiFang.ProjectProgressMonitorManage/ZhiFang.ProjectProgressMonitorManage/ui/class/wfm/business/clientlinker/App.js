/**
 * 联系人
 * @author liangyl
 * @version 2017-03-20
 */
Ext.define('Shell.class.wfm.business.clientlinker.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '联系人',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	    var ClientAreaID = me.ClientGrid.getComponent('buttonsToolbar').getComponent('ClientAreaID');
		me.ClientGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.ClientGrid.PKField);
					var name=record.get('PClient_Name');
					if(id=='-1' && ClientAreaID.getValue()){
					   id=me.getClientAreaIDS();
					}
				    me.Grid.PClientID=id;
				    me.Grid.PClientName=name;
				    me.Grid.onSearch();
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
				    var id = record.get(me.ClientGrid.PKField);
				    var name=record.get('PClient_Name');
				    if(id=='-1' && ClientAreaID.getValue()){
					   id=me.getClientAreaIDS();
					}
				    me.Grid.PClientID=id;
				    me.Grid.PClientName=name;
				    me.Grid.onSearch();
				}, null, 500);
			}
		});
	},
	getClientAreaIDS:function(){
		var me=this;
		var IDS='';
		me.ClientGrid.store.each(function(record) {
			if(record.get('PClient_Id')!='-1'){
				 IDS+=","+record.get('PClient_Id');
			}
        });
        IDS=0==IDS.indexOf(",")?IDS.substr(1):IDS;
		return IDS;
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.ClientGrid = Ext.create('Shell.class.wfm.business.clientlinker.ClientGrid', {
			region: 'west',
			split: true,
			header: false,
			width: 350,
			title: '财务收款详情',
			collapsible: true,
			itemId: 'ClientGrid'
		});
		me.Grid = Ext.create('Shell.class.wfm.business.clientlinker.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.ClientGrid, me.Grid];
	}
});