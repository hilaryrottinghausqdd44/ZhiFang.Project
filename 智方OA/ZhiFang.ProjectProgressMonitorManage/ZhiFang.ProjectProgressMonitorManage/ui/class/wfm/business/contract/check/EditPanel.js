/**
 * 合同验收
 * @author Jcall
 * @version 2016-11-20
 */
Ext.define('Shell.class.wfm.business.contract.check.EditPanel', {
	extend: 'Shell.class.wfm.business.contract.basic.ActionTabPanel',
	title: '合同验收',
	
	/**通过按钮文字*/
    OverButtonName:'合同验收通过',
    
    /**通过状态文字*/
	OverName:'已验收',
	
	/**合同ID*/
	PK: null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.Attachment.on({
			selectedfilerender: function(p) {
				me.Attachment.save();
			},
			save: function(p) {
				if(me.Attachment.progressMsg!="")
				JShell.Msg.alert(me.Attachment.progressMsg);
			}
		});
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
		items=me.callParent(arguments);
		/**添加附件页签
		 * @author liangyl
		 * @version 2017-01-05
		 */
		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment',{
			title:'合同附件',
			PK:me.PK
		});
	
        items.splice(1,0,me.Attachment);
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