/**
 * 任务类型选择
 * @author 
 * @version 2016-06-22
 */
Ext.define('Shell.class.wfm.task.type.CheckTree', {
	extend: 'Shell.class.sysbase.dicttree.CheckTree',
	
	title: '任务类型选择',
	width:260,
	height:500,
	rootVisible: false,
	/**默认加载数据*/
	defaultLoad: true,
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**数据变换*/
	changeData: function(data) {
		data = this.callParent(arguments);
		
		if(data.Tree && data.Tree.length == 1){
			data.Tree = data.Tree[0].Tree || [];
		}
		
		return data;
	},
	/**确定按钮处理*/
	onAcceptClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();

		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		var node = records[0];
		//任务类别+任务父类别+任务主类别
		if(node && node.parentNode && node.parentNode.parentNode && !node.parentNode.parentNode.parentNode){
			me.fireEvent('accept', me, node);
		}else{
			JShell.Msg.error('请选择一个任务详细类别，例如：OA->考勤管理');
		}
	}
});