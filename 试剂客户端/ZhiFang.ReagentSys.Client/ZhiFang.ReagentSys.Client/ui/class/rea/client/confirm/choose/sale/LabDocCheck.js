/**
 * 本地供货单选择
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.choose.sale.LabDocCheck', {
	extend: 'Shell.class.rea.client.confirm.choose.sale.SaleDocCheck',

	title: '本地供货单选择',
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocByHQL?isPlanish=true',

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
	userUIKey: 'confirm.choose.sale.LabDocCheck',
	/**用户UI配置Name*/
	userUIName: "本地供货单选择列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 360,
			emptyText: '供货单号',
			itemId: 'search',
			isLike: true,
			fields: ['reabmscensaledoc.SaleDocNo']
		};
		me.callParent(arguments);
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();

		var labOrgNo = JShell.REA.System.CENORG_CODE;
		if(!labOrgNo) {
			var error = me.errorFormat.replace(/{msg}/, "获取机构平台编码信息为空!");
			me.getView().update(error);
			return false;
		};
		if(!labOrgNo) labOrgNo = '';
		//reabmscensaledoc.Source=2 and 
		//实验室自己录入的供货单(数据来源为实验室:2) 并且 (审核通过或部分验收,供货提取)
		me.defaultWhere="((reabmscensaledoc.ReaServerLabcCode='"+labOrgNo+"') and (reabmscensaledoc.Status=4 or reabmscensaledoc.Status=6 or reabmscensaledoc.Status=7))";
		
		me.store.proxy.url = me.getLoadUrl(); //查询条件		
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var buttonsToolbar2 = me.getComponent('buttonsToolbar')
		var search = buttonsToolbar2.getComponent('search');
		var where = [];
		if(me.ReaServerCompCode) {
			where.push("reabmscensaledoc.ReaServerCompCode='" + me.ReaServerCompCode + "'");
		}
		if(search) {
			var value = search.getValue();
			if(value) {
				var searchHql = me.getSearchWhere(value);
				if(searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		me.internalWhere = where.join(" and ");
		return me.callParent(arguments);
	}
});