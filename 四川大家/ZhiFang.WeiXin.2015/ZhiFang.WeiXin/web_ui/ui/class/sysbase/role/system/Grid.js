/**
 * 角色列表-开发商功能
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.role.system.Grid', {
	extend: 'Shell.class.sysbase.role.Grid',
	title: '角色列表-开发商功能 ',
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'角色名称',dataIndex:'RBACRole_CName',width:150
		},{
			text:'GUID码',dataIndex:'RBACRole_Id',width:170,isKey:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'角色编码',dataIndex:'RBACRole_UseCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'开发商代码',dataIndex:'RBACRole_DeveCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'RBACRole_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'创建时间',dataIndex:'RBACRole_DataAddTime',width:130,
			isDate:true,hasTime:true
		},{
			text:'角色描述',dataIndex:'RBACRole_Comment',width:200,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'显示次序',dataIndex:'RBACRole_DispOrder',width:60,
			sortable:false,menuDisabled:true,defaultRenderer:true,type:'int'
		}];
		
		return columns;
	}
});