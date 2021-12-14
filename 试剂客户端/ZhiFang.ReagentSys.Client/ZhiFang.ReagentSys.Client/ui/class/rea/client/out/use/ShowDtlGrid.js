/**
 * 使用出库明细列表
 * @author longfc
 * @version 2019-03-26
 */
Ext.define('Shell.class.rea.client.out.use.ShowDtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.ShowDtlGrid',

	/**用户UI配置Key*/
	userUIKey: 'out.use.ShowDtlGrid',
	/**用户UI配置Name*/
	userUIName: "使用出库明细列表",

	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);

		columns.splice(1, 0, {
			dataIndex: 'ReaBmsOutDtl_IsNeedBOpen',
			text: '开瓶管理',
			width: 90,
			type: 'bool',
			hidden: true,
			isBool: true,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '开瓶管理',
			align: 'center',
			width: 65,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				getClass: function(v, meta, record) {
					var isNeedBOpen = "" + record.get("ReaBmsOutDtl_IsNeedBOpen");
					if (isNeedBOpen == "1" || isNeedBOpen == "true") {
						return 'button-edit hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openNeedBOpen(rec);
				}
			}]
		});
		columns.splice(12, 0, {
			dataIndex: 'ReaBmsOutDtl_ReqGoodsQty',
			text: '申请数',
			sortable: false,
			width: 70,
			defaultRenderer: true
		});
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', {
			xtype: 'button',
			hidden: true,
			iconCls: 'button-add',
			text: '开瓶管理',
			handler: function() {
				var records = me.OrderGrid.getSelectionModel().getSelection();
				if (records.length <= 0) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var visible = "" + rec.get("ReaBmsOutDtl_IsNeedBOpen");
				if (isNeedBOpen == "1" || isNeedBOpen == "true") {
					me.openNeedBOpen(records[0]);
				} else {

				}
			}
		}];
		return items;
	},
	openNeedBOpen: function(rec) {
		var me = this;
		var outDtlID = rec.get("ReaBmsOutDtl_Id");
		var maxWidth = 380; //document.body.clientWidth * 0.99;
		var height = 420; // document.body.clientHeight * 0.98;
		var win = JShell.Win.open('Shell.class.rea.client.openbottleoper.Form', {
			title: '开瓶管理',
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '2',
			OutDtlID: outDtlID,
			/**获取数据服务路径*/
			selectUrl: '/ReaManageService.svc/ST_UDTO_GetOBottleOperDocByOutDtlId?isPlanish=true',
			listeners: {
				beforeclose: function(p, eOpts) {

				}
			}
		}).show();
		
		win.isEdit(outDtlID);
	}
});
