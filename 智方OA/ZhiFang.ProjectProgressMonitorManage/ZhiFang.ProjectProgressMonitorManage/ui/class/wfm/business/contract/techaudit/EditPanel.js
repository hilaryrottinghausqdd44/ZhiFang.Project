/**
 * 合同技术评审
 * @author Jcall
 * @version 2016-11-20
 */
Ext.define('Shell.class.wfm.business.contract.techaudit.EditPanel', {
	extend: 'Shell.class.wfm.business.contract.basic.ActionTabPanel',
	title: '合同技术评审',
	
	/**通过按钮文字*/
    OverButtonName:'技术评审通过',
    /**不通过按钮文字*/
    BackButtonName:'技术评审未通过',
    
    /**通过状态文字*/
	OverName:'技术已评',
	/**不通过状态文字*/
	BackName:'评审未通过',
	
	/**处理意见字段*/
	OperMsgField:'TechReviewInfo',
	
	/**合同ID*/
	PK: null,
	
	/**表单参数*/
	FormConfig:{
		/**需要保存的 信息*/
		Entity:{
			TechReviewManID:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			TechReviewMan:JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)
		}
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this;
		var items=me.callParent(arguments);
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
		items.push(me.Pdf);
		return items;
	}
});