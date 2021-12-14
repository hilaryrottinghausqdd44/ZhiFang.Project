/**
 * 报告查询
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.report.search.App',{
    extend:'Shell.ux.panel.AppPanel',
    
    Shell_class_report_search_App:{
    	title:{
			TEXT:'报告查询'
		}
    },
    
    layout:{type:'border',regionWeights:{north:3,west:2,south:1}},
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var Grid = me.getComponent('Grid');
		Grid.on({
			itemclick:function(v, record) {
				me.changeContent(record);
			},
			select:function(RowModel, record){
				me.changeContent(record);
			},
			nodata:function(p){
				me.Content.clearData();
				me.ChartPanel.clearData();
			}
		});
		Grid.onSearch();
	},
    
	initComponent:function(){
		var me = this;
		
		//替换语言包
		me.changeLangage('Shell.class.report.search.App');
		//标题
		me.title = me.Shell_class_report_search_App.title.TEXT;
		
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		me.Grid = Ext.create('Shell.class.report.search.Grid', {
			region: 'west',
			header: false,
			itemId: 'Grid',
			split: true,
			collapsible: true
		});
		me.Content = Ext.create('Shell.class.report.search.Content', {
			region: 'center',
			header: false,
			itemId: 'Content',
			pageType: 2//返回数据类型,1:报告;2:结果
		});
		me.ChartPanel = Ext.create('Shell.class.report.search.ChartPanel', {
			region: 'south',
			header: false,
			itemId: 'ChartPanel',
			split: true,
			collapsible: true,
			collapsed:true,
			height:250
		});
		
		return [me.Grid,me.Content,me.ChartPanel];
	},
	/**修改报告内容*/
	changeContent:function(record){
		var me = this;
		JShell.Action.delay(function(){
			me.showContent(record);
		},null,500);
	},
	/**显示报告内容*/
	showContent:function(record){
		var me = this;
        
		me.Content.changeContent({
			ReportFormID:record.get("ReportFormID"),
			SectionNo:record.get("SECTIONNO"),
			SectionType: record.get("SectionType"),
			RECEIVEDATE: record.get("RECEIVEDATE")
		});
	}
});