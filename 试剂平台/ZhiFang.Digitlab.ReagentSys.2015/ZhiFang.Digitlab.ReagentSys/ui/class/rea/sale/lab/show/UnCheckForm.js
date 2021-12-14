/**
 * 实验室验收取消
 * @author Jcall
 * @version 2017-02-17
 */
Ext.define('Shell.class.rea.sale.lab.show.UnCheckForm', {
	extend: 'Shell.ux.form.Panel',
	title: '实验室验收取消',
	
	/**取消验收服务*/
	uncheckUrl:'/ReagentService.svc/RS_UDTO_ConfirmSaleCancel',
	
	width:340,
	height:170,
	formtype: 'add',
	
	/** 每个组件的默认属性*/
    defaults:{
        labelWidth:55,
        labelAlign:'right'
    },
    /**光标定位延时*/
    focusTimes:200,
    
    /**供货单ID*/
    SaleId:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		setTimeout(function(){
			me.getComponent('AccepterMemo').focus();
		},me.focusTimes);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');
		
		me.items = [{
			x:10,y:10,width:300,allowBlank:false,
			xtype:'textarea',height:60,
			fieldLabel:'取消原因',emptyText:'请输入备注',
			itemId:'AccepterMemo',name:'AccepterMemo'
		},{
			x:10,y:75,width:175,allowBlank:false,
			fieldLabel:'供货确认',emptyText:'请输入账号',
			itemId:'Account',name:'Account'
		},{
			x:190,y:75,width:120,allowBlank:false,
			emptyText:'请输入密码',
			itemId:'Pwd',name:'Pwd',inputType:'password'
		}];
		
		me.buttonToolbarItems = ['->','accept'];
		
		me.callParent(arguments);
	},
	/**更改标题*/
	changeTitle:function(){
		//不处理
	},
	onAcceptClick:function(){
		var me = this;
		if(!me.getForm().isValid()) return;
		
		var values = me.getValues();
		JShell.Msg.confirm({
			msg:'确认取消验收吗？'
		},function(but){
			if (but != "ok") return;
			me.onUncheckSave();
		});
	},
	/**保存取消验收信息*/
	onUncheckSave:function(){
		var me = this,
			url = JShell.System.Path.ROOT + me.uncheckUrl,
			values = me.getValues();
			
		var params = {
			docID:me.SaleId,
			reason:values.AccepterMemo,
			accepterAccount:values.Account,
			accepterPwd:values.Pwd
		};
		
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				id = me.formtype == 'add' ? data.value : id;
				id += '';
				me.fireEvent('save',me,id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});