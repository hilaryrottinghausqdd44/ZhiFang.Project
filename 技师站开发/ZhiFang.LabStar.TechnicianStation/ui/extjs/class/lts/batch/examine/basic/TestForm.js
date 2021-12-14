/**
 * 检验单
 * @author liangyl	
 * @version 2021-03-23
 */
Ext.define('Shell.class.lts.batch.examine.basic.TestForm',{
    extend:'Shell.ux.panel.AppPanel',
    title:'检验单',
    hasLoadMask:true,	
	layout: {
	    type: 'hbox',
	    pack: 'start',
	    align: 'stretch'
	},
	border:false,
	SectionID:null,
	    //检验单列表
    TestFormGrid:'Ext.panel.Panel',
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Grid.on({
		    itemdblclick:function(v, record) {
				me.onSearch(v, record);
			},
			select:function(v, record){
				me.onSearch(v, record);
			},
			nodata:function(){
				me.Item.showErrorInView(JShell.Server.NO_DATA);
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
		me.Grid = Ext.create(me.TestFormGrid, {
            flex:1,	header:false,itemId:'Grid',SectionID:me.SectionID
		});
		me.Item = Ext.create('Shell.class.lts.batch.examine.basic.Item', {
			flex:1,header:false,margin:'0px 0px 0px 1px',itemId:'Item'
		});
		return [me.Grid,me.Item];
	},
	//检验单查询
	onSearchTestForm:function(obj){
		var me = this;
		me.Grid.loadData(obj);
	},
	//样本项目查询
	onSearch:function(v, record){
		var me = this;
		JShell.Action.delay(function(){
			var TestFormID = record.get('LisTestForm_Id');
			me.Item.onLoadDataByID(TestFormID);
		},null,500);
	},
	//获取检验单ID列表
	getIdList :function(){
		var me = this;
		return me.Grid.getIdList();
	}
});