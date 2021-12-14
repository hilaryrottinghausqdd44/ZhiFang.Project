/**
 * 员工选择
 * 添加部门Id列
 * @author liangyl	
 * @version 2018-04-16
 */
Ext.define('Shell.class.rea.client.out.user.CheckApp',{
    extend:'Shell.class.sysbase.user.CheckApp',
    
    
	createItems:function(){
		var me = this;
		me.Tree = Ext.create('Shell.class.sysbase.org.Tree', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			itemId: 'Tree',
			width:200
		});
		me.Grid = Ext.create('Shell.class.rea.client.out.user.CheckGrid', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			checkOne:me.checkOne,
			/**默认加载*/
			defaultLoad:false
		});
		return [me.Tree,me.Grid];
	}
});