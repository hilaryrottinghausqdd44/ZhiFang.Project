/**
 * 平台未提取的供货单
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.choose.sale.PlatformSaleCheck', {
	extend: 'Shell.class.rea.client.confirm.choose.sale.SaleDocCheck',

	title: '平台未提取的供货单',
	/**获取数据服务路径*/
	selectUrl: '/ZFReaRestfulService.svc/RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsCenSaleDoc_CheckTime',
		direction: 'DESC'
	}],
	
	/**用户UI配置Key*/
	userUIKey: 'confirm.choose.sale.PlatformSaleCheck',
	/**用户UI配置Name*/
	userUIName: "平台未提取的供货单列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//系统运行参数"数据库是否独立部署"
		JcallShell.REA.RunParams.getRunParamsValue("ReaDataBaseIsDeploy", true, function(data) {

		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onExtractAfter');
		//查询框信息
		me.searchInfo = {
			width: 360,
			emptyText: '供货单号',
			itemId: 'search',
			isLike: true,
			fields: ['reabmscensaledoc.SaleDocNo']
		};

		//访问BS平台的URL
		//JShell.REA.RunParams.getRunParamsValue("BSPlatformURL", true, function(data) {});
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = me.callParent(arguments);
		columns.splice(5, 0, {
			xtype: 'actioncolumn',
			text: '提取',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-import hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					//数据库是否独立部署: 1: 是;2: 否;
					var isDeploy = "" + JcallShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
					if (isDeploy == "1") {
						me.onExtractSaleDocOfDeploy(rec, function(result) {
							//me.onSearch();							
						});
					} else if (isDeploy == "2") {
						me.onExtractSaleDocBySaleDocId(rec);
					}
				}
			}]
		});
		columns.splice(11, 0,{
			dataIndex: 'ReaBmsCenSaleDoc_OrderDocNo',
			text: '订货单号',
			width: 100,
			defaultRenderer: true
		});
		return columns;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();

		var labOrgNo = JShell.REA.System.CENORG_CODE;
		if (!labOrgNo) {
			var error = me.errorFormat.replace(/{msg}/, "获取机构平台编码信息为空!");
			me.getView().update(error);
			return false;
		};
		if (!labOrgNo) labOrgNo = '';

		//供应商自己录入的供货单(数据来源为供应商:1) 并且 供货单状态为(审核通过,供货提取,部分验收) 并且(数据标志为"未提取"或"部分提取") 并且 供货单的订货方为当前机构的所属机构平台编码
		me.defaultWhere =
			"reabmscensaledoc.Source=1 and (reabmscensaledoc.Status in (4,6,7)) and (reabmscensaledoc.IOFlag=0 or reabmscensaledoc.IOFlag=2 or reabmscensaledoc.IOFlag is null) and reabmscensaledoc.ReaServerLabcCode='" +
			labOrgNo + "'";
		me.store.proxy.url = me.getLoadUrl(); //查询条件		
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var buttonsToolbar2 = me.getComponent('buttonsToolbar')
		var search = buttonsToolbar2.getComponent('search');
		var where = [];
		if (me.ReaServerCompCode) {
			where.push("reabmscensaledoc.ReaServerCompCode='" + me.ReaServerCompCode + "'");
		}

		if (search) {
			var value = search.getValue();
			if (value) {
				var searchHql = me.getSearchWhere(value);
				if (searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		me.internalWhere = where.join(" and ");

		var url = me.callParent(arguments);
		//数据库是否独立部署: 1: 是;2: 否;
		var isDeploy = "" + JcallShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
		if (isDeploy == "1") {
			var labcCode = JcallShell.REA.System.CENORG_CODE;
			if (!labcCode) labcCode = "";
			var compCode = me.ReaServerCompCode;
			if (!compCode) compCode = "";
			var platformUrl = JShell.REA.RunParams.Lists["BSPlatformURL"].Value;
			url = url + "&platformUrl=" + platformUrl + "&labcCode=" + labcCode + "&compCode=" + compCode;
		}

		return url;
	},
	/**@description 通过选择供货单提取供货单(客户端与平台在同一数据库)*/
	onExtractSaleDocBySaleDocId: function(rec) {
		var me = this;

		var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenSaleDocOfExtract";
		var id = rec.get("ReaBmsCenSaleDoc_Id");
		var reaServerCompCode = rec.get("ReaBmsCenSaleDoc_ReaServerCompCode");
		var saleDocNo = rec.get("ReaBmsCenSaleDoc_SaleDocNo");
		var reaServerLabcCode = JcallShell.REA.System.CENORG_CODE;
		if (!reaServerLabcCode) reaServerLabcCode = "";
		var params = {
			"saleDocId": id,
			"saleDocNo": saleDocNo,
			"reaServerCompCode": reaServerCompCode,
			"reaServerLabcCode": reaServerLabcCode
		};
		JcallShell.Action.delay(function() {
			JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
				if (data.success) {
					//me.onSearch();
				} else {
					JShell.Msg.error(data.msg);
				}
			});
		}, null,100);
	},
	/**提取平台供货单到客户端(客户端与平台不在同一数据库)*/
	onExtractSaleDocOfDeploy: function(rec, callback) {
		var me = this;
		me.showMask("供货提取中...");
		var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_UDTO_AddSaleDocAndDtlOfPlatformExtract";
		var id = rec.get("ReaBmsCenSaleDoc_Id");
		var reaServerCompCode = rec.get("ReaBmsCenSaleDoc_ReaServerCompCode");
		var saleDocNo = rec.get("ReaBmsCenSaleDoc_SaleDocNo");
		var reaServerLabcCode = JcallShell.REA.System.CENORG_CODE;
		if (!reaServerLabcCode) reaServerLabcCode = "";
		var platformUrl = JShell.REA.RunParams.Lists["BSPlatformURL"].Value;
		var params = {
			"platformUrl": platformUrl,
			"saleDocId": id,
			"saleDocNo": saleDocNo,
			"compCode": reaServerCompCode,
			"labcCode": reaServerLabcCode
		};

		JcallShell.Action.delay(function() {
			JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
				me.hideMask();
				if (data.success) {
					console.log(data);
					me.fireEvent('onExtractAfter', me, data, rec);
					if (callback) {
						callback(data);
					} else {
						me.onSearch();
					}
				} else {
					JShell.Msg.error(data.msg);
				}
			});
		},null,100);
	}
});
