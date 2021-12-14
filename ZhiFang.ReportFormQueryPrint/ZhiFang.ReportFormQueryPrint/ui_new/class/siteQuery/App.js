/**
 * 代码新包迁移
 * @author Jing
 * @version 2018-09-20
*/
Ext.Loader.setConfig({
    enabled: true,
    paths: { 'Shell': Shell.util.Path.uiPath }
});
var panel = null;
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.useShims=true;//防止PDF挡住下拉框
	Shell.util.Win.begin();//屏蔽快捷键
	var appType = 'siteQuery';
	//根据参数决定是否显示log信息
	var params = Shell.util.Path.getRequestParams(true);
	for(var i in params){
		if(i.toLowerCase() === "SHOWLOG" && params[i] === "true"){
			Shell.util.Config.showLog = true;
		}else if(i.toLowerCase() === "SHOWLOGWIN" && params[i] === "true"){
			Shell.util.Config.showLogWin = true;
		}
	}
	//判断用户是否登录
	function getCookie(name) {
			var cookies = document.cookie;
			var list = cookies.split("; ");    // 解析出名/值对列表
			      
			for(var i = 0; i < list.length; i++) {
				var arr = list[i].split("=");  // 解析出名和值
				if(arr[0] == name)
					return decodeURIComponent(arr[1]);  // 对cookie值解码
			} 
			return "";
		}
	var UserNo = getCookie("UserNo");
	var UserCName = getCookie("UserCName");
	var ShortCode = getCookie("ShortCode");
  	var cookie = {UserNo:UserNo,CName:UserCName,ShortCode:ShortCode}
  	var dept = [];
	if (UserNo == null || UserNo == "") {
	    window.location.href = Shell.util.Path.uiPath + '/class/siteQuery/userSign/index.html';
	} else {
	  	dept = Ext.JSON.decode(getCookie("dept").replace(/\+/g," "));
	    if( dept==null || dept==[] || dept.length<=0){
	    	window.location.href = Shell.util.Path.uiPath + '/class/siteQuery/addEmpDept/index.html';
	    }
	}
	
	var isviewportHeader = true;	
    //获得页面配置信息
	var config = {};
	Ext.Ajax.defaultPostHeader = 'application/json';
	Ext.Ajax.request({
	    url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetAllPublicSetting?pageType=' + encodeURI(appType),
	    async: false,
	    method: 'get',
	    success: function (response, options) {
	        rs = Ext.JSON.decode(response.responseText);
	        if (rs.success) {
	            var items = Ext.JSON.decode(rs.ResultDataValue).list;
	            for (var i = 0; i < items.length; i++) {
	                var item = items[i];
	                if (item.ParaDesc == 'bool') {
	                    item.ParaValue = item.ParaValue === 'true' ? true : false;
	                }
	                if (item.ParaDesc == 'int') {
	                    item.ParaValue = parseInt(item.ParaValue);
	                }
	                if (item.ParaDesc == 'stringArry') {
	                    if (item.ParaValue == '') {
	                        item.ParaValue = [];
	                    } else {
	                        item.ParaValue = item.ParaValue.split(',');
	                    }
	                }
	                if (item.ParaNo == 'ForcedPagingField') {
	                    if (item.ParaValue == '') {
	                        item.ParaValue = '';
	                    } else {
	                        item.ParaValue = { dataIndex: item.ParaValue, text: '' };
	                    }
	                }
	                 if(item.ParaNo == 'isviewportHeader'){
                    	isviewportHeader = item.ParaValue;
                    }
	                config[item.ParaNo] = item.ParaValue;
	            }
	        }
	    }
	});


	panel = Ext.create('Shell.class.siteQuery.basic.App', Ext.apply(config, {
	    appType: appType,
	    region: "center",
	    title:'站点查询',
	    dept:dept,
	    header: isviewportHeader
	}));
	title = Ext.create("Shell.class.siteQuery.Title", {
        region: 'north',
        padding: 0,
        height: 30,
        frame: true,
        cookie: cookie
	});
   
    //总体布局
    var viewport = Ext.create('Ext.container.Viewport', {
        layout: 'border',
        padding: 1,
        items: [title,panel]
    });
});

/**用于结果页面的行点击调用,变更历史对比信息*/
function printResult(PatNo, ItemNo, Table, ReceiveDate) {
    var HistoryCompare = panel.getComponent('HistoryCompare');

    if (!HistoryCompare) return;

    HistoryCompare.load({
        PatNo: PatNo,
        ItemNo: ItemNo,
        Table: Table,
        ReceiveDate: ReceiveDate
    });
}