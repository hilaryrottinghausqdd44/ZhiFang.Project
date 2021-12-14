/**
 * 角色员工列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.jurisdiction.roleuser.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	
	title: '角色员工列表 ',
	width: 800,
	height: 500,
	
	/**角色ID*/
	RoleId:null,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACEmpRoles',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_DelRBACEmpRoles',
	
	/**是否默认勾选*/
	autoSelect:false,
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:500,
	/**分页栏下拉框数据*/
	pageSizeList:[[500,500]],

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	/**序号列宽度*/
	rowNumbererWidth: 40,
	
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:true,
	/**是否启用删除按钮*/
	hasDel:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'员工',dataIndex:'RBACEmpRoles_HREmployee_CName',width:80
		},{
			text:'手机号',dataIndex:'RBACEmpRoles_HREmployee_MobileTel',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'员工代码',dataIndex:'RBACEmpRoles_HREmployee_UseCode',width:80,
			isDate:true,hasTime:true
		},{
			text:'隶属部门',dataIndex:'RBACEmpRoles_HREmployee_HRDept_CName',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'RBACEmpRoles_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'员工ID',dataIndex:'RBACEmpRoles_HREmployee_Id',hidden:true,hideable:false
		}];
		
		return columns;
	},
	
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		JShell.Win.open('Shell.class.sysbase.user.CheckApp',{
			resizable:false,
			title:'员工选择',
			checkOne:false,
			listeners:{
				accept:function(p,records){
					me.onAddLink(p,records);
				}
			}
		}).show();
	},
	
	/**根据角色ID加载数据*/
	loadByRoleId:function(id){
		var me = this;
		me.RoleId = id;
		me.defaultWhere = 'rbacemproles.RBACRole.Id=' + me.RoleId;
		me.onSearch();
	},
	
	/**保存关系*/
	onAddLink:function(p,records){
		var me = this,
			len = records.length,
			ids = [];
			
		for(var i=0;i<len;i++){
			var userId = records[i].get('HREmployee_Id');
			var rec = me.store.findRecord('RBACEmpRoles_HREmployee_Id',userId);
			if(!rec){
				ids.push(userId);
			}
		}
		
		//勾选的人员已在关系中，不需要保存
		if(ids.length == 0){
			p.close();
			return;
		}
		
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = ids.length;
		
		for(var i in ids){
			var userId = ids[i];
			me.addLink(p,userId);
		}
	},
	/**添加一条关系*/
	addLink:function(p,userId){
		var me = this;
		var url = JShell.System.Path.ROOT + me.addUrl;
		var entity = {
			IsUse:true,
			HREmployee:{Id:userId,DataTimeStamp:[0,0,0,0,0,0,0,0]},
			RBACRole:{Id:me.RoleId,DataTimeStamp:[0,0,0,0,0,0,0,0]}
		};
		JShell.Server.post(url,Ext.JSON.encode({entity:entity}),function(data){
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}
			me.onSaveEnd(p);
		});
	},
	onSaveEnd:function(win){
		var me = this;
		if (me.saveCount + me.saveErrorCount == me.saveLength) {
			win.close();
			me.hideMask(); //隐藏遮罩层
			if (me.saveErrorCount == 0){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
			}else{
				JShell.Msg.error('存在失败信息，请重新保存！');
			}
			me.onSearch();
		}
	}
});