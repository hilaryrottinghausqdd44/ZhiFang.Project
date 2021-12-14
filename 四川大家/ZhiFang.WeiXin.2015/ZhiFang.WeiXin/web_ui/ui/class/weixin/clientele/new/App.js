/**
 * 实验室维护
 * @author GHX
 * @version 2021-01-11
 */
Ext.define('Shell.class.weixin.clientele.new.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'医疗机构维护',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Grid.on({
            itemclick: function (v, record) {
                me.Form.getComponent("CLIENTELE_Id").setDisabled(true);
                JShell.Action.delay(function () {
                    me.Form.LabId = record.get(me.Grid.PKField);
					me.Form.isEdit(record.get(me.Grid.PKField));
				},null,500);
			},
            select: function (RowModel, record) {
                me.Form.getComponent("CLIENTELE_Id").setDisabled(true);
                JShell.Action.delay(function () {
                    me.Form.LabId = record.get(me.Grid.PKField);
					me.Form.isEdit(record.get(me.Grid.PKField));
				},null,500);
			},
			addclick:function(){
                me.Form.isAdd();
                me.Form.getComponent("CLIENTELE_Id").setDisabled(false);
			},
            editclick: function (p, record) {
                me.Form.getComponent("CLIENTELE_Id").setDisabled(true);
				me.Form.isEdit(record.get(me.Grid.PKField));
			},
            nodata: function (p) {
                me.Form.getComponent("CLIENTELE_Id").setDisabled(true);
				me.Form.clearData();
			}
		});
		me.Form.on({
            save: function (p, id) {
                me.Form.getComponent("CLIENTELE_Id").setDisabled(true);
				me.Grid.onSearch(id);
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.Grid = Ext.create('Shell.class.weixin.clientele.new.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.weixin.clientele.new.Form', {
			region: 'east',
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true
		});
		
		return [me.Grid,me.Form];
	}
});