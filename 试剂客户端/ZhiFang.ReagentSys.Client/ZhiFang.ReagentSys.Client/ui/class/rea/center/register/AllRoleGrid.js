/**
 * 所有角色列表
 * @author liangyl	
 * @version 2018-05-15
 */
Ext.define('Shell.class.rea.center.register.AllRoleGrid',{
    extend:'Shell.ux.grid.Panel',
   
    title:'角色选择',
    width:240,
    height:500,
    /**获取数据服务路径*/
    selectUrl:'/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL?isPlanish=true',
	/**默认加载数据*/
	defaultLoad:true,
	/**排序字段*/
	defaultOrderBy:[{property:'RBACRole_DispOrder',direction:'ASC'}],
	hasDel: false,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: false,
    margin: '0 0 1 0',
    /**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	autoSelect:false,
	/**显示某个角色分类*/
    ROLETYPE:'',
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		if(!JShell.System.Cookie.map.USERID){
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
		//查询框信息
		me.searchInfo = {
			width:135,emptyText:'角色名称/代码',isLike:true,itemId:'Search',
			fields:['rbacrole.CName','rbacrole.UseCode']
		};
		me.buttonToolbarItems = ['refresh','-',{
			xtype: 'label',text: '角色选择',
			margin: '0 0 8 5',style: "font-weight:bold;color:blue;",
			itemId: 'RoleInfo',name: 'RoleInfo'
		},'-',{
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'RBACRole_CName',
			text: '角色名称',
			flex:1,
			defaultRenderer: true
		},{
			dataIndex: 'RBACRole_SName',
			text: '角色分类',hidden:true,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'RBACRole_UseCode',
			text: '代码',hidden:true,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'RBACRole_Id',
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
			params = [];
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
	/**@overwrite 改变返回的数据
	 *过滤角色分类me.ROLETYPE的数据
	 * */
	changeResult: function(data) {
		var me=this, result = {},list = [],arr=[];
		for(var i=0;i<data.list.length;i++){
			var SName =data.list[i].RBACRole_SName;
			if(SName!=me.ROLETYPE || !SName){
				arr.push(data.list[i]);
			}
		}
		result.list = arr;
		return result;
	}
   	
});