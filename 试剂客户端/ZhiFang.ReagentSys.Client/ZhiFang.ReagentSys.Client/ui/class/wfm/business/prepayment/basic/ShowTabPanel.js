/**
 * 还款信息查看
 * @author liangyl	
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.prepayment.basic.ShowTabPanel',{
    extend: 'Ext.tab.Panel',
    title:'还款信息查看',
    width: 700,
	height: 350,
    autoScroll:false,
    /**任务ID*/
    PK:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this;
		me.ContentPanel = Ext.create('Shell.class.wfm.business.prepayment.basic.ContentPanel',{
			title:'还款信息',
			formtype:'show',
			hasLoadMask:false,//开启加载数据遮罩层
			InfoId:me.PK
		});
		me.Operate = Ext.create('Shell.class.wfm.business.prepayment.operate.Panel', {
			title: '操作记录',
			formtype: 'show',
			itemId: 'Operate',
			hasLoadMask: false,
			PK: me.PK
		});
		me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App',{
			title:'交流信息',
			FormPosition:'e',
			PK:me.PK
		});
		return [me.ContentPanel,me.Operate,me.Interaction];
	}
});