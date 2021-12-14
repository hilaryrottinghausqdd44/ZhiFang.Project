Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});
var panel = null;
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	
	//Shell.util.Win.begin();//屏蔽快捷键
	
	panel = Ext.create('Shell.ReportPrint_TaiDa.class.PrintApp',{header:false});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		padding:2,
		items:[panel]
	});
});

/**用于结果页面的行点击调用,变更历史对比信息*/
function printResult(PatNo,ItemNo,Table,ReceiveDate){
	var PrintChart = panel.getComponent('PrintChart');
		
	if (!PrintChart) return;

	PrintChart.load({
		PatNo:PatNo,
		ItemNo:ItemNo,
		Table:Table,
		ReceiveDate:ReceiveDate
	});
}
/**支持数据内容刷新*/
function refreshData(url){
	if(!url){
		panel.showError("参数未传递到本程序!");
		panel.clearData();
		return;
	}
	
	if(url.indexOf("?") == -1) return;
   	var arr = url.split("?")[1],
		strs = arr.split("&"),
		len = strs.length,
		params = {};
		
	for(var i=0;i<len;i++){
		var arr = strs[i].split("=");
		params[arr[0]] = decodeURI(arr[1]);
	}
	
	if(!params.patNO){
		PrintList.showError("缺少参数:patNO(病历号)未传递到本程序!");
		panel.clearData();
		return;
	}
	
	var PrintList = panel.getComponent("PrintList");
	PrintList.defaultWhere = "patNO='" + params.patNO + "'";
	PrintList.load(null,true);
}