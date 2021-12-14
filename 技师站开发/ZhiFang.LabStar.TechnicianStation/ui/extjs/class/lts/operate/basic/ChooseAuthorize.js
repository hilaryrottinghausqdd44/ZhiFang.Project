/**
 * 选择已有预授权
 * @author liangyl
 * @version 2020-05-11
 */
Ext.define('Shell.class.lts.operate.basic.ChooseAuthorize',{
    extend:'Shell.ux.panel.AppPanel',
    title:'选择已有预授权',
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
	onSearch : function(record){
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get(me.Grid.PKField);
			me.Info.isEdit(id);
			me.Info.loadDataByID(id);
		}, null, 200);
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
		me.Info = Ext.create('Shell.class.lts.operate.basic.Info', {
			region:'east',
			width:265,
			itemId:'Info',
			OperateTypeID:me.OperateTypeID,
			SectionID:me.SectionID,
			header:false,
			split:true,
			collapsible:true
		});
	
		me.Grid = Ext.create('Shell.class.lts.operate.basic.Grid', {
			region:'center',
			itemId:'Grid',
			OperateTypeID:me.OperateTypeID,
			header:false
		});
		
		return [me.Grid,me.Info];
	}
	
});