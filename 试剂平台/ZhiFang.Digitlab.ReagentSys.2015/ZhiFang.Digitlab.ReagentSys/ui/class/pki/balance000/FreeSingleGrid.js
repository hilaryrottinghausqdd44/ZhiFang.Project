/**
 * 免单项目
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance.FreeSingleGrid', {
	extend: 'Shell.class.pki.balance.ItemBasicGrid',

	title: '免单',

	/**修改服务地址*/
	editUrl: '/BaseService.svc/ST_UDTO_UpdateNRequestItemByField',

	/**默认条件*/
	defaultWhere: "(nrequestitem.ItemPriceType='1' or nrequestitem.ItemPriceType='2' or nrequestitem.ItemPriceType='3')",
	/**显示价格类型下拉框*/
	showItemPriceTypeCombobox: true,

	/**默认选中送检时间*/
	isDateRadio: false,

	/**价格类型列表*/
	ItemPriceTypeList: [
		[0, '全部', 'font-weight:bold;color:black;'],
		['1', '合同价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
		['2', '阶梯价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';'],
		['3', '免单价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E3'] + ';']
	],

	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),

	initComponent: function() {
		var me = this;

		//初始化送检时间
		me.initDate();
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			text: '免单',
			iconCls: 'button-text-free',
			tooltip: '<b>批量免单</b>',
			handler: function() {
				me.onCheckedFreeClick(false);
			}
		}, '-', {
			text: '解单',
			iconCls: 'button-text-relieve',
			tooltip: '<b>批量解单</b>',
			handler: function() {
				me.onCheckedFreeClick(true);
			}
		}, '-', 'save'];
		//创建挂靠功能栏
		me.dockedItems = me.createFilterToolbar();

		me.columns = [{
			dataIndex: 'NRequestItem_NRequestForm_NFClientName',
			text: '送检单位',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_NFDeptName',
			text: '科室',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_CName',
			text: '姓名',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BarCode',
			text: '条码号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BTestItem_CName',
			text: '项目',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BBillingUnit_Name',
			text: '开票方',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BillingUnitInfo',
			text: '开票方信息',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BDealer_Name',
			text: '经销商',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_ItemPriceType',
			text: '价格类型',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.ItemPriceType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'NRequestItem_ItemContPrice',
			text: '合同价格',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_IsFreeType',
			width: 60,
			text: '免单类型',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_ItemPrice',
			text: '<b style="color:blue;">免单价格</b>',
			width: 60,
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
			},
			sortable: false,
			type: 'float'
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var type = record.get('NRequestItem_ItemPriceType');
					if (type == '3') {
						meta.tdAttr = 'data-qtip="<b>解除免单</b>"';
						meta.style = 'background-color:green;';
						return 'button-text-relieve hand';
					} else if (type == '1' || type == '2') {
						meta.tdAttr = 'data-qtip="<b>免单</b>"';
						meta.style = 'background-color:red;';
						return 'button-text-free hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isOpen = rec.get('NRequestItem_ItemPriceType') == '3' ? true : false;
					me.onFreeClick(id, isOpen);
				}
			}]
		}, {
			dataIndex: 'NRequestItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, ];

		me.callParent(arguments);
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;

		var date = JShell.Date.getNextDate(new Date(), JShell.PKI.Balance.Dates);

		var year = date.getFullYear();
		var month = date.getMonth() + 1;

		me.BeginDate = JShell.Date.getMonthFirstDate(year, month);
		me.EndDate = JShell.Date.getMonthLastDate(year, month);

		me.YearMonth = year;
		me.MonthMonth = month;

	},
	/**选中的数据免单/解除免单*/
	onCheckedFreeClick: function(isOpen) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		var msg = '';
		var ItemPriceType = '';
		if (isOpen) {
			msg = "确定要取消免单吗";
			ItemPriceType = '1';
		} else {
			msg = "确定要免单吗";
			ItemPriceType = '3';
		}

		if (isOpen) {
			JShell.Msg.confirm({
				msg:msg
			}, function(but) {
				if (but != "ok") return;

				me.showMask(me.saveText); //显示遮罩层
				me.saveErrorCount = 0;
				me.saveCount = 0;
				me.saveLength = len;

				for (var i in records) {
					var id = records[i].get(me.PKField);
					me.updateOneByParams(id, {
						entity: {
							Id: id,
							ItemPriceType: ItemPriceType
						},
						fields: 'Id,ItemPriceType,IsFreeType'
					});
				}
			});
		} else {
			JShell.Win.open('Shell.class.pki.balance.FreeTypeWin', {
				formtype:'add',
				resizable: false,
				listeners: {
					save: function(win, values) {
						var FreeType = values.FreeType;
						var ItemFreePrice = values.ItemFreePrice;
						win.close();

						me.showMask(me.saveText); //显示遮罩层
						me.saveErrorCount = 0;
						me.saveCount = 0;
						me.saveLength = len;

						for (var i in records) {
							var id = records[i].get(me.PKField);
							me.updateOneByParams(id, {
								entity: {
									Id: id,
									ItemPriceType: ItemPriceType,
									IsFreeType: FreeType,
									ItemFreePrice:ItemFreePrice
								},
								fields: 'Id,ItemPriceType,IsFreeType,ItemFreePrice'
							});
						}
					}
				}
			}).show();
		}
	},
	/**单个数据免单/解除免单*/
	onFreeClick: function(id, isOpen) {
		var me = this;

		var msg = '';
		var ItemPriceType = '';
		if (isOpen) {
			msg = "确定要取消免单吗";
			ItemPriceType = '1';
		} else {
			msg = "确定要免单吗";
			ItemPriceType = '3';
		}

		if (isOpen) {
			JShell.Msg.confirm({
				msg:msg
			}, function(but) {
				if (but != "ok") return;

				me.showMask(me.saveText); //显示遮罩层
				me.saveErrorCount = 0;
				me.saveCount = 0;
				me.saveLength = 1;

				me.updateOneByParams(id, {
					entity: {
						Id: id,
						ItemPriceType: ItemPriceType
					},
					fields: 'Id,ItemPriceType,IsFreeType'
				});
			});
		} else {
			JShell.Win.open('Shell.class.pki.balance.FreeTypeWin', {
				formtype:'add',
				resizable: false,
				listeners: {
					save: function(win, values) {
						var FreeType = values.FreeType;
						var ItemFreePrice = values.ItemFreePrice;
						win.close();

						me.showMask(me.saveText); //显示遮罩层
						me.saveErrorCount = 0;
						me.saveCount = 0;
						me.saveLength = 1;

						me.updateOneByParams(id, {
							entity: {
								Id: id,
								ItemPriceType: ItemPriceType,
								IsFreeType: FreeType,
								ItemFreePrice:ItemFreePrice
							},
							fields: 'Id,ItemPriceType,IsFreeType,ItemFreePrice'
						});
					}
				}
			}).show();
		}
	},
	/**保存数据*/
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var price = rec.get('NRequestItem_ItemPrice');
			me.updateOneByParams(id, {
				entity: {
					Id: id,
					ItemPrice: price
				},
				fields: 'Id,ItemPrice'
			});
		}
	},
	/**修改价格*/
	updateOneByParams: function(id, params) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode(params);

		JShell.Server.post(url, params, function(data) {
			var record = me.store.findRecord(me.PKField, id);
			if (data.success) {
				if (record) {
					record.set(me.DelField, true);
					record.commit();
				}
				me.saveCount++;
			} else {
				me.saveErrorCount++;
				if (record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}
			if (me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if (me.saveErrorCount == 0) me.onSearch();
			}
		}, false);
	}
});