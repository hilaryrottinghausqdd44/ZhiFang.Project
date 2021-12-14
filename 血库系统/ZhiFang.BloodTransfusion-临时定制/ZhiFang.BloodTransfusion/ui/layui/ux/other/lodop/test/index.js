Ext.Loader.setConfig({
	enabled:true,
	paths:{
		'Shell':JShell.System.Path.UI,
		'Ext.ux':JShell.System.Path.UI + '/extjs/ux'
	}
});

Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	
	var panel = Ext.create('Ext.panel.Panel',{
		title:'Lodop测试',
		bodyPadding:10,
		items:[{
			xtype:'button',text:'二维码',handler:function(){
				onQRCodeClick();
			}
		}]
	});
	
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
	
	//二维码
	function onQRCodeClick(){
//		alert("二维码");
		var LODOP=getLodop(document.getElementById('LODOP_OB'),document.getElementById('LODOP_EM'));
		
		LODOP.PRINT_INITA(0,0,400,300,"Lodop功能_打印条码_测试");
		LODOP.ADD_PRINT_BARCODE(10,10,128,128,"QRCode","r.zhifang.com.cn/rea_new/webapp/index.html");
		//LODOP.SET_PRINT_STYLEA(0,"GroundColor","#C0C0C0");//背景色
		LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",7);
		LODOP.SET_LICENSES("北京智方科技开发有限公司", "653726269717472919278901905623", "", "");
		
		LODOP.PREVIEW();
	}
});