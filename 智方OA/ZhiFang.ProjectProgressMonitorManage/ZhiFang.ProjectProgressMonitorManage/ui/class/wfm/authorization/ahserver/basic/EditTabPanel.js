/**
 * 服务器授权
 * @author longfc
 * @version 2016-12-26
 */
Ext.define('Shell.class.wfm.authorization.ahserver.basic.EditTabPanel', {
	extend: 'Ext.tab.Panel',
	
	title: '服务器授权',
	width: 600,
	height: 400,

	/**授权ID*/
	PK: null,
	PClientID:null,
	AHServerLicence: null,
	LicenceProgramTypeList: null,
	AHServerEquipLicenceList: null,
	ProgramGrid: '',
	EquipGrid: '',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.ProgramLicenceGrid = Ext.create(me.ProgramGrid, {
			AHServerLicence: me.AHServerLicence,
			LicenceProgramTypeList: me.LicenceProgramTypeList,
			itemId: 'ProgramLicenceGrid',
			border:false,
			/**带功能按钮栏*/
	        hasButtontoolbar: false,
			PK: me.PK
		});
		me.EquipLicenceGrid = Ext.create(me.EquipGrid, {
			AHServerLicence: me.AHServerLicence,
			AHServerEquipLicenceList: me.AHServerEquipLicenceList,
			itemId: 'EquipLicenceGrid',
			border:false,
			PK: me.PK
		});
		me.MemoForm = Ext.create('Shell.class.wfm.authorization.ahserver.basic.MemoForm', {
			title: '服务器授权备注',
			PK: me.PK,
			header: false,
			border: false,
			formtype:'edit',
			hasLoadMask:false,
			height: me.height,
			itemId: 'MemoForm'
		});
		me.ContractPanel = Ext.create('Shell.class.wfm.authorization.contract.ShowPanel', {
			itemId: 'ContractPanel',
			hiddenPContractName:false,
			PClientID: me.PClientID
		});
		me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App', {
			title: '交流信息',
			FormPosition: 'e',
			PK: me.PK
		});

		me.Operate = Ext.create('Shell.class.wfm.authorization.basic.SCOperation', {
			title: '操作记录',
			classNameSpace: 'ZhiFang.Entity.ProjectProgressMonitorManage', //类域
			className: 'LicenceStatus', //类名
			formtype: 'show',
			itemId: 'Operate',
			hasLoadMask: false,
			PK: me.PK
		});
		return [me.ProgramLicenceGrid, me.EquipLicenceGrid,me.MemoForm,me.ContractPanel , me.Interaction, me.Operate];

	},
	/**
	 * 隐藏 tab
	 * @param tabPanel
	 * @param tab
	 * @returns {boolean}
	 */
	hideTab: function(index) {
		var me = this;
		var tab = me.items.getAt(index);
		tab.hide();
		tab.tab.hide();
	},
	/**
	 * 显示 tab
	 * @param tabPanel
	 * @param tab
	 */
	showTab: function(index) {
		var me = this;
		var tab = me.items.getAt(index);
		tab.show();
		tab.tab.show();
		me.setActiveTab(tab);
	}
});