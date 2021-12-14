/**
 * 供应商条码规则维护
 * @author longfc
 * @version 2018-01-10
 */
Ext.define('Shell.class.rea.client.barcodeformat.cenorg.Grid', {
	extend: 'Shell.class.rea.client.barcodeformat.basic.Grid',

	title: '条码规则信息',
	width: 800,
	height: 500,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**当前的供货方平台机构编码*/
	PlatformOrgNo: null,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaCenBarCodeFormat_DispOrder',
		direction: 'ASC'
	}],
	/**应用类型:是否平台:是:1,否:0或null*/
	APPTYPE: "1",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		if(me.APPTYPE == "1") me.hasEdit = true;
		me.callParent(arguments);
	},
	onAddClick: function() {
		var me = this;
		me.showFromPanel();
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.showFromPanel(records[0]);
	},
	/**@description 弹出验收录入信息*/
	showFromPanel: function(record) {
		var me = this;

		if(!me.PlatformOrgNo) {
			JcallShell.Msg.error("当前供货商的平台编码为空!");
			return;
		}
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var id = null;
		if(record) id = record.get(me.PKField);

		var config = {
			resizable: true,
			PK: null,
			PlatformOrgNo: me.PlatformOrgNo,
			DispOrder: me.store.getCount() + 1,
			SUB_WIN_NO: '1',
			width: 595,
			height: 415,
			/**条码规则分类为按公共部分:1,和按供应商:2*/
			Category: 2,
			listeners: {
				save: function(p, pk) {
					p.close();
					me.onSearch();
				}
			}
		};
		if(!id)
			config.formtype = 'add';
		else {
			config.formtype = 'edit';
			config.PK = id;
		}
		var win = JShell.Win.open('Shell.class.rea.client.barcodeformat.basic.Form', config);
		win.show();
	}
});