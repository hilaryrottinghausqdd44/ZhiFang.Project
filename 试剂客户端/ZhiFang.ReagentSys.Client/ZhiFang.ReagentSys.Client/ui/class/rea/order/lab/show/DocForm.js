/**
 * 供货总单表单-查看
 * @author Jcall
 * @version 2017-07-15
 */
Ext.define('Shell.class.rea.order.lab.show.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
	title: '供货总单表单-查看',
	
	width:240,
    height:300,
    
	
	 /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocById?isPlanish=true',
    
	/**内容周围距离*/
	bodyPadding:'10px 5px 0 10px',
	/**布局方式*/
	layout:'anchor',
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:55,
        labelAlign:'right'
    },
    /**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
    
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
		
		items = [
			{fieldLabel:'主键ID',name:'BmsCenOrderDoc_Id',hidden:true,type:'key'},
			{fieldLabel:'订货单号',name:'BmsCenOrderDoc_OrderDocNo',readOnly:true,locked:true},
			{fieldLabel:'供货方',name:'BmsCenOrderDoc_Comp_CName',readOnly:true,locked:true},
			{fieldLabel:'订货方',name:'BmsCenOrderDoc_Lab_CName',readOnly:true,locked:true},
			{fieldLabel:'订购人员',name:'BmsCenOrderDoc_UserName',readOnly:true,locked:true},
			{
				fieldLabel:'紧急标志',xtype:'uxSimpleComboBox',
				name:'BmsCenOrderDoc_UrgentFlag',
				itemId:'BmsCenOrderDoc_UrgentFlag',
				data:JShell.REA.Enum.getComboboxList('BmsCenOrderDoc_UrgentFlag'),
				allowBlank:false,value:'0'
			},
			{
				fieldLabel:'单据状态',xtype:'uxSimpleComboBox',
				name:'BmsCenOrderDoc_Status',
				itemId:'BmsCenOrderDoc_Status',
				data:JShell.REA.Enum.getComboboxList('BmsCenOrderDoc_Status'),
				allowBlank:false,value:'0'
			},
			{fieldLabel:'订购时间',name:'BmsCenOrderDoc_OperDate',readOnly:true,locked:true},
			{fieldLabel:'订货方备注',name:'BmsCenOrderDoc_LabMemo',readOnly:true,locked:true,xtype:'textarea',height:60},
			{fieldLabel:'供货方备注',name:'BmsCenOrderDoc_CompMemo',readOnly:true,locked:true,xtype:'textarea',height:60}
		];
		
		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.BmsCenOrderDoc_OperDate = JShell.Date.toString(data.BmsCenOrderDoc_OperDate);
		return data;
	}
});