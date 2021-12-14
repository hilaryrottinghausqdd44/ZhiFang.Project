/**
 * 查看列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.show.Grid',{
    extend: 'Shell.class.wfm.task.basic.Grid',
    title:'查看列表',
    /**默认员工类型*/
	defaultUserType:'',
	
	/**默认加载数据*/
	defaultLoad:false,
	  /**是否按部门查询*/
	hasDept:true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				me.openShowForm(id);
			}
		});
	},
   
	/**根据字段和值列表刷新数据*/
	onSearchByFieldAndIds:function(fieldName,id){
		var me = this;
		
		me.defaultWhere = 'ptask.IsUse=1';
		if(fieldName){
			me.defaultWhere += " and ptask." + fieldName + "=" + id;
		}
		
		me.onSearch();
	}
});