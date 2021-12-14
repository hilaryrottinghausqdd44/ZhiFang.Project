/**
 * 选择按钮
 * @author liangyl
 * @version 2019-11-20
 */
Ext.define('Shell.class.lts.print.transfer.BtnPanel',{
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
		me.addEvents('checked','addLeft','addRight','clear');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.BtnLeft = Ext.create('Ext.button.Button', {
		    xtype: 'button',text:'选择',iconCls:'left16',iconAlign:'left',
		    handler: function(){
		    	me.fireEvent('addLeft', me);
		    }
		});
		me.BtnRight = Ext.create('Ext.button.Button', {
			margin:"20 0",
		    xtype: 'button',text:'选择',iconCls:'right16',iconAlign:'right',
		    handler: function(){
		    	me.fireEvent('addRight', me);
		    }
		});
		
		me.BtnAccept = Ext.create('Ext.button.Button', {
			margin:"10 0",
		    xtype: 'button',text:'确定',iconCls:'button-accept',
		    handler: function(){
		    	me.fireEvent('checked', me);
		    }
		});
		
		me.BtnClear = Ext.create('Ext.button.Button', {
			margin:"10 0",
		    xtype: 'button',text:'取消',iconCls:'button-cancel',
		    handler: function(){
		    	me.fireEvent('clear', me);
		    }
		});
		return [me.BtnLeft,me.BtnRight,me.BtnAccept,me.BtnClear];
	}
});