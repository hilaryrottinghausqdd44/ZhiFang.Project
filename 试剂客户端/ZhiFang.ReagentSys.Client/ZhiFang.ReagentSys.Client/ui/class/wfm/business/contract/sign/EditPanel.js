/**
 * 合同正式签署
 * @author Jcall
 * @version 2016-11-20
 */
Ext.define('Shell.class.wfm.business.contract.sign.EditPanel', {
	extend: 'Shell.class.wfm.business.contract.basic.ActionTabPanel',
	title: '合同正式签署',
	
	/**通过按钮文字*/
    OverButtonName:'正式签署',
    
    /**通过状态文字*/
	OverName:'正式签署',
	
	/**合同ID*/
	PK: null,

    /**合同名称*/
	PContractName: '',
	/**合同总金额*/
	Amount: 0,
	/**合同销售负责人ID*/
	PrincipalID: null,
	/**合同销售负责人*/
	Principal: '',
	/**付款单位*/
	PayOrgID:null,
	/**付款单位*/
	PayOrg:null,
	/**客户*/
	PClientID:null,
	/**客户*/
	PClientName:null,
	/**选择行状态*/
	ContractStatus:null,
    /**创建内部组件*/
	createItems:function(){
		var me = this;
		
		me.Form = Ext.create('Shell.class.wfm.business.contract.sign.ActionForm',Ext.apply({
			title:'合同内容',
			OverButtonName:me.OverButtonName,
		    BackButtonName:me.BackButtonName,
		    OverName:me.OverName,
		    BackName:me.BackName,
		    OperMsgField:me.OperMsgField,
		    ContractStatus:me.ContractStatus,
			PK:me.PK
		},me.FormConfig));
		
		me.Operate = Ext.create('Shell.class.sysbase.scoperation.SCOperation',{
			title:'操作记录',
			classNameSpace:'ZhiFang.Entity.ProjectProgressMonitorManage',//类域
			className:'PContractStatus',//类名
			PK:me.PK
		});
		
		me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App',{
			title:'交流信息',
			FormPosition:'e',
			PK:me.PK
		});
		me.Preceiveplan = Ext.create('Shell.class.wfm.business.receive.preceiveplan.apply.EditGrid',{
			title:'收款计划',
	        Amount: me.Amount,//合同总金额
			PContractID:me.PK,//合同id
			PContractName:me.PContractName,
			PrincipalID: me.PrincipalID,
			Principal: me.Principal,
			/**付款单位*/
			PayOrgID:me.PayOrgID,
			/**付款单位*/
			PayOrg:me.PayOrg,
			/**客户*/
			PClientID:me.PClientID,
			/**客户*/
			PClientName:me.PClientName
		});
			/**liangyl 
		 * 预览pdf
		 * 2017-01-06
		 * */
		me.Pdf = Ext.create('Shell.class.wfm.business.contract.basic.PdfApp', {
			title: '预览PDF',
			itemId: 'Pdf',
			border: false,
			height: me.height,
			width: me.width,
			hasBtntoolbar:false,
			defaultLoad:true,
			PK: me.PK
		});
		return [me.Form,me.Operate,me.Interaction,me.Preceiveplan,me.Pdf];

	}
});