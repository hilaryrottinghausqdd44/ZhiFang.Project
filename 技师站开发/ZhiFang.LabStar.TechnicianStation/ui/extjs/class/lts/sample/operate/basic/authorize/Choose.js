/**
 * 选择已有预授权
 * @author Jcall
 * @version 2020-09-09
 */
Ext.define('Shell.class.lts.sample.operate.basic.authorize.Choose',{
    extend:'Shell.ux.panel.AppPanel',
    title:'选择已有预授权',
    width:800,
	height:400,
	
	//操作类型ID
	OperateTypeID:'',
	//当前小组
	SectionID:null,
	
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Grid.on({
			itemclick:function(v, record) {
				me.onSearch(record);
			},
			select:function(RowModel, record){
				me.onSearch(record);
			}
		});
		me.Info.on({
			accept:function(obj){
				me.fireEvent('accept',obj);
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	//创建内部组件
	createItems:function(){
		var me = this;
		
		me.Info = Ext.create('Shell.class.lts.sample.operate.basic.authorize.Info', {
			region:'east',width:265,itemId:'Info',
			header:false,split:true,collapsible:true,
			OperateTypeID:me.OperateTypeID,
			SectionID:me.SectionID
		});
		me.Grid = Ext.create('Shell.class.lts.sample.operate.basic.authorize.Grid', {
			region:'center',itemId:'Grid',header:false,
			OperateTypeID:me.OperateTypeID
		});
		
		return [me.Grid,me.Info];
	},
	onSearch:function(record){
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get(me.Grid.PKField);
			me.Info.isEdit(id);
			me.Info.loadDataByID(id);
		},null,200);
	}
});