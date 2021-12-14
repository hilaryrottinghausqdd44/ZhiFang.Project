/**
 * 已选的供货单
 * @author liangyl
 * @version 2017-06-02
 */
Ext.define('Shell.class.rea.recorded.basic.accountsaledoc.Grid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '已选的供货单',

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsAccountSaleDocByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentService.svc/ST_UDTO_DelBmsAccountSaleDoc',
	/**新增数据服务路径*/
	addUrl: '/ReagentService.svc/ST_UDTO_AddBmsAccountSaleDoc',
	/**修改数据服务路径*/
	editUrl: '/ReagentService.svc/ST_UDTO_UpdateBmsAccountSaleDocByField',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BmsAccountSaleDoc_DataAddTime',
		direction: 'ASC'
	}],
	/**默认选中*/
	autoSelect: false,
	/**是否单选*/
	checkOne: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onRemoveClick');
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	initButtonToolbarItems: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.buttonToolbarItems || [];
		me.buttonToolbarItems.unshift({
			xtype: 'button',
			iconCls: 'button-show',
			text: '检查是否已入账',
			tooltip: '检查是否已入账',
			handler: function() {
				me.onCheckAccountInput();
			}
		}, {
			text: '删除',
			iconCls: 'button-del',
			tooltip: '<b>删除勾选中选择行</b>',
			handler: function() {
				var records = me.getSelectionModel().getSelection();
				me.fireEvent('onRemoveClick', records, me);
			}
		});
		return me.buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_SaleDocNo',
			text: '供货单号',
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 35,
			hidden: true,
			sortable: false,
			menuDisabled: false,
			sortable: false,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.fireEvent('onRemoveClick', rec, grid);
					me.store.remove(rec);
				}
			}]
		}, {
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_TotalPrice',
			text: '总计金额',
			align: 'center',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			text: '已入账',
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_IsAccountInput',
			width: 55,
			align: 'center',
			sortable: false,
			isBool: true,
			type: 'bool'
		}, {
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_Lab_CName',
			text: '订货方',
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_Comp_CName',
			text: '供货方',
			width: 150,
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_Id',
			text: '供货ID',
			hidden: true,
			sortable: false,
			hideable: false
		}, {
			dataIndex: 'BmsAccountSaleDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			sortable: false,
			isKey: true
		}, {
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_OperDate',
			text: '操作时间',
			hidden: true,
			sortable: false,
			align: 'center',
			width: 130,
			isDate: true,
			hasTime: true
		}];
		return columns;
	},
	/**增加多行*/
	addDateRecs: function(records) {
		var me = this;
		for(var i in records) {
			var obj = {
				BmsAccountSaleDoc_BmsCenSaleDoc_SaleDocNo: records[i].get('BmsCenSaleDoc_SaleDocNo'),
				BmsAccountSaleDoc_BmsCenSaleDoc_Id: records[i].get('BmsCenSaleDoc_Id'),
				BmsAccountSaleDoc_BmsCenSaleDoc_Comp_CName: records[i].get('BmsCenSaleDoc_Comp_CName'),
				BmsAccountSaleDoc_BmsCenSaleDoc_Lab_CName: records[i].get('BmsCenSaleDoc_Lab_CName'),
				BmsAccountSaleDoc_BmsCenSaleDoc_TotalPrice: records[i].get('BmsCenSaleDoc_TotalPrice'),
				BmsAccountSaleDoc_BmsCenSaleDoc_OperDate: records[i].get('BmsCenSaleDoc_OperDate')
			};
			me.store.insert(me.getStore().getCount(), obj);
		}
	},
	/**增加单行*/
	addDateRec: function(record) {
		var me = this;
		var obj = {
			BmsAccountSaleDoc_BmsCenSaleDoc_SaleDocNo: record.get('BmsCenSaleDoc_SaleDocNo'),
			BmsAccountSaleDoc_BmsCenSaleDoc_Id: record.get('BmsCenSaleDoc_Id'),
			BmsAccountSaleDoc_BmsCenSaleDoc_Comp_CName: record.get('BmsCenSaleDoc_Comp_CName'),
			BmsAccountSaleDoc_BmsCenSaleDoc_Lab_CName: record.get('BmsCenSaleDoc_Lab_CName'),
			BmsAccountSaleDoc_BmsCenSaleDoc_TotalPrice: record.get('BmsCenSaleDoc_TotalPrice'),
			BmsAccountSaleDoc_BmsCenSaleDoc_OperDate: record.get('BmsCenSaleDoc_OperDate')
		};
		me.store.insert(me.getStore().getCount(), obj);
	},
	/**删除多行数据*/
	DelDataRecS: function(records, CheckGrid) {
		var me = this;
		for(var i in records) {
			//删除待选供货单数组的某一项
			Ext.Array.remove(CheckGrid.changeList, records[i].get('BmsAccountSaleDoc_BmsCenSaleDoc_Id')); //删除数组中指定元素 注意：只删除一项
			me.store.remove(records[i]);
		}
	},
	/**获取列表供货单id*/
	getBmsCenSaleDocIdS: function() {
		var me = this;
		var ids = '',
			i = 0;
		me.store.each(function(record) {
			if(i > 0) {
				ids += ',';
			}
			ids += record.get('BmsAccountSaleDoc_BmsCenSaleDoc_Id');
			i = i + 1;
		});
		return ids;
	},
	/**返回总金额*/
	getTotalPrice: function() {
		var me = this;
		var TotalPrice = 0;
		me.store.each(function(record) {
			TotalPrice += Number(record.get('BmsAccountSaleDoc_BmsCenSaleDoc_TotalPrice'));
		});
		TotalPrice = TotalPrice.toFixed(2); // 输出结果为 2.45
		return TotalPrice;
	},
	/**刷新按钮点击处理方法*/
	onRefreshClick: function() {
		this.getView().refresh();
	},
	/**检查是否已入账*/
	checkIsAccountInput: function(callback) {
		var me = this;
		var ids = me.getBmsCenSaleDocIdS();
		if(!ids) return;
		url = JShell.System.Path.getRootUrl(me.selectUrl);

		url += '&fields=BmsAccountSaleDoc_BmsCenSaleDoc_Id,BmsAccountSaleDoc_Id';
		url += '&where=bmsaccountsaledoc.BmsCenSaleDoc.Id in(' + ids + ')';

		JShell.Server.get(url, function(data) {
			callback(data);
		});
	},
	/**检查是否已入账*/
	onCheckAccountInput: function() {
		var me = this;
		me.checkIsAccountInput(function(data) {
			if(data && data.value) {
				var list = data.value.list;
				me.store.each(function(record) {
					var Id = record.get('BmsAccountSaleDoc_BmsCenSaleDoc_Id');
					for(var i = 0; i < list.length; i++) {
						if(Id == list[i].BmsAccountSaleDoc_BmsCenSaleDoc_Id) {
							record.set('BmsAccountSaleDoc_BmsCenSaleDoc_IsAccountInput', true);
							record.commit();
							break;
						}
					}
				});
			}
		});
	}
});