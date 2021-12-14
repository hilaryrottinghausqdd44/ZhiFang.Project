/**
 * 批次信息
 * @author liangyl
 * @version 2018-01-24
 */
Ext.define('Shell.class.rea.client.stock.confirm.LotCheckGrid', {
	extend: 'Shell.class.rea.client.goodslot.CheckGrid',
	width: 600,
	height: 400,
	/**创建数据列*/
	
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoodsLot_ReaGoodsNo',
			text: '货品编码',
			width: 100,
			hidden:true,
			defaultRenderer: true

		}, {
			dataIndex: 'ReaGoodsLot_LotNo',
			text: '货品批号',
			flex: 1,
			maxWidth: 180,
			editor: {
				allowBlank: false
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_ProdDate',
			text: '生产日期',
			width: 100,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_InvalidDate',
			text: '有效期',
			width: 100,
			type: 'date',
			isDate: true,
			defaultRenderer: true

		}, {
			xtype: 'checkcolumn',
			dataIndex: 'ReaGoodsLot_Visible',
			text: '启用',
			hidden: true,
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_DispOrder',
			text: '显示次序',
			width: 70,
			align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_GoodsID',
			text: '货品主键ID',
			hidden: true,
			hideable: false,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 45,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, rec) {
					var id = rec.get(me.PKField);
					if(id)
						return '';
					else
						return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					if(id) {
						JShell.Msg.alert("当前货品批号信息不允许删除！", null, 2000);
					} else {
						me.store.remove(rec);
						me.delCount++;
					}
				}
			}]
		}];
		columns.push({
			dataIndex: me.DelField,
			text: '',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var v = '';
				if(value === 'true') {
					v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';
				}
				if(value === 'false') {
					v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';
				}
				var msg = record.get('ErrorInfo');
				if(msg) {
					meta.tdAttr = 'data-qtip="<b style=\'color:red\'>' + msg + '</b>"';
				}

				return v;
			}
		});
		return columns;
	}
});