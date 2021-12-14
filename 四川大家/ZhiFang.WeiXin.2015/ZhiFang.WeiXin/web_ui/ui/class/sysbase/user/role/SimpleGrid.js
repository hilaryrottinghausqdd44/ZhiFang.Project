/**
 * 角色人员列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.user.role.SimpleGrid',{
    extend: 'Shell.ux.grid.Panel',
    
    width:270,
    height:400,
    
    title:'角色人员列表',
    
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{ property: 'RBACEmpRoles_HREmployee_HRDept_CName', direction: 'ASC' }],
  	
  	/**默认加载数据*/
	defaultLoad:true,
	/**默认选中数据*/
	autoSelect: false,
	/**是否启用查询框*/
	hasSearch: true,
	
	/**角色ids*/
	ROLE_IDS:'',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if (me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'rbacemproles.HREmployee.IsUse=1';
		
		if(me.ROLE_IDS){
			var arr = me.ROLE_IDS.split(',');
			var roleWhere = [];
			for(var i in arr){
				roleWhere.push('rbacemproles.RBACRole.Id=' + arr[i]);
			}
			if(roleWhere.length > 0){
				me.defaultWhere += ' and (' + roleWhere.join(' or ') + ')';
			}
		}
		
		//查询框信息
		me.searchInfo = {
			width: '100%',
			emptyText: '名称',
			isLike: true,
			fields: ['rbacemproles.HREmployee.CName']
		};
		
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'部门',dataIndex:'RBACEmpRoles_HREmployee_HRDept_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'员工',dataIndex:'RBACEmpRoles_HREmployee_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'员工ID',dataIndex:'RBACEmpRoles_HREmployee_Id',hidden:true,hideable:false
		},{
			text:'主键ID',dataIndex:'RBACEmpRoles_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var list = data.list || [],
			len = list.length,
			hash = {},
			result = [];
			
	    for (var i=0;i<len;i++) {
	    	var EmpId = list[i].RBACEmpRoles_HREmployee_Id;
	        if (!hash[EmpId]) {
	            result.push(list[i]);
	            hash[EmpId] = true;
	        }
	    }
		data.list = result;
		
		return data;
	}
});