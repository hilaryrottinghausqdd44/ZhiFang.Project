/**
 * 字典列表-开发商功能
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.dict.system.Grid',{
    extend: 'Shell.class.wfm.dict.Grid',
    
    title:'字典列表-开发商功能',
    
	/**创建数据列*/
	createGridColumns:function(){
		var me = this,
			columns = me.callParent(arguments);
		
		columns.splice(1,0,{
			text:'开发商代码',dataIndex:'BDict_DeveCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		});
		
		return columns;
	},
	initEditorListeners:function(){
		
	}
});