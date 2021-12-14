/**
 * 打印测试
 * @author Jcall
 * @version 2020-11-23
 */
Ext.define('Shell.test.Print',{
	extend:'Ext.panel.Panel',
	title:'打印测试',
	bodyPadding:1,
	
	//模板内容
	modelContent:[
		'LODOP.PRINT_INITA(0,0,"65mm","40mm","试剂系统_{TITLE}_打印条码");',
		'LODOP.SET_PRINT_PAGESIZE(1,"65mm","40mm","");',
		
		'LODOP.ADD_PRINT_TEXT("5mm","5mm","34mm","10mm","{GoodsName}");',
		'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");',
		'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");',
		'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	],
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		
		Ext.create('Shell.ux.print.Print').init(function(print){
			me.Print = print;
		});
		Ext.create('Shell.ux.print.Model').init(function(model){
			me.PrintModel = model;
		});
		
		me.items = [{
			xtype:'button',text:'PDF直接打印',handler:function(){me.onPdfPrint();}
		},{
			xtype:'button',text:'PDF浏览打印',handler:function(){me.onPdfPreview();}
		},{
			xtype:'button',text:'模板直接打印',handler:function(){me.onModelPrint();}
		},{
			xtype:'button',text:'模板浏览打印',handler:function(){me.onModelPreview();}
		},{
			xtype:'button',text:'模板文件浏览打印',handler:function(){me.onModelFilePreview();}
		}];
		
		me.callParent(arguments);
	},
	//PDF直接打印
	onPdfPrint:function(){
		var me = this;
		var url = JShell.System.Path.UI + '/test/test.pdf';
		me.Print.pdf.print(url);
		me.Print.pdf.print(url);
	},
	//PDF浏览打印
	onPdfPreview:function(){
		var me = this;
		var url = JShell.System.Path.UI + '/test/test.pdf';
		me.Print.pdf.preview([url,url]);
	},
	//模板直接打印
	onModelPrint:function(){
		var me = this;
		
		for(var i=0;i<3;i++){
			var content = me.PrintModel.getLodopContentByModel(me.modelContent.join(''),{
				TITLE:'测试标题',
				GoodsName:'测试试剂'
			});
			me.Print.model.print(content);
		}
	},
	//模板浏览打印
	onModelPreview:function(){
		var me = this;
		var contentList = [];
		for(var i=0;i<3;i++){
			var content = me.PrintModel.getLodopContentByModel(me.modelContent.join(''),{
				TITLE:'测试标题',
				GoodsName:'测试试剂'
			});
			contentList.push(content);
		}
		
		me.Print.model.preview(contentList);
	},
	//模板文件浏览打印
	onModelFilePreview:function(){
		var me = this;
		var FileUrl = JShell.System.Path.UI + '/test/model1.txt';
		me.PrintModel.getLodopContentByModelFile(FileUrl,{
			TITLE:'测试标题',
			GoodsName:'测试试剂'
		},function(content){
			me.Print.model.preview(content);
		},function(err){
			if(err.response.status == 404){
				JShell.Msg.alert("模板文件不存在！");
			}
		});
	}
});