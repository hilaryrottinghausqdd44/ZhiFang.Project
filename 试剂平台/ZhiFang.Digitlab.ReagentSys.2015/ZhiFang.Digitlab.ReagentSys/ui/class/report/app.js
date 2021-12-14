Ext.Loader.setConfig({
	enabled:true,
	paths:{
		'Shell':JShell.System.Path.UI,
		'Ext.ux':JShell.System.Path.UI + '/extjs/ux'
	}
});

var panel = null;
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	
	var params = JShell.Page.getParams(true);
	if(params.LANG){
		JShell.System.Lang = params.LANG;
	}
	
	panel = Ext.create('Shell.class.report.search.App',{header: false});
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		padding:1,
		items:[panel]
	});
});
/**用于结果页面的行点击调用,变更历史对比信息*/
function printResult(PatNo,ItemNo,Table,ReceiveDate){
	var ChartPanel = panel.getComponent('ChartPanel');
		
	if (!ChartPanel) return;

	ChartPanel.serverParams = {
		PatNo:PatNo,
		ItemNo:ItemNo,
		Table:Table,
		ReceiveDate:ReceiveDate
	};
	ChartPanel.onSearch();
}