/**
 * 实验室(订货方)供货管理
 * @author longfc
 * @version 2018-05-08
 */
Ext.define('Shell.class.rea.client.reasale.lab.DocGrid', {
	extend: 'Shell.class.rea.client.reasale.basic.add.DocGrid',

	title: '供货信息',
	/**是否为实验室应用*/
	isLab: true,
	/**用户UI配置Key*/
	userUIKey: 'reasale.lab.DocGrid',
	/**用户UI配置Name*/
	userUIName: "供货列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(0);
		//系统运行参数"数据库是否独立部署"
		JcallShell.REA.RunParams.getRunParamsValue("ReaDataBaseIsDeploy", false,null);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			emptyText: '供货单号/发票号',
			itemId: 'Search',
			//flex: 1,
			width: "72%",
			isLike: true,
			fields: ['reabmscensaledoc.SaleDocNo', 'reabmscensaledoc.InvoiceNo']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		//订货方需要录入供货单
		//		items.push({
		//			xtype: 'button',
		//			iconCls: 'button-add',
		//			itemId: "btnAdd",
		//			text: '新增',
		//			tooltip: '新增供货单',
		//			handler: function() {
		//				me.onAddClick();
		//			}
		//		});
		//		items.push('-', {
		//			xtype: 'button',
		//			iconCls: 'button-check',
		//			itemId: "btnConfirm",
		//			text: '确认提交',
		//			tooltip: '确认提交',
		//			handler: function() {
		//				me.onConfirmClick();
		//			}
		//		});
		//		items.push({
		//			xtype: 'button',
		//			iconCls: 'button-check',
		//			itemId: "btnUnConfirm",
		//			text: '取消提交',
		//			tooltip: '取消提交',
		//			handler: function() {
		//				me.onUnConfirmClick();
		//			}
		//		});
		items.push('->', {
			iconCls: 'button-right',
			tooltip: '<b>收缩面板</b>',
			handler: function() {
				me.collapse();
			}
		});
		return items;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		//需要新增录入时使用
		//me.callParent(arguments);
		items.unshift({
			xtype: 'button',
			iconCls: 'button-import',
			itemId: "btnImport",
			text: '提取导入',
			tooltip: '从平台导入供应商供货单',
			handler: function() {
				me.onImportClick();
			}
		});
		return items;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();
		if(!JcallShell.REA.System.CENORG_CODE) {
			var error = me.errorFormat.replace(/{msg}/, "获取机构平台编码信息为空!");
			me.getView().update(error);
			return false;
		};

		var labcCode = JcallShell.REA.System.CENORG_CODE;
		//实验室自己录入的供货单(数据来源为实验室:2) 或者从供应商提取过来的供货单(供货单数据标志不等于未提取:0)
		me.defaultWhere = "((reabmscensaledoc.Source=2 and reabmscensaledoc.ReaServerLabcCode='" + labcCode + "') or (reabmscensaledoc.Source=1 and reabmscensaledoc.IOFlag!=0))";
		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**@description 通过选择供货单号提取供货单(客户端与平台为同一数据库)*/
	onImportClick: function() {
		var me = this;
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			title: '订货方为【' + JcallShell.REA.System.CENORG_NAME + "】的供货单提取",
			resizable: true,
			SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			ReaServerLabcCode: JcallShell.REA.System.CENORG_CODE,
			ReaCompCName: JcallShell.REA.System.CENORG_NAME,
			listeners: {
				accept: function(p, record) {
					if(record) {
						//数据库是否独立部署: 1: 是;2: 否;
						var isDeploy = "" + JcallShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
						if(isDeploy == "1") {

						} else if(isDeploy == "2") {
							me.onExtractSaleDocBySaleDocId(record, p);
						}
					} else {
						p.close();
					}
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.reasale.lab.import.SaleDocGrid', config);
		win.show();
	},
	/**@description 通过选择供货单号提取供货单(客户端与平台为同一数据库)*/
	onExtractSaleDocBySaleDocId: function(rec, p) {
		var me = this;

		var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenSaleDocOfExtract";
		var id = rec.get("ReaBmsCenSaleDoc_Id");
		var reaServerCompCode = rec.get("ReaBmsCenSaleDoc_ReaServerCompCode");
		var saleDocNo = rec.get("ReaBmsCenSaleDoc_SaleDocNo");
		var reaServerLabcCode = JcallShell.REA.System.CENORG_CODE;
		if(!reaServerLabcCode) reaServerLabcCode = "";
		var params = {
			"saleDocId": id,
			"reaServerCompCode": reaServerCompCode,
			"saleDocNo": saleDocNo,
			"reaServerLabcCode": reaServerLabcCode
		};
		JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
			if(data.success) {
				p.close();
				me.onSearch();
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});