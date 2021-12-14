/**
 * 任务二审
 * @author liangyl
 * @version 2017-06-05
 */
Ext.define('Shell.class.wfm.task.new.twoaudit.App', {
    extend:'Shell.class.wfm.task.new.basic.App',
    
    title:'任务二审',
    
    width:600,
    height:400,
    
    createItems: function() {
		var me = this;
		me.Tree = Ext.create('Shell.class.wfm.task.new.basic.Tree', {
			region: 'west',
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			/**二审*/
			TwoAudit: 'ptasktypeemplink.TwoAudit=1'
		});
		me.Grid = Ext.create('Shell.class.wfm.task.new.twoaudit.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.Tree, me.Grid];
	}
});