/**
 * 服务器授权
 * @author longfc
 * @version 2016-12-26
 */
Ext.define('Shell.class.wfm.authorization.ahserver.show.ShowTabPanel', {
	extend: 'Ext.tab.Panel',
	title: '查看服务器授权信息',

	width: 600,
	height: 500,
	/**授权ID*/
	PK: null,
	PClientID: null,
	AHServerLicence: null,
	/**默认加载*/
	defaultLoad: true,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchAHServerLicenceAndAndDetailsById',
	ProgramGrid: 'Shell.class.wfm.authorization.ahserver.show.ProgramLicenceGrid',
	EquipGrid: 'Shell.class.wfm.authorization.ahserver.show.EquipLicenceGrid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.defaultLoad == true)
			me.loadData();
	},
	initComponent: function() {
		var me = this;
		me.bodyPadding = 1;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.ContentPanel = Ext.create('Shell.class.wfm.authorization.ahserver.show.ContentPanel', {
			itemId: 'ContentPanel',
			IsShowAudit: true,
			border:false,
			title: '授权信息',
			PK: me.PK
		});
		me.ProgramLicenceGrid = Ext.create(me.ProgramGrid, {
			AHServerLicence: me.AHServerLicence,
			header: false,
			collapsible: false,
			itemId: 'ProgramLicenceGrid',
			/**带功能按钮栏*/
	        hasButtontoolbar: false,
			PK: me.PK
		});
		me.EquipLicenceGrid = Ext.create(me.EquipGrid, {
			AHServerLicence: me.AHServerLicence,
			itemId: 'EquipLicenceGrid',
			/**带功能按钮栏*/
	        hasButtontoolbar: false,
			PK: me.PK
		});
		me.ContractPanel = Ext.create('Shell.class.wfm.authorization.contract.ShowPanel', {
			itemId: 'ContractPanel',
			border:false,
			hiddenPContractName: false,
			PClientID: me.PClientID
		});
		me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App', {
			title: '交流信息',
			border:false,
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
			border:false,
			PK: me.PK
		});
		return [me.ContentPanel, me.ProgramLicenceGrid, me.EquipLicenceGrid, me.ContractPanel, me.Interaction, me.Operate];

	},
	/**@public加载数据*/
	loadData: function() {
		var me = this;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectUrl + "?id=" + me.PK;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				//me.hideMask(); //隐藏遮罩层
				if(data.value != null) {
					//程序明细数据加载
					me.ProgramLicenceGrid.ApplyProgramInfoList = data.value.ApplyProgramInfoList;
					if(me.ProgramLicenceGrid.ApplyProgramInfoList != null) {
						me.ProgramLicenceGrid.store.loadData(me.ProgramLicenceGrid.ApplyProgramInfoList);
					} else {
						me.ProgramLicenceGrid.clearData();
					}

					//仪器明细数据加载
					me.EquipLicenceGrid.AHServerEquipLicenceList = data.value.AHServerEquipLicenceList;
					if(me.EquipLicenceGrid.AHServerEquipLicenceList != null) {
						me.EquipLicenceGrid.store.loadData(me.EquipLicenceGrid.AHServerEquipLicenceList);
					} else {
						me.EquipLicenceGrid.clearData();
					}
					me.AHServerLicence = data.value.AHServerLicence;
				}
			} else {
				//me.hideMask();
				JShell.Msg.error("获取服务器授权信息失败!<br />" + data.msg);
			}
		});
	}
});