/**
 * 套餐项目明细列表
 * @author longfc
 * @version 2017-04-17
 */
Ext.define('Shell.class.weixin.item.check.GroupItemGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '套餐项目明细列表',
	width: 270,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabGroupItemByPItemNoAndAreaID?isPlanish=true',
	/**是否单选*/
	checkOne: true,
	/**PItemNo*/
	PItemNo: null,
	/**区域ID*/
	AreaID: null,
	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '项目编号',
			dataIndex: 'BLabTestItem_ItemNo',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '名称',
			dataIndex: 'BLabTestItem_CName',
			width: 120,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '代码',
			dataIndex: 'BLabTestItem_ShortCode',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '简称',
			dataIndex: 'BLabTestItem_ShortName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '英文名称',
			dataIndex: 'BLabTestItem_EName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BLabTestItem_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		if (me.AreaID) {
			url += '&areaID=' +me.AreaID;
		}
		if (me.PItemNo) {
			url += '&pitemNo=' +me.PItemNo;
		}
		return url;
		//return me.callParent(arguments);
	}
});