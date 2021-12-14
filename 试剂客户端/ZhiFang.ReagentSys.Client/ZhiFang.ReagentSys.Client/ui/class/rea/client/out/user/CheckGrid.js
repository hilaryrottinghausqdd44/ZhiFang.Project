/**
 * 员工选择列表
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.client.out.user.CheckGrid',{
    extend:'Shell.class.sysbase.user.CheckGrid',
    title:'员工选择列表',
   
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = me.callParent(arguments);
		columns.push({
			text:'隶属部门id',dataIndex:'HREmployee_HRDept_Id',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		});
		return columns;
	}
});