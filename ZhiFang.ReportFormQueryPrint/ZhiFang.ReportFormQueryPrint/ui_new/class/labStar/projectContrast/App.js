/**
 * 技师站调用历史对比
 * @author ghx
 * @version 2020-04-21
*/
Ext.Loader.setConfig({
    enabled: true,
    paths: { 'Shell': Shell.util.Path.uiPath }
});
var panel = null;
Ext.onReady(function () {
    Ext.QuickTips.init();//初始化后就会激活提示功能
    Ext.useShims = true;//防止PDF挡住Exj原始组件内容
    Shell.util.Win.begin();//屏蔽快捷键
    var appType = 'labstar'; //页面类型
    
    panel = Ext.create('Shell.class.labStar.projectContrast.contrast.App',{
        appType: appType,
        title:'',border: false,
			autoScroll: true, split: true,
			collapsible: false, animCollapse: false
        
    });

    //总体布局
    var viewport = Ext.create('Ext.container.Viewport', {
        layout: 'fit',
        padding: 1,
        //header: false,
        items: [panel]
    });
});
var  result_tr_backgroundcolorID = "";
/**用于行点击调用,变更历史对比信息*/
function printResult(ItemNo) {
	if(result_tr_backgroundcolorID != ""){
		$(result_tr_backgroundcolorID).css("background-color","#FFF");
	}
	$("#tbody .r"+ItemNo).css("background-color","lightskyblue");
	result_tr_backgroundcolorID = "#tbody .r"+ItemNo;
    var HistoryCompare = panel.getComponent('HistoryCompare');
	var obj2 = panel.obresponse;
	var obj = panel.parameter;
	var datearr = [];
	var where = decodeURI(obj.split("where=")[1]).split("and");
	for(var i=0;i<where.length;i++){
		if(where[i].indexOf("Date")>0){
			datearr.push(where[i]);
		}
	}
    if (!HistoryCompare) return;
	HistoryCompare.setPatientInfo({
		PatName:obj2[0][0].CName,
		PatNo:obj2[0][0].PatNo
	});
    HistoryCompare.load({
        PatNo: obj2[0][0].PatNo,
        ItemNo: ItemNo,
        Table: 'item'
    },datearr.join(" and "));
}