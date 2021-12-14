/**
 * 套餐项目选择按钮
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.itemallitem.comitem.BtnPanel',{
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
		me.addEvents('click');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.BtnRight = Ext.create('Ext.button.Button', {
			margin: '5 5 5 5',
		    xtype: 'button',text:'选择',iconCls:'right16',iconAlign:'left',
		    handler: function(){
		    	me.fireEvent('click', me);
		    }
		});
		return [me.BtnRight];
	}
});