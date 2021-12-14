/**
 * 返回消息
 * @author liangyl	
 * @version 2021-03-23
 */
Ext.define('Shell.class.lts.batch.examine.basic.Info',{
    extend:'Shell.ux.panel.AppPanel',
    title:'消息',
    hasLoadMask:true,	
	layout: {
	    type: 'hbox',
	    pack: 'start',
	    align: 'stretch'
	},
	border:false,
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	
	createItems:function(){
		var me = this;
		me.Smsg = Ext.create('Shell.class.lts.batch.examine.basic.Sinfo', {
            flex:1,	header:false,itemId:'Smsg'
		});
		me.Fmsg = Ext.create('Shell.class.lts.batch.examine.basic.Finfo', {
			flex:1,header:false,margin:'0px 0px 0px 1px',itemId:'Fmsg'
		});
		return [me.Smsg,me.Fmsg];
	},
	//赋值
	setValue:function(smsg,fmsg){
		var me = this;
		me.Smsg.setValue(smsg);
		me.Fmsg.setValue(fmsg);
	}
	
});