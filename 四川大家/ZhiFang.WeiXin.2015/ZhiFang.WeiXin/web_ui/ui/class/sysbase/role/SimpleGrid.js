/**
 * 角色列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.role.SimpleGrid', {
	extend: 'Shell.class.sysbase.role.Grid',
	
	/**是否启用新增按钮*/
	hasAdd:false,
	/**是否启用修改按钮*/
	hasEdit:false,
	/**是否启用删除按钮*/
	hasDel:false,
	/**是否启用保存按钮*/
	hasSave:false,
	/**复选框*/
	multiSelect: false,
	selType: 'rowmodel',
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var me = this;
		var columns = [{
			text:'角色名称',dataIndex:'RBACRole_CName',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'系统代码',dataIndex:'RBACRole_UseCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'标准代码',dataIndex:'RBACRole_StandCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'开发商代码',dataIndex:'RBACRole_DeveCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
//		},{
//			xtype:'checkcolumn',text:'使用',dataIndex:'RBACRole_IsUse',
//			width:40,align:'center',sortable:false,menuDisabled:true,
//			stopSelection:false,type:'boolean'
		},{
			text:'显示次序',dataIndex:'RBACRole_DispOrder',width:60,
			sortable:false,menuDisabled:true,defaultRenderer:true,type:'int'
		},{
			text:'模块ID',dataIndex:'RBACRole_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'RBACRole_DataTimeStamp',hidden:true,hideable:false
		}];
		
		return columns;
	}
});