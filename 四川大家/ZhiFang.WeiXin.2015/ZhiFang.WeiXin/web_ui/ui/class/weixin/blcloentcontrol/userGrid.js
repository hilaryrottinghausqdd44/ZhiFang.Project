Ext.define("Shell.class.weixin.blcloentcontrol.userGrid", {
    extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: true,
    /**默认加载数据*/
    defaultLoad: true,
    appType: '',
    /**获取列表数据服务*/
    selectUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL?isPlanish=true',
    multiSelect: true,
    pagingtoolbar: 'number',
    /**默认每页数量*/
    defaultPageSize: 25,
	hasRefresh:true,
	hasSearch:true,
    initComponent: function () {
        var me = this;
        me.columns = [
             {
                dataIndex: 'RBACUser_Id',
                text: '用户编号',
                width: 100,
				hidden:true,
                sortable: false
            }, {
                dataIndex: 'RBACUser_Account',
                editor: true,
                text: '账号',
                width: 100,
                sortable: false
            },{
                dataIndex: 'RBACUser_HREmployee_CName',
                editor: true,
                text: '用户姓名',
                width: 100,
                sortable: false
            }
        ];
		//查询框信息
		me.searchInfo = {width:145,emptyText:'账号',isLike:true,
			fields:['rbacuser.Account']};
        me.callParent(arguments);
    }
});