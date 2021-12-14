/**
 * 角色选择列表
 * @author Jcall
 * @version 2017-01-19
 */
Ext.define('Shell.class.sysbase.role.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'角色选择列表',
    width:285,
    height:600,
	
	/**获取数据服务路径*/
	selectUrl:'/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL?isPlanish=true',
	
	/**序号列宽度*/
	rowNumbererWidth: 40,
    /**是否单选*/
	checkOne:true,
	
	defaultOrderBy:[{property:'RBACRole_DispOrder',direction:'ASC'}],
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'rbacrole.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			fields:['rbacrole.CName']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'角色名称',dataIndex:'RBACRole_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'角色编码',dataIndex:'RBACRole_UseCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'RBACRole_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});