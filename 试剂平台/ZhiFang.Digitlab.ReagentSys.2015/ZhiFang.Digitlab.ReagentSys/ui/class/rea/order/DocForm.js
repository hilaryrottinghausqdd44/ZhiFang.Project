/**
 * 供货总单表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.order.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
	title: '供货总单表单',
	
	width:240,
    height:300,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReagentSysService.svc/ST_UDTO_AddBmsCenOrderDoc',
    /**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDocByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
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
    
    /***/
    CenOrgId:null,
    CenOrgName:null,
	
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
		
		items.push({fieldLabel:'主键ID',name:'BmsCenOrderDoc_Id',hidden:true,type:'key'});
		
		//供货单号
		items.push({
			fieldLabel:'订货单号',name:'BmsCenOrderDoc_OrderDocNo'
			//emptyText:'必填项',allowBlank:false
		});
		//供货方
		items.push({
			fieldLabel:'供货方',emptyText:'必填项',allowBlank:false,
			name:'BmsCenOrderDoc_Comp_CName',itemId:'BmsCenOrderDoc_Comp_CName',
			xtype:'trigger',triggerCls:'x-form-search-trigger',
			enableKeyEvents:false,editable:false,
			onTriggerClick:function(){
				JShell.Win.open('Shell.class.rea.cenorgcondition.ParentCheckGrid',{
					resizable:false,
					CenOrgId:JShell.REA.System.CENORG_ID,
					listeners:{
						accept:function(p,record){me.onCompAccept(record);p.close();}
					}
				}).show();
			}
		},{
			fieldLabel:'供货方主键ID',hidden:true,
			name:'BmsCenOrderDoc_Comp_Id',
			itemId:'BmsCenOrderDoc_Comp_Id'
		});
		//供货方
		items.push({
			fieldLabel:'订货方',
			name:'BmsCenOrderDoc_Lab_CName',
			itemId:'BmsCenOrderDoc_Lab_CName',
			readOnly:true,allowBlank:false,locked:true
		},{
			fieldLabel:'订货方ID',
			name:'BmsCenOrderDoc_Lab_Id',
			itemId:'BmsCenOrderDoc_Lab_Id',
			hidden:true
		});
		//操作人员
		items.push({
			fieldLabel:'操作人员',
			name:'BmsCenOrderDoc_UserName',
			itemId:'BmsCenOrderDoc_UserName',
			readOnly:true,allowBlank:false,locked:true
		},{
			fieldLabel:'操作人员ID',
			name:'BmsCenOrderDoc_UserID',
			itemId:'BmsCenOrderDoc_UserID',
			hidden:true
		});
		//紧急标志
		items.push({
			fieldLabel:'紧急标志',xtype:'uxSimpleComboBox',
			name:'BmsCenOrderDoc_UrgentFlag',
			itemId:'BmsCenOrderDoc_UrgentFlag',
			data:JShell.REA.Enum.getComboboxList('BmsCenOrderDoc_UrgentFlag'),
			allowBlank:false,value:'0'
		});	
		//单据状态
		items.push({
			fieldLabel:'单据状态',xtype:'uxSimpleComboBox',
			name:'BmsCenOrderDoc_Status',
			itemId:'BmsCenOrderDoc_Status',
			data:JShell.REA.Enum.getComboboxList('BmsCenOrderDoc_Status'),
			allowBlank:false,value:'0'
		});
		//提交日期
		items.push({
			xtype:'datefield',fieldLabel:'订购时间',format:'Y-m-d H:m:s',
			name:'BmsCenOrderDoc_OperDate',
			itemId:'BmsCenOrderDoc_OperDate',
			allowBlank:false
		});
		//备注
		items.push({
			xtype:'textarea',fieldLabel:'备注',
			name:'BmsCenOrderDoc_Memo',
			itemId:'BmsCenOrderDoc_Memo',
			height:60
		});
		return items;
	},
	onCompAccept:function(record){
		var me = this;
		var ComId = me.getComponent('BmsCenOrderDoc_Comp_Id');
		var ComName = me.getComponent('BmsCenOrderDoc_Comp_CName');
		
		ComId.setValue(record.get('CenOrgCondition_cenorg1_Id') || '');
		ComName.setValue(record.get('CenOrgCondition_cenorg1_CName') || '');
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.BmsCenOrderDoc_OperDate = JShell.Date.getDate(data.BmsCenOrderDoc_OperDate);
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			OrderDocNo:values.BmsCenOrderDoc_OrderDocNo,
			UserID:values.BmsCenOrderDoc_UserID,
			UserName:values.BmsCenOrderDoc_UserName,
			LabName:values.BmsCenOrderDoc_Lab_CName,
			CompanyName:values.BmsCenOrderDoc_Comp_CName,
			Memo:values.BmsCenOrderDoc_Memo,
			OperDate:JShell.Date.toServerDate(values.BmsCenOrderDoc_OperDate),
			Status:values.BmsCenOrderDoc_Status,
			UrgentFlag:values.BmsCenOrderDoc_UrgentFlag,
			Lab:{
				Id:values.BmsCenOrderDoc_Lab_Id
			},
			Comp:{
				Id:values.BmsCenOrderDoc_Comp_Id
			}
		};
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
			
		var fields = [
			'Id','SaleDocNo','UserID','UserName','CompanyName','Memo',
			'OperDate','Status','UrgentFlag','Lab_Id','Comp_Id'
		];
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.BmsCenOrderDoc_Id;
		return entity;
	},
	isAdd:function(){
		var me = this;
		me.callParent(arguments);
		
		var BmsCenOrderDoc_UserID = me.getComponent('BmsCenOrderDoc_UserID');
		var BmsCenOrderDoc_UserName = me.getComponent('BmsCenOrderDoc_UserName');
		BmsCenOrderDoc_UserID.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
		BmsCenOrderDoc_UserName.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));
		
		var BmsCenOrderDoc_Lab_Id = me.getComponent('BmsCenOrderDoc_Lab_Id');
		var BmsCenOrderDoc_Lab_CName = me.getComponent('BmsCenOrderDoc_Lab_CName');
		BmsCenOrderDoc_Lab_Id.setValue(JShell.REA.System.CENORG_ID);
		BmsCenOrderDoc_Lab_CName.setValue(JShell.REA.System.CENORG_NAME);
		
		var BmsCenOrderDoc_OperDate = me.getComponent('BmsCenOrderDoc_OperDate');
		BmsCenOrderDoc_OperDate.setValue(new Date());
	}
});