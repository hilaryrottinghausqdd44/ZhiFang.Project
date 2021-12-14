/**
 * 输血过程记录:血袋不良反应入口
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.adversereaction.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '不良反应信息',
	//输血过程记录主单ID
	PK:null,
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents( 'nodata');
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		//不良反应症状tab
		me.TabPanel = Ext.create("Shell.class.blood.nursestation.transrecord.adversereaction.TabPanel", {
			region: 'center',
			header: false,
			//border:false,
			itemId: 'TabPanel'
		});
		//临床处理措施
		me.ClinicalMeasuresGrid = Ext.create('Shell.class.blood.nursestation.transrecord.clinicalmeasures.TransItemGrid', {
			region: 'south',
			header: false,
			height: 180,
			//border:false,
			itemId: 'ClinicalMeasuresGrid',
			split: false,
			collapsible: false
		});
		//临床处理结果
		me.ClinicalResultsForm = Ext.create('Shell.class.blood.nursestation.transrecord.clinicalresults.Form', {
			region: 'south',
			header: false,
			border:false,
			height: 40,
			itemId: 'ClinicalResultsForm',
			split: false,
			collapsible: false
		});
		//临床处理结果描述
		me.ClinicalResultsDescForm = Ext.create('Shell.class.blood.nursestation.transrecord.clinicalresultsdesc.Form', {
			region: 'south',
			header: false,
			border:false,
			height: 90,
			itemId: 'ClinicalResultsDescForm',
			split: false,
			collapsible: false
		});
		return [me.TabPanel, me.ClinicalMeasuresGrid, me.ClinicalResultsForm, me.ClinicalResultsDescForm];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
	
		if (me.hasButtontoolbar) {
			var buttontoolbar = me.createButtontoolbar();
			if (buttontoolbar) items.push(buttontoolbar);
		}
		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		items.push({
			xtype: 'button',
			iconCls: 'button-add',
			border:true,
			text: '不良反应',
			tooltip: '对当前不良反应页签选择',
			listeners: {
				click: function(but) {
	
				}
			}
		});
		items.push({
			xtype: 'button',
			border:true,
			iconCls: 'button-add',
			text: '临床处理措施',
			listeners: {
				click: function(but) {
	
				}
			}
		});
		if (items.length == 0) return null;
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: "top",
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**显示遮罩*/
	showMask: function (text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function () {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	/*程序列表的事件监听**/
	onListeners: function() {
		var me = this;

	},
	clearData: function() {
		var me = this;
		me.setPKVals(null);
		me.TabPanel.clearData();
		me.ClinicalMeasuresGrid.clearData();
		me.ClinicalResultsForm.getForm().reset();
		me.ClinicalResultsDescForm.getForm().reset();
		//me.fireEvent('nodata', me);
	},
	setPKVals:function(id){
		var me=this;
		me.PK=id;
		me.TabPanel.PK=id;
		var gridPanel = me.TabPanel.getActiveTab();
		gridPanel.PK=id;

		me.ClinicalMeasuresGrid.PK=id;
		me.ClinicalResultsForm.PK=id;
		me.ClinicalResultsDescForm.PK=id;
	},
	loadData: function() {
		var me = this;
		JShell.Action.delay(function() {
			me.isShow(me.PK);
		}, null, 300);
	},
	isShow:function(id){
		var me=this;
		me.setPKVals(id);
		//不良反应症状列表
		me.TabPanel.loadData(id);
		//临床处理措施
		me.ClinicalMeasuresGrid.loadData(id);
		//临床处理结果
		me.ClinicalResultsForm.isShow(id);
		//临床处理结果描述
		me.ClinicalResultsDescForm.isShow(id);
	}
});
