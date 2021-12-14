/**
 * 客户端订单明细信息列表
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.apply.OrderDtlGrid', {
	extend: 'Shell.class.rea.client.order.basic.EditDtlGrid',

	height: 300,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**用户UI配置Key*/
	userUIKey: 'order.apply.OrderDtlGrid',
	/**用户UI配置Name*/
	userUIName: "订单申请明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		
		//处于新增数据不可变
		if(me.formtype == "add"){
			//不做处理
			return false;
		}else{
			me.getView().update();
			me.store.removeAll();
			if(!me.PK && me.formtype == "add") return false;
			
			me.store.proxy.url = me.getLoadUrl(); //查询条件
			
			me.disableControl(); //禁用 所有的操作功能
			if(!me.defaultLoad) return false;
		}
	}
});