/**
 * 匹配合同价格
 * @author longfc
 * @version 2016-05-10
 */
Ext.define('Shell.class.pki.balance2.MatchContractPriceGrid', {
	extend: 'Shell.class.pki.balance2.ItemGrid',
	title: '匹配合同价格',

	/**默认条件or nrequestitem.IsLocked=3*/
	defaultWhere: '(nrequestitem.IsLocked=0 or nrequestitem.IsLocked=1)',
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**锁定服务*/
	lockUrl: '/StatService.svc/Stat_UDTO_SelectReconciliationLocking',

	/**匹配合同价格:idList:NRequestItem对象的ID列表;isGetPrice:目前只传true;isRedo:重新匹配传true，否则为false*/
	matchContractUrl: '/StatService.svc/Stat_UDTO_SelectMatchNRequestItemContract',
	/**锁定的确认内容*/
	lockText: '您确定要对账锁定吗？',
	/**解除锁定的确认内容*/
	openText: '您确定要解除对账锁定吗？',
	/**财务报表类型*/
	reportType: '1',
	/**默认不加载*/
	defaultLoad: false,
	/**查询栏参数设置*/
	searchToolbarConfig: {
		/**是否包含财务*/
		hasFinanceLock: true
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			text: '匹配合同价格',
			iconCls: 'button-lock',
			hidden: true,
			tooltip: '<b>将选中的记录进行匹配合同价格</b>',
			handler: function() {
				me.doCheckedMatchContract(false);
			}
		}, {
			text: '匹配合同价格',
			iconCls: 'button-lock',
			tooltip: '<b>将选中的记录进行匹配合同价格</b>',
			handler: function() {
				me.doCheckedMatchContract(true);
			}
		}, {
			itemId: 'print5',
			text: '打印送检单位和项目',
			hidden: false,
			iconCls: 'button-print',
			tooltip: '<b>打印送检单位和项目</b>',
			handler: function() {
				var oldReportType = me.reportType;
				me.reportType = 5;
				me.doActionClick = true;
				var url = me.getExcelUrl();
				//还原
				if(me.reportType == 5) {
					me.reportType = 1;
				} else {
					me.reportType = oldReportType;
				}
				//console.log(url);
				window.open(url);
			}
		}, {
			itemId: 'print4',
			text: '打印送检单位、科室和项目',
			hidden: false,
			iconCls: 'button-print',
			tooltip: '<b>打印送检单位、科室和项目</b>',
			handler: function() {
				var oldReportType = me.reportType;
				me.reportType = 4;
				me.doActionClick = true;
				var url = me.getExcelUrl();
				//还原
				if(me.reportType == 4) {
					me.reportType = 1;
				} else {
					me.reportType = oldReportType;
				}
				//console.log(url);
				window.open(url);
			}
		}];

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
			sortable: true,
			width: 100,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.rendererIsGetPriceStyle(value, meta, record, rowIndex, colIndex, store, view);
			}
		}, {
			dataIndex: 'NRequestItem_GetPriceUser',
			text: '匹配人',
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_GetPriceTime',
			text: '匹配时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_BLaboratory_CName',
			text: '送检单位',
			width: 140,
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
			dataIndex: 'NRequestItem_BTestItem_CName',
			text: '项目名称',
			width: 140,
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
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {

					if(record.get('NRequestItem_IsLocked') == '1') {
						meta.tdAttr = 'data-qtip="<b>解除匹配合同价格</b>"';
						meta.style = 'background-color:green;';
						return 'button-text-relieve hand';
					} else {
						meta.tdAttr = 'data-qtip="<b>匹配合同价格</b>"';
						meta.style = 'background-color:red;';
						return 'button-lock hand';
					}

				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isOpen = rec.get('NRequestItem_IsLocked') == '1' ? true : false;
					me.doLock(id, isOpen);
					//me.doCheckedMatchContract(false);
				}
			}]
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
			dataIndex: 'NRequestItem_IsFree',
			text: '是否免单',
			width: 60,
			align: 'center',
			isBool: true,
			type: 'bool'
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
			dataIndex: 'NRequestItem_ItemContPrice',
			text: '合同价',
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
			dataIndex: 'NRequestItem_BDealer_Name',
			text: '经销商',
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
			dataIndex: 'NRequestItem_Id',
			text: '主键ID',
			hidden: true,
			//hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**(重新)匹配合同价格*/
	doCheckedMatchContract: function(isRedo) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if(len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		var idList = [];
		for(var i = 0; i < len; i++) {
			idList.push(records[i].get(me.PKField));
		}
		var msg = isRedo ? "是否执行重新匹配合同价格操作?" : "是否执行匹配合同价格操作";

		JShell.Msg.confirm({
			msg: msg
		}, function(but) {
			if(but != "ok") return;

			var url = (me.matchContractUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.matchContractUrl;

			var params = {
				idList: idList.join(","),
				isGetPrice: true,
				isRedo: (isRedo ? true : false)
			};

			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
				me.hideMask(); //隐藏遮罩层
				if(data.success) {
					if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
					me.onSearch();
				} else {
					if(data.msg == 'ERROR001') {
						data.msg = '提示找不到对应的合同价格，匹配合同价格错误';
					}
					JShell.Msg.error(data.msg);
				}
			}, null, 600000);
		});
	},
	/**锁定选中的数据*/
	doCheckedLock: function(isOpen) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if(len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		var ids = [];
		for(var i = 0; i < len; i++) {
			ids.push(records[i].get(me.PKField));
		}

		//me.doLock(ids.join(","), isOpen);
	},
	/**锁定一条数据*/
	doLock: function(ids, isOpen) {
		var me = this;
		var msg = isOpen ? me.openText : me.lockText;

		JShell.Msg.confirm({
			msg: msg
		}, function(but) {
			if(but != "ok") return;

			var url = (me.lockUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.lockUrl;

			var params = {
				idList: ids,
				isLock: (isOpen ? false : true)
			};

			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
				me.hideMask(); //隐藏遮罩层
				if(data.success) {
					if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
					me.onSearch();
				} else {
					if(data.msg == 'ERROR001') {
						data.msg = '提示找不到对应的合同价格，对账错误';
					}
					JShell.Msg.error(data.msg);
				}
			}, null, 600000);
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		//me.showPrintButtons();
		return me.callParent(arguments);
	}
});