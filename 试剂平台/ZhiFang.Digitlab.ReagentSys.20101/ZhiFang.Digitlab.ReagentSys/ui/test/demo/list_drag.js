Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	Ext.Loader.setPath('Ext.app',getRootPath()+'/ui/test/demo/portal/classes');
	
	Ext.require([
        //'Ext.diag.layout.ContextItem.*',
        //'Ext.diag.layout.Context.*',

        'Ext.layout.container.*',
        'Ext.resizer.Splitter',
        'Ext.fx.target.Element',
        'Ext.fx.target.Component',
        'Ext.window.Window',
        'Ext.app.Portlet',
        'Ext.app.PortalColumn',
        'Ext.app.PortalPanel',
        'Ext.app.Portlet',
        'Ext.app.PortalDropZone',
        'Ext.app.GridPortlet',
        'Ext.app.ChartPortlet'
    ]);
	
	//获取随机字符串
	var getRandomStr = function(len){
        var x = "123456789poiuytrewqasdfghjklmnbvcxzQWERTYUIPLKJHGFDSAZXCVBNM";
        var str = "";
        for (var i = 0; i < len; i++) {
            str += x.charAt(Math.ceil(Math.random() * 100000000) % x.length);
        }
        return str;
    };
    //说去随机数字
    var getRandom = function(min, max){
        return parseInt(Math.random() * (max - min) + min);
    };
	//获取随机的列表数据
	var getListData = function(num){
		var data = [];
		var count = num || 100;//列表数据量
		for(var i=0;i<count;i++){
			data.push({
				name:getRandomStr(8),
				code:getRandomStr(12),
				type:getRandom(1,10)
			});
		}
		return data;
	};
	
	var store = Ext.create('Ext.data.Store',{
		fields:['name','code','type'],
		data:getListData(10)
	});
	var getGridPanel = function(num){
		var grid = {
			xtype:'grid',title:'项目列表' +num,id:'portlet-' + num,
			collapsible:true,split:true,
			columns:[
				{text:'项目名称',dataIndex:'name'},
				{text:'项目编码',dataIndex:'code'},
				{text:'项目类型',dataIndex:'type'}
			],
			store:store,tools:this.getTools(),
            listeners:{'close':Ext.bind(this.onPortletClose,this)}
		};
		return grid;
	};
	
	
	
	
	var panel = Ext.create('Ext.panel.Panel',{
		xtype:'panel',
		header:false,
		//layout:'border',
		items:[{
			id:'col-1',
            items:[{
                id:'portlet-1',
                title:'Grid Portlet',
                tools:this.getTools(),
                items:Ext.create('Ext.app.GridPortlet'),
                listeners:{
                    'close': Ext.bind(this.onPortletClose, this)
                }
            },{
                id: 'portlet-2',
                title: 'Portlet 2',
                tools: this.getTools(),
                html: content,
                listeners: {
                    'close': Ext.bind(this.onPortletClose, this)
                }
            }]
		}]
	});
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});