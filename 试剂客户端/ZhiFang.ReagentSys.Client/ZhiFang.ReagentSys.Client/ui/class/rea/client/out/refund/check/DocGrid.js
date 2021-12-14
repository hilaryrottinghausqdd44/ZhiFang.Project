/**
 * 出库总单
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.out.refund.check.DocGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '出库总单列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsOutDocByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsInDoc',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDocByField',
	/**默认加载*/
	defaultLoad: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsOutDoc_DataAddTime',
		direction: 'DESC'
	}],
    autoSelect: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//单选双击触发确认事件
		if(me.checkOne){
			me.on({
				itemdblclick:function(view,record){
				    me.fireEvent('accept',me,record);
				}
			});
		}
	},
	initComponent: function() {
		var me = this;
			//查询框信息
		me.searchInfo = {
			width:200,isLike:true,itemId: 'Search',
			emptyText:'出库单号/领用人',
			fields:['reabmsoutdoc.OutDocNo','reabmsoutdoc.TakerName']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsOutDoc_DataAddTime',
			text: '出库时间',
			align: 'center',
			width: 135,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsOutDoc_OutDocNo',
			text: '出库单号',
			width: 150	,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsOutDoc_DeptName',
			text: '使用部门',
			width: 150	,
			defaultRenderer: true
		},  {
			dataIndex: 'ReaBmsOutDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'ReaBmsOutDoc_TakerName',
			text: '领用人',
			width: 80	,
			defaultRenderer: true
		}];

		return columns;
	}
});