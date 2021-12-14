/**
 * 站点列表
 * @author ghx
 * @version 2021-01-07
 */
Ext.define('Shell.class.weixin.sampling.basic.dic.ClientCheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '站点列表',
	width: 270,
	height: 300,
	account:'',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBusinessLogicClientControlByHQL?isPlanish=true',
	isWinOpen:false,
	/**是否单选*/
	checkOne: true,
	/**默认选中*/
	autoSelect: true,
	initComponent: function () {
		var me = this;
		me.externalWhere="businesslogicclientcontrol.Account='"+me.account+"'";
		me.defaultWhere = me.defaultWhere || '';

		if (me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}

		//查询框信息
		me.searchInfo = {
			width: 145, emptyText: '名称', isLike: true,
			fields: ['businesslogicclientcontrol.CLIENTELE.CNAME']
		};

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function () {
		var me = this;

		var columns = [{
			text: '采血站点名称', dataIndex: 'BusinessLogicClientControl_CLIENTELE_CNAME', width: 200,
			sortable: false, menuDisabled: true, defaultRenderer: true
		}, {
			text: '主键ID', dataIndex: 'BusinessLogicClientControl_CLIENTELE_Id', isKey: true, hidden: true, hideable: false
		}];
		return columns;
	}
});