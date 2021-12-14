/**
 * 合同信息查看面板
 * @author liangyl
 * @version 2017-07-26
 */
Ext.define('Shell.class.wfm.business.contract.search.ShowTabPanel', {
	extend: 'Shell.class.wfm.business.contract.basic.ShowTabPanel',
	title: '合同信息查看面板',

	width:800,
	height:500,
	
	/**合同ID*/
    PK:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	
	/**创建内部组件*/
	createItems:function(){
		var me = this;
	    var items = me.callParent(arguments);
	    
	    me.RcvedRecordGrid = Ext.create('Shell.class.wfm.business.contract.search.RcvedRecordGrid', {
			title: '收款记录',
			header: false,
			layout: 'fit',
			itemId: 'RcvedRecordGrid',
	        defaultLoad: true,
			PContractID:me.PK
		});
	 	me.RcvedRecordHtml = Ext.create('Shell.class.wfm.business.contract.search.RcvedRecordHtml', {
			title: '老收款记录',
			header: false,
			layout: 'fit',
			itemId: 'RcvedRecordHtml',
			PK:me.PK
		});
		
		items.push(me.RcvedRecordGrid,me.RcvedRecordHtml);
		return items;
	}
});