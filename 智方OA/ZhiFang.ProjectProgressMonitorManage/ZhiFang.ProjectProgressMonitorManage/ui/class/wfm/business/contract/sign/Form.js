/**
 * 合同申请表单
 * @author liangyl
 * @version 2017-1-5
 */
Ext.define('Shell.class.wfm.business.contract.sign.Form', {
    extend:'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'合同申请表单',
    width:250,
    height:150,
    bodyPadding:10,
	
	/**显示成功信息*/
	showSuccessInfo:false,
	/**获取数据服务路径*/
    selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPContract',
    /**修改服务地址*/
    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractByField',
	
    /**布局方式*/
//  layout:{type:'table',columns:3},
	layout: 'anchor',
    /**每个组件的默认属性*/
    defaults:{labelWidth:70,width:200,labelAlign:'right'},
     /**选择行状态*/
    ContractStatus:null,
    
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.initListeners();
    },
    initComponent: function () {
        var me = this;
        me.buttonToolbarItems = ['->'];
		me.buttonToolbarItems.push({
			text:'提交',
			iconCls:'button-save',
			tooltip:'提交',
			handler:function(){
				me.onSave(true);
			}
		},'reset');
        me.callParent(arguments);
    },
    /**@overwrite 创建内部组件*/
    createItems: function () {
        var me = this;
		
		var items = [{
			fieldLabel:'签署人',name:'PContract_SignMan',itemId:'PContract_SignMan',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',
			className: 'Shell.class.sysbase.user.role.CheckGrid',
			classConfig: {
				checkOne: true,
				searchInfoWidth: '70%',
				RoleHREmployeeCName: "'总经理','副总经理'"
			},
//			className:'Shell.class.sysbase.user.CheckApp',
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'签署日期',name:'PContract_SignDate',
			emptyText:'必填项',allowBlank:false,
			xtype:'datefield',format: 'Y-m-d',
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'主键ID',name:'PContract_Id',hidden:true
		},{
        	fieldLabel:'签署人ID',name:'PContract_SignManID',itemId:'PContract_SignManID',hidden:true
		},{
        	fieldLabel:'状态',name:'PContract_ContractStatus',itemId:'PContract_ContractStatus',hidden:true
        }];
        
		return items;
	},
	/**更改标题*/
	changeTitle: function() {},
	/**返回数据处理方法*/
	changeResult: function(data) {
		data.PContract_SignDate = JShell.Date.getDate(data.PContract_SignDate);
		return data;
	},
	/**初始化监听*/
	initListeners:function(){
		var me = this;
		
		//签署人
		var SignMan = me.getComponent('PContract_SignMan'),
			SignManID = me.getComponent('PContract_SignManID');
		
		SignMan.on({
			check: function(p, record) {
				SignMan.setValue(record ? record.get('RBACEmpRoles_HREmployee_CName') : '');
				SignManID.setValue(record ? record.get('RBACEmpRoles_HREmployee_Id') : '');
				p.close();
			}
		});
	
	},
	
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		if(!JShell.System.Cookie.map.USERID){
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
		var entity = {
			SignDate:JShell.Date.toServerDate(values.PContract_SignDate),//签署日期
			SignMan:values.PContract_SignMan,//签署人
			SignManID:values.PContract_SignManID,
			ContractStatus:values.PContract_ContractStatus//状态
		};
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		var fields = [
			
			'SignDate','SignManID','SignMan',
			'Id','ContractStatus'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.PContract_Id;
		return entity;
	},
	/**保存按钮点击处理方法*/
	onSave:function(isSubmit){
		var me = this,
			values = me.getForm().getValues();
		
		if(!me.getForm().isValid()){
			me.fireEvent('isValid',me);
			return;
		}
		
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
			if(!JShell.System.ClassDict.PContractStatus){
    			JShell.Msg.error('未获取到合同状态，请重新保存');
    			return;
    		}
			me.getForm().setValues({
				PContract_ContractStatus:me.ContractStatus
			});
			if(values.PContract_Id){
				me.onSaveClick();
			}
    	});
	}
});