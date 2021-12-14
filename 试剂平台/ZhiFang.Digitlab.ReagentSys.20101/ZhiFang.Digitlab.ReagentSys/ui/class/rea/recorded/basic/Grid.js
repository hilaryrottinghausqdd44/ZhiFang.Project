/**
 * 入账管理列表-基础
 * @author liangyl
 * @version 2017-06-02
 */
Ext.define('Shell.class.rea.recorded.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '入账管理列表-基础',

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsAccountInputByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DeleteBmsAccountInputAndDtList',
	/**新增数据服务路径*/
	addUrl: '/ReagentSysService.svc/ST_UDTO_AddBmsAccountInput',
	/**修改数据服务路径*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateBmsAccountInputByField',
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BmsAccountInput_DataAddTime',
		direction: 'DESC'
	}],
	/**隐藏删除列*/
	hideDelColumn: false,
	/**默认选中*/
	autoSelect: true,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.regStr = new RegExp('"', "g");
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-'];
		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '名称',
			isLike: true,
			itemId: 'Search',
			fields: ['bmsaccountinput.CName']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'BmsAccountInput_CName',
			text: '名称',
			sortable: false,
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsAccountInput_UserName',
			text: '入帐人',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsAccountInput_DataAddTime',
			sortable: false,
			text: '入帐时间',
			width: 85,
			type: 'date',
			isDate: true
		}, {
			dataIndex: 'BmsAccountInput_TotalPrice',
			sortable: false,
			text: '总金额',
			align: 'center',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsAccountInput_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			xtype: 'actioncolumn',
			text: '明细',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			sortable: false,
			hideable: false,
			items: [{
				iconCls: 'button-show hand',
				tooltip: '<b>明细</b>',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('BmsAccountInput_Id');
					me.showAccountSaleDocById(id);
				}
			}]
		}, {
			dataIndex: 'BmsAccountInput_Comment',
			text: '备注',
			sortable: false,
			flex: 1,
			renderer: function(value, meta, record) {
				var v = me.showMemoText(value, meta, record);
				return v;
			}
		}];
		return columns;
	},
	onAddClick: function() {},
	/**显示明细*/
	showAccountSaleDocById: function(id) {
		var me = this;
		var height = document.body.clientHeight - 150;
		JShell.Win.open('Shell.class.rea.recorded.show.Grid', {
			height: height,
			//resizable: false,
			BmsAccountInputID: id //入账单ID
		}).show();
	},
	/**只显示一行*/
	showMemoText: function(value, meta, record) {
		var me = this;
		var val = value.replace(/(^\s*)|(\s*$)/g, "");
		val = val.replace(/\\r\\n/g, "<br />");
		val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1 = v.indexOf("</br>");
		if(index1 > 0) v = v.substring(0, index1);
		var Comment = "" + record.get("BmsAccountInput_Comment");
		Comment = Comment.replace(me.regStr, "'");
		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>备注:</b>" + value + "</p>";
		meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		return v
	}
});