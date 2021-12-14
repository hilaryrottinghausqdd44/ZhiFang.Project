/**
 * 角色列表
 * @author liangyl	
 * @version 2018-05-15
 */
Ext.define('Shell.class.rea.center.register.RoleGrid',{
    extend:'Shell.ux.grid.Panel',
   
    title:'角色列表',
    width:240,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL?isPlanish=true',
   
    /**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:false,
	/**是否启用修改按钮*/
	hasEdit:false,
	/**是否启用删除按钮*/
	hasDel:false,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**默认加载数据*/
	defaultLoad:true,
	/**排序字段*/
	defaultOrderBy:[{property:'RBACEmpRoles_DispOrder',direction:'ASC'}],

	hasDel: false,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: false,
    margin: '0 0 1 0',
    /**显示某个角色分类*/
    ROLETYPE:'',
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
//		me.initFilterListeners();
	},
	initComponent:function(){
		var me = this;
		if(!JShell.System.Cookie.map.USERID){
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.defaultWhere = "rbacemproles.HREmployee.Id='"+userId+"'";
			
		//查询框信息
		me.searchInfo = {
			width:180,emptyText:'角色名称/代码',isLike:true,itemId:'Search',
			fields:['rbacemproles.CName','rbacemproles.UseCode']
		};
			
		me.buttonToolbarItems = ['refresh','-',{
			xtype: 'label',text: '版本选择',
			margin: '0 0 8 5',style: "font-weight:bold;color:blue;",
			itemId: 'RoleInfo',name: 'RoleInfo'
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			dataIndex: 'RBACEmpRoles_RBACRole_CName',
			text: '角色名称',
			flex:1,
			defaultRenderer: true
		},{
			dataIndex: 'RBACEmpRoles_RBACRole_UseCode',
			text: '代码',hidden:true,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'RBACEmpRoles_RBACRole_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	},
	
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
//			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
        if(me.ROLETYPE){
        	params.push("rbacemproles.RBACRole.SName='"+me.ROLETYPE+"'");
        }
		if (params.length > 0) {
			me.internalWhere = params.join(' and ');
		}

		return me.callParent(arguments);
	},
	/**获取查询框内容*/
	getSearchWhere: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for (var i = 0; i < len; i++) {
			
			if (isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me=this;
//		if(!data || !data.list) return data;
//		var list = data.list;
//		 me.POrgEnum = {},me.POrgList=[];
//		 me.POrgEnum[0] = '所有机构';
//		for(var i = 0; i < list.length; i++) {
//			var tempArr = [list[i].CenOrg_Id, list[i].CenOrg_CName];
//			me.POrgEnum[list[i].CenOrg_Id] = list[i].CenOrg_CName;
//			me.POrgList.push(tempArr);
//		}
		return data;
    }
});