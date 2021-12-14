/**
 * 历史对比
 * @author Jcall
 * @version 2020-03-22
 */
Ext.define('Shell.class.lts.sample.result.history.App', {
	extend:'Shell.ux.panel.AppPanel',
	title:'历史对比',
	layout:{
		type:'border',
		regionWeights:{west:2,north:1}
	},
	//是否加载过数据
	hasLoaded:false,
	oldSectionId:'',
	afterRender:function(){
		var me = this;		
		me.callParent(arguments);
		
		me.SearchForm.on({
			searchHisCom:function(where){
				me.ReasultTab.onSearch({where:where});
			}
		});
	},
	initComponent:function(){
		var me = this;
		//创建内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	//创建内部组件
	createItems:function(){
		var me = this;
		
		me.SearchForm = Ext.create('Shell.class.lts.sample.result.history.SearchForm',{
			region:'west',itemId:'SearchForm',width:250,
			header:false,autoScroll:true,split:true,
			collapsible:true//,collapseMode:'mini'
		});
//		me.ChartPanel = Ext.create('Shell.class.lts.sample.result.history.ChartPanel',{
//			region:'center',itemId:'ChartPanel',
//			header:false,autoScroll:true,split:true,
//			collapsible:true//,collapseMode:'mini'
//		});
		me.ReasultTab = Ext.create('Shell.class.lts.sample.result.history.result.Tab',{
			region: 'center', itemId: 'ReasultTab', header: false,height:'100%'//region: 'north',
		});

		return [me.SearchForm, me.ReasultTab];
	},
	//查询数据
	onSearch:function(testFormRecord){
		var me = this;
		var sectionid = testFormRecord ? testFormRecord.get("LisTestForm_LBSection_Id") : '';
		me.getComponent("SearchForm").sectionId = sectionid;
		me.getComponent("SearchForm").testFormRecord = testFormRecord;
		var endDate = JcallShell.Date.toString(testFormRecord.get("LisTestForm_GTestDate"), true),
			startDate = JcallShell.Date.toString(JcallShell.Date.getNextDate(endDate, 1 - 60),true);
		me.getComponent("SearchForm").getComponent("historyComparisonScope").getComponent("LisTestForm_GTestDate_start").setValue(startDate);
		me.getComponent("SearchForm").getComponent("historyComparisonScope").getComponent("LisTestForm_GTestDate_end").setValue(endDate);
		if(sectionid != me.oldSectionId){
			me.getComponent("SearchForm").createSectionScopeItems();
			me.oldSectionId = sectionid;
		}	
		if(!me.hasLoaded){
			me.hasLoaded = true;
		}
	},
	//清空数据
	clearData:function(){
		var me = this;
		me.SearchForm.clearData();
		me.ReasultTab.clearData();
	}
});