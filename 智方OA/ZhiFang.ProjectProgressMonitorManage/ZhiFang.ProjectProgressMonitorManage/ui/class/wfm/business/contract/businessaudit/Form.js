/**
 * 合同修改
 * @author liangyl	
 * @version 2016-10-31
 */
Ext.define('Shell.class.wfm.business.contract.businessaudit.Form', {
    extend:'Shell.class.wfm.business.contract.apply.Form',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'合同修改',
    width:640,
    height:360,
    bodyPadding:10,
    hastempSave:false,
    /**状态*/
    ContractStatus:null,
    /**修改服务地址*/
    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractStatus',
   	/**通过按钮文字*/
    OverButtonName:'商务评审通过',
    /**不通过按钮文字*/
    BackButtonName:'商务评审未通过',
    
    /**通过状态文字*/
	OverName:'商务已评',
	/**不通过状态文字*/
	BackName:'评审未通过',
	
	/**处理意见字段*/
	OperMsgField:'ReviewInfo',
	
	/**合同ID*/
	PK: null,
	
	/**表单参数*/
	FormConfig:{
		/**需要保存的 信息*/
		Entity:{
			ReviewManID:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			ReviewMan:JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)
		}
	},
	/**处理意见内容*/
	OperMsg:'',
    initComponent: function () {
        var me = this;
        
        me.addEvents('save');
        
		
        me.callParent(arguments);
    },
	
	//验证客户和合同编号
	changeResult : function(data) {
		var me=this;
		data.PContract_PaidServiceStartTime = JShell.Date.getDate(data.PContract_PaidServiceStartTime);
		data.PContract_SignDate = JShell.Date.getDate(data.PContract_SignDate);
		data.PContract_IsInvoices = data.PContract_IsInvoices == '是' ? true : false;
	
		var PClientName=me.getComponent('PContract_PClientName');
	    PClientName.emptyText = '必填项';
		PClientName.allowBlank = false;
		
		var ContractNumber=me.getComponent('PContract_ContractNumber');
	    ContractNumber.emptyText = '必填项';
		ContractNumber.allowBlank = false;
		return data;
	},
//	 /**通过*/
//	onOver:function(){
//		var me = this;
//		
//		if(me.OperMsgField){
//			//处理意见
//			JShell.Msg.confirm({
//				title:'<div style="text-align:center;">处理意见</div>',
//				msg:'',
//				closable:false,
//				multiline:true//多行输入框
//			},function(but,text){
//				if(but != "ok") return;
//				me.OperMsg = text;
//				me.onSave(me.OverName);
//			});
//		}else{
//			//确定进行该操作
//			JShell.Msg.confirm({
//				msg:'确定进行该操作？'
//			},function(but,text){
//				if(but != "ok") return;
//				me.onSave(me.OverName);
//			});
//		}
//	},
//  /**未通过*/
//	onBack:function(){
//		var me = this;
//		
//		if(me.OperMsgField){
//			//处理意见
//			JShell.Msg.confirm({
//				title:'<div style="text-align:center;">处理意见</div>',
//				msg:'',
//				closable:false,
//				multiline:true//多行输入框
//			},function(but,text){
//				if(but != "ok") return;
//				me.OperMsg = text;
//				me.onSave(me.BackName);
//			});
//		}else{
//			//确定进行该操作
//			JShell.Msg.confirm({
//				msg:'确定进行该操作？'
//			},function(but,text){
//				if(but != "ok") return;
//				me.onSave(me.BackName);
//			});
//		}
//	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		var fields = [
			'Name','ContractNumber','PClientName','Principal',
			'PayOrg','PaidServiceStartTime','Software','Hardware',
			'SignDate','MiddleFee','Amount','SignMan',
			'Compname','IsInvoices','Comment','PClientID',
			'PayOrgID','PrincipalID','SignManID','CompnameID',
			'Id','ContractStatus','EquipOneWayCount','EquipTwoWayCount',
			'Content','ContractServiceCharge','Emphases'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.PContract_Id;
		
		entity.entity.ContractStatus = values.PContract_ContractStatus;
		entity.entity.ReviewInfo=me.OperMsg;
		return entity;
	}
//	/**保存按钮点击处理方法*/
//	onSave:function(StatusName){
//		var me = this,
//			values = me.getForm().getValues();
//		
//		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
//			if(!JShell.System.ClassDict.PContractStatus){
//  			JShell.Msg.error('未获取到合同状态，请重新保存');
//  			return;
//	    	}
//			var info = JShell.System.ClassDict.getClassInfoByName('PContractStatus',StatusName);
//
//			me.getForm().setValues({
//				PContract_ContractStatus:info.Id
//			});
//			me.isValidContractNumber(function(){
//				me.onSaveClick();
//			},me.PK);
//  	});
//	}
});