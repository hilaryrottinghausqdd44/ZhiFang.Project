Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});
var panel = null;
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	
	Shell.util.Win.begin();//屏蔽快捷键
	
	panel = Ext.create('Shell.ReportSearch.class.PrintApp', {
	    //header: false,
        //帮助文档
	    help: true,
	    //定义的接收参数,病历号、核收日期
	    //SERIALNO(申请单号)，patno(病历号)，zdy4（病人就诊号），zdy5（病人编号）
	    requestParamsArr: ['SERIALNO', 'PATNO', 'zdy4', 'zdy5', 'RECEIVEDATE'],
	    //运算符,and/or
	    operator: 'and',
	    //默认每页数量[10,20,50,100,200,300,400,500]
	    defaultPageSize: 500,
	    //是否开启历史对比图
	    showChart: true,
        /**是否具有打印功能*/
        hasPrint:true,
        //参数转化
	    changeParams: function (params) {
	        if (!params) return params;

	        //SEARCH_TYPE = 1 & SEARCH_NO - serialno
	        //SEARCH_TYPE = 2 & SEARCH_NO - zdy4
	        //SEARCH_TYPE = 4 & SEARCH_NO - patno

	        var SEARCH_TYPE = params.SEARCH_TYPE,
                SEARCH_NO = params.SEARCH_NO;

	        if (SEARCH_TYPE && SEARCH_NO) {
	            switch(SEARCH_TYPE){
	                case "1": params.SERIALNO = SEARCH_NO; break;
	                case "2": params.zdy4 = SEARCH_NO; break;
	                case "4": params.PATNO = SEARCH_NO; break;
	            }
	        }

	        return params;
	    }
	});
	
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