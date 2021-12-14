/**
 * 批量错误样本处理
 * @author liangyl	
 * @version 2021-02-23
 */
Ext.define('Shell.class.lts.batch.dislocation.Panel',{
    extend:'Shell.ux.panel.AppPanel',
    title:'批量错误样本处理',
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
		me.LeftGrid = Ext.create('Shell.class.lts.batch.dislocation.LeftGrid', {
            flex:1,	
			itemId:'LeftGrid'
		});
		me.RightGrid = Ext.create('Shell.class.lts.batch.dislocation.RightGrid', {
			flex:1,
			margin:'0px 0px 0px 1px',
			itemId:'RightGrid'
		});
		return [me.LeftGrid,me.RightGrid];
	},
	onSearch:function(TestFormID,EquipFormID){
		var me = this;
		me.LeftGrid.onLoadDataByID(TestFormID);
		me.RightGrid.onLoadDataByID(EquipFormID);
	}
});