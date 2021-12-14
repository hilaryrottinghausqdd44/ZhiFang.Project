/**
 * 个人开票
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.PersonGrid', {
	extend: 'Shell.class.pki.balance2.ItemGrid',

	title: '个人开票',

	/**默认条件*/
	defaultWhere: "nrequestitem.IsLocked=1 and (nrequestitem.ItemPriceType='1' or nrequestitem.ItemPriceType='2' or nrequestitem.ItemPriceType='4')",
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**默认不加载*/
	defaultLoad: false,
	/**取消个人开票服务地址*/
	openPersonUrl: '/StatService.svc/Stat_UDTO_CancelIndividualInvoice',
	/**个人开票批量保存数据*/
	individualInvoiceUrl: '/StatService.svc/Stat_UDTO_IndividualInvoice',
	//	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
	//		clicksToEdit: 1
	//	}),
	/**
	 * 排序字段
	 * 
	 * @exception 
	 * [{property: 'NRequestItem_ReconciliationState',direction: 'ASC'}]
	 */
	defaultOrderBy: [{
		property: 'NRequestItem_ReconciliationState',
		direction: 'ASC'
	}, {
		property: 'NRequestItem_NRequestForm_BLaboratory_CName',
		direction: 'ASC'
	}],
	/**查询栏参数设置*/
	searchToolbarConfig: {
		/**对账状态列表*/
		IsLockedList: [
			['2', '销售锁定', 'font-weight:bold;color:' + JcallShell.PKI.Enum.IsLockedColor['E2'] + ';']
		],
		defaultIsLockedValue: '2',
		/**价格类型列表*/
		ItemPriceTypeList: [
			[0, '全部', 'font-weight:bold;color:black;'],
			['1', '合同价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
			['2', '阶梯价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';'],
			['4', '终端价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E4'] + ';']
		]
	},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.on({
			beforeedit: function(editor, e) {
				var isFree = e.record.get('NRequestItem_ItemPriceType') == '4';
				if(!isFree) return false;
			}
		});
	},

	initComponent: function() {
		var me = this;

		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			text: '个人开票',
			iconCls: 'button-text-person',
			tooltip: '<b>批量个人开票</b>',
			handler: function() {
				me.onAllPersonClick(null, true);
			}
		}, '-', {
			text: '取消个人开票',
			iconCls: 'button-text-relieve',
			tooltip: '<b>批量取消个人开票</b>',
			handler: function() {
				me.onOpenPersonClick();
			}
		}, '-', {
			text: '个人样本批量导入',
			iconCls: 'file-excel',
			tooltip: '<b>批量导入个人样本</b>',
			handler: function() {
				me.onImportExcelClick();
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
			width: 100,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.IsFinanceLocked['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.IsFinanceLockedColor['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'NRequestItem_NRequestForm_BLaboratory_CName',
			text: '送检单位',
			sortable: true,
			width: 140,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_BDept_CName',
			text: '科室',
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_CName',
			text: '病人姓名',
			sortable: true,
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
			sortable: true,
			width: 140,
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
			dataIndex: 'NRequestItem_BillingUnitInfo',
			text: '个人开票信息',
			width: 85,
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
			dataIndex: 'NRequestItem_ItemEditPrice',
			text: '<b style="color:blue;">终端价</b>',
			width: 60,
			align: 'right',
			readOnly: true,
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
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var type = record.get('NRequestItem_ItemPriceType');
					if(type == '4') {
						meta.tdAttr = 'data-qtip="<b>解除个人开票</b>"';
						meta.style = 'background-color:green;';
						return 'button-text-relieve hand';
					} else if(type == '1' || type == '2') {
						meta.tdAttr = 'data-qtip="<b>个人开票</b>"';
						meta.style = 'background-color:red;';
						return 'button-text-person hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isOpen = rec.get('NRequestItem_ItemPriceType') == '4' ? true : false;
					if(isOpen) {
						me.onOpenPersonClick([rec]);
					} else {
						me.onPersonClick([rec]);
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
			dataIndex: 'NRequestItem_IsFreeType',
			text: '免单类型',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_ItemFreePrice',
			text: '免单价格',
			align: 'right',
			width: 60,
			defaultRenderer: true
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
			dataIndex: 'NRequestItem_IsFree',
			text: '是否免单',
			width: 60,
			align: 'center',
			isBool: true,
			type: 'bool'
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
			dataIndex: 'NRequestItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**批量个人开票操作
	 * 在一批样本中,开票方类型如果其中有一个为个人开票时,该批样本都不作处理,提示
	 * */
	onAllPersonClick: function(records, isJudge) {
		var me = this,
			recs = records || me.getSelectionModel().getSelection(),
			len = recs.length;

		if(len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		//是否需要作判断处理
		if(isJudge && isJudge == true) {
			var isBreak = false;
			for(var i in recs) {
				isBreak = recs[i].get("NRequestItem_BillingUnitType");
				switch(isBreak) {
					case "个人":
						isBreak = true;
						break;
					case 3:
						isBreak = true;
						break;
					case "3":
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
				JShell.Msg.error("当前选择的行信息中,开票方类型值包含有【个人开票】<br/>请去除不符合条件的行后再操作!");
				return;
			}
		}
		JShell.Win.open('Shell.class.pki.balance2.PersonForm', {
			formtype: 'add',
			resizable: false,
			listeners: {
				save: function(win, params) {
					var Name = params.Name;
					var Price = params.Price;
					var IsAll = params.IsAll;
					win.close();

					me.showMask(me.saveText); //显示遮罩层
					me.saveErrorCount = 0;
					me.saveCount = 0;
					me.saveLength = len;

					for(var i in recs) {
						var rec = recs[i];
						var id = rec.get(me.PKField);
						var BillingUnitInfo = IsAll ? Name : rec.get('NRequestItem_NRequestForm_CName');

						me.IndividualInvoice(id, BillingUnitInfo, Price, i);

					}
				}
			}
		}).show();
	},

	/**个人开票批量保存数据*/
	IndividualInvoice: function(id, billingUnitInfo, itemEditPrice, index) {
		var me = this;
		var url = (me.individualInvoiceUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.individualInvoiceUrl;
		url = url + "?idList=" + id + "&billingUnitInfo=" + billingUnitInfo + "&itemEditPrice=" + itemEditPrice;
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

	/**个人开票保存操作*/
	onPersonClick: function(records) {
		var me = this,
			recs = records,
			len = recs.length;

		if(len != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var rec = recs[0];
		var name = rec.get('NRequestItem_NRequestForm_CName');
		JShell.Win.open('Shell.class.pki.balance2.OnePersonForm', {
			formtype: 'add',
			resizable: false,
			defaultName: name,
			Price: rec.get('NRequestItem_ItemEditPrice'),
			listeners: {
				save: function(win, params) {
					var Name = params.Name;
					var Price = params.Price;
					win.close();

					me.showMask(me.saveText); //显示遮罩层
					me.saveErrorCount = 0;
					me.saveCount = 0;
					me.saveLength = len;

					var id = rec.get(me.PKField);
					var BillingUnitInfo = Name;
					me.IndividualInvoice(id, BillingUnitInfo, Price, 1);

				}
			}
		}).show();
	},
	/**解除个人开票操作*/
	onOpenPersonClick: function(records) {
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

		var msg = "确定要取消个人开票吗";

		JShell.Msg.confirm({
			msg: msg
		}, function(but) {
			if(but != "ok") return;

			var url = (me.openPersonUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.openPersonUrl;

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
			var price = rec.get('NRequestItem_ItemEditPrice');
			me.updateOneByParams(id, {
				entity: {
					Id: id,
					ItemEditPrice: price,
					ItemPrice: price
				},
				fields: 'Id,ItemEditPrice,ItemPrice'
			});
		}
	},
	/**点击导入按钮处理*/
	onImportExcelClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.pki.excel.FileUpdatePanel', {
			formtype: 'add',
			resizable: false,
			showSuccessInfo: false,
			url: '/StatService.svc/Stat_UDTO_ImportPersonalInvoiceData',
			TableName: 'PersonalInvoice', //N_RequestItem
			ERROR_UNIQUE_KEY_INFO: '预制条码有重复',

			listeners: {
				save: function(form, action) {
					//me.onSearch();
					form.close();
					var resultDataValue = action.result.ResultDataValue;
					if(resultDataValue && resultDataValue != "") {
						resultDataValue = Ext.JSON.decode(resultDataValue); //JShell.Server.toJson(resultDataValue);
					}
					me.openImportPersonalInvoiceDataGrid(resultDataValue);

				}
			}
		}).show();
	},
	openImportPersonalInvoiceDataGrid: function(resultDataValue) {
		var me = this;
		JShell.Win.open('Shell.class.pki.balance2.ImportPersonalInvoiceDataGrid', {
			resizable: true,
			datas: resultDataValue.list,
			listeners: {
				save: function(win, params) {
					me.onSearch();
				}
			}
		}).show();
	}
});