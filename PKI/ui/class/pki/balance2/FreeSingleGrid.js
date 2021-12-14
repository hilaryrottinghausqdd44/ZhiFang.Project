/**
 * 免单项目
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.FreeSingleGrid', {
	extend: 'Shell.class.pki.balance2.ItemGrid',

	title: '免单',

	/**批量免单保存数据*/
	freeSingleUrl: '/StatService.svc/Stat_UDTO_FreeSingle',
	/**默认条件*/
	defaultWhere: "nrequestitem.IsLocked=1 and (nrequestitem.ItemPriceType='1' or nrequestitem.ItemPriceType='2' or nrequestitem.ItemPriceType='3')",
	/**带功能按钮栏*/
	hasButtontoolbar: true,

	/**取消免单服务地址*/
	openFreeUrl: '/StatService.svc/Stat_UDTO_CancelFreeSingle',

	//	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
	//		clicksToEdit: 1
	//	}),
	/**默认不加载*/
	defaultLoad: false,
	/**查询栏参数设置*/
	searchToolbarConfig: {
		/**对账状态默认值*/
		defaultIsLockedValue: '2',
		/**对账状态列表*/
		IsLockedList: [
			['2', '销售锁定', 'font-weight:bold;color:' + JcallShell.PKI.Enum.IsLockedColor['E2'] + ';']
		],
		/**价格类型列表*/
		ItemPriceTypeList: [
			[0, '全部', 'font-weight:bold;color:black;'],
			['1', '合同价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
			['2', '阶梯价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';'],
			['3', '免单价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E3'] + ';']
		]
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.on({
			beforeedit: function(editor, e) {
				var isFree = e.record.get('NRequestItem_ItemPriceType') == '3';
				if(!isFree) return false;
			}
		});
	},

	initComponent: function() {
		var me = this,
			config = me.searchToolbarConfig || {};
		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.balance2.SearchToolbar', Ext.apply(config, {
			itemId: 'filterToolbar',
			dock: 'top',
			isLocked: true,
			hasFinanceLock: false,
			height: 105
		}))];
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			text: '批量免单',
			iconCls: 'button-text-free',
			tooltip: '<b>批量免单</b>',
			handler: function() {
				me.onFreeClick(null, true);
			}
		}, '-', {
			text: '批量解单',
			iconCls: 'button-text-relieve',
			tooltip: '<b>批量解单</b>',
			handler: function() {
				me.onOpenFreeClick();
			}
		}]; //, '-', 'save'

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'NRequestItem_ReconciliationState',
			align: 'center',
			text: '对帐状态',
			sortable: true,
			width: 80,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.rendererIsFinanceLockedAndIsLocked(value, meta, record, rowIndex, colIndex, store, view);
			}
		}, {
			dataIndex: 'NRequestItem_IsLocked',
			align: 'center',
			text: '对帐状态',
			sortable: false,
			hideable: false,
			menuDisabled: true,
			hidden: true,
			width: 75,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.IsLocked['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.IsLockedColor['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'NRequestItem_IsFinanceLocked',
			align: 'center',
			text: '财务锁定标志',
			sortable: false,
			hideable: false,
			menuDisabled: true,
			hidden: true,
			width: 80,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.IsFinanceLocked['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.IsFinanceLockedColor['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'NRequestItem_IsGetPrice',
			align: 'center',
			text: '匹配状态',
			width: 80,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.rendererIsGetPriceStyle(value, meta, record, rowIndex, colIndex, store, view);
			}
		}, {
			dataIndex: 'NRequestItem_GetPriceUser',
			text: '匹配人',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_GetPriceTime',
			text: '匹配时间',
			width: 120,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_BLaboratory_CName',
			text: '送检单位',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_BDept_CName',
			text: '科室',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_CName',
			text: '病人姓名',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_SerialNo',
			text: '样本预制条码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BarCode',
			text: '实验室条码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BTestItem_CName',
			text: '项目名称',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BillingUnitType',
			align: 'center',
			text: '开票方类型',
			width: 75,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'NRequestItem_BBillingUnit_Name',
			text: '开票方(付款单位)',
			width: 105,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_ItemPriceType',
			text: '价格类型',
			width: 60,
			align: 'center',
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.ItemPriceType['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'NRequestItem_IsFree',
			text: '是否免单',
			width: 60,
			align: 'center',
			isBool: true,
			type: 'bool'
		}, {
			dataIndex: 'NRequestItem_IsFreeType',
			text: '免单类型',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_ItemFreePrice',
			text: '<b style="color:blue;">免单价格</b>',
			width: 60,
			align: 'right',
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
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var type = record.get('NRequestItem_ItemPriceType');
					if(type == '3') {
						meta.tdAttr = 'data-qtip="<b>解除免单</b>"';
						meta.style = 'background-color:green;';
						return 'button-text-relieve hand';
					} else if(type == '1' || type == '2') {
						meta.tdAttr = 'data-qtip="<b>免单</b>"';
						meta.style = 'background-color:red;';
						return 'button-text-free hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isOpen = rec.get('NRequestItem_ItemPriceType') == '3' ? true : false;
					if(isOpen) {
						me.onOpenFreeClick([rec]);
					} else {
						me.onFreeClick([rec], false);
					}
				}
			}]
		}, {
			dataIndex: 'NRequestItem_BDealer_Name',
			text: '经销商',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_ItemContPrice',
			text: '合同价',
			align: 'right',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_SampleState',
			align: 'center',
			text: '样本状态',
			width: 70,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.SampleStateList[value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.SampleStateColor[value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'NRequestItem_CollectDate',
			text: '采样时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'NRequestItem_OperDate',
			text: '录入时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'NRequestItem_ReceiveDate',
			text: '核收时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'NRequestItem_SenderTime2',
			text: '报告时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'NRequestItem_IsStepPrice',
			text: '是否有阶梯价',
			width: 90,
			align: 'center',
			isBool: true,
			type: 'bool'
		}, {
			dataIndex: 'NRequestItem_ItemEditPrice',
			text: '终端价',
			align: 'right',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_ItemStepPrice',
			text: '阶梯价',
			align: 'right',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_ItemPrice',
			text: '应收价',
			align: 'right',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BSeller_AreaIn',
			text: '销售区域',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BSeller_Name',
			text: '销售',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_CoopLevel',
			align: 'center',
			text: '合作分级',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.CoopLevel['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'NRequestItem_BillingUnitInfo',
			text: '个人开票信息',
			width: 85,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**免单操作
	 * 在选择的一批样本中,是否免单如果其中有一个为是时,该批样本都不作处理,提示
	 */
	onFreeClick: function(records, isJudge) {
		var me = this,
			recs = records || me.getSelectionModel().getSelection(),
			len = recs.length;

		if(len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		//判断该批样本中是否免单是否已存在是的样本信息
		if(isJudge && isJudge == true) {
			var isBreak = false;
			for(var i in recs) {
				isBreak = recs[i].get("NRequestItem_IsFree");
				switch(isBreak) {
					case 1:
						isBreak = true;
						break;
					case true:
						isBreak = true;
						break;
					case "1":
						isBreak = true;
						break;
					default:
						isBreak = false;
						break;
				}
				if(isBreak) {
					break;
				}
			}
			if(isBreak) {
				JShell.Msg.error("当前选择的行信息中,是否免单值包含有【是】<br/>请去除不符合条件的行后再操作!");
				return;
			}
		}
		JShell.Win.open('Shell.class.pki.balance2.FreeTypeWin', {
			resizable: false,
			listeners: {
				save: function(win, values) {
					var IsFreeType = values.FreeType;
					var ItemFreePrice = values.ItemFreePrice;
					win.close();

					me.showMask(me.saveText); //显示遮罩层
					me.saveErrorCount = 0;
					me.saveCount = 0;
					me.saveLength = len;

					for(var i in recs) {
						var id = recs[i].get(me.PKField);
						me.FreeSingle(id, IsFreeType, ItemFreePrice, i);
					}
				}
			}
		}).show();
	},
	/**批量免单保存数据*/
	FreeSingle: function(id, freeType, itemFreePrice, index) {
		var me = this;
		var url = (me.freeSingleUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.freeSingleUrl;
		url = url + "?idList=" + id + "&freeType=" + freeType + "&itemFreePrice=" + itemFreePrice;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if(record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},

	/**解除免单操作*/
	onOpenFreeClick: function(records) {
		var me = this,
			recs = records || me.getSelectionModel().getSelection(),
			len = recs.length;

		if(len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		var ids = [];
		for(var i = 0; i < len; i++) {
			ids.push(recs[i].get(me.PKField));
		}

		var msg = "确定要取消免单吗";

		JShell.Msg.confirm({
			msg: msg
		}, function(but) {
			if(but != "ok") return;

			var url = (me.openFreeUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.openFreeUrl;

			url += "?idList=" + ids.join(',');

			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if(data.success) {
					if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
					me.onSearch();
				} else {
					JShell.Msg.error(data.msg);
				}
			});
		});
	},
	/**保存数据*/
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var price = rec.get('NRequestItem_ItemFreePrice');
			me.updateOneByParams(id, {
				entity: {
					Id: id,
					ItemFreePrice: price,
					ItemPrice: price
				},
				fields: 'Id,ItemFreePrice,ItemPrice'
			});
		}
	}
});