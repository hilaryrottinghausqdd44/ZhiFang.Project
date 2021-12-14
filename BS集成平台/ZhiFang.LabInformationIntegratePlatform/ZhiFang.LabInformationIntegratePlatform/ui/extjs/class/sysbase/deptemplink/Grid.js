/**
 * 人员部门挂靠
 * @author liangyl
 * @version 2020-04-07
 */
Ext.define('Shell.class.sysbase.deptemplink.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '人员部门挂靠列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 480,
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**是否启用查询框*/
	hasSearch: false,
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptEmpByHQL?isPlanish=true',
	delUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_DelHRDeptEmp',
	defaultOrderBy: [{
		property: 'HRDeptEmp_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: false,

	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems =  [];
			
		buttonToolbarItems.push('refresh','-',{
			fieldLabel:'',labelAlign: 'right',
			emptyText:'科室',labelWidth:0,width: 150,	
			name:'HRDept_CName',itemId:'HRDept_CName',xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.deptemplink.CheckTree',
			classConfig: {
				title: '科室选择',
				/**是否显示根节点*/
	            rootVisible:false
			},
			listeners: {
				check: function(p, record) {
					var	buttonsToolbar = me.getComponent('buttonsToolbar'),
				        Id = buttonsToolbar.getComponent('HRDept_Id'),
			            CName = buttonsToolbar.getComponent('HRDept_CName');
                    if(record==null){
			    		CName.setValue('');
				    	Id.setValue('');
				    	p.close();
			    	    me.onSearch();
			    	    return;
			    	}
			    	if(record.data){
			    		CName.setValue(record.data ? record.data.text : '');
				    	Id.setValue(record.data ? record.data.tid : '');
				    	p.close();
			    	    me.onSearch();
			    	}
				}
			}
		},{
			fieldLabel:'科室ID',hidden:true,
			name:'HRDept_Id',xtype: 'textfield',itemId:'HRDept_Id'
		},{
			fieldLabel:'',labelAlign: 'right',
			emptyText:'员工',labelWidth:0,width: 150,	
			name:'Employee_CName',itemId:'Employee_CName',xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.deptemplink.CheckGrid',
			classConfig: {
				title: '员工选择'
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('buttonsToolbar').getComponent('Employee_CName');
					var ID = me.getComponent('buttonsToolbar').getComponent('Employee_Id');
					CName.setValue(record ? record.get('HREmployee_CName') : '');
					ID.setValue(record ? record.get('HREmployee_Id') : '');
					me.onSearch();
					p.close();
				}
			}
		},{
			fieldLabel:'小组ID',hidden:true,
			name:'Employee_Id',xtype: 'textfield',itemId:'Employee_Id'
		},'-','add','del');
		
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '科室',dataIndex: 'HRDeptEmp_HRDept_CName',
			sortable: true,width: 180,
			menuDisabled: true,defaultRenderer: true
		},{
			text: '员工',dataIndex: 'HRDeptEmp_HREmployee_CName',
			width: 100,sortable: true,
			menuDisabled: true,defaultRenderer: true
		},{
			text: '备注',dataIndex: 'HRDeptEmp_HREmployee_Comment',width: 180,hidden:true,
			sortable: true,menuDisabled: true,defaultRenderer: true
		},{
			text: '主键ID',dataIndex: 'HRDeptEmp_Id',isKey: true,
			hideable: false,hidden:true,defaultRenderer: true
		}]
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function(id) {
		var me = this,
			config = {	
				resizable: false,
				formtype:'add',
				maximizable: false,
				listeners: {
					save:function(p){
						p.close();
						me.onSearch();
					}
				}
			};
		JShell.Win.open('Shell.class.sysbase.deptemplink.Form', config).show();
	},
		/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];
        var	buttonsToolbar = me.getComponent('buttonsToolbar'),
			HRDeptId = buttonsToolbar.getComponent('HRDept_Id').getValue(),
			EmployeeId = buttonsToolbar.getComponent('Employee_Id').getValue();
			
		if(HRDeptId) {
			params.push("hrdeptemp.HRDept.Id=" + HRDeptId);
		}
		if(EmployeeId) {
			params.push("hrdeptemp.HREmployee.Id=" + EmployeeId);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	}
	
});