/**
 * 员工选择列表
 * @author liangyl
 * @version 2020-04-07
 */
Ext.define('Shell.class.sysbase.deptemplink.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'员工选择列表',
    width:270,
    height:300,
    
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true',
	
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'hremployee.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,itemId:'search',
			fields:['hremployee.CName']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
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
	},
	initButtonToolbarItems:function(){
		var me = this;
		me.callParent(arguments);
	},
	
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,params = [];
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
		}
		
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	}
});