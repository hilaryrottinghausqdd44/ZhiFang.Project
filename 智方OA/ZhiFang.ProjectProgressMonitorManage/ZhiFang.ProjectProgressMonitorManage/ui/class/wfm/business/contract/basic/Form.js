/**
 * 合同基础表单
 * @author Jcall
 * @version 2016-10-31
 */
Ext.define('Shell.class.wfm.business.contract.basic.Form', {
    extend:'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'合同基础表单',
    width:640,
    height:360,
    bodyPadding:10,

    /**新增服务地址*/
    addUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPContract',
    /**修改服务地址*/
    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractByField',

    /**是否启用保存按钮*/
    hasSave:true,
    /**是否重置按钮*/
    hasReset:true,
    
    /**布局方式*/
    layout:{type:'table',columns:3},
    /**每个组件的默认属性*/
    defaults:{labelWidth:70,width:200,labelAlign:'right'},
    
    /*本公司名称*/
    COMPNAME:'OurCorName',

    afterRender: function () {
        var me = this;
        me.callParent(arguments);
    },
    initComponent: function () {
        var me = this;
		
        me.callParent(arguments);
    },
    /**@overwrite 创建内部组件*/
    createItems: function () {
        var me = this;
		
		var items = [{
			fieldLabel:'合同名称',name:'PContract_Name',
			emptyText:'必填项',allowBlank:false,
			colspan:2,width:me.defaults.width * 2
		},{
			fieldLabel:'合同编号',name:'PContract_ContractNumber',
			emptyText:'必填项',allowBlank:false,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'客户选择',name:'PContract_PClientName',itemId:'PContract_PClientName',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.client.CheckGrid',
			colspan:2,width:me.defaults.width * 2
		},{
			fieldLabel:'销售负责人',name:'PContract_Principal',itemId:'PContract_Principal',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.user.CheckApp',
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'付款单位',name:'PContract_PayOrg',itemId:'PContract_PayOrg',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.client.CheckGrid',
			colspan:2,width:me.defaults.width * 2	
		},{
			fieldLabel:'有偿服务',name:'PContract_PaidServiceStartTime',
			emptyText:'必填项',allowBlank:false,
			xtype:'datefield',format:'Y-m-d',
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'软件价格',name:'PContract_Software',itemId:'PContract_Software',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'硬件价格',name:'PContract_Hardware',itemId:'PContract_Hardware',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'签署日期',name:'PContract_SignDate',
			emptyText:'必填项',allowBlank:false,
			xtype:'datefield',format: 'Y-m-d',
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'其它费用',name:'PContract_MiddleFee',itemId:'PContract_MiddleFee',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'合同总额',name:'PContract_Amount',itemId:'PContract_Amount',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'签署人',name:'PContract_SignMan',itemId:'PContract_SignMan',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.user.CheckApp',
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'本公司名称',name:'PContract_Compname',itemId:'PContract_Compname',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title:'本公司名称选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + me.COMPNAME + "'"
			},
			colspan:2,width:me.defaults.width * 2
		},{
		    boxLabel:'是否开具发表',name:'PContract_IsInvoices',itemId:'PContract_IsInvoices',
		    xtype:'checkbox',checked:true,fieldLabel:'&nbsp;',labelSeparator:'',
		    colspan:1,width:me.defaults.width * 1
		},{
            fieldLabel:'备注',name:'PContract_Comment',itemId:'PContract_Comment',
            xtype:'textarea',height:80,
            colspan:3,width:me.defaults.width * 3
		},{
			fieldLabel:'主键ID',name:'PContract_Id',hidden:true
		},{
			fieldLabel:'客户主键ID',name:'PContract_PClientID',itemId:'PContract_PClientID',hidden:true
		},{
        	fieldLabel:'付款单位ID',name:'PContract_PayOrgID',itemId:'PContract_PayOrgID',hidden:true
		},{
        	fieldLabel:'销售负责人主键ID',name:'PContract_PrincipalID',itemId:'PContract_PrincipalID',hidden:true
		},{
        	fieldLabel:'所属部门ID',name:'PContract_DeptID',itemId:'PContract_DeptID',hidden:true
        },{
        	fieldLabel:'签署人ID',name:'PContract_SignManID',itemId:'PContract_SignManID',hidden:true
		},{
			fieldLabel:'本公司ID',name:'PContract_CompnameID',itemId:'PContract_CompnameID',hidden:true
        }];
        
		return items;
	},
	/**更改标题*/
	changeTitle: function() {}
});