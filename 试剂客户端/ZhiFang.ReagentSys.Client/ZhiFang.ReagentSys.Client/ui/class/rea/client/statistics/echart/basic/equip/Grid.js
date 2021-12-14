/**
 * EChart图表统计-按仪器
 * @author longfc
 * @version 2019-02-28
 */
Ext.define('Shell.class.rea.client.statistics.echart.basic.equip.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	title: '仪器列表',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipLabByHQL?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaTestEquipLab_DispOrder',
		direction: 'ASC'
	}],

	defaultLoad: true,
	//序号列宽度
	rowNumbererWidth: 35,
	//后台排序
	remoteSort: false,
	/**默认每页数量*/
	defaultPageSize: 200,
	//带分页栏
	hasPagingtoolbar: true,
	//带功能按钮栏
	hasButtontoolbar: false,
	//不加载时默认禁用功能按钮
	defaultDisableControl: false,
	features: [{
		ftype: 'summary'
	}],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaTestEquipLab_LisCode',
			text: 'Lis编码',
			hidden:true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_CName',
			text: '仪器名称',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipLab_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			text: '总金额（元）',
			dataIndex: 'SumTotal',
			align: 'right',
			type: 'float',
			summaryType: 'sum',
			renderer: function(value,meta) {
				var v=Ext.util.Format.currency(value, '￥', 2) + '元';
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return value;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">' + Ext.util.Format.currency(value, '￥', 2) + '</span>';
			}
		}, {
			text: '金额占总比(%)',
			dataIndex: 'SumTotalPercent',
			align: 'right',
			width: 90,
			renderer: function(value, meta) {
				var v = "";
				if(value) v = value + '%';
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}];

		return columns;
	}

});