/**
 * 使用出库月用量统计
 * @author longfc
 * @version 2018-09-10
 */
Ext.define('Shell.class.rea.client.statistics.monthusage.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.DtlGrid',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.TextSearchTrigger'
	],
	title: '月用量统计',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaMonthUsageStatisticsDtlByAllJoinHql?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**用户UI配置Key*/
	userUIKey: 'statistics.monthusage.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "月用量统计明细列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		//查询框信息
		me.searchInfo = null;
		var items = me.createFullscreenItems();
		items.push({
			emptyText: '一级分类',
			labelWidth: 0,
			width: 115,
			itemId: 'GoodsClass',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '一级分类',
				ClassType: "GoodsClass"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClass');
				}
			}
		}, {
			emptyText: '二级分类',
			labelWidth: 0,
			width: 135,
			itemId: 'GoodsClassType',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '二级分类',
				ClassType: "GoodsClassType"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClassType');
				}
			}
		});
		items.push('-', 'refresh');

		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaMonthUsageStatisticsDtl_ReaGoodsNo',
			text: '货品编码',
			width: 125,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaMonthUsageStatisticsDtl_GoodsName',
			text: '货品',
			width: 160,
			defaultRenderer: true
		}, {
			text: '月用量',
			dataIndex: 'ReaMonthUsageStatisticsDtl_OutQty',
			width: 95,
			hideable: false,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaMonthUsageStatisticsDtl_GoodsUnit',
			text: '单位',
			width: 75,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaMonthUsageStatisticsDtl_UnitMemo',
			text: '规格',
			width: 110,
			defaultRenderer: true
		}];

		return columns;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		if(!me.PK) return;

		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onSearch();
	},
	/**机构货品查询条件*/
	getReaGoodsHql: function() {
		var me = this,
			arr = [];
		var buttonsToolbar1 = me.getComponent('buttonsToolbar'),
			goodsClass = buttonsToolbar1.getComponent('GoodsClass').getValue(),
			goodsClassType = buttonsToolbar1.getComponent('GoodsClassType').getValue();
		//一级分类	
		if(goodsClass) {
			arr.push("reagoods.GoodsClass='" + goodsClass + "'");
		}
		//二级分类	
		if(goodsClassType) {
			arr.push("reagoods.GoodsClassType='" + goodsClassType + "'");
		}
		var where = "";
		if(arr && arr.length > 0) where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		return where;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		//		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
		//			JShell.System.Path.ROOT) + me.selectUrl;
		//		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		//
		//		//查询条件
		//		var whereHql = "";
		//		if(whereHql) {
		//			url += '&where=' + JShell.String.encode(whereHql);
		//		}
		var url = me.callParent(arguments);

		//机构货品查询条件
		var reaGoodsHql = me.getReaGoodsHql();
		if(reaGoodsHql) {
			url += '&reaGoodsHql=' + JShell.String.encode(reaGoodsHql);
		}
		return url;
	}
});