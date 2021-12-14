/**
 * 拖动排序列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.grid.DispOrderDrag',{
    extend:'Shell.ux.grid.DragPanel',
    alias:'widget.uxGridDispOrderDrag',
    requires:['Shell.ux.toolbar.Button'],
    
    width:300,
    height:500,
    
    /**带功能按钮栏*/
	hasButtontoolbar:false,
    /**带分页栏*/
    hasPagingtoolbar:false,
    /**后台排序*/
	remoteSort:false,
	/**复选框*/
	multiSelect: true,
    /**列表数据*/
	Data:[],
    
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.on({
			afterdrop:function(panel,node,data,dropRec,dropPosition){
				me.onAfterDrop();
			}
		});
		
		me.store.loadData(me.Data);
		me.enableControl(); //启用所有的操作功能
	},
	initComponent:function(){
		var me = this;
		
		me.columns = [
			{text:'名称',dataIndex:'Name',width:180,defaultRenderer:true},
			{text:'次序',dataIndex:'DispOrder',width:50,align:'center',type:'int'},
			{text:'主键ID',dataIndex:'Id',isKey:true,hidden:true,hideable:false}
		];
		
		me.dockedItems = [{
			xtype:'uxButtontoolbar',
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: ['->','save']
		}];
		
		me.callParent(arguments);
	},
	onAfterDrop:function(){
		var me = this,
			records = me.store.data.items,
			len = records.length;
			
		for(var i=0;i<len;i++){
			records[i].set('DispOrder',i+1);
		}
	},
	onSaveClick:function(){
		var me = this;
		
		var records = me.store.data.items;
		
		me.fireEvent('save',me,records);
	}
});