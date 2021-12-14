/**
 * 合同和发票页签
 * @author liangyl	
 * @version 2016-12-26
 */
Ext.define('Shell.class.wfm.business.receive.preceive.ShowTabPanel',{
    extend: 'Ext.tab.Panel',
    title:'合同和发票页签',
    
    width:800,
	height:500,
     //发票加载
    IsInvoiceLoad:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
      	me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				switch(newCard.itemId) {
					case "InvoiceGrid":
					    if(me.IsInvoiceLoad==false){
					    	me.InvoiceGrid.onSearch();
					    	me.IsInvoiceLoad=true;
					    }
						break;
					default:
						break;
				}
			}
		});
	},
	initComponent:function(){
		var me = this;

		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this;
		
		me.ContractGrid = Ext.create('Shell.class.wfm.business.receive.preceive.ContractGrid', {
			region: 'west',
			split: true,
			header: false,
			width: 380,
			title: '合同',
			collapsible: true,
			itemId: 'ContractGrid'
		});
		me.InvoiceGrid = Ext.create('Shell.class.wfm.business.receive.preceive.InvoiceGrid', {
			region: 'center',
			header: false,
			title: '发票',
			itemId: 'InvoiceGrid'
		});
		return [me.ContractGrid,me.InvoiceGrid];
	}
});