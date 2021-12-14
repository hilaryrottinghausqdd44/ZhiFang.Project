/**
 * 员工选择列表 添加列排序
 * @author liangyl 
 * @version 2015-07-02
 */
Ext.define('Shell.class.qms.equip.templet.user.check.CheckGrid',{
    extend:'Shell.class.sysbase.user.CheckGrid',
    title:'员工选择列表',
    width:270,
    height:300,
    
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'员工姓名',dataIndex:'HREmployee_CName',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'员工代码',dataIndex:'HREmployee_UseCode',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'隶属部门',dataIndex:'HREmployee_HRDept_CName',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'隶属部门ID',dataIndex:'HREmployee_HRDept_Id',hidden:true,hideable:false
		},{
			text:'主键ID',dataIndex:'HREmployee_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'HREmployee_DataTimeStamp',hidden:true,hideable:false
		}]
		
		return columns;
	}
});