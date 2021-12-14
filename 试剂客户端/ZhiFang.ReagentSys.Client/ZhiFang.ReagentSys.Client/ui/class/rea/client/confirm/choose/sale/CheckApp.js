/**
 * 供货单选择
 * @author longfc
 * @version 2018-05-09
 */
Ext.define('Shell.class.rea.client.confirm.choose.sale.CheckApp', {
	extend: 'Shell.ux.panel.AppPanel',

	/**供应商ID*/
	ReaCompID: null,
	ReaCompCName: null,
	/**供应商平台编码*/
	ReaServerCompCode: null,
	/**访问BS平台的URL*/
	BSPlatformURL: null,
	/**客户端数据库是否独立部署*/
	ReaDataBaseIsDeploy: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//本地供货单列表
		me.LabDocCheck.on({
			accept: function(p, record) {
				//当客户端与平台在同一数据库时,只显示一个供货列表
				//数据库是否独立部署: 1: 是;2: 否;
				var isDeploy = "" + JcallShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
				if(isDeploy == "1") {
					me.fireEvent('accept', me, record);
				} else {
					if(record) {
						//如果是供应商供货,还未提取选择的供货单,需要先提取后才能验收
						var IOFlag = "" + record.get("ReaBmsCenSaleDoc_IOFlag");
						//未提取,部分提取
						if(IOFlag == "0" || IOFlag == "2") {
							me.onExtractSaleDocBySaleDocId(record);
						} else {
							//已提取的供货单
							me.fireEvent('accept', me, record);
						}
					}
				}
			}
		});
		//平台供货单列表(未提取的供货单)
		me.PlatformSaleCheck.on({
			accept: function(p, record) {
				//数据库是否独立部署: 1: 是;2: 否;
				var isDeploy = "" + JcallShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
				if(isDeploy == "1") {
					me.onExtractSaleDocOfDeploy(record);
				} else if(isDeploy == "2") {
					me.onExtractSaleDocBySaleDocId(record);
				}
			},
			onExtractAfter: function(p, result, record) {
				me.extractAfter();
			}
		});
		//系统运行参数"数据库是否独立部署"
		JcallShell.REA.RunParams.getRunParamsValue("ReaDataBaseIsDeploy", false, function(data) {
			//console.log(data);
			if(data.success) {
				//访问BS平台的URL
				JcallShell.REA.RunParams.getRunParamsValue("BSPlatformURL", false, function(data) {
					me.initUI();
				});
			} else {
				me.initUI();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var height1 = document.body.clientHeight * 0.98 / 2;
		//本地供货单
		me.LabDocCheck = Ext.create('Shell.class.rea.client.confirm.choose.sale.LabDocCheck', {
			header: true,
			title: "本地供货单",
			itemId: 'LabDocCheck',
			region: 'center',
			split: true,
			collapsible: false,
			collapsed: false,
			ReaServerCompCode: me.ReaServerCompCode,
			ReaCompID: me.ReaCompID,
			ReaCompCName: me.ReaCompCName
		});
		//平台供货单(供应商供货单)
		me.PlatformSaleCheck = Ext.create('Shell.class.rea.client.confirm.choose.sale.PlatformSaleCheck', {
			header: true,
			title: "平台未提取供货信息",
			itemId: 'PlatformSaleCheck',
			region: 'south',
			height: height1,
			split: true,
			collapsible: false,
			collapsed: false,
			animCollapse: false,
			hidden: true,
			ReaServerCompCode: me.ReaServerCompCode,
			ReaCompID: me.ReaCompID,
			ReaCompCName: me.ReaCompCName
		});
		var appInfos = [me.PlatformSaleCheck, me.LabDocCheck];

		return appInfos;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		}
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	},
	initUI: function() {
		var me = this;
		//ReaDataBaseIsDeploy: 数据库是否独立部署: 1: 是;2: 否;		
		var isDeploy = "" + JcallShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
		//BSPlatformURL: 访问BS平台的URL
		var purl = JcallShell.REA.RunParams.Lists["BSPlatformURL"].Value;
		if(isDeploy == "1" && purl) {
			me.PlatformSaleCheck.onSearch();
			me.LabDocCheck.header = true;
			me.PlatformSaleCheck.setVisible(true);
			me.PlatformSaleCheck.expand();
		} else {
			me.LabDocCheck.header = false;
			me.LabDocCheck.split = false;
			me.PlatformSaleCheck.setVisible(false);
			me.PlatformSaleCheck.split = false;
			me.PlatformSaleCheck.collapse();
		}
		me.LabDocCheck.onSearch();
	},
	/**@description 通过选择供货单提取供货单(客户端与平台在同一数据库)*/
	onExtractSaleDocBySaleDocId: function(record) {
		var me = this;

		var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenSaleDocOfExtract";
		var id = record.get("ReaBmsCenSaleDoc_Id");
		var reaServerCompCode = record.get("ReaBmsCenSaleDoc_ReaServerCompCode");
		var saleDocNo = record.get("ReaBmsCenSaleDoc_SaleDocNo");
		var reaServerLabcCode = JcallShell.REA.System.CENORG_CODE;
		if(!reaServerLabcCode) reaServerLabcCode = "";
		var params = {
			"saleDocId": id,
			"reaServerCompCode": reaServerCompCode,
			"saleDocNo": saleDocNo,
			"reaServerLabcCode": reaServerLabcCode
		};
		JcallShell.Action.delay(function() {
			JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
				if(data.success) {
					me.fireEvent('accept', me, record);
				} else {
					JShell.Msg.error(data.msg);
				}
			});
		}, null);
	},
	/**提取平台供货单到客户端(客户端与平台不在同一数据库)*/
	onExtractSaleDocOfDeploy: function(record) {
		var me = this;
		me.PlatformSaleCheck.onExtractSaleDocOfDeploy(record, function(result) {

		});
	},
	/**提取平台供货单到客户端后(客户端与平台不在同一数据库)*/
	extractAfter: function(result, record) {
		var me = this;
		me.LabDocCheck.onSearch();
		me.PlatformSaleCheck.onSearch();
	}
});