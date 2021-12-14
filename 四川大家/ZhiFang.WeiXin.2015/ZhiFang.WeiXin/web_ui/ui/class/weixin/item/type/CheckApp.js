/**
 * 上级项目分类树选择
 * @author liangyl	
 * @version 2017-05-26
 */
Ext.define('Shell.class.weixin.item.type.CheckApp',{
    extend:'Shell.ux.panel.AppPanel',
     
    title:'上级分类选择',
    
    width:820,
    height:480,
    autoSelect:true,
    /**是否单选*/
	checkOne:true,
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick:function(v, record){
				JShell.Action.delay(function(){
					me.Tree.AreaID=record.get('ClientEleArea_Id');
					me.Tree.load();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
                    me.Tree.AreaID=record.get('ClientEleArea_Id');
					me.Tree.load();
				},null,500);
			}
		});
		me.Tree.on({
			accept: function(p, record) {
				me.fireEvent('accept',me,record);
			}
		});
	},
    
	initComponent:function(){
		var me = this;
//		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		//区域列表
		me.Grid = Ext.create('Shell.class.weixin.item.type.AreaGrid', {
			region: 'west',
			header: false,
			itemId: 'Grid',
			width:320,
			/**默认加载数据*/
	        defaultLoad:true,
			split: true,
			collapsible: true
		});
		me.Tree = Ext.create('Shell.class.weixin.item.type.CheckTree', {
			region: 'center',
			header: false,
			itemId: 'Tree',
			/**默认加载数据*/
	        defaultLoad:false,
	        selectId: 0
		});
		return [me.Grid,me.Tree];
	}
});