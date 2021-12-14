Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	Ext.Loader.setPath('Ext.iqc',getRootPath()+'/ui/iqc/class');
	
	var chart1 = Ext.create('Ext.iqc.chart.NormalDistributionChartPanel');
	var panel1 = {
		xtype:'panel',
		title:'正太分布图',
		layout:'fit',
		tbar:[{
			xtype:'button',text:'加载数据',handler:function(but){
				chart1.load({ids:'1042',start:'2014-02-28',end:'2014-03-02'});
			}
		}],
		items:[chart1]
	};
	
	var chart2 = Ext.create('Ext.iqc.chart.YoudenChartPanel');
	var panel2 = {
		xtype:'panel',
		title:'U顿图',
		layout:'fit',
		tbar:[{
			xtype:'button',text:'加载数据(3月)',handler:function(but){
				chart2.load({ids:'1042,1074',start:'2014-03-01',end:'2014-03-31'});
			}
		},{
			xtype:'button',text:'加载数据(2月)',handler:function(but){
				chart2.load({ids:'1042,1074',start:'2014-02-1',end:'2014-02-28'});
			}
		},{
			xtype:'button',text:'加载数据(2014-06-01)',handler:function(but){
				chart2.load({ids:'1042,1074',start:'2014-06-01',end:'2014-06-02'});
			}
		},{
			xtype:'button',text:'加载数据(2014-06-02)',handler:function(but){
				chart2.load({ids:'1042,1074',start:'2014-06-02',end:'2014-06-03'});
			}
		},{
			xtype:'button',text:'加载数据(2014-06-03~2014-06-05)',handler:function(but){
				chart2.load({ids:'1042,1074',start:'2014-06-03',end:'2014-06-06'});
			}
		},{
			xtype:'button',text:'清空数据',handler:function(but){
				chart2.getChart().store.loadData([]);
			}
		}],
		items:[chart2]
	};
	
	
	
	var tabpanel = {
		xtype:'tabpanel',
		items:[panel1,panel2]
	};
	
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[tabpanel]
	});
});