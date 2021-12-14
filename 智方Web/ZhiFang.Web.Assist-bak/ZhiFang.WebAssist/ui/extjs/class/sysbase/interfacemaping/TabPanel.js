/**
 * 字典对照维护
 * @author longfc	
 * @version 2020-07-31
 */
Ext.define('Shell.class.sysbase.interfacemaping.TabPanel', {
	extend: 'Ext.tab.Panel',

	title: '字典对照维护',
	header: false,
	border: false,
	bodyPadding: 1,
	/**当前选择的对照字典Id*/
	PK: null,
	/**当前选择行*/
	checkRecord: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				me.onTabChange(newCard.itemId);
			}
		});
		me.activeTab = 0;

		me.Department.on({
			save: function(p, id) {

			}
		});
		me.PUser.on({
			save: function(p, id) {

			}
		});
		me.BloodChargeItem.on({
			save: function(p, id) {

			}
		});
		me.BDict.on({
			save: function(p, id) {

			}
		});
	},
	initComponent: function() {
		var me = this;
		//me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		//公共字典的数据对照
		me.BDict = Ext.create('Shell.class.sysbase.interfacemaping.BDictGrid', {
			title: '字典对照',
			header: true,
			hidden: true,
			itemId: 'BDict'
		});
		//科室对照
		me.Department = Ext.create('Shell.class.sysbase.interfacemaping.DepartmentGrid', {
			title: '科室对照',
			header: true,
			hidden: true,
			itemId: 'Department'
		});
		//人员对照
		me.PUser = Ext.create('Shell.class.sysbase.interfacemaping.PUserGrid', {
			title: '人员对照',
			header: true,
			hidden: true,
			itemId: 'PUser'
		});
		var appInfos = [me.BDict, me.Department, me.PUser];
		return appInfos;
	},
	loadData: function(record) {
		var me = this;
		me.checkRecord = record;
		me.PK = record.get("BDict_Id");
		var deveCode = "" + record.get("BDict_DeveCode");
		//如果deveCode等于BDict,useCode为字典类型编码(作为过滤字典数据使用)
		var useCode = "" + record.get("BDict_UseCode");
		
		switch (deveCode) {
			case "BloodChargeItem":
				
				break;
			case "BDict":
		
				break;
			case "Department":
			
				break;
			case "PUser":
			
				break;
			default:
				break;
		}
		me.setTabHide(deveCode);
		var curGrid=me.getComponent(deveCode);
		curGrid.deveCode=deveCode;
		curGrid.useCode=useCode;
		curGrid.objectTypeId=me.PK;
		curGrid.onSearch();
		
		me.setActiveTab(me.child('#' + deveCode));
		me.child('#' + deveCode).tab.show();
		
	},
	/**@description 控制页签显示*/
	setTabHide: function(deveCode) {
		var me = this;
		Ext.Array.each(me.items.items,function(item){
			if(item.itemId!=deveCode){
				me.child('#'+item.itemId).tab.hide();
			}
		});		
	},
	clearData: function() {
		var me = this;
	},
	nodata: function() {
		var me = this;
		me.PK = null;
		me.checkRecord = null;
		me.Department.clearData();
		me.BDict.clearData();
		me.clearData();
	},
	onTabChange:function(deveCode){
		var me=this;
		switch (deveCode) {

			case "BDict":
			
				break;
			
			case "Department":
			
				break;
			case "PUser":
			
				break;
			default:
				break;
		}
	}
});
