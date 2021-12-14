/**
 * 合同邮寄
 * @author liangyl	
 * @version 2017-03-17
 */
Ext.define('Shell.class.wfm.business.contract.mail.Form', {
    extend:'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'合同邮寄表单',
    width:470,
    height:190,
    bodyPadding:10,
	
	/**显示成功信息*/
	showSuccessInfo:false,
	/**获取数据服务路径*/
    selectUrl:'/SingleTableService.svc/ST_UDTO_SearchPContractById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/SingleTableService.svc/ST_UDTO_AddPContract',
    /**修改服务地址*/
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdatePContractByField',
	
    /**布局方式*/
    layout:{type:'table',columns:2},
//	layout: 'anchor',
    /**每个组件的默认属性*/
    defaults:{labelWidth:80,width:210,labelAlign:'right'},
     /**选择行状态*/
    ContractStatus:null,
    
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
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
			fieldLabel:'收货人姓名',name:'PContract_ReceiveName',itemId:'PContract_ReceiveName',
			emptyText:'必填项',allowBlank:false,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'收货人电话',name:'PContract_ReceivePhoneNumbers',
			emptyText:'必填项',allowBlank:false,
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'收货人地址',name:'PContract_ReceiveAddress',
			emptyText:'必填项',allowBlank:false,
			colspan:2,width:me.defaults.width * 2	
		},{
			fieldLabel:'货运单号',name:'PContract_FreightOddNumbers',
			emptyText:'必填项',allowBlank:false,
			colspan:2,width:me.defaults.width * 2	
		},{
			fieldLabel:'货运公司名称',name:'PContract_FreightName',
			emptyText:'必填项',allowBlank:false,
			colspan:2,width:me.defaults.width * 2	
		},{
			fieldLabel:'主键ID',name:'PContract_Id',hidden:true
		},{
        	fieldLabel:'状态',name:'PContract_ContractStatus',itemId:'PContract_ContractStatus',hidden:true
        }];
        
		return items;
	},
	/**更改标题*/
	changeTitle: function() {},
	/**返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		if(!JShell.System.Cookie.map.USERID){
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}    			
		var info = JShell.System.ClassDict.getClassInfoByName('PContractStatus','已邮寄');
		var entity = {
			ReceiveName:values.PContract_ReceiveName,
			ReceiveAddress:values.PContract_ReceiveAddress,
			ReceivePhoneNumbers:values.PContract_ReceivePhoneNumbers,
			FreightName:values.PContract_FreightName,
			FreightOddNumbers:values.PContract_FreightOddNumbers,
			ContractStatus:me.ContractStatus
		};
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		var fields = [
			'ReceiveName','ReceiveAddress','ReceivePhoneNumbers',
			'FreightName','FreightOddNumbers','Id','ContractStatus'
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