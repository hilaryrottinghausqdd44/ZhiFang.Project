/**
 * 对账锁定
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.LockAGrid', {
	extend: 'Shell.class.pki.balance2.ItemGrid',

	title: '对账锁定',

	/**默认条件 or nrequestitem.IsLocked=3*/
	defaultWhere: '(nrequestitem.IsLocked=0 or nrequestitem.IsLocked=1)',
	/**带功能按钮栏*/
	hasButtontoolbar: true,

	/**锁定服务*/
	lockUrl: '/StatService.svc/Stat_UDTO_SelectReconciliationLocking',
	/**锁定的确认内容*/
	lockText: '您确定要对账锁定吗？',
	/**解除锁定的确认内容*/
	openText: '您确定要解除对账锁定吗？',
	/**默认加载*/
	defaultLoad: false,
	/**财务报表类型*/
	reportType: '1',
	/**查询栏参数设置*/
	searchToolbarConfig: {
		/**是否包含财务*/
		hasFinanceLock: true
	},
	initComponent: function() {
		var me = this;

		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			text: '对账锁定',
			iconCls: 'button-lock',
			tooltip: '<b>将选中的记录进行对账锁定</b>',
			handler: function() {
				me.doCheckedLock(false);
			}
		}, '-', {
			text: '解除锁定',
			iconCls: 'button-text-relieve',
			tooltip: '<b>将选中的记录进行解除锁定</b>',
			handler: function() {
				me.doCheckedLock(true);
			}
		}, '-'];

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
			menuDisabled: false,
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
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_GetPriceTime',
			text: '匹配时间',
			width: 120,
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
						meta.tdAttr = 'data-qtip="<b>解除对账锁定</b>"';
						meta.style = 'background-color:green;';
						return 'button-text-relieve hand';
					} else {
						meta.tdAttr = 'data-qtip="<b>对账锁定</b>"';
						meta.style = 'background-color:red;';
						return 'button-lock hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isOpen = rec.get('NRequestItem_IsLocked') == '1' ? true : false;
					me.doLock(id, isOpen);
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
			dataIndex: 'NRequestItem_FirstLocker',
			text: '对账人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_FirstLockedDate',
			text: '对账时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'NRequestItem_LockBatchNumber',
			text: '对账批次号',
			width: 80,
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
			hideable: false,
			isKey: true
		}];

		return columns;
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

		me.doLock(ids.join(","), isOpen);
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