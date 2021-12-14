//alert("app.js");
Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	Ext.application({
		name:'AM',//应用名字
		appFolder:'app',//应用目录
		launch:function(){//当前页面加载完成后执行的函数
			Ext.create("Ext.container.Viewport",{
				layout:  'border',//Border布局
		        items: [{
		            region: 'north',height:40,
		            html: "<html><body><iframe src='Page/Top.html' style='height:100%;width:100%' frameborder='no'></iframe></body></html>"
		        },{
		            region: 'west',collapsible: true,split: true,
		            xtype:'menuTreeView',
		            title: '功能菜单',width:180 
		        },{
		        	region: 'center',header: false,
		        	xtype:'tabpanel'
		        }]
			});
		},
		controllers: ['MainController']//加载控制
	})
})