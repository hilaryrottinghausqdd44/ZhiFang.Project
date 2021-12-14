/**
 * 财务收入日报表查询条件
 * @author liangyl	
 * @version 2017-01-23
 */
Ext.define('Shell.class.weixin.report.finance.SearchToolbar', {
	extend: 'Shell.class.weixin.report.basic.SearchToolbar',
	height: 108,
	toolbarHeight: 108,
	/**下载EXCEL文件服务地址*/
	downLoadExcelUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_ExportExcelFinanceIncome',
	/**打印pdf服务地址*/
	downPdfUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_GetFinanceIncomeExcelToPdfFile',
	/**@overwrite 获取列表布局组件内容*/
	getTableLayoutItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		//订单编号
		me.UOFCode.x = 5;
		me.UOFCode.y = 32;
		me.UOFCode.width = 180;
		items.push(me.UOFCode);
		//子订单编号
		me.UOIID.x = 185;
		me.UOIID.y = 32;
		me.UOIID.width = 182;
		items.push(me.UOIID);

		//开单日期
		me.BillingBeginDate.x = 368;
		me.BillingBeginDate.y = 32;
		items.push(me.BillingBeginDate);

		me.BillingEndDate.x = 528;
		me.BillingEndDate.y = 32;
		items.push(me.BillingEndDate);

		//采样日期
		me.SampleBeginDate.x = 635;
		me.SampleBeginDate.y = 32;
		items.push(me.SampleBeginDate);

		me.SampleEndDate.x = 795;
		me.SampleEndDate.y = 32;
		items.push(me.SampleEndDate);

		//退款单号
		me.RefundNO.x = 5;
		me.RefundNO.y = 57;
		me.RefundNO.width = 180;
		items.push(me.RefundNO);

		//转款单号
		me.TransferNO.x = 187;
		me.TransferNO.y = 57;
		me.TransferNO.width = 180;
		items.push(me.TransferNO);

		//是否转款
		me.ISTransfer.x = 368;
		me.ISTransfer.y = 57;
		me.ISTransfer.width = 140;
		items.push(me.ISTransfer);

		//是否退款
		me.ISRefund.x = 520;
		me.ISRefund.y = 57;
		me.ISRefund.width = 140;
		items.push(me.ISRefund);

		//客户
		me.AccountName.x = 5;
		me.AccountName.y = 82;
		me.AccountName.width = 180;
		items.push(me.AccountName);

		//区域
		me.AreaName.x = 187;
		me.AreaName.y = 82;
		me.AreaName.width = 180;
		items.push(me.AreaName);

		//开单医生
		me.DoctorName.x = 368;
		me.DoctorName.y = 82;
		me.DoctorName.width = 180;
		items.push(me.DoctorName);

		return items;
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.getComponent('down').hide();
		me.getComponent('up').show();
		me.OrderHide(true);
		//		me.setHeight(30);
	},
	/**隐藏其他组件*/
	OrderHide: function(bo) {
		var me = this;

		var textList = ['UOFCode', 'UOIID', 'BillingBeginDate', 'BillingEndDate', 'SampleBeginDate', 'SampleEndDate', 'ItemName', 'RefundNO', 'TransferNO'];
		var comboList = ['ISRefund', 'ISTransfer'];
		var checkList = [
			'Area', 'Doctor', 'Item', 'Account'
		];

		for(var i in textList) {
			var text = me.getComponent(textList[i]);
			if(text) {
				if(bo) {
					text.show();
				} else {
					text.hide();
					text.setValue('');
				}
			}
		}
		for(var i in comboList) {
			var combo = me.getComponent(comboList[i]);
			if(combo) {
				if(bo) {
					combo.show();
				} else {
					combo.hide();
					combo.setValue(null);
				}
			}
		}
		for(var i in checkList) {
			var check = me.getComponent(checkList[i] + 'Name');
			var check_Id = me.getComponent(checkList[i] + 'ID');

			if(check) {
				if(bo) {
					check.show();
				} else {
					check.hide();
					if(check) check.setValue('');
					if(check_Id) check_Id.setValue('');
				}
			}
		}
	},
	/**清空查询内容*/
	onClearSearch: function() {
		var me = this,
			BeginDate = me.getComponent('BeginDate'),
			EndDate = me.getComponent('EndDate'),
			YearMonth = me.getComponent('YearMonth'),
			MonthMonth = me.getComponent('MonthMonth');

		var textList = ['AreaID','DoctorID', 'AccountID', 'ItemID','UOFCode', 'UOIID', 'BillingBeginDate', 'BillingEndDate', 'SampleBeginDate', 'SampleEndDate', 'ItemName', 'RefundNO', 'TransferNO'];
		var comboList = ['ISRefund', 'ISTransfer'];
		var checkList = [
			'AreaName', 'DoctorName', 'ItemName', 'AccountName'
		];
		for(var i in textList) {
			var text = me.getComponent(textList[i]);
			if(text) text.setValue('');
		}
		for(var i in comboList) {
			var combo = me.getComponent(comboList[i]);
			if(combo) combo.setValue(null);
		}
		for(var i in checkList) {
			var check = me.getComponent(checkList[i]);
			var check_Id = me.getComponent(checkList[i].split('_')[0] + '_Id');
			if(check) check.setValue('');
			if(check_Id) check_Id.setValue('');
		}
	},
	/**获取参数*/
	getParams: function() {
		var me = this,
			params = {};
		var BeginDate = me.getComponent('BeginDate'),
			EndDate = me.getComponent('EndDate');
		if(BeginDate && BeginDate.getValue()) {
			params.StartDate = JShell.Date.toString(BeginDate.getValue(), true);
		}
		if(EndDate && EndDate.getValue()) {
			params.EndDate = JShell.Date.toString(EndDate.getValue(), true);
		}
		var textList = [ 'UOFCode', 'UOIID', 'BillingBeginDate', 'BillingEndDate', 'SampleBeginDate', 'SampleEndDate', 'ItemName', 'RefundNO', 'TransferNO'];
		var comboList = ['ISRefund', 'ISTransfer'];
		var checkList = [
			'AreaID', 'DoctorID', 'ItemID', 'AccountID'
		];
		params = me.getSearchParams(params, textList, "textList");
		params = me.getSearchParams(params, comboList, "comboList");
		params = me.getSearchParams(params, checkList, "checkList");
		return params;
	},
	/**获取查询实体信息*/
	getEntityJson: function() {
		var me = this,
			entityJson = {};
		var params = me.getParams();
		if(params) {
			entityJson = {
				UOFCode: params.UOFCode,
				OSUserConsumerFormCode: params.UOIID,
				MRefundFormCode: params.RefundNO,
				BonusFormCode: params.TransferNO,
				AreaID: params.AreaID,
				UserAccountID: params.AccountID,
				DoctorAccountID: params.DoctorID,
				IsSettlement: params.ISTransfer ? 1 : 0,
				IsRefund: params.ISRefund ? 1 : 0,
				BillingStartDate: params.BillingBeginDate,
				BillingEndDate: params.BillingEndDate,
				SamplingStartDate: params.SampleBeginDate,
				SamplingEndDate: params.SampleEndDate,
				ConsumptionStartDate: params.StartDate,
				ConsumptionEndDate: params.EndDate
			};
		} else {
			entityJson = null;
		}
		return entityJson;
	},
	/**导出EXCEL文件*/
	onExpExcelClick: function() {
		var me = this,
			entityJson = {},
			operateType = '0';
		var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;
		entityJson = me.getEntityJson();
		if(entityJson != null) {
			url += "?searchEntity=" + Ext.JSON.encode(entityJson) + "&operateType=" + operateType;
			window.open(url);
		}
	},
	/**打印*/
	onDownloadPdf: function() {
		var me = this,
			entityJson = {},
			operateType = '1';
		var url = JShell.System.Path.ROOT + me.downPdfUrl;
		entityJson = me.getEntityJson();
		if(entityJson != null) {
			url += "?searchEntity=" + Ext.JSON.encode(entityJson) + "&operateType=" + operateType;
			me.openPreviewForm(true, url);
		}
	}
});