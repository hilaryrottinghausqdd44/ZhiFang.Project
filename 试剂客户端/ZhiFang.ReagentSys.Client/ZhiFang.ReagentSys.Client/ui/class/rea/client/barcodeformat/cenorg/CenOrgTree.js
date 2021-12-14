/**
 * 供应商条码规则维护
 * @author longfc
 * @version 2018-01-30
 */
Ext.define('Shell.class.rea.client.barcodeformat.cenorg.CenOrgTree', {
	extend: 'Shell.class.rea.client.reacenorg.Tree',

	title: '供应商信息',
	width: 300,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgListTreeByOrgID',
	
	/**默认加载数据*/
	defaultLoad: true,
	/**应用类型:是否平台:是:1,否:0或null*/
	APPTYPE: "1",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	createTopToolbar: function() {
		var me = this;
		me.callParent(arguments);
		me.topToolbar.splice(1, 0, {
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
		});
	},
	changeData: function(data) {
		var me = this;
		var changeNode = function(node) {
			//机构编号
			if(node.value && node.value.PlatformOrgNo) {
				node.text = '[' + node.value.PlatformOrgNo + '] ' + node.text;
			} else {
				node.text = '<b style="color:red;">[无平台机构码]</b> ' + node.text;
			}
			var children = node[me.defaultRootProperty];
			if(children) {
				changeChildren(children);
			}
		};

		var changeChildren = function(children) {
			Ext.Array.each(children, changeNode);
		};
		var children = data[me.defaultRootProperty];
		changeChildren(children);

		return data;
	},
	onBeforeLoad: function() {
		var me = this;
		if(me.OrgType == null || me.OrgType == undefined || me.OrgType == "") return false;
		me.store.proxy.url = me.getLoadUrl();
	},
	getSearchFields: function() {
		var me = this;
		return "ReaCenOrg_Id,ReaCenOrg_OrgNo,ReaCenOrg_PlatformOrgNo";
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + ('fields=' + me.getSearchFields() + "&orgType=" + me.OrgType);
		return url;
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