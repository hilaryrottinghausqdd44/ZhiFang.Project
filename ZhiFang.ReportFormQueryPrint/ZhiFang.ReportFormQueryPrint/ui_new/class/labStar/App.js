/**
 * 技师站调用
 * @author guohx	
 * @version 2020-04-20
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

    //根据参数决定是否显示log信息
    var params = Shell.util.Path.getRequestParams(true);
    for (var i in params) {
        if (i.toLowerCase() === "SHOWLOG" && params[i] === "true") {
            Shell.util.Config.showLog = true;
        } else if (i.toLowerCase() === "SHOWLOGWIN" && params[i] === "true") {
            Shell.util.Config.showLogWin = true;
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


    panel = Ext.create('Shell.class.labStar.basic.App', Ext.apply(config, {
        appType: appType,
        title:'医生站',
        header: false
        
    }));

    //总体布局
    var viewport = Ext.create('Ext.container.Viewport', {
        layout: 'fit',
        padding: 1,
        //header: false,
        items: [panel]
    });
});