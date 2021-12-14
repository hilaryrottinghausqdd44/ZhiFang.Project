/**
 * 供应商条码规则维护
 * @author longfc
 * @version 2018-01-10
 */
Ext.define('Shell.class.rea.client.barcodeformat.cenorg.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '供应商条码规则维护',
	header: false,
	border: false,
	//width:680,
	
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	/**应用类型:是否平台:是:1,否:0或null*/
	APPTYPE:"1",
	layout: {
		type: 'border'
	},
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.CenOrgTree.on({
			select: function(rowModel, record, index, eOpts) {
				JShell.Action.delay(function() {
					me.loadGridData(record);
				}, null, 500);
			},
			nodata: function(p) {
				me.Grid.defaultWhere = "";
				me.Grid.PlatformOrgNo = null;
				me.Grid.clearData();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.CenOrgTree = Ext.create('Shell.class.rea.client.barcodeformat.cenorg.CenOrgTree', {
			header: false,
			region: 'west',
			width: 360,
			split: true,
			rootVisible: false,
			collapsible: true,
			collapsed: false,
			/**机构类型*/
			OrgType: "0",
			itemId: 'CenOrgTree',
			APPTYPE: me.APPTYPE
		});
		me.Grid = Ext.create('Shell.class.rea.client.barcodeformat.cenorg.Grid', {
			header: false,
			itemId: 'Grid',
			region: 'center',
			collapsible: true,
			collapsed: false,
			APPTYPE: me.APPTYPE
		});
		var appInfos = [me.CenOrgTree, me.Grid];
		return appInfos;
	},
	loadGridData: function(record) {
		var me = this;
		var id = record.get("tid");
		var text = record.get("text");
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		var orgNo = "";
		if(record.data.value)
			orgNo = record.data.value.PlatformOrgNo;
		if(!orgNo) {
			me.Grid.defaultWhere = "";
			me.Grid.PlatformOrgNo = null;
			me.Grid.store.removeAll();
			me.Grid.disableControl();
		} else {
			me.Grid.PlatformOrgNo = orgNo;
			//reacenbarcodeformat.Type=2 and 
			me.Grid.defaultWhere = "reacenbarcodeformat.PlatformOrgNo=" + orgNo;
			me.Grid.onSearch();
		}
	}
});