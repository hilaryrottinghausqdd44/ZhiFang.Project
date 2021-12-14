/**
 * 选择按钮
 * @author liangyl
 * @version 2019-11-20
 */
Ext.define('Shell.class.lts.batchedit.Btn',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'选择按钮',
	header: false,
	split: true,
	collapsible: true,
	layout: {
	    type: 'vbox',
	    align: 'center',
	    pack:'center'
	},
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
	},
    
	initComponent:function(){
		var me = this;
	    me.addEvents('left','right');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.BtnLeft = Ext.create('Ext.button.Button', {
		    xtype: 'button',text:'选择',iconCls:'left16',iconAlign:'left',
		    handler: function(){
		    	me.fireEvent('left', me);
		    }
		});
		me.BtnRight = Ext.create('Ext.button.Button', {
			margin:"20 0",
		    xtype: 'button',text:'选择',iconCls:'right16',iconAlign:'right',
		    handler: function(){
		    	me.fireEvent('right', me);
		    }
		});
		return [me.BtnLeft,me.BtnRight];
	}
});