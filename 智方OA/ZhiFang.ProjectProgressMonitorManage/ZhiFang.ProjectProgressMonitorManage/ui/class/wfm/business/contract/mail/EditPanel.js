/**
 * 合同邮寄
 * @author liangyl	
 * @version 2017-03-17
 */
Ext.define('Shell.class.wfm.business.contract.mail.EditPanel', {
	extend: 'Shell.class.wfm.business.contract.basic.ActionTabPanel',
	title: '合同邮寄',
	
	/**通过按钮文字*/
    OverButtonName:'合同邮寄',
    
    /**通过状态文字*/
	OverName:'已邮寄',
	
	/**合同ID*/
	PK: null,
    /**创建内部组件*/
	createItems:function(){
		var me = this;
		
		me.Form = Ext.create('Shell.class.wfm.business.contract.mail.ActionForm',Ext.apply({
			title:'合同内容',
			OverButtonName:me.OverButtonName,
		    BackButtonName:me.BackButtonName,
		    OverName:me.OverName,
		    BackName:me.BackName,
		    OperMsgField:me.OperMsgField,
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
		return [me.Form,me.Operate,me.Interaction,me.Pdf];

	}
});