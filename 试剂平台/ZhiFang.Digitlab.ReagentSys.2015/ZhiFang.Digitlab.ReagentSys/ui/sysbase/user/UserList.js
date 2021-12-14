/**
 * 用户列表
 * @author Jcall
 * @version 2015-07-07
 */
Ext.define('Shell.sysbase.user.UserList',{
	extend:'Shell.ux.panel.Grid',
	
	title:'用户列表',
	width:1200,
	height:600,
	
	multiSelect:false,
	defaultPageSize:50,
	remoteSort:false,
	pagingtoolbar:'simple',
	/**默认顺序*/
    defaultOrderBy:[],
	
	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true&' +
		'fields=HREmployee_CName,HREmployee_HRDept_Id,HREmployee_HRDept_CName,HREmployee_HRPosition_CName,' +
		'HREmployee_UseCode,HREmployee_IsEnabled,HREmployee_EName,HREmployee_Comment,HREmployee_Id',
	delUrl:'/RBACService.svc/RBAC_UDTO_DelHREmployee',
	
	initComponent:function(){
		var me = this;
		
		me.toolbars = me.createToolbars();
		me.columns = me.createColumns() || [];
		
		me.callParent(arguments);
	},
	createColumns:function(){
		var me = this;
		return [{ 
			xtype:'rownumberer',text:'序号',
			width:50,align:'center'
		},{
			text:'名称',width:90,
			dataIndex:'HREmployee_CName',
			renderer:me.showColoumnTip
		},{
			text:'直属部门',width:90,
			dataIndex:'HREmployee_HRDept_CName',
			renderer:me.showColoumnTip
		},{
			text:'职位',width:90,
			dataIndex:'HREmployee_HRPosition_CName',
			renderer:me.showColoumnTip
		},{
			text:'员工代码',width:60,
			dataIndex:'HREmployee_UseCode',
			renderer:me.showColoumnTip
		},{
			text:'在职',width:40,align:'center',
			dataIndex:'HREmployee_IsEnabled',
			renderer:function(value,meta,record){
				var v = '';
				if(value === null || value === undefined){
					v = ''
				}else if(value === false || value === 'false' || value === 0 || value === '0'){
					v = '<span style="color:red;">否</span>';
				}else{
					v = '<span style="color:green;">是</span>';
				}
				
		        return v;
			}
		},{
			text:'英文名称',width:80,
			dataIndex:'HREmployee_EName',
			renderer:me.showColoumnTip
		},{
			text:'描述',width:100,
			dataIndex:'HREmployee_Comment',
			renderer:me.showColoumnTip
		},{
			text:'主键ID',
			dataIndex:'HREmployee_Id',
			hidden:true,hideable:false
		},{
			text:'部门主键ID',
			dataIndex:'HREmployee_HRDept_Id',
			hidden:true,hideable:false
		}];
	},
	createToolbars:function(){
		var me = this;
		return [{
			dock:'top',itemId:'toptoolbar',
			buttons:['refresh','add','->',{
				btype:'searchtext',emptyText:'名称/直属部门/员工代码',width:160
			}],
			searchFields:'hremployee.CName,hremployee.HRDept.CName,hremployee.UseCode'
		}];
	},
	showColoumnTip:function(value,meta,record){
        if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
        return value;
	},
	/**获取带查询参数的URL*/
	getLoadUrl:function(){
		var me = this,
			url = Shell.util.Path.rootPath + me.selectUrl,
			arr = [];
		
		//默认条件
		if(me.defaultWhere && me.defaultWhere != ''){
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != ''){
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != ''){
			arr.push(me.externalWhere);
		}
		
		var where = 'id=' + me.orgId + '^';
		
		if(arr.length > 0){
			where += '(' + arr.join(" and ") + ')';
		}
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'where=' + Shell.util.String.encode(where);
		
		return url;
	},
	loadByOrgId:function(id){
		var me = this;
		me.orgId = id;
		me.onSearch();
	}
});