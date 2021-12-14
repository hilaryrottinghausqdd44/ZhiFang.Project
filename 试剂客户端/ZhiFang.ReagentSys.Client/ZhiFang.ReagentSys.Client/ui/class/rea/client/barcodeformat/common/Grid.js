/**
 * 按供应商条码规则维护
 * @author longfc
 * @version 2018-01-10
 */
Ext.define('Shell.class.rea.client.barcodeformat.common.Grid', {
	extend: 'Shell.class.rea.client.barcodeformat.basic.Grid',

	title: '条码规则信息',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: true,
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
		var hql = "reacenbarcodeformat.Type=1 and reacenbarcodeformat.PlatformOrgNo is null";
		if(me.defaultWhere) {
			me.defaultWhere = me.defaultWhere + " and " + hql;
		} else {
			me.defaultWhere = hql;
		}
		me.callParent(arguments);
	},
	initbuttonToolbarItems: function() {
		var me = this;
		me.buttonToolbarItems = [];
		me.buttonToolbarItems.push({
			xtype: 'button',
			iconCls: 'button-exp',
			text: '导出',
			hidden: me.APPTYPE == "1" ? false : true,
			tooltip: '导出选择的供应商所属条码规则及公共的条码规则给离线客户端',
			handler: function() {
				me.onExpBarCodeFormat();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-import',
			hidden: me.APPTYPE == "1" ? true : false,
			text: '导入',
			tooltip: '客户端导入条码规则(覆盖式导入,先删除原有的条码)',
			handler: function() {
				me.onImportBarCodeFormat();
			}
		}, '-');
	},

	onAddClick: function() {
		var me = this;
		me.fireEvent('addclick', me);
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.fireEvent('editclick', me, records[0]);
	},
	/**导出选择的供应商所属条码规则及公共的条码规则给离线客户端*/
	onExpBarCodeFormat: function() {
		var me = this;

		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight * 0.68;

		var config = {
			resizable: true,
			SUB_WIN_NO: '1',
			width: 520,
			height: height,
			selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchCenOrgByHQL?isPlanish=true',
			listeners: {
				accept: function(p, record) {
					me.onCenOrgAccept(record);
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.cenorg.CheckGrid', config);
		win.show();
	},
	/**机构选后择*/
	onCenOrgAccept: function(record) {
		var me = this;
		var orgNo = record ? record.get('CenOrg_OrgNo') : '';
		if(!orgNo) {
			JShell.Msg.error("请选择机构后再导出条码规则信息!");
			return;
		}
		var url = JShell.System.Path.ROOT + '/ReaSysManageService.svc/ST_UDTO_DownLoadReaCenBarCodeFormatOfPlatformOrgNo?operateType=0&platformOrgNo=' + orgNo;
		window.open(url);
	},
	/**客户端导入条码规则(覆盖式导入,先删除原有的条码)*/
	onImportBarCodeFormat: function() {
		var me = this;
		var config = {
			resizable: true,
			SUB_WIN_NO: '2',
			width: 360,
			height: 220,
			listeners: {
				save: function(p, record) {
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.barcodeformat.import.Form', config);
		win.show();
	}
});