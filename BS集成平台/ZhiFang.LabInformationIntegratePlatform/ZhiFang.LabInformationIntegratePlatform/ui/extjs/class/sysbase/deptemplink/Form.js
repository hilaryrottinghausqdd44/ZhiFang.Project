/**
 * 人员部门挂靠表单
 * @author liangyl
 * @version 2020-04-07
 */
Ext.define('Shell.class.sysbase.deptemplink.Form', {
	extend:'Shell.ux.form.Panel',
	title:'新增挂靠',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width:280,
	height:200,
	bodyPadding:'20px 10px',
	layout:'anchor',
	defaults:{
		anchor:'100%',
		labelWidth:40,
		labelAlign:'right'
	},
	autoScroll:true,
	
	//获取数据服务路径
    selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptEmpById?isPlanish=true',
	//新增服务地址
    addUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddHRDeptEmp',
    //修改服务地址
    editUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_UpdateHRDeptEmpByField',
	/**获取数据服务路径*/
	selectUrl2: '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptEmpByHQL?isPlanish=true',
	//是否启用保存按钮
	hasSave:true,
	//是否重置按钮
	hasReset:true,
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var items = [
			{fieldLabel: '员工',xtype: 'uxCheckTrigger',
				name: 'HRDeptEmp_HREmployee_CName',itemId: 'HRDeptEmp_HREmployee_CName',
			    emptyText:'必填项',allowBlank:false,
			    classConfig: {
					title: '选择员工'
				},
				className: 'Shell.class.sysbase.deptemplink.CheckGrid',
				listeners: {
					check: function(p, record) {
						var CName = me.getComponent('HRDeptEmp_HREmployee_CName');
						var ID = me.getComponent('HRDeptEmp_HREmployee_Id');
						CName.setValue(record ? record.get('HREmployee_CName') : '');
						ID.setValue(record ? record.get('HREmployee_Id') : '');
						p.close();
					}
				}
			}, 
			{fieldLabel:'员工id',name:'HRDeptEmp_HREmployee_Id',itemId:'HRDeptEmp_HREmployee_Id',hidden:true},
			{fieldLabel: '部门',xtype: 'uxCheckTrigger',
				name: 'HRDeptEmp_HRDept_CName',itemId: 'HRDeptEmp_HRDept_CName',
			    emptyText:'必填项',allowBlank:false,
			    classConfig: {
					title: '选择部门',
					/**是否显示根节点*/
		            rootVisible:false
				},
				className: 'Shell.class.sysbase.deptemplink.CheckTree',
				listeners: {
					check: function(p, record) {
						var	Id = me.getComponent('HRDeptEmp_HRDept_Id'),
				            CName = me.getComponent('HRDeptEmp_HRDept_CName');
	                    if(record==null){
				    		CName.setValue('');
					    	Id.setValue('');
					    	p.close();
				    	}
				    	if(record.data){
				    		CName.setValue(record.data ? record.data.text : '');
					    	Id.setValue(record.data ? record.data.tid : '');
					    	p.close();
				    	}
					}
				}
			}, 
			{fieldLabel:'部门id',name:'HRDeptEmp_HRDept_Id',itemId:'HRDeptEmp_HRDept_Id',hidden:true},
			{fieldLabel:'主键ID',name:'HRDeptEmp_Id',hidden:true}
		];
		return items;
	},
	//@overwrite 获取新增的数据
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			HRDept : {
				Id:values.HRDeptEmp_HRDept_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			},
			HREmployee : {
				Id:values.HRDeptEmp_HREmployee_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			},
			IsUse:1
		};
		
		return {entity:entity};
	},
	//@overwrite 获取修改的数据
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];
		
		for(var i in fields){
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		
		entity.entity.Id = values.HRDeptEmp_Id;
		return entity;
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
		/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		if(!me.getForm().isValid()) return;
		var values = me.getForm().getValues();
		me.isCheckData(values.HRDeptEmp_HREmployee_Id,values.HRDeptEmp_HRDept_Id,function(list){
			if(list.length>0 && me.formtype == 'add'){
				JShell.Msg.alert("选择员工已经是所属部门");
				return;
			}
			var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
			url = JShell.System.Path.getRootUrl(url);
			
			var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
			
			if(!params) return;
			
			var id = params.entity.Id;
			
			params = Ext.JSON.encode(params);
			
			me.showMask(me.saveText);//显示遮罩层
			me.fireEvent('beforesave',me);
			JShell.Server.post(url,params,function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					id = me.formtype == 'add' ? data.value : id;
					if(Ext.typeOf(id) == 'object'){
						id = id.id;
					}
					
					if(me.isReturnData){
						me.fireEvent('save',me,me.returnData(id));
					}else{
						me.fireEvent('save',me,id);
					}
					
					if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
				}else{
					me.fireEvent('saveerror',me);
					JShell.Msg.error(data.msg);
				}
			});
		});
		
	},
	/**判断选择的人员是否已存在*/
	isCheckData:function(EmpId,DeptId,callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectUrl2);
			
		var fields = [
			'HRDeptEmp_Id'
		];
		url += '&fields=' + fields.join(',');
		url += '&where=hrdeptemp.HRDept.Id='+DeptId+ ' and hrdeptemp.HREmployee.Id=' + EmpId;
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = data.value ? data.value.list : [];
				callback(list);
			}
		});
	}
});