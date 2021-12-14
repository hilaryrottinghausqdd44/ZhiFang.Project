/**
 * 机构关系表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorgcondition.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: '机构关系表单',
	
	width:300,
    height:185,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchCenOrgConditionById?isPlanish=true',
    /**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateCenOrgConditionByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**内容周围距离*/
	bodyPadding:'10px 10px 0 10px',
	/**布局方式*/
	layout:'anchor',
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:55,
        labelAlign:'right'
    },
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		
		items.push({fieldLabel:'主键ID',name:'CenOrgCondition_Id',hidden:true,type:'key'});
		
		//中文名
		items.push({
			fieldLabel:'上级别称',name:'CenOrgCondition_OrgAlias1'
		},{
			fieldLabel:'下级别称',name:'CenOrgCondition_OrgAlias2'
		},{
			fieldLabel:'外接编码',name:'CenOrgCondition_CustomerCode',
			emptyText:'实验室在供应商系统中的编码'
		},{
			fieldLabel:'外接账户',name:'CenOrgCondition_CustomerAccount',
			emptyText:'实验室在供应商系统中的账户'
		});
		
		return items;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			fields = ['Id','OrgAlias1','OrgAlias2','CustomerCode','CustomerAccount'],
			values = me.getForm().getValues();
			
		var entity = {
			fields:fields.join(','),
			entity:{
				Id:values.CenOrgCondition_Id,
				OrgAlias1:values.CenOrgCondition_OrgAlias1,
				OrgAlias2:values.CenOrgCondition_OrgAlias2,
				CustomerCode:values.CenOrgCondition_CustomerCode,
				CustomerAccount:values.CenOrgCondition_CustomerAccount
			}
		};
		return entity;
	}
});