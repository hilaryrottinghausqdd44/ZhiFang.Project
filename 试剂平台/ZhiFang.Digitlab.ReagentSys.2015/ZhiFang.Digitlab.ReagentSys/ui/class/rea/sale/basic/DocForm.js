/**
 * 供货总单表单-基础
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.basic.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
	title: '供货总单表单-基础',
	
	width:240,
    height:300,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReagentSysService.svc/ST_UDTO_AddBmsCenSaleDoc',
    /**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDocByField',
    
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
		
		items.push({fieldLabel:'主键ID',name:'BmsCenSaleDoc_Id',hidden:true,type:'key'});
		
		//供货单号
		items.push({
			fieldLabel:'供货单号',name:'BmsCenSaleDoc_SaleDocNo',
			emptyText:'必填项',allowBlank:false
		});
		//订货方
		items.push({
			fieldLabel:'订货方',emptyText:'必填项',allowBlank:false,
			name:'BmsCenSaleDoc_Lab_CName',itemId:'BmsCenSaleDoc_Lab_CName',
			xtype:'trigger',triggerCls:'x-form-search-trigger',
			enableKeyEvents:false,editable:false,
			onTriggerClick:function(){
				JShell.Win.open('Shell.class.rea.cenorgcondition.ChildrenCheckGrid',{
					resizable:false,
					CenOrgId:JShell.REA.System.CENORG_ID,
					listeners:{
						accept:function(p,record){me.onLabAccept(record);p.close();}
					}
				}).show();
			}
		},{
			fieldLabel:'订货方主键ID',hidden:true,
			name:'BmsCenSaleDoc_Lab_Id',
			itemId:'BmsCenSaleDoc_Lab_Id'
		});
		//供货方
		items.push({
			fieldLabel:'供货方',
			name:'BmsCenSaleDoc_Comp_CName',
			itemId:'BmsCenSaleDoc_Comp_CName',
			readOnly:true,locked:true
		},{
			fieldLabel:'供货方ID',
			name:'BmsCenSaleDoc_Comp_Id',
			itemId:'BmsCenSaleDoc_Comp_Id',
			hidden:true
		});
		//操作人员
		items.push({
			fieldLabel:'操作人员',
			name:'BmsCenSaleDoc_UserName',
			itemId:'BmsCenSaleDoc_UserName',
			readOnly:true,locked:true
		},{
			fieldLabel:'操作人员ID',
			name:'BmsCenSaleDoc_UserID',
			itemId:'BmsCenSaleDoc_UserID',
			hidden:true
		});
		//紧急标志
		items.push({
			fieldLabel:'紧急标志',xtype:'uxSimpleComboBox',
			name:'BmsCenSaleDoc_UrgentFlag',
			itemId:'BmsCenSaleDoc_UrgentFlag',
			data:JShell.REA.Enum.getList('BmsCenSaleDoc_UrgentFlag'),
			allowBlank:false,value:'0'
		});	
		//单据状态
		items.push({
			fieldLabel:'单据状态',xtype:'uxSimpleComboBox',
			name:'BmsCenSaleDoc_Status',
			itemId:'BmsCenSaleDoc_Status',
			data:JShell.REA.Enum.getList('BmsCenSaleDoc_Status'),
			allowBlank:false,value:'0',
			readOnly:true,allowBlank:false,locked:true
		});
		//操作日期
		items.push({
			xtype:'datefield',fieldLabel:'操作日期',format:'Y-m-d H:m:s',
			name:'BmsCenSaleDoc_OperDate',
			itemId:'BmsCenSaleDoc_OperDate',
			allowBlank:false
		});
		//备注
		items.push({
			xtype:'textarea',fieldLabel:'备注',
			name:'BmsCenSaleDoc_Memo',
			itemId:'BmsCenSaleDoc_Memo',
			height:60
		});
		return items;
	},
	
	/**订货方选择*/
	onLabAccept:function(record){
		var me = this;
		var ComId = me.getComponent('BmsCenSaleDoc_Lab_Id');
		var ComName = me.getComponent('BmsCenSaleDoc_Lab_CName');
		
		ComId.setValue(record.get('CenOrgCondition_cenorg2_Id') || '');
		ComName.setValue(record.get('CenOrgCondition_cenorg2_CName') || '');
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.BmsCenSaleDoc_OperDate = JShell.Date.getDate(data.BmsCenSaleDoc_OperDate);
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		
		var entity = {
			SaleDocNo:values.BmsCenSaleDoc_SaleDocNo,
			UserID:values.BmsCenSaleDoc_UserID,
			UserName:values.BmsCenSaleDoc_UserName,
			CompanyName:values.BmsCenSaleDoc_Comp_CName,
			Memo:values.BmsCenSaleDoc_Memo,
			OperDate:JShell.Date.toServerDate(values.BmsCenSaleDoc_OperDate),
			Status:values.BmsCenSaleDoc_Status,
			UrgentFlag:values.BmsCenSaleDoc_UrgentFlag,
			Lab:{
				Id:values.BmsCenSaleDoc_Lab_Id
			},
			Comp:{
				Id:values.BmsCenSaleDoc_Comp_Id
			}
		};
		
		//如果操作人员不存在不让保存
		entity.UserID = entity.UserID || JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		entity.UserName = entity.UserName || JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		
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
		
		entity.entity.Id = values.BmsCenSaleDoc_Id;
		return entity;
	},
	
	/**新增*/
	isAdd:function(){
		var me = this;
		me.callParent(arguments);
		
		me.initAddInfo();
	},
	/**新增初始化信息*/
	initAddInfo:function(){
		var me = this;
		var BmsCenSaleDoc_UserID = me.getComponent('BmsCenSaleDoc_UserID');
		var BmsCenSaleDoc_UserName = me.getComponent('BmsCenSaleDoc_UserName');
		BmsCenSaleDoc_UserID.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
		BmsCenSaleDoc_UserName.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));
		
		var BmsCenSaleDoc_Comp_Id = me.getComponent('BmsCenSaleDoc_Comp_Id');
		var BmsCenSaleDoc_Comp_CName = me.getComponent('BmsCenSaleDoc_Comp_CName');
		BmsCenSaleDoc_Comp_Id.setValue(JShell.REA.System.CENORG_ID);
		BmsCenSaleDoc_Comp_CName.setValue(JShell.REA.System.CENORG_NAME);
		
		var BmsCenSaleDoc_OperDate = me.getComponent('BmsCenSaleDoc_OperDate');
		BmsCenSaleDoc_OperDate.setValue(new Date());
	}
});