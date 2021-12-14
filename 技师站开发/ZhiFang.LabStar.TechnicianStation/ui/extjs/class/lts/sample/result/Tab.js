/**
 * 样本检验结果TAB
 * @author Jcall
 * @version 20200120
 */
Ext.define('Shell.class.lts.sample.result.Tab',{
	extend:'Ext.tab.Panel',
	title:'样本检验结果',
	activeTab:0,
	
	//小组ID
	sectionId:null,
	//检验单数据
	testFormRecord:null,
	//是否只读
	isReadOnly:false,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.on({
			tabchange:function(tabPanel,newCard,oldCard,eOpts){
				me.activedTab = newCard;
				//if()
				if(me.activedTab.onSearch){
					me.activedTab.onSearch(me.testFormRecord);
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.activedTab = me.Result = Ext.create('Shell.class.lts.sample.result.sample.App',{
			title:'检验结果',itemId:'Result',
			sectionId:me.sectionId,
			isReadOnly: me.isReadOnly,
			listeners: {
				save: function (p) {
					me.fireEvent("save", me);
				}
			}
		});
	    me.History = Ext.create('Shell.class.lts.sample.result.history.App', {
			title:'历史对比',
			itemId:'History',
			isReadOnly:me.isReadOnly
		});
//		me.SampleAll = Ext.create('Shell.class.lts.sample.result.all.App', {
//			title:'样本全部结果',
//			itemId:'SampleAll'
//		});
		me.DoctorReport = Ext.create('Shell.class.lts.sample.result.report.others.App', {
			title:'相关医嘱报告',
			itemId:'DoctorReport',
			isReadOnly:me.isReadOnly
		});
//		me.Report = Ext.create('Shell.class.lts.sample.result.report.App', {
//			title:'报告',
//			itemId:'Report'
//		});
		me.EquipResult = Ext.create('Shell.class.lts.sample.result.equip.App', {
			title:'仪器结果',
			itemId:'EquipResult',
			isReadOnly:me.isReadOnly
		});
		me.PickUpEquipResult = Ext.create('Shell.class.lts.sample.result.equip.extract.App', {
			title:'重新提取仪器结果',
			itemId: 'PickUpEquipResult',
			sectionId: me.sectionId,
			isReadOnly: me.isReadOnly,
			listeners: {
				updateTestFormRecord: function () {
					me.activedTab.onSearch(me.testFormRecord);
				}
			}
		});
		return [me.Result,me.History,me.DoctorReport,me.EquipResult,me.PickUpEquipResult];
	},
	//查询数据
	onSearch:function(testFormRecord){
		var me = this;
		me.testFormRecord = testFormRecord;
		if(me.activedTab){
			if(me.activedTab.onSearch){
				me.activedTab.onSearch(me.testFormRecord);
			}
		}
	},
	//清空数据
	clearData:function(){
		var me = this;
		me.testFormRecord = null;
		if(me.activedTab && Ext.typeOf(me.activedTab.clearData) == 'function'){
			me.activedTab.clearData();
		}
	},
	//新增检验单
	isAdd:function(){
		var me = this;
		if(me.activedTab && Ext.typeOf(me.activedTab.isAdd) == 'function'){
			me.activedTab.isAdd();
		}
	}
});