/**
 * 服务器授权
 * @author longfc	
 * @version 2016-12-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.show.Panel', {
	extend: 'Shell.ux.panel.AppPanel',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchAHServerLicenceAndAndDetailsById',
	title: '服务器授权查看',
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: false,

	/**上传的授权申请文件的主要信息*/
	AHServerLicence: null,
	LicenceProgramTypeList: null,
	AHServerEquipLicenceList: null,
	/**是否包含是否特批复选框(只有审核时才显示)*/
	hasIsSpecially: false,
	/**是否特批复选框选择值*/
	IsSpeciallyValue: false,
	/**是否联动页签*/
	IsLinkpageTabPage: true,
	/**授权ID*/
	PK: null,
	PClientID: null,
	ProgramGrid: 'Shell.class.wfm.authorization.ahserver.show.ProgramLicenceGrid',
	EquipGrid: 'Shell.class.wfm.authorization.ahserver.show.EquipLicenceGrid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
		//me.initDate();
		me.loadData();
	},
	initListeners: function() {
		var me = this;
		me.bodyPadding = 1;
		me.SimpleGrid.on({
			itemclick: function(v, record, item, index, e, eOpts) {
				me.DetailsShow(record);
			},
			select: function(rowModel, record, index, eOpts) {
				me.DetailsShow(record);
			},
			nodata: function(p) {
				//me.Grid.clearData();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.SimpleGrid = Ext.create('Shell.class.wfm.authorization.ahserver.basic.SimpleGrid', {
			region: 'west',
			width: 220,
			header: true,
			split: true,
			collapsible: false,
			itemId: 'SimpleGrid',
			PK: me.PK
		});
		me.ShowTabPanel = Ext.create('Shell.class.wfm.authorization.ahserver.show.ShowTabPanel', {
			region: 'center',
			header: false,
			ProgramGrid: me.ProgramGrid,
			EquipGrid: me.EquipGrid,
			itemId: 'ShowTabPanel',
			AHServerLicence: me.AHServerLicence,
			LicenceProgramTypeList: me.LicenceProgramTypeList,
			AHServerEquipLicenceList: me.AHServerEquipLicenceList,
			defaultLoad: false,
			border:false,
			PK: me.PK,
			PClientID: me.PClientID
		});

		return [me.SimpleGrid, me.ShowTabPanel];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			items = me.createButtontoolbar();
		}
		return Ext.create('Ext.toolbar.Toolbar', {
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		items.push('-', '->');

		return items;
	},
	/**@public加载数据*/
	loadData: function() {
		var me = this;
		//me.showMask("数据加载中...");
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectUrl + "?id=" + me.PK;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				//me.hideMask(); //隐藏遮罩层
				if(data.value != null) {
					me.SimpleGrid.LicenceProgramTypeList = data.value.LicenceProgramTypeList;
					if(me.SimpleGrid.LicenceProgramTypeList != null) {
						me.SimpleGrid.store.loadData(me.SimpleGrid.LicenceProgramTypeList);
					} else {
						me.SimpleGrid.clearData();
					}

					//程序明细数据加载
					me.ShowTabPanel.ProgramLicenceGrid.ApplyProgramInfoList = data.value.ApplyProgramInfoList;
					if(me.ShowTabPanel.ProgramLicenceGrid.ApplyProgramInfoList != null) {
						me.ShowTabPanel.ProgramLicenceGrid.store.loadData(me.ShowTabPanel.ProgramLicenceGrid.ApplyProgramInfoList);
					} else {
						me.ShowTabPanel.ProgramLicenceGrid.clearData();
					}

					//仪器明细数据加载
					me.ShowTabPanel.EquipLicenceGrid.AHServerEquipLicenceList = data.value.AHServerEquipLicenceList;
					if(me.ShowTabPanel.EquipLicenceGrid.AHServerEquipLicenceList != null) {
						me.ShowTabPanel.EquipLicenceGrid.store.loadData(me.ShowTabPanel.EquipLicenceGrid.AHServerEquipLicenceList);
					} else {
						me.ShowTabPanel.EquipLicenceGrid.clearData();
					}
					me.AHServerLicence = data.value.AHServerLicence;
				}
			} else {
				//me.hideMask();
				JShell.Msg.error("获取服务器授权信息失败!<br />" + data.msg);
			}
		});
	},
	/*左列表的事件监听联动右区域**/
	DetailsShow: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get('Id');
			var code = record.get('Code');
			var index = 0;
			switch(code) {
				case "BEquip":
					//me.ShowTabPanel.setActiveTab(1);
					//me.ShowTabPanel.hideTab(0);
					//me.ShowTabPanel.showTab(1);
					index = 1;
					me.ShowTabPanel.setActiveTab(me.ShowTabPanel.EquipLicenceGrid);
					break;
				default:
					//me.ShowTabPanel.showTab(0);
					//me.ShowTabPanel.hideTab(1);
					index = 0;
					me.ShowTabPanel.setActiveTab(me.ShowTabPanel.ProgramLicenceGrid);
					break;
			}

		}, null, 500);
	},
	/**获取状授权类型信息*/
	getLicenceTypeData: function(LicenceTypeList) {
		var me = this,
			data = [];
		for(var i in LicenceTypeList) {
			var obj = LicenceTypeList[i];
			var style = ['font-weight:bold;']; //text-align:center
			if(obj.BGColor) {
				style.push('color:' + obj.BGColor);
			}
			data.push([obj.Id, obj.Name, style.join(';')]);
		}
		return data;
	}
});