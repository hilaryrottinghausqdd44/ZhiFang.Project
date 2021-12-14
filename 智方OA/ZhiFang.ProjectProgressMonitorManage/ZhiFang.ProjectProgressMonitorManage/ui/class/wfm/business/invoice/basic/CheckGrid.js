/**
 * 付款单位()
 * @author liangyl	
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.invoice.basic.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '销售人员同客户关系表',
	width: 270,
	height: 300,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPSalesManClientLinkByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'PSalesManClientLink_DataAddTime',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.defaultWhere += "psalesmanclientlink.SalesManID=" + userId;
		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '名称',
			isLike: true,
			fields: ['psalesmanclientlink.PClientName']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '编号',
			dataIndex: 'PSalesManClientLink_PClientID',
			isKey: true,
//			hidden: true,
			hideable: false
		}, {
			text: '名称',
			dataIndex: 'PSalesManClientLink_PClientName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '时间戳',
			dataIndex: 'PSalesManClientLink_DataTimeStamp',
			hidden: true,
			hideable: false
		}]
		return columns;
	}
});