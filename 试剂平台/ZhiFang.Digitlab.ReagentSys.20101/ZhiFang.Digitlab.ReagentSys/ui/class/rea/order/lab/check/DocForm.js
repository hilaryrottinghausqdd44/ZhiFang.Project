/**
 * 实验室确定订单
 * @author Jcall
 * @version 2017-03-06
 */
Ext.define('Shell.class.rea.order.lab.check.DocForm', {
	extend: 'Shell.ux.form.Panel',
	title: '实验室确定订单',
	
	width:240,
    height:300,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocById?isPlanish=true',
    /**修改服务地址，自动生成供货单*/
    editUrl:'/ReagentService.svc/RS_UDTO_BmsCenSaleDocByOrderDoc',
	/**内容周围距离*/
	bodyPadding:'5px 10px 0 10px',
	/**布局方式*/
	layout:'anchor',
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:55,
        labelAlign:'right'
    },
    /**系统自动生成供货单的状态值*/
    IsAutoCreateBmsCenOrderDoc:'2',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.hideSaveButton();
		me.on('load',function(p,data){
			me.changeStatusDiv(data);
			if(data.value.BmsCenOrderDoc_Status == "1"){
				me.showSaveButton();
			}else{
				me.hideSaveButton();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		
		me.buttonToolbarItems = [{
			xtype:'label',
			itemId:'StatusDiv',
			height:22
		},'->',{
			text:'订单确认',
			itemId:'save',
			tooltip:'订单确认',
			iconCls:'button-save',
			handler:function(){
				me.updateOrder();
			}
		}];
		
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
			{fieldLabel:'操作人员',name:'BmsCenOrderDoc_UserName',readOnly:true,locked:true},
			{fieldLabel:'提交时间',name:'BmsCenOrderDoc_OperDate',readOnly:true,locked:true,xtype:'datefield',format:'Y-m-d H:m:s'},
			{fieldLabel:'订货方备注',name:'BmsCenOrderDoc_LabMemo',readOnly:true,locked:true,xtype:'textarea',height:60},
			{fieldLabel:'供货方备注(可填)',name:'BmsCenOrderDoc_CompMemo',
				itemId:'BmsCenOrderDoc_CompMemo',xtype:'textarea',height:60},
			
			{fieldLabel:'紧急标志',name:'BmsCenOrderDoc_UrgentFlag',hidden:true},
			{fieldLabel:'单据状态',name:'BmsCenOrderDoc_Status',hidden:true}
		];
		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.BmsCenOrderDoc_OperDate = JShell.Date.toString(data.BmsCenOrderDoc_OperDate,true);
		data.BmsCenOrderDoc_LabMemo = data.BmsCenOrderDoc_LabMemo.replace(/<BR\/>/g,"\n");
		data.BmsCenOrderDoc_CompMemo = data.BmsCenOrderDoc_CompMemo.replace(/<BR\/>/g,"\n");
		return data;
	},
	/**更改状态值*/
	changeStatusDiv:function(data){
		var me = this,
			StatusDiv = me.getComponent('buttonsToolbar').getComponent('StatusDiv');
			
		if(!StatusDiv) return;
			
		var UrgentFlag = JShell.REA.Enum.BmsCenOrderDoc_UrgentFlag['E' + data.value.BmsCenOrderDoc_UrgentFlag] || {};
		var Status = JShell.REA.Enum.BmsCenOrderDoc_Status['E' + data.value.BmsCenOrderDoc_Status] || {};
			
		var style="float:left;width:60px;padding:2px 5px;margin:1px 5px;";
		var html = 
		'<div style="float:left;text-align:center;margin-bottom:5px;">' +
			'<div style="' + style + 'background-color:' + (UrgentFlag.bcolor || '#FFFFFF') + 
				';color:' + (UrgentFlag.color || '#000000') + ';">' +
				(UrgentFlag.value || '') +
			'</div>' +
			'<div style="' + style + 'background-color:' + (Status.bcolor || '#FFFFFF') + 
				';color:' + (Status.color || '#000000') + ';">' +
				(Status.value || '') +
			'</div>' +
		'</div>';
			
		StatusDiv.setText(html,false);
	},
	/**显示保存按钮*/
	showSaveButton:function(bo){
		var me = this,
			save = me.getComponent('buttonsToolbar').getComponent('save'),
			CompMemo = me.getComponent('BmsCenOrderDoc_CompMemo');
			
		if(bo === false){
			save.hide();
			CompMemo.setReadOnly(true);
		}else{
			save.show();
			CompMemo.setReadOnly(false);
		}
	},
	/**隐藏保存按钮*/
	hideSaveButton:function(){
		this.showSaveButton(false);
	},
	updateOrder:function(){
		var me = this,
			url = JShell.System.Path.getUrl(me.editUrl),
			values = me.getForm().getValues();
			
		var params = {
			entity:{
				Id:values.BmsCenOrderDoc_Id,
				Status:me.IsAutoCreateBmsCenOrderDoc,
				CompMemo:values.BmsCenOrderDoc_CompMemo,
				UserID:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
				UserName:JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)
			},
			fields:'Id,Status,CompMemo,UserID,UserName'
		};
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				me.fireEvent('save',me,values.BmsCenOrderDoc_Id);
				if(me.showSuccessInfo){
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});