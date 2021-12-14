Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});
var panel = null;
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.useShims=true;//防止PDF挡住Exj原始组件内容
	//Shell.util.Win.begin();//屏蔽快捷键
	var appType = 'selfhelp';

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
	                config[item.ParaNo] = item.ParaValue;
	            }
	        }
	    }
	});

	panel = Ext.create('Shell.class.selfhelp.app.App', Ext.apply(config, {
	    appType: appType,
	    // printPageType: 'A4', //打印类型  A4 , A5 , 双A5
         // selectColumn: 'SerialNo', // 设置查询字段
         //lastDay: 90, //查询多少天之前的记录
         // printtimes:1, //限制打印次数
         ////倒计时关闭
	    // tackTime:5,
	    header: false

	}));
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		padding:1,
		items:[panel]
	});
});