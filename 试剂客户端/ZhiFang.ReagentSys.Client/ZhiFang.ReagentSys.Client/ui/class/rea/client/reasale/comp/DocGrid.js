/**
 * 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.comp.DocGrid', {
	extend: 'Shell.class.rea.client.reasale.basic.add.DocGrid',

	title: '供货信息',
	/**用户UI配置Key*/
	userUIKey: 'reasale.comp.DocGrid',
	/**用户UI配置Name*/
	userUIName: "供货列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(0);
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
		var items = items = me.callParent(arguments);

		items.unshift({
			xtype: 'button',
			iconCls: 'button-import',
			itemId: "btnImport",
			text: '订单转供单',
			tooltip: '从平台订货方的订单转换为供货单',
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
		//默认条件为:供货商自己录入的供货单及实验室上传的供货单 reabmscensaledoc.Source=1 and 
		//(供货商所属机构平台编码=登录帐号所属的机构平台编码 并且 LabId=登录帐号所属的LabId) 或者 (供货商所属机构平台编码=登录帐号所属的机构平台编码 and 供货单状态=审核通过 and 供货单提取标志=已上传)
		me.defaultWhere = "reabmscensaledoc.ReaServerCompCode='" + labcCode + "'";

		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**订单转供单提取*/
	onImportClick: function() {
		var me = this;
		//系统运行参数"数据库是否独立部署":1:是;2:否;
		JcallShell.REA.RunParams.getRunParamsValue("ReaDataBaseIsDeploy", false, function(data) {
			if(data.success) {
				var isDeploy = "" + JcallShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
				if(isDeploy == "2") {
					me.onShowOrderPanel();
				} else if(isDeploy == "1") {
					//访问BS平台的URL
					JcallShell.REA.RunParams.getRunParamsValue("BSPlatformURL", false, function(data) {
						if(data.success) {
							me.onShowOrderPanelOfDeploy();
						} else {
							JShell.Msg.error('获取系统运行参数"访问BS平台的URL"配置项失败！' + data.msg);
						}
					});
				}
			} else {
				JShell.Msg.error('获取系统运行参数"数据库是否独立部署"配置项失败！' + data.msg);
			}
		});
	},
	/**提取订单转供货单(实验室与平台同一数据库)*/
	onShowOrderPanel: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			title: '本地订单转供单提取',
			resizable: true,
			SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			listeners: {
				accept: function(p, record) {
					if(record) {
						me.onOrderToSupply(record, p);
					} else {
						p.close();
					}
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.reasale.comp.import.OrderGrid', config);
		win.show();
	},
	/**提取订单转供货单(实验室与平台同一数据库)*/
	onOrderToSupply: function(rec, p) {
		var me = this;
		var id = rec.get("ReaBmsCenOrderDoc_Id");
		var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_UDTO_AddReaBmsCenSaleDocOfOrderToSupply";
		var params = {
			"orderId": id
		};
		JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
			if(data.success) {
				p.close();
				me.onSearch();
			} else {
				JShell.Msg.error('提取订单转供货单失败！' + data.msg);
			}
		});
	},
	/**提取订单转供货单(实验室与平台不在同一数据库)*/
	onShowOrderPanelOfDeploy: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			title: '平台订单转供单提取',
			resizable: true,
			SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			listeners: {
				accept: function(p, record) {
					if(record) {
						me.onOrderToSupplyOfDeploy(record, p);
					} else {
						p.close();
					}
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.reasale.comp.deploy.OrderGrid', config);
		win.show();
	},
	/**提取订单转供货单(实验室与平台不在同一数据库)*/
	onOrderToSupplyOfDeploy: function(rec, p) {
		var me = this;
	}
});