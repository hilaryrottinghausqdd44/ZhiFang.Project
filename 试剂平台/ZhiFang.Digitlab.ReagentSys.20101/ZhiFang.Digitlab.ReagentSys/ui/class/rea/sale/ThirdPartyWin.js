/**
 * 第三方供货单获取页面
 * @author Jcall
 * @version 2017-05-05
 */
Ext.define('Shell.class.rea.sale.ThirdPartyWin', {
	extend: 'Shell.ux.form.Panel',
	
	title: '第三方供货单获取',
	formtype:'add',
	width:280,
    height:110,
	
	 /**第三方（迈克）供货单数据同步服务地址*/
    selectUrl:'/ReagentService.svc/RS_UDTO_InputSaleDocInterface',
    
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		me.addEvents('success');
		
		me.items = [{
			x:10,y:15,width:254,name:'SaleDocNo',
			emptyText:'供货单号',allowBlank:false
		}];
		
		me.dockedItems = [{
			xtype:'toolbar',
			dock:'bottom',
			itemId:'buttonsToolbar',
			items:['->',{
				text:'同步数据',
				iconCls:'button-import',
				tooltip:'将第三方供货单同步到平台',
				handler:function(){me.onSearchSaleClick();}
			}]
		}];
		
		me.callParent(arguments);
	},
	/**更改标题*/
	changeTitle:function(){},
	/**供货单查询*/
	onSearchSaleClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var values = me.getForm().getValues();
		
		var url = JShell.System.Path.ROOT + me.selectUrl + "?saleDocNo=" + values.SaleDocNo;
		
		me.showMask("供货单数据同步中");//显示遮罩层
		JShell.Server.get(url,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				me.fireEvent('success',me);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});